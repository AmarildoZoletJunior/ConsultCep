using ConsultCep.Domain.DTO;
using ConsultCep.Domain.DTOs;
using ConsultCep.Domain.Entities;
using ConsultCep.Domain.Interfaces;
using ConsultCep.Domain.Validador;
using ConsultCep.Utils.ClassUtils;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsultCep.Domain.Repositories
{
    public class CepRepository : ICepRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMessengerRepository _messengerRepository;

        public CepRepository(IHttpClientFactory httpClientFactory,IMessengerRepository messenger)
        {
            _httpClientFactory = httpClientFactory;
            _messengerRepository = messenger;
        }

        public async Task<Response> BuscarInfoCep(string cep)
        {
            var requestValidacao = new CepDTORequest { CEP = cep };
            Response responseClass = new Response();
            CepValidador Validator = new CepValidador();

            var validResponse = Validator.Validate(requestValidacao);
            if (!validResponse.IsValid)
            {
                validResponse.Errors.ForEach(X => responseClass.AdicionarMensagem("CepValidador", X.ErrorMessage));
                return responseClass;
            }

            var http = _httpClientFactory.CreateClient("ViaCepApi");
            var response = await http.GetAsync($"{cep}/json/");
            if (!response.IsSuccessStatusCode)
            {
                responseClass.AdicionarMensagem("Ocorreu um erro", "Erro não mapeado");
                return responseClass;
            }

            CepEntity CepObject = await response.Content.ReadFromJsonAsync<CepEntity>();
            if (CepObject?.cep == null)
            {
                responseClass.AdicionarMensagem("Cep não encontrado", "Este CEP não foi encontrado");
                return responseClass;
            }

            var mapeado = new CepResponseDTO { Bairro = CepObject.bairro, CodigoPostal = CepObject.cep, Complemento = CepObject.complemento, Gia = CepObject.gia, Localidade = CepObject.localidade, NumeroDDD = int.Parse(CepObject.ddd), Populacao = int.Parse(CepObject.ibge), Siafi = int.Parse(CepObject.siafi), UnidadeFederativa = CepObject.uf, Endereco = CepObject.logradouro };
            responseClass.AdicionarDados(mapeado);


            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                 Port = 5672
            };

           //await _messengerRepository.InviteMessage(mapeado, factory);


           return responseClass;
        }
    }
}

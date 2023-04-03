using ConsultCep.Domain.DTO;
using ConsultCep.Domain.DTOs;
using ConsultCep.Domain.Entities;
using ConsultCep.Domain.Interfaces;
using ConsultCep.Domain.Validador;
using System.Net.Http.Json;

namespace ConsultCep.Domain.Repositories
{
    public class CepRepository : ICepRepository
    {
        public async Task<Response> BuscarInfoCep(CepDTORequest cep)
        {
            Response responseClass = new Response();
            CepValidador Validator = new CepValidador();
            var validResponse = Validator.Validate(cep);
            if (!validResponse.IsValid)
            {
               validResponse.Errors.ForEach(X => responseClass.AdicionarMensagem("CepValidador", X.ErrorMessage));
               return responseClass;
            }
            HttpClient client = new HttpClient();
            string url = $"https://viacep.com.br/ws/{cep.CEP}/json/";
            HttpResponseMessage response = await client.GetAsync(url);

            if(!response.IsSuccessStatusCode)
            {
                responseClass.AdicionarMensagem("Ocorreu um erro", "Erro não mapeado");
                return responseClass;
            }

            CepEntity CepObject = await response.Content.ReadFromJsonAsync<CepEntity>();
            if (CepObject.cep == null)
            {
                responseClass.AdicionarMensagem("Cep não encontrado", "Este CEP não foi encontrado");
                return responseClass;
            }

            var mapeado = new CepResponseDTO { Bairro = CepObject.bairro, CodigoPostal = CepObject.cep, Complemento = CepObject.complemento, Gia = CepObject.gia, Localidade = CepObject.localidade, NumeroDDD = int.Parse(CepObject.ddd), Populacao = int.Parse(CepObject.ibge), Siafi = int.Parse(CepObject.siafi), UnidadeFederativa = CepObject.uf, Endereco = CepObject.logradouro };
            responseClass.AdicionarDados(mapeado);

            return responseClass;
        }
    }
}

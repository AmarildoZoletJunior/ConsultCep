using ConsultCep.API.Controllers;
using ConsultCep.Domain.DTOs;
using ConsultCep.Domain.Interfaces;
using ConsultCep.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.Core.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsultCep.Testes
{
    public class CepControllerTeste
    {
        private readonly ICepRepository _cepRepository;
        private readonly CepController _sut;

        public CepControllerTeste()
        {
            this._cepRepository = Substitute.For<ICepRepository>();
            this._sut = new CepController(_cepRepository);
        }

        [Fact]
        public async Task TesteValidarControllerRetornaBadRequest()
        {
            Response response = new Response();
            response.AdicionarMensagem("CepValidator", "O campo deve conter no mínimo 8 números.");
            
            _cepRepository.BuscarInfoCep("8927000").Returns(response);

            var resultado = await _sut.GetAsync("8927000");

            var resultadoOk = resultado as BadRequestObjectResult;
            ///Assert
            Assert.NotNull(resultadoOk);
        }

        [Fact]
        public async Task TesteValidarControllerRetornaOk()
        {
            Response response = new Response();

            _cepRepository.BuscarInfoCep("89270000").Returns(response);

            var resultado = await _sut.GetAsync("89270000");

            var resultadoOk = resultado as OkObjectResult;
            ///Assert
            Assert.NotNull(resultadoOk);
        }
    }
}

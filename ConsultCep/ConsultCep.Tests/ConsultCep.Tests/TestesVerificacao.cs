using ConsultCep.Domain.DTOs;
using ConsultCep.Domain.Repositories;

namespace ConsultCep.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }


        [Test]
        public async Task VerificarSeRetornaObjetoComCepDesejadoAsync()
        {
            CepRepository repository = new CepRepository();
            CepDTORequest request = new CepDTORequest { CEP = "89270000"};
            var resultado = await repository.BuscarInfoCep(request);
            Assert.AreEqual("89270-000", resultado.Dados.CodigoPostal);
            Assert.True(resultado.EstaValido);
        }

        [Test]
        public async Task VerificarSeRetornaErroDigitoMenorQue8Caracteres()
        {
            CepRepository repository = new CepRepository();
            CepDTORequest request = new CepDTORequest { CEP = "8927000" };
            var resultado = await repository.BuscarInfoCep(request);
            Assert.True(resultado.Mensagens.Select(x => x.Mensagem).Contains("O campo deve conter no mínimo 8 números."));
            Assert.False(resultado.EstaValido);
        }

        [Test]
        public async Task VerificarSeRetornaErroDigitoMaiorQue8Caracteres()
        {
            CepRepository repository = new CepRepository();
            CepDTORequest request = new CepDTORequest { CEP = "892700000" };
            var resultado = await repository.BuscarInfoCep(request);
            Assert.True(resultado.Mensagens.Select(x => x.Mensagem).Contains("O campo deve conter apenas 8 números."));
        }
    }
}
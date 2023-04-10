using ConsultCep.Domain.Interfaces;
using ConsultCep.Domain.Repositories;
using NSubstitute;

namespace ConsultCep.Testes
{
    public class CepRepositorioTeste
    {
        private readonly IHttpClientFactory _httpClientFactory = Substitute.For<IHttpClientFactory>();
        private readonly CepRepository _sut;
        private readonly IMessengerRepository messengerRepository = Substitute.For<IMessengerRepository>();

        public CepRepositorioTeste()
        {
            _sut = new CepRepository(_httpClientFactory, messengerRepository);
        }

        [Fact]
        public async Task TesteValidarBoolECepRetornadoSeCondizComPesquisado()
        {

            _httpClientFactory.CreateClient("ViaCepApi").Returns(new HttpClient { BaseAddress = new Uri("https://viacep.com.br/ws/") });
            var result = await _sut.BuscarInfoCep("35182016");

            ///Assert
            Assert.Equal("35182-016", result.Dados.CodigoPostal);
        }
    }
}
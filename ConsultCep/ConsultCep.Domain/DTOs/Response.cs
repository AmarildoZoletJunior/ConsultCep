using ConsultCep.Domain.DTO;

namespace ConsultCep.Domain.DTOs
{
    public class Response
    {
        public bool EstaValido { get; set; }
        public CepResponseDTO Dados { get; set; }
        public List<MensagemErro> Mensagens { get; set; } = new List<MensagemErro>();

        public Response() 
        {
            EstaValido = true;
        }   
        public void AdicionarMensagem(string titulo,string mensagem)
        {
            Mensagens.Add(new MensagemErro(titulo,mensagem));
            this.ValidarResponse();
        }
        public void ValidarResponse()
        {
            if(this.Mensagens.Count > 0)
            {
                this.EstaValido = false;
            }
        }
        public void AdicionarDados(CepResponseDTO cepResponse)
        {
            this.Dados = cepResponse;
        }
    }
}

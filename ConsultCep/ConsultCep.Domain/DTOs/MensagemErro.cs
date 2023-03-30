namespace ConsultCep.Domain.DTOs
{
    public class MensagemErro
    {
        public string TituloMensagem { get; set; }
        public string Mensagem { get; set; }
        public MensagemErro(string titulo,string mensagem)
        {
            this.Mensagem = mensagem;
            this.TituloMensagem= titulo;
        }
    }
}

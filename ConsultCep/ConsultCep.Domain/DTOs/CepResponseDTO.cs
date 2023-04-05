namespace ConsultCep.Domain.DTO
{
    [Serializable]
    public class CepResponseDTO
    {
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string UnidadeFederativa { get; set; }
        public string Endereco { get; set; }
        public string Localidade { get; set; }
        public int NumeroDDD { get; set; }
        public string CodigoPostal { get; set; }
        public int Populacao { get; set; }
        public int Siafi { get; set; }
        public string Gia { get; set; }
    }
}

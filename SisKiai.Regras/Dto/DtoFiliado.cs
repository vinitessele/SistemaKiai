using System;
using System.Linq;

namespace SisKiai.Regras.Dto
{
    public class DtoFiliado
    {
        public long IdFiliado { get; set; }
        public long? NumeroRegistro { get; set; }
        public string NomeFiliado { get; set; }
        public string EnderecoFiliado { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public string EmailFiliado { get; set; }
        public string CpfFiliado { get; set; }
        public string RgFiliado { get; set; }
        public DateTime DataNascimento { get; set; }
        public long IdadeFiliado { get; set; }
        public decimal Altura { get; set; }
        public string SexoFiliado { get; set; }
        public decimal Peso { get; set; }
        public string CepFiliado { get; set; }
        public long IdAssociacao { get; set; }
        public long IdCidade { get; set; }
        public string NomeAssociacao { get; set; }
        public string StatusFiliado { get; set; }
        public string NomeCidade { get; set; }
        public string DescGraducacao { get; set; }
        public long? IdGraduacao { get; set; }
        public byte[] imagem { get; set; }
        public string UrlImagem { get; set; }
    }
}

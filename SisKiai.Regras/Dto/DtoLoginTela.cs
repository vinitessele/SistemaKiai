using System;
using System.Linq;

namespace SisKiai.Regras.Dto
{
    public class DtoLoginTela
    {
        public long IdLoginTela { get; set; }
        public long IdLogin { get; set; }
        public long IdTela { get; set; }
        public string NomeTela { get; set; }
        public string EnderecoTela { get; set; }
    }
}

using System;
using System.Linq;

namespace SisKiai.Regras.Dto
{
    public class DtoGraduacaoFiliado
    {
        public long IdGraduacaoFiliado { get; set; }
        public long IdGraduacao { get; set; }
        public string Descricao { get; set; }
        public DateTime DataGraduacao { get; set; }
        public long IdFiliado { get; set; }
        public string NomeFiliado { get; set; }
        public bool? Status { get; set; }
        public string StatusAtivo { get; set; }
    }
}

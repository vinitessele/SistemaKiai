using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
    public class DtoAcademia
    {
        public long IdAcademia { get; set; }
        public long idInscriAssociacao { get; set; }
        public string NomeAcademia { get; set; }
        public string Sigla { get; set; }
        public string EnderecoAcademia { get; set; }
        public string ResponsavelAcademia { get; set; }
        public string TelefoneFixoAcademia { get; set; }
        public string TelefoneCelularAcademia { get; set; }
        public string CnpjAcademia { get; set; }
        public string InscriAcademia { get; set; }
        public long IdCidadeAcademia { get; set; }
        public long IdEsporte { get; set; }
        public string CepAcademia { get; set; }
        public string EmailAcademia { get; set; }
    }
}

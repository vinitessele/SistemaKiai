using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
    public class DtoDadosAtleta
    {
        public int? IdAtleta { get; set; }
        public string NomeAtleta { get; set; }
        public int IdadeAtleta { get; set; }
        public DateTime DataNascAtleta { get; set; }
        public string SexoAtleta { get; set; }
        public string PesoAtleta { get; set; }
        public string GraduacaoAtleta { get; set; }
        public string AlturaAtleta { get; set; }
        public string TipoInscricao { get; set; }
    }
}

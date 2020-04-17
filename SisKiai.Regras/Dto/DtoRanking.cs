using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
    public class DtoRanking
    {
        public long IdRanking { get; set; }
        public string NomeRanking { get; set; }
        public long? IdFiliadoRanking { get; set; }
        public long? AnoRanking { get; set; }
        public long? PontoRanking { get; set; }
        public bool? StatusRanking { get; set; }
        public long IdAssociacao { get; set; }
    }
}

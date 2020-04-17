using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
    public class DtoCategoria
    {
        public long IdCategoria { get; set; }
        public long IdCategoriaCompeticao { get; set; }
        public int? NumeroCategoria { get; set; }
        public long IdEsporte { get; set; }
        public string NmEsporte { get; set; }
        public long IdGraduacaoInicial { get; set; }
        public long IdGraduacaoFinal { get; set; }
        public string DescricaoCategoria { get; set; }
        public string SexoCategoria { get; set; }
        public string TpCategoria { get; set; }
        public string StringtpCompeticao { get; set; }
        public long? TpCompeticao { get; set; }
        public string TpCompeticaoDescricao { get; set; }
        public decimal AlturaInicial { get; set; }
        public decimal AlturaFinal { get; set; }
        public decimal PesoInicial { get; set; }
        public decimal PesoFinal { get; set; }
        public bool StatusCategoria { get; set; }
        public int IdadeInicial { get; set; }
        public int IdadeFinal { get; set; }
    }
}

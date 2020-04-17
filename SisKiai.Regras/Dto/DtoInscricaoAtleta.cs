using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
    public class DtoInscricaoAtleta
    {
        public long IdInscricao { get; set; }
        public long IdInscricaoAtleta { get; set; }
        public long? IdAtleta { get; set; }
        public long? IdAssociacao { get; set; }
        public long? IdCompeticao { get; set; }
        public long? NumeroRegistro { get; set; }
        public long IdCategoria { get; set; }
        public long? NumeroCategoria { get; set; }
        public decimal ValorInscricao { get; set; }
        public string NomeAtleta { get; set; }
        public string NomeCategoria { get; set; }
        public string TipoInscricao { get; set; }
        public string TipoCompeticao { get; set; }
    }
}

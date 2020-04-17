using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
    public class DtoTipoCompeticao
    {
        public long IdTipoCompeticao { get; set; }
        public string DescricaoCompeticao { get; set; }
        public long IdEsporte { get; set; }
        public string NomeEsporte { get; set; }
    }
}

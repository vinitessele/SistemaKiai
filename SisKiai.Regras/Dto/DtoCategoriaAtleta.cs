using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
    public class DtoCategoriaAtleta
    {
        public long? IdFiliado { get; set; }
        public long? IdCategoriaCompeticao { get; set; }
        public long? IdCategoriaAtleta { get; set; }
        public long? IdinscriaAtleta { get; set; }
        public long? NumRegistro { get; set; }
        public string NomeFiliado { get; set; }
        public string NomeAcademia { get; set; }
        public string NomeCategoria { get; set; }
        public string SiglaAssociacao { get; set; }
        public long? PosicaoSorteio { get; set; }
        public long? ResultadoCompeticao { get; set; }
        public long? IdSorteio { get; set; }
        public string TipoCompeticao { get; set; }
    }
}

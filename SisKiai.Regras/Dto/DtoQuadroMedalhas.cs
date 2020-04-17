using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
    public class DtoQuadroMedalhas
    {
        public long? IdAssociacao { get; set; }
        public string NomeAssociacao { get; set; }
        public long? IdInscriAtleta { get; set; }
        public long? ResultadoFinal { get; set; }
        public long? Primeiro { get; set; }
        public long? Segundo { get; set; }
        public long? Terceiro { get; set; }
        public long? Quarto { get; set; }
        public long? Quinto { get; set; }
        public long? TotalMedalhas { get; set; }
        public long? TotalPontos { get; set; }

    }
}

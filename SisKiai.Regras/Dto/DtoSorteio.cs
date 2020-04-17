using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
   public class DtoSorteio
    {
       public long? IdCategoria { get; set; }
       public string NomeAtleta { get; set; }
       public long? IdCompeticao { get; set; }
       public long? IdEsporte { get; set; }
       public long? IdFiliado { get; set; }
       public long? IdResultadoSorteio { get; set; }
       public long? PosicaoSorteio { get; set; }
       public long? ResultadoFinal { get; set; }
       public long? IdSorteio { get; set; }
       public long? IdAssociacao { get; set; }
       public long? IdInscriAtleta { get; set; }
    }
}

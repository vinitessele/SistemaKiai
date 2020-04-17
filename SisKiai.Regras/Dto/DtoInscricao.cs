using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
   public class DtoInscricao
    {
       public long IdInscricao { get; set; }
       public long IdAssociacao { get; set; }
       public DateTime DataInscricao { get; set; }
       public bool StatusInscricao { get; set; }
       public long IdCompeticao { get; set; }
       public long IdEsporte { get; set; }
       public long IdLogin { get; set; }
       public decimal ValorTotal { get; set; }
       public string NomeCompeticao { get; set; }
    }
}

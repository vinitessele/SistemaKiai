using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
   public class DtoMensagem
    {
       public long Id { get; set; }
       public string Mensagem { get; set; }
       public string Finalidade { get; set; }
       public DateTime? DataLimite { get; set; }
    }
}

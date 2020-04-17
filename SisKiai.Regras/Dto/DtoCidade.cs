using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
   public class DtoCidade
    {
       public long IdCidade { get; set; }
       public string NomeCidade { get; set; }
       public long? IdEstado { get; set; }
       public string CepCidade { get; set; }
       public string SiglaEstado { get; set; }
    }
}

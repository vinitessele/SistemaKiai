using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
   public class DtoGraduacao
    {
       public long IdGraduacao { get; set; }
       public string DescricaoGraduacao { get; set; }
       public string NomeEsporte { get; set; }
       public long IdEsporte { get; set; }
    }
}

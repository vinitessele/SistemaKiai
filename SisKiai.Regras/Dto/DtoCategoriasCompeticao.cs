using System;
using System.Linq;

namespace SisKiai.Regras.Dto
{
   public class DtoCategoriasCompeticao
    {
       public long IdCategoriaCompeticao { get; set; }
       public long IdCompeticao { get; set; }
       public long IdCategoria { get; set; }
       public long IdEsporte { get; set; }
       public string NomeCategoria { get; set; }
       public long NrCategoria { get; set; }
       public long QteAtletasCategoria { get; set; }
       public bool Categoria_Finalizada { get; set; }
       public string Categoria_Finalizada2 { get; set; }
       public string TipoCompeticao { get; set; }
    }
}

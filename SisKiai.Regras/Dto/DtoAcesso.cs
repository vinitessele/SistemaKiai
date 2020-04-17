using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai.Regras.Dto
{
    public class DtoAcesso
    {
        public long IdAcesso { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool? Administrador { get; set; }
        public bool? StatusLogin { get; set; }
        public long? IdAssociacao { get; set; }
    }
}

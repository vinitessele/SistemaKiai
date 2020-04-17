using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SisKiai
{
    public class DtoImg
    {
        public long Id { get; set; }
        public byte[] Imagem { get; set; }
        public string Mensagem { get; set; }
        public string Url { get; set; }
        }
}

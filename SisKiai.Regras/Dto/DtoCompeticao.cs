using System;
using System.Linq;

namespace SisKiai.Regras.Dto
{
   public class DtoCompeticao
    {
       public long IdCompeticao { get; set; }
       public long IdEsporte { get; set; }
       public string NomeCompeticao { get; set; }
       public string DescricaoCompeticao { get; set; }
       public long IdCidade { get; set; }
       public string NomeCidade { get; set; }
       public string EnderecoCompeticao { get; set; }
       public string Cep { get; set; }
       public string NomeResponsavel { get; set; }
       public string TelefoneResponsavel { get; set; }
       public DateTime DataCompeticao { get; set; }
       public DateTime DataLimiteInscricao { get; set; }
       public decimal ValorCompeticao { get; set; }
       public bool? StatusCompeticao { get; set; }
       public bool? PermiteNaoFiliado { get; set; }
       public bool? ContapRanking { get; set; }
       public string PermiteNaoFiliadoDescricao { get; set; }
       public long Primeiro { get; set; }
       public long Segundo { get; set; }
       public long Terceiro { get; set; }
       public long Quarto { get; set; }
       public long Quinto { get; set; }
       public long? Participacao { get; set; }
       public byte[] imagem { get; set; }
       public string UrlImagem { get; set; }
    }
}

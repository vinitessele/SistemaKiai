using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SisKiai.Regras
{
    public class PDF
    {
        public byte[] GerarPdf(string dados, string nomeArquivo)
        {
            dados = dados.Replace("<br>","\n");
            string[] linhas = dados.Split('\n');

            string retorno = string.Empty;
            MemoryStream stream = new MemoryStream();
            Document documento = new Document(PageSize.A4, 36, 36, 0, 10);
            PdfWriter writer = PdfWriter.GetInstance(documento, stream);
            documento.AddTitle(nomeArquivo);
            documento.Open();
            documento.Add(new Paragraph(nomeArquivo, FontFactory.GetFont("Arial", 13)));
            foreach (var l in linhas)
            {
                documento.Add(new Paragraph(l, FontFactory.GetFont("Arial", 10)));
            }
            documento.Close();

            byte[] bytes = stream.ToArray();

            return bytes;
        }
    }
}

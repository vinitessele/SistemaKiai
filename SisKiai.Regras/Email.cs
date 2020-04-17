using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;

namespace SisKiai.Regras
{
    public class Email
    {
        public void EnviaEmail(string Para, string De, string CC, string Msg, string Assunto, byte[] anexo, bool pdf)
        {
            try
            {
                SmtpClient oEnviar = new SmtpClient();
                oEnviar.Port = 587;
                oEnviar.Host = "smtp.fprke.com.br";
                oEnviar.UseDefaultCredentials = true;
                oEnviar.Credentials = new System.Net.NetworkCredential("mensagem@fprke.com.br", "fprke1q2w3e4r");
                oEnviar.EnableSsl = false;
                oEnviar.Timeout = 10000;

                MailMessage oEmail = new MailMessage();
                oEmail.To.Add(Para);
                oEmail.From = new MailAddress(De.ToLower());
                if (!string.IsNullOrWhiteSpace(CC))
                    oEmail.CC.Add(CC);
                oEmail.Bcc.Add("vinicius_tessele@hotmail.com");
                oEmail.Bcc.Add("marioronei@hotmail.com");
                oEmail.Priority = MailPriority.Normal;
                oEmail.IsBodyHtml = true;
                oEmail.Subject = Assunto;
                string NomeArquivo = Assunto.Trim() + ".pdf";

                Byte[] arquivo;
                if (anexo == null)
                {
                    if (pdf)
                    {
                        PDF gerar = new PDF();
                        arquivo = gerar.GerarPdf(Msg, Assunto);
                        oEmail.Attachments.Add(new Attachment(new MemoryStream(arquivo), NomeArquivo));
                    }
                }
                else
                {
                    oEmail.Attachments.Add(new Attachment(new MemoryStream(anexo), NomeArquivo));
                }

                Msg += "\n\n Att ";
                Msg += "\n\nSistema Kiai de Competição";

                oEmail.Body = Msg;

                oEnviar.Send(oEmail);
                oEmail.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

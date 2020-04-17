using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web;
using System.Drawing;
using System.Collections.Generic;
using SisKiai.Regras.Dto;

namespace SisKiai
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Regras.Regras mensagem = new Regras.Regras();

            string msg = mensagem.GetMensagemTodos();
            if (!string.IsNullOrWhiteSpace(msg))
            {
                Label1.Text = msg;
                Label1.ForeColor = Color.Red;
            }

            List<DtoImg> img = new List<DtoImg>();
            //img = mensagem.GetImagem();

            foreach (var i in img)
            {
                if (i.Imagem != null)
                {
                    addImagem(i);
                }
            };
            List<DtoCompeticao> listCompeticao = new List<DtoCompeticao>();
            //listCompeticao = mensagem.GetCompeticaoAtiva();

            foreach (var Li in listCompeticao)
            {
                if (Li.imagem != null)
                {
                    addImagem2(Li);
                }
            };
        }

        private void addImagem2(DtoCompeticao Li)
        {
            System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();

            if (Li.imagem != null)
            {
                byte[] bytes = Li.imagem;
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                string url = "data:image/png;base64," + base64String;
                img.ImageUrl = url;
                img.Height = 300;
                img.Width = 250;
                img.Attributes["title"] = Li.NomeCompeticao + " - " + Li.NomeResponsavel;
                img.Attributes["alt"] = Li.NomeCompeticao + " - " + Li.NomeResponsavel + " - " + Li.TelefoneResponsavel;
            }
            else
            {
                string url = "~/imagens/FPRKE_Pequeno.JPG";
                img.ImageUrl = url;
                img.Height = 300;
                img.Width = 250;
                img.Attributes["title"] = Li.NomeCompeticao + " - " + Li.NomeResponsavel;
                img.Attributes["alt"] = Li.NomeCompeticao + " - " + Li.NomeResponsavel + " - " + Li.TelefoneResponsavel;
            }
           // slide2.Controls.Add(img);
        }

        private void addImagem(DtoImg i)
        {
            System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();

            byte[] bytes = i.Imagem;
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

            string url = "data:image/png;base64," + base64String;
            img.ImageUrl = url;
            img.Height = 577;
            img.Width = 397;
            img.Attributes["title"] = i.Mensagem;
            //slide1.Controls.Add(img);
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            Regras.Dto.DtoAcesso login = new Regras.Dto.DtoAcesso();

            login.Login = Login1.UserName;
            login.Senha = Login1.Password;

            Regras.Regras confere = new Regras.Regras();
            var resultado = confere.ConfereUsuarioSenha(login);

            if (resultado != null)
            {
                HttpCookie cookie = new HttpCookie("Informacao");
                cookie.Values.Add("IdUser", resultado.IdAcesso.ToString());
                cookie.Values.Add("User", resultado.Nome.ToString());
                cookie.Values.Add("Administrador", resultado.Administrador.ToString());
                cookie.Values.Add("IdAssociacao", resultado.IdAssociacao.ToString());
                TimeSpan somarTempo = new TimeSpan(0, 0, 60, 0);
                cookie.Expires = DateTime.Now + somarTempo;
                Response.Cookies.Add(cookie);

                Session["IdUser"] = resultado.IdAcesso;
                Session["User"] = resultado.Nome;
                Session["Administrador"] = resultado.Administrador;
                Session["IdAssociacao"] = resultado.IdAssociacao;
                Response.Redirect("Default2.aspx");
            }
        }
    }
}
using System;
using System.Web;
using System.Drawing;
using System.Collections.Generic;

namespace SisKiai
{
    public partial class Default2 : System.Web.UI.Page
    {
        public bool adm = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    HttpCookie cookie = Request.Cookies["Informacao"];
                    if (cookie.Value != null)
                    {
                        Session["IdUser"] = cookie["IdUser"];
                        Session["User"] = cookie["User"];
                        Session["Administrador"] = cookie["Administrador"];
                        string usuario = Session["User"].ToString();
                        string idUsuario = Session["IdUser"].ToString();
                        adm = bool.Parse(Session["Administrador"].ToString());

                        string id = idUsuario;
                        Regras.Regras confere = new Regras.Regras();
                        var resultado = confere.GetAcessoPorId(id);

                        Session["User"] = resultado.Nome;
                        Session["IdUser"] = resultado.IdAcesso;
                        Session["Administrador"] = resultado.Administrador;
                        Session["IdAssociacao"] = resultado.IdAssociacao;
                    }

                    if (Session["User"] != null)
                    {
                        Label1.Text += Session["User"].ToString();
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }

                    adm = bool.Parse(Session["Administrador"].ToString());
                    if (!adm)
                    {
                        BtnCalculaIdade.Visible = false;
                    }
                    else
                    {
                        BtnCalculaIdade.Visible = true;
                    }

                    Regras.Regras mensagem = new Regras.Regras();
                    string msg = mensagem.GetMensagemFiliados();

                    if (!string.IsNullOrWhiteSpace(msg))
                    {
                        Label3.Text = msg;
                        Label3.ForeColor = Color.Red;
                    }

                    List<DtoImg> img = new List<DtoImg>();

                    img = mensagem.GetImagem();

                    foreach (var i in img)
                    {
                        if (i.Imagem != null)
                        {
                            addImagem(i);
                        }
                    };
                }
                catch (Exception)
                {
                    Response.Redirect("Default2.aspx");
                }
            }
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
           // slide.Controls.Add(img);
        }

        protected void CalcularIdade(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras regras = new Regras.Regras();
                regras.CalculaIdadeFiliados();
            }
            catch (Exception ex)
            {
                FailureText.Text = ex.Message;
            }
        }
    }
}
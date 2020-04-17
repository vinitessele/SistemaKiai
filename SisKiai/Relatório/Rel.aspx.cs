using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace SisKiai.Relatorio
{
    public partial class Rel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string idFiliado = Request.QueryString["id"];
                    Regras.Regras get = new Regras.Regras();
                    Regras.Dto.DtoFiliado filiado = get.GetFiliadoPorId(int.Parse(idFiliado));

                    if (filiado != null)
                    {
                        if (filiado.imagem != null)
                        {
                            byte[] bytes = filiado.imagem;
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            Image7.ImageUrl = "data:image/png;base64," + base64String;
                            Image7.Width = 120;
                            Image7.Height = 154;
                        }
                    }

                    string NRegistro = filiado.NumeroRegistro.ToString();
                    string Nome = filiado.NomeFiliado.ToString();
                    string Cidade = filiado.NomeCidade.ToString();
                    string DataNasc = filiado.DataNascimento.ToShortDateString();
                    string Graduacao = filiado.DescGraducacao.ToString() != null ? filiado.DescGraducacao.ToString() : string.Empty;
                    string Associacao = filiado.NomeAssociacao;

                    litNumeroRegistro.Text = string.Format("N°.: {0} ", NRegistro);
                    LitNome.Text = string.Format("Nome.: {0} ", Nome);
                    LitDtNascimento.Text = string.Format("Dt Nascimento.: {0} ", DataNasc);
                    //LitCidade.Text = string.Format("Cidade.: {0} ", Cidade);
                    LitGraduacao.Text = string.Format("Graduação.: {0} ", Graduacao);
                    LitAssociacao.Text = string.Format("Assoc.:{0}", Associacao);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
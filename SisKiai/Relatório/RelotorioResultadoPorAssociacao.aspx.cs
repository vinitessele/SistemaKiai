using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net;

namespace SisKiai.Relatorio
{
    public partial class RelotorioResultadoPorAssociacao : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["IdAssociacao"] = Request.QueryString["IdAssociacao"];
                Session["idcompeticao"] = Request.QueryString["IdCompeticao"];
                PreencheCabecalho();
                ListaAtletas();

            }
        }

        private void ExportaPDF()
        {

            string url = Request.Url.ToString();

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
            GridViewResultado.RenderControl(htmlWriter);
            string s = stringWriter.ToString();
            Response.Write(s);
            byte[] bytes = null;
            Regras.Email envia = new Regras.Email();
            envia.EnviaEmail("presidente@fprke.com.br", "mensagem@fprke.com.br", "vinicius_tessele@hotmail.com", s, "Resultado por Associação", bytes,false);
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        private void ListaAtletas()
        {
            Regras.Regras get = new Regras.Regras();
            List<DtoCategoriaAtleta> list = get.GetAllResultAcademiaAtletas(Session["IdAssociacao"].ToString(), Session["idcompeticao"].ToString());
            GridViewResultado.DataSource = list;
            GridViewResultado.DataBind();
        }

        private void PreencheCabecalho()
        {
            Regras.Regras get = new Regras.Regras();
            DtoCompeticao competicao = get.GetCompeticaoPorId(int.Parse(Session["idcompeticao"].ToString()));
            DtoAcademia academia = get.GetAcademiaPorId(int.Parse(Session["IdAssociacao"].ToString()));
            Label1.Text = string.Format("<b> {1} - Cidade.: {2} </b>", competicao.IdCompeticao, competicao.NomeCompeticao, competicao.NomeCidade);
            Label2.Text = academia.NomeAcademia;
            Session["nomeAssociacao"] = academia.NomeAcademia;
            Session["Competicao"] = string.Format("<b> {1} - Cidade.: {2} </b>", competicao.IdCompeticao, competicao.NomeCompeticao, competicao.NomeCidade);
        }

        protected void BntenviarClick(object sender, EventArgs e)
        {
            ExportaPDF();
        }
    }
}
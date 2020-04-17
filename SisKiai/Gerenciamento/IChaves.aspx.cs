using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Diagnostics;
using SelectPdf;


namespace SisKiai.Gerenciamento
{
    public partial class IChaves : System.Web.UI.Page
    {
        String json = System.Web.Helpers.Json.Encode(null);
        public static List<DtoRelacao> listChave = new List<DtoRelacao>();
        public static int StaticIdCategoria = 0;
        public static string envia = "False";
        public static long idcompeticao = 0;

        protected void Page_Load(object sender, EventArgs e)
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
                    bool adm = bool.Parse(Session["Administrador"].ToString());
                }

                StaticIdCategoria = int.Parse(Request.QueryString["IdCategoriaCompeticao"]);
                idcompeticao = long.Parse(Request.QueryString["IdCompeticao"]);
                envia = Request.QueryString["EnviaEmail"];
                PreencheCabecalho();
            }
            catch (Exception)
            {
                // Response.Redirect("~\Default.aspx");
            }
        }

        private void PreencheCabecalho()
        {
            Regras.Regras get = new Regras.Regras();
            DtoCompeticao competicao = get.GetCompeticaoPorId(int.Parse(idcompeticao.ToString()));
            Label1.Text = string.Format("<b> {0} - Cidade.: {1} </b>", competicao.NomeCompeticao, competicao.NomeCidade);
            DtoCategoriasCompeticao categoria = get.GetCategoriasPorIdCategoriaIdCompeticao(StaticIdCategoria, int.Parse(idcompeticao.ToString()));
            Label2.Text = string.Format("Categoria.:<b>{0} - {1} </ br><center> <h2>{2}</h2></center</b>", categoria.NrCategoria, categoria.NomeCategoria, categoria.TipoCompeticao);
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public static string GeraSorteio()
        {
            int idCategoria = StaticIdCategoria;
            listChave.Clear();
            MontaChave(idCategoria);
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(listChave);
        }

        private static void MontaChave(int idCategoria)
        {
            Regras.Regras get = new Regras.Regras();
            List<DtoCategoriaAtleta> listaSorteio = get.ListaSorteio(idCategoria);

            foreach (var l in listaSorteio)
            {
                DtoRelacao i = new DtoRelacao();
                i.name = l.NomeFiliado + "<br>" + l.SiglaAssociacao;
                i.seed = int.Parse(l.PosicaoSorteio.ToString());
                listChave.Add(i);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;

namespace SisKiai.Relatorio
{
    public partial class ResultadoCompeticao : System.Web.UI.Page
    {
        public static int StaticIdCategoria = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool adm = bool.Parse(Session["Administrador"].ToString());
                if (adm)
                {
                    Session["idcompeticao"] = Request.QueryString["IdCompeticao"];
                    PreencheCabecalho();
                    CarregarCompeticaoAtiva();
                }
                Session["idCompeticaoSelecionada"] = null;
            }
        }

        private void PreencheCabecalho()
        {
            Regras.Regras get = new Regras.Regras();
            DtoCompeticao competicao = get.GetCompeticaoPorId(int.Parse(Session["idcompeticao"].ToString()));
            Label1.Text = string.Format("<b> {1} - Cidade.: {2} </b>", competicao.IdCompeticao, competicao.NomeCompeticao, competicao.NomeCidade);
        }

        private void CarregarCompeticaoAtiva()
        {
            Regras.Regras get = new Regras.Regras();
            List<DtoCategoriasCompeticao> list = get.GetAllCategoriaPorCompeticao(Session["idcompeticao"].ToString());
            GridCategoria.DataSource = list;
            GridCategoria.DataBind();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Regras.Regras get = new Regras.Regras();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string IdCategoria = GridCategoria.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView GridAtletasCategoria = e.Row.FindControl("GridAtletasCategoria") as GridView;
                    string idCompeticaoSelecionada = Session["idCompeticaoSelecionada"].ToString();
                    List<DtoCategoriaAtleta> list = get.GetAllAtletasPorCompeticaoColocacao(long.Parse(IdCategoria));
                    GridAtletasCategoria.DataSource = list;
                    GridAtletasCategoria.DataBind();
                }
            }
            catch (Exception )
            {
                //FailureText.Text = ex.Message;
            }
        }
    }
}
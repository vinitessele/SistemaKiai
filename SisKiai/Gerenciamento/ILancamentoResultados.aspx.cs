using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;

namespace SisKiai.Gerenciamento
{
    public partial class ILancamentoResultados : System.Web.UI.Page
    {
        public static int StaticIdCategoria = 0;

        protected void Page_Load(object sender, EventArgs e)
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
            if (!IsPostBack)
            {
                bool adm = bool.Parse(Session["Administrador"].ToString());
                if (adm)
                {
                    CarregarCompeticaoAtiva();
                    CarregarCompeticaoFinalizada();
                }
                Session["idCompeticaoSelecionada"] = null;
            }
        }

        #region Métodos

        private void CarregarCompeticaoFinalizada()
        {
            Regras.Regras dados = new Regras.Regras();
            List<DtoCompeticao> competicao = dados.GetAllCompeticaFinalizadas();

            DropDownListReabrir.DataSource = competicao;
            DropDownListReabrir.DataTextField = "NomeCompeticao";
            DropDownListReabrir.DataValueField = "IdCompeticao";
            DropDownListReabrir.DataBind();
            DropDownListReabrir.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }

        private void CarregarCompeticaoAtiva()
        {
            Regras.Regras dados = new Regras.Regras();
            List<DtoCompeticao> competicao = dados.GetAllCompeticaoAtiva10Dias();

            DropDownListCompeticao.DataSource = competicao;
            DropDownListCompeticao.DataTextField = "NomeCompeticao";
            DropDownListCompeticao.DataValueField = "IdCompeticao";
            DropDownListCompeticao.DataBind();
            DropDownListCompeticao.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }

        private void CarregarCategoriasCompeticao(string idCompeticao)
        {
            Regras.Regras get = new Regras.Regras();
            List<DtoCategoriasCompeticao> list = get.GetAllCategoriaPorCompeticao(idCompeticao);
            List<DtoCategoriasCompeticao> newlist = new List<DtoCategoriasCompeticao>();
            foreach (var l in list)
            {
                DtoCategoriasCompeticao a = new DtoCategoriasCompeticao();
                a = l;
                if (a.Categoria_Finalizada)
                {
                    a.Categoria_Finalizada2 = "SIM";
                }
                else
                {
                    a.Categoria_Finalizada2 = "NÃO";
                }
            }

            GridCategoria.DataSource = list;
            GridCategoria.DataBind();
        }

        #endregion

        #region Formulário

        public void DropDownListComnpeticaoChanged(object sender, EventArgs e)
        {
            Session["idCompeticaoSelecionada"] = DropDownListCompeticao.SelectedValue;
            CarregarCategoriasCompeticao(DropDownListCompeticao.SelectedValue);
        }

        protected void btnFinalizarClick(object sender, EventArgs e)
        {
            try
            {
                long idCompetição = long.Parse(DropDownListCompeticao.SelectedValue);
                Regras.Regras finaliza = new Regras.Regras();
                finaliza.SetFinalizaCompeticao(idCompetição);
                CarregarCompeticaoAtiva();
                CarregarCompeticaoFinalizada();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        #endregion

        #region Grid

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
                    List<DtoCategoriaAtleta> list = get.GetAllAtletasPorCategoria(IdCategoria, idCompeticaoSelecionada);
                    GridAtletasCategoria.DataSource = list;
                    GridAtletasCategoria.DataBind();
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        protected void GridAtletasCategoriaEdit(object sender, GridViewEditEventArgs e)
        {
            GridCategoria.EditIndex = e.NewEditIndex;
        }

        protected void GridAtletasCategoriaCancel(object sender, GridViewCancelEditEventArgs e)
        {
            GridView grid = sender as GridView;
            grid.EditIndex = -1;
        }

        protected void GridAtletasCategoriaUp(object sender, GridViewUpdateEventArgs e)
        {
            if (e.NewValues["ResultadoCompeticao"] != null)
            {
                Regras.Regras set = new Regras.Regras();

                string id = e.Keys["IdCategoriaAtleta"].ToString();
                String resultado = e.NewValues["ResultadoCompeticao"].ToString();
                set.SetResultado(int.Parse(id), int.Parse(resultado.Replace(",", "")));

                CarregarCategoriasCompeticao(DropDownListCompeticao.SelectedValue);
            }
        }

        protected void GridCategoriaOnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(e.CommandArgument.ToString()))
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    string idCategoria = GridCategoria.DataKeys[index]["IdCategoria"].ToString();
                    StaticIdCategoria = int.Parse(idCategoria.ToString());
                    string idCompeticao = DropDownListCompeticao.SelectedValue;
                    Regras.Regras set = new Regras.Regras();
                    List<DtoSorteio> ListaSorteio = new List<DtoSorteio>();
                    switch (e.CommandName)
                    {
                        case "FinalizaCategoria":
                            set.SetFinalizaCategoriaCompeticao(idCategoria, idCompeticao);
                            CarregarCategoriasCompeticao(idCompeticao);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }
        private void AlertError(string ex)
        {
            AlertNotificationDiv.Attributes.Add("class", "col-md-10 col-xs-10 col-ms-10 alert alert-danger alert-dismissable");
            AlertNotificationBox.Text = "Erro " + ex;
        }

        private void AlertSucesso()
        {
            AlertNotificationDiv.Attributes.Add("class", "col-md-10 col-xs-10 col-ms-10 alert alert-success alert-dismissable");
            AlertNotificationBox.Text = "Sucesso";
        }

        private void AlertVisibleTrue()
        {
            if (!AlertNotificationDiv.Visible)
                AlertNotificationDiv.Visible = true;
            if (!AlertNotificationBox.Visible)
                AlertNotificationBox.Visible = true;
        }

        #endregion

        protected void btnReabrirClick(object sender, EventArgs e)
        {
            try
            {
                long idCompeticao = long.Parse(DropDownListReabrir.SelectedValue);
                Regras.Regras Reabrir = new Regras.Regras();
                Reabrir.SetReabrirCompeticao(idCompeticao);
                CarregarCompeticaoAtiva();
                CarregarCompeticaoFinalizada();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }
    }
}
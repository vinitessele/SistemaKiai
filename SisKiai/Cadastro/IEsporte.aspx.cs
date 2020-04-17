using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SisKiai.Cadastro
{
    public partial class IEsporte : System.Web.UI.Page
    {
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
                CarregarEsportes();
            }
        }

        public void GrdEsportePageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridEsportes.PageIndex = e.NewPageIndex;
            CarregarEsportes();
        }

        private void CarregarEsportes()
        {
            Regras.Regras dados = new Regras.Regras();
            List<Regras.Dto.DtoEsportes> esportes = dados.GetAllEsportes();
            GridEsportes.DataSource = esportes;
            GridEsportes.DataBind();
        }
        protected void GridEsportesCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                int index = Convert.ToInt32(e.CommandArgument);
                string idEsporte = GridEsportes.DataKeys[index]["IdEsportes"].ToString();

                if (e.CommandName == "Excluir")
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelEsporte(idEsporte);
                }
                else if (e.CommandName == "Editar")
                {
                    Regras.Regras get = new Regras.Regras();
                    Regras.Dto.DtoEsportes dados = get.GetEsportePorId(int.Parse(idEsporte));
                    CarregaTela(dados);
                }
                CarregarEsportes();
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

        private void CarregaTela(Regras.Dto.DtoEsportes dados)
        {
            TextId.Text = dados.IdEsportes.ToString();
            TextNomeEsporte.Text = dados.NomeEsportes;
        }
        protected void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoEsportes esporte = new Regras.Dto.DtoEsportes();

                if (!String.IsNullOrWhiteSpace(TextId.Text))
                    esporte.IdEsportes = int.Parse(TextId.Text);
                esporte.NomeEsportes = TextNomeEsporte.Text;

                Regras.Regras set = new Regras.Regras();
                set.SetEsporte(esporte);

                AlertVisibleTrue();
                AlertSucesso();
                CarregarEsportes();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }
        protected void ConsultarClick(object sender, EventArgs e)
        {
            Regras.Regras dados = new Regras.Regras();

            var esporte = dados.GetEsporteNome(TextConsulta.Text);
            GridEsportes.DataSource = esporte;
            GridEsportes.DataBind();
        }
    }
}
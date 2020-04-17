using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SisKiai.Cadastro
{
    public partial class IGraduacao : System.Web.UI.Page
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
                CarregarGrid();
                CarregarEsporte();
            }
        }

        private void CarregarEsporte()
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoEsportes> dados = get.GetAllEsportes();
            DropDownListEsporte.DataSource = dados;
            DropDownListEsporte.DataValueField = "IdEsportes";
            DropDownListEsporte.DataTextField = "NomeEsportes";
            DropDownListEsporte.DataBind();
            DropDownListEsporte.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }

        public void GrdGraduacaoPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridGraduacao.PageIndex = e.NewPageIndex;
            CarregarGrid();
        }

        public void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras set = new Regras.Regras();
                Regras.Dto.DtoGraduacao dados = new Regras.Dto.DtoGraduacao();
                if (!String.IsNullOrWhiteSpace(TextId.Text))
                    dados.IdGraduacao = int.Parse(TextId.Text);
                dados.DescricaoGraduacao = TextGraduacao.Text;
                dados.IdEsporte = int.Parse(DropDownListEsporte.SelectedValue);
                set.SetGraduacao(dados);
                CarregarGrid();
                AlertVisibleTrue();
                AlertSucesso();
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

        public void GridGraduacaoCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string idGraduacao = GridGraduacao.DataKeys[index]["IdGraduacao"].ToString();
                if (e.CommandName == "Excluir")
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelGraduacao(idGraduacao);
                }
                else if (e.CommandName == "Editar")
                {
                    Regras.Regras get = new Regras.Regras();
                    Regras.Dto.DtoGraduacao dados = get.GetGraduacaoPorId(int.Parse(idGraduacao));
                    CarregarTela(dados);
                }
                CarregarGrid();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        private void CarregarGrid()
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoGraduacao> dados = get.GetAllGraduacao();
            GridGraduacao.DataSource = dados;
            GridGraduacao.DataBind();
        }

        private void CarregarTela(Regras.Dto.DtoGraduacao dados)
        {
            TextId.Text = dados.IdGraduacao.ToString();
            TextGraduacao.Text = dados.DescricaoGraduacao;
            DropDownListEsporte.SelectedValue = dados.IdEsporte.ToString();
        }

        public void ConsultarClick(object sender, EventArgs e)
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoGraduacao> dados = get.GetGraduacaoPorNome(TextConsulta.Text);
            GridGraduacao.DataSource = dados;
            GridGraduacao.DataBind();
        }
    }
}
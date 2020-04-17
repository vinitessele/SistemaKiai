using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras;

namespace SisKiai.Cadastro
{
    public partial class ICidade : System.Web.UI.Page
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
                CarregarEstado();
                CarregarGrid();
            }
        }

        protected void GrdCidadePageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewCidade.PageIndex = e.NewPageIndex;
            CarregarGrid();
        }

        private void CarregarEstado()
        {
            Regras.Regras dados = new Regras.Regras();
            var estados = dados.GetAllEstado();
            DropDownListEstado.DataSource = estados;
            DropDownListEstado.DataValueField = "IdEstado";
            DropDownListEstado.DataTextField = "SiglaEstado";
            DropDownListEstado.DataBind();
            DropDownListEstado.Items.Insert(0, new ListItem("-Selecione-", "0"));
            Session["Estados"] = estados;
        }

        protected void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoCidade cidade = new Regras.Dto.DtoCidade();
                if (!String.IsNullOrWhiteSpace(TextIdCidade.Text))
                {
                    cidade.IdCidade = int.Parse(TextIdCidade.Text);
                }
                cidade.NomeCidade = TextNomeCidade.Text.ToUpper();
                cidade.CepCidade = TextCep.Text;
                cidade.IdEstado = int.Parse(DropDownListEstado.SelectedValue);

                Regras.Regras set = new Regras.Regras();
                set.SetCidade(cidade);

                AlertVisibleTrue();
                AlertSucesso();
                CarregarGrid();
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

        private void CarregarGrid()
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoCidade> dados = get.GetAllCidade();
            GridViewCidade.DataSource = dados;
            GridViewCidade.DataBind();
        }

        protected void GridCidadeCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string idCidade = GridViewCidade.DataKeys[index]["IdCidade"].ToString();
                if (e.CommandName == "Excluir")
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelCidade(idCidade);
                }
                else if (e.CommandName == "Editar")
                {
                    Regras.Regras get = new Regras.Regras();
                    Regras.Dto.DtoCidade dados = get.GetCidadePorId(int.Parse(idCidade));
                    CarregaTela(dados);
                }
                CarregarGrid();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        private void CarregaTela(Regras.Dto.DtoCidade dados)
        {
            TextIdCidade.Text = dados.IdCidade.ToString();
            TextNomeCidade.Text = dados.NomeCidade;
            if (!String.IsNullOrWhiteSpace(dados.IdEstado.ToString()))
                DropDownListEstado.SelectedValue = dados.IdEstado.ToString();
            TextCep.Text = dados.CepCidade;
        }

        protected void ConsultarClick(object sender, EventArgs e)
        {
            Regras.Regras dados = new Regras.Regras();

            var cidade = dados.GetCidadeNome(TextConsulta.Text);
            GridViewCidade.DataSource = cidade;
            GridViewCidade.DataBind();
        }
    }
}
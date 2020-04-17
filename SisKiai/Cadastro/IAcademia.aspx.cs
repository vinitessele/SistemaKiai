using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web;

namespace SisKiai.Cadastro
{
    public partial class IAcademia : System.Web.UI.Page
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
                CarregarCidade();
                CarregarAcademia();
                CarregarEsporte();
            }
        }

        #region Formularios

        protected void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoAcademia academia = new Regras.Dto.DtoAcademia();
                if (!String.IsNullOrWhiteSpace(TextIdAssocicao.Text))
                    academia.IdAcademia = int.Parse(TextIdAssocicao.Text);
                academia.NomeAcademia = TextNomeAssociacao.Text.ToUpper();
                academia.EnderecoAcademia = TextEndereco.Text.ToUpper();
                academia.ResponsavelAcademia = TextResponsavel.Text.ToUpper();
                academia.TelefoneCelularAcademia = TextTelefoneCelular.Text;
                academia.TelefoneFixoAcademia = TextTelefone.Text;
                academia.CnpjAcademia = TextCnpj.Text;
                academia.InscriAcademia = TextInscri.Text;
                academia.EmailAcademia = TextEmail.Text.ToUpper();
                academia.IdCidadeAcademia = int.Parse(DropDownListCidade.SelectedValue);
                academia.CepAcademia = TextCep.Text;
                academia.IdEsporte = int.Parse(DropDownListEsporte.SelectedValue);
                academia.Sigla = TextSigla.Text.ToUpper();

  
                Regras.Regras set = new Regras.Regras();
                set.SetAcademia(academia);
                CarregarAcademia();
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

        #endregion

        #region Grids
        protected void GrdAcademiaPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridAcademia.PageIndex = e.NewPageIndex;
            CarregarAcademia();
        }

        protected void GridAcademiaCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string idAcademia = GridAcademia.DataKeys[index]["IdAcademia"].ToString();

                if (e.CommandName == "Excluir")
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelAcademia(idAcademia);
                }
                else if (e.CommandName == "Editar")
                {
                    Regras.Regras get = new Regras.Regras();
                    Regras.Dto.DtoAcademia dados = get.GetAcademiaPorId(int.Parse(idAcademia));
                    CarregaTela(dados);
                }

                CarregarAcademia();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        #endregion

        #region Eventos e Validacoes

        private void CarregaTela(Regras.Dto.DtoAcademia dados)
        {
            TextIdAssocicao.Text = dados.IdAcademia.ToString();
            TextNomeAssociacao.Text = dados.NomeAcademia;
            TextEndereco.Text = dados.EnderecoAcademia;
            TextResponsavel.Text = dados.ResponsavelAcademia;
            TextSigla.Text = dados.Sigla;
            DropDownListCidade.SelectedValue = dados.IdCidadeAcademia.ToString();
            DropDownListEsporte.SelectedValue = dados.IdEsporte.ToString();
            TextCep.Text = dados.CepAcademia;
            TextTelefone.Text = dados.TelefoneFixoAcademia;
            TextTelefoneCelular.Text = dados.TelefoneCelularAcademia;
            TextEmail.Text = dados.EmailAcademia;
            TextCnpj.Text = dados.CnpjAcademia;
            TextInscri.Text = dados.InscriAcademia;
            TextSigla.Text = dados.Sigla;
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

        private void CarregarAcademia()
        {
            Regras.Regras dados = new Regras.Regras();
            var academia = dados.GetAllAcademia();
            GridAcademia.DataSource = academia;
            GridAcademia.DataBind();
        }

        private void CarregarCidade()
        {
            Regras.Regras dados = new Regras.Regras();
            var cidades = dados.GetAllCidade();
            DropDownListCidade.DataSource = cidades;
            DropDownListCidade.DataTextField = "NomeCidade";
            DropDownListCidade.DataValueField = "IdCidade";
            DropDownListCidade.DataBind();
            DropDownListCidade.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }

        public void TextValidaCnpj(object sender, EventArgs e)
        {
            try
            {
                bool valida = Regras.Regras.ValidaCnpj(TextCnpj.Text);
                if (!valida)
                    throw new Exception("CNPJ inválido");
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

            List<Regras.Dto.DtoAcademia> academia = dados.GetAcademiaNome(TextConsulta.Text);
            GridAcademia.DataSource = academia;
            GridAcademia.DataBind();
        }
        public void BuscaCep(object sender, EventArgs e)
        {
            Regras.Regras get = new Regras.Regras();
            TextCep.Text = get.GetCidadePorId(int.Parse(DropDownListCidade.SelectedValue)).CepCidade;
        }
        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SisKiai.Cadastro
{
    public partial class IRanking : System.Web.UI.Page
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
                CarregarAcademia();
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

        private void CarregarAcademia()
        {
            Regras.Regras dados = new Regras.Regras();
            TextPontos.Text = string.Empty;
            Session["AllAcademia"] = dados.GetAllAcademia();
            var listassociacao = Session["AllAcademia"];
            DropDownListAssociacao.DataSource = listassociacao;
            DropDownListAssociacao.DataTextField = "NomeAcademia";
            DropDownListAssociacao.DataValueField = "IdAcademia";
            DropDownListAssociacao.DataBind();
            DropDownListAssociacao.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Selecione-", "0"));
        }

        protected void ConsultarClick(object sender, EventArgs e)
        {

        }

        protected void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoRanking dados = new Regras.Dto.DtoRanking();

                if (!String.IsNullOrWhiteSpace(TextIdRanking.Text))
                    dados.IdRanking = int.Parse(TextIdRanking.Text);
                if (!String.IsNullOrWhiteSpace(TextAno.Text))
                    dados.AnoRanking = long.Parse(TextAno.Text);
                if (DropDownListNome.SelectedValue != "0")
                    dados.IdFiliadoRanking = int.Parse(DropDownListNome.SelectedValue);
                if (!String.IsNullOrWhiteSpace(TextPontos.Text))
                    dados.PontoRanking = int.Parse(TextPontos.Text);


                if (!string.IsNullOrWhiteSpace(TextIdRanking.Text))
                {
                    AlterarRanking(dados);
                }
                else
                {
                    SalvarRanking(dados);
                }
                CarregarRanking();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
                CarregarRanking();
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

        private void SalvarRanking(Regras.Dto.DtoRanking dados)
        {
            Regras.Regras set = new Regras.Regras();
            set.SetRanking(dados);
        }

        private void AlterarRanking(Regras.Dto.DtoRanking dados)
        {
            Regras.Regras alter = new Regras.Regras();
            alter.AlterRanking(dados);
        }

        protected void GridRankingPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridRanking.PageIndex = e.NewPageIndex;
            CarregarRanking();
        }

        private void CarregarRanking()
        {
            Regras.Regras dados = new Regras.Regras();
            var listRanking = dados.GetAllRanking(TextAno.Text);
            GridRanking.DataSource = listRanking;
            GridRanking.DataBind();
        }

        protected void DropDownListAssociacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idAssociação = int.Parse(DropDownListAssociacao.SelectedValue);
            CarregarAtletas(idAssociação);
        }

        private void CarregarAtletas(int idAssociação)
        {
            Regras.Regras dados = new Regras.Regras();
            var listFiliados = dados.GetFiliadoPorIdAssociacao(idAssociação);
            DropDownListNome.DataSource = listFiliados;
            DropDownListNome.DataTextField = "nomefiliado";
            DropDownListNome.DataValueField = "idfiliado";
            DropDownListNome.DataBind();
            DropDownListNome.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Selecione-", "0"));
        }
        public void GridRankingCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string idRanking = GridRanking.DataKeys[index]["IdRanking"].ToString();
                //Session["IdFiliado"] = idFiliado;
                if (e.CommandName == "Excluir")
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelRanking(idRanking);
                }
                else if (e.CommandName == "Editar")
                {
                    Regras.Regras get = new Regras.Regras();
                    Regras.Dto.DtoRanking dados = get.GetRankingPorId(int.Parse(idRanking));
                    CarregarAcademia();
                    PreencheTela(dados);
                }

                AlertVisibleTrue();
                AlertSucesso();
                CarregarRanking();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        private void PreencheTela(Regras.Dto.DtoRanking dados)
        {
            CarregarAtletas(int.Parse(dados.IdAssociacao.ToString()));
            DropDownListAssociacao.SelectedValue = dados.IdAssociacao.ToString();
            DropDownListNome.SelectedValue = dados.IdFiliadoRanking.ToString();
            TextAno.Text = dados.AnoRanking.ToString();
            TextIdRanking.Text = dados.IdRanking.ToString();
            TextPontos.Text = dados.PontoRanking.ToString();
        }

        protected void DropDownListNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            long ano = 0;
            if (!String.IsNullOrWhiteSpace(TextAno.Text))
                ano = long.Parse(TextAno.Text);

            Regras.Regras get = new Regras.Regras();
            var verificaRanking = get.GetVerificaRanking(ano, int.Parse(DropDownListNome.SelectedValue));
            if (verificaRanking != null)
            {
                TextIdRanking.Text = verificaRanking.IdRanking.ToString();
                TextPontos.Text = verificaRanking.PontoRanking.ToString();
            }
        }

        protected void DropDownListEsporteChange(object sender, EventArgs e)
        {
            CarregarRanking();
        }
    }
}
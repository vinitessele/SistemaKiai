using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web;

namespace SisKiai.Cadastro
{
    public partial class ICategoria : System.Web.UI.Page
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
                CarregarTpCompeticao();
            }
        }

        public void SelecionaTPCompeticao(object sender, EventArgs e)
        {
            CarregarGraduacao();
            CarregarTpCompeticao();
        }

        private void CarregarTpCompeticao()
        {
            Regras.Regras dados = new Regras.Regras();
            var tpCompeticao = dados.GetAllTpCompeticaoPorEsporte(DropDownListEsporte.SelectedValue);
            DropDownTpCompeticao.DataSource = tpCompeticao;
            DropDownTpCompeticao.DataValueField = "IdTipoCompeticao";
            DropDownTpCompeticao.DataTextField = "DescricaoCompeticao";
            DropDownTpCompeticao.DataBind();
            DropDownTpCompeticao.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }

        private void CarregarGraduacao()
        {
            Regras.Regras dados = new Regras.Regras();
            var graduacao = dados.GetAllGraduacaoPorEsporte(DropDownListEsporte.SelectedValue);
            DropDownListGraduacaoInicial.DataSource = graduacao;
            DropDownListGraduacaoInicial.DataValueField = "IdGraduacao";
            DropDownListGraduacaoInicial.DataTextField = "DescricaoGraduacao";
            DropDownListGraduacaoInicial.DataBind();
            DropDownListGraduacaoInicial.Items.Insert(0, new ListItem("-Selecione-", "0"));

            DropDownListGraduacaoFinal.DataSource = graduacao;
            DropDownListGraduacaoFinal.DataValueField = "IdGraduacao";
            DropDownListGraduacaoFinal.DataTextField = "DescricaoGraduacao";
            DropDownListGraduacaoFinal.DataBind();
            DropDownListGraduacaoFinal.Items.Insert(0, new ListItem("-Selecione-", "0"));
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

            DropDownListEsporte2.DataSource = dados;
            DropDownListEsporte2.DataValueField = "IdEsportes";
            DropDownListEsporte2.DataTextField = "NomeEsportes";
            DropDownListEsporte2.DataBind();
            DropDownListEsporte2.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }

        public void GrdCategoriaPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridCategoria.PageIndex = e.NewPageIndex;
            CarregarGrid();
        }

        public void GridCategoriaCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CarregarGraduacao();
                int n = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out n))
                {

                    int index = Convert.ToInt32(e.CommandArgument);
                    string idCategoria = GridCategoria.DataKeys[index]["IdCategoria"].ToString();
                    if (e.CommandName == "Excluir")
                    {
                        Regras.Regras del = new Regras.Regras();
                        del.DelCategoria(idCategoria);
                    }
                    else if (e.CommandName == "Editar")
                    {
                        Regras.Regras get = new Regras.Regras();
                        Regras.Dto.DtoCategoria dados = get.GetCategoriaPorId(int.Parse(idCategoria));
                        CarregaTela(dados);
                        CarregarGraduacao();
                        CarregarTpCompeticao();
                    }
                    CarregarGrid();
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        private void CarregaTela(Regras.Dto.DtoCategoria dados)
        {
            TextId.Text = dados.IdCategoria.ToString();
            TextNumeroCategoria.Text = dados.NumeroCategoria.ToString();
            TextDescricaoCategoria.Text = dados.DescricaoCategoria;
            DropDownListEsporte.SelectedValue = dados.IdEsporte.ToString();
            CarregarTpCompeticao();
            DropDownTpCompeticao.SelectedValue = dados.TpCompeticao.ToString();
            DropDownListTpCategoria.SelectedValue = dados.TpCategoria.ToString();
            RBtnListSexo.SelectedValue = dados.SexoCategoria.ToString();
            TextAlturaInicial.Text = dados.AlturaInicial.ToString();
            TextAlturaFinal.Text = dados.AlturaFinal.ToString();
            TextPesoInicial.Text = dados.PesoInicial.ToString();
            TextPesoFinal.Text = dados.PesoFinal.ToString();
            TextIdadeInicial.Text = dados.IdadeInicial.ToString();
            TextIdadeFinal.Text = dados.IdadeFinal.ToString();
            DropDownListGraduacaoInicial.SelectedValue = dados.IdGraduacaoInicial.ToString();
            DropDownListGraduacaoFinal.SelectedValue = dados.IdGraduacaoFinal.ToString();
            CheckStatus.Checked = dados.StatusCategoria;
        }

        private void CarregarGrid()
        {
            Regras.Regras dados = new Regras.Regras();
            List<Regras.Dto.DtoCategoria> lista = dados.GetAllCategoria();
            GridCategoria.DataSource = lista;
            GridCategoria.DataBind();
        }

        public void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoCategoria item = new Regras.Dto.DtoCategoria();
                if (!String.IsNullOrWhiteSpace(TextId.Text))
                    item.IdCategoria = int.Parse(TextId.Text);
                item.IdEsporte = int.Parse(DropDownListEsporte.SelectedValue);
                item.IdGraduacaoInicial = int.Parse(DropDownListGraduacaoInicial.SelectedValue);
                item.IdGraduacaoFinal = int.Parse(DropDownListGraduacaoFinal.SelectedValue);
                item.DescricaoCategoria = TextDescricaoCategoria.Text;
                item.SexoCategoria = RBtnListSexo.SelectedValue;
                item.TpCategoria = DropDownListTpCategoria.SelectedValue;
                item.TpCompeticao = long.Parse(DropDownTpCompeticao.SelectedValue.ToString());
                if (!String.IsNullOrWhiteSpace(TextNumeroCategoria.Text))
                    item.NumeroCategoria = int.Parse(TextNumeroCategoria.Text);
                if (!String.IsNullOrWhiteSpace(TextAlturaInicial.Text))
                    item.AlturaInicial = decimal.Parse(TextAlturaInicial.Text);
                if (!String.IsNullOrWhiteSpace(TextAlturaFinal.Text))
                    item.AlturaFinal = decimal.Parse(TextAlturaFinal.Text);
                if (!String.IsNullOrWhiteSpace(TextPesoInicial.Text))
                    item.PesoInicial = decimal.Parse(TextPesoInicial.Text);
                if (!String.IsNullOrWhiteSpace(TextPesoFinal.Text))
                    item.PesoFinal = decimal.Parse(TextPesoFinal.Text);
                item.StatusCategoria = CheckStatus.Checked;
                if (!String.IsNullOrWhiteSpace(TextIdadeInicial.Text))
                    item.IdadeInicial = int.Parse(TextIdadeInicial.Text);
                if (!String.IsNullOrWhiteSpace(TextIdadeFinal.Text))
                    item.IdadeFinal = int.Parse(TextIdadeFinal.Text);

                Regras.Regras set = new Regras.Regras();
                set.SetCategoria(item);
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

        public void ConsultarClick(object sender, EventArgs e)
        {
            Regras.Regras consulta = new Regras.Regras();
            if (!String.IsNullOrEmpty(TextConsulta.Text))
            {
                List<Regras.Dto.DtoCategoria> list = consulta.GetCategoriaPorNome(TextConsulta.Text);
                GridCategoria.DataSource = list;
                GridCategoria.DataBind();
            }
            else
            {
                List<Regras.Dto.DtoCategoria> list = consulta.GetCategoriaEsporte(int.Parse(DropDownListEsporte2.SelectedValue));
                GridCategoria.DataSource = list;
                GridCategoria.DataBind();
            }
        }
    }
}
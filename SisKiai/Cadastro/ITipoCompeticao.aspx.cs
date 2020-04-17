using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SisKiai.Cadastro
{
    public partial class ITipoCompeticao : System.Web.UI.Page
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

        private void CarregarGrid()
        {
            Regras.Regras dados = new Regras.Regras();
            var tipoCompeticao = dados.GetAllTipoCompeticao();

            GridViewTipoCompeticao.DataSource = tipoCompeticao;
            GridViewTipoCompeticao.DataBind();
        }

        public void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoTipoCompeticao item = new Regras.Dto.DtoTipoCompeticao();
                if (!String.IsNullOrWhiteSpace(TextIdTipo.Text))
                    item.IdTipoCompeticao = int.Parse(TextIdTipo.Text);
                item.DescricaoCompeticao = TextDescricao.Text;
                item.IdEsporte = int.Parse(DropDownListEsporte.SelectedValue);

                Regras.Regras set = new Regras.Regras();
                set.SetTipoCompeticao(item);
                CarregarGrid();
                FailureText.Text = "Sucesso!";
            }
            catch (Exception ex)
            {
                FailureText.Text = ex.Message;
            }
        }

        public void ConsultarClick(object sender, EventArgs e)
        {
            Regras.Regras consulta = new Regras.Regras();
            List<Regras.Dto.DtoTipoCompeticao> list = consulta.GetTipoCompeticaoPorNome(TextConsulta.Text);
            GridViewTipoCompeticao.DataSource = list;
            GridViewTipoCompeticao.DataBind();
        }

        public void GrdTipoCompeticaoPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewTipoCompeticao.PageIndex = e.NewPageIndex;
            CarregarGrid();
        }

        public void GridTipoCompeticaoCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string idTipoCompeticao = GridViewTipoCompeticao.DataKeys[index]["IdTipoCompeticao"].ToString();
                if (e.CommandName == "Excluir")
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelTipoCompeticao(idTipoCompeticao);
                }
                else if (e.CommandName == "Editar")
                {
                    Regras.Regras get = new Regras.Regras();
                    Regras.Dto.DtoTipoCompeticao dados = get.GetTipoCompeticaoPorId(int.Parse(idTipoCompeticao));
                    CarregaTela(dados);
                }
                CarregarGrid();
            }
            catch (Exception ex)
            {
                FailureText.Text = ex.Message;
            }
        }

        private void CarregaTela(Regras.Dto.DtoTipoCompeticao dados)
        {
            TextIdTipo.Text = dados.IdTipoCompeticao.ToString();
            TextDescricao.Text = dados.DescricaoCompeticao;
            DropDownListEsporte.SelectedValue = dados.IdEsporte.ToString();
        }
    }
}
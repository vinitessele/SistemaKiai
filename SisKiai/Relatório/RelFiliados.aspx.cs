using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;

namespace SisKiai.Relatorio
{
    public partial class RelFiliados : System.Web.UI.Page
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
                CarregarAssociacao();
            }
            CarregarFiliado();
        }

        private void CarregarAssociacao()
        {
            Regras.Regras dados = new Regras.Regras();
            List<Regras.Dto.DtoAcademia> listassociacao = new List<DtoAcademia>();
            listassociacao = dados.GetAllAcademia();
            DropDownListAssociacao.DataSource = listassociacao;
            DropDownListAssociacao.DataTextField = "NomeAcademia";
            DropDownListAssociacao.DataValueField = "IdAcademia";
            DropDownListAssociacao.DataBind();
            DropDownListAssociacao.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Todos-", "0"));
        }

        public void GrdFiliadoPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridFiliado.PageIndex = e.NewPageIndex;
            CarregarFiliado();
        }

        private void CarregarFiliado()
        {
            Regras.Regras dados = new Regras.Regras();
            List<DtoFiliado> filiados = new List<DtoFiliado>();
            int idAssociacao = int.Parse(DropDownListAssociacao.SelectedValue);

            if (idAssociacao != 0)
            {
                filiados = dados.GetAllFiliadoPorIdAssociacao(idAssociacao);
            }
            else
            {
                filiados = dados.GetAllFiliado();
            }

            if (RBListOrdem.SelectedValue == "0")
                filiados = filiados.OrderBy(x => x.NomeFiliado).ToList();
            if (RBListOrdem.SelectedValue == "1")
                filiados = filiados.OrderBy(x => x.NumeroRegistro).ToList();
            if (RBListOrdem.SelectedValue == "2")
                filiados = filiados.OrderBy(x => x.IdAssociacao).ToList();
            if (RBListStatus.SelectedValue == "A")
                filiados = filiados.Where(x => x.StatusFiliado == "A").ToList();
            if (RBListStatus.SelectedValue == "I")
                filiados = filiados.Where(x => x.StatusFiliado == "I").ToList();

            GridFiliado.DataSource = filiados;
            GridFiliado.DataBind();
        }

        public void DropDowListChanged(object sender, EventArgs e)
        {
            CarregarFiliado();
        }
    }
}
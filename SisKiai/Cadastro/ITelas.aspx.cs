using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;

namespace SisKiai.Cadastro
{
    public partial class ITelas : System.Web.UI.Page
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
                CarregarTelas();
            }
        }

        private void CarregarTelas()
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoTelas> lista = get.GetAllTelas();
            GridTelas.DataSource = lista;
            GridTelas.DataBind();
        }
        public void GrdTelasPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridTelas.PageIndex = e.NewPageIndex;
            CarregarTelas();
        }

        public void GridTelasCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string idTelas = GridTelas.DataKeys[index]["IdTelas"].ToString();
                if (e.CommandName == "Excluir")
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelTelas(idTelas);
                }
                else if (e.CommandName == "Editar")
                {
                    Regras.Regras get = new Regras.Regras();
                    Regras.Dto.DtoTelas dados = get.GetTelasPorId(int.Parse(idTelas));
                    CarregaTela(dados);
                }
                CarregarTelas();
            }
            catch (Exception ex)
            {
                FailureText.Text = ex.Message;
            }
        }
  
        private void CarregaTela(DtoTelas dados)
        {
            TextIdTelas.Text = dados.IdTelas.ToString();
            TextNome.Text = dados.Nome;
            TextEndereco.Text = dados.Endereco;
        }

        public void ConsultarClick(object sender, EventArgs e)
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoTelas> lista = get.GetTelasPorNome(TextConsulta.Text);
            GridTelas.DataSource = lista;
            GridTelas.DataBind();
        }

        public void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoTelas telas = new Regras.Dto.DtoTelas();

                if (!String.IsNullOrWhiteSpace(TextIdTelas.Text))
                    telas.IdTelas = long.Parse(TextIdTelas.Text);
                telas.Nome = TextNome.Text;
                telas.Endereco = TextEndereco.Text;

                Regras.Regras set = new Regras.Regras();
                set.SetTelas(telas);

                FailureText.Text = "Sucesso";
                CarregarTelas();
            }
            catch (Exception ex)
            { FailureText.Text = ex.Message; }

        }
    }
}
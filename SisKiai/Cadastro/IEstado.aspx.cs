using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web;

namespace SisKiai.Cadastro
{
    public partial class IEstado : System.Web.UI.Page
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
                CarregarGrid();
        }

        public void GrdEstadoPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewEstado.PageIndex = e.NewPageIndex;
            CarregarGrid();
        }
        protected void GridEstadoCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string idEstado = GridViewEstado.DataKeys[index]["IdEstado"].ToString();

                if (e.CommandName == "Excluir")
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelEstado(idEstado);
                }
                else if (e.CommandName == "Editar")
                {
                    Regras.Regras get = new Regras.Regras();
                    Regras.Dto.DtoEstado dados = get.GetEstadoPorId(int.Parse(idEstado));
                    CarregarTela(dados);
                }
                CarregarGrid();
            }
            catch (Exception ex)
            {
                FailureText.Text = ex.Message;
            }
        }

        private void CarregarGrid()
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoEstado> dados = get.GetAllEstado();
            GridViewEstado.DataSource = dados;
            GridViewEstado.DataBind();
        }

        protected void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoEstado estado = new Regras.Dto.DtoEstado();
                if (!String.IsNullOrWhiteSpace(TextIDEstado.Text))
                {
                    estado.IdEstado = int.Parse(TextIDEstado.Text);
                }
                estado.NomeEstado = TextNomeEstado.Text.ToUpper();
                estado.SiglaEstado = TextSiglaEstado.Text.ToUpper();

                Regras.Regras set = new Regras.Regras();
                set.SetEstado(estado);

                FailureText.Text = "Sucesso!";
                CarregarGrid();
            }
            catch (Exception ex)
            {
                FailureText.Text = ex.Message;
            }
        }
        private void CarregarTela(Regras.Dto.DtoEstado dados)
        {
            TextIDEstado.Text = dados.IdEstado.ToString();
            TextNomeEstado.Text = dados.NomeEstado;
            TextSiglaEstado.Text = dados.SiglaEstado;
        }
        protected void ConsultarClick(object sender, EventArgs e)
        {
            Regras.Regras dados = new Regras.Regras();

            List<Regras.Dto.DtoEstado> estado = dados.GetEstadoNome(TextConsulta.Text);
            GridViewEstado.DataSource = estado;
            GridViewEstado.DataBind();
        }
    }
}
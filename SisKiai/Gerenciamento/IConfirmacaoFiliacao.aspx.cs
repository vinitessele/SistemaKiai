using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;

namespace SisKiai.Gerenciamento
{
    public partial class IConfirmacaoFiliacao : System.Web.UI.Page
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
                carregarAssociacao();
            }
        }

        private void carregarAssociacao()
        {
            Regras.Regras dados = new Regras.Regras();
            List<DtoAcademia> listassociacao = new List<DtoAcademia>();
            listassociacao = dados.GetAllAcademia();

            DropDownListAssociacao.DataSource = listassociacao;
            DropDownListAssociacao.DataTextField = "NomeAcademia";
            DropDownListAssociacao.DataValueField = "IdAcademia";
            DropDownListAssociacao.DataBind();
            DropDownListAssociacao.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }

        public void ListaFiliadosPorAssociacao(object sender, EventArgs e)
        {
            CarregarFiliado();
        }

        private void CarregarFiliado()
        {
            Regras.Regras dados = new Regras.Regras();

            List<DtoFiliado> filiados = new List<DtoFiliado>();

            int idAssociacao = int.Parse(DropDownListAssociacao.SelectedValue);
            filiados = dados.GetAllFiliadoPorIdAssociacao(idAssociacao);
            Session["ListaFiliados"] = filiados;
            GridFiliado.DataSource = filiados;
            GridFiliado.DataBind();
        }

        public void ConsultarClick(object sender, EventArgs e)
        {
            Regras.Regras consulta = new Regras.Regras();
            List<Regras.Dto.DtoFiliado> list = consulta.GetFiliadoPorNome(TextConsulta.Text);
            GridFiliado.DataSource = list;
            GridFiliado.DataBind();
        }

        public void InativarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras dados = new Regras.Regras();
                dados.setInativarAtletas();
                CarregarFiliado();
            }
            catch (Exception ex)
            {
                FailureText.Text = string.Format("Erro ao inativar.{0}", ex.Message);
            }
        }
        public void GrdFiliadoPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridFiliado.PageIndex = e.NewPageIndex;
            GridFiliado.DataSource = Session["ListaFiliados"];
            GridFiliado.DataBind();
        }

        public void GridFiliadoCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Regras.Regras dados = new Regras.Regras();
                int index = Convert.ToInt32(e.CommandArgument);
                string idFiliado = GridFiliado.DataKeys[index]["IdFiliado"].ToString();
                Session["IdFiliado"] = idFiliado;
                if (e.CommandName == "Bloquear")
                {
                    dados.BloquearFiliado(int.Parse(idFiliado));
                }
                else if (e.CommandName == "Liberar")
                {
                    dados.LiberarFiliado(int.Parse(idFiliado));
                }
                CarregarFiliado();
                FailureText.Text = "Operação realizado com Sucesso";

            }
            catch (Exception ex)
            {
                FailureText.Text = string.Format("Erro ao confirmar Filiação do atleta.{0}", ex.Message);
            }
        }

        protected void GridFiliadoDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idFiliado = (string)this.GridFiliado.DataKeys[e.Row.RowIndex]["IdFiliado"].ToString();
                Session["IdFiliado"] = idFiliado;

                Regras.Regras get = new Regras.Regras();
                Regras.Dto.DtoFiliado dados = get.GetFiliadoPorId(int.Parse(idFiliado));

                if (dados != null)
                {
                    if (dados.StatusFiliado.Equals("A"))
                    {
                        ((ImageButton)e.Row.Cells[8].Controls[0]).ImageUrl = "~/Imagens/lock.png";
                        ((ImageButton)e.Row.Cells[8].Controls[0]).AlternateText = "Bloquear";
                        ((ImageButton)e.Row.Cells[8].Controls[0]).CommandName = "Bloquear";
                    }
                    else
                    {
                        ((ImageButton)e.Row.Cells[8].Controls[0]).ImageUrl = "~/Imagens/Unlock.png";
                        ((ImageButton)e.Row.Cells[8].Controls[0]).AlternateText = "Liberar";
                        ((ImageButton)e.Row.Cells[8].Controls[0]).CommandName = "Liberar";
                    }
                }
            }
        }


    }
}
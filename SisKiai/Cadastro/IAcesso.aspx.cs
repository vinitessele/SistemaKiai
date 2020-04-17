using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;
using System.Web;

namespace SisKiai.Cadastro
{
    public partial class IAcesso : System.Web.UI.Page
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
                CarregarAllTelas();
                CarregarAssociacao();
                CarregarGrid();
            }
        }

        private void CarregarGrid()
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoAcesso> lista = get.GetAllLogin();
            GridViewLogin.DataSource = lista;
            GridViewLogin.DataBind();
        }

        private void CarregarAllTelas()
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoTelas> lista = get.GetAllTelas();
            ListBoxTelasDisponiveis.DataSource = lista;
            ListBoxTelasDisponiveis.DataTextField = "Nome";
            ListBoxTelasDisponiveis.DataValueField = "IdTelas";
            ListBoxTelasDisponiveis.DataBind();
        }
        private void CarregarAssociacao()
        {
            Regras.Regras dados = new Regras.Regras();
            List<Regras.Dto.DtoAcademia> associacao = dados.GetAllAcademia();
            DropDownListAssociacao.DataSource = associacao;
            DropDownListAssociacao.DataTextField = "NomeAcademia";
            DropDownListAssociacao.DataValueField = "IdAcademia";
            DropDownListAssociacao.DataBind();
            DropDownListAssociacao.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }
        public void GridLoginCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int n = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out n))
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    string idAcesso = GridViewLogin.DataKeys[index]["IdAcesso"].ToString();
                    Session["idLogin"] = idAcesso;
                    if (e.CommandName == "Excluir")
                    {
                        Regras.Regras del = new Regras.Regras();
                        del.DelLoginAcesso(idAcesso);
                    }
                    else if (e.CommandName == "Editar")
                    {
                        Regras.Regras get = new Regras.Regras();
                        Regras.Dto.DtoAcesso dados = get.GetLoginPorId(int.Parse(idAcesso));
                        CarregaTela(dados);
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

        private void CarregaTela(DtoAcesso dados)
        {
            TextIdAcesso.Text = dados.IdAcesso.ToString();
            TextNome.Text = dados.Nome;
            TextLogin.Text = dados.Login;
            TextSenha.Text = dados.Senha;
            if (!String.IsNullOrWhiteSpace(dados.IdAssociacao.ToString()))
                DropDownListAssociacao.SelectedValue = dados.IdAssociacao.ToString();
            if (!String.IsNullOrWhiteSpace(dados.Administrador.ToString()))
                CheckBoxAdm.Checked = dados.Administrador.Value;
            if (!String.IsNullOrWhiteSpace(dados.StatusLogin.ToString()))
                CheckBoxStatus.Checked = dados.StatusLogin.Value;
            ControleListBox(dados);
        }

        private void ControleListBox(DtoAcesso dados)
        {
            CarregarAllTelas();

            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoLoginTela> listaTelasLiberadas = get.GetTelasLiberadasPorIdLogin(dados.IdAcesso);
            List<ListItem> itemsToRemove = new List<ListItem>();
            foreach (var lst in listaTelasLiberadas)
            {
                foreach (ListItem l in ListBoxTelasDisponiveis.Items)
                {
                    if (lst.IdTela == long.Parse(l.Value))
                    {
                        itemsToRemove.Add(l);
                    }
                }
            }
            foreach (ListItem listIem in itemsToRemove)
            {
                ListBoxTelasDisponiveis.Items.Remove(listIem);
            }
            ListBoxTelasLiberadas.DataSource = listaTelasLiberadas;
            ListBoxTelasLiberadas.DataTextField = "NomeTela";
            ListBoxTelasLiberadas.DataValueField = "IdTela";
            ListBoxTelasLiberadas.DataBind();
        }

        public void GrdLoginPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewLogin.PageIndex = e.NewPageIndex;
            CarregarGrid();
        }

        public void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoAcesso dados = new Regras.Dto.DtoAcesso();
                if (!String.IsNullOrWhiteSpace(TextIdAcesso.Text))
                    dados.IdAcesso = int.Parse(TextIdAcesso.Text);
                dados.Nome = TextNome.Text;
                dados.Login = TextLogin.Text;
                dados.Senha = TextSenha.Text;
                dados.Administrador = CheckBoxAdm.Checked;
                dados.StatusLogin = CheckBoxStatus.Checked;
                dados.IdAssociacao = int.Parse(DropDownListAssociacao.SelectedValue);
                Regras.Regras set = new Regras.Regras();
                long idLogin = set.SetAcesso(dados);
                Session["idLogin"] = idLogin;
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
        public void ConsultarClick(object sender, EventArgs e)
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoAcesso> lista = get.GetLoginPorNome(TextConsulta.Text);
            GridViewLogin.DataSource = lista;
            GridViewLogin.DataBind();
        }
        public void RemoveTodosClick(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem lst in ListBoxTelasLiberadas.Items)
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelLoginTelas(lst.Value, Session["idLogin"].ToString());
                }
                ListBoxTelasLiberadas.Items.Clear();
                CarregarAllTelas();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        public void RemoveClick(object sender, EventArgs e)
        {
            try
            {
                if (ListBoxTelasLiberadas.Items.Count != 0)
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelLoginTelas(ListBoxTelasLiberadas.SelectedValue, Session["idLogin"].ToString());

                    ListBoxTelasDisponiveis.Items.Add(ListBoxTelasLiberadas.SelectedItem);
                    ListBoxTelasLiberadas.Items.RemoveAt(ListBoxTelasLiberadas.SelectedIndex);
                }
                else
                {
                    throw new Exception("Selecione ao menos uma tela");
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        public void AdicionaTodosClick(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem lst in ListBoxTelasDisponiveis.Items)
                {
                    lst.Selected = true;
                    ListBoxTelasLiberadas.Items.Add(lst);
                    lst.Selected = false;

                    Regras.Dto.DtoLoginTela dados = new DtoLoginTela();
                    dados.IdLogin = long.Parse(Session["idLogin"].ToString());
                    dados.IdTela = long.Parse(lst.Value);
                    Regras.Regras set = new Regras.Regras();
                    set.SetLoginTelas(dados);
                }
                ListBoxTelasDisponiveis.Items.Clear();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        public void AdicionaClick(object sender, EventArgs e)
        {
            try
            {
                if (ListBoxTelasDisponiveis.Items.Count != 0)
                {
                    ListBoxTelasLiberadas.Items.Add(ListBoxTelasDisponiveis.SelectedItem);
                    ListBoxTelasDisponiveis.Items.RemoveAt(ListBoxTelasDisponiveis.SelectedIndex);

                    foreach (ListItem lst in ListBoxTelasLiberadas.Items)
                    {
                        Regras.Dto.DtoLoginTela dados = new DtoLoginTela();
                        dados.IdLogin = long.Parse(Session["idLogin"].ToString());
                        dados.IdTela = long.Parse(lst.Value);
                        Regras.Regras set = new Regras.Regras();
                        set.SetLoginTelas(dados);
                    }
                }
                else
                {
                    throw new Exception("Selecione ao menos uma tela");
                }
                ListBoxTelasLiberadas.ClearSelection();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }
    }
}
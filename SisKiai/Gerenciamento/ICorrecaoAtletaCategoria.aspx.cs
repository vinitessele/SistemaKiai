using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;

namespace SisKiai.Gerenciamento
{
    public partial class ICorrecaoAtletaCategoria : System.Web.UI.Page
    {
        public enum StatusFiliado
        {
            Pendente,
            Ativo,
            Inativo,
        }

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
                bool adm = bool.Parse(Session["Administrador"].ToString());
                if (adm)
                {
                    CarregarCompeticaoAtiva();
                }
            }
        }

        private void CarregarAssociacao()
        {
            Regras.Regras dados = new Regras.Regras();
            List<DtoAcademia> listassociacao = new List<DtoAcademia>();
            long idCompeticao = long.Parse(DropDownListCompeticao.SelectedValue);
            listassociacao = dados.GetAllAcademiaParticipante(idCompeticao);

            DropDownListAssociacao.DataSource = listassociacao;
            DropDownListAssociacao.DataTextField = "NomeAcademia";
            DropDownListAssociacao.DataValueField = "idacademia";
            DropDownListAssociacao.DataBind();
            DropDownListAssociacao.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }

        private void CarregarCompeticaoAtiva()
        {
            Regras.Regras dados = new Regras.Regras();
            List<DtoCompeticao> competicao = dados.GetAllCompeticaoAtiva10Dias();

            DropDownListCompeticao.DataSource = competicao;
            DropDownListCompeticao.DataTextField = "NomeCompeticao";
            DropDownListCompeticao.DataValueField = "IdCompeticao";
            DropDownListCompeticao.DataBind();
            DropDownListCompeticao.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }

        private void CarregarCategoriasCompeticao(string idCompeticao)
        {
            Regras.Regras get = new Regras.Regras();
            DtoCompeticao dados = get.GetCompeticaoPorId(int.Parse(idCompeticao));
            HabilitaCampos(dados);
            List<DtoCategoriasCompeticao> list = get.GetAllCategoriaPorCompeticao(idCompeticao);
            List<DtoCategoriasCompeticao> lista = new List<DtoCategoriasCompeticao>();
            foreach (var l in list)
            {
                DtoCategoriasCompeticao a = new DtoCategoriasCompeticao();
                a.NomeCategoria = l.NrCategoria + " - " + l.NomeCategoria;
                a.IdCategoria = l.IdCategoria;
                lista.Add(a);
            }

            DropDownListCategoria.DataSource = lista;
            DropDownListCategoria.DataTextField = "NomeCategoria";
            DropDownListCategoria.DataValueField = "IdCategoria";
            DropDownListCategoria.DataBind();
            DropDownListCategoria.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }

        private void HabilitaCampos(DtoCompeticao dados)
        {
            TextNumeroRegistro.Enabled = true;
        }

        #region Formulário

        public void DropDownListComnpeticaoChanged(object sender, EventArgs e)
        {
            Session["idCompeticaoSelecionada"] = DropDownListCompeticao.SelectedValue;
            CarregarCategoriasCompeticao(DropDownListCompeticao.SelectedValue);
            CarregarAssociacao();
        }

        public void DropDownListCategoriaChanged(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void AtualizaGrid()
        {
            Regras.Regras get = new Regras.Regras();
            List<DtoCategoriaAtleta> list = get.GetAllAtletasPorCategoria(DropDownListCategoria.SelectedValue, DropDownListCompeticao.SelectedValue);
            GridAtletas.DataSource = list;
            GridAtletas.DataBind();
        }

        #endregion

        protected void GridAtletasCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string idInscricao = GridAtletas.DataKeys[index]["IdinscriaAtleta"].ToString();

                if (e.CommandName == "Excluir")
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelInscricaoCompeticao(idInscricao, DropDownListCategoria.SelectedValue, DropDownListCompeticao.SelectedValue);
                }
                AtualizaGrid();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        protected void AdicionarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras set = new Regras.Regras();
                long idInscricaoAtleta = 0;
                if (!String.IsNullOrWhiteSpace(TextNumeroRegistro.Text))
                    idInscricaoAtleta = set.GetInscricaoAtleta(TextNumeroRegistro.Text, DropDownListCompeticao.SelectedValue);
                
                if (idInscricaoAtleta == 0)
                {
                    idInscricaoAtleta = set.SetCorrecaoCategoria(DropDownListCategoria.SelectedValue, DropDownListCompeticao.SelectedValue, TextNome.Text, DropDownListAssociacao.SelectedValue, TextNumeroRegistro.Text);
                }
                else
                {
                    idInscricaoAtleta = set.GetInscricaoAtleta(TextNumeroRegistro.Text, DropDownListCompeticao.SelectedValue);
                }
                DtoInscricaoAtleta dados = new DtoInscricaoAtleta();
                dados.IdInscricaoAtleta = idInscricaoAtleta;
                dados.IdCategoria = int.Parse(DropDownListCategoria.SelectedValue);
                if (!String.IsNullOrWhiteSpace(TextNumeroRegistro.Text))
                    dados.NumeroRegistro = int.Parse(TextNumeroRegistro.Text);
                long idInscricaoCategoria = set.SetInscriAtletaCategoria(dados, idInscricaoAtleta);

                AtualizaGrid();
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

        public void BuscaFiliadoPorRegistro(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras dados = new Regras.Regras();
                DtoFiliado filiado = new DtoFiliado();

                int numeroRegistro = int.Parse(TextNumeroRegistro.Text);
                filiado = dados.GetFiliadoPorRegistro(numeroRegistro);

                if (filiado.StatusFiliado == StatusFiliado.Ativo.ToString().Substring(0, 1))
                {
                    TextNome.Text = filiado.NomeFiliado.ToString();
                    try
                    {
                        DropDownListAssociacao.SelectedValue = filiado.IdAssociacao.ToString();
                    }
                    catch (Exception )
                    {
                        //throw new Exception("Filiado de outra Associação que não esta presente no evento.");
                    }
                }
                else
                {
                    throw new Exception("Filiado Inativo ou bloqueado procure o responsável pelo sistema do Sistema");
                }
                if (filiado == null)
                {
                    throw new Exception("Filiado Não encontrado");
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }
    }
}
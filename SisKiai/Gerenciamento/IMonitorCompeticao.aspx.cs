using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;
using System.Data;

namespace SisKiai.Gerenciamento
{
    public partial class IMonitorCompeticao : System.Web.UI.Page
    {
        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }
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
                Session["idCompeticaoSelecionada"] = null;
            }
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

        public void DropDownListComnpeticaoChanged(object sender, EventArgs e)
        {
            try
            {
                Session["idCompeticaoSelecionada"] = DropDownListCompeticao.SelectedValue;
                //ListaCategorias(Session["idCompeticaoSelecionada"].ToString());
                CountCompetidoresPorAssociacao(Session["idCompeticaoSelecionada"].ToString());
                QuadroDeMedalhas(Session["idCompeticaoSelecionada"].ToString(), string.Empty, string.Empty);
                AndamentoCompeticao(Session["idCompeticaoSelecionada"].ToString());
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

        private void AndamentoCompeticao(string idCompeticao)
        {
            Regras.Regras get = new Regras.Regras();

            List<DtoCategoriasCompeticao> categorias = get.GetAllCategoriaPorCompeticao(idCompeticao);
            int countCategorias = categorias.Count();
            List<DtoCategoriasCompeticao> CategoriasCompeticaoFinalizada = get.GetAllPorCategoriaFinalizadas(idCompeticao);
            int countCategoriasFinalizadas = CategoriasCompeticaoFinalizada.Count();
            List<DtoGraficoAndamentoCompeticao> listcount = new List<DtoGraficoAndamentoCompeticao>();
            bool first = true;
            for (int i = 0; i < 2; i++)
            {
                DtoGraficoAndamentoCompeticao count = new DtoGraficoAndamentoCompeticao();
                if (first)
                {
                    count.Nome = "Total de Categoras ";
                    count.Count = countCategorias;
                    first = false;
                }
                else
                {
                    count.Nome = "Categorias Finalizadas ";
                    count.Count = countCategoriasFinalizadas;
                }
                listcount.Add(count);
            }
            GraficoAndamentoCompeticao.DataSource = listcount;
            GraficoAndamentoCompeticao.DataBind();
        }

        private void QuadroDeMedalhas(string idCompeticao, string ordenacao, string colunaOrdenacao)
        {
            Regras.Regras get = new Regras.Regras();
            List<DtoQuadroMedalhas> quadrodeMedalhas = get.GetQuadroMedalhaPorCompeticao(idCompeticao, ordenacao, colunaOrdenacao);
            GridQuadrodeMedalhas.DataSource = quadrodeMedalhas;
            GridQuadrodeMedalhas.DataBind();
            Session["QuadrodeMedalhas"] = quadrodeMedalhas;
        }

        private void CountCompetidoresPorAssociacao(string idCompeticao)
        {
            GetAllAtletasPorCompeticao(idCompeticao);
        }

        private void GetAllAtletasPorCompeticao(string idCompeticao)
        {
            Regras.Regras get = new Regras.Regras();
            List<DtoAtletasCompeticao> atletas = get.GetCountAllAtletasPorCompeticao(idCompeticao);
            long countAtletas = atletas.Sum(p => p.QteAtletas).Value;
            long countAssociacoes = atletas.Count();
            LiteralQteAtleta.Text = string.Format("Total de atletas inscritos.: {0} de {1}, Associações ou Academias", countAtletas, countAssociacoes);
            QteAtletas.DataSource = atletas;
            QteAtletas.DataBind();
        }

        #region Grid

        protected void GridMedalhasSorting(object sender, GridViewSortEventArgs e)
        {
            string colunaSelecionada = e.SortExpression;

            if (dir == SortDirection.Ascending)
            {
                dir = SortDirection.Descending;
                QuadroDeMedalhas(Session["idCompeticaoSelecionada"].ToString(), dir.ToString(), colunaSelecionada);
            }
            else
            {
                dir = SortDirection.Ascending;
                QuadroDeMedalhas(Session["idCompeticaoSelecionada"].ToString(), dir.ToString(), colunaSelecionada);
            }
            CountCompetidoresPorAssociacao(Session["idCompeticaoSelecionada"].ToString());
        }

        #endregion

        protected void ResultadoCompeticao(object sender, EventArgs e)
        {
            Response.Redirect("~/Relatório/ResultadoCompeticao.aspx?IdCompeticao=" + Session["idCompeticaoSelecionada"].ToString());
        }

        protected void GridQuadrodeMedalhasCommand(object sender, GridViewCommandEventArgs e)
        {
            string clicado = e.CommandArgument.ToString();
            int index;
            if (int.TryParse(clicado, out index))
                index = Convert.ToInt32(e.CommandArgument);
            string IdAssociacao = GridQuadrodeMedalhas.DataKeys[index]["IdAssociacao"].ToString();

            switch (e.CommandName)
            {
                case "ListaResultados":
                    Response.Redirect("~/Relatório/RelotorioResultadoPorAssociacao.aspx?IdAssociacao=" + IdAssociacao + "&IdCompeticao=" + Session["idCompeticaoSelecionada"].ToString());
                    break;
            }
        }
        protected void EmailQuadroMedalha(object sender, EventArgs e)
        {
            Regras.Regras get = new Regras.Regras();
            List<DtoQuadroMedalhas> quadrodeMedalhas = (List<DtoQuadroMedalhas>)Session["QuadrodeMedalhas"];

            string idCompeticaoSelecionada = Session["idCompeticaoSelecionada"].ToString();
            List<DtoAcademia> listaAssociacoes = get.GetAllAcademiaParticipante(long.Parse(idCompeticaoSelecionada));

            foreach (var l1 in listaAssociacoes)
            {
                string msg = string.Empty;
                string competicao = get.GetCompeticaoPorId(int.Parse(idCompeticaoSelecionada)).NomeCompeticao.ToString();
                string assunto = "Quadro de Medalhas";
                msg = "SysKiai - Quadro de Medalhas<br>";
                msg += competicao + "<br><br>";

                foreach (var l in quadrodeMedalhas)
                {

                    msg += l.NomeAssociacao + "<br><br>";
                    msg += l.Primeiro;
                    msg += " Ouro - ";
                    msg += l.Segundo;
                    msg += " Prata - ";
                    msg += l.Terceiro;
                    msg += " Bronze - ";
                    msg += l.Quarto;
                    msg += " Quarto - ";
                    msg += l.Quinto;
                    msg += " Quinto - ";
                    msg += l.TotalMedalhas;
                    msg += "Total de Medalhas - ";
                    msg += l.TotalPontos;
                    msg += " Total de Pontos - ";

                    msg += "<br><br>";
                }
                byte[] bytes = null;
                Regras.Email envia = new Regras.Email();
                envia.EnviaEmail("presidente@fprke.com.br", "mensagem@fprke.com.br", "vinicius_tessele@hotmail.com", msg, assunto, bytes, true);
            }
            //string url = Request.Url.ToString();

            //System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
            //QuadroGeraldeMedalha.RenderControl(htmlWriter);
            //string s = stringWriter.ToString();
            //Response.Write(s);
            //byte[] bytes = null;
            //Regras.Email envia = new Regras.Email();
            //envia.EnviaEmail("presidente@fprke.com.br", "mensagem@fprke.com.br", "vinicius_tessele@hotmail.com", s, "Resultado por Associação", bytes, false);
            //Response.End();


        }
    }
}
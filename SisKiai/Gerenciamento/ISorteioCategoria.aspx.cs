using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Script.Serialization;
using SelectPdf;

namespace SisKiai.Gerenciamento
{
    public partial class ISorteioCategoria : System.Web.UI.Page
    {
        String json = System.Web.Helpers.Json.Encode(null);
        public static List<DtoRelacao> listChave = new List<DtoRelacao>();
        public static int StaticIdCategoria = 0;

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

        #region Formulário

        public void DropDownListComnpeticaoChanged(object sender, EventArgs e)
        {
            Session["idCompeticaoSelecionada"] = DropDownListCompeticao.SelectedValue;
            CarregarCategoriasCompeticao(DropDownListCompeticao.SelectedValue);
        }

        #endregion

        #region Métodos

        private void CarregarCategoriasCompeticao(string idCompeticao)
        {
            Regras.Regras get = new Regras.Regras();
            List<DtoCategoriasCompeticao> list = get.GetAllCategoriaPorCompeticao(idCompeticao);
            Session["AllCategorias"] = list;
            GridCategoria.DataSource = list;
            GridCategoria.DataBind();
            BtnRelacaoConferencia.Visible = true;
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

        #endregion

        #region Eventos Grid

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Regras.Regras get = new Regras.Regras();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string IdCategoria = GridCategoria.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView GridAtletasCategoria = e.Row.FindControl("GridAtletasCategoria") as GridView;
                    string idCompeticaoSelecionada = Session["idCompeticaoSelecionada"].ToString();
                    List<DtoCategoriaAtleta> list = get.GetAllAtletasPorCategoria(IdCategoria, idCompeticaoSelecionada);
                    GridAtletasCategoria.DataSource = list;
                    GridAtletasCategoria.DataBind();
                }
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

        protected void GridCategoriaOnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(e.CommandArgument.ToString()))
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    string idCategoria = GridCategoria.DataKeys[index]["IdCategoria"].ToString();
                    StaticIdCategoria = int.Parse(idCategoria.ToString());
                    Regras.Regras get = new Regras.Regras();
                    List<DtoSorteio> ListaSorteio = new List<DtoSorteio>();
                    switch (e.CommandName)
                    {
                        case "SorteioEliminatoriaSimples":
                            ListaSorteio = get.SorteioEliminatoriaSimples(int.Parse(idCategoria));
                            var sorteioJaRealizado = ListaSorteio.Where(x => x.PosicaoSorteio != null);
                            Sorteio(ListaSorteio);
                            CarregarCategoriasCompeticao(DropDownListCompeticao.SelectedValue);
                            break;
                        case "GerarChave":
                            ListaSorteio = get.SorteioEliminatoriaSimples(int.Parse(idCategoria));
                            var RealizarNovoSorteio = ListaSorteio.Count(p => p.PosicaoSorteio == null);
                            if (RealizarNovoSorteio == 0)
                                Response.Redirect("~/Gerenciamento/IChaves.aspx?IdCategoriaCompeticao=" + StaticIdCategoria + "&IdCompeticao=" + Session["idCompeticaoSelecionada"].ToString() + "&EnviaEmail=False");
                            else
                                throw new Exception("Atleta adicionado após realizar sorteio, Realizar novo Sorteio.");
                            break;
                        case "EnviarEmail":
                            string url = "~/Gerenciamento/IChaves.aspx?IdCategoriaCompeticao=" + StaticIdCategoria + "&IdCompeticao=" + Session["idCompeticaoSelecionada"].ToString() + "&EnviaEmail=True";
                            EnviaEmail(url, Session["idCompeticaoSelecionada"].ToString());
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        private void EnviaEmail(string url, string idCompeticao)
        {
            try
            {
                Regras.Regras get = new Regras.Regras();

                List<DtoAcademia> associacao = get.GetAllAcademiaParticipante(int.Parse(idCompeticao));
                DtoCompeticao competicao = get.GetCompeticaoPorId(int.Parse(idCompeticao.ToString()));
                DtoCategoriasCompeticao categoria = get.GetCategoriasPorIdCategoriaIdCompeticao(StaticIdCategoria, int.Parse(idCompeticao.ToString()));
                List<DtoCategoriaAtleta> listAtletas = get.GetAllAtletasPorCategoria(categoria.IdCategoriaCompeticao.ToString(), idCompeticao.ToString());
                string msg = string.Empty;
                string assunto = "Inscritos por Categoria " + categoria.NomeCategoria;
                msg = "SysKiai - Listagem para Conferência de Categorias " + competicao.NomeCompeticao + "<br><br>";
                msg += "<br>";
                msg += categoria.NomeCategoria + "<br>";
                foreach (var li in listAtletas)
                {
                    msg += li.NomeFiliado + " <br> ";
                }
                byte[] bytes = null;
                if (listAtletas.Count() > 0)
                {
                    Regras.Email envia = new Regras.Email();
                    envia.EnviaEmail("presidente@fprke.com.br", "mensagem@fprke.com.br", "mensagem@fprke.com.br", msg, assunto, bytes, true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Sorteio(List<DtoSorteio> ListaSorteio)
        {
            int Nc = ListaSorteio.Count(); // Numero de Competidores
            int Nj = Nc - 1;
            int Nr = Nj * 2;
            List<int> ListNrSorteio = new List<int>();
            ListNrSorteio.Clear();
            int nrGerado = 0;

            Random randNum = new Random();

            while (ListNrSorteio.Count < Nc)
            {
                nrGerado = randNum.Next(1, Nc + 1);
                //Se não tiver o numero gerado na lista insere
                if (!ListNrSorteio.Contains(nrGerado))
                {
                    ListNrSorteio.Add(nrGerado);
                }
            }
            for (int x = 0; x < Nc; x++)
            {
                try
                {
                    ListaSorteio[x].PosicaoSorteio = int.Parse(ListNrSorteio[x].ToString());
                }
                catch
                {
                }
            }

            ValidaSorteio(ListaSorteio);
            Regras.Regras set = new Regras.Regras();
            set.SetSorteio(ListaSorteio);
        }

        private void ValidaSorteio(List<DtoSorteio> ListaSorteio)
        {
            int QuantosAtletas = ListaSorteio.Count();
            int QuantasAssociacoes = ListaSorteio.Select(s => s.IdAssociacao).Distinct().Count();
            if (QuantasAssociacoes > 1)
            {
                var ListaAtletaPorAssociacao = ListaSorteio.GroupBy(g => g.IdAssociacao).ToList();
                bool first = true;
                long? posicao = 0;
                if (ListaAtletaPorAssociacao.Count() > 1)
                {
                    int count = 0;
                    foreach (var l in ListaAtletaPorAssociacao)
                    {
                        count++;
                        if (first)
                        {
                            posicao = l.FirstOrDefault().PosicaoSorteio;
                            first = false;
                        }
                        else
                        {
                            if (l.FirstOrDefault().PosicaoSorteio - 1 == posicao)
                            {
                                if (count <= 1)
                                    Sorteio(ListaSorteio);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        protected void BtnConferencia(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras get = new Regras.Regras();
                string idCompeticaoSelecionada = Session["idCompeticaoSelecionada"].ToString();
                List<DtoAcademia> listaAssociacoes = get.GetAllAcademiaParticipante(long.Parse(idCompeticaoSelecionada));

                foreach (var l in listaAssociacoes)
                {
                    if (!string.IsNullOrWhiteSpace(l.EmailAcademia))
                    {
                        List<DtoInscricaoAtleta> listAtletas = get.GetInscritoPorAssociacao(int.Parse(l.IdAcademia.ToString()), int.Parse(idCompeticaoSelecionada));
                        string msg = string.Empty;
                        string competicao = get.GetCompeticaoPorId(int.Parse(idCompeticaoSelecionada)).NomeCompeticao.ToString();
                        string assunto = "Inscritos " + l.NomeAcademia + " ";
                        msg = "SysKiai - Listagem para Conferência " + competicao + "<br><br>"; ;
                        msg += "<br>";
                        foreach (var li in listAtletas)
                        {
                            msg += "<br>";
                            if (li.NumeroRegistro != null)
                                msg += li.NumeroRegistro + " - ";
                            else
                                msg += "      ";
                            msg += li.NomeAtleta + " <br> ";
                            msg += li.NumeroCategoria + " - ";
                            msg += li.NomeCategoria;
                        }
                        byte[] bytes = null;
                        if (listAtletas.Count() > 0)
                        {
                            Regras.Email envia = new Regras.Email();
                            envia.EnviaEmail("presidente@fprke.com.br", "mensagem@fprke.com.br", l.EmailAcademia, msg, assunto, bytes, true);
                        }
                    }
                    else
                    {
                        List<DtoInscricaoAtleta> listAtletas = get.GetInscritoPorAssociacao(int.Parse(l.IdAcademia.ToString()), int.Parse(idCompeticaoSelecionada));
                        string msg = string.Empty;
                        string competicao = get.GetCompeticaoPorId(int.Parse(idCompeticaoSelecionada)).NomeCompeticao.ToString();
                        string assunto = "Inscritos " + l.NomeAcademia + " ";
                        msg = "SysKiai - Listagem para Conferência " + competicao + "<br><br>"; ;
                        msg += "<br>";
                        foreach (var li in listAtletas)
                        {
                            msg += "<br>";
                            if (li.NumeroRegistro != null)
                                msg += li.NumeroRegistro + " - ";
                            else
                                msg += "      ";
                            msg += li.NomeAtleta + " <br> ";
                            msg += li.NumeroCategoria + " - ";
                            msg += li.NomeCategoria;
                        }
                        byte[] bytes = null;
                        if (listAtletas.Count() > 0)
                        {
                            Regras.Email envia = new Regras.Email();
                            envia.EnviaEmail("presidente@fprke.com.br", "mensagem@fprke.com.br", "mensagem@fprke.com.br", msg, assunto, bytes, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
                // FailureText.Text = string.Format("Erro ou enviar E-mail.:{0}", ex.Message);
            }
        }

        protected void BtnConferenciaCategoria(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;
using System.Web.Script.Services;
using System.Web.Services;
using System.IO;



namespace SisKiai.Gerenciamento
{
    public partial class IInscricao : System.Web.UI.Page
    {
        public enum StatusFiliado
        {
            Pendente,
            Ativo,
            Inativo,
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginCookie();

            if (!IsPostBack)
            {
                bool adm = bool.Parse(Session["Administrador"].ToString());

                if (!adm)
                {
                    CarregaCompeticaoAtiva();
                    CarregarAssociacao();
                }
            }
        }

        private void LoginCookie()
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
        }

        #region Grid

        public void GrdAtletaPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridAtletas.PageIndex = e.NewPageIndex;
            CarregarGridAtleta();
        }

        public void GridCompeicoesPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridCompeicoes.PageIndex = e.NewPageIndex;
            CarregarGridAtleta();
        }

        public void GridAtletasCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int n = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out n))
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    string idInscricaoAtleta = GridAtletas.DataKeys[index]["IdInscricaoAtleta"].ToString();
                    int idinscricao = int.Parse(Session["IdInscricao"].ToString());

                    if (e.CommandName == "Excluir")
                    {
                        Regras.Regras del = new Regras.Regras();
                        del.DelInscricaoAtelta(idInscricaoAtleta, idinscricao);
                        CarregarGridAtleta();
                    }
                }
            }
            catch (Exception)
            {
                //FailureText.Text = ex.Message;
            }
        }

        public void GridCompeicoesCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int n = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out n))
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    string idCompeticao = GridCompeicoes.DataKeys[index]["IdCompeticao"].ToString();

                    if (e.CommandName == "Inscrever")
                    {
                        Session["IdCompeticao"] = idCompeticao;

                        MenuItem mnuItem = Menu1.FindItem("1");
                        mnuItem.Enabled = true;
                        CarregaTela(long.Parse(idCompeticao));
                    }
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
                //FailureText.Text = string.Format("Erro ao selecionar competição.{0}", ex.Message);
            }
        }

        #endregion

        #region Métodos

        private void LiberaCampos(DtoCompeticao competicao)
        {
            TextNomeFiliado.Enabled = true;
            TextIdAtleta.Enabled = true;
            TextDtNascimento.Enabled = true;
            TextIdade.Enabled = true;
            TextPeso.Enabled = true;
            TextAltura.Enabled = true;
            DropDownListGraduacao.Enabled = true;
            ListAtletasCadastrados.Visible = false;
            btnSeleciona.Visible = false;
        }

        private void BloqueiaCampos(DtoCompeticao competicao)
        {
            TextNomeFiliado.Enabled = false;
            TextIdAtleta.Enabled = false;
            TextDtNascimento.Enabled = false;
            TextIdade.Enabled = false;
            TextPeso.Enabled = false;
            TextAltura.Enabled = false;
            DropDownListGraduacao.Enabled = false;
            ListAtletasCadastrados.Visible = true;
            btnSeleciona.Visible = true;
        }

        private void CarregarAtletasPorAssociaçao()
        {
            try
            {
                Regras.Regras get = new Regras.Regras();
                List<DtoFiliado> Atleta = get.GetFiliadoPorIdAssociacao(int.Parse(Session["IdAssociacao"].ToString()));
                if (Atleta != null)
                {
                    if (Session["IdCompeticao"] != null)
                    {
                        DtoCompeticao dados = get.GetCompeticaoPorId(int.Parse(Session["IdCompeticao"].ToString()));

                        if (dados.PermiteNaoFiliado == false)
                        {
                            Atleta = Atleta.Where(p => p.NumeroRegistro != null && p.StatusFiliado == "A").ToList();
                            List<DtoInscricaoAtleta> AtletaInscricao = (List<DtoInscricaoAtleta>)Session["ListAtletaInscritos"];
                            List<DtoFiliado> listaAtletas = new List<DtoFiliado>();
                            foreach (var item in Atleta)
                            {
                                DtoFiliado filiado = new DtoFiliado();
                                if (!AtletaInscricao.Any(e => e.NumeroRegistro == item.NumeroRegistro))
                                {
                                    filiado = item;
                                    listaAtletas.Add(filiado);
                                }
                            }

                            Session["ListAtletasPorAssociacao"] = listaAtletas.OrderBy(x => x.NomeFiliado);
                            ListAtletasCadastrados.DataSource = Session["ListAtletasPorAssociacao"];
                            ListAtletasCadastrados.DataTextField = "NomeFiliado";
                            ListAtletasCadastrados.DataValueField = "NumeroRegistro";
                            ListAtletasCadastrados.DataBind();
                        }
                        else
                        {
                            List<DtoInscricaoAtleta> AtletaInscricao = (List<DtoInscricaoAtleta>)Session["ListAtletaInscritos"];
                            List<DtoFiliado> listaAtletas = new List<DtoFiliado>();
                            foreach (var item in Atleta)
                            {
                                DtoFiliado filiado = new DtoFiliado();
                                if (!AtletaInscricao.Any(e => e.IdAtleta == item.IdFiliado))
                                {
                                    filiado = item;
                                    listaAtletas.Add(filiado);
                                }
                            }
                            var ListAtletasPorAssociacao = listaAtletas.OrderBy(x => x.NomeFiliado);
                            Session["ListAtletasPorAssociacao"] = ListAtletasPorAssociacao;
                            ListAtletasCadastrados.DataSource = Session["ListAtletasPorAssociacao"];
                            ListAtletasCadastrados.DataTextField = "NomeFiliado";
                            ListAtletasCadastrados.DataValueField = "IdFiliado";
                            ListAtletasCadastrados.DataBind();
                        }
                    }
                }
                else
                {
                    ListAtletasCadastrados.Visible = false;
                    btnSeleciona.Visible = false;
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
                //FailureText.Text = "Erro ao Carregar Atletas por associação.:" + ex.Message;
            }
        }

        private void TrataCampos(DtoCompeticao dados)
        {
            if (dados.PermiteNaoFiliado == true)
            {
                TextNumeroRegistro.Enabled = false;
                TextIdAtleta.Enabled = false;
                TextNomeFiliado.Enabled = true;
                TextDtNascimento.Enabled = true;
                TextIdade.Enabled = true;
                TextPeso.Enabled = true;
                TextAltura.Enabled = true;
                DropDownListGraduacao.Enabled = true;
                Permite.Text = dados.PermiteNaoFiliadoDescricao;
                MenuItem mnuItem = Menu1.FindItem("3");
                mnuItem.Enabled = true;
            }
            else
            {
                TextNumeroRegistro.Enabled = false;
                TextIdAtleta.Enabled = false;
                TextNomeFiliado.Enabled = false;
                TextDtNascimento.Enabled = false;
                TextIdade.Enabled = false;
                TextPeso.Enabled = false;
                TextAltura.Enabled = false;
                DropDownListGraduacao.Enabled = false;
                Permite.Text = dados.PermiteNaoFiliadoDescricao;
            }
        }

        private void VerificaInscricaoAberta()
        {
            try
            {
                Regras.Regras get = new Regras.Regras();
                int IdAssociacao = 0;
                int IdCompeticao = 0;

                if (!string.IsNullOrWhiteSpace(Session["IdAssociacao"].ToString()))
                {
                    IdAssociacao = int.Parse(Session["IdAssociacao"].ToString());
                }
                else
                {
                    LoginCookie();
                    CarregarAssociacao();
                }

                if (!string.IsNullOrWhiteSpace(Session["IdCompeticao"].ToString()))
                {
                    IdCompeticao = int.Parse(Session["IdCompeticao"].ToString());
                }

                long idInscricao = get.GetVerificaInscricaoAberta(IdAssociacao, IdCompeticao);

                Session["IdInscricao"] = idInscricao;

                if (idInscricao != 0)
                {
                    CarregarGridAtleta();
                    HabilitaInscricao();
                    Menu1.Items[MultiView1.ActiveViewIndex = 1].Selected = true;
                }
                else
                {
                    Session["IdInscricao"] = null;
                    SetInscricao();
                    HabilitaInscricao();
                    Menu1.Items[MultiView1.ActiveViewIndex = 1].Selected = true;
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(string.Format("Erro ao verificar inscrições abertas.{0}", ex.Message));
                // FailureText.Text = string.Format("Erro ao verificar inscrições abertas.{0}", ex.Message);
            }
        }

        private void CarregaTela(long idcompeticao)
        {
            try
            {
                Regras.Regras get = new Regras.Regras();
                DtoCompeticao competicao = get.GetCompeticaoPorId(int.Parse(idcompeticao.ToString()));
                Session["Competicao"] = competicao;
                LabelCampeonato.Text = competicao.NomeCompeticao.ToString();
                LabelCidade.Text = competicao.NomeCidade.ToString();
                LabelDtCompeticao.Text = competicao.DataCompeticao.ToShortDateString();
                LabelDtLimite.Text = competicao.DataLimiteInscricao.ToShortDateString();
                VerificaInscricaoAberta();
                TrataCampos(competicao);
                CarregarGraduacao(competicao.IdEsporte);
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
                //FailureText.Text = string.Format("Erro ao Carregar a tela.{0}", ex.Message);
            }
        }

        private void CarregarAssociacao()
        {
            try
            {
                Regras.Regras dados = new Regras.Regras();
                Regras.Dto.DtoAcademia listassociacao = new DtoAcademia();
                int IdAssociacao = int.Parse(Session["IdAssociacao"].ToString());
                listassociacao = dados.GetAcademiaPorId(IdAssociacao);
                TextIdAssociacao.Text = listassociacao.IdAcademia.ToString();
                TextNomeAssociacao.Text = listassociacao.NomeAcademia;
                TextData.Text = DateTime.Now.Date.ToShortDateString();
                TextLogin.Text = Session["User"].ToString();
                Session["IdEsporte"] = listassociacao.IdEsporte;
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
                //FailureText.Text = string.Format("Erro ao carregar Associação/Academia.{0}", ex.Message);
            }
        }

        private void CarregaCompeticaoAtiva()
        {
            try
            {
                Regras.Regras dados = new Regras.Regras();
                List<DtoCompeticao> ListCompeticao = dados.GetAllCompeticaoAtiva();

                if (ListCompeticao != null)
                {
                    Literal1.Text = string.Empty;
                    List<DtoCompeticao> list = new List<DtoCompeticao>();
                    foreach (var l in ListCompeticao)
                    {
                        DtoCompeticao i = new DtoCompeticao();
                        i = l;
                        if (l.imagem != null && l.imagem.Length > 0)
                        {
                            byte[] bytes = l.imagem;
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            i.UrlImagem = "data:image/png;base64," + base64String;
                        }
                        else
                        {
                            i.UrlImagem = "~/imagens/FPRKE_Pequeno.JPG";
                        }
                        list.Add(i);
                    }
                    if (list != null)
                    {
                        GridCompeicoes.DataSource = list;
                        GridCompeicoes.DataBind();
                    }
                }
                else { Literal1.Text = "Nenhuma competição ativa no momento."; }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
                //FailureText.Text = string.Format("Erro ao carregar competição ativa.{0}", ex.Message);
            }
        }

        private void AddDadosCompeticao(DtoCompeticao i)
        {
            string comp = "<br /> " + i.IdCompeticao.ToString() + " - " + i.NomeCompeticao.ToString() + " - " + i.NomeResponsavel.ToString() + " - " + i.NomeCidade.ToString();
            Literal l = new Literal();
            l.Text = comp;
            DivCompeticoes.Controls.Add(l);
            string linha = "<br />";
            Literal l1 = new Literal();
            l1.Text = linha;
            DivCompeticoes.Controls.Add(l1);
        }

        private void CarregaLogado()
        {
            try
            {
                Regras.Regras dados = new Regras.Regras();
                bool adm = bool.Parse(Session["Administrador"].ToString());
                DtoAcademia associacao = new DtoAcademia();
                List<DtoFiliado> filiados = new List<DtoFiliado>();
                if (!adm)
                {
                    int idAssociacao = int.Parse(Session["IdAssociacao"].ToString());
                    associacao = dados.GetAcademiaPorId(idAssociacao);
                    Session["Filiados"] = dados.GetAllFiliadoPorIdAssociacao(idAssociacao, StatusFiliado.Ativo.ToString().Substring(0, 1));
                    filiados = (List<DtoFiliado>)Session["Filiados"];
                }
            }
            catch (Exception)
            {
                //FailureText.Text = ex.Message;
            }
        }

        private void CalcularValorTotal(int idInscricao)
        {
            throw new NotImplementedException();
        }

        private void CarregarGridAtleta()
        {
            try
            {
                Regras.Regras get = new Regras.Regras();
                int idAssociacao = int.Parse(Session["IdAssociacao"].ToString());
                int idInscricao = int.Parse(Session["IdInscricao"].ToString());
                List<DtoInscricaoAtleta> list = get.GetInscritoPorCompeticaoeAssociacao(idAssociacao, idInscricao);

                Session["ListAtletaInscritos"] = list;
                GridAtletas.DataSource = list;
                GridAtletas.DataBind();

                CarregarAtletasPorAssociaçao();
                AtualizaListFiliados(list, idInscricao);
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        private void AtualizaListFiliados(List<DtoInscricaoAtleta> list, int idInscricao)
        {
        }

        private void NovoAtleta()
        {
            TextNumeroRegistro.Text = string.Empty;
            TextNomeFiliado.Text = string.Empty;
            TextDtNascimento.Text = string.Empty;
            TextIdade.Text = string.Empty;
            TextPeso.Text = string.Empty;
            TextAltura.Text = string.Empty;
            TextIdAtleta.Text = string.Empty;
            DropDownListGraduacao.SelectedValue = "0";
            if (TextNumeroRegistro.Enabled == true)
                TextNumeroRegistro.Focus();
            else
                TextNomeFiliado.Focus();
            CheckBoxListCategorias.Items.Clear();
        }

        private void SetInscricao()
        {
            try
            {
                Regras.Regras set = new Regras.Regras();
                DtoInscricao dados = new DtoInscricao();
                if (Session["IdInscricao"] != null)
                {
                    string idinscricao = Session["IdInscricao"].ToString();
                    dados.IdInscricao = int.Parse(idinscricao);
                }
                dados.IdAssociacao = int.Parse(TextIdAssociacao.Text);
                dados.DataInscricao = DateTime.Parse(TextData.Text);
                dados.IdLogin = int.Parse(Session["IdUser"].ToString());
                dados.StatusInscricao = bool.Parse(RBtnListStatus.SelectedValue);
                dados.IdCompeticao = int.Parse(Session["IdCompeticao"].ToString());
                dados.IdEsporte = int.Parse(Session["IdEsporte"].ToString());
                Session["IdInscricao"] = set.SetInscricao(dados);
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
                //FailureText.Text = string.Format("Erro ao inserir inscrição para a associação.{0}", ex.Message);
            }
        }

        private void CarregarGraduacao(long id)
        {
            Regras.Regras get = new Regras.Regras();
            List<DtoGraduacao> graduacao = get.GetAllGraduacaoPorEsporte(id.ToString());
            DropDownListGraduacao.DataSource = graduacao;
            DropDownListGraduacao.DataTextField = "DescricaoGraduacao";
            DropDownListGraduacao.DataValueField = "IdGraduacao";
            DropDownListGraduacao.DataBind();
            DropDownListGraduacao.Items.Insert(0, new ListItem("-Selecione-", "0"));
        }

        private void HabilitaInscricao()
        {
            PainelInscricao.Enabled = true;
            PanelInscricaoAtleta.Enabled = true;
            PanelContinuacaoInscricaoAtleta.Enabled = true;
            BtnConfirmaInscricao.Enabled = true;
            BtnCancelamento.Enabled = true;
        }

        private void GetCategoriaDoAtleta()
        {
            try
            {
                LiteralMensagem.Text = string.Empty;
                DtoDadosAtleta atleta = new DtoDadosAtleta();
                if (!String.IsNullOrWhiteSpace(TextNumeroRegistro.Text))
                    atleta.IdAtleta = int.Parse(TextNumeroRegistro.Text);
                atleta.NomeAtleta = TextNomeFiliado.Text;
                if (RadioTipoCompeticao.SelectedValue.Equals("IND"))
                {
                    atleta.PesoAtleta = TextPeso.Text;
                    atleta.IdadeAtleta = int.Parse(TextIdade.Text);
                    atleta.AlturaAtleta = TextAltura.Text;
                    atleta.GraduacaoAtleta = DropDownListGraduacao.SelectedValue;
                }

                atleta.SexoAtleta = RBtnListSexo.SelectedValue;
                atleta.TipoInscricao = RadioTipoCompeticao.SelectedValue;

                Regras.Regras dados = new Regras.Regras();
                int idCompeticao = int.Parse(Session["idCompeticao"].ToString());
                List<DtoCategoriaAtleta> list = dados.GetCategoriaPorAtelta(atleta, idCompeticao);
                if (list.Count > 0)
                {
                    CarregarListaCategoriaAtleta(list.OrderBy(p => p.IdCategoriaCompeticao).ToList());
                }
                else
                {
                    CheckBoxListCategorias.Items.Clear();
                    LiteralMensagem.Text = "CATEGORIA NÃO ENCONTRADA";
                }
            }
            catch (Exception)
            {
                //FailureText.Text = ex.Message; 
            }
        }

        private void CarregarListaCategoriaAtleta(List<DtoCategoriaAtleta> list)
        {
            CheckBoxListCategorias.DataSource = list;
            CheckBoxListCategorias.DataTextField = "NomeCategoria";
            CheckBoxListCategorias.DataValueField = "IdCategoriaCompeticao";
            CheckBoxListCategorias.DataBind();
        }

        private void AtualizaDadosFiliados()
        {
            if (!string.IsNullOrWhiteSpace(TextNumeroRegistro.Text))
            {
                DtoFiliado filiado = (DtoFiliado)Session["Filiado"];
                if (filiado.Peso.ToString() != TextPeso.Text)
                {
                    filiado.Peso = decimal.Parse(TextPeso.Text);
                }
                if (filiado.Altura.ToString() != TextAltura.Text)
                {
                    filiado.Altura = decimal.Parse(TextAltura.Text);
                }
                if (filiado.IdadeFiliado.ToString() != TextIdade.Text)
                {
                    filiado.IdadeFiliado = int.Parse(TextIdade.Text);
                }
                Regras.Regras set = new Regras.Regras();
                set.AlterFiliado(filiado);
            }
        }

        #endregion Metodos

        #region Formulario

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiView1.ActiveViewIndex = index;
            if (index == 0)
                CarregaCompeticaoAtiva();
        }

        protected void btnSelecionaClick(object sender, EventArgs e)
        {
            try
            {
                MenuItem mnuItem = Menu1.FindItem("3");
                mnuItem.Enabled = true;
                Menu1.Items[MultiView1.ActiveViewIndex = 3].Selected = true;

                Regras.Regras get = new Regras.Regras();
                if (Session["IdCompeticao"] != null)
                {
                    DtoCompeticao dados = get.GetCompeticaoPorId(int.Parse(Session["IdCompeticao"].ToString()));

                    if (dados.PermiteNaoFiliado == false)
                    {
                        if (!string.IsNullOrWhiteSpace(ListAtletasCadastrados.SelectedValue))
                        {
                            TextNumeroRegistro.Text = ListAtletasCadastrados.SelectedValue;
                            BuscaFiliadoPorRegistro(sender, e);
                        }
                    }
                    else
                    {
                        TextIdAtleta.Text = ListAtletasCadastrados.SelectedValue;
                        BuscaFiliadoPorId(sender, e);
                    }
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

        protected void btnProximoClick(object sender, EventArgs e)
        {
            MenuItem mnuItem = Menu1.FindItem("2");
            mnuItem.Enabled = true;
            Menu1.Items[MultiView1.ActiveViewIndex = 2].Selected = true;
        }

        public void CalcularIdade(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras regras = new Regras.Regras();
                TextIdade.Text = regras.CalculaIdade(DateTime.Parse(TextDtNascimento.Text)).ToString();
                TextPeso.Focus();
            }
            catch (Exception)
            {
                //FailureText.Text = ex.Message;
            }
        }

        public void BuscaFiliadoPorId(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras dados = new Regras.Regras();
                DtoFiliado filiado = new DtoFiliado();

                int idAssociacao = int.Parse(Session["IdAssociacao"].ToString());
                int id = int.Parse(TextIdAtleta.Text);
                filiado = dados.GetFiliadoPorId(id);
                Session["Filiado"] = filiado;

                if (filiado.IdAssociacao != idAssociacao)
                {
                    throw new Exception("N° de registro informado não pertence a esta associação!");
                }

                if (!String.IsNullOrWhiteSpace(filiado.NumeroRegistro.ToString()))
                    TextNumeroRegistro.Text = filiado.NumeroRegistro.ToString();
                TextDtNascimento.Text = filiado.DataNascimento.ToShortDateString();
                if (!string.IsNullOrWhiteSpace(TextDtNascimento.Text))
                    CalcularIdade(sender, e);
                TextNomeFiliado.Text = filiado.NomeFiliado.ToString();
                if (filiado.Altura != 0)
                    TextAltura.Text = filiado.Altura.ToString();
                else
                {
                    TextAltura.Enabled = true;
                    //TextAltura.Focus();
                }
                if (filiado.Peso != 0)
                {
                    TextPeso.Text = filiado.Peso.ToString();
                }
                else
                {
                    TextPeso.Enabled = true;
                    //TextPeso.Focus();
                }
                RBtnListSexo.SelectedValue = filiado.SexoFiliado;
                DropDownListGraduacao.SelectedIndex = int.Parse(filiado.IdGraduacao.ToString());
                TextIdAtleta.Text = filiado.IdFiliado.ToString();
                TextIdade.Text = dados.CalculaIdade(DateTime.Parse(filiado.DataNascimento.ToString())).ToString();
                if ((TextAltura.Text != "0" || !String.IsNullOrWhiteSpace(TextAltura.Text)) && (TextPeso.Text != "0" || !String.IsNullOrWhiteSpace(TextPeso.Text)))
                    GetCategoriaDoAtleta();
                else
                { throw new Exception("Atleta sem Peso ou altura."); }

            }
            catch (Exception)
            {
                //FailureText.Text = ex.Message;
            }
        }

        public void BuscaFiliadoPorRegistro(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras dados = new Regras.Regras();
                DtoFiliado filiado = new DtoFiliado();

                int idAssociacao = int.Parse(Session["IdAssociacao"].ToString());
                int numeroRegistro = int.Parse(TextNumeroRegistro.Text);
                filiado = dados.GetFiliadoPorRegistro(numeroRegistro);
                Session["Filiado"] = filiado;

                if (filiado.IdAssociacao != idAssociacao)
                {
                    throw new Exception("N° de registro informado não pertence a esta associação!");
                }

                if (filiado.StatusFiliado == StatusFiliado.Ativo.ToString().Substring(0, 1))
                {

                    TextDtNascimento.Text = filiado.DataNascimento.ToShortDateString();
                    if (!string.IsNullOrWhiteSpace(TextDtNascimento.Text))
                        CalcularIdade(sender, e);
                    TextNomeFiliado.Text = filiado.NomeFiliado.ToString();
                    if (filiado.Altura != 0)
                        TextAltura.Text = filiado.Altura.ToString().Replace(".", ",");
                    else
                    {
                        TextAltura.Enabled = true;
                        TextAltura.Focus();
                    }
                    if (filiado.Peso != 0)
                    {
                        TextPeso.Text = filiado.Peso.ToString().Replace(".", ",");
                    }
                    else
                    {
                        TextPeso.Enabled = true;
                        TextPeso.Focus();
                    }
                    RBtnListSexo.SelectedValue = filiado.SexoFiliado;
                    DropDownListGraduacao.SelectedIndex = int.Parse(filiado.IdGraduacao.ToString());
                    TextIdAtleta.Text = filiado.IdFiliado.ToString();
                    TextIdade.Text = dados.CalculaIdade(DateTime.Parse(filiado.DataNascimento.ToString())).ToString();
                    if ((TextAltura.Text != "0" || !String.IsNullOrWhiteSpace(TextAltura.Text)) && (TextPeso.Text != "0" || !String.IsNullOrWhiteSpace(TextPeso.Text)))
                        GetCategoriaDoAtleta();
                    else
                    { throw new Exception("Atleta sem Peso ou altura."); }
                }
                else
                {
                    throw new Exception("Filiado Inativo ou bloqueado procure o responsável pelo sistema do Sistema");
                }
            }
            catch (Exception)
            {
                //FailureText.Text = ex.Message;
            }
        }

        public void CancelaClick(object sender, EventArgs e)
        {
            CarregaLogado();
        }

        protected void SelectCategoriaClick(object sender, EventArgs e)
        {
            GetCategoriaDoAtleta();
        }

        protected void SelectTipoInscricaoClick(object sender, EventArgs e)
        {
            DtoCompeticao competicao = (DtoCompeticao)Session["Competicao"];

            if (RadioTipoCompeticao.SelectedValue.Equals("IND"))
            {
                NovoAtleta();
                BloqueiaCampos(competicao);
            }
            else if (RadioTipoCompeticao.SelectedValue.Equals("EQP"))
            {
                NovoAtleta();
                LiberaCampos(competicao);
            }
        }

        protected void ConfirmaInscriAtletaClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras set = new Regras.Regras();
                DtoInscricaoAtleta dados = new DtoInscricaoAtleta();

                if (!String.IsNullOrWhiteSpace(TextIdAtleta.Text))
                    dados.IdAtleta = int.Parse(TextIdAtleta.Text);
                dados.NomeAtleta = TextNomeFiliado.Text;
                dados.IdInscricao = int.Parse(Session["IdInscricao"].ToString());
                dados.TipoInscricao = RadioTipoCompeticao.SelectedValue;
                dados.IdCompeticao = int.Parse(Session["IdCompeticao"].ToString());
                dados.IdAssociacao = int.Parse(Session["IdAssociacao"].ToString());
                long idInscricaoAtleta = set.SetInscriAtleta(dados);


                foreach (ListItem lis in CheckBoxListCategorias.Items)
                {
                    if (lis.Selected)
                    {
                        dados.IdInscricaoAtleta = idInscricaoAtleta;
                        dados.IdCategoria = int.Parse(lis.Value);
                        long idInscricaoCategoria = set.SetInscriAtletaCategoria(dados, idInscricaoAtleta);
                    }
                }
                CarregarGridAtleta();
                AtualizaDadosFiliados();
                NovoAtleta();
            }
            catch (Exception)
            {
                //FailureText.Text = ex.Message;
            }
        }


        #endregion

        protected void btnEnviaEmailClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras get = new Regras.Regras();
                List<DtoInscricaoAtleta> list = new List<DtoInscricaoAtleta>();
                list = (List<DtoInscricaoAtleta>)Session["ListAtletaInscritos"];
                list = list.OrderBy(p => p.NumeroCategoria).ThenBy(n => n.NomeAtleta).ToList();
                int idAssociacao = int.Parse(Session["IdAssociacao"].ToString());
                long idInscricao = int.Parse(Session["IdInscricao"].ToString());
                string emailAssociacao = get.GetAcademiaPorId(idAssociacao).EmailAcademia;
                string nomeAssociao = get.GetAcademiaPorId(idAssociacao).NomeAcademia;

                if (!string.IsNullOrWhiteSpace(emailAssociacao))
                {
                    string msg = string.Empty;
                    string competicao = get.GetInscritoPorId(idInscricao).NomeCompeticao.ToString();
                    string assunto = "Inscritos " + nomeAssociao + " ";
                    msg = "SysKiai - Relação de Atletas inscritos<br>";
                    msg += "Lista de Inscritos para a competição " + competicao + "<br><br>";

                    foreach (var l in list)
                    {
                        msg += "<br>";
                        if (l.NumeroRegistro != null)
                            msg += l.NumeroRegistro + " - ";
                        else
                            msg += "      ";
                        msg += l.NomeAtleta ;
                        msg += "<br>";
                        msg += l.NumeroCategoria + " - ";
                        msg += l.NomeCategoria;
                    }
                    byte[] bytes = null;

                    Regras.Email envia = new Regras.Email();
                    envia.EnviaEmail("presidente@fprke.com.br", "mensagem@fprke.com.br", emailAssociacao, msg, assunto, bytes, true);
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(string.Format("Erro ou enviar E-mail.:{0}", ex.Message));
            }
        }
        

        protected void DropDownListGraduacaoSelect(object sender, EventArgs e)
        {
            GetCategoriaDoAtleta();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;
using System.Web;
using System.IO;
using System.Drawing;

namespace SisKiai.Cadastro
{
    public partial class ICompeticao : System.Web.UI.Page
    {
        static byte[] imagem;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginCookie();
            if (!IsPostBack)
            {
                CarregarEsporte();
                CarregarCidade();
                CarregarGrid();
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

        #region Métodos

        private void ControleListBox()
        {
            CarregarCategorias();

            Regras.Regras get = new Regras.Regras();
            string idcompeticao = Session["IdCompeticao"].ToString();
            if (!String.IsNullOrWhiteSpace(idcompeticao))
            {
                List<Regras.Dto.DtoCategoriasCompeticao> list = get.GetCompeticaoCategorias(idcompeticao);

                List<ListItem> itemsToRemove = new List<ListItem>();

                foreach (var lst in list)
                {
                    foreach (ListItem l in ListBoxCategoriasDisponiveis.Items)
                    {
                        if (lst.IdCategoria == long.Parse(l.Value))
                        {
                            itemsToRemove.Add(l);
                        }
                    }
                }
                foreach (ListItem listIem in itemsToRemove)
                {
                    ListBoxCategoriasDisponiveis.Items.Remove(listIem);
                }

                ListBoxCategoriasLiberadas.DataSource = list;
                ListBoxCategoriasLiberadas.DataTextField = "NomeCategoria";
                ListBoxCategoriasLiberadas.DataValueField = "IdCategoria";
                ListBoxCategoriasLiberadas.DataBind();
            }
        }

        private byte[] AlteracaoTamanhoImagem()
        {
            MemoryStream ms = new MemoryStream(flUpload.FileBytes);
            Bitmap map = System.Drawing.Image.FromStream(ms) as Bitmap;
            System.Drawing.Image thumbnail = new Bitmap(400, 300);
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(thumbnail);
            graphic.DrawImage(map, 0, 0, 400, 300);
            System.IO.MemoryStream imageStream = new System.IO.MemoryStream();
            thumbnail.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageContent = new Byte[imageStream.Length];
            imageStream.Position = 0;
            imageStream.Read(imageContent, 0, (int)imageStream.Length);
            return imageContent;
        }

        private void CarregarGrid()
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoCompeticao> list = get.GetAllCompeticao();
            GridViewCompeticoes.DataSource = list;
            GridViewCompeticoes.DataBind();
        }

        private void CarregarCategorias()
        {
            Regras.Regras dados = new Regras.Regras();
            var ListaCategorias = dados.GetAllCategoriaPorIdEsporte(int.Parse(DropDownListEsporte.SelectedValue));

            ListBoxCategoriasDisponiveis.DataSource = ListaCategorias;
            ListBoxCategoriasDisponiveis.DataTextField = "DescricaoCategoria";
            ListBoxCategoriasDisponiveis.DataValueField = "IdCategoria";
            ListBoxCategoriasDisponiveis.DataBind();
        }

        private void CarregarCidade()
        {
            Regras.Regras dados = new Regras.Regras();
            var cidades = dados.GetAllCidade();
            DropDownListCidade.DataSource = cidades;
            DropDownListCidade.DataTextField = "NomeCidade";
            DropDownListCidade.DataValueField = "IdCidade";
            DropDownListCidade.DataBind();
            DropDownListCidade.Items.Insert(0, new ListItem("-Selecione-", "0"));
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

        private void CarregaTela(Regras.Dto.DtoCompeticao dados)
        {
            TextId.Text = dados.IdCompeticao.ToString();
            DropDownListEsporte.SelectedValue = dados.IdEsporte.ToString();
            TextNomeCompeticao.Text = dados.NomeCompeticao;
            TextResponsavel.Text = dados.NomeResponsavel;
            TextDescricao.Text = dados.DescricaoCompeticao;
            TextEndereco.Text = dados.EnderecoCompeticao;
            DropDownListCidade.SelectedValue = dados.IdCidade.ToString();
            TextCep.Text = dados.Cep;
            TextTelefone.Text = dados.TelefoneResponsavel;
            TextDtCompeticao.Text = dados.DataCompeticao.ToString("dd/MM/yyyy");
            TextDtLimite.Text = dados.DataLimiteInscricao.ToString("dd/MM/yyyy");
            TextValor.Text = dados.ValorCompeticao.ToString();
            bool boleano = dados.StatusCompeticao.Value;
            CheckBoxStatus.Checked = boleano;
            if (dados.StatusCompeticao == false)
            {
                CheckBoxStatus.Text = "Inativo";
            }
            else { CheckBoxStatus.Text = "Ativo"; }
            RBtnListPermiteNaoFiliado.SelectedValue = dados.PermiteNaoFiliado.ToString();
            TextPrimeiro.Text = dados.Primeiro.ToString();
            TextSegundo.Text = dados.Segundo.ToString();
            TextTerceiro.Text = dados.Terceiro.ToString();
            TextQuarto.Text = dados.Quarto.ToString();
            TextQuinto.Text = dados.Quinto.ToString();
            TextParticipacao.Text = dados.Participacao.ToString();
            imagem = dados.imagem;
        }

        #endregion

        #region Grid

        public void GrdCompeticoesPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewCompeticoes.PageIndex = e.NewPageIndex;
            CarregarGrid();
            // FailureText.Text = string.Empty;
        }

        public void GridViewCompeticoesCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int n = 0;
                if (int.TryParse(e.CommandArgument.ToString(), out n))
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    string idCompeticao = GridViewCompeticoes.DataKeys[index]["IdCompeticao"].ToString();
                    Session["IdCompeticao"] = idCompeticao;
                    if (e.CommandName == "Excluir")
                    {
                        Regras.Regras del = new Regras.Regras();
                        del.DelCompeticao(idCompeticao);
                    }
                    else if (e.CommandName == "Editar")
                    {
                        Regras.Regras get = new Regras.Regras();
                        Regras.Dto.DtoCompeticao dados = get.GetCompeticaoPorId(int.Parse(idCompeticao));
                        LimpaTela();
                        CarregaTela(dados);
                        ControleListBox();
                        Menu1.Items[MultiView1.ActiveViewIndex = 0].Selected = true;
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

        private void LimpaTela()
        {
            TextId.Text = string.Empty;
            TextNomeCompeticao.Text = string.Empty;
            TextPrimeiro.Text = string.Empty;
            TextSegundo.Text = string.Empty;
            TextTerceiro.Text = string.Empty;
            TextQuarto.Text = string.Empty;
            TextQuinto.Text = string.Empty;
            TextResponsavel.Text = string.Empty;
            TextTelefone.Text = string.Empty;
            TextValor.Text = string.Empty;
            TextEndereco.Text = string.Empty;
            TextDescricao.Text = string.Empty;
            TextCep.Text = string.Empty;
            TextEndereco.Text = string.Empty;
        }

        public void DropDownListEsporteChange(object sender, EventArgs e)
        {
            CarregarCategorias();
        }

        #endregion

        #region Formulario

        protected void btnUploadClick(object sender, EventArgs e)
        {
            try
            {
                imagem = AlteracaoTamanhoImagem();
            }
            catch (Exception ex)
            { lblInfo.Text = ex.Message; }
        }

        public void ConsultarClick(object sender, EventArgs e)
        {
            Regras.Regras get = new Regras.Regras();
            List<Regras.Dto.DtoCompeticao> list = get.GetCompeticaoPorNome(TextConsulta.Text);
            GridViewCompeticoes.DataSource = list;
            GridViewCompeticoes.DataBind();
        }

        public void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoCompeticao item = new Regras.Dto.DtoCompeticao();
                if (!String.IsNullOrWhiteSpace(TextId.Text))
                    item.IdCompeticao = int.Parse(TextId.Text);
                item.IdEsporte = int.Parse(DropDownListEsporte.SelectedValue);
                item.NomeCompeticao = TextNomeCompeticao.Text;
                item.DescricaoCompeticao = TextDescricao.Text;
                item.IdCidade = int.Parse(DropDownListCidade.SelectedValue);
                if (TextEndereco.Text.Length > 70)
                    throw new Exception("Quantidade de Caracteres no endereço não permitido");
                item.EnderecoCompeticao = TextEndereco.Text;
                item.Cep = TextCep.Text;
                item.DataCompeticao = DateTime.Parse(TextDtCompeticao.Text);
                item.DataLimiteInscricao = DateTime.Parse(TextDtLimite.Text);
                item.NomeResponsavel = TextResponsavel.Text;
                item.TelefoneResponsavel = TextTelefone.Text;
                item.StatusCompeticao = CheckBoxStatus.Checked;
                if (!String.IsNullOrWhiteSpace(TextValor.Text))
                    item.ValorCompeticao = decimal.Parse(TextValor.Text);
                item.PermiteNaoFiliado = bool.Parse(RBtnListPermiteNaoFiliado.SelectedValue.ToString());
                if (!String.IsNullOrWhiteSpace(TextPrimeiro.Text))
                    item.Primeiro = long.Parse(TextPrimeiro.Text);
                if (!String.IsNullOrWhiteSpace(TextSegundo.Text))
                    item.Segundo = long.Parse(TextSegundo.Text);
                if (!String.IsNullOrWhiteSpace(TextTerceiro.Text))
                    item.Terceiro = long.Parse(TextTerceiro.Text);
                if (!String.IsNullOrWhiteSpace(TextQuarto.Text))
                    item.Quarto = long.Parse(TextQuarto.Text);
                if (!String.IsNullOrWhiteSpace(TextQuinto.Text))
                    item.Quinto = long.Parse(TextQuinto.Text);
                if (!String.IsNullOrWhiteSpace(TextParticipacao.Text))
                    item.Participacao = long.Parse(TextParticipacao.Text);
                item.ContapRanking = bool.Parse(RadioButtonContaRanking.SelectedValue.ToString());
                if (imagem != null)
                    item.imagem = imagem;

                Regras.Regras set = new Regras.Regras();
                long idCompeticao = set.SetCompeticao(item);
                Session["idCompeticao"] = idCompeticao;

                CarregarGrid();
                AlertVisibleTrue();
                AlertSucesso();
                LiteralCompeticao.Text = item.NomeCompeticao;
                Menu1.Items[MultiView1.ActiveViewIndex = 1].Selected = true;
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        public void BuscaCep(object sender, EventArgs e)
        {
            Regras.Regras get = new Regras.Regras();
            TextCep.Text = get.GetCidadePorId(int.Parse(DropDownListCidade.SelectedValue)).CepCidade;
        }

        public void AdicionaClick(object sender, EventArgs e)
        {
            try
            {
                if (ListBoxCategoriasDisponiveis.Items.Count != 0)
                {
                    ListBoxCategoriasLiberadas.Items.Add(ListBoxCategoriasDisponiveis.SelectedItem);

                    Regras.Dto.DtoCategoriasCompeticao dados = new DtoCategoriasCompeticao();
                    dados.IdCategoria = long.Parse(ListBoxCategoriasDisponiveis.SelectedValue);
                    dados.IdCompeticao = long.Parse(Session["idCompeticao"].ToString());
                    dados.IdEsporte = long.Parse(DropDownListEsporte.SelectedValue);
                    dados.Categoria_Finalizada = false;
                    Regras.Regras set = new Regras.Regras();
                    set.SetCategoriaCompeticao(dados);

                    ListBoxCategoriasDisponiveis.Items.RemoveAt(ListBoxCategoriasDisponiveis.SelectedIndex);
                }
                else
                {
                    throw new Exception("Selecione ao menos uma categoria");
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
                foreach (ListItem lst in ListBoxCategoriasDisponiveis.Items)
                {
                    lst.Selected = true;
                    ListBoxCategoriasLiberadas.Items.Add(lst);
                    lst.Selected = false;

                    Regras.Dto.DtoCategoriasCompeticao dados = new DtoCategoriasCompeticao();
                    dados.IdCategoria = long.Parse(lst.Value);
                    dados.IdCompeticao = long.Parse(Session["idCompeticao"].ToString());
                    dados.IdEsporte = long.Parse(DropDownListEsporte.SelectedValue);
                    dados.Categoria_Finalizada = false;
                    Regras.Regras set = new Regras.Regras();
                    set.SetCategoriaCompeticao(dados);
                }
                ListBoxCategoriasDisponiveis.Items.Clear();
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
                if (ListBoxCategoriasLiberadas.Items.Count != 0)
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelCategoriaComepticao(ListBoxCategoriasLiberadas.SelectedValue, Session["idCompeticao"].ToString());

                    ListBoxCategoriasDisponiveis.Items.Add(ListBoxCategoriasLiberadas.SelectedItem);
                    ListBoxCategoriasLiberadas.Items.RemoveAt(ListBoxCategoriasLiberadas.SelectedIndex);
                }
                else
                {
                    throw new Exception("Selecione ao menos uma categoria");
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        public void RemoveTodosClick(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem lst in ListBoxCategoriasLiberadas.Items)
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelCategoriaComepticao(lst.Value, Session["idCompeticao"].ToString());
                }
                ListBoxCategoriasLiberadas.Items.Clear();
                CarregarCategorias();
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

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiView1.ActiveViewIndex = index;
        }

        #endregion

    }
}
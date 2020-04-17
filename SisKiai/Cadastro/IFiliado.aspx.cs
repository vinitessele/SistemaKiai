using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web;
using System.Drawing;


namespace SisKiai.Cadastro
{
    public partial class IFiliado : System.Web.UI.Page
    {
        static byte[] imagem;

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
                CarregarCidade();
                CarregarAssociacao();
                CarregarGraduacao();
                CarregarFiliado();
                AlertNotificationDiv.Visible = false;
                AlertNotificationBox.Text = string.Empty;
            }
        }

        #region Grid

        public void GrdFiliadoPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridFiliado.PageIndex = e.NewPageIndex;
            GridFiliado.DataSource = (List<Regras.Dto.DtoFiliado>)Session["Filiados"];
            GridFiliado.DataBind();
        }

        public void GridFiliadoCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string argument = e.CommandArgument.ToString();
                if (argument != "Next" && argument != "Last")
                {
                    if (e.CommandName == "Imprimir")
                    {
                        ImprimirCarteirinha(int.Parse(argument));
                    }
                    else
                    {
                        int index = Convert.ToInt32(e.CommandArgument);
                        string idFiliado = GridFiliado.DataKeys[index]["IdFiliado"].ToString();
                        Session["IdFiliadoCommand"] = idFiliado;

                        if (e.CommandName == "Excluir")
                        {
                            Regras.Regras del = new Regras.Regras();
                            del.DelGraduacaoFiliadoPorIdFiliado(idFiliado);
                            del.DelFiliado(idFiliado);
                        }
                        else if (e.CommandName == "Editar")
                        {
                            Regras.Regras get = new Regras.Regras();
                            Regras.Dto.DtoFiliado dados = get.GetFiliadoPorIdSemGraduacao(int.Parse(idFiliado));
                            List<DtoGraduacaoFiliado> ListaGraduacao = get.GetGraduacaoPorIdFiliado(int.Parse(idFiliado));
                            DtoGraduacaoFiliado graduacao = ListaGraduacao.FirstOrDefault(p => p.Status == true);

                            CarregaTela(dados, graduacao);
                            Menu1.Items[MultiView1.ActiveViewIndex = 0].Selected = true;
                        }
                    }

                    AlertVisibleTrue();
                    AlertSucesso();
                    //FailureText.Text = string.Format("Operação realizado com Sucesso.:{0}", e.CommandName.ToString());
                    CarregarFiliado();
                    MultiView1.ActiveViewIndex = 0;
                }
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        protected void GridFiliadoDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idFiliado = (string)this.GridFiliado.DataKeys[e.Row.RowIndex]["IdFiliado"].ToString();
                Session["IdFiliado"] = idFiliado;

                List<Regras.Dto.DtoFiliado> lista = (List<Regras.Dto.DtoFiliado>)Session["Filiados"];
                int id = int.Parse(idFiliado);
                Regras.Dto.DtoFiliado dados = lista.FirstOrDefault(p => p.IdFiliado == id);

                ImageButton button = (ImageButton)e.Row.FindControl("carteirinha");
                if (dados != null)
                {
                    if (!dados.StatusFiliado.Equals("A"))
                    {
                        button.Visible = false;
                    }
                    else
                    {
                        button.CommandArgument = dados.IdFiliado.ToString();
                    }
                }
            }
        }

        #endregion

        #region Validacao ou cargas

        private void CarregaTela(Regras.Dto.DtoFiliado dados, DtoGraduacaoFiliado graduacao)
        {
            TextId.Text = dados.IdFiliado.ToString();
            TextNumeroRegistro.Text = dados.NumeroRegistro.ToString();
            TextNomeFiliado.Text = dados.NomeFiliado.ToUpper();
            TextEndereco.Text = dados.EnderecoFiliado;
            DropDownListCidade.SelectedValue = dados.IdCidade.ToString();
            DropDownListAssociacao.SelectedValue = dados.IdAssociacao.ToString();
            TextCep.Text = dados.CepFiliado;
            TextTelefone.Text = dados.TelefoneFixo;
            TextTelefoneCelular.Text = dados.TelefoneCelular;
            TextEmail.Text = dados.EmailFiliado;
            TextDtNascimento.Text = dados.DataNascimento.ToShortDateString();
            TextIdade.Text = dados.IdadeFiliado.ToString();
            if (!String.IsNullOrWhiteSpace(dados.SexoFiliado))
                RBtnListSexo.SelectedValue = dados.SexoFiliado;
            TextAltura.Text = dados.Altura.ToString();
            TextPeso.Text = dados.Peso.ToString();
            TextRG.Text = dados.RgFiliado;
            TextCPF.Text = dados.CpfFiliado;
            RBtnListStatus.SelectedValue = dados.StatusFiliado;
            if (graduacao != null)
            {
                DropDownListGraduacao.SelectedValue = graduacao.IdGraduacao.ToString();
                TextDataGraduacao.Text = graduacao.DataGraduacao.ToShortDateString();
                RBtnGraduacao.SelectedValue = graduacao.Status == true ? "A" : "I";
            }
            else
            {
                DropDownListGraduacao.SelectedValue = "0";
                TextDataGraduacao.Text = string.Empty;
            }
        }

        private void CarregarAssociacao()
        {
            Regras.Regras dados = new Regras.Regras();
            bool adm = bool.Parse(Session["Administrador"].ToString());

            List<Regras.Dto.DtoAcademia> listassociacao = new List<DtoAcademia>();
            listassociacao = dados.GetAllAcademia();
            if (!adm)
            {
                int IdAssociacao = int.Parse(Session["IdAssociacao"].ToString());
                listassociacao = listassociacao.Where(p => p.IdAcademia == IdAssociacao).ToList();
                DropDownListAssociacao.DataSource = listassociacao;
                DropDownListAssociacao.DataTextField = "NomeAcademia";
                DropDownListAssociacao.DataValueField = "IdAcademia";
                DropDownListAssociacao.DataBind();
            }
            else
            {
                DropDownListAssociacao.DataSource = listassociacao;
                DropDownListAssociacao.DataTextField = "NomeAcademia";
                DropDownListAssociacao.DataValueField = "IdAcademia";
                DropDownListAssociacao.DataBind();
                DropDownListAssociacao.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Selecione-", "0"));
            }
        }

        private void CarregarFiliado()
        {
            Regras.Regras dados = new Regras.Regras();
            bool adm = bool.Parse(Session["Administrador"].ToString());
            List<DtoFiliado> filiados = new List<DtoFiliado>();

            if (adm)
            {
                filiados = dados.GetAllFiliado();
            }
            else
            {
                int idAssociacao = int.Parse(Session["IdAssociacao"].ToString());
                filiados = dados.GetAllFiliadoPorIdAssociacao(idAssociacao);
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

            List<DtoFiliado> listImgUrl = new List<DtoFiliado>();

            foreach (var i in filiados)
            {
                DtoFiliado f = new DtoFiliado();
                f = i;
                if (i.imagem != null && i.imagem.Length > 0)
                {
                    byte[] bytes = i.imagem;
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    f.UrlImagem = "data:image/png;base64," + base64String;
                }

               else
                {
                    i.UrlImagem = "~/imagens/sem-imagem.JPG";
                }


                listImgUrl.Add(f);
            }
            Session["Filiados"] = listImgUrl;
            GridFiliado.DataSource = listImgUrl;
            GridFiliado.DataBind();
        }

        private void CarregarCidade()
        {
            Regras.Regras dados = new Regras.Regras();
            var cidades = dados.GetAllCidade();
            DropDownListCidade.DataSource = cidades;
            DropDownListCidade.DataTextField = "NomeCidade";
            DropDownListCidade.DataValueField = "IdCidade";
            DropDownListCidade.DataBind();
            DropDownListCidade.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Selecione-", "0"));
        }

        public void BuscaCep(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras get = new Regras.Regras();
                TextCep.Text = get.GetCidadePorId(int.Parse(DropDownListCidade.SelectedValue)).CepCidade;
                TextTelefone.Focus();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }

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

        public void TextValidaCpf(object sender, EventArgs e)
        {
            try
            {
                bool valida = Regras.Regras.ValidaCpf(TextCPF.Text);
                if (!valida)
                    throw new Exception("CPF inválido");
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        private void CarregarGraduacao()
        {
            Regras.Regras dados = new Regras.Regras();
            List<Regras.Dto.DtoGraduacao> lista = dados.GetAllGraduacao();
            DropDownListGraduacao.DataSource = lista;
            DropDownListGraduacao.DataTextField = "DescricaoGraduacao";
            DropDownListGraduacao.DataValueField = "IdGraduacao";
            DropDownListGraduacao.DataBind();
            DropDownListGraduacao.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-Selecione-", "0"));
        }

        public void ConsultarClick(object sender, EventArgs e)
        {
            Regras.Regras consulta = new Regras.Regras();
            List<Regras.Dto.DtoFiliado> filiados = (List<Regras.Dto.DtoFiliado>)Session["Filiados"];
            if (!string.IsNullOrWhiteSpace(TextConsulta.Text))
                filiados = filiados.Where(x => x.NomeFiliado.Contains(TextConsulta.Text.ToUpper())).ToList();
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

            Session["Filiados"] = filiados;
            GridFiliado.DataSource = Session["Filiados"];
            GridFiliado.DataBind();
        }

        public void StatusClick(object sender, EventArgs e)
        {
            ConsultarClick(sender, e);
        }

        public void OrdemClick(object sender, EventArgs e)
        {
            ConsultarClick(sender, e);
        }

        #endregion

        #region Formulario

        public void SalvarGraduacaoClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoGraduacaoFiliado graduacaofiliado = new Regras.Dto.DtoGraduacaoFiliado();

                if (!String.IsNullOrWhiteSpace(TextIdGraduacao.Text))
                    graduacaofiliado.IdGraduacaoFiliado = int.Parse(TextIdGraduacao.Text);
                graduacaofiliado.IdGraduacao = int.Parse(DropDownListGraduacao.SelectedValue);
                if (!String.IsNullOrWhiteSpace(TextDataGraduacao.Text))
                    graduacaofiliado.DataGraduacao = DateTime.Parse(TextDataGraduacao.Text);
                if (RBtnGraduacao.SelectedValue.Equals("A"))
                    graduacaofiliado.Status = true;
                else
                    graduacaofiliado.Status = false;

                if (!String.IsNullOrWhiteSpace(TextId.Text))
                {
                    graduacaofiliado.IdFiliado = int.Parse(Session["IdFiliado"].ToString());
                }
                else
                {
                    graduacaofiliado.IdFiliado = int.Parse(Session["IdFiliadoCommand"].ToString());
                }

                Regras.Regras set = new Regras.Regras();
                Session["IdGraduacaoFiliado"] = set.SetGraduacaoFiliado(graduacaofiliado);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Salvar Graduação.:" + ex.Message);
            }
        }

        public void SalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Dto.DtoFiliado filiado = new Regras.Dto.DtoFiliado();
                if (!String.IsNullOrWhiteSpace(TextId.Text))
                    filiado.IdFiliado = int.Parse(TextId.Text);
                if (!String.IsNullOrWhiteSpace(TextNumeroRegistro.Text))
                    filiado.NumeroRegistro = int.Parse(TextNumeroRegistro.Text);
                filiado.NomeFiliado = TextNomeFiliado.Text.ToUpper();
                filiado.EnderecoFiliado = TextEndereco.Text.ToUpper();
                filiado.IdCidade = int.Parse(DropDownListCidade.SelectedValue);
                filiado.CepFiliado = TextCep.Text;
                filiado.TelefoneFixo = TextTelefone.Text;
                filiado.TelefoneCelular = TextTelefoneCelular.Text;
                filiado.EmailFiliado = TextEmail.Text.ToUpper();
                filiado.DataNascimento = DateTime.Parse(TextDtNascimento.Text);
                filiado.IdadeFiliado = int.Parse(TextIdade.Text);
                filiado.SexoFiliado = RBtnListSexo.SelectedValue;
                filiado.IdAssociacao = int.Parse(DropDownListAssociacao.SelectedValue);
                filiado.StatusFiliado = RBtnListStatus.SelectedValue;
                if (!String.IsNullOrWhiteSpace(TextAltura.Text))
                {

                    filiado.Altura = decimal.Parse(TextAltura.Text.Replace(".", ","));
                }
                if (!String.IsNullOrWhiteSpace(TextPeso.Text))
                    filiado.Peso = decimal.Parse(TextPeso.Text.Replace(".", ","));
                filiado.RgFiliado = TextRG.Text;
                filiado.CpfFiliado = TextCPF.Text;
                filiado.imagem = imagem;

                Regras.Regras set = new Regras.Regras();
                Session["IdFiliado"] = set.SetFiliado(filiado);
                TextId.Text = Session["IdFiliado"].ToString();
                //Graduação
                SalvarGraduacaoClick(sender, e);

                CarregarFiliado();

                AlertVisibleTrue();
                AlertSucesso();

                MultiView1.ActiveViewIndex = 1;
                imagem = null;
                LimpaTela();
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

        private void LimpaTela()
        {
            TextId.Text = string.Empty;
            TextNumeroRegistro.Text = string.Empty;
            TextNomeFiliado.Text = string.Empty;
            TextEndereco.Text = string.Empty;
            DropDownListCidade.SelectedValue = "0";
            TextCep.Text = string.Empty;
            TextTelefone.Text = string.Empty;
            TextTelefoneCelular.Text = string.Empty;
            TextEmail.Text = string.Empty;
            TextDtNascimento.Text = string.Empty;
            TextIdade.Text = string.Empty;
            TextAltura.Text = string.Empty;
            TextPeso.Text = string.Empty;
            TextPeso.Text = string.Empty;
            TextRG.Text = string.Empty;
            TextCPF.Text = string.Empty;
            imagem = null;
            DropDownListGraduacao.SelectedValue = "0";
            TextDataGraduacao.Text = string.Empty;
        }

        private void ImprimirCarteirinha(int idFiliado)
        {
            try
            {
                Response.Redirect("~/Relatório/Rel.aspx?id=" + idFiliado.ToString(), false);
            }
            catch (Exception ex)
            {
                // FailureText.Text = "Erro ao visualizar a carteirinha.: " + ex.Message;
                AlertVisibleTrue();
                AlertError("Erro ao visualizar a carteira "+ex.Message);               
        
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (flUpload.FileName != string.Empty)
            {
                imagem = AlteracaoTamanhoImagem();
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

        #endregion

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiView1.ActiveViewIndex = index;
            //FailureText.Text = string.Empty;
        }
    }
}
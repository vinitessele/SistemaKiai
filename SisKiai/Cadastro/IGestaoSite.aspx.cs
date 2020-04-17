using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;
using System.IO;
using System.Drawing;

namespace SisKiai.Cadastro
{
    public partial class GestaoSite : System.Web.UI.Page
    {
        static byte[] imagem;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginCookie();

            if (!IsPostBack)
            {
                CarregarGrid();
                AlertNotificationDiv.Visible = false;
                AlertNotificationBox.Text = string.Empty;
            }
            //this.Page.Form.Enctype = "multipart/form-data";
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

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiView1.ActiveViewIndex = index;
        }

        protected void BtnSalvarClick(object sender, EventArgs e)
        {
            try
            {
                Regras.Regras set = new Regras.Regras();
                DtoMensagem dados = new DtoMensagem();

                if (!string.IsNullOrWhiteSpace(Textid.Text))
                    dados.Id = int.Parse(Textid.Text);
                String s = TextArea1.Value;
                if (s.Length > 500)
                    throw new Exception("Quantidade de caractéres deve ser menor que 500");
                dados.Mensagem = s;
                dados.Finalidade = RbListFinalidade.SelectedValue;
                dados.DataLimite = DateTime.Parse(TextDataFinal.Text);
                set.SetMensagem(dados);
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

        protected void BtnSalvarImagem(object sender, EventArgs e)
        {
            try
            {

                Regras.Regras set = new Regras.Regras();
                DtoImg img = new DtoImg();
                if (!string.IsNullOrWhiteSpace(TextIDImagem.Text))
                    img.Id = int.Parse(TextIDImagem.Text);
                img.Mensagem = TextMensagem.Text;
                img.Imagem = imagem;
                set.SetImagem(img);
                AlertVisibleTrue();
                AlertSucesso();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        protected void GridMensagemCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string idMensagem = GridViewMensagem.DataKeys[index]["Id"].ToString();
                if (e.CommandName == "Excluir")
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelMesnsagem(idMensagem);
                }
                else if (e.CommandName == "Editar")
                {
                    Regras.Regras get = new Regras.Regras();
                    Regras.Dto.DtoMensagem dados = get.GetMensagemPorId(int.Parse(idMensagem));
                    CarregaTela(dados);
                }
                CarregarGrid();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }

        private void CarregarGrid()
        {
            Regras.Regras get = new Regras.Regras();
            List<DtoMensagem> list = get.GetMensagemTodosList();
            GridViewMensagem.DataSource = list;
            GridViewMensagem.DataBind();

            List<DtoImg> listImg = get.GetImagem();
            List<DtoImg> listImgUrl = new List<DtoImg>();

            foreach (var i in listImg)
            {
                DtoImg DtoImg = new DtoImg();
                if (i.Imagem != null)
                {
                    byte[] bytes = i.Imagem;
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    DtoImg.Url = "data:image/png;base64," + base64String;
                    DtoImg.Id = i.Id;
                    DtoImg.Mensagem = i.Mensagem;
                    listImgUrl.Add(DtoImg);
                }
            }

            GridViewImg.DataSource = listImgUrl;
            GridViewImg.DataBind();
        }

        private void CarregaTela(DtoMensagem dados)
        {
            Textid.Text = dados.Id.ToString();
            TextDataFinal.Text = dados.DataLimite.Value.ToShortDateString();
            RbListFinalidade.SelectedValue = dados.Finalidade;
            TextArea1.Value = dados.Mensagem;
        }

        protected void GrdMensagemPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewMensagem.PageIndex = e.NewPageIndex;
            CarregarGrid();
        }

        protected void GrdImagemPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewImg.PageIndex = e.NewPageIndex;
            CarregarGrid();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                imagem = AlteracaoTamanhoImagem();
                DtoImg img = new DtoImg();
                if (!string.IsNullOrWhiteSpace(TextIDImagem.Text))
                    img.Id = int.Parse(TextIDImagem.Text);
                img.Mensagem = TextMensagem.Text;
                img.Imagem = imagem;
                Regras.Regras set = new Regras.Regras();
                set.SetImagem(img);
                CarregarGrid();
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private byte[] AlteracaoTamanhoImagem()
        {
            MemoryStream ms = new MemoryStream(flUpload.FileBytes);
            Bitmap map = System.Drawing.Image.FromStream(ms) as Bitmap;
            System.Drawing.Image thumbnail = new Bitmap(650, 550);
            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(thumbnail);
            graphic.DrawImage(map, 0, 0, 650, 550);
            System.IO.MemoryStream imageStream = new System.IO.MemoryStream();
            thumbnail.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageContent = new Byte[imageStream.Length];
            imageStream.Position = 0;
            imageStream.Read(imageContent, 0, (int)imageStream.Length);
            return imageContent;
        }

        protected void GridImagemCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string idImagem = GridViewImg.DataKeys[index]["Id"].ToString();
                if (e.CommandName == "Excluir")
                {
                    Regras.Regras del = new Regras.Regras();
                    del.DelImg(idImagem);
                }
                CarregarGrid();
            }
            catch (Exception ex)
            {
                AlertVisibleTrue();
                AlertError(ex.Message);
            }
        }
    }
}
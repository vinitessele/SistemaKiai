using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SisKiai.Regras.Dto;

namespace SisKiai
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {

        public bool adm = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Refresh", String.Concat((Session.Timeout * 60), ";URL=/Default.aspx"));

            if (Session["User"] != null)
            {
                string usuario = Session["User"].ToString();
                string idUsuario = Session["IdUser"].ToString();
                adm = bool.Parse(Session["Administrador"].ToString());
                ControlePaginas(usuario, idUsuario);
            }
            else
            {
                HttpCookie cookie = Request.Cookies["Informacao"];
                if (cookie.Value != null)
                {
                    Session["IdUser"] = cookie["IdUser"];
                    Session["User"] = cookie["User"];
                    Session["Administrador"] = cookie["Administrador"];
                    string usuario = Session["User"].ToString();
                    string idUsuario = Session["IdUser"].ToString();
                    adm = bool.Parse(Session["Administrador"].ToString());
                    ControlePaginas(usuario, idUsuario);
                }
            }
        }

        private void ControlePaginas(string usuario, string idUsuario)
        {
            try
            {
                MenuItem menu = null;
                MenuItem menuPai = null;
                string first = string.Empty;
                NavigationMenu.Items.Clear();
                Regras.Regras dados = new Regras.Regras();

                List<DtoLoginTela> teleasLiberadas = new List<DtoLoginTela>();
                if (adm)
                    teleasLiberadas = dados.GetAllTelasLogin();
                else
                    teleasLiberadas = dados.GetTelasLiberadasPorIdLogin(int.Parse(idUsuario));

                foreach (var l in teleasLiberadas)
                {
                    string[] strArray = null;
                    char[] splitchar = { '/' };

                    strArray = l.EnderecoTela.Split(splitchar);

                    if (!first.Equals(strArray[1]))
                    {
                        if (strArray[2].ToString().Contains("Default2.aspx"))
                        {
                            menuPai = new MenuItem("&nbsp;&nbsp;" + "Home" + "&nbsp;&nbsp;", strArray[2]);
                            NavigationMenu.Items.Add(menuPai);
                            first = strArray[2];
                            menuPai = NavigationMenu.FindItem(strArray[2]);
                            menu = new MenuItem(l.NomeTela, l.NomeTela, string.Empty, "~/Default2.aspx");
                            menuPai.ChildItems.Add(menu);
                        }
                        else
                        {
                            menuPai = new MenuItem("&nbsp;&nbsp;" + strArray[1] + "&nbsp;&nbsp;", strArray[1]);
                            NavigationMenu.Items.Add(menuPai);
                            first = strArray[1];
                            menuPai = NavigationMenu.FindItem(strArray[1]);
                            menu = new MenuItem(l.NomeTela, l.NomeTela, string.Empty, l.EnderecoTela);
                            menuPai.ChildItems.Add(menu);
                        }
                    }
                    else
                    {
                        menuPai = NavigationMenu.FindItem(strArray[1]);
                        menu = new MenuItem(l.NomeTela, l.NomeTela, string.Empty, l.EnderecoTela);
                        menuPai.ChildItems.Add(menu);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void SairClick(object sender, EventArgs e)
        {
            Session["User"] = null;
            Session["IdUser"] = null;
            Response.Redirect("~/Default.aspx");
        }
    }
}

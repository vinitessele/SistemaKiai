<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SisKiai.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-type" content="text/html;charset=ISO-8859-1" />
    <title>..:::FPRKE - Federação Paranaense de Karatê Esportiva:::..</title>
    <link type="text/css" rel="stylesheet" href="Styles/Site.css" />
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cycle.all.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cycle2.min.js"></script>
    <script type="text/javascript">
        function exibe(id) {
            if (document.getElementById(id).style.display === "none") {
                document.getElementById(id).style.display = "inline";
            }
            else {
                document.getElementById(id).style.display = "none";
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.slideshow').cycle({
                fx: 'fade'
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.slideshow2').cycle({
                fx: 'shuffle'
            });
        });
    </script>
    <style type="text/css">
        .style1
        {
            width: 387px;
            height: 639px;
        }
        .style3
        {
            width: 198px;
            height: 169px;
        }
    </style>
</head>
<body>
    <div class="tudo">
        <div class="topo">
            <center>
                <label class="TitulePage">
                    Federação Paranaense de Karatê Esportivo</center>
        </div>
        <div class="Esqueda">
            <div id="login" class="Login">
                <form id="Form1" runat="server">
                <div id="Div2" style="display: inline;">
                    <asp:Login ID="Login1" runat="server" Height="10px" Width="10px" FailureText="Acesso negado!"
                        LoginButtonText="Acessar" OnAuthenticate="Login1_Authenticate" PasswordRequiredErrorMessage="A Senha é obrigatória"
                        RememberMeText="Lembrar" UserNameLabelText="Usuário" UserNameRequiredErrorMessage="Nome do Usuário é obrigatório"
                        BackColor="White" BorderColor="#E6E2D8" BorderPadding="4" BorderStyle="Solid"
                        BorderWidth="1px" Font-Names="Verdana" Font-Size="Small" ForeColor="#333333"
                        TextLayout="TextOnTop" RememberMeSet="True">
                        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                        <LayoutTemplate>
                            <table cellpadding="4" cellspacing="0" style="border-collapse: collapse;">
                                <tr>
                                    <td>
                                        <table cellpadding="0" style="height: 51px; width: 200px;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Usuário:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="UserName" runat="server" Font-Size="Medium" Width="121px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                        ErrorMessage="Nome do Usuário é obrigatório" ToolTip="Nome do Usuário é obrigatório"
                                                        ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Password" runat="server" Font-Size="Medium" TextMode="Password"
                                                        Width="120px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                        ErrorMessage="A Senha é obrigatória" ToolTip="A Senha é obrigatória" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC"
                                                        BorderStyle="Solid" BorderWidth="1px" CommandName="Login" Font-Names="Verdana"
                                                        Font-Size="0.8em" ForeColor="#284775" Text="Acessar" ValidationGroup="Login1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="color: Red;">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                        <LoginButtonStyle BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#1C5E55" />
                        <TextBoxStyle Font-Size="0.8em" />
                        <TitleTextStyle BackColor="#1C5E55" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
                    </asp:Login>
                </div>
                </form>
            </div><br /><br />
            <asp:Image CssClass="img" ID="Image2" runat="server" 
                ImageUrl="~/Imagens/FPRKE.jpg" Height="241px" Width="185px" />
        </div>
        <div class="conteudo">
            <h2>
                Bem vindo a FPRKE</h2>
            <br />
            <h2>
                Próximos Eventos</h2>
            <h3>
                Atenção Professores e Atletas.</h3>
            <p>
                <asp:Label ID="Label1" runat="server" Text=" " Font-Bold="true"></asp:Label>
                <img alt="Apucarana" class="style1" src="Imagens/IMG-20170918-WA0027.jpg" /></p>
            <br />
            <br />
        </div>
        <div id="direita">
            <br />
            <asp:Image CssClass="img" ID="Image3" runat="server" ImageUrl="~/Imagens/Logo IKU.jpg" /><br />
            <img alt="Ceebk" class="style3" src="Imagens/ceebk.jpg" /></div>
    </div>
    <div class="rodape">
        Rua. Carlos Gomes, Nº 537<br />
        Goioerê - PR - CEP: 87360-000<br />
        Email: <a href="mailto:presidente@fprke.com.br">presidente@fprke.com.br</a>
    </div>
    <div class="clear">
    </div>
</body>
</html>

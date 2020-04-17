<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="default2.aspx.cs" Inherits="SisKiai.Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cycle.all.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cycle2.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.slideshow').cycle({
                fx: 'curtainX'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div>
        <a href="https://email.uolhost.com.br/fprke.com.br" title="Acessar Caixa de E-Mails">
            <span class="glyphicon glyphicon-envelope"></span></a>
        <asp:Label ID="Label1" runat="server" Text="Seja bem Vindo.: " Font-Bold="true"></asp:Label>
        <dir>
            <asp:Label ID="Label2" runat="server" Text="Palavra do Presidente.: " Font-Bold="true"></asp:Label><br />
            <asp:Label ID="Label3" runat="server" Text=" " Font-Bold="true"></asp:Label>
        </dir>
        <br />
        <br />
        <div class="externa">
        </div>
    </div>
    <div class="footer">
        <asp:Button ID="BtnCalculaIdade" runat="server" Text="Calcular idade Todos Atletas"
            OnClick="CalcularIdade" CssClass="btn btn-success" />
        <br />
        <span class="failureNotification">
            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
        </span>
        <br />
        <br />
    </div>
    <div class="clear">
    </div>
</asp:Content>

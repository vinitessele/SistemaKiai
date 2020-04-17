<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ICorrecaoAtletaCategoria.aspx.cs" Inherits="SisKiai.Gerenciamento.ICorrecaoAtletaCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                <ProgressTemplate>
                    <div class="overlay">
                        <div id="ProcessMessage">
                            <h3>
                                Aguarde...</h3>
                            <br />
                            <br />
                            <img alt="progress" src="../Imagens/ajax-loader.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="row" style="margin: 0 2px o 2px">
                <div class="col-md-10" id="AlertNotificationDiv" runat="server">
                    <asp:Label ID="AlertNotificationBox" runat="server"></asp:Label>
                </div>
            </div>
            <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="ValidationGroup" />
            <%--<div class="container" id="ParentDiv">--%>
                <br />
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Correção de Inscrição</div>
                    Selecione a Competição desejada<br />
                    <asp:DropDownList CssClass="form-control" ID="DropDownListCompeticao" runat="server"
                        Height="40px" Width="350px" AutoPostBack="true" OnSelectedIndexChanged="DropDownListComnpeticaoChanged">
                        <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    Selecione categoria que deseja alterar - Remover ou Adicionar<br />
                    <asp:DropDownList CssClass="form-control" ID="DropDownListCategoria" runat="server"
                        Height="40px" Width="500px" AutoPostBack="true" OnSelectedIndexChanged="DropDownListCategoriaChanged">
                        <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <%--<div class="container">--%>
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                Adicionar atleta na categoria</div>
                        </div>
                        <br />
                        <br />
                        <div>
                            N° Registro.:<br />
                            <asp:TextBox ID="TextNumeroRegistro" runat="server" CssClass="form-control input-lg"
                                Width="76px" Enabled="true" AutoPostBack="true" OnTextChanged="BuscaFiliadoPorRegistro"></asp:TextBox>
                        </div>
                        <br />
                        <br />
                        <div>
                            <asp:TextBox ID="TextNome" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox>
                            &nbsp;
                            <br />
                            <asp:DropDownList CssClass="form-control" ID="DropDownListAssociacao" runat="server"
                                Height="40px" Width="300px" AutoPostBack="true">
                                <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:Button CssClass="btn btn-success" ID="BtnAdicionar" runat="server" CommandName="Adicionar"
                                OnClick="AdicionarClick" Text="Adicionar" />
                        </div>
                    </div>
                    <br />
                    <asp:GridView CssClass="table table-bordered bs-table" ID="GridAtletas" runat="server"
                        AutoGenerateColumns="False" DataKeyNames="IdinscriaAtleta" OnRowCommand="GridAtletasCommand"
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="IdinscriaAtleta" HeaderText="ID">
                                <HeaderStyle BackColor="ControlLight" Height="10%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumRegistro" HeaderText="N° Registro">
                                <HeaderStyle BackColor="ControlLight" Height="10%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NomeFiliado" HeaderText="Nome">
                                <HeaderStyle BackColor="ControlLight" Height="30%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NomeAcademia" HeaderText="Associação">
                                <HeaderStyle BackColor="ControlLight" Height="20%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/Imagens/delete.gif"
                                Text="Excluir">
                                <HeaderStyle BackColor="ControlLight" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>
                        </Columns>
                        <PagerSettings Position="Bottom" Mode="NextPreviousFirstLast" PreviousPageText="|Anterior|"
                            NextPageText="|Próxima|" FirstPageText="|Primeira Página|" LastPageText="|Última Página|"
                            PageButtonCount="50" />
                    </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

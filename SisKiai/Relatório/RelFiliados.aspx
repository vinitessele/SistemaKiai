<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RelFiliados.aspx.cs" Inherits="SisKiai.Relatorio.RelFiliados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        function cont() {
            var conteudo = document.getElementById('print').innerHTML;
            tela_impressao = window.open('about:blank');
            tela_impressao.document.write(conteudo);
            tela_impressao.window.print();
            tela_impressao.window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="main" id="ParentDiv">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Relatório de Situação Filiados</div>
                        <br />
                    <asp:DropDownList CssClass="form-control" ID="DropDownListAssociacao" AutoPostBack="true"
                        runat="server" Height="40px" Width="400px" OnSelectedIndexChanged="DropDowListChanged">
                        <asp:ListItem Value="0" Selected="True">-Todos-</asp:ListItem>
                    </asp:DropDownList>
                    <table width="100%">
                        <tr>
                            <td>
                                
                                    Ordem.:<br />
                                    <asp:RadioButtonList ID="RBListOrdem" runat="server" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text="Alfabética" Value="0">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Número de Registro" Value="1">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Ordem de Cadastro" Value="2">
                                        </asp:ListItem>
                                    </asp:RadioButtonList>
                                
                            </td>
                            <td>
                                
                                    Status.:<br />
                                    <asp:RadioButtonList ID="RBListStatus" runat="server" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text="Todos" Value="T">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Ativos" Value="A">
                                        </asp:ListItem>
                                        <asp:ListItem Text="Inativos" Value="I">
                                        </asp:ListItem>
                                    </asp:RadioButtonList>
                                
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="overlay">
                            <img alt="progress" src="../Imagens/ajax-loader.gif" />
                            <h3>
                                Aguarde...</h3>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div id="print" class="Print">
                    <asp:GridView CssClass="table table-bordered bs-table" ID="GridFiliado" runat="server"
                        AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IdFiliado" OnPageIndexChanging="GrdFiliadoPageIndexChanging"
                        Width="100%" PageSize="100">
                        <Columns>
                            <asp:BoundField DataField="IdFiliado" HeaderText="ID">
                                <HeaderStyle BackColor="ControlLight" Height="5%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NumeroRegistro" HeaderText="N° Registro">
                                <HeaderStyle BackColor="ControlLight" Height="5%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NomeFiliado" HeaderText="Nome">
                                <HeaderStyle BackColor="ControlLight" Height="30%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NomeAssociacao" HeaderText="Associação">
                                <HeaderStyle BackColor="ControlLight" Height="20%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmailFiliado" HeaderText="Email">
                                <HeaderStyle BackColor="ControlLight" Height="20%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IdadeFiliado" HeaderText="Idade">
                                <HeaderStyle BackColor="ControlLight" Height="5%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="StatusFiliado" HeaderText="Status">
                                <HeaderStyle BackColor="ControlLight" Height="5%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <PagerSettings Position="Bottom" Mode="NextPreviousFirstLast" PreviousPageText="|Anterior|"
                            NextPageText="|Próxima|" FirstPageText="|Primeira Página|" LastPageText="|Última Página|"
                            PageButtonCount="50" />
                    </asp:GridView>
                </div>
                <input type="button" onclick="cont();" value="Imprimir" class="btn btn-success">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ISorteioCategoria.aspx.cs" Inherits="SisKiai.Gerenciamento.ISorteioCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../Imagens/minus.jpg");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../Imagens/plus.jpg");
            $(this).closest("tr").next().remove();
        });
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
                            <center>
                                <img alt="progress" src="../Imagens/ajax-loader.gif" /></center>
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
            <div class="main" id="ParentDiv">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Sorteio</div>
                    <br />
                    <br />
                    <asp:DropDownList CssClass="form-control" ID="DropDownListCompeticao" runat="server"
                        Height="40px" Width="500px" AutoPostBack="true" OnSelectedIndexChanged="DropDownListComnpeticaoChanged">
                        <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <h3>
                        Enviar E-Mail para as associações para conferência</h3>
                    <hr width="100%" align="left" />
                    <asp:Button ID="BtnRelacaoConferencia" runat="server" CommandName="BtnRelacaoConferencia"
                        OnClick="BtnConferencia" Text="Enviar E-Mail para conferência de atletas" CssClass="btn btn-info"
                        Visible="false" />
<%--                    <asp:Button ID="BtnRelacaoConferencia0" runat="server" 
                        CommandName="BtnRelacaoConferencia" CssClass="btn btn-info" 
                        OnClick="BtnConferenciaCategoria" Text="Enviar E-Mail para conferência das categirias" 
                        Visible="false" />--%>
                    <br />
                    <h3>
                        Lista de Categorias a serem sorteadas</h3>
                    <hr width="100%" align="left" />
                    <p>
                        <asp:GridView CssClass="table table-bordered bs-table" ID="GridCategoria" runat="server"
                            AutoGenerateColumns="false" DataKeyNames="IdCategoria,NrCategoria" OnRowCommand="GridCategoriaOnRowCommand"
                            OnRowDataBound="OnRowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img alt="" style="cursor: pointer" src="../Imagens/plus.jpg" />
                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                            <asp:GridView CssClass="table table-bordered bs-table" ID="GridAtletasCategoria"
                                                runat="server" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="NumRegistro" HeaderText="Número Registro" ItemStyle-Width="150px">
                                                        <HeaderStyle BackColor="ControlLight" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NomeFiliado" HeaderText="Nome Atleta" ItemStyle-Width="150px">
                                                        <HeaderStyle BackColor="ControlLight" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SiglaAssociacao" HeaderText="Associação" ItemStyle-Width="150px">
                                                        <HeaderStyle BackColor="ControlLight" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PosicaoSorteio" HeaderText="Posição Sorteio" ItemStyle-Width="150px">
                                                        <HeaderStyle BackColor="ControlLight" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="ControlLight" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="IdCategoria" HeaderText="Id" ItemStyle-Width="30px" Visible="false">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NrCategoria" HeaderText="Número Categoria" ItemStyle-Width="120px">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TipoCompeticao" HeaderText="Tipo" ItemStyle-Width="100px">
                                    <HeaderStyle BackColor="ControlLight" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NomeCategoria" HeaderText="Nome Categoria" ItemStyle-Width="350px">
                                    <HeaderStyle BackColor="ControlLight" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QteAtletasCategoria" HeaderText="Qte Atletas" ItemStyle-Width="80px">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField ButtonType="Button" CommandName="SorteioEliminatoriaSimples" ControlStyle-CssClass="btn btn-success"
                                    HeaderText="Sorteio" ItemStyle-Width="150px" Text="Eliminatória Simples">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="ControlLight" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Button" CommandName="GerarChave" ControlStyle-CssClass="btn btn-default"
                                    HeaderText="Gerar Chave" ItemStyle-Width="150px" Text="Gerar Chave">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="ControlLight" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Button" CommandName="EnviarEmail" ControlStyle-CssClass="btn btn-primary"
                                    HeaderText="Enviar Categoria por E-mail" ItemStyle-Width="150px" Text="Enviar Categoria por E-mail">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="ControlLight" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </p>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

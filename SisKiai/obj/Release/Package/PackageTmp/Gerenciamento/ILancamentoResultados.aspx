<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ILancamentoResultados.aspx.cs" Inherits="SisKiai.Gerenciamento.ILancamentoResultados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            var manager = Sys.WebForms.PageRequestManager.getInstance();
            manager.add_endRequest(endRequest);
            manager.add_beginRequest(OnBeginRequest);
        }
        function OnBeginRequest(sender, args) {
            $get('ParentDiv').className = 'Background';
        }
        function endRequest(sender, args) {
            $get('ParentDiv').className = '';
        }  
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
            <div class="main" id="ParentDiv">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Lançamento de Resultados</div><br />
                    <asp:DropDownList CssClass="form-control" ID="DropDownListCompeticao" runat="server"
                        Height="40px" Width="500px" AutoPostBack="true" OnSelectedIndexChanged="DropDownListComnpeticaoChanged">
                        <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:GridView CssClass="table table-bordered bs-table" ID="GridCategoria" runat="server"
                        AutoGenerateColumns="false" DataKeyNames="IdCategoria,NrCategoria" OnRowCommand="GridCategoriaOnRowCommand"
                        OnRowDataBound="OnRowDataBound" Width="100%">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle BackColor="ControlLight" />
                                <ItemTemplate>
                                    <asp:GridView CssClass="table table-bordered bs-table" ID="GridAtletasCategoria"
                                        runat="server" AutoGenerateColumns="false" DataKeyNames="IdCategoriaAtleta" OnRowCancelingEdit="GridAtletasCategoriaCancel"
                                        OnRowEditing="GridAtletasCategoriaEdit" OnRowUpdating="GridAtletasCategoriaUp"
                                        Width="600px" GridLines="None" EditRowStyle-HorizontalAlign="Center" EditRowStyle-VerticalAlign="Middle">
                                        <Columns>
                                            <asp:CommandField ButtonType="Button" ItemStyle-Width="10px" ShowEditButton="true">
                                                <HeaderStyle BackColor="ControlLight" />
                                            </asp:CommandField>
                                            <asp:BoundField DataField="IdCategoriaAtleta" HeaderText="ID" InsertVisible="false"
                                                ItemStyle-Width="30px" ReadOnly="true" Visible="false">
                                                <HeaderStyle BackColor="ControlLight" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumRegistro" HeaderText="Registro" InsertVisible="false"
                                                ItemStyle-Width="60px" ReadOnly="true">
                                                <HeaderStyle BackColor="ControlLight" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NomeFiliado" HeaderText="Nome Atleta" InsertVisible="false"
                                                ItemStyle-Width="250px" ReadOnly="true">
                                                <HeaderStyle BackColor="ControlLight" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SiglaAssociacao" HeaderText="Associação" InsertVisible="false"
                                                ItemStyle-Width="150px" ReadOnly="true">
                                                <HeaderStyle BackColor="ControlLight" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ResultadoCompeticao" HeaderText="Resultado" ItemStyle-Width="60px">
                                                <HeaderStyle BackColor="ControlLight" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                    <%--</asp:Panel>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IdCategoria" HeaderText="Id" ItemStyle-Width="30px" ReadOnly="true"
                                Visible="false">
                                <HeaderStyle BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NrCategoria" HeaderText="Categoria" ItemStyle-Width="50px"
                                ReadOnly="true">
                                <HeaderStyle BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipoCompeticao" HeaderText="Tipo" ItemStyle-Width="80px"
                                ReadOnly="true">
                                <HeaderStyle BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NomeCategoria" HeaderText="Nome Categoria" ItemStyle-Width="350px"
                                ReadOnly="true">
                                <HeaderStyle BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="QteAtletasCategoria" HeaderText="Qte" ItemStyle-Width="80px"
                                ReadOnly="true">
                                <HeaderStyle BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Categoria_Finalizada2" HeaderText="Finalizada?" ItemStyle-Width="80px"
                                ReadOnly="true">
                                <HeaderStyle BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Button" CommandName="FinalizaCategoria" HeaderText="Finalizar?"
                                ItemStyle-CssClass="bold" ItemStyle-Width="150px" Text="Finaliza">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle BackColor="ControlLight" />
                            </asp:ButtonField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="clear">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Finalizar Competição</div>
                    <br />
                    <br />
                    <asp:Button ID="btnFinalizar" runat="server" Text=" Finalizar Competição " OnClick="btnFinalizarClick"
                        CssClass="btn btn-danger" />
                    <br />
                    <br />
                </div>
            </div>
            <div class="clear">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Finalizar Competição</div>
                    <br />
                    <asp:DropDownList CssClass="form-control" ID="DropDownListReabrir" runat="server"
                        Height="40px" Width="500px" AutoPostBack="true" OnSelectedIndexChanged="DropDownListComnpeticaoChanged">
                        <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Button ID="Button1" runat="server" Text=" Reabrir Competição " OnClick="btnReabrirClick"
                        CssClass="btn btn-success" /><br />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

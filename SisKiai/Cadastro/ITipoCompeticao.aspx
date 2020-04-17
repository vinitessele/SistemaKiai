<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ITipoCompeticao.aspx.cs" Inherits="SisKiai.Cadastro.ITipoCompeticao" %>

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
    <span class="failureNotification">
        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="ValidationGroup" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container" id="ParentDiv">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Tipo Competição</div>
                    <asp:TextBox ID="TextIdTipo" runat="server" CssClass="form-control input-lg" Visible="false"
                        Width="40px"></asp:TextBox>
                    <br />
                    
                        Nome.:<br />
                        <asp:TextBox ID="TextDescricao" runat="server" CssClass="form-control input-lg" Width="300px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TxtDescricaoRequired" runat="server" ControlToValidate="TextDescricao"
                            CssClass="failureNotification" ErrorMessage="Campo Obrigatório Descrição." ToolTip="Campo Obrigatório."
                            ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                    
                    <br />
                    
                        Esporte.:<br />
                        <asp:DropDownList CssClass="form-control" ID="DropDownListEsporte" runat="server"
                            Height="40px" Width="280px" AutoPostBack="true">
                            <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                        </asp:DropDownList>
                    
                    <br />
                    <p class="submitButton">
                        <asp:Button CssClass="btn btn-success" ID="BtnSalvar" runat="server" CommandName="Salvar"
                            Text="Salvar" ValidationGroup="ValidationGroup" OnClick="SalvarClick" />
                        <input id="Cancelar" type="reset" value="Cancelar" class="btn btn-danger" />
                        <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                            <ProgressTemplate>
                                <div class="overlay">
                                    <img alt="progress" src="../Imagens/ajax-loader.gif" />
                                    <h3>
                                        Aguarde...</h3>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                </div>
                <h3>
                    Lista</h3>
                <div>
                    
                        Consulta.:
                        <asp:TextBox ID="TextConsulta" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox><br />
                        <asp:Button ID="BtnConsulta" runat="server" CommandName="Consulta" class="btn btn-info"
                            OnClick="ConsultarClick" Text="Consultar" />
                    
                </div>
                <asp:GridView CssClass="table table-bordered bs-table" ID="GridViewTipoCompeticao"
                    runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IdTipoCompeticao"
                    OnPageIndexChanging="GrdTipoCompeticaoPageIndexChanging" OnRowCommand="GridTipoCompeticaoCommand"
                    Width="100%">
                    <Columns>
                        <asp:BoundField DataField="IdTipoCompeticao" HeaderText="ID">
                            <HeaderStyle BackColor="ControlLight" Height="10%" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DescricaoCompeticao" HeaderText="Descrição">
                            <HeaderStyle BackColor="ControlLight" Height="60%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NomeEsporte" HeaderText="Esporte">
                            <HeaderStyle BackColor="ControlLight" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagens/edit.gif"
                            Text="Editar">
                            <HeaderStyle BackColor="ControlLight" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
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
                <%--  </fieldset>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

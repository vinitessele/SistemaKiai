<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ITelas.aspx.cs" Inherits="SisKiai.Cadastro.ITelas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
//        function pageLoad() {
//            var manager = Sys.WebForms.PageRequestManager.getInstance();
//            manager.add_endRequest(endRequest);
//            manager.add_beginRequest(OnBeginRequest);
//        }
//        function OnBeginRequest(sender, args) {
//            $get('ParentDiv').className = 'Background';
//        }
//        function endRequest(sender, args) {
//            $get('ParentDiv').className = '';
//        }  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification"
                ValidationGroup="ValidationGroup" />
            <div class="main" id="ParentDiv">
                <br />
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Cadastro de Telas</div>
                    <hr width="100%" align="left" />
                    <asp:TextBox ID="TextIdTelas" runat="server" CssClass="form-control input-lg" Visible="false"
                        Width="40px"></asp:TextBox>
                    <div class="container">
                        
                            Nome.:<br />
                            <asp:TextBox ID="TextNome" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TxtNomeRequired" runat="server" ControlToValidate="TextNome"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Nome." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                    </div>
                    <br />
                    <br />
                    <br />
                    <div>
                        
                            Endereço .:<br />
                            <asp:TextBox ID="TextEndereco" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TextEnderecoValidator1" runat="server" ControlToValidate="TextEndereco"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Endereço." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                    </div>
                    <br />
                    <br />
                    <br />
                    <p class="submitButton">
                        <asp:Button CssClass="btn btn-success" ID="BtnSalvar" runat="server" CommandName="Salvar"
                            Text="Salvar" ValidationGroup="ValidationGroup" OnClick="SalvarClick" />
                        <input id="Cancelar" type="reset" value="Cancelar" class="btn btn-danger" />
                    </p>
                </div>
                <h3>
                    Lista</h3>
                <hr width="100%" align="left" />
                <div>
                    
                        Consulta.:
                        <asp:TextBox ID="TextConsulta" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox><br />
                        <asp:Button class="btn btn-info" ID="BtnConsulta" runat="server" CommandName="Consulta"
                            Text="Consultar" OnClick="ConsultarClick" />
                    
                </div>
                <asp:GridView CssClass="table table-bordered bs-table" ID="GridTelas" runat="server"
                    AutoGenerateColumns="False" Width="100%" DataKeyNames="IdTelas" OnRowCommand="GridTelasCommand"
                    AllowPaging="True" OnPageIndexChanging="GrdTelasPageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="IdTelas">
                            <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Nome" DataField="Nome">
                            <HeaderStyle HorizontalAlign="Center" Height="40%" BackColor="ControlLight" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Endereço" DataField="Endereco">
                            <HeaderStyle HorizontalAlign="Center" Height="40%" BackColor="ControlLight" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagens/edit.gif" CommandName="Editar"
                            Text="Editar">
                            <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagens/delete.gif" CommandName="Excluir"
                            Text="Excluir">
                            <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                    </Columns>
                    <PagerSettings Position="Bottom" Mode="NextPreviousFirstLast" PreviousPageText="|Anterior|"
                        NextPageText="|Próxima|" FirstPageText="|Primeira Página|" LastPageText="|Última Página|"
                        PageButtonCount="50" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

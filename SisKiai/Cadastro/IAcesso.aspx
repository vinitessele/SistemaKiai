<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IAcesso.aspx.cs" Inherits="SisKiai.Cadastro.IAcesso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        function exibe(id) {
            if (document.getElementById(id).style.display === "none") {
                document.getElementById(id).style.display = "inline";
            }
            else {
                document.getElementById(id).style.display = "none";
            }
        }
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
    <div class="main" id="ParentDiv">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
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
                <div class="container">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Cadastro de Acesso</div>
                        <asp:TextBox ID="TextIdAcesso" runat="server" CssClass="form-control input-lg" Visible="false"
                            Width="40px"></asp:TextBox>
                        <br />
                        
                            Nome.:<br />
                            <asp:TextBox ID="TextNome" runat="server" CssClass="form-control input-lg" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TxtNomeRequired" runat="server" ControlToValidate="TextNome"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Nome." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                        <br />
                        <br />
                        <br />
                        
                            Login.:<br />
                            <asp:TextBox ID="TextLogin" runat="server" CssClass="form-control input-lg"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TextLoginValidator1" runat="server" ControlToValidate="TextLogin"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Login." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                        <br />
                        
                            Senha.:<br />
                            <asp:TextBox ID="TextSenha" runat="server" CssClass="form-control input-lg" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TextSenhaValidator1" runat="server" ControlToValidate="TextSenha"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Senha." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                        <br />
                        <br />
                        <br />
                        
                            Associação/Academia/Clube.:<br />
                            <asp:DropDownList CssClass="form-control" ID="DropDownListAssociacao" runat="server"
                                Height="40px" Width="500px">
                                <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                            </asp:DropDownList>
                        
                        <br />
                        <br />
                        <br />
                        
                            Administrador.:
                            <asp:CheckBox ID="CheckBoxAdm" runat="server" Checked="false" />
                            &nbsp; -&nbsp; Status.:
                            <asp:CheckBox ID="CheckBoxStatus" runat="server" Checked="true" Text="Ativo" />
                        
                        <br />
                        <br />
                        <br />
                        <p class="submitButton">
                            <asp:Button CssClass="btn btn-success" ID="BtnSalvar" runat="server" CommandName="Salvar"
                                Text="Salvar" ValidationGroup="ValidationGroup" OnClick="SalvarClick" />
                            <input id="Cancelar" type="reset" value="Cancelar" class="btn btn-danger" />
                        </p>
                    </div>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Liberação de Telas</div>
                        <br />
                        <a href="#" onclick="javascript: exibe('conteudo');">Adicionar/Liberar Telas</a><br />
                        <br />
                        <div id="conteudo" style="display: none;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ListBox ID="ListBoxTelasDisponiveis" runat="server" Height="200px" Width="187px">
                                        </asp:ListBox>
                                    </td>
                                    <td>
                                        <div>
                                            <center>
                                                <asp:Button CssClass="btn btn-success" ID="BtnAdiciona" runat="server" CommandName="Adiciona"
                                                    Text="Adiciona" OnClick="AdicionaClick" Height="30px" Width="130px" /><br /><br />
                                                <asp:Button CssClass="btn btn-success" ID="BtnTodos" runat="server" CommandName="AdicionaTodos"
                                                    Text="Adiciona Todos" OnClick="AdicionaTodosClick" Height="30px" Width="130px" /><br /><br />
                                                <asp:Button CssClass="btn btn-danger" ID="BtnRemove" runat="server" CommandName="Remove"
                                                    Text="Remove" OnClick="RemoveClick" Height="30px" Width="130px" /><br /><br />
                                                <asp:Button CssClass="btn btn-danger" ID="BtnRemoveTodos" runat="server" CommandName="RemoveTodos"
                                                    Text="Remove Todos" OnClick="RemoveTodosClick" Height="30px" Width="130px" />
                                            </center>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="ListBoxTelasLiberadas" runat="server" Height="200px" Width="187px">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Consulta acessos</div>
                            
                                Consulta.:
                                <asp:TextBox ID="TextConsulta" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox>
                                <br />
                                <asp:Button class="btn btn-info" ID="BtnConsulta" runat="server" CommandName="Consulta"
                                    Text="Consultar" OnClick="ConsultarClick" />
                            
                        </div>
                        <asp:GridView CssClass="table table-bordered bs-table" ID="GridViewLogin" runat="server"
                            AutoGenerateColumns="False" Width="100%" DataKeyNames="IdAcesso" OnRowCommand="GridLoginCommand"
                            OnPageIndexChanging="GrdLoginPageIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="IdAcesso">
                                    <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Nome" DataField="Nome">
                                    <HeaderStyle HorizontalAlign="Center" Height="40%" BackColor="ControlLight" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Login" DataField="Login">
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

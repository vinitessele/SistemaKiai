<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IAcademia.aspx.cs" Inherits="SisKiai.Cadastro.IAcademia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
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
            <div class="container" id="ParentDiv">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Cadastro de Associação/Clube/Academia</div>
                    <asp:TextBox ID="TextIdAssocicao" runat="server" CssClass="form-control input-lg"
                        Visible="false" Width="40px">
                    </asp:TextBox>
                    <div>
                        
                            Nome.:<asp:RequiredFieldValidator ID="TextNomeAssociacaoRequired" runat="server" ControlToValidate="TextNomeAssociacao"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Nome." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                        
                            <br />
                            <asp:TextBox ID="TextNomeAssociacao" runat="server" CssClass="form-control input-lg"
                                Width="600px"></asp:TextBox>
                        
                        
                            Sigla.:<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextSigla"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Sigla." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="TextSigla" runat="server" CssClass="form-control input-lg"
                                Width="200px"></asp:TextBox>
                            <br />
                        
                    </div>
                    <div>
                        
                            Endereço.:<asp:RequiredFieldValidator ID="TextEnderecoValidator1" runat="server" ControlToValidate="TextEndereco"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Endereço." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                            <br />
                            <asp:TextBox ID="TextEndereco" runat="server" CssClass="form-control input-lg" Width="400px"></asp:TextBox>
                        
                    </div>
                    <table>
                        <tr>
                            <td>
                                <div>
                                    
                                        Cidade.:<br />
                                        <asp:DropDownList CssClass="form-control" ID="DropDownListCidade" runat="server"
                                            Height="40px" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="BuscaCep">
                                            <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                                        </asp:DropDownList>
                                    
                                </div>
                            </td>
                            <td>
                                <div>
                                    
                                        CEP.:<br />
                                        <asp:TextBox ID="TextCep" runat="server" CssClass="form-control input-lg" Width="150px"
                                            Enabled="False"></asp:TextBox>
                                    
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div>
                        
                            Responsável.:<asp:RequiredFieldValidator ID="TextResponsavelValidator1" runat="server" ControlToValidate="TextResponsavel"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Responsável."
                                ToolTip="Campo Obrigatório." ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                            <br />
                            <asp:TextBox ID="TextResponsavel" runat="server" CssClass="form-control input-lg"
                                Width="400px"></asp:TextBox>
                        
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    
                                        Telefone Fixo.:<br />
                                        <asp:TextBox ID="TextTelefone" runat="server" CssClass="form-control input-lg" Width="150px"></asp:TextBox>
                                    
                                </td>
                                <td>
                                    
                                        Telefone Celular.:<br />
                                        <asp:TextBox ID="TextTelefoneCelular" runat="server" CssClass="form-control input-lg"
                                            Width="150px"></asp:TextBox>
                                    
                                </td>
                                <td>
                                    
                                        E-Mail.:<br />
                                        <asp:TextBox ID="TextEmail" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox>
                                    
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        
                                            CNPJ.:<br />
                                            <asp:TextBox ID="TextCnpj" runat="server" CssClass="form-control input-lg" Width="200px"
                                                OnTextChanged="TextValidaCnpj"></asp:TextBox>
                                        
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        
                                            Inscrição Estadual.:<br />
                                            <asp:TextBox ID="TextInscri" runat="server" CssClass="form-control input-lg" Width="200px"></asp:TextBox>
                                        
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                        Esporte.:<br />
                        <asp:DropDownList CssClass="form-control" ID="DropDownListEsporte" runat="server"
                            Height="40px" Width="280px">
                            <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    
                    <p class="submitButton">
                        <asp:Button CssClass="btn btn-success" ID="BtnSalvar" runat="server" CommandName="Salvar"
                            Text="Salvar" ValidationGroup="ValidationGroup" OnClick="SalvarClick" />
                        <input class="btn btn-danger" id="Cancelar" type="reset" value="Cancelar" />
                </div>
                <br />
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Consulta de Associação/Clube/Academia</div>
                    
                        Consulta.:
                        <asp:TextBox ID="TextConsulta" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox>
                        &nbsp;<br />
                        <asp:Button ID="BtnConsulta" class="btn btn-info" runat="server" CommandName="Consulta"
                            OnClick="ConsultarClick" Text="Consultar" />
                    
                </div>
                <br />
                <asp:GridView CssClass="table table-bordered bs-table" ID="GridAcademia" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="IdAcademia" OnPageIndexChanging="GrdAcademiaPageIndexChanging"
                    OnRowCommand="GridAcademiaCommand" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="IdAcademia" HeaderText="ID">
                            <HeaderStyle BackColor="ControlLight" Height="5%" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NomeAcademia" HeaderText="Nome">
                            <HeaderStyle BackColor="ControlLight" Height="30%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ResponsavelAcademia" HeaderText="Resposável">
                            <HeaderStyle BackColor="ControlLight" Height="20%" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EmailAcademia" HeaderText="Email">
                            <HeaderStyle BackColor="ControlLight" Height="20%" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

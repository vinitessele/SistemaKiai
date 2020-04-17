<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ICidade.aspx.cs" Inherits="SisKiai.Cadastro.ICidade" %>

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
            <div class="container" id="ParentDiv">
                <br />
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Cadastro de Cidades
                    </div>
                    <asp:TextBox ID="TextIdCidade" runat="server" CssClass="form-control input-lg" Visible="false"
                        Width="40px"></asp:TextBox>
                    <div>
                        
                            Nome.:<br />
                            <asp:TextBox ID="TextNomeCidade" runat="server" CssClass="form-control input-lg"
                                Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TxtNomeCidadeRequired" runat="server" ControlToValidate="TextNomeCidade"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Nome." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                        <br />
                        
                            Sigla.:<br />
                            <asp:DropDownList CssClass="form-control" ID="DropDownListEstado" runat="server"
                                Height="40px" Width="150px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="DropDownList1Validator" runat="server" ControlToValidate="DropDownListEstado"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Estado." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                        <br />
                        
                            CEP.:<br />
                            <asp:TextBox ID="TextCep" runat="server" CssClass="form-control input-lg" Width="130px"></asp:TextBox>
                        
                    </div>
                    <br />
                    <br />
                    <p class="clear">
                        <br />
                        <br />
                        <asp:Button CssClass="btn btn-success" ID="BtnSalvar" runat="server" CommandName="Salvar"
                            Text="Salvar" ValidationGroup="ValidationGroup" OnClick="SalvarClick" />
                        <input id="Cancelar" type="reset" value="Cancelar" class="btn btn-danger" />
                        <br />
                        <p>
                        </p>
                </div>
                <h3>
                    Lista</h3>
                <hr width="100%" align="left" />
                <br />
                
                    Consulta.:
                    <asp:TextBox ID="TextConsulta" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox><br />
                    <asp:Button ID="BtnConsulta" class="btn btn-info" runat="server" CommandName="Consulta"
                        OnClick="ConsultarClick" Text="Consultar" />
                
                <br />
                <asp:GridView CssClass="table table-bordered bs-table" ID="GridViewCidade" runat="server"
                    AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IdCidade" OnPageIndexChanging="GrdCidadePageIndexChanging"
                    OnRowCommand="GridCidadeCommand" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="IdCidade" HeaderText="ID">
                            <HeaderStyle BackColor="ControlLight" Height="10%" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NomeCidade" HeaderText="Nome">
                            <HeaderStyle BackColor="ControlLight" Height="80%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SiglaEstado" HeaderText="Estado">
                            <HeaderStyle BackColor="ControlLight" Height="10%" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CepCidade" HeaderText="CEP">
                            <HeaderStyle BackColor="ControlLight" Height="10%" HorizontalAlign="Center" />
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
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                </p>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

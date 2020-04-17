<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IEsporte.aspx.cs" Inherits="SisKiai.Cadastro.IEsporte" %>

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
                        Cadastro de Esportes</div>
                    <asp:TextBox ID="TextId" runat="server" CssClass="form-control input-lg" Visible="false"
                        Width="40px"></asp:TextBox><br />
                    
                        Nome do Esporte.:<br />
                        <asp:TextBox ID="TextNomeEsporte" runat="server" CssClass="form-control input-lg"
                            Width="400px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="TxtNomeEsporteRequired" runat="server" ControlToValidate="TextNomeEsporte"
                            CssClass="failureNotification" ErrorMessage="Campo Obrigatório Nome" ToolTip="Campo Obrigatório."
                            ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                    <p class="submitButton">
                        <br />
                        <asp:Button CssClass="btn btn-success" ID="BtnSalvar" runat="server" CommandName="Salvar"
                            Text="Salvar" ValidationGroup="ValidationGroup" OnClick="SalvarClick" />
                        <input id="Cancelar" class="btn btn-danger" type="reset" value="Cancelar" />
                    </p>
                </div>
                <h3>
                    Lista</h3>
                <hr width="100%" align="left" />
                
                    Consulta.:
                    <asp:TextBox ID="TextConsulta" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox><br />
                    <asp:Button ID="BtnConsulta" class="btn btn-info" runat="server" CommandName="Consulta"
                        Text="Consultar" OnClick="ConsultarClick" />
                <asp:GridView CssClass="table table-bordered bs-table" ID="GridEsportes" runat="server"
                    AutoGenerateColumns="False" Width="100%" DataKeyNames="IdEsportes" OnRowCommand="GridEsportesCommand"
                    OnPageIndexChanging="GrdEsportePageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="IdEsportes">
                            <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Nome" DataField="NomeEsportes">
                            <HeaderStyle HorizontalAlign="Center" Height="80%" BackColor="ControlLight" />
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

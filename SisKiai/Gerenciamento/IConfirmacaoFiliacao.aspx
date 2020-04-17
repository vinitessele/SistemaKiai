<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IConfirmacaoFiliacao.aspx.cs" Inherits="SisKiai.Gerenciamento.IConfirmacaoFiliacao" %>

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
                        Confirmação ou bloqueio de Filiados</div>
                    <div>
                        
                            Associação/Academia/Clube.:<br />
                            <asp:DropDownList CssClass="form-control" ID="DropDownListAssociacao" runat="server"
                                Height="40px" Width="500px" AutoPostBack="true" OnSelectedIndexChanged="ListaFiliadosPorAssociacao">
                                <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                            </asp:DropDownList>
                        
                    </div>
                </div>
                <div>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Consulta</div>
                        
                            Consulta.:
                            <asp:TextBox ID="TextConsulta" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox><br>
                            &nbsp;<asp:Button ID="BtnConsulta" class="btn btn-info" runat="server" CommandName="Consulta"
                                Text="Consultar" OnClick="ConsultarClick" />
                        
                        <br />
                        <h4>
                            Status A = Ativo, I = Inativo, P = Pendente</h4>
                        <br />
                        &nbsp;<asp:Button ID="Button1" class="btn btn-danger" runat="server" CommandName="Inativartodos"
                            Text="Inativar Todos" OnClick="InativarClick" />
                    </div>
                    <br />
                    <asp:GridView CssClass="table table-bordered bs-table" ID="GridFiliado" runat="server"
                        AutoGenerateColumns="False" Width="100%" DataKeyNames="IdFiliado" OnRowCommand="GridFiliadoCommand"
                        OnPageIndexChanging="GrdFiliadoPageIndexChanging" OnRowDataBound="GridFiliadoDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="IdFiliado">
                                <HeaderStyle HorizontalAlign="Center" Height="5%" BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="N° Registro" DataField="NumeroRegistro">
                                <HeaderStyle HorizontalAlign="Center" Height="5%" BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nome" DataField="NomeFiliado">
                                <HeaderStyle HorizontalAlign="Center" Height="20%" BackColor="ControlLight" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Associação" DataField="NomeAssociacao">
                                <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Email" DataField="EmailFiliado">
                                <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Telefone" DataField="TelefoneCelular">
                                <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Idade" DataField="IdadeFiliado">
                                <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Status" DataField="StatusFiliado">
                                <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:ButtonField ButtonType="Image">
                                <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonField>
                        </Columns>
                        <PagerSettings Position="Bottom" Mode="NextPreviousFirstLast" PreviousPageText="|Anterior|"
                            NextPageText="|Próxima|" FirstPageText="|Primeira Página|" LastPageText="|Última Página|"
                            PageButtonCount="50" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

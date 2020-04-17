<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IRanking.aspx.cs" Inherits="SisKiai.Cadastro.IRanking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
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
            <div class="row" style="margin: 0 2px o 2px">
                <div class="col-md-10" id="AlertNotificationDiv" runat="server">
                    <asp:Label ID="AlertNotificationBox" runat="server"></asp:Label>
                </div>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification"
                ValidationGroup="ValidationGroup" />
            <div class="container" id="ParentDiv">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Ranking</div>
                    <asp:TextBox ID="TextIdRanking" runat="server" CssClass="form-control input-lg" Visible="false"
                        Width="40px"></asp:TextBox>
                    <div>
                        
                            ANO.:<br />
                            <asp:TextBox ID="TextAno" runat="server"  CssClass="form-control input-lg" Width="145px"
                                Height="40px" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TxtAnoRequired" runat="server" ControlToValidate="TextAno"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório ANO." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                    </div>
                    <br />
                    <br />
                    <br />
                    <div>
                        
                            Esporte.:<br />
                            <asp:DropDownList CssClass="form-control" ID="DropDownListEsporte" runat="server"
                                Height="40px" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="DropDownListEsporteChange">
                                <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                            </asp:DropDownList>
                        
                    </div>
                    <br />
                    <div>
                        
                            Associação/Academia/Clube.:<br />
                            <asp:DropDownList CssClass="form-control" ID="DropDownListAssociacao" runat="server"
                                Height="40px" Width="400px" AutoPostBack="true" OnSelectedIndexChanged="DropDownListAssociacao_SelectedIndexChanged">
                                <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                            </asp:DropDownList>
                        
                    </div>
                    <br />
                    
                        Nome .:<br />
                        <asp:DropDownList CssClass="form-control" ID="DropDownListNome" runat="server" Height="40px"
                            Width="400" AutoPostBack="true" OnSelectedIndexChanged="DropDownListNome_SelectedIndexChanged">
                        </asp:DropDownList>
                    
                    <br />
                    <br />
                    <br />
                    
                        Pontos .:<br />
                        <asp:TextBox ID="TextPontos" runat="server" 
                        CssClass="form-control input-lg" Width="145px"
                            Height="40px"></asp:TextBox>
                    
                    <br />
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
                        &nbsp;<asp:Button ID="BtnConsulta" class="btn btn-info" runat="server" CommandName="Consulta"
                            Text="Consultar" OnClick="ConsultarClick" />
                    
                    <asp:GridView CssClass="table table-bordered bs-table" ID="GridRanking" runat="server"
                        AutoGenerateColumns="False" Width="100%" DataKeyNames="IdRanking" OnRowCommand="GridRankingCommand"
                        AllowPaging="True" OnPageIndexChanging="GridRankingPageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="IdRanking">
                                <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nome" DataField="NomeRanking">
                                <HeaderStyle HorizontalAlign="Center" Height="40%" BackColor="ControlLight" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Pontos" DataField="PontoRanking">
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

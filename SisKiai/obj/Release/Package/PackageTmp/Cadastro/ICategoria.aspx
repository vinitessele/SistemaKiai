<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ICategoria.aspx.cs" Inherits="SisKiai.Cadastro.ICategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <%--    <script type="text/javascript">
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
    </script>--%>
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
            <div class="container">
                <br />
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Cadastro de Categorias</div>
                    <asp:TextBox ID="TextId" runat="server" CssClass="form-control input-lg" Visible="false"
                        Width="40px"></asp:TextBox>
                    <div>
                        
                            Número Categoria.:<br />
                            <asp:TextBox ID="TextNumeroCategoria" runat="server" CssClass="form-control input-lg"
                                Width="169px"></asp:TextBox>
                        
                        <br />
                        
                            Descrição.:<br />
                            <asp:TextBox ID="TextDescricaoCategoria" runat="server" CssClass="form-control input-lg"
                                Width="400px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="TextDescrCategoriaRequired" runat="server" ControlToValidate="TextDescricaoCategoria"
                                CssClass="failureNotification" ErrorMessage="Campo Obrigatório Descrição." ToolTip="Campo Obrigatório."
                                ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                        
                        <div runat="server">
                            
                                Esporte.:<br />
                                <asp:DropDownList ID="DropDownListEsporte" CssClass="form-control" Height="40px"
                                    Width="280px" runat="server" AutoPostBack="True" SelectedIndexChanged="SelecionaTPCompeticao">
                                    <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                                </asp:DropDownList>
                            
                            
                                Tipo Categoria.:<br />
                                <asp:DropDownList CssClass="form-control" ID="DropDownListTpCategoria" runat="server"
                                    Height="40px" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="SelecionaTPCompeticao">
                                    <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                                    <asp:ListItem Value="1">Individual</asp:ListItem>
                                    <asp:ListItem Value="2">Equipe</asp:ListItem>
                                </asp:DropDownList>
                            
                            
                                Tipo Competição.:<br />
                                <asp:DropDownList CssClass="form-control" ID="DropDownTpCompeticao" runat="server"
                                    Height="40px" Width="180px">
                                    <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                                </asp:DropDownList>
                            
                        </div>
                    </div>
                    
                        Genero.:<br />
                        <asp:RadioButtonList ID="RBtnListSexo" runat="server">
                            <asp:ListItem Value="M" Text="Masculino"></asp:ListItem>
                            <asp:ListItem Value="F" Text="Feminino"></asp:ListItem>
                            <asp:ListItem Value="A" Text="Masculino e Feminino"></asp:ListItem>
                        </asp:RadioButtonList>
                    
                    
                        Altura Inicial.:
                        <asp:TextBox ID="TextAlturaInicial" runat="server" Width="100px" CssClass="form-control input-lg"></asp:TextBox>
                    
                    
                        Altura Final.:
                        <asp:TextBox ID="TextAlturaFinal" runat="server" Width="100px" CssClass="form-control input-lg"></asp:TextBox>
                    
                    
                        Peso Inicial .:
                        <asp:TextBox ID="TextPesoInicial" runat="server" Width="150px" CssClass="form-control input-lg"></asp:TextBox>
                    
                    
                        Peso Final .:
                        <asp:TextBox ID="TextPesoFinal" runat="server" Width="150px" CssClass="form-control input-lg"></asp:TextBox>
                    
                    
                        Idade Inicial .:
                        <asp:TextBox ID="TextIdadeInicial" runat="server" Width="150px" CssClass="form-control input-lg"></asp:TextBox>
                    
                    
                        Idade Final .:
                        <asp:TextBox ID="TextIdadeFinal" runat="server" Width="150px" CssClass="form-control input-lg"></asp:TextBox>
                    
                    
                        Graduação Inicial.:<br />
                        <asp:DropDownList CssClass="form-control" ID="DropDownListGraduacaoInicial" runat="server"
                            Height="40px" Width="380px">
                            <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                        </asp:DropDownList>
                    
                    
                        Graduação Final.:<br />
                        <asp:DropDownList CssClass="form-control" ID="DropDownListGraduacaoFinal" runat="server"
                            Height="40px" Width="380px">
                            <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                        </asp:DropDownList>
                    
                    
                        Status Categoria.:
                        <asp:CheckBox ID="CheckStatus" runat="server" Text="Ativo" />
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
                        Esporte.:<br />
                        <asp:DropDownList ID="DropDownListEsporte2" CssClass="form-control" Height="40px" Width="280px"
                            runat="server" >
                            <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                        </asp:DropDownList><br />
                        <asp:Button ID="BtnConsulta" class="btn btn-info" runat="server" CommandName="Consulta"
                            Text="Consultar" OnClick="ConsultarClick" />
                    
                </div>
                <br />
                <asp:GridView CssClass="table table-bordered bs-table" ID="GridCategoria" runat="server"
                    AutoGenerateColumns="False" Width="100%" DataKeyNames="IdCategoria" OnRowCommand="GridCategoriaCommand"
                    OnPageIndexChanging="GrdCategoriaPageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="IdCategoria">
                            <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="N° Categoria" DataField="NumeroCategoria">
                            <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Descrição" DataField="DescricaoCategoria">
                            <HeaderStyle HorizontalAlign="Center" Height="50%" BackColor="ControlLight" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Tipo" DataField="StringtpCompeticao">
                            <HeaderStyle HorizontalAlign="Center" Height="50%" BackColor="ControlLight" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Sexo" DataField="SexoCategoria">
                            <HeaderStyle HorizontalAlign="Center" Height="50%" BackColor="ControlLight" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Esporte" DataField="NmEsporte">
                            <HeaderStyle HorizontalAlign="Center" Height="50%" BackColor="ControlLight" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ICompeticao.aspx.cs" Inherits="SisKiai.Cadastro.ICompeticao" %>

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
        function Cancelar_onclick() {

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
            <div>
                <asp:Menu ID="Menu1" CssClass="Menu1" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
                    OnMenuItemClick="Menu1_MenuItemClick" RenderingMode="Table" BackColor="#F7F6F3"
                    DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="1.0em" ForeColor="#7C6F57"
                    StaticSubMenuIndent="10px">
                    <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <DynamicMenuStyle BackColor="#F7F6F3" />
                    <DynamicSelectedStyle BackColor="#5D7B9D" />
                    <Items>
                        <asp:MenuItem Text="|Cadastro de Competição|  " Value="0"></asp:MenuItem>
                        <asp:MenuItem Text="|Cadastrar Categorias da Competição|  " Value="1"></asp:MenuItem>
                        <asp:MenuItem Text="|Lista de Competições|  " Value="2"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticSelectedStyle BackColor="#5D7B9D" />
                </asp:Menu>
            </div>
            <div class="container" id="ParentDiv">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="Tab1" runat="server">
                        <br />
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                Cadastro de Competição</div>
                            <asp:TextBox ID="TextId" runat="server" CssClass="form-control input-lg" Visible="false"
                                Width="40px"></asp:TextBox>
                            <div>
                                Esporte.:<br />
                                <asp:DropDownList CssClass="form-control" ID="DropDownListEsporte" runat="server"
                                    Height="40px" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="DropDownListEsporteChange">
                                    <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div>
                                Nome.:<br />
                                <asp:TextBox ID="TextNomeCompeticao" runat="server" CssClass="form-control input-lg"
                                    Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="TextNomeCompeticaoRequired" runat="server" ControlToValidate="TextNomeCompeticao"
                                    CssClass="failureNotification" ErrorMessage="Campo Obrigatório Nome." ToolTip="Campo Obrigatório."
                                    ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                            </div>
                            <div>
                                Descrição.:<br />
                                <asp:TextBox ID="TextDescricao" runat="server" CssClass="form-control input-lg" Width="400px"></asp:TextBox>
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        Endereço.:<br />
                                        <asp:TextBox ID="TextEndereco" runat="server" CssClass="form-control input-lg" Width="400px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Cidade.:<br />
                                        <asp:DropDownList CssClass="form-control" ID="DropDownListCidade" runat="server"
                                            Height="40px" Width="200px" OnSelectedIndexChanged="BuscaCep" AutoPostBack="true">
                                            <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Cep.:<br />
                                        <asp:TextBox ID="TextCep" runat="server" CssClass="form-control input-lg" Width="166px"
                                            Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            Responsável.:<br />
                            <asp:TextBox ID="TextResponsavel" runat="server" CssClass="form-control input-lg"
                                Width="400px"></asp:TextBox>
                            <div>
                                Telefone.:<br />
                                <asp:TextBox ID="TextTelefone" runat="server" CssClass="form-control input-lg" Width="250px"></asp:TextBox>
                            </div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div>
                                            Data Competição.:<br />
                                            <asp:TextBox ID="TextDtCompeticao" runat="server" CssClass="form-control input-lg"
                                                Width="150px"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextDtCompeticao"
                                                Mask="99/99/9999" MessageValidatorTip="true" OnInvalidCssClass="MaskedEditError"
                                                MaskType="Number" ClearMaskOnLostFocus="false" />
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            Data Limite Inscrição.:<br />
                                            <asp:TextBox ID="TextDtLimite" runat="server" CssClass="form-control input-lg" Width="150px"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextDtLimite"
                                                Mask="99/99/9999" MessageValidatorTip="true" OnInvalidCssClass="MaskedEditError"
                                                MaskType="Number" ClearMaskOnLostFocus="false" />
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            Valor Inscrição.:<br />
                                            <asp:TextBox ID="TextValor" runat="server" CssClass="form-control input-lg" Width="100px"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            Status
                                            <asp:CheckBox ID="CheckBoxStatus" runat="server" Text="Ativo" Checked="true" />
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            Permite Não Filiados
                                            <asp:RadioButtonList ID="RBtnListPermiteNaoFiliado" runat="server">
                                                <asp:ListItem Text="SIM" Value="True" />
                                                <asp:ListItem Text="NÃO" Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <h2>
                                            Pontos para o Ranking</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Primeiro.:<br />
                                        <asp:TextBox ID="TextPrimeiro" runat="server" CssClass="form-control input-lg" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Segundo.:<br />
                                        <asp:TextBox ID="TextSegundo" runat="server" CssClass="form-control input-lg" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Terceiro.:<br />
                                        <asp:TextBox ID="TextTerceiro" runat="server" CssClass="form-control input-lg" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Quarto.:<br />
                                        <asp:TextBox ID="TextQuarto" runat="server" CssClass="form-control input-lg" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Quinto.:<br />
                                        <asp:TextBox ID="TextQuinto" runat="server" CssClass="form-control input-lg" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Participação.:<br />
                                        <asp:TextBox ID="TextParticipacao" runat="server" CssClass="form-control input-lg"
                                            Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <div>
                                            Competição Conta pontos p/ Ranking
                                            <asp:RadioButtonList ID="RadioButtonContaRanking" runat="server">
                                                <asp:ListItem Text="SIM" Value="True" />
                                                <asp:ListItem Text="NÃO" Value="False" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <h2>
                                            Cartaz do Evento</h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <br />
                                        <asp:FileUpload ID="flUpload" runat="server" />
                                        <br />
                                        <asp:Label runat="server" ID="lblInfo" Text="" ForeColor="Red" />
                                        <br />
                                        <asp:Button ID="btnEnviarArquivo" runat="server" Text=" Carregar Cartaz " OnClick="btnUploadClick"
                                            CssClass="btn btn-default" />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <p class="submitButton">
                                <br />
                                <asp:Button CssClass="btn btn-success" ID="BtnSalvar" runat="server" CommandName="Salvar"
                                    Text="Salvar" ValidationGroup="ValidationGroup" OnClick="SalvarClick" />
                                <input id="Cancelar" type="reset" value="Cancelar" class="btn btn-danger" onclick="return Cancelar_onclick()" />
                            </p>
                        </div>
                    </asp:View>
                    <asp:View ID="Tab2" runat="server">
                        <h3>
                            Adicionar Categorias</h3>
                        <br />
                        <h2>
                            <asp:Literal ID="LiteralCompeticao" runat="server"></asp:Literal></h2>
                        <hr width="100%" align="left" />
                        <br />
                        <a href="#" onclick="javascript: exibe('conteudo');">Adicionar/Categorias</a><br />
                        <br />
                        <div id="conteudo">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:ListBox ID="ListBoxCategoriasDisponiveis" runat="server" Height="400px" Width="450px">
                                        </asp:ListBox>
                                    </td>
                                    <td>
                                        <div>
                                            <center>
                                                <asp:Button ID="BtnAdiciona" runat="server" CommandName="Adiciona" Height="35px"
                                                    OnClick="AdicionaClick" Text="Adiciona" Width="130px" CssClass="btn btn-success" />
                                                <br />
                                                <br />
                                                <asp:Button ID="BtnTodos" runat="server" CommandName="AdicionaTodos" Height="35px"
                                                    OnClick="AdicionaTodosClick" Text="Adiciona Todos" Width="130px" CssClass="btn btn-success" />
                                                <br />
                                                <br />
                                                <asp:Button ID="BtnRemove" runat="server" CommandName="Remove" Height="35px" OnClick="RemoveClick"
                                                    Text="Remove" Width="130px" CssClass="btn btn-danger" />
                                                <br />
                                                <br />
                                                <asp:Button ID="BtnRemoveTodos" runat="server" CommandName="RemoveTodos" Height="35px"
                                                    OnClick="RemoveTodosClick" Text="Remove Todos" Width="130px" CssClass="btn btn-danger" />
                                            </center>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:ListBox ID="ListBoxCategoriasLiberadas" runat="server" Height="400px" Width="450px">
                                        </asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:View>
                    <asp:View ID="Tab3" runat="server">
                        <h3>
                            Competições cadastradas</h3>
                        <hr width="100%" align="left" />
                        <br />
                        <div>
                            Consulta.:
                            <asp:TextBox ID="TextConsulta" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox>
                            <br />
                            <asp:Button ID="BtnConsulta" class="btn btn-info" runat="server" CommandName="Consulta"
                                OnClick="ConsultarClick" Text="Consultar" />
                        </div>
                        <div>
                            <asp:GridView CssClass="table table-bordered bs-table" ID="GridViewCompeticoes" runat="server"
                                AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IdCompeticao" OnPageIndexChanging="GrdCompeticoesPageIndexChanging"
                                OnRowCommand="GridViewCompeticoesCommand" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="IdCompeticao" HeaderText="ID">
                                        <HeaderStyle Height="10%" HorizontalAlign="Center" BackColor="ControlLight" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeCompeticao" HeaderText="Nome">
                                        <HeaderStyle Height="30%" HorizontalAlign="Center" BackColor="ControlLight" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeResponsavel" HeaderText="Nome Reponsável">
                                        <HeaderStyle Height="30%" HorizontalAlign="Center" BackColor="ControlLight" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TelefoneResponsavel" HeaderText="Telefone">
                                        <HeaderStyle Height="20%" HorizontalAlign="Center" BackColor="ControlLight" />
                                    </asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/Imagens/edit.gif"
                                        Text="Editar">
                                        <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/Imagens/delete.gif"
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
                    </asp:View>
                </asp:MultiView>
                </p>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnEnviarArquivo" runat="server" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

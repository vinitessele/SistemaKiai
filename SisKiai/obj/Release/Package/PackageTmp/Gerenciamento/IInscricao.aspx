<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IInscricao.aspx.cs" Inherits="SisKiai.Gerenciamento.IInscricao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript">
        function cont() {
            //pega o Html da DIV
            var divElements = document.getElementById('print').innerHTML;
            //pega o HTML de toda tag Body
            var oldPage = document.body.innerHTML;
            //Alterna o body
            document.body.innerHTML =
          "<html><head>  <title></title> </head> <body>  " + divElements + "</body>";
            //Imprime o body atual
            window.print();
            //Retorna o conteudo original da página.
            document.body.innerHTML = oldPage;
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
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
            <div class="main" id="ParentDiv">
                <hr width="100%" align="left" />
                <asp:Menu CssClass="Menu1" ID="Menu1" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
                    OnMenuItemClick="Menu1_MenuItemClick" RenderingMode="Table" StaticSelectedStyle-CssClass="SelectTab">
                    <Items>
                        <asp:MenuItem Text="| Inscrições |" Value="0" Selected="true"></asp:MenuItem>
                        <asp:MenuItem Text="|  Dados Associação-Academia cadastrados |" Value="1" Enabled="false">
                        </asp:MenuItem>
                        <asp:MenuItem Text="|  Atletas |" Value="2" Enabled="false"></asp:MenuItem>
                        <asp:MenuItem Text="|  Categorias |" Value="3" Enabled="false"></asp:MenuItem>
                    </Items>
                </asp:Menu>
                <hr width="100%" align="left" />
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="Tab1" runat="server">
                        <br />
                        <legend>Escolha a competição que deseja inscrever seus Atletas.</legend>&nbsp;<div>
                            <div id="DivCompeticoes" runat="server">
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                <asp:GridView CssClass="table table-bordered bs-table" ID="GridCompeicoes" runat="server"
                                    AutoGenerateColumns="False" DataKeyNames="IdCompeticao" OnPageIndexChanging="GridCompeicoesPageIndexChanging"
                                    OnRowCommand="GridCompeicoesCommand" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="IdCompeticao" HeaderText="ID">
                                            <HeaderStyle BackColor="ControlLight" Height="5%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NomeCompeticao" HeaderText="Competição">
                                            <HeaderStyle BackColor="ControlLight" Height="30%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NomeCidade" HeaderText="Cidade">
                                            <HeaderStyle BackColor="ControlLight" Height="20%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NomeResponsavel" HeaderText="Responsável">
                                            <HeaderStyle BackColor="ControlLight" Height="20%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TelefoneResponsavel" HeaderText="Telefone">
                                            <HeaderStyle BackColor="ControlLight" Height="20%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DataCompeticao" HeaderText="Dt Competição" DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle BackColor="ControlLight" Height="10%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DataLimiteInscricao" HeaderText="Dt Limite" DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle BackColor="ControlLight" Height="5%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PermiteNaoFiliadoDescricao" HeaderText="Informação">
                                            <HeaderStyle BackColor="ControlLight" Height="5%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ValorCompeticao" HeaderText="Valor">
                                            <HeaderStyle BackColor="ControlLight" Height="5%" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:ButtonField ButtonType="Image" CommandName="Inscrever" ImageUrl="~/Imagens/add.gif"
                                            Text="Inscrever">
                                            <HeaderStyle BackColor="ControlLight" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:TemplateField HeaderText="Imagem">
                                            <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imagem" ImageUrl='<%# Eval("UrlImagem") %>' runat="server" Width="80"
                                                    Height="80" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Position="Bottom" Mode="NextPreviousFirstLast" PreviousPageText="|Anterior|"
                                        NextPageText="|Próxima|" FirstPageText="|Primeira Página|" LastPageText="|Última Página|"
                                        PageButtonCount="50" />
                                </asp:GridView>
                            </div>
                            <br />
                        </div>
                    </asp:View>
                    <asp:View ID="Tab2" runat="server">
                        <asp:Panel Enabled="false" runat="server" ID="PainelInscricao">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h4>
                                        Dados para Inscrição</h4>
                                </div>
                                <div>
                                    <br />
                                    <h4>
                                        <asp:Label ID="LabelCampeonato" runat="server" Text="" CssClass="bold"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text=" - " CssClass="bold"></asp:Label>&nbsp
                                        <asp:Label ID="LabelCidade" runat="server" Text="" CssClass="bold"></asp:Label>&nbsp
                                        <asp:Label ID="Label2" runat="server" Text="Data Limite Inscrição.:"></asp:Label>&nbsp
                                        <asp:Label ID="LabelDtLimite" runat="server" Text="" CssClass="bold"></asp:Label>&nbsp
                                        <asp:Label ID="Label1" runat="server" Text="Data Competição.:"></asp:Label>&nbsp
                                        <asp:Label ID="LabelDtCompeticao" runat="server" Text="" CssClass="bold"></asp:Label>&nbsp
                                    </h4>
                                    <br />
                                    <br />
                                </div>
                                <div>
                                    Id
                                    <br />
                                    <asp:TextBox ID="TextIdAssociacao" runat="server" CssClass="form-control input-lg"
                                        Width="70px" Enabled="false"></asp:TextBox>
                                    Nome Associação.:<asp:TextBox ID="TextNomeAssociacao" runat="server" CssClass="form-control input-lg"
                                        Width="700px" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="TextNomeAssociacaoRequired" runat="server" ControlToValidate="TextNomeAssociacao"
                                        CssClass="failureNotification" ErrorMessage="Campo Obrigatório Nome." ToolTip="Campo Obrigatório."
                                        ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                Data.:<br />
                                                <asp:TextBox ID="TextData" runat="server" CssClass="form-control input-lg" Width="150px"
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                Login.:<br />
                                                <asp:TextBox ID="TextLogin" runat="server" CssClass="form-control input-lg" Width="200px"
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                Valor Total.:<br />
                                                <asp:TextBox ID="TextValorTotal" runat="server" CssClass="form-control input-lg"
                                                    Width="100px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                Status.:<br />
                                                <asp:RadioButtonList ID="RBtnListStatus" runat="server" Enabled="false" RepeatDirection="Vertical">
                                                    <asp:ListItem Text="Liberado" Value="True" />
                                                    <asp:ListItem Text="Pendente" Value="False" Selected="True" />
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Button ID="btnProximo" runat="server" Text=" Próximo " OnClick="btnProximoClick"
                                        CssClass="btn btn-success" />
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:View>
                    <asp:View ID="Tab3" runat="server">
                        <asp:Panel Enabled="false" runat="server" ID="PanelInscricaoAtleta">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h4>
                                        Inscrição</h4>
                                </div>
                                <br />
                                <b>Permite não Filiados.:</b><br />
                                <asp:Label runat="server" ID="Permite" Text="" ForeColor="Red" Font-Bold="true" />
                                <br />
                                <hr width="100%" align="left" />
                                <br />
                                <b>Tipo de Inscrição.:</b><br />
                                <asp:RadioButtonList ID="RadioTipoCompeticao" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="SelectTipoInscricaoClick" RepeatDirection="Vertical">
                                    <asp:ListItem Text="Individual" Value="IND" Selected="True" />
                                    <asp:ListItem Text="Equipe" Value="EQP" />
                                </asp:RadioButtonList>
                                <br />
                                <hr width="100%" align="left" />
                                <br />
                                <h3>
                                    Sua Associação/Academia possui atletas Cadastrados.:
                                </h3>
                                <h4>
                                    Selecione o nome do atleta e clique em Adicionar</h4>
                                <br />
                                <asp:ListBox ID="ListAtletasCadastrados" runat="server" Height="500px" Width="900px">
                                </asp:ListBox>
                                <br />
                                <a href="#Categorias">
                                    <asp:Button ID="btnSeleciona" runat="server" Text=" Selecionar " OnClick="btnSelecionaClick"
                                        CssClass="btn btn-info" Width="845px" />
                                </a><a name="Categorias"></a>
                                <br />
                            </div>
                        </asp:Panel>
                    </asp:View>
                    <asp:View ID="Tab4" runat="server">
                        <asp:Panel Enabled="false" runat="server" ID="PanelContinuacaoInscricaoAtleta">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h4>
                                        Dados do Atleta/Equipe</h4>
                                </div>
                                N° Registro.:<br />
                                <asp:TextBox ID="TextNumeroRegistro" runat="server" CssClass="form-control input-lg"
                                    Width="116px" Enabled="true" AutoPostBack="true" OnTextChanged="BuscaFiliadoPorRegistro"></asp:TextBox>
                                <asp:TextBox ID="TextIdAtleta" runat="server" Enabled="false" Visible="false" Width="16px"
                                    CssClass="form-control input-lg" OnTextChanged="BuscaFiliadoPorId"></asp:TextBox>
                                Nome.:
                                <asp:RequiredFieldValidator ID="TextNomeFiliadoRequired" runat="server" ControlToValidate="TextNomeFiliado"
                                    CssClass="failureNotification" ErrorMessage="Campo Obrigatório Nome." ToolTip="Campo Obrigatório."
                                    ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:TextBox ID="TextNomeFiliado" runat="server" CssClass="form-control input-lg"
                                    Width="400px"></asp:TextBox>
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            Data Nascimento.:<asp:RequiredFieldValidator ID="TextDtNascimentoValidator1" runat="server"
                                                ControlToValidate="TextDtNascimento" CssClass="failureNotification" ErrorMessage="Campo Obrigatório Data Nascimento."
                                                ToolTip="Campo Obrigatório." ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                                            <br />
                                            <asp:TextBox ID="TextDtNascimento" runat="server" CssClass="form-control input-lg"
                                                Width="180px" OnTextChanged="CalcularIdade" AutoPostBack="true"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextDtNascimento"
                                                Mask="99/99/9999" MessageValidatorTip="true" OnInvalidCssClass="MaskedEditError"
                                                MaskType="Number" ClearMaskOnLostFocus="false" />
                                        </td>
                                        <td>
                                            Idade.:<br />
                                            <asp:TextBox ID="TextIdade" runat="server" CssClass="form-control input-lg" Width="150px"
                                                Enabled="False"></asp:TextBox>
                                        </td>
                                        <td>
                                            Peso.:<br />
                                            <asp:TextBox ID="TextPeso" runat="server" CssClass="form-control input-lg" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            Altura.:<br />
                                            <asp:TextBox ID="TextAltura" runat="server" CssClass="form-control input-lg" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            Graduação.:<br />
                                            <asp:DropDownList CssClass="form-control" ID="DropDownListGraduacao" runat="server"
                                                Height="40px" Width="210px" AutoPostBack="true" 
                                                onselectedindexchanged="DropDownListGraduacaoSelect">
                                                <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Gênero.:<br />
                                            <asp:RadioButtonList ID="RBtnListSexo" runat="server" AutoPostBack="true" RepeatDirection="Vertical" OnSelectedIndexChanged="SelectCategoriaClick">
                                                <asp:ListItem Text="Masculino" Value="M" />
                                                <asp:ListItem Text="Feminino" Value="F" />
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h4>
                                        Categorias Encontradas.:</h4>
                                </div>
                                <div>
                                    <span class="failureNotification">
                                        <asp:Literal ID="LiteralMensagem" runat="server"></asp:Literal>
                                    </span>
                                    <asp:CheckBoxList ID="CheckBoxListCategorias" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                 
                                <asp:Button ID="BtnConfirmaInscricao" runat="server" CommandName="ConfirmaInscricao"
                                    Enabled="false" OnClick="ConfirmaInscriAtletaClick" Text="Confirma Inscrição do Atleta"
                                    CssClass="btn btn-success" Width="448px" />
                                <asp:Button ID="BtnCancelamento" runat="server" CommandName="CancelaInscricao" Enabled="false"
                                    CssClass="btn btn-danger" OnClick="CancelaClick" 
                                    Text="Cancela Inscrição do Atleta" Width="371px" />
                                <br />
                   
                            <br />
                            <div id="print">
                                <h3>
                                    Lista de Atletas Inscritos para competição selecionada</h3>
                                <hr width="100%" align="left" />
                                <br />
                                <asp:GridView CssClass="table table-bordered bs-table" ID="GridAtletas" runat="server"
                                    AutoGenerateColumns="False" Width="100%" DataKeyNames="IdInscricaoAtleta" OnRowCommand="GridAtletasCommand"
                                    OnPageIndexChanging="GrdAtletaPageIndexChanging">
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="IdInscricaoAtleta">
                                            <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="N° Registro" DataField="NumeroRegistro">
                                            <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Nome" DataField="NomeAtleta">
                                            <HeaderStyle HorizontalAlign="Center" Height="50%" BackColor="ControlLight" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Categoria" DataField="NomeCategoria">
                                            <HeaderStyle HorizontalAlign="Center" Height="50%" BackColor="ControlLight" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Tipo" DataField="TipoCompeticao">
                                            <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
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
                                <br />
                                <input type="button" onclick="cont();" value="Imprimir Inscritos" class="btn btn-default">
                                <asp:Button ID="Button1" runat="server" Text=" Envia Email da Inscrição " OnClick="btnEnviaEmailClick"
                                    CssClass="btn btn-default" />
                            </div>
                            <br />
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

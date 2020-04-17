<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IFiliado.aspx.cs" Inherits="SisKiai.Cadastro.IFiliado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap.css" rel="stylesheet" type="text/css" />
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
            <div class="main" id="ParentDiv">
                <asp:Menu CssClass="Menu1" ID="Menu1" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
                    OnMenuItemClick="Menu1_MenuItemClick" RenderingMode="Table" StaticSelectedStyle-CssClass="SelectTab"
                    BackColor="#F7F6F3" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="1.0em"
                    ForeColor="#7C6F57" StaticSubMenuIndent="13px">
                    <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <DynamicMenuStyle BackColor="#F7F6F3" />
                    <DynamicSelectedStyle BackColor="#5D7B9D" />
                    <Items>
                        <asp:MenuItem Text="|-- Cadastro de Filiados " Value="0" Selectable="true"></asp:MenuItem>
                        <asp:MenuItem Text="|-- Relação de Filiados  " Value="1"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticSelectedStyle BackColor="#5D7B9D" CssClass="SelectTab" />
                </asp:Menu>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="Tab1" runat="server">
                        <br />
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                Cadastro de Atletas/Filiados</div>
                            <pre class="bg bg-primary">
                                Peso e Altura dos atletas devem ser utilizados vírgula (,) e não ponto(.)</pre>
                            <asp:TextBox ID="TextId" runat="server" CssClass="form-control input-lg" Visible="false"
                                Width="40px"></asp:TextBox>
                            <div>
                                N° Registro.:<br />
                                <asp:TextBox ID="TextNumeroRegistro" runat="server" CssClass="form-control input-lg"
                                    Width="100px" Enabled="false"></asp:TextBox>
                            </div>
                            <table>
                                <tr>
                                    <td>
                                        Nome.:<asp:RequiredFieldValidator ID="TextNomeFiliadoRequired" runat="server" ControlToValidate="TextNomeFiliado"
                                            CssClass="failureNotification" ErrorMessage="Campo Obrigatório Nome." ToolTip="Campo Obrigatório."
                                            ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                                        <br />
                                        <asp:TextBox ID="TextNomeFiliado" runat="server" CssClass="form-control input-lg"
                                            Width="400px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Endereço.:<asp:RequiredFieldValidator ID="TextEnderecoValidator1" runat="server"
                                            ControlToValidate="TextEndereco" CssClass="failureNotification" ErrorMessage="Campo Obrigatório Endereço."
                                            ToolTip="Campo Obrigatório." ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                                        <br />
                                        <asp:TextBox ID="TextEndereco" runat="server" CssClass="form-control input-lg" Width="400px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Cidade.:<br />
                                        <asp:DropDownList CssClass="form-control" ID="DropDownListCidade" runat="server"
                                            Height="40px" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="BuscaCep">
                                            <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        CEP.:<br />
                                        <asp:TextBox ID="TextCep" runat="server" CssClass="form-control input-lg" Width="150px"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
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
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        E-Mail.:<br />
                                        <asp:TextBox ID="TextEmail" runat="server" CssClass="form-control input-lg" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        RG.:<br />
                                        <asp:TextBox ID="TextRG" runat="server" CssClass="form-control input-lg" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        CPF.:<br />
                                        <asp:TextBox ID="TextCPF" runat="server" CssClass="form-control input-lg" Width="200px"
                                            OnTextChanged="TextValidaCpf"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <h3>
                                Caracteristicas</h3>
                            <table>
                                <tr>
                                    <td>
                                        Dt Nasc.:<asp:RequiredFieldValidator ID="TextDtNascimentoValidator1" runat="server"
                                            ControlToValidate="TextDtNascimento" CssClass="failureNotification" ErrorMessage="Campo Obrigatório Data Nascimento."
                                            ToolTip="Campo Obrigatório." ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="TextDtNascimento" runat="server" CssClass="form-control input-lg"
                                            Width="150px" OnTextChanged="CalcularIdade" AutoPostBack="true"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender runat="server" TargetControlID="TextDtNascimento"
                                            Mask="99/99/9999" MessageValidatorTip="true" OnInvalidCssClass="MaskedEditError"
                                            MaskType="Number" ClearMaskOnLostFocus="false" />
                                    </td>
                                    <td>
                                        Idade.:<br />
                                        <asp:TextBox ID="TextIdade" runat="server" CssClass="form-control input-lg" Width="80px"
                                            Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        Peso.:<br />
                                        <asp:TextBox ID="TextPeso" runat="server" CssClass="form-control input-lg" Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        Altura.:<br />
                                        <asp:TextBox ID="TextAltura" runat="server" CssClass="form-control input-lg" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            Sexo.:<br />
                            <asp:RadioButtonList ID="RBtnListSexo" runat="server">
                                <asp:ListItem Text="Masculino" Value="M" />
                                <asp:ListItem Text="Feminino" Value="F" />
                            </asp:RadioButtonList>
                            <br />
                            Status.:<br />
                            <asp:RadioButtonList ID="RBtnListStatus" runat="server" Enabled="false">
                                <asp:ListItem Text="Ativo" Value="A" />
                                <asp:ListItem Text="Pendente" Value="P" Selected="True" />
                                <asp:ListItem Text="Inativo" Value="I" />
                            </asp:RadioButtonList>
                            <h3>
                                Graduações</h3>
                            Graduação.:<br />
                            <asp:DropDownList CssClass="form-control" ID="DropDownListGraduacao" runat="server"
                                Height="40px" Width="200px">
                                <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            Data Graduação
                            <asp:TextBox ID="TextDataGraduacao" runat="server" CssClass="form-control input-lg"
                                Width="150px"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextDataGraduacao"
                                Mask="99/99/9999" MessageValidatorTip="true" OnInvalidCssClass="MaskedEditError"
                                MaskType="Number" ClearMaskOnLostFocus="false" />
                            <br />
                            Status.:<br />
                            <asp:RadioButtonList ID="RBtnGraduacao" runat="server" TextAlign="Right">
                                <asp:ListItem Text="Atual" Value="A" Selected />
                                <asp:ListItem Text="Inativo" Value="I" />
                            </asp:RadioButtonList>
                            <br />
                            <asp:TextBox ID="TextIdGraduacao" runat="server" CssClass="form-control input-lg"
                                Visible="false" Width="40px"></asp:TextBox>
                            <br />
                            <h3>
                                Foto</h3>
                            <asp:FileUpload ID="flUpload" runat="server" />
                            <br />
                            <asp:Label runat="server" ID="lblInfo" Text="" ForeColor="Red" />
                            <br />
                            <br />
                            <asp:Button CssClass="btn btn-default" ID="btnEnviarArquivo" runat="server" Text=" Carregar Fotos "
                                OnClick="btnUpload_Click" Height="26px" Width="155px" />
                            <br />
                            <br />
                            <br />
                            <div style="height: 48px">
                                Associação/Academia/Clube.:<br />
                                <asp:DropDownList CssClass="form-control" ID="DropDownListAssociacao" runat="server"
                                    Height="40px" Width="400px">
                                    <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </div>
                            <br />
                            <br />
                            <p class="submitButton">
                                <asp:Button CssClass="btn btn-success" ID="BtnSalvar" runat="server" CommandName="Salvar"
                                    Text="Salvar" ValidationGroup="ValidationGroup" OnClick="SalvarClick" />
                                <input id="Cancelar" type="reset" value="Cancelar" class="btn btn-danger" />
                            </p>
                        </div>
                    </asp:View>
                    <asp:View ID="Tab2" runat="server">
                        <div>
                            Consulta.:
                            <asp:TextBox ID="TextConsulta" runat="server" CssClass="form-control input-lg" Width="500px"></asp:TextBox><br />
                            &nbsp;<asp:Button ID="BtnConsulta" class="btn btn-info" runat="server" CommandName="Consulta"
                                Text="Consultar" OnClick="ConsultarClick" />
                            <br />
                            <hr width="100%" align="left" />
                        </div>
                        <br />
                        <br />
                        Ordem.:<br />
                        <asp:RadioButtonList ID="RBListOrdem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OrdemClick">
                            <asp:ListItem Selected="True" Text="Alfabética" Value="0">
                            </asp:ListItem>
                            <asp:ListItem Text="Número de Registro" Value="1">
                            </asp:ListItem>
                            <asp:ListItem Text="Ordem de Cadastro" Value="2">
                            </asp:ListItem>
                        </asp:RadioButtonList>
                        Status.:<br />
                        <asp:RadioButtonList ID="RBListStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="StatusClick">
                            <asp:ListItem Selected="True" Text="Todos" Value="T">
                            </asp:ListItem>
                            <asp:ListItem Text="Ativos" Value="A">
                            </asp:ListItem>
                            <asp:ListItem Text="Inativos" Value="I">
                            </asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                        <br />
                        <div id="print">
                            <asp:GridView CssClass="table table-bordered bs-table" ID="GridFiliado" runat="server"
                                AutoGenerateColumns="False" Width="100%" DataKeyNames="IdFiliado" OnRowCommand="GridFiliadoCommand"
                                OnPageIndexChanging="GrdFiliadoPageIndexChanging" OnRowDataBound="GridFiliadoDataBound"
                                AllowPaging="True" PageSize="50">
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="IdFiliado" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="N° Registro" DataField="NumeroRegistro">
                                        <HeaderStyle BackColor="ControlLight" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nome" DataField="NomeFiliado">
                                        <HeaderStyle HorizontalAlign="Center" Height="30%" BackColor="ControlLight" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Associação" DataField="NomeAssociacao">
                                        <HeaderStyle HorizontalAlign="Center" Height="20%" BackColor="ControlLight" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Email" DataField="EmailFiliado">
                                        <HeaderStyle HorizontalAlign="Center" Height="20%" BackColor="ControlLight" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Idade" DataField="IdadeFiliado">
                                        <HeaderStyle HorizontalAlign="Center" Height="20%" BackColor="ControlLight" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagens/edit.gif" CommandName="Editar">
                                        <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagens/delete.gif" CommandName="Excluir">
                                        <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonField>
                                    <asp:TemplateField HeaderText="Imprimir">
                                        <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton CommandName="Imprimir" ID="carteirinha" runat="server" ImageUrl="~/imagens/printer.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Imagem">
                                        <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" runat="server" Height="80" Width="80" ImageUrl='<%# Eval("UrlImagem") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Position="Bottom" Mode="NextPreviousFirstLast" PreviousPageText="|Anterior|"
                                    NextPageText="|Próxima|" FirstPageText="|Primeira Página|" LastPageText="|Última Página|"
                                    PageButtonCount="50" />
                            </asp:GridView>
                            <input type="button" onclick="cont();" value="Imprimir" class="btn btn-default">
                        </div>
                    </asp:View>
                </asp:MultiView></div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnEnviarArquivo" runat="server" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

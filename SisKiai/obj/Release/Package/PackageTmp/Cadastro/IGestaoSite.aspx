<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IGestaoSite.aspx.cs" Inherits="SisKiai.Cadastro.GestaoSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/jscript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Styles/jquery.classyedit.css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/jscript" src="~/Scripts/jquery.classyedit.js"></script>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $(".classy-editor").ClassyEdit();
        });
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
                <asp:Menu ID="Menu1" CssClass="Menu1" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
                    OnMenuItemClick="Menu1_MenuItemClick" RenderingMode="Table" 
                    BackColor="#F7F6F3" DynamicHorizontalOffset="2" Font-Names="Verdana" 
                    Font-Size="0.8em" ForeColor="#7C6F57" StaticSubMenuIndent="10px">
                    <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
                    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <DynamicMenuStyle BackColor="#F7F6F3" />
                    <DynamicSelectedStyle BackColor="#5D7B9D" />
                    <Items>
                        <asp:MenuItem Text="|-- Mensagem do Presidente " Value="0"></asp:MenuItem>
                        <asp:MenuItem Text="|-- Imagens   " Value="1"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticSelectedStyle BackColor="#5D7B9D" />
                </asp:Menu>
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="Tab1" runat="server">
                        <br />
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                Gestão do Site</div>
                            
                                <asp:TextBox ID="Textid" runat="server" Width="47px" Enabled="false" Visible="false"></asp:TextBox><br />
                            
                            <br />
                            
                                Data Final publicação.:<br />
                                <asp:TextBox ID="TextDataFinal" runat="server" Width="150px" CssClass="form-control input-lg"></asp:TextBox><br />
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextDataFinal"
                                    Mask="99/99/9999" MessageValidatorTip="true" OnInvalidCssClass="MaskedEditError"
                                    MaskType="Number" ClearMaskOnLostFocus="false" />
                            
                            <br />
                            
                                Finalidade.:<br />
                                <asp:RadioButtonList ID="RbListFinalidade" runat="server">
                                    <asp:ListItem Value="T" Text="Todos" Selected="True">  </asp:ListItem>
                                    <asp:ListItem Value="F" Text="Filiados">  </asp:ListItem>
                                </asp:RadioButtonList>
                            
                            <br />
                            <div>
                                <textarea rows="4" cols="60" id="TextArea1" runat="server" class="form-control"></textarea>
                            </div>
                            <div>
                                <br />
                                <asp:Button ID="BtnSalvarMensagem" runat="server" Text="Salvar" OnClick="BtnSalvarClick"
                                    CssClass="btn btn-success" /><br />
                                <br />
                            </div>
                            <div>
                                <asp:GridView CssClass="table table-bordered bs-table" ID="GridViewMensagem" runat="server"
                                    AutoGenerateColumns="False" Width="100%" DataKeyNames="Id" OnRowCommand="GridMensagemCommand"
                                    AllowPaging="True" OnPageIndexChanging="GrdMensagemPageIndexChanging">
                                    <Columns>
                                        <asp:BoundField HeaderText="ID" DataField="Id">
                                            <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Mensagem" DataField="Mensagem">
                                            <HeaderStyle HorizontalAlign="Center" Height="80%" BackColor="ControlLight" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Finalidade" DataField="Finalidade">
                                            <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Data" DataField="DataLimite" DataFormatString="{0:dd/MM/yyyy}">
                                            <HeaderStyle HorizontalAlign="Center" Height="20%" BackColor="ControlLight" />
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
                            <br />
                            <br />
                        </div>
                    </asp:View>
                    <asp:View ID="Tab2" runat="server">
                        <br />
                        <h3>
                            Imagens</h3>
                        <hr width="100%" align="left" />
                        <asp:TextBox ID="TextIDImagem" runat="server" Width="47px" Visible="false" CssClass="form-control input-lg"></asp:TextBox><br />
                        
                            Mensagem.:<br />
                            <asp:TextBox ID="TextMensagem" runat="server" Width="629px" CssClass="form-control input-lg"></asp:TextBox><br />
                        
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <label class="btn btn-default btn-file">
                                        <asp:FileUpload ID="flUpload" runat="server" />
                                    
                                    <br />
                                    <asp:Label runat="server" ID="lblInfo" Text="" ForeColor="Red" />
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnEnviarArquivo" runat="server" Text=" Carregar Fotos " OnClick="btnUpload_Click"
                                        CssClass="btn btn-default" />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div>
                            <asp:GridView CssClass="table table-bordered bs-table" ID="GridViewImg" runat="server"
                                AutoGenerateColumns="False" Width="100%" DataKeyNames="Id" OnRowCommand="GridImagemCommand"
                                AllowPaging="True" OnPageIndexChanging="GrdImagemPageIndexChanging">
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="Id">
                                        <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Mensagem" DataField="Mensagem">
                                        <HeaderStyle HorizontalAlign="Center" Height="30%" BackColor="ControlLight" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Imagem">
                                        <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" runat="server" Height="80" Width="80" ImageUrl='<%# Eval("Url") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnEnviarArquivo" runat="server" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

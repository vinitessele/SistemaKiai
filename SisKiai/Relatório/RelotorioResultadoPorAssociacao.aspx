<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelotorioResultadoPorAssociacao.aspx.cs"
    Inherits="SisKiai.Relatorio.RelotorioResultadoPorAssociacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <a href="javascript:window.history.go(-1)">Voltar</a>
            <br />
            <br />
            <asp:Button ID="bntenviar" runat="server" OnClick="BntenviarClick" Text="Enviar E-mail"
                CssClass="btn-btn-info" />
            <asp:Panel ID="pnlPerson" runat="server">
                <center>
                    <h4>
                        ...::: Sistema de Competição Kiai :::...<br />
                        ...:::Impressão Resultado da Competição por Associação:::...</h4>
                </center>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Label" Font-Size="Medium"></asp:Label><br />
                    <asp:Label ID="Label2" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
                </p>
                <div>
                    <asp:GridView CssClass="table table-bordered bs-table"   ID="GridViewResultado" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="NumRegistro" GridLines="None">
                        <Columns>
                            <asp:BoundField HeaderText="N° Registro" DataField="NumRegistro">
                                <HeaderStyle HorizontalAlign="Center" Height="10%" BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nome" DataField="NomeFiliado">
                                <HeaderStyle HorizontalAlign="Center" Height="60%" BackColor="ControlLight" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Tipo" DataField="TipoCompeticao">
                                <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Resultado" DataField="ResultadoCompeticao">
                                <HeaderStyle HorizontalAlign="Center" BackColor="ControlLight" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultadoCompeticao.aspx.cs"
    Inherits="SisKiai.Relatorio.ResultadoCompeticao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>...:::Impressão Resultado Final Competição - SisKiai:::...</title>
</head>
<body>
    <script language="javascript" type="text/javascript">
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
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <a href="javascript:window.history.go(-1)">Voltar</a>
            <input  type="button" onclick="cont();" value="Imprimir">
            <div id="print">
                <center>
                    <h4>
                        ...::: Sistema de Competição Kiai :::...<br />
                        ...:::Impressão Resultado Final Competição:::...</h4>
                </center>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
                </p>
                <asp:GridView CssClass="table table-bordered bs-table"   ID="GridCategoria" runat="server" AutoGenerateColumns="false" 
                    DataKeyNames="IdCategoria,NrCategoria" OnRowDataBound="OnRowDataBound" GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="IdCategoria" HeaderText="Id" ReadOnly="true" Visible="false">
                            <HeaderStyle BackColor="ControlLight" />
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NrCategoria" HeaderText="Categoria" ReadOnly="true">
                            <HeaderStyle BackColor="ControlLight" Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Font-Size="Small" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipoCompeticao" HeaderText="Tipo" ReadOnly="true">
                            <HeaderStyle BackColor="ControlLight" Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Font-Size="Small" Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NomeCategoria" HeaderText="Nome Categoria" ReadOnly="true">
                            <HeaderStyle BackColor="ControlLight" Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Font-Size="Small" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QteAtletasCategoria" HeaderText="Qte" ReadOnly="true">
                            <HeaderStyle BackColor="ControlLight" Width="20px" />
                            <ItemStyle HorizontalAlign="Center" Font-Size="Small" Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <HeaderStyle BackColor="ControlLight"  Width="420px"  />
                            <ItemTemplate>
                                <asp:GridView CssClass="table table-bordered bs-table"   ID="GridAtletasCategoria" runat="server" AutoGenerateColumns="false"
                                    DataKeyNames="IdCategoriaAtleta" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="IdCategoriaAtleta" HeaderText="ID" InsertVisible="false"
                                            ReadOnly="true" Visible="false">
                                            <HeaderStyle BackColor="ControlLight" Width="20px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NumRegistro" HeaderText="Registro" InsertVisible="false" Visible="false"
                                            ReadOnly="true">
                                            <HeaderStyle BackColor="ControlLight" Width="10px" />
                                            <ItemStyle HorizontalAlign="Center" Font-Size="Small" Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NomeFiliado" HeaderText="Nome Atleta" InsertVisible="false"
                                            ReadOnly="true">
                                            <HeaderStyle BackColor="ControlLight" Width="350px" />
                                            <ItemStyle HorizontalAlign="Center" Font-Size="Small" Width="350px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SiglaAssociacao" HeaderText="Associação" InsertVisible="false"
                                            ReadOnly="true">
                                            <HeaderStyle BackColor="ControlLight" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" Font-Size="Small" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ResultadoCompeticao" HeaderText="Resultado">
                                            <HeaderStyle BackColor="ControlLight" Width="10px" />
                                            <ItemStyle HorizontalAlign="Center" Font-Size="Small" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

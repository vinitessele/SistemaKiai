<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rel.aspx.cs" Inherits="SisKiai.Relatorio.Rel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onselectstart="return false">
    <form id="form1" runat="server">
    <style>
        body
        {
            margin: 0px;
            background-color: write;
            font-family: "Trebuchet MS" , Tahoma, Arial, Verdana;
            font-size: 12px;
            color: #000;
            height: 262px;
        }
        #todo
        {
            position: relative;
            border: 1px solid black;
            margin: 1px auto;
            background-color: white;
            top: 0px;
            left: 0px;
            height: 269px;
            width: 1028px;
        }
        
        #topo
        {
            -webkit-print-color-adjust:exact;
            position: relative;
            height: 19px;
            border: 2px solid #088A08;
            background-color: #00922f !important;
            color: White;
            top: -1px;
            left: -1px;
            width: 512px;
        }
        #meio
        {
            position: relative;
            width: 1017px;
            min-height: 1;
            background-color: write;
            top: 3px;
            left: 4px;
            height: 238px;
        }
        #rodape
        {
            width: 99%;
            height: 23px;
            background: #088A08;
        }
        
        #esquerda
        {
            float: left;
            width: 119px;
            min-height: 500px;
            height: 76px;
        }
        #direita
        {
            float: right;
            width: 494px;
            min-height: 400px;
            height: 235px;
            margin-left: 4px;
        }
        #Centrodireita
        {
            float: right;
            width: 317px;
            min-height: 400px;
            height: 216px;
        }
        #CNEKI
        {
            float: left;
            width: 317px;
            min-height: 400px;
            height: 88px;
        }
        #IKU
        {
            float: right;
            width: 317px;
            min-height: 400px;
            height: 92px;
        }
        #Centro
        {
            float: left;
            width: 317px;
            min-height: 400px;
            height: 215px;
        }
        #miolo
        {
            float: left;
            min-height: 400px;
            margin-right: 0px;
            height: 238px;
            text-align: center;
            width: 395px;
        }
        #menu
        {
            width: 96px;
            height: 448px;
            margin-right: 0px;
        }
        .itemMenu
        {
            width: 98%;
            height: 21px;
            background-color: white;
            padding: 3px 0px 0px 10px;
            border-bottom: 0px solid write;
        }
        #logo
        {
            float: left;
            width: 200px;
            height: 200px;
            text-align: center;
            background-color: write;
            margin-right: 20px;
        }
        
        .secao_miolo
        {
            text-align: center;
            background-color: White;
            margin: 2px 5px 4px 5px;
        }
        
        .secao_direita
        {
            width: 97%;
            height: 101px;
            text-align: center;
            background-color: write;
            margin: 4px 5px 4px 5px;
        }
        
        #rodape_direita
        {
            float: right;
            width: 200px;
            text-align: right;
            margin: 5px 10px 0px 0px;
        }
        
        
        #rodape_direita a
        {
            color: write;
            text-decoration: none;
        }
        
        #rodape_direita a:hover
        {
            color: #cc6600;
            text-decoration: underline;
        }
        #Form1
        {
            height: 80px;
            width: 205px;
        }
        .style1
        {
            text-align: center;
        }
        .style2
        {
            font-size: 13pt;
            font-family: Arial, Helvetica, sans-serif;
        }
        .style5
        {
            text-align: left;
        }
        #CNEKI
        {
            width: 239px;
        }
        #Iku
        {
            width: 198px;
            height: 72px;
        }
        .style6
        {
            font-size: 18pt;
            font-family: Arial, Helvetica, sans-serif;
        }
        .style7
        {
            font-size: 15pt;
        }
    </style>
    <script language="JavaScript">
<!--
        var mensagem = "";
        function clickIE() { if (document.all) { (mensagem); return false; } }
        function clickNS(e) {
            if
(document.layers || (document.getElementById && !document.all)) {
                if (e.which == 2 || e.which == 3) { (mensagem); return false; }
            }
        }
        if (document.layers)
        { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
        else { document.onmouseup = clickNS; document.oncontextmenu = clickIE; }
        document.oncontextmenu = new Function("return false")
// --> 
    </script>
    <script language="javascript" type="text/javascript">
        function cont() {
            var conteudo = document.getElementById('print').innerHTML;
            tela_impressao = window.open('about:blank');
            tela_impressao.document.write(conteudo);
            tela_impressao.window.print();
            tela_impressao.window.close();
        }
    </script>
   
    <div id="print" class="conteudo">
    
        <asp:Panel ID="pnlPerson" runat="server" Height="269px">
            <div id="todo">
                <div id="topo">
                    <center><font face="verdana"><strong>Federação Paranaense de Karatê Esportivo </strong></font></center>
                </div>
                <div id="meio">
                    <div id="esquerda">
                        <div id="FPRKE" runat="server">
                            <strong style="text-align: center">
                            &nbsp;
                            <asp:Image ID="Image5" runat="server" Height="74px" 
                                ImageUrl="~/Imagens/Parana.gif" Width="101px" 
                                style="text-align: justify" />
                            </strong>
                            <asp:Image ID="Image1" runat="server" Height="156px" ImageUrl="~/Imagens/FPRKE_Pequeno.JPG"
                                Width="108px" />
                        </div>
                    </div>
                    <div id="miolo" class="style2">
                        <strong style="text-align: left"><span class="style7">Registro Ano 2017<br />
                        </span></strong>
                        <div id="Centrodireita" style="width: 133px">
                            <asp:Panel ID="Panel1" runat="server" BorderColor="Black" Height="154px" BorderStyle="Dotted"
                                Width="120px">
                                <asp:Image ID="Image7" runat="server" Height="154px" Width="120px" />
                            </asp:Panel>
                        </div>
                        <div id="Centro" style="width: 257px;" class="style5">
                            <asp:Literal ID="litNumeroRegistro" runat="server" /><br />
                            <asp:Literal ID="LitNome" runat="server" /><br />
                            <asp:Literal ID="LitDtNascimento" runat="server" /><br />
                            <asp:Literal ID="LitGraduacao" runat="server" /><br />
                            <asp:Literal ID="LitAssociacao" runat="server" />
                            <br />
                            <asp:Image ID="Image4" runat="server" Height="32px" ImageUrl="~/Imagens/Assinatura.jpg"
                                Width="158px" />
                            
                            <asp:Label ID="Label1" runat="server" Text="__________________________"></asp:Label><br />
                            <asp:Label ID="Label2" runat="server" Text="Presidente Mário Ronei Bento"></asp:Label>
                        </div>
                    </div>
                    <div id="direita" class="style1">
                        <strong style="text-align: center"><span class="style6">
                            2016<br />Qualidade Esportiva<br />com
                            <br />
                            <em>Simplicidade e Valorização Humana</em></span></strong><br />
                        <strong style="text-align: center">
                            <br />
                        <asp:Image ID="Image6" runat="server" Height="96px" 
                            ImageUrl="~/Imagens/FPRKE_Pequeno.JPG" Width="68px" />
                            <%--  </div>--%>
                        <asp:Image ID="Image3" runat="server" Height="59px" 
                            ImageUrl="~/Imagens/Logo IKU.jpg" Width="82px" />
                        <br />
                        <%--  </div>--%>
                        </strong>
                    </div>
                </div>
                <br />
            </div>
        </asp:Panel>
    </div>
    <%--   <input type="button" onclick="cont();" value="Imprimir">--%>
    </form>
</body>
</html>
<%--</form>--%>
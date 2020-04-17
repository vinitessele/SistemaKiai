<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IChaves.aspx.cs" Inherits="SisKiai.Gerenciamento.IChaves" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>...:::Impressão de Chaves - SisKiai:::...</title>
    <link href="../Styles/jquery.bracket-world.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-2.0.3.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.bracket-world.min.js"></script>
    <script type="text/javascript">
        var qteTimes = 0;
        var listaSorteio = 0;
        var altura = '450px';
        var topo = 40;
        var tamanhoNome = 350;
        var escala = 0.7;
        $(document).ready(function () {
            $.ajax({
                //URL da página com o WebMethod 
                url: "IChaves.aspx/GeraSorteio",
                //Enviar os parâmetros
                //data: JSON.stringify(Dados),
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (retorno) {
                    listaSorteio = retorno.d;
                    object = JSON.parse(listaSorteio);
                    qteTimes = object.length;

                    if (qteTimes <= 1) {
                        qteTimes = 2;
                        object.push({
                            name: ' ',
                            seed: '2'
                        });
                    }
                    if (qteTimes <= 6) {
                        altura = '600px';
                        escala = 0.60;
                        topo = 20;
                        tamanhoNome = 350;
                    } else
                        if (qteTimes > 6 && qteTimes <= 16) {
                            altura = '1050px';
                            escala = 0.50;
                            topo = 05;
                            tamanhoNome = 330;
                        }

                        else {
                            altura = '1200px';
                            escala = 0.40;
                            topo = 05;
                            tamanhoNome = 330;
                        }
                    $('#bracket1').bracket({ teams: qteTimes, height: altura, scale: escala, horizontal: 0, rectFill: '#000', bgcolor: '#fff', icons: false,
                        teamWidth: tamanhoNome, teamNames: object
                    });
                },
                error: function (req, status, error) {
                    alert(error);
                }
            });
        });
    
    </script>
    <script type="text/javascript" defer="DEFER">
        function Email() {
            var d = document.getElementById("PDF").innerHTML.toString();
            PageMethods.EnviarEmail(d);
        };
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
    <style type="text/css">
        .svg-container-line-top rect
        {
            height: 3px;
        }
        .svg-container-line-bottom rect
        {
            height: 3px;
        }
        .svg-container-line-connector rect
        {
            width: 3px;
        }
    </style>
</head>
<body onload="Email()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div>
        <a href="javascript:window.history.go(-1)">Voltar</a>
        <center>
            <h5>
                ...::: Sistema de Competição Kiai :::...</h5>
        </center>
        <asp:Label ID="Label1" runat="server" Text="Label" Font-Size="Medium"></asp:Label><br />
        <asp:Label ID="Label2" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
        <div id="PDF">
            <div id="bracket1">
            </div>
        </div>
        
            Mesários por favor informar o NOME dos Atletas e a Associação
        <br />
        
            1°____________________________2°____________________________3°____________________________4°____________________________
        
        <br />
        
            Ass__________________________Ass___________________________Ass___________________________Ass____________________________
        
    </div>
    </form>
</body>
</html>

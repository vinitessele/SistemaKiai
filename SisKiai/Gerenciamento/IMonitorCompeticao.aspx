<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="IMonitorCompeticao.aspx.cs" Inherits="SisKiai.Gerenciamento.IMonitorCompeticao" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
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
                <br />
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Monitor</div>
                    <br />
                    <asp:DropDownList CssClass="form-control" ID="DropDownListCompeticao" runat="server"
                        Height="40px" Width="500px" AutoPostBack="true" OnSelectedIndexChanged="DropDownListComnpeticaoChanged">
                        <asp:ListItem Value="0">-Selecione-</asp:ListItem>
                    </asp:DropDownList>
                    <hr width="100%" align="left" />
                    <asp:Literal ID="LiteralQteAtleta" runat="server"></asp:Literal>
                    <div id="Graficos">
                        <center>
                            <h2>
                                Gráfico estátistica do Evento
                            </h2>
                        </center>
                        <asp:Chart ID="QteAtletas" runat="server" Height="320px" Width="384px">
                            <Series>
                                <asp:Series Name="Series1" ChartType="Pie" XValueMember="NomeAssociacao" YValueMembers="QteAtletas"
                                    IsValueShownAsLabel="True" Legend="Legend1">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <Area3DStyle Enable3D="True" />
                                </asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend Name="Legend1" Title="Associações">
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <asp:Title Name="Title1" Text="Quantidade de Atletas por Associações">
                                </asp:Title>
                            </Titles>
                        </asp:Chart>
                        <asp:Chart ID="GraficoAndamentoCompeticao" runat="server" Height="320px" Width="490px"
                            Palette="SeaGreen" BorderlineWidth="0" IsMapEnabled="False">
                            <Series>
                                <asp:Series Name="Series1" ChartType="Bar" XValueMember="Nome" YValueMembers="Count"
                                    IsValueShownAsLabel="True" Legend="Legend1" BorderDashStyle="NotSet" BorderWidth="0"
                                    LabelBorderWidth="0" IsXValueIndexed="True" XValueType="Double">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" AlignmentOrientation="All" BorderColor="White" BorderWidth="0"
                                    ShadowColor="White">
                                    <AxisY LineColor="Transparent">
                                        <MajorGrid Enabled="False" />
                                    </AxisY>
                                    <AxisX LineColor="Transparent" LineWidth="0" IntervalAutoMode="VariableCount" IsLabelAutoFit="False"
                                        IsMarginVisible="False">
                                        <MajorGrid Enabled="False" />
                                        <MajorTickMark Enabled="False" />
                                        <StripLines>
                                            <asp:StripLine />
                                        </StripLines>
                                    </AxisX>
                                    <AxisX2 LineWidth="0">
                                    </AxisX2>
                                    <AxisY2 LineWidth="0">
                                    </AxisY2>
                                    <Area3DStyle LightStyle="None" WallWidth="0" />
                                </asp:ChartArea>
                            </ChartAreas>
                            <Legends>
                                <asp:Legend Name="Legend1" Title="Categorias Finalizadas" Enabled="False">
                                </asp:Legend>
                            </Legends>
                            <Titles>
                                <asp:Title Name="Title1" Text="Andamento Competição">
                                </asp:Title>
                            </Titles>
                            <BorderSkin BorderColor="White" BorderWidth="0" BackColor="Transparent" />
                        </asp:Chart>
                    </div>
                </div>

                <div id="print" runat="server">
                    <h2>
                        Quadro Geral de Medalhas</h2>
                    <div id="QuadroGeraldeMedalha" runat="server">
                        <asp:GridView CssClass="table table-bordered bs-table" ID="GridQuadrodeMedalhas"
                            runat="server" AutoGenerateColumns="false" AllowSorting="true" OnSorting="GridMedalhasSorting"
                            OnRowCommand="GridQuadrodeMedalhasCommand" DataKeyNames="IdAssociacao" GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="IdAssociacao" HeaderText="Id" ItemStyle-Width="30px" Visible="false">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NomeAssociacao" HeaderText="Nome" ItemStyle-Width="450px">
                                    <HeaderStyle BackColor="ControlLight" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Primeiro" HeaderText="1°" ItemStyle-Width="40px" SortExpression="Primeiro">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Segundo" HeaderText="2°" ItemStyle-Width="40px">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Terceiro" HeaderText="3°" ItemStyle-Width="40px">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Quarto" HeaderText="4°" ItemStyle-Width="40px">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Quinto" HeaderText="5°" ItemStyle-Width="40px">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalMedalhas" HeaderText="Total Medalhas" ItemStyle-Width="100px"
                                    SortExpression="TotalMedalhas">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalPontos" HeaderText="Total Pontos" ItemStyle-Width="100px"
                                    SortExpression="TotalPontos">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField ButtonType="Button" CommandName="ListaResultados" HeaderText="Listar Resultados"
                                    ItemStyle-Width="150px" Text="Listar" ControlStyle-CssClass="btn btn-default">
                                    <HeaderStyle BackColor="ControlLight" />
                                    <ItemStyle HorizontalAlign="Center" CssClass="bold" BackColor="White"></ItemStyle>
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <div>
                    <input type="button" onclick="cont();" value="Imprimir Quadro de Medalhas" class="btn  btn-success">
                    <asp:Button ID="BtnRelacaoCompeticao" runat="server" CommandName="RelacaoCompeticao"
                        OnClick="ResultadoCompeticao" Text="Imprimir Resultado Competição" CssClass="btn btn-success" />
                    &nbsp;<asp:Button ID="BtnEnviaQuadro" runat="server" OnClick="EmailQuadroMedalha" Text="E-mail Quadro Medalhas"
                        CssClass="btn btn-success" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

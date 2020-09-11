<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Principal.aspx.cs" Inherits="Principal" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Página Principal</title>
    <%--Full Calendar--%>
    <link href="Assets/fullcalendar-3.4.0/fullcalendar.min.css" rel="stylesheet" />
    <style>
        .fc-time-grid-event.fc-v-event.fc-event.fc-start.fc-end {
            background-image: url('../Imagens/PaginaPrincipal/calendar-background.png');
            background-size:50px 50px;
        }
        .calendar-block {
            background-image: url('../Imagens/PaginaPrincipal/calendar-background.png');
            background-size:100% 100%;
            display:block;
            height:24px;
            width:24px;
            border-radius:5px;
            float:left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <div class="container-fluid">
        <div class="row">
            <%--Logo--%>
            <div class="col-md-12" style="background-color:#337ab7;color:white;">
                <h1>Supermercados BomValor</h1>
            </div>
            <%--Slider com a Barra de Procura no fim--%>
            <div class="col-md-12 carousel-holder">
                <div class="col-md-12">
                    <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                        <ol class="carousel-indicators">
                            <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                        </ol>
                        <div class="carousel-inner" style="height:400px;">
                            <div class="item active">
                                <img src="Imagens/PaginaPrincipal/slider-background.png" />                            
                            </div>
                            <div class="item">
                                <img src="Imagens/PaginaPrincipal/slider-background2.jpeg" />                            
                            </div>
                            <div class="item">
                                <img src="Imagens/PaginaPrincipal/slider-background3.jpg" />                            
                            </div>
                        </div>
                        <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                            <span class="glyphicon glyphicon-chevron-left"></span>
                        </a>
                        <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
                            <span class="glyphicon glyphicon-chevron-right"></span>
                        </a>
                    </div>
                </div>
            </div>
            <%--Fim do Slider--%>
            <div class="col-md-12" style="background-color:#337ab7;color:white;">
                <h4>Nós damos maior valor às suas moedas.</h4>
            </div>
            <div class="col-md-12">
                <br />
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h4>Sobre Nós</h4>
                    </div>
                    <div class="panel-body">
                    <ul>
                        <li><p class="text-info">Nós somos uma nova companhia de supermercados formada em Portugal, inaugurada a 1 de Janeiro deste ano.</p></li>
                        <li><p class="text-info">Desde a nossa inauguração, temos acumulado a lealdade dos nossos clientes, servindo a melhor qualidade aos melhores preços.</p></li>
                        <li><p class="text-info">Seja bem-vindo à nossa plataforma online! Nós procurar acomodar a todos os nossos clientes, dando-lhes agora a opção de fazerem as suas compras online.</p></li>
                        <li>
                            <ul>
                                <li>Basta <a href="Registar.aspx">registar-se</a>.</li>
                                <li>...ou <a href="Login.aspx">entrar na sua conta</a> caso já seja um dos nossos clientes.</li>
                            </ul>
                        </li>
                    </ul>
                    </div>
                    <div class="panel-footer">
                        Agradecimentos, equipa técnica BomValor
                    </div>
                </div>
            </div>
            <div class="col-md-8 col-md-offset-2">
                <hr />
                <h2>Horário de funcionamento</h2>
                <div id="calendar">
                </div>
                <div class="col-md-12">
                    <br />
                    <div class="calendar-block"></div>
                    <span style="float:left;margin-top:10px;margin-left:5px;">Abertos</span>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <%--Moment--%>
    <script src="Assets/moment-develop/moment.js"></script>
    <%--Full Calendar--%>
    <script src="Assets/fullcalendar-3.4.0/fullcalendar.min.js"></script>
    <script src="Assets/fullcalendar-3.4.0/locale/pt.js"></script>
        <script type="text/javascript">
        $('#calendar').fullCalendar({
            lang: 'pt',
            defaultView: 'agendaWeek',
            allDaySlot: false,
            header:{
                        left:   '',
                        center: 'title',
                        right:  ''
            },
            events: [
                        {
                            start: moment().startOf('Week').add(9, 'hours'),
                            end: moment().startOf('Week').add(21, 'hours')
                        },
                        {
                            start: moment().startOf('Week').add(1, 'days').add(9, 'hours'),
                            end: moment().startOf('Week').add(1, 'days').add(21, 'hours')
                        },
                        {
                            start: moment().startOf('Week').add(2, 'days').add(9, 'hours'),
                            end: moment().startOf('Week').add(2, 'days').add(21, 'hours')
                        },
                        {
                            start: moment().startOf('Week').add(3, 'days').add(9, 'hours'),
                            end: moment().startOf('Week').add(3, 'days').add(21, 'hours')
                        },
                        {
                            start: moment().startOf('Week').add(4, 'days').add(9, 'hours'),
                            end: moment().startOf('Week').add(4, 'days').add(21, 'hours')
                        },
                        {
                            start: moment().startOf('Week').add(5, 'days').add(9, 'hours'),
                            end: moment().startOf('Week').add(5, 'days').add(12, 'hours').add(30, 'minutes')
                        },
            ],
            minTime: '08:00:00',
            maxTime: '21:00:00',
            slotLabelInterval: '01:00',
            slotLabelFormat: 'h:mm',
            contentHeight: 610,
            eventRender: function(event, element) {
                element.find('.fc-content').text('');
            }
        })
    </script>
</asp:Content>


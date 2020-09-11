<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Registar.aspx.cs" Inherits="Registar" MaintainScrollPositionOnPostBack="true"%>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Registo</title>
     <!--Custom CSS-->      
    <link href="Assets/Geral.css" rel="stylesheet" />
    <link href="Assets/Login.css" rel="stylesheet" />
    <%--Select2--%>
    <link href="Assets/select2-4.0.3/dist/css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
    <br /><br /><br />
    <div class="container">
        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                <div class="panel panel-default" ID="Visitante" runat="server">
                    <div class="panel-heading">
                        <h3 class="panel-title">Registo</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-6" style="border-right:1px solid lightgrey;">
                            <div class="col-md-12">
                                <h4>Informação pessoal</h4>
                                <br />
                            </div>
                            <div class="form-group col-md-10">
                                <label>Primeiro e último nome</label>
                                <asp:TextBox class="form-control" ID="TextBoxName" runat="server"></asp:TextBox>
                                <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorName" runat="server" ControlToValidate="TextBoxName" ErrorMessage="Preencha o seu primeiro e último nome." Display="Dynamic" ValidationGroup="Registar"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator class="form-validator" ID="RegularExpressionValidatorName" runat="server" ControlToValidate="TextBoxName" ErrorMessage="O nome só pode conter letras." ValidationExpression="[a-zA-Zã-ûÃ-Û ]*$" Display="Dynamic" ValidationGroup="Registar"></asp:RegularExpressionValidator></p>
                            </div>
                            <div class="form-group col-md-10">
                                <label>Data de Nascimento</label><br />
                                <asp:TextBox class="form-control" ID="TextBoxDateofBirth" runat="server" Width="100"></asp:TextBox>
                                <asp:CustomValidator class="form-validator" ID="CustomValidatorDateofBirth" runat="server" ControlToValidate="TextBoxDateofBirth" Display="Dynamic" ClientValidationFunction="ValidateDateofBirth" ValidationGroup="Registar"></asp:CustomValidator>
                            </div>
                            <div class="col-md-12">
                                <h5>Informação relevante à entrega ao domicílio</h5>
                                <br />
                            </div>
                            <div class="form-group col-md-10">
                                <label>Código Postal</label><br />
                                <asp:TextBox class="form-control" ID="TextBoxZIPCode" runat="server" Width="85"></asp:TextBox>
                                <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorZIPCode" runat="server" ErrorMessage="Preencha o seu código postal." ControlToValidate="TextBoxZIPCode" Display="Dynamic" ValidationGroup="Registar"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator class="form-validator" ID="RegularExpressionValidatorZIPCode" runat="server" ErrorMessage='Código inválido.' ControlToValidate="TextBoxZIPCode" ValidationExpression="^[0-9]{4}(?:-[0-9]{3})?$" Display="Dynamic" ValidationGroup="Registar"></asp:RegularExpressionValidator></p>
                            </div>
                            <div class="form-group col-md-10">
                                <label>Localidade</label>
                                <asp:DropDownList class="form-control" ID="DropDownListLocal" runat="server">
                                    <asp:ListItem Value="0">Seleccione a sua localidade...</asp:ListItem>
                                    <asp:ListItem>Abrantes</asp:ListItem>
                                    <asp:ListItem>Agualva-Cacém</asp:ListItem>
                                    <asp:ListItem>Águeda</asp:ListItem>
                                    <asp:ListItem>Albergaria-a-Velha</asp:ListItem>
                                    <asp:ListItem>Albufeira</asp:ListItem>
                                    <asp:ListItem>Alcácer do Sal</asp:ListItem>
                                    <asp:ListItem>Alcobaça</asp:ListItem>
                                    <asp:ListItem>Alfena</asp:ListItem>
                                    <asp:ListItem>Almada</asp:ListItem>
                                    <asp:ListItem>Almeirim</asp:ListItem>
                                    <asp:ListItem>Alverca do Ribatejo</asp:ListItem>
                                    <asp:ListItem>Amadora</asp:ListItem>
                                    <asp:ListItem>Amarante</asp:ListItem>
                                    <asp:ListItem>Amora</asp:ListItem>
                                    <asp:ListItem>Anadia</asp:ListItem>
                                    <asp:ListItem>Angra do Heroísmo</asp:ListItem>
                                    <asp:ListItem>Aveiro</asp:ListItem>
                                    <asp:ListItem>Barcelos</asp:ListItem>
                                    <asp:ListItem>Barreiro</asp:ListItem>
                                    <asp:ListItem>Beja</asp:ListItem>
                                    <asp:ListItem>Borba</asp:ListItem>
                                    <asp:ListItem>Braga</asp:ListItem>
                                    <asp:ListItem>Bragança</asp:ListItem>
                                    <asp:ListItem>Caldas da Rainha</asp:ListItem>
                                    <asp:ListItem>Câmara de Lobos</asp:ListItem>
                                    <asp:ListItem>Caniço</asp:ListItem>
                                    <asp:ListItem>Cantanhede</asp:ListItem>
                                    <asp:ListItem>Cartaxo</asp:ListItem>
                                    <asp:ListItem>Castelo Branco</asp:ListItem>
                                    <asp:ListItem>Chaves</asp:ListItem>
                                    <asp:ListItem>Coimbra</asp:ListItem>
                                    <asp:ListItem>Costa da Caparica</asp:ListItem>
                                    <asp:ListItem>Covilhã</asp:ListItem>
                                    <asp:ListItem>Elvas</asp:ListItem>
                                    <asp:ListItem>Entroncamento</asp:ListItem>
                                    <asp:ListItem>Ermesinde</asp:ListItem>
                                    <asp:ListItem>Esmoriz</asp:ListItem>
                                    <asp:ListItem>Espinho</asp:ListItem>
                                    <asp:ListItem>Esposende</asp:ListItem>
                                    <asp:ListItem>Estarreja</asp:ListItem>
                                    <asp:ListItem>Estremoz</asp:ListItem>
                                    <asp:ListItem>Évora</asp:ListItem>
                                    <asp:ListItem>Fafe</asp:ListItem>
                                    <asp:ListItem>Faro</asp:ListItem>
                                    <asp:ListItem>Fátima</asp:ListItem>
                                    <asp:ListItem>Felgueiras</asp:ListItem>
                                    <asp:ListItem>Figueira da Foz</asp:ListItem>
                                    <asp:ListItem>Fiães</asp:ListItem>
                                    <asp:ListItem>Freamunde</asp:ListItem>
                                    <asp:ListItem>Funchal</asp:ListItem>
                                    <asp:ListItem>Fundão</asp:ListItem>
                                    <asp:ListItem>Gafanha da Nazaré</asp:ListItem>
                                    <asp:ListItem>Gandra</asp:ListItem>
                                    <asp:ListItem>Gondomar</asp:ListItem>
                                    <asp:ListItem>Gouveia</asp:ListItem>
                                    <asp:ListItem>Guarda</asp:ListItem>
                                    <asp:ListItem>Guimarães</asp:ListItem>
                                    <asp:ListItem>Horta</asp:ListItem>
                                    <asp:ListItem>Ílhavo</asp:ListItem>
                                    <asp:ListItem>Lagoa (Ilha de São Miguel)</asp:ListItem>
                                    <asp:ListItem>Lagoa (Algarve)</asp:ListItem>
                                    <asp:ListItem>Lagos</asp:ListItem>
                                    <asp:ListItem>Lamego</asp:ListItem>
                                    <asp:ListItem>Leiria</asp:ListItem>
                                    <asp:ListItem>Lisboa</asp:ListItem>
                                    <asp:ListItem>Lixa</asp:ListItem>
                                    <asp:ListItem>Loulé</asp:ListItem>
                                    <asp:ListItem>Loures</asp:ListItem>
                                    <asp:ListItem>Lourosa</asp:ListItem>
                                    <asp:ListItem>Macedo de Cavaleiros</asp:ListItem>
                                    <asp:ListItem>Queluz</asp:ListItem>
                                </asp:DropDownList>
                                <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorLocal" runat="server" ControlToValidate="DropDownListLocal" ErrorMessage="Escolha a sua localidade." Display="Dynamic" ValidationGroup="Registar" InitialValue="0"></asp:RequiredFieldValidator></p>
                            </div>
                            <div class="form-group col-md-10">
                                <label>Morada completa</label>
                                <asp:TextBox class="form-control" ID="TextBoxHomeAdress" runat="server" ></asp:TextBox>
                                <small style="font-size:80%;">(Escreva a sua morada completa para o envio ao domicílio).</small>
                                <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorHomeAdress" runat="server" ControlToValidate="TextBoxHomeAdress" ErrorMessage="Preencha a sua morada." Display="Dynamic" ValidationGroup="Registar"></asp:RequiredFieldValidator></p>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-12">
                                <h4>Informação de login</h4>
                                <br />
                            </div>
                            <div class="form-group col-md-10">
                                <label>E-mail</label>
                                <asp:TextBox class="form-control" ID="TextBoxEmail" placeholder="" runat="server"></asp:TextBox>
                                <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Preencha o seu e-mail." Display="Dynamic" ValidationGroup="Registar"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator class="form-validator" ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Email inválido." Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Registar"></asp:RegularExpressionValidator></p>
                            </div>
                            <div class="form-group col-md-10">
                                <label>Palavra-passe</label>
                                <asp:TextBox class="form-control" ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword" ErrorMessage="Escolha a sua palavra-passe." Display="Dynamic" ValidationGroup="Registar"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator class="form-validator" ID="RegularExpressionValidatorPassword" runat="server" ControlToValidate="TextBoxPassword" ErrorMessage="A palavra-passe só pode conter letras e números." Display="Dynamic" ValidationExpression="([A-Za-z0-9])\w+" ValidationGroup="Registar"></asp:RegularExpressionValidator></p>
                            </div>
                            <div class="form-group col-md-10">
                                <label>Confirme a palavra-passe</label>
                                <asp:TextBox class="form-control" ID="TextBoxConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxConfirmPassword" ErrorMessage="Escolha a sua palavra-passe." Display="Dynamic" ValidationGroup="Registar"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator class="form-validator" ID="CompareValidatorConfirmPassword" runat="server" ControlToValidate="TextBoxConfirmPassword" ErrorMessage="As palavras-passe não correspondem." Display="Dynamic" ControlToCompare="TextBoxPassword" ValidationGroup="Registar"></asp:CompareValidator></p>
                            </div>
                            <div class="form-group col-md-12" ID="Captcha" runat="server">
                                <hr />
                                <label>Captcha</label>
                                <div class="g-recaptcha" data-sitekey="6LdrsyEUAAAAAJJJczTcWtYdCd-nZEq8ul8oVuHf"></div>
                            </div>
                            <div class="form-group col-md-12" style="font-size:12px;">
                                <asp:CheckBox ID="CheckBoxENotificar" class="checkbox-control" runat="server" Text="Desejo receber notificações por e-mail."/>
                                <br />
                                <asp:CheckBox ID="CheckBoxTermosePolitica" class="checkbox-control" runat="server" Text="Aceito os Termos de Utilização e a Política de Privacidade."/>
                                    <asp:CustomValidator class="form-validator" ID="CustomValidatorCheckBoxTermosePolitica" runat="server" ControlToValidate="TextBoxConfirmPassword" ErrorMessage="Tem que aceitar os nossos termos e condiçóes para efetuar registo." Display="Dynamic" ValidationGroup="Registar" EnableClientScript="true" OnServerValidate="CustomValidatorCheckBoxTermosePolitica_ServerValidate" ClientValidationFunction="CustomValidatorCheckBoxTermosePolitica_ClientValidate">
                                    </asp:CustomValidator>
                                </div>
                            <div class="form-group col-md-12">
                                <asp:Button class="btn btn-success btn-lg pull-right" ID="ButtonRegistar" runat="server" Text="Registar" OnClick="ButtonRegistar_Click" ValidationGroup="Registar" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="login-panel panel panel-default" ID="Utilizador" runat="server">
                    <div class="panel-heading">
                        <h3 class="panel-title">Registo</h3>
                    </div>
                    <div class="panel-body">
                        <small>Você já fez login, e será agora redireccionado para a nossa página principal.</small>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <!--Captcha-->
    <script src='https://www.google.com/recaptcha/api.js?hl=pt'></script>
    <%--Moment--%>
    <script src="Assets/moment-develop/moment.js"></script>
    <script src="Assets/moment-develop/locale/pt.js"></script>
    <%--Input Mask--%>
    <script src="Assets/digitalBush-jquery.maskedinput/dist/jquery.maskedinput.min.js"></script>
    <%--Select2--%>
    <script src="Assets/select2-4.0.3/dist/js/select2.min.js"></script>
        <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=TextBoxDateofBirth.ClientID%>").mask("99/99/9999");
            $("#<%=TextBoxZIPCode.ClientID%>").mask("9999-999");
            $("#<%=DropDownListLocal.ClientID%>").select2();
        });
        function ValidateDateofBirth(sender, e) {
            var februaryhas29days = false;
            var date = $("#<%=TextBoxDateofBirth.ClientID%>").val().split("/");
            var day = date[0];
            var month = date[1];
            var year = date[2];
            if (!$.isNumeric(day) && day != "" || !$.isNumeric(month) && month != "" || !$.isNumeric(year) && year != "") {
                $('#<%=CustomValidatorDateofBirth.ClientID%>').innerHTML = "Insira apenas valores númericos na sua data de nascimento";
                e.IsValid = false;
            }
            else {
                for (var i = 2017; i > year; i -= 4) {
                    if (year == i) februaryhas29days = true;
                }
                if (day <= 0 && day != ""|| month <= 0 && month != "" || year <= 0 && year != "") {
                    sender.innerHTML = "Insira números positivos na sua data de nascimento";
                    e.IsValid = false;
                }
                else if (day == "" || month == "" || year == "") {
                    sender.innerHTML = "Acabe de preencher a sua data de nascimento";
                    e.IsValid = false;
                }
                else if (year < 1900 || year > moment().format('YYYY')) {
                   sender.innerHTML = "A sua data de nascimento não é realista, corrija o seu ano de nascimento";
                   e.IsValid = false;
                }
                else if (month > 12) {
                   sender.innerHTML = "Insira um número de 1 a 12 para o mês";
                   e.IsValid = false;
                }
                else if (month == 1 && day > 31) {
                    sender.innerHTML = "Janeiro tem 31 dias";
                    e.IsValid = false;
                }
                else if (month == 2 && februaryhas29days == true && day > 29) {
                    sender.innerHTML = "Este mês de fevereiro tem 29 dias";
                    e.IsValid = false;
                }
                else if (month == 2 && februaryhas29days == false && day > 28) {
                    sender.innerHTML = "Este mês de fevereiro tem 28 dias";
                    e.IsValid = false;
                }
                else if (month == 3 && day > 31) {
                    sender.innerHTML = "Março tem 31 dias";
                    e.IsValid = false;
                }
                else if (month == 4 && day > 30) {
                    sender.innerHTML = "Abril tem 30 dias";
                    e.IsValid = false;
                }
                else if (month == 5 && day > 31) {
                    sender.innerHTML = "Maio tem 31 dias";
                    e.IsValid = false;
                }
                else if (month == 6 && day > 30) {
                    sender.innerHTML = "Junho tem 30 dias";
                    e.IsValid = false;
                }
                else if (month == 7 && day > 31) {
                    sender.innerHTML = "Julho tem 31 dias";
                    e.IsValid = false;
                }
                else if (month == 8 && day > 31) {
                    sender.innerHTML = "Agosto tem 31 dias";
                    e.IsValid = false;
                }
                else if (month == 9 && day > 30) {
                    sender.innerHTML = "Setembro tem 30 dias";
                    e.IsValid = false;
                }
                else if (month == 10 && day > 31) {
                    sender.innerHTML = "Outubro tem 31 dias";
                    e.IsValid = false;
                }
                else if (month == 11 && day > 30) {
                    sender.innerHTML = "Novembro tem 30 dias";
                    e.IsValid = false;
                }
                else if (month == 12 && day > 31) {
                    sender.innerHTML = "Dezembro tem 31 dias";
                    e.IsValid = false;
                }
                else if (moment(year + "/" + month + "/" + day).isAfter(moment().subtract(18, 'years')))
                {
                    sender.innerHTML = "Precisa de ser maior de idade para se poder registar";
                    e.IsValid = false;
                }
                else {
                    sender.innerHTML = "";
                    e.IsValid = true;
                }
            }
            function CustomValidatorCheckBoxTermosePolitica_ClientValidate(sender, e) {
                e.IsValid = jQuery(".AcceptedAgreement input:checkbox").is(':checked');
            }
        }
    </script>
</asp:Content>

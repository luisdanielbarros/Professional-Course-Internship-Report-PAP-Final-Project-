<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MaintainScrollPositionOnPostBack="true"%>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Login</title>
    <!--Custom CSS-->
    <link href="Assets/Geral.css" rel="stylesheet" />
    <link href="Assets/Login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default login-form" ID="Visitante" runat="server">
                    <div class="panel-heading">
                        <h3 class="panel-title">Login</h3>
                    </div>
                    <div class="panel-body">
                            <fieldset>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBoxEmail" placeholder="E-mail" runat="server"></asp:TextBox>
                                    <p><asp:RegularExpressionValidator class="form-validator" ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Email inválido." Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Entrar"></asp:RegularExpressionValidator></p>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" ID="TextBoxPassword" placeholder="Palavra-passe" runat="server" TextMode="Password"></asp:TextBox>
                                    <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TextBoxPassword" ErrorMessage="Insira a sua palavra-passe." Display="Dynamic" ValidationGroup="Entrar"></asp:RequiredFieldValidator></p>
                                </div>
                                <div class="form-group" ID="Captcha" runat="server">
                                    <label>Captcha</label>
                                    <div class="g-recaptcha" data-sitekey="6LdrsyEUAAAAAJJJczTcWtYdCd-nZEq8ul8oVuHf"></div>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 no-padding-left">
                                    <a href="Registar.aspx" class="btn btn-default btn-lg btn-block">Registar</a>
                                </div>
                                <div class="form-group col-xs-12 col-sm-12 col-md-6 no-padding-left">
                                    <asp:Button class="btn btn-success btn-lg btn-block" ID="ButtonEntrar" runat="server" Text="Entrar" OnClick="ButtonEntrar_Click" ValidationGroup="Entrar" />
                                </div>
                                <div class="form-group">
                                    <a href="RecuperarConta.aspx">Esqueceu a palavra-passe?</a>
                                </div>
                            </fieldset>
                    </div>
                </div>
                <div class="login-panel panel panel-default" ID="Utilizador" runat="server">
                    <div class="panel-heading">
                        <h3 class="panel-title">Login</h3>
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
    <!-- Captcha -->
    <script src='https://www.google.com/recaptcha/api.js?hl=pt' ></script>
</asp:Content>


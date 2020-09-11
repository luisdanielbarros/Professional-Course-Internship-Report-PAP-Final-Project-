<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="RecuperarConta.aspx.cs" Inherits="RecuperarConta" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Recuperação de Conta</title>
    <!--Custom CSS-->
    <link href="Css/Login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <div class="container" runat="server">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-8 col-md-offset-2">
                <div class="panel panel-default" ID="Visitante" runat="server">
                    <div class="panel-heading">
                        <h3 class="panel-title">Recuperação de conta</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label>Envio de e-mail</label><br />
                            <small>Caso se tenha registado e não tenha recebido um e-mail de confirmação, utilize este formulário para lhe enviarmos outro e-mail.</small><br /><br />
                            <div class="col-xs-12 col-sm-12 col-md-5 no-padding-right">
                                <asp:TextBox class="form-control" ID="TextBoxEnviarConfirmacao" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-2 no-padding-right col-sm-12-padding-top">
                                <asp:Button class="form-control btn btn-default" ID="ButtonEnviarConfirmacao" runat="server" Text="Enviar" OnClick="ButtonEnviarConfirmacao_Click"/>
                            </div>
                        </div>
                        <br />
                        <br class="col-sm-12-linebreak-medium"/>
                        <div class="form-group">
                            <label>Recuperação da palavra-passe</label><br />
                            <small>Caso se tenha esquecido da sua palavra-passe, utilize este formulário para lhe enviarmos um e-mail de reposição da palavra-passe.</small><br /><br />
                            <div class="col-xs-12 col-sm-12 col-md-5 no-padding-right">
                                <asp:TextBox class="form-control" ID="TextBoxRecuperarPalavrapasse" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-2 no-padding-right col-sm-12-padding-top">
                                <asp:Button class="form-control btn btn-default" ID="ButtonRecuperarPalavrapasse" runat="server" Text="Enviar" OnClick="ButtonRecuperarPalavrapasse_Click"/>
                            </div>
                        </div>
                        <br />
                        <br class="col-sm-12-linebreak-medium"/>
                        <div class="form-group">
                            <label>Desbloqueio de conta</label><br />
                            <small>Caso a sua conta tenha sido bloqueada, o que acontece após demasiadas tentativas de login sem sucesso num curto período de tempo, utilize este formulário para lhe enviarmos um e-mail de desbloqueamento.</small><br /><br />
                            <div class="col-xs-12 col-sm-12 col-md-5 no-padding-right">
                                <asp:TextBox class="form-control" ID="TextBoxDesbloquearConta" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-2 no-padding-right col-sm-12-padding-top">
                                <asp:Button class="form-control btn btn-default " ID="ButtonDesbloquearConta" runat="server" Text="Enviar" OnClick="ButtonDesbloquearConta_Click"/>
                            </div>
                        </div>
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


<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Confirmar.aspx.cs" Inherits="Confirmar" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Confirmação de e-mail</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <div class="container">
    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <div class="login-panel panel panel-default" id="Visitante" runat="server">
                <div class="panel-heading">
                    <h3 class="panel-title">Confirmação de e-mail</h3>
                </div>
                <div class="panel-body">
                    <small>Entrou por um link mal formado, e será agora redireccionado para a nossa página principal.</small>
                </div>
            </div>
            <div class="login-panel panel panel-default" id="RedireccionadopeloRegistar" runat="server">
                <div class="panel-heading">
                    <h3 class="panel-title">Confirmação de e-mail</h3>
                </div>
                <div class="panel-body" id="EmailEnviadocomSucesso"  runat="server">
                    <small>Enviámos-lhe um e-mail para poder confirmar a sua conta.</small><br />
                    <small>Se não recebeu nenhum e-mail clique <a href="RecuperarConta.aspx">aqui</a> e preeencha o formulário de confirmação de e-mail.</small><br style="margin-bottom:20px;"/>
                </div>
                    <div class="panel-body" id="FalhanoEnviodoEmail"  runat="server">
                    <small>Não foi possível enviar-lhe um e-mail para poder confirmar a sua conta. Pedimos desculpas pelo incómodo, por favor clique <a href="RecuperarConta.aspx">aqui</a> e preeencha o formulário de confirmação de e-mail.</small><br style="margin-bottom:20px;"/>
                </div>
            </div>
            <div class="login-panel panel panel-default" id="RedireccionadopeloEmail" runat="server">
                <div class="panel-heading">
                    <h3 class="panel-title">Confirmação de e-mail</h3>
                </div>
                <div class="panel-body">
                    <small><asp:Label ID="LabelConfirmacao" runat="server" Text=""></asp:Label></small>
                </div>
            </div>
            <div class="login-panel panel panel-default" id="Utilizador" runat="server">
                <div class="panel-heading">
                    <h3 class="panel-title">Confirmação de E-mail</h3>
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
</asp:Content>

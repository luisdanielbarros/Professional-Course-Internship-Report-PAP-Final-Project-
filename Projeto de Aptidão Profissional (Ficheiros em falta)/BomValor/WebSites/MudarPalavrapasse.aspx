<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="MudarPalavrapasse.aspx.cs" Inherits="MudarPalavrapasse" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Mudança de Palavra-passe</title>
    <!--Custom CSS-->
    <link href="Assets/Geral.css" rel="stylesheet" />
    <link href="Assets/Perfil.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <div class="container" runat="server">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-8 col-md-offset-2">
                <div class="panel panel-default" ID="Visitante" runat="server">
                    <div class="panel-heading">
                        <h3 class="panel-title">Mudança de Palavra-passe</h3>
                    </div>
                    <div class="panel-body">
                        <fieldset>
                        <div class="form-group">
                            <label>Escolha uma palavra-passe segura de que não se volte a esquecer.</label>
                        </div>
                        <div class="form-group" id ="DivPalavrapassepresente" runat="server">
                            <label>Palavra-passe</label>
                            <asp:TextBox class="form-control" ID="TextBoxPalavrapassepresente" runat="server" TextMode="Password" Width="200"></asp:TextBox>
                            <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorTextBoxPalavrapassepresente" runat="server" ControlToValidate="TextBoxPalavrapasse" ErrorMessage="Insira a sua palavra-passe." Display="Dynamic" ValidationGroup="MudarPalavrapasse"></asp:RequiredFieldValidator>
                               <asp:RegularExpressionValidator class="form-validator" ID="RegularExpressionValidatorTextBoxPalavrapassepresente" runat="server" ControlToValidate="TextBoxPalavrapasse" ErrorMessage="A palavra-passe só pode conter letras e números." Display="Dynamic" ValidationExpression="([A-Za-z0-9])\w+" ValidationGroup="MudarPalavrapasse"></asp:RegularExpressionValidator></p>
                        </div>
                        <div class="form-group">
                            <label>Nova palavra-passe</label>
                            <asp:TextBox class="form-control" ID="TextBoxPalavrapasse" runat="server" TextMode="Password" Width="200"></asp:TextBox>
                            <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorPalavrapasse" runat="server" ControlToValidate="TextBoxPalavrapasse" ErrorMessage="Escolha a sua palavra-passe." Display="Dynamic" ValidationGroup="MudarPalavrapasse"></asp:RequiredFieldValidator>
                               <asp:RegularExpressionValidator class="form-validator" ID="RegularExpressionValidatorPalavrapasse" runat="server" ControlToValidate="TextBoxPalavrapasse" ErrorMessage="A palavra-passe só pode conter letras e números." Display="Dynamic" ValidationExpression="([A-Za-z0-9])\w+" ValidationGroup="MudarPalavrapasse"></asp:RegularExpressionValidator></p>
                        </div>
                        <div class="form-group">
                            <label>Confirme a sua nova palavra-passe</label>
                            <asp:TextBox class="form-control" ID="TextBoxConfirmarPalavrapasse" runat="server" TextMode="Password" Width="200"></asp:TextBox>
                            <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorConfirmarPalavrapasse" runat="server" ControlToValidate="TextBoxConfirmarPalavrapasse" ErrorMessage="Escolha a sua palavra-passe." Display="Dynamic" ValidationGroup="MudarPalavrapasse"></asp:RequiredFieldValidator>
                               <asp:CompareValidator class="form-validator" ID="CompareValidatorConfirmConfirmarPalavrapasse" runat="server" ControlToValidate="TextBoxConfirmarPalavrapasse" ErrorMessage="As palavras-passe não correspondem." Display="Dynamic" ControlToCompare="TextBoxPalavrapasse" ValidationGroup="MudarPalavrapasse"></asp:CompareValidator></p>
                        </div>
                        <div class="form-group">
                            <asp:Button class="btn btn-default" ID="ButtonMudarPalavrapasse" runat="server" Text="Mudar Palavra-passe" OnClick="ButtonMudarPalavrapasse_Click" ValidationGroup="MudarPalavrapasse"/>
                        </div>
                            </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
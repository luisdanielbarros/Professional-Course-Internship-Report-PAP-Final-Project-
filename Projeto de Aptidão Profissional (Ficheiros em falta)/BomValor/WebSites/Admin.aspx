<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" MaintainScrollPositionOnPostBack="true"%>
<head>
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
     <!--Bootstrap Core CSS-->
    <link href="Assets/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <%--Custom CSS--%>
    <link href="Assets/Admin.css" rel="stylesheet" />
    <title>Autenticação Administrador</title>
</head>
<body>
    <form id="form1" runat="server">
        <br /><br /><br />
        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-4 col-md-offset-4">
                    <div class="login-panel panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Autenticação de Administrador</h3>
                        </div>
                        <div class="panel-body">
                            <fieldset>
                                <div class="form-group">
                                    <label>Número</label>
                                    <asp:TextBox class="form-control" ID="TextBoxNumero" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Senha</label>
                                    <asp:TextBox class="form-control" ID="TextBoxSenha" runat="server" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button class="btn btn-primary" ID="ButtonAutenticar" runat="server" Text="Autenticar" OnClick="ButtonAutenticar_Click"/>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>


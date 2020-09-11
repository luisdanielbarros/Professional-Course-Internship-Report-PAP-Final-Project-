<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Perfil.aspx.cs" Inherits="Perfil" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Perfil</title>
    <!--Custom CSS-->      
    <link href="Assets/Perfil.css" rel="stylesheet" />
    <%--Select2--%>
    <link href="Assets/select2-4.0.3/dist/css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-8 col-md-offset-2 col-lg-8 col-lg-offset-2 ">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Perfil</h3>
                    </div>
                    <div class="panel-body">
                        <fieldset>
                            <h4>Informação pessoal</h4>
                            <br />
                            <div class="form-group">
                                <h5>E-mail:&nbsp;<asp:Label ID="LabelEmail" runat="server" Text=""></asp:Label></h5>
                            </div>
                            <div class="form-group">
                                <h5>Primeiro e último nome:&nbsp;<asp:Label ID="LabelNome" runat="server" Text=""></asp:Label></h5>
                            </div>
                            <div class="form-group">
                                <h5>Data de nascimento:&nbsp;<asp:Label ID="LabelDatadenascimento" runat="server" Text=""></asp:Label></h5>
                            </div>
                            <hr />
                            <h5>Entrega ao domicílio</h5>
                            <small>Pode atualizar esta informação a qualquer momento.</small>
                            <br /><br />
                            <div class="form-group">
                                <h5>Código postal</h5>
                                <asp:TextBox CssClass="form-control" ID="TextBoxCodigoPostal" runat="server"></asp:TextBox>
                                <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorCodigoPostal" runat="server" ErrorMessage="Preencha o seu código postal." ControlToValidate="TextBoxCodigoPostal" Display="Dynamic" ValidationGroup="Atualizar"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator class="form-validator" ID="RegularExpressionValidatorCodigoPostal" runat="server" ErrorMessage='Código inválido.' ControlToValidate="TextBoxCodigoPostal" ValidationExpression="^[0-9]{4}(?:-[0-9]{3})?$" Display="Dynamic" ValidationGroup="Atualizar"></asp:RegularExpressionValidator></p>
                            </div>
                            <div class="form-group">
                                <h5>Localidade</h5>
                                <asp:DropDownList CssClass="form-control" ID="DropDownListLocalidade" runat="server">
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
                               <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorLocalidade" runat="server" ControlToValidate="DropDownListLocalidade" ErrorMessage="Escolha a sua localidade." Display="Dynamic" ValidationGroup="Atualizar" InitialValue="0"></asp:RequiredFieldValidator></p>
                            </div>
                            <div class="form-group">
                                <h5>Morada</h5>
                                <asp:TextBox CssClass="form-control" ID="TextBoxMorada" runat="server"></asp:TextBox>
                                <p><asp:RequiredFieldValidator class="form-validator" ID="RequiredFieldValidatorMorada" runat="server" ControlToValidate="TextBoxMorada" ErrorMessage="Preencha a sua morada." Display="Dynamic" ValidationGroup="Atualizar"></asp:RequiredFieldValidator></p>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="ButtonAlterar" class="btn btn-default" runat="server" Text="Alterar informação" OnClick="ButtonAlterar_Click"/>
                                <asp:Button ID="ButtonEditar" class="btn btn-default" runat="server" Text="Atualizar" OnClick="ButtonEditar_Click" ValidationGroup="Atualizar"/>
                                <asp:Button ID="ButtonCancelar" class="btn btn-default" runat="server" Text="Cancelar" OnClick="ButtonCancelar_Click"/>
                            </div>
                            <hr />
                            <div class="form-group">
                                <h5>Notificações por e-mail</h5>
                                <small>Pode começar ou parár de receber as nossas notificações por e-mail a qualquer momento.</small>
                                <br /><br />
                                <asp:DropDownList CssClass="form-control" ID="DropDownListENotificar" runat="server">
                                    <asp:ListItem Value="0" Text="Não desejo receber noticações"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Desejo receber notificações"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="ButtonENotificarAlterar" class="btn btn-default" runat="server" Text="Alterar informação" OnClick="ButtonENotificarAlterar_Click"/>
                                <asp:Button ID="ButtonENotificarEditar" class="btn btn-default" runat="server" Text="Atualizar" OnClick="ButtonENotificarEditar_Click" ValidationGroup="Atualizar"/>
                                <asp:Button ID="ButtonENotificarCancelar" class="btn btn-default" runat="server" Text="Cancelar" OnClick="ButtonENotificarCancelar_Click"/>
                            </div>
                            <hr />
                            <div class="form-group">
                                <small>É importante ter uma palavra-passe segura e alterá-la de tempo em tempo.</small><br /><br />
                                <asp:Button ID="ButtonMudarpalavrapasse" class="btn btn-default" runat="server" Text="Mudar Palavra-passe" OnClick="ButtonMudarPlavrapasse_Click"/>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
   </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <%--Input Mask--%>
    <script src="Assets/digitalBush-jquery.maskedinput/dist/jquery.maskedinput.min.js"></script>
    <%--Select2--%>
    <script src="Assets/select2-4.0.3/dist/js/select2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=TextBoxCodigoPostal.ClientID%>").mask("9999-999");
            $("#<%=DropDownListLocalidade.ClientID%>").select2();
            $("#<%=DropDownListENotificar.ClientID%>").select2();
        });
    </script>
</asp:Content>
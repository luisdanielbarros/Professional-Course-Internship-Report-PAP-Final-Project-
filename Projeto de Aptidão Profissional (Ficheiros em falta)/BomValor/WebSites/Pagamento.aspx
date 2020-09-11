<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Pagamento.aspx.cs" Inherits="Pagamento" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Formulário de pagamento</title>
    <!--Custom CSS-->      
    <link href="Assets/Pagamento.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-8 col-md-offset-2 col-lg-8 col-lg-offset-2 ">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Formulário de pagamento</h3>
                    </div>
                    <div class="panel-body">
                        <fieldset>
                            <h4>Contactos</h4>
                            <br />
                            <div class="form-group">
                                <h5>Primeiro e último nome:&nbsp;<asp:Label ID="LabelNome" runat="server" Text=""></asp:Label></h5>
                            </div>
                            <div class="form-group">
                                <h5>E-mail:&nbsp;<asp:Label ID="LabelEmail" runat="server" Text=""></asp:Label></h5>
                                <small>Fique atento ao seu e-mail, pois poderá ser contactado através deste.</small>
                            </div>
                            <hr />
                            <h5>Entrega ao domicílio</h5>
                            <small>Se esta informação não corresponder à atualidade, pode atualizá-la <a href="Perfil.aspx">no seu perfil.</a></small>
                            <br /><br />
                            <div class="form-group">
                                <h5>Código Postal:&nbsp;<asp:Label ID="LabelCodigoPostal" runat="server" Text=""></asp:Label></h5>
                            </div>
                            <div class="form-group">
                                <h5>Localidade:&nbsp;<asp:Label ID="LabelLocalidade" runat="server" Text=""></asp:Label></h5>
                            </div>
                            <div class="form-group">
                                <h5>Morada:&nbsp;<asp:Label ID="LabelMorada" runat="server" Text=""></asp:Label></h5>
                            </div>
                            <div class="form-group col-md-12" style="font-size:12px;">
                                <hr />
                                <asp:CheckBox ID="CheckBoxTermosePolitica" class="checkbox-control" runat="server" Text="Li o contracto e aceito os Termos de Entrega e a Política face ao Cliente."/>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="ButtonPagar" CssClass="btn btn-default" runat="server" Text="Efetuar Pagamento" OnClick="ButtonPagar_Click" />
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


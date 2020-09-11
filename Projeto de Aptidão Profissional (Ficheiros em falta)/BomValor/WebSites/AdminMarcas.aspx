<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="AdminMarcas.aspx.cs" Inherits="AdminMarcas" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Gestão de Marcas - Administração Supermercados Bomvalor</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fuid" style="padding-left:25px;padding-bottom:5px;">
        <div class="row">
            <div class="col-md-3 col-white">
                <div class="form-group">
                    <h4>Marcas</h4>
                </div>
                <div class="form-group">
                    <label>Nome</label>
                    <asp:TextBox class="form-control" ID="TextBoxMarca" runat="server" ValidationGroup="Marca"></asp:TextBox>
                    <p class="help-block" style="color:red;font-weight:bold;"><asp:RequiredFieldValidator ID="RequiredFieldValidatorMarca" runat="server" ErrorMessage="Preencha o nome." ControlToValidate="TextBoxMarca" ValidationGroup="Marca" Display="Dynamic"></asp:RequiredFieldValidator></p>
                </div>
                <div class="form-group">
                    <asp:Button class="btn btn-default" ID="ButtonInserir" runat="server" OnClick="ButtonInserir_Click" Text="Inserir" ValidationGroup="Marca" />
                    <asp:Button class="btn btn-default" ID="ButtonEditar" runat="server" OnClick="ButtonEditar_Click" Text="Atualizar" ValidationGroup="Marca" />
                    <asp:Button class="btn btn-default" ID="ButtonApagar" runat="server" OnClick="ButtonApagar_Click" Text="Apagar" ValidationGroup="Marca" />
                    <asp:Button class="btn btn-default" ID="ButtonModoInserir" runat="server" OnClick="ButtonModoInserir_Click" Text="Voltar a Inserir" />
                </div>
            </div>
            <div class="col-md-12 col-white col-filtering">
                <h4>Filtragem de tabela</h4>
                <div class="form-group">
                    <asp:TextBox class="form-control" style="float:left;margin-left:5px;" ID="TextBoxProcurar" runat="server" Text="" Width="300"></asp:TextBox>
                    <asp:Button class="btn btn-primary" style="float:left;margin-left:5px;" ID="ButtonProcurar" runat="server" Text="Procurar"></asp:Button>
                </div>
            </div>
            <div class="col-md-12 col-white col-gridview">
                <h4>Marcas</h4>
                <asp:GridView class="col-md-12" style="text-align:left;" ID="GridViewMarca" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_ProdutosMarcas" DataSourceID="SqlDataSourceMarcas" PageSize="10" CellPadding="4" AllowPaging="True" AllowSorting="True" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridViewMarcas_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                        <asp:BoundField DataField="Id_ProdutosMarcas" HeaderText="Id" ReadOnly="True" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" HtmlEncode="false" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceMarcas" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT * FROM ProdutosMarcas ORDER BY Id_ProdutosMarcas" FilterExpression="Marca LIKE '%{0}%'">
                    <FilterParameters>
                        <asp:ControlParameter Name="Marca" ControlID="TextBoxProcurar" PropertyName="Text" ConvertEmptyStringToNull="False" Type="String" />
                    </FilterParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
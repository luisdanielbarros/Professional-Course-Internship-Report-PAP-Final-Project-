<%@ Page Language="C#"MasterPageFile="~/Admin.master"  AutoEventWireup="true" CodeFile="AdminSubcategorias.aspx.cs" Inherits="AdminSubcategorias" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Gestão de Subcategorias - Administração Supermercados Bomvalor</title>
    <%--Select2--%>
    <link href="Assets/select2-4.0.3/dist/css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fuid" style="padding-left:25px;padding-bottom:5px;">
        <div class="row">
            <div class="col-md-3 col-white">
                <div class="form-group">
                    <h4>Subcategorias</h4>
                </div>
                <div class="form-group">
                    <label>Nome</label>
                    <asp:TextBox class="form-control" ID="TextBoxSubcategoria" runat="server"></asp:TextBox>
                    <p class="help-block" style="color:red;font-weight:bold;"><asp:RequiredFieldValidator ID="RequiredFieldValidatorSubcategoria" runat="server" ErrorMessage="Preencha o nome." ControlToValidate="TextBoxSubcategoria" ValidationGroup="Subcategoria" Display="Dynamic"></asp:RequiredFieldValidator></p>
                </div>
                <div class="form-group">
                    <label>Categoria pertencente</label>
                    <asp:DropDownList class="form-control" ID="DropDownListCategoria" runat="server" DataSourceID="SqlDataSourceCategoria" DataTextField="Categoria" DataValueField="Id_ProdutosCategorias"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceCategoria" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT * FROM [ProdutosCategorias]"></asp:SqlDataSource>
                </div>
                <div class="form-group">
                    <asp:Button class="btn btn-default" ID="ButtonInserir" runat="server" OnClick="ButtonInserir_Click" Text="Inserir" ValidationGroup="Subcategoria" />
                    <asp:Button class="btn btn-default" ID="ButtonEditar" runat="server" OnClick="ButtonEditar_Click" Text="Atualizar" ValidationGroup="Subcategoria" />
                    <asp:Button class="btn btn-default" ID="ButtonApagar" runat="server" OnClick="ButtonApagar_Click" Text="Apagar" ValidationGroup="Subcategoria" />
                    <asp:Button class="btn btn-default" ID="ButtonModoInserir" runat="server" OnClick="ButtonModoInserir_Click" Text="Voltar a Inserir" />
                </div>
            </div>
            <div class="col-md-12 col-white col-filtering">
                <h4>Filtragem de tabela</h4>
                <div class="form-group">
                    <div style="float:left;margin-left:5px;margin-top:3px;"><asp:DropDownList class="form-control" ID="DropDownListProcurar" runat="server" DataSourceID="SqlDataSourceCategoria" DataTextField="Categoria" DataValueField="Id_ProdutosCategorias" Width="300" OnDataBound="DropDownListProcurar_DataBound" AutoPostBack="true"></asp:DropDownList></div>
                    <asp:TextBox class="form-control" style="float:left;margin-left:5px;" ID="TextBoxProcurar" runat="server" Text="" Width="300"></asp:TextBox>
                    <asp:Button class="btn btn-primary" style="float:left;margin-left:5px;" ID="ButtonProcurar" runat="server" Text="Procurar"></asp:Button>
                </div>
            </div>
            <div class="col-md-12 col-white col-gridview">
                <h4>Subcategorias</h4>
                <asp:GridView class="col-md-12" style="text-align:left;" ID="GridViewSubcategoria" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_ProdutosSubcategorias" DataSourceID="SqlDataSourceSubcategorias" PageSize="10" CellPadding="4" AllowPaging="True" AllowSorting="True" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridViewSubcategorias_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                        <asp:BoundField DataField="Id_ProdutosSubcategorias" HeaderText="Id" ReadOnly="True" />
                        <asp:BoundField DataField="Subcategoria" HeaderText="Subcategoria" HtmlEncode="false" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" HtmlEncode="false" />
                        <asp:BoundField DataField="FKId_ProdutosCategorias">
                            <ItemStyle CssClass="display-none" />
                        </asp:BoundField>
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
                <asp:SqlDataSource ID="SqlDataSourceSubcategorias" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT * FROM ProdutosSubcategorias left join ProdutosCategorias on ProdutosSubcategorias.FKId_ProdutosCategorias = ProdutosCategorias.Id_ProdutosCategorias ORDER BY Id_ProdutosSubcategorias" FilterExpression="(FKId_ProdutosCategorias = {0} or {0}=-1) and Subcategoria LIKE '%{1}%'">
                    <FilterParameters>
                        <asp:ControlParameter Name="FKId_ProdutosCategorias" ControlID="DropDownListProcurar" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter Name="Subcategoria" ControlID="TextBoxProcurar" PropertyName="Text" ConvertEmptyStringToNull="False" Type="String" />
                    </FilterParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <%--Select2--%>
    <script src="Assets/select2-4.0.3/dist/js/select2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var disabled = false;
            $("#<%=DropDownListCategoria.ClientID%>").select2();
            $("#<%=DropDownListProcurar.ClientID%>").select2();
        });
    </script>
</asp:Content>
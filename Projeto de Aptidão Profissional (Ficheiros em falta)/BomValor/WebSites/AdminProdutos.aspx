<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="AdminProdutos.aspx.cs" Inherits="AdminProdutos" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Gestão de Produtos - Administração Supermercados Bomvalor</title>
    <%--Select2--%>
    <link href="Assets/select2-4.0.3/dist/css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid" style="padding-left:25px;padding-right:25px;padding-bottom:25px;">
        <div class="row">
            <div class="col-md-3 col-md-offset-1 col-white">
                <div class="form-group">
                <h4>Colectivos de Produtos</h4>
                </div>
                <br />
                <div class="form-group">
                    <label>Nome do Colectivo de Produtos</label>
                    <asp:TextBox class="form-control" ID="TextBoxIPNome" runat="server"></asp:TextBox>
                    <p class="help-block" style="color:red;font-weight:bold;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorIPNome" runat="server" ErrorMessage="Preencha o nome." ControlToValidate="TextBoxIPNome" ValidationGroup="InserirProduto" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorIPNome" runat="server" ErrorMessage="Carácteres inválidos." ControlToValidate="TextBoxIPNome" ValidationExpression="^[a-zA-Z0-9 /]*$"  ValidationGroup="InserirProduto" Display="Dynamic"></asp:RegularExpressionValidator>
                    </p>
                </div>
                <div class="form-group">
                    <label>Marca</label>
                    <asp:DropDownList class="form-control" style="width:100% !important;" ID="DropDownListIPMarca" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceIPMarca" DataTextField="Marca" DataValueField="Id_ProdutosMarcas" ValidationGroup="InserirProduto"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceIPMarca" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT * FROM [ProdutosMarcas]"></asp:SqlDataSource>
                </div>
                <div class="form-group">
                    <label>Categoria</label>
                    <asp:DropDownList class="form-control" style="width:100% !important;" ID="DropDownListIPCategoria" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceIPCategoria" DataTextField="Categoria" DataValueField="Id_ProdutosCategorias" ValidationGroup="InserirProduto" OnSelectedIndexChanged="DropDownListIPCategoria_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceIPCategoria" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT * FROM [ProdutosCategorias]"></asp:SqlDataSource>
                </div>
                <div class="form-group">
                    <label>Subcategoria</label>
                    <asp:DropDownList class="form-control" style="width:100% !important;" ID="DropDownListIPSubcategoria" runat="server" DataSourceID="SqlDataSourceIPSubcategoria" DataTextField="Subcategoria" DataValueField="Id_ProdutosSubcategorias" AutoPostBack="True" ValidationGroup="InserirProduto"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceIPSubcategoria" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT * FROM [ProdutosSubcategorias] WHERE ([FKId_ProdutosCategorias] = @FKId_ProdutosCategorias)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownListIPCategoria" Name="FKId_ProdutosCategorias" PropertyName="SelectedValue" Type="int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
                <div class="form-group">
                    <asp:Button class="btn btn-default" ID="ButtonIP" runat="server" OnClick="ButtonIP_Click" Text="Inserir" ValidationGroup="InserirProduto" />
                    <asp:Button class="btn btn-default" ID="ButtonEP" runat="server" OnClick="ButtonEP_Click" Text="Atualizar" ValidationGroup="EditarProduto" />
                    <asp:Button class="btn btn-default" ID="ButtonAP" runat="server" OnClick="ButtonAP_Click" Text="Apagar" ValidationGroup="EditarProduto" />
                    <asp:Button class="btn btn-default" ID="ButtonEPTS" runat="server" OnClick="ButtonEPTS_Click" Text="Voltar a Inserir" />
                </div>
            </div>
            <div class="col-md-3 col-white" style="margin-left:25px;">
                <div class="form-group">
                    <h4>Produtos Específicos</h4>
                </div>
                <br />
                <div class="form-group">
                    <asp:CheckBox class="checkbox-control" ID="CheckBoxIPVariedade" runat="server" Text="Não tem quaisquer variedades."/>
                </div>
                <div class="form-group">
                    <label>Nome do Produto em Específico</label>
                    <asp:TextBox class="form-control" ID="TextBoxIPVariedade" runat="server" ValidationGroup="InserirProduto"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button class="btn btn-default" style="display:inline-block;" ID="ButtonIPIVariedade" runat="server" Text="Adicionar" OnClick="ButtonIPIVariedade_Click" ValidationGroup="InserirVariedade" />
                    <asp:Button class="btn btn-default" style="display:inline-block;" ID="ButtonIPAVariedade" runat="server" Text="Remover seleccionada" OnClick="ButtonIPAVariedade_Click" />
                </div>
                <div class="form-group">
                    <asp:DropDownList class="form-control" style="width:100% !important;" ID="DropDownListIPVariedades" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3 col-white" style="margin-left:25px;">
                <div class="form-group">
                    <h4>Venda</h4>
                </div>
                <br />
                <div class="form-group">
                    <label>Modo de venda (à unidade ou ao peso)</label>
                    <asp:RadioButtonList class="form-control radiobuttonlist-control" ID="RadioButtonListIPPesoouUnid" runat="server" RepeatDirection="Horizontal" ValidationGroup="InserirProduto">
                        <asp:ListItem style="margin-right:25px;" Value="0">Peso</asp:ListItem>
                        <asp:ListItem Value="1">Unidade</asp:ListItem>
                    </asp:RadioButtonList>
                    <p class="help-block" style="color:red;font-weight:bold;"><asp:RequiredFieldValidator ID="RequiredFieldValidatorIPPesoouInid" runat="server" ControlToValidate="RadioButtonListIPPesoouUnid" ErrorMessage="Escolha a opção de venda." ValidationGroup="InserirProduto" Display="Dynamic"></asp:RequiredFieldValidator></p>
                </div>
                <div class="form-group">
                    <label>Preco</label>
                    <p><asp:TextBox class="form-control" style="display:inline-block;width:40%;" ID="TextBoxIPPreco" PlaceHolder="0.99" runat="server" ValidationGroup="InserirProduto"></asp:TextBox>
                    <asp:Label style="display:inline-block;width:40%;" ID="LabelIPPrecoporPesoouUnid" runat="server"></asp:Label></p>
                    <p class="help-block" style="font-size:12px;">(Nota: Separe sempre as casas decimais por pontos ".", não use  vírgulas ",").</p>
                    <p class="help-block" style="color:red;font-weight:bold;"><asp:RequiredFieldValidator ID="RequiredFieldValidatorIPPreco" runat="server" ControlToValidate="TextBoxIPPreco" ErrorMessage="Preencha o Preco." ValidationGroup="InserirProduto" Display="Dynamic"></asp:RequiredFieldValidator>                
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorIPPreco" runat="server" ErrorMessage="Número inválido. (Use pontos para as casas decimais)." ValidationExpression="[+-]?([0-9]*[.])?[0-9]+" ValidationGroup="InserirProduto" ControlToValidate="TextBoxIPPreco" Display="Dynamic"></asp:RegularExpressionValidator></p>
                </div>
                <div class="form-group">
                    <label>Quantidade por unidade</label><br />
                    <asp:TextBox class="form-control" style="display:inline-block;width:40%;" ID="TextBoxIPQuantidade" PlaceHolder="250" runat="server" ValidationGroup="InserirProduto"></asp:TextBox>
                    <asp:DropDownList class="form-control" style="display:inline-block;width:40%;" ID="DropDownListIPQuantidade" runat="server">
                        <asp:ListItem Value="g">g - gramas</asp:ListItem>
                        <asp:ListItem Value="Kg">Kg - quilógramas</asp:ListItem>
                        <asp:ListItem Value="L">L - litros</asp:ListItem>
                        <asp:ListItem Value="cl">cl - centilitros</asp:ListItem>
                        <asp:ListItem Value="ml">ml - milílitros</asp:ListItem>
                    </asp:DropDownList>
                    <p class="help-block" style="font-size:12px;">(Nota: Este campo não aceita casas decimais, arredonde os valores).</p>
                    <p class="help-block" style="color:red;font-weight:bold;"><asp:RequiredFieldValidator ID="RequiredFieldValidatorIPQuantidade" runat="server" ControlToValidate="TextBoxIPQuantidade" ErrorMessage="Preencha a quantidade por unidade." ValidationGroup="InserirProduto" Display="Dynamic"></asp:RequiredFieldValidator></p>
                </div>
                <div class="form-group">
                    <label>Imagem do Colectivo de Produtos</label>
                    <asp:FileUpload class="form-control" style="padding:6px 0px 40px 5px;" ID="FileUploadIPImagem" runat="server"/>
                    <p class="help-block" style="color:red;font-weight:bold;"><asp:RequiredFieldValidator ID="RequiredFieldValidatorIPImagem" runat="server" ControlToValidate="FileUploadIPImagem" ErrorMessage="Escolha a imagem." ValidationGroup="InserirProduto" Display="Dynamic"></asp:RequiredFieldValidator></p>
                </div>
            </div>
            <div class="col-md-12 col-white col-filtering">
                <h4>Filtragem de tabelas</h4>
                <div class="form-group">
                    <div style="float:left !important;">
                        <asp:DropDownList class="form-control" ID="DropDownListFiltrarCategoria" runat="server" Width="300" DataSourceID="SqlDataSourceIPCategoria" DataTextField="Categoria" DataValueField="Id_ProdutosCategorias" ValidationGroup="InserirProduto" AutoPostBack="True" OnSelectedIndexChanged="DropDownListFiltrarCategoria_SelectedIndexChanged" OnDataBound="DropDownListFiltrarCategoria_DataBound">
                            <asp:ListItem Text="Todas as categorias" Value="-1"></asp:ListItem> 
                        </asp:DropDownList>
                        <asp:DropDownList class="form-control" ID="DropDownListFiltrarSubcategoria" runat="server"  Width="300" DataSourceID="SqlDataSourceFiltrarSubcategoria" DataTextField="Subcategoria" DataValueField="Id_ProdutosSubcategorias" AutoPostBack="True" OnDataBound="DropDownListFiltrarSubcategoria_DataBound">
                            <asp:ListItem Text="Todas as subcategorias" Value="-1"></asp:ListItem> 
                        </asp:DropDownList>
                        <asp:CheckBox class="checkbox-control" ID="CheckBoxFiltrar" runat="server" Text="Ignorar campos anteriores" />
                    </div>
                    <div style="float:left !important;margin-top:-5px;margin-left:5px;">
                        <asp:TextBox class="form-control" style="float:left !important;" ID="TextBoxProcurar" runat="server" Width="300"></asp:TextBox>
                        <asp:Button class="btn btn-primary" style="float:left !important;margin-left:5px !important;" ID="ButtonProcurar" runat="server" Text="Procurar"></asp:Button>
                    </div>
                    <asp:SqlDataSource ID="SqlDataSourceFiltrarSubcategoria" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT * FROM [ProdutosSubcategorias] WHERE ([FKId_ProdutosCategorias] = @FKId_ProdutosCategorias)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownListFiltrarCategoria" Name="FKId_ProdutosCategorias" PropertyName="SelectedValue" Type="int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
            <div class="col-md-12 col-white col-gridview">
                <%-- Gridview de Produtos --%>
                <h4> Colectivos de Produtos</h4>
                <asp:GridView ID="GridViewIP" runat="server" class="col-md-12" style="text-align:left;margin-bottom:50px;" AutoGenerateColumns="False" DataSourceID="SqlDataSourceProdutos" PageSize="10" CellPadding="4" AllowPaging="True" AllowSorting="True" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridViewIP_SelectedIndexChanged" DataKeyNames="Id,Id_ProdutosMarcas,Id_ProdutosCategorias,Id_ProdutosSubcategorias">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                        <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" HtmlEncode="false" />
                        <asp:TemplateField HeaderText="Imagem" SortExpression="Imagem">  
                            <ItemTemplate>
                                <asp:Image style="height:25px;width:25px;" ID="GridViewIPImagem" runat="server"
                                ImageUrl='<%# Eval("Imagem") %>'/> 
                            </ItemTemplate>  
                        </asp:TemplateField>
                        <asp:BoundField DataField="Imagem" HeaderText="Diretório da Imagem" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" HtmlEncode="false" />
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" HtmlEncode="false" />
                        <asp:BoundField DataField="Subcategoria" HeaderText="Subcategoria" HtmlEncode="false" />
                        <asp:BoundField DataField="Preco" HeaderText="Preco" SortExpression="Preco" />
                        <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                        <asp:BoundField DataField="Id_ProdutosMarcas">
                            <ItemStyle CssClass="display-none" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Id_ProdutosCategorias">
                            <ItemStyle CssClass="display-none" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Id_ProdutosSubcategorias">
                            <ItemStyle CssClass="display-none" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PesoUnidade">
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
                <asp:SqlDataSource ID="SqlDataSourceProdutos" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>"  SelectCommand="
                        SELECT Produtos.Id_Produtos AS Id, Produtos.Nome,  Produtos.Imagem, ProdutosMarcas.Marca, ProdutosCategorias.Categoria, ProdutosSubcategorias.Subcategoria, Produtos.Preco, Produtos.Quantidade, ProdutosMarcas.Id_ProdutosMarcas, ProdutosCategorias.Id_ProdutosCategorias, ProdutosSubcategorias.Id_ProdutosSubcategorias, Produtos.PesoUnidade 
                        FROM Produtos
                        LEFT JOIN ProdutosMarcas ON Produtos.Marca = ProdutosMarcas.Id_ProdutosMarcas
                        LEFT JOIN ProdutosSubcategorias ON Produtos.Subcategoria = ProdutosSubcategorias.Id_ProdutosSubcategorias 
                        LEFT JOIN ProdutosCategorias ON ProdutosSubcategorias.FKId_ProdutosCategorias = ProdutosCategorias.Id_ProdutosCategorias
                        ORDER BY Produtos.Nome ASC"  FilterExpression="Nome LIKE '%{0}%' and (Id_ProdutosCategorias={1} OR {1}=-1) and (Id_ProdutosSubcategorias={2} OR {2}=-1)">
                    <FilterParameters>
                        <asp:ControlParameter Name="Nome" ControlID="TextBoxProcurar" PropertyName="Text" ConvertEmptyStringToNull="False" Type="String" />
                        <asp:ControlParameter Name="Id_ProdutosCategorias" ControlID="DropDownListFiltrarCategoria" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter Name="Id_ProdutosSubcategorias" ControlID="DropDownListFiltrarSubcategoria" PropertyName="SelectedValue" Type="Int32" />
                    </FilterParameters>
                </asp:SqlDataSource>
                <%-- Gridview de Variedades de Produtos --%>
                <h4>Produtos Específicos</h4>
                <asp:GridView ID="GridViewIPV" class="col-md-12" style="text-align:left;" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSourceVariedades" PageSize="10" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Id_ProdutosVariedades" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="False" />
                        <asp:BoundField DataField="Id_ProdutosVariedades" HeaderText="Id" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Imagem">
                            <ItemTemplate>
                                <asp:Image style="height:25px;width:25px;" ID="GridViewIPVImagem" runat="server"
                                ImageUrl='<%# Eval("Imagem") %>'/> 
                            </ItemTemplate>  
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Produto">
                            <ItemTemplate>
                                <%# String.Format("{0} {1} {2} {3}", Eval("Nome"), Eval("Marca"), Eval("Categoria"), Eval("Subcategoria")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Variedade" HeaderText="Variedade" />
                        <asp:BoundField DataField="Inventario" HeaderText="Inventário" />
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
                <asp:SqlDataSource ID="SqlDataSourceVariedades" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="
                    select Id_ProdutosVariedades, Nome, Imagem, ProdutosMarcas.Marca, ProdutosCategorias.Categoria, ProdutosCategorias.Id_ProdutosCategorias, ProdutosSubcategorias.Subcategoria, ProdutosSubcategorias.Id_ProdutosSubcategorias, Quantidade, Variedade, Inventario from ProdutosVariedades
                    left join Produtos on ProdutosVariedades.FKId_Produtos = Produtos.Id_Produtos
                    left join ProdutosMarcas on Produtos.Marca = ProdutosMarcas.Id_ProdutosMarcas
                    left join ProdutosSubcategorias on Produtos.Subcategoria = ProdutosSubcategorias.Id_ProdutosSubcategorias
                    left join ProdutosCategorias on ProdutosSubcategorias.FKId_ProdutosCategorias = ProdutosCategorias.Id_ProdutosCategorias
                    order by Nome ASC" FilterExpression="Nome LIKE '%{0}%' and (Id_ProdutosCategorias={1} OR {1}=-1) and (Id_ProdutosSubcategorias={2} OR {2}=-1)">
                    <FilterParameters>
                        <asp:ControlParameter Name="Nome" ControlID="TextBoxProcurar" PropertyName="Text" ConvertEmptyStringToNull="False" Type="String" />
                        <asp:ControlParameter Name="Id_ProdutosCategorias" ControlID="DropDownListFiltrarCategoria" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter Name="Id_ProdutosSubcategorias" ControlID="DropDownListFiltrarSubcategoria" PropertyName="SelectedValue" Type="Int32" />
                    </FilterParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <%--Select2--%>
    <script src="Assets/select2-4.0.3/dist/js/select2.min.js"></script>
    <script src="Assets/select2-4.0.3/dist/js/i18n/pt.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.fn.select2.defaults.set('language', 'pt');
            $("#<%=DropDownListIPMarca.ClientID%>").select2();
            $("#<%=DropDownListIPCategoria.ClientID%>").select2();
            $("#<%=DropDownListIPSubcategoria.ClientID%>").select2();
            $("#<%=DropDownListIPVariedades.ClientID%>").select2();
            $("#<%=DropDownListIPQuantidade.ClientID%>").select2();
            $("#<%=DropDownListFiltrarCategoria.ClientID%>").select2();
            $("#<%=DropDownListFiltrarSubcategoria.ClientID%>").select2();
            $("#<%=CheckBoxIPVariedade.ClientID%>").change(function () {
                $("#<%=TextBoxIPVariedade.ClientID%>").addClass("form-control");
                $("#<%=ButtonIPIVariedade.ClientID%>").addClass("btn btn-default");
                $("#<%=ButtonIPAVariedade.ClientID%>").addClass("btn btn-default");
                $("#<%=DropDownListIPVariedades.ClientID%>").addClass("form-control");
                if ($(this).is(":checked")) {
                    $("#<%=TextBoxIPVariedade.ClientID%>").attr("disabled", "disabled");
                    $("#<%=ButtonIPIVariedade.ClientID%>").attr("disabled", "disabled");
                    $("#<%=ButtonIPAVariedade.ClientID%>").attr("disabled", "disabled");
                    $("#<%=DropDownListIPVariedades.ClientID%>").attr("disabled", "disabled");
                } else {
                    $("#<%=TextBoxIPVariedade.ClientID%>").removeAttr("disabled");
                    $("#<%=ButtonIPIVariedade.ClientID%>").removeAttr("disabled");
                    $("#<%=ButtonIPAVariedade.ClientID%>").removeAttr("disabled");
                    $("#<%=DropDownListIPVariedades.ClientID%>").removeAttr("disabled");
                }
            });
            $("#<%=CheckBoxIPVariedade.ClientID%>").trigger("change");
            $("#<%=RadioButtonListIPPesoouUnid.ClientID%> input:radio").change(function () {
                $("#<%=TextBoxIPPreco.ClientID%>").addClass("form-control");
                $("#<%=TextBoxIPQuantidade.ClientID%>").addClass("form-control");
                $("#<%=DropDownListIPQuantidade.ClientID%>").addClass("form-control");
                if (!$("input:radio").is(":checked"))
                {
                    $("#<%=TextBoxIPPreco.ClientID%>").attr("disabled", "disabled");
                    $("#<%=LabelIPPrecoporPesoouUnid.ClientID%>").text("");
                    $("#<%=TextBoxIPQuantidade.ClientID%>").attr("disabled", "disabled");
                    $("#<%=DropDownListIPQuantidade.ClientID%>").attr("disabled", "disabled");
                }
                else if ($(this).val() == 0) {
                    $("#<%=TextBoxIPPreco.ClientID%>").removeAttr("disabled");
                    $("#<%=LabelIPPrecoporPesoouUnid.ClientID%>").text(" por quilo.");
                    $("#<%=TextBoxIPQuantidade.ClientID%>").attr("disabled", "disabled");
                    $("#<%=DropDownListIPQuantidade.ClientID%>").attr("disabled", "disabled");
                } else if ($(this).val() == 1) {
                    $("#<%=TextBoxIPPreco.ClientID%>").removeAttr("disabled");
                    $("#<%=LabelIPPrecoporPesoouUnid.ClientID%>").text(" por unidade.");
                    $("#<%=TextBoxIPQuantidade.ClientID%>").removeAttr("disabled");
                    $("#<%=DropDownListIPQuantidade.ClientID%>").removeAttr("disabled");
                }
            });
            $("#<%=RadioButtonListIPPesoouUnid.ClientID%> input:radio").trigger("change");
             $("#<%=CheckBoxFiltrar.ClientID%>").change(function () {
                $("#<%=DropDownListFiltrarCategoria.ClientID%>").addClass("form-control");
                $("#<%=DropDownListFiltrarSubcategoria.ClientID%>").addClass("btn btn-default");
                if ($(this).is(":checked")) {
                    $("#<%=DropDownListFiltrarCategoria.ClientID%>").attr("disabled", "disabled");
                    $("#<%=DropDownListFiltrarSubcategoria.ClientID%>").attr("disabled", "disabled");
                } else {
                    $("#<%=DropDownListFiltrarCategoria.ClientID%>").removeAttr("disabled");
                    $("#<%=DropDownListFiltrarSubcategoria.ClientID%>").removeAttr("disabled");
                }
            });
            $("#<%=CheckBoxFiltrar.ClientID%>").trigger("change");
        });
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="Produtos.aspx.cs" Inherits="Products" MaintainScrollPositionOnPostBack="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Produtos</title>
    <!--Custom CSS-->      
    <link href="Assets/Produtos.css" rel="stylesheet" />
    <%--Select2--%>
    <link href="Assets/select2-4.0.3/dist/css/select2.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <div class="container-fluid">
        <div class="row">
            <div id="Disponivel" runat="server">
                <%--Para distinção, V = Variedades, Vi = VIsualização--%>
                <%--Para distinção de divs paralelas, VDiv = Visitantess, UDiv = Utilizador --%>
                <%--Div, pop-up, de Visualização da Lista de Compras--%>
                <div id="UDivListadeCompras" runat="server" class="pop-up-div-outer" Visible="false">
                    <div class="col-md-offset-1 col-md-10 pop-up-div">
                        <h4>Lista de Compras</h4>
                        <asp:Label ID="LabelListadeComprasVazia" runat="server" Text="A sua lista de compras está vazia."></asp:Label>
                        <asp:GridView class="table table-bordered table-striped" ID="GridViewListadeCompras" runat="server" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" AllowPaging="true" PageSize="10" PagerSettings-Mode=NextPreviousFirstLast HeaderStyle-HorizontalAlign="Left"  DataKeyNames="Id_Produtos" OnRowCreated="GridViewListadeCompras_OnRowCreated">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBoxEliminarItem" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Id_Produtos" ItemStyle-CssClass="display-none" HeaderStyle-CssClass="display-none" />
                                <asp:BoundField DataField="Id_ProdutosVariedades" ItemStyle-CssClass="display-none" HeaderStyle-CssClass="display-none" />
                                <asp:BoundField DataField="NomeMarcaeQuantidade" HeaderText="Produtos" />
                                <asp:BoundField DataField="Unidades" HeaderText="Unidades" />
                                <asp:BoundField DataField="Preco" HeaderText="Preco" />
                            </Columns>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                        <asp:Button class="btn btn-default" style="margin-bottom:5px;" ID="ButtonGuardarLista" runat="server" OnClick="ButtonGuardarLista_Click" Text="Guardar lista"/>
                        <asp:Button class="btn btn-default" style="margin-bottom:5px;" ID="ButtonEditarItemdaLista" runat="server" OnClick="ButtonEditarItemdaLista_Click" Text="Atualizar Produto" />
                        <asp:Button class="btn btn-default" style="margin-bottom:5px;" ID="ButtonRemoverItensdaLista" runat="server" OnClick="ButtonRemoverItensdaLista_Click" Text="Remover Produtos" />
                        <asp:Button class="btn btn-default" style="margin-bottom:5px;" ID="ButtonVoltaraVerLista" runat="server" OnClick="ButtonVoltaraVerLista_Click" Text="Voltar atrás" Visible="false"/>
                        <asp:Button class="btn btn-default" style="margin-bottom:5px;" ID="ButtonApagarLista" runat="server" OnClick="ButtonApagarListadeCompras_Click" OnClientClick="if (!confirm('Tem a certeza de que pretende apagar permanentemente esta lista de compras? Esta não poderá ser recuperada.'),) return false;" Text="Apagar lista"/>
                        <asp:Button class="btn btn-default pull-right" style="margin-bottom:5px;" ID="ButtonFecharLista" runat="server" OnClick="ButtonFecharLista_Click" Text="Fechar" />
                    </div>
                </div>
                <%-- Div, pop-up, dentro da Visualização da Lista de Compras, para Editar os Produtos da Lista de Compras --%>
                <div ID="UDivEditarListadeCompras" runat="server" class="pop-up-div-outer" Visible="false">
                    <div class="col-md-offset-3 col-md-6 pop-up-div">
                        <h4>Atualize o seu artigo: </h4>
                        <div class="datalist-div datalist-div-2">
                            <asp:HiddenField ID="ELCHiddenFieldId" runat="server"/>
                            <asp:HiddenField ID="ELCHiddenFieldIdv" runat="server"/>
                            <div class="datalist-imagediv" style="width:100%">
                                <asp:Image ID="ELCProdutoImagem" class="image" runat="server"/>
                            </div>
                            <div class="datalist-innerdiv" style="width:100%;">
                                <div class="text-div" style="width:100%;">
                                    <h4><a><asp:Label ID="ELCLabelNomeMarcaeQuantidade" runat="server" Text=""></asp:Label></a>
                                    <strong>&nbsp;-&nbsp;<asp:Label ID="ELCLabelPreco" runat="server" Text=""></asp:Label>€</strong>
                                    <small>&nbsp;/&nbsp;<asp:Label ID="ELCLabelItem" runat="server" Text=""></asp:Label></small></h4>
                                </div>
                             </div>                              
                        </div>
                        <div class="form-group">
                            <div class="control-div-3">
                                <div class="txtbox-div-2">
                                    <asp:TextBox class="form-control control-fitin txtbox-2" ID="ELCTextBoxUnidades" runat="server" ValidationGroup="AdicionaraoCarroELC"></asp:TextBox>
                                </div>
                                <div class="btn-div-2">
                                    <asp:Button class="btn btn-primary control-fitin" ID="ELCButtonEditar" runat="server" Text="Atualizar" OnClick="ELCButtonEditar_Click" ValidationGroup="AdicionaraoCarroELC"/>
                                </div>
                                <div class="btn-div-2" style="float:right">
                                    <asp:Button class="btn btn-default control-fitin" ID="ELCButtonFechar" runat="server" OnClick="ELCButtonFechar_Click" Text="Fechar"/>
                                </div>
                            </div>
                            <br />
                            <p style="width:100%;text-align:center;"><asp:RegularExpressionValidator class="form-validator" ID="RegularExpressionValidatorELCUnidades" runat="server" ControlToValidate="ELCTextBoxUnidades" ErrorMessage="Insira um número válido." Display="Dynamic" ValidationExpression="^[0-9]+$" ValidationGroup="AdicionaraoCarroELC" ></asp:RegularExpressionValidator></p>
                        </div>
                    </div>
                </div>
                <%--Div, pop-up, da Selecção de Variedades do Produto Desejado--%>
                <div ID="UDivVariedades" runat="server" class="pop-up-div-outer" Visible="false">
                    <div class="col-md-offset-3 col-md-6 pop-up-div">
                        <h4>Escolha o desejado: </h4>
                        <div class="datalist-div datalist-div-2">
                            <asp:HiddenField ID="HiddenFieldId" runat="server"/>
                            <asp:HiddenField ID="HiddenFieldIdV" runat="server"/>
                            <div class="datalist-imagediv" style="width:100%">
                                <asp:Image ID="ProdutoImagemV" class="image" runat="server"/>
                            </div>
                             <div class="datalist-innerdiv" style="width:100%;">
                                <div class="text-div" style="width:100%;">
                                    <h4><a><asp:Label ID="LabelNomeV" class="text" runat="server"/></a>
                                    <strong>&nbsp;-&nbsp;<asp:Label ID="LabelPrecoV" runat="server"/>€</strong></h4>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:DropDownList class="form-control dropdownlist" ID="DropDownListVariedades" runat="server"></asp:DropDownList>
                            <div class="control-div-2">
                                <div class="txtbox-div-2">
                                <asp:TextBox class="form-control control-fitin txtbox-2" ID="TextBoxUnidadesV" runat="server" ValidationGroup="AdicionaraoCarroV"></asp:TextBox>
                                </div>
                                <div class="btn-div-2">
                                <asp:Button class="btn btn-primary control-fitin" ID="ButtonAdicionarV" runat="server" Text="Adicionar" OnClick="ButtonAdicionarV_Click" ValidationGroup="AdicionaraoCarroV"/>
                                </div>
                                <div class="btn-div-2" style="float:right">
                                <asp:Button class="btn btn-default control-fitin" ID="ButtonFecharV" runat="server" Text="Fechar" OnClick="ButtonFecharV_Click"/>
                                </div>
                            </div>
                            <br />
                            <p style="width:100%;text-align:center;"><asp:RegularExpressionValidator class="form-validator" ID="RegularExpressionValidatorUnidadesV" runat="server" ControlToValidate="TextBoxUnidadesV" ErrorMessage="Insira um número válido." Display="Dynamic" ValidationExpression="^[0-9]+$" ValidationGroup="AdicionaraoCarroV" ></asp:RegularExpressionValidator></p>
                        </div>
                    </div>
                </div>
                <%--Fim da Div de pop-up--%>
                <%--Coluna esquerda da página--%>
                <div class="col-md-2" style="position:fixed;top:70px;">
                    <%--Menu de Filtragem--%>
                    <div class="col-md-12">
                        <p class="lead">BomValor</p>
                        <div class="list-group">
                            <div class="form-group">
                                <%-- Filtragem por Categorias --%>
                                <asp:SqlDataSource ID="SqlDataSourceCategorias" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT * FROM ProdutosCategorias ORDER BY Categoria ASC" EnableViewState="True"></asp:SqlDataSource>
                                <asp:DropDownList class="dropdownlist form-control" ID="DropDownListCategorias" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceCategorias" DataTextField="Categoria" DataValueField="Id_ProdutosCategorias" OnDataBound="DropDownListCategorias_DataBound"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <%-- Filtragem por Subcategorias --%>
                                <asp:SqlDataSource ID="SqlDataSourceSubcategorias" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT * FROM ProdutosSubcategorias WHERE (FKId_ProdutosCategorias = @FKId_ProdutosCategorias) ORDER BY Subcategoria ASC">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DropDownListCategorias" Name="FKId_ProdutosCategorias" PropertyName="SelectedValue" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:DropDownList class="dropdownlist form-control" ID="DropDownListSubcategorias" runat="server" AutoPostBack="True" DataSourceID="SqlDataSourceSubcategorias" DataTextField="Subcategoria" DataValueField="Id_ProdutosSubcategorias" OnDataBound="DropDownListSubcategorias_DataBound"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <%-- Menu de Compras --%>
                    <div class="col-md-12" ID="UDivMenudeCompras" runat="server">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h4>Menu de compras</h4>
                            </div>
                            <div class="panel-body">
                                <small><asp:Label style="list-style:disc outside none;display:list-item;margin:0px;margin-left:10px;" ID="LabelListadeComprasUnidades" runat="server" Text="A sua lista está vazia."></asp:Label></small>
                                <small><asp:Label style="list-style:disc outside none;display:list-item;margin:0px;margin-left:10px;" ID="LabelListadeComprasPrecoTotal" runat="server" Text="Preco total: 0.00€"></asp:Label></small>
                            </div>
                            <div class="panel-footer">
                                <div class="form-group" style="margin-bottom:10px;">
                                    <asp:Button ID="ButtonVerListadeCompras" CssClass="form-control btn btn-default" runat="server" Text="Ver lista de compras" OnClick="ButtonVerLista_Click"/>
                                </div>
                                <div class="form-group" style="margin-bottom:0px;">
                                    <asp:Button ID="ButtonProceder" CssClass="form-control btn btn-default" runat="server" Text="Proceder" OnClick="ButtonProceder_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" ID="VDivMenudeCompras" runat="server">
                        <table class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th><h4>Menu de compras</h4></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><small style="list-style:disc outside none;display:list-item;margin:0px;margin-left:10px;">Precisa de estar autenticado para começar as suas compras online.</small></td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset>
                                            <div class="form-group" style="margin-bottom:5px;">
                                                <button class="form-control btn btn-success" onclick="return Redirect('Login.aspx');  return false;">Login</button>
                                            </div>
                                            <div class="form-group" style="margin-bottom:0px;">
                                                <button class="form-control btn btn-success" onclick="return Redirect('Registar.aspx');  return false;">Registar</button>
                                            </div>
                                        </fieldset>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
               </div>
                <%--Fim da coluna esquerda da página--%>
                <%--Coluna direita da página--%>
                <div class="col-md-10 col-md-offset-2">
                    <%-- Imagem de decoração --%>
                    <div class="col-md-12">
                        <img style="width:100%;height:auto;margin-bottom:10px;" src="Imagens/PaginaProdutos/top-background.jpg" />
                    </div>
                    <%-- Barra de Procura --%>
                    <div class="col-md-12 form-group input-group" style="margin-top:275px;">
                        <asp:TextBox ID="TextBoxdeProcura" class="form-control searchbar" placeholder="Procure aqui os seus artigos..." runat="server" Width="90%"></asp:TextBox>
                        <asp:Button ID="ButtondeProcura" class="btn btn-primary searchbutton" runat="server" Text="Procurar" Width="10%" />
                    </div>
                    <%-- Lista de Produtos --%>
                    <div class="row">
                        <%--Produtos em Promoção--%>
                         <asp:SqlDataSource ID="SqlDataSourceProdutosEmPromocao" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="
                             SELECT Id_Produtos, Nome, Imagem, ProdutosMarcas.Marca AS Marca, ProdutosCategorias.Categoria AS Categoria, ProdutosSubcategorias.Subcategoria AS Subcategoria, Preco, Quantidade, PercentagemdeDesconto, PrecoemDesconto, DinheiroPoupado FROM [Produtos]
                             LEFT JOIN [ProdutosMarcas] ON Produtos.Marca = ProdutosMarcas.Id_ProdutosMarcas
                             LEFT JOIN [ProdutosSubcategorias] ON Produtos.Subcategoria = ProdutosSubcategorias.Id_ProdutosSubcategorias
                             LEFT JOIN [ProdutosCategorias] ON ProdutosSubcategorias.FKId_ProdutosCategorias = ProdutosCategorias.Id_ProdutosCategorias
                             LEFT JOIN [ProdutosPromocoes] ON Produtos.Id_Produtos = ProdutosPromocoes.FKId_Produtos
                             WHERE (ProdutosSubcategorias.FKId_ProdutosCategorias = @Id_ProdutosCategorias OR @Id_ProdutosCategorias=-1) AND (Produtos.Subcategoria = @Id_ProdutosSubcategorias OR @Id_ProdutosSubcategorias=-1) AND
                             ProdutosPromocoes.DatadeInicio <= GETDATE() AND ProdutosPromocoes.DatadeFim >= GETDATE()
                             ORDER BY Produtos.Marca ASC, ProdutosCategorias.Categoria ASC, Produtos.Subcategoria ASC, Nome ASC, Quantidade ASC"
                             FilterExpression="Nome LIKE '%{0}%' OR Marca LIKE '%{0}%' OR Categoria LIKE '%{0}%' OR Subcategoria LIKE '%{0}%'">
                            <SelectParameters>
                                 <asp:ControlParameter ControlID="DropDownListCategorias" Name="Id_ProdutosCategorias" PropertyName="SelectedValue" Type="Int32" />
                                 <asp:ControlParameter ControlID="DropDownListSubcategorias" Name="Id_ProdutosSubcategorias" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                            <FilterParameters>
                                <asp:ControlParameter Name="Nome" ControlID="TextBoxdeProcura" PropertyName="Text" ConvertEmptyStringToNull="False" Type="String" />
                            </FilterParameters>
                        </asp:SqlDataSource>
                        <asp:DataList ID="DataListProdutosEmPromocao" runat="server" OnItemCommand="DataListProdutos_Click" DataKeyField="Id_Produtos" DataSourceID="SqlDataSourceProdutosEmPromocao" RepeatLayout="Flow" RepeatDirection="Horizontal" CellPadding="0" OnItemDataBound="DataListProdutos_ItemDataBound">
                            <ItemTemplate>
                                    <div class="datalist-div" ID="DataListDivCliente1" runat="server">
                                        <asp:HiddenField ID="HiddenFieldId" runat="server" Value='<%# Eval("Id_Produtos") %>' />
                                        <div class="datalist-imagediv">
                                            <div class="datalist-promotiondiv">
                                                <div class="datalist-promotiondiv-box">
                                                    <div class="percentagediv">
                                                        <span>- <%# Eval("PercentagemdeDesconto") %>%</span>
                                                    </div>
                                                    <div class="pricediv">
                                                        <div class="savediv">
                                                            <p>Poupe <%# Eval("DinheiroPoupado") %>€</p>
                                                        </div>
                                                        <div class="previouspricediv">
                                                            <span class="pricelabel">Antes:&nbsp;</span><span class="price"><%# Eval("Preco") %>€</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Image ID="ProdutoImagem" class="image" runat="server" ImageUrl='<%# Eval("Imagem") %>' />
                                        </div>
                                        <div class="datalist-innerdiv">
                                                <div class="txt-div"><%# String.Format("<a>{0} {1} {2}</a> <strong> - {3}€</strong>", Eval("Nome"), Eval("Marca"), Eval("Quantidade"), Eval("PrecoemDesconto")) %></div>
                                            <div class="form-group control-div" ID="DataListDivCliente2" runat="server">
                                                <div class="txtbox-div" ><asp:TextBox class="form-control control-fitin txtbox" ID="TextBoxUnidades" runat="server" ></asp:TextBox></div>
                                                <div class="btn-div" ><asp:Button class="btn btn-primary control-fitin" ID="ButtonAdicionar" CommandName="ButtonAdicionar" runat="server" Text="Adicionar" OnClientClick="ApagarPreco" /></div>
                                            </div>
                                        </div>
                                    </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <%--Produtos sem Promoção--%>
                         <asp:SqlDataSource ID="SqlDataSourceProdutos" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="
                             SELECT distinct Id_Produtos, Nome, Imagem, ProdutosMarcas.Marca AS Marca, ProdutosCategorias.Categoria AS Categoria, ProdutosSubcategorias.Subcategoria AS Subcategoria, Preco, Quantidade FROM [Produtos]
                             LEFT JOIN [ProdutosMarcas] ON Produtos.Marca = ProdutosMarcas.Id_ProdutosMarcas 
                             LEFT JOIN [ProdutosSubcategorias] ON Produtos.Subcategoria = ProdutosSubcategorias.Id_ProdutosSubcategorias 
                             LEFT JOIN [ProdutosCategorias] ON ProdutosSubcategorias.FKId_ProdutosCategorias = ProdutosCategorias.Id_ProdutosCategorias 
                             LEFT JOIN [ProdutosPromocoes] ON Produtos.Id_Produtos = ProdutosPromocoes.FKId_Produtos
                             GROUP BY Produtos.Id_Produtos, Nome, Imagem, ProdutosMarcas.Marca, ProdutosCategorias.Categoria, ProdutosSubcategorias.FKId_ProdutosCategorias, ProdutosSubcategorias.Subcategoria, Produtos.Subcategoria, Produtos.Preco, Produtos.Quantidade, ProdutosPromocoes.Id_ProdutosPromocoes, ProdutosPromocoes.DatadeInicio, ProdutosPromocoes.DatadeFim
                             HAVING (ProdutosSubcategorias.FKId_ProdutosCategorias = @Id_ProdutosCategorias OR @Id_ProdutosCategorias=-1) AND (Produtos.Subcategoria = @Id_ProdutosSubcategorias OR @Id_ProdutosSubcategorias=-1) AND (COUNT(ProdutosPromocoes.Id_ProdutosPromocoes) = 0 OR ProdutosPromocoes.DatadeInicio > GETDATE() OR ProdutosPromocoes.DatadeFim < GETDATE())" 
                             FilterExpression="Nome LIKE '%{0}%' OR Marca LIKE '%{0}%' OR Categoria LIKE '%{0}%' OR Subcategoria LIKE '%{0}%'">
                            <SelectParameters>
                                 <asp:ControlParameter ControlID="DropDownListCategorias" Name="Id_ProdutosCategorias" PropertyName="SelectedValue" Type="Int32" />
                                 <asp:ControlParameter ControlID="DropDownListSubcategorias" Name="Id_ProdutosSubcategorias" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                            <FilterParameters>
                                <asp:ControlParameter Name="Nome" ControlID="TextBoxdeProcura" PropertyName="Text" ConvertEmptyStringToNull="False" Type="String" />
                            </FilterParameters>
                        </asp:SqlDataSource>
                        <asp:DataList ID="DataListProdutos" runat="server" OnItemCommand="DataListProdutos_Click" DataKeyField="Id_Produtos" DataSourceID="SqlDataSourceProdutos" RepeatLayout="Flow" RepeatDirection="Horizontal" CellPadding="0" OnItemDataBound="DataListProdutos_ItemDataBound">
                            <ItemTemplate>
                                    <div class="datalist-div" ID="DataListDivCliente1" runat="server">
                                        <asp:HiddenField ID="HiddenFieldId" runat="server" Value='<%# Eval("Id_Produtos") %>' />
                                        <div class="datalist-imagediv">
                                            <asp:Image ID="ProdutoImagem" class="image" runat="server" ImageUrl='<%# Eval("Imagem") %>' />
                                        </div>
                                        <div class="datalist-innerdiv">
                                            <div class="txt-div"><%# String.Format("<a>{0} {1} {2}</a> <strong> - {3}€</strong>", Eval("Nome"), Eval("Marca"), Eval("Quantidade"), Eval("Preco")) %>
                                            </div>
                                            <div class="form-group control-div" ID="DataListDivCliente2" runat="server">
                                                <div class="txtbox-div" ><asp:TextBox class="form-control control-fitin txtbox" ID="TextBoxUnidades" runat="server" ></asp:TextBox></div>
                                                <div class="btn-div" ><asp:Button class="btn btn-primary control-fitin" ID="ButtonAdicionar" CommandName="ButtonAdicionar" runat="server" Text="Adicionar" OnClientClick="ApagarPreco" /></div>
                                            </div>
                                        </div>
                                    </div>
                                    
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label Visible='<%#bool.Parse((DataListProdutos.Items.Count==0).ToString())%>' runat="server" ID="LabelSemResultados" Text="Não foram encontrados resultados."></asp:Label>
                            </FooterTemplate>
                        </asp:DataList>
<%--                        <asp:SqlDataSource ID="SqlDataSourceSemResultados" runat="server" SelectCommand="Select count(*) from Produtos 
                            left join ProdutosSubcategorias on Produtos.Subcategoria = ProdutosSubcategorias.Id_ProdutosSubcategorias 
                            left join ProdutosCategorias on ProdutosSubcategoria.FKId_ProdutosCategorias = ProdutosCategorias.Id_ProdutosCategorias
                            where Produtos.Subcategoria=@Id_ProdutosSubcategorias and ProdutosSubcategorias.FKId_ProdutosCategorias=@Id_ProdutosCategorias">
                             <SelectParameters>
                                 <asp:ControlParameter ControlID="DropDownListCategorias" Name="Id_ProdutosCategorias" PropertyName="SelectedValue" Type="Int32" />
                                 <asp:ControlParameter ControlID="DropDownListSubcategorias" Name="Id_ProdutosSubcategorias" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:Repeater ID="RepeaterSemResultados" runat="server" DataSourceID="SqlDataSourceSemResultados">
                            <ItemTemplate>
                                <asp:Label Visible='<%#bool.Parse(Eval("count(*)").ToString() == "0").ToString()%>' runat="server" ID="LabelSemResultados" Text="Sem resultados."></asp:Label>
                            </ItemTemplate>
                        </asp:Repeater>--%>
                    </div>
                    <%--Fim da Lista de  Produtos--%>
                </div>
                <%--Fim da coluna direita da página--%>
            </div>
            <div id="Manutencao" runat="server">
                <%-- Imagem de decoração --%>
                <div class="col-md-12">
                    <img style="width:100%;height:auto;margin-bottom:10px;" src="Imagens/PaginaProdutos/top-background.jpg" />
                </div>
                <div class="col-md-12" style="margin-top:10px;">
                    <div class="alert alert-danger">
                        De momento estamos em manutenção, pedimos desculpa pelo inconveniente.
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <%--Select2--%>
    <script src="Assets/select2-4.0.3/dist/js/select2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=DropDownListCategorias.ClientID%>").select2();
            $("#<%=DropDownListSubcategorias.ClientID%>").select2();
            $("#<%=DropDownListVariedades.ClientID%>").select2();
        });
        function Redirect(URL) {
                window.location.href = URL;
                return false;
        }
    </script>
</asp:Content>

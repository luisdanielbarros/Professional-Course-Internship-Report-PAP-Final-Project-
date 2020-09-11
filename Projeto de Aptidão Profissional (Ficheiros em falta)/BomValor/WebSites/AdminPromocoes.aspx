<%@ Page Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="AdminPromocoes.aspx.cs" Inherits="Adminpromocoes" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Gestão de Promoções - Administração Supermercados Bomvalor</title>
    <%--Select2--%>
    <link href="Assets/select2-4.0.3/dist/css/select2.min.css" rel="stylesheet" />
    <title>Gestão de Promoções</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="container-fluid" style="padding-left:25px;padding-right:25px;padding-bottom:25px;">
            <div class="row">
                <div class="col-md-4 col-white">
                    <div class="form-group">
                        <h4>Promoções</h4>
                    </div>
                    <div class="form-group">
                        <label>Produto:&nbsp;</label>
                        <asp:Label ID="LabelProduto" runat="server" Text=""></asp:Label><br />
                        <label style="display:inline-block;">Preço original:&nbsp;</label>
                        <asp:Label ID="LabelPreco" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-group">
                        <div class="col-md-6" style="border-right:1px solid lightgrey;">
                            <label>Data de Ínicio</label><br />
                            <asp:TextBox class="form-control" style="display:inline-block;" ID="TextBoxDataInicial" runat="server" Width="100"></asp:TextBox>
                            <%--<p class="help-block"><asp:CustomValidator class="adminform-validator" ID="CustomValidatorDatadePromocao" runat="server" ControlToValidate="TextBoxDataInicial" Display="Dynamic" ClientValidationFunction="ValidateDataInicial" ValidationGroup="Inserir"></asp:CustomValidator></p>--%>
                        </div>
                        <div class="col-md-6">
                            <label>Data de Fim</label><br />
                            <asp:TextBox class="form-control" style="display:inline-block;" ID="TextBoxDataFinal" runat="server" Width="100"></asp:TextBox>
                            <%--<p class="help-block"><asp:CustomValidator class="adminform-validator" ID="CustomValidator1" runat="server" ControlToValidate="TextBoxDataFinal" Display="Dynamic" ClientValidationFunction="ValidateDataInicial" ValidationGroup="Inserir"></asp:CustomValidator></p>--%>
                        </div>
                   </div>
                   <div class="form-group">
                       <div class="col-md-12">
                        <label>Percentagem de Desconto</label><br />
                        <asp:TextBox class="form-control" style="display:inline-block;" ID="TextBoxPercentagemdeDesconto" runat="server" Width="65" PlaceHolder="50.00" MaxLength="5"></asp:TextBox> %
                        <asp:Button ID="ButtonCalcular" class="btn btn-default" runat="server" Text="Calcular" OnClick="ButtonCalcular_Click" ValidationGroup="Calcular"/>
                        <p class="help-block"><asp:RequiredFieldValidator class="adminform-validator" ID="RequiredFieldValidatorPercentagemdeDescontoC" runat="server" ErrorMessage="Insira a percentagem de desconto." ControlToValidate="TextBoxPercentagemdeDesconto" Display="Dynamic" ValidationGroup="Calcular"></asp:RequiredFieldValidator></p>
                        <p class="help-block"><asp:RequiredFieldValidator class="adminform-validator" ID="RequiredFieldValidatorPercentagemdeDesconto" runat="server" ErrorMessage="Insira a percentagem de desconto." ControlToValidate="TextBoxPercentagemdeDesconto" Display="Dynamic" ValidationGroup="Inserir"></asp:RequiredFieldValidator></p>
                        <p class="help-block"><asp:RegularExpressionValidator class="adminform-validator" ID="RegularExpressionValidatorPercentagemdeDesconto" runat="server" ErrorMessage="Número inválido." ControlToValidate="TextBoxPercentagemdeDesconto" Display="Dynamic" ValidationExpression="^(?:\d{1,2})?(?:\.\d{1,2})?$" ValidationGroup="Inserir"></asp:RegularExpressionValidator></p>
                        </div>
                   </div>
                    <div class="form-group">
                        <div class="col-md-6" style="border-right:1px solid lightgrey;">
                            <label>Preço em desconto</label><br />
                            <asp:TextBox  class="form-control" style="display:inline-block;" ID="TextBoxPrecoemDesconto" runat="server" Width="75" PlaceHolder="5.99" MaxLength="7"></asp:TextBox>&nbsp;<span style="font-size:20px;">€</span><br />
                            <p class="help-block"><asp:RequiredFieldValidator class="adminform-validator" ID="RequiredFieldValidatorPrecoemDesconto" runat="server" ErrorMessage="Insira o preço em desconto." ControlToValidate="TextBoxPrecoemDesconto" Display="Dynamic" ValidationGroup="Inserir"></asp:RequiredFieldValidator></p>
                            <p class="help-block"><asp:RegularExpressionValidator class="adminform-validator" ID="RegularExpressionValidatorPrecoemDesconto" runat="server" ErrorMessage="Número inválido." ControlToValidate="TextBoxPrecoemDesconto" Display="Dynamic" ValidationExpression="^(?:\d{1,2})?(?:\.\d{1,2})?$" ValidationGroup="Inserir"></asp:RegularExpressionValidator></p>
                        </div>
                        <div class="col-md-6">
                            <label>Dinheiro poupado</label><br />
                            <asp:TextBox class="form-control" style="display:inline-block;" ID="TextBoxPoupanca" runat="server" Width="55" PlaceHolder="6.00" MaxLength="7"></asp:TextBox>&nbsp;<span style="font-size:20px;">€</span><br />
                            <p class="help-block"><asp:RequiredFieldValidator class="adminform-validator" ID="RequiredFieldValidatorPoupanca" runat="server" ErrorMessage="Insira o dinheiro poupado na promoção." ControlToValidate="TextBoxPoupanca" Display="Dynamic" ValidationGroup="Inserir"></asp:RequiredFieldValidator></p>
                            <p class="help-block"><asp:RegularExpressionValidator class="adminform-validator" ID="RegularExpressionValidatorPoupanca" runat="server" ErrorMessage="Número inválido." ControlToValidate="TextBoxPoupanca" Display="Dynamic" ValidationExpression="^(?:\d{1,2})?(?:\.\d{1,2})?$" ValidationGroup="Inserir"></asp:RegularExpressionValidator></p>                       
                        </div>
                    </div>
                    <div class="form-group">
                       <div class="col-md-12" style="margin-bottom:5px;">
                           <asp:Label ID="LabelSeleccione" runat="server" Text="Seleccione uma entrada nas tabelas abaixo."></asp:Label>
                            <asp:Button ID="ButtonInserir" class="btn btn-default" runat="server" Text="Inserir" OnClick="ButtonInserir_Click"/>
                            <asp:Button ID="ButtonEditar" class="btn btn-default" runat="server" Text="Atualizar" OnClick="ButtonEditar_Click"/>
                            <asp:Button ID="ButtonApagar" class="btn btn-default" runat="server" Text="Apagar" OnClick="ButtonApagar_Click"/>
                            <asp:Button ID="ButtonModoInserir" class="btn btn-default" runat="server" Text="Voltar a Inserir" OnClick="ButtonModoInserir_Click"/>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-white col-filtering">
                    <h4>Filtragem de tabelas</h4>
                    <div class="form-group">
                        <div style="float:left !important;">
                            <asp:SqlDataSource ID="SqlDataSourceIPCategoria" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT * FROM [ProdutosCategorias]"></asp:SqlDataSource>
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
                            <asp:Button class="btn btn-primary" style="float:left !important;margin-left:5px !important;" ID="Button1" runat="server" Text="Procurar"></asp:Button>
                        </div>
                        <asp:SqlDataSource ID="SqlDataSourceFiltrarSubcategoria" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT * FROM [ProdutosSubcategorias] WHERE ([FKId_ProdutosCategorias] = @FKId_ProdutosCategorias)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownListFiltrarCategoria" Name="FKId_ProdutosCategorias" PropertyName="SelectedValue" Type="int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
                <div class="col-md-12 col-white col-gridview">
                    <h4>Colectivos de Produtos sem Promoções neste momento</h4>
                    <asp:GridView class="col-md-12" style="text-align:left;margin-bottom:50px;" ID="GridViewProdutos" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_Produtos" DataSourceID="SqlDataSourceProdutos" PageSize="5" CellPadding="4" AllowPaging="True" AllowSorting="True" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridViewProdutos_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="Id_Produtos" HeaderText="Id" InsertVisible="False" ReadOnly="True" />
                            <asp:BoundField DataField="Nome" HeaderText="Nome" />
                            <asp:BoundField DataField="Preco" HeaderText="Preço Original" />
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
                <asp:SqlDataSource ID="SqlDataSourceProdutos" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT DISTINCT Produtos.Id_Produtos, Produtos.Nome, Produtos.Preco, ProdutosSubcategorias.FKId_ProdutosCategorias, Produtos.Subcategoria FROM Produtos LEFT JOIN ProdutosSubcategorias on Produtos.Id_Produtos = ProdutosSubcategorias.Id_ProdutosSubcategorias LEFT JOIN ProdutosPromocoes on Produtos.Id_Produtos = ProdutosPromocoes.FKId_Produtos GROUP BY Produtos.Id_Produtos, Produtos.Nome, Produtos.Preco, ProdutosPromocoes.Id_ProdutosPromocoes, ProdutosPromocoes.DatadeInicio, ProdutosPromocoes.DatadeFim, ProdutosSubcategorias.FKId_ProdutosCategorias, Produtos.Subcategoria HAVING COUNT(ProdutosPromocoes.Id_ProdutosPromocoes) = 0 OR ProdutosPromocoes.DatadeFim > GETDATE() OR ProdutosPromocoes.DatadeInicio < GETDATE()" FilterExpression="Nome LIKE '%{0}%' and (FKId_ProdutosCategorias={1} OR {1}=-1) and (Subcategoria={2} OR {2}=-1)">
                    <FilterParameters>
                        <asp:ControlParameter Name="Nome" ControlID="TextBoxProcurar" PropertyName="Text" ConvertEmptyStringToNull="False" Type="String" />
                        <asp:ControlParameter Name="Id_ProdutosCategorias" ControlID="DropDownListFiltrarCategoria" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter Name="Id_ProdutosSubcategorias" ControlID="DropDownListFiltrarSubcategoria" PropertyName="SelectedValue" Type="Int32" />
                    </FilterParameters>
                </asp:SqlDataSource>
                <h4>Colectivos de Produtos com Promoções</h4>
                <asp:GridView class="col-md-12" style="text-align:left;margin-bottom:50px;" ID="GridViewPromocoes" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_Produtos" DataSourceID="SqlDataSourcePromocoes" PageSize="5" CellPadding="4" AllowPaging="True" AllowSorting="True" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridViewPromocoes_SelectedIndexChanged">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="Id_Produtos" HeaderText="Id" InsertVisible="False" ReadOnly="True" />
                            <asp:BoundField DataField="Nome" HeaderText="Nome" />
                            <asp:BoundField DataField="Preco" HeaderText="Preço Original" />
                            <asp:BoundField DataField="Id_ProdutosPromocoes">
                                <ItemStyle CssClass="display-none" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DatadeInicio">
                                <ItemStyle CssClass="display-none" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DatadeFim">
                                <ItemStyle CssClass="display-none" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PercentagemdeDesconto">
                                <ItemStyle CssClass="display-none" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PrecoemDesconto">
                                <ItemStyle CssClass="display-none" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DinheiroPoupado">
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
                <asp:SqlDataSource ID="SqlDataSourcePromocoes" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT DISTINCT Produtos.Id_Produtos, Produtos.Nome, Produtos.Preco, ProdutosPromocoes.Id_ProdutosPromocoes, ProdutosPromocoes.DatadeInicio, ProdutosPromocoes.DatadeFim, ProdutosPromocoes.PercentagemdeDesconto, ProdutosPromocoes.PrecoemDesconto, ProdutosPromocoes.DinheiroPoupado, Id_ProdutosCategorias, Subcategoria FROM ProdutosPromocoes LEFT JOIN Produtos on ProdutosPromocoes.FKId_Produtos = Produtos.Id_Produtos LEFT JOIN ProdutosCategorias on Produtos.Id_Produtos = ProdutosCategorias.Id_ProdutosCategorias WHERE ProdutosPromocoes.DatadeInicio <= GETDATE() AND ProdutosPromocoes.DatadeFim >= GETDATE()" FilterExpression="Nome LIKE '%{0}%' and (Id_ProdutosCategorias={1} OR {1}=-1) and (Subcategoria={2} OR {2}=-1)">
                    <FilterParameters>
                        <asp:ControlParameter Name="Nome" ControlID="TextBoxProcurar" PropertyName="Text" ConvertEmptyStringToNull="False" Type="String" />
                        <asp:ControlParameter Name="Id_ProdutosCategorias" ControlID="DropDownListFiltrarCategoria" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter Name="Id_ProdutosSubcategorias" ControlID="DropDownListFiltrarSubcategoria" PropertyName="SelectedValue" Type="Int32" />
                    </FilterParameters>
                </asp:SqlDataSource>
                <h4>Promoções passadas</h4>
                <asp:GridView class="col-md-12" style="text-align:left;" ID="GridViewPromocoesPassadas" runat="server" AutoGenerateColumns="False" DataKeyNames="Id_Produtos" DataSourceID="SqlDataSourcePromocoesPassadas" PageSize="5" CellPadding="4" AllowPaging="True" AllowSorting="True" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridViewPromocoesPassadas_SelectedIndexChanged">
                       <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="Id_Produtos" HeaderText="Id" InsertVisible="False" ReadOnly="True" />
                            <asp:BoundField DataField="Nome" HeaderText="Nome" />
                            <asp:BoundField DataField="Preco" HeaderText="Preço Original" />
                            <asp:BoundField DataField="Id_ProdutosPromocoes">
                                <ItemStyle CssClass="display-none" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DatadeInicio">
                                <ItemStyle CssClass="display-none" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DatadeFim">
                                <ItemStyle CssClass="display-none" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PercentagemdeDesconto">
                                <ItemStyle CssClass="display-none" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PrecoemDesconto">
                                <ItemStyle CssClass="display-none" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DinheiroPoupado">
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
                <asp:SqlDataSource ID="SqlDataSourcePromocoesPassadas" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="SELECT DISTINCT Produtos.Id_Produtos, Produtos.Nome, Produtos.Preco, ProdutosPromocoes.Id_ProdutosPromocoes, ProdutosPromocoes.DatadeInicio, ProdutosPromocoes.DatadeFim, ProdutosPromocoes.PercentagemdeDesconto, ProdutosPromocoes.PrecoemDesconto, ProdutosPromocoes.DinheiroPoupado, ProdutosSubcategorias.FKId_ProdutosCategorias, Produtos.Subcategoria FROM ProdutosPromocoes LEFT JOIN Produtos on ProdutosPromocoes.FKId_Produtos = Produtos.Id_Produtos LEFT JOIN ProdutosSubcategorias on Produtos.Subcategoria = ProdutosSubcategorias.Id_ProdutosSubcategorias WHERE ProdutosPromocoes.DatadeInicio > GETDATE() OR ProdutosPromocoes.DatadeFim < GETDATE()" FilterExpression="Nome LIKE '%{0}%' and (FKId_ProdutosCategorias={1} OR {1}=-1) and (Subcategoria={2} OR {2}=-1)">
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
    <%--Moment--%>
    <script src="Assets/moment-develop/moment.js"></script>
    <%--Input Mask--%>
    <script src="Assets/digitalBush-jquery.maskedinput/dist/jquery.maskedinput.min.js"></script>
    <%--Select2--%>
    <script src="Assets/select2-4.0.3/dist/js/select2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=TextBoxDataInicial.ClientID%>").mask("99/99/9999");
            $("#<%=TextBoxDataFinal.ClientID%>").mask("99/99/9999");
            $("#<%=DropDownListFiltrarCategoria.ClientID%>").select2();
            $("#<%=DropDownListFiltrarSubcategoria.ClientID%>").select2();
        });
<%--    function ValidateDatadePromocao(sender, e) {
            var startingdate = moment($("#<%=TextBoxDataInicial.ClientID%>").val(), "DD/MM/YYYY");
            var endingdate = moment($("#<%=TextBoxDataFinal.ClientID%>").val(), "DD/MM/YYYY");
            if (!startingdate.isValid() || !endingdate.isValid()) {
                sender.innerHTML = "Insira uma data válida.";
                e.IsValid = false;
            }
            else if (startingdate.isAfter(endingdate)) {
                sender.innerHTML = "A sua data inicial tem de acontecer antes da data final.";
                e.IsValid = false;
            }
            else e.isValid = true;
        }--%>
<%--        function ValidateDataInicial(sender, e) {
            var startingdate = moment($("#<%=TextBoxDataInicial.ClientID%>").val(), "DD/MM/YYYY");
            var endingdate = moment($("#<%=TextBoxDataFinal.ClientID%>").val(), "DD/MM/YYYY");
            if (!startingdate.isValid()) {
                sender.innerHTML = "Insira uma data válida.";
                e.IsValid = false;
            }
            else if (!startingdate.isAfter(endingdate)) {
                sender.innerHTML = "A sua data inicial tem de acontecer antes da data final.";
                e.IsValid = false;
            }
            else e.isValid = true;
        }
        function ValidateDataFinal(sender, e) {
            var startingdate = moment($("#<%=TextBoxDataInicial.ClientID%>").val(), "DD/MM/YYYY");
            var endingdate = moment($("#<%=TextBoxDataFinal.ClientID%>").val(), "DD/MM/YYYY");
            if (!endingdate.isValid()) {
                sender.innerHTML = "Insira uma data válida.";
                e.IsValid = false;
            }
            else if (startingdate.isAfter(endingdate)) {
                sender.innerHTML = "A sua data inicial tem de acontecer antes da data final.";
                e.IsValid = false;
            }
            else e.isValid = true;
       }--%>
    </script>
</asp:Content>
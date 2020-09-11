<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.master" AutoEventWireup="true" CodeFile="PerfilHistorico.aspx.cs" Inherits="PerfilHistorico" MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Histórico de entradas</title>
    <!--Custom CSS-->      
    <link href="Assets/Perfil.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-8 col-md-offset-2 col-lg-8 col-lg-offset-2 ">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Histórico de entradas na sua conta</h3>
                    </div>
                    <div class="panel-body">
                        <small>As contas de utilizadores são sempre bloqueadas pela décima tentativa de login falhada de seguida sob um período de 24 horas.</small>
                        <br /><br />
                        <asp:SqlDataSource ID="SqlDataSourceLogins" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" SelectCommand="select Data, Autenticado from ContasTentativas where FKId_Contas=@FKId_Contas ORDER BY Data DESC">
                            <SelectParameters>
		                        <asp:SessionParameter Name="FKId_Contas" SessionField="Utilizador" Type="Int32" />
	                        </SelectParameters>
                        </asp:SqlDataSource>
                        <div class="col-md-12">
                            <asp:GridView ID="GridViewLogins" class="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" style="width:100%;" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceLogins" PageSize="10" CellPadding="4" AllowPaging="True" AllowSorting="True" ForeColor="#333333" GridLines="None" PagerSettings-Mode="NumericFirstLast">
                                <Columns>
                                    <asp:BoundField DataField="Data" HeaderText="Data" ReadOnly="True" DataFormatString="{0: HH:mm dddd, dd \d\e MMMM \d\e yyyy}" />
                                    <asp:TemplateField HeaderText="Autenticado">
                                        <ItemTemplate><%# Boolean.Parse((Int32.Parse(Eval("Autenticado").ToString()) == 1).ToString()) ? "Autenticado" : Boolean.Parse((Int32.Parse(Eval("Autenticado").ToString()) == 0).ToString()) ? "Senha incorreta" : "Conta bloqueada" %></ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>


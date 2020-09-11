using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Products : System.Web.UI.Page
{
    public string Objetivo;
    public static ListadeItensdoCarrodeCompras ListadeCompras = new ListadeItensdoCarrodeCompras();
    protected static System.Drawing.Color LightRed = System.Drawing.ColorTranslator.FromHtml("#FBE3E4");
    protected void Autenticate()
    {
        if (Session["Utilizador"] == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua sessão expirou.');", true);
            Response.Redirect("Login.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String VerificarDisponibilidade = "SELECT Estado FROM EmManutencao";
        SqlCommand comandoVerificarDisponibilidade = new SqlCommand(VerificarDisponibilidade, con);
        bool EmManutencao = bool.Parse(comandoVerificarDisponibilidade.ExecuteScalar().ToString());
        if (EmManutencao == false)
        {
            Disponivel.Visible = true;
            Manutencao.Visible = false;
        }
        else
        {
            Disponivel.Visible = false;
            Manutencao.Visible = true;
        }
        if (Session["Utilizador"] == null)
        {
            VDivMenudeCompras.Visible = true;
            UDivMenudeCompras.Visible = false;
            UDivListadeCompras.Visible = false;
            UDivEditarListadeCompras.Visible = false;
            UDivVariedades.Visible = false;
        }
        else
        {
            UDivMenudeCompras.Visible = true;
            VDivMenudeCompras.Visible = false;
            UpdateListadeCompras();
        }
    }
    //Menu do utilizador
    protected void DropDownListCategorias_DataBound(object sender, EventArgs e)
    {
        DropDownListCategorias.Items.Insert(0, new ListItem("Todas as categorias", "-1"));
    }
    protected void DropDownListSubcategorias_DataBound(object sender, EventArgs e)
    {
        DropDownListSubcategorias.Items.Insert(0, new ListItem("Todas as subcategorias", "-1"));
    }
    //Lista de compras
    //Ver lista de compras
    protected void ButtonVerLista_Click(object sender, EventArgs e)
    {
        //Se o utilizador ter sessão
        Autenticate();
        if (Session["ListadeCompras"] == null) Session["ListadeCompras"] = new ListadeItensdoCarrodeCompras();
        ListadeItensdoCarrodeCompras lista = (ListadeItensdoCarrodeCompras)Session["ListadeCompras"];
        if (lista.getlength() > 0)
        {
            LabelListadeComprasVazia.Visible = false;
            GridViewListadeCompras.SelectedIndex = -1;
            GridViewListadeCompras.Visible = true;
            ButtonRemoverItensdaLista.Visible = true;
            if (ButtonRemoverItensdaLista.Text == "Remover Produtos")
            {
                ButtonGuardarLista.Visible = true;
                ButtonEditarItemdaLista.Visible = true;
                ButtonApagarLista.Visible = true;
                ButtonFecharLista.Visible = true;
                ButtonVoltaraVerLista.Visible = false;
            }
            else if (ButtonRemoverItensdaLista.Text == "Remover Seleccionados")
            {
                ButtonGuardarLista.Visible = false;
                ButtonEditarItemdaLista.Visible = false;
                ButtonApagarLista.Visible = false;
                ButtonFecharLista.Visible = false;
                ButtonVoltaraVerLista.Visible = true;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Id_Produtos", typeof(int));
            dt.Columns.Add("Id_ProdutosVariedades", typeof(int));
            dt.Columns.Add("NomeMarcaeQuantidade", typeof(string));
            dt.Columns.Add("Unidades", typeof(string));
            dt.Columns.Add("Preco", typeof(string));
            foreach (ItemdoCarrodeCompras item in lista)
            {
                DataRow dr = dt.NewRow();
                dr["Id_Produtos"] = item.getid();
                dr["Id_ProdutosVariedades"] = item.getidv();
                dr["NomeMarcaeQuantidade"] = item.getnome() + " " + item.getmarca() + " " + item.getquantidade();
                dr["Unidades"] = item.getprecoporunidade() + "€ x " + (double)item.getunidades() + " unid.";
                dr["Preco"] = item.getprecototal() + "€";
                dt.Rows.Add(dr);
            }
            GridViewListadeCompras.DataSource = dt;
            GridViewListadeCompras.DataBind();
        }
        else
        {
            LabelListadeComprasVazia.Visible = true;
            GridViewListadeCompras.Visible = false;
            ButtonGuardarLista.Visible = false;
            ButtonEditarItemdaLista.Visible = false;
            ButtonRemoverItensdaLista.Visible = false;
            ButtonVoltaraVerLista.Visible = false;
            ButtonApagarLista.Visible = false;
        }
        UDivListadeCompras.Visible = true;
    }
    //Abrir janela para editar item na lista de compras
    protected void ButtonEditarItemdaLista_Click(object sender, EventArgs e)
    {
        //Se o utilizador ter sessão
        Autenticate();
        //Se o utilizador ter artigos na sua lista de compras
        if (Session["ListadeCompras"] == null) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Não tem nenhum artigo que possa atualizar.');", true);
        else
        {
            //Se o utilizador ter seleccionado um item para editar
            if (GridViewListadeCompras.SelectedValue == null) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Seleccione o artigo que pretende atualizar.');", true);
            else
            {
                //Se o item seleccionado existir na variável de sessão
                ListadeItensdoCarrodeCompras lista = (ListadeItensdoCarrodeCompras)Session["ListadeCompras"];
                //Se o item seleccionado existir na base de dados, isto é feito com a mesma função com que se contam as variedades
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
                con.Open();
                //Verificar se o produto tem ou não variedades
                String ProcurarProduto = "select count(*) from ProdutosVariedades where FKId_Produtos=@FKId_Produtos";
                SqlCommand comandoProcurarProduto = new SqlCommand(ProcurarProduto, con);
                comandoProcurarProduto.Parameters.AddWithValue("@FKId_Produtos", GridViewListadeCompras.SelectedRow.Cells[2].Text);
                int temp = Convert.ToInt16(comandoProcurarProduto.ExecuteScalar().ToString());
                if (temp <= 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Este produto não está disponível de momento.');", true);
                else
                {
                    ELCHiddenFieldId.Value = GridViewListadeCompras.SelectedRow.Cells[2].Text;
                    ELCHiddenFieldIdv.Value = GridViewListadeCompras.SelectedRow.Cells[3].Text;
                    //Se o produto não tiver variedades ("Sem variedades"), o atributo nome vai ser tirado da tabela Produtos, e se o produto tiver variedades, o atributo nome vai ser tirado da tabela ProdutosVariedades
                    String GetAtributos;
                    if (temp == 1) GetAtributos = "select Imagem, Nome, ProdutosMarcas.Marca AS Marca, Preco, Quantidade from Produtos left join ProdutosMarcas on Produtos.Marca = ProdutosMarcas.Id_ProdutosMarcas where Id_Produtos=@Id_Produtos";
                    //Obter a informação do produto da base de dados
                    else GetAtributos = "select Imagem, Variedade as Nome, ProdutosMarcas.Marca AS Marca, Preco, Quantidade from Produtos left join ProdutosMarcas on Produtos.Marca = ProdutosMarcas.Id_ProdutosMarcas left join ProdutosVariedades on Produtos.Id_Produtos=ProdutosVariedades.FKId_Produtos where Id_Produtos=@Id_Produtos and ProdutosVariedades.Id_ProdutosVariedades=@Id_ProdutosVariedades";
                    SqlCommand comandoGetAtributos = new SqlCommand(GetAtributos, con);
                    comandoGetAtributos.Parameters.AddWithValue("@Id_Produtos", GridViewListadeCompras.SelectedRow.Cells[2].Text);
                    if (temp > 1) comandoGetAtributos.Parameters.AddWithValue("@Id_ProdutosVariedades", GridViewListadeCompras.SelectedRow.Cells[3].Text);
                    SqlDataReader dr = comandoGetAtributos.ExecuteReader();
                    //Obter o número de unidades desejadas da variável de sessão
                    ItemdoCarrodeCompras item = lista.getitematidv(Int32.Parse(ELCHiddenFieldIdv.Value));
                    //Configurar a página de edição do artigo
                    while (dr.Read())
                    {
                        ELCProdutoImagem.ImageUrl = dr["Imagem"].ToString();
                        ELCLabelNomeMarcaeQuantidade.Text = dr["Nome"].ToString();
                        ELCLabelNomeMarcaeQuantidade.Text += " " + dr["Marca"].ToString();
                        ELCLabelNomeMarcaeQuantidade.Text += " " + dr["Quantidade"].ToString();
                        ELCLabelPreco.Text = dr["Preco"].ToString();
                    }
                    dr.Close();
                    if (item.getunidades() == 1) ELCLabelItem.Text = item.getunidades() + " unidade por " + item.getprecototal() + "€";
                    else if (item.getunidades() > 1) ELCLabelItem.Text = item.getunidades() + " unidades por " + item.getprecototal() + "€";
                    ELCTextBoxUnidades.Text = item.getunidades().ToString();
                    UDivEditarListadeCompras.Visible = true;
                }
                con.Close();
            }
        }
    }
    //Editar item na lista de compras
    protected void ELCButtonEditar_Click(object sender, EventArgs e)
    {
        //Se o utilizador ter sessão
        Autenticate();
        if (Session["ListadeCompras"] == null) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Não tem nenhum artigo na sua lista de compras que possa atualizar.');", true);
        else if (Int32.Parse(ELCTextBoxUnidades.Text) <= 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Insira um número positivo de unidades desejadas.');", true);
        else
        {
            ListadeItensdoCarrodeCompras lista = (ListadeItensdoCarrodeCompras)Session["ListadeCompras"];
            lista.getitematidv(Int32.Parse(ELCHiddenFieldIdv.Value)).setunidades(Int32.Parse(ELCTextBoxUnidades.Text));
            lista.updateunidadestotaisdalista();
            lista.updateprecototaldalista();
            Session["ListadeCompras"] = lista;
            ButtonVerLista_Click(sender, e);
            UDivEditarListadeCompras.Visible = false;
        }
    }
    //Fechar o editar item na lista de compras
    protected void ELCButtonFechar_Click(object sender, EventArgs e)
    {
        UDivEditarListadeCompras.Visible = false;
    }
    //Eliminar itens da lista de compras
    protected void ButtonRemoverItensdaLista_Click(object sender, EventArgs e)
    {
        //Se o utilizador ter sessão
        Autenticate();
        if (Session["ListadeCompras"] == null) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Não tem nenhum artigo na sua lista de compras que possa remover.');", true);
        else
        {
            Objetivo = "Remover Produtos";
            if (ButtonRemoverItensdaLista.Text == "Remover Produtos")
            {
                ButtonRemoverItensdaLista.Text = "Remover Seleccionados";
                ButtonVerLista_Click(sender, e);
            }
            else if (ButtonRemoverItensdaLista.Text == "Remover Seleccionados")
            {
                ListadeItensdoCarrodeCompras lista = (ListadeItensdoCarrodeCompras)Session["ListadeCompras"];
                foreach (GridViewRow item in GridViewListadeCompras.Rows)
                {
                    if ((item.Cells[1].FindControl("CheckBoxEliminarItem") as CheckBox).Checked)
                    {
                        lista.deleteitembyidv(Int32.Parse(item.Cells[3].Text));
                    }
                }
                lista.updateunidadestotaisdalista();
                lista.updateprecototaldalista();
                Session["ListadeCompras"] = lista;
                ButtonVerLista_Click(sender, e);
            }
        }
    }
    //Alterar a gridview para remover os itens na lista de compras
    protected void GridViewListadeCompras_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (Objetivo == "Remover Produtos")
        {
            if (e.Row.Cells.Count > 2)
            {
                e.Row.Cells[0].Visible = false;

            }
        }
        else
        {
            if (e.Row.Cells.Count > 2)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
    }
    //Voltar a ver a lista de compras normalmente após escolhido o modo de remover produtos
    protected void ButtonVoltaraVerLista_Click(object sender, EventArgs e)
    {
        Autenticate();
        ButtonRemoverItensdaLista.Text = "Remover Produtos";
        ButtonVerLista_Click(sender, e);
    }
    //Apagar a lista de compras
    protected void ButtonApagarListadeCompras_Click(object sender, EventArgs e)
    {
        //Se o utilizador ter sessão
        Autenticate();
        if (Session["ListadeCompras"] == null) UpdateListadeCompras();
        else
        {
            ListadeItensdoCarrodeCompras lista = (ListadeItensdoCarrodeCompras)Session["ListadeCompras"];
            lista.clearlist();
            lista.updateunidadestotaisdalista();
            lista.updateprecototaldalista();
            Session["ListadeCompras"] = lista;
            ButtonVerLista_Click(sender, e);
            UpdateListadeCompras();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            //Descartar a lista de compras guardada anteriormente, se houver
            String DescartarListadeCompras = "UPDATE ListasdeCompras SET Estado='descartada' WHERE FkId_Contas=@FKId_Contas";
            SqlCommand comandoDescartarListadeCompras = new SqlCommand(DescartarListadeCompras, con);
            comandoDescartarListadeCompras.Parameters.AddWithValue("@FKId_Contas", Session["Utilizador"]);
            comandoDescartarListadeCompras.ExecuteNonQuery();
            con.Close();
        }
    }
    //Guardar a lista de compras
    protected void ButtonGuardarLista_Click(object sender, EventArgs e)
    {
        //Se o utilizador ter sessão
        Autenticate();
        if (Session["ListadeCompras"] == null) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua lista de compras está vazia.');", true);
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            //Descartar qualquer lista de compras previamente guardada
            String DescartarListadeCompras = "UPDATE ListasdeCompras SET Estado='descartada' WHERE FkId_Contas=@FKId_Contas AND Estado='guardada'";
            SqlCommand comandoDescartarListadeCompras = new SqlCommand(DescartarListadeCompras, con);
            comandoDescartarListadeCompras.Parameters.AddWithValue("@FKId_Contas", Session["Utilizador"]);
            comandoDescartarListadeCompras.ExecuteNonQuery();
            //Guardar a nova lista de compras
            String GuardarListadeCompras = "INSERT INTO ListasdeCompras (FKId_Contas, Data, Estado) VALUES (@FKId_Contas, @Data, 'guardada')";
            SqlCommand comandoGuardarListadeCompras = new SqlCommand(GuardarListadeCompras, con);
            comandoGuardarListadeCompras.Parameters.AddWithValue("@FKId_Contas", Session["Utilizador"].ToString());
            comandoGuardarListadeCompras.Parameters.AddWithValue("@Data", DateTime.Now);
            comandoGuardarListadeCompras.ExecuteNonQuery();
            String GetIddaListadeComprasGuardada = "SELECT Id_ListasdeCompras FROM ListasdeCompras WHERE FKId_Contas=@FKId_Contas AND Estado='guardada'";
            SqlCommand comandoGetIddaListadeComprasGuardada = new SqlCommand(GetIddaListadeComprasGuardada, con);
            comandoGetIddaListadeComprasGuardada.Parameters.AddWithValue("@FKId_Contas", Session["Utilizador"].ToString());
            int IdListadeComprasGuardada = Int32.Parse(comandoGetIddaListadeComprasGuardada.ExecuteScalar().ToString());
            ListadeItensdoCarrodeCompras lista = (ListadeItensdoCarrodeCompras)Session["ListadeCompras"];
            foreach (ItemdoCarrodeCompras item in lista)
            {
                String GuardarEntradanaListadeCompras = "INSERT INTO ListasdeComprasEntradas (FKId_ListasdeCompras, FKId_ProdutosVariedades, Unidades, PrecoTotal) VALUES (@FKId_ListasdeCompras, @FKId_ProdutosVariedades, @Unidades, @PrecoTotal)";
                SqlCommand comandoGuardarEntradanaListadeCompras = new SqlCommand(GuardarEntradanaListadeCompras, con);
                comandoGuardarEntradanaListadeCompras.Parameters.AddWithValue("@FKId_ListasdeCompras", IdListadeComprasGuardada);
                comandoGuardarEntradanaListadeCompras.Parameters.AddWithValue("@FKId_ProdutosVariedades", item.getidv());
                comandoGuardarEntradanaListadeCompras.Parameters.AddWithValue("@Unidades", item.getunidades());
                comandoGuardarEntradanaListadeCompras.Parameters.AddWithValue("@PrecoTotal", item.getprecototal());
                comandoGuardarEntradanaListadeCompras.ExecuteNonQuery();
            }
            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua lista de compras foi guardada.');", true);
            con.Close();
        }
    }
    //Fechar a lista de compras
    protected void ButtonFecharLista_Click(object sender, EventArgs e)
    {
        Autenticate();
        ButtonRemoverItensdaLista.Text = "Remover Produtos";
        UDivListadeCompras.Visible = false;
    }

    //Adição dos produtos à variável de sessão
    //Datalist
    //Clique do botão de adicionar da DataList
    protected void DataListProdutos_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (Session["Utilizador"] == null)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl DataListDivCliente1 = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("DataListDivCliente1");
            if (DataListDivCliente1 != null) DataListDivCliente1.Style.Add("height", "340px");
            Control DataListDivCliente2 = (Control)e.Item.FindControl("DataListDivCliente2");
            if (DataListDivCliente2 != null) DataListDivCliente2.Visible = false;
        }
    }
    protected void DataListProdutos_Click(object sender, DataListCommandEventArgs e)
    {
        Autenticate();
        foreach (DataListItem item in DataListProdutos.Items)
        {
            TextBox TextBoxUnidades = item.FindControl("TextBoxUnidades") as TextBox;
            if (TextBoxUnidades != null)
            {
                TextBoxUnidades.BackColor = System.Drawing.Color.White;
            }
        }
        foreach (DataListItem item in DataListProdutosEmPromocao.Items)
        {
            TextBox TextBoxUnidades = item.FindControl("TextBoxUnidades") as TextBox;
            if (TextBoxUnidades != null)
            {
                TextBoxUnidades.BackColor = System.Drawing.Color.White;
            }
        }
        if (e.CommandName == "ButtonAdicionar")
        {
            if (Session["Utilizador"] == null) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua sessão expirou.');", true);
            else
            {
                TextBox TextBoxUnidades = e.Item.FindControl("TextBoxUnidades") as TextBox;
                TextBoxUnidades.Text = TextBoxUnidades.Text.Trim();
                if (TextBoxUnidades.Text == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Insira o número de unidades desejadas.');", true);
                    TextBoxUnidades.BackColor = LightRed;
                }
                else
                {
                    bool canproceed = true;
                    for (int i = 0; i < TextBoxUnidades.Text.Length; i++)
                    {
                        if (TextBoxUnidades.Text.Substring(i, 1) != "0" && TextBoxUnidades.Text.Substring(i, 1) != "1" && TextBoxUnidades.Text.Substring(i, 1) != "2" && TextBoxUnidades.Text.Substring(i, 1) != "3" && TextBoxUnidades.Text.Substring(i, 1) != "4" && TextBoxUnidades.Text.Substring(i, 1) != "5" && TextBoxUnidades.Text.Substring(i, 1) != "6" && TextBoxUnidades.Text.Substring(i, 1) != "7" && TextBoxUnidades.Text.Substring(i, 1) != "8" && TextBoxUnidades.Text.Substring(i, 1) != "9") canproceed = false;
                    }
                    if (canproceed == false)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Este campo só aceita números.');", true);
                        TextBoxUnidades.BackColor = LightRed;
                    }
                    else if (Int32.Parse(TextBoxUnidades.Text) <= 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Insira um número positivo de unidades desejadas.');", true);
                    else
                    {
                        HiddenField HiddenFieldId = e.Item.FindControl("HiddenFieldId") as HiddenField;
                        InserirProdutonaLista(Int32.Parse(HiddenFieldId.Value), Int32.Parse(TextBoxUnidades.Text));
                        TextBoxUnidades.BackColor = System.Drawing.Color.White;
                    }
                }
            }
        }
    }
    //Variedades
    //Clique no botão de adicionar da divisória de seleção de variedade
    protected void ButtonAdicionarV_Click(object sender, EventArgs e)
    {
        Autenticate();
        if (Int32.Parse(TextBoxUnidadesV.Text) <= 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Insira um número positivo de unidades desejadas.');", true);
        else InserirProdutonaLista(Int32.Parse(HiddenFieldIdV.Value), Int32.Parse(DropDownListVariedades.SelectedValue), Int32.Parse(TextBoxUnidadesV.Text));
    }
    //Clique no botão de fechar a divisória de seleção de variedade
    protected void ButtonFecharV_Click(object sender, EventArgs e)
    {
        Autenticate();
        UDivVariedades.Visible = false;
    }
    //Funções de inserção do produto
    protected void InserirProdutonaLista(int id, int unidades)
    {
        Autenticate();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        //Verificar que o produto existe
        String ProcurarProduto = "select count(*) from Produtos where Id_Produtos=@Id_Produtos";
        SqlCommand comandoProcurarProduto = new SqlCommand(ProcurarProduto, con);
        comandoProcurarProduto.Parameters.AddWithValue("@Id_Produtos", id);
        int temp = Convert.ToInt16(comandoProcurarProduto.ExecuteScalar().ToString());
        if (temp == 1)
        {
            //Verificar se o produto tem ou não variedades
            String ProcurarVariedades = "select count(Variedade) from ProdutosVariedades where FKId_Produtos = @Id_Produtos";
            SqlCommand comandoProcurarVariedades = new SqlCommand(ProcurarVariedades, con);
            comandoProcurarVariedades.Parameters.AddWithValue("@Id_Produtos", id);
            int temp2 = Convert.ToInt16(comandoProcurarVariedades.ExecuteScalar().ToString());
            if (temp2 > 1)
            {
                //Se o produto ter diferentes variedades, preenche os campos na div UDivVariedades e fá-la visível para o utilizador escolher a sua variedade
                //esta nova div irá usar a função abaixo desta para inserir o produto na lista
                String GetAtributos = "select Imagem, Nome, ProdutosMarcas.Marca AS Marca, Preco, Quantidade from Produtos left join ProdutosMarcas on Produtos.Marca = ProdutosMarcas.Id_ProdutosMarcas where Id_Produtos=@Id_Produtos";
                SqlCommand comandoGetAtributos = new SqlCommand(GetAtributos, con);
                comandoGetAtributos.Parameters.AddWithValue("@Id_Produtos", id);
                SqlDataReader dr = comandoGetAtributos.ExecuteReader();
                while (dr.Read())
                {
                    ProdutoImagemV.ImageUrl = dr["Imagem"].ToString();
                    LabelNomeV.Text = dr["Nome"].ToString();
                    LabelNomeV.Text += " " + dr["Marca"].ToString();
                    LabelNomeV.Text += " " + dr["Quantidade"].ToString();
                    LabelPrecoV.Text = dr["Preco"].ToString();
                }
                dr.Close();
                String Getvariedades = "select * from ProdutosVariedades where FKId_Produtos = @Id_Produtos";
                SqlCommand comandoGetvariedades = new SqlCommand(Getvariedades, con);
                comandoGetvariedades.Parameters.AddWithValue("@Id_Produtos", id);
                SqlDataReader dr2 = comandoGetvariedades.ExecuteReader();
                DropDownListVariedades.Items.Clear();
                while (dr2.Read())
                {
                    ListItem ItemdeVariedade = new ListItem();
                    ItemdeVariedade.Text = dr2["Variedade"].ToString();
                    ItemdeVariedade.Value = dr2["Id_ProdutosVariedades"].ToString();
                    DropDownListVariedades.Items.Add(ItemdeVariedade);
                }
                dr2.Close();
                HiddenFieldIdV.Value = id.ToString();
                TextBoxUnidadesV.Text = unidades.ToString();
                UDivVariedades.Visible = true;
            }
            else if (temp2 == 1)
            {
                //Se o produto não tiver nenhuma variedade, insere-se o produto na lista
                String GetAtributos = "select Nome, ProdutosMarcas.Marca, Quantidade, Preco from Produtos left join ProdutosMarcas on Produtos.Marca = ProdutosMarcas.Id_ProdutosMarcas where Id_Produtos=@Id_Produtos";
                SqlCommand comandoGetAtributos = new SqlCommand(GetAtributos, con);
                comandoGetAtributos.Parameters.AddWithValue("@Id_Produtos", id);
                string nome = "";
                string marca = "";
                string quantidade = "";
                float precoporunidade = 0;
                SqlDataReader dr = comandoGetAtributos.ExecuteReader();
                while (dr.Read())
                {
                    nome = dr["Nome"].ToString();
                    marca = dr["Marca"].ToString();
                    quantidade = dr["Quantidade"].ToString();
                    precoporunidade = float.Parse(dr["Preco"].ToString());
                }
                dr.Close();

                String GetPromocao = "select PrecoemDesconto from ProdutosPromocoes where FKId_Produtos=@FKId_Produtos and DatadeInicio <= GETDATE() and DatadeFim >= GETDATE()";
                SqlCommand comandoGetPromocao = new SqlCommand(GetPromocao, con);
                comandoGetPromocao.Parameters.AddWithValue("@FKId_Produtos", id);
                SqlDataReader dr2 = comandoGetPromocao.ExecuteReader();
                while (dr2.Read())
                {
                    precoporunidade = float.Parse(dr2["PrecoemDesconto"].ToString());
                }
                dr2.Close();

                String GetAtributo = "select Id_ProdutosVariedades from ProdutosVariedades where FKId_Produtos=@Id_Produtos";
                SqlCommand comandoGetAtributo = new SqlCommand(GetAtributo, con);
                comandoGetAtributo.Parameters.AddWithValue("@Id_Produtos", id);
                int idv = 0;
                SqlDataReader dr3 = comandoGetAtributo.ExecuteReader();
                while (dr3.Read())
                {
                    idv = Int32.Parse(dr3["Id_ProdutosVariedades"].ToString());
                }
                dr3.Close();

                if (Session["ListadeCompras"] == null) Session["ListadeCompras"] = new ListadeItensdoCarrodeCompras();
                ListadeItensdoCarrodeCompras lista = (ListadeItensdoCarrodeCompras)Session["ListadeCompras"];
                ItemdoCarrodeCompras item;
                item = new ItemdoCarrodeCompras(id, idv, nome, marca, quantidade, precoporunidade, unidades);
                lista.additem(item);
                lista.updateunidadestotaisdalista();
                lista.updateprecototaldalista();
                Session["ListadeCompras"] = lista;
                UpdateListadeCompras();
            }
        }
        con.Close();
    }
    protected void InserirProdutonaLista(int id, int idv, int unidades)
    {
        Autenticate();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        string nome = "";
        string marca = "";
        string quantidade = "";
        float precoporunidade = 0;
        String GetAtributos = "select ProdutosMarcas.Marca, Quantidade, Preco from Produtos left join ProdutosMarcas on Produtos.Marca = ProdutosMarcas.Id_ProdutosMarcas where Id_Produtos=@Id_Produtos";
        SqlCommand comandoGetAtributos = new SqlCommand(GetAtributos, con);
        comandoGetAtributos.Parameters.AddWithValue("@Id_Produtos", id);
        SqlDataReader dr = comandoGetAtributos.ExecuteReader();
        ItemdoCarrodeCompras item;
        while (dr.Read())
        {
            marca = dr["Marca"].ToString();
            quantidade = dr["Quantidade"].ToString();
            precoporunidade = float.Parse(dr["Preco"].ToString());
        }
        dr.Close();
        String GetPromocao = "select PrecoemDesconto from ProdutosPromocoes where FKId_Produtos=@FKId_Produtos and DatadeInicio <= GETDATE() and DatadeFim >= GETDATE()";
        SqlCommand comandoGetPromocao = new SqlCommand(GetPromocao, con);
        comandoGetPromocao.Parameters.AddWithValue("@FKId_Produtos", id);
        SqlDataReader dr2 = comandoGetPromocao.ExecuteReader();
        while (dr2.Read())
        {
            precoporunidade = float.Parse(dr2["PrecoemDesconto"].ToString());
        }
        dr2.Close();
        String GetAtributo = "select Variedade from ProdutosVariedades where Id_ProdutosVariedades=@Id_ProdutosVariedades";
        SqlCommand comandoGetAtributo = new SqlCommand(GetAtributo, con);
        comandoGetAtributo.Parameters.AddWithValue("@Id_ProdutosVariedades", idv);
        SqlDataReader dr3 = comandoGetAtributo.ExecuteReader();
        while (dr3.Read())
        {
            nome = dr3["Variedade"].ToString();
        }
        dr3.Close();
        if (Session["ListadeCompras"] == null) Session["ListadeCompras"] = new ListadeItensdoCarrodeCompras();
        ListadeItensdoCarrodeCompras lista = (ListadeItensdoCarrodeCompras)Session["ListadeCompras"];
        item = new ItemdoCarrodeCompras(id, idv, nome, marca, quantidade, precoporunidade, unidades);
        lista.additem(item);
        lista.updateunidadestotaisdalista();
        lista.updateprecototaldalista();
        Session["ListadeCompras"] = lista;
        UpdateListadeCompras();
        UDivVariedades.Visible = false;
    }
    //Atualizar a divisória de lista de compras
    protected void UpdateListadeCompras()
    {
        Autenticate();
        if (Session["ListadeCompras"] != null)
        {
            UDivMenudeCompras.Visible = true;
            ListadeItensdoCarrodeCompras lista = (ListadeItensdoCarrodeCompras)Session["ListadeCompras"];
            lista.updateunidadestotaisdalista();
            lista.updateprecototaldalista();
            int length = lista.getlength();
            if (length == 0)
            {
                Session["ListadeCompras"] = new ListadeItensdoCarrodeCompras();
                LabelListadeComprasUnidades.Text = "A sua lista está vazia.";
                LabelListadeComprasPrecoTotal.Text = "";
                LabelListadeComprasPrecoTotal.Visible = false;
            }
            else if (length == 1)
            {
                LabelListadeComprasUnidades.Text = "Tem " + lista.getunidadestotaisdalista().ToString() + " unidade no seu carrinho.";
                LabelListadeComprasPrecoTotal.Text = "Preco Total: " + lista.getprecototaldalista().ToString() + "€";
                LabelListadeComprasPrecoTotal.Visible = true;
            }
            else if (length > 1)
            {
                LabelListadeComprasUnidades.Text = "Tem " + lista.getunidadestotaisdalista().ToString() + " unidades no seu carrinho.";
                LabelListadeComprasPrecoTotal.Text = "Preco Total: " + lista.getprecototaldalista().ToString() + "€";
                LabelListadeComprasPrecoTotal.Visible = true;
            }
        }
    }
    
    //Proceder para o pagamento
    protected void ButtonProceder_Click(object sender, EventArgs e)
    {
        Autenticate();
        if (Session["ListadeCompras"] == null) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua lista de compras está vazia.');", true);
        else Response.Redirect("Pagamento.aspx");
    }
}
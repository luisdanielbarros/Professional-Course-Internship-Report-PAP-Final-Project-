using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminProdutos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) IPModo(1);
        else if (string.IsNullOrWhiteSpace(TextBoxIPNome.Text) && (DropDownListIPMarca.SelectedIndex != 0) && (DropDownListIPCategoria.SelectedIndex != 0) && (DropDownListIPSubcategoria.SelectedIndex != 0) && string.IsNullOrWhiteSpace(TextBoxIPPreco.Text) && string.IsNullOrWhiteSpace(TextBoxIPQuantidade.Text) && string.IsNullOrWhiteSpace(TextBoxIPVariedade.Text) && (DropDownListIPVariedades.Items.Count > 0)) IPModo(1);
    }
    //Produtos
    //Atualizar os Dropdowns relativos às Subcategorias e à filtragem
    protected void DropDownListIPCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListIPSubcategoria.DataBind();
    }
    protected void DropDownListFiltrarCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListFiltrarSubcategoria.DataBind();
    }
    protected void DropDownListFiltrarCategoria_DataBound(object sender, EventArgs e)
    {
        DropDownListFiltrarCategoria.Items.Insert(0, new ListItem("Todas as categorias", "-1"));
    }
    protected void DropDownListFiltrarSubcategoria_DataBound(object sender, EventArgs e)
    {
        DropDownListFiltrarSubcategoria.Items.Insert(0, new ListItem("Todas as subcategorias", "-1"));
    }
    /*
    1 - Modo de inserção, campos vazios - Usado após Inserir, Apagar e Tirar Seleção
    2 - Após escolhida a opção de o produto ser vendido ao peso - Usado no evento do RadioButtonList
    3 - Após escolhida a opção de o produto ser vendido à unidade - Usado no evento do RadioButtonList
    4 - Modo de edição - Usado após seleccionado o produto na gridview
    5 - Após escolhida a opção de o produto não ter variedades
    6 - Após escolhida a opção de o produto ter variedades
    */
    protected void IPModo(int modo)
    {
        //Modo de inserção, campos vazios
        if (modo == 1)
        {
            TextBoxIPNome.Text = "";
            if (DropDownListIPMarca.Items.Count > 0) DropDownListIPMarca.SelectedIndex = 0;
            if (DropDownListIPCategoria.Items.Count > 0)
            {
                DropDownListIPCategoria.SelectedIndex = 0;
                DropDownListIPSubcategoria.DataBind();
            }
            if (DropDownListIPSubcategoria.Items.Count > 0) DropDownListIPSubcategoria.SelectedIndex = 0;
            CheckBoxIPVariedade.Checked = true;
            TextBoxIPPreco.Text = "";
            RadioButtonListIPPesoouUnid.SelectedIndex = -1;
            TextBoxIPPreco.Enabled = false;
            LabelIPPrecoporPesoouUnid.Text = "";
            TextBoxIPQuantidade.Text = "";
            TextBoxIPQuantidade.Enabled = false;
            DropDownListIPQuantidade.Enabled = false;
            ButtonIP.Enabled = true;
            ButtonIP.Visible = true;
            ButtonEP.Enabled = false;
            ButtonEP.Visible = false;
            ButtonAP.Enabled = false;
            ButtonAP.Visible = false;
            ButtonEPTS.Enabled = false;
            ButtonEPTS.Visible = false;
            RequiredFieldValidatorIPImagem.Enabled = true;
            DropDownListIPVariedades.Items.Clear();
            GridViewIP.SelectedIndex = -1;
            GridViewIPV.SelectedIndex = -1;
            TextBoxIPNome.Focus();
        }
        //Modo de edição, após seleccionado o produto na gridview
        else if (modo == 2)
        {
            //Configuração do que fica vísivel e invisível no modo de edição
            ButtonIP.Enabled = false;
            ButtonIP.Visible = false;
            ButtonEP.Enabled = true;
            ButtonEP.Visible = true;
            ButtonAP.Enabled = true;
            ButtonAP.Visible = true;
            ButtonEPTS.Enabled = true;
            ButtonEPTS.Visible = true;
            DropDownListIPVariedades.Items.Clear();
            DropDownListIPVariedades.Enabled = true;
            //Preenchimento dos campos
            TextBoxIPNome.Text = GridViewIP.SelectedRow.Cells[2].Text;
            DropDownListIPMarca.ClearSelection();
            DropDownListIPCategoria.ClearSelection();
            DropDownListIPSubcategoria.ClearSelection();
            DropDownListIPQuantidade.ClearSelection();
            if (DropDownListIPMarca.Items.FindByValue(GridViewIP.SelectedRow.Cells[10].Text) == null) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Esta marca não existe na base de dados (Verifique se foi apagada).');", true);
            else DropDownListIPMarca.SelectedValue = GridViewIP.SelectedRow.Cells[10].Text;
            if (DropDownListIPCategoria.Items.FindByValue(GridViewIP.SelectedRow.Cells[11].Text) == null) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Esta categoria não existe na base de dados (Verifique se foi apagada).');", true);
            else
            {
                DropDownListIPCategoria.SelectedValue = GridViewIP.SelectedRow.Cells[11].Text;
                DropDownListIPSubcategoria.DataBind();
            }
            if (DropDownListIPSubcategoria.Items.FindByValue(GridViewIP.SelectedRow.Cells[12].Text) == null) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Esta subcategoria não existe na base de dados (Verifique se foi apagada).');", true);
            else DropDownListIPSubcategoria.SelectedValue = GridViewIP.SelectedRow.Cells[12].Text;
            TextBoxIPPreco.Text = GridViewIP.SelectedRow.Cells[8].Text;
            TextBoxIPPreco.Enabled = true;
            if (GridViewIP.SelectedRow.Cells[13].Text == "False")
            {
                RadioButtonListIPPesoouUnid.SelectedIndex = 0;
                TextBoxIPQuantidade.Text = "";
                TextBoxIPQuantidade.Enabled = false;
                DropDownListIPQuantidade.Enabled = false;
                LabelIPPrecoporPesoouUnid.Text = "ao Kg";
            }
            else if (GridViewIP.SelectedRow.Cells[13].Text == "True")
            {
                RadioButtonListIPPesoouUnid.SelectedIndex = 1;
                TextBoxIPQuantidade.Text = "";
                int j = 0;
                do
                {
                    TextBoxIPQuantidade.Text += GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1);
                    j++;
                } while (GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == "x" | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == " " | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == "0" | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == "1" | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == "2" | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == "3" | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == "4" | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == "5" | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == "6" | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == "7" | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == "8" | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == "9" | GridViewIP.SelectedRow.Cells[9].Text.Substring(j, 1) == ",");
                try
                {
                    DropDownListIPQuantidade.Items.FindByValue(GridViewIP.SelectedRow.Cells[9].Text.Substring(j, GridViewIP.SelectedRow.Cells[9].Text.Length - j)).Selected = true;
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Erro.');", true);
                }
                j = 0;
                TextBoxIPQuantidade.Enabled = true;
                DropDownListIPQuantidade.Enabled = true;
            }
            LabelIPPrecoporPesoouUnid.Text = "por unidade.";
            //relativo à imagem
            FileUploadIPImagem.Attributes.Clear();
            RequiredFieldValidatorIPImagem.Enabled = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            String Procurarvariedades = "select * from ProdutosVariedades where FKId_Produtos = @FKId_Produtos";
            SqlCommand comando = new SqlCommand(Procurarvariedades, con);
            comando.Parameters.AddWithValue("@FKId_Produtos", GridViewIP.SelectedRow.Cells[1].Text);
            SqlDataReader dr = comando.ExecuteReader();
            DropDownListIPVariedades.Items.Clear();
            while (dr.Read())
            {
                if (dr["Variedade"].ToString() == "Sem variedades")
                {
                    CheckBoxIPVariedade.Checked = true;
                    TextBoxIPVariedade.Enabled = false;
                    ButtonIPIVariedade.Enabled = false;
                    ButtonIPAVariedade.Enabled = false;
                    DropDownListIPVariedades.Enabled = false;
                }
                else
                {
                    CheckBoxIPVariedade.Checked = false;
                    TextBoxIPVariedade.Enabled = true;
                    ButtonIPIVariedade.Enabled = true;
                    ButtonIPAVariedade.Enabled = true;
                    DropDownListIPVariedades.Enabled = true;
                }
                DropDownListIPVariedades.Items.Add(new ListItem(dr["Variedade"].ToString()));
            }
            dr.Close();
            con.Close();
        }
    }
    protected void GridViewIP_SelectedIndexChanged(object sender, EventArgs e)
    {
        IPModo(2);
    }
    //Tirar Selecção
    protected void ButtonEPTS_Click(object sender, EventArgs e)
    {
        IPModo(1);
    }
    //Fazer Resize à imagem antes de a inserir na base de dados
    public static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
    {
        return (System.Drawing.Image)(new Bitmap(imgToResize, size));
    }
    //Inserir
    protected void ButtonIP_Click(object sender, EventArgs e)
    {
        if (CheckBoxIPVariedade.Checked == false && DropDownListIPVariedades.Items.Count == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Impossível inserir, o seu Colectivo de Produtos contém variedades, no entanto a sua lista de variedades encontra-se vazia.');", true);
        }
        else
        {
            string filename = TextBoxIPNome.Text + " " + DropDownListIPMarca.SelectedItem + " " + TextBoxIPQuantidade.Text + DropDownListIPQuantidade.SelectedValue + " " + DropDownListIPCategoria.SelectedItem + " " + DropDownListIPSubcategoria.SelectedItem + ".png";
            filename = filename.Replace("/", "_");
            //Se o diretório não existir, cria-se
            if (!Directory.Exists(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem)) Directory.CreateDirectory(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            String Procurarrepetidos = "select count(*) from Produtos where Nome = @Nome and Marca = @Marca and Quantidade = @Quantidade";
            SqlCommand comando = new SqlCommand(Procurarrepetidos, con);
            comando.Parameters.AddWithValue("@Nome", TextBoxIPNome.Text);
            comando.Parameters.AddWithValue("@Marca", DropDownListIPMarca.SelectedValue);
            comando.Parameters.AddWithValue("@Quantidade", TextBoxIPQuantidade.Text + DropDownListIPQuantidade.SelectedValue);
            int temp = Convert.ToInt32(comando.ExecuteScalar().ToString());
            if (temp > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Impossível inserir, o Colectivo de Produtos que pretende inserir já existe na base de dados. Já existe um Colectivo de Produtos com o mesmo Nome, Marca e Quantidade por Unidade.');", true);
            }
            else if (temp == 0)
            {
                Guid newGUID = Guid.NewGuid();
                String Inserirproduto = "insert into Produtos (Nome,Marca,Imagem,Subcategoria,PesoUnidade,Preco,Quantidade,DisponivelOnline) values (@Nome,@Marca,@Imagem,@Subcategoria,@PesoUnidade,@Preco,@Quantidade,@DisponivelOnline)";
                SqlCommand comando2 = new SqlCommand(Inserirproduto, con);
                comando2.Parameters.AddWithValue("@Nome", TextBoxIPNome.Text);
                comando2.Parameters.AddWithValue("@Marca", DropDownListIPMarca.SelectedValue);
                comando2.Parameters.AddWithValue("@Imagem", "~/Imagens/Produtos/" + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/" + filename);
                comando2.Parameters.AddWithValue("@Subcategoria", DropDownListIPSubcategoria.SelectedValue);
                comando2.Parameters.AddWithValue("@PesoUnidade", RadioButtonListIPPesoouUnid.SelectedValue);
                comando2.Parameters.AddWithValue("@Preco", TextBoxIPPreco.Text);
                if (RadioButtonListIPPesoouUnid.SelectedValue == "0") comando2.Parameters.AddWithValue("@Quantidade", "");
                else if (RadioButtonListIPPesoouUnid.SelectedValue == "1") comando2.Parameters.AddWithValue("@Quantidade", TextBoxIPQuantidade.Text + DropDownListIPQuantidade.SelectedValue);
                comando2.Parameters.AddWithValue("@DisponivelOnline", true);
                comando2.ExecuteNonQuery();
                String GetIdProdutos = "select Id_Produtos from Produtos where Nome = @Nome and Marca = @Marca and Imagem = @Imagem and Subcategoria = @Subcategoria and PesoUnidade = @PesoUnidade and Preco = @Preco and Quantidade = @Quantidade";
                SqlCommand comando3 = new SqlCommand(GetIdProdutos, con);
                comando3.Parameters.AddWithValue("@Nome", TextBoxIPNome.Text);
                comando3.Parameters.AddWithValue("@Marca", DropDownListIPMarca.SelectedValue);
                comando3.Parameters.AddWithValue("@Imagem", "~/Imagens/Produtos/" + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/" + filename);
                comando3.Parameters.AddWithValue("@Subcategoria", DropDownListIPSubcategoria.SelectedValue);
                comando3.Parameters.AddWithValue("@PesoUnidade", RadioButtonListIPPesoouUnid.SelectedValue);
                comando3.Parameters.AddWithValue("@Preco", TextBoxIPPreco.Text);
                if (RadioButtonListIPPesoouUnid.SelectedValue == "0") comando3.Parameters.AddWithValue("@Quantidade", "");
                else if (RadioButtonListIPPesoouUnid.SelectedValue == "1") comando3.Parameters.AddWithValue("@Quantidade", TextBoxIPQuantidade.Text + DropDownListIPQuantidade.SelectedValue);
                int IdProdutos = Convert.ToInt32(comando3.ExecuteScalar().ToString());
                String Inserirvariedades = "insert into ProdutosVariedades (FKId_Produtos,Variedade,Inventario) values (@FKId_Produtos,@Variedade,@Inventario)";
                SqlCommand comando4 = new SqlCommand(Inserirvariedades, con);
                comando4.Parameters.AddWithValue("@FKId_Produtos", IdProdutos);
                comando4.Parameters.AddWithValue("@Inventario", 200);
                if (CheckBoxIPVariedade.Checked == false)
                {
                    foreach (ListItem ltItem in DropDownListIPVariedades.Items)
                    {
                        if (comando4.Parameters.Contains("@Variedade")) comando4.Parameters.RemoveAt("@Variedade");
                        comando4.Parameters.AddWithValue("@Variedade", ltItem.Text);
                        comando4.ExecuteNonQuery();
                    }
                }
                else if (CheckBoxIPVariedade.Checked == true)
                {
                    comando4.Parameters.AddWithValue("@Variedade", "Sem variedades");
                    comando4.ExecuteNonQuery();
                }
                if ((System.IO.File.Exists(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/" + filename))) System.IO.File.Delete(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/" + filename);
                FileUploadIPImagem.SaveAs(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/" + filename);
                GridViewIP.DataBind();
                GridViewIPV.DataBind();
                IPModo(1);
                GridViewIP.PageIndex = GridViewIP.PageCount - 1;
                GridViewIPV.PageIndex = GridViewIPV.PageCount - 1;
            }
            con.Close();
        }
    }
    //Editar
    protected void ButtonEP_Click(object sender, EventArgs e)
    {
        if (CheckBoxIPVariedade.Checked == false && DropDownListIPVariedades.Items.Count == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Impossível inserir, o seu Colectivo de Produtos contém variedades, no entanto a sua lista de variedades encontra-se vazia.');", true);
        }
        else
        {
            //Verificar que o diretório aonde a imagem será guardada existe, se não existir cria-se
            if (!Directory.Exists(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem)) Directory.CreateDirectory(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            //Verificar que não há um produto diferente na base de dados com o mesmo nome, marca e quantidade
            String Procurarrepetidos = "select count(*) from Produtos where Nome = @Nome and Marca = @Marca and Quantidade = @Quantidade and Id_Produtos != @Id_Produtos";
            SqlCommand comando = new SqlCommand(Procurarrepetidos, con);
            comando.Parameters.AddWithValue("@Nome", TextBoxIPNome.Text);
            comando.Parameters.AddWithValue("@Marca", DropDownListIPMarca.SelectedValue);
            comando.Parameters.AddWithValue("@Quantidade", TextBoxIPQuantidade.Text + DropDownListIPQuantidade.SelectedValue);
            comando.Parameters.AddWithValue("@Id_Produtos", GridViewIP.SelectedRow.Cells[1].Text);
            int temp = Convert.ToInt32(comando.ExecuteScalar().ToString());
            if (temp > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Impossível editar, o Colectivo de Produtos que pretende editar já existe na base de dados sob um Id diferente. Já existe um Colectivo de Produtos com o mesmo Nome, Marca, Quantidade por Unidade mas Id diferentes.');", true);
            }
            else if (temp == 0)
            {
                //Obter o link da imagem do produto
                string filename = TextBoxIPNome.Text + " " + DropDownListIPMarca.SelectedItem + " " + TextBoxIPQuantidade.Text + DropDownListIPQuantidade.SelectedValue + " " + DropDownListIPCategoria.SelectedItem + " " + DropDownListIPSubcategoria.SelectedItem + ".png";
                filename = filename.Replace("/", "_");
                String GetImagem = "select Imagem from Produtos where Id_Produtos=@Id_Produtos";
                SqlCommand comandoGetImagem = new SqlCommand(GetImagem, con);
                comandoGetImagem.Parameters.AddWithValue("@Id_Produtos", GridViewIP.SelectedRow.Cells[1].Text);
                string PathdaImagem = "";
                if (comandoGetImagem.ExecuteScalar() != null) PathdaImagem = comandoGetImagem.ExecuteScalar().ToString();
                //Começar a preparar o query de atualização do produto
                String EditarProduto = "update Produtos set Nome = @Nome, Imagem = @Imagem, Marca = @Marca, Subcategoria = @Subcategoria, PesoUnidade = @PesoUnidade, Preco = @Preco, Quantidade = @Quantidade where Id_Produtos = @Id_Produtos";
                SqlCommand comandoEditarProduto = new SqlCommand(EditarProduto, con);
                comandoEditarProduto.Parameters.AddWithValue("@Id_Produtos", GridViewIP.SelectedRow.Cells[1].Text);
                comandoEditarProduto.Parameters.AddWithValue("@Nome", TextBoxIPNome.Text);
                comandoEditarProduto.Parameters.AddWithValue("@Marca", DropDownListIPMarca.SelectedValue);
                comandoEditarProduto.Parameters.AddWithValue("@Subcategoria", DropDownListIPSubcategoria.SelectedValue);
                comandoEditarProduto.Parameters.AddWithValue("@PesoUnidade", RadioButtonListIPPesoouUnid.SelectedValue);
                comandoEditarProduto.Parameters.AddWithValue("@Preco", double.Parse(TextBoxIPPreco.Text));
                if (RadioButtonListIPPesoouUnid.SelectedValue == "0")
                {
                    comandoEditarProduto.Parameters.AddWithValue("@Quantidade", "");
                }
                else if (RadioButtonListIPPesoouUnid.SelectedValue == "1")
                {
                    comandoEditarProduto.Parameters.AddWithValue("@Quantidade", TextBoxIPQuantidade.Text + DropDownListIPQuantidade.SelectedValue);
                }
                //Verificar as diferentes possibilidades face à atualização da imagem
                //Caso a imagem seja editada
                if (FileUploadIPImagem.HasFile == true)
                {
                    if (!Directory.Exists(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem)) Directory.CreateDirectory(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem);
                    if ((System.IO.File.Exists(Server.MapPath(PathdaImagem)))) System.IO.File.Delete(Server.MapPath(PathdaImagem));
                    FileUploadIPImagem.SaveAs(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/" + filename);
                    comandoEditarProduto.Parameters.AddWithValue("@Imagem", "~/Imagens/Produtos/" + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/" + filename);
                }
                //Caso a imagem não seja editada, mas outros atributos do produto o tenham sido
                else if ("~/Imagens/Produtos/" + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/" + filename != PathdaImagem)
                {
                    //Se o novo diretório a que a imagem passará a pertencer não existir, cria-se o novo diretório, e tenta-se mover o ficheiro
                    //ao mover o ficheiro, se o link da imagem, guardado na base de dados, não corresponder a nenhuma imagem nos diretórios, o programa entra no catch e avisa de que a imagem não foi encontrada
                    if (!Directory.Exists(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem)) Directory.CreateDirectory(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem);
                    try
                    {
                        System.IO.File.Move(Server.MapPath(PathdaImagem), Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/ a mover " + filename);
                        System.IO.File.Move(Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/ a mover " + filename, Server.MapPath("~/Imagens/Produtos/") + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/" + filename);
                        comandoEditarProduto.Parameters.AddWithValue("@Imagem", "~/Imagens/Produtos/" + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/" + filename);
                    }
                    catch
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A imagem não foi encontrada. Note que não se devem modificar as imagens nos seus diretórios em (~/Imagens/Produtos/...).');", true);
                        comandoEditarProduto.Parameters.AddWithValue("@Imagem", "~/Imagens/Produtos/" + DropDownListIPCategoria.SelectedItem + "/" + DropDownListIPSubcategoria.SelectedItem + "/" + DropDownListIPMarca.SelectedItem + "/" + filename);
                    }
                }
                else
                {
                    comandoEditarProduto.Parameters.AddWithValue("@Imagem", PathdaImagem);
                }
                comandoEditarProduto.ExecuteNonQuery();
                //Após a atualização do produto, segue-se a atualização das variedades do produto
                String Procurarvariedades = "select * from ProdutosVariedades where FKId_Produtos = @FKId_Produtos";
                SqlCommand comando2 = new SqlCommand(Procurarvariedades, con);
                comando2.Parameters.AddWithValue("@FKId_Produtos", GridViewIP.SelectedRow.Cells[1].Text);
                SqlDataReader dr = comando2.ExecuteReader();
                string[] variedades = new string[0];
                while (dr.Read())
                {
                    Array.Resize(ref variedades, variedades.Length + 1);
                    variedades[variedades.Length - 1] = dr["variedade"].ToString();
                }
                dr.Close();
                for (int i = 0; i < variedades.Length; i++)
                {
                    if (!DropDownListIPVariedades.Items.Contains(new ListItem(variedades[i].ToString())))
                    {
                        String Apagarvariedades = "delete from ProdutosVariedades WHERE FKId_produtos = @FKId_Produtos and Variedade = @Variedade";
                        SqlCommand comandoApagarvariedades = new SqlCommand(Apagarvariedades, con);
                        comandoApagarvariedades.Parameters.AddWithValue("@FKId_Produtos", GridViewIP.SelectedRow.Cells[1].Text);
                        comandoApagarvariedades.Parameters.AddWithValue("@Variedade", variedades[i].ToString());
                        comandoApagarvariedades.ExecuteNonQuery();
                    }
                }
                foreach (ListItem ltItem in DropDownListIPVariedades.Items)
                {
                    if (!variedades.Contains(ltItem.Text))
                    {
                        String Inserirvariedades = "insert into ProdutosVariedades (FKId_Produtos,Variedade,Inventario) values (@FKId_Produtos,@Variedade,@Inventario)";
                        SqlCommand comandoInserirvariedades = new SqlCommand(Inserirvariedades, con);
                        comandoInserirvariedades.Parameters.AddWithValue("@FKId_Produtos", GridViewIP.SelectedRow.Cells[1].Text);
                        comandoInserirvariedades.Parameters.AddWithValue("@Variedade", ltItem.Text);
                        comandoInserirvariedades.Parameters.AddWithValue("@Inventario", 200);
                        comandoInserirvariedades.ExecuteNonQuery();
                    }
                }
                con.Close();
                GridViewIP.DataBind();
                GridViewIPV.DataBind();
            }
        }
    }
    //Apagar
    protected void ButtonAP_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String GetImagem = "select Imagem from Produtos where Id_Produtos = @Id_Produtos";
        SqlCommand comandoGetImagem = new SqlCommand(GetImagem, con);
        comandoGetImagem.Parameters.AddWithValue("@Id_Produtos", GridViewIP.SelectedRow.Cells[1].Text);
        string PathdaImagem = comandoGetImagem.ExecuteScalar().ToString();
        if ((System.IO.File.Exists(Server.MapPath(PathdaImagem)))) System.IO.File.Delete(Server.MapPath(PathdaImagem));
        String Apagarvariedades = "delete from ProdutosVariedades WHERE FKId_produtos = @FKId_Produtos";
        SqlCommand comando2 = new SqlCommand(Apagarvariedades, con);
        comando2.Parameters.AddWithValue("@FKId_Produtos", GridViewIP.SelectedRow.Cells[1].Text);
        comando2.ExecuteNonQuery();
        String Apagarproduto = "delete from Produtos WHERE Id_Produtos = @Id_Produtos";
        SqlCommand comando = new SqlCommand(Apagarproduto, con);
        comando.Parameters.AddWithValue("@Id_Produtos", GridViewIP.SelectedRow.Cells[1].Text);
        comando.ExecuteNonQuery();
        con.Close();
        GridViewIP.DataBind();
        GridViewIPV.DataBind();
        IPModo(1);
    }
    ////Inserir Atigo na Lista de Variedades
    protected void ButtonIPIVariedade_Click(object sender, EventArgs e)
    {
        if (TextBoxIPVariedade.Text.Trim() == "") ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua caixa de texto está vazia.');", true);
        else if (DropDownListIPVariedades.Items.Contains(new ListItem(TextBoxIPVariedade.Text))) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Este produto já está listado.');", true);
        else
        {
            if (DropDownListIPVariedades.Items.Count == 1 && DropDownListIPVariedades.Items[0].Text == "Sem variedades") DropDownListIPVariedades.Items.RemoveAt(0);
            DropDownListIPVariedades.Items.Add(TextBoxIPVariedade.Text);
            TextBoxIPVariedade.Text = "";
        }
    }
    //Apagar Lista de Variedades
    protected void ButtonIPAVariedade_Click(object sender, EventArgs e)
    {
        if (DropDownListIPVariedades.Items.Count > 0) DropDownListIPVariedades.Items.RemoveAt(DropDownListIPVariedades.SelectedIndex);
        else ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua lista de variedades está vazia.');", true);
    }
}
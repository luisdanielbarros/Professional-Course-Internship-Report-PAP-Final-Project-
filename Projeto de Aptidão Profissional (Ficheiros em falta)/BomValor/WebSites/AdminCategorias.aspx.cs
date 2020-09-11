using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminCategorias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) viewmode(1);
    }
    protected void viewmode(int mode)
    {
        switch (mode)
        {
            case 1:
                TextBoxCategoria.Text = "";
                GridViewCategoria.SelectedIndex = -1;
                ButtonInserir.Enabled = true;
                ButtonInserir.Visible = true;
                ButtonEditar.Enabled = false;
                ButtonEditar.Visible = false;
                ButtonApagar.Enabled = false;
                ButtonApagar.Visible = false;
                ButtonModoInserir.Enabled = false;
                ButtonModoInserir.Visible = false;
                break;
            case 2:
                TextBoxCategoria.Text = GridViewCategoria.SelectedRow.Cells[2].Text;
                ButtonInserir.Enabled = false;
                ButtonInserir.Visible = false;
                ButtonEditar.Enabled = true;
                ButtonEditar.Visible = true;
                ButtonApagar.Enabled = true;
                ButtonApagar.Visible = true;
                ButtonModoInserir.Enabled = true;
                ButtonModoInserir.Visible = true;
                break;
        }
    }
    protected void GridViewCategorias_SelectedIndexChanged(object sender, EventArgs e)
    {
        viewmode(2);
    }
    protected void ButtonInserir_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String ProcurarCategoriasRepetidas = "SELECT COUNT(*) FROM ProdutosCategorias WHERE Categoria=@Categoria";
        SqlCommand comandoProcurarCategoriasRepetidas = new SqlCommand(ProcurarCategoriasRepetidas, con);
        comandoProcurarCategoriasRepetidas.Parameters.AddWithValue("@Categoria", TextBoxCategoria.Text);
        int CategoriasRepetidas = Convert.ToInt32(comandoProcurarCategoriasRepetidas.ExecuteScalar().ToString());
        if (CategoriasRepetidas > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Esta Categoria já existe na base de dados.');", true);
        else if (CategoriasRepetidas == 0)
        {
            String InserirCategoria = "INSERT INTO ProdutosCategorias (Categoria) values (@Categoria)";
            SqlCommand comandoInserirCategoria = new SqlCommand(InserirCategoria, con);
            comandoInserirCategoria.Parameters.AddWithValue("@Categoria", TextBoxCategoria.Text);
            comandoInserirCategoria.ExecuteNonQuery();
            GridViewCategoria.DataBind();
            viewmode(1);
        }
        con.Close();
    }
    protected void ButtonEditar_Click(object sender, EventArgs e)
    {
        if (GridViewCategoria.SelectedValue != null && GridViewCategoria.SelectedIndex > -1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            String ProcurarCategoriasRepetidas = "SELECT COUNT(*) FROM ProdutosCategorias WHERE Categoria=@Categoria and Id_ProdutosCategorias!=@Id_ProdutosCategorias";
            SqlCommand comandoProcurarCategoriasRepetidas = new SqlCommand(ProcurarCategoriasRepetidas, con);
            comandoProcurarCategoriasRepetidas.Parameters.AddWithValue("@Categoria", TextBoxCategoria.Text);
            comandoProcurarCategoriasRepetidas.Parameters.AddWithValue("@Id_ProdutosCategorias", GridViewCategoria.SelectedRow.Cells[1].Text);
            int CategoriasRepetidas = Convert.ToInt32(comandoProcurarCategoriasRepetidas.ExecuteScalar().ToString());
            if (CategoriasRepetidas > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Já existe uma Categoria com o mesmo nome na base de dados.');", true);
            else if (CategoriasRepetidas == 0)
            {
                String EditarCategoria = "UPDATE ProdutosCategorias SET Categoria=@Categoria WHERE Id_ProdutosCategorias=@Id_ProdutosCategorias";
                SqlCommand comandoEditarCategoria = new SqlCommand(EditarCategoria, con);
                comandoEditarCategoria.Parameters.AddWithValue("@Categoria", TextBoxCategoria.Text);
                comandoEditarCategoria.Parameters.AddWithValue("@Id_ProdutosCategorias", GridViewCategoria.SelectedRow.Cells[1].Text);
                comandoEditarCategoria.ExecuteNonQuery();
                GridViewCategoria.DataBind();
            }
            con.Close();
        }
        else ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Seleccione a Categoria.');", true);
    }
    protected void ButtonApagar_Click(object sender, EventArgs e)
    {
        if (GridViewCategoria.SelectedValue != null && GridViewCategoria.SelectedIndex > -1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            String VerificarProdutos = "SELECT COUNT(Produtos.Id_Produtos) FROM ProdutosCategorias LEFT JOIN ProdutosSubcategorias on ProdutosCategorias.Id_ProdutosCategorias=ProdutosSubcategorias.FKId_ProdutosCategorias LEFT JOIN Produtos ON ProdutosSubcategorias.Id_ProdutosSubcategorias=Produtos.Subcategoria WHERE ProdutosCategorias.Id_ProdutosCategorias=@Categoria";
            SqlCommand comandoVerificarProdutos = new SqlCommand(VerificarProdutos, con);
            comandoVerificarProdutos.Parameters.AddWithValue("@Categoria", GridViewCategoria.SelectedRow.Cells[1].Text);
            int Produtos = Int32.Parse(comandoVerificarProdutos.ExecuteScalar().ToString());
            if (Produtos > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Existem produtos com esta categoria atribuída, por favor certifique-se que esta categoria não está associada a nenhuns produtos para a poder apagar.');", true);
            else
            {
                String ApagarCategoria = "DELETE FROM ProdutosCategorias WHERE Id_ProdutosCategorias=@Id_ProdutosCategorias";
                SqlCommand comandoApagarCategoria = new SqlCommand(ApagarCategoria, con);
                comandoApagarCategoria.Parameters.AddWithValue("@Id_ProdutosCategorias", GridViewCategoria.SelectedRow.Cells[1].Text);
                comandoApagarCategoria.ExecuteNonQuery();
                GridViewCategoria.DataBind();
                viewmode(1);
            }
            con.Close();
        }
        else ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Selecciona a Categoria.');", true);
    }
    protected void ButtonModoInserir_Click(object sender, EventArgs e)
    {
        viewmode(1);
    }
}
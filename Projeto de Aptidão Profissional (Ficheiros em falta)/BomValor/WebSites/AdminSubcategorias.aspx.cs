using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminSubcategorias : System.Web.UI.Page
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
                TextBoxSubcategoria.Text = "";
                GridViewSubcategoria.SelectedIndex = -1;
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
                TextBoxSubcategoria.Text = GridViewSubcategoria.SelectedRow.Cells[2].Text;
                DropDownListCategoria.SelectedValue = GridViewSubcategoria.SelectedRow.Cells[4].Text;
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
    protected void DropDownListProcurar_DataBound(object sender, EventArgs e)
    {
        DropDownListProcurar.Items.Insert(0, new ListItem("Todas as subcategorias", "-1"));
    }
    protected void GridViewSubcategorias_SelectedIndexChanged(object sender, EventArgs e)
    {
        viewmode(2);
    }
    protected void ButtonInserir_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String ProcurarSubcategoriasRepetidas = "SELECT COUNT(*) FROM ProdutosSubcategorias WHERE Subcategoria=@Subcategoria AND FKId_ProdutosCategorias=@FKId_ProdutosCategorias";
        SqlCommand comandoProcurarSubcategoriasRepetidas = new SqlCommand(ProcurarSubcategoriasRepetidas, con);
        comandoProcurarSubcategoriasRepetidas.Parameters.AddWithValue("@Subcategoria", TextBoxSubcategoria.Text);
        comandoProcurarSubcategoriasRepetidas.Parameters.AddWithValue("@FKId_ProdutosCategorias", DropDownListCategoria.SelectedValue);
        int SubcategoriasRepetidas = Convert.ToInt32(comandoProcurarSubcategoriasRepetidas.ExecuteScalar().ToString());
        if (SubcategoriasRepetidas > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Esta Subcategoria já existe na base de dados.');", true);
        else if (SubcategoriasRepetidas == 0)
        {

            String InserirSubcategoria = "INSERT INTO ProdutosSubcategorias (Subcategoria,FKId_ProdutosCategorias) values (@Subcategoria,@FKId_ProdutosCategorias)";
            SqlCommand comandoInserirSubcategoria = new SqlCommand(InserirSubcategoria, con);
            comandoInserirSubcategoria.Parameters.AddWithValue("@Subcategoria", TextBoxSubcategoria.Text);
            comandoInserirSubcategoria.Parameters.AddWithValue("@FKId_ProdutosCategorias", DropDownListCategoria.SelectedValue);
            comandoInserirSubcategoria.ExecuteNonQuery();
            GridViewSubcategoria.DataBind();
            viewmode(1);
        }
        con.Close();
    }
    protected void ButtonEditar_Click(object sender, EventArgs e)
    {
        if (GridViewSubcategoria.SelectedValue != null && GridViewSubcategoria.SelectedIndex > -1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            String ProcurarSubcategoriasRepetidas = "SELECT COUNT(*) FROM ProdutosSubcategorias WHERE Subcategoria=@Subcategoria and FKId_ProdutosCategorias=@FKId_ProdutosCategorias and Id_ProdutosSubcategorias!=@Id_ProdutosSubcategorias";
            SqlCommand comandoProcurarSubcategoriasRepetidas = new SqlCommand(ProcurarSubcategoriasRepetidas, con);
            comandoProcurarSubcategoriasRepetidas.Parameters.AddWithValue("@Subcategoria", TextBoxSubcategoria.Text);
            comandoProcurarSubcategoriasRepetidas.Parameters.AddWithValue("@FKId_ProdutosCategorias", DropDownListCategoria.SelectedValue);
            comandoProcurarSubcategoriasRepetidas.Parameters.AddWithValue("@Id_ProdutosSubcategorias", GridViewSubcategoria.SelectedRow.Cells[1].Text);
            int SubcategoriasRepetidas = Convert.ToInt32(comandoProcurarSubcategoriasRepetidas.ExecuteScalar().ToString());
            if (SubcategoriasRepetidas > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Já existe uma Subcategoria com o mesmo nome na base de dados.');", true);
            else if (SubcategoriasRepetidas == 0)
            {
                if (DropDownListCategoria.SelectedValue != GridViewSubcategoria.SelectedRow.Cells[4].Text)
                {
                    String AtualizarProdutos = "update Produtos set Categoria=@Categoria WHERE Subcategoria=@Subcategoria";
                    SqlCommand comandoAtualizarProdutos = new SqlCommand(AtualizarProdutos, con);
                    comandoAtualizarProdutos.Parameters.AddWithValue("@Categoria", DropDownListCategoria.SelectedValue);
                    comandoAtualizarProdutos.Parameters.AddWithValue("@Subcategoria", GridViewSubcategoria.SelectedRow.Cells[1].Text);
                    comandoAtualizarProdutos.ExecuteNonQuery();
                }
                String EditarSubcategoria = "UPDATE ProdutosSubcategorias SET Subcategoria=@Subcategoria, FKId_ProdutosCategorias=@FKId_ProdutosCategorias WHERE Id_ProdutosSubcategorias=@Id_ProdutosSubcategorias";
                SqlCommand comandoEditarSubcategoria = new SqlCommand(EditarSubcategoria, con);
                comandoEditarSubcategoria.Parameters.AddWithValue("@Subcategoria", TextBoxSubcategoria.Text);
                comandoEditarSubcategoria.Parameters.AddWithValue("@FKId_ProdutosCategorias", DropDownListCategoria.SelectedValue);
                comandoEditarSubcategoria.Parameters.AddWithValue("@Id_ProdutosSubcategorias", GridViewSubcategoria.SelectedRow.Cells[1].Text);
                comandoEditarSubcategoria.ExecuteNonQuery();
                GridViewSubcategoria.DataBind();
            }
            con.Close();
        }
        else ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Seleccione a Subcategoria.');", true);
    }
    protected void ButtonApagar_Click(object sender, EventArgs e)
    {
        if (GridViewSubcategoria.SelectedValue != null && GridViewSubcategoria.SelectedIndex > -1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            String VerificarProdutos = "SELECT COUNT(Produtos.Id_Produtos) FROM Produtos WHERE Subcategoria=@Id_ProdutosSubcategorias";
            SqlCommand comandoVerificarProdutos = new SqlCommand(VerificarProdutos, con);
            comandoVerificarProdutos.Parameters.AddWithValue("@Id_ProdutosSubcategorias", GridViewSubcategoria.SelectedRow.Cells[1].Text);
            int Produtos = Int32.Parse(comandoVerificarProdutos.ExecuteScalar().ToString());
            if (Produtos > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Existem produtos com esta subcategoria atribuída, por favor certifique-se que esta subcategoria não está associada a nenhuns produtos para a poder apagar.');", true);
            else
            {
                String ApagarSubcategoria = "DELETE FROM ProdutosSubcategorias WHERE Id_ProdutosSubcategorias=@Id_ProdutosSubcategorias";
                SqlCommand comandoApagarSubcategoria = new SqlCommand(ApagarSubcategoria, con);
                comandoApagarSubcategoria.Parameters.AddWithValue("@Id_ProdutosSubcategorias", GridViewSubcategoria.SelectedRow.Cells[1].Text);
                comandoApagarSubcategoria.ExecuteNonQuery();
                GridViewSubcategoria.DataBind();
                viewmode(1);
            }
            con.Close();
        }
        else ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Selecciona a Subcategoria.');", true);
    }
    protected void ButtonModoInserir_Click(object sender, EventArgs e)
    {
        viewmode(1);
    }
}
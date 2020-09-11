using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMarcas : System.Web.UI.Page
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
                TextBoxMarca.Text = "";
                GridViewMarca.SelectedIndex = -1;
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
                TextBoxMarca.Text = GridViewMarca.SelectedRow.Cells[2].Text;
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
    protected void GridViewMarcas_SelectedIndexChanged(object sender, EventArgs e)
    {
        viewmode(2);
    }
    protected void ButtonInserir_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String ProcurarMarcasRepetidas = "SELECT COUNT(*) FROM ProdutosMarcas WHERE Marca=@Marca";
        SqlCommand comandoProcurarMarcasRepetidas = new SqlCommand(ProcurarMarcasRepetidas, con);
        comandoProcurarMarcasRepetidas.Parameters.AddWithValue("@Marca", TextBoxMarca.Text);
        int MarcasRepetidas = Convert.ToInt32(comandoProcurarMarcasRepetidas.ExecuteScalar().ToString());
        if (MarcasRepetidas > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Esta marca já existe na base de dados.');", true);
        else if (MarcasRepetidas == 0)
        {
            String InserirMarca = "INSERT INTO ProdutosMarcas (Marca) values (@Marca)";
            SqlCommand comandoInserirMarca = new SqlCommand(InserirMarca, con);
            comandoInserirMarca.Parameters.AddWithValue("@Marca", TextBoxMarca.Text);
            comandoInserirMarca.ExecuteNonQuery();
            GridViewMarca.DataBind();
            viewmode(1);
        }
        con.Close();
    }
    protected void ButtonEditar_Click(object sender, EventArgs e)
    {
        if (GridViewMarca.SelectedValue != null && GridViewMarca.SelectedIndex > -1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            String ProcurarMarcasRepetidas = "SELECT COUNT(*) FROM ProdutosMarcas WHERE Marca=@Marca and Id_ProdutosMarcas!=@Id_ProdutosMarcas";
            SqlCommand comandoProcurarMarcasRepetidas = new SqlCommand(ProcurarMarcasRepetidas, con);
            comandoProcurarMarcasRepetidas.Parameters.AddWithValue("@Marca", TextBoxMarca.Text);
            comandoProcurarMarcasRepetidas.Parameters.AddWithValue("@Id_ProdutosMarcas", GridViewMarca.SelectedRow.Cells[1].Text);
            int MarcasRepetidas = Convert.ToInt32(comandoProcurarMarcasRepetidas.ExecuteScalar().ToString());
            if (MarcasRepetidas > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Já existe uma Marca com o mesmo nome na base de dados.');", true);
            else if (MarcasRepetidas == 0)
            {
                String EditarMarca = "UPDATE ProdutosMarcas SET Marca=@Marca WHERE Id_ProdutosMarcas=@Id_ProdutosMarcas";
                SqlCommand comandoEditarMarca = new SqlCommand(EditarMarca, con);
                comandoEditarMarca.Parameters.AddWithValue("@Marca", TextBoxMarca.Text);
                comandoEditarMarca.Parameters.AddWithValue("@Id_ProdutosMarcas", GridViewMarca.SelectedRow.Cells[1].Text);
                comandoEditarMarca.ExecuteNonQuery();
                GridViewMarca.DataBind();
            }
            con.Close();
        }
        else ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Seleccione a marca.');", true);
    }
    protected void ButtonApagar_Click(object sender, EventArgs e)
    {
        if (GridViewMarca.SelectedValue != null && GridViewMarca.SelectedIndex > -1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            String VerificarProdutos = "SELECT COUNT(Produtos.Id_Produtos) FROM Produtos WHERE Marca=@Id_ProdutosMarcas";
            SqlCommand comandoVerificarProdutos = new SqlCommand(VerificarProdutos, con);
            comandoVerificarProdutos.Parameters.AddWithValue("@Id_ProdutosMarcas", GridViewMarca.SelectedRow.Cells[1].Text);
            int Produtos = Int32.Parse(comandoVerificarProdutos.ExecuteScalar().ToString());
            if (Produtos > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Existem produtos com esta marca atribuída, por favor certifique-se que esta marca não está associada a nenhuns produtos para a poder apagar.');", true);
            else
            {
                String ApagarMarca = "DELETE FROM ProdutosMarcas WHERE Id_ProdutosMarcas=@Id_ProdutosMarcas";
                SqlCommand comandoApagarMarca = new SqlCommand(ApagarMarca, con);
                comandoApagarMarca.Parameters.AddWithValue("@Id_ProdutosMarcas", GridViewMarca.SelectedRow.Cells[1].Text);
                comandoApagarMarca.ExecuteNonQuery();
                GridViewMarca.DataBind();
                viewmode(1);
            }
            con.Close();
        }
        else ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Selecciona a marca.');", true);
    }
    protected void ButtonModoInserir_Click(object sender, EventArgs e)
    {
        viewmode(1);
    }
}
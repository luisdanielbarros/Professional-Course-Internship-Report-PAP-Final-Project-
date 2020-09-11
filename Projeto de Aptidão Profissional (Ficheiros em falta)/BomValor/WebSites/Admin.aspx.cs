using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Administrador"] != null)
        {
            Response.Redirect("AdminProdutos.aspx");
        }
    }
    protected void ButtonAutenticar_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String Autenticar = "SELECT Id_Administrador FROM Administradores WHERE Numero=@Numero AND Senha=@Senha";
        SqlCommand comandoAutenticar = new SqlCommand(Autenticar, con);
        comandoAutenticar.Parameters.AddWithValue("@Numero", TextBoxNumero.Text);
        comandoAutenticar.Parameters.AddWithValue("@Senha", TextBoxSenha.Text);
        int Id_Administrador = Convert.ToInt32(comandoAutenticar.ExecuteScalar());
        if (Id_Administrador > 0)
        {
            String InserirManutencao = "INSERT INTO Manutencoes (FKId_Administrador, Tipo, Inicio) VALUES (@FKId_Administrador, 1, GETDATE())";
            SqlCommand comandoInserirManutencao = new SqlCommand(InserirManutencao, con);
            comandoInserirManutencao.Parameters.AddWithValue("@FKId_Administrador", Id_Administrador);
            comandoInserirManutencao.ExecuteNonQuery();
            string IniciarManutencao = "UPDATE EmManutencao SET Estado=1";
            SqlCommand comandoIniciarManutencao = new SqlCommand(IniciarManutencao, con);
            comandoIniciarManutencao.ExecuteNonQuery();
            Session["Administrador"] = Id_Administrador;
            Response.Redirect("AdminProdutos.aspx");
        }
        else ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Autenticação errada.');", true);
    }
}
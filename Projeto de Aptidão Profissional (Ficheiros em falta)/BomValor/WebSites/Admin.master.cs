using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Administrador"] == null)
        {
            Response.Redirect("Admin.aspx");
        }
    }
    protected void LinkButtonTerminar_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String TerminarManutencao = "UPDATE Manutencoes SET Fim=GETDATE() WHERE FKId_Administrador=@FKId_Administrador";
        SqlCommand comandoTerminarManutencao = new SqlCommand(TerminarManutencao, con);
        comandoTerminarManutencao.Parameters.AddWithValue("@FKId_Administrador", Session["Administrador"]);
        comandoTerminarManutencao.ExecuteNonQuery();
        string FinalizarManutencao = "UPDATE EmManutencao SET Estado=0";
        SqlCommand comandoFinalizarManutencao = new SqlCommand(FinalizarManutencao, con);
        comandoFinalizarManutencao.ExecuteNonQuery();
        Session["Administrador"] = null;
        Response.Redirect("Admin.aspx");
    }
}

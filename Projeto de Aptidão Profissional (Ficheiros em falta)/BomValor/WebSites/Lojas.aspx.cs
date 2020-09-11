using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lojas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Utilizador"] != null)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            String ProcurarLocalidade = "select Localidade from Contas where Id_Contas=@Id_Contas";
            SqlCommand comandoProcurarLocalidade = new SqlCommand(ProcurarLocalidade, con);
            comandoProcurarLocalidade.Parameters.AddWithValue("@Id_Contas", Session["Utilizador"].ToString());
            SqlDataReader dr = comandoProcurarLocalidade.ExecuteReader();
            while (dr.Read())
            {
                DropDownListLojas.SelectedIndex = DropDownListLojas.Items.IndexOf(DropDownListLojas.Items.FindByText(dr["Localidade"].ToString()));
            }
            dr.Close();
            con.Close();
        }
    }
}
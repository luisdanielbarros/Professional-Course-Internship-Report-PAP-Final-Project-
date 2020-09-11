using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Utilizador"] == null)
        {
            Visitante.Visible = true;
            Utilizador.Visible = false;
        }
        else
        {
            Visitante.Visible = false;
            Utilizador.Visible = true;
            LabelUtilizador.Text = Session["Nome_Utilizador"].ToString();
        }
    }
    protected void LinkButtonLogout_Click(object sender, EventArgs e)
    {
        Session["Utilizador"] = null;
        Session["Nome_Utilizador"] = null;
        Session["Email_Utilizador"] = null;
        Session["ListadeCompras"] = null;
        Session["EmailporConfirmar"] = null;
        Session["EmailporConfirmarFalhanoEnvio"] = null;
        Session["SenhaporMudar"] = null;
        Response.Redirect("Produtos.aspx");
    }
}

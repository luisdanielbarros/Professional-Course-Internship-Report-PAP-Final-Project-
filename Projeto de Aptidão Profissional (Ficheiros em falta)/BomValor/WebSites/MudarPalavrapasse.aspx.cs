using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MudarPalavrapasse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SenhaporMudar"] == null && Session["SenhaporMudar2"] == null) Response.Redirect("Login.aspx");
        if (Session["SenhaporMudar"] != null)
        {
            DivPalavrapassepresente.Visible = false;
            RequiredFieldValidatorTextBoxPalavrapassepresente.Enabled = false;
            RegularExpressionValidatorTextBoxPalavrapassepresente.Enabled = false;
        }
    }
    protected void ButtonMudarPalavrapasse_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        int temp = 1;
        if (Session["SenhaporMudar2"] != null)
        {
            String ConfirmarPalavrapasse = "SELECT COUNT(*) FROM Contas WHERE Palavrapasse=@Palavrapasse AND Id_Contas=@Id_Contas";
            SqlCommand comandoConfirmarPalavrapasse = new SqlCommand(ConfirmarPalavrapasse, con);
            comandoConfirmarPalavrapasse.Parameters.AddWithValue("@Palavrapasse", TextBoxPalavrapassepresente.Text);
            comandoConfirmarPalavrapasse.Parameters.AddWithValue("@Id_Contas", Session["SenhaporMudar2"].ToString());
            comandoConfirmarPalavrapasse.ExecuteNonQuery();
            temp = Int32.Parse(comandoConfirmarPalavrapasse.ExecuteScalar().ToString());
        }
        if (temp == 0) ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('A palavra-passe inserida está errada.');", true);
        else
        {
            String MudarPalavrapasse = "UPDATE Contas SET Palavrapasse=@Palavrapasse WHERE Id_Contas=@Id_Contas";
            SqlCommand comandoMudarPalavrapasse = new SqlCommand(MudarPalavrapasse, con);
            comandoMudarPalavrapasse.Parameters.AddWithValue("@Palavrapasse", TextBoxPalavrapasse.Text);
            if (Session["SenhaporMudar"] != null) comandoMudarPalavrapasse.Parameters.AddWithValue("@Id_Contas", Session["SenhaporMudar"].ToString());
            else comandoMudarPalavrapasse.Parameters.AddWithValue("@Id_Contas", Session["SenhaporMudar2"].ToString());
            comandoMudarPalavrapasse.ExecuteNonQuery();
            con.Close();
            Session["SenhaporMudar"] = null;
            Session["SenhaporMudar2"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('A sua palavra-passe foi alterada com sucesso.'); window.location.href = 'Perfil.aspx';", true);
        }
    }
    protected void RedirectToMudarPalavrapasse(object sender, EventArgs e)
    {
        Session["SenhaporMudar"] = Session["Utilizador"];
        Response.Redirect("MudarPalavrapasse.aspx");
    }
}
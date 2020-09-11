using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Perfil : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Utilizador"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else if (!Page.IsPostBack)
        {
            ButtonAlterar.Visible = true;
            ButtonEditar.Visible = false;
            ButtonCancelar.Visible = false;
            TextBoxCodigoPostal.Enabled = false;
            DropDownListLocalidade.Enabled = false;
            TextBoxMorada.Enabled = false;
            ButtonENotificarAlterar.Visible = true;
            ButtonENotificarEditar.Visible = false;
            ButtonENotificarCancelar.Visible = false;
            DropDownListENotificar.Enabled = false;
            AtualizarCampos();
        }
    }
    protected void AtualizarCampos()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String ProcurarInformacao = "select Email, Nome, DatadeNascimento, CodigoPostal, Localidade, Morada, ENotificar from Contas where Id_Contas=@Id_Contas";
        SqlCommand comandoProcurarInformacao = new SqlCommand(ProcurarInformacao, con);
        comandoProcurarInformacao.Parameters.AddWithValue("@Id_Contas", Session["Utilizador"].ToString());
        SqlDataReader dr = comandoProcurarInformacao.ExecuteReader();
        while (dr.Read())
        {
            LabelEmail.Text = dr["Email"].ToString();
            LabelNome.Text = dr["Nome"].ToString();
            DateTime Datadenascimento = DateTime.Parse(dr["DatadeNascimento"].ToString());
            LabelDatadenascimento.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(Datadenascimento.DayOfWeek).ToLower()) + ", " + Datadenascimento.Day + " de " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Datadenascimento.Month).ToLower()) + " de " + Datadenascimento.Year;
            TextBoxCodigoPostal.Text = dr["CodigoPostal"].ToString();
            DropDownListLocalidade.SelectedValue = dr["Localidade"].ToString();
            TextBoxMorada.Text = dr["Morada"].ToString();
            if (bool.Parse(dr["ENotificar"].ToString()) == false) DropDownListENotificar.SelectedValue = "0";
            else DropDownListENotificar.SelectedValue = "1";
        }
        dr.Close();
        con.Close();
    }
    protected void ButtonAlterar_Click(object sender, EventArgs e)
    {
        ButtonAlterar.Visible = false;
        ButtonEditar.Visible = true;
        ButtonCancelar.Visible = true;
        TextBoxCodigoPostal.Enabled = true;
        DropDownListLocalidade.Enabled = true;
        TextBoxMorada.Enabled = true;
    }
    protected void ButtonEditar_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String AtualizarInformacao = "update Contas set CodigoPostal=@CodigoPostal, Localidade=@Localidade, Morada=@Morada where Id_Contas=@Id_Contas";
        SqlCommand comandoAtualizarInformacao = new SqlCommand(AtualizarInformacao, con);
        comandoAtualizarInformacao.Parameters.AddWithValue("@CodigoPostal", TextBoxCodigoPostal.Text);
        comandoAtualizarInformacao.Parameters.AddWithValue("@Localidade", DropDownListLocalidade.SelectedItem.ToString());
        comandoAtualizarInformacao.Parameters.AddWithValue("@Morada", TextBoxMorada.Text);
        comandoAtualizarInformacao.Parameters.AddWithValue("@Id_Contas", Session["Utilizador"].ToString());
        comandoAtualizarInformacao.ExecuteNonQuery();
        con.Close();
        ButtonAlterar.Visible = true;
        ButtonEditar.Visible = false;
        ButtonCancelar.Visible = false;
        TextBoxCodigoPostal.Enabled = false;
        DropDownListLocalidade.Enabled = false;
        TextBoxMorada.Enabled = false;
        AtualizarCampos();
    }
    protected void ButtonCancelar_Click(object sender, EventArgs e)
    {
        ButtonAlterar.Visible = true;
        ButtonEditar.Visible = false;
        ButtonCancelar.Visible = false;
        TextBoxCodigoPostal.Enabled = false;
        DropDownListLocalidade.Enabled = false;
        TextBoxMorada.Enabled = false;
    }
    protected void ButtonMudarPlavrapasse_Click(object sender, EventArgs e)
    {
        Session["SenhaporMudar2"] = Session["Utilizador"];
        Response.Redirect("MudarPalavrapasse.aspx");
    }
    protected void ButtonENotificarAlterar_Click(object sender, EventArgs e)
    {
        ButtonENotificarAlterar.Visible = false;
        ButtonENotificarEditar.Visible = true;
        ButtonENotificarCancelar.Visible = true;
        DropDownListENotificar.Enabled = true;
    }
    protected void ButtonENotificarEditar_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String AtualizarInformacao = "update Contas set ENotificar=@ENotificar where Id_Contas=@Id_Contas";
        SqlCommand comandoAtualizarInformacao = new SqlCommand(AtualizarInformacao, con);
        bool ENotificar = false;
        if (DropDownListENotificar.SelectedValue == "0") ENotificar = false;
        else ENotificar = true;
        comandoAtualizarInformacao.Parameters.AddWithValue("@ENotificar", ENotificar);
        comandoAtualizarInformacao.Parameters.AddWithValue("@Id_Contas", Session["Utilizador"].ToString());
        comandoAtualizarInformacao.ExecuteNonQuery();
        con.Close();
        ButtonENotificarAlterar.Visible = true;
        ButtonENotificarEditar.Visible = false;
        ButtonENotificarCancelar.Visible = false;
        DropDownListENotificar.Enabled = false;
        AtualizarCampos();
    }
    protected void ButtonENotificarCancelar_Click(object sender, EventArgs e)
    {
        ButtonENotificarAlterar.Visible = true;
        ButtonENotificarEditar.Visible = false;
        ButtonENotificarCancelar.Visible = false;
        DropDownListENotificar.Enabled = false;
    }

}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pagamento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Utilizador"] == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua sessão expirou.');", true);
            Response.Redirect("Login.aspx");
        }
        else if (Session["ListadeCompras"] == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua lista de compras está vazia.');", true);
            Response.Redirect("Produtos.aspx");
        }
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            String ProcurarInformacao = "select Email, Nome, CodigoPostal, Localidade, Morada from Contas where Id_Contas=@Id_Contas";
            SqlCommand comandoProcurarInformacao = new SqlCommand(ProcurarInformacao, con);
            comandoProcurarInformacao.Parameters.AddWithValue("@Id_Contas", Session["Utilizador"].ToString());
            SqlDataReader dr = comandoProcurarInformacao.ExecuteReader();
            while (dr.Read())
            {
                LabelEmail.Text = dr["Email"].ToString();
                LabelNome.Text = dr["Nome"].ToString();
                LabelCodigoPostal.Text = dr["CodigoPostal"].ToString();
                LabelLocalidade.Text = dr["Localidade"].ToString();
                LabelMorada.Text = dr["Morada"].ToString();
            }
            dr.Close();
            con.Close();
        }
    }
    protected void ButtonPagar_Click(object sender, EventArgs e)
    {
        //Se o utilizador ter sessão
        //if (Session["ListadeCompras"] == null) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua lista de compras está vazia.');", true);
        //else
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        //    con.Open();
        //    //Descartar qualquer lista de compras previamente guardada
        //    String DescartarListadeCompras = "UPDATE ListasdeCompras SET Estado='descartada' WHERE FkId_Contas=@FKId_Contas AND Estado='guardada'";
        //    SqlCommand comandoDescartarListadeCompras = new SqlCommand(DescartarListadeCompras, con);
        //    comandoDescartarListadeCompras.Parameters.AddWithValue("@FKId_Contas", Session["Utilizador"]);
        //    comandoDescartarListadeCompras.ExecuteNonQuery();
        //    //Guardar a nova lista de compras
        //    String GuardarListadeCompras = "INSERT INTO ListasdeCompras (FKId_Contas, Data, Estado) VALUES (@FKId_Contas, @Data, 'comprada')";
        //    SqlCommand comandoGuardarListadeCompras = new SqlCommand(GuardarListadeCompras, con);
        //    comandoGuardarListadeCompras.Parameters.AddWithValue("@FKId_Contas", Session["Utilizador"].ToString());
        //    comandoGuardarListadeCompras.Parameters.AddWithValue("@Data", DateTime.Now);
        //    comandoGuardarListadeCompras.ExecuteNonQuery();
        //    String GetIddaListadeComprasGuardada = "SELECT Id_ListasdeCompras FROM ListasdeCompras WHERE FKId_Contas=@FKId_Contas AND Estado='comprada'";
        //    SqlCommand comandoGetIddaListadeComprasGuardada = new SqlCommand(GetIddaListadeComprasGuardada, con);
        //    comandoGetIddaListadeComprasGuardada.Parameters.AddWithValue("@FKId_Contas", Session["Utilizador"].ToString());
        //    int IdListadeComprasGuardada = Int32.Parse(comandoGetIddaListadeComprasGuardada.ExecuteScalar().ToString());
        //    ListadeItensdoCarrodeCompras lista = (ListadeItensdoCarrodeCompras)Session["ListadeCompras"];
        //    foreach (ItemdoCarrodeCompras item in lista)
        //    {
        //        String GuardarEntradanaListadeCompras = "INSERT INTO ListasdeComprasEntradas (FKId_ListasdeCompras, FKId_ProdutosVariedades, Unidades, PrecoTotal) VALUES (@FKId_ListasdeCompras, @FKId_ProdutosVariedades, @Unidades, @PrecoTotal)";
        //        SqlCommand comandoGuardarEntradanaListadeCompras = new SqlCommand(GuardarEntradanaListadeCompras, con);
        //        comandoGuardarEntradanaListadeCompras.Parameters.AddWithValue("@FKId_ListasdeCompras", IdListadeComprasGuardada);
        //        comandoGuardarEntradanaListadeCompras.Parameters.AddWithValue("@FKId_ProdutosVariedades", item.getidv());
        //        comandoGuardarEntradanaListadeCompras.Parameters.AddWithValue("@Unidades", item.getunidades());
        //        comandoGuardarEntradanaListadeCompras.Parameters.AddWithValue("@PrecoTotal", item.getprecototal());
        //        comandoGuardarEntradanaListadeCompras.ExecuteNonQuery();
        //    }
        //    con.Close();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Esta função encontra-se incompleta devido à impossibilidade de implementar um protocolo de pagamento legítimo, assim sendo, a sua compra foi devidamente registada.'); window.location.href = 'Produtos.aspx';", true);
        //}
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Esta função encontra-se incompleta devido à impossibilidade de implementar um protocolo de pagamento legítimo.'); window.location.href = 'Produtos.aspx';", true);
    }
}
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Adminpromocoes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) viewmode(1);
    }
    protected void DropDownListFiltrarCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListFiltrarSubcategoria.DataBind();
    }
    protected void DropDownListFiltrarCategoria_DataBound(object sender, EventArgs e)
    {
        DropDownListFiltrarCategoria.Items.Insert(0, new ListItem("Todas as categorias", "-1"));
    }
    protected void DropDownListFiltrarSubcategoria_DataBound(object sender, EventArgs e)
    {
        DropDownListFiltrarSubcategoria.Items.Insert(0, new ListItem("Todas as subcategorias", "-1"));
    }
    protected void viewmode(int mode)
    {
        if (mode == 1)
        {
            LabelProduto.Text = "";
            LabelPreco.Text = "";
            GridViewProdutos.SelectedIndex = -1;
            GridViewPromocoes.SelectedIndex = -1;
            GridViewPromocoesPassadas.SelectedIndex = -1;
            TextBoxDataInicial.Text = "";
            TextBoxDataFinal.Text = "";
            TextBoxPercentagemdeDesconto.Text = "";
            TextBoxPrecoemDesconto.Text = "";
            TextBoxPoupanca.Text = "";
            ButtonInserir.Enabled = false;
            ButtonInserir.Visible = false;
            ButtonEditar.Enabled = false;
            ButtonEditar.Visible = false;
            ButtonApagar.Enabled = false;
            ButtonApagar.Visible = false;
            ButtonModoInserir.Enabled = false;
            ButtonModoInserir.Visible = false;
            LabelSeleccione.Visible = true;
        }
        else if (mode == 2)
        {
            LabelProduto.Text = GridViewProdutos.SelectedRow.Cells[2].Text;
            LabelPreco.Text = GridViewProdutos.SelectedRow.Cells[3].Text + " €";
            GridViewPromocoes.SelectedIndex = -1;
            GridViewPromocoesPassadas.SelectedIndex = -1;
            TextBoxDataInicial.Text = "";
            TextBoxDataFinal.Text = "";
            TextBoxPercentagemdeDesconto.Text = "";
            TextBoxPrecoemDesconto.Text = "";
            TextBoxPoupanca.Text = "";
            ButtonInserir.Enabled = true;
            ButtonInserir.Visible = true;
            ButtonEditar.Enabled = false;
            ButtonEditar.Visible = false;
            ButtonApagar.Enabled = false;
            ButtonApagar.Visible = false;
            ButtonModoInserir.Enabled = false;
            ButtonModoInserir.Visible = false;
            LabelSeleccione.Visible = false;
        }
        else if (mode == 3)
        {
            LabelProduto.Text = GridViewPromocoes.SelectedRow.Cells[2].Text;
            LabelPreco.Text = GridViewPromocoes.SelectedRow.Cells[3].Text + " €";
            GridViewProdutos.SelectedIndex = -1;
            GridViewPromocoesPassadas.SelectedIndex = -1;
            TextBoxDataInicial.Text = GridViewPromocoes.SelectedRow.Cells[5].Text;
            TextBoxDataFinal.Text = GridViewPromocoes.SelectedRow.Cells[6].Text;
            TextBoxPercentagemdeDesconto.Text = GridViewPromocoes.SelectedRow.Cells[7].Text;
            TextBoxPrecoemDesconto.Text = GridViewPromocoes.SelectedRow.Cells[8].Text;
            TextBoxPoupanca.Text = GridViewPromocoes.SelectedRow.Cells[9].Text;
            ButtonInserir.Enabled = false;
            ButtonInserir.Visible = false;
            ButtonEditar.Enabled = true;
            ButtonEditar.Visible = true;
            ButtonApagar.Enabled = true;
            ButtonApagar.Visible = true;
            ButtonModoInserir.Enabled = true;
            ButtonModoInserir.Visible = true;
            LabelSeleccione.Visible = false;
        }
        else if (mode == 4)
        {
            LabelProduto.Text = GridViewPromocoesPassadas.SelectedRow.Cells[2].Text;
            LabelPreco.Text = GridViewPromocoesPassadas.SelectedRow.Cells[3].Text + " €";
            GridViewProdutos.SelectedIndex = -1;
            GridViewPromocoes.SelectedIndex = -1;
            TextBoxDataInicial.Text = GridViewPromocoesPassadas.SelectedRow.Cells[5].Text;
            TextBoxDataFinal.Text = GridViewPromocoesPassadas.SelectedRow.Cells[6].Text;
            TextBoxPercentagemdeDesconto.Text = GridViewPromocoesPassadas.SelectedRow.Cells[7].Text;
            TextBoxPrecoemDesconto.Text = GridViewPromocoesPassadas.SelectedRow.Cells[8].Text;
            TextBoxPoupanca.Text = GridViewPromocoesPassadas.SelectedRow.Cells[9].Text;
            ButtonInserir.Enabled = false;
            ButtonInserir.Visible = false;
            ButtonEditar.Enabled = true;
            ButtonEditar.Visible = true;
            ButtonApagar.Enabled = true;
            ButtonApagar.Visible = true;
            ButtonModoInserir.Enabled = true;
            ButtonModoInserir.Visible = true;
            LabelSeleccione.Visible = false;
        }
    }
    protected void GridViewProdutos_SelectedIndexChanged(object sender, EventArgs e)
    {
        viewmode(2);
    }
    protected void GridViewPromocoes_SelectedIndexChanged(object sender, EventArgs e)
    {
        viewmode(3);
    }
    protected void GridViewPromocoesPassadas_SelectedIndexChanged(object sender, EventArgs e)
    {
        viewmode(4);
    }
    protected void ButtonCalcular_Click(object sender, EventArgs e)
    {
        float PercentagemdeDesconto = float.Parse(TextBoxPercentagemdeDesconto.Text) / 100;
        float PrecoOriginal = float.Parse(LabelPreco.Text.Substring(0, LabelPreco.Text.Length - 2));
        double Poupanca = PrecoOriginal * PercentagemdeDesconto;
        double PrecoemDesconto = PrecoOriginal - Poupanca;
        double Resto = (Math.Round(Poupanca, 3) + Math.Round(PrecoemDesconto, 3)) - (Math.Round(Poupanca, 2) + Math.Round(PrecoemDesconto, 2));
        if (Resto < 0)
        {
            PrecoemDesconto += Resto;
            Resto = 0;
        }
        Poupanca = Math.Round(Poupanca, 2) + Resto;
        PrecoemDesconto = Math.Round(PrecoemDesconto, 2);
        TextBoxPrecoemDesconto.Text = PrecoemDesconto.ToString();
        TextBoxPoupanca.Text = Poupanca.ToString();
    }
    protected void ButtonInserir_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String Procurarrepetidos = "select count(*) from ProdutosPromocoes where FKId_Produtos = @FKId_Produtos and @DatadeFim >= DatadeInicio and @DatadeInicio <= DatadeFim";
        SqlCommand comando = new SqlCommand(Procurarrepetidos, con);
        comando.Parameters.AddWithValue("@FKId_Produtos", GridViewProdutos.SelectedRow.Cells[1].Text);
        DateTime DataInicial = DateTime.ParseExact(TextBoxDataInicial.Text, "dd/MM/yyyy", null);
        DateTime DataFinal = DateTime.ParseExact(TextBoxDataFinal.Text, "dd/MM/yyyy", null);
        comando.Parameters.AddWithValue("@DatadeInicio", DataInicial);
        comando.Parameters.AddWithValue("@DatadeFim", DataFinal);
        int temp = Convert.ToInt32(comando.ExecuteScalar().ToString());
        if (temp > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Já existe uma promoção para o mesmo produto, entre estes intervalos de tempo.');", true);
        }
        else if (temp == 0)
        {
            String Inserirpromocao = "insert into ProdutosPromocoes (FKId_Produtos, DatadeInicio, DatadeFim, PercentagemdeDesconto, PrecoemDesconto, DinheiroPoupado) values (@FKId_Produtos, @DatadeInicio, @DatadeFim, @PercentagemdeDesconto, @PrecoemDesconto, @DinheiroPoupado)";
            SqlCommand comandoInserir = new SqlCommand(Inserirpromocao, con);
            comandoInserir.Parameters.AddWithValue("@FKId_Produtos", GridViewProdutos.SelectedRow.Cells[1].Text);
            comandoInserir.Parameters.AddWithValue("@DatadeInicio", DataInicial);
            comandoInserir.Parameters.AddWithValue("@DatadeFim", DataFinal);
            comandoInserir.Parameters.AddWithValue("@PercentagemdeDesconto", Math.Round(float.Parse(TextBoxPercentagemdeDesconto.Text), 2));
            comandoInserir.Parameters.AddWithValue("@PrecoemDesconto", Math.Round(float.Parse(TextBoxPrecoemDesconto.Text), 2));
            comandoInserir.Parameters.AddWithValue("@DinheiroPoupado", Math.Round(float.Parse(TextBoxPoupanca.Text), 2));
            comandoInserir.ExecuteNonQuery();
            GridViewProdutos.DataBind();
            GridViewPromocoes.DataBind();
            GridViewPromocoesPassadas.DataBind();
            viewmode(1);
        }
        con.Close();
    }
    protected void ButtonEditar_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String Procurarrepetidos = "select count(*) from ProdutosPromocoes where FKId_Produtos=@FKId_Produtos and Id_ProdutosPromocoes!=@Id_ProdutosPromocoes and ((@DatadeInicio between DatadeInicio and DatadeFim) or (@DatadeFim between DatadeInicio and DatadeFim))";
        SqlCommand comando = new SqlCommand(Procurarrepetidos, con);
        if (GridViewPromocoes.SelectedIndex > -1)
        {
            comando.Parameters.AddWithValue("@FKId_Produtos", GridViewPromocoes.SelectedRow.Cells[1].Text);
            comando.Parameters.AddWithValue("@Id_ProdutosPromocoes", GridViewPromocoes.SelectedRow.Cells[4].Text);
        }
        else if (GridViewPromocoesPassadas.SelectedIndex > -1)
        {
            comando.Parameters.AddWithValue("@FKId_Produtos", GridViewPromocoesPassadas.SelectedRow.Cells[1].Text);
            comando.Parameters.AddWithValue("@Id_ProdutosPromocoes", GridViewPromocoesPassadas.SelectedRow.Cells[4].Text);
        }
        DateTime DataInicial = DateTime.ParseExact(TextBoxDataInicial.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime DataFinal = DateTime.ParseExact(TextBoxDataFinal.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        comando.Parameters.AddWithValue("@DatadeInicio", DataInicial);
        comando.Parameters.AddWithValue("@DatadeFim", DataFinal);
        int temp = Convert.ToInt32(comando.ExecuteScalar().ToString());
        if (temp > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Já existe uma promoção para o mesmo produto, entre estes intervalos de tempo.');", true);
        }
        else if (temp == 0)
        {
            String Editarpromocao = "update ProdutosPromocoes set DatadeInicio=@DatadeInicio, DatadeFim=@DatadeFim, PercentagemdeDesconto=@PercentagemdeDesconto, PrecoemDesconto=@PrecoemDesconto, DinheiroPoupado=@DinheiroPoupado where Id_ProdutosPromocoes=@Id_ProdutosPromocoes";
            SqlCommand comandoEditar = new SqlCommand(Editarpromocao, con);
            if (GridViewPromocoes.SelectedIndex > -1)
            {
                comandoEditar.Parameters.AddWithValue("@Id_ProdutosPromocoes", GridViewPromocoes.SelectedRow.Cells[4].Text);
            }
            else if (GridViewPromocoesPassadas.SelectedIndex > -1)
            {
                comandoEditar.Parameters.AddWithValue("@Id_ProdutosPromocoes", GridViewPromocoesPassadas.SelectedRow.Cells[4].Text);
            }
            comandoEditar.Parameters.AddWithValue("@DatadeInicio", DataInicial);
            comandoEditar.Parameters.AddWithValue("@DatadeFim", DataFinal);
            comandoEditar.Parameters.AddWithValue("@PercentagemdeDesconto", Math.Round(float.Parse(TextBoxPercentagemdeDesconto.Text), 2));
            comandoEditar.Parameters.AddWithValue("@PrecoemDesconto", Math.Round(float.Parse(TextBoxPrecoemDesconto.Text), 2));
            comandoEditar.Parameters.AddWithValue("@DinheiroPoupado", Math.Round(float.Parse(TextBoxPoupanca.Text), 2));
            comandoEditar.ExecuteNonQuery();
            GridViewProdutos.DataBind();
            GridViewPromocoes.DataBind();
            GridViewPromocoesPassadas.DataBind();
        }
        con.Close();
    }
    protected void ButtonApagar_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String Apagarpromocao = "delete from ProdutosPromocoes where Id_ProdutosPromocoes=@Id_ProdutosPromocoes";
        SqlCommand comandoApagar = new SqlCommand(Apagarpromocao, con);
        if (GridViewPromocoes.SelectedIndex > -1)
        {
            comandoApagar.Parameters.AddWithValue("@Id_ProdutosPromocoes", GridViewPromocoes.SelectedRow.Cells[4].Text);

        }
        else if (GridViewPromocoesPassadas.SelectedIndex > -1)
        {
            comandoApagar.Parameters.AddWithValue("@Id_ProdutosPromocoes", GridViewPromocoesPassadas.SelectedRow.Cells[4].Text);
        }
        comandoApagar.ExecuteNonQuery();
        con.Close();
        GridViewProdutos.DataBind();
        GridViewPromocoes.DataBind();
        viewmode(1);
    }
    protected void ButtonModoInserir_Click(object sender, EventArgs e)
    {
        viewmode(1);
    }
}
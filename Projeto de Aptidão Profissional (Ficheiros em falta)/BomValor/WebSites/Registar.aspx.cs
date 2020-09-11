using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Registar : System.Web.UI.Page
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
            Response.Redirect("Produtos.aspx");
        }
    }
    public class MyObject
    {
        public string success { get; set; }
    }
    public bool Validate()
    {
        string Response = Request["g-recaptcha-response"];//Getting Response String Append to Post Method
        bool Valid = false;
        //Request to Google Server
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create
        (" https://www.google.com/recaptcha/api/siteverify?secret=6LdrsyEUAAAAAC4HheyouhYyh42mNFYB-RHiCajR&response=" + Response);
        try
        {
            //Google recaptcha Response
            using (WebResponse wResponse = req.GetResponse())
            {

                using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                {
                    string jsonResponse = readStream.ReadToEnd();

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    MyObject data = js.Deserialize<MyObject>(jsonResponse);// Deserialize Json

                    Valid = Convert.ToBoolean(data.success);
                }
            }

            return Valid;
        }
        //Se não houver conexão de internet
        catch (WebException ex)
        {
            return true;
            //throw ex;
        }
    }
    protected void CustomValidatorCheckBoxTermosePolitica_ServerValidate(object sender, ServerValidateEventArgs e)
    {
        e.IsValid = CheckBoxTermosePolitica.Checked;
    }
    protected void ButtonRegistar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Validate())
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
                con.Open();
                String VerificarRegisto = "SELECT COUNT(*) AS Count, Id_Contas, Confirmada, DatadeCriacao FROM Contas WHERE Email=@Email GROUP BY Id_Contas, Confirmada, DatadeCriacao";
                SqlCommand comandoVerificarRegisto = new SqlCommand(VerificarRegisto, con);
                comandoVerificarRegisto.Parameters.AddWithValue("@Email", TextBoxEmail.Text);
                SqlDataReader dr = comandoVerificarRegisto.ExecuteReader();
                int Count = 0;
                int Id_Contas = 0;
                bool Confirmada = false;
                DateTime DatadeCriacao = DateTime.Now;
                while (dr.Read())
                {
                    Count = Int32.Parse(dr["Count"].ToString());
                    Id_Contas = Int32.Parse(dr["Id_Contas"].ToString());
                    Confirmada = bool.Parse(dr["Confirmada"].ToString());
                    DatadeCriacao = DateTime.Parse(dr["DatadeCriacao"].ToString());
                }
                dr.Close();
                //Se o e-mail não existir na base de dados
                if (Count == 0) Register();
                //Se o e-mail existir
                else if (Count == 1)
                {
                    //Se a conta associada ao e-mail estiver confirmada
                    if (Confirmada == true) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Este endereço de e-mail já existe.');", true);
                    //Se a conta associada ao e-mail não estiver confirmada, ou seja, se a conta não tiver completado o processo de registo
                    if (Confirmada == false)
                    {
                        DateTime DatadeExpiracao = DatadeCriacao.AddDays(1);
                        //Se a conta associada ao e-mail ainda estiver na lista de espera
                        if (DatadeExpiracao > DateTime.Now)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Este endereço de e-mail já foi registado e estamos à espera da sua confirmação. Caso este endereço não for confirmado nas 24 horas após o seu registo, ficará livre novamente.');", true);
                        }
                        //Se a conta associada ao e-mail já não estiver na lista de espera, apaga-se a conta e insere-se uma nova com o mesmo e-mail
                        else if (DatadeExpiracao < DateTime.Now)
                        {
                            String ApagarContaExpirada = "DELETE FROM Contas where Id_Contas=@Id_Contas";
                            SqlCommand comandoApagarContaExpirada = new SqlCommand(ApagarContaExpirada, con);
                            comandoApagarContaExpirada.Parameters.AddWithValue("@Id_Contas", Id_Contas);
                            comandoApagarContaExpirada.ExecuteNonQuery();
                            Register();
                        }
                    }
                }
                con.Close();
            }
            else ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Preencha o Captcha corretamente.');", true);
        }
    }
    public void Register()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();

        String InserirConta = "INSERT INTO Contas (Nome,DatadeNascimento,CodigoPostal,Localidade,Morada,Email,ENotificar,Palavrapasse,DatadeCriacao,UltimoLogin,Confirmada) values (@Nome,@DatadeNascimento,@CodigoPostal,@Localidade,@Morada,@Email,@ENotificar,@Palavrapasse,@DatadeCriacao,@UltimoLogin,@Confirmada)";
        SqlCommand comandoInserirConta = new SqlCommand(InserirConta, con);
        comandoInserirConta.Parameters.AddWithValue("@Nome", TextBoxName.Text);
        string[] DatadeNascimentoarray = TextBoxDateofBirth.Text.Split('/');
        DateTime DatadeNascimento = new DateTime(Int32.Parse(DatadeNascimentoarray[2]), Int32.Parse(DatadeNascimentoarray[1]), Int32.Parse(DatadeNascimentoarray[0]));
        comandoInserirConta.Parameters.AddWithValue("@DatadeNascimento", DatadeNascimento);
        comandoInserirConta.Parameters.AddWithValue("@CodigoPostal", TextBoxZIPCode.Text);
        comandoInserirConta.Parameters.AddWithValue("@Localidade", DropDownListLocal.SelectedItem.ToString());
        comandoInserirConta.Parameters.AddWithValue("@Morada", TextBoxHomeAdress.Text);
        comandoInserirConta.Parameters.AddWithValue("@Email", TextBoxEmail.Text);
        comandoInserirConta.Parameters.AddWithValue("@ENotificar", CheckBoxENotificar.Checked);
        comandoInserirConta.Parameters.AddWithValue("@Palavrapasse", TextBoxPassword.Text);
        DateTime DatadeCriacao2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        comandoInserirConta.Parameters.AddWithValue("@DatadeCriacao", DatadeCriacao2);
        comandoInserirConta.Parameters.AddWithValue("@Confirmada", false);
        comandoInserirConta.Parameters.AddWithValue("@UltimoLogin", DatadeCriacao2);
        comandoInserirConta.ExecuteNonQuery();

        String GetIddaContaInserida = "SELECT Id_Contas FROM Contas WHERE Email=@Email";
        SqlCommand comandoGetIddaContaInserida = new SqlCommand(GetIddaContaInserida, con);
        comandoGetIddaContaInserida.Parameters.AddWithValue("@Email", TextBoxEmail.Text);
        int IddaContaInserida = Int32.Parse(comandoGetIddaContaInserida.ExecuteScalar().ToString());

        GeradordeCodigos Gerador = new GeradordeCodigos();
        string CodigodeConfirmacao = "";
        int CodigosEncontrados = 1;
        do
        {
            CodigodeConfirmacao = Gerador.GerarCodigo(50);
            //Verificar que o código de confirmação não existe, o que é extremamente improvável
            String ProcurarporCodigodeConfirmacao = "SELECT COUNT(*) FROM ContasCodigosdeConfirmacao WHERE CodigodeConfirmacao=@CodigodeConfirmacao";
            SqlCommand comandoProcurarporCodigodeConfirmacao = new SqlCommand(ProcurarporCodigodeConfirmacao, con);
            comandoProcurarporCodigodeConfirmacao.Parameters.AddWithValue("@CodigodeConfirmacao", CodigodeConfirmacao);
            CodigosEncontrados = Convert.ToInt32(comandoProcurarporCodigodeConfirmacao.ExecuteScalar());
        } while (CodigosEncontrados != 0);

        //Enviar o código para o e-email associado à conta registada
        SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 25);
        smtpClient.Port = 587;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential("SupermercadosBomValor@outlook.com", "abc123IO");
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true;
        MailMessage mail = new MailMessage("SupermercadosBomValor@outlook.com", "Luisdanielbarros997@outlook.com");
        mail.Subject = "Confirme a sua conta";
        Uri uri = new Uri("http://localhost:58808/Confirmar.aspx?c=" + 2.ToString() + "&cc=" + CodigodeConfirmacao.ToString());
        mail.Body = "Clique no link para confirmar a sua conta: <a href=" + uri + ">link de confirmação</a>";
        mail.IsBodyHtml = true;
        Session["EmailporConfirmar"] = TextBoxEmail.Text;
        try
        {
            smtpClient.Send(mail);
            //Inserir o código de confirmação
            String InserirCodigodeConfirmacao = "INSERT INTO ContasCodigosdeConfirmacao (FKId_Contas,CodigodeConfirmacao, Objetivo, Usado, Data) values (@FKId_Contas, @CodigodeConfirmacao, 0, 'false', GETDATE())";
            SqlCommand comandoInserirCodigodeConfirmacao = new SqlCommand(InserirCodigodeConfirmacao, con);
            comandoInserirCodigodeConfirmacao.Parameters.AddWithValue("@FKId_Contas", IddaContaInserida);
            comandoInserirCodigodeConfirmacao.Parameters.AddWithValue("@CodigodeConfirmacao", CodigodeConfirmacao);
            comandoInserirCodigodeConfirmacao.ExecuteNonQuery();
        }
        catch 
        {
            //Quando a conta de e-mail SupermercadosBomValor@outlook.com é bloqueada por suspeitas de spam, o programa entra no catch. É um problema básico em que me basta fazer login e confirmar a minha identidade
            //por telemóvel.
            Session["EmailporConfirmarFalhanoEnvio"] = 1;
        }
        Response.Redirect("Confirmar.aspx?c=" + 1.ToString());
        con.Close();
    }
}
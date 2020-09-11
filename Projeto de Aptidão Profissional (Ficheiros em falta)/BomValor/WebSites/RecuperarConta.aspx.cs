using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RecuperarConta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailporConfirmar"] != null && !Page.IsPostBack) TextBoxEnviarConfirmacao.Text = Session["EmailporConfirmar"].ToString();
    }
    protected bool ProcurarEmail(string email)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String ProcurarEmail = "SELECT COUNT(*) FROM Contas WHERE Email=@Email";
        SqlCommand comandoProcurarEmail = new SqlCommand(ProcurarEmail, con);
        comandoProcurarEmail.Parameters.AddWithValue("@Email", email);
        int temp = Convert.ToInt32(comandoProcurarEmail.ExecuteScalar());
        con.Close();
        if (temp == 1) return true;
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Este endereço de e-mail não existe.');", true);
            return false;
        }
    }
    protected void ButtonEnviarConfirmacao_Click(object sender, EventArgs e)
    {
        if (ProcurarEmail(TextBoxEnviarConfirmacao.Text))
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            //Verificar que a conta ainda não foi confirmada
            String ProcurarConfirmacaodaConta = "SELECT COUNT(*) FROM Contas WHERE Email=@Email and Confirmada='false'";
            SqlCommand comandoProcurarConfirmacaodaConta = new SqlCommand(ProcurarConfirmacaodaConta, con);
            comandoProcurarConfirmacaodaConta.Parameters.AddWithValue("@Email", TextBoxEnviarConfirmacao.Text);
            int temp = Int32.Parse(comandoProcurarConfirmacaodaConta.ExecuteScalar().ToString());
            if (temp == 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A conta associada a este endereço de e-mail já foi confirmada.');", true);
            else
            {
                String GetIddaConta = "SELECT Id_Contas FROM Contas WHERE Email=@Email";
                SqlCommand comandoGetIddaConta = new SqlCommand(GetIddaConta, con);
                comandoGetIddaConta.Parameters.AddWithValue("@Email", TextBoxEnviarConfirmacao.Text);
                int IddaConta = Int32.Parse(comandoGetIddaConta.ExecuteScalar().ToString());
                //Verificar que não se enviou nenhum e-mail à mesma conta, com o mesmo objetivo de confirmar a conta, à menos de 5 minutos
                String EvitarSpamdeEmail = "SELECT COUNT(*) FROM ContasCodigosdeConfirmacao WHERE FKId_Contas=@FKId_Contas AND Objetivo='0' AND Usado='false' AND Data>DATEADD(minute, -5, GETDATE())";
                SqlCommand comandoEvitarSpamdeEmail = new SqlCommand(EvitarSpamdeEmail, con);
                comandoEvitarSpamdeEmail.Parameters.AddWithValue("@FKId_Contas", IddaConta);
                int temp2 = Convert.ToInt32(comandoEvitarSpamdeEmail.ExecuteScalar());
                if (temp2 > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Espere 5 minutos antes de pedir um novo e-mail de confirmação.');", true);
                else
                {
                    //Gerar o código de confirmação, certificando-se de que o código não existe na base de dados
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
                    //Enviar o código de confirmação para o e-email
                    SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 25);
                    smtpClient.Port = 587;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential("SupermercadosBomValor@outlook.com", "abc123IO");
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    MailMessage mail = new MailMessage("SupermercadosBomValor@outlook.com", "Luisdanielbarros997@outlook.com");
                    mail.Subject = "Confirme a sua conta.";
                    Uri uri = new Uri("http://localhost:58808/Confirmar.aspx?c=" + 2.ToString() + "&cc=" + CodigodeConfirmacao.ToString());
                    mail.Body = "Clique no link para confirmar a sua conta: <a href=" + uri + ">link de confirmação</a>";
                    mail.IsBodyHtml = true;
                    try
                    {
                        smtpClient.Send(mail);
                        //Invalidar o código de confirmação anterior
                        String InvalidarCodigodeConfirmacao = "UPDATE ContasCodigosdeConfirmacao SET Usado='true' WHERE FKId_Contas=@FKId_Contas and Objetivo='0'";
                        SqlCommand comandoInvalidarCodigodeConfirmacao = new SqlCommand(InvalidarCodigodeConfirmacao, con);
                        comandoInvalidarCodigodeConfirmacao.Parameters.AddWithValue("@FKId_Contas", IddaConta);
                        comandoInvalidarCodigodeConfirmacao.ExecuteNonQuery();
                        //Inserir o código de confirmação
                        String InserirCodigodeConfirmacao = "INSERT INTO ContasCodigosdeConfirmacao (FKId_Contas, CodigodeConfirmacao, Objetivo, Usado, Data) values (@FKId_Contas, @CodigodeConfirmacao, 0, 'false', GETDATE())";
                        SqlCommand comandoInserirCodigodeConfirmacao = new SqlCommand(InserirCodigodeConfirmacao, con);
                        comandoInserirCodigodeConfirmacao.Parameters.AddWithValue("@FKId_Contas", IddaConta);
                        comandoInserirCodigodeConfirmacao.Parameters.AddWithValue("@CodigodeConfirmacao", CodigodeConfirmacao);
                        comandoInserirCodigodeConfirmacao.ExecuteNonQuery();
                        ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('E-mail enviado.');", true);
                    }
                    catch
                    {
                        //Caso não seja possível enviar o e-mail, avisar o utilizador.
                        //Quando a conta de e-mail SupermercadosBomValor@outlook.com é bloqueada por suspeitas de spam, o programa entra no catch. É um problema básico em que me basta fazer login e confirmar a minha identidade
                        //por telemóvel.
                        ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('De momento não é possível enviar-lhe um e-mail de confirmação, por favor tente mais tarde. Pedimos desculpas pelo incómodo.');", true);
                    }
                }
            }
            con.Close();
        }
    }
    protected void ButtonRecuperarPalavrapasse_Click(object sender, EventArgs e)
    {
        if (ProcurarEmail(TextBoxRecuperarPalavrapasse.Text))
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            //Verificar que a conta ainda não foi confirmada
            String ProcurarConfirmacaodaConta = "SELECT COUNT(*) FROM Contas WHERE Email=@Email and Confirmada='false'";
            SqlCommand comandoProcurarConfirmacaodaConta = new SqlCommand(ProcurarConfirmacaodaConta, con);
            comandoProcurarConfirmacaodaConta.Parameters.AddWithValue("@Email", TextBoxRecuperarPalavrapasse.Text);
            int temp = Int32.Parse(comandoProcurarConfirmacaodaConta.ExecuteScalar().ToString());
            if (temp == 1) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Não é possível enviar o e-mail desejado, visto que a conta associada a este endereço de e-mail ainda não foi confirmada.');", true);
            else
            {
                String GetIddaConta = "SELECT Id_Contas FROM Contas WHERE Email=@Email";
                SqlCommand comandoGetIddaConta = new SqlCommand(GetIddaConta, con);
                comandoGetIddaConta.Parameters.AddWithValue("@Email", TextBoxRecuperarPalavrapasse.Text);
                int IddaConta = Int32.Parse(comandoGetIddaConta.ExecuteScalar().ToString());
                //Verificar que não se enviou nenhum e-mail à mesma conta, com o mesmo objetivo de confirmar a conta, à menos de 5 minutos
                String EvitarSpamdeEmail = "SELECT COUNT(*) FROM ContasCodigosdeConfirmacao WHERE FKId_Contas=@FKId_Contas AND Objetivo='2' AND Usado='false' AND Data>DATEADD(minute, -5, GETDATE())";
                SqlCommand comandoEvitarSpamdeEmail = new SqlCommand(EvitarSpamdeEmail, con);
                comandoEvitarSpamdeEmail.Parameters.AddWithValue("@FKId_Contas", IddaConta);
                int temp2 = Convert.ToInt32(comandoEvitarSpamdeEmail.ExecuteScalar());
                if (temp2 > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Espere 5 minutos antes de pedir um novo e-mail de alteração de palavra-passe.');", true);
                else
                {
                    //Gerar o código de confirmação, certificando-se de que o código não existe na base de dados
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
                    //Enviar o código de confirmação para o e-email
                    SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 25);
                    smtpClient.Port = 587;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential("SupermercadosBomValor@outlook.com", "abc123IO");
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    MailMessage mail = new MailMessage("SupermercadosBomValor@outlook.com", "Luisdanielbarros997@outlook.com");
                    mail.Subject = "Alteração de palavra-passe.";
                    Uri uri = new Uri("http://localhost:58808/Confirmar.aspx?c=" + 4.ToString() + "&cc=" + CodigodeConfirmacao.ToString());
                    mail.Body = "Clique no link para mudar a sua palavra-passe: <a href=" + uri + ">link de mudança de palavra-passe</a>";
                    mail.IsBodyHtml = true;
                    try
                    {
                        smtpClient.Send(mail);
                        //Invalidar o código de confirmação anterior
                        String InvalidarCodigodeConfirmacao = "UPDATE ContasCodigosdeConfirmacao SET Usado='true' WHERE FKId_Contas=@FKId_Contas and Objetivo='2'";
                        SqlCommand comandoInvalidarCodigodeConfirmacao = new SqlCommand(InvalidarCodigodeConfirmacao, con);
                        comandoInvalidarCodigodeConfirmacao.Parameters.AddWithValue("@FKId_Contas", IddaConta);
                        comandoInvalidarCodigodeConfirmacao.ExecuteNonQuery();
                        //Inserir o código de confirmação
                        String InserirCodigodeConfirmacao = "INSERT INTO ContasCodigosdeConfirmacao (FKId_Contas, CodigodeConfirmacao, Objetivo, Usado, Data) values (@FKId_Contas, @CodigodeConfirmacao, 2, 'false', GETDATE())";
                        SqlCommand comandoInserirCodigodeConfirmacao = new SqlCommand(InserirCodigodeConfirmacao, con);
                        comandoInserirCodigodeConfirmacao.Parameters.AddWithValue("@FKId_Contas", IddaConta);
                        comandoInserirCodigodeConfirmacao.Parameters.AddWithValue("@CodigodeConfirmacao", CodigodeConfirmacao);
                        comandoInserirCodigodeConfirmacao.ExecuteNonQuery();
                        ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('E-mail enviado.');", true);
                    }
                    catch
                    {
                        //Caso não seja possível enviar o e-mail, avisar o utilizador.
                        //Quando a conta de e-mail SupermercadosBomValor@outlook.com é bloqueada por suspeitas de spam, o programa entra no catch. É um problema básico em que me basta fazer login e confirmar a minha identidade
                        //por telemóvel.
                        ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('De momento não é possível enviar-lhe um e-mail de mudança de palavra-passe, por favor tente mais tarde. Pedimos desculpas pelo incómodo.');", true);
                    }
                }
            }
            con.Close();
        }
    }
    protected void ButtonDesbloquearConta_Click(object sender, EventArgs e)
    {
        if (ProcurarEmail(TextBoxDesbloquearConta.Text))
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
            con.Open();
            //Verificar que a conta ainda não foi confirmada
            String ProcurarConfirmacaodaConta = "SELECT COUNT(*) FROM Contas WHERE Email=@Email and Confirmada='false'";
            SqlCommand comandoProcurarConfirmacaodaConta = new SqlCommand(ProcurarConfirmacaodaConta, con);
            comandoProcurarConfirmacaodaConta.Parameters.AddWithValue("@Email", TextBoxRecuperarPalavrapasse.Text);
            int temp = Int32.Parse(comandoProcurarConfirmacaodaConta.ExecuteScalar().ToString());
            if (temp == 1) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Não é possível enviar o e-mail desejado, visto que a conta associada a este endereço de e-mail ainda não foi confirmada.');", true);
            else
            {
                //Veriricar que a conta está bloqueada
                String ProcurarIdporEmail = "SELECT Id_Contas FROM Contas WHERE Email=@Email";
                SqlCommand comandoProcurarIdporEmail = new SqlCommand(ProcurarIdporEmail, con);
                comandoProcurarIdporEmail.Parameters.AddWithValue("@Email", TextBoxDesbloquearConta.Text);
                int IdConta = Convert.ToInt32(comandoProcurarIdporEmail.ExecuteScalar());
                String ProcurarEstadodeBloqueio = "SELECT COUNT(*) FROM ContasBloqueios WHERE FKId_Contas=@FKId_Contas and Estado=1";
                SqlCommand comandoProcurarEstadodeBloqueio = new SqlCommand(ProcurarEstadodeBloqueio, con);
                comandoProcurarEstadodeBloqueio.Parameters.AddWithValue("@FKId_Contas", IdConta);
                int temp2 = Convert.ToInt32(comandoProcurarEstadodeBloqueio.ExecuteScalar());
                if (temp2 == 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A conta associada a este endereço de e-mail não está bloqueada.');", true);
                else
                {
                    //Verificar que não se enviou nenhum e-mail à mesma conta, com o mesmo objetivo de confirmar a conta, à menos de 5 minutos
                    String EvitarSpamdeEmail = "SELECT COUNT(*) FROM ContasCodigosdeConfirmacao WHERE FKId_Contas=@FKId_Contas AND Objetivo='1' AND Usado='false' AND Data>DATEADD(minute, -5, GETDATE())";
                    SqlCommand comandoEvitarSpamdeEmail = new SqlCommand(EvitarSpamdeEmail, con);
                    comandoEvitarSpamdeEmail.Parameters.AddWithValue("@FKId_Contas", IdConta);
                    int temp3 = Convert.ToInt32(comandoEvitarSpamdeEmail.ExecuteScalar());
                    if (temp3 > 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Espere 5 minutos antes de pedir um novo e-mail de desbloqueamento.');", true);
                    else
                    {
                        //Gerar o código de confirmação, certificando-se de que o código não existe na base de dados
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
                        //Enviar o código de confirmação para o e-email
                        SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 25);
                        smtpClient.Port = 587;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new System.Net.NetworkCredential("SupermercadosBomValor@outlook.com", "abc123IO");
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.EnableSsl = true;
                        MailMessage mail = new MailMessage("SupermercadosBomValor@outlook.com", "Luisdanielbarros997@outlook.com");
                        mail.Subject = "Desbloqueie a sua conta.";
                        Uri uri = new Uri("http://localhost:58808/Confirmar.aspx?c=" + 3.ToString() + "&cc=" + CodigodeConfirmacao.ToString());
                        mail.Body = "Clique no link para desbloquear a sua conta: <a href=" + uri + ">link de desbloqueamento</a>";
                        mail.IsBodyHtml = true;
                        try
                        {
                            smtpClient.Send(mail);
                            //Invalidar o código de confirmação anterior
                            String InvalidarCodigodeConfirmacao = "UPDATE ContasCodigosdeConfirmacao SET Usado='true' WHERE FKId_Contas=@FKId_Contas and Objetivo='1'";
                            SqlCommand comandoInvalidarCodigodeConfirmacao = new SqlCommand(InvalidarCodigodeConfirmacao, con);
                            comandoInvalidarCodigodeConfirmacao.Parameters.AddWithValue("@FKId_Contas", IdConta);
                            comandoInvalidarCodigodeConfirmacao.ExecuteNonQuery();
                            //Inserir o código de confirmação
                            String InserirCodigodeConfirmacao = "INSERT INTO ContasCodigosdeConfirmacao (FKId_Contas, CodigodeConfirmacao, Objetivo, Usado, Data) values (@FKId_Contas, @CodigodeConfirmacao, 1, 'false', GETDATE())";
                            SqlCommand comandoInserirCodigodeConfirmacao = new SqlCommand(InserirCodigodeConfirmacao, con);
                            comandoInserirCodigodeConfirmacao.Parameters.AddWithValue("@FKId_Contas", IdConta);
                            comandoInserirCodigodeConfirmacao.Parameters.AddWithValue("@CodigodeConfirmacao", CodigodeConfirmacao);
                            comandoInserirCodigodeConfirmacao.ExecuteNonQuery();
                            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('E-mail enviado.');", true);
                        }
                        catch
                        {
                            //Caso não seja possível enviar o e-mail, avisar o utilizador.
                            //Quando a conta de e-mail SupermercadosBomValor@outlook.com é bloqueada por suspeitas de spam, o programa entra no catch. É um problema básico em que me basta fazer login e confirmar a minha identidade
                            //por telemóvel.
                            ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('De momento não é possível enviar-lhe um e-mail de confirmação, por favor tente mais tarde. Pedimos desculpas pelo incómodo.');", true);
                        }
                    }
                }
            }
            con.Close();
        }
    }
}
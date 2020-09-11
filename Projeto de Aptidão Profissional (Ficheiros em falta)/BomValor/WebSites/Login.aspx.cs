using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Utilizador"] == null)
        {
            if (Session["PasswordsErradas"] == null) Captcha.Visible = false;
            else
            {
                if (Int32.Parse(Session["PasswordsErradas"].ToString()) >= 3) Captcha.Visible = true;
                else Captcha.Visible = false;
            }
            
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
    protected void ButtonEntrar_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        con.Open();
        String VerificarLogin = "SELECT COUNT(*) AS Count, Id_Contas, Confirmada FROM Contas WHERE Email=@Email GROUP BY Id_Contas, Confirmada";
        SqlCommand comandoVerificarLogin = new SqlCommand(VerificarLogin, con);
        comandoVerificarLogin.Parameters.AddWithValue("@Email", TextBoxEmail.Text);
        SqlDataReader dr = comandoVerificarLogin.ExecuteReader();
        int Count = 0;
        int Id_Contas = 0;
        bool Confirmada = false;
        while (dr.Read())
        {
            Count = Int32.Parse(dr["Count"].ToString());
            Id_Contas = Int32.Parse(dr["Id_Contas"].ToString());
            Confirmada = bool.Parse(dr["Confirmada"].ToString());
        }
        dr.Close();
        bool canlogin = true;
        if (Session["PasswordsErradas"] != null) if (Int32.Parse(Session["PasswordsErradas"].ToString()) >= 3) canlogin = Validate();
        if (canlogin == false) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('O captcha está errado.');", true);
        //Se o utilizador for obrigado a preencher o captcha, e se o tiver preenchido corretamente
        if (canlogin)
        {
            //Se o email inserido estiver associado a uma conta
            if (Count == 0) ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Este Email não existe.');", true);
            else
            {
                //Se a conta estiver confirmada
                if (Confirmada == false)
                {
                    Session["EmailporConfirmar"] = TextBoxEmail.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Esta conta ainda não foi confirmada. Verifique o seu e-mail, enviámos-lhe uma mensagem sobre como confirmar a sua conta. Caso necessário, peça o reenvio do e-mail de confirmação.'); window.location.href = 'RecuperarConta.aspx';", true);
                }
                else
                {
                    String ProcurarEstadodeBloqueio = "SELECT COUNT(*) FROM ContasBloqueios WHERE FKId_Contas=@FKId_Contas and Estado=1";
                    SqlCommand comandoProcurarEstadodeBloqueio = new SqlCommand(ProcurarEstadodeBloqueio, con);
                    comandoProcurarEstadodeBloqueio.Parameters.AddWithValue("@FKId_Contas", Id_Contas);
                    int Bloqueada = 0;
                    if (comandoProcurarEstadodeBloqueio.ExecuteScalar() != null) Bloqueada = Int32.Parse(comandoProcurarEstadodeBloqueio.ExecuteScalar().ToString());
                    //Se a conta não estiver bloqueada
                    if (Bloqueada == 1)
                    {
                        //Inserir a tentativa de login
                        String InserirTentativadeLogin = "INSERT INTO ContasTentativas (FKId_Contas, Data, Autenticado) VALUES (@FKId_Contas, GETDATE(), 2)";
                        SqlCommand comandoInserirTentativadeLogin = new SqlCommand(InserirTentativadeLogin, con);
                        comandoInserirTentativadeLogin.Parameters.AddWithValue("@FKId_Contas", Id_Contas);
                        comandoInserirTentativadeLogin.ExecuteNonQuery();
                        ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Esta conta foi bloqueada devido a demasiadas tentativas de login seguidas e sem sucesso. Verifique o seu e-mail, enviámos-lhe um e-mail para poder desbloquear a sua conta.');", true);
                    }
                    else
                    {
                        String Autenticar = "SELECT COUNT(*) FROM Contas WHERE Email=@Email and Palavrapasse=@Palavrapasse";
                        SqlCommand comandoAutenticar = new SqlCommand(Autenticar, con);
                        comandoAutenticar.Parameters.AddWithValue("@Email", TextBoxEmail.Text);
                        comandoAutenticar.Parameters.AddWithValue("@Palavrapasse", TextBoxPassword.Text);
                        int temp2 = Convert.ToInt32(comandoAutenticar.ExecuteScalar());
                        //Se a palavra-passe inserida estiver correta
                        if (temp2 == 1)
                        {
                            //Inserir a tentativa de login
                            String InserirTentativadeLogin = "INSERT INTO ContasTentativas (FKId_Contas, Data, Autenticado) VALUES (@FKId_Contas, GETDATE(), 1)";
                            SqlCommand comandoInserirTentativadeLogin = new SqlCommand(InserirTentativadeLogin, con);
                            comandoInserirTentativadeLogin.Parameters.AddWithValue("@FKId_Contas", Id_Contas);
                            comandoInserirTentativadeLogin.ExecuteNonQuery();
                            //Criar as sessões de utilizador
                            String ProcurarNomeporId = "SELECT Nome FROM Contas WHERE Id_Contas=@Id_Contas";
                            SqlCommand comandoProcurarNomeporId = new SqlCommand(ProcurarNomeporId, con);
                            comandoProcurarNomeporId.Parameters.AddWithValue("@Id_Contas", Id_Contas);
                            string Nome = comandoProcurarNomeporId.ExecuteScalar().ToString();
                            Session["Utilizador"] = null;
                            Session["Nome_Utilizador"] = null;
                            Session["Email_Utilizador"] = null;
                            Session["ListadeCompras"] = null;
                            Session["EmailporConfirmar"] = null;
                            Session["EmailporConfirmarFalhanoEnvio"] = null;
                            Session["SenhaporMudar"] = null;
                            Session["Utilizador"] = Id_Contas;
                            Session["Nome_Utilizador"] = Nome;
                            Session["Email_Utilizador"] = TextBoxEmail.Text;
                            //Se o utilizador tiver qualquer lista de compras guardada, o servidor irá criar a sua sessão de compras
                            //Obter a lista de compras atual
                            String ProcurarListadeCompras = "SELECT Id_ListasdeCompras FROM ListasdeCompras WHERE FKId_Contas=@FKId_Contas and Estado='guardada'";
                            SqlCommand comandoProcurarListadeCompras = new SqlCommand(ProcurarListadeCompras, con);
                            comandoProcurarListadeCompras.Parameters.AddWithValue("@FKId_Contas", Id_Contas);
                            int IdListasdeCompras;
                            if (comandoProcurarListadeCompras.ExecuteScalar() == null) IdListasdeCompras = 0;
                            else IdListasdeCompras = Int32.Parse(comandoProcurarListadeCompras.ExecuteScalar().ToString());
                            if (IdListasdeCompras > 0)
                            {
                                //Obter o Id de todos os artigos nessa lista de compras
                                String ProcurarArtigosnaListadeCompras = "SELECT FKId_ProdutosVariedades FROM ListasdeComprasEntradas WHERE FKId_ListasdeCompras=@FKId_ListasdeCompras";
                                SqlCommand comandoProcurarArtigosnaListadeCompras = new SqlCommand(ProcurarArtigosnaListadeCompras, con);
                                comandoProcurarArtigosnaListadeCompras.Parameters.AddWithValue("@FKId_ListasdeCompras", IdListasdeCompras);
                                List<int> IdArtigosnaListadeCompras = new List<int>();
                                SqlDataReader dr3 = comandoProcurarArtigosnaListadeCompras.ExecuteReader();
                                while (dr3.Read())
                                {
                                    IdArtigosnaListadeCompras.Add(Int32.Parse(dr3["FKId_ProdutosVariedades"].ToString()));
                                }
                                dr3.Close();
                                ListadeItensdoCarrodeCompras lista = new ListadeItensdoCarrodeCompras();
                                for (int i = 0; i < IdArtigosnaListadeCompras.Count; i++)
                                {
                                    //Através do Id, obter a informação dos artigos e carregá-la para a variável de sessão
                                    String ProcurarAtributosdoArtigo = "SELECT * FROM ListasdeComprasEntradas left join ProdutosVariedades on ListasdeComprasEntradas.FKId_ProdutosVariedades = ProdutosVariedades.Id_ProdutosVariedades left join Produtos on ProdutosVariedades.FKId_Produtos = Produtos.Id_Produtos where ListasdeComprasEntradas.FKId_ProdutosVariedades=@FKId_ProdutosVariedades";
                                    SqlCommand comandoProcurarAtributosdoArtigo = new SqlCommand(ProcurarAtributosdoArtigo, con);
                                    comandoProcurarAtributosdoArtigo.Parameters.AddWithValue("@FKId_ProdutosVariedades", IdArtigosnaListadeCompras[i]);
                                    SqlDataReader dr4 = comandoProcurarAtributosdoArtigo.ExecuteReader();
                                    while (dr4.Read())
                                    {
                                        ItemdoCarrodeCompras item = new ItemdoCarrodeCompras(Int32.Parse(dr4["Id_Produtos"].ToString()), Int32.Parse(dr4["Id_ProdutosVariedades"].ToString()), dr4["Nome"].ToString(), dr4["Marca"].ToString(), dr4["Quantidade"].ToString(), float.Parse(dr4["PrecoTotal"].ToString()), Int32.Parse(dr4["Unidades"].ToString()));
                                        lista.additem(item);
                                    }
                                    dr4.Close();
                                }
                                Session["ListadeCompras"] = lista;
                            }
                            Response.Redirect("Produtos.aspx");
                        }
                        //Se a palavra-passe estiver errada
                        else
                        {
                            if (Session["PasswordsErradas"] == null) Session["PasswordsErradas"] = 1;
                            else Session["PasswordsErradas"] = Int32.Parse(Session["PasswordsErradas"].ToString()) + 1;
                            //Inserir a tentativa de login
                            String InserirTentativadeLogin = "INSERT INTO ContasTentativas (FKId_Contas, Data, Autenticado) VALUES (@FKId_Contas, GETDATE(), 0)";
                            SqlCommand comandoInserirTentativadeLogin = new SqlCommand(InserirTentativadeLogin, con);
                            comandoInserirTentativadeLogin.Parameters.AddWithValue("@FKId_Contas", Id_Contas);
                            comandoInserirTentativadeLogin.ExecuteNonQuery();
                            ///Ver as 10 últimas tentativas de login desta conta, nas últimas 24 horas
                            String ProcurarTentativadeLogin = "SELECT TOP 10 * FROM ContasTentativas LEFT JOIN Contas ON ContasTentativas.FKId_Contas = Contas.Id_Contas WHERE Contas.Email=@Email AND ContasTentativas.Data>@Data ORDER BY Data DESC";
                            SqlCommand comandoProcurarTentativadeLogin = new SqlCommand(ProcurarTentativadeLogin, con);
                            comandoProcurarTentativadeLogin.Parameters.AddWithValue("@Email", TextBoxEmail.Text);
                            DateTime DatadeValidadedaContagem = DateTime.Now.AddDays(-1);
                            comandoProcurarTentativadeLogin.Parameters.AddWithValue("@Data", DatadeValidadedaContagem);
                            SqlDataReader dr3 = comandoProcurarTentativadeLogin.ExecuteReader();
                            int numerodetentativassemsucesso = 0;
                            while (dr3.Read())
                            {
                                if (Int16.Parse(dr3["Autenticado"].ToString()) != 1) numerodetentativassemsucesso++;
                                else numerodetentativassemsucesso -= 10;
                            }
                            dr3.Close();
                            //Se a conta tiver sido vítima de 10 tentativas de login erradas, seguidas e num período de 24 horas, bloqueá-la
                            if (numerodetentativassemsucesso == 10)
                            {
                                //Inserir o bloqueamento da conta na base de dados
                                String InserirBloqueio = "INSERT INTO ContasBloqueios (FKId_Contas, Data, Estado) VALUES (@Id_Contas, @Data, @Estado)";
                                SqlCommand comandoInserirBloqueio = new SqlCommand(InserirBloqueio, con);
                                comandoInserirBloqueio.Parameters.AddWithValue("@Id_Contas", Id_Contas);
                                comandoInserirBloqueio.Parameters.AddWithValue("@Data", DateTime.Now);
                                comandoInserirBloqueio.Parameters.AddWithValue("@Estado", 1);
                                comandoInserirBloqueio.ExecuteNonQuery();
                                //Inserir o código associado ao bloqueio na base de dados, confirmando que este não se repete com qualquer outro código existente
                                GeradordeCodigos Gerador = new GeradordeCodigos();
                                string CodigodeConfirmacao = "";
                                int CodigosEncontrados = 1;
                                do
                                {
                                    CodigodeConfirmacao = Gerador.GerarCodigo(50);
                                    String ProcurarporCodigodeConfirmacao = "SELECT COUNT(*) FROM ContasCodigosdeConfirmacao WHERE CodigodeConfirmacao=@CodigodeConfirmacao";
                                    SqlCommand comandoProcurarporCodigodeConfirmacao = new SqlCommand(ProcurarporCodigodeConfirmacao, con);
                                    comandoProcurarporCodigodeConfirmacao.Parameters.AddWithValue("@CodigodeConfirmacao", CodigodeConfirmacao);
                                    CodigosEncontrados = Convert.ToInt32(comandoProcurarporCodigodeConfirmacao.ExecuteScalar());
                                } while (CodigosEncontrados != 0);
                                try
                                {
                                    //Enviar o código para o e-email associado à conta bloqueada
                                    SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 25);
                                    smtpClient.EnableSsl = true;
                                    smtpClient.Port = 587;
                                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtpClient.UseDefaultCredentials = false;
                                    smtpClient.Credentials = new NetworkCredential("SupermercadosBomValor@outlook.com", "abc123IO");
                                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    MailMessage mail = new MailMessage("SupermercadosBomValor@outlook.com", "Luisdanielbarros997@outlook.com");
                                    mail.Subject = "A sua conta foi bloqueada.";
                                    Uri uri = new Uri("http://localhost:58808/Confirmar.aspx?c=" + 3.ToString() + "&cc=" + CodigodeConfirmacao.ToString());
                                    mail.Body = "<p>A sua conta foi bloqueada a " + DateTime.Now.ToString() + " devido a dez tentativas de login seguidas e sem sucesso, num período de 24 horas. Estas tentativas foram suas? Se não recomendamos rever a segurança da sua palavra-passe, e se necessário alterá-la por uma palavra-passe mais segura.</p><p>Clique no link para desbloquear a sua conta: <a href=" + uri + ">link de desbloqueamento</a></p>";
                                    mail.IsBodyHtml = true;
                                    smtpClient.Send(mail);
                                    //Inserir o código de confirmação, associado à data a que pode ser sobreposto
                                    String InserirCodigodeConfirmacao = "INSERT INTO ContasCodigosdeConfirmacao (FKId_Contas, CodigodeConfirmacao, Objetivo, Usado, Data) values (@FKId_Contas,@CodigodeConfirmacao, 1, 'false', GETDATE())";
                                    SqlCommand comandoInserirCodigodeConfirmacao = new SqlCommand(InserirCodigodeConfirmacao, con);
                                    comandoInserirCodigodeConfirmacao.Parameters.AddWithValue("@FKId_Contas", Id_Contas);
                                    comandoInserirCodigodeConfirmacao.Parameters.AddWithValue("@CodigodeConfirmacao", CodigodeConfirmacao);
                                    comandoInserirCodigodeConfirmacao.ExecuteNonQuery();
                                    ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Esta conta foi bloqueada devido a demasiadas tentativas de login seguidas e sem sucesso. Enviámos-lhe um e-mail para poder desbloquear a sua conta.');", true);
                                }
                                catch
                                {
                                    //Quando a conta de e-mail SupermercadosBomValor@outlook.com é bloqueada por suspeitas de spam, o programa entra no catch. É um problema básico em que me basta fazer login e confirmar a minha identidade
                                    //por telemóvel.
                                    ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('Esta conta foi bloqueada devido a demasiadas tentativas de login seguidas e sem sucesso. Por outro lado não foi possível enviar-lhe um e-mail para poder desbloquear a sua conta, iremos enviar-lhe assim que possível. Pedimos desculpas pelo incómodo.');", true);
                                }
                            }
                            else ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A palavra-passe está errada.');", true);
                        }
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Confirmar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Se a pessoa for ou não um utilizador autenticado
        if (Session["Utilizador"] != null)
        {
            Visitante.Visible = false;
            RedireccionadopeloRegistar.Visible = false;
            RedireccionadopeloEmail.Visible = false;
            Utilizador.Visible = true;
            Response.Redirect("Produtos.aspx");
        }
        else
        {
            //Se a pessoa ter seguido um link mal formatado
            if (Request.QueryString["c"] == null || !Regex.IsMatch(Request.QueryString["c"].ToString(), "^[0-9]+$"))
            {
                Visitante.Visible = true;
                RedireccionadopeloRegistar.Visible = false;
                RedireccionadopeloEmail.Visible = false;
                Utilizador.Visible = false;
                LabelConfirmacao.Text = LabelConfirmacao.Text = "O link que seguiu está errado, verifique que o escreveu corretamente. Se sim, peça o re-envio de um novo link.";
            }
            else
            {
                int Objetivo = Int32.Parse(Request.QueryString["c"].ToString());
                //Se a pessoa entrar na página via o redirecionamento da página de registo
                if (Objetivo == 1)
                {
                    Visitante.Visible = false;
                    RedireccionadopeloRegistar.Visible = true;
                    if (Session["EmailporConfirmarFalhanoEnvio"] == null)
                    {
                        EmailEnviadocomSucesso.Visible = true;
                        FalhanoEnviodoEmail.Visible = false;
                    }
                    else
                    {
                        EmailEnviadocomSucesso.Visible = false;
                        FalhanoEnviodoEmail.Visible = true;
                    }
                        RedireccionadopeloEmail.Visible = false;
                        Utilizador.Visible = false;
                }
                //Se a pessoa entrar na página via um link recebido por e-mail, quaisquer que sejam os objetivos
                else
                {
                    //Se a pessoa ter seguido um link mal formatado, mas vir agora redireccionada pelo e-mail
                    if (Request.QueryString["cc"] == null || !Regex.IsMatch(Request.QueryString["cc"].ToString(), "^[0-9a-zA-Z]+$"))
                    {
                        Visitante.Visible = false;
                        RedireccionadopeloRegistar.Visible = false;
                        RedireccionadopeloEmail.Visible = true;
                        Utilizador.Visible = false;
                        LabelConfirmacao.Text = LabelConfirmacao.Text = "O link que seguiu está errado, verifique que o escreveu corretamente. Se sim, peça o re-envio de um novo link.";
                    }
                    else
                    {
                        string CodigodeConfirmacao = Request.QueryString["cc"].ToString().ToString();
                        Visitante.Visible = false;
                        RedireccionadopeloRegistar.Visible = false;
                        RedireccionadopeloEmail.Visible = true;
                        Utilizador.Visible = false;
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
                        con.Open();
                        //Verificar se o código de confirmação existe na base de dados
                        String ProcurarCodigodeConfirmacao = "SELECT COUNT(*) FROM ContasCodigosdeConfirmacao WHERE CodigodeConfirmacao=@CodigodeConfirmacao and Usado='false' and Objetivo=@Objetivo";
                        SqlCommand comandoProcurarCodigodeConfirmacao = new SqlCommand(ProcurarCodigodeConfirmacao, con);
                        comandoProcurarCodigodeConfirmacao.Parameters.AddWithValue("@CodigodeConfirmacao", CodigodeConfirmacao);
                        int ParameterObjetivo = -1;
                        if (Objetivo == 2) ParameterObjetivo = 0;
                        else if (Objetivo == 3) ParameterObjetivo = 1;
                        else if (Objetivo == 4) ParameterObjetivo = 2;
                        comandoProcurarCodigodeConfirmacao.Parameters.AddWithValue("@Objetivo", ParameterObjetivo);
                        int temp = Convert.ToInt32(comandoProcurarCodigodeConfirmacao.ExecuteScalar().ToString());
                        if (temp != 1)
                        {
                            Visitante.Visible = false;
                            RedireccionadopeloRegistar.Visible = false;
                            RedireccionadopeloEmail.Visible = true;
                            Utilizador.Visible = false;
                            LabelConfirmacao.Text = "O link que seguiu está errado, verifique que o escreveu corretamente. Se sim, peça o re-envio de um novo link.";
                        }
                        else
                        {
                            //Arranjar o id da conta associado ao código, este troço de código está aqui encima porque é usado em todas as if clauses embaixo
                            String ProcurarIdporCodigo = "SELECT Id_Contas FROM Contas LEFT JOIN ContasCodigosdeConfirmacao on Contas.Id_Contas = ContasCodigosdeConfirmacao.FKId_Contas WHERE CodigodeConfirmacao=@CodigodeConfirmacao";
                            SqlCommand comandoProcurarIdporCodigo = new SqlCommand(ProcurarIdporCodigo, con);
                            comandoProcurarIdporCodigo.Parameters.AddWithValue("@CodigodeConfirmacao", CodigodeConfirmacao);
                            int IdConta = Int32.Parse(comandoProcurarIdporCodigo.ExecuteScalar().ToString());
                            //Agir conforme o objetivo do código de confirmação
                            if (Objetivo == 2)
                            {
                                //Verificar que a conta ainda não foi confirmada
                                String VerificarConfirmacaodaConta = "SELECT Confirmada FROM Contas WHERE Id_Contas=@Id_Contas";
                                SqlCommand comandoVerificarConfirmacaodaConta = new SqlCommand(VerificarConfirmacaodaConta, con);
                                comandoVerificarConfirmacaodaConta.Parameters.AddWithValue("@Id_Contas", IdConta);
                                bool Confirmacao = bool.Parse(comandoVerificarConfirmacaodaConta.ExecuteScalar().ToString());
                                if (Confirmacao == true) LabelConfirmacao.Text = "Esta conta já foi confirmada.";
                                else if (Confirmacao == false)
                                {
                                    //Confirmar a conta
                                    String ConfirmarConta = "Update Contas set Confirmada=@Confirmada WHERE Id_Contas=@Id_Contas";
                                    SqlCommand comandoConfirmarConta = new SqlCommand(ConfirmarConta, con);
                                    comandoConfirmarConta.Parameters.AddWithValue("@Confirmada", true);
                                    comandoConfirmarConta.Parameters.AddWithValue("@Id_Contas", IdConta);
                                    comandoConfirmarConta.ExecuteNonQuery();
                                    Session["EmailporConfirmar"] = null;
                                    LabelConfirmacao.Text = "A sua conta foi confirmada com sucesso!";
                                    //Autenticar o utilizador e redirecioná-lo para a página principal
                                    String GetNomeeEmailporId = "SELECT Nome, Email FROM Contas WHERE Id_Contas=@Id_Contas";
                                    SqlCommand comandoGetNomeeEmailporId = new SqlCommand(GetNomeeEmailporId, con);
                                    comandoGetNomeeEmailporId.Parameters.AddWithValue("@Id_Contas", IdConta);
                                    string NomedaConta = "";
                                    string EmaildaConta = "";
                                    SqlDataReader dr = comandoGetNomeeEmailporId.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        NomedaConta = dr["Nome"].ToString();
                                        EmaildaConta = dr["Email"].ToString();
                                    }
                                    dr.Close();
                                    Session["Utilizador"] = IdConta;
                                    Session["Nome_Utilizador"] = NomedaConta;
                                    Session["Email_Utilizador"] = EmaildaConta;
                                    //Invalidar o código de confirmação após ter sido usado
                                    String InvalidarCodigo = "UPDATE ContasCodigosdeConfirmacao SET Usado='true' WHERE FKId_Contas=@FKId_Contas AND Objetivo=0";
                                    SqlCommand comandoInvalidarCodigo = new SqlCommand(InvalidarCodigo, con);
                                    comandoInvalidarCodigo.Parameters.AddWithValue("@FKId_Contas", IdConta);
                                    comandoInvalidarCodigo.ExecuteNonQuery();
                                    ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua conta foi confirmada.');  window.location.href = 'Produtos.aspx';", true);
                                }
                            }
                            //Se o objetivo for desbloquear uma conta registada
                            else if (Objetivo == 3)
                            {
                                //Desbloquear a conta
                                String DesbloquearConta = "UPDATE ContasBloqueios SET Estado=@Estado WHERE FKId_Contas=@FKId_Contas";
                                SqlCommand comandoDesbloquearConta = new SqlCommand(DesbloquearConta, con);
                                comandoDesbloquearConta.Parameters.AddWithValue("@FKId_Contas", IdConta);
                                comandoDesbloquearConta.Parameters.AddWithValue("@Data", DateTime.Now);
                                comandoDesbloquearConta.Parameters.AddWithValue("@Estado", 0);
                                comandoDesbloquearConta.ExecuteNonQuery();
                                LabelConfirmacao.Text = "A sua conta foi desbloqueada com sucesso!";
                                //Inserir a tentativa de login
                                String InserirTentativadeLogin = "INSERT INTO ContasTentativas (FKId_Contas, Data, Autenticado) VALUES (@FKId_Contas, GETDATE(), 1)";
                                SqlCommand comandoInserirTentativadeLogin = new SqlCommand(InserirTentativadeLogin, con);
                                comandoInserirTentativadeLogin.Parameters.AddWithValue("@FKId_Contas", IdConta);
                                comandoInserirTentativadeLogin.ExecuteNonQuery();
                                //Autenticar o utilizador e redirecioná-lo para a página principal
                                String GetNomeeEmailporId = "SELECT Nome, Email FROM Contas WHERE Id_Contas=@Id_Contas";
                                SqlCommand comandoGetNomeeEmailporId = new SqlCommand(GetNomeeEmailporId, con);
                                comandoGetNomeeEmailporId.Parameters.AddWithValue("@Id_Contas", IdConta);
                                string NomedaConta = "";
                                string EmaildaConta = "";
                                SqlDataReader dr = comandoGetNomeeEmailporId.ExecuteReader();
                                while (dr.Read())
                                {
                                    NomedaConta = dr["Nome"].ToString();
                                    EmaildaConta = dr["Email"].ToString();
                                }
                                dr.Close();
                                Session["Utilizador"] = IdConta;
                                Session["Nome_Utilizador"] = NomedaConta;
                                Session["Email_Utilizador"] = EmaildaConta;
                                //Invalidar o código de confirmação após ter sido usado
                                String InvalidarCodigo = "UPDATE ContasCodigosdeConfirmacao SET Usado='true' WHERE FKId_Contas=@FKId_Contas AND Objetivo=1";
                                SqlCommand comandoInvalidarCodigo = new SqlCommand(InvalidarCodigo, con);
                                comandoInvalidarCodigo.Parameters.AddWithValue("@FKId_Contas", IdConta);
                                comandoInvalidarCodigo.ExecuteNonQuery();
                                ClientScript.RegisterStartupScript(this.GetType(), "Aviso", "alert('A sua conta foi desbloqueada.');  window.location.href = 'Produtos.aspx';", true);
                            }
                            else if (Objetivo == 4)
                            {
                                //Invalidar o código de confirmação após ter sido usado
                                String InvalidarCodigo = "UPDATE ContasCodigosdeConfirmacao SET Usado='true' WHERE FKId_Contas=@FKId_Contas AND Objetivo=2";
                                SqlCommand comandoInvalidarCodigo = new SqlCommand(InvalidarCodigo, con);
                                comandoInvalidarCodigo.Parameters.AddWithValue("@FKId_Contas", IdConta);
                                comandoInvalidarCodigo.ExecuteNonQuery();
                                Session["SenhaporMudar"] = IdConta;
                                Response.Redirect("MudarPalavrapasse.aspx");
                            }
                        }
                    }
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Microsoft.Win32.SafeHandles;
using System.Data.Odbc;

namespace TAD_Treinamento_a_distancia.Models
{
    public class Usuario
    {


        [Required(ErrorMessage = "O campo é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "Senha deve ter 6 a 8 caracteres")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [RegularExpression(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$",ErrorMessage = "Formato de email invalido")]
        public string Email { get; set; }

        


        public bool Autentica(string login, string senha)
        {

            using (var conexao = ConexaoBd.CreateConnection())
            {
                const string query ="Select LoginUsuario, Nome, Email from Usuario where LoginUsuario = ? and Senha = ?";
                var command = new OdbcCommand (query, conexao);
                conexao.Open();

                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Senha", senha);


                var resultado = command.ExecuteReader();
                if (resultado.Read())
                {

                    UsuarioLogado.Nome = resultado["Nome"].ToString();
                    UsuarioLogado.Login = resultado["LoginUsuario"].ToString();
                    UsuarioLogado.Email = resultado["Email"].ToString();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public string CadastraUsuario(string nome, string email, string login, string senha)
        {
            string msgStatus;

            using (var conexao = ConexaoBd.CreateConnection())
            {
                //INSERT INTO Usuario (Nome, Email, LoginUsuario, Senha) values (?,?,?,?);
                const string dml = "Insert into Usuario(Nome, Email, LoginUsuario, Senha) values (?,?,?,?);";
                var command = new OdbcCommand(dml, conexao);
                conexao.Open();

                command.Parameters.AddWithValue("@Nome", nome);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Senha", senha);

                try
                {
                    command.ExecuteNonQuery();
                    return msgStatus = "Usuario cadastrado com sucesso";
                }
                catch (OdbcException)
                {
                    return msgStatus = "Ocorreu um erro";
                }
                


            }
        }

        public string RecuperarSenha(string email)
        {

            

            using ( var conexao = ConexaoBd.CreateConnection())
            {
                string statusRecuperacao;
                const string query = "Select LoginUsuario, Nome, Senha from Usuario where Email = ?";
                var command = new OdbcCommand(query, conexao);
                conexao.Open();
                command.Parameters.AddWithValue("@Email", email);
                
                var resultado = command.ExecuteReader();

                if (resultado.Read())
                {
                    
                    var login = resultado["LoginUsuario"].ToString();
                    var senha = resultado["Senha"].ToString();
                    var nome = resultado["Nome"].ToString();

                    var m = new Mensagens();
                    return m.EnviaEmail(email, nome + ", Seu Login é: " + login + ", e sua senha é: " + senha, "Recuperação de login ou senha"); 
                }
                else
                {
                    return statusRecuperacao = "Email não encontrado";
                }
            }

            

        }
    }

   





}







   
        
    


   
    



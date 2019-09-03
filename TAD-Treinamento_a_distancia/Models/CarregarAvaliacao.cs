using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace TAD_Treinamento_a_distancia.Models
{
    public class CarregarAvaliacao
    {
        
        public List<string> Enunciados = new List<string>();
        public List<string> Alternativas = new List<string>();
        public List<string> Corretas = new List<string>();
        public List<string> Gabarito = new List<string>();
        int nota = 0;
        
        public void BuscaEnunciados(string treinamento)
        {
            using (var conn = ConexaoBd.CreateConnection())
            {
                const string dml = "SELECT Questao.Enunciado "+
                                    "FROM Teste LEFT JOIN Questao ON Teste.TesteId = Questao.TesteId "+
                                    "WHERE (((Teste.Treinamento)=?)) "+
                                    "ORDER BY Questao.QuestaoNumero;";

                var command = new OdbcCommand(dml, conn);
                conn.Open();
                command.Parameters.AddWithValue("@Treinamento",treinamento);
                var resultado = command.ExecuteReader();
                while (resultado.Read())
                {
                    Enunciados.Add(resultado["Enunciado"].ToString());
                }
            }

        }

        public void BuscaAlternativas(string treinamento, int questao)
        {

            Alternativas.Clear();
            using (OdbcConnection conn = ConexaoBd.CreateConnection())
            {
                const string dml = "SELECT Questao.AlternativaA, Questao.AlternativaB, Questao.AlternativaC, Questao.AlternativaD, Questao.AlternativaE, Questao.AlternativaCorreta " +
                                   "FROM Teste LEFT JOIN Questao ON Teste.TesteId = Questao.TesteId "+
                                   "WHERE (((Teste.Treinamento)=?) AND ((Questao.QuestaoNumero)=?));";

                var command = new OdbcCommand(dml, conn);
                conn.Open();
                command.Parameters.AddWithValue("@Treinamento", treinamento);
                command.Parameters.AddWithValue("@Questao", questao);
                var resultado = command.ExecuteReader();

                
                while (resultado.Read())
                {
                    Alternativas.Add(resultado["AlternativaA"].ToString());
                    Alternativas.Add(resultado["AlternativaB"].ToString());
                    Alternativas.Add(resultado["AlternativaC"].ToString());
                    Alternativas.Add(resultado["AlternativaD"].ToString());
                    Alternativas.Add(resultado["AlternativaE"].ToString());
                    

                }
            }

        }

        public void Corrigir(string treinamento, int questao)
        {

                       
            
            using (OdbcConnection conn = ConexaoBd.CreateConnection())
            {
                const string dql = "SELECT Questao.AlternativaCorreta " +
                                   "FROM Teste LEFT JOIN Questao ON Teste.TesteId = Questao.TesteId " +
                                   "WHERE (((Teste.Treinamento)=?) AND ((Questao.QuestaoNumero)=?));";

                var command = new OdbcCommand(dql, conn);
                conn.Open();
                command.Parameters.AddWithValue("@Treinamento", treinamento);
                command.Parameters.AddWithValue("@Questao", questao);
                var resultado = command.ExecuteReader();


                while (resultado.Read())
                {
                    Corretas.Add(resultado["AlternativaCorreta"].ToString());
                    
                }
            
            }





            if (Corretas[questao-1] == Gabarito[questao-1])
                {
                    nota = nota + 2;
                }
            
            


        }

        public void GravarNota(Pessoa pessoa, string treinamento)
        {

            using (OdbcConnection conexao = ConexaoBd.CreateConnection())
            {

                string grava = "INSERT INTO Historico (LoginUsuario, Treinamento, DataHora, Nome, Nota ) values (?,?,?,?,?);";
                var command = new OdbcCommand(grava, conexao);

                               
                command.Parameters.AddWithValue("@LoginUsuario", pessoa.Usuario.Login);
                command.Parameters.AddWithValue("@Treinamento", treinamento);
                command.Parameters.AddWithValue("@DataHora", String.Format("{0:d/M/yyyy HH:mm:ss}", DateTime.Now));
                command.Parameters.AddWithValue("@Nome", pessoa.Nome);
                command.Parameters.AddWithValue("@Nota", nota);

                conexao.Open();
                command.ExecuteNonQuery();


            }
        }


    }
}
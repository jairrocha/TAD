using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace TAD_Treinamento_a_distancia.Models
{
    public class Prova
    {
        public List<string> Questao = new List<string>(); 
        public  List<string> Resposta = new List<string>();



        public void BuscaProva(string treinamento)
        {
            using (var conn = ConexaoBd.CreateConnection())
            {
                const string queryQuestao = "SELECT Questao.Questao FROM Treinamento LEFT JOIN Questao"+
                " ON Treinamento.IdTreinamento = Questao.idTreinamento WHERE (((Treinamento.Treinamento)= ? ));";

             
    

                var command = new OleDbCommand(queryQuestao, conn);
                conn.Open();
                command.Parameters.AddWithValue("@Treinamento", treinamento);
                
                
                var resultado = command.ExecuteReader();
                while (resultado.Read())
                {
                    Questao.Add(resultado["Questao"].ToString());
                }

            }

        }
        public void BuscaAlternativas(string treinamento, int alternativa)
        {
            const string queryResposta =
                "SELECT Resposta.Alternativa " +
                "FROM Treinamento LEFT JOIN (Questao LEFT JOIN Resposta ON Questao.IdQuestao = Resposta.IdQuestao) " +
                "ON Treinamento.IdTreinamento = Questao.idTreinamento " +
                "WHERE (((Treinamento.Treinamento)=?) AND ((Questao.IdQuestao)=?));"; 

            using (var conexao = ConexaoBd.CreateConnection())
            {
                var command = new OleDbCommand(queryResposta, conexao);
                conexao.Open();
                command.Parameters.AddWithValue("@Treinamento", treinamento);
                command.Parameters.AddWithValue("@Alternativa", alternativa);
                
                
                var resultado = command.ExecuteReader();
                while (resultado.Read())
                {
                    Resposta.Add(resultado["Alternativa"].ToString());
                }

            }

            }
        }
            
    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using System.Data.Odbc;

namespace TAD_Treinamento_a_distancia.Models
{
    public  class CarregarTreinamentos
    {
        public List<string> LinkVideo = new List<string>();
        public List <string> Nome = new List<string>();
        


        public void Busca()
        {
            using (OdbcConnection conn = ConexaoBd.CreateConnection())
            {

                //const string dml = "Select * from LocalAquivo Order by Nome;";
                const string dql = "SELECT Teste.Treinamento, LocalAquivo.Link " +
                                   "FROM Teste LEFT JOIN LocalAquivo ON Teste.TesteId = LocalAquivo.TesteId " +
                                   "ORDER BY Teste.Treinamento;";

                var command = new OdbcCommand(dql, conn);
                conn.Open();
                var resultado = command.ExecuteReader();
                while (resultado.Read())
                {
                    LinkVideo.Add(resultado["Link"].ToString());
                    Nome.Add(resultado["Treinamento"].ToString());
                    
                }

            }
        }

    }
}
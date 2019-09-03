using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;


namespace TAD_Treinamento_a_distancia.Models
{
    public class Historico
    {
        public string LoginUsuario { get; set; }

        public List<string> Treinamento = new List<string>();
        public List<string> DataHora = new List<string>();
        public List<string> Nome = new List<string>();
        public List<string> Nota = new List<string>();
        

        public void RealizaBusca()
        {
            using (OdbcConnection conexao = ConexaoBd.CreateConnection())
            {
                var login = LoginUsuario;
                const string query = "Select * from Historico where LoginUsuario = ? ORDER BY DataHora DESC;";
                var command = new OdbcCommand(query, conexao);
                command.Parameters.AddWithValue("@Login", login);
                
                conexao.Open();
                var resultado = command.ExecuteReader();

                while (resultado.Read())
                {
                    var a = resultado["Treinamento"].ToString();
                    var b = resultado["DataHora"].ToString();
                    var c = resultado["Nome"].ToString();
                    var d = resultado["Nota"].ToString();

                    GravaDados(a, b, c, d);
                    
                }
            }
        }



        private void GravaDados(string treinamento, string dataHora, string nome, string nota)
        {
            this.Treinamento.Add(treinamento);
            this.DataHora.Add(dataHora);
            this.Nome.Add(nome);
            this.Nota.Add(nota);
        }

    }
}
                    
                

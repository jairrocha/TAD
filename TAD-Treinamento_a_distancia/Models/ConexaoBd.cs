using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;
using System.Text;


namespace TAD_Treinamento_a_distancia.Models
{
    public class ConexaoBd
    {

        public static OdbcConnection CreateConnection()
        {
            /*string stringDeConexao =@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+
            @"C:\Users\jairm\Desktop\TAD-Treinamento_a_distancia\TAD-Treinamento_a_distancia\App_Data\TAD.accdb;" +
            @"Persist Security Info=True;";*/

           string stringDeConexao = @"Driver={Microsoft Access Driver (*.mdb, *.accdb)}; Dbq=C:\Users\jairm\Desktop\TAD-Treinamento_a_distancia\TAD-Treinamento_a_distancia\App_Data\TAD.accdb; Uid=Admin; Pwd =;";
           
            


            return new OdbcConnection(stringDeConexao.ToString()); 
        }



    }


    

}
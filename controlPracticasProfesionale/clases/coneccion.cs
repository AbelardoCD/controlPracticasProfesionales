using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace controlPracticasProfesionale.clases
{
    public class coneccion
    {

        private string datasource = "localhost";
        private string database = "practilis";
        private string username = "root";
        private string passworrd = "";
        private MySqlConnection conn = new MySqlConnection();
        public MySqlConnection con()
        {
            conn.ConnectionString = "datasource=" + datasource + "; database = " + database + "; uid = " + username + " ;pwd=" + passworrd + ";";

            try
            {
                // conn.Open();
                Console.WriteLine("Se abrio la conexion");

                return conn;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return conn;

            }
            // conn.Close();
        }

    }
}
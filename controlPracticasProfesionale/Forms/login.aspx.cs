using controlPracticasProfesionale.clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace controlPracticasProfesionale.Forms
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static estudiante getRegistro(string matricula, string password)
        {

            coneccion c = new coneccion();
            MySqlConnection con = c.con();
            estudiante item = new estudiante();
            DataSet ds = new DataSet();

            try
            {


                con.Open();
                string sql = "";

                sql = "select e.matricula,e.contrasenia,e.nombre,e.correoElectronico,e.status, t.tipoUsuario FROM estudiante e" +
                        " JOIN tipoUsuario t ON (t.idTipoUsuario = e.idTipoUsario)" +
                        "  WHERE e.matricula = @matricula and e.contrasenia = @contrasenia";



                MySqlDataAdapter adp = new MySqlDataAdapter(sql, con);
                adp.SelectCommand.Parameters.AddWithValue("@matricula", matricula);
                adp.SelectCommand.Parameters.AddWithValue("@contrasenia", password);

                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        item.matricula = ds.Tables[0].Rows[i]["matricula"].ToString();

                        item.contrasenia = ds.Tables[0].Rows[i]["contrasenia"].ToString();
                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.correoElectronico = ds.Tables[0].Rows[i]["correoElectronico"].ToString();
                        item.status = ds.Tables[0].Rows[i]["status"].ToString();
                        item.tipoUsuario= ds.Tables[0].Rows[i]["tipoUsuario"].ToString();

                    }
                }





                return item;
            }
            catch (Exception ex)
            {
                Debug.Write("Error ... " + ex.Message);
                Debug.Write(ex.StackTrace);
                return item;
            }

            finally
            {
                con.Close();
            }

        }

    }
}
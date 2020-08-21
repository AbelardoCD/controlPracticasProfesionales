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
    public partial class encargadoProyecto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<encargadoProyectoPojo> getRegistros()
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            List<encargadoProyectoPojo> items = new List<encargadoProyectoPojo>();

            try {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "SELECT nombre,cargo,correoElectronico,idEncargadoProyecto FROM encargadoproyecto";
                MySqlDataAdapter mda = new MySqlDataAdapter(query, coneccion);
                mda.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                        encargadoProyectoPojo item = new encargadoProyectoPojo();

                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.cargo = ds.Tables[0].Rows[i]["cargo"].ToString();
                        item.correo = ds.Tables[0].Rows[i]["correoElectronico"].ToString();
                        item.idEncargadoProyecto = ds.Tables[0].Rows[i]["idEncargadoProyecto"].ToString();
                        items.Add(item);
                    }
                }

                return items;
            } catch (Exception ex) {
                Debug.Write("Error......." + ex.Message);
                return items;
            }
            finally
            {
                coneccion.Close();
            }

        }


        [WebMethod]
        public static object guardar(encargadoProyectoPojo encargadoProyecto , string accion) {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();

            try {
                coneccion.Open();
                string query = "";
                if (accion =="Nuevo") {
                    query = "INSERT INTO encargadoproyecto(nombre,cargo,correoElectronico) VALUES(@nombre,@cargo,@correoElectronico)";
                }
                else {
                    query = "UPDATE encargadoproyecto SET nombre =@nombre,cargo = @cargo,correoElectronico = @correoElectronico WHERE idEncargadoProyecto = @id";
                }

                MySqlCommand comand = new MySqlCommand(query,coneccion);
                comand.CommandType = CommandType.Text;
                comand.Parameters.Add("nombre", MySqlDbType.VarChar).Value = encargadoProyecto.nombre;
                comand.Parameters.Add("cargo",MySqlDbType.VarChar).Value = encargadoProyecto.cargo;
                comand.Parameters.Add("correoElectronico",MySqlDbType.VarChar).Value = encargadoProyecto.correo;
                comand.Parameters.Add("@id", MySqlDbType.Int64).Value = encargadoProyecto.idEncargadoProyecto;

                int r = comand.ExecuteNonQuery();
                return r;
            } catch (Exception ex) {
                Debug.Write("Error........."  + ex.Message);
                return -1;

            }
            finally
            {
                coneccion.Close();
            }
        }


        [WebMethod]
        public static object eliminar(string id)
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();

            try {
                coneccion.Open();
                string query = "DELETE FROM encargadoproyecto WHERE idEncargadoProyecto =@id";
                MySqlCommand msc = new MySqlCommand(query,coneccion);
                msc.CommandType = CommandType.Text;
                msc.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
                int r = msc.ExecuteNonQuery();
                return r;

            } catch (Exception ex) {
                Debug.Write("Error......." + ex.Message );
                return -1;
            }
        }
        [WebMethod]
        public static encargadoProyectoPojo getRegistro(string id) {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            encargadoProyectoPojo item = new encargadoProyectoPojo();
            try {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "SELECT nombre,cargo,correoElectronico,idEncargadoProyecto FROM encargadoproyecto WHERE idEncargadoProyecto = @id";
                MySqlDataAdapter Msa = new MySqlDataAdapter(query,coneccion);
                Msa.SelectCommand.Parameters.AddWithValue("@id", id);
                Msa.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0) {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {

                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.cargo = ds.Tables[0].Rows[i]["cargo"].ToString();
                        item.correo = ds.Tables[0].Rows[i]["correoElectronico"].ToString();
                        item.idEncargadoProyecto = ds.Tables[0].Rows[i]["idEncargadoProyecto"].ToString();

                    }
                }
                return item;

            } catch (Exception ex) {
                Debug.Write("Error..." +  ex.Message);
                return item;
            }
            finally
            {
                coneccion.Close();
            }


        }

    }
}
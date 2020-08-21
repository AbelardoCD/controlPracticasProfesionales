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
    public partial class organizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<organizacionPojo> getOrganizaciones()
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            List<organizacionPojo> items = new List<organizacionPojo>();

            try {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "select nombre,direccion,ciudad,estado,telefono,correoElectronico,sector,idOrganizacion FROM organizacion";
                MySqlDataAdapter mda = new MySqlDataAdapter(query,coneccion);
                mda.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0) {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        organizacionPojo organizacion = new organizacionPojo();
                        organizacion.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        organizacion.direccion = ds.Tables[0].Rows[i]["direccion"].ToString();
                        organizacion.ciudad = ds.Tables[0].Rows[i]["ciudad"].ToString();
                        organizacion.estado = ds.Tables[0].Rows[i]["estado"].ToString();
                        organizacion.telefono = ds.Tables[0].Rows[i]["telefono"].ToString();
                        organizacion.correo = ds.Tables[0].Rows[i]["correoElectronico"].ToString();
                        organizacion.sector = ds.Tables[0].Rows[i]["sector"].ToString();
                        organizacion.idOrganizacion = ds.Tables[0].Rows[i]["idOrganizacion"].ToString();

                        items.Add(organizacion);
                    }
                }
                return items;

            } catch (Exception ex) {
                Debug.Write("Error........." + ex.Message);
                return items;
            }
            finally
            {
                coneccion.Close();
            }

        }
        [WebMethod]
        public static object guardar(organizacionPojo organizacion, string accion) {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();

            try {
                coneccion.Open();
                string query = "";
                if (accion =="Nuevo") {
                    query = "insert into organizacion(nombre,direccion,ciudad,estado,telefono,correoElectronico,sector) values(@nombre,@direccion,@ciudad,@estado,@telefono,@correoElectronico,@sector)";

                }
                else {
                    query = "UPDATE organizacion SET nombre=@nombre,direccion =@direccion,ciudad=@ciudad,estado=@estado,telefono=@telefono,correoElectronico = @correoElectronico, sector=@sector WHERE idOrganizacion = @id";
                }
                MySqlCommand comand = new MySqlCommand(query,coneccion);
                comand.CommandType = CommandType.Text;
                comand.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = organizacion.nombre;
                comand.Parameters.Add("@direccion", MySqlDbType.VarChar).Value = organizacion.direccion;
                comand.Parameters.Add("@ciudad", MySqlDbType.VarChar).Value = organizacion.ciudad;
                comand.Parameters.Add("@estado", MySqlDbType.VarChar).Value = organizacion.estado;
                comand.Parameters.Add("@telefono", MySqlDbType.VarChar).Value = organizacion.telefono;
                comand.Parameters.Add("@correoElectronico", MySqlDbType.VarChar).Value = organizacion.correo;
                comand.Parameters.Add("@sector",MySqlDbType.VarChar).Value =organizacion.sector;

                comand.Parameters.Add("@id",MySqlDbType.Int64).Value = organizacion.idOrganizacion;
                int r = comand.ExecuteNonQuery();
                return r;

            } catch (Exception ex) {
                Debug.Write("Error....."  + ex.Message);
                return -1;
            }
            finally
            {
                coneccion.Close();
            }
        }

        [WebMethod]
        public static organizacionPojo getRegistro( string id)
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();

            organizacionPojo organizacion = new organizacionPojo();
            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "select nombre,direccion,ciudad,estado,telefono,correoElectronico,sector,idOrganizacion FROM organizacion WHERE idOrganizacion = @id";
                MySqlDataAdapter mda = new MySqlDataAdapter(query, coneccion);
                mda.SelectCommand.Parameters.AddWithValue("@id", id);
                mda.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        organizacion.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        organizacion.direccion = ds.Tables[0].Rows[i]["direccion"].ToString();
                        organizacion.ciudad = ds.Tables[0].Rows[i]["ciudad"].ToString();
                        organizacion.estado = ds.Tables[0].Rows[i]["estado"].ToString();
                        organizacion.telefono = ds.Tables[0].Rows[i]["telefono"].ToString();
                        organizacion.correo = ds.Tables[0].Rows[i]["correoElectronico"].ToString();
                        organizacion.sector = ds.Tables[0].Rows[i]["sector"].ToString();
                        organizacion.idOrganizacion = ds.Tables[0].Rows[i]["idOrganizacion"].ToString();

                    }
                    
                }
                return organizacion;

            }
            catch (Exception ex)
            {
                Debug.Write("Error........." + ex.Message);
                return organizacion;
            }
            finally
            {
                coneccion.Close();
            }

        }

        [WebMethod]
        public static object eliminarRegistro(string id)
        {

            coneccion c = new coneccion();
            MySqlConnection con = c.con();

            try
            {


                con.Open();
                string sql = "";

                sql = " DELETE from organizacion " +
                        " WHERE idOrganizacion = @id ";



                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;


                int r = cmd.ExecuteNonQuery();
                Debug.Write("r = " + r);
                Debug.Write("Eliminado -> OK ");



                return r;
            }
            catch (Exception ex)
            {
                Debug.Write("Error ... " + ex.Message);
                Debug.Write(ex.StackTrace);
                return -1;
            }

            finally
            {
                con.Close();
            }

        }
    }
}
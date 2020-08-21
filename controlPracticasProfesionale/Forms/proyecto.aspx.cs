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
    public partial class proyecto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<proyectoJOINOrganizacion> listarProyectos()
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            List<proyectoJOINOrganizacion> items = new List<proyectoJOINOrganizacion>();

            try {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "SELECT p.nombre, p.descripcion,p.capacidadEstudiantes, " +
                               "p.numEstudiantesAsignados,p.idProyecto,p.status, o.nombre AS nombreOrganizacion,e.nombre AS nombreEncargado " +
                               "FROM proyecto p " +
                               "JOIN organizacion o ON(p.idOrganizacion = o.idOrganizacion) " +
                               "JOIN encargadoproyecto e ON(p.idEncargadoProyecto = e.idEncargadoProyecto)";

                MySqlDataAdapter msda = new MySqlDataAdapter(query, coneccion);
                msda.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                        proyectoJOINOrganizacion item = new proyectoJOINOrganizacion();
                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.descripcion = ds.Tables[0].Rows[i]["descripcion"].ToString();
                        item.capacidadEstudiantes = ds.Tables[0].Rows[i]["capacidadEstudiantes"].ToString();
                        item.numEstudiantesAsignados = ds.Tables[0].Rows[i]["numEstudiantesAsignados"].ToString();
                        item.idProyecto = ds.Tables[0].Rows[i]["idProyecto"].ToString();
                        item.status = ds.Tables[0].Rows[i]["status"].ToString();
                        item.Organizacion = ds.Tables[0].Rows[i]["nombreOrganizacion"].ToString();
                        item.EncargadoProyecto = ds.Tables[0].Rows[i]["nombreEncargado"].ToString();
                        items.Add(item);
                    }
                }
                return items;
            } catch (Exception ex) {
                Debug.Write("Error...." + ex.Message);
                return items;
            }
            finally
            {
                coneccion.Close();
            }

        }
        [WebMethod]
        public static List<encargadoProyectoPojo> listarEncargadosProyectos()
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            List<encargadoProyectoPojo> items = new List<encargadoProyectoPojo>();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "SELECT nombre,cargo,correoElectronico,idEncargadoProyecto FROM encargadoProyecto";

                MySqlDataAdapter msda = new MySqlDataAdapter(query, coneccion);
                msda.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        encargadoProyectoPojo item = new encargadoProyectoPojo();
                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.cargo = ds.Tables[0].Rows[i]["cargo"].ToString();
                        item.correo = ds.Tables[0].Rows[i]["correoElectronico"].ToString();
                        item.idEncargadoProyecto = ds.Tables[0].Rows[i]["idEncargadoProyecto"].ToString();
                        items.Add(item);
                    }
                }
                return items;
            }
            catch (Exception ex)
            {
                Debug.Write("Error...." + ex.Message);
                return items;
            }
            finally
            {
                coneccion.Close();
            }

        }

        [WebMethod]
        public static List<organizacionPojo> listarOrganizaciones()
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            List<organizacionPojo> items = new List<organizacionPojo>();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "SELECT nombre,direccion,ciudad,estado,telefono, correoElectronico,sector,idOrganizacion " +
                                "FROM organizacion";

                MySqlDataAdapter msda = new MySqlDataAdapter(query, coneccion);
                msda.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        organizacionPojo item = new organizacionPojo();
                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.direccion = ds.Tables[0].Rows[i]["direccion"].ToString();
                        item.correo = ds.Tables[0].Rows[i]["correoElectronico"].ToString();
                        item.idOrganizacion = ds.Tables[0].Rows[i]["idOrganizacion"].ToString();
                        items.Add(item);
                    }
                }
                return items;
            }
            catch (Exception ex)
            {
                Debug.Write("Error...." + ex.Message);
                return items;
            }
            finally
            {
                coneccion.Close();
            }

        }

        [WebMethod]
        public static object guardar(proyectos proyecto, string accion) {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            try {
                coneccion.Open();
                string query = "";
                if (accion == "Nuevo")
                {
                    query = "INSERT INTO proyecto(nombre,descripcion,capacidadEstudiantes,numEstudiantesAsignados,status," +
                        "   idOrganizacion,idEncargadoProyecto) VALUES(@nombre,@descripcion,@capacidadEstudiantes,@numEstudiantesAsignados," +
                        "   @status, @idOrganizacion,@idEncargadoProyecto)";
                }
                else
                {
                    query = "UPDATE SET nombre=@nombre,descripcion=@descripcion,capacidadEstudiantes=@capacidadEstudiantes,numEstudiantesAsignados =@numEstudiantesAsignados, status=@status,idOrganizacion =@idOrganizacion ,idEncargadoProyecto=@idEncargadoProyecto" +
                        "       FROM proyecto WHERE idProyecto = @id";
                }

                MySqlCommand cmd = new MySqlCommand(query, coneccion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = proyecto.nombre;
                cmd.Parameters.Add("@descripcion", MySqlDbType.VarChar).Value = proyecto.descripcion;
                cmd.Parameters.Add("@capacidadEstudiantes", MySqlDbType.VarChar).Value = proyecto.capacidadEstudiantes;
                cmd.Parameters.Add("@numEstudiantesAsignados", MySqlDbType.VarChar).Value = proyecto.numEstudiantesAsignados;
                cmd.Parameters.Add("@status", MySqlDbType.VarChar).Value = proyecto.status;
                cmd.Parameters.Add("@idOrganizacion", MySqlDbType.Int64).Value = proyecto.idOrganizacion;
                cmd.Parameters.Add("@idEncargadoProyecto", MySqlDbType.Int64).Value = proyecto.idEncargadoProyecto;

                cmd.Parameters.Add("@id", MySqlDbType.Int64).Value = proyecto.idProyecto;

                int r = cmd.ExecuteNonQuery();
                return r;
            } catch (Exception ex) {
                Debug.Write("Error..." + ex.Message);
                return -1;

            }
            finally
            {
                coneccion.Close();
            }

        }

        [WebMethod]
        public static proyectoJOINOrganizacion getProyecto(string id)
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            proyectoJOINOrganizacion item = new proyectoJOINOrganizacion();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "SELECT p.nombre, p.descripcion,p.capacidadEstudiantes, " +
                               "p.numEstudiantesAsignados,p.idProyecto,p.status, o.idOrganizacion,o.nombre AS nombreOrganizacion,e.idEncargadoProyecto,e.nombre AS nombreEncargado " +
                               "FROM proyecto p " +
                               "JOIN organizacion o ON(p.idOrganizacion = o.idOrganizacion) " +
                               "JOIN encargadoproyecto e ON(p.idEncargadoProyecto = e.idEncargadoProyecto)" +
                               "WHERE idProyecto =@id";

                MySqlDataAdapter msda = new MySqlDataAdapter(query, coneccion);

                msda.SelectCommand.Parameters.AddWithValue("@id", id);
                msda.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.descripcion = ds.Tables[0].Rows[i]["descripcion"].ToString();
                        item.capacidadEstudiantes = ds.Tables[0].Rows[i]["capacidadEstudiantes"].ToString();
                        item.numEstudiantesAsignados = ds.Tables[0].Rows[i]["numEstudiantesAsignados"].ToString();
                        item.idProyecto = ds.Tables[0].Rows[i]["idProyecto"].ToString();
                        item.status = ds.Tables[0].Rows[i]["status"].ToString();
                        item.Organizacion = ds.Tables[0].Rows[i]["idOrganizacion"].ToString();
                        item.EncargadoProyecto = ds.Tables[0].Rows[i]["idEncargadoProyecto"].ToString();

                    }
                }
                return item;
            }
            catch (Exception ex)
            {
                Debug.Write("Error...." + ex.Message);
                return item;
            }
            finally
            {
                coneccion.Close();
            }

        }


        [WebMethod]
        public static object eliminar(string id) {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            try
            {
                coneccion.Open();
                string query = "DELETE FROM proyecto WHERE idProyecto = @id";
                MySqlCommand msc = new MySqlCommand(query, coneccion);
                msc.CommandType = CommandType.Text;
                msc.Parameters.Add("@id",MySqlDbType.Int64).Value = id;
                int r = msc.ExecuteNonQuery();
                return r;
            }
            catch (Exception ex) {
                Debug.Write("Error..." + ex.Message);
                return -1;
            }
            finally
            {
                coneccion.Close();
            }

        } 
    }
}
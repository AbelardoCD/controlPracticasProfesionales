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
    public partial class elegirProyectos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        [WebMethod]
        public static List<proyectos> GetProyectos()
        {
            //Traemos la conexion
            coneccion c = new coneccion();
            //Creamos un objeto de tipo MysqlConextion y le asignamos el metodo donde viene la conexion.
            MySqlConnection coneccion = c.con();
            //Creamos una lista de tipo producto, ahi guardaremos objetos de los registros de la bd
            List<proyectos> items = new List<proyectos>();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "Select nombre, descripcion, capacidadEstudiantes, numEstudiantesAsignados,idProyecto,status," +
                    "idOrganizacion,idEncargadoProyecto from proyecto ";
                // MySqlCommand  mysc = new MySqlCommand(query, coneccion);
                MySqlDataAdapter mAdapter = new MySqlDataAdapter(query, coneccion);
                mAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        proyectos item = new proyectos();
                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();

                        item.descripcion = ds.Tables[0].Rows[i]["descripcion"].ToString();
                        item.capacidadEstudiantes = ds.Tables[0].Rows[i]["capacidadEstudiantes"].ToString();
                        item.numEstudiantesAsignados = ds.Tables[0].Rows[i]["numEstudiantesAsignados"].ToString();// float.Parse(ds.Tables[0].Rows[i]["precio"].ToString());
                        item.idProyecto = ds.Tables[0].Rows[i]["idProyecto"].ToString(); //int.Parse(ds.Tables[0].Rows[i]["stock"].ToString());
                        item.status = ds.Tables[0].Rows[i]["status"].ToString();
                        item.idOrganizacion= ds.Tables[0].Rows[i]["idOrganizacion"].ToString();
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
        public static List<proyectos> GetSolicitudes(string matricula)
        {
            //Traemos la conexion
            coneccion c = new coneccion();
            //Creamos un objeto de tipo MysqlConextion y le asignamos el metodo donde viene la conexion.
            MySqlConnection coneccion = c.con();
            //Creamos una lista de tipo producto, ahi guardaremos objetos de los registros de la bd
            List<proyectos> items = new List<proyectos>();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "  select p.nombre, s.idProyecto From proyecto p JOIN solicitud s ON (p.idProyecto = s.idProyecto) where s.matricula = @matricula";
                // MySqlCommand  mysc = new MySqlCommand(query, coneccion);
                MySqlDataAdapter mAdapter = new MySqlDataAdapter(query, coneccion);
                mAdapter.SelectCommand.Parameters.AddWithValue("@matricula", matricula);

                mAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        proyectos item = new proyectos();
                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();

                        item.idProyecto = ds.Tables[0].Rows[i]["idProyecto"].ToString();
                        

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
        public static object GuardarRegistro(solicitud solicitud)
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();


            //comandatabase.CommandTimeout = 60;
            //  MySqlDataReader reader;

            try
            {
                coneccion.Open();
                string query = "";
                
                    query = "insert into solicitud(preriodo,fecha,idProyecto,matricula) values(@preriodo,@fecha,@idProyecto,@matricula)";

               
                MySqlCommand comandatabase = new MySqlCommand(query, coneccion);

                // reader = comandatabase.ExecuteReader();
                comandatabase.CommandType = CommandType.Text;

                comandatabase.Parameters.Add("@preriodo", MySqlDbType.VarChar).Value = solicitud.periodo;
                comandatabase.Parameters.Add("@fecha", MySqlDbType.VarChar).Value = solicitud.fecha;
                comandatabase.Parameters.Add("@idProyecto", MySqlDbType.Int64).Value = solicitud.idProyecto;
                comandatabase.Parameters.Add("@matricula", MySqlDbType.VarChar).Value = solicitud.matricula;

                int r = comandatabase.ExecuteNonQuery();
                return r;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                Debug.Write("Error...." + ex.Message);

                return -1; //Retornamos menos uno cuando se dió por alguna razón un error
            }
            finally
            {
                coneccion.Close();
            }
        }

    }
}
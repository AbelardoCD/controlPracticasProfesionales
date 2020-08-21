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
    public partial class asignarProyectos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<estudiante> GetEstudiantes()
        {
            //Traemos la conexion
            coneccion c = new coneccion();
            //Creamos un objeto de tipo MysqlConextion y le asignamos el metodo donde viene la conexion.
            MySqlConnection coneccion = c.con();
            //Creamos una lista de tipo producto, ahi guardaremos objetos de los registros de la bd
            List<estudiante> items = new List<estudiante>();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "Select e.matricula, e.contrasenia, e.nombre, e.correoElectronico,e.status, t.tipoUsuario " +
                    "from estudiante e JOIN tipoUsuario t ON (e.idTipoUsario = t.idTipoUsuario) WHERE t.tipoUsuario = 2";
                // MySqlCommand  mysc = new MySqlCommand(query, coneccion);
                MySqlDataAdapter mAdapter = new MySqlDataAdapter(query, coneccion);
                mAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        estudiante item = new estudiante();
                        item.matricula = ds.Tables[0].Rows[i]["matricula"].ToString();

                        item.contrasenia = ds.Tables[0].Rows[i]["contrasenia"].ToString();
                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.correoElectronico = ds.Tables[0].Rows[i]["correoElectronico"].ToString();// float.Parse(ds.Tables[0].Rows[i]["precio"].ToString());
                        item.status = ds.Tables[0].Rows[i]["status"].ToString(); //int.Parse(ds.Tables[0].Rows[i]["stock"].ToString());
                        item.tipoUsuario= ds.Tables[0].Rows[i]["TipoUsuario"].ToString();

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
        public static List<solicitudProyecto> getProyectos(string matricula) {
            coneccion c = new coneccion();
            //Creamos un objeto de tipo MysqlConextion y le asignamos el metodo donde viene la conexion.
            MySqlConnection coneccion = c.con();
            //Creamos una lista de tipo producto, ahi guardaremos objetos de los registros de la bd
            List<solicitudProyecto> items = new List<solicitudProyecto>();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "Select s.preriodo, s.fecha, s.idProyecto, s.matricula,p.nombre" +
                    " from solicitud s JOIN proyecto p ON (s.idProyecto = p.idProyecto) WHERE s.matricula  = @matricula";
                // MySqlCommand  mysc = new MySqlCommand(query, coneccion);
                MySqlDataAdapter mAdapter = new MySqlDataAdapter(query, coneccion);
                mAdapter.SelectCommand.Parameters.AddWithValue("@matricula", matricula);

                mAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        solicitudProyecto item = new solicitudProyecto();
                        item.periodo = ds.Tables[0].Rows[i]["preriodo"].ToString();

                        item.fecha = ds.Tables[0].Rows[i]["fecha"].ToString();
                        item.idProyecto = ds.Tables[0].Rows[i]["idProyecto"].ToString();
                        item.matricula= ds.Tables[0].Rows[i]["matricula"].ToString();// float.Parse(ds.Tables[0].Rows[i]["precio"].ToString());
                        item.nombre= ds.Tables[0].Rows[i]["nombre"].ToString();// float.Parse(ds.Tables[0].Rows[i]["precio"].ToString());

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
        public static List<profesores> getProfesores()
        {
            coneccion c = new coneccion();
            //Creamos un objeto de tipo MysqlConextion y le asignamos el metodo donde viene la conexion.
            MySqlConnection coneccion = c.con();
            //Creamos una lista de tipo producto, ahi guardaremos objetos de los registros de la bd
            List<profesores> items = new List<profesores>();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "Select contrasenia,usuario, matricula FROM profesor";
                // MySqlCommand  mysc = new MySqlCommand(query, coneccion);
                MySqlDataAdapter mAdapter = new MySqlDataAdapter(query, coneccion);

                mAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        profesores item = new profesores();
                        item.contrasenia = ds.Tables[0].Rows[i]["contrasenia"].ToString();

                        item.usuario = ds.Tables[0].Rows[i]["usuario"].ToString();
                        item.matricula = ds.Tables[0].Rows[i]["matricula"].ToString();

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
        public static object setAsignacion(asignacion asignacion) {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();



            try
            {
                coneccion.Open();
                string query = "";

                query = "insert into asignacion(preriodo,nrCCurso,progreso,idProyecto,matriculaProfesor,matriculaEstudiante) values(@preriodo,@nrCCurso,@progreso,@idProyecto,@matriculaProfesor,@matriculaEstudiante)";


                MySqlCommand comandatabase = new MySqlCommand(query, coneccion);

                comandatabase.CommandType = CommandType.Text;

                comandatabase.Parameters.Add("@preriodo", MySqlDbType.VarChar).Value = asignacion.periodo;
                comandatabase.Parameters.Add("@nrCCurso", MySqlDbType.VarChar).Value = asignacion.nrcCurso;
                comandatabase.Parameters.Add("@progreso", MySqlDbType.Int64).Value = asignacion.progreso;
                comandatabase.Parameters.Add("@idProyecto", MySqlDbType.VarChar).Value = asignacion.idProyecto;
                comandatabase.Parameters.Add("@matriculaProfesor", MySqlDbType.VarChar).Value = asignacion.matriculaProfesor;
                comandatabase.Parameters.Add("@matriculaEstudiante", MySqlDbType.VarChar).Value = asignacion.matriculaEstudiante;

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


        [WebMethod]
        public static List<asignacionJoinEstudianteJoinProfesor> GetAsignaciones()
        {
            //Traemos la conexion
            coneccion c = new coneccion();
            //Creamos un objeto de tipo MysqlConextion y le asignamos el metodo donde viene la conexion.
            MySqlConnection coneccion = c.con();
            //Creamos una lista de tipo producto, ahi guardaremos objetos de los registros de la bd
            List<asignacionJoinEstudianteJoinProfesor> items = new List<asignacionJoinEstudianteJoinProfesor>();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = " select  e.nombre,p.nombre as proyecto, pr.usuario as profesor,a.matriculaEstudiante FROM asignacion a "+
                                "JOIN estudiante e ON (a.matriculaEstudiante = e.matricula) "+
                                "JOIN proyecto p ON (a.idProyecto = p.idProyecto) "+
                                "JOIN profesor pr ON (a.matriculaProfesor = pr.matricula)";
                // MySqlCommand  mysc = new MySqlCommand(query, coneccion);
                MySqlDataAdapter mAdapter = new MySqlDataAdapter(query, coneccion);
                mAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        asignacionJoinEstudianteJoinProfesor item = new asignacionJoinEstudianteJoinProfesor();
                        item.nombreAlumno = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.proyecto = ds.Tables[0].Rows[i]["proyecto"].ToString();
                        item.profesor = ds.Tables[0].Rows[i]["profesor"].ToString();
                        item.matriculaAlumno= ds.Tables[0].Rows[i]["matriculaEstudiante"].ToString();

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
        public static asignacion consultarAsignacion(string matricula)
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            asignacion item = new asignacion();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "SELECT preriodo,idAsignacion,nrcCurso, progreso,idProyecto,matriculaProfesor,matriculaEstudiante FROM asignacion" +
                    " WHERE matriculaEstudiante = @matricula";

                MySqlDataAdapter msda = new MySqlDataAdapter(query, coneccion);

                msda.SelectCommand.Parameters.AddWithValue("@matricula", matricula);
                msda.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        item.periodo= ds.Tables[0].Rows[i]["preriodo"].ToString();
                        item.idAsignacion= ds.Tables[0].Rows[i]["idAsignacion"].ToString();
                        item.matriculaEstudiante = ds.Tables[0].Rows[i]["matriculaEstudiante"].ToString();


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
        public static object guardarExpediente(string id)
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();



            try
            {
                coneccion.Open();
                string query = "";

                query = "insert into expediente(idAsignacion) values(@idAsignacion)";


                MySqlCommand comandatabase = new MySqlCommand(query, coneccion);

                comandatabase.CommandType = CommandType.Text;

                comandatabase.Parameters.Add("@idAsignacion", MySqlDbType.VarChar).Value = id;
                

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
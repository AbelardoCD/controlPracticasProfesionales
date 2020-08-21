using controlPracticasProfesionale.clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace controlPracticasProfesionale.Forms
{
    public partial class menuPrincipal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<asignacionJoinEstudianteJoinProfesor> GetAsignaciones(string matricula)
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
                string query = " select  e.nombre,p.nombre as proyecto, pr.usuario as profesor FROM asignacion a " +
                                "JOIN estudiante e ON (a.matriculaEstudiante = e.matricula) " +
                                "JOIN proyecto p ON (a.idProyecto = p.idProyecto) " +
                                "JOIN profesor pr ON (a.matriculaProfesor = pr.matricula)" +
                                " WHERE e.matricula = @matricula";
                // MySqlCommand  mysc = new MySqlCommand(query, coneccion);
                MySqlDataAdapter mAdapter = new MySqlDataAdapter(query, coneccion);
                mAdapter.SelectCommand.Parameters.AddWithValue("@matricula", matricula);
                mAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        asignacionJoinEstudianteJoinProfesor item = new asignacionJoinEstudianteJoinProfesor();
                        item.nombreAlumno = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.proyecto = ds.Tables[0].Rows[i]["proyecto"].ToString();
                        item.profesor = ds.Tables[0].Rows[i]["profesor"].ToString();

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
        public static List<expediente> consultarExpediente(string matricula)
        {
            //Traemos la conexion
            coneccion c = new coneccion();
            //Creamos un objeto de tipo MysqlConextion y le asignamos el metodo donde viene la conexion.
            MySqlConnection coneccion = c.con();
            //Creamos una lista de tipo producto, ahi guardaremos objetos de los registros de la bd
            List<expediente> items = new List<expediente>();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "select e.idAsignacion,e.idExpediente,a.idProyecto,p.nombre FROM expediente e" +
                        " JOIN asignacion a ON(e.idAsignacion = a.idAsignacion)" +
                        " JOIN proyecto p ON(a.idProyecto = p.idProyecto)" +
                        " WHERE a.matriculaEstudiante = @matricula";
                // MySqlCommand  mysc = new MySqlCommand(query, coneccion);
                MySqlDataAdapter mAdapter = new MySqlDataAdapter(query, coneccion);
                mAdapter.SelectCommand.Parameters.AddWithValue("@matricula", matricula);
                mAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        expediente item = new expediente();
                        item.idExpediente = ds.Tables[0].Rows[i]["idExpediente"].ToString();
                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();


                        items.Add(item);
                    }
                }
                return items;

            }
            catch (Exception ex)
            {
                Debug.Write("Error...." + ex);
                return items;
            }
            finally
            {
                coneccion.Close();
            }

        }

        [WebMethod]
        public static List<reporteJOINExpediente> getReportes(string matricula, int tipoUsuario, string matriculaAlumno)
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            List<reporteJOINExpediente> items = new List<reporteJOINExpediente>();


            try
            {
                string query = "";
                coneccion.Open();
                DataSet ds = new DataSet();
               
                    query =
                    "SELECT p.nombre,a.matriculaEstudiante,e.idExpediente,r.horasReportadas,r.fechaCarga,r.estado,r.fechaInicio,r.fechaFin,r.numeroReporte,es.nombre as nombreEstudiante " +
                    " FROM proyecto as p" +
                    " JOIN asignacion a ON(p.idProyecto = a.idProyecto)" +
                    " JOIN expediente e ON(a.idAsignacion = e.idAsignacion)" +
                    " JOIN reporte r ON(e.idExpediente = r.idExpediente) " +
                    " JOIN estudiante es ON(es.matricula = a.matriculaEstudiante) " +
                    " WHERE matriculaEstudiante = @matriculaAlumno   ";

             




                MySqlDataAdapter mda = new MySqlDataAdapter(query, coneccion);
                mda.SelectCommand.Parameters.AddWithValue("@matriculaAlumno", matriculaAlumno);
                mda.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reporteJOINExpediente organizacion = new reporteJOINExpediente();
                        organizacion.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        organizacion.matriculaEstudiante = ds.Tables[0].Rows[i]["matriculaEstudiante"].ToString();
                        organizacion.horas = ds.Tables[0].Rows[i]["horasReportadas"].ToString();
                        organizacion.fechaCarga = ds.Tables[0].Rows[i]["fechaCarga"].ToString();
                        organizacion.estado = ds.Tables[0].Rows[i]["estado"].ToString();
                        organizacion.fechaInicio = ds.Tables[0].Rows[i]["FechaInicio"].ToString();
                        organizacion.fechaFin = ds.Tables[0].Rows[i]["FechaFin"].ToString();
                        organizacion.idExpediente = ds.Tables[0].Rows[i]["idExpediente"].ToString();
                        organizacion.numeroReporte = ds.Tables[0].Rows[i]["numeroReporte"].ToString();
                        organizacion.nombreEstudiante = ds.Tables[0].Rows[i]["nombreEstudiante"].ToString();
                        items.Add(organizacion);
                    }
                }
                return items;

            }
            catch (Exception ex)
            {
                Debug.Write("Error........." + ex.Message);
                Debug.Write("Error........." + ex.StackTrace);

                return items;
            }
            finally
            {
                coneccion.Close();
            }

        }


        [WebMethod]
        public static List<estudiante> getEstudiantes(string matricula, int tipoUsuario)
        {
            //Traemos la conexion
            coneccion c = new coneccion();
            //Creamos un objeto de tipo MysqlConextion y le asignamos el metodo donde viene la conexion.
            MySqlConnection coneccion = c.con();
            //Creamos una lista de tipo producto, ahi guardaremos objetos de los registros de la bd
            List<estudiante> items = new List<estudiante>();

            try
            {
                string query = "";
                coneccion.Open();
                DataSet ds = new DataSet();
                if (tipoUsuario!=1) {
                    query = "SELECT e.nombre,e.correoElectronico,e.matricula,p.nombre as nombreProyecto FROM estudiante e" +
                                  " JOIN asignacion a ON(e.matricula = a.matriculaEstudiante)" +
                                   " JOIN proyecto p ON(a.idProyecto= p.idProyecto) WHERE matricula = @matricula";

                }
                else
                {
                    query = "SELECT e.nombre,e.correoElectronico,e.matricula,p.nombre as nombreProyecto FROM estudiante e" +
                                 " JOIN asignacion a ON(e.matricula = a.matriculaEstudiante)" +
                                  " JOIN proyecto p ON(a.idProyecto= p.idProyecto)";
                }
               
                // MySqlCommand  mysc = new MySqlCommand(query, coneccion);
                MySqlDataAdapter mAdapter = new MySqlDataAdapter(query, coneccion);
                mAdapter.SelectCommand.Parameters.AddWithValue("@matricula",matricula);
                mAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        estudiante item = new estudiante();
                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.correoElectronico = ds.Tables[0].Rows[i]["correoElectronico"].ToString();
                        item.status = ds.Tables[0].Rows[i]["nombreProyecto"].ToString();
                        item.matricula= ds.Tables[0].Rows[i]["matricula"].ToString();


                        items.Add(item);
                    }
                }
                return items;

            }
            catch (Exception ex)
            {
                Debug.Write("Error...." + ex);
                return items;
            }
            finally
            {
                coneccion.Close();
            }

        }
    }
}
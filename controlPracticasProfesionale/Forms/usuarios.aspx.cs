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
    public partial class usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<usuario> GetUsuarios()
        {
            //Traemos la conexion
            coneccion c = new coneccion();
            //Creamos un objeto de tipo MysqlConextion y le asignamos el metodo donde viene la conexion.
            MySqlConnection coneccion = c.con();
            //Creamos una lista de tipo producto, ahi guardaremos objetos de los registros de la bd
            List<usuario> items = new List<usuario>();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = " SELECT e.matricula,e.contrasenia,e.nombre,e.correoElectronico,e.status,e.idTipoUsario, t.tipoUsuario "
                            +" FROM estudiante e JOIN tipoUsuario t ON(e.idTipoUsario = t.idTipoUsuario)";
                // MySqlCommand  mysc = new MySqlCommand(query, coneccion);
                MySqlDataAdapter mAdapter = new MySqlDataAdapter(query, coneccion);
                mAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        usuario item = new usuario();
                        item.matricula = ds.Tables[0].Rows[i]["matricula"].ToString();
                        item.contrasenia = ds.Tables[0].Rows[i]["contrasenia"].ToString();
                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.email = ds.Tables[0].Rows[i]["correoElectronico"].ToString();
                        item.status = ds.Tables[0].Rows[i]["status"].ToString(); 
                        item.tipoUsuario= ds.Tables[0].Rows[i]["tipoUsuario"].ToString();

                        items.Add(item);
                    }
                }
                return items;

            }
            catch (Exception ex)
            {
                Debug.Write("Error...." +ex);
                return items;
            }
            finally
            {
                coneccion.Close();
            }

        }


        [WebMethod]
        public static List<tipoUsuario> getTipoUsuarios()
        {
            //Traemos la conexion
            coneccion c = new coneccion();
            //Creamos un objeto de tipo MysqlConextion y le asignamos el metodo donde viene la conexion.
            MySqlConnection coneccion = c.con();
            //Creamos una lista de tipo producto, ahi guardaremos objetos de los registros de la bd
            List<tipoUsuario> items = new List<tipoUsuario>();

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = " SELECT idTipoUsuario, tipoUsuario FROM tipoUsuario";
                // MySqlCommand  mysc = new MySqlCommand(query, coneccion);
                MySqlDataAdapter mAdapter = new MySqlDataAdapter(query, coneccion);
                mAdapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tipoUsuario item = new tipoUsuario();
                        item.idTipoUsuario = ds.Tables[0].Rows[i]["idTipoUsuario"].ToString();
                        item.tipo = ds.Tables[0].Rows[i]["tipoUsuario"].ToString();
                       

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
        public static object GuardarUsuario(usuario usuario, string accion)
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();


            //comandatabase.CommandTimeout = 60;
            //  MySqlDataReader reader;

            try
            {
                coneccion.Open();
                string query = "";
                if (accion == "Nuevo")
                {
                    query = "insert into estudiante(matricula,contrasenia,nombre,correoElectronico,status,idTipoUsario) values(@matricula,@contrasenia,@nombre,@correoElectronico,@status,@idTipoUsario)";

                }
                else
                {
                    query = "UPDATE estudiante SET matricula= @matricula, contrasenia= @contrasenia, nombre = @nombre, correoElectronico= @correoElectronico, status=@status,  idTipoUsario=@idTipoUsario WHERE matricula = @id";


                }
                MySqlCommand comandatabase = new MySqlCommand(query, coneccion);

                // reader = comandatabase.ExecuteReader();
                comandatabase.CommandType = CommandType.Text;

                comandatabase.Parameters.Add("@matricula", MySqlDbType.VarChar).Value = usuario.matricula;
                comandatabase.Parameters.Add("@contrasenia", MySqlDbType.VarChar).Value = usuario.contrasenia;
                comandatabase.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = usuario.nombre;
                comandatabase.Parameters.Add("@correoElectronico", MySqlDbType.VarChar).Value = usuario.email;
                comandatabase.Parameters.Add("@status", MySqlDbType.VarChar).Value = usuario.status;
                comandatabase.Parameters.Add("@idTipoUsario", MySqlDbType.Int64).Value = usuario.tipoUsuario;

                comandatabase.Parameters.Add("@id", MySqlDbType.VarChar).Value = usuario.matricula;
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
        public static object eliminarRegistro(string id)
        {

            coneccion c = new coneccion();
            MySqlConnection con = c.con();

            try
            {


                con.Open();
                string sql = "";

                sql = " DELETE from estudiante " +
                        " WHERE matricula = @id ";



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

        [WebMethod]
        public static usuario getRegistro(string id)
        {

            coneccion c = new coneccion();
            MySqlConnection con = c.con();
            usuario item = new usuario();
            DataSet ds = new DataSet();

            try
            {


                con.Open();
                string sql = "";

                sql = " SELECT e.matricula,e.contrasenia,e.nombre,e.correoElectronico,e.status,e.idTipoUsario, t.tipoUsuario "
                            + " FROM estudiante e JOIN tipoUsuario t ON(e.idTipoUsario = t.idTipoUsuario)" +
                        " WHERE e.matricula = @id ";



                MySqlDataAdapter adp = new MySqlDataAdapter(sql, con);
                adp.SelectCommand.Parameters.AddWithValue("@id", id);

                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        item.matricula = ds.Tables[0].Rows[i]["matricula"].ToString();

                        item.contrasenia = ds.Tables[0].Rows[i]["contrasenia"].ToString();
                        item.nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                        item.email = ds.Tables[0].Rows[i]["correoElectronico"].ToString();// float.Parse(ds.Tables[0].Rows[i]["precio"].ToString());
                        item.status = ds.Tables[0].Rows[i]["status"].ToString(); //int.Parse(ds.Tables[0].Rows[i]["stock"].ToString());
                        item.tipoUsuario = ds.Tables[0].Rows[i]["tipoUsuario"].ToString(); //int.Parse(ds.Tables[0].Rows[i]["stock"].ToString());

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
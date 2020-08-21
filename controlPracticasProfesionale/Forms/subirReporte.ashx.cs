using controlPracticasProfesionale.clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace controlPracticasProfesionale.Forms
{
    /// <summary>
    /// Descripción breve de subirReporte
    /// </summary>
    public class subirReporte : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           System.Diagnostics.Debug.Print("Respuesta desde subirReporte ");
            context.Response.ContentType = "text/plain";
            DateTime fecha = DateTime.Today;
            try {
                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                   
                    string horas = context.Request.Form[0];
                    string fechaCarga = fecha.ToShortDateString();
                    string estado = context.Request.Form[1];
                    string fechainicio = context.Request.Form[2];
                    string fechafin = context.Request.Form[3];
                    string idExpediente = context.Request.Form[4];
                    string numeroReporte = context.Request.Form[5];


                    System.IO.Stream fs = file.InputStream;
                    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                    guardar(base64String, horas, fechaCarga, estado, fechainicio, fechafin,idExpediente,numeroReporte);
                   System.Diagnostics.Debug.Print("file subido: " + base64String );


                }

            } catch (Exception ex) {
                System.Diagnostics.Debug.Print("Error " + ex.Message);
                System.Diagnostics.Debug.Print(ex.StackTrace);
            }
           

        }

        public int guardar(string base64,string horas,string fechaCarga,string estado,string fechaInicio,string fechaFin, string idExpediente,string numeroReporte)
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            try
            {
                coneccion.Open();
                string query = "INSERT INTO reporte(horasReportadas,fechaCarga,estado,reporte,fechaInicio,fechaFin,idExpediente,numeroReporte) VALUES(@horasReportadas,@fechaCarga,@estado,@reporte,@fechaInicio,@fechaFin,@idExpediente,@numeroReporte)";
                MySqlCommand msc = new MySqlCommand(query,coneccion);

                msc.CommandType = CommandType.Text;
               
                msc.Parameters.Add("@horasReportadas", MySqlDbType.VarChar).Value = horas;
                msc.Parameters.Add("@fechaCarga", MySqlDbType.DateTime).Value = DateTime.Now;
                msc.Parameters.Add("@estado", MySqlDbType.VarChar).Value = estado;
                msc.Parameters.Add("@reporte", MySqlDbType.LongText).Value = base64;
                msc.Parameters.Add("@fechaInicio", MySqlDbType.VarChar).Value = fechaInicio;
                msc.Parameters.Add("@fechaFin", MySqlDbType.VarChar).Value = fechaFin;
                msc.Parameters.Add("@idExpediente", MySqlDbType.VarChar).Value = idExpediente;
                msc.Parameters.Add("@numeroReporte", MySqlDbType.VarChar).Value = numeroReporte;

                int r = msc.ExecuteNonQuery();

                return r;



            }
            catch (Exception ex) {
                System.Diagnostics.Debug.Print("Error " + ex.Message);
                System.Diagnostics.Debug.Print(ex.StackTrace);

                return -1;
            }
            finally
            {
                coneccion.Close();
            }


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
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

namespace controlPracticasProfesionale.Forms
{
    /// <summary>
    /// Descripción breve de DescargarArhivo
    /// </summary>
    public class DescargarArhivo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
                string idExpediente = context.Request.QueryString["idExpediente"].ToString();
                string numeroReporte = context.Request.QueryString["numeroReporte"].ToString();

            System.Diagnostics.Debug.Print("idEx " + idExpediente);
            try
            {
                

                string pdfB64 = getReportes(idExpediente,numeroReporte);
             
                System.Diagnostics.Debug.Print("file: " + pdfB64);
                byte[] sPDFDecoded = Convert.FromBase64String(pdfB64);



                string dirFullPath = context.Server.MapPath("~/Forms/MediaUploader");
                string nuevoArchivo = dirFullPath + "/Reporte_" + ".pdf";
                File.WriteAllBytes(nuevoArchivo, sPDFDecoded);



                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(nuevoArchivo));
                context.Response.WriteFile(nuevoArchivo);
                context.Response.End();


            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(ex.Message);
            }



        

    }

        public static string getReportes(string idExpediente,string numeroReporte)
        {
            coneccion c = new coneccion();
            MySqlConnection coneccion = c.con();
            List<reporte> items = new List<reporte>();
                var base64 = "";

            try
            {
                coneccion.Open();
                DataSet ds = new DataSet();
                string query = "SELECT Convert(reporte USING utf8) FROM reporte WHERE idExpediente = @idExpediente AND numeroReporte=@numeroReporte";

                MySqlDataAdapter mdsa = new MySqlDataAdapter(query,coneccion);
                mdsa.SelectCommand.Parameters.AddWithValue("@idExpediente", idExpediente);
                mdsa.SelectCommand.Parameters.AddWithValue("@numeroReporte", numeroReporte);

                mdsa.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    base64 = ds.Tables[0].Rows[0][0].ToString();
                }

            }
            catch (Exception ex)
            {
                Debug.Write("Error........." + ex.Message);
                Debug.Write("Error........." + ex.StackTrace);

                return "Error";
            }
            finally
            {
                coneccion.Close();
            }
            return base64;
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
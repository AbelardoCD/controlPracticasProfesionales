using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace controlPracticasProfesionale.clases
{
    public class proyectoJOINOrganizacion
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string capacidadEstudiantes { get; set; }
        public string numEstudiantesAsignados { get; set; }
        public string idProyecto { get; set; }
        public string status { get; set; }
        public string Organizacion { get; set; }
        public string EncargadoProyecto { get; set; }
    }
}
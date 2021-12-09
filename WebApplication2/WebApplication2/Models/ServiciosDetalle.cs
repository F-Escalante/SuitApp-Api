
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("servicios")]
    public class ServiciosDetalle : Servicios
    {
        

        public String account_image { get; set; }

        public List<Imagenes> imagenes { get; set; }

        public List<string> adjuntos { get; set; }

        public List<Comentarios> comments { get; set; }
        public string calle { get; set; }
        public string altura { get; set; }
        public string ciudad { get; set; }
        public List<DiasTurnos> diasTurnos { get; set; }
        public string horarioDesde { get; set; }
        public string horarioHasta { get; set; }

        public string turnoDuracion { get; set; }

        public string recursos { get; set; }


    }
}
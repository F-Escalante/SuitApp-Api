using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ServicioAgregar : ServiciosDetalle
    {
        public string calle { get; set; }
        public string altura { get; set; }
        public string ciudad { get; set; }
        public List<DiasTurnos> diasTurnos {get; set;}
        public string horarioDesde { get; set; }
        public string horarioHasta { get; set; }

        public string turnoDuracion { get; set; }

        public string recursos { get; set; }
    }
}
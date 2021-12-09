
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("comentarios")]
    public class Comentarios
    {

      /*  [Key, Column("id_comentario")]
        public int code { get; set; }*/
        [Column("comentario")]
        public String comment { get; set; }
        [Column("puntuacion")]
        public int stars { get; set; }
        [Column("fecha")]
        public DateTime date { get; set; }
        public String user_name { get; set; }
        
    }
}
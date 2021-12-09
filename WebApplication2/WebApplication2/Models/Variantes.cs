
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("variants")]
    public class Variantes 
    {

        [Key, Column("variant_id")]
        public int id { get; set; }
        public String color { get; set; }
        public String hexCode { get; set; }
        public String size { get; set; }

        public int stock { get; set; }
    }
}
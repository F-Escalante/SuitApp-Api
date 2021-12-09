using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{

    [Table("roles")]
   
    public class Roles
    {
        [Column("nombre")]
        public String rol_name { get; set; }

        public bool value { get; set; }
    }
}
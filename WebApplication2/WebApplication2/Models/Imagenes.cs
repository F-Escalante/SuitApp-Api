
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("images")]
    public class Imagenes 
    {

        [Key, Column("image_id")]
        public int order { get; set; }
        [Column("image")]
        public string image { get; set; }
        
    }
}
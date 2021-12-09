
using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;

namespace WebApplication2.Models
{
    
    public class CarritoAdd
    {
        
        public string? email { get; set; }
        public int articleId { get; set; }
                     
        public int colorId { get; set; }

        public int sizeId { get; set; }

        public int? quantity { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    
    public class VariantesAdd
    {

        public int colorId { get; set; }
        public int sizeId { get; set; }

        public int stock { get; set; }
    }
}
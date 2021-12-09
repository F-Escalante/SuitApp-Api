
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    
    public class ArticulosImagenes 
    {

        public int articleId { get; set; }

        public List<Imagenes> images { get; set; }

    }
}
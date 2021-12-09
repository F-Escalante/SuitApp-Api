
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    
    public class StoresGrupos
    {
        
        public int groupId { get; set; }
       
        public String title { get; set; }

        public List<StoresEnGrupos> stores { get; set; }
        
    }
}
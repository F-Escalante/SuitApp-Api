
using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication2.Models
{

    public class StoresAdd : Stores
    {
        public string email { get; set; }
        
        public Address addressAdd { get; set; }
        
        
       
    }
}
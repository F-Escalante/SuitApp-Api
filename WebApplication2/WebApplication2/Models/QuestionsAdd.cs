
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class QuestionsAdd
    {

                
        public String email { get; set; }
        public int articleId { get; set; }
        public String question { get; set; }
        

    }
}
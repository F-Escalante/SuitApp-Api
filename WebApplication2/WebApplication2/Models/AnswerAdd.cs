
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class AnswerAdd
    {

                
        public String email { get; set; }
        public int articleId { get; set; }
        public int questionId { get; set; }
        public String answer { get; set; }
        

    }
}
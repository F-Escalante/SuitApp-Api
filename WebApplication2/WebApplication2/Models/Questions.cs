
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication2.Models
{
    [Table("questions")]
    public class Questions
    {

        /*  [Key, Column("id_comentario")]
          public int code { get; set; }*/
        [Column("question_id")]
        public int questionId { get; set; }
        [Column("questions")]
        public String questions { get; set; }
        public String answer { get; set; }
        public String user_name { get; set; }
        [Column("date_question")]
        public DateTime dateQuestion { get; set; }

        [Column("date_answer")]
        public DateTime? dateAnswer { get; set; }

        public bool isOwner { get; set; }
        public static List<Questions> serializarQuestions(DataTable dataTable, bool owner)
        {
            List<Questions> questionsList = new List<Questions>();
                        
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Questions questions = new Questions();
                questions.questionId = Convert.ToInt16(dataTable.Rows[i]["question_id"]);
                questions.user_name = dataTable.Rows[i]["name"].ToString().Trim();
                questions.questions = dataTable.Rows[i]["question"].ToString().Trim();
                questions.answer = dataTable.Rows[i]["answer"].ToString().Trim();
                questions.dateQuestion = Convert.ToDateTime(dataTable.Rows[i]["date_question"]);
                if(Convert.IsDBNull(dataTable.Rows[i]["date_answer"])) 
                {
                    questions.dateAnswer = null;
                }
                else { questions.dateAnswer = Convert.ToDateTime(dataTable.Rows[i]["date_answer"]); }
                
                questions.isOwner = owner;

                questionsList.Add(questions);
            }
            return questionsList;
        }
          
          


        }
    }


/*if(userId == Convert.ToInt16(dataTable.Rows[0]["question_id"])
            {
                owner = true;   
             }
            for (int i = 0; i<dataTable.Rows.Count; i++)
            {
                Questions questions = new Questions();
                questions.questionId = Convert.ToInt16(dataTable.Rows[i]["question_id"]);
                questions.user_name = dataTable.Rows[i]["name"].ToString().Trim();
                questions.questions = dataTable.Rows[i]["question"].ToString().Trim();
                questions.questions = dataTable.Rows[i]["answer"].ToString().Trim();
                questions.dateQuestion = Convert.ToDateTime(dataTable.Rows[i]["date_question"]);
                questions.dateAnswer = Convert.ToDateTime(dataTable.Rows[i]["date_answer"]);
                                
                questionsList.Add(questions);
            }*/



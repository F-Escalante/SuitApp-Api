using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class QuestionsController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public List<Questions> Get(int idArticle, string email)
        {
            List<Questions> questionsList;
            questionsList = Models.Database.getQuestions(idArticle, email);
            return questionsList;

        }

        // POST: api/Login
        public HttpResponseMessage Post(QuestionsAdd questionsAdd)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se pudo agregar la pregunta");
                

            bool result = Models.Database.addQuestion(questionsAdd);
            if(result == true) 
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Pregunta registrada correctamente");
            }
            
            return response;
        }

        // PUT: api/Login/5
        public HttpResponseMessage Put(AnswerAdd answerAdd)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se pudo registrar la respuesta");
            bool deslogueo;
            deslogueo = Models.Database.addAnswer(answerAdd);
            if (deslogueo == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Respuesta registrada correctamente");
            }


            return response;
        }

        // DELETE: api/Login/5
        public HttpResponseMessage Delete(string email, string articleId, string questionId)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se pudo eliminar la respuesta");
            bool deslogueo;
            deslogueo = Models.Database.deleteAnswer(email, articleId, questionId);
            if(deslogueo == true) 
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Respuesta eliminada correctamente");
            }


            return response;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LoginController : ApiController
    {
        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        public HttpResponseMessage Post(LoginEntrada loginEntrada)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "El login no se ha podido realizar");
            /*LoginSalida login = new LoginSalida();
            login = Models.Database.getLogin(loginEntrada.email, loginEntrada.nombre, loginEntrada.telefono, loginEntrada.device_id);
            if (login.roles != null)
            {
                response = Request.CreateResponse<LoginSalida>(HttpStatusCode.OK, login);
            }*/
      

            bool result = Models.Database.getLogin(loginEntrada.email, loginEntrada.nombre, loginEntrada.device_id);
            if(result == true) 
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Login Correcto");
            }
            
            return response;
        }

        // PUT: api/Login/5
        public HttpResponseMessage Put(string email, string device_id)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se pudo desloguear");
            bool deslogueo;
            deslogueo = Models.Database.desloguear(email, device_id);
            if (deslogueo == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Usuario deslogueado");
            }


            return response;
        }

        // DELETE: api/Login/5
        public HttpResponseMessage Delete(string email, string device_id, string project_id)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se pudo desloguear");
            bool deslogueo;
            deslogueo = Models.Database.desloguear(email, device_id);
            if(deslogueo == true) 
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Usuario deslogueado");
            }


            return response;
        }
    }
}

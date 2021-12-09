using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CategoriasController : ApiController
    {

        
        // GET api/categorias
        public HttpResponseMessage Get()
        {
            List<Categorias> categorias;
            categorias = Models.Database.getCategorias();
            return Request.CreateResponse<List<Categorias>>(HttpStatusCode.OK, categorias); 
        }

        // GET api/categorias/5
        public HttpResponseMessage Get(int id)
        {
            List<Categorias> categorias;
            categorias = Models.Database.getCategorias(id);
            return Request.CreateResponse<List<Categorias>>(HttpStatusCode.OK, categorias);

        }

        /*public HttpResponseMessage Get(int quantity, bool top)
        {
            List<Categorias> categorias;
            categorias = Models.Database.getCategorias(quantity, top);
            return Request.CreateResponse<List<Categorias>>(HttpStatusCode.OK, categorias);
        }*/

        // POST api/categorias
        public void Post([FromBody]string value)
        {
        }

        // PUT api/categorias/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/categorias/5
        public void Delete(int id)
        {
        }
    }
}

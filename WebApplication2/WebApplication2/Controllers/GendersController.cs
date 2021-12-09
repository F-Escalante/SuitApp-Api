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
    public class GendersController : ApiController
    {
        // GET: Genders
        // GET api/categorias
        public HttpResponseMessage Get()
        {
            List<Genders> generos;
            generos = Models.Database.getGeneros();
            return Request.CreateResponse<List<Genders>>(HttpStatusCode.OK, generos);
        }

       

        
    }
}

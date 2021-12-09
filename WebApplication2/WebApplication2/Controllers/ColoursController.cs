using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ColoursController : ApiController
    {
        // GET: Colours
        
        public HttpResponseMessage Get()
        {
            List<Colores> colores;
            colores = Models.Database.getColores();
            return Request.CreateResponse<List<Colores>>(HttpStatusCode.OK, colores);
        }
    }
}
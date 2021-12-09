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
    public class SizesController : ApiController
    {
        // GET: Sizes
       
        public HttpResponseMessage Get()
        {
            List<Talles> talle;
            talle = Models.Database.getTalles();
            return Request.CreateResponse<List<Talles>>(HttpStatusCode.OK, talle);
        }
    }
}
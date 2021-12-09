using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ImagesController : ApiController
    {
        // GET api/serviciosdetalle
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/serviciosdetalle/5
        public ArticulosImagenes Get(int id)
        {
            ArticulosImagenes articulosImg;
            articulosImg = Models.Database.getArticuloImagenes(id);
            return articulosImg;

        }

        // POST api/serviciosdetalle
        public void Post([FromBody]string value)
        {
        }

        // PUT api/serviciosdetalle/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/serviciosdetalle/5
        public void Delete(int id)
        {
        }
    }
}

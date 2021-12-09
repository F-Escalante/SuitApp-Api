using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CartController : ApiController
    {
        
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

      
        public HttpResponseMessage Get(string hashCarrito)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido obtener carrito");
            List<Carrito> carritoList;

            string emailHash = hashCarrito.ToUpper();

            int id_usuario = Database.consultaIdUsuarioPorHash(emailHash);
            if (id_usuario == 0)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "El usuario no está registrado");
            }
            carritoList = Models.Database.getCarrito(id_usuario);
            if (carritoList != null)
            {
                response = Request.CreateResponse<List<Carrito>>(HttpStatusCode.OK, carritoList);
            }
            return response;
        }

        
        public HttpResponseMessage Post(CarritoAdd carritoAdd)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido agregar el articulo al carrito");
            
            bool result = Models.Database.addArticlesCarrito(carritoAdd);
            if (result == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Articulo agregado al carrito");
            }

            return response;
        }
        // PUT api/serviciosdetalle/5
        public HttpResponseMessage Put(CarritoAdd carritoAdd)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido quitar el articulo del carrito");
            
            bool result = Models.Database.deleteArticlesCarrito(carritoAdd);
            if (result == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "El artículo se quitó del carrito");
            }

            return response;
        }

        // DELETE api/serviciosdetalle/5
        public void Delete(int id)
        {
        }
    }
}

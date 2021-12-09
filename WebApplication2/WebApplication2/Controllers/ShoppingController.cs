using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ShoppingController : ApiController
    {
        
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

      
        public HttpResponseMessage Get(string hashShopping)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido obtener carrito");
            List<Compras> carritoList;

            string emailHash = hashShopping.ToUpper();

            int id_usuario = Database.consultaIdUsuarioPorHash(emailHash);
            if (id_usuario == 0)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "El usuario no está registrado");
            }
            carritoList = Models.Database.getCompras(id_usuario);
            if (carritoList != null)
            {
                response = Request.CreateResponse<List<Compras>>(HttpStatusCode.OK, carritoList);
            }
            return response;
        }

        
        public HttpResponseMessage Post(ComprasAdd comprasAdd)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido realizar la compra");
            
            bool result = Models.Database.addShopping(comprasAdd);
            if (result == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Compra realizada con exito");
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

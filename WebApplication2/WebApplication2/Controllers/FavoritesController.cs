using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class FavoritesController : ApiController
    {
        
        public HttpResponseMessage Get(string hashFavoritos)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido obtener favoritos");
            List<ArticulosEnGrupos> articulosGrpList;

            string emailHash = hashFavoritos.ToUpper();

            int id_usuario = Database.consultaIdUsuarioPorHash(emailHash);
            if (id_usuario == 0)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "El usuario no está registrado");
            }
            articulosGrpList = Models.Database.getArticulosFavoritos(id_usuario);
            if (articulosGrpList != null) {
                response = Request.CreateResponse<List<ArticulosEnGrupos>>(HttpStatusCode.OK, articulosGrpList);
            }
            return response;
        }
        

        public HttpResponseMessage Post(FavoritosAdd favoritosAdd)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido agregar el articulo a favoritos");
            /*LoginSalida login = new LoginSalida();
            login = Models.Database.getLogin(loginEntrada.email, loginEntrada.nombre, loginEntrada.telefono, loginEntrada.device_id);
            if (login.roles != null)
            {
                response = Request.CreateResponse<LoginSalida>(HttpStatusCode.OK, login);
            }*/


            bool result = Models.Database.addArticlesFavoritos(favoritosAdd);
            if (result == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Articulo agregado a favoritos");
            }

            return response;
        }
        // PUT api/serviciosdetalle/5
        public HttpResponseMessage Put(FavoritosAdd favoritosAdd)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido quitar el articulo de favoritos");
            /*LoginSalida login = new LoginSalida();
            login = Models.Database.getLogin(loginEntrada.email, loginEntrada.nombre, loginEntrada.telefono, loginEntrada.device_id);
            if (login.roles != null)
            {
                response = Request.CreateResponse<LoginSalida>(HttpStatusCode.OK, login);
            }*/


            bool result = Models.Database.deleteArticlesFavoritos(favoritosAdd);
            if (result == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "El artículo se quitó de favoritos");
            }

            return response;
        }

        // DELETE api/serviciosdetalle/5
        public void Delete(int id)
        {
        }
    }
}

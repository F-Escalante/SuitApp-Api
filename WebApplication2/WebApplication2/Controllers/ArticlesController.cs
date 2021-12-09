using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ArticlesController : ApiController
    {
        // GET api/serviciosdetalle
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        Database database = new Database();
        // GET api/serviciosdetalle/5
        public Articulos Get(int id)
        {
            
            Articulos articulos;
            articulos = database.getArticulo(id);
            return  articulos;
            
        }

        public List<ArticulosGrupos> Get(int groupQty, int artQty, int? idArticle)
        {
            List<ArticulosGrupos> articulosGrpList;
            articulosGrpList = Models.Database.getArticuloGrupos(groupQty, artQty, idArticle);
            return articulosGrpList;

        }

        public List<ArticulosEnGrupos> Get(int artQty, int pageId, int? genderId, float? minPrice, float? maxPrice, int? size, int? colourId, int? categoryId, int? storeId, int order, string? search)
        {
            List<ArticulosEnGrupos> articulosGrpList;
            articulosGrpList = Models.Database.getArticuloGruposConFiltros(artQty, pageId, genderId, minPrice, maxPrice, size, colourId, categoryId, storeId, order , search);
            return articulosGrpList;

        }
                
        // POST api/serviciosdetalle
        public HttpResponseMessage Post(ArticulosAdd articlesAdd)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido agregar el articulo");
            /*LoginSalida login = new LoginSalida();
            login = Models.Database.getLogin(loginEntrada.email, loginEntrada.nombre, loginEntrada.telefono, loginEntrada.device_id);
            if (login.roles != null)
            {
                response = Request.CreateResponse<LoginSalida>(HttpStatusCode.OK, login);
            }*/


            bool result = Models.Database.addArticles(articlesAdd);
            if (result == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Articulo agregado");
            }

            return response;
        }

       
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/serviciosdetalle/5
        public HttpResponseMessage Delete(string email, int articleId)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido eliminar el articulo");
            string emailHash = email.ToUpper();

            int id_usuario = Database.consultaIdUsuarioPorHash(emailHash);
            if (id_usuario == 0)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "El usuario no está registrado");
            }


            bool result = Models.Database.deleteArticles(id_usuario, articleId);
            if (result == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Articulo eliminado");
            }

            return response;
        }
    }
}

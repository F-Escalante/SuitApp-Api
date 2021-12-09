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
    public class StoresController : ApiController
    {
       
        // GET api/serviciosdetalle
       
        public List<StoresEnGrupos> Get()
        {
            List<StoresEnGrupos> stores;
            stores = Models.Database.getStore();
            return stores;

        }
        public Stores Get(int id)
        {
            Stores stores;
            stores = Models.Database.getStore(id);
            return stores;

        }

        public List<StoresEnGrupos> Get(string hash)
        {
            List<StoresEnGrupos> stores;
            stores = Models.Database.getStore(hash);
            return stores;

        }

        public List<StoresGrupos> Get(int groupQty, int storeQty)
        {
            List<StoresGrupos> storesGrpList;
            storesGrpList = Models.Database.getStoresGrupos(groupQty, storeQty);
            return storesGrpList;

        }

        public List<StoresEnGrupos> GetPremium(int storeQty)
        {
            List<StoresEnGrupos> storesGrpList;
            storesGrpList = Models.Database.getStoresPremium(storeQty);
            return storesGrpList;

        }

        public List<StoresEnGrupos> Get(int storesQty, int pageId, int? genderId, float? minPrice, float? maxPrice, int? size, int? colourId, int? categoryId, string? search)
        {
            List<StoresEnGrupos> storesGrpList;
            storesGrpList = Models.Database.getStoresGruposConFiltros(storesQty, pageId, genderId, minPrice, maxPrice, size, colourId, categoryId, search);
            return storesGrpList;

        }
        // POST api/serviciosdetalle
        public HttpResponseMessage Post(StoresAdd storeadd)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido agregar tienda");
            /*LoginSalida login = new LoginSalida();
            login = Models.Database.getLogin(loginEntrada.email, loginEntrada.nombre, loginEntrada.telefono, loginEntrada.device_id);
            if (login.roles != null)
            {
                response = Request.CreateResponse<LoginSalida>(HttpStatusCode.OK, login);
            }*/


            bool result = Models.Database.addStore(storeadd);
            if (result == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Tienda agregada");
            }

            return response;
        }

        // PUT api/serviciosdetalle/5
        public void Put(int id, [FromBody] string value)
        {
        }

        public HttpResponseMessage Delete(string email, int storeId)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido eliminar la tienda");
            string emailHash = email.ToUpper();

            int id_usuario = Database.consultaIdUsuarioPorHash(emailHash);
            if (id_usuario == 0)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "El usuario no está registrado");
            }


            bool result = Models.Database.deleteStores(id_usuario, storeId);
            if (result == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Tienda eliminada");
            }

            return response;
        }
    }
}
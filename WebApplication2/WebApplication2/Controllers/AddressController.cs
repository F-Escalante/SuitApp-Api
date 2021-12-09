using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AddressController : ApiController
    {
        
        public HttpResponseMessage Get(string hashAddress)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido obtener las direcciones");
            List<Address> addressList;

            string emailHash = hashAddress.ToUpper();

            int id_usuario = Database.consultaIdUsuarioPorHash(emailHash);
            if (id_usuario == 0)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "El usuario no está registrado");
            }
            addressList = Models.Database.getAddress(id_usuario);
            if (addressList != null) {
                response = Request.CreateResponse<List<Address>>(HttpStatusCode.OK, addressList);
            }
            return response;
        }
        

        public HttpResponseMessage Post(Address address)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido agregar la direccion");
            /*LoginSalida login = new LoginSalida();
            login = Models.Database.getLogin(loginEntrada.email, loginEntrada.nombre, loginEntrada.telefono, loginEntrada.device_id);
            if (login.roles != null)
            {
                response = Request.CreateResponse<LoginSalida>(HttpStatusCode.OK, login);
            }*/


            bool result = Models.Database.addAddress(address);
            if (result == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Dirección agregada");
            }

            return response;
        }
        // PUT api/serviciosdetalle/5
        public void Put()
        {
            
        }

        // DELETE api/serviciosdetalle/5
        public HttpResponseMessage Delete(string email, int addressId)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se ha podido eliminar la dirección");
            /*LoginSalida login = new LoginSalida();
            login = Models.Database.getLogin(loginEntrada.email, loginEntrada.nombre, loginEntrada.telefono, loginEntrada.device_id);
            if (login.roles != null)
            {
                response = Request.CreateResponse<LoginSalida>(HttpStatusCode.OK, login);
            }*/


            bool result = Models.Database.deleteAddress(email, addressId);
            if (result == true)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.OK, "La dirección ha sido eliminada");
            }

            return response;
        }
    }
}

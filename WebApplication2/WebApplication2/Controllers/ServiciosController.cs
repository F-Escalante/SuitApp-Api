using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ServiciosController : ApiController
    {
        // GET api/servicios
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/servicios/5
        public List<Servicios> Get(int id)
        {
            List<Servicios> servicios;
            servicios = Models.Database.getServicios(id);

            return servicios;
        }

        public HttpResponseMessage  Get(string email)
        {
            List<Servicios> servicios;
            servicios = Models.Database.getServicios(email);
            HttpResponseMessage response = Request.CreateResponse<List<Servicios>>(HttpStatusCode.OK, servicios);
            return response;
        }

        // POST api/servicios
        public HttpResponseMessage Post([FromBody]ServiciosDetalle servicioAgregar)
        {
            HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "No se pudo agregar el servicio");

            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        //SE ABRE LA CONEXION 
                        con.Open();
                        SqlCommand command = new SqlCommand("Select * from Buscador_Servicios.dbo.servicios where nombre = @nombre", con);
                        command.Parameters.AddWithValue("@nombre", servicioAgregar.nombre);
                        SqlDataReader dataReader = command.ExecuteReader();
                        if (!dataReader.Read())
                        {
                            bool respuesta = Models.Database.agregarServicio(servicioAgregar);
                            if (respuesta == true)
                            {

                                response = Request.CreateErrorResponse(HttpStatusCode.OK, "Servicio agregado correctamente");
                            }
                        }else 
                        {
                            response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "Ya existe un servicio con ese nombre");
                        }
                    }
                    catch (SqlException x)
                    {
                        response = Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, x);
                    }
                    finally
                    {

                        con.Close();

                    }
                }
            }

            
            return response;
        }

        // PUT api/servicios/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/servicios/5
        public void Delete(int id)
        {
        }
    }
}

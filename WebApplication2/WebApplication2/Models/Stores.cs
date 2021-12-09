
using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication2.Models
{

    public class Stores
    {

        public int storeId { get; set; }
        public String storeName { get; set; }
                
        public String coord { get; set; }
        public String address { get; set; }
        public String description { get; set; }

        public string? storeLogo { get; set; }
        public string? storeCoverPhoto { get; set; }
        public bool premium { get; set; }
        public bool physical_store { get; set; }

#pragma warning disable CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".
        public List<ShippingPrice> shippingPrice { get; set; }
#pragma warning restore CS8632 // La anotación para tipos de referencia que aceptan valores NULL solo debe usarse en el código dentro de un contexto de anotaciones "#nullable".

        public bool mailShipping { get; set; }
        public static string getImagen(int id_store)
        {
            DataTable artImagen = new DataTable();
            string imagen = null;
            // Image rImage = null;
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        //SE ABRE LA CONEXION 
                        con.Open();

                        //SE UTILIZA PARA LLENAR UN DATASET CON LOS ELEMENTOS NECESARIOS  
                        //COMO UNA CONEXION DE BASE DE DATOS 

                        //lista de variantes
                        SqlCommand command30 = new SqlCommand("sp_get_imagen @p_id_componente = @id, @p_id_tipo = @p_id_tipo", con);
                        command30.Parameters.Add("@id", SqlDbType.Int);
                        command30.Parameters.Add("@p_id_tipo", SqlDbType.Int);
                        command30.Parameters["@id"].Value = id_store;
                        command30.Parameters["@p_id_tipo"].Value = 1;
                        artImagen.Load(command30.ExecuteReader());
                        int k = 0;

                        foreach (DataRow fila2 in artImagen.Rows)
                        {
                            //public static Image ConvertToImage(System.Data.Linq.Binary iBinary)
                            // {
                            // var arrayBinary = iBinary.ToArray();

                            imagen = (artImagen.Rows[0]["image"]).ToString();



                            //}

                        }

                        //return imagen;

                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA                                     

            return imagen;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.IO;

namespace WebApplication2.Models
{
    
    public class StoresEnGrupos
    {
        
        public int storeId { get; set; }
       
        
        public String storeName { get; set; }
        
        
        public string storeSlogan { get; set; }
        // public List<Imagenes> images { get; set; }
        public string storeLogo { get; set; }

        public string storeCoverPhoto { get; set; }


        public static List<StoresEnGrupos> serializarStores(DataTable dataTable, int typeGetStore, int contador)
        {

            //typeGetStore {1: Logo y portada; 2: Logo; 3: portada}

            List<StoresEnGrupos> storesList = new List<StoresEnGrupos>();
            
            //List<Object> categoriasList = new List<System.Object>();
            
            int i;
            for (i = contador; i < dataTable.Rows.Count; i++)
            {
                StoresEnGrupos stores = new StoresEnGrupos();
                stores.storeId = Convert.ToInt16(dataTable.Rows[i]["store_id"]);
                stores.storeName = dataTable.Rows[i]["name"].ToString().Trim();
                if (dataTable.Columns.Contains("slogan"))
                {
                    stores.storeSlogan = dataTable.Rows[i]["slogan"].ToString().Trim();
                }
                switch (typeGetStore) 
                {
                    case 1:
                        if (dataTable.Columns.Contains("logo"))
                        {
                            stores.storeLogo = dataTable.Rows[i]["logo"].ToString();
                        }
                        stores.storeCoverPhoto = getImagen(stores.storeId);
                        break;
                    case 2:
                        if (dataTable.Columns.Contains("logo"))
                        {
                            stores.storeLogo = dataTable.Rows[i]["logo"].ToString();
                        }
                        break;
                    case 3:
                        stores.storeCoverPhoto = getImagen(stores.storeId);
                        break;
                
                }
                
                storesList.Add(stores);
            }

            return storesList;
        }

       
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
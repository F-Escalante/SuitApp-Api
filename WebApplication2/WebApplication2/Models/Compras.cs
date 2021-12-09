
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
    
    public class Compras
    {

        public int shoppingId { get; set; }
        public int articleId { get; set; }
       
        public int storeId { get; set; }
        public String articleName { get; set; }
        
        
        public float articlePrice { get; set; }
        // public List<Imagenes> images { get; set; }
        public string articleImage { get; set; }

        public int quantity { get; set; }

        public int colorId { get; set; }

        public int sizeId { get; set; }

        public int statusId { get; set; }
        public DateTime buyDate { get; set; }

        public DateTime arriveDate { get; set; }



        public static List<Compras> serializarArticulos(DataTable dataTable)
        {

            List<Compras> comprasList = new List<Compras>();
            
            //List<Object> categoriasList = new List<System.Object>();
            int i = 0;
            foreach(DataRow fila in dataTable.Rows)
            {
                Compras compras = new Compras();
                compras.shoppingId = Convert.ToInt16(dataTable.Rows[i]["shopping_id"]);
                compras.articleId = Convert.ToInt16(dataTable.Rows[i]["article_id"]);
                compras.storeId = Convert.ToInt16(dataTable.Rows[i]["store_id"]);
                compras.articleName = dataTable.Rows[i]["name"].ToString().Trim();
                compras.articlePrice = Convert.ToSingle(dataTable.Rows[i]["price"]);
                compras.quantity = Convert.ToInt16(dataTable.Rows[i]["quantity"]);
                compras.colorId = Convert.ToInt16(dataTable.Rows[i]["colour_id"]);
                compras.sizeId = Convert.ToInt16(dataTable.Rows[i]["size_id"]);
                compras.statusId = Convert.ToInt16(dataTable.Rows[i]["status_id"]);
                compras.buyDate = Convert.ToDateTime(dataTable.Rows[i]["buy_date"]);
                compras.arriveDate = Convert.ToDateTime(dataTable.Rows[i]["arrive_date"]);
                compras.articleImage = getImagen(compras.articleId);

                comprasList.Add(compras);
                i++;
            }

            return comprasList;
        }

       
        public static string getImagen(int id_articulo)
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

                        
                        SqlCommand command30 = new SqlCommand("sp_get_imagen @p_id_componente = @id, @p_id_tipo = @p_id_tipo", con);
                        command30.Parameters.Add("@id", SqlDbType.Int);
                        command30.Parameters.Add("@p_id_tipo", SqlDbType.Int);
                        command30.Parameters["@id"].Value = id_articulo;
                        command30.Parameters["@p_id_tipo"].Value = 2;
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
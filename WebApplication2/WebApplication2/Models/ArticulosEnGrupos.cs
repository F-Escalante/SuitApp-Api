
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
    
    public class ArticulosEnGrupos
    {
        
        public int articleId { get; set; }
       
        
        public String articleName { get; set; }
        
        
        public float articlePrice { get; set; }
        // public List<Imagenes> images { get; set; }
        public string articleImage { get; set; }

        public int articleCountColours { get; set; }


        public static List<ArticulosEnGrupos> serializarArticulos(DataTable dataTable, int contador)
        {

            List<ArticulosEnGrupos> articulosList = new List<ArticulosEnGrupos>();
            
            //List<Object> categoriasList = new List<System.Object>();
            int i;
            for (i = contador; i < dataTable.Rows.Count; i++)
            {
                ArticulosEnGrupos articulos = new ArticulosEnGrupos();
                articulos.articleId = Convert.ToInt16(dataTable.Rows[i]["article_id"]);
                articulos.articleName = dataTable.Rows[i]["name"].ToString().Trim();
                articulos.articlePrice = Convert.ToSingle(dataTable.Rows[i]["price"]);
                articulos.articleImage = getImagen(articulos.articleId);
                if (dataTable.Columns.Contains("count_color")) 
                {
                    articulos.articleCountColours = Convert.ToInt16(dataTable.Rows[i]["count_color"]);
                }
                
                articulosList.Add(articulos);
            }

            return articulosList;
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
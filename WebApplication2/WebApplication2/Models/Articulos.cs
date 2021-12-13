
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

    public class Articulos
    {

        public int articleId { get; set; }

        public int storeId { get; set; }
        public String articleName { get; set; }


        public float articlePrice { get; set; }
        // public List<Imagenes> images { get; set; }
        public string articleImage { get; set; }
        public List<Variantes> variants { get; set; }
        public float ranking { get; set; }
        public String articleDescription { get; set; }
        public int commentsQty { get; set; }

     
        public static Articulos serializarArticulos(DataTable dataTable)
        {

            //List<Articulos> articulosList = new List<Articulos>();
            Articulos articulos = new Articulos();
            //List<Object> categoriasList = new List<System.Object>();
            int i = 0;
            foreach (DataRow fila in dataTable.Rows)
            {
                
                articulos.articleId = Convert.ToInt16(dataTable.Rows[i]["article_id"]);
                articulos.storeId = Convert.ToInt16(dataTable.Rows[i]["store_id"]);
                articulos.articleName = dataTable.Rows[i]["name"].ToString();
                articulos.articlePrice = Convert.ToSingle(dataTable.Rows[i]["price"]);
                articulos.articleImage = getImagen(articulos.articleId);
                //lista de variantes
                List<Variantes> variantesList = new List<Variantes>();
                variantesList = getVariantes(articulos.articleId);
                
                articulos.variants = variantesList;
                articulos.ranking = Convert.ToSingle(dataTable.Rows[i]["ranking"]);
                articulos.articleDescription = dataTable.Rows[i]["description"].ToString();
                articulos.commentsQty = Convert.ToInt16(dataTable.Rows[i]["commentsQty"]);
                i++;
                //articulosList.Add(articulos);
            }

            return articulos;
        }

        public static List<Variantes> getVariantes(int id_articulo)
        {
            DataTable variantes = new DataTable();
            List<Variantes> variantesList = new List<Variantes>();
            
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
                        SqlCommand command30 = new SqlCommand("sp_get_variantes @p_id_articulo = @id", con);
                        command30.Parameters.Add("@id", SqlDbType.Int);
                        command30.Parameters["@id"].Value = id_articulo;
                        variantes.Load(command30.ExecuteReader());
                        int k = 0;

                        foreach (DataRow fila2 in variantes.Rows)
                        {
                            Variantes variant = new Variantes();
                            variant.id = Convert.ToInt16(variantes.Rows[k]["variants_id"]);
                            variant.color = variantes.Rows[k]["colour"].ToString();
                            variant.hexCode = variantes.Rows[k]["hex_code"].ToString();
                            variant.size = variantes.Rows[k]["size"].ToString();
                            variant.stock = Convert.ToInt16(variantes.Rows[k]["stock"]);
                            variantesList.Add(variant);
                            k++;
                        }

                        return variantesList;

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

            return variantesList;
        }

        public static string getImagen(int id_articulo)
        {
            DataTable artImagen = new DataTable();
            string imagen = null;

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
                        SqlCommand command30 = new SqlCommand("sp_get_imagen @p_id_componente = @id, @p_id_tipo = @type_id", con);
                        command30.Parameters.Add("@id", SqlDbType.Int);
                        command30.Parameters.Add("@type_id", SqlDbType.Int);
                        command30.Parameters["@id"].Value = id_articulo;
                        command30.Parameters["@type_id"].Value = 2;
                        artImagen.Load(command30.ExecuteReader());
                        int k = 0;

                         


                        foreach (DataRow fila2 in artImagen.Rows)
                        {
                            imagen = artImagen.Rows[0]["image"].ToString(); ;
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

using WebApplication2.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Collections;



namespace WebApplication2.Models
{
    public class Database
    {
        // Declaración de instancias.
        private string connectionString;
        /// <summary>
        /// Método constructor sin parámetros.
        /// </summary>
        public Database()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString; // Se obtiene la cadena de conexión a la base de datos.
        }//Fin del método constructor.

        #region ARTICULOS
        public Articulos getArticulo(int id_articulo)
        {
            DataTable dataTable = new DataTable();
            DataTable variantes = new DataTable();
            Articulos articulos = new Articulos();
            // ServiciosDetalle serviciosDetalleList = new ServiciosDetalle();
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
                        SqlCommand command = new SqlCommand("sp_get_articulo @p_id_articulo = @id", con);
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = id_articulo;
                        dataTable.Load(command.ExecuteReader());
                        articulos = Articulos.serializarArticulos(dataTable);


                        return articulos;

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

            return articulos;
        }

        public static ArticulosImagenes getArticuloImagenes(int id_articulo)
        {
            DataTable dataTable = new DataTable();
            DataTable imagenes = new DataTable();
            ArticulosImagenes articulosImg = new ArticulosImagenes();
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
                        SqlCommand command = new SqlCommand("sp_get_articulo @p_id_articulo = @id", con);
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = id_articulo;
                        dataTable.Load(command.ExecuteReader());



                        articulosImg.articleId = Convert.ToInt16(dataTable.Rows[0]["article_id"]);
                        
                        //lista de imagenes
                        SqlCommand command30 = new SqlCommand("sp_get_imagenes @p_id_articulo = @id", con);
                        command30.Parameters.Add("@id", SqlDbType.Int);
                        command30.Parameters["@id"].Value = id_articulo;
                        imagenes.Load(command30.ExecuteReader());
                        int k = 0;
                        List<Imagenes> imagenesList = new List<Imagenes>();

                        foreach (DataRow fila2 in imagenes.Rows)
                        {
                            Imagenes img = new Imagenes();
                            img.order = Convert.ToInt16(imagenes.Rows[k]["image_id"]);
                            img.image = (imagenes.Rows[k]["image"]).ToString();

                            imagenesList.Add(img);
                            k++;
                        }

                        articulosImg.images = imagenesList;
                        return articulosImg;

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

            return articulosImg;
        }

        public static List<ArticulosGrupos> getArticuloGrupos(int groupQty, int artQty, int? idArticle)
        {
            DataTable articulos = new DataTable();
            ArticulosGrupos articulosGrp = new ArticulosGrupos();
            List <ArticulosGrupos> artGrupos = new List<ArticulosGrupos>();
            List<int> idArtGrupos = new List<int>() {1,2,3,4,5,6};
            List<int> idArtGruposIdArt = new List<int>() { 1, 2, 3};
            int idGrupoArticulo = 0;
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
                        Random random = new Random();
                        for (int i = 0; i < groupQty; i++)
                        {


                            if (idArticle == null)
                            {
                                int count = idArtGrupos.Count;

                                int index = random.Next(0, count);
                                idGrupoArticulo = idArtGrupos[index];
                                idArtGrupos.Remove(idGrupoArticulo);

                                switch (idGrupoArticulo)
                                {
                                    case 1:
                                        articulosGrp.groupId = 1;
                                        articulosGrp.title = "Artículos populares";

                                        SqlCommand command = new SqlCommand("sp_get_articulos_populares @cant_articulos = @artQty", con);
                                        command.Parameters.Add("@artQty", SqlDbType.Int);
                                        command.Parameters["@artQty"].Value = artQty;
                                        articulos.Load(command.ExecuteReader());
                                        articulosGrp.articulos = ArticulosEnGrupos.serializarArticulos(articulos,0);
                                        artGrupos.Add(articulosGrp);
                                        break;
                                    case 2:

                                        ArticulosGrupos articulosGrp2 = new ArticulosGrupos();
                                        DataTable articulos2 = new DataTable();
                                        articulosGrp2.groupId = 2;
                                        articulosGrp2.title = "Artículos destacados";

                                        SqlCommand command2 = new SqlCommand("sp_get_articulos_destacados @cant_articulos = @artQty", con);
                                        command2.Parameters.Add("@artQty", SqlDbType.Int);
                                        command2.Parameters["@artQty"].Value = artQty;
                                        articulos2.Load(command2.ExecuteReader());
                                        articulosGrp2.articulos = ArticulosEnGrupos.serializarArticulos(articulos2,0);

                                        artGrupos.Add(articulosGrp2);
                                        break;
                                    case 3:

                                        ArticulosGrupos articulosGrp3 = new ArticulosGrupos();
                                        DataTable articulos3 = new DataTable();
                                        articulosGrp3.groupId = 3;
                                        articulosGrp3.title = "Artículos más comprados";

                                        SqlCommand command3 = new SqlCommand("sp_get_articulos_mas_comprados @cant_articulos = @artQty", con);
                                        command3.Parameters.Add("@artQty", SqlDbType.Int);
                                        command3.Parameters["@artQty"].Value = artQty;
                                        articulos3.Load(command3.ExecuteReader());
                                        articulosGrp3.articulos = ArticulosEnGrupos.serializarArticulos(articulos3,0);

                                        artGrupos.Add(articulosGrp3);
                                        break;
                                    case 4:

                                        ArticulosGrupos articulosGrp4 = new ArticulosGrupos();
                                        DataTable articulos4 = new DataTable();
                                        articulosGrp4.groupId = 4;
                                        articulosGrp4.title = "Artículos favoritos";

                                        SqlCommand command4 = new SqlCommand("sp_get_articulos_favoritos @cant_articulos = @artQty", con);
                                        command4.Parameters.Add("@artQty", SqlDbType.Int);
                                        command4.Parameters["@artQty"].Value = artQty;
                                        articulos4.Load(command4.ExecuteReader());
                                        articulosGrp4.articulos = ArticulosEnGrupos.serializarArticulos(articulos4,0);

                                        artGrupos.Add(articulosGrp4);
                                        break;
                                    case 5:

                                        ArticulosGrupos articulosGrp5 = new ArticulosGrupos();
                                        DataTable articulos5 = new DataTable();
                                        articulosGrp5.groupId = 5;
                                        articulosGrp5.title = "Artículos nuevos";

                                        SqlCommand command5 = new SqlCommand("sp_get_articulos_nuevos @cant_articulos = @artQty", con);
                                        command5.Parameters.Add("@artQty", SqlDbType.Int);
                                        command5.Parameters["@artQty"].Value = artQty;
                                        articulos5.Load(command5.ExecuteReader());
                                        articulosGrp5.articulos = ArticulosEnGrupos.serializarArticulos(articulos5,0);

                                        artGrupos.Add(articulosGrp5);
                                        break;
                                    case 6:

                                        ArticulosGrupos articulosGrp6 = new ArticulosGrupos();
                                        DataTable articulos6 = new DataTable();
                                        articulosGrp6.groupId = 6;
                                        articulosGrp6.title = "Artículos populares de tiendas premium";

                                        SqlCommand command6 = new SqlCommand("sp_get_articulos_populares_tiendas_premium @cant_articulos = @artQty", con);
                                        command6.Parameters.Add("@artQty", SqlDbType.Int);
                                        command6.Parameters["@artQty"].Value = artQty;
                                        articulos6.Load(command6.ExecuteReader());
                                        articulosGrp6.articulos = ArticulosEnGrupos.serializarArticulos(articulos6,0);

                                        artGrupos.Add(articulosGrp6);
                                        break;
                                }
                            }
                            else
                            {
                                int count = idArtGruposIdArt.Count;

                                int index = random.Next(0, count);
                                idGrupoArticulo = idArtGruposIdArt[index];
                                idArtGruposIdArt.Remove(idGrupoArticulo);
                                switch (idGrupoArticulo)
                                {
                                    case 1:
                                        articulosGrp.groupId = 1;
                                        articulosGrp.title = "Más artículos del mismo vendedor";

                                        SqlCommand command = new SqlCommand("sp_get_articulos_mismo_vendedor @cant_articulos = @artQty, @id_articulo = @id_articulo", con);
                                        command.Parameters.Add("@artQty", SqlDbType.Int);
                                        command.Parameters.Add("@id_articulo", SqlDbType.Int);
                                        command.Parameters["@artQty"].Value = artQty;
                                        command.Parameters["@id_articulo"].Value = idArticle;
                                        articulos.Load(command.ExecuteReader());
                                        articulosGrp.articulos = ArticulosEnGrupos.serializarArticulos(articulos,0);
                                        artGrupos.Add(articulosGrp);
                                        break;
                                    case 2:

                                        ArticulosGrupos articulosGrp2 = new ArticulosGrupos();
                                        DataTable articulos2 = new DataTable();

                                        articulosGrp2.groupId = 2;
                                        articulosGrp2.title = "Quienes compraron ese artículo también compraron";

                                        SqlCommand command2 = new SqlCommand("sp_get_articulos_compraron_art_tambien_comp @cant_articulos = @artQty, @id_articulo = @id_articulo", con);
                                        command2.Parameters.Add("@artQty", SqlDbType.Int);
                                        command2.Parameters.Add("@id_articulo", SqlDbType.Int);
                                        command2.Parameters["@artQty"].Value = artQty;
                                        command2.Parameters["@id_articulo"].Value = idArticle;
                                        articulos2.Load(command2.ExecuteReader());
                                        articulosGrp2.articulos = ArticulosEnGrupos.serializarArticulos(articulos2,0);
                                        artGrupos.Add(articulosGrp2);
                                        break;
                                        
                                    case 3:

                                        ArticulosGrupos articulosGrp3 = new ArticulosGrupos();
                                        DataTable articulos3 = new DataTable();
                                        articulosGrp3.groupId = 3;
                                        articulosGrp3.title = "Artículos más vendidos de la misma categoría";

                                        SqlCommand command3 = new SqlCommand("sp_get_articulos_mas_vendidos_misma_cat @cant_articulos = @artQty, @id_articulo = @id_articulo", con);
                                        command3.Parameters.Add("@artQty", SqlDbType.Int);
                                        command3.Parameters.Add("@id_articulo", SqlDbType.Int);
                                        command3.Parameters["@artQty"].Value = artQty;
                                        command3.Parameters["@id_articulo"].Value = idArticle;
                                        articulos3.Load(command3.ExecuteReader());
                                        articulosGrp3.articulos = ArticulosEnGrupos.serializarArticulos(articulos3,0);

                                        artGrupos.Add(articulosGrp3);
                                        break;
                                }
                            }

                        }
                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                    }
                    catch (Exception x)
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

            return artGrupos;
        }

        public static List<ArticulosEnGrupos> getArticuloGruposConFiltros(int artQty, int pageId, int? genderId, float? minPrice, float? maxPrice, int? size, int? colourId, int? categoryId, int? storeId, int order, string? search)
        {
            DataTable articulos = new DataTable();
            ArticulosEnGrupos articulosGrp = new ArticulosEnGrupos();
            List<ArticulosEnGrupos> artGruposList = new List<ArticulosEnGrupos>();
            int countArt = artQty * pageId; 
            int showQty = 0;
            if (pageId != 1)
            {
                showQty = countArt - artQty;
            }
            else {
                showQty = 0;
            }

            
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
                        Random random = new Random();
                        
                        SqlCommand command = new SqlCommand("get_articulos_con_filtro @gender_id = @genderId, @colour_id = @colourId, @price_max = @maxPrice, @price_min = @minPrice, @size_id = @size, @category_id = @categoryId, @store_id = @storeId, @art_cuantity = @countArt, @order = @order, @search = @search", con);
                        command.Parameters.Add("@genderId", SqlDbType.Int);
                        command.Parameters.Add("@colourId", SqlDbType.Int);
                        command.Parameters.Add("@maxPrice", SqlDbType.Float);
                        command.Parameters.Add("@minPrice", SqlDbType.Float);
                        command.Parameters.Add("@size", SqlDbType.Int);
                        command.Parameters.Add("@categoryId", SqlDbType.Int);
                        command.Parameters.Add("@storeId", SqlDbType.Int);
                        command.Parameters.Add("@countArt", SqlDbType.Int);
                        command.Parameters.Add("@order", SqlDbType.Int);
                        command.Parameters.Add("@search", SqlDbType.VarChar);
                        if (genderId == null) { genderId = 0; }
                        command.Parameters["@genderId"].Value = genderId;
                        if (colourId == null) { colourId = 0; }
                        command.Parameters["@colourId"].Value = colourId;
                        if (maxPrice == null) { maxPrice = 100000; }
                        command.Parameters["@maxPrice"].Value = maxPrice;
                        if (minPrice == null) { minPrice = 0; }
                        command.Parameters["@minPrice"].Value = minPrice;
                        if (size == null) { size = 0; }
                        command.Parameters["@size"].Value = size;
                        if (categoryId == null) { categoryId = 0; }
                        command.Parameters["@categoryId"].Value = categoryId;
                        if (storeId == null) { storeId = 0; }
                        command.Parameters["@storeId"].Value = storeId;
                        if (search == null) { search = ""; }
                        command.Parameters["@search"].Value = search;
                        command.Parameters["@countArt"].Value = countArt;
                        command.Parameters["@order"].Value = order;
                            

                        articulos.Load(command.ExecuteReader());
                            artGruposList = ArticulosEnGrupos.serializarArticulos(articulos, showQty);
                               
                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                    }
                    catch (Exception x)
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

            return artGruposList;
        }

        public static List<ArticulosEnGrupos> getArticulosFavoritos(int idUsuario)
        {
            DataTable articulos = new DataTable();
            ArticulosEnGrupos articulosGrp = new ArticulosEnGrupos();
            List<ArticulosEnGrupos> artGruposList = new List<ArticulosEnGrupos>();
                        
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
                        Random random = new Random();

                        SqlCommand command = new SqlCommand("sp_get_favoritos @user_id = @user_id", con);
                        command.Parameters.Add("@user_id", SqlDbType.Int);
                        command.Parameters["@user_id"].Value = idUsuario;
                       
                        articulos.Load(command.ExecuteReader());
                        artGruposList = ArticulosEnGrupos.serializarArticulos(articulos, 0);

                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                    }
                    catch (Exception x)
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

            return artGruposList;
        }
        public static bool addArticles(ArticulosAdd articulosAdd)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();

                        string emailHash = articulosAdd.email.ToUpper();

                        int id_usuario = consultaIdUsuarioPorHash(emailHash);
                        if (id_usuario == 0)
                        {
                            return false;
                        }

                        SqlCommand command = new SqlCommand("Select store_id from suitapp.dbo.stores where user_id = @user_id and store_id = @store_id", con);
                        command.Parameters.AddWithValue("@user_id", id_usuario);
                        command.Parameters.AddWithValue("@store_id", articulosAdd.storeId);
                        SqlDataReader store = command.ExecuteReader();

                        if (!store.Read())
                        {
                            return false;
                        }

                        SqlCommand command5 = new SqlCommand("sp_ins_articles", con);
                        command5.CommandType = CommandType.StoredProcedure;
                        command5.Parameters.AddWithValue("@store_id", articulosAdd.storeId);
                        command5.Parameters.AddWithValue("@price", articulosAdd.articlePrice);
                        command5.Parameters.AddWithValue("@category_id", articulosAdd.categoryId);
                        command5.Parameters.AddWithValue("@description", articulosAdd.articleDesc);
                        command5.Parameters.AddWithValue("@gender_id", articulosAdd.genderId);
                        command5.Parameters.AddWithValue("@name", articulosAdd.articleName);
                        command5.ExecuteNonQuery();

                        DataTable dataTable = new DataTable();
                        SqlCommand command10 = new SqlCommand("select max(article_id) as id_article from suitapp.dbo.articles", con);
                        dataTable.Load(command10.ExecuteReader());

                        int id_article = Convert.ToInt32(dataTable.Rows[0]["id_article"]);

                        int i = 0;
                        foreach (string fila in articulosAdd.images)
                        {
                            if (articulosAdd.images[i] != null)
                            {
                                SqlCommand command6 = new SqlCommand("sp_ins_images", con);
                                command6.CommandType = CommandType.StoredProcedure;
                                command6.Parameters.AddWithValue("@image", articulosAdd.images[i]);
                                command6.Parameters.AddWithValue("@component_id", id_article);
                                command6.Parameters.AddWithValue("@type_id", 2);
                                command6.ExecuteNonQuery();
                            }
                            i++;
                        }

                        int j = 0;
                        foreach (VariantesAdd fila in articulosAdd.variants)
                        {
                            if (articulosAdd.variants[j] != null)
                            {
                                SqlCommand command7 = new SqlCommand("sp_ins_variantes", con);
                                command7.CommandType = CommandType.StoredProcedure;
                                command7.Parameters.AddWithValue("@article_id", id_article);
                                command7.Parameters.AddWithValue("@colour_id", articulosAdd.variants[j].colorId);
                                command7.Parameters.AddWithValue("@size_id", articulosAdd.variants[j].sizeId);
                                command7.Parameters.AddWithValue("@stock", articulosAdd.variants[j].stock);
                                command7.ExecuteNonQuery();
                            }
                            j++;
                        }

                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return true;
        }

        public static bool deleteArticles(int id_usuario, int id_articulo)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();

                        
                        SqlCommand command = new SqlCommand("select s.store_id from suitapp.dbo.stores s left join suitapp.dbo.articles a on s.store_id = a.store_id where s.user_id = @user_id and a.article_id = @article_id", con);
                        command.Parameters.AddWithValue("@user_id", id_usuario);
                        command.Parameters.AddWithValue("@article_id", id_articulo);
                        SqlDataReader store = command.ExecuteReader();

                        if (!store.Read())
                        {
                            return false;
                        }

                        SqlCommand command5 = new SqlCommand("sp_del_articles", con);
                        command5.CommandType = CommandType.StoredProcedure;
                        command5.Parameters.AddWithValue("@id_articulo", id_articulo);
                        command5.Parameters.AddWithValue("@type_id", 2);
                        command5.ExecuteNonQuery();
                                                                        
                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return true;
        }
        #endregion
        #region STORES
        public static List<StoresGrupos> getStoresGrupos(int groupQty, int storeQty)
        {
           
            StoresGrupos storesGrp = new StoresGrupos();
            List<StoresGrupos> storeGrupos = new List<StoresGrupos>();
            List<int> idStoreGrupos = new List<int>() { 1, 2, 3, 4};
            
            int idGrupoStore = 0;
           
                        Random random = new Random();
                        for (int i = 0; i < groupQty; i++)
                        {


                            int count = idStoreGrupos.Count;
                            string nameStored;
                            int index = random.Next(0, count);
                            idGrupoStore = idStoreGrupos[index];
                            idStoreGrupos.Remove(idGrupoStore);

                            switch (idGrupoStore)
                            {
                                case 1:
                                    nameStored = "sp_get_tiendas_populares";
                                    storesGrp.groupId = 1;
                                    storesGrp.title = "Tiendas populares";
                                    storesGrp.stores = getStoresForIdGroup(storeQty, nameStored,2);

                                    storeGrupos.Add(storesGrp);
                                    break;
                                case 2:
                        StoresGrupos storesGrp2 = new StoresGrupos();
                                    nameStored = "sp_get_tiendas_mas_art_vendidos";
                                    storesGrp2.groupId = 2;
                                    storesGrp2.title = "Mas articulos vendidos";
                                    storesGrp2.stores = getStoresForIdGroup(storeQty, nameStored,2);

                                    storeGrupos.Add(storesGrp2);
                        break;
                                case 3:

                        StoresGrupos storesGrp3 = new StoresGrupos();
                        nameStored = "sp_get_tiendas_mejor_puntuacion";
                        storesGrp3.groupId = 3;
                        storesGrp3.title = "Tiendas con mejor puntuacion";
                        storesGrp3.stores = getStoresForIdGroup(storeQty, nameStored,2);

                        storeGrupos.Add(storesGrp3);
                        break;
                                case 4:

                        StoresGrupos storesGrp4 = new StoresGrupos();
                        nameStored = "sp_get_tiendas_nuevas";
                        storesGrp4.groupId = 3;
                        storesGrp4.title = "Tiendas nuevas";
                        storesGrp4.stores = getStoresForIdGroup(storeQty, nameStored,2);

                        storeGrupos.Add(storesGrp4);
                        break;
                                
                        }
                    }
            return storeGrupos;

        }

        public static List<StoresEnGrupos> getStoresPremium(int storeQty)
        {
            List <StoresEnGrupos> strEngrpList = new List<StoresEnGrupos> ();
            string nameStored;
            nameStored = "sp_get_tiendas_premium";

            strEngrpList = getStoresForIdGroup(storeQty, nameStored,3);

            return strEngrpList;

        }


        public static Stores getStore(int id_store)
        {
            DataTable dataTable = new DataTable();
            DataTable storesShimpents = new DataTable();
            Stores stores = new Stores();
            List <ShippingPrice> shipingPriceList = new List<ShippingPrice>();
            // ServiciosDetalle serviciosDetalleList = new ServiciosDetalle();
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString))
            //{

                //{
                    try
                    {
                        //SE ABRE LA CONEXION 
                        con.Open();

                        //SE UTILIZA PARA LLENAR UN DATASET CON LOS ELEMENTOS NECESARIOS  
                        //COMO UNA CONEXION DE BASE DE DATOS 
                        SqlCommand command = new SqlCommand("sp_get_store @p_id_store = @id", con);
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = id_store;
                        dataTable.Load(command.ExecuteReader());

                        stores.storeId = Convert.ToInt16(dataTable.Rows[0]["store_id"]);
                        stores.storeName = dataTable.Rows[0]["name"].ToString();
                        stores.coord = dataTable.Rows[0]["coord"].ToString();
                        stores.address = dataTable.Rows[0]["address"].ToString();
                        stores.description = dataTable.Rows[0]["description"].ToString();
                        stores.storeLogo = dataTable.Rows[0]["logo"].ToString();
                        stores.storeCoverPhoto = Stores.getImagen(stores.storeId);
                    if (Convert.ToInt16(dataTable.Rows[0]["physical_store"]) == 1)
                        {
                            stores.physical_store = true;
                        }
                        else
                        {
                            stores.physical_store = false;
                        }
                        if (Convert.ToInt16(dataTable.Rows[0]["premium"]) == 1)
                        {
                            stores.premium = true;
                        }
                        else
                        {
                            stores.premium = false;
                        }

                    SqlCommand command2 = new SqlCommand("sp_get_stores_shipments @store_id = @id", con);
                    command2.Parameters.Add("@id", SqlDbType.Int);
                    command2.Parameters["@id"].Value = id_store;
                    storesShimpents.Load(command2.ExecuteReader());

                    if(storesShimpents.Rows.Count > 0) 
                    {
                        int k = 0;
                        foreach (DataRow fila2 in storesShimpents.Rows)
                        {
                            ShippingPrice shippingPrice = new ShippingPrice();
                            shippingPrice.idStore = Convert.ToInt16(storesShimpents.Rows[k]["store_id"]);
                            shippingPrice.idProvince = Convert.ToInt16(storesShimpents.Rows[k]["province_id"]);
                            shippingPrice.price = Convert.ToSingle(storesShimpents.Rows[k]["price"]);
                            shippingPrice.nameProvince = storesShimpents.Rows[k]["name"].ToString();

                            shipingPriceList.Add(shippingPrice);
                            k++;
                        }

                        stores.shippingPrice = shipingPriceList;
                        stores.mailShipping = true;

                    }
                    else{ stores.mailShipping = false; }
                    return stores;

                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                    }
                    finally
                    {

                        con.Close();

                    }

              //  }
            //}
            //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA                                     

            return stores;
        }

        public static List<StoresEnGrupos> getStore()
        {
            DataTable dataTable = new DataTable();
            List<StoresEnGrupos> stores = new List<StoresEnGrupos>();
            // ServiciosDetalle serviciosDetalleList = new ServiciosDetalle();
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString))
                //{

                //{
                try
                {
                    //SE ABRE LA CONEXION 
                    con.Open();

                    //SE UTILIZA PARA LLENAR UN DATASET CON LOS ELEMENTOS NECESARIOS  
                    //COMO UNA CONEXION DE BASE DE DATOS 
                    SqlCommand command = new SqlCommand("select store_id, name, slogan, logo from stores where activo = 1 order by name asc", con);
                    dataTable.Load(command.ExecuteReader());

                    stores = StoresEnGrupos.serializarStores(dataTable, 1,0);
                }
                catch (SqlException x)
                {
                    Console.WriteLine(x);
                }
                finally
                {

                    con.Close();

                }

            //  }
            //}
            //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA                                     

            return stores;
        }

        public static List<StoresEnGrupos> getStore(string hash)
        {
            DataTable dataTable = new DataTable();
            List<StoresEnGrupos> stores = new List<StoresEnGrupos>();
            // ServiciosDetalle serviciosDetalleList = new ServiciosDetalle();
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ConnectionString))
                //{

                //{
                try
                {
                    //SE ABRE LA CONEXION 
                    con.Open();

                    //SE UTILIZA PARA LLENAR UN DATASET CON LOS ELEMENTOS NECESARIOS  
                    //COMO UNA CONEXION DE BASE DE DATOS 
                    string hashUpper = hash.ToUpper();

                    int id_usuario = consultaIdUsuarioPorHash(hashUpper);
                    SqlCommand command = new SqlCommand("select store_id, name, logo from stores where user_id = @id_usuario and activo = 1 order by name asc", con);
                    command.Parameters.AddWithValue("@id_usuario", id_usuario);
                    dataTable.Load(command.ExecuteReader());

                    stores = StoresEnGrupos.serializarStores(dataTable, 1, 0);
                }
                catch (SqlException x)
                {
                    Console.WriteLine(x);
                }
                finally
                {

                    con.Close();

                }

            //  }
            //}
            //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA                                     

            return stores;
        }

        public static List<StoresEnGrupos> getStoresGruposConFiltros(int storesQty, int pageId, int? genderId, float? minPrice, float? maxPrice, int? size, int? colourId, int? categoryId, string? search)
        {
            DataTable stores = new DataTable();
            StoresEnGrupos storesGrp = new StoresEnGrupos();
            List<StoresEnGrupos> strGruposList = new List<StoresEnGrupos>();
            int countArt = storesQty * pageId;
            int showQty = 0;
            if (pageId != 1)
            {
                showQty = countArt - storesQty;
            }
            else
            {
                showQty = 0;
            }


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
                        Random random = new Random();

                        SqlCommand command = new SqlCommand("sp_get_tiendas_con_filtro @gender_id = @genderId, @colour_id = @colourId, @price_max = @maxPrice, @price_min = @minPrice, @size_id = @size, @category_id = @categoryId, @art_cuantity = @countArt, @search = @search", con);
                        command.Parameters.Add("@genderId", SqlDbType.Int);
                        command.Parameters.Add("@colourId", SqlDbType.Int);
                        command.Parameters.Add("@maxPrice", SqlDbType.Float);
                        command.Parameters.Add("@minPrice", SqlDbType.Float);
                        command.Parameters.Add("@size", SqlDbType.Int);
                        command.Parameters.Add("@categoryId", SqlDbType.Int);
                        command.Parameters.Add("@countArt", SqlDbType.Int);
                        command.Parameters.Add("@search", SqlDbType.VarChar);
                        if (genderId == null) { genderId = 0; }
                        command.Parameters["@genderId"].Value = genderId;
                        if (colourId == null) { colourId = 0; }
                        command.Parameters["@colourId"].Value = colourId;
                        if (maxPrice == null) { maxPrice = 100000; }
                        command.Parameters["@maxPrice"].Value = maxPrice;
                        if (minPrice == null) { minPrice = 0; }
                        command.Parameters["@minPrice"].Value = minPrice;
                        if (size == null) { size = 0; }
                        command.Parameters["@size"].Value = size;
                        if (categoryId == null) { categoryId = 0; }
                        command.Parameters["@categoryId"].Value = categoryId;
                        if (search == null) { search = ""; }
                        command.Parameters["@search"].Value = search;
                        command.Parameters["@countArt"].Value = countArt;



                        stores.Load(command.ExecuteReader());
                        strGruposList = StoresEnGrupos.serializarStores(stores, 2, showQty);

                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                    }
                    catch (Exception x)
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

            return strGruposList;
        }

        public static List<StoresEnGrupos> getStoresForIdGroup(int store, string nameStore, int typeGetStore)
        {

            DataTable stores = new DataTable();
            List<StoresEnGrupos> storeEnGrupoList = new List<StoresEnGrupos>();
             
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
                        SqlCommand command = new SqlCommand(nameStore+" @cant_tiendas = @artQty", con);
                        command.Parameters.Add("@artQty", SqlDbType.Int);
                        command.Parameters["@artQty"].Value = store;
                        stores.Load(command.ExecuteReader());
                        storeEnGrupoList = StoresEnGrupos.serializarStores(stores, typeGetStore,0);
                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x);
                    }
                    finally
                    {

                        con.Close();

                    }
                    return storeEnGrupoList;
                }
            }
        }

        public static bool addStore(StoresAdd storeadd)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();

                        string emailHash = storeadd.email.ToUpper();

                        int id_usuario = consultaIdUsuarioPorHash(emailHash);
                        if(id_usuario == 0)
                        {
                            return false;
                        }

                        SqlCommand command5 = new SqlCommand("sp_ins_store", con);
                        command5.CommandType = CommandType.StoredProcedure;
                        command5.Parameters.AddWithValue("@name", storeadd.storeName);
                        //command5.Parameters.AddWithValue("@slogan", storeadd.storeSlogan);
                        command5.Parameters.AddWithValue("@user_id", id_usuario);
                        command5.Parameters.AddWithValue("@address", storeadd.addressAdd.street + " " + storeadd.addressAdd.street_number + (storeadd.addressAdd.floor != null ? " P: " + storeadd.addressAdd.floor : "") + (storeadd.addressAdd.apartment != null ? " D: " + storeadd.addressAdd.apartment : "") + ", " + storeadd.addressAdd.city);
                        command5.Parameters.AddWithValue("@description", storeadd.description);
                        command5.Parameters.AddWithValue("@logo", storeadd.storeLogo);
                        if (storeadd.physical_store == true) 
                        {
                            command5.Parameters.AddWithValue("@physical_store", 1);
                        }
                        else { command5.Parameters.AddWithValue("@physical_store", 0); }
    
                        command5.ExecuteNonQuery();

                        DataTable dataTable = new DataTable();
                        SqlCommand command10 = new SqlCommand("select max(store_id) as id_store from suitapp.dbo.stores", con);
                        dataTable.Load(command10.ExecuteReader());
                        
                        int id_store = Convert.ToInt32(dataTable.Rows[0]["id_store"]);

                        SqlCommand command6 = new SqlCommand("sp_ins_images", con);
                        command6.CommandType = CommandType.StoredProcedure;
                        command6.Parameters.AddWithValue("@image", storeadd.storeCoverPhoto);
                        command6.Parameters.AddWithValue("@component_id", id_store);
                        command6.Parameters.AddWithValue("@type_id", 1);
                        command6.ExecuteNonQuery();

                        int i = 0;
                        foreach (ShippingPrice fila in storeadd.shippingPrice)
                        {
                            if (storeadd.shippingPrice[i] != null)
                            {
                                SqlCommand command7 = new SqlCommand("sp_ins_stores_shipments", con);
                                command7.CommandType = CommandType.StoredProcedure;
                                command7.Parameters.AddWithValue("@store_id", id_store);
                                command7.Parameters.AddWithValue("@province_id", storeadd.shippingPrice[i].idProvince);
                                command7.Parameters.AddWithValue("@price", storeadd.shippingPrice[i].price);
                                command7.ExecuteNonQuery();
                            }
                            i++;
                        }

                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return true;
        }
        public static bool deleteStores(int id_usuario, int id_store)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();


                        SqlCommand command = new SqlCommand("select store_id from suitapp.dbo.stores where store_id = @store_id and user_id = @user_id", con);
                        command.Parameters.AddWithValue("@user_id", id_usuario);
                        command.Parameters.AddWithValue("@store_id", id_store);
                        SqlDataReader store = command.ExecuteReader();

                        if (!store.Read())
                        {
                            return false;
                        }

                        SqlCommand command2 = new SqlCommand("select article_id from suitapp.dbo.articles where store_id = @store_id", con);
                        command2.Parameters.AddWithValue("@store_id", id_store);
                        DataTable article = new DataTable();
                        article.Load(command2.ExecuteReader());
                        int i = 0;
                        foreach (DataRow fila in article.Rows)
                        {
                            SqlCommand command5 = new SqlCommand("sp_del_articles", con);
                            command5.CommandType = CommandType.StoredProcedure;
                            command5.Parameters.AddWithValue("@id_articulo", Convert.ToInt16(article.Rows[i]["article_id"]));
                            command5.Parameters.AddWithValue("@type_id", 2);
                            command5.ExecuteNonQuery();
                            i++;
                        }

                        SqlCommand command6 = new SqlCommand("sp_del_stores", con);
                        command6.CommandType = CommandType.StoredProcedure;
                        command6.Parameters.AddWithValue("@id_store", id_store);
                        command6.Parameters.AddWithValue("@type_id", 1);
                        command6.ExecuteNonQuery();

                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return true;
        }
        #endregion
        #region CATEGORIAS
        public static List<Categorias> getCategorias()
        {
            DataTable dataTable = new DataTable();
            List<Categorias> categoriasList = new List<Categorias>();
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
                        SqlCommand command = new SqlCommand("sp_get_categorias", con);
                        //command.Parameters.Add("@id", SqlDbType.Int);
                        //command.Parameters["@id"].Value = 0;
                        dataTable.Load(command.ExecuteReader());

                        categoriasList = Categorias.SerializarCategorias(dataTable);

                    }
                    catch (Exception x)
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

            return categoriasList;
        }

        public static List<Categorias> getCategorias(int id)
        {
            //EL DataSet REPRESENTA UNA MEMORIA CACHÉ DE DATOS EN MEMORIA 
            DataTable dataTable = new DataTable();
            List<Categorias> categoriasList = new List<Categorias>();

            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {
                try
                {
                    //SE ABRE LA CONEXION 
                    con.Open();

                    //SE UTILIZA PARA LLENAR UN DATASET CON LOS ELEMENTOS NECESARIOS  
                    //COMO UNA CONEXION DE BASE DE DATOS 
                    //   using (SqlDataAdapter sqlAdapter = new SqlDataAdapter())

                    SqlCommand command = new SqlCommand("sp_get_categorias @p_id_categoria_padre = @id", con);
                    command.Parameters.Add("@id", SqlDbType.Int);
                    command.Parameters["@id"].Value = id;

                    dataTable.Load(command.ExecuteReader());
                    categoriasList = Categorias.SerializarCategorias(dataTable);


                }
                catch (Exception x)
                {

                }
                finally
                {

                    con.Close();

                }


            }
            //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA 

            return categoriasList;
        }

        /* public static List<Categorias> getCategorias(int quantity, bool top)
         {
             //EL DataSet REPRESENTA UNA MEMORIA CACHÉ DE DATOS EN MEMORIA 
             DataTable dataTable = new DataTable();
             int populares;
             List<Categorias> categoriasList = new List<Categorias>();

             //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
             //CON EL CONFIGURATIONMANAGER 
             using (SqlConnection con = new SqlConnection(
                 ConfigurationManager.ConnectionStrings["cs"]
                 .ConnectionString))
             {
                 try
                 {
                     //SE ABRE LA CONEXION 
                     con.Open();

                     if(top == true)
                     {
                         populares = 1;
                     }else
                     {
                         populares = 0;
                     }

                     SqlCommand command = new SqlCommand("sp_get_categorias @quantity = @quantity, @top = @top", con);
                     command.Parameters.Add("@quantity", SqlDbType.Int);
                     command.Parameters.Add("@top", SqlDbType.Int);
                     command.Parameters["@quantity"].Value = quantity;
                     command.Parameters["@top"].Value = populares;

                     dataTable.Load(command.ExecuteReader());
                     categoriasList = Categorias.SerializarCategorias(dataTable);


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
             //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA 

             return categoriasList;
         }*/

        #endregion

        #region COLORES
        public static List<Colores> getColores()
        {
            DataTable dataTable = new DataTable();
            List<Colores> coloresList = new List<Colores>();
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
                        SqlCommand command = new SqlCommand("select colour_id, name, hex_code from colours ", con);
                        //command.Parameters.Add("@id", SqlDbType.Int);
                        //command.Parameters["@id"].Value = 0;
                        dataTable.Load(command.ExecuteReader());

                        coloresList = Colores.SerializarColores(dataTable);

                    }
                    catch (Exception x)
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

            return coloresList;
        }
        #endregion

        #region Carrito
        public static List<Carrito> getCarrito(int idUsuario)
        {
            DataTable articulos = new DataTable();
            Carrito carrito = new Carrito();
            List<Carrito> carritoList = new List<Carrito>();

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
                        Random random = new Random();

                        SqlCommand command = new SqlCommand("sp_get_carrito @user_id = @user_id", con);
                        command.Parameters.Add("@user_id", SqlDbType.Int);
                        command.Parameters["@user_id"].Value = idUsuario;

                        articulos.Load(command.ExecuteReader());
                        carritoList = Carrito.serializarArticulos(articulos);

                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                    }
                    catch (Exception x)
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

            return carritoList;
        }

        public static bool addArticlesCarrito(CarritoAdd carritoAdd)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();

                        string emailHash = carritoAdd.email.ToUpper();

                        int id_usuario = consultaIdUsuarioPorHash(emailHash);
                        if (id_usuario == 0)
                        {
                            return false;
                        }


                        SqlCommand command4 = new SqlCommand("Select variants_id from suitapp.dbo.variants where article_id = @article_id and colour_id = @color_id and size_id = @size_id", con);
                        command4.Parameters.AddWithValue("@article_id", carritoAdd.articleId);
                        command4.Parameters.AddWithValue("@color_id", carritoAdd.colorId);
                        command4.Parameters.AddWithValue("@size_id", carritoAdd.sizeId);

                        DataTable variants = new DataTable();
                        variants.Load(command4.ExecuteReader());

                        int variantId = 0;
                        if (variants.Columns.Contains("variants_id"))
                        {
                            variantId = Convert.ToInt32(variants.Rows[0]["variants_id"]);
                        }
                        else
                        {
                            return false;

                        }

                        SqlCommand command3 = new SqlCommand("Select * from suitapp.dbo.cart where user_id = @user_id and variant_id = @variant_id", con);
                        command3.Parameters.AddWithValue("@user_id", id_usuario);
                        command3.Parameters.AddWithValue("@variant_id", variantId);
                        SqlDataReader dataReader = command3.ExecuteReader();
                        if (!dataReader.Read())
                        {
                            SqlCommand command5 = new SqlCommand("sp_ins_carrito", con);
                            command5.CommandType = CommandType.StoredProcedure;
                            command5.Parameters.AddWithValue("@user_id", id_usuario);
                            command5.Parameters.AddWithValue("@variant_id", variantId);
                            command5.Parameters.AddWithValue("@quantity", carritoAdd.quantity);
                            command5.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand command6 = new SqlCommand("UPDATE suitapp.dbo.cart set active = 1, quantity = @quantity where user_id = @user_id and variant_id = @variant_id", con);
                            command6.Parameters.AddWithValue("@user_id", id_usuario);
                            command6.Parameters.AddWithValue("@variant_id", variantId);
                            command6.Parameters.AddWithValue("@quantity", carritoAdd.quantity);
                            command6.ExecuteNonQuery();
                        }

                        

                                                
                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return true;
        }

        public static bool deleteArticlesCarrito(CarritoAdd carritoAdd)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();

                        string emailHash = carritoAdd.email.ToUpper();

                        int id_usuario = consultaIdUsuarioPorHash(emailHash);
                        if (id_usuario == 0)
                        {
                            return false;
                        }

                        SqlCommand command4 = new SqlCommand("Select variants_id from suitapp.dbo.variants where article_id = @article_id and colour_id = @color_id and size_id = @size_id", con);
                        command4.Parameters.AddWithValue("@article_id", carritoAdd.articleId);
                        command4.Parameters.AddWithValue("@color_id", carritoAdd.colorId);
                        command4.Parameters.AddWithValue("@size_id", carritoAdd.sizeId);

                        DataTable variants = new DataTable();
                        variants.Load(command4.ExecuteReader());

                        int variantId = 0;
                        if (variants.Columns.Contains("variants_id"))
                        {
                            variantId = Convert.ToInt32(variants.Rows[0]["variants_id"]);
                        }
                        else
                        {
                            return false;

                        }

                        SqlCommand command5 = new SqlCommand("UPDATE suitapp.dbo.cart set active = 0 where user_id = @user_id and variant_id = @variant_id", con);
                        command5.Parameters.AddWithValue("@user_id", id_usuario);
                        command5.Parameters.AddWithValue("@variant_id", variantId);
                        command5.ExecuteNonQuery();


                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return true;
        }

        #endregion

        #region Favoritos
        public static bool addArticlesFavoritos(FavoritosAdd favoritosAdd)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();

                        string emailHash = favoritosAdd.email.ToUpper();

                        int id_usuario = consultaIdUsuarioPorHash(emailHash);
                        if (id_usuario == 0)
                        {
                            return false;
                        }

                        SqlCommand command3 = new SqlCommand("Select * from suitapp.dbo.favorites where user_id = @user_id and article_id = @article_id", con);
                        command3.Parameters.AddWithValue("@user_id", id_usuario);
                        command3.Parameters.AddWithValue("@article_id", favoritosAdd.articleId);
                        SqlDataReader dataReader = command3.ExecuteReader();
                        if (!dataReader.Read())
                        {
                            SqlCommand command5 = new SqlCommand("sp_ins_favoritos", con);
                            command5.CommandType = CommandType.StoredProcedure;
                            command5.Parameters.AddWithValue("@user_id", id_usuario);
                            command5.Parameters.AddWithValue("@article_id", favoritosAdd.articleId);
                            command5.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand command4 = new SqlCommand("UPDATE suitapp.dbo.favorites set active = 1 where user_id = @user_id and article_id = @article_id", con);
                            command4.Parameters.AddWithValue("@user_id", id_usuario);
                            command4.Parameters.AddWithValue("@article_id", favoritosAdd.articleId);
                            command4.ExecuteNonQuery();
                        }


                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return true;
        }

        public static bool deleteArticlesFavoritos(FavoritosAdd favoritosAdd)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();

                        string emailHash = favoritosAdd.email.ToUpper();

                        int id_usuario = consultaIdUsuarioPorHash(emailHash);
                        if (id_usuario == 0)
                        {
                            return false;
                        }


                        SqlCommand command5 = new SqlCommand("UPDATE suitapp.dbo.favorites set active = 0 where user_id = @user_id and article_id = @article_id", con);
                        command5.Parameters.AddWithValue("@user_id", id_usuario);
                        command5.Parameters.AddWithValue("@article_id", favoritosAdd.articleId);
                        command5.ExecuteNonQuery();


                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return true;
        }
        #endregion

        #region Compras
        public static List<Compras> getCompras(int idUsuario)
        {
            DataTable articulos = new DataTable();
            Compras compras = new Compras();
            List<Compras> comprasList = new List<Compras>();

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
                        Random random = new Random();

                        SqlCommand command = new SqlCommand("sp_get_compras @user_id = @user_id", con);
                        command.Parameters.Add("@user_id", SqlDbType.Int);
                        command.Parameters["@user_id"].Value = idUsuario;

                        articulos.Load(command.ExecuteReader());
                        comprasList = Compras.serializarArticulos(articulos);

                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                    }
                    catch (Exception x)
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

            return comprasList;
        }

        public static bool addShopping(ComprasAdd comprasAdd)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();

                        string emailHash = comprasAdd.email.ToUpper();

                        int id_usuario = consultaIdUsuarioPorHash(emailHash);
                        if (id_usuario == 0)
                        {
                            return false;
                        }

                        int shipPrice = 0;

                        if (comprasAdd.mailing == true) 
                        {
                            SqlCommand command3 = new SqlCommand("select distinct ss.price from suitapp.dbo.stores_shipments ss join address a on ss.province_id = a.province_id join articles art on ss.store_id = art.store_id where art.article_id = @article_id", con);
                            command3.Parameters.AddWithValue("@article_id", comprasAdd.carritos[0].articleId);
                            DataTable shippingPrice = new DataTable();
                            shippingPrice.Load(command3.ExecuteReader());

                            shipPrice = Convert.ToInt16(shippingPrice.Rows[0]["price"]);
                        }
                        else
                        {
                            shipPrice = 0;
                        }

                        if(comprasAdd.addressId == null)
                        {
                            comprasAdd.addressId = 0;
                        }
                       
                        SqlCommand command5 = new SqlCommand("sp_ins_compras", con);
                        command5.CommandType = CommandType.StoredProcedure;
                        command5.Parameters.AddWithValue("@user_id", id_usuario);
                        command5.Parameters.AddWithValue("@address_id", comprasAdd.addressId);
                        command5.Parameters.AddWithValue("@shipping_price", shipPrice);
                        command5.ExecuteNonQuery();

                        SqlCommand command6 = new SqlCommand("select max(shopping_id) as shopping_id from shopping", con);
                        DataTable shoppingId = new DataTable();
                        shoppingId.Load(command6.ExecuteReader());

                        int idShopping = Convert.ToInt16(shoppingId.Rows[0]["shopping_id"]);

                        int j = 0;
                        foreach (CarritoAdd fila in comprasAdd.carritos)
                        {
                            if (comprasAdd.carritos[j] != null)
                            {
                                SqlCommand command4 = new SqlCommand("Select variants_id from suitapp.dbo.variants where article_id = @article_id and colour_id = @color_id and size_id = @size_id", con);
                                command4.Parameters.AddWithValue("@article_id", comprasAdd.carritos[j].articleId);
                                command4.Parameters.AddWithValue("@color_id", comprasAdd.carritos[j].colorId);
                                command4.Parameters.AddWithValue("@size_id", comprasAdd.carritos[j].sizeId);

                                DataTable variants = new DataTable();
                                variants.Load(command4.ExecuteReader());

                                int variantId = 0;
                                if (variants.Columns.Contains("variants_id"))
                                {
                                    variantId = Convert.ToInt32(variants.Rows[0]["variants_id"]);
                                }
                                else
                                {
                                    return false;

                                }

                                SqlCommand command8 = new SqlCommand("select price from suitapp.dbo.articles where article_id = @article_id", con);
                                command8.Parameters.AddWithValue("@article_id", comprasAdd.carritos[j].articleId);
                                
                                DataTable price = new DataTable();
                                price.Load(command8.ExecuteReader());

                                int priceArt = Convert.ToInt32(price.Rows[0]["price"]);
                                int? priceTotal = priceArt * comprasAdd.carritos[j].quantity;


                                SqlCommand command7 = new SqlCommand("sp_ins_compras_detalle", con);
                                command7.CommandType = CommandType.StoredProcedure;
                                command7.Parameters.AddWithValue("@package_id", idShopping);
                                command7.Parameters.AddWithValue("@article_id", comprasAdd.carritos[j].articleId);
                                command7.Parameters.AddWithValue("@variant_id", variantId);
                                command7.Parameters.AddWithValue("@quantity", comprasAdd.carritos[j].quantity);
                                command7.Parameters.AddWithValue("@price", priceTotal);
                                command7.Parameters.AddWithValue("@shopping_id", idShopping);
                                command7.Parameters.AddWithValue("@user_id", id_usuario);
                                command7.ExecuteNonQuery();
                            }
                            j++;
                        }


                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return true;
        }
        #endregion

        #region ADDRESS
        public static List<Address> getAddress(int idUsuario)
        {
            DataTable addres = new DataTable();
            List<Address> addressList = new List<Address>();

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
                        
                        SqlCommand command = new SqlCommand("sp_get_address @user_id = @user_id", con);
                        command.Parameters.Add("@user_id", SqlDbType.Int);
                        command.Parameters["@user_id"].Value = idUsuario;

                        addres.Load(command.ExecuteReader());
                        addressList = Address.serializarAddress(addres);

                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                    }
                    catch (Exception x)
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

            return addressList;
        }

        public static bool addAddress(Address address)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();

                        string emailHash = address.email.ToUpper();

                        int id_usuario = consultaIdUsuarioPorHash(emailHash);
                        if (id_usuario == 0)
                        {
                            return false;
                        }

                        
                        SqlCommand command5 = new SqlCommand("sp_ins_address", con);
                        command5.CommandType = CommandType.StoredProcedure;
                        command5.Parameters.AddWithValue("@user_id", id_usuario);
                        command5.Parameters.AddWithValue("@street", address.street);
                        command5.Parameters.AddWithValue("@street_number", address.street_number);
                        command5.Parameters.AddWithValue("@province_id", address.province_id);
                        command5.Parameters.AddWithValue("@city", address.city);
                        command5.Parameters.AddWithValue("@postal_code", address.postal_code);
                        command5.Parameters.AddWithValue("@description", address.description);
                        if (address.floor == null) { command5.Parameters.AddWithValue("@floor", ""); }
                        else { command5.Parameters.AddWithValue("@floor", address.floor); }
                        if(address.apartment == null) { command5.Parameters.AddWithValue("@aparment", ""); }
                        else { command5.Parameters.AddWithValue("@aparment", address.apartment); }
                        


                        command5.ExecuteNonQuery();
                        


                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return true;
        }
        public static bool deleteAddress(string email, int addressId)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();

                        string emailHash = email.ToUpper();

                        int id_usuario = consultaIdUsuarioPorHash(emailHash);
                        if (id_usuario == 0)
                        {
                            return false;
                        }


                        SqlCommand command5 = new SqlCommand("UPDATE suitapp.dbo.address set activo = 0 where user_id = @user_id and address_id = @address_id", con);
                        command5.Parameters.AddWithValue("@user_id", id_usuario);
                        command5.Parameters.AddWithValue("@address_id", addressId);
                        command5.ExecuteNonQuery();


                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return true;
        }

        #endregion

        #region TALLES
        public static List<Talles> getTalles()
        {
            DataTable dataTable = new DataTable();
            List<Talles> tallesList = new List<Talles>();
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
                        SqlCommand command = new SqlCommand("select size_id, name from sizes", con);
                        //command.Parameters.Add("@id", SqlDbType.Int);
                        //command.Parameters["@id"].Value = 0;
                        dataTable.Load(command.ExecuteReader());

                        tallesList = Talles.SerializarTalles(dataTable);

                    }
                    catch (Exception x)
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

            return tallesList;
        }

        #endregion

        #region GENEROS
        public static List<Genders> getGeneros()
        {
            DataTable dataTable = new DataTable();
            List<Genders> generosList = new List<Genders>();
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
                        SqlCommand command = new SqlCommand("sp_get_generos", con);
                        //command.Parameters.Add("@id", SqlDbType.Int);
                        //command.Parameters["@id"].Value = 0;
                        dataTable.Load(command.ExecuteReader());

                        generosList = Genders.SerializarGeneros(dataTable);

                    }
                    catch (Exception x)
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

            return generosList;
        }
        #endregion

        #region LOGIN
        public static bool getLogin(string email, string nombre, string device_id)
        {
            DataTable dataTable = new DataTable();
            LoginSalida login = new LoginSalida();

            // Obtenemos un array de bytes a partir de dicho mensaje
            byte[] mensajeBytesArray = Encoding.Default.GetBytes(email);

            // Instanciamos el algoritmo
            SHA1Managed algoritmoHash = new SHA1Managed();

            // Se podría haber instanciado de esta otra forma.
            // Si no proporcionas un valor al método Create
            // "SHA1CryptoServiceProvider" será usado como opción por defecto.
            // HashAlgorithm algoritmoHash = HashAlgorithm.Create("SHA512");

            // Obtenemos el código hash mediante el método ComputeHash
            // ComputeHash es un método sobrecargado.
            // Existe una versión con la cual poder procesar solamente una
            // subregión del array de bytes.
            byte[] codigoHashBytesArray = algoritmoHash.ComputeHash(mensajeBytesArray);
            Console.WriteLine(codigoHashBytesArray);
            bool result;
            string mensaje = BitConverter.ToString(codigoHashBytesArray);
            string hash = mensaje.Replace("-", string.Empty);
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        con.Open();

                        if (existeUsuario(email) == false)
                        {
                            SqlCommand command2 = new SqlCommand("sp_ins_usuario", con);
                            command2.CommandType = CommandType.StoredProcedure;
                            command2.Parameters.AddWithValue("@name", nombre);
                            command2.Parameters.AddWithValue("@email", email);
                            command2.ExecuteNonQuery();
                        }

                        int id_usuario = consultaIdUsuario(email);

                        if (existeSesion(id_usuario, device_id) == true) 
                        {
                            desloguear(hash, device_id);
                        }

                        SqlCommand command3 = new SqlCommand("sp_ins_sesion", con);
                        command3.CommandType = CommandType.StoredProcedure;
                        command3.Parameters.AddWithValue("@device_id", device_id);
                        command3.Parameters.AddWithValue("@user_id", id_usuario);
                        command3.Parameters.AddWithValue("@hash", hash);
                        command3.ExecuteNonQuery();
                        result = true;

                    }
                    catch (SqlException x)
                    {
                        result = false;
                        return result;
                    }
                    catch (Exception x)
                    {
                        result = false;
                        return result;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA                                     

            return result;

        }

        public static void agregarDispositivo(string email, string device_id)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        SqlCommand command5 = new SqlCommand("sp_ins_dispositivo", con);
                        command5.CommandType = CommandType.StoredProcedure;
                        command5.Parameters.AddWithValue("@device_id", device_id);
                        command5.Parameters.AddWithValue("@email", email);
                        con.Open();
                        command5.ExecuteNonQuery();

                    }
                    catch (Exception x)
                    {

                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
        }

        public static bool existeUsuario(string email)
        {
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
                        SqlCommand command = new SqlCommand("Select * from suitapp.dbo.users where email = @email", con);
                        command.Parameters.AddWithValue("@email", email);
                        SqlDataReader dataReader = command.ExecuteReader();
                        if (!dataReader.Read())
                        {
                            return false;
                        }
                    }
                    catch (Exception x)
                    {

                    }
                    finally
                    {

                        con.Close();

                    }
                }
            }
            return true;
        }

        public static bool existeSesion(int user_id, string device_id)
        {
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
                        SqlCommand command = new SqlCommand("Select * from suitapp.dbo.sessions where user_id = @user_id and device_id = @device_id and date_out is null", con);
                        command.Parameters.AddWithValue("@user_id", user_id);
                        command.Parameters.AddWithValue("@device_id", device_id);
                        SqlDataReader dataReader = command.ExecuteReader();
                        if (!dataReader.Read())
                        {
                            return false;
                        }
                    }
                    catch (Exception x)
                    {

                    }
                    finally
                    {

                        con.Close();

                    }
                }
            }
            return true;
        }

        public static DataTable existeDispositivo(string email, string device_id)
        {
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        //SE ABRE LA CONEXION 
                        con.Open();
                        SqlCommand command3 = new SqlCommand("Select * from Buscador_Servicios.dbo.dispositivos where email = @email and device_id = @device_id", con);
                        command3.Parameters.AddWithValue("@email", email);
                        command3.Parameters.AddWithValue("@device_id", device_id);

                        //con.Open();
                        dataTable.Load(command3.ExecuteReader());

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
            return dataTable;
        }

        public static bool desloguear(string email, string device_id)
        {
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
                        SqlCommand command = new SqlCommand("UPDATE suitapp.dbo.sessions set date_out = sysdatetime() where hash = @email and device_id = @device_id", con);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@device_id", device_id);
                        command.ExecuteNonQuery();

                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }
                }
            }
            return true;
        }

        public static void activarDispositivo(string email, string device_id)
        {
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
                        SqlCommand command = new SqlCommand("UPDATE Buscador_Servicios.dbo.dispositivos set activo = 1 where email = @email and device_id = @device_id", con);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@device_id", device_id);
                        command.ExecuteNonQuery();

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

        }

        #endregion

        #region IMAGENES

        public static bool agregarImagenes(ServiciosDetalle servicio, int id_servicio)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        int i = 0;
                        con.Open();
                        foreach (string fila in servicio.adjuntos)
                        {
                            if (servicio.adjuntos[i] != null)
                            {
                                SqlCommand command6 = new SqlCommand("ins_imagen", con);
                                command6.CommandType = CommandType.StoredProcedure;
                                command6.Parameters.AddWithValue("@imagen", servicio.adjuntos[i]);
                                command6.Parameters.AddWithValue("@id_servicio", id_servicio);
                                command6.ExecuteNonQuery();
                            }
                            i++;
                        }
                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                        command.Parameters.AddWithValue("@id_servicio", id_servicio);
                        command.ExecuteNonQuery();
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }

            return true;

        }
        #endregion

        #region USUARIO

        public static int consultaIdUsuario(string email) 
        {
            int id_usuario = 0;
            using (SqlConnection con = new SqlConnection(
               ConfigurationManager.ConnectionStrings["cs"]
               .ConnectionString))
            {

                {

                    try
                    {
                        con.Open();
                        SqlCommand command = new SqlCommand("Select * from suitapp.dbo.users where email = @email", con);
                        command.Parameters.AddWithValue("@email", email);
                        DataTable usuario = new DataTable();
                        usuario.Load(command.ExecuteReader());

                        
                        id_usuario = Convert.ToInt32(usuario.Rows[0]["user_id"]);
                        return id_usuario;
                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/
                        
                    }
                    finally
                    {

                        con.Close();
                        
                    }

                }
            }
            return id_usuario;
        }

        public static int consultaIdUsuarioPorHash(string hash)
        {
            int id_usuario = 0;
            using (SqlConnection con = new SqlConnection(
               ConfigurationManager.ConnectionStrings["cs"]
               .ConnectionString))
            {

                {

                    try
                    {
                        con.Open();
                        SqlCommand command = new SqlCommand("Select user_id from suitapp.dbo.sessions where hash = @hash and date_out is null", con);
                        command.Parameters.AddWithValue("@hash", hash);
                        DataTable usuario = new DataTable();
                        usuario.Load(command.ExecuteReader());

                        if (usuario.Columns.Contains("user_id"))
                        {
                            id_usuario = Convert.ToInt32(usuario.Rows[0]["user_id"]);
                        }
                        else {
                            id_usuario = 0;

                        }
                        
                        return id_usuario;
                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        /*   DataTable dataTable = new DataTable();              

                           SqlCommand command = new SqlCommand("DELETE from Buscador_Servicios.dbo.servicios where id_servicio = @id_servicio", con);
                           command.Parameters.AddWithValue("@id_servicio", id_servicio);
                           command.ExecuteNonQuery();*/

                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            return id_usuario;
        }
        #endregion
    }// Fin de la clase.
}

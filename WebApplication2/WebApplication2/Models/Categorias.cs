
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("[categories]")]
    public class Categorias 
    {

        [Key, Column("category_id")]
        public int idCategory { get; set; }
        [Column("name")]
        public String nameCategory { get; set; }
        public int visits { get; set; }


        public static List<Categorias> SerializarCategorias(DataTable dataTable)
        {


            List<Categorias> categoriasList = new List<Categorias>();
            //List<Object> categoriasList = new List<System.Object>();
            int i = 0;
            foreach (DataRow fila in dataTable.Rows)
            {
                
                    Categorias cat = new Categorias();

                    cat.idCategory = Convert.ToInt16(dataTable.Rows[i]["category_id"]);
                    cat.nameCategory = dataTable.Rows[i]["name"].ToString().Trim();
                    cat.visits = Convert.ToInt16(dataTable.Rows[i]["visits"]);
                                        
                    i++;
                    categoriasList.Add(cat);
                
            }

            return categoriasList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("sizes")]
    public class Talles
    {

        [Key, Column("size_id")]
        public int sizeId { get; set; }
        [Column("name")]
        public string sizeName { get; set; }
        

        public static List<Talles> SerializarTalles(DataTable dataTable)
        {


            List<Talles> tallesList = new List<Talles>();
            //List<Object> categoriasList = new List<System.Object>();
            int i = 0;
            foreach (DataRow fila in dataTable.Rows)
            {

                Talles talle = new Talles();

                talle.sizeId = Convert.ToInt16(dataTable.Rows[i]["size_id"]);
                talle.sizeName = dataTable.Rows[i]["name"].ToString().Trim();
                
                i++;
                tallesList.Add(talle);

            }

            return tallesList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("colours")]
    public class Colores
    {

        [Key, Column("colour_id")]
        public int colourId { get; set; }
        [Column("name")]
        public string colourName { get; set; }
        [Column("hex_code")]
        public string colourHexCode { get; set; }

        public static List<Colores> SerializarColores(DataTable dataTable)
        {


            List<Colores> coloresList = new List<Colores>();
            //List<Object> categoriasList = new List<System.Object>();
            int i = 0;
            foreach (DataRow fila in dataTable.Rows)
            {

                Colores color = new Colores();

                color.colourId = Convert.ToInt16(dataTable.Rows[i]["colour_id"]);
                color.colourName = dataTable.Rows[i]["name"].ToString().Trim();
                color.colourHexCode = dataTable.Rows[i]["hex_code"].ToString().Trim();


                i++;
                coloresList.Add(color);

            }

            return coloresList;
        }
    }
}
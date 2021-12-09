
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("stores_shipments")]
    public class ShippingPrice
    {

        [Key, Column("store_id")]
        public int? idStore { get; set; }
        [Key, Column("province_id")]
        public int? idProvince { get; set; }
        [Column("price")]
        public float? price { get; set; }
        public string? nameProvince { get; set; }

        public static List<Genders> SerializarGeneros(DataTable dataTable)
        {


            List<Genders> generosList = new List<Genders>();
            //List<Object> categoriasList = new List<System.Object>();
            int i = 0;
            foreach (DataRow fila in dataTable.Rows)
            {

                Genders gender = new Genders();

                gender.idGender = Convert.ToInt16(dataTable.Rows[i]["gender_id"]);
                gender.nameGender = dataTable.Rows[i]["name"].ToString().Trim();
                

                i++;
                generosList.Add(gender);

            }

            return generosList;
        }
    }
}
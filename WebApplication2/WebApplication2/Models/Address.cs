
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("[address]")]
    public class Address
    {
        public string email { get; set; }

        [Key, Column("address_id")]
        public int? address_id { get; set; }
        [Column("street")]
        public String street { get; set; }

        [Key, Column("street_number")]
        public int street_number { get; set; }
        [Key, Column("postal_code")]
        public int? postal_code { get; set; }
        [Column("description")]
        public String? description { get; set; }
        [Key, Column("province_id")]
        public int? province_id { get; set; }
        [Key, Column("user_id")]
        public int user_id { get; set; }
        [Column("city")]
        public String? city { get; set; }
        [Column("floor")]
        public String? floor { get; set; }
        [Column("aparment")]
        public String? apartment { get; set; }
        //[Column("shipping_price")]
        //public float? shipping_price { get; set; }
    

        public static List<Address> serializarAddress(DataTable dataTable)
        {
            List<Address> addressList = new List<Address>();
            try
            {
                
                //List<Object> categoriasList = new List<System.Object>();
                int i = 0;
                foreach (DataRow fila in dataTable.Rows)
                {

                    Address addres = new Address();

                    addres.address_id = Convert.ToInt16(dataTable.Rows[i]["address_id"]);
                    addres.street = dataTable.Rows[i]["street"].ToString().Trim();
                    addres.street_number = Convert.ToInt16(dataTable.Rows[i]["street_number"]);
                    addres.postal_code = Convert.ToInt16(dataTable.Rows[i]["postal_code"]);
                    addres.description = dataTable.Rows[i]["description"].ToString().Trim();
                    addres.province_id = Convert.ToInt16(dataTable.Rows[i]["province_id"]);
                    addres.user_id = Convert.ToInt16(dataTable.Rows[i]["user_id"]);
                    addres.city = dataTable.Rows[i]["city"].ToString().Trim();
                    addres.floor = dataTable.Rows[i]["floor"].ToString().Trim();
                    addres.apartment = dataTable.Rows[i]["aparment"].ToString().Trim();
                    //addres.shipping_price = Convert.ToSingle(dataTable.Rows[i]["shipping_price"]);


                    i++;
                    addressList.Add(addres);

                }
            }
           
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
            
            return addressList;
            }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("servicios")]
    public class Servicios
    {
        [Key, Column("id_servicio")]
        public int code { get; set; }
        [Column("nombre")]
        public String nombre { get; set; }
        [Column("descripcion")]
        public String descripcion { get; set; }
        public string email;
        [Column("id_categoria")]
        public int subcategoria;
        [Column("direccion")]
        public String addres { get; set; }
        [Column("unidad")]
        public String unidad { get; set; }
        [Column("precio")]
        public float precio { get; set; }
        /*   [Column("id_categoria_padre")]
           public int id_categoria_padre { get; set; }*/
        [Column("imagen_account")]
        public String fotoPerfil { get; set; }
        [Column("imagen_avatar")]
        public String fotoPortada { get; set; }

        public string fotoPerfilMini { get; set; }
        /*[id_servicio]
        [nombre]
        [descripcion]
        [id_usuario]
        [id_categoria]
        [direccion]
        [unidad]
        [precio]
        [imagen_account]
        [imagen_avatar])*/

    }
}
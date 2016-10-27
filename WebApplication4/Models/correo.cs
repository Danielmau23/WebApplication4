using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Models
{
    public class correo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int idPlantilla { get; set; }

        [UIHint("String")]
        [Display(Name = "Para:")]
        [DataType(DataType.Text)]
        public string para { get; set; }

        [UIHint("String")]
        [Display(Name = "Asunto:")]
        [DataType(DataType.Text)]
        public string asunto { get; set; }

        [UIHint("String")]
        [Display(Name = "Mensaje:")]
        [DataType(DataType.Text)]
        public string mensaje { get; set; }


        [UIHint("String")]
        [Display(Name = "Nombre Emisor")]
        [DataType(DataType.Text)]
        public string nombre { get; set; }

        [UIHint("String")]
        [Display(Name = "Correo")]
        [DataType(DataType.Text)]
        public string correo2 { get; set; }

        [UIHint("String")]
        [Display(Name = "contraseña")]
        [DataType(DataType.Text)]
        public string contrasena { get; set; }

        [UIHint("String")]
        [Display(Name = "HTML")]
        [DataType(DataType.Text)]
        public string html { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace WebApplication4.Models
{
    public class plantillas
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int idPlantilla { get; set; }


        [UIHint("String")]
        [Display(Name = "Mensaje")]
        [DataType(DataType.Text)]
        public string mensaje { get; set; }

        [UIHint("String")]
        [Display(Name = "HTML")]
        [DataType(DataType.Text)]
        public string html { get; set; }
    }
}
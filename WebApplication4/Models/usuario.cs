using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class usuario
    {
        private string nombre;
        private string correo;
        private string pas;

        public usuario(string n, string c, string p)
        {
            nombre = n;
            correo = c;
            pas = p;
        }

        public string getNom()
        {
            return nombre;
        }
        public string getCor()
        {
            return correo;
        }
        public string getPas()
        {
            return pas;
        }


    }
}
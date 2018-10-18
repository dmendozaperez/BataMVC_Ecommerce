using BataEcommerce.Util;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BataEcommerce.Web.Models
{
    public class LoginViewModel
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public bool Recordar { get; set; }
        public string Correo { get; set; }
    }

    public class ResponseViewModel
    {
        public string Tipo { get; set; }
        public string Mensaje { get; set; }
        public string Token { get; set; }
        public IEnumerable<object> Lista { get; set; }
        public object Modelo { get; set; }
        public ResponseViewModel() {
            Tipo = Configuration.SI_MSJ_TIP_DEFECTO;
            Mensaje = Configuration.SI_MSJ_DES_DEFECTO;
            Token = Configuration.SI_TOKEN_DEFECTO;
            Lista = null;
        }
    }




}
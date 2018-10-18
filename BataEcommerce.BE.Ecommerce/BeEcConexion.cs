using System;

namespace BataEcommerce.BE.Ecommerce
{
   public class BeEcConexion
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Url { get; set; }
        public string BaseDatos { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public Nullable<bool> Trusted_Connection { get; set; }
        public string Estado{ get; set; }
        public DateTime Fecha_Crea { get; set; }
        public DateTime Fecha_Modif { get; set; }

    }
}


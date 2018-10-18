using BataEcommerce.BE.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BE.Ecommerce
{
    public class BeEcFuncion
    {
        public BeEcFuncion()
        {
            fun_sistema = new BeEcSistema();
        }
        public decimal fun_id { get; set; }
        public string fun_nombre { get; set; }
        public string fun_descripcion { get; set; }
        public decimal fun_orden { get; set; }
        public decimal fun_padre { get; set; }
        public decimal fun_system { get; set; }
        public BeEcSistema fun_sistema { get; set; }
    }    
}

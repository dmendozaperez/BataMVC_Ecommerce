using System;

namespace BataEcommerce.BE.Ecommerce
{
    public class BeEcMenu
    {
        public Int32 fun_id { get; set; }
        public string fun_nombre { get; set; }
        public string fun_descripcion { get; set; }
        public Int32 fun_padre { get; set; }
        public string apl_url { get; set; }
        public string apl_comentario { get; set; }
        public Int32 fun_orden { get; set; }
        public Int32 apl_id { get; set; }

        public string apl_controller { get; set; }
        public string apl_action { get; set; }
    }
}

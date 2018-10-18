namespace BataEcommerce.BE.Prestashop
{
    public class BePsOrderState
    {
        public int Id { get; set; }
        public string Referencia { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public bool Invoice { get; set; }
        public bool SendEmail { get; set; }
    }
}

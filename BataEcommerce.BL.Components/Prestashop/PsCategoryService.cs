using System.Collections.Generic;
using BataEcommerce.BE.Prestashop;
using BataEcommerce.DA.Prestashop;

namespace BataEcommerce.BL.Components.Prestashop
{
    public class PsCategoryService
    {
        private PsCategoryDAO psCategoryDAO;

        public PsCategoryService()
        {
            psCategoryDAO = new PsCategoryDAO();
        }

        public IEnumerable<BePsCategory> Buscar(BePsCategory objeto, ref string mensaje, ref string tipo)
        {
            return psCategoryDAO.Buscar(objeto, ref mensaje, ref tipo);
        }
        
    }
}

using System.Collections.Generic;
using BataEcommerce.BE.Prestashop;
using BataEcommerce.DA.Prestashop;

namespace BataEcommerce.BL.Components.Prestashop
{
    public class PsOrderStateService
    {
        private PsOrderStateDAO psOrderStateDAO;

        public PsOrderStateService()
        {
            psOrderStateDAO = new PsOrderStateDAO();
        }

        public IEnumerable<BePsOrderState> Buscar(BePsOrderState objeto, ref string mensaje, ref string tipo)
        {
            return psOrderStateDAO.Buscar(objeto, ref mensaje, ref tipo);
        }

    }
}

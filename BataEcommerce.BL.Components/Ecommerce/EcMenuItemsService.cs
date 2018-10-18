using BataEcommerce.BE.Ecommerce;
using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcMenuItemsService
    {
        private EcMenuItemsDAO ecMenuItemsDAO;
        public EcMenuItemsService()
        {
            ecMenuItemsDAO = new EcMenuItemsDAO();
        }
        public IEnumerable<BeEcMenuItem> navbarItems(Decimal _bas_id)
        {
            return ecMenuItemsDAO.navbarItems(_bas_id);
        }
    }
}

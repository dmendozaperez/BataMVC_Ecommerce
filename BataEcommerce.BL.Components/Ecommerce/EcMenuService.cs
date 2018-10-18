using BataEcommerce.BE.Ecommerce;
using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcMenuService
    {
        private EcMenuDAO ecMenuDAO;
        public EcMenuService()
        {
            ecMenuDAO = new EcMenuDAO();
        }
        public List<BeEcMenu> Menu_Acceso(Decimal _bas_id)
        {
            return ecMenuDAO.Menu_Acceso(_bas_id);
        }
    }
}

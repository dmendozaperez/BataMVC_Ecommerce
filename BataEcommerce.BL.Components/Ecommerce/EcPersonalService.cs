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
    public class EcPersonalService
    {
        private EcPersonalDAO ecPersonalDAO;
        public EcPersonalService()
        {
            ecPersonalDAO = new EcPersonalDAO();
        }

        public List<BeEcPersonal> get_lista()
        {
            return ecPersonalDAO.get_lista();
        }

    }
}

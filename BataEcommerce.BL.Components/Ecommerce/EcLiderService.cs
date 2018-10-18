using BataEcommerce.BE.Ecommerce;
using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcLiderService
    {
        private EcLiderDAO ecLiderDAO;
        public EcLiderService()
        {
            ecLiderDAO = new EcLiderDAO();
        }
        public List<BeEcLider> _leer_lider()
        {
            return ecLiderDAO._leer_lider();
        }
    }
}

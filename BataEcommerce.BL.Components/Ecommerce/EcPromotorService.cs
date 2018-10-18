using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcPromotorService
    {
        private EcPromotorDAO ecPromotorDAO;

        public EcPromotorService()
        {
            ecPromotorDAO = new EcPromotorDAO();
        }

        public string updateCoord(string _customerId, string _area, string _status)
        {
            return ecPromotorDAO.updateCoord(_customerId, _area, _status);
        }
        public DataTable get_dtcliente(string _documento)
        {
            return ecPromotorDAO.get_dtcliente(_documento);
        }

    }
}

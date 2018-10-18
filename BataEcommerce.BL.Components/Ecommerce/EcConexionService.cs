using System;
using System.Collections.Generic;
using BataEcommerce.DA.Ecommerce;
using BataEcommerce.BE.Ecommerce;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcConexionService
    {
        private EcConexionDAO ecConexionDAO;

        public EcConexionService()
        {
            this.ecConexionDAO = new EcConexionDAO();
        }

        public BeEcConexion Buscar(BeEcConexion objeto, ref string mensaje, ref string tipo)
        {
            return ecConexionDAO.Buscar(objeto, ref mensaje, ref tipo);
        }


    }
}

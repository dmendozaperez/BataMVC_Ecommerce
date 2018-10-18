using System;
using System.Collections.Generic;
using BataEcommerce.DA.Ecommerce;
using BataEcommerce.BE.Ecommerce;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcSistemaService
    {
        private EcSistemaDAO ecSistemaDAO;

        public EcSistemaService()
        {
            ecSistemaDAO = new EcSistemaDAO();
        }

        public List<BeEcSistema> get_lista(ref string mensaje, ref string tipo)
        {
            return ecSistemaDAO.get_lista(ref mensaje, ref tipo);
        }

    }
}

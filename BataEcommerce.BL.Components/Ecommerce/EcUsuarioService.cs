using System;
using System.Collections.Generic;
using BataEcommerce.DA.Ecommerce;
using BataEcommerce.BE.Ecommerce;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcUsuarioService
    {
        private EcUsuarioDAO ecUsuarioDAO;

        public EcUsuarioService()
        {
            ecUsuarioDAO = new EcUsuarioDAO();
        }

        public IEnumerable<BeEcUsuario> Buscar(BeEcUsuario objeto, ref string mensaje, ref string tipo)
        {
            return ecUsuarioDAO.Buscar(objeto, ref mensaje, ref tipo);
        }

        public Boolean UpdateUsuario(BeEcUsuario obj)
        {
            return ecUsuarioDAO.UpdateUsuario(obj);
        }
        public List<BeEcUsuario> GetUserByName(BeEcBasico objeto, ref string mensaje, ref string tipo)
        {
            return ecUsuarioDAO.GetUserByName(objeto, ref mensaje, ref tipo);
        }

        public List<BeEcUsuario> ConsultarUsuario(BeEcUsuario objeto, ref string mensaje, ref string tipo)
        {
            return ecUsuarioDAO.ConsultarUsuario(objeto, ref mensaje, ref tipo);
        }

    }
}

using BataEcommerce.BE.Ecommerce;
using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcRolService
    {
        private EcRolDAO ecRolDAO;
        public EcRolService() {
            ecRolDAO = new EcRolDAO();
        }
        public Boolean InsertarRoles(BeEcRol obj, ref string mensaje, ref string tipo)
        {
            return ecRolDAO.InsertarRoles(obj, ref mensaje, ref tipo);
        }
        public List<BeEcRol> get_lista()
        {
            return ecRolDAO.get_lista();
        }
        public Boolean EditarRoles(BeEcRol obj, ref string mensaje, ref string tipo)
        {
            return ecRolDAO.EditarRoles(obj, ref mensaje, ref tipo);
        }

        public List<BeEcRol> ConsultarRoles(BeEcRol obj, ref string mensaje, ref string tipo)
        {
            return ecRolDAO.ConsultarRoles(obj, ref mensaje, ref tipo);
        }
    }
}

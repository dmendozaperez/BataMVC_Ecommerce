using BataEcommerce.BE.Ecommerce;
using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcRolFuncionService
    {
        private EcRolFuncionDAO ecRolFuncionDAO;
        public EcRolFuncionService()
        {
            ecRolFuncionDAO = new EcRolFuncionDAO();
        }
        public Boolean Eliminar_Fun_Roles(Decimal fun_id, Decimal rol_id, ref string mensaje, ref string tipo)
        {
            return ecRolFuncionDAO.Eliminar_Fun_Roles(fun_id, rol_id, ref mensaje, ref tipo);
        }

        public Boolean Insertar_Fun_Roles(Decimal fun_id, Decimal rol_id, ref string mensaje, ref string tipo)
        {
            return ecRolFuncionDAO.Insertar_Fun_Roles(fun_id, rol_id, ref mensaje, ref tipo);
        }


        public List<BeEcFuncion> get_lista(BeEcRol obj, ref string mensaje, ref string tipo)
        {
            return ecRolFuncionDAO.get_lista(obj, ref mensaje, ref tipo);
        }
    }
}

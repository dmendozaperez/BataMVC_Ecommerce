using BataEcommerce.BE.Ecommerce;
using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcFuncionAplicacionService
    {
        private EcFuncionAplicacionDAO ecFuncionAplicacionDAO;
        public EcFuncionAplicacionService()
        {
            ecFuncionAplicacionDAO = new EcFuncionAplicacionDAO();
        }
        public Boolean Eliminar_App_Funcion(Decimal fun_id, Decimal apl_id, ref string mensaje, ref string tipo)
        {
            return ecFuncionAplicacionDAO.Eliminar_App_Funcion(fun_id, apl_id, ref mensaje, ref tipo);
        }

        public Boolean Insertar_App_Funcion(Decimal fun_id, Decimal apl_id, ref string mensaje, ref string tipo)
        {
            return ecFuncionAplicacionDAO.Insertar_App_Funcion(fun_id, apl_id, ref mensaje, ref tipo);
        }


        public List<BeEcAplicacion> get_lista(BeEcFuncion obj, ref string mensaje, ref string tipo)
        {
            return ecFuncionAplicacionDAO.get_lista(obj, ref mensaje, ref tipo);
        }
    }
}

using BataEcommerce.BE.Ecommerce;
using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcFuncionService
    {
        private EcFuncionDAO ecFuncionDAO;
        public EcFuncionService()
        {
            ecFuncionDAO = new EcFuncionDAO();
        }
        public bool InsertarFuncion(BeEcFuncion obj, ref string mensaje, ref string tipo)
        {
            return ecFuncionDAO.InsertarFuncion(obj, ref mensaje, ref tipo);
        }
        public bool EditarFuncion(BeEcFuncion obj, ref string mensaje, ref string tipo)
        {
            return ecFuncionDAO.EditarFuncion(obj, ref mensaje, ref tipo);
        }
        public List<BeEcFuncion> get_lista(ref string mensaje, ref string tipo)
        {
            return ecFuncionDAO.get_lista(ref mensaje, ref tipo);
        }

        public List<BeEcFuncion> ConsultarFuncion(BeEcFuncion obj, ref string mensaje, ref string tipo)
        {
            return ecFuncionDAO.ConsultarFuncion(obj, ref mensaje, ref tipo);
        }

    }
}

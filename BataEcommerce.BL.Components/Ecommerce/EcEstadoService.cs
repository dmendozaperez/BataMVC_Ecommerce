using BataEcommerce.BE.Ecommerce;
using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcEstadoService
    {
        private EcEstadoDAO ecEstadoDAO;

        public EcEstadoService()
        {
            ecEstadoDAO = new EcEstadoDAO();
        }
        public List<BeEcEstado> _LeerEstado(Decimal _modulo, ref string mensaje, ref string tipo)
        {
            return ecEstadoDAO._LeerEstado(_modulo, ref mensaje, ref tipo);
        }
    }
}

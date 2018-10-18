using BataEcommerce.BE.Ecommerce;
using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcBasicoService
    {
        private EcBasicoDAO ecBasicoDAO;
        public EcBasicoService()
        {
            ecBasicoDAO = new EcBasicoDAO();
        }
        public void Ejecuta(ref List<BeEcTipoPersona> lstTipoPersona, ref List<BeEcTipoUsuario> lstTipoUsuario, ref List<BeEcTipoDocumento> lstTipoDocumento, ref List<BeEcDepartamento> lstDepartamento, ref List<BeEcProvincia> lstProvincia, ref List<BeEcDistrito> lstDistrito, ref string mensaje, ref string tipo)
        {
            ecBasicoDAO.Ejecuta(ref lstTipoPersona, ref lstTipoUsuario, ref lstTipoDocumento, ref lstDepartamento, ref lstProvincia, ref lstDistrito, ref mensaje, ref tipo);
        }

        public BeEcBasico Ejecuta(BeEcBasico obj, ref string mensaje, ref string tipo)
        {
            return ecBasicoDAO.Ejecuta(obj, ref mensaje, ref tipo);
        }

        public Boolean EditarBasicoUsuario(BeEcBasico obj, ref string mensaje, ref string tipo)
        {
            return ecBasicoDAO.EditarBasicoUsuario(obj, ref mensaje, ref tipo);
        }

        public Boolean InsertarBasicoUsuario(BeEcBasico obj, ref string mensaje, ref string tipo)
        {
            return ecBasicoDAO.InsertarBasicoUsuario(obj, ref mensaje, ref tipo);
        }


    }
}

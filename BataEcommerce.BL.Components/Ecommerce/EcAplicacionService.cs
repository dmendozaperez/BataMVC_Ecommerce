using BataEcommerce.BE.Ecommerce;
using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcAplicacionService
    {
        private EcAplicacionDAO ecAplicacionDAO;
        public EcAplicacionService()
        {
            ecAplicacionDAO = new EcAplicacionDAO();
        }

        public Boolean UpdateAplicacion(BeEcAplicacion obj, ref string mensaje, ref string tipo)
        {
            return ecAplicacionDAO.UpdateAplicacion(obj, ref mensaje, ref tipo);
        }


        public Boolean InsertarAplicacion(BeEcAplicacion obj, ref string mensaje, ref string tipo) {
            return ecAplicacionDAO.InsertarAplicacion(obj, ref mensaje, ref tipo);
        }

        public List<BeEcAplicacion> get_lista(ref string mensaje, ref string tipo)
        {
            return ecAplicacionDAO.get_lista(ref mensaje, ref tipo);
        }


        public List<BeEcAplicacion> ConsultarAplicacion(BeEcAplicacion obj, ref string mensaje, ref string tipo)
        {
            return ecAplicacionDAO.ConsultarAplicacion(obj, ref mensaje, ref tipo);
        }

        public List<BeEcTipoAplicacion> LeerAplicacionTipo(ref string mensaje, ref string tipo)
        {
            return ecAplicacionDAO.LeerAplicacionTipo(ref mensaje, ref tipo);
        }


    }
}

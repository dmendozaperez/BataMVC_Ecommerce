using BataEcommerce.BE.Ecommerce;
using BataEcommerce.DA.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BL.Components.Ecommerce
{
    public class EcUsurioRolService
    {
        private EcUsuarioRolDAO ecUsurioRolDAO;

        public EcUsurioRolService()
        {
            ecUsurioRolDAO = new EcUsuarioRolDAO();
        }
        public Boolean Eliminar_Rol_Usuario(Decimal _usu_rol_idusu, Decimal _usu_rol_idrol, ref string mensaje, ref string tipo)
        {
            return ecUsurioRolDAO.Eliminar_Rol_Usuario(_usu_rol_idusu, _usu_rol_idrol, ref mensaje, ref tipo);
        }
        public Boolean Insertar_Rol_Usuario(Decimal _usu_rol_idusu, Decimal _usu_rol_idrol, ref string mensaje, ref string tipo)
        {
            return ecUsurioRolDAO.Insertar_Rol_Usuario(_usu_rol_idusu, _usu_rol_idrol, ref mensaje, ref tipo);
        }
        public List<BeEcUsuarioRol> get_lista(BeEcUsuario obj, ref string mensaje, ref string tipo)
        {
            return ecUsurioRolDAO.get_lista(obj, ref mensaje, ref tipo);
        }

    }
}

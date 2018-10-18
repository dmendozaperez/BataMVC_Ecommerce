using BataEcommerce.BE.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BataEcommerce.Web.Helpers
{
    public static class WebUtil
    {
        public static bool AccesoMenu(List<BeEcMenu> lstMenu, string strAction, string StrController)
        {
            Boolean valida = false;
            try
            {
                //var existe = lstMenu.Where(t => t.apl_action == strAction && t.apl_controller == StrController).ToList();
                var existe = lstMenu.Where(t => t.apl_action.Contains(strAction) && t.apl_controller.Contains(StrController)).ToList();
                if (existe.Count > 0) valida = true;
            }
            catch
            {
                valida = false;
            }
            return valida;
        }
    }
}
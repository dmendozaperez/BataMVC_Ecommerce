using BataEcommerce.BE.Ecommerce;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BataEcommerce.Web.Helpers;

namespace BataEcommerce.Web.Filters
{
    public class FilterSecurity : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // ***
            // Validar Token Sessión
            HttpCookie cToken = filterContext.HttpContext.Request.Cookies.Get("Token");
            if (cToken == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", Controller = "App" }));
                return;
            }

            var sToken = filterContext.HttpContext.Session["Token"];
            if (sToken == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", Controller = "App" }));
                return;
            }

            if (cToken.Value == "" || (string)sToken == "" || cToken.Value != (string)sToken)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", Controller = "App" }));
                return;
            }
            // ***
            // Validar Permisos
            List<BeEcMenu> lstMenu = (List<BeEcMenu>)filterContext.HttpContext.Session["menu"];
            bool validar = WebUtil.AccesoMenu(lstMenu, filterContext.ActionDescriptor.ActionName, filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
            if (!validar)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Error403", Controller = "App" }));
            }


        }



    }
}
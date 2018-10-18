using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BataEcommerce.Util;
using BataEcommerce.Web.Filters;
using BataEcommerce.Web.Models;
using BataEcommerce.BL.Components.Ecommerce;
using BataEcommerce.BE.Ecommerce;
using System.Collections.Generic;
using BataEcommerce.BL.Components.Prestashop;
using BataEcommerce.BE.Prestashop;

namespace BataEcommerce.Web.Controllers
{
    public class AppController : Controller
    {
        public ActionResult Login()
        {
            // Limpiar
            LimpiarCache();

            ViewBag.Title = "Login";
            return View();
        }

        string random = "";
        EcUsuarioService ecUsuService = null;
        EcMenuService ecMenuService = null;
        EcEstadoService ecEstadoService = null;
        EcAplicacionService ecAplicacionService = null;
        EcFuncionService ecFuncionService = null;
        EcSistemaService ecSistemaService = null;
        EcRolService ecRolService = null;
        EcFuncionAplicacionService ecFuncionAplicacionService = null;
        EcRolFuncionService ecRolFuncionService = null;
        EcBasicoService ecBasicoService = null;
        EcUsurioRolService ecUsurioRolService = null;

        [HttpPost]
        public JsonResult Login(LoginViewModel model)
        {
            ecUsuService = new EcUsuarioService();
            ecMenuService = new EcMenuService();
            ResponseViewModel res = new ResponseViewModel();

            string mensaje = "";
            string tipo = "";

            try
            {
                if (!ModelState.IsValid)
                {
                    res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                    res.Mensaje = Configuration.SI_MSJ_MODEL_ERROR;
                    return Json(res);
                }

                BeEcUsuario usu = new BeEcUsuario();
                usu._usu_nombre = model.Usuario;
                usu._usu_password = Cryptographic.encrypt(model.Contrasena);

                List<BeEcUsuario> lst = ecUsuService.Buscar(usu, ref mensaje, ref tipo).ToList();

                if (lst.Count > 0)
                {
                    random = GeneraNumeroRandom();
                    Session["Token"] = random;
                    Session["Id"] = lst.First()._usu_id;
                    Session["Usuario"] = lst.First()._usu_nombre;
                    Session["Menu"] = ecMenuService.Menu_Acceso(lst.First()._usu_id).ToList();

                    Response.Cookies.Set(new HttpCookie("Token", (string)Session["Token"]));

                    res.Tipo = tipo;
                    res.Mensaje = mensaje;
                    res.Token = random;
                }
                else
                {
                    res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                    res.Mensaje = Configuration.SI_MSJ_ACC_ERROR;
                }
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [FilterSecurity]
        public ActionResult Panel(string sid)
        {
            ViewBag.Token = sid;
            ViewBag.Title = "Panel de Administración";
            return View(ViewBag);
        }

        #region Aplicacion
        public ActionResult AplicacionGeneral()
        {
            ViewBag.Title = "Mantenimiento de Aplicaciones";
            ViewBag.Subtitle = "Permite registrar, modificar aplicaciones del Sistema.";
            return View();
        }
        [HttpPost]
        public JsonResult BuscarEstadoModulo(string modid)
        {
            ecEstadoService = new EcEstadoService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                List<BeEcEstado> lst = ecEstadoService._LeerEstado(Convert.ToDecimal(modid), ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.Where(x => x._est_id != "E" && x._est_id != "P").OrderBy(x => x._est_des);
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult BuscarTipoAplicacion()
        {
            ecAplicacionService = new EcAplicacionService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                List<BeEcTipoAplicacion> lst = ecAplicacionService.LeerAplicacionTipo(ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.OrderBy(x => x.apl_tip_id);
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult BuscarAplicacion(BeEcAplicacion obj)
        {
            ecAplicacionService = new EcAplicacionService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                List<BeEcAplicacion> lst = ecAplicacionService.ConsultarAplicacion(obj, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.OrderBy(x => x.apl_id);
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult AgregarActualizarAplicacion(BeEcAplicacion obj)
        {
            ecAplicacionService = new EcAplicacionService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                if (obj.apl_id == 0)
                {
                    bool valida = ecAplicacionService.InsertarAplicacion(obj, ref mensaje, ref tipo);
                    res.Tipo = tipo;
                    res.Mensaje = mensaje;
                }
                else
                {
                    bool valida = ecAplicacionService.UpdateAplicacion(obj, ref mensaje, ref tipo);
                    res.Tipo = tipo;
                    res.Mensaje = mensaje;
                }
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        #endregion

        #region Funcion
        public ActionResult FuncionGeneral()
        {
            ViewBag.Title = "Mantenimiento de Funciones de Sistema";
            ViewBag.Subtitle = "Permite registrar, modificar, asignar aplicaciones a Funciones del Sistema.";
            return View();
        }
        [HttpPost]
        public JsonResult BuscarFuncion(BeEcFuncion obj)
        {
            ecFuncionService = new EcFuncionService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                List<BeEcFuncion> lst = ecFuncionService.ConsultarFuncion(obj, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.OrderBy(x => x.fun_id);
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult BuscarSistema()
        {
            ecSistemaService = new EcSistemaService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                List<BeEcSistema> lst = ecSistemaService.get_lista(ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.OrderBy(x => x.sis_id);
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult AgregarActualizarFuncion(BeEcFuncion obj)
        {
            ecFuncionService = new EcFuncionService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                if (obj.fun_id == 0)
                {
                    bool valida = ecFuncionService.InsertarFuncion(obj, ref mensaje, ref tipo);
                    res.Tipo = tipo;
                    res.Mensaje = mensaje;
                }
                else
                {
                    bool valida = ecFuncionService.EditarFuncion(obj, ref mensaje, ref tipo);
                    res.Tipo = tipo;
                    res.Mensaje = mensaje;
                }
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult BuscarFuncionAplicacion(BeEcFuncion obj)
        {
            ecFuncionAplicacionService = new EcFuncionAplicacionService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                List<BeEcAplicacion> lst = ecFuncionAplicacionService.get_lista(obj, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.OrderBy(x => x.apl_orden);
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult AgregarFuncionAplicacion(Decimal fun_id, Decimal apl_id)
        {
            ecFuncionAplicacionService = new EcFuncionAplicacionService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                bool valida = ecFuncionAplicacionService.Insertar_App_Funcion(fun_id, apl_id, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult EliminarFuncionAplicacion(Decimal fun_id, Decimal apl_id)
        {
            ecFuncionAplicacionService = new EcFuncionAplicacionService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                bool valida = ecFuncionAplicacionService.Eliminar_App_Funcion(fun_id, apl_id, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        #endregion

        #region Rol
        public ActionResult RolGeneral()
        {
            ViewBag.Title = "Mantenimiento de Roles de Sistema";
            ViewBag.Subtitle = "Permite registrar, modificar, asignar Roles del Sistema.";
            return View();
        }
        [HttpPost]
        public JsonResult BuscarRol(BeEcRol obj)
        {
            ecRolService = new EcRolService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                List<BeEcRol> lst = ecRolService.ConsultarRoles(obj, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.OrderBy(x => x.rol_id);
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult AgregarActualizarRol(BeEcRol obj)
        {
            ecRolService = new EcRolService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                if (obj.rol_id == 0)
                {
                    bool valida = ecRolService.InsertarRoles(obj, ref mensaje, ref tipo);
                    res.Tipo = tipo;
                    res.Mensaje = mensaje;
                }
                else
                {
                    bool valida = ecRolService.EditarRoles(obj, ref mensaje, ref tipo);
                    res.Tipo = tipo;
                    res.Mensaje = mensaje;
                }
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }

        /*----*/
        [HttpPost]
        public JsonResult BuscarRolFuncion(BeEcRol obj)
        {
            ecRolFuncionService = new EcRolFuncionService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                List<BeEcFuncion> lst = ecRolFuncionService.get_lista(obj, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.OrderBy(x => x.fun_orden);
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult AgregarRolFuncion(Decimal fun_id, Decimal rol_id)
        {
            ecRolFuncionService = new EcRolFuncionService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                bool valida = ecRolFuncionService.Insertar_Fun_Roles(fun_id, rol_id, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult EliminarRolFuncion(Decimal fun_id, Decimal rol_id)
        {
            ecRolFuncionService = new EcRolFuncionService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                bool valida = ecRolFuncionService.Eliminar_Fun_Roles(fun_id, rol_id, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }

        #endregion

        #region Usuario
        public ActionResult UsuarioGeneral()
        {
            ViewBag.Title = "Mantenimiento de Usuarios de Sistema";
            ViewBag.Subtitle = "Permite registrar, modificar, Usuarios del Sistema.";
            return View();
        }

        public ActionResult UsuarioMantenimiento()
        {
            ViewBag.Title = "Creación de un nuevo Usuario";
            ViewBag.Subtitle = "Permite registrar un nuevo Usuario del Sistema.";
            return View();
        }

        [HttpPost]
        public JsonResult BuscarBasicoDato(BeEcBasico obj)
        {
            ecBasicoService = new EcBasicoService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                obj = ecBasicoService.Ejecuta(obj, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Modelo = obj;
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult AgregarActualizarBasicoDato(BeEcBasico obj, string accion)
        {
            ecBasicoService = new EcBasicoService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            string s_usuid = Convert.ToString(Session["Id"]); // Id de Usuario    
            try
            {
                bool valida = false;
                switch (accion)
                {
                    // Actualizar Promotor
                    case Configuration.SI_APP_WIN_ACTPRO:
                        break;
                    // Crear Promotor
                    case Configuration.SI_APP_WIN_CREPRO:
                        obj.bas_cre_usuario = s_usuid;
                        obj.bas_are_id = "";
                        obj.bas_acceso = "1"; // No Promotor
                        obj.bas_lider = false;
                        obj.bas_agencia = "";
                        obj.bas_destino = "";
                        obj.bas_agencia_ruc = "";
                        obj.bas_contrasena = Cryptographic.encrypt(obj.bas_documento);
                        valida = ecBasicoService.InsertarBasicoUsuario(obj, ref mensaje, ref tipo);
                        break;
                    // Actualizar Cliente
                    case Configuration.SI_APP_WIN_ACTCLI:
                        obj.bas_mod_usuario = s_usuid;
                        valida = ecBasicoService.EditarBasicoUsuario(obj, ref mensaje, ref tipo);
                        break;
                    // Crear Cliente
                    case Configuration.SI_APP_WIN_CRECLI:
                        break;
                    default:
                        break;
                }

                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Modelo = obj;
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult BuscarUsuario(BeEcUsuario obj)
        {
            ecUsuService = new EcUsuarioService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                List<BeEcUsuario> lst = ecUsuService.ConsultarUsuario(obj, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.OrderBy(x => x._usu_id);
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }

        /*----*/
        [HttpPost]
        public JsonResult BuscarUsuarioRol(BeEcUsuario obj)
        {
            ecUsurioRolService = new EcUsurioRolService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                List<BeEcUsuarioRol> lst = ecUsurioRolService.get_lista(obj, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.OrderBy(x => x.rol_id);
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult AgregarUsuarioRol(Decimal usu_id, Decimal rol_id)
        {
            ecUsurioRolService = new EcUsurioRolService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                bool valida = ecUsurioRolService.Insertar_Rol_Usuario(usu_id, rol_id, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        [HttpPost]
        public JsonResult EliminarUsuarioRol(Decimal usu_id, Decimal rol_id)
        {
            ecUsurioRolService = new EcUsurioRolService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                bool valida = ecUsurioRolService.Eliminar_Rol_Usuario(usu_id, rol_id, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }


        #endregion

        #region Basico
        [HttpPost]
        public JsonResult BuscarBasico()
        {
            ecBasicoService = new EcBasicoService();
            ResponseViewModel res = new ResponseViewModel();
            string mensaje = "";
            string tipo = "";
            try
            {
                List<BeEcTipoPersona> lstTipoPersona = null;
                List<BeEcTipoUsuario> lstTipoUsuario = null;
                List<BeEcTipoDocumento> lstTipoDocumento = null;
                List<BeEcDepartamento> lstDepartamento = null;
                List<BeEcProvincia> lstProvincia = null;
                List<BeEcDistrito> lstDistrito = null;

                ecBasicoService.Ejecuta(ref lstTipoPersona, ref lstTipoUsuario, ref lstTipoDocumento, ref lstDepartamento, ref lstProvincia, ref lstDistrito, ref mensaje, ref tipo);
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = new List<object> { lstTipoPersona, lstTipoUsuario, lstTipoDocumento, lstDepartamento, lstProvincia, lstDistrito };
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }
        #endregion

        public ActionResult Error403()
        {
            ViewBag.Title = "Error 403 - Página Prohibida";
            return View();
        }

        #region Metodos Utiles
        public void LimpiarCache()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.Subtract(new TimeSpan(1, 0, 0)));
            Response.Cache.SetLastModified(DateTime.Now);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
        }
        private static Random rdm = new Random();
        private string GeneraNumeroRandom()
        {
            string text = "";
            var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            for (var i = 0; i < 10; i++)
                text += possible[(rdm.Next(possible.Length))];
            return text;
        }
        #endregion


        PsCategoryService psCategoryService = null;
        PsOrderStateService psOrderStateService = null;

        [HttpPost]
        public JsonResult BuscarPsOrderState(string ordnom)
        {
            psOrderStateService = new PsOrderStateService();
            ResponseViewModel res = new ResponseViewModel();

            string mensaje = "";
            string tipo = "";
            try
            {
                BePsOrderState ors = new BePsOrderState();
                ors.Nombre = ordnom;
                List<BePsOrderState> lst = psOrderStateService.Buscar(ors, ref mensaje, ref tipo).ToList();
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.OrderBy(x => x.Nombre);
            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }

        [HttpPost]
        public JsonResult BuscarPsCategory(string catnom)
        {
            psCategoryService = new PsCategoryService();
            ResponseViewModel res = new ResponseViewModel();

            string mensaje = "";
            string tipo = "";
            try
            {
                BePsCategory cat = new BePsCategory();
                cat.Nombre = catnom;
                List<BePsCategory> lst = psCategoryService.Buscar(cat, ref mensaje, ref tipo).ToList();
                res.Tipo = tipo;
                res.Mensaje = mensaje;
                res.Lista = lst.OrderBy(x => x.Nombre);

            }
            catch (Exception e)
            {
                res.Tipo = Configuration.SI_MSJ_TIP_ERROR;
                res.Mensaje = e.Message;
            }
            return Json(res);
        }


    }
}
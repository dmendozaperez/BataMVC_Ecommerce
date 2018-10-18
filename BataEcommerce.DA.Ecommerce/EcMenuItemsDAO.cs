using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System;
using System.Linq;

namespace BataEcommerce.DA.Ecommerce
{
    public class EcMenuItemsDAO
    {
        public IEnumerable<BeEcMenuItem> navbarItems(Decimal _bas_id)
        {

            EcMenuDAO menu_data = new EcMenuDAO();

            List<BeEcMenu> menu_acceso = menu_data.Menu_Acceso(_bas_id);

            /*select a padre de menu*/
            //var menu_padre = menu_acceso.Where(menupadre => menupadre.fun_id == menupadre.fun_padre);            

            var menu = new List<BeEcMenuItem>();

            //menu = null;

            if (menu_acceso != null)
            {
                //Int32 _id = 1;
                //Int32 _id_pad = 1;
                //recorre el padre de menu
                foreach (BeEcMenu app_padre in menu_acceso.Where(menupadre => menupadre.fun_id == menupadre.fun_padre))
                {
                    var menu_padre = menu_acceso.Where(menuv => menuv.fun_padre == app_padre.fun_id);
                    if (menu_padre.Count() != 1)
                    {
                        menu.Add(new BeEcMenuItem { Id = app_padre.fun_id, nameOption = app_padre.fun_nombre, controller = "Home", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = true, parentId = app_padre.fun_padre, activeli = "submenu" });
                    }
                    //{ 
                    //     menu.Add(new Navbar { Id = app_padre.fun_id, nameOption = app_padre.fun_nombre, controller = "Home", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = true, parentId = app_padre.fun_padre, activeli = "submenu" });
                    //}
                    //_id += 1;
                    //en este caso vamos a ver el submenu del menu principal
                    //var sub_menu_padre = menu_acceso.Where(submenupadre => submenupadre.fun_padre == app_padre.fun_id && submenupadre.fun_id == 0);

                    foreach (BeEcMenu app_sub_padre in menu_acceso.Where(submenupadre => submenupadre.fun_padre == app_padre.fun_id && submenupadre.fun_padre != submenupadre.fun_id))
                    {
                        if (app_sub_padre.fun_id == 0)
                        {
                            menu.Add(new BeEcMenuItem { Id = app_sub_padre.fun_id, nameOption = app_sub_padre.fun_nombre, controller = app_sub_padre.apl_controller, action = app_sub_padre.apl_action, imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = false, parentId = app_sub_padre.fun_padre });
                        }
                        else
                        {
                            menu.Add(new BeEcMenuItem { Id = app_sub_padre.fun_id, nameOption = app_sub_padre.fun_nombre, controller = "Home", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = true, parentId = app_sub_padre.fun_padre, activeli = "submenu" });

                            /*sumenu nivel 2 del menu*/
                            foreach (BeEcMenu app_sub_menu in menu_acceso.Where(submenu => submenu.fun_padre == app_sub_padre.fun_id))
                            {
                                menu.Add(new BeEcMenuItem { Id = app_sub_menu.fun_id, nameOption = app_sub_menu.fun_nombre, controller = app_sub_menu.apl_controller, action = app_sub_menu.apl_action, imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = false, parentId = app_sub_menu.fun_padre });
                            }

                        }
                        //_id += 1;
                    }
                    //_id += 1;
                    //_id_pad += 1;

                }

            }

            return menu;
        }
    }
}

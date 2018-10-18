using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System;


namespace BataEcommerce.DA.Ecommerce
{
    public class EcMenuDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);

        public List<BeEcMenu> Menu_Acceso(Decimal _bas_id)
        {
            DataTable dt = null;
            List<BeEcMenu> menu = null;
            try
            {
                dt = dt_menu(_bas_id);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        menu = new List<BeEcMenu>();
                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                        {
                            BeEcMenu items = new BeEcMenu
                            {
                                fun_id = Convert.ToInt32(dt.Rows[i]["fun_id"]),
                                fun_nombre = dt.Rows[i]["fun_nombre"].ToString(),
                                fun_descripcion = dt.Rows[i]["fun_descripcion"].ToString(),
                                fun_padre = Convert.ToInt32(dt.Rows[i]["fun_padre"]),
                                apl_url = dt.Rows[i]["apl_url"].ToString(),
                                apl_comentario = dt.Rows[i]["apl_comentario"].ToString(),
                                fun_orden = Convert.ToInt32(dt.Rows[i]["fun_orden"]),
                                apl_id = Convert.ToInt32(dt.Rows[i]["apl_id"]),
                                apl_controller = dt.Rows[i]["apl_controller"].ToString(),
                                apl_action = dt.Rows[i]["apl_action"].ToString(),
                            };
                            menu.Add(items);
                        }

                    }
                }
            }
            catch
            {
                menu = null;
            }
            return menu;
        }
        private DataTable dt_menu(Decimal _bas_id)
        {
            string sqlquery = "[USP_Leer_Funcion_Arbol]";
            DataTable dt = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bas_id", _bas_id);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch
            {

            }
            return dt;
        }

    }
}

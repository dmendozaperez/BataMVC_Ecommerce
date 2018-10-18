using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System;

namespace BataEcommerce.DA.Ecommerce
{
    public class EcEstadoDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);
        public List<BeEcEstado> _LeerEstado(Decimal _modulo, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Leer_EstadoModuloTotal";
            List<BeEcEstado> data_select = new List<BeEcEstado>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@est_mod_id", _modulo);
                        SqlDataReader sqlread = cmd.ExecuteReader();

                        if (sqlread.HasRows)
                        {
                            while (sqlread.Read())
                            {
                                BeEcEstado item = new BeEcEstado();
                                item._est_id = sqlread["Est_Id"].ToString();
                                item._est_des = sqlread["Est_Descripcion"].ToString();
                                data_select.Add(item);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                data_select = null;
                mensaje = Configuration.SI_MSJ_DES_ERROR + "\n\n" + "Detalle de Error: " + e.Message;
                tipo = Configuration.SI_MSJ_TIP_ERROR;
            }
            mensaje = Configuration.SI_MSJ_TIP_EXITO;
            tipo = Configuration.SI_MSJ_TIP_EXITO;
            return data_select;
        }
    }
}

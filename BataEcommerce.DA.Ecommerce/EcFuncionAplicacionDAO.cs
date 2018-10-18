using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System;


namespace BataEcommerce.DA.Ecommerce
{
    public class EcFuncionAplicacionDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);
        public Boolean Eliminar_App_Funcion(Decimal fun_id, Decimal apl_id, ref string mensaje, ref string tipo)
        {
            Boolean valida = false;
            string sqlquery = "USP_Borrar_Apl_Fun";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0; cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@apl_fun_aplid", apl_id);
                        cmd.Parameters.AddWithValue("@apl_fun_funid", fun_id);
                        cmd.ExecuteNonQuery();
                        valida = true;
                    }
                }
            }
            catch (Exception e)
            {
                mensaje = Configuration.SI_MSJ_DES_ERROR + "\n\n" + "Detalle de Error: " + e.Message;
                tipo = Configuration.SI_MSJ_TIP_ERROR;
                valida = false;
            }
            mensaje = Configuration.SI_MSJ_DES_EXITO;
            tipo = Configuration.SI_MSJ_TIP_EXITO;
            return valida;
        }

        public Boolean Insertar_App_Funcion(Decimal fun_id, Decimal apl_id, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Insertar_Apl_Fun";
            Boolean valida = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@apl_fun_aplid", apl_id);
                        cmd.Parameters.AddWithValue("@apl_fun_funid", fun_id);
                        cmd.ExecuteNonQuery();
                        valida = true;
                    }
                }
            }
            catch (Exception e)
            {
                mensaje = Configuration.SI_MSJ_DES_ERROR + "\n\n" + "Detalle de Error: " + e.Message;
                tipo = Configuration.SI_MSJ_TIP_ERROR;
                valida = false;
            }
            mensaje = Configuration.SI_MSJ_DES_EXITO;
            tipo = Configuration.SI_MSJ_TIP_EXITO;
            return valida;
        }

        public List<BeEcAplicacion> get_lista(BeEcFuncion obj, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Leer_Apl_Fun";
            List<BeEcAplicacion> list = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Fun_Id", obj.fun_id);
                        SqlDataReader dr = cmd.ExecuteReader();
                        list = new List<BeEcAplicacion>();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                int posId = dr.GetOrdinal("apl_id");
                                int posNombre = dr.GetOrdinal("apl_nombre");
                                int posOrden = dr.GetOrdinal("apl_orden");
                                BeEcAplicacion fila = new BeEcAplicacion();
                                fila.apl_id = dr.GetDecimal(posId);
                                fila.apl_nombre = dr.GetString(posNombre);
                                fila.apl_orden = dr.GetDecimal(posOrden);
                                list.Add(fila);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                mensaje = Configuration.SI_MSJ_DES_ERROR + "\n\n" + "Detalle de Error: " + e.Message;
                tipo = Configuration.SI_MSJ_TIP_ERROR;
                list = null;
            }
            mensaje = Configuration.SI_MSJ_DES_EXITO;
            tipo = Configuration.SI_MSJ_TIP_EXITO;
            return list;
        }

    }
}

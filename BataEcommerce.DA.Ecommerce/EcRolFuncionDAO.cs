using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System;

namespace BataEcommerce.DA.Ecommerce
{
    public class EcRolFuncionDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);

        public Boolean Eliminar_Fun_Roles(Decimal fun_id, decimal rol_id, ref string mensaje, ref string tipo)
        {
            Boolean valida = false;
            string sqlquery = "USP_Borrar_Roles_Funcion";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0; cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@rol_fun_rolid", rol_id);
                        cmd.Parameters.AddWithValue("@rol_fun_funid", fun_id);
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
        public Boolean Insertar_Fun_Roles(Decimal _fun_id, decimal _rol_id, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Insertar_Roles_Funcion";
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
                        cmd.Parameters.AddWithValue("@rol_fun_rolid", _rol_id);
                        cmd.Parameters.AddWithValue("@rol_fun_funid", _fun_id);
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
        public List<BeEcFuncion> get_lista(BeEcRol obj, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Leer_Funcion_Roles";
            List<BeEcFuncion> list = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@rol_id", obj.rol_id);
                        SqlDataReader dr = cmd.ExecuteReader();
                        list = new List<BeEcFuncion>();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                int posId = dr.GetOrdinal("fun_id");
                                int posNombre = dr.GetOrdinal("fun_nombre");
                                int posOrden = dr.GetOrdinal("fun_orden");

                                BeEcFuncion fila = new BeEcFuncion();
                                fila.fun_id = dr.GetDecimal(posId);
                                fila.fun_nombre = dr.GetString(posNombre);
                                fila.fun_orden = dr.GetDecimal(posOrden);
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

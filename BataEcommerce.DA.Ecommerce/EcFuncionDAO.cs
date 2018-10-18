using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System;


namespace BataEcommerce.DA.Ecommerce
{
    public class EcFuncionDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);
        public Boolean InsertarFuncion(BeEcFuncion obj, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Insertar_Funcion";
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
                        cmd.Parameters.AddWithValue("@fun_id", obj.fun_id);
                        cmd.Parameters.AddWithValue("@fun_nombre", obj.fun_nombre);
                        cmd.Parameters.AddWithValue("@fun_descripcion", obj.fun_descripcion);
                        cmd.Parameters.AddWithValue("@fun_orden", obj.fun_orden);
                        cmd.Parameters.AddWithValue("@fun_padre", obj.fun_padre);
                        cmd.Parameters.AddWithValue("@fun_sisid", obj.fun_system);
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

        public Boolean EditarFuncion(BeEcFuncion obj, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Modificar_Funcion";
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
                        cmd.Parameters.AddWithValue("@fun_id", obj.fun_id);
                        cmd.Parameters.AddWithValue("@fun_nombre", obj.fun_nombre);
                        cmd.Parameters.AddWithValue("@fun_descripcion", obj.fun_descripcion);
                        cmd.Parameters.AddWithValue("@fun_orden", obj.fun_orden);
                        cmd.Parameters.AddWithValue("@fun_padre", obj.fun_padre);
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

        public List<BeEcFuncion> get_lista(ref string mensaje, ref string tipo)
        {

            string sqlquery = "USP_Leer_Funcion_Sistema";
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
                        SqlDataReader dr = cmd.ExecuteReader();

                        list = new List<BeEcFuncion>();

                        if (dr.HasRows)
                        {
                            int posId = dr.GetOrdinal("fun_id");
                            int posNombre = dr.GetOrdinal("fun_nombre");
                            int posDescripcion = dr.GetOrdinal("fun_descripcion");
                            int posOrden = dr.GetOrdinal("fun_orden");
                            int posPadre = dr.GetOrdinal("fun_padre");
                            int posSisId = dr.GetOrdinal("fun_sisid");
                            int posSisNombre = dr.GetOrdinal("sis_nombre");
                            int posSisDescripcion = dr.GetOrdinal("sis_descripcion");

                            while (dr.Read())
                            {
                                BeEcFuncion fun = new BeEcFuncion();
                                fun.fun_id = dr.GetDecimal(posId);
                                fun.fun_nombre = (dr.IsDBNull(posNombre)) ? "" : dr.GetString(posNombre);
                                fun.fun_descripcion = (dr.IsDBNull(posDescripcion)) ? "" : dr.GetString(posDescripcion);
                                fun.fun_orden = (dr.IsDBNull(posOrden)) ? 0 : dr.GetDecimal(posOrden);
                                fun.fun_padre = (dr.IsDBNull(posPadre)) ? 0 : dr.GetDecimal(posPadre);
                                fun.fun_system = (dr.IsDBNull(posSisId)) ? 0 : dr.GetDecimal(posSisId);
                                fun.fun_sistema.sis_id = (dr.IsDBNull(posSisId)) ? 0 : dr.GetDecimal(posSisId);
                                fun.fun_sistema.sis_nombre = (dr.IsDBNull(posSisNombre)) ? "" : dr.GetString(posSisNombre);
                                fun.fun_sistema.sis_descripcion = (dr.IsDBNull(posSisDescripcion)) ? "" : dr.GetString(posSisDescripcion);
                                list.Add(fun);
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

        public List<BeEcFuncion> ConsultarFuncion(BeEcFuncion obj,  ref string mensaje, ref string tipo)
        {

            string sqlquery = "USP_Consultar_Funcion_Sistema";
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
                        cmd.Parameters.AddWithValue("@fun_id", obj.fun_id);
                        cmd.Parameters.AddWithValue("@fun_nombre", obj.fun_nombre);
                        cmd.Parameters.AddWithValue("@fun_descripcion", obj.fun_descripcion);
                        cmd.Parameters.AddWithValue("@fun_orden", obj.fun_orden);
                        cmd.Parameters.AddWithValue("@fun_padre", obj.fun_padre);
                        cmd.Parameters.AddWithValue("@fun_sisId", obj.fun_system);

                        SqlDataReader dr = cmd.ExecuteReader();

                        list = new List<BeEcFuncion>();

                        if (dr.HasRows)
                        {
                            int posId = dr.GetOrdinal("fun_id");
                            int posNombre = dr.GetOrdinal("fun_nombre");
                            int posDescripcion = dr.GetOrdinal("fun_descripcion");
                            int posOrden = dr.GetOrdinal("fun_orden");
                            int posPadre = dr.GetOrdinal("fun_padre");
                            int posSisId = dr.GetOrdinal("fun_sisid");
                            int posSisNombre = dr.GetOrdinal("sis_nombre");
                            int posSisDescripcion = dr.GetOrdinal("sis_descripcion");

                            while (dr.Read())
                            {
                                BeEcFuncion fun = new BeEcFuncion();
                                fun.fun_id = dr.GetDecimal(posId);
                                fun.fun_nombre = (dr.IsDBNull(posNombre)) ? "" : dr.GetString(posNombre);
                                fun.fun_descripcion = (dr.IsDBNull(posDescripcion)) ? "" : dr.GetString(posDescripcion);
                                fun.fun_orden = (dr.IsDBNull(posOrden)) ? 0 : dr.GetDecimal(posOrden);
                                fun.fun_padre = (dr.IsDBNull(posPadre)) ? 0 : dr.GetDecimal(posPadre);
                                fun.fun_system = (dr.IsDBNull(posSisId)) ? 0 : dr.GetDecimal(posSisId);
                                fun.fun_sistema.sis_id = (dr.IsDBNull(posSisId)) ? 0 : dr.GetDecimal(posSisId);
                                fun.fun_sistema.sis_nombre = (dr.IsDBNull(posSisNombre)) ? "" : dr.GetString(posSisNombre);
                                fun.fun_sistema.sis_descripcion = (dr.IsDBNull(posSisDescripcion)) ? "" : dr.GetString(posSisDescripcion);
                                list.Add(fun);
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

using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System;

namespace BataEcommerce.DA.Ecommerce
{
    public class EcAplicacionDAO
    {

        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);

        public Boolean UpdateAplicacion(BeEcAplicacion obj, ref string mensaje, ref string tipo)
        {
            Boolean valida = false;
            string sqlquery = "USP_Modificar_Aplicacion";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {

                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@apl_id", obj.apl_id);
                        cmd.Parameters.AddWithValue("@apl_nombre", obj.apl_nombre);
                        cmd.Parameters.AddWithValue("@apl_tip_id", obj.apl_tip_id);
                        cmd.Parameters.AddWithValue("@apl_url", obj.apl_url);
                        cmd.Parameters.AddWithValue("@apl_orden", obj.apl_orden);
                        cmd.Parameters.AddWithValue("@apl_est_id", obj.apl_est_id);
                        cmd.Parameters.AddWithValue("@apl_ayuda", obj.apl_ayuda);
                        cmd.Parameters.AddWithValue("@apl_comentario", obj.apl_comentario);
                        cmd.Parameters.AddWithValue("@apl_controller", obj.apl_controller);
                        cmd.Parameters.AddWithValue("@apl_action", obj.apl_action);
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
        public Boolean InsertarAplicacion(BeEcAplicacion obj, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Insertar_Aplicacion";
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
                        cmd.Parameters.AddWithValue("@apl_id", obj.apl_id).Direction = ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("@apl_nombre", obj.apl_nombre);
                        cmd.Parameters.AddWithValue("@apl_tip_id", obj.apl_tip_id);
                        cmd.Parameters.AddWithValue("@apl_url", obj.apl_url);
                        cmd.Parameters.AddWithValue("@apl_orden", obj.apl_orden);
                        cmd.Parameters.AddWithValue("@apl_est_id", obj.apl_est_id);
                        cmd.Parameters.AddWithValue("@apl_ayuda", obj.apl_ayuda);
                        cmd.Parameters.AddWithValue("@apl_comentario", obj.apl_comentario);
                        cmd.Parameters.AddWithValue("@apl_controller", obj.apl_controller);
                        cmd.Parameters.AddWithValue("@apl_action", obj.apl_action);
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
        public List<BeEcAplicacion> get_lista(ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Consultar_Aplicacion";
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
                        SqlDataReader dr = cmd.ExecuteReader();

                        list = new List<BeEcAplicacion>();

                        if (dr.HasRows)
                        {
                            int posId = dr.GetOrdinal("apl_id");
                            int posNombre = dr.GetOrdinal("apl_nombre");
                            int posTipId = dr.GetOrdinal("apl_tip_id");
                            int posUrl = dr.GetOrdinal("apl_url");
                            int posOrden = dr.GetOrdinal("apl_orden");
                            int posEstId = dr.GetOrdinal("apl_est_id");
                            int posAction = dr.GetOrdinal("apl_action");
                            int posController = dr.GetOrdinal("apl_controller");

                            while (dr.Read())
                            {
                                BeEcAplicacion apl = new BeEcAplicacion();
                                apl.apl_id = dr.GetDecimal(posId);
                                apl.apl_nombre = (dr.IsDBNull(posNombre)) ? "" : dr.GetString(posNombre);
                                apl.apl_tip_id = (dr.IsDBNull(posTipId)) ? "" : dr.GetString(posTipId);
                                apl.apl_url = (dr.IsDBNull(posUrl)) ? "" : dr.GetString(posUrl);
                                apl.apl_orden = (dr.IsDBNull(posOrden)) ? 0 : dr.GetDecimal(posOrden);
                                apl.apl_est_id = (dr.IsDBNull(posEstId)) ? "" : dr.GetString(posEstId);
                                apl.apl_action = (dr.IsDBNull(posAction)) ? "" : dr.GetString(posAction);
                                apl.apl_controller = (dr.IsDBNull(posController)) ? "" : dr.GetString(posController);
                                list.Add(apl);
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


        public List<BeEcAplicacion> ConsultarAplicacion(BeEcAplicacion obj, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Consultar_Aplicacion";
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
                        cmd.Parameters.AddWithValue("@apl_id", obj.apl_id);
                        cmd.Parameters.AddWithValue("@apl_nombre", obj.apl_nombre);
                        cmd.Parameters.AddWithValue("@apl_url", obj.apl_url);
                        cmd.Parameters.AddWithValue("@apl_est_id", obj.apl_est_id);
                        cmd.Parameters.AddWithValue("@apl_orden", obj.apl_orden);
                        SqlDataReader dr = cmd.ExecuteReader();

                        list = new List<BeEcAplicacion>();

                        if (dr.HasRows)
                        {
                            int posId = dr.GetOrdinal("apl_id");
                            int posNombre = dr.GetOrdinal("apl_nombre");
                            int posTipId = dr.GetOrdinal("apl_tip_id");
                            int posUrl = dr.GetOrdinal("apl_url");
                            int posOrden = dr.GetOrdinal("apl_orden");
                            int posAyuda = dr.GetOrdinal("apl_ayuda");
                            int posComentario = dr.GetOrdinal("apl_comentario");
                            int posEstId = dr.GetOrdinal("apl_est_id");
                            int posAction = dr.GetOrdinal("apl_action");
                            int posController = dr.GetOrdinal("apl_controller");

                            while (dr.Read())
                            {
                                BeEcAplicacion apl = new BeEcAplicacion();
                                apl.apl_id = dr.GetDecimal(posId);
                                apl.apl_nombre = (dr.IsDBNull(posNombre)) ? "" : dr.GetString(posNombre);
                                apl.apl_tip_id = (dr.IsDBNull(posTipId)) ? "" : dr.GetString(posTipId);
                                apl.apl_url = (dr.IsDBNull(posUrl)) ? "" : dr.GetString(posUrl);
                                apl.apl_orden = (dr.IsDBNull(posOrden)) ? 0 : dr.GetDecimal(posOrden);
                                apl.apl_ayuda = (dr.IsDBNull(posAyuda)) ? "" : dr.GetString(posAyuda);
                                apl.apl_comentario = (dr.IsDBNull(posComentario)) ? "" : dr.GetString(posComentario);
                                apl.apl_est_id = (dr.IsDBNull(posEstId)) ? "" : dr.GetString(posEstId);
                                apl.apl_action = (dr.IsDBNull(posAction)) ? "" : dr.GetString(posAction);
                                apl.apl_controller = (dr.IsDBNull(posController)) ? "" : dr.GetString(posController);
                                list.Add(apl);
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



        public List<BeEcTipoAplicacion> LeerAplicacionTipo(ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Leer_Aplicacion_Tipo";
            List<BeEcTipoAplicacion> list = null;
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

                        list = new List<BeEcTipoAplicacion>();

                        if (dr.HasRows)
                        {
                            int posId = dr.GetOrdinal("apl_tip_id");
                            int posDescripcion = dr.GetOrdinal("apl_tip_descripcion");

                            while (dr.Read())
                            {
                                BeEcTipoAplicacion apl = new BeEcTipoAplicacion();
                                apl.apl_tip_id = dr.GetString(posId);
                                apl.apl_tip_descripcion = dr.GetString(posDescripcion);
                                list.Add(apl);
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

using System;
using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;

namespace BataEcommerce.DA.Ecommerce
{
    public class EcRolDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);

        public Boolean InsertarRoles(BeEcRol obj, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Insertar_Roles";
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
                        cmd.Parameters.AddWithValue("@rol_id", 0);
                        cmd.Parameters.AddWithValue("@rol_nombre", obj.rol_nombre);
                        cmd.Parameters.AddWithValue("@rol_descripcion", obj.rol_descripcion);
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
        public List<BeEcRol> get_lista()
        {
            List<BeEcRol> list = null;
            string sqlquery = "USP_Leer_Roles";
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
                        if (dr.HasRows)
                        {
                            list = new List<BeEcRol>();

                            while (dr.Read())
                            {

                                int posId = dr.GetOrdinal("rol_id");
                                int posNombre = dr.GetOrdinal("rol_nombre");
                                int posDescripcion = dr.GetOrdinal("rol_descripcion");

                                while (dr.Read())
                                {
                                    BeEcRol rol = new BeEcRol();
                                    rol.rol_id = dr.GetDecimal(posId);
                                    rol.rol_nombre = (dr.IsDBNull(posNombre)) ? "" : dr.GetString(posNombre);
                                    rol.rol_descripcion = (dr.IsDBNull(posDescripcion)) ? "" : dr.GetString(posDescripcion);
                                    list.Add(rol);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                list = null;
            }
            return list;
        }
        public Boolean EditarRoles(BeEcRol obj, ref string mensaje, ref string tipo)
        {
            Boolean valida = false;
            string sqlquery = "USP_Modificar_Roles";
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
                        cmd.Parameters.AddWithValue("@rol_nombre", obj.rol_nombre);
                        cmd.Parameters.AddWithValue("@rol_descripcion", obj.rol_descripcion);
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

        public List<BeEcRol> ConsultarRoles(BeEcRol obj, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Consultar_Rol";
            List<BeEcRol> list = null;
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
                        cmd.Parameters.AddWithValue("@rol_nombre", obj.rol_nombre);
                        cmd.Parameters.AddWithValue("@rol_descripcion", obj.rol_descripcion);
                        SqlDataReader dr = cmd.ExecuteReader();

                        list = new List<BeEcRol>();

                        if (dr.HasRows)
                        {
                            int posId = dr.GetOrdinal("rol_id");
                            int posNombre = dr.GetOrdinal("rol_nombre");
                            int posDescripcion = dr.GetOrdinal("rol_descripcion");

                            while (dr.Read())
                            {
                                BeEcRol rol = new BeEcRol();
                                rol.rol_id = dr.GetDecimal(posId);
                                rol.rol_nombre= (dr.IsDBNull(posNombre)) ? "" : dr.GetString(posNombre);
                                rol.rol_descripcion = (dr.IsDBNull(posDescripcion)) ? "" : dr.GetString(posDescripcion);
                                list.Add(rol);
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

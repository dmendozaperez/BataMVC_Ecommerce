using System;
using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;

namespace BataEcommerce.DA.Ecommerce
{
    public class EcUsuarioRolDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);

        public Boolean Eliminar_Rol_Usuario(Decimal _usu_rol_idusu, decimal _usu_rol_idrol, ref string mensaje, ref string tipo)
        {
            Boolean valida = false;
            string sqlquery = "USP_Borrar_Usuario_Roles";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0; cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usu_rol_idusu", _usu_rol_idusu);
                        cmd.Parameters.AddWithValue("@usu_rol_idrol", _usu_rol_idrol);
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
        public Boolean Insertar_Rol_Usuario(Decimal _usu_rol_idusu, decimal _usu_rol_idrol, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Insertar_Usuario_Roles";
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
                        cmd.Parameters.AddWithValue("@usu_rol_idusu", _usu_rol_idusu);
                        cmd.Parameters.AddWithValue("@usu_rol_idrol", _usu_rol_idrol);
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
        public List<BeEcUsuarioRol> get_lista(BeEcUsuario obj, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Leer_Roles_Usuario";
            List<BeEcUsuarioRol> list = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usu_id", obj._usu_id);
                        SqlDataReader dr = cmd.ExecuteReader();
                        list = new List<BeEcUsuarioRol>();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                BeEcUsuarioRol fila = new BeEcUsuarioRol();
                                fila.rol_id = dr["rol_id"].ToString();
                                fila.rol_nombre = dr["rol_nombre"].ToString();
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

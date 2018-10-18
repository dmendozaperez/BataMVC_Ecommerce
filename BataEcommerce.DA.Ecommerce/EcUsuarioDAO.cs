using System;
using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System.Linq;

namespace BataEcommerce.DA.Ecommerce
{
    public class EcUsuarioDAO
    {

        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);

        public IEnumerable<BeEcUsuario> Buscar(BeEcUsuario objeto, ref string mensaje, ref string tipo)
        {
            List<BeEcUsuario> lbeEcUsuario = new List<BeEcUsuario>();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0)
                        cn.Open();

                    string query = @"SELECT * FROM Usuario WHERE Usu_Nombre = @P_UsuNom AND Usu_Contraseña = @P_UsuPas AND Usu_Est_Id = 'A';";
                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P_UsuNom", objeto._usu_nombre);
                        cmd.Parameters.AddWithValue("@P_UsuPas", objeto._usu_password);
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.Default);

                        if (dr.HasRows)
                        {
                            int posUsuId = dr.GetOrdinal("Usu_Id");
                            int posUsuNom = dr.GetOrdinal("Usu_Nombre");
                            int posUsuEst = dr.GetOrdinal("Usu_Est_Id");

                            BeEcUsuario obeEcUsuario;
                            while (dr.Read())
                            {
                                obeEcUsuario = new BeEcUsuario();
                                obeEcUsuario._usu_id = dr.GetDecimal(posUsuId);
                                obeEcUsuario._usu_nombre = dr.GetString(posUsuNom);
                                obeEcUsuario._usu_est_id = dr.GetString(posUsuEst);
                                lbeEcUsuario.Add(obeEcUsuario);
                            }

                            dr.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                mensaje = Configuration.SI_MSJ_DES_ERROR + "\n\n" + "Detalle de Error: " + e.Message;
                tipo = Configuration.SI_MSJ_TIP_ERROR;
            }

            mensaje = Configuration.SI_MSJ_DES_EXITO;
            tipo = Configuration.SI_MSJ_TIP_EXITO;

            return (lbeEcUsuario);
        }


        public Boolean UpdateUsuario(BeEcUsuario obj)
        {
            Boolean _upd = false;
            string sqlquery = "USP_Modificar_Usuario";
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
                        cmd.Parameters.AddWithValue("@usu_nombre", obj._usu_nombre);
                        cmd.Parameters.AddWithValue("@contraseña", obj._usu_password);
                        cmd.Parameters.AddWithValue("@usu_fecha_cre", obj._usu_fec_cre);
                        cmd.Parameters.AddWithValue("@usu_est_id", obj._usu_est_id);
                        cmd.ExecuteNonQuery();
                        _upd = true;
                    }

                }
            }
            catch
            {
                _upd = false;
            }
            return _upd;
        }

        public List<BeEcUsuario> GetUserByName(BeEcBasico objeto, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Leer_Usuario_Nombre";
            List<BeEcUsuario> listar = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    //if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bas_primer_nombre", objeto.bas_primer_nombre);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            listar = new List<BeEcUsuario>();
                            listar = (from DataRow dr in dt.Rows
                                      select new BeEcUsuario()
                                      {
                                          _usu_id = Convert.ToInt32(dr["usu_id"]),
                                          _usu_nombre = dr["usu_nombre"].ToString(),
                                          _usu_password = dr["usu_contraseña"].ToString(),
                                          _usu_fec_cre = dr["usu_fecha_cre"].ToString(),
                                          _usu_est_id = dr["usu_est_id"].ToString(),
                                          _usu_nom_ape = dr["nombre"].ToString()
                                      }).ToList();
                        }

                    }
                }
            }
            catch
            {
                listar = null;
            }
            return listar;
        }


        public List<BeEcUsuario> ConsultarUsuario(BeEcUsuario objeto, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Leer_Usuario_Persona";
            List<BeEcUsuario> listar = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    //if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usu_Id", objeto._usu_id);
                        cmd.Parameters.AddWithValue("@Usu_Nombre", objeto._usu_nombre);
                        cmd.Parameters.AddWithValue("@bas_nombre", objeto._usu_nom_ape);
                        cmd.Parameters.AddWithValue("@Usu_Fecha_Cre", objeto._usu_fec_cre);
                        cmd.Parameters.AddWithValue("@Usu_Est_Id", objeto._usu_est_id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            listar = new List<BeEcUsuario>();
                            listar = (from DataRow dr in dt.Rows
                                      select new BeEcUsuario()
                                      {
                                          _usu_id = Convert.ToInt32(dr["usu_id"]),
                                          _usu_nombre = dr["usu_nombre"].ToString(),
                                          _usu_fec_cre = Convert.ToDateTime(dr["usu_fecha_cre"]).ToString("dd/MM/yyyy"),
                                          _usu_est_id = dr["usu_est_id"].ToString(),
                                          _usu_nom_ape = dr["nombre"].ToString()
                                      }).ToList();
                        }

                    }
                }
            }
            catch (Exception e)
            {
                mensaje = Configuration.SI_MSJ_DES_ERROR + "\n\n" + "Detalle de Error: " + e.Message;
                tipo = Configuration.SI_MSJ_TIP_ERROR;
            }

            mensaje = Configuration.SI_MSJ_DES_EXITO;
            tipo = Configuration.SI_MSJ_TIP_EXITO;
            return listar;
        }



    }
}

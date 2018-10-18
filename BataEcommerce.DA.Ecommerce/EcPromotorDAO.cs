using System;
using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System.Linq;
namespace BataEcommerce.DA.Ecommerce
{
    public class EcPromotorDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);

        public string updateCoord(string _customerId, string _area, string _status)
        {
            string sqlquery = "USP_Modificar_Promotor_EstLid";
            string _error = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bas_id", _customerId);
                        cmd.Parameters.AddWithValue("@bas_est_id", _status);
                        cmd.Parameters.AddWithValue("@bas_are_id", _area);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exc)
            {
                _error = exc.Message;
            }
            return _error;
        }
        public DataTable get_dtcliente(string _documento)
        {
            string sqlquery = "USP_Leer_Persona_Usuario";
            DataTable dt = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bas_documento", _documento);
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
                dt = null;
            }
            return dt;
        }

    }
}

using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;

namespace BataEcommerce.DA.Ecommerce
{
    public class EcLiderDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);
        public List<BeEcLider> _leer_lider()
        {
            string sqlquery = "USP_Leer_Area";
            List<BeEcLider> data_select = new List<BeEcLider>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader sqlread = cmd.ExecuteReader();

                        if (sqlread.HasRows)
                        {
                            while (sqlread.Read())
                            {
                                BeEcLider item = new BeEcLider();
                                item.are_id = sqlread["Are_Id"].ToString();
                                item.are_descripcion = sqlread["Are_Descripcion"].ToString();
                                data_select.Add(item);
                            }

                        }
                    }
                }
            }
            catch
            {
                data_select = null;
            }
            return data_select;
        }


    }
}

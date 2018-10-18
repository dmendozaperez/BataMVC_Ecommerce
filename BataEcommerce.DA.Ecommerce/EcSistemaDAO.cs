using System;
using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System;


namespace BataEcommerce.DA.Ecommerce
{
    public class EcSistemaDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);

        public List<BeEcSistema> get_lista(ref string mensaje, ref string tipo)
        {

            string sqlquery = "USP_Leer_Sistema";
            List<BeEcSistema> list = null;
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

                        list = new List<BeEcSistema>();

                        if (dr.HasRows)
                        {
                            int posId = dr.GetOrdinal("sis_id");
                            int posNombre = dr.GetOrdinal("sis_nombre");
                            int posDescripcion = dr.GetOrdinal("sis_descripcion");
                            int posUbicacion = dr.GetOrdinal("sis_ubicacion");

                            while (dr.Read())
                            {
                                BeEcSistema sis = new BeEcSistema();
                                sis.sis_id = dr.GetDecimal(posId);
                                sis.sis_nombre = (dr.IsDBNull(posNombre)) ? "" : dr.GetString(posNombre);
                                sis.sis_descripcion= (dr.IsDBNull(posDescripcion)) ? "" : dr.GetString(posDescripcion);
                                sis.sis_ubicacion = (dr.IsDBNull(posUbicacion)) ? "" : dr.GetString(posUbicacion);
                                list.Add(sis);
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

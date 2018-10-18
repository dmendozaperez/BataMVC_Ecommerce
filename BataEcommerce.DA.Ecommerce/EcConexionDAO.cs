using System;
using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;


namespace BataEcommerce.DA.Ecommerce
{
   public  class EcConexionDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);

        public BeEcConexion Buscar(BeEcConexion objeto, ref string mensaje, ref string tipo)
        {
            BeEcConexion oBeEcConexion = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0)
                        cn.Open();

                    string sp = @"USP_Obtiene_DatosConexion";
                    using (SqlCommand cmd = new SqlCommand(sp, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", objeto.Id);
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.Default);

                        if (dr.HasRows)
                        {
                            int posId = dr.GetOrdinal("Id");
                            int posDes = dr.GetOrdinal("Descripcion");
                            int posTip = dr.GetOrdinal("Tipo");
                            int posUrl = dr.GetOrdinal("Url");
                            int posBd = dr.GetOrdinal("BaseDatos");
                            int posUsu = dr.GetOrdinal("Usuario");
                            int posCon = dr.GetOrdinal("Contrasena");
                            int posTrc = dr.GetOrdinal("Trusted_Conection");
                            int posEst = dr.GetOrdinal("Estado");
                            int posFec = dr.GetOrdinal("Fecha_Crea");
                            int posFem = dr.GetOrdinal("Fecha_Modif");

                            while (dr.Read())
                            {
                                oBeEcConexion = new BeEcConexion();
                                oBeEcConexion.Id = dr.GetString(posId);
                                oBeEcConexion.Descripcion = dr.GetString(posDes);
                                oBeEcConexion.Tipo = dr.GetString(posTip);
                                oBeEcConexion.Url = dr.GetString(posUrl);
                                oBeEcConexion.BaseDatos = dr.GetString(posBd);
                                oBeEcConexion.Usuario = dr.GetString(posUsu);
                                oBeEcConexion.Contrasena = dr.GetString(posCon);
                                oBeEcConexion.Trusted_Connection = (dr.IsDBNull(posTrc)) ? false : dr.GetBoolean(posTrc);
                                oBeEcConexion.Estado = dr.GetString(posCon);
                                oBeEcConexion.Fecha_Crea = dr.GetDateTime(posFec);
                                oBeEcConexion.Fecha_Modif = dr.GetDateTime(posFem);
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

            return (oBeEcConexion);
        }


    }
}

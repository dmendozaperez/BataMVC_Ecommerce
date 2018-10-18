using System;
using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System.Linq;

namespace BataEcommerce.DA.Ecommerce
{
    public class EcPersonalDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);

        public List<BeEcPersonal> get_lista()
        {
            string sqlquery = "USP_LeerLista_UsuariosAQ";
            List<BeEcPersonal> listar = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            listar = new List<BeEcPersonal>();
                            listar = (from DataRow dr in dt.Rows
                                      select new BeEcPersonal()
                                      {
                                          bas_id = Convert.ToInt32(dr["bas_id"]),
                                          nombres = dr["nombres"].ToString(),
                                          primer_nombre = dr["primer_nombre"].ToString(),
                                          segundo_nombre = dr["segundo_nombre"].ToString(),
                                          primer_apellido = dr["primer_apellido"].ToString(),
                                          segundo_apellido = dr["segundo_apellido"].ToString(),
                                          dni_ruc = dr["dni_ruc"].ToString(),
                                          direccion = dr["direccion"].ToString(),
                                          telefono = dr["telefono"].ToString(),
                                          celular = dr["celular"].ToString(),
                                          correo = dr["correo"].ToString(),
                                          tipo_usuario = dr["tipo_usuario"].ToString(),
                                          estado = dr["estado"].ToString(),
                                          fec_nac = dr["fec_nac"].ToString(),
                                          sexo = dr["sexo"].ToString(),
                                          tipo_doc = dr["tipo_doc"].ToString(),
                                          tipo_persona = dr["tipo_persona"].ToString(),
                                          depar = dr["depar"].ToString(),
                                          tipo_usuid = dr["tipo_usu"].ToString(),
                                          prv_id = dr["prv_id"].ToString(),
                                          dis_id = dr["dis_id"].ToString(),
                                      }).ToList();

                        }
                    }
                }
            }
            catch (Exception exc)
            {
                listar = null;
            }
            return listar;
        }
    }
}

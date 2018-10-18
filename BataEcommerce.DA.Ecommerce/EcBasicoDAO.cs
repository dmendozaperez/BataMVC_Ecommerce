using System.Collections.Generic;
using BataEcommerce.BE.Ecommerce;
using System.Data.SqlClient;
using System.Data;
using BataEcommerce.Util;
using System;
using System.Linq;

namespace BataEcommerce.DA.Ecommerce
{
    public class EcBasicoDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_ECOMMERCE);


        public void Ejecuta(ref List<BeEcTipoPersona> lstTipoPersona, ref List<BeEcTipoUsuario> lstTipoUsuario, ref List<BeEcTipoDocumento> lstTipoDocumento, ref List<BeEcDepartamento> lstDepartamento, ref List<BeEcProvincia> lstProvincia, ref List<BeEcDistrito> lstDistrito, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Leer_Datos_Maestros";
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
                            DataSet ds = new DataSet();
                            da.Fill(ds, "Maestros");

                            if (ds.Tables.Count > 0)
                            {
                                DataTable dt_tipopersona = ds.Tables[0];
                                DataTable dt_tipousuario = ds.Tables[1];
                                DataTable dt_tipodocumento = ds.Tables[2];
                                DataTable dt_departamento = ds.Tables[3];
                                DataTable dt_provincia = ds.Tables[4];
                                DataTable dt_distrito = ds.Tables[5];

                                lstTipoPersona = (from DataRow dr in dt_tipopersona.Rows
                                                  select new BeEcTipoPersona()
                                                  {
                                                      per_tip_id = dr["per_tip_id"].ToString(),
                                                      per_tip_des = dr["per_tip_descripcion"].ToString(),
                                                  }).ToList();

                                lstTipoUsuario = (from DataRow dr in dt_tipousuario.Rows
                                                  select new BeEcTipoUsuario()
                                                  {
                                                      usu_tip_id = dr["usu_tip_id"].ToString(),
                                                      usu_tip_nom = dr["usu_tip_nombre"].ToString(),
                                                  }).ToList();

                                lstTipoDocumento = (from DataRow dr in dt_tipodocumento.Rows
                                                    select new BeEcTipoDocumento()
                                                    {
                                                        doc_tip_id = dr["doc_tip_id"].ToString(),
                                                        doc_tip_nom = dr["doc_tip_descripcion"].ToString(),
                                                    }).ToList();

                                lstDepartamento = (from DataRow dr in dt_departamento.Rows
                                                   select new BeEcDepartamento()
                                                   {
                                                       dep_id = dr["dep_id"].ToString(),
                                                       dep_nom = dr["dep_descripcion"].ToString(),
                                                   }).ToList();

                                lstProvincia = (from DataRow dr in dt_provincia.Rows
                                                select new BeEcProvincia()
                                                {
                                                    prv_id = dr["prv_id"].ToString(),
                                                    prv_dep_id = dr["prv_dep_id"].ToString(),
                                                    prv_descripcion = dr["prv_descripcion"].ToString(),
                                                }).ToList();

                                lstDistrito = (from DataRow dr in dt_distrito.Rows
                                               select new BeEcDistrito()
                                               {
                                                   dis_id = dr["dis_id"].ToString(),
                                                   dis_prv_id = dr["dis_prv_id"].ToString(),
                                                   dis_descripcion = dr["dis_descripcion"].ToString(),
                                               }).ToList();

                            }

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
        }

        public BeEcBasico Ejecuta(BeEcBasico obj, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Leer_Persona_Usuario";
            BeEcBasico objBasico = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bas_documento", obj.bas_documento);
                        SqlDataReader dr = cmd.ExecuteReader();

                        objBasico = new BeEcBasico();

                        if (dr.HasRows)
                        {
                            int posBasId = dr.GetOrdinal("bas_id");
                            int posBasPrimerNombre = dr.GetOrdinal("bas_primer_nombre");
                            int posBasSegundoNombre = dr.GetOrdinal("bas_segundo_nombre");
                            int posBasPrimerApellido = dr.GetOrdinal("bas_primer_apellido");
                            int posBasSegundoApellido = dr.GetOrdinal("bas_segundo_apellido");
                            int posBasDocumento = dr.GetOrdinal("bas_documento");
                            int posBasDireccion = dr.GetOrdinal("bas_direccion");
                            int posBasTelefono = dr.GetOrdinal("bas_telefono");
                            int posBasCelular = dr.GetOrdinal("bas_celular");
                            int posBasCorreo = dr.GetOrdinal("bas_correo");
                            int posBasAreId = dr.GetOrdinal("bas_are_id");
                            int posBasSexId = dr.GetOrdinal("bas_sex_id");
                            int posBasFecNac = dr.GetOrdinal("bas_fec_nac");
                            int posBasPerTipId = dr.GetOrdinal("bas_per_tip_id");
                            int posBasFax = dr.GetOrdinal("bas_fax");
                            int posBasEstId = dr.GetOrdinal("bas_est_id");
                            int posUsuTipId = dr.GetOrdinal("usu_tip_id");
                            int posBasFechaCre = dr.GetOrdinal("bas_fecha_cre");
                            int posBasCreUsuario = dr.GetOrdinal("bas_cre_usuario");
                            int posBasModUsuario = dr.GetOrdinal("bas_mod_usuario");
                            int posBasDocTipId = dr.GetOrdinal("bas_doc_tip_id");
                            int posDisDepId = dr.GetOrdinal("dis_dep_id");
                            int posDisPrvCod = dr.GetOrdinal("dis_prv_cod");
                            int posDisId = dr.GetOrdinal("dis_id");
                            int posDisPrvId = dr.GetOrdinal("Dis_Prv_Id");
                            int posDisDescripcion = dr.GetOrdinal("dis_descripcion");
                            int posNombreCompleto = dr.GetOrdinal("NombreCompleto");
                            int posUbicacion = dr.GetOrdinal("Ubicacion");
                            int posConFigIgv = dr.GetOrdinal("Con_Fig_Igv");
                            int posConFigPercepcion = dr.GetOrdinal("Con_Fig_Percepcion");
                            int posConFigPorcDesc = dr.GetOrdinal("Con_Fig_PorcDesc");
                            int posConFigPorcDescPos = dr.GetOrdinal("Con_Fig_PorcDescPos");
                            int posAreDescripcion = dr.GetOrdinal("Are_Descripcion");
                            int posAplicaPercepcion = dr.GetOrdinal("aplica_percepcion");
                            int posBasAgencia = dr.GetOrdinal("bas_agencia");
                            int posBasAgenciaRuc = dr.GetOrdinal("bas_agencia_ruc");
                            int posAsesor = dr.GetOrdinal("asesor");


                            while (dr.Read())
                            {
                                objBasico.bas_id = dr.GetDecimal(posBasId);
                                objBasico.bas_primer_nombre = (dr.IsDBNull(posBasPrimerNombre)) ? "" : dr.GetString(posBasPrimerNombre);
                                objBasico.bas_segundo_nombre = (dr.IsDBNull(posBasSegundoNombre)) ? "" : dr.GetString(posBasSegundoNombre);
                                objBasico.bas_primer_apellido = (dr.IsDBNull(posBasPrimerApellido)) ? "" : dr.GetString(posBasPrimerApellido);
                                objBasico.bas_segundo_apellido = (dr.IsDBNull(posBasSegundoApellido)) ? "" : dr.GetString(posBasSegundoApellido);
                                objBasico.bas_documento = (dr.IsDBNull(posBasSegundoNombre)) ? "" : dr.GetString(posBasSegundoNombre);
                                objBasico.bas_direccion = (dr.IsDBNull(posBasDireccion)) ? "" : dr.GetString(posBasDireccion);
                                objBasico.bas_telefono = (dr.IsDBNull(posBasTelefono)) ? "" : dr.GetString(posBasTelefono);
                                objBasico.bas_celular = (dr.IsDBNull(posBasCelular)) ? "" : dr.GetString(posBasCelular);
                                objBasico.bas_correo = (dr.IsDBNull(posBasCorreo)) ? "" : dr.GetString(posBasCorreo);
                                objBasico.bas_are_id = (dr.IsDBNull(posBasAreId)) ? "" : dr.GetString(posBasAreId);
                                objBasico.bas_sex_id = (dr.IsDBNull(posBasSexId)) ? "" : dr.GetString(posBasSexId);
                                objBasico.bas_fec_nac = (dr.IsDBNull(posBasFecNac)) ? "" : dr.GetString(posBasFecNac);
                                objBasico.bas_per_tip_id = (dr.IsDBNull(posBasPerTipId)) ? "" : dr.GetString(posBasPerTipId);
                                objBasico.bas_fax = (dr.IsDBNull(posBasFax)) ? "" : dr.GetString(posBasFax);
                                objBasico.bas_est_id = (dr.IsDBNull(posBasEstId)) ? "" : dr.GetString(posBasEstId);
                                objBasico.bas_usu_tip_id = (dr.IsDBNull(posUsuTipId)) ? "" : dr.GetString(posUsuTipId);
                                objBasico.bas_fecha_cre = (dr.IsDBNull(posBasFechaCre)) ? "" : dr.GetString(posBasFechaCre);
                                objBasico.bas_cre_usuario = (dr.IsDBNull(posBasCreUsuario)) ? "" : dr.GetString(posBasCreUsuario);
                                objBasico.bas_mod_usuario = (dr.IsDBNull(posBasModUsuario)) ? "" : dr.GetString(posBasModUsuario);
                                objBasico.bas_doc_tip_id = (dr.IsDBNull(posBasDocTipId)) ? "" : dr.GetString(posBasDocTipId);
                                objBasico.bas_distrito = new BeEcDistrito();
                                objBasico.bas_distrito.dis_dep_id = (dr.IsDBNull(posDisDepId)) ? "" : dr.GetString(posDisDepId);
                                objBasico.bas_distrito.dis_prv_cod = (dr.IsDBNull(posDisPrvCod)) ? "" : dr.GetString(posDisPrvCod);
                                objBasico.bas_distrito.dis_id = (dr.IsDBNull(posDisId)) ? "" : dr.GetDecimal(posDisId).ToString();
                                objBasico.bas_distrito.dis_prv_id = (dr.IsDBNull(posDisPrvId)) ? "" : dr.GetDecimal(posDisPrvId).ToString();
                                objBasico.bas_distrito.dis_descripcion = (dr.IsDBNull(posDisDescripcion)) ? "" : dr.GetString(posDisDescripcion);
                                objBasico.bas_distrito.dis_ubicacion = (dr.IsDBNull(posUbicacion)) ? "" : dr.GetString(posUbicacion);
                                objBasico.bas_nombre_completo = (dr.IsDBNull(posNombreCompleto)) ? "" : dr.GetString(posNombreCompleto);
                                objBasico.bas_configuracion = new BeEcConfiguracion();
                                objBasico.bas_configuracion.con_fig_igv = (dr.IsDBNull(posConFigIgv)) ? 0 : dr.GetDecimal(posConFigIgv);
                                objBasico.bas_configuracion.con_fig_percepcion = (dr.IsDBNull(posConFigPercepcion)) ? 0 : dr.GetDecimal(posConFigPercepcion);
                                objBasico.bas_configuracion.con_fig_procdesc = (dr.IsDBNull(posConFigPorcDesc)) ? 0 : dr.GetDecimal(posConFigPorcDesc);
                                objBasico.bas_configuracion.con_fig_porcdescpos = (dr.IsDBNull(posConFigPorcDescPos)) ? 0 : dr.GetDecimal(posConFigPorcDescPos);
                                objBasico.bas_configuracion.con_fig_porcdescpos = (dr.IsDBNull(posConFigPorcDescPos)) ? 0 : dr.GetDecimal(posConFigPorcDescPos);
                                objBasico.bas_area = new BeEcArea();
                                objBasico.bas_area.are_descripcion = (dr.IsDBNull(posAreDescripcion)) ? "" : dr.GetString(posAreDescripcion); ;
                                objBasico.bas_percepcion = (dr.IsDBNull(posAplicaPercepcion)) ? false : dr.GetBoolean(posAplicaPercepcion);
                                objBasico.bas_agencia = (dr.IsDBNull(posBasAgencia)) ? "" : dr.GetString(posBasAgencia);
                                objBasico.bas_agencia_ruc = (dr.IsDBNull(posBasAgenciaRuc)) ? "" : dr.GetString(posBasAgenciaRuc);
                                objBasico.bas_asesor = (dr.IsDBNull(posAsesor)) ? "" : dr.GetString(posAsesor);

                                break;
                            }
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
            return objBasico;
        }


        public Boolean EditarBasicoUsuario(BeEcBasico obj, ref string mensaje, ref string tipo)
        {
            Boolean valida = false;
            string sqlquery = "USP_Modificar_Basico_Dato";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bas_id", obj.bas_id);
                        cmd.Parameters.AddWithValue("@bas_primer_nombre", obj.bas_primer_nombre);
                        cmd.Parameters.AddWithValue("@bas_segundo_nombre", obj.bas_segundo_nombre);
                        cmd.Parameters.AddWithValue("@bas_primer_apellido", obj.bas_primer_apellido);
                        cmd.Parameters.AddWithValue("@bas_segundo_apellido", obj.bas_segundo_apellido);
                        cmd.Parameters.AddWithValue("@bas_fec_nac", obj.bas_fec_nac);
                        cmd.Parameters.AddWithValue("@bas_doc_tip_id", obj.bas_doc_tip_id);
                        cmd.Parameters.AddWithValue("@bas_per_tip_id", obj.bas_per_tip_id);
                        cmd.Parameters.AddWithValue("@bas_direccion", obj.bas_direccion);
                        cmd.Parameters.AddWithValue("@bas_telefono", obj.bas_telefono);
                        cmd.Parameters.AddWithValue("@bas_fax", obj.bas_fax);
                        cmd.Parameters.AddWithValue("@bas_celular", obj.bas_celular);
                        cmd.Parameters.AddWithValue("@bas_correo", obj.bas_correo);
                        cmd.Parameters.AddWithValue("@bas_are_id", obj.bas_are_id);
                        cmd.Parameters.AddWithValue("@bas_mod_usuario", obj.bas_mod_usuario);
                        cmd.Parameters.AddWithValue("@bas_sex_id", obj.bas_sex_id);
                        cmd.Parameters.AddWithValue("@bas_dis_id", obj.bas_dis_id);
                        cmd.Parameters.AddWithValue("@bas_usu_tipid", obj.bas_usu_tip_id);
                        cmd.Parameters.AddWithValue("@DefectoTipoUsu", obj.bas_usu_tip_def);

                        cmd.Parameters.AddWithValue("@bas_agencia", obj.bas_agencia);
                        cmd.Parameters.AddWithValue("@bas_destino", obj.bas_destino);
                        cmd.Parameters.AddWithValue("@bas_agencia_ruc", obj.bas_agencia_ruc);
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


        public Boolean InsertarBasicoUsuario(BeEcBasico obj, ref string mensaje, ref string tipo)
        {
            string sqlquery = "USP_Crear_Usuario";
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
                        cmd.Parameters.AddWithValue("@Bas_Primer_Nombre", obj.bas_primer_nombre);
                        cmd.Parameters.AddWithValue("@Bas_Segundo_Nombre", obj.bas_segundo_nombre);
                        cmd.Parameters.AddWithValue("@Bas_Primer_Apellido", obj.bas_primer_apellido);
                        cmd.Parameters.AddWithValue("@Bas_Segundo_Apellido", obj.bas_segundo_apellido);
                        cmd.Parameters.AddWithValue("@Bas_Fec_nac", obj.bas_fec_nac);
                        cmd.Parameters.AddWithValue("@Bas_Documento", obj.bas_documento);
                        cmd.Parameters.AddWithValue("@Bas_Doc_Tip_Id", obj.bas_doc_tip_id);
                        cmd.Parameters.AddWithValue("@Bas_Per_Tip_Id", obj.bas_per_tip_id);
                        cmd.Parameters.AddWithValue("@Bas_Direccion", obj.bas_direccion);
                        cmd.Parameters.AddWithValue("@Bas_Telefono", obj.bas_telefono);
                        cmd.Parameters.AddWithValue("@Bas_Fax", obj.bas_fax);
                        cmd.Parameters.AddWithValue("@Bas_Celular", obj.bas_celular);
                        cmd.Parameters.AddWithValue("@Bas_Correo", obj.bas_correo);
                        cmd.Parameters.AddWithValue("@Bas_Are_Id", obj.bas_are_id);
                        cmd.Parameters.AddWithValue("@Bas_Cre_Usuario", obj.bas_cre_usuario);
                        cmd.Parameters.AddWithValue("@Bas_Sex_Id", obj.bas_sex_id);
                        cmd.Parameters.AddWithValue("@Bas_Dis_Id", obj.bas_dis_id);
                        cmd.Parameters.AddWithValue("@Bas_Usu_TipId", obj.bas_usu_tip_id);
                        cmd.Parameters.AddWithValue("@Bas_Contraseña", obj.bas_contrasena);
                        cmd.Parameters.AddWithValue("@promotor_defecto", obj.bas_acceso);
                        cmd.Parameters.AddWithValue("@lider", obj.bas_lider);

                        cmd.Parameters.AddWithValue("@bas_agencia", obj.bas_agencia);
                        cmd.Parameters.AddWithValue("@bas_destino", obj.bas_destino);
                        cmd.Parameters.AddWithValue("@bas_agencia_ruc", obj.bas_agencia_ruc);
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

    }
}

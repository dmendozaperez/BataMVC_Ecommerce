using System.Collections.Generic;
using BataEcommerce.Util;
using BataEcommerce.BE.Prestashop;
using MySql.Data.MySqlClient;
using MySql.Data;
using System;
using System.Data;

namespace BataEcommerce.DA.Prestashop
{
    public class PsOrderStateDAO : PsBaseDAO
    {

        public PsOrderStateDAO() : base()
        {
        }

        public IEnumerable<BePsOrderState> Buscar(BePsOrderState objeto, ref string mensaje, ref string tipo)
        {
            List<BePsOrderState> lbePsOrderState = null;

            try
            {
                using (MySqlConnection cn = new MySqlConnection(Conexion))
                {
                    if (cn.State == 0)
                        cn.Open();

                    string sp = "Sp_AppSelectOrderState ";

                    using (MySqlCommand cmd = new MySqlCommand(sp, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.Default);

                        if (dr.HasRows)
                        {
                            lbePsOrderState = new List<BePsOrderState>();
                            int posCatRef = dr.GetOrdinal("Id");
                            int posCatNam = dr.GetOrdinal("Nombre");

                            BePsOrderState oBePsOrderState;
                            while (dr.Read())
                            {
                                oBePsOrderState = new BePsOrderState();
                                oBePsOrderState.Referencia = dr.GetString(posCatRef);
                                oBePsOrderState.Nombre = dr.GetString(posCatNam);
                                lbePsOrderState.Add(oBePsOrderState);
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

            mensaje = Configuration.SI_MSJ_TIP_EXITO;
            tipo = Configuration.SI_MSJ_TIP_EXITO;

            return (lbePsOrderState);
        }

    }
}

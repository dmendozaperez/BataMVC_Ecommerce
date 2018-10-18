using System.Collections.Generic;
using BataEcommerce.Util;
using BataEcommerce.BE.Prestashop;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using BataEcommerce.DA.Ecommerce;
using BataEcommerce.BE.Ecommerce;

namespace BataEcommerce.DA.Prestashop
{
    public class PsCategoryDAO : PsBaseDAO
    {

        public PsCategoryDAO() : base()
        {
        }

        public IEnumerable<BePsCategory> Buscar(BePsCategory objeto, ref string mensaje, ref string tipo)
        {
            List<BePsCategory> lbePsCategory = null;

            try
            {
                using (MySqlConnection cn = new MySqlConnection(Conexion))
                {
                    if (cn.State == 0)
                        cn.Open();

                    string sp = "Sp_AppSelectCategory";

                    using (MySqlCommand cmd = new MySqlCommand(sp, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.Default);

                        if (dr.HasRows)
                        {
                            lbePsCategory = new List<BePsCategory>();
                            int posCatRef = dr.GetOrdinal("category_value");
                            int posCatNam = dr.GetOrdinal("category_name");

                            BePsCategory obePsCategory;
                            while (dr.Read())
                            {
                                obePsCategory = new BePsCategory();
                                obePsCategory.Referencia = dr.GetString(posCatRef);
                                obePsCategory.Nombre = dr.GetString(posCatNam);
                                lbePsCategory.Add(obePsCategory);
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

            return (lbePsCategory);
        }

    }
}

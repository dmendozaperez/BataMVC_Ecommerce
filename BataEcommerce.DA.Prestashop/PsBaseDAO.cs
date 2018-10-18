using BataEcommerce.Util;
using System;
using BataEcommerce.DA.Ecommerce;
using BataEcommerce.BE.Ecommerce;

namespace BataEcommerce.DA.Prestashop
{
    public class PsBaseDAO
    {
        public string Conexion = Configuration.GetConectionSting(Configuration.CN_SQL_PRESTASHOP);
        public PsBaseDAO() {
            string mensaje = "", tipo = "";

            EcConexionDAO ecConexionDAO = new EcConexionDAO();
            BeEcConexion oBeEcConexion = new BeEcConexion();
            oBeEcConexion.Id = "01";

            oBeEcConexion = ecConexionDAO.Buscar(oBeEcConexion, ref mensaje, ref tipo);
            Conexion = String.Format(Configuration.CN_SQL_MYSQL, oBeEcConexion.Url, oBeEcConexion.Usuario, oBeEcConexion.Contrasena, oBeEcConexion.BaseDatos);
        }
    }
}

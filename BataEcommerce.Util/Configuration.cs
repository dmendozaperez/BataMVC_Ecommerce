using System.Configuration;

namespace BataEcommerce.Util
{
    public sealed class Configuration
    {
        public static string GetConectionSting(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        public static string GetParameter(string name)
        {
            return ConfigurationManager.AppSettings[name].ToString();
        }

        public const string CN_SQL_ECOMMERCE = "EcomCn";
        public const string CN_SQL_PRESTASHOP = "PsCn";
        public const string CN_SQL_SQL = "Data Source={0};Initial Catalog={1};Persist Security Info={2};User ID={3};Password={4}";
        public const string CN_SQL_MYSQL = "server={0};user id={1};password={2};persistsecurityinfo=True;database={3}";

        public const string SI_MSJ_TIP_DEFECTO = "1";
        public const string SI_MSJ_TIP_EXITO = "2";
        public const string SI_MSJ_TIP_ADVERTENCIA = "3";
        public const string SI_MSJ_TIP_ERROR = "4";

        public const string SI_MSJ_POS_TOP = "1";
        public const string SI_MSJ_POS_BOTTOM = "2";

        public const string SI_MSJ_DES_DEFECTO = "No hay mensaje establecido.";
        public const string SI_MSJ_DES_EXITO = "Se ejecuto con éxito.";
        public const string SI_MSJ_DES_ADVERTENCIA = "Advertencia! Verifique antes de ejecutar.";
        public const string SI_MSJ_DES_ERROR = @"Se produjo un error.\nComuniquese con el área de Sistemas.";

        public const string SI_MSJ_WIN_LOADER = "1";
        public const string SI_MSJ_WIN_INFORMACION = "2";
        public const string SI_MSJ_WIN_SINO = "3";
        public const string SI_MSJ_WIN_ADVERTENCIA = "4";
        public const string SI_MSJ_WIN_ERROR = "5";
        public const string SI_MSJ_WIN_TITLE = SI_INF_APP_NOMBRE + " - Mensaje de Sistema";

        public const string SI_MSJ_ALT_TITLE = "Notificación de Sistema";

        public const string SI_TOKEN_DEFECTO = "NULL";
                
        public const string SI_MSJ_MODEL_ERROR = "El modelo no cumple con las reglas de validación.";

        public const string SI_MSJ_ACC_EXITO = "Inicio de Sesión correcto.";
        public const string SI_MSJ_ACC_ERROR = "Usuario y/o Contraseña incorrectos.";
        public const string SI_MSJ_ACC_EXCEP = "Se ha producido un error.";

        public const string SI_INF_APP_NOMBRE = "Bata Report App";
        public const string SI_INF_APP_VERSION = "0.0.1";

        public const string SI_DEF_SEL_VALUE = "-- Seleccione --";

        public const string SI_BDD_MOD_TODOS = "0";
        public const string SI_BDD_SIS_AQUA2 = "3";

        public const string SI_APP_WIN_ACTPRO = "1";
        public const string SI_APP_WIN_CREPRO = "2";
        public const string SI_APP_WIN_ACTCLI = "3";
        public const string SI_APP_WIN_CRECLI = "4";


    }
}

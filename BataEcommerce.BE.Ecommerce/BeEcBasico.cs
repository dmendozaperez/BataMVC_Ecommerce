using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BataEcommerce.BE.Ecommerce
{
    public class BeEcBasico
    {
        public decimal bas_id { get; set; }
        public string bas_acceso { get; set; }
        public string bas_contrasena { get; set; }
        public string bas_primer_nombre { get; set; }
        public string bas_segundo_nombre { get; set; }
        public string bas_primer_apellido { get; set; }
        public string bas_segundo_apellido { get; set; }
        public string bas_fec_nac { get; set; }
        public string bas_documento { get; set; }
        public string bas_doc_tip_id { get; set; }
        public string bas_per_tip_id { get; set; }
        public string bas_direccion { get; set; }
        public string bas_telefono { get; set; }
        public string bas_fax { get; set; }
        public string bas_celular { get; set; }
        public string bas_correo { get; set; }
        public string bas_est_id { get; set; }
        public string bas_are_id { get; set; }
        public string bas_fecha_cre { get; set; }
        public string bas_cre_usuario { get; set; }
        public string bas_fecha_mod { get; set; }
        public string bas_mod_usuario { get; set; }
        public string bas_sex_id { get; set; }
        public decimal bas_dis_id { get; set; }
        public bool bas_usu_tip_def { get; set; }
        public string bas_usu_tip_id { get; set; }
        public bool bas_percepcion { get; set; }
        public string bas_agencia { get; set; }
        public string bas_destino { get; set; }
        public string cod_intranet { get; set; }
        public string bas_agencia_ruc { get; set; }
        public string bas_aco_id { get; set; }
        public string bas_nro_cuenta { get; set; }
        public string bas_nombre_completo { get; set; }
        public string bas_asesor { get; set; }
        public bool bas_lider { get; set; }
        public BeEcArea bas_area { get; set; }
        public BeEcTipoUsuario bas_tipo_usuario { get; set; }
        public BeEcDistrito bas_distrito { get; set; }
        public BeEcProvincia bas_provincia { get; set; }
        public BeEcDepartamento bas_departamento { get; set; }
        public BeEcConfiguracion bas_configuracion { get; set; }

        public List<BeEcTipoPersona> tipo_persona { get; set; }
        public List<BeEcTipoUsuario> tipo_usuario { get; set; }
        public List<BeEcTipoDocumento> tipo_documento { get; set; }
        public List<BeEcDepartamento> departamento { get; set; }
        public List<BeEcProvincia> provincia { get; set; }
        public List<BeEcDistrito> distrito { get; set; }
    }
}

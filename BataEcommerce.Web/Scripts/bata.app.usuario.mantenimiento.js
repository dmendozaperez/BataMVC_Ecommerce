// =======================================================================//
// ! Declaración de Variables Globales                                    //
// =======================================================================//
var txtNumDoc, btnValidarDoc, frmGrabar;
var selTipoDocumento, txtPrimerNombre, txtSegundoNombre, txtPrimerApellido, txtSegundoApellido, dtFechaNacimiento, chkMasculino, chkFemenino;
var selTipoPersona, txtEmail, txtTelefono, txtFax, txtCelular;
var selDepartamento, selProvincia, selDistrito, txtDireccion, selArea, btnRegistrar;
//var jsonTipoPersona, jsonTipoDoc, jsonDepartamento, jsonProvincia, jsonDistrito;
var pnlInformacion, pnlTipoPersona, pnlUbicacion, hddAccion, hddSexo, hddBasicoId;

$(document).ready(function () {
    // =======================================================================//
    // ! Asignar Variables Globales                                           //
    // =======================================================================//
    txtNumDoc = $("#txtNumDoc");
    btnValidarDoc = $("#btnValidarDoc");
    frmGrabar = $("#frmGrabar");
    selTipoDocumento = $("#selTipoDocumento");
    txtPrimerNombre = $("#txtPrimerNombre");
    txtSegundoNombre = $("#txtSegundoNombre");
    txtPrimerApellido = $("#txtPrimerApellido");
    txtSegundoApellido = $("#txtSegundoApellido");
    dtFechaNacimiento = $("#dtFechaNacimiento");
    chkMasculino = $("#chkMasculino");
    chkFemenino = $("#chkFemenino");
    selTipoPersona = $("#selTipoPersona");
    txtEmail = $("#txtEmail");
    txtTelefono = $("#txtTelefono");
    txtFax = $("#txtFax");
    txtCelular = $("#txtCelular");
    selDepartamento = $("#selDepartamento");
    selProvincia = $("#selProvincia");
    selDistrito = $("#selDistrito");
    txtDireccion = $("#txtDireccion");
    selArea = $("#selArea");
    btnRegistrar = $("#btnRegistrar");
    pnlInformacion = $("#pnlInformacion");
    pnlTipoPersona = $("#pnlTipoPersona");
    pnlUbicacion = $("#pnlUbicacion");
    hddAccion = $("#hddAccion");
    hddSexo = $("#hddSexo");
    hddBasicoId = $("#hddBasicoId");
    Iniciar();
});

// Configuracion
function Iniciar() {
    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, null, Bata.TipoMensaje.Loader, null, null);
    SetupParsley();
    CargarFecha();
    CargarBasico();
    CargarCheckBox();
    MostrarOcultarPanel(false);
    Bata.FncCerrarMensaje();
}

function SetupParsley() {
    $.listen('parsley:field:validate', function () {
        Bata.FncValidateParsley(frmGrabar);
    });
}

function CargarFecha() {
    // Cargar DateRangeTime
    dtFechaNacimiento.daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        calender_style: "picker_1",
        format: 'DD/MM/YYYY',
        minDate: moment("1900-01-01T00:00:00"),
        maxDate: moment(),
        ignoreReadonly: true
    });
}

function CargarCheckBox() {
    Bata.FncEstableceEventoRadioCheck(chkMasculino, EstableceSexo);
    Bata.FncEstableceEventoRadioCheck(chkFemenino, EstableceSexo);
}


function MostrarOcultarPanel(flg) {
    Bata.FncMostrarOcultarPanel(pnlInformacion, flg);
    Bata.FncMostrarOcultarPanel(pnlTipoPersona, flg);
    Bata.FncMostrarOcultarPanel(pnlUbicacion, flg);
}


function CargarBasico() {

    var request = new FormData();

    $.ajax({
        url: GLB_RUT_APP_GETBAS,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            if (data.Tipo == Bata.Notifica.Error) return;
            if (data.Tipo == Bata.Notifica.Exito) {
                // Format Select2 Json
                GLB_JSN_TIP_PER = Bata.FncConvertirJsonSelect2(data.Lista[0], "per_tip_id", "per_tip_des");
                GLB_JSN_TIP_USU = Bata.FncConvertirJsonSelect2(data.Lista[1], "usu_tip_id", "usu_tip_nom");
                GLB_JSN_TIP_DOC = Bata.FncConvertirJsonSelect2(data.Lista[2], "doc_tip_id", "doc_tip_nom");
                GLB_JSN_DPTO = Bata.FncConvertirJsonSelect2(data.Lista[3], "dep_id", "dep_nom");
                GLB_JSN_PROV = data.Lista[4];
                GLB_JSN_DIST = data.Lista[5];
                Bata.FncEstableceSelectJson(selTipoPersona, GLB_JSN_TIP_PER);
                Bata.FncEstableceSelectJson(selArea, GLB_JSN_TIP_USU);
                Bata.FncEstableceSelectJson(selTipoDocumento, GLB_JSN_TIP_DOC);
                Bata.FncEstableceSelectJson(selDepartamento, GLB_JSN_DPTO);
                Bata.FncEstableceSelectJson(selProvincia, Bata.FncObtenerJsonSelect2Vacio());
                Bata.FncEstableceSelectJson(selDistrito, Bata.FncObtenerJsonSelect2Vacio());
                Bata.FncEstableceUbigeoSelect2(selDepartamento, selProvincia, selDistrito);
            }
            return;
        },
        error: GLB_MSJ_DES_ERROR
    });
}


function ValidarDocumento(e) {
    if (e != null)
        e.preventDefault();
    Limpiar();

    var valNumDoc = Bata.FncObtenerValor(txtNumDoc);

    var request = new FormData();
    request.append('bas_documento', valNumDoc);

    $.ajax({
        url: GLB_RUT_APP_GETBAU,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            if (data.Tipo == Bata.Notifica.Error) return;
            if (data.Tipo == Bata.Notifica.Exito) {
                var obj = data.Modelo;

                if (obj.bas_documento != null) {
                    Bata.FncLlenarControl(hddBasicoId, obj.bas_id);
                    Bata.FncLlenarControl(selTipoDocumento, obj.bas_doc_tip_id);
                    Bata.FncLlenarControl(txtPrimerNombre, obj.bas_primer_nombre);
                    Bata.FncLlenarControl(txtSegundoNombre, obj.bas_segundo_nombre);
                    Bata.FncLlenarControl(txtPrimerApellido, obj.bas_primer_apellido);
                    Bata.FncLlenarControl(txtSegundoApellido, obj.bas_segundo_apellido);
                    Bata.FncLlenarControl(dtFechaNacimiento, obj.bas_fec_nac);
                    if (obj.bas_sex_id == 'M') Bata.FncLlenarControl(chkMasculino, true);
                    if (obj.bas_sex_id == 'F') Bata.FncLlenarControl(chkFemenino, true);
                    Bata.FncLlenarControl(selTipoPersona, obj.bas_per_tip_id);
                    Bata.FncLlenarControl(txtEmail, obj.bas_correo);
                    Bata.FncLlenarControl(txtTelefono, obj.bas_telefono);
                    Bata.FncLlenarControl(txtFax, obj.bas_fax);
                    Bata.FncLlenarControl(txtCelular, obj.bas_celular);
                    Bata.FncLlenarControl(txtDireccion, obj.bas_direccion);
                    Bata.FncLlenarControl(selArea, obj.bas_usu_tip_id);
                    Bata.FncLlenarUbigeo([obj.bas_distrito.dis_dep_id, obj.bas_distrito.dis_prv_id, obj.bas_distrito.dis_id], selDepartamento, selProvincia, selDistrito);
                    Bata.FncLlenarControl(btnRegistrar, "<i class='fa fa-save'></i> Actualizar");
                    Bata.FncLlenarControl(hddAccion, GLB_APP_WIN_ACTCLI);
                }
                else {
                    Bata.FncLlenarControl(btnRegistrar, "<i class='fa fa-save'></i> Registrar");
                    Bata.FncLlenarControl(hddAccion, GLB_APP_WIN_CREPRO);
                }
                Bata.FncEstableceFoco(selTipoDocumento);
                MostrarOcultarPanel(true);

            }
            return;
        },
        error: GLB_MSJ_DES_ERROR
    });



}


function Grabar(e) {
    e.preventDefault();

    // Validar Campos
    if (frmGrabar.parsley().validate() === false)
        return;


    var valBasicoId = Bata.FncObtenerValor(hddBasicoId);
    var valCedula = Bata.FncObtenerValor(txtNumDoc);
    var valTipoDoc = Bata.FncObtenerValor(selTipoDocumento);
    var valPrimerNombre = Bata.FncObtenerValor(txtPrimerNombre);
    var valSegundoNombre = Bata.FncObtenerValor(txtSegundoNombre);
    var valPrimerApellido = Bata.FncObtenerValor(txtPrimerApellido);
    var valSegundoApellido = Bata.FncObtenerValor(txtSegundoApellido);
    var valFecNac = Bata.FncConvertirFechaDB(Bata.FncObtenerValor(dtFechaNacimiento));
    var valTipPer = Bata.FncObtenerValor(selTipoPersona);
    var valSexo = Bata.FncObtenerValor(hddSexo);
    var valDireccion = Bata.FncObtenerValor(txtDireccion);
    var valTelefono = Bata.FncObtenerValor(txtTelefono);
    var valCorreo = Bata.FncObtenerValor(txtEmail);
    var valCelular = Bata.FncObtenerValor(txtCelular);
    var valFax = Bata.FncObtenerValor(txtFax);
    var valCiudad = Bata.FncObtenerValor(selDistrito);
    var valAccion = Bata.FncObtenerValor(hddAccion);
    var valArea = Bata.FncObtenerValor(selArea);

    var request = new FormData();
    request.append('bas_id', valBasicoId);
    request.append('bas_documento', valCedula);
    request.append('bas_doc_tip_id', valTipoDoc);
    request.append('bas_primer_nombre', valPrimerNombre);
    request.append('bas_segundo_nombre', valSegundoNombre);
    request.append('bas_primer_apellido', valPrimerApellido);
    request.append('bas_segundo_apellido', valSegundoApellido);
    request.append('bas_fec_nac', valFecNac);
    request.append('bas_per_tip_id', valTipPer);
    request.append('bas_sex_id', valSexo);
    request.append('bas_direccion', valDireccion);
    request.append('bas_telefono', valTelefono);
    request.append('bas_correo', valCorreo);
    request.append('bas_celular', valCelular);
    request.append('bas_fax', valFax);
    request.append('bas_dis_id', valCiudad);
    request.append('bas_usu_tip_id', valArea);
    request.append('accion', valAccion);

    $.ajax({
        url: GLB_RUT_APP_ADDBAS,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            if (data.Tipo == GLB_MSJ_ERROR) {
                Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, data.Mensaje, Bata.TipoMensaje.Error, null, null);
            }
            if (data.Tipo == GLB_MSJ_EXITO) {
                MostrarOcultarPanel(false);
                Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, data.Mensaje, Bata.TipoMensaje.Informacion, null, null);
            }
        },
        error: GLB_MSJ_DES_ERROR
    });

    Limpiar()
    Bata.FncLimpiarControl(txtNumDoc);
    Bata.FncEstableceFoco(txtNumDoc);
}



function Limpiar() {
    Bata.FncLimpiarControl(selTipoDocumento);
    Bata.FncLimpiarControl(txtPrimerNombre);
    Bata.FncLimpiarControl(txtSegundoNombre);
    Bata.FncLimpiarControl(txtPrimerApellido);
    Bata.FncLimpiarControl(txtSegundoApellido);
    Bata.FncLimpiarControl(dtFechaNacimiento);
    Bata.FncLimpiarControl(chkMasculino);
    Bata.FncLimpiarControl(chkFemenino);
    Bata.FncLimpiarControl(selTipoPersona);
    Bata.FncLimpiarControl(txtEmail);
    Bata.FncLimpiarControl(txtTelefono);
    Bata.FncLimpiarControl(txtFax);
    Bata.FncLimpiarControl(txtCelular);
    Bata.FncLimpiarControl(txtDireccion);
    Bata.FncLimpiarControl(selArea);
    Bata.FncLimpiarControl(selDepartamento);
    Bata.FncLimpiarControl(selProvincia);
    Bata.FncLimpiarControl(selDistrito);
    Bata.FncLimpiarControl(hddAccion);
    Bata.FncLimpiarControl(hddSexo);
    Bata.FncLimpiarControl(hddBasicoId);
}


function EstableceSexo() {
    if (chkMasculino.is(":checked"))
        Bata.FncLlenarControl(hddSexo, 'M');
    if (chkFemenino.is(":checked"))
        Bata.FncLlenarControl(hddSexo, 'F');
}

// =======================================================================//
// ! Declaración de Variables Globales                                    //
// =======================================================================//
var txtId, txtUsuario, txtNombre, dtFechaDesde, selEstado, btnConsultar, btnLimpiar, frmConsulta, tblUsuario;
var modRol, lblUsuario, frmConsultaMod, selRol, hddUsuario, tblRol;
var jsonUsuarioTotal;

$(document).ready(function () {
    // =======================================================================//
    // ! Asignar Variables Globales                                           //
    // =======================================================================//
    txtId = $("#txtId");
    txtUsuario = $("#txtUsuario");
    txtNombre = $("#txtNombre");
    dtFechaDesde = $("#dtFechaDesde");
    selEstado = $("#selEstado");
    btnConsultar = $("#btnConsultar");
    btnLimpiar = $("#btnLimpiar");
    frmConsulta = $("#frmConsulta");
    tblUsuario = $("#tblUsuario");

    modRol = $("#modRol");
    lblUsuario = $("#lblUsuario");
    frmConsultaMod = $("#frmConsultaMod");
    selRol = $("#selRol");
    hddUsuario = $("#hddUsuario");
    tblRol = $("#tblRol");
    Iniciar();
});

// Configuracion
function Iniciar() {
    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, null, Bata.TipoMensaje.Loader, null, null);
    SetupParsley();
    CargarFecha();
    CargarEstado(GLB_BDD_MOD_TODOS);
    CargarRoles();
    CargarDataTable();
    Bata.FncCerrarMensaje();
}


function CargarFecha() {
    // Cargar DateRangeTime
    dtFechaDesde.daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        calender_style: "picker_1",
        format: 'DD/MM/YYYY',
        minDate: moment("1900-01-01T00:00:00"),
        maxDate: moment(),
        ignoreReadonly: true
    });
}

function SetupParsley() {
    $.listen('parsley:field:validate', function () {
        Bata.FncValidateParsley(frmConsulta);
    });
}

function CargarEstado(modid) {
    var request = new FormData();
    request.append('modid', modid);

    $.ajax({
        url: GLB_RUT_APP_GETEST,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            if (data.Tipo == Bata.Notifica.Error) {
                Bata.FncMostrarAlerta(GLB_ALT_TITLE, GLB_MSJ_DES_ERROR, Bata.Notifica.Error, Bata.NotificaPos.Bottom);
            }
            if (data.Tipo == Bata.Notifica.Exito) {
                // Format Select2 Json
                var json = Bata.FncConvertirJsonSelect2(data.Lista, "_est_id", "_est_des");
                Bata.FncEstableceSelectJson(selEstado, json);
            }
            return;
        },
        error: GLB_MSJ_DES_ERROR
    });
}


function CargarDataTable() {

    var valId = (Bata.FncObtenerValor(txtId) == '') ? -1 : Bata.FncObtenerValor(txtId);
    var valUsuario = Bata.FncObtenerValor(txtUsuario);
    var valNombre = Bata.FncObtenerValor(txtNombre);
    var valFechaCre = (Bata.FncObtenerValor(dtFechaDesde) == '') ? '' : Bata.FncConvertirFechaDB(Bata.FncObtenerValor(dtFechaDesde));
    var valEstId = (Bata.FncObtenerValor(selEstado) == GLB_DEF_SEL_VALUE) ? '' : Bata.FncObtenerValor(selEstado);

    var request = new FormData();
    request.append('_usu_id', valId);
    request.append('_usu_nombre', valUsuario);
    request.append('_usu_nom_ape', valNombre);
    request.append('_usu_fec_cre', valFechaCre);
    request.append('_usu_est_id', valEstId);

    $.ajax({
        url: GLB_RUT_APP_GETUSU,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            // Validar
            if (data.Tipo == GLB_MSJ_ERROR) return;
            if (data.Lista.length < 0) return;

            // Eliminar la tabla
            Bata.FncLimpiarControl(tblUsuario);

            tblUsuario.dataTable({
                "bAutoWidth": false,
                "columnDefs": [
                    { "width": "5%", "targets": 0, "searchable": false },
                    { "width": "20%", "targets": 1 },
                    { "width": "55%", "targets": 2 },
                    { "width": "10%", "targets": 3, "searchable": false },
                    { "width": "5%", "targets": 4, "searchable": false },
                    {
                        "width": "5%",
                        "targets": 5,
                        "searchable": false,
                        "orderable": false,
                        "render": function (data, type, full, meta) {
                            var id = $('<div/>').text(data).html();
                            return '<a rel="tooltip" data-toggle="tooltip" data-placement="top" title="Editar Roles Nro.' + id + '" class="btn btn-default" name="id[]" onclick="MostrarRol(' + id + ')" value="' + id + '"><i class="fa fa-edit"></i></a>';
                        }
                    }
                ],
                "aoColumns": [
                    { "mDataProp": "_usu_id" },
                    { "mDataProp": "_usu_nombre" },
                    { "mDataProp": "_usu_nom_ape" },
                    { "mDataProp": "_usu_fec_cre" },
                    { "mDataProp": "_usu_est_id" },
                    { "mDataProp": "_usu_id" },
                ],
                "aaData": data.Lista,
                "language": {
                    "sUrl": "http://cdn.datatables.net/plug-ins/1.10.7/i18n/Spanish.json"
                },
                "bDestroy": true,
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": false
            });
            jsonUsuarioTotal = data.Lista;
        },
        error: GLB_MSJ_DES_ERROR
    });
    //Recarga Tooltip
    Bata.FncCargarTooltip();
}


function Consultar(e) {
    if (e != null)
        e.preventDefault();
    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, null, Bata.TipoMensaje.Loader, null, null);
    CargarDataTable();
    Bata.FncCerrarMensaje();
}


function Limpiar(e) {
    if (e != null)
        e.preventDefault();
    Bata.FncLimpiarControl(txtId);
    Bata.FncLimpiarControl(txtUsuario);
    Bata.FncLimpiarControl(txtNombre);
    Bata.FncLimpiarControl(dtFechaDesde);
    Bata.FncLimpiarControl(selEstado);
    Bata.FncLimpiarControl(tblUsuario);
}


function LimpiarModRol() {
    Bata.FncLimpiarControl(lblUsuario);
    Bata.FncLimpiarControl(selRol);
    Bata.FncLimpiarControl(hddUsuario);
    Bata.FncLimpiarControl(tblRol);
}



function MostrarRol(id) {
    LimpiarModRol();
    CargarDataTableRol(id);
    Bata.FncLlenarControl(hddUsuario, id);
    Bata.FncLlenarControl(lblUsuario, BuscarFuncionNombre(id));
    Bata.FncMostrarModal(modRol);
}


function BuscarFuncionNombre(id) {
    var result = "";
    $.each(jsonUsuarioTotal, function (i, v) {
        if (v._usu_id === id) {
            result = v._usu_nombre;
            return false;
        }
    });
    return result;
}


function CargarRoles() {
    var request = new FormData();
    request.append('rol_id', 0);
    request.append('rol_nombre', '');
    request.append('rol_descripcion', '');

    $.ajax({
        url: GLB_RUT_APP_GETROL,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            if (data.Tipo == Bata.Notifica.Error) {
                Bata.FncMostrarAlerta(GLB_ALT_TITLE, GLB_MSJ_DES_ERROR, Bata.Notifica.Error, Bata.NotificaPos.Bottom);
            }
            if (data.Tipo == Bata.Notifica.Exito) {
                // Format Select2 Json
                var json = Bata.FncConvertirJsonSelect2(data.Lista, "rol_id", "rol_nombre");
                Bata.FncEstableceSelectJson(selRol, json);
            }
            return;
        },
        error: GLB_MSJ_DES_ERROR
    });
}


function CargarDataTableRol(id) {
    var request = new FormData();
    request.append('_usu_id', id);

    $.ajax({
        url: GLB_RUT_APP_GETUSR,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            //Validar
            if (data.Tipo == GLB_MSJ_ERROR) return;
            if (data.Lista.length < 0) return;

            // Eliminar la tabla
            Bata.FncLimpiarControl(tblRol);

            tblRol.dataTable({
                "bAutoWidth": false,
                "columnDefs": [
                    { "width": "5%", "targets": 0, "searchable": false },
                    { "width": "82%", "targets": 1 },
                    {
                        "width": "8%",
                        "targets": 2,
                        "searchable": false,
                        "orderable": false,
                        "render": function (data, type, full, meta) {
                            var id = $('<div/>').text(data).html();
                            return '<a rel="tooltip" data-toggle="tooltip" data-placement="top" title="Eliminar Rol Nro.' + id + '" class="btn btn-default" name="id[]" onclick="EliminarRol(' + id + ')" value="' + id + '"><i class="fa fa-trash"></i></a>';
                        }
                    }
                ],
                "aoColumns": [
                    { "mDataProp": "rol_id" },
                    { "mDataProp": "rol_nombre" },
                    { "mDataProp": "rol_id" }
                ],
                "aaData": data.Lista,
                "language": {
                    "sUrl": "http://cdn.datatables.net/plug-ins/1.10.7/i18n/Spanish.json"
                },
                "bDestroy": true,
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": false,
                "ordering": false
            });
        },
        error: GLB_MSJ_DES_ERROR
    });
}




function AgregarRol(e) {
    if (e != null)
        e.preventDefault();

    var valUsuId = Bata.FncObtenerValor(hddUsuario);
    var valRolId = Bata.FncObtenerValor(selRol);

    var request = new FormData();
    request.append('usu_id', valUsuId);
    request.append('rol_id', valRolId);

    $.ajax({
        url: GLB_RUT_APP_ADDUSR,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            if (data.Tipo == GLB_MSJ_ERROR) return;
            if (data.Tipo == GLB_MSJ_EXITO) CargarDataTableRol(valUsuId);
        },
        error: GLB_MSJ_DES_ERROR
    });

}



function EliminarRol(id) {

    var valUsuId = Bata.FncObtenerValor(hddUsuario);
    var valRolId = id;

    var request = new FormData();
    request.append('usu_id', valUsuId);
    request.append('rol_id', valRolId);

    $.ajax({
        url: GLB_RUT_APP_DELUSR,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            if (data.Tipo == GLB_MSJ_ERROR) return;
            if (data.Tipo == GLB_MSJ_EXITO) CargarDataTableRol(valUsuId);
        },
        error: GLB_MSJ_DES_ERROR
    });

}
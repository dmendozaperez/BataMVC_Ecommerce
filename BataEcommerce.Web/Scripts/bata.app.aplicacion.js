// =======================================================================//
// ! Declaración de Variables Globales                                    //
// =======================================================================//
var txtId, txtNombre, txtUrl, selEstado, txtOrden, btnConsultar, btnLimpiar, frmConsulta, tblAplicacion, modNuevo;
var txtNId, txtNNombre, selNTipo, txtNUrl, txtNOrden, selNEstado, txtNAyuda, txtNController, txtNAccion, txtNComentario;

$(document).ready(function () {
    // =======================================================================//
    // ! Asignar Variables Globales                                           //
    // =======================================================================//
    txtId = $("#txtId");
    txtNombre = $("#txtNombre");
    txtUrl = $("#txtUrl");
    txtOrden = $("#txtOrden");
    selEstado = $('#selEstado');
    btnConsultar = $('#btnConsultar');
    btnLimpiar = $('#btnLimpiar');
    frmConsulta = $('#frmConsulta');
    tblAplicacion = $('#tblAplicacion');
    modNuevo = $('#modNuevo');
    txtNId = $('#txtNId');
    txtNNombre = $('#txtNNombre');
    selNTipo = $('#selNTipo');
    txtNUrl = $('#txtNUrl');
    txtNOrden = $('#txtNOrden');
    selNEstado = $('#selNEstado');
    txtNAyuda = $('#txtNAyuda');
    txtNController = $('#txtNController');
    txtNAccion = $('#txtNAccion');
    txtNComentario = $('#txtNComentario');
    Iniciar();
});

// Configuracion
function Iniciar() {
    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, null, Bata.TipoMensaje.Loader, null, null);
    SetupParsley();
    CargarEstado(GLB_BDD_MOD_TODOS);
    CargarTipoAplicacion();
    CargarDataTable();
    Bata.FncCerrarMensaje();
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
                Bata.FncEstableceSelectJson(selNEstado, json);
            }
            return;
        },
        error: GLB_MSJ_DES_ERROR
    });
}


function CargarTipoAplicacion() {
    var request = new FormData();

    $.ajax({
        url: GLB_RUT_APP_GETTAP,
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
                var json = Bata.FncConvertirJsonSelect2(data.Lista, "apl_tip_id", "apl_tip_descripcion");
                Bata.FncEstableceSelectJson(selNTipo, json);
            }
            return;
        },
        error: GLB_MSJ_DES_ERROR
    });
}


function CargarDataTable() {

    var valId = (Bata.FncObtenerValor(txtId) == '') ? 0 : Bata.FncObtenerValor(txtId);
    var valNombre = Bata.FncObtenerValor(txtNombre);
    var valUrl = Bata.FncObtenerValor(txtUrl);
    var valEstId = Bata.FncObtenerValor(selEstado);
    var valOrden = (Bata.FncObtenerValor(txtOrden) == '') ? -1 : Bata.FncObtenerValor(txtOrden);

    var request = new FormData();
    request.append('apl_id', valId);
    request.append('apl_nombre', valNombre);
    request.append('apl_url', valUrl);
    request.append('apl_est_id', valEstId);
    request.append('apl_orden', valOrden);

    $.ajax({
        url: GLB_RUT_APP_GETAPP,
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
            Bata.FncLimpiarControl(tblAplicacion);

            tblAplicacion.dataTable({
                "bAutoWidth": false,
                "columnDefs": [
                    { "width": "5%", "targets": 0, "searchable": false },
                    { "width": "10%", "targets": 1 },
                    { "width": "5%", "targets": 2, "searchable": false },
                    { "width": "30%", "targets": 3 },
                    { "width": "5%", "targets": 4, "searchable": false },
                    { "width": "10%", "targets": 5, "searchable": false },
                    { "width": "10%", "targets": 6, "searchable": false },
                    { "width": "5%", "targets": 7, "searchable": false },
                    {
                        "width": "5%",
                        "targets": 8,
                        "searchable": false,
                        "orderable": false,
                        "render": function (data, type, full, meta) {
                            var id = $('<div/>').text(data).html();
                            return '<a rel="tooltip" data-toggle="tooltip" data-placement="top" title="Editar Aplicación Nro.' + id + '" class="btn btn-default" name="id[]" onclick="MostrarAplicacion(' + id + ')" value="' + id + '"><i class="fa fa-edit"></i></a>';
                        }
                    }
                ],
                "aoColumns": [
                    { "mDataProp": "apl_id" },
                    { "mDataProp": "apl_nombre" },
                    { "mDataProp": "apl_tip_id" },
                    { "mDataProp": "apl_url" },
                    { "mDataProp": "apl_orden" },
                    { "mDataProp": "apl_controller" },
                    { "mDataProp": "apl_action" },
                    { "mDataProp": "apl_est_id" },
                    { "mDataProp": "apl_id" },
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
    Bata.FncLimpiarControl(txtNombre);
    Bata.FncLimpiarControl(txtUrl);
    Bata.FncLimpiarControl(txtOrden);
    Bata.FncLimpiarControl(selEstado);
    Bata.FncLimpiarControl(tblAplicacion);
}

function MostrarAplicacion(id) {
    LimpiarModNuevo();

    if (id == null) {
        Bata.FncMostrarModal(modNuevo);
    }
    else {
        var valId = id;
        var valNombre = "";
        var valUrl = "";
        var valEstId = "";
        var valOrden = -1;

        var request = new FormData();
        request.append('apl_id', valId);
        request.append('apl_nombre', valNombre);
        request.append('apl_url', valUrl);
        request.append('apl_est_id', valEstId);
        request.append('apl_orden', valOrden);

        $.ajax({
            url: GLB_RUT_APP_GETAPP,
            processData: false,
            contentType: false,
            type: 'POST',
            async: false,
            data: request,
            success: function (data) {
                //Validar
                if (data == null || data.Lista == null || data.Lista[0] == null)
                    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, "No se encontrarón registros.", Bata.TipoMensaje.Error, null, null);
                if (data.Lista[0].apl_est_id != 'A') {
                    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, "El Registro no se encuentra activo.", Bata.TipoMensaje.Error, null, null);
                }

                var valId = data.Lista[0].apl_id;
                var valNombre = data.Lista[0].apl_nombre;
                var valTipo = data.Lista[0].apl_tip_id;
                var valUrl = data.Lista[0].apl_url;
                var valOrden = data.Lista[0].apl_orden;
                var valEstId = data.Lista[0].apl_est_id;
                var valAyuda = data.Lista[0].apl_ayuda;
                var valController = data.Lista[0].apl_controller;
                var valAccion = data.Lista[0].apl_action;
                var valComentario = data.Lista[0].apl_comentario;

                Bata.FncLlenarControl(txtNId, valId);
                Bata.FncLlenarControl(txtNNombre, valNombre);
                Bata.FncLlenarControl(selNTipo, valTipo);
                Bata.FncLlenarControl(txtNUrl, valUrl);
                Bata.FncLlenarControl(txtNOrden, valOrden);
                Bata.FncLlenarControl(selNEstado, valEstId);
                Bata.FncLlenarControl(txtNAyuda, valAyuda);
                Bata.FncLlenarControl(txtNController, valController);
                Bata.FncLlenarControl(txtNAccion, valAccion);
                Bata.FncLlenarControl(txtNComentario, valComentario);

                Bata.FncMostrarModal(modNuevo);
            },
            error: GLB_MSJ_DES_ERROR
        });
    }
}


function LimpiarModNuevo() {
    Bata.FncLimpiarControl(txtNId);
    Bata.FncLimpiarControl(txtNNombre);
    Bata.FncLimpiarControl(selNTipo);
    Bata.FncLimpiarControl(txtNUrl);
    Bata.FncLimpiarControl(txtNOrden);
    Bata.FncLimpiarControl(selNEstado);
    Bata.FncLimpiarControl(txtNAyuda);
    Bata.FncLimpiarControl(txtNController);
    Bata.FncLimpiarControl(txtNAccion);
    Bata.FncLimpiarControl(txtNComentario);
}


function Agregar() {

    var valId = Bata.FncObtenerValor(txtNId);
    var valNombre = Bata.FncObtenerValor(txtNNombre);
    var valTipo = Bata.FncObtenerValor(selNTipo);
    var valUrl = Bata.FncObtenerValor(txtNUrl);
    var valOrden = Bata.FncObtenerValor(txtNOrden);
    var valEstId = Bata.FncObtenerValor(selNEstado);
    var valAyuda = Bata.FncObtenerValor(txtNAyuda);
    var valController = Bata.FncObtenerValor(txtNController);
    var valAccion = Bata.FncObtenerValor(txtNAccion);
    var valComentario = Bata.FncObtenerValor(txtNComentario);

    var request = new FormData();
    request.append('apl_id', valId);
    request.append('apl_nombre', valNombre);
    request.append('apl_tip_id', valTipo);
    request.append('apl_url', valUrl);
    request.append('apl_orden', valOrden);
    request.append('apl_est_id', valEstId);
    request.append('apl_ayuda', valAyuda);
    request.append('apl_controller', valController);
    request.append('apl_action', valAccion);
    request.append('apl_comentario', valComentario);

    $.ajax({
        url: GLB_RUT_APP_ADDAPP,
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
                Bata.FncCerrarModal(modNuevo);
                Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, data.Mensaje, Bata.TipoMensaje.Informacion, null, 500);
            }
        },
        error: GLB_MSJ_DES_ERROR
    });
    CargarDataTable();
}

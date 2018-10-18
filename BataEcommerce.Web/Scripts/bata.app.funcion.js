// =======================================================================//
// ! Declaración de Variables Globales                                    //
// =======================================================================//
var txtId, txtNombre, txtDescripcion, selPadre, selSistema, txtOrden, btnConsultar, btnLimpiar, frmConsulta, tblFuncion, modNuevo;
var txtNId, txtNNombre, txtNDescripcion, txtNOrden, selNPadre, selNSistema;
var jsonFuncion, jsonFuncionTotal;
var modAplicacion, tblAplicacion, selAplicacion, hddFuncion, lblFuncion;



$(document).ready(function () {
    // =======================================================================//
    // ! Asignar Variables Globales                                           //
    // =======================================================================//
    txtId = $("#txtId");
    txtNombre = $("#txtNombre");
    txtDescripcion = $("#txtDescripcion");
    txtOrden = $("#txtOrden");
    selPadre = $('#selPadre');
    selSistema = $('#selSistema');
    btnConsultar = $('#btnConsultar');
    btnLimpiar = $('#btnLimpiar');
    frmConsulta = $('#frmConsulta');
    tblFuncion = $('#tblFuncion');
    modNuevo = $('#modNuevo');
    txtNId = $('#txtNId');
    txtNNombre = $('#txtNNombre');
    txtNDescripcion = $('#txtNDescripcion');
    txtNOrden = $('#txtNOrden');
    selNPadre = $('#selNPadre');
    selNSistema = $('#selNSistema');
    modAplicacion = $('#modAplicacion');
    selAplicacion = $('#selAplicacion');
    tblAplicacion = $('#tblAplicacion');
    hddFuncion = $('#hddFuncion');
    lblFuncion = $('#lblFuncion');
    Iniciar();
});

// Configuracion
function Iniciar() {
    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, null, Bata.TipoMensaje.Loader, null, null);
    SetupParsley();
    CargarPadre();
    CargarSistema();
    CargarAplicacion();
    CargarDataTable();
    Bata.FncCerrarMensaje();
}

function SetupParsley() {
    $.listen('parsley:field:validate', function () {
        Bata.FncValidateParsley(frmConsulta);
    });
}



function CargarDataTable() {

    var valId = (Bata.FncObtenerValor(txtId) == '') ? 0 : Bata.FncObtenerValor(txtId);
    var valNombre = Bata.FncObtenerValor(txtNombre);
    var valDescripcion = Bata.FncObtenerValor(txtDescripcion);
    var valOrden = (Bata.FncObtenerValor(txtOrden) == '') ? -1 : Bata.FncObtenerValor(txtOrden);
    var valPadre = (Bata.FncObtenerValor(selPadre) == '') ? -1 : Bata.FncObtenerValor(selPadre);
    var valSistema = GLB_BDD_SIS_AQUA2;

    var request = new FormData();
    request.append('fun_id', valId);
    request.append('fun_nombre', valNombre);
    request.append('fun_descripcion', valDescripcion);
    request.append('fun_orden', valOrden);
    request.append('fun_padre', valPadre);
    request.append('fun_system', valSistema);

    $.ajax({
        url: GLB_RUT_APP_GETFUN,
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
            Bata.FncLimpiarControl(tblFuncion);

            tblFuncion.dataTable({
                "bAutoWidth": false,
                "columnDefs": [
                    { "width": "5%", "targets": 0, "searchable": false },
                    { "width": "25%", "targets": 1 },
                    { "width": "26%", "targets": 2 },
                    { "width": "5%", "targets": 3, "searchable": false },
                    {
                        "width": "10%",
                        "targets": 4,
                        "searchable": false,
                        "render": function (data, type, full, meta) {
                            var id = $('<div/>').text(data).html();
                            var padre = BuscarFuncionJsonValor(jsonFuncion, id);
                            if (padre == null)
                                return '';
                            return '<span rel="tooltip" data-toggle="tooltip" data-placement="top" title="' + padre.fun_descripcion + '">' + padre.fun_nombre + "</span>";
                        }
                    },
                    {
                        "width": "10%",
                        "targets": 5,
                        "searchable": false,
                        "orderable": false,
                        "render": function (data, type, full, meta) {
                            var sis_id = $('<div/>').text(data.sis_id).html();
                            var sis_nombre = $('<div/>').text(data.sis_nombre).html();
                            var sis_descripcion = $('<div/>').text(data.sis_descripcion).html();
                            if (sis_id == '0')
                                return '';
                            return '<span rel="tooltip" data-toggle="tooltip" data-placement="top" title="' + sis_descripcion + '">' + sis_nombre + "</span>";
                        }
                    },
                    {
                        "width": "9%",
                        "targets": 6,
                        "searchable": false,
                        "orderable": false,
                        "render": function (data, type, full, meta) {
                            var id = $('<div/>').text(data).html();
                            return '<a rel="tooltip" data-toggle="tooltip" data-placement="top" title="Editar Función Nro.' + id + '" class="btn btn-default" name="id[]" onclick="MostrarFuncion(' + id + ')" value="' + id + '"><i class="fa fa-edit"></i></a>' +
                                   '<a rel="tooltip" data-toggle="tooltip" data-placement="top" title="Editar Aplicación" class="btn btn-default" name="id[]" onclick="MostrarAplicacion(' + id + ')" value="' + id + '"><i class="fa fa-gear"></i></a>';
                        }
                    }
                ],
                "aoColumns": [
                    { "mDataProp": "fun_id" },
                    { "mDataProp": "fun_nombre" },
                    { "mDataProp": "fun_descripcion" },
                    { "mDataProp": "fun_orden" },
                    { "mDataProp": "fun_padre" },
                    { "mDataProp": "fun_sistema" },
                    { "mDataProp": "fun_id" }
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
            jsonFuncionTotal = data.Lista;
        },
        error: GLB_MSJ_DES_ERROR
    });
    //Recarga Tooltip
    Bata.FncCargarTooltip();
}


function CargarAplicacion() {

    var valId = 0;
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
            if (data.Tipo == Bata.Notifica.Error) return;
            if (data.Tipo == Bata.Notifica.Exito) {
                // Format Select2 Json
                var json = Bata.FncConvertirJsonSelect2(data.Lista, "apl_id", "apl_nombre");
                Bata.FncEstableceSelectJson(selAplicacion, json);
            }
            return;
        },
        error: GLB_MSJ_DES_ERROR
    });
}

function CargarSistema() {
    var request = new FormData();

    $.ajax({
        url: GLB_RUT_APP_GETSIS,
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
                var json = Bata.FncConvertirJsonSelect2(data.Lista, "sis_id", "sis_nombre");
                Bata.FncEstableceSelectJson(selSistema, json);
                Bata.FncEstableceSelectJson(selNSistema, json);
                Bata.FncLlenarControl(selSistema, GLB_BDD_SIS_AQUA2);
                Bata.FncLlenarControl(selNSistema, GLB_BDD_SIS_AQUA2);
            }
            return;
        },
        error: GLB_MSJ_DES_ERROR
    });
}

function CargarPadre() {
    var request = new FormData();

    var valId = 0;
    var valNombre = "";
    var valDescripcion = "";
    var valOrden = -1;
    var valPadre = -99;
    //var valPadre = -1;
    var valSistema = GLB_BDD_SIS_AQUA2;

    var request = new FormData();
    request.append('fun_id', valId);
    request.append('fun_nombre', valNombre);
    request.append('fun_descripcion', valDescripcion);
    request.append('fun_orden', valOrden);
    request.append('fun_padre', valPadre);
    request.append('fun_system', valSistema);

    $.ajax({
        url: GLB_RUT_APP_GETFUN,
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
                jsonFuncion = data.Lista;
                var json = Bata.FncConvertirJsonSelect2(data.Lista, "fun_id", "fun_nombre");
                Bata.FncEstableceSelectJson(selPadre, json);
                Bata.FncEstableceSelectJson(selNPadre, json);
            }
            return;
        },
        error: GLB_MSJ_DES_ERROR
    });
}

function BuscarFuncionJsonValor(json, valor) {
    var result = null;
    $.each(json, function (i, v) {
        if (String(v.fun_id) == valor) {
            result = v;
            return false;
        }
    });
    return result;
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
    Bata.FncLimpiarControl(txtDescripcion);
    Bata.FncLimpiarControl(selPadre);
    Bata.FncLimpiarControl(txtOrden);
    Bata.FncLimpiarControl(tblFuncion);
}



function LimpiarModNuevo() {
    Bata.FncLimpiarControl(txtNId);
    Bata.FncLimpiarControl(txtNNombre);
    Bata.FncLimpiarControl(txtNDescripcion);
    Bata.FncLimpiarControl(txtNOrden);
    Bata.FncLimpiarControl(selNPadre);
}


function MostrarFuncion(id) {
    LimpiarModNuevo();

    if (id == null) {
        Bata.FncMostrarModal(modNuevo);
    }
    else {
        var valId = id;
        var valNombre = "";
        var valDescripcion = "";
        var valOrden = -1;
        var valPadre = -1;
        var valSistema = GLB_BDD_SIS_AQUA2;

        var request = new FormData();
        request.append('fun_id', valId);
        request.append('fun_nombre', valNombre);
        request.append('fun_descripcion', valDescripcion);
        request.append('fun_orden', valOrden);
        request.append('fun_padre', valPadre);
        request.append('fun_system', valSistema);

        $.ajax({
            url: GLB_RUT_APP_GETFUN,
            processData: false,
            contentType: false,
            type: 'POST',
            async: false,
            data: request,
            success: function (data) {
                //Validar
                if (data == null || data.Lista == null || data.Lista[0] == null)
                    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, "No se encontrarón registros.", Bata.TipoMensaje.Error, null, null);

                var valId = data.Lista[0].fun_id;
                var valNombre = data.Lista[0].fun_nombre;
                var valDescripcion = data.Lista[0].fun_descripcion;
                var valOrden = data.Lista[0].fun_orden;
                var valPadre = (data.Lista[0].fun_padre == 0) ? GLB_DEF_SEL_VALUE : data.Lista[0].fun_padre;
                var valSistema = data.Lista[0].fun_system;

                Bata.FncLlenarControl(txtNId, valId);
                Bata.FncLlenarControl(txtNNombre, valNombre);
                Bata.FncLlenarControl(txtNDescripcion, valDescripcion);
                Bata.FncLlenarControl(txtNOrden, valOrden);
                Bata.FncLlenarControl(selNPadre, valPadre);
                Bata.FncLlenarControl(selNSistema, valSistema);

                Bata.FncMostrarModal(modNuevo);
            },
            error: GLB_MSJ_DES_ERROR
        });
    }
}



function Agregar() {

    var valId = Bata.FncObtenerValor(txtNId);
    var valNombre = Bata.FncObtenerValor(txtNNombre);
    var valDescripcion = Bata.FncObtenerValor(txtNDescripcion);
    var valOrden = Bata.FncObtenerValor(txtNOrden);
    var valPadre = Bata.FncObtenerValor(selNPadre);
    var valSistema = GLB_BDD_SIS_AQUA2;

    var request = new FormData();
    request.append('fun_id', valId);
    request.append('fun_nombre', valNombre);
    request.append('fun_descripcion', valDescripcion);
    request.append('fun_orden', valOrden);
    request.append('fun_padre', valPadre);
    request.append('fun_system', valSistema);

    $.ajax({
        url: GLB_RUT_APP_ADDFUN,
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




function MostrarAplicacion(id) {
    LimpiarModAplicacion();
    CargarDataTableAplicacion(id);
    Bata.FncLlenarControl(hddFuncion, id);
    Bata.FncLlenarControl(lblFuncion, BuscarFuncionNombre(id));
    Bata.FncMostrarModal(modAplicacion);
}


function BuscarFuncionNombre(id) {
    var result = "";
    $.each(jsonFuncionTotal, function (i, v) {
        if (v.fun_id === id) {
            result = v.fun_nombre;
            return false;
        }
    });
    return result;
}

function LimpiarModAplicacion() {
    Bata.FncLimpiarControl(selAplicacion);
    Bata.FncLimpiarControl(tblAplicacion);
    Bata.FncLimpiarControl(hddFuncion);
    Bata.FncLimpiarControl(lblFuncion);
}

function AgregarAplicacion(e) {
    if (e != null)
        e.preventDefault();

    var valFunId = Bata.FncObtenerValor(hddFuncion);
    var valAplId = Bata.FncObtenerValor(selAplicacion);

    var request = new FormData();
    request.append('fun_id', valFunId);
    request.append('apl_id', valAplId);

    $.ajax({
        url: GLB_RUT_APP_ADDFUA,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            if (data.Tipo == GLB_MSJ_ERROR) return;
            if (data.Tipo == GLB_MSJ_EXITO) CargarDataTableAplicacion(valFunId);
        },
        error: GLB_MSJ_DES_ERROR
    });
    
}


function CargarDataTableAplicacion(id) {
    var request = new FormData();
    request.append('fun_id', id);

    $.ajax({
        url: GLB_RUT_APP_GETFUA,
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
            Bata.FncLimpiarControl(tblAplicacion);

            tblAplicacion.dataTable({
                "bAutoWidth": false,
                "columnDefs": [
                    { "width": "5%", "targets": 0, "searchable": false },
                    { "width": "82%", "targets": 1 },
                    { "width": "5%", "targets": 2 },
                    {
                        "width": "8%",
                        "targets": 3,
                        "searchable": false,
                        "orderable": false,
                        "render": function (data, type, full, meta) {
                            var id = $('<div/>').text(data).html();
                            return '<a rel="tooltip" data-toggle="tooltip" data-placement="top" title="Eliminar Aplicación Nro.' + id + '" class="btn btn-default" name="id[]" onclick="EliminarAplicacion(' + id + ')" value="' + id + '"><i class="fa fa-trash"></i></a>';
                        }
                    }
                ],
                "aoColumns": [
                    { "mDataProp": "apl_id" },
                    { "mDataProp": "apl_nombre" },
                    { "mDataProp": "apl_orden" },
                    { "mDataProp": "apl_id" }
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

function EliminarAplicacion(id) {

    var valFunId = Bata.FncObtenerValor(hddFuncion);
    var valAplId = id;

    var request = new FormData();
    request.append('fun_id', valFunId);
    request.append('apl_id', valAplId);

    $.ajax({
        url: GLB_RUT_APP_DELFUA,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            if (data.Tipo == GLB_MSJ_ERROR) return;
            if (data.Tipo == GLB_MSJ_EXITO) CargarDataTableAplicacion(valFunId);
        },
        error: GLB_MSJ_DES_ERROR
    });

}
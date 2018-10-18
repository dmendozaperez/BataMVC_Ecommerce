// =======================================================================//
// ! Declaración de Variables Globales                                    //
// =======================================================================//
var txtId, txtNombre, txtDescripcion, btnConsultar, btnLimpiar, frmConsulta, tblRol, modNuevo;
var txtNId, txtNNombre, txtNDescripcion;
var jsonRol;
var modFuncion, tblFuncion, selFuncion, hddRol, lblRol;

$(document).ready(function () {
    // =======================================================================//
    // ! Asignar Variables Globales                                           //
    // =======================================================================//
    txtId = $("#txtId");
    txtNombre = $("#txtNombre");
    txtDescripcion = $("#txtDescripcion");
    btnConsultar = $('#btnConsultar');
    btnLimpiar = $('#btnLimpiar');
    frmConsulta = $('#frmConsulta');
    tblRol = $('#tblRol');
    modNuevo = $('#modNuevo');
    txtNId = $('#txtNId');
    txtNNombre = $('#txtNNombre');
    txtNDescripcion = $('#txtNDescripcion');
    modFuncion = $('#modFuncion');
    tblFuncion = $('#tblFuncion');
    selFuncion = $('#selFuncion');
    hddRol = $('#hddRol');
    lblRol = $('#lblRol');
    Iniciar();
});

// Configuracion
function Iniciar() {
    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, null, Bata.TipoMensaje.Loader, null, null);
    SetupParsley();
    CargarDataTable();
    CargarFuncion();
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

    var request = new FormData();
    request.append('rol_id', valId);
    request.append('rol_nombre', valNombre);
    request.append('rol_descripcion', valDescripcion);

    $.ajax({
        url: GLB_RUT_APP_GETROL,
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
            Bata.FncLimpiarControl(tblRol);

            tblRol.dataTable({
                "bAutoWidth": false,
                "columnDefs": [
                    { "width": "5%", "targets": 0, "searchable": false },
                    { "width": "40%", "targets": 1 },
                    { "width": "45%", "targets": 2 },
                    {
                        "width": "10%",
                        "targets": 3,
                        "searchable": false,
                        "orderable": false,
                        "render": function (data, type, full, meta) {
                            var id = $('<div/>').text(data).html();
                            return '<a rel="tooltip" data-toggle="tooltip" data-placement="top" title="Editar Rol Nro.' + id + '" class="btn btn-default" name="id[]" onclick="MostrarRol(' + id + ')" value="' + id + '"><i class="fa fa-edit"></i></a>' + 
                                   '<a rel="tooltip" data-toggle="tooltip" data-placement="top" title="Asignar Función" class="btn btn-default" name="id[]" onclick="MostrarFuncion(' + id + ')" value="' + id + '"><i class="fa fa-gear"></i></a>';
                        }
                    }
                ],
                "aoColumns": [
                    { "mDataProp": "rol_id" },
                    { "mDataProp": "rol_nombre" },
                    { "mDataProp": "rol_descripcion" },
                    { "mDataProp": "rol_id" }
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
            jsonRol = data.Lista;
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
    Bata.FncLimpiarControl(txtDescripcion);
    Bata.FncLimpiarControl(tblRol);
}

function MostrarRol(id) {
    LimpiarModNuevo();

    if (id == null) {
        Bata.FncMostrarModal(modNuevo);
    }
    else {
        var valId = id;
        var valNombre = "";
        var valDescripcion = "";

        var request = new FormData();
        request.append('rol_id', valId);
        request.append('rol_nombre', valNombre);
        request.append('rol_url', valDescripcion);

        $.ajax({
            url: GLB_RUT_APP_GETROL,
            processData: false,
            contentType: false,
            type: 'POST',
            async: false,
            data: request,
            success: function (data) {
                //Validar
                if (data == null || data.Lista == null || data.Lista[0] == null)
                    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, "No se encontrarón registros.", Bata.TipoMensaje.Error, null, null);
            
                var valId = data.Lista[0].rol_id;
                var valNombre = data.Lista[0].rol_nombre;
                var valDescripcion = data.Lista[0].rol_descripcion;

                Bata.FncLlenarControl(txtNId, valId);
                Bata.FncLlenarControl(txtNNombre, valNombre);
                Bata.FncLlenarControl(txtNDescripcion, valDescripcion);

                Bata.FncMostrarModal(modNuevo);
            },
            error: GLB_MSJ_DES_ERROR
        });
    }
}


function LimpiarModNuevo() {
    Bata.FncLimpiarControl(txtNId);
    Bata.FncLimpiarControl(txtNNombre);
    Bata.FncLimpiarControl(txtNDescripcion);
}


function Agregar() {
    var valId = Bata.FncObtenerValor(txtNId);
    var valNombre = Bata.FncObtenerValor(txtNNombre);
    var valDescripcion = Bata.FncObtenerValor(txtNDescripcion);

    var request = new FormData();
    request.append('rol_id', valId);
    request.append('rol_nombre', valNombre);
    request.append('rol_descripcion', valDescripcion);

    $.ajax({
        url: GLB_RUT_APP_ADDROL,
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


function MostrarFuncion(id) {
    LimpiarModFuncion();
    CargarDataTableFuncion(id);
    Bata.FncLlenarControl(hddRol, id);
    Bata.FncLlenarControl(lblRol, BuscarRolNombre(id));
    Bata.FncMostrarModal(modFuncion);
}

function BuscarRolNombre(id) {
    var result = "";
    $.each(jsonRol, function (i, v) {
        if (v.rol_id === id) {
            result = v.rol_nombre;
            return false;
        }
    });
    return result;
}

function LimpiarModFuncion() {
    Bata.FncLimpiarControl(selFuncion);
    Bata.FncLimpiarControl(tblFuncion);
    Bata.FncLimpiarControl(hddRol);
    Bata.FncLimpiarControl(lblRol);
}



function AgregarFuncion(e) {
    if (e != null)
        e.preventDefault();

    var valRolId = Bata.FncObtenerValor(hddRol);
    var valFunId = Bata.FncObtenerValor(selFuncion);

    var request = new FormData();
    request.append('rol_id', valRolId);
    request.append('fun_id', valFunId);

    $.ajax({
        url: GLB_RUT_APP_ADDROF,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            if (data.Tipo == GLB_MSJ_ERROR) return;
            if (data.Tipo == GLB_MSJ_EXITO) CargarDataTableFuncion(valRolId);
        },
        error: GLB_MSJ_DES_ERROR
    });
}


function CargarDataTableFuncion(id) {
    var request = new FormData();
    request.append('rol_id', id);

    $.ajax({
        url: GLB_RUT_APP_GETROF,
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
            Bata.FncLimpiarControl(tblFuncion);

            tblFuncion.dataTable({
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
                            return '<a rel="tooltip" data-toggle="tooltip" data-placement="top" title="Eliminar Aplicación Nro.' + id + '" class="btn btn-default" name="id[]" onclick="EliminarFuncion(' + id + ')" value="' + id + '"><i class="fa fa-trash"></i></a>';
                        }
                    }
                ],
                "aoColumns": [
                    { "mDataProp": "fun_id" },
                    { "mDataProp": "fun_nombre" },
                    { "mDataProp": "fun_orden" },
                    { "mDataProp": "fun_id" }
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


function EliminarFuncion(id) {

    var valRolId = Bata.FncObtenerValor(hddRol);
    var valFunId = id;

    var request = new FormData();
    request.append('rol_id', valRolId);
    request.append('fun_id', valFunId);

    $.ajax({
        url: GLB_RUT_APP_DELROF,
        processData: false,
        contentType: false,
        type: 'POST',
        async: false,
        data: request,
        success: function (data) {
            if (data.Tipo == GLB_MSJ_ERROR) return;
            if (data.Tipo == GLB_MSJ_EXITO) CargarDataTableFuncion(valRolId);
        },
        error: GLB_MSJ_DES_ERROR
    });

}



function CargarFuncion() {

    var valId = 0;
    var valNombre = '';
    var valDescripcion = '';
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
            if (data.Tipo == Bata.Notifica.Error) return;
            if (data.Tipo == Bata.Notifica.Exito) {
                // Format Select2 Json
                var json = Bata.FncConvertirJsonSelect2(data.Lista, "fun_id", "fun_nombre");
                Bata.FncEstableceSelectJson(selFuncion, json);
            }
            return;
        },
        error: GLB_MSJ_DES_ERROR
    });
}
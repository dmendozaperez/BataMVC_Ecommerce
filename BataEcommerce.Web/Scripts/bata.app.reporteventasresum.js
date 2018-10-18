// =======================================================================//
// ! Declaración de Variables Globales                                    //
// =======================================================================//
var dtAno, btnConsultar, btnLimpiar, ifrReporte, frmConsulta;

$(document).ready(function () {
    // =======================================================================//
    // ! Asignar Variables Globales                                           //
    // =======================================================================//
    txtAno = $("#txtAno");
    btnConsultar = $('#btnConsultar');
    btnLimpiar = $('#btnLimpiar');
    ifrReporte = $('#ifrReporte');
    frmConsulta = $('#frmConsulta');
    Iniciar();
});

// Configuracion
function Iniciar() {
    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, null, Bata.TipoMensaje.Loader, null, null);
    //CargarFecha();
    //CargarEstado();
    SetupParsley();
    Bata.FncCerrarMensaje();
}

function SetupParsley() {
    $.listen('parsley:field:validate', function () {
        Bata.FncValidateParsley(frmConsulta);
    });
}


//function CargarFecha() {
//    // Cargar DateRangeTime
//    dtFechaDesde.daterangepicker({
//        singleDatePicker: true,
//        showDropdowns: true,
//        calender_style: "picker_1",
//        format: 'DD/MM/YYYY',
//        minDate: moment("1900-01-01T00:00:00"),
//        maxDate: moment(),
//        ignoreReadonly: true
//    });

//    dtFechaHasta.daterangepicker({
//        singleDatePicker: true,
//        showDropdowns: true,
//        calender_style: "picker_1",
//        format: 'DD/MM/YYYY',
//        minDate: moment("1900-01-01T00:00:00"),
//        maxDate: moment(),
//        ignoreReadonly: true
//    });
//}

//function CargarEstado() {
//    // Cargar Selects
//    var json = [{ id: GLB_DEF_SEL_VALUE, text: GLB_DEF_SEL_VALUE }, { id: 'PF', text: 'Pedientes' }, { id: 'PDE', text: 'Facturados' }]
//    Bata.FncEstableceSelectJson(selEstado, json);
//}

function MostrarReporte(e) {
    e.preventDefault();

    // Validar FrontEnd
    if (frmConsulta.parsley().validate() === false)
        return;

    // Validar Params
    var pAno = $.trim(txtAno.val());

    Bata.FncDeshabilitarControl(btnConsultar);
    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, null, Bata.TipoMensaje.Loader, null, null);

    $.ajax({
        url: GLB_RUT_APP_REPVTRR + "?pAno=" + pAno,
        cache: false,
        async: false,
        dataType: "html",
        success: function (data) {
            ifrReporte.html(data);
            return false;
        },
        error: function (request, status, error) {
            Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, GLB_MSJ_DES_ERROR, Bata.TipoMensaje.Informacion, null, null);
        }
    }).done(function () {
        Bata.FncCerrarMensaje();
    });

    Bata.FncHabilitarControl(btnConsultar);
}

function Limpiar(e) {
    e.preventDefault();
    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, null, Bata.TipoMensaje.Loader, null, null);
    Bata.FncLlenarControl(txtAno, '');
    Bata.FncCerrarMensaje();
}




// =======================================================================//
// ! Declaración de Variables Globales                                    //
// =======================================================================//
var dtFechaDesde, dtFechaHasta, txtCliente, selEstado, btnConsultar, btnLimpiar, ifrReporte, frmConsulta;

$(document).ready(function () {
    // =======================================================================//
    // ! Asignar Variables Globales                                           //
    // =======================================================================//
    txtCliente = $("#txtCliente");
    dtFechaDesde = $("#dtFechaDesde");
    dtFechaHasta = $('#dtFechaHasta');
    selEstado = $('#selEstado');
    btnConsultar = $('#btnConsultar');
    btnLimpiar = $('#btnLimpiar');
    ifrReporte = $('#ifrReporte');
    frmConsulta = $('#frmConsulta');
    Iniciar();
});

// Configuracion
function Iniciar() {
    Bata.FncMostrarMensaje(GLB_INF_APP_NOMBRE, null, Bata.TipoMensaje.Loader, null, null);
    CargarFecha();
    CargarEstado('');
    SetupParsley();
    Bata.FncCerrarMensaje();
}

function SetupParsley() {
    $.listen('parsley:field:validate', function () {
        Bata.FncValidateParsley(frmConsulta);
    });
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

    dtFechaHasta.daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        calender_style: "picker_1",
        format: 'DD/MM/YYYY',
        minDate: moment("1900-01-01T00:00:00"),
        maxDate: moment(),
        ignoreReadonly: true
    });
}

function CargarEstado(ordnom) {
    var request = new FormData();
    request.append('ordnom', ordnom);

    $.ajax({
        url: GLB_RUT_APP_GETORS,
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
                var json = Bata.FncConvertirJsonSelect2(data.Lista, "Nombre", "Nombre");
                Bata.FncEstableceSelectJson(selEstado, json);
            }
            return;
        },
        error: GLB_MSJ_DES_ERROR
    });
}

function MostrarReporte(e) {
    e.preventDefault();

    // Validar FrontEnd
    if (frmConsulta.parsley().validate() === false)
        return;

    // Validar Params
    var pFecDes = $.trim(dtFechaDesde.val());
    var pFecHas = $.trim(dtFechaHasta.val());
    var pEst = $.trim(selEstado.val());
    var pCli = $.trim(txtCliente.val());

    pFecDes = (pFecDes == '') ? pFecDes : Bata.FncConvertirFechaDB(pFecDes);
    pFecHas = (pFecHas == '') ? pFecHas : Bata.FncConvertirFechaDB(pFecHas);
    pEst = (pEst == GLB_DEF_SEL_VALUE) ? '' : pEst;

    Bata.FncDeshabilitarControl(btnConsultar);
    Bata.FncMostrarMensaje(GLB_WIN_TITLE, null, Bata.TipoMensaje.Loader, null, null);

    $.ajax({
        url: GLB_RUT_APP_REPPEDR + "?pFecDes=" + pFecDes + "&pFecHas=" + pFecHas + "&pEst=" + pEst + "&pCli=" + pCli,
        cache: false,
        async: false,
        dataType: "html",
        success: function (data) {
            ifrReporte.html(data);
            return false;
        },
        error: function (request, status, error) {
            Bata.FncMostrarMensaje(GLB_WIN_TITLE, GLB_MSJ_DES_ERROR, Bata.TipoMensaje.Informacion, null, null);
        }
    }).done(function () {
        Bata.FncCerrarMensaje();
    });

    Bata.FncHabilitarControl(btnConsultar);
}

function Limpiar(e) {
    e.preventDefault();
    Bata.FncMostrarMensaje(GLB_WIN_TITLE, null, Bata.TipoMensaje.Loader, null, null);
    Bata.FncLlenarControl(txtCliente, '');
    Bata.FncLlenarControl(dtFechaDesde, '');
    Bata.FncLlenarControl(dtFechaHasta, '');
    Bata.FncLlenarControl(selEstado, GLB_DEF_SEL_VALUE);
    Bata.FncCerrarMensaje();
}




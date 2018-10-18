// =======================================================================//
// ! Declaración de Variables Globales                                    //
// =======================================================================//
var txtProCod, txtProNom, selCat, txtTalla, ddbSigno, btnSigno, hddSigno, lblBtnSigno, txtVenta, btnConsultar, btnLimpiar, ifrReporte, frmConsulta;

$(document).ready(function () {
    // =======================================================================//
    // ! Asignar Variables Globales                                           //
    // =======================================================================//
    txtProCod = $("#txtProCod");
    txtProNom = $("#txtProNom");
    selCat = $('#selCategoria');
    txtTalla = $('#txtTalla');
    ddbSigno = $('#ddbSigno');
    btnSigno = $('#btnSigno');
    lblBtnSigno = 'Signo <span class="caret"></span>';
    hddSigno = $('#hddSigno');
    txtVenta = $('#txtVenta');
    btnConsultar = $('#btnConsultar');
    btnLimpiar = $('#btnLimpiar');
    ifrReporte = $('#ifrReporte');
    frmConsulta = $('#frmConsulta');
    Iniciar();
});


function Iniciar() {
    Bata.FncMostrarMensaje(GLB_WIN_TITLE, null, Bata.TipoMensaje.Loader, null, null);
    CargarBtnSigno();
    CargarSigno(); // Carga Todas las Cat.
    CargarCategoria('');
    SeleccionSigno();
    SetupParsley();
    Bata.FncCerrarMensaje();
}

function SetupParsley() {
    $.listen('parsley:field:validate', function () {
        Bata.FncValidateParsley(frmConsulta);
    });
}


function CargarBtnSigno() {
    btnSigno.html(lblBtnSigno);
}


function CargarSigno() {
    // Cargar Selects
    var json = [
                    { id: '=', text: 'Igual' },
                    { id: '>', text: 'Mayor' },
                    { id: '<', text: 'Menor' },
                    { id: '>=', text: 'Mayor Igual' },
                    { id: '<=', text: 'Menor Igual' }
    ];
    Bata.FncLlenarDropDownButon(ddbSigno, json);
}

function CambiarSigno(e) {
    e.preventDefault();
}

function SeleccionSigno() {
    $("#ddbSigno li").click(function () {
        btnSigno.html($(this).text() + ' <span class="caret"></span>');
        hddSigno.val($(this).attr('value'));
        txtVenta.focus();
    });
}


function CargarCategoria(catnom) {
    var request = new FormData();
    request.append('catnom', catnom);

    $.ajax({
        url: GLB_RUT_APP_GETCAT,
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
                Bata.FncEstableceSelectJson(selCat, json);
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
    var pProCod = $.trim(txtProCod.val());
    var pProNom = $.trim(txtProNom.val());
    var pTalla = $.trim(txtTalla.val());
    var pVenta = $.trim(txtVenta.val());
    var pCateg = $.trim(selCat.val());
    var pSigno = $.trim(hddSigno.val());

    pCateg = (pCateg == GLB_DEF_SEL_VALUE) ? '' : pCateg;
    pSigno = (pSigno == GLB_DEF_SEL_VALUE) ? '' : pSigno;

    Bata.FncDeshabilitarControl(btnConsultar);
    Bata.FncMostrarMensaje(GLB_WIN_TITLE, null, Bata.TipoMensaje.Loader, null, null);

    $.ajax({
        url: GLB_RUT_APP_REPRNKR + "?pProcod=" + pProCod + "&pPronom=" + pProNom + "&pCateg=" + pCateg + "&pTalla=" + pTalla + "&pSigno=" + pSigno + "&pVenta=" + pVenta,
        cache: false,
        async: false,
        dataType: "html",
        success: function (data) {
            ifrReporte.html(data);
            return false;
        },
        error: function (request, status, error) {
            Bata.FncMostrarMensaje(GLB_WIN_TITLE, GLB_MSJ_DES_ERROR, Bata.TipoMensaje.Error, null, null);
        }
    }).done(function () {
        Bata.FncCerrarMensaje();
    });

    Bata.FncHabilitarControl(btnConsultar);
}

function Limpiar(e) {
    e.preventDefault();
    Bata.FncMostrarMensaje(GLB_WIN_TITLE, null, Bata.TipoMensaje.Loader, null, null);
    Bata.FncLlenarControl(txtProCod, '');
    Bata.FncLlenarControl(txtProNom, '');
    Bata.FncLlenarControl(txtTalla, '');
    Bata.FncLlenarControl(txtVenta, '');
    Bata.FncLlenarControl(hddSigno, '');
    CargarBtnSigno();
    Bata.FncLlenarControl(selCat, GLB_DEF_SEL_VALUE);
    Bata.FncCerrarMensaje();
}

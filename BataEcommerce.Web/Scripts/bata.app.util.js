// =======================================================================//
// ! Declaración de Namespace Global                                      //
// =======================================================================//
Bata = {
    Notifica: {
        Defecto: GLB_MSJ_DEFECTO,
        Exito: GLB_MSJ_EXITO,
        Advertencia: GLB_MSJ_ADVERTENCIA,
        Error: GLB_MSJ_ERROR
    },
    NotificaPos: {
        Top: GLB_MSJ_POS_TOP,
        Bottom: GLB_MSJ_POS_BOTTOM
    },
    TipoMensaje: {
        Loader: GLB_WIN_LOADER,
        Informacion: GLB_WIN_INFORMACION,
        SiNo: GLB_WIN_SINO,
        Advertencia: GLB_WIN_ADVERTENCIA,
        Error: GLB_WIN_ERROR
    },
    FncRedirigePost: function (url, params) {
        var form = $('<form></form>');
        form.attr("method", "post");
        form.attr("action", url);
        $.each(params, function (key, value) {
            var field = $('<input></input>');
            field.attr("type", "hidden");
            field.attr("name", key);
            field.attr("value", value);
            form.append(field);
        });
        $(form).appendTo('body').submit();
    },
    FncObtenerNumeroAleatorio: function () {
        var n = Math.random() * 100000;
        var str = n.toString().substring(0, 5);
        return str;
    },
    FncIngresarEntero: function (e) {
        var key = e.which ? e.which : e.keyCode;
        if (e.keyCode === 8 || e.keyCode === 9 || e.keyCode === 46) {
            return true;
        } else if (key < 48 || key > 57) {
            return false;
        } else {
            return true;
        }
    },
    FncIngresarAlfanumerico: function (e) {
        var key = e.which ? e.which : e.keyCode;
        if (e.keyCode === 8 || e.keyCode === 9 || e.keyCode === 46) {
            return true;
        } else if ((key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122)) {
            return true;
        } else {
            return false;
        }
    },
    FncMostrarAlerta: function (titulo, mensaje, tipo, posicion) {
        var ptype = '';
        var pstack = null;
        var pstackcss = '';
        var picon = '';

        // Validar PNotify

        switch (tipo) {
            case Bata.Notifica.Defecto:
                ptype = 'info';
                picon = 'pnotify-icon information';
                break;
            case Bata.Notifica.Exito:
                ptype = 'success';
                picon = 'pnotify-icon success';
                break;
            case Bata.Notifica.Advertencia:
                ptype = 'notice';
                picon = 'pnotify-icon warning';
                break;
            case Bata.Notifica.Error:
                ptype = 'error';
                picon = 'pnotify-icon error';
                break;
            default:
                ptype = 'info';
                picon = 'pnotify-icon information';
                break;
        }


        switch (posicion) {
            case Bata.NotificaPos.Top:
                pstack = { "dir1": "down", "dir2": "right", "push": "top", "spacing1": 0, "spacing2": 0, "firstpos1": 0, "firstpos2": 0 };
                pstackcss = "stack-bar-top";
                break;
            case Bata.NotificaPos.Bottom:
                pstack = { "dir1": "up", "dir2": "right", "push": "bottom", "spacing1": 0, "spacing2": 0, "firstpos1": 0, "firstpos2": 0 };
                pstackcss = "stack-bottomright";
                break;
            default:
                pstack = { "dir1": "down", "dir2": "right", "push": "top", "spacing1": 0, "spacing2": 0, "firstpos1": 0, "firstpos2": 0 };
                pstackcss = "stack-bar-top";
                break;
        }

        new PNotify({
            title: titulo,
            type: ptype,
            text: mensaje,
            nonblock: {
                nonblock: true,
                nonblock_opacity: .9
            },
            styling: 'bootstrap3',
            //addclass: 'translucent stack-bar-bottom',
            addclass: pstackcss,
            //icon: true,
            icon: picon,
            opacity: 1,
            hide: true,
            shadow: false,
            delay: 9999999,
            mouse_reset: false,
            history: false,
            width: "100%",
            stack: pstack,
            remove: true,
            before_open: function (a) {
                PNotify.removeAll();
                return true;
            }
        });

        return;
    },
    FncMostrarMensaje: function (titulo, mensaje, tipo, funcion, delay) {
        // Configurar Mensaje
        var footer = '';
        var icono = '';
        var iconoClass = '';
        var time = (delay == null) ? 0 : delay;

        // 1 = Loader
        // 2 = Information
        // 3 = Yes/No
        switch (tipo) {
            case Bata.TipoMensaje.Loader:
                mensaje = '<div class="text-center">Procesando....<br/><img src="' + GLB_GIF_LOADER + '" alt="" style="width: 42px; height: 42px;"></div>';
                footer = '';
                break;
            case Bata.TipoMensaje.Informacion:
                footer = '<button type="button" class="btn btn-primary" data-dismiss="modal">Aceptar</button>';
                icono = '<i class="material-icons md-36 text-primary">&#xE88E;</i>&nbsp;&nbsp;';
                iconoClass = 'hd-info';
                break;
            case Bata.TipoMensaje.SiNo:
                footer = '<button type="button" class="btn btn-primary" onClick="' + funcion + '"><i class="material-icons">&#xE86C;</i> Aceptar</button><button type="button" class="btn" data-dismiss="modal">Cancelar</button>';
                break;
            case Bata.TipoMensaje.Advertencia:
                footer = '<button type="button" class="btn btn-primary" data-dismiss="modal">Aceptar</button>';
                icono = '<i class="material-icons md-36 text-primary">&#xE002;</i>&nbsp;&nbsp;';
                iconoClass = 'hd-warning';
                break;
            case Bata.TipoMensaje.Error:
                footer = '<button type="button" class="btn btn-primary" data-dismiss="modal">Aceptar</button>';
                icono = '<i class="material-icons md-36 text-primary">&#xE000;</i>&nbsp;&nbsp;';
                iconoClass = 'hd-error';
                break;
        }

        var modal = '';
        modal = modal.concat('<div class="modal fade ' + iconoClass + '" id="modMensaje">');
        modal = modal.concat('<div class="modal-dialog">');
        modal = modal.concat('<div class="modal-content">');
        modal = modal.concat('<div class="modal-header">');
        modal = modal.concat('<h4 class="modal-title">' + icono + '<span style="vertical-align:middle;">' + titulo + '</span></h4>');
        modal = modal.concat('</div>');
        modal = modal.concat('<div class="modal-body">');
        modal = modal.concat(mensaje);
        modal = modal.concat('</div>');
        modal = modal.concat('<div class="modal-footer text-right">');
        modal = modal.concat(footer);
        modal = modal.concat('</div>');
        modal = modal.concat('</div>');
        modal = modal.concat('</div>');
        modal = modal.concat('</div>');
        // Eliminar y Agregar Modal
        $('#modMensaje').remove();
        $('#pageWrapper').after(modal);

        var modMensaje = $("#modMensaje");
        setTimeout(function () {
            Bata.FncMostrarModal(modMensaje);
        }, time);
    },
    FncCerrarMensaje: function () {
        var modMensaje = $("#modMensaje");
        setTimeout(function () {
            if ($('body').hasClass('modal-open')) {
                modMensaje.modal('hide').data('bs.modal', null);
            }
        }, 3000);
    },
    FncMostrarModal: function (mod) {
        if ($('body').hasClass('modal-open')) {
            mod.modal('hide').data('bs.modal', null);
        } else {
            mod.modal({
                backdrop: 'static',
                keyboard: false,
                show: true
            });
        }
    },
    FncCerrarModal: function (modal) {
        modal.modal('hide').data('bs.modal', null);
    },
    FncEnviarFormEnter: function (form, boton) {
        $(form).keyup(function (e) {
            e.preventDefault();
            if (e.keyCode === 13) {
                $(boton).click();
            }
        });
        return;
    },
    FncEstableceFoco: function (control) {
        control.focus();
        return;
    },
    FncEstableceSelectJson: function (ctr, json) {

        if (ctr.data('select2')) {
            ctr.select2('destroy');
            ctr.empty();
        }


        ctr.select2({
            tags: false,
            multiple: false,
            allowClear: false,
            data: json,
            width: '100%'
        });
    },
    FncEsVacioNulo: function (value) {
        return (value === null || value.length === 0);
    },
    FncEstableceMenuActual: function (value) {
        var li = $(".menu_section li:contains('" + value + "')");
        if (li.length === 1) {
            li.addClass("current_section");
        }
        if (li.length === 2) {
            li.addClass("act_item");
        }
        return;
    },
    TipoEventoControl: {
        Click: 1
    },
    FncEstableceEventoControl: function (control, evento, funcion) {
        switch (evento) {
            case Bata.TipoEventoControl.Click:
                control.click(function () {
                    funcion();
                });
                break;
        }
    },
    FncEstableceFormatoFecha: function (fecha) {
        var ano = fecha.substr(0, 4);
        var mes = fecha.substr(5, 2);
        var dia = fecha.substr(8, 2);
        var fecha = dia + "/" + mes + "/" + ano;
        if (fecha === '01/01/1900') {
            return 'Sin Datos';
        } else {
            return fecha;
        }
    },
    FncEstableceFormatoFechaCompleto: function (fecha) {
        var ano = fecha.substr(0, 4);
        var mes = fecha.substr(5, 2);
        var dia = fecha.substr(8, 2);
        var hora = fecha.substr(11, 2);
        var min = fecha.substr(14, 2);
        var seg = fecha.substr(17, 2);
        var fecha = dia + "/" + mes + "/" + ano + " " + hora + ":" + min + ":" + seg;
        if (fecha === '01/01/1900 00:00:00') {
            return 'Sin Datos';
        } else {
            return fecha;
        }
    },
    TipoPad: {
        Left: 1,
        Right: 1
    },
    FncPadCaracter: function (pad, str, pos) {
        var val = null;
        if (typeof str === 'undefined') {
            val = pad;
        }
        if (pos === Bata.TipoPad.Left) {
            val = (str + pad).substring(0, pad.length);
        }
        if (pos === Bata.TipoPad.Right) {
            val = (pad + str).slice(-pad.length);
        }
        return val;
    },
    TipoCadena: {
        Numero: 1,
        Boolean: 2
    },
    FncConvertirCadenaTipo: function (str, tipo) {
        var val = null;
        if (tipo === Bata.TipoCadena.Numero) {
            val = Number(str);
        }
        if (tipo === Bata.TipoCadena.Boolean) {
            if (str === "true") {
                val = true;
            }
            if (str === "false") {
                val = false;
            }
        }
        return val;
    },
    FncConvertirFechaDB: function (str) {
        var fec = str.split('/');
        return fec[2] + "-" + fec[1] + "-" + fec[0];
    },
    FncHabilitarControl: function (ctrl) {
        if (ctrl.is('input')) {
            ctrl.prop('disabled', false);
            return;
        }
    },
    FncDeshabilitarControl: function (ctrl) {
        if (ctrl.is('input')) {
            ctrl.prop('disabled', true);
            return;
        }
    },
    FncLimpiarFormulario: function (frm) {
        frm[0].reset();
    },
    FncObtenerValor: function (ctrl) {
        if (ctrl.is('input') && ctrl.attr('type') === 'text') {
            return ctrl.val();
        }
        if (ctrl.is('textarea')) {
            return ctrl.val();
        }
        if (ctrl.is('input') && ctrl.attr('type') === 'hidden') {
            return ctrl.val();
        }
        if (ctrl.is('select')) {
            return (ctrl.val() == GLB_DEF_SEL_VALUE) ? "" : ctrl.val();
        }
    },
    FncLimpiarControl: function (ctrl) {
        if (ctrl.is('input') && ctrl.attr('type') === 'text') {
            ctrl.val('');
            ctrl.parent().removeClass('md-input-filled');
            return;
        }
        if (ctrl.is('input') && ctrl.attr('type') === 'hidden') {
            ctrl.val('');
            return;
        }
        if (ctrl.is('input') && ctrl.attr('type') === 'file') {
            ctrl.val('');
            return;
        }
        if (ctrl.is('textarea')) {
            ctrl.val('');
            return;
        }
        if (ctrl.is('input') && ctrl.attr('type') === 'radio') {
            ctrl.iCheck('uncheck');
            ctrl.iCheck('update');
            return;
        }
        if (ctrl.is('select')) {
            ctrl.val(GLB_DEF_SEL_VALUE).select2();
            return;
        }
        if (Bata.FncEsDataTable(ctrl)) {
            if (Bata.FncEsDataTable(ctrl)) {
                ctrl.DataTable().clear();
                ctrl.DataTable().draw();
            }
            return;
        }
        if (ctrl.is('span')) {
            ctrl.text('');
            return;
        }
    },
    FncLlenarControl: function (ctrl, value) {
        if (ctrl.is('input') && ctrl.attr('type') === 'text') {
            ctrl.val(value);
            ctrl.parent().addClass('md-input-filled');
            return;
        }
        if (ctrl.is('textarea')) {
            ctrl.val(value);
            ctrl.parent().addClass('md-input-filled');
            return;
        }
        if (ctrl.is('input') && ctrl.attr('type') === 'hidden') {
            ctrl.val(value);
            return;
        }
        if (ctrl.is('input') && ctrl.attr('type') === 'radio') {
            ctrl.iCheck('check');
            ctrl.iCheck('update');
            return;
        }
        if (ctrl.is('label')) {
            ctrl.text(value);
            return;
        }
        if (ctrl.is('button')) {
            ctrl.html(value);
            return;
        }
        if (ctrl.is('img')) {
            ctrl.attr("src", value);
            return;
        }
        if (ctrl.is('select')) {
            ctrl.val(value).trigger('change');
            return;
        }
        if (ctrl.is('span')) {
            ctrl.text(value);
            return;
        }

    },
    FncLlenarRadio: function (ctrl, value, opciones) {
        opciones.forEach(function (item) {
            if (value === item.valor) {
                Bata.FncLlenarControl(item.control, null);
                Bata.FncLlenarControl(ctrl, value);
                return;
            }
        });
    },
    FncLlenarUbigeo: function (value, ctrlDepa, ctrlProv, ctrlDist) {
        if (value && value.length > 0) {
            var depid = value[0];
            var prvid = value[1];
            var disid = value[2];

            Bata.FncLlenarControl(ctrlDepa, depid);

            /*  Llenar Provincia */
            var jsonProv = [];
            $.each(GLB_JSN_PROV, function (i, v) {
                if (i == 0) {
                    jsonProv.push({ id: GLB_DEF_SEL_VALUE, text: GLB_DEF_SEL_VALUE });
                }
                if (v.prv_dep_id === depid) {
                    jsonProv.push({ id: v.prv_id, text: v.prv_descripcion });
                }
            });
            Bata.FncEstableceSelectJson(ctrlProv, jsonProv);

            Bata.FncLlenarControl(ctrlProv, prvid);
            var jsonDist = [];
            $.each(GLB_JSN_DIST, function (i, v) {
                if (i == 0) {
                    jsonDist.push({ id: GLB_DEF_SEL_VALUE, text: GLB_DEF_SEL_VALUE });
                }
                if (v.dis_prv_id === prvid) {
                    jsonDist.push({ id: v.dis_id, text: v.dis_descripcion });
                }
            });
            Bata.FncEstableceSelectJson(ctrlDist, jsonDist);
            Bata.FncLlenarControl(ctrlDist, disid);
        }
    },
    FncEliminarArchivoTemporal: function (foto) {
        var request = new FormData();
        request.append('foto', foto);
        $.ajax({
            url: glbRutaEliminarImagenTemporal,
            processData: false,
            contentType: false,
            type: 'POST',
            async: true,
            data: request,
            success: function (data) {
                console.log(data.Descripcion);
                return;
            },
            error: glbMsjViewErrorAjax
        });
    },
    FncEstableceEventoRadioCheck: function (ctrl, func) {
        ctrl.on('ifChecked', func);
    },
    FncBuscarJsonValorCampo: function (json, valor) {
        var result = '';
        $.each(json, function (i, v) {
            if (v.valor === valor) {
                result = v.campo;
                return false;
            }
        });
        return result;
    },
    FncBuscarJsonCampoValor: function (json, campo) {
        var result = '';
        $.each(json, function (i, v) {
            if (v.campo === campo) {
                result = v.valor;
                return false;
            }
        });
        return result;
    },
    FncValidateParsley: function (form) {
        if (form.parsley().isValid() === true) {
            $('.bs-callout-info').removeClass('hidden');
            $('.bs-callout-warning').addClass('hidden');
        } else {
            $('.bs-callout-info').addClass('hidden');
            $('.bs-callout-warning').removeClass('hidden');
        }
    },
    FncConvertirJsonSelect2: function (json, idf, valf) {
        var jsel = [];
        $.each(json, function (i, v) {
            if (i == 0) {
                jsel.push({ id: GLB_DEF_SEL_VALUE, text: GLB_DEF_SEL_VALUE });
            }
            jsel.push({ id: v[idf], text: v[valf] });
        });
        return jsel;
    },
    FncObtenerJsonSelect2Vacio: function () {
        var jsel = [];
        jsel.push({ id: GLB_DEF_SEL_VALUE, text: GLB_DEF_SEL_VALUE });
        return jsel;
    },
    FncLlenarDropDownButon: function (ctr, json) {
        var ul = ctr.children("ul");
        ul.empty();
        $.each(json, function (i, v) {
            ul.append("<li value='" + v["id"] + "'><a>" + v["text"] + "</a></li>");
        });
        return;
    },
    FncEsDataTable: function (ctr) {
        return $.fn.dataTable.isDataTable(ctr);
    },
    FncCargarTooltip: function () {
        $('body').tooltip({
            selector: '[rel=tooltip]'
        });
    },
    FncEstableceUbigeoSelect2: function (ctrDep, ctrPrv, ctrDis) {

         ctrDep.on('select2:select', function (e) {
            var id = e.params.data.id;

            if (id == GLB_DEF_SEL_VALUE) {
                Bata.FncEstableceSelectJson(ctrPrv, Bata.FncObtenerJsonSelect2Vacio());
                Bata.FncEstableceSelectJson(ctrDis, Bata.FncObtenerJsonSelect2Vacio());
                return;
            }
            var jsonProv = [];
            $.each(GLB_JSN_PROV, function (i, v) {
                if (i == 0) {
                    jsonProv.push({ id: GLB_DEF_SEL_VALUE, text: GLB_DEF_SEL_VALUE });
                }
                if (v.prv_dep_id === id) {
                    jsonProv.push({ id: v.prv_id, text: v.prv_descripcion });
                }
            });
            Bata.FncEstableceSelectJson(ctrPrv, jsonProv);
            Bata.FncEstableceSelectJson(ctrDis, Bata.FncObtenerJsonSelect2Vacio());
        });

        ctrPrv.on('select2:select', function (e) {
            var id = e.params.data.id;

            if (id == GLB_DEF_SEL_VALUE) {
                Bata.FncEstableceSelectJson(ctrDis, Bata.FncObtenerJsonSelect2Vacio());
                return;
            }
            var jsonDist = [];
            $.each(GLB_JSN_DIST, function (i, v) {
                if (i == 0) {
                    jsonDist.push({ id: GLB_DEF_SEL_VALUE, text: GLB_DEF_SEL_VALUE });
                }
                if (v.dis_prv_id === id) {
                    jsonDist.push({ id: v.dis_id, text: v.dis_descripcion });
                }
            });

            Bata.FncEstableceSelectJson(ctrDis, jsonDist);
        });
    },
    FncMostrarOcultarPanel: function (ctr, flg) {
        var est = ctr.is(':hidden');
        if (flg == false && est == true) return;
        if (flg == true && est == false) return;
        pnlInformacion.slideToggle(200);
        pnlTipoPersona.slideToggle(200);
        pnlUbicacion.slideToggle(200);
    },    
};
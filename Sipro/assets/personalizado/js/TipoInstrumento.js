window.addEventListener('load', iniciar, false);

function iniciar() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("BandejaTipoInstrumento") > 0) {
        var imagenEliminar = document.getElementsByName('imgEliminar');
        var imagenModificar = document.getElementsByName('imgModificar');
        var imagenDetalle = document.getElementsByName('imgDetalle');

        for (var i = 0; i < imagenEliminar.length; i++) {
            imagenEliminar[i].addEventListener('click', eliminarTipoInstrumento, false);
            imagenModificar[i].addEventListener('click', direccionarModificarTipoInstrumento, false);
            imagenDetalle[i].addEventListener('click', direccionarDetalleTipoInstrumento, false);
        }
    }

    if (obtenerUrl.search('CrearTipoInstrumento') > 0) {
        var botonGuardaTipoInstrumento = document.getElementById('btnGuardarTipoInstrumento');

        botonGuardaTipoInstrumento.addEventListener('click', guadarTipoInstrumento, false);
    }

    if (obtenerUrl.search("ActualizarTipoInstrumento") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');

        botonGuardarModificar.addEventListener('click', modificarTipoInstrumento, false);
    }
}

function guadarTipoInstrumento() {

    var _tipoInstrumentoDto = { Descripcion: document.getElementById('Descripcion').value }

    $.ajax({
        type: 'post',
        url: '/TipoInstrumento/CrearTipoInstrumento/',
        data: _tipoInstrumentoDto,
        beforeSend: function () {
            var botonGuardarTipoInstrumento = document.getElementById('btnGuardarTipoInstrumento');
            botonGuardarTipoInstrumento.disabled = true;
            botonGuardarTipoInstrumento.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
        },
        success: function (resultado) {
            switch (resultado.IdentificadorEstado) {
                case 1:
                    swal({
                        title: "Información",
                        text: resultado.Descripcion,
                        type: "success",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
                    break;
                case 0:
                    swal({
                        title: "Alerta!",
                        text: resultado.Descripcion,
                        icon: "warning",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
                    break;
                case -1:
                    swal({
                        title: "Error!",
                        text: resultado.Descripcion,
                        type: "error",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
                    break;
                default:
                    swal({
                        title: "Alerta!",
                        text: "Consulte con el administrador.",
                        icon: "error",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
            }
            estadoNormalBoton();
        }
    });
}


function eliminarTipoInstrumento() {

    var divPadreOpciones = this.parentNode;

    $.ajax({
        method: "get",
        url: "/TipoInstrumento/DeshabilitarTipoInstrumento?_guid=" + divPadreOpciones.children[0].value,
        beforeSend: function () {
            divPadreOpciones.removeChild(divPadreOpciones.children[1]);
        },
        success: function (resultado) {

            switch (resultado.IdentificadorEstado) {
                case 1:
                    swal({
                        title: "Información",
                        text: resultado.Descripcion,
                        type: "success",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    }).then(function () { recargarLaPagina(); });
                    break;
                case 0:
                    swal({
                        title: "Alerta!",
                        text: resultado.Descripcion,
                        icon: "warning",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    }).then(function () { recargarLaPagina(); });;
                    break;
                case -1:
                    swal({
                        title: "Error!",
                        text: resultado.Descripcion,
                        type: "error",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    }).then(function () { recargarLaPagina(); });;
                    break;
                default:
                    swal({
                        title: "Alerta!",
                        text: "Consulte con el administrador.",
                        icon: "error",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    }).then(function () { recargarLaPagina(); });;
            }
        }
    });
}

function direccionarModificarTipoInstrumento() {
    location.href = "/TipoInstrumento/ActualizarTipoInstrumento?_guid=" + this.parentNode.children[0].value;
}

function modificarTipoInstrumento() {

    var _TipoInstrumentoDto = {
        Descripcion: document.getElementById('Descripcion').value,
        IdTipoInstrumento: document.getElementById('IdTipoInstrumento').value
    }

    $.ajax({
        method: "post",
        url: "/TipoInstrumento/ActualizarTipoInstrumento/",
        data: _TipoInstrumentoDto,
        beforeSend: function () {
            var botonGuardarModificacionTipoInstrumento = document.getElementById('btnGuardarModificacion');
            botonGuardarModificacionTipoInstrumento.disabled = true;
            botonGuardarModificacionTipoInstrumento.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
        },
        success: function (resultado) {

            switch (resultado.IdentificadorEstado) {
                case 1:
                    swal({
                        title: "Información",
                        text: resultado.Descripcion,
                        type: "success",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    }).then(function () { recargarLaPagina(); });
                    break;
                case 0:
                    swal({
                        title: "Alerta!",
                        text: resultado.Descripcion,
                        icon: "warning",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
                    break;
                case -1:
                    swal({
                        title: "Error!",
                        text: resultado.Descripcion,
                        type: "error",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
                    break;
                default:
                    swal({
                        title: "Alerta!",
                        text: "Consulte con el administrador.",
                        icon: "error",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
            }
            estadoNormalBoton();
        },
        error: function () {
            alert("Error por algun motivo.");
        }
    });
}

function direccionarDetalleTipoInstrumento() {
    location.href = "/TipoInstrumento/DetalleTipoInstrumento?_guid=" + this.parentNode.children[0].value;
}

function estadoNormalBoton(){

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("CrearTipoInstrumento") > 0) {
        var botonGuardarTipoInstrumento = document.getElementById('btnGuardarTipoInstrumento');
        botonGuardarTipoInstrumento.innerText = "Guardar";
        botonGuardarTipoInstrumento.disabled = false;
        botonGuardarTipoInstrumento.style = '';
    }


    if (obtenerUrl.search("ActualizarInstrumento") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');
        botonGuardarModificar.innerText = "Guardar";
        botonGuardarModificar.disabled = false;
        botonGuardarModificar.style = '';
    }
}

function recargarLaPagina() {
    window.location.reload();
}



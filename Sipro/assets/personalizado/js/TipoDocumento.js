window.addEventListener('load', iniciar, false);


function iniciar() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("BandejaTipoDocumento") > 0) {
        var imagenEliminar = document.getElementsByName('imgEliminar');
        var imagenModificar = document.getElementsByName('imgModificar');
        var imagenDetalle = document.getElementsByName('imgDetalle');

        for (var i = 0; i < imagenEliminar.length; i++) {
            imagenEliminar[i].addEventListener('click', eliminarTipoDocumento, false);
            imagenModificar[i].addEventListener('click', direccionarModificarTipoDocumento, false);
            imagenDetalle[i].addEventListener('click', direccionarDetalleTipoDocumento, false);
        }
    }

    if (obtenerUrl.search("CrearTipoDocumento") > 0) {
        var botonGuardarTipoDocumento = document.getElementById('btnGuardarTipoDocumento');
        botonGuardarTipoDocumento.addEventListener('click', guardarTipoDocumento, false);
    }

    if (obtenerUrl.search("ActualizarTipoDocumento") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');

        botonGuardarModificar.addEventListener('click', modificarTipoDocumento, false);
    }
}

function guardarTipoDocumento() {

    var _tipoDocumentoDto = { Descripcion: document.getElementById('Descripcion').value };

    $.ajax({
        type: "post",
        url: "/TipoDocumento/CrearTipoDocumento/",
        data: _tipoDocumentoDto,
        cache: false,
        beforeSend: function () {
            var botonGuardarTipoDocumento = document.getElementById('btnGuardarTipoDocumento');
            botonGuardarTipoDocumento.disabled = true;
            botonGuardarTipoDocumento.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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
        },
        error: function (error) {
            alert("Algo ocurreo." + error.responseText);
        }
    });
}

function estadoNormalBoton() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("CrearTipoDocumento") > 0) {
        var botonGuardarTipoDocumento = document.getElementById('btnGuardarTipoDocumento');
        botonGuardarTipoDocumento.innerText = "Guardar";
        botonGuardarTipoDocumento.disabled = false;
        botonGuardarTipoDocumento.style = '';
    }

    if (obtenerUrl.search("ActualizarTipoDocumento") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');
        botonGuardarModificar.innerText = "Guardar";
        botonGuardarModificar.disabled = false;
        botonGuardarModificar.style = '';
    }
}

function eliminarTipoDocumento() {
    var divPadreOpciones = this.parentNode;
    $.ajax({
        method: "get",
        url: "/TipoDocumento/DeshabilitarTipoDocumento?_guid=" + divPadreOpciones.children[0].value,
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

function direccionarModificarTipoDocumento() {
    location.href = "/TipoDocumento/ActualizarTipoDocumento?_guid=" + this.parentNode.children[0].value;
}

function modificarTipoDocumento() {

    var _tipoDocumentoDto = {
        Descripcion: document.getElementById('Descripcion').value,
        TipoDocumentoId: document.getElementById('TipoDocumentoId').value
    }

    $.ajax({
        method: "post",
        url: "/TipoDocumento/ActualizarTipoDocumento/",
        data: _tipoDocumentoDto,
        beforeSend: function () {
            var botonGuardarModificacionTipoDocumento = document.getElementById('btnGuardarModificacion');
            botonGuardarModificacionTipoDocumento.disabled = true;
            botonGuardarModificacionTipoDocumento.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function direccionarDetalleTipoDocumento() {
    location.href = "/TipoDocumento/DetalleTipoDocumento?_guid=" + this.parentNode.children[0].value;
}

function recargarLaPagina() {
    window.location.reload();
}
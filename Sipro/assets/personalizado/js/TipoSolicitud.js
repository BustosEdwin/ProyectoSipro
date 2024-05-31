window.addEventListener('load', iniciar, false);


function iniciar() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("BandejaTipoSolicitud") > 0) {
        var imagenEliminar = document.getElementsByName('imgEliminar');
        var imagenModificar = document.getElementsByName('imgModificar');
        var imagenDetalle = document.getElementsByName('imgDetalle');

        for (var i = 0; i < imagenEliminar.length; i++) {
            imagenEliminar[i].addEventListener('click', eliminarTipoSolicitud, false);
            imagenModificar[i].addEventListener('click', direccionarModificarTipoSolicitud, false);
            imagenDetalle[i].addEventListener('click', direccionarDetalleTipoSolicitud, false);
        }
    }

    if (obtenerUrl.search("CrearTipoSolicitud") > 0) {
        var botonGuardarTipoSolcitud = document.getElementById('btnGuardarTipoSolicitud');
        botonGuardarTipoSolcitud.addEventListener('click', guardarTipoSolicitud, false);
    }

    if (obtenerUrl.search("ActualizarTipoSolicitud") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');

        botonGuardarModificar.addEventListener('click', modificarTipoSolicitud, false);
    }
}

function guardarTipoSolicitud() {

    var _tipoSolicitudDto = { Descripcion: document.getElementById('Descripcion').value };

    $.ajax({
        type: "post",
        url: "/TipoSolicitud/CrearTipoSolicitud/",
        data: _tipoSolicitudDto,
        cache: false,
        beforeSend: function () {
            var botonGuardarTipoSolicitud = document.getElementById('btnGuardarTipoSolicitud');
            botonGuardarTipoSolicitud.disabled = true;
            botonGuardarTipoSolicitud.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function eliminarTipoSolicitud() {
    var divPadreOpciones = this.parentNode;
    $.ajax({
        method: "get",
        url: "/TipoSolicitud/DeshabilitarTipoSolicitud?_guid=" + divPadreOpciones.children[0].value,
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

function direccionarModificarTipoSolicitud() {
    location.href = "/TipoSolicitud/ActualizarTipoSolicitud?_guid=" + this.parentNode.children[0].value;
}

function modificarTipoSolicitud() {

    var _tipoSolicitudDto = {
        Descripcion: document.getElementById('Descripcion').value,
        IdTipoSolicitud: document.getElementById('IdTipoSolicitud').value
    }

    $.ajax({
        method: "post",
        url: "/TipoSolicitud/ActualizarTipoSolicitud/",
        data: _tipoSolicitudDto,
        beforeSend: function () {
            var botonGuardarModificacionTipoPersona = document.getElementById('btnGuardarModificacion');
            botonGuardarModificacionTipoPersona.disabled = true;
            botonGuardarModificacionTipoPersona.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function direccionarDetalleTipoSolicitud() {
    location.href = "/TipoSolicitud/DetalleTipoSolicitud?_guid=" + this.parentNode.children[0].value;
}


function estadoNormalBoton() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("CrearTipoSolicitud") > 0) {
        var botonGuardarTipoSolicitud = document.getElementById('btnGuardarTipoSolicitud');
        botonGuardarTipoSolicitud.innerText = "Guardar";
        botonGuardarTipoSolicitud.disabled = false;
        botonGuardarTipoSolicitud.style = '';
    }

    if (obtenerUrl.search("ActualizarTipoSolicitud") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');
        botonGuardarModificar.innerText = "Guardar";
        botonGuardarModificar.disabled = false;
        botonGuardarModificar.style = '';
    }
}


function recargarLaPagina() {
    window.location.reload();
}
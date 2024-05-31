window.addEventListener('load', iniciar, false);


function iniciar() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("BandejaTipoPersona") > 0) {
        var imagenEliminar = document.getElementsByName('imgEliminar');
        var imagenModificar = document.getElementsByName('imgModificar');
        var imagenDetalle = document.getElementsByName('imgDetalle');

        for (var i = 0; i < imagenEliminar.length; i++) {
            imagenEliminar[i].addEventListener('click', eliminarTipoPersona, false);
            imagenModificar[i].addEventListener('click', direccionarModificarTipoPersona, false);
            imagenDetalle[i].addEventListener('click', direccionarDetalleTipoPersona, false);
        }
    }

    if (obtenerUrl.search("CrearTipoPersona") > 0) {
        var botonGuardarTipoPersona = document.getElementById('btnGuardarTipoPersona');
        botonGuardarTipoPersona.addEventListener('click', guardarTipoPersona, false);
    }

    if (obtenerUrl.search("ActualizarTipoPersona") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');

        botonGuardarModificar.addEventListener('click', modificarTipoPersona, false);
    }
}

function guardarTipoPersona() {

    var _tipoPersonaDto = { Descripcion: document.getElementById('Descripcion').value };

    $.ajax({
        type: "post",
        url: "/TipoPersona/CrearTipoPersona/",
        data: _tipoPersonaDto,
        cache: false,
        beforeSend: function () {
            var botonGuardarTipoPersona = document.getElementById('btnGuardarTipoPersona');
            botonGuardarTipoPersona.disabled = true;
            botonGuardarTipoPersona.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function eliminarTipoPersona() {
    var divPadreOpciones = this.parentNode;
    $.ajax({
        method: "get",
        url: "/TipoPersona/DeshabilitarTipoPersona?_guid=" + divPadreOpciones.children[0].value,
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

function direccionarModificarTipoPersona() {
    location.href = "/TipoPersona/ActualizarTipoPersona?_guid=" + this.parentNode.children[0].value;
}

function modificarTipoPersona() {

    var _tipoPersonaDto = {
        Descripcion: document.getElementById('Descripcion').value,
        TipoPersonaId: document.getElementById('TipoPersonaId').value
    }

    $.ajax({
        method: "post",
        url: "/TipoPersona/ActualizarTipoPersona/",
        data: _tipoPersonaDto,
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

function direccionarDetalleTipoPersona() {
    location.href = "/TipoPersona/DetalleTipoPersona?_guid=" + this.parentNode.children[0].value;
}

function estadoNormalBoton() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("CrearTipoPersona") > 0) {
        var botonGuardarTipoPersona = document.getElementById('btnGuardarTipoPersona');
        botonGuardarTipoPersona.innerText = "Guardar";
        botonGuardarTipoPersona.disabled = false;
        botonGuardarTipoPersona.style = '';
    }

    if (obtenerUrl.search("ActualizarTipoPersona") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');
        botonGuardarModificar.innerText = "Guardar";
        botonGuardarModificar.disabled = false;
        botonGuardarModificar.style = '';
    }
}

function recargarLaPagina() {
    window.location.reload();
}

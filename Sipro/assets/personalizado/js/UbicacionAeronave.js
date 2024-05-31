window.addEventListener('load', iniciar, false);

function iniciar() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("BandejaUbicacionAeronave") > 0) {
        var imagenEliminar = document.getElementsByName('imgEliminar');
        var imagenModificar = document.getElementsByName('imgModificar');
        var imagenDetalle = document.getElementsByName('imgDetalle');

        for (var i = 0; i < imagenEliminar.length; i++) {
            imagenEliminar[i].addEventListener('click', eliminarUbicacionAeronave, false);
            imagenModificar[i].addEventListener('click', direccionarModificarUbicacionAeronave, false);
            imagenDetalle[i].addEventListener('click', direccionarDetalleUbicacionAeronave, false);
        }
    }

    if (obtenerUrl.search('CrearUbicacionAeronave') > 0) {
        var botonGuardaUbicacionAeronave = document.getElementById('btnGuardarUbicacionAeronave');

        botonGuardaUbicacionAeronave.addEventListener('click', guadarUbicacionAeronave, false);
    }

    if (obtenerUrl.search("ActualizarUbicacionAeronave") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');

        botonGuardarModificar.addEventListener('click', modificarUbicacionAeronave, false);
    }
}


function guadarUbicacionAeronave() {

    var _ubicacionAeronaveDto = { Descripcion: document.getElementById('Descripcion').value }

    $.ajax({
        type: 'post',
        url: '/UbicacionAeronave/CrearUbicacionAeronave/',
        data: _ubicacionAeronaveDto,
        beforeSend: function () {

            var botonGuardarUbicacionAeronave = document.getElementById('btnGuardarUbicacionAeronave');
            botonGuardarUbicacionAeronave.disabled = true;
            botonGuardarUbicacionAeronave.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function eliminarUbicacionAeronave() {

    var divPadreOpciones = this.parentNode;

    $.ajax({
        method: "get",
        url: "/UbicacionAeronave/DeshabilitarUbicacionAeronave?_guid=" + divPadreOpciones.children[0].value,
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

function direccionarModificarUbicacionAeronave() {
    location.href = "/UbicacionAeronave/ActualizarUbicacionAeronave?_guid=" + this.parentNode.children[0].value;
}


function modificarUbicacionAeronave() {

    var _ubicacionAeronaveDto = {
        Descripcion: document.getElementById('Descripcion').value,
        IdUbicacionAeronave: document.getElementById('IdUbicacionAeronave').value
    }

    $.ajax({
        method: "post",
        url: "/UbicacionAeronave/ActualizarUbicacionAeronave/",
        data: _ubicacionAeronaveDto,
        beforeSend: function () {
            var botonGuardarModificacionUbicacionAeronave = document.getElementById('btnGuardarModificacion');
            botonGuardarModificacionUbicacionAeronave.disabled = true;
            botonGuardarModificacionUbicacionAeronave.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function direccionarDetalleUbicacionAeronave() {
    location.href = "/UbicacionAeronave/DetalleUbicacionAeronave?_guid=" + this.parentNode.children[0].value;
}

function estadoNormalBoton() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("CrearUbicacionAeronave") > 0) {
        var botonGuardarUbicacionAeronave = document.getElementById('btnGuardarUbicacionAeronave');
        botonGuardarUbicacionAeronave.innerText = "Guardar";
        botonGuardarUbicacionAeronave.disabled = false;
        botonGuardarUbicacionAeronave.style = '';
    }

    if (obtenerUrl.search("ActualizarUbicacionAeronave") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');
        botonGuardarModificar.innerText = "Guardar";
        botonGuardarModificar.disabled = false;
        botonGuardarModificar.style = '';
    }
}

function recargarLaPagina() {
    window.location.reload();
}
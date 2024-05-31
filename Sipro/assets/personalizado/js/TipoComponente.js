window.addEventListener('load', iniciar, false);

function iniciar() {
    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("BandejaTipoComponente") > 0) {
        var imagenEliminar = document.getElementsByName('imgEliminar');
        var imagenModificar = document.getElementsByName('imgModificar');
        var imagenDetalle = document.getElementsByName('imgDetalle');

        for (var i = 0; i < imagenEliminar.length; i++) {
            imagenEliminar[i].addEventListener('click', eliminarTipoComponente, false);
            imagenModificar[i].addEventListener('click', direccionarModificarTipoComponente, false);
            imagenDetalle[i].addEventListener('click', direccionarDetalleTipoComponente, false);
        }
    }

    if (obtenerUrl.search('CrearTipoComponente') > 0) {
        var botonGuardaMarca = document.getElementById('btnGuardarTipoComponente');

        botonGuardaMarca.addEventListener('click', guardarTipoComponente, false);
    }

    if (obtenerUrl.search("ActualizarTipoComponente") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');

        botonGuardarModificar.addEventListener('click', modificarTipoComponente, false);
    }
}

function guardarTipoComponente() {

    var _tipoComponenteDto = { Descripcion: document.getElementById('Descripcion').value };

    $.ajax({
        type: "post",
        url: "/TipoComponente/CrearTipoComponente/",
        data: _tipoComponenteDto,
        cache: false,
        beforeSend: function () {
            var botonGuardarTipoComponente = document.getElementById('btnGuardarTipoComponente');
            botonGuardarTipoComponente.disabled = true;
            botonGuardarTipoComponente.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function eliminarTipoComponente() {
    var divPadreOpciones = this.parentNode;
    $.ajax({
        method: "get",
        url: "/TipoComponente/DeshabilitarTipoComponente?_guid=" + divPadreOpciones.children[0].value,
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

function direccionarModificarTipoComponente() {
    location.href = "/TipoComponente/ActualizarTipoComponente?_guid=" + this.parentNode.children[0].value;
}

function modificarTipoComponente() {

    var _tipoComponenteDto = {
        Descripcion: document.getElementById('Descripcion').value,
        IdTipoComponente: document.getElementById('IdTipoComponente').value
    }

    $.ajax({
        method: "post",
        url: "/TipoComponente/ActualizarTipoComponente/",
        data: _tipoComponenteDto,
        beforeSend: function () {
            var botonGuardarModificacionTipoComponente = document.getElementById('btnGuardarModificacion');
            botonGuardarModificacionTipoComponente.disabled = true;
            botonGuardarModificacionTipoComponente.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function direccionarDetalleTipoComponente() {
    location.href = "/TipoComponente/DetalleTipoComponente?_guid=" + this.parentNode.children[0].value;
}

function estadoNormalBoton() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("CrearTipoComponente") > 0) {
        var botonGuardarTipoComponente = document.getElementById('btnGuardarTipoComponente');
        botonGuardarTipoComponente.innerText = "Guardar";
        botonGuardarTipoComponente.disabled = false;
        botonGuardarTipoComponente.style = '';
    }

    if (obtenerUrl.search("ActualizarTipoComponente") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');
        botonGuardarModificar.innerText = "Guardar";
        botonGuardarModificar.disabled = false;
        botonGuardarModificar.style = '';
    }
}

function recargarLaPagina() {
    window.location.reload();
}
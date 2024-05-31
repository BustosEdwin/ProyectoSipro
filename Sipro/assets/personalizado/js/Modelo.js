window.addEventListener('load', iniciar, false);

function iniciar() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("BandejaModelo") > 0) {
        var imagenEliminar = document.getElementsByName('imgEliminar');
        var imagenModificar = document.getElementsByName('imgModificar');
        var imagenDetalle = document.getElementsByName('imgDetalle');

        for (var i = 0; i < imagenEliminar.length; i++) {
            imagenEliminar[i].addEventListener('click', eliminarModelo, false);
            imagenModificar[i].addEventListener('click', direccionarModificarModelo, false);
            imagenDetalle[i].addEventListener('click', direccionarDetalleModelo, false);
        }
    }

    if (obtenerUrl.search('CrearModelo') > 0) {
        var botonGuardaModelo = document.getElementById('btnGuardarModelo');

        botonGuardaModelo.addEventListener('click', guadarModelo, false);
    }   

    if (obtenerUrl.search("ActualizarModelo") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');

        botonGuardarModificar.addEventListener('click', modificarModelo , false);
    }
}


function guadarModelo() {

    var _modeloDto = { Descripcion: document.getElementById('Descripcion').value }

    $.ajax({
        type: 'post',
        url: '/Modelo/CrearModelo/',
        data: _modeloDto,
        beforeSend: function () {
            var botonGuardarModelo = document.getElementById('btnGuardarModelo');
            botonGuardarModelo.disabled = true;
            botonGuardarModelo.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function eliminarModelo() {

    var divPadreOpciones = this.parentNode;

    $.ajax({
        method: "get",
        url: "/Modelo/DeshabilitarModelo?_guid=" + divPadreOpciones.children[0].value,
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

function modificarModelo() {

    var _modeloDto = {
        Descripcion: document.getElementById('Descripcion').value,
        IdModelo: document.getElementById('IdModelo').value
    }

    $.ajax({
        method: "post",
        url: "/Modelo/ActualizarModelo/",
        data: _modeloDto,
        beforeSend: function () {
            var botonGuardarModificacionModelo = document.getElementById('btnGuardarModificacion');
            botonGuardarModificacionModelo.disabled = true;
            botonGuardarModificacionModelo.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function direccionarModificarModelo() {
    location.href = "/Modelo/ActualizarModelo?_guid=" + this.parentNode.children[0].value;
}

function direccionarDetalleModelo() {
    location.href = "/Modelo/DetalleModelo?_guid=" + this.parentNode.children[0].value;
}

function estadoNormalBoton() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("CrearModelo") > 0) {
        var botonGuardarModelo = document.getElementById('btnGuardarModelo');
        botonGuardarModelo.innerText = "Guardar";
        botonGuardarModelo.disabled = false;
        botonGuardarModelo.style = '';
    }

    if (obtenerUrl.search("ActualizarModelo") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');
        botonGuardarModificar.innerText = "Guardar";
        botonGuardarModificar.disabled = false;
        botonGuardarModificar.style = '';
    }
}

function recargarLaPagina() {
    window.location.reload();
}
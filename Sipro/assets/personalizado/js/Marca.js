window.addEventListener('load', iniciar, false);

function iniciar() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("BandejaMarca") > 0) {
        var imagenEliminar = document.getElementsByName('imgEliminar');
        var imagenModificar = document.getElementsByName('imgModificar');
        var imagenDetalle = document.getElementsByName('imgDetalle');

        for (var i = 0; i < imagenEliminar.length; i++) {
            imagenEliminar[i].addEventListener('click', eliminarMarca, false);
            imagenModificar[i].addEventListener('click', direccionarModificarMarca, false);
            imagenDetalle[i].addEventListener('click', direccionarDetalleMarca, false);
        }
    }

    if (obtenerUrl.search('CrearMarca') > 0) {
        var botonGuardaMarca = document.getElementById('btnGuardarMarca');        

        botonGuardaMarca.addEventListener('click', guadarMarca, false);
    }

    if (obtenerUrl.search("ActualizarMarca") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');

        botonGuardarModificar.addEventListener('click', modificarMarca, false);
    }
}

function guadarMarca() {

    var _marcaDto = { Descripcion: document.getElementById('Descripcion').value }

    $.ajax({
        type: 'post',
        url: '/Marca/CrearMarca/',
        data: _marcaDto,
        beforeSend: function () {
            var botonGuardarMarca = document.getElementById('btnGuardarMarca');
            botonGuardarMarca.disabled = true;
            botonGuardarMarca.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function eliminarMarca() {

    var divPadreOpciones = this.parentNode;

    $.ajax({
        method: "get",
        url: "/Marca/DeshabilitarMarca?_guid=" + divPadreOpciones.children[0].value,
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

function direccionarModificarMarca() {
    location.href = "/Marca/ActualizarMarca?_guid=" + this.parentNode.children[0].value;
}

function modificarMarca() {

    var _marcaDto = {
        Descripcion: document.getElementById('Descripcion').value,
        IdMarca: document.getElementById('IdMarca').value
    }

    $.ajax({
        method: "post",
        url: "/Marca/ActualizarMarca/",
        data: _marcaDto,
        beforeSend: function () {
            var botonGuardarModificacionMarca = document.getElementById('btnGuardarModificacion');
            botonGuardarModificacionMarca.disabled = true;
            botonGuardarModificacionMarca.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
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

function direccionarDetalleMarca() {
    location.href = "/Marca/DetalleMarca?_guid=" + this.parentNode.children[0].value;
}


function estadoNormalBoton() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("CrearMarca") > 0) {
        var botonGuardarMarca = document.getElementById('btnGuardarMarca');
        botonGuardarMarca.innerText = "Guardar";
        botonGuardarMarca.disabled = false;
        botonGuardarMarca.style = '';
    }

    if (obtenerUrl.search("ActualizarMarca") > 0) {
        var botonGuardarModificar = document.getElementById('btnGuardarModificacion');
        botonGuardarModificar.innerText = "Guardar";
        botonGuardarModificar.disabled = false;
        botonGuardarModificar.style = '';
    }
}

function recargarLaPagina() {
    window.location.reload();
}
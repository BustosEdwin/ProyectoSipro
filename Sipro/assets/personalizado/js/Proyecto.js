window.addEventListener('load', iniciar, false);

function iniciar() {
    var obtenerUrl = window.location.href;

    //if (obtenerUrl.search("CrearProyecto") > 0) {
    //    var botonAgregarProyecto = document.getElementById('btnCrearProyecto');
    //    botonAgregarProyecto.addEventListener('click', agregarProyecto, false);
    //}

    if (obtenerUrl.search("BandejaProyectos")) {
        var botonDetalleProyecto = document.getElementsByName('btnDetalleProyecto');
        var botonModalAgregarProyecto = document.getElementById('btnAgregarProyecto');
        var botonAgregarProyecto = document.getElementById('btnCrearProyecto');

        for (var i = 0; i < botonDetalleProyecto.length; i++) {
            botonDetalleProyecto[i].addEventListener('click', detalleProyecto, false);
        }

        botonModalAgregarProyecto.addEventListener('click', mostrarModalAgregarProyecto, false);
        botonAgregarProyecto.addEventListener('click', agregarProyecto, false);

    }
}

function agregarProyecto() {

    var idUnidadResponsable = document.getElementById('UnidadesResponsable').value;
    var idUnidadSolicitante = document.getElementById('UnidadesSolicitante').value;

    var _siproProyecto = {
        Nombre: document.getElementById('Nombre').value,
        Descripcion: document.getElementById('Descripcion').value,
        IdUnidadResponsable: idUnidadResponsable,
        IdUnidadSolicitante: idUnidadSolicitante,
        Acronimo: document.getElementById('Acronimo').value
    };

    if (validarUnidades(idUnidadResponsable, idUnidadSolicitante))
        $.ajax({
            type: 'post',
            url: '/Proyecto/CrearProyectoAjax/',
            data: _siproProyecto,
            beforeSend: function () {
                //var botonGuardarMarca = document.getElementById('btnGuardarMarca');
                //botonGuardarMarca.disabled = true;
                //botonGuardarMarca.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
            },
            success: function (resultado) {
                switch (resultado.Codigo) {
                    case 1:
                        swal({
                            title: "Información",
                            text: resultado.Mensaje,
                            type: "success",
                            closeOnEsc: false,
                            closeOnClickOutside: false
                        }).then(function () {
                            window.location = "/Proyecto/BandejaProyectos";
                        });;
                        break;
                    case 0:
                        swal({
                            title: "Alerta!",
                            text: resultado.Mensaje,
                            icon: "warning",
                            closeOnEsc: false,
                            closeOnClickOutside: false
                        });
                        break;
                    case -1:
                        swal({
                            title: "Error!",
                            text: resultado.Mensaje,
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
                //estadoNormalBoton();
            }
        });
}

function validarUnidades(UnidadResponsable, UnidadSolicitante) {
    if (UnidadResponsable == null || UnidadResponsable == "") {
        swal({
            title: "Alerta",
            text: "Debe elegir la unidad responsable",
            type: "error",
            closeOnEsc: false,
            closeOnClickOutside: false
        });
        return false;
    }

    if (UnidadSolicitante == null || UnidadSolicitante == "") {
        swal({
            title: "Alerta",
            text: "Debe elegir la unidad Solicitante",
            type: "error",
            closeOnEsc: false,
            closeOnClickOutside: false
        });
        return false;
    }

    return true;

}

function detalleProyecto() {
    var elementoHijo = this.childNodes;

    //alert(elementoHijo[0].value);

    $.ajax({
        type: 'get',
        url: '/Proyecto/DetalleProyectoAjax?_idProyecto=' + elementoHijo[0].value,
        //data: _siproProyecto,
        beforeSend: function () {
            //var botonGuardarMarca = document.getElementById('btnGuardarMarca');
            //botonGuardarMarca.disabled = true;
            //botonGuardarMarca.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
        },
        success: function (resultado) {
            switch (resultado.Codigo) {
                case 1:
                    //swal({
                    //    title: "Descripción del Proyecto",
                    //    text: resultado.Mensaje.Descripcion,
                    //    type: "success",
                    //    closeOnEsc: false,
                    //    closeOnClickOutside: false
                    //});
                    document.getElementById('pDetalleProyecto').textContent = resultado.Mensaje.Descripcion;
                    $('#modalDetalleProyecto').modal('show'); // abrir                    

                    break;
                case 0:
                    swal({
                        title: "Alerta!",
                        text: resultado.Mensaje,
                        icon: "warning",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
                    break;
                case -1:
                    swal({
                        title: "Error!",
                        text: resultado.Mensaje,
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
            //estadoNormalBoton();
        }
    });
}

function mostrarModalAgregarProyecto() {
    $('#modalAgregarProyecto').modal('show'); // abrir                    

}
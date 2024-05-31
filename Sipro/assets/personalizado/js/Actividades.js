window.addEventListener('load', iniciar, false);

function iniciar() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("BandejaActividadesProyecto") > 0) {
        var botonModalAdicionarRecurso = document.getElementsByName('btnModalAdicionarRecurso');
        var botonGuardarRecurso = document.getElementById('btnGuardarRecurso');
        var botonModalAgregarResponsable = document.getElementsByName('btnModalAgregarResponsable');
        var botonAgregarResponsable = document.getElementById('btnGuardarResponsable');
        var botonModalAgregarActividad = document.getElementsByName('btnModalAgregarActividad');
        var botonGuardarActividad = document.getElementById('btnGuardarActividad');
        var campoIdentificacion = document.getElementById('Identificacion');

        for (var i = 0; i < botonModalAdicionarRecurso.length; i++) {
            botonModalAdicionarRecurso[i].addEventListener('click', mostrarModalAdicionarRecurso, false);
        }

        for (var i = 0; i < botonModalAgregarResponsable.length; i++) {
            botonModalAgregarResponsable[i].addEventListener('click', mostrarModalAgregarResponsable, false);
        }

        for (var i = 0; i < botonModalAgregarActividad.length; i++) {
            botonModalAgregarActividad[i].addEventListener('click', mostrarModalAgregarActividad, false);
        }

        botonGuardarRecurso.addEventListener('click', guardarRecurso, false);

        botonAgregarResponsable.addEventListener('click', guardarResponsable, false);

        botonGuardarActividad.addEventListener('click', guardarActividad, false);

        campoIdentificacion.addEventListener('change', obtenerFuncionario);
    }
}

function mostrarModalAdicionarRecurso() {
    $('#modalAdicionarRecurso').modal('show'); // abrir
}

function guardarRecurso() {
    //alert(document.getElementById('valorIdProyecto').value);
    //alert(document.getElementById('tipoRecursos').value);

    var datoTipoRecurso = document.getElementById('tipoRecursos').value;

    if (datoTipoRecurso == null || datoTipoRecurso == "") {
        swal({
            title: "Alerta!",
            text: "Seleccione una opción para el tipo de recurso",
            type: "warning",
            closeOnEsc: false,
            closeOnClickOutside: false
        });
    } else {
        var _siproRecurso = {
            IdProyecto: document.getElementById('IdProyecto').value,
            IdTipoRecurso: datoTipoRecurso,
            Nombre: document.getElementById('Nombre').value,
            DireccionIp: document.getElementById('DireccionIp').value,
            BaseDatos: document.getElementById('BaseDatos').value,
            Adicionales: document.getElementById('Descripcion').value
        };

        $.ajax({
            type: 'post',
            url: '/Recurso/CrearRecursoAjax/',
            data: _siproRecurso,
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
                        }).then(() => {
                            $('#modalAdicionarRecurso').modal('hide'); // abrir
                            location.reload();
                        });
                        break;
                    case 0:
                        swal({
                            title: "Alerta!",
                            text: resultado.Mensaje,
                            type: "warning",
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
                            type: "error",
                            closeOnEsc: false,
                            closeOnClickOutside: false
                        });
                }
                //estadoNormalBoton();
            }
        });
    }


}

function mostrarModalAgregarResponsable() {

    $('#modalAgregarResponsable').modal('show'); // abrir
}

function guardarResponsable() {

    var datoTipoResponsable = document.getElementById('tipoResonsabilidad').value;
    if (datoTipoResponsable == null || datoTipoResponsable == "") {
        swal({
            title: "Alerta!",
            text: "Elige opción para el tipo de responsabilidad",
            type: "warning",
            closeOnEsc: false,
            closeOnClickOutside: false
        });
    } else {
        var _agregarResponsable = {
            IdProyecto: document.getElementById('IdProyecto').value,
            IdTipoResponsable: datoTipoResponsable,
            Identificacion: document.getElementById('Identificacion').value//,
            //IdUnidad: document.getElementById('DireccionIp').value,        
        };

        $.ajax({
            type: 'post',
            url: '/Responsable/CrearResponsableAjax/',
            data: _agregarResponsable,
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
                        }).then(() => {
                            $('#modalAgregarResponsable').modal('hide'); // abrir
                            location.reload();
                        });
                        break;
                    case 0:
                        swal({
                            title: "Alerta!",
                            text: resultado.Mensaje,
                            type: "warning",
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
                            type: "error",
                            closeOnEsc: false,
                            closeOnClickOutside: false
                        });
                }
                //estadoNormalBoton();
            }
        });
    }

}

function mostrarModalFinalizarResponsable(idResponsable) {
    $("#IdResponsable").val(idResponsable);
    $('#modalFinalizarResponsable').modal('show'); // abrir
}

function finalizarResponsable() {

    var obtenerObservaciones = $("#Observaciones").val();

    if (obtenerObservaciones == '') {
        swal({
            title: "Información",
            text: "El campo observación es obligatorio.",
            type: "warning",
            closeOnEsc: false,
            closeOnClickOutside: false
        });
        return;
    }        

    var _siproResponsableDto = {
        IdResponsable: $("#IdResponsable").val(),
        //FechaFin: $("#FechaFinResponsable").val(),
        Observaciones: obtenerObservaciones
    }

    $.ajax({
        type: 'post',
        url: '/Responsable/FinalizarResponsableAjax/',
        data: _siproResponsableDto,
        success: function (resultado) {
            switch (resultado.Codigo) {
                case 1:
                    swal({
                        title: "Información",
                        text: resultado.Mensaje,
                        type: "success",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    }).then(() => {
                        location.reload();
                    });
                    break;
                case 0:
                    swal({
                        title: "Alerta!",
                        text: resultado.Mensaje,
                        type: "warning",
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
                        type: "error",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
            }
        }
    });
}


function mostrarModalAgregarActividad() {
    $('#modalAgregarActividad').modal('show'); // abrir
}

function guardarActividad() {

    var datoFase = document.getElementById('IdFase').value;

    if (datoFase == null || datoFase == "") {
        swal({
            title: "Alerta!",
            text: "Elige una opción para la fase.",
            type: "warning",
            closeOnEsc: false,
            closeOnClickOutside: false
        });
    } else {
        var _siproBitacora = {
            IdProyecto: document.getElementById('IdProyecto').value,
            Observacion: document.getElementById('ObservacionActividad').value,
            IdFase: datoFase,
            Descripcion: document.getElementById('DescripcionActividad').value,
            FechaInicio: document.getElementById('FechaInicio').value,
            FechaFin: document.getElementById('FechaFin').value
        };

        $.ajax({
            type: 'post',
            url: '/Actividad/AgregarActividadProyectoAjax/',
            data: _siproBitacora,
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
                        }).then(() => {
                            $('#modalAgregarActividad').modal('hide');
                            location.reload();
                        });
                        break;
                    case 0:
                        swal({
                            title: "Alerta!",
                            text: resultado.Mensaje,
                            type: "warning",
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
                            type: "error",
                            closeOnEsc: false,
                            closeOnClickOutside: false
                        });
                }
                //estadoNormalBoton();
            }
        });
    }



}

function obtenerFuncionario() {
    var identificacion = document.getElementById('Identificacion').value;
    $.ajax({
        type: 'post',
        url: '/Actividad/ObtenerFuncionario/',
        data: { _identificacion: identificacion},
        beforeSend: function () {
            //var botonGuardarMarca = document.getElementById('btnGuardarMarca');
            //botonGuardarMarca.disabled = true;
            //botonGuardarMarca.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
        },
        success: function (resultado) {
            switch (resultado.Codigo) {
                case 1:
                    //swal({
                    //    title: "Información",
                    //    text: 'Funcionario: ' + resultado.Objeto.GradAlfabetico + '- ',
                    //    type: "success",
                    //    closeOnEsc: false,
                    //    closeOnClickOutside: false
                    //}).then(() => {
                    //    $('#modalAgregarActividad').modal('hide');
                    //    location.reload();
                    //});                   
                        
                    document.getElementById('datosFuncionario').innerHTML = '<div class="alert alert-success">' +
                        '<strong>' + resultado.Objeto.Nombres + ' ' + resultado.Objeto.Apellidos + '</strong>' + ' perteneciente a la ' + resultado.Objeto.DescripcionDependencia + ' - ' + resultado.Objeto.Fisica + ' con identificación ' + resultado.Objeto.Identificacion +
                        '</div>';
                    break;
                case 0:
                    swal({
                        title: "Alerta!",
                        text: resultado.Mensaje,
                        type: "warning",
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
                        type: "error",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
            }
            //estadoNormalBoton();
        }
    });
}
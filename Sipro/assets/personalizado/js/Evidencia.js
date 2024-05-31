window.addEventListener('load', iniciar, false);
var lstResponsables;
function iniciar() {

    lstResponsables = [];

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("BandejaEvidencias") > 0) {
        var botonModalAsignarResponsable = document.getElementsByName('btnAsignarResponsableActividad');
        var botonAgregarResponsableActividad = document.getElementById('btnGuardarResponsableActividad');
        var botonModalSubirEvidencia = document.getElementsByName('btnSubirEnvidencia');
        var botonGuardarEvidencia = document.getElementById('btnGudarEvidencia');
        var seleccionComentarioEvidencia = document.getElementById('IdResponsable');
        var botonCerrarModalResponsableComentario = document.getElementById('btnCerrarModalRespobleComentario');

        for (var i = 0; i < botonModalAsignarResponsable.length; i++) {
            botonModalAsignarResponsable[i].addEventListener('click', mostrarModalAsignarActividadResponsable, false);
        }

        for (var i = 0; i < botonModalSubirEvidencia.length; i++) {
            botonModalSubirEvidencia[i].addEventListener('click', mostrarModalSubirEvidencia, false);
        }


        botonAgregarResponsableActividad.addEventListener('click', guardarResponsableActividad, false);

        botonGuardarEvidencia.addEventListener('click', guardarEvidencia, false);

        seleccionComentarioEvidencia.addEventListener('change', obtenerFuncionarioSeleccionado, false);

        botonCerrarModalResponsableComentario.addEventListener('click', cerrarModalResponsableComentario, false);

    }
}

function mostrarModalAsignarActividadResponsable() {
    $('#modalAsignarResponsable').modal('show'); // abrir

}

function guardarResponsableActividad() {

    //Responsable/AsignarResponsableActividadAjax/
    var _SiproBitacoResponsablesDto = {
        IdBitacora: document.getElementById('IdActividad').value,
        IdResponsable: document.getElementById('IdResponsable2').value,
    };

    $.ajax({
        type: 'post',
        url: '/Responsable/AsignarResponsableActividadAjax/',
        data: _SiproBitacoResponsablesDto,
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
        }
    });
}

function mostrarModalSubirEvidencia() {
    $('#modalSubirEvidencia').modal('show'); // abrir
}

function guardarEvidencia() {

    var entradaArchivos = document.getElementsByName('ArchivoPdf');


    var _siproEvidencia = new FormData();
    _siproEvidencia.append("IdBitacora", document.getElementById('IdActividad').value);
    _siproEvidencia.append("Observacion", document.getElementById('Observacion').value);
    //_siproEvidencia.append("Usuario", document.getElementById('Usuario').value);
    //_siproEvidencia.append("Contrasena", document.getElementById('Contrasena').value);
    _siproEvidencia.append("Archivo", entradaArchivos[0].files[0]);


    //var _siproEvidencia = {
    //    IdBitacora: document.getElementById('valorIdBitacoraEvidencia').value,
    //    Observacion: document.getElementById('Observacion').value,
    //    Usuario: document.getElementById('Usuario').value,
    //    Contrasena: document.getElementById('Contrasena').value,
    //};

    $.ajax({
        type: 'post',
        url: '/Evidencia/SubirEvidenciaAjax/',
        data: _siproEvidencia,
        processData: false,
        contentType: false,
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
        }
    });
}

function obtenerFuncionarioSeleccionado() {
    var seleccionResponsableEvidencia = document.getElementById('IdResponsable');
    var componenteFuncionarioSeleccionado = document.getElementById('divInformacionFuncionario');

    if (seleccionResponsableEvidencia.value == '' || seleccionResponsableEvidencia.value == null) {
        swal({
            title: "Información",
            text: "Por favor seleccione algo valido.",
            type: "error",
            closeOnEsc: false,
            closeOnClickOutside: false
        });
        return;
    }

    var contador = 0;

    for (var i = 0; i < lstResponsables.length; i++)
        if (seleccionResponsableEvidencia.value == lstResponsables[i])
            contador++;

    if (contador < 1) {
        componenteFuncionarioSeleccionado.innerHTML += `<div class="alert alert-success" id='${seleccionResponsableEvidencia.value}' onclick='(quitarElementoClick("${seleccionResponsableEvidencia.value}"))'>
                                                        <strong >${seleccionResponsableEvidencia.options[seleccionResponsableEvidencia.selectedIndex].text}
                                                        </strong>
                                                        <input type="hidden" name="idResponsable" value="${seleccionResponsableEvidencia.value}" /></div>`;
        lstResponsables.push(seleccionResponsableEvidencia.value);
    }
}

function cerrarModalResponsableComentario() {
    $('#modalComentarioEvidencia').modal('hide');
    lstResponsables = [];
    document.getElementById('divInformacionFuncionario').innerHTML = '';;
}

function quitarElementoClick(_identificador) {
    document.getElementById(_identificador).remove();
    lstResponsables = lstResponsables.filter(a => a != _identificador)
}

function ModalEnviar(idEvidencia) {
    $("#btnEnviarResponsableActividad").attr("onclick", "EnviaResponsableActividad(" + "'" + idEvidencia + "'" + ")");
    $('#modalComentarioEvidencia').modal({ backdrop: 'static', keyboard: false });
    $('#modalComentarioEvidencia').modal('show');

}

function EnviaResponsableActividad(idEvidencia) {

    var lstElementoIdResponsables = document.getElementsByName('idResponsable');
    var idProyecto = document.getElementById('IdProyecto').value;


    var lstIdsResponsables = [];
    for (var i = 0; i < lstElementoIdResponsables.length; i++) {
        lstIdsResponsables.push(lstElementoIdResponsables[i].value);

    }

    if (lstIdsResponsables.length == 0) {
        swal({
            title: "Información",
            text: "No ha elegido ningun responsable para revisiòn o aprobaciòn.",
            type: "error",
            closeOnEsc: false,
            closeOnClickOutside: false
        });
        return;
    }

    var _parametrosAprobarEvidencia = {
        LstIdentificadorResponsables: lstIdsResponsables,
        IdEvidencia: idEvidencia,
        IdProyecto: idProyecto,
        Comentario: document.getElementById('ComentarioEvidencia').value
    };

    $.ajax({
        type: 'POST',
        url: '/Evidencia/EnviarAprobarEvidenciaAnexaAjax/',
        dataType: 'json',
        data: _parametrosAprobarEvidencia,
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
        },
        error: function (ex) {
            alert('Anexar número de cedula!!!');
        }
    })

}

function ConsultaTrazabilidad(idEvidencia) {
 

    $.ajax({
        type: 'get',
        url: '/Comentarios/ObtenerTrazabilidadAjax?_idEvidencia='+idEvidencia,       
        success: function (resultado) {

            for (var i = 0; i < resultado.Objeto.length; i++) {
                listaEvidencia.push([resultado.Objeto[i].NombreGradoFuncionario, resultado.Objeto[i].Unidad, resultado.Objeto[i].DescripcionComentario, resultado.Objeto[i].EstadoComentario]);
            }

            var tbodyEvidencia = "";

            for (i = 0; i < listaEvidencia.length; i++) {
                tbodyEvidencia = tbodyEvidencia + "<tr>";
                for (j = 0; j < listaEvidencia[i].length; j++) {
                    if (j == 0) {
                        //tbodyEvidencia = tbodyEvidencia + "<td>" + "<button class='btn btn-success' onclick='AgregarServidorConvocado(" + listaEvidencia[i][j] + ")'> Agregar</button>" + "</td>";
                    }
                    tbodyEvidencia = tbodyEvidencia + "<td>" + listaEvidencia[i][j] + "</td>";
                }
                tbodyEvidencia = tbodyEvidencia + "</tr>";
            }
            $("#tbodyEvidencia").html(tbodyEvidencia);
            $('#modaltrazabilidad').modal('show');

        },
        error: function (ex) {
            sweetAlert("Fallo al traer los registro", ex, "error");
        }
    });





}

function EliminarEvidencia(idEvidencia, idProyecto, idActividad) {

    var siproEvidenciaDto = {
        IdEvidencia: idEvidencia,
        IdProyecto: idProyecto,
        IdActividad: idActividad,
    };
    $.ajax({
        type: 'post',
        url: '/Evidencia/EliminarEvidencia/',
        data: { _siproEvidenciaDto: siproEvidenciaDto },
        //processData: false,
        //contentType: false,
        success: function (Resultado) {
            switch (Resultado.Codigo) {
                case 1:


                    swal({
                        title: "Información",
                        text: Resultado.Mensaje,
                        type: "success",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
                    if (Resultado.Codigo == 1) {
                        //$("#notificacionEliminarDocumento").html('<div class="alert alert-info alert-dismissible"><h4 class="text-center">' + data.mensaje + '<button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + '</h4></div>');
                        setTimeout(function () {
                            location.reload();
                        }, 1500);
                    }
                    break;
                case 0:
                    swal({
                        title: "Alerta!",
                        text: Resultado.Mensaje,
                        icon: "warning",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
                    break;
                case -1:
                    swal({
                        title: "Error!",
                        text: Resultado.Mensaje,
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
        }
    });


}




window.addEventListener('load', iniciar, false);

function iniciar() {



    var botonObtenerComentariosEvidencias = document.getElementById('btnObtenerComentariosEvidencias');
    botonObtenerComentariosEvidencias.addEventListener('click', obtenerComentariosEnviadosEvidencias, false);

    //var botonPorCorregirEstadoComentarios = document.getElementById('btnPorCorregirEstadoComentarios');

    //botonPorCorregirEstadoComentarios.addEventListener('click', obtenerComentariosEnviadosEvidenciasParaCorregir, false);

    var botonCorregirEvidencia = document.getElementById('btnCorregir');
    botonCorregirEvidencia.addEventListener('click', corregirEvidencia , false);

}

function obtenerComentariosEnviadosEvidencias() {
    document.getElementById('tituloTblEstadoComentarios').textContent = 'Lista Comentarios a Revisar';
    $.ajax({
        type: 'get',
        url: '/Comentarios/BandejaEstadoComentariosAjax/',
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
                    var bodyTableEstadoComentarios = document.getElementById('tbEstadoComentarios');
                    var trTbEstadoComentarios = '';
                    for (var i = 0; i < resultado.Objeto.length; i++) {
                        trTbEstadoComentarios += `<tr><td>${resultado.Objeto[i].Descripcion}</td> 
                                                  <td>${resultado.Objeto[i].UsuarioCreacion}</td>
                                                  <td>${resultado.Objeto[i].Identificacion}</td>
                                                  <td>
                                                  <button class="btn btn-success" onclick="irAEvicencia('${resultado.Objeto[i].IdComentario}')"><i class="mdi mdi-arrow-right-bold-circle-outline"></i></button>
                                                  </td></tr>`;
                    }

                    bodyTableEstadoComentarios.innerHTML = trTbEstadoComentarios;

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

function obtenerComentariosEnviadosEvidenciasParaCorregir() {
    document.getElementById('tituloTblEstadoComentarios').textContent = 'Lista Comentarios a Corregir';

    $.ajax({
        type: 'get',
        url: '/Comentarios/BandejaEstadoComentariosParaCorregirAjax/',
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
                    var bodyTableEstadoComentarios = document.getElementById('tbEstadoComentarios');
                    var trTbEstadoComentarios = '';
                    for (var i = 0; i < resultado.Objeto.length; i++) {
                        trTbEstadoComentarios += `<tr><td>${resultado.Objeto[i].Descripcion}</td> 
                                                  <td>${resultado.Objeto[i].UsuarioCreacion}</td>
                                                  <td>${resultado.Objeto[i].Identificacion}</td>
                                                  <td><a class="btn btn-success" type="button" href="#"><i class="mdi mdi-comment-remove-outline"></i></a></td></tr>`;
                    }


                    bodyTableEstadoComentarios.innerHTML = trTbEstadoComentarios;

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

function irAEvicencia(idComentario) {
    $.ajax({
        type: 'get',
        url: '/Evidencia/IrAEvidenciaAjax?_idComentario=' + idComentario,
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

                    var contenidoEvidencia = document.getElementById('detalleEvidencia');

                    contenidoEvidencia.innerHTML = `<div class="row">
                                                        <div class="col-md-12">
                                                            <div class="ribbon-wrapper card">
                                                                <div class="ribbon ribbon-bookmark ribbon-success">Nombre Proyecto</div>
                                                                <input type="hidden" id='idProyectoEvidencia' value=${resultado.Objeto.IdProyecto} />
                                                                <p class="ribbon-content"><b>${resultado.Objeto.NombreProyecto}</b></p>
                                                            </div>
                                                            <!-- /.box -->
                                                        </div>
                                                    </div>

                                                  <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="ribbon-wrapper card">
                                                                <div class="ribbon ribbon-bookmark ribbon-success">Responsable Evidencia</div>
                                                                <p class="ribbon-content"><b>${resultado.Objeto.NombreGradoResponsable}</b></p>
                                                            </div>
                                                            <!-- /.box -->
                                                        </div>
                                                    </div>

                                                   <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="ribbon-wrapper card">
                                                                <div class="ribbon ribbon-bookmark ribbon-success">Evidencia</div>
                                                                <input type="hidden" id="idComentarioEvidencia" value="${resultado.Objeto.IdComentario}"/>
                                                                <p class="ribbon-content">Nombre Archivo: <b>${resultado.Objeto.NombreArchivo}</b></p>
                                                                <p class="ribbon-content">Nota u Observación: <b>${resultado.Objeto.Observacion}</b></p>
                                                                <button id="verDocumento" class="btn btn-success">Ver documento</button>                        
                                                            </div>
                                                            <!-- /.box -->
                                                        </div>
                                                    </div>
                                                                                    
                                                    <div class="modal" id="modalMostrarArchivoEvidencia" tabindex="-1" role="dialog" aria-labelledby="addContainerLabel">
                                                        <div class="modal-dialog modal-lg" role="document">
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="cerrar"><span aria-hidden="true">&times;</span></button>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="panorama" style="display:block;">
                                                                        <object id='pdfbox' width="100%" height="800px" type="application/pdf" data="/Evidencia/VerArchivoPdf?_nombreArchivo=${resultado.Objeto.NombreArchivo}">
                                                                            @Html.ActionLink("Visualizar Archivo Pdf", "VerArchivoPdf", "Evidencia", new { id = item.IdEvidencia }, new { @class = "btn btn-warning btn-block btn-flat" })
                                                                        </object>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="card card-outline-inverse">
                                                        <div class="card-header">
                                                            <h4 class="m-b-0 text-white">Opciones</h4>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="box-body text-justify">
                                                               <button id="btnMostrarModalComentario" class="btn btn-danger">Corregir</button> 
                                                               <button id="btnAprobar" class="btn btn-success">Aprobar</button> 

                                                            </div>
                                                        </div>
                                                    </div>`;


                    var botonVerModalDocumento = document.getElementById('verDocumento');
                    var botonAprobarEvidencia = document.getElementById('btnAprobar');
                    var botonMostrarModalComentario = document.getElementById('btnMostrarModalComentario');


                    botonVerModalDocumento.addEventListener('click', function () {
                        $("#modalMostrarArchivoEvidencia").modal("show");
                    }, false);

                    botonAprobarEvidencia.addEventListener('click', aprobarEvidencia, false);
                    botonMostrarModalComentario.addEventListener('click', mostrarModalComentarioCorregirEvidencia, false);

                    


                    $("#modalDetalleEvidencia").modal("show");
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


function aprobarEvidencia() {

    var idComentario = document.getElementById('idComentarioEvidencia').value;
    var idProyecto = document.getElementById('idProyectoEvidencia').value;

    $.ajax({
        type: 'get',
        url: '/Evidencia/AprobarEvidenciaAjax?_idComentario=' + idComentario + '&_idProyecto=' + idProyecto ,
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
                        title: "Informativo!",
                        text: resultado.Mensaje,
                        type: "success",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    }).then(function () {
                        window.location.reload();
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

function corregirEvidencia() {   

    var _corregirEvidencia = {
        IdComentario: document.getElementById('idComentarioEvidencia').value,
        IdProyecto: document.getElementById('idProyectoEvidencia').value,
        DescripcionComentario: document.getElementById('txtComentarioCorreccionEvidencia').value
    };

  

    $.ajax({
        type: 'post',
        url: '/Evidencia/CorregirEvidenciaAjax',
        data: { _corregirEvidencia: _corregirEvidencia},       
        beforeSend: function () {
            //var botonGuardarMarca = document.getElementById('btnGuardarMarca');
            //botonGuardarMarca.disabled = true;
            //botonGuardarMarca.style = "background:url('/assets/personalizado/imagenes/loading.gif'); background-size: 80px 80px; background-repeat: no-repeat; background-position:50% 50%;";
        },
        success: function (resultado) {
            switch (resultado.Codigo) {
                case 1:
                    swal({
                        title: "Informativo!",
                        text: 'Evidencia enviada a corregir.',
                        type: "success",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });

                    $("#modalCorreccionEvidencia").modal("hide");
                    $("#modalDetalleEvidencia").modal("hide");
                    break;
                case 0:
                    swal({
                        title: "Alerta!",
                        text: resultado.Mensaje,
                        type: "warning",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
                    $("#modalCorreccionEvidencia").modal("hide");
                    $("#modalDetalleEvidencia").modal("hide");
                    break;
                case -1:
                    swal({
                        title: "Error!",
                        text: resultado.Mensaje,
                        type: "error",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
                    $("#modalCorreccionEvidencia").modal("hide");
                    $("#modalDetalleEvidencia").modal("hide");
                    break;
                default:
                    swal({
                        title: "Alerta!",
                        text: "Consulte con el administrador.",
                        type: "error",
                        closeOnEsc: false,
                        closeOnClickOutside: false
                    });
                    $("#modalCorreccionEvidencia").modal("hide");
                    $("#modalDetalleEvidencia").modal("hide");
                    break;

            }
        }
    });
}

function mostrarModalComentarioCorregirEvidencia() {
    $("#modalCorreccionEvidencia").modal("show");
}

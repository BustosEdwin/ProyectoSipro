//function onComplete(respuesta) {

//    var numero = respuesta.responseText.search("IdentificadorEstado");

//    if (respuesta.responseText.search("IdentificadorEstado") == 4) {

//        var objeto = JSON.parse(respuesta.responseJSON);

//        switch (objeto.IdentificadorEstado) {
//            case 0:
//                $(document).ready(function () {
//                    var objeto = JSON.parse(respuesta.responseJSON);
//                    document.getElementById('mensajeAlerta').innerHTML = objeto.Descripcion;
//                    $("#modal-alerta").modal('show');
//                });
//                break;
//            case 1:
//                $(document).ready(function () {
//                    var objeto = JSON.parse(respuesta.responseJSON);
//                    document.getElementById('mensajeBueno').innerHTML = objeto.Descripcion;
//                    $("#modal-success").modal('show');
//                });
//                break;
//            case 2:
//                $(document).ready(function () {
//                    var objeto = JSON.parse(respuesta.responseJSON);
//                    document.getElementById('mensajeMalo').innerHTML = objeto.Descripcion;
//                    $("#modal-danger").modal('show');
//                });
//                break;

//            default:
//                alert('Es algo Urgene averigua con el administrador.');
//        }
//    }    


//}

function ParaPeticionesAjaxGet(url, guid) {

    //var url = "@Url.Action("ActivarMarca", "GestionBloqueados", new { area = "CACIV" })";

    //var TipoPersona = { Descripcion: $("#Descripcion").val()};
    var _guid = { _guid: guid };

    $.get(url, _guid).done(function (mensaje) {
        var objeto = JSON.parse(mensaje);
        switch (objeto.IdentificadorEstado) {
            case 0:
                $(document).ready(function () {
                    document.getElementById('mensajeAlerta').innerHTML = objeto.Descripcion;
                    $("#modal-alerta").modal('show');
                });
                break;
            case 1:
                $(document).ready(function () {
                    document.getElementById('mensajeBueno').innerHTML = objeto.Descripcion;
                    $("#modal-success").modal('show');
                });
                break;
            case 2:
                $(document).ready(function () {
                    document.getElementById('mensajeMalo').innerHTML = objeto.Descripcion;
                    $("#modal-danger").modal('show');
                });
                break;

            default:
                alert('Es algo Urgene averigua con el administrador.');
        }
    }).fail(function (err) {

        $(document).ready(function () {
            document.getElementById('mensajeMalo').innerHTML = objeto.Descripcion;
            $("#modal-danger").modal('show');
        });
    })
}


function DeterminarAcciones(indentificador) {
    
    switch (identificador) {
        case "Radicacion":
            var Radicado = { Descripcion: $("#Descripcion").val() };
            return Radicado;
        case "RegistrarAvion":
            var avion = {}
        default:

    }

}

function ParaPeticionesAjaxPost(url, identificador) {

    //var url = "@Url.Action("ActivarMarca", "GestionBloqueados", new { area = "CACIV" })";

    //var TipoPersona = { Descripcion: $("#Descripcion").val()};   

    $.post(url, DeterminarAcciones(identificador)).done(function (mensaje) {
        var objeto = JSON.parse(mensaje);
        switch (objeto.IdentificadorEstado) {
            case 0:
                $(document).ready(function () {
                    document.getElementById('mensajeAlerta').innerHTML = objeto.Descripcion;
                    $("#modal-alerta").modal('show');
                });
                break;
            case 1:
                $(document).ready(function () {
                    document.getElementById('mensajeBueno').innerHTML = objeto.Descripcion;
                    $("#modal-success").modal('show');
                });
                break;
            case 2:
                $(document).ready(function () {
                    document.getElementById('mensajeMalo').innerHTML = objeto.Descripcion;
                    $("#modal-danger").modal('show');
                });
                break;

            default:
                alert('Es algo Urgene averigua con el administrador.');
        }
    }).fail(function (err) {

        $(document).ready(function () {
            document.getElementById('mensajeMalo').innerHTML = objeto.Descripcion;
            $("#modal-danger").modal('show');
        });
    })
}


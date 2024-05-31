function Detalle(url,id) {
    $("#CargarDetalle").prop("disabled", "disabled");
    //var url = "/Animal/DetalleAnimal/" + id;
    var datoId = { _guid: id };
    $.get(url, datoId).done(function (data) {
        $("#CargarDetalle").empty(data);
        $("#CargarDetalle").append(data);
        $("#botonCargarDetalle").prop("enabled", "enabled");
    }).fail();
}
function VerRolesUsuario(url, id) {
    $("#CargarRol").prop("disabled", "disabled");
    //var url = "/Animal/DetalleAnimal/" + id;
    var datoId = { _guid: id };
    $.get(url, datoId).done(function (data) {
        $("#CargarRol").empty(data);
        $("#CargarRol").append(data);
        $("#botonCargarRol").prop("enabled", "enabled");
    }).fail();
}
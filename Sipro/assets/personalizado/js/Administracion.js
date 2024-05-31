
window.addEventListener('load', iniciar, false);

function iniciar() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search("CrearRol") > 0) {
        var botonGuardarRol = document.getElementById('btnGudarRol');
        botonGuardarRol.addEventListener('click', guardarRol, false);


        var botonguardarUsuario = document.getElementById('btnGudarUsuario');
        botonguardarUsuario.addEventListener('click', guardarUsuario, false);
        

        var botonModalAbrir= document.getElementsByName('btnAbrirModal');

        


        var botonConsultaFuncionario = document.getElementById('btn_Identificacion');
        botonConsultaFuncionario.addEventListener('click', ConsultaFuncionario, false);



        for (var i = 0; i < botonModalAbrir.length; i++) {
            botonModalAbrir[i].addEventListener('click', mostrarModal, false);
        }

    }
}

function guardarRol() {
           var _SiproRol = new FormData();
    _SiproRol.append("DescripcionRol", document.getElementById('DescripcionRol').value);

    $.ajax({
        type: 'post',
        url: '/Administracion/CrearRol/',
        data: _SiproRol,
        processData: false,
        contentType: false,
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
                        $('#modalRol').modal('hide'); // abrir
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

function ConsultaFuncionario() {


    $("#IdNombre").val("");
    $("#IdCargo").val("");
    $("#IdUnidad").val("");
    $("#IdVigente").val("");
    $("#IdRol").val("");
    $("#FechaInicio").val("");
    $("#FechaFin").val("");
    $("#consecutivo").val("");
    $("#undeconsectutivo").val("");
    $("#undefuerza").val("");
    $("#usuario").val("");


    $.ajax({
        type: 'post',
        url: '/Administracion/ConsultaFuncionarioAjax/',
        data: { _Identificacion: $("#Identificacion2").val() },
        success: function (resultado) {
            $("#IdNombre").val(resultado.NombreGrado);
            $("#IdCargo").val(resultado.CargoActual);
            $("#IdUnidad").val(resultado.Fisica);
            $("#Consecutivo").val(resultado.Consecutivo);
            $("#UndeConsecutivo").val(resultado.UndeConsecutivo);
            $("#UndeFuerza").val(resultado.UndeFuerza);
            $("#usuario").val(resultado.UsuarioEmpresarial);

        },
        error: function (ex) {
            alert('Anexar número de cedula!!!');
        }
    });
}

function mostrarModal() {
    $('#modalRol').modal('show'); // abrir
}

function guardarUsuario() {
    var _SiproUsuario = {      
        Consecutivo: document.getElementById('Consecutivo').value,
        UndeConsecutivo: document.getElementById('UndeConsecutivo').value,
        UndeFuerza: document.getElementById('UndeFuerza').value,
        usuario: document.getElementById('usuario').value,
        Vigente: document.getElementById('IdVigente').value,
        IdRol: document.getElementById('IdRol').value,
        FechaInicio: document.getElementById('FechaInicio').value,
        FechaFin: document.getElementById('FechaFin').value,
    };
    $.ajax({
        type: 'post',
        url: '/Administracion/CrearUsuario/',
        data: _SiproUsuario,
        //processData: false,
        //contentType: false,
        success: function (resultado) {
            switch (resultado.Codigo) {
                case 1:
                    swal({
                        title: "Información",
                        text: resultado.Mensaje,
                        type: "success",
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


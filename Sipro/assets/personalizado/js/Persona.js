window.addEventListener('load', iniciar, false);

function iniciar() {

    var obtenerUrl = window.location.href;

    if (obtenerUrl.search('CrearPersonaPropietarioAeronave') > 0) {
        var guardarPersonaPropietariaAeronave = document.getElementById('guardarPersonaPropietarioAeronave');

        guardarPersonaPropietariaAeronave.addEventListener('click', guardarPropietarioAeronave, false);
    }

    if (obtenerUrl.search('CrearPersonaPiloto') > 0) {
        var guardarPilotoAeronave = document.getElementById('guardarPersonaPilotoAeronave');

        guardarPilotoAeronave.addEventListener('click', agregarPersonaPiloto, false);
    }

    if (obtenerUrl.search('CrearPersonaRepresentanteLegal') > 0) {
        var guardarRepresentanteLegal = document.getElementById('guardarRepresentanteLegal');

        guardarRepresentanteLegal.addEventListener('click', agregarPersonaRepresentanteLegal, false);
    }

    var campoTextoNumeroDocumento = document.getElementById('NumeroDocumento');
    var botonCerrarModal = document.getElementById('btnCerrar');

    campoTextoNumeroDocumento.addEventListener('change', consultarPersonaRnec, false);
    botonCerrarModal.addEventListener('click', cerrarModalPersona, false);
    bloquearCampos();
}

function consultarPersonaRnec() {

    $.ajax({
        type: "get",
        url: "/Persona/ConsultarPersonaRnec?_identificacion=" + document.getElementById('NumeroDocumento').value,
        data: "",
        cache: false,
        success: function (resultado) {
            switch (resultado.IdentificadorEstado) {
                case 1:
                    //Datos puesto en la modal
                    document.getElementById('nombrePersonaRnec').textContent = resultado.Objeto.primerNombre + " "
                        + resultado.Objeto.segundoNombre + " "
                        + resultado.Objeto.primerApellido + " "
                        + resultado.Objeto.segundoApellido;
                    document.getElementById('municipioExpedicionRnec').textContent = resultado.Objeto.municipioExpedicion;
                    document.getElementById('departamentoExpedicionRnec').textContent = resultado.Objeto.departamentoExpedicion;
                    document.getElementById('fechaExpedicionRnec').textContent = resultado.Objeto.fechaExpedicion;

                    $('#responsive-modal').modal('show');

                    //Datos puestos en el formulario
                    document.getElementById('NombreCompleto').value = resultado.Objeto.primerNombre + " "
                        + resultado.Objeto.segundoNombre + " "
                        + resultado.Objeto.primerApellido + " "
                        + resultado.Objeto.segundoApellido;
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
                        text: resultado.Objeto,
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

function guardarPropietarioAeronave() {
    var entradaArchivos = document.getElementsByName('ArchivoPdf');

    var _personaDto = new FormData();
    _personaDto.append("TipoDocumentoId", document.getElementById('TipoDocumento').value);
    _personaDto.append("NumeroDocumento", document.getElementById('NumeroDocumento'.value));
    _personaDto.append("TipoPersonaId", document.getElementById('TipoPersona').value);
    _personaDto.append("NombreCompleto", document.getElementById('NombreCompleto').value);
    _personaDto.append("Telefono", document.getElementById('Telefono').value);
    _personaDto.append("FechaNacimiento", document.getElementById('mdate').value);
    _personaDto.append("DepartamentoReside", document.getElementById('DepartamentoReside').value);
    _personaDto.append("CiudadReside", document.getElementById('CiudadReside').value);
    _personaDto.append("Email", document.getElementById('Email').value);
    _personaDto.append("DireccionReside", document.getElementById('DireccionReside').value);
    _personaDto.append("NumeroLicencia", document.getElementById('NumeroLicencia').value);
    _personaDto.append("CertificadoMedico", document.getElementById('CertificadoMedico').value);
    _personaDto.append("VigenciaCertificadoMedico", document.getElementById('VigenciaCertificadoMedico').value);
    _personaDto.append("Observaciones", document.getElementById('Observaciones').value);

    for (var i = 0; i < entradaArchivos.length; i++) {
        if (entradaArchivos[i].files[0] != null) {
            _personaDto.append("AdjuntoArchivosPdf", entradaArchivos[i].files[0]);
        }
    }

    $.ajax({
        type: "post",
        url: "/Radicacion/CrearPersonaPropietarioAeronave",
        data: _personaDto,
        processData: false,
        contentType: false,
        success: function (resultado) {
            alert("Algo Paso.");
        }
    });
}

function agregarPersonaPiloto() {

    var entradaArchivos = document.getElementsByName('ArchivoPdf');

    var _personaDto = new FormData();
    _personaDto.append("TipoDocumentoId", document.getElementById('TipoDocumento'.value));
    _personaDto.append("NumeroDocumento", document.getElementById('NumeroDocumento').value);
    _personaDto.append("TipoPersonaId", document.getElementById('TipoPersona').value);
    _personaDto.append("NombreCompleto", document.getElementById('NombreCompleto').value);
    _personaDto.append("Telefono", document.getElementById('Telefono').value);
    _personaDto.append("FechaNacimiento", document.getElementById('FechaNacimiento').value);
    _personaDto.append("DepartamentoReside", document.getElementById('DepartamentoReside').value);
    _personaDto.append("CiudadReside", document.getElementById('CiudadReside').value);
    _personaDto.append("Email", document.getElementById('Email').value);
    _personaDto.append("DireccionReside", document.getElementById('DireccionReside').value);
    _personaDto.append("NumeroLicencia", document.getElementById('NumeroLicencia').value);
    _personaDto.append("CertificadoMedico", document.getElementById('CertificadoMedico').value);
    _personaDto.append("VigenciaCertificadoMedico", document.getElementById('VigenciaCertificadoMedico').value);
    _personaDto.append("Observaciones", document.getElementById('Observaciones').value);
    //_personaDto.append("TipoDocumentoId", document.getElementById('TipoDocumento').value);
    //_personaDto.append("NumeroDocumento", document.getElementById('NumeroDocumento'.value));
    //_personaDto.append("TipoPersonaId", document.getElementById('TipoPersona').value);
    //_personaDto.append("NombreCompleto", document.getElementById('NombreCompleto').value);
    //_personaDto.append("Telefono", document.getElementById('Telefono').value);
    //_personaDto.append("FechaNacimiento", document.getElementById('mdate').value);
    //_personaDto.append("DepartamentoReside", document.getElementById('DepartamentoReside').value);
    //_personaDto.append("CiudadReside", document.getElementById('CiudadReside').value);
    //_personaDto.append("Email", document.getElementById('Email').value);
    //_personaDto.append("DireccionReside", document.getElementById('DireccionReside').value);
    //_personaDto.append("NumeroLicencia", document.getElementById('NumeroLicencia').value);
    //_personaDto.append("CertificadoMedico", document.getElementById('CertificadoMedico').value);
    //_personaDto.append("VigenciaCertificadoMedico", document.getElementById('VigenciaCertificadoMedico').value);
    //_personaDto.append("Observaciones", document.getElementById('Observaciones').value);


    for (var i = 0; i < entradaArchivos.length; i++) {
        if (entradaArchivos[i].files[0] != null) {
            _personaDto.append("AdjuntoArchivosPdf", entradaArchivos[i].files[0]);
        }
    }

    $.ajax({
        type: "post",
        url: "/Radicacion/CrearPersonaPiloto",
        data: _personaDto,
        processData: false,
        contentType: false,
        success: function (resultado) {
            alert("Algo paso");
        }
    });
}

function agregarPersonaRepresentanteLegal() {

    var entradaArchivos = document.getElementsByName('ArchivoPdf');

    var _personaDto = new FormData();
    //_personaDto.append("NombreCompleto", document.getElementById('NombreCompleto').value);

    _personaDto.append("TipoDocumentoId", document.getElementById('TipoDocumento'.value));//Ok
    _personaDto.append("NumeroDocumento", document.getElementById('NumeroDocumento').value);//Ok
    _personaDto.append("TipoPersonaId", document.getElementById('TipoPersona').value);//Ok
    _personaDto.append("NombreCompleto", document.getElementById('NombreCompleto').value); //Ok
    _personaDto.append("Telefono", document.getElementById('Telefono').value); //Ok
    _personaDto.append("FechaNacimiento", document.getElementById('FechaNacimiento').value);//Ok
    _personaDto.append("DepartamentoReside", document.getElementById('DepartamentoReside').value);//Ok
    _personaDto.append("CiudadReside", document.getElementById('CiudadReside').value);
    _personaDto.append("Email", document.getElementById('Email').value);
    _personaDto.append("DireccionReside", document.getElementById('DireccionReside').value);
    _personaDto.append("NumeroLicencia", document.getElementById('NumeroLicencia').value);
    _personaDto.append("CertificadoMedico", document.getElementById('CertificadoMedico').value);
    _personaDto.append("VigenciaCertificadoMedico", document.getElementById('VigenciaCertificadoMedico').value);
    _personaDto.append("Observaciones", document.getElementById('Observaciones').value);



    for (var i = 0; i < entradaArchivos.length; i++) {
        if (entradaArchivos[i].files[0] != null) {
            _personaDto.append("AdjuntoArchivosPdf", entradaArchivos[i].files[0]);
        }
    }

    $.ajax({
        type: "post",
        url: "/Radicacion/CrearPersonaRepresentanteLegal",
        data: _personaDto,
        processData: false,
        contentType: false,
        success: function (resultado) {
            alert("Algo paso");
        }
    });
}

function cerrarModalPersona() {
    desbloquearCampos();
}

function bloquearCampos() {

    document.getElementById('NombreCompleto').disabled = true;
    document.getElementById('Telefono').disabled = true;
    document.getElementById('DepartamentoReside').disabled = true;
    document.getElementById('CiudadReside').disabled = true;
    document.getElementById('Email').disabled = true;
    document.getElementById('DireccionReside').disabled = true;
    document.getElementById('NumeroLicencia').disabled = true;
    document.getElementById('CertificadoMedico').disabled = true;
    document.getElementById('VigenciaCertificadoMedico').disabled = true;
    document.getElementById('Observaciones').disabled = true;
}

function desbloquearCampos() {
    document.getElementById('Telefono').disabled = false;
    document.getElementById('DepartamentoReside').disabled = false;
    document.getElementById('CiudadReside').disabled = false;
    document.getElementById('Email').disabled = false;
    document.getElementById('DireccionReside').disabled = false;
    document.getElementById('NumeroLicencia').disabled = false;
    document.getElementById('CertificadoMedico').disabled = false;
    document.getElementById('VigenciaCertificadoMedico').disabled = false;
    document.getElementById('Observaciones').disabled = false;
}






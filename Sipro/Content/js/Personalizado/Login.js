window.addEventListener('load', iniciar, false);

function iniciar() {

    var botonRedirigirAplicaciones = document.getElementsByName('btnRedirigirLogin');

    for (var i = 0; i < botonRedirigirAplicaciones.length; i++) {
        botonRedirigirAplicaciones[i].addEventListener('click', redireccionAplicacion, false);
    }
}

function redireccionAplicacion() {
    var aplicacionObtenida = this.value;
    window.open('/BandejaAplicacion/DireccionarAplicaciones?_aplicacion=' + aplicacionObtenida, '_blank');
}
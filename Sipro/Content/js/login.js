/// <reference path="login.js" />
/// <reference path="login.js" />
$(function () {
    $(".precarga").fadeOut(1500, function () {
        $(".contenido").fadeIn(1000);
    });
});

function cerrar() {

    Custombox.modal.close();
    $("#modal").addClass("fade");
    jQuery('[id$=modal]')[0].style.display = "none";
}

function esperar() {
    var modal = new Custombox.modal({
        content: {
            effect: 'makeway',
            target: '#modal'
        }
    });

    modal.open();
}

document.addEventListener('custombox:overlay:close', function () {
    $("#modal").addClass("fade");
    jQuery('[id$=modal]')[0].style.display = "none";
});


$(document).ready(function () {
    var modal = new Custombox.modal({
        content: {
            effect: 'makeway',
            target: '#modal'
        }
    });

    modal.open();
});

$.getScript("https://cdnjs.cloudflare.com/ajax/libs/particles.js/2.0.0/particles.min.js", function () {
    particlesJS('particles-js',
      {
          "particles": {
              "number": {
                  "value": 80,
                  "density": {
                      "enable": true,
                      "value_area": 800
                  }
              },
              "color": {
                  "value": "#ffffff"
              },
              "shape": {
                  "type": "circle",
                  "stroke": {
                      "width": 0,
                      "color": "#000000"
                  },
                  "polygon": {
                      "nb_sides": 5
                  },
                  "image": {
                      "width": 100,
                      "height": 100
                  }
              },
              "opacity": {
                  "value": 0.5,
                  "random": false,
                  "anim": {
                      "enable": false,
                      "speed": 1,
                      "opacity_min": 0.1,
                      "sync": false
                  }
              },
              "size": {
                  "value": 5,
                  "random": true,
                  "anim": {
                      "enable": false,
                      "speed": 40,
                      "size_min": 0.1,
                      "sync": false
                  }
              },
              "line_linked": {
                  "enable": true,
                  "distance": 150,
                  "color": "#ffffff",
                  "opacity": 0.4,
                  "width": 1
              },
              "move": {
                  "enable": true,
                  "speed": 6,
                  "direction": "none",
                  "random": false,
                  "straight": false,
                  "out_mode": "out",
                  "attract": {
                      "enable": false,
                      "rotateX": 600,
                      "rotateY": 1200
                  }
              }
          },
          "interactivity": {
              "detect_on": "canvas",
              "events": {
                  "onhover": {
                      "enable": true,
                      "mode": "repulse"
                  },
                  "onclick": {
                      "enable": true,
                      "mode": "push"
                  },
                  "resize": true
              },
              "modes": {
                  "grab": {
                      "distance": 400,
                      "line_linked": {
                          "opacity": 1
                      }
                  },
                  "bubble": {
                      "distance": 400,
                      "size": 40,
                      "duration": 2,
                      "opacity": 8,
                      "speed": 3
                  },
                  "repulse": {
                      "distance": 200
                  },
                  "push": {
                      "particles_nb": 4
                  },
                  "remove": {
                      "particles_nb": 2
                  }
              }
          },
          "retina_detect": true,
          "config_demo": {
              "hide_card": false,
              "background_color": "#b61924",
              "background_image": "",
              "background_position": "50% 50%",
              "background_repeat": "no-repeat",
              "background_size": "cover"
          }
      }
    );

});

$("#sistema").change(function () {
    var data = $("#sistema").val();
    var src = "../Content/Moneda.png";
    if (data === "SICEX") {
        $("#h1login").empty();
        $("#h4login").empty();

        $("#h1login").append("<span class='tweak'>SICEX</span>");
        $("#h4login").append("<span class='tweak'></span>Sistema de Información Control Exportaciones");
        src = "../Content/img/SICEX/Sicex.png";
        $("#imglogin").attr("src", src);
    }
    else if (data === "CACIV") {
        $("#h1login").empty();      
        $("#h4login").empty();

        $("#h1login").append("<span class='tweak'>CAVIC</span>");
        $("#h4login").append("<span class='tweak'></span>Aviación Civil");
        src = "../Content/img/CACIV/Caciv.png";
        $("#imglogin").attr("src", src);
    }
    else if (data === "SGSO") {
        $("#h1login").empty();
        $("#h4login").empty();

        $("#h1login").append("<span class='tweak'>SGSO</span>");
        $("#h4login").append("<span class='tweak'></span>Sistema de Gestión de Seguridad Operacional");
        src = "../Content/img/SGSO/Sgso.png";
        $("#imglogin").attr("src", src);
    }
});
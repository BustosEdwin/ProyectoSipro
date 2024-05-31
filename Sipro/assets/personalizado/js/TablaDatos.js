function poblarTabla(tabla) {
    $('#' + tabla).DataTable({
        "language": glOpcionesIdioma
    })
}

//Configuración del idioma
var glOpcionesIdioma = {
    search: '<span>Filtro:</span> _INPUT_',
    lengthMenu: '<span>Mostrar:</span> _MENU_',
    paginate: { 'first': 'Primero', 'last': 'Último', 'next': '→', 'previous': '←' },
    info: "Mostrando _START_ a _END_ de _TOTAL_ registros.",
    infoEmpty: "Mostrando _START_ a _END_ de _TOTAL_ registros.",
    loadingRecords: "Cargando registros...",
    zeroRecords: "No se han encontrado registros",
    processing: "Procesando...",
    infoFiltered: "(Filtrados de _MAX_ registros.)",
    oPaginate: {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior"
    }
};

$(function () {
    $('#myTable').DataTable();
    $(function () {
        var table = $('#example').DataTable({
            "columnDefs": [{
                "visible": false,
                "targets": 2
            }],
            "order": [
                [2, 'asc']
            ],
            "displayLength": 25,
            "drawCallback": function (settings) {
                var api = this.api();
                var rows = api.rows({
                    page: 'current'
                }).nodes();
                var last = null;
                api.column(2, {
                    page: 'current'
                }).data().each(function (group, i) {
                    if (last !== group) {
                        $(rows).eq(i).before('<tr class="group"><td colspan="5">' + group + '</td></tr>');
                        last = group;
                    }
                });
            }
        });
        // Order by the grouping
        $('#example tbody').on('click', 'tr.group', function () {
            var currentOrder = table.order()[0];
            if (currentOrder[0] === 2 && currentOrder[1] === 'asc') {
                table.order([2, 'desc']).draw();
            } else {
                table.order([2, 'asc']).draw();
            }
        });
    });
});

$('#example23').DataTable({
    dom: 'Bfrtip',
    buttons: [
        'copy', 'csv', 'excel', 'pdf', 'print'
    ]
});

$('#config-table').DataTable({
    responsive: true
});
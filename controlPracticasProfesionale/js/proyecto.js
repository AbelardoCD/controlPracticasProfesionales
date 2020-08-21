var accion = "";
var id_Seleccionado = "";
$(document).ready(function () {
    if (sessionStorage.getItem("matricula") == "" || sessionStorage.getItem("matricula") == null || sessionStorage.getItem("matricula") == undefined) {
        window.location = "../Forms/login.aspx";
    } else {
        if (sessionStorage.getItem("user") == 1) {
            $("#seccionAdmin").show();
            $("#itemproyectos").hide();
        } else {
            $("#seccionAdmin").hide();
            $("#itemproyectos").show();

        }
        $("#seccionTabla").show();
        $("#modal-proyecto").hide();

        listarProyectos();
        comboOrganizacion();
        comboEncargado();

        $("#btnNuevo").click(function () {
            $('#formDatos')[0].reset();
            $("#seccionTabla").hide();
            $("#modal-proyecto").show();

        });
        $("#btnCancelar").click(function () {
            $("#seccionTabla").show();
            $("#modal-proyecto").hide();

        });

        $("#btnGuardar").click(function (e) {

            accion = "Nuevo";
            guardar(e);

        });

        $("#cerrarSesion").click(function () {
            sessionStorage.removeItem('matricula');
            sessionStorage.removeItem('nombre');
            sessionStorage.removeItem('user');
            window.location = "../Forms/login.aspx";

        });
    }
});

function listarProyectos() {
    $.ajax({
        type: "POST",
        url: "../Forms/proyecto.aspx/listarProyectos",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            for (var i = 0; i < data.length; i++) {
                var tr = "";

                tr += '<tr>';
                tr += '<td style="font-size:15px;">' + data[i].nombre + '</td>';
                tr += '<td style="font-size:10px;">' + data[i].descripcion + '</td>';
                tr += '<td>' + data[i].capacidadEstudiantes + '</td>';
                tr += '<td>' + data[i].numEstudiantesAsignados + '</td>';
                tr += '<td>' + data[i].status + '</td>';
                tr += '<td>' + data[i].Organizacion + '</td>';
                tr += '<td>' + data[i].EncargadoProyecto + '</td>';

                tr += "<td align-center>";
                tr += "<label class='btn btn-success' id = " + data[i].idProyecto + " onClick='editar(" + data[i].idProyecto + ")'>Editar</label>";
                tr += "</td>";
                tr += "<td align-center>";
                tr += "<label class='btn btn-danger' id = " + data[i].idProyecto + " onClick='eliminar(" + data[i].idProyecto + ")'>Eliminar</label>";
                tr += "</td>";
                tr += '</tr>';

                $("#tbodyDatos").append(tr);

            }

        }
    });


}


function comboOrganizacion() {
    $.ajax({
        type: "POST",
        url: "../Forms/proyecto.aspx/listarOrganizaciones",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            var option = "";
            for (var i = 0; i < data.length; i++) {
                option += "<option value=" + data[i].idOrganizacion + "> " + data[i].nombre+"</option>";

            }
            $("#comboOrganizacion").empty().append(option);
        }
    });


}

function comboEncargado() {
    $.ajax({
        type: "POST",
        url: "../Forms/proyecto.aspx/listarEncargadosProyectos",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            var option = "";
            for (var i = 0; i < data.length; i++) {
                option += "<option value=" + data[i].idEncargadoProyecto + "> " + data[i].nombre + "</option>";

            }
            $("#comboEncargado").empty().append(option);
        }
    });


}

function eliminar(idSeleccionado) {
    var value = idSeleccionado;
    var parametros = new Object();
    parametros.id = value;
    var id = JSON.stringify(parametros);
    $.ajax({
        type: "POST",
        url: "../Forms/proyecto.aspx/eliminar",
        data: id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var result = response.d;
            if (result > 0) {



                $("#registroGuardado").modal("show");
                $("#btnClose").addClass("alert-success");
                $("#head-color").addClass("alert-success");
                $("#msg").text('Registro Eliminado correctamente...');


                $("#tbodyDatos").children("tr").remove();
                listarProyectos();

            } else {
                $("#registroGuardado").modal("show");
                $("#btnClose").addClass("alert-danger");
                $("#head-color").addClass("alert-danger");
                $("#msg").text('No se pudo eliminar el registro...');




            }
        }
    });
}

function guardar(e) {
    e.preventDefault();
    var hasErrors = $('form[name="formDatos"]').validator('validate').has('.has-error').length;

    console.log(hasErrors);
    if (!hasErrors) {
        var params = new Object();
        params.nombre = $("#txtNombre").val();
        params.idProyecto = id_Seleccionado;
        params.descripcion = $("#txtDescripcion").val();
        params.capacidadEstudiantes = $("#txtEstudiantes").val();
        params.numEstudiantesAsignados = $("#txtEstudiantesAsignados").val();
        params.status = $("#txtStatus").val();

        params.idOrganizacion = $("#comboOrganizacion").val();
        params.idEncargadoProyecto = $("#comboEncargado").val();


        var items = new Object();
        items.proyecto = params;
        items.accion = accion;
        items = JSON.stringify(items);
        $.ajax({
            type: "POST",
            url: "../Forms/proyecto.aspx/guardar",
            data: items,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                if (response.d > 0) {
                    $("#modal-proyecto").hide();
                    $("#seccionTabla").show();

                    $("#registroGuardado").modal("show");
                    $("#btnClose").addClass("alert-success");
                    $("#head-color").addClass("alert-success");
                    $("#msg").text('Registro guardado correctamente...');


                    $("#tbodyDatos").children("tr").remove();
                    listarProyectos();


                } else {
                    $("#registroGuardado").modal("show");
                    $("#btnClose").addClass("alert-danger");
                    $("#head-color").addClass("alert-danger");
                    $("#msg").text('No se puede guardar el registro...');


                }
            }

        });
    }
}


function editar(idSeleccionado) {
    accion = "Updated";
    var parametros = new Object();
    parametros.id = idSeleccionado;
    var id = JSON.stringify(parametros);
    $.ajax({
        type: "POST",
        url: "../Forms/proyecto.aspx/getProyecto",
        data: id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {

            var data = response.d;

            $("#txtNombre").val(data.nombre);
            $("#txtDescripcion").val(data.descripcion);
            $("#txtEstudiantes").val(data.capacidadEstudiantes);
            $("#txtEstudiantesAsignados").val(data.numEstudiantesAsignados);
            $("#txtStatus").val(data.status);
            $("#comboOrganizacion").val(data.Organizacion);
            $("#comboEncargado").val(data.EncargadoProyecto);
           
            id_Seleccionado = data.idProyecto;

           $("#seccionTabla").hide();
           $("#modal-proyecto").show();

        }

    });

}
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
        $("#modal-encargado").hide();
        listarRegistros();

        $("#btnNuevo").click(function () {
            $('#formDatos')[0].reset();
            $("#seccionTabla").hide();
            $("#modal-encargado").show();

            accion = "Nuevo";
        });

        $("#btnGuardar").click(function (e) {
            nuevoUsuario(e);
        });
        $("#btnCancelar").click(function () {
            $("#modal-encargado").hide();
            $("#seccionTabla").show();
        });


        $("#cerrarSesion").click(function () {
            sessionStorage.removeItem('matricula');
            sessionStorage.removeItem('nombre');
            sessionStorage.removeItem('user');
            window.location = "../Forms/login.aspx";

        });
    }
});

function nuevoUsuario(e) {
    e.preventDefault();
    var hasErrors = $('form[name="formDatos"]').validator('validate').has('.has-error').length;

    console.log(hasErrors);
    if (!hasErrors) {
        var parametros = new Object();
        parametros.idEncargadoProyecto = id_Seleccionado;
        parametros.nombre = $("#txtNombre").val();
        parametros.cargo = $("#txtCargo").val();
        parametros.correo = $("#txtCorreo").val();

        var items = new Object();
        items.encargadoProyecto = parametros;
        items.accion = accion;
        items = JSON.stringify(items);

        $.ajax({
            type: "POST",
            url: "../Forms/encargadoProyecto.aspx/guardar",
            data: items,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
                if (response.d > 0) {




                    $("#modal-encargado").hide();
                    $("#seccionTabla").show();

                    $("#registroGuardado").modal("show");
                    $("#btnClose").addClass("alert-success");
                    $("#head-color").addClass("alert-success");
                    $("#msg").text('Registro guardado correctamente...');


                    $("#tbodyDatos").children("tr").remove();
                    listarRegistros();


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
function listarRegistros() {
    $.ajax({
        type: "POST",
        url: "../Forms/encargadoProyecto.aspx/getRegistros",
    
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            var tr = "";
            for (var i = 0; i < data.length; i++) {
                tr += '<tr>';
                tr += '<td>' + data[i].nombre + '</td>';
                tr += '<td>' + data[i].cargo + '</td>';
                tr += '<td>' + data[i].correo + '</td>';
              
                tr += "<td align-center>";
                tr += "<label class='btn btn-success' id = " + data[i].idEncargadoProyecto + " onClick='editar("+data[i].idEncargadoProyecto +")'>Editar</label>";
                tr += "</td>";
                tr += "<td align-center>";
                tr += "<label class='btn btn-danger' id = " + data[i].idEncargadoProyecto + " onClick='eliminar(" + data[i].idEncargadoProyecto +")'>Eliminar</label>";
                tr += "</td>";
                tr += '</tr>';
            }
            $("#tbodyDatos").append(tr);
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
        url: "../Forms/encargadoProyecto.aspx/eliminar",
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
                listarRegistros();

            } else {
                $("#registroGuardado").modal("show");
                $("#btnClose").addClass("alert-danger");
                $("#head-color").addClass("alert-danger");
                $("#msg").text('No se pudo eliminar el registro...');




            }
        }
    });
}


function editar(idSeleccionado) {
    var value = idSeleccionado;
    accion = "updated";
    var parametros = new Object();
    parametros.id = value;
    var id = JSON.stringify(parametros);
    $.ajax({
        type: "POST",
        url: "../Forms/encargadoProyecto.aspx/getRegistro",
        data: id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;

            $("#txtNombre").val(data.nombre);
            $("#txtCargo").val(data.cargo);
            $("#txtCorreo").val(data.correo);

            id_Seleccionado = data.idEncargadoProyecto;
            $("#seccionTabla").hide();

            $("#modal-encargado").show();
        }
    });
}
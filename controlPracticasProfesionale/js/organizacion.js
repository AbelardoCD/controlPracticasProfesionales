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
        $("#modal-organizacion").hide();

        $("#btnNuevo").click(function () {
            $('#formDatos')[0].reset();
            $("#seccionTabla").hide();
            $("#modal-organizacion").show();
            accion = "Nuevo";
        });

        $("#btnGuardar").click(function (e) {
            guardar(e);
        });

        $("#btnCancelar").click(function () {
            $("#modal-organizacion").hide();
            $("#seccionTabla").show();
        });


        listarOrganizaciones();
    }
});



function listarOrganizaciones() {
    $.ajax({
        type: "POST",
        url: "../Forms/organizacion.aspx/getOrganizaciones",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            for (var i = 0; i < data.length; i++) {
                var tr = "";

                tr += '<tr>';
                tr += '<td>' + data[i].nombre + '</td>';
                tr += '<td>' + data[i].direccion + '</td>';
                tr += '<td>' + data[i].ciudad + '</td>';
                tr += '<td>' + data[i].estado + '</td>';
                tr += '<td>' + data[i].telefono + '</td>';
                tr += '<td>' + data[i].correo + '</td>';
                tr += '<td>' + data[i].sector + '</td>';

                tr += "<td align-center>";
                tr += "<label class='btn btn-success' id = " + data[i].idOrganizacion + " onClick='editar(" + data[i].idOrganizacion + ")'>Editar</label>";
                tr += "</td>";
                tr += "<td align-center>";
                tr += "<label class='btn btn-danger' id = " + data[i].idOrganizacion + " onClick='eliminar(" + data[i].idOrganizacion + ")'>Eliminar</label>";
                tr += "</td>";
                tr += '</tr>';

                $("#tbodyDatos").append(tr);

            }

        }
    });
}


function guardar(e) {
    e.preventDefault();
    var hasErrors = $('form[name="formDatos"]').validator('validate').has('.has-error').length;

    console.log(hasErrors);
    if (!hasErrors) {
        var parametros = new Object();
        parametros.idOrganizacion = id_Seleccionado;
        parametros.nombre = $("#txtNombre").val();
        parametros.direccion = $("#txtDireccion").val();
        parametros.ciudad = $("#txtCiudad").val();
        parametros.estado = $("#txtEstado").val();
        parametros.telefono = $("#txtTelefono").val();
        parametros.correo = $("#txtCorreo").val();
        parametros.sector = $("#comboSector").val();

        var items = new Object();
        items.organizacion = parametros;
        items.accion = accion;

        items = JSON.stringify(items);

        $.ajax({
            type: "POST",
            url: "../Forms/organizacion.aspx/guardar",
            data: items,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {

                if (response.d > 0) {
                    $("#modal-organizacion").hide();
                    $("#seccionTabla").show();

                    $("#registroGuardado").modal("show");
                    $("#btnClose").addClass("alert-success");
                    $("#head-color").addClass("alert-success");
                    $("#msg").text('Registro guardado correctamente...');


                    $("#tbodyDatos").children("tr").remove();
                    listarOrganizaciones();


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
function eliminar(idSeleccionado) {
    var value = idSeleccionado;
    var parametros = new Object();
    parametros.id = value;
    var id = JSON.stringify(parametros);
    $.ajax({
        type: "POST",
        url: "../Forms/organizacion.aspx/eliminarRegistro",
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
                listarOrganizaciones();

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
    accion = "updated";
    var parametros = new Object();
    parametros.id = idSeleccionado;
    var id = JSON.stringify(parametros);
    $.ajax({
        type: "POST",
        url: "../Forms/organizacion.aspx/getRegistro",
        data: id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {

            var data = response.d;

            $("#txtNombre").val(data.nombre);
            $("#txtDireccion").val(data.direccion);
            $("#txtCiudad").val(data.ciudad);
            $("#txtEstado").val(data.estado);
            $("#txtTelefono").val(data.telefono);
            $("#txtCorreo").val(data.correo);
            $("#comboSector").val(data.sector);
            id_Seleccionado = data.idOrganizacion;

            $("#seccionTabla").hide();
            $("#modal-organizacion").show();

        }

    });



}
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
        $("#modal-usuario").hide();
        getComboTipoUsuario();
        listarUsuarios();

        $("#btnNuevo").click(function () {
            $('#formDatos')[0].reset();
            $("#seccionTabla").hide();
            $("#modal-usuario").show();

            accion = "Nuevo";

        });
        $("#btnGuardar").click(function (e) {
            nuevoUsuario(e);
        });

        $("#btnCancelar").click(function () {
            $("#modal-usuario").hide();
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

function listarUsuarios() {

    $.ajax({
        type: "POST",
        url: "../Forms/usuarios.aspx/getUsuarios",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            for (var i = 0; i < data.length; i++) {
                var tr = "";
                if (data[i].tipoUsuario == 1) {
                    tipoUsuario = "Administrador";
                } else {
                    tipoUsuario = "Alumno";
                }
                tr += '<tr>';
                tr += '<td>' + data[i].nombre + '</td>';
                tr += '<td>' + data[i].email + '</td>';
                tr += '<td>' + data[i].matricula + '</td>';
                tr += '<td>' + data[i].contrasenia + '</td>';
                tr += '<td>' + tipoUsuario + '</td>';
                tr += "<td align-center>";
                tr += "<label class='btn btn-success' id = " + data[i].matricula + " onClick='editar(this)'>Editar</label>";
                tr += "</td>";
                tr += "<td align-center>";
                tr += "<label class='btn btn-danger' id = " + data[i].matricula + " onClick='eliminar(this)'>Eliminar</label>";
                tr += "</td>";
                tr += '</tr>';

                $("#tbodyDatos").append(tr);

            }

        }
    });
}

function nuevoUsuario(e) {
    e.preventDefault();
    var hasErrors = $('form[name="formDatos"]').validator('validate').has('.has-error').length;

    console.log(hasErrors);
    if (!hasErrors) {
        var parametros = new Object();
        parametros.matricula = $("#txtMatricula").val();
        parametros.contrasenia = $("#txtContrasenia").val();
        parametros.nombre = $("#txtNombre").val();
        parametros.email = $("#txtEmail").val();
        parametros.status = $("#txtStatus").val();
        parametros.tipoUsuario = $("#comboTipoUsuario").val();

        var items = new Object();
        items.usuario = parametros;
        items.accion = accion;

        items = JSON.stringify(items);


        $.ajax({
            type: "POST",
            url: "../Forms/usuarios.aspx/GuardarUsuario",
            data: items,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
                if (response.d > 0) {




                    $("#modal-usuario").hide();
                    $("#seccionTabla").show();

                    $("#registroGuardado").modal("show");
                    $("#btnClose").addClass("alert-success");
                    $("#head-color").addClass("alert-success");
                    $("#msg").text('Registro guardado correctamente...');


                    $("#tbodyDatos").children("tr").remove();
                    listarUsuarios();


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

function getComboTipoUsuario() {


    $.ajax({
        type: "POST",
        url: "../Forms/usuarios.aspx/getTipoUsuarios",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;

            var opcion = "";
            var tipoUsuario = "";
            for (var i = 0; i < data.length; i++) {
                if (data[i].tipo == 1) {
                    tipoUsuario = "Administrador";
                } else {
                    tipoUsuario = "Alumno";

                }

                opcion += '<option value = "' + data[i].idTipoUsuario + '">' + tipoUsuario + '</option>';
            }
            $('#comboTipoUsuario').empty().append(opcion);

        }


    });
}

function eliminar(idSeleccionado) {
    var value = idSeleccionado.id;
    var parametros = new Object();
    parametros.id = value;
    var id = JSON.stringify(parametros);
    $.ajax({
        type: "POST",
        url: "../Forms/usuarios.aspx/eliminarRegistro",
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
                listarUsuarios();   

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
    var value = idSeleccionado.id;
    accion = "updated";
    var parametros = new Object();
    parametros.id = value;
    var id = JSON.stringify(parametros);
    $.ajax({
        type: "POST",
        url: "../Forms/usuarios.aspx/getRegistro",
        data: id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;

            $("#txtMatricula").val(data.matricula);
            $("#txtContrasenia").val(data.contrasenia);
            $("#txtNombre").val(data.nombre);
            $("#txtEmail").val(data.email);
            $("#txtStatus").val(data.status);
            $("#comboTipoUsuario").val(data.tipoUsuario);

            id_Seleccionado = data.matricula;
            $("#seccionTabla").hide();

            $("#modal-usuario").show();
        }
    });
}
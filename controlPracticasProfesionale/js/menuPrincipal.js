var id_Seleccionado = "";
var fecha = "";
var fileGlobal = "";
var tipoUsuario = "";
$(document).ready(function () {

    if (sessionStorage.getItem("matricula") == "" || sessionStorage.getItem("matricula") == null || sessionStorage.getItem("matricula") == undefined) {
        window.location = "../Forms/login.aspx";
    } else {
        if (sessionStorage.getItem("user") == 1) {
            $("#seccionAdmin").show();
            $("#itemproyectos").hide();
            tipoUsuario = 1;
            $("#btnFormReporte").hide();
        } else {
            $("#seccionAdmin").hide();
            $("#itemproyectos").show();
            tipoUsuario = 2;

        }

        var d = new Date();
        fecha = d.getFullYear();

        $("#userName").text(sessionStorage.getItem("nombre"));


        proyectoAsignado();
        consultarExpediente();
        consultarEstudiantes();
       // consultarReporte();
        $("#cerrarSesion").click(function () {
            sessionStorage.removeItem('matricula');
            sessionStorage.removeItem('nombre');
            sessionStorage.removeItem('user');
            window.location = "../Forms/login.aspx";

        });


        $("#btnFormReporte").click(function () {
            $("#modalReporte").modal("show");
        });

        $("#btnGuardar").click(function (e) {
            guardarReporte(e);
        });

        $("#btnCancel").click(function () {
            $("#modalReporte").modal("hide");
        });
        $("#cerrarModalReportes").click(function () {
            $("#tablaReportes").modal("hide");
        });

    }


    var _URL = window.URL || window.webkitURL;
    $("#fileReporte").on('change', function () {

        var file;
        if ((file = this.files[0])) {

            fileGlobal = file;

        }
    });

   

});

function proyectoAsignado() {
    var parametros = new Object();
    parametros.matricula = sessionStorage.getItem("matricula");


    parametros = JSON.stringify(parametros);
    $.ajax({
        type: "POST",
        url: "../Forms/menuPrincipal.aspx/GetAsignaciones",
        data: parametros,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            for(var i = 0; i < data.length; i++) {
                $("#nombre").text(data[i].nombreAlumno);   
                $("#proyecto").text(data[i].proyecto);   
                $("#profesor").text(data[i].profesor);   


            }
        }
    });
}

function guardarReporte(e) {
    e.preventDefault();
    var hasErrors = $('form[name="formDatos"]').validator('validate').has('.has-error').length;

    console.log(hasErrors);
    if (!hasErrors) {
        var formData = new FormData();
        formData.append('file', fileGlobal);
        formData.append('Horas', $("#txthorasReportadas").val());
        //formData.append('FechaCarga', fecha);
        formData.append('estado', $("#txtestado").val());
        formData.append('fechaInicio', $("#fechaIni").val());
        formData.append('fechaFin', $("#fechaFin").val());
        formData.append('idExpediente', $("#comboExpediente").val());
        formData.append('numeroReporte', $("#comboNumeroReporte").val());






        $.ajax({
            type: "POST",
            url: "subirReporte.ashx",
            data: formData,
            async: true,
            success: function (status) {
                console.log(status);
                if (status == "") {

                    $("#modalReporte").modal("hide");
                    $("#registroGuardado").modal("show");
                    $("#btnClose").addClass("alert-success");
                    $("#head-color").addClass("alert-success");
                    $("#msg").text('Reporte guardado correctamente...');


                    $("#tbodyDatos").children("tr").remove();
                    consultarReporte();


                } else {
                    $("#registroGuardado").modal("show");
                    $("#btnClose").addClass("alert-danger");
                    $("#head-color").addClass("alert-danger");
                    $("#msg").text('No se puede guardar el reporte...');


                }




            },
            processData: false,
            contentType: false,
            error: function () {
                alert("Error general!");
            }

        });
    }
}

function consultarExpediente() {
    var params = new Object();
    params.matricula = sessionStorage.getItem("matricula")

    var matricula = JSON.stringify(params);
    $.ajax({
        type: "POST",
        url: "../Forms/menuPrincipal.aspx/consultarExpediente",
        data: matricula,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            var option = "";
            for (var i = 0; i < data.length; i++) {
                option += '<option value='+data[i].idExpediente+'>' +data[i].nombre +'</option>';
            }
            $("#comboExpediente").append(option);
        }
    });

}


function consultarReporte(matriculaAlumno) {
    $("#tbodyDatos").children("tr").remove();
    var matriculaAlumnoAbrir = matriculaAlumno.id;

    var params = new Object();
    params.matricula = sessionStorage.getItem("matricula")
    params.tipoUsuario = tipoUsuario;
    params.matriculaAlumno = matriculaAlumnoAbrir;
    var items = JSON.stringify(params);

    $.ajax({
        type: "POST",
        url: "../Forms/menuPrincipal.aspx/getReportes",
        data:items,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
           
            $("#tablaReportes").modal("show");
            
            var data = response.d;
            var tr = "";
            for (var i = 0; i < data.length; i++) {           
                tr += '<tr>';               
                tr += '<td class="text-center">' + data[i].numeroReporte + '</td>';             
                tr += '<td>' + data[i].nombre + '</td>';             
                tr += '<td>' + data[i].nombreEstudiante + '</td>';             
                tr += '<td>' + data[i].horas + '</td>';
                tr += '<td>' + data[i].estado + '</td>';
                tr += '<td>' + data[i].fechaInicio + '</td>';
                tr += '<td>' + data[i].fechaFin+ '</td>';
                tr += "<td align-center>";
                tr += "<label class='btn btn-success' id = " + data[i].idExpediente + "   onClick='descargar(" + data[i].idExpediente + "," + data[i].numeroReporte + " )'>Descargar</label>";
                tr += "</td>";
                
          
            }
            $("#tbodyDatos").append(tr);
        }
    });

}

function consultarEstudiantes() {
    var params = new Object();
    params.matricula = sessionStorage.getItem("matricula");
    params.tipoUsuario = tipoUsuario;
    var items = JSON.stringify(params);
    $.ajax({
        type: "POST",
        url: "../Forms/menuPrincipal.aspx/getEstudiantes",
        data: items,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            var tr = "";
            for (var i = 0; i < data.length; i++) {
                tr += '<tr>';
                tr += '<td ">' + data[i].nombre + '</td>';
                tr += '<td>' + data[i].correoElectronico + '</td>';
                tr += '<td>' + data[i].status + '</td>';
                tr += "<td align-center>";
                tr += "<label class='btn btn-success' id=" + data[i].matricula + " onClick='consultarReporte(this)'>Abrir</label>";
                tr += "</td>";
                tr += '</tr>';
            }
            $("#tbodyDatosAlumno").append(tr);
        }
    });
}


function descargar(idExpediente,numeroReporte) {

    window.open("../Forms/DescargarArhivo.ashx?idExpediente=" + idExpediente + "&&numeroReporte=" + numeroReporte);
 
}
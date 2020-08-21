var periodo = "Feb-Jul 2020";
var fecha = "2020-08-05";
    //new Date();
//fecha = fecha.getFullYear();
$(document).ready(function () {
    if (sessionStorage.getItem("matricula") == "" || sessionStorage.getItem("matricula") == null || sessionStorage.getItem("matricula") == undefined) {
        window.location = "../Forms/login.aspx";
    } else {
        if (sessionStorage.getItem("user") == 1) {
            $("#seccionAdmin").show();
            $("#itemproyectos").hide();
            $("#comboProyectoUno").attr("disabled", true);
            $("#comboProyectoDos").attr("disabled", true);
            $("#comboProyectoTres").attr("disabled", true);

        } else {
            $("#seccionAdmin").hide();
            $("#itemproyectos").show();
        }


        
        $("#userName").text(sessionStorage.getItem("nombre"));  

        console.log(fecha);
      
        getProyectosEstudiante();
        

        $("#btnGuardar").click(function () {
            guardarProyectoUno();
            guardarProyectoDos();
            guardarProyectoTres();

        });


        $("#cerrarSesion").click(function () {
            sessionStorage.removeItem('matricula');
            sessionStorage.removeItem('nombre');
            sessionStorage.removeItem('user');
            window.location= "../Forms/login.aspx";

        });
    }


});

function getProyectosEstudiante() {
    var parametros = new Object();
    parametros.matricula = sessionStorage.getItem("matricula");
    var matricula = JSON.stringify(parametros);
   
    $.ajax({
        type: "POST",
        url: "../Forms/elegirProyectos.aspx/GetSolicitudes",
        data: matricula,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            if (data.length != 0) {
                $("#comboProyectoUno").attr("disabled",true);
                $("#comboProyectoDos").attr("disabled",true);
                $("#comboProyectoTres").attr("disabled",true);
                $("#btnGuardar").attr("disabled", true);

                var opcionUno = '<option value="' + data[0].idProyecto + '">' + data[0].nombre + '</option>';
                var opcionDos = '<option value="' + data[1].idProyecto + '">' + data[1].nombre + '</option>';
                var opciontres = '<option value="' + data[2].idProyecto + '">' + data[2].nombre + '</option>';

                $("#comboProyectoUno").append(opcionUno);
                $("#comboProyectoDos").append(opcionDos);
                $("#comboProyectoTres").append(opciontres);
            } else {
                getProyectos();
            }
          

            


        }
    });
}

function getProyectos() {

    $.ajax({
        type: "POST",
        url: "../Forms/elegirProyectos.aspx/getProyectos",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {

            var data = response.d;
            var opcion = "";
            for (var i = 0; i < data.length; i++) {
                opcion += '<option value="' + data[i].idProyecto + '">' + data[i].nombre + '</option>';

            }
            $("#comboProyectoUno").append(opcion);
            $("#comboProyectoDos").append(opcion);
            $("#comboProyectoTres").append(opcion);

        }
    });

}
function guardarProyectoUno() {
    var matricula = sessionStorage.getItem("matricula");
    var parametros = new Object();
    parametros.periodo = periodo;
    parametros.fecha = fecha;
    parametros.idProyecto = $("#comboProyectoUno").val();
    parametros.matricula = matricula;

    var item = new Object();
    item.solicitud = parametros;

    item = JSON.stringify(item);

    $.ajax({
        type: "POST",
        url: "../Forms/elegirProyectos.aspx/GuardarRegistro",
        data: item,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            
        }
    });
    
}
function guardarProyectoDos() {
    var matricula = sessionStorage.getItem("matricula");
    var parametros = new Object();
    parametros.periodo = periodo;
    parametros.fecha = fecha;
    parametros.idProyecto = $("#comboProyectoDos").val();
    parametros.matricula = matricula;

    var item = new Object();
    item.solicitud = parametros;

    item = JSON.stringify(item);

    $.ajax({
        type: "POST",
        url: "../Forms/elegirProyectos.aspx/GuardarRegistro",
        data: item,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {

        }
    });

}

function guardarProyectoTres() {
    var matricula = sessionStorage.getItem("matricula");
    var parametros = new Object();
    parametros.periodo = periodo;
    parametros.fecha = fecha;
    parametros.idProyecto = $("#comboProyectoTres").val();
    parametros.matricula = matricula;

    var item = new Object();
    item.solicitud = parametros;

    item = JSON.stringify(item);

    $.ajax({
        type: "POST",
        url: "../Forms/elegirProyectos.aspx/GuardarRegistro",
        data: item,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {

            if (response.d > 0) {
                alert("registro guardado correctamente");

                getProyectosEstudiante();

            } else {
                alert("No se puede guardar");

                
            }
        }
    });

}
var periodo = "";
var idProyecto = "";
var matriculaProfesor = "";
var nrcCurso = "180121";
var horasAcumuladas = 0;
var elemValue = "";
var validarSiEstudianteTieneProyectoAsignado = "";
var datosvalidarEstudiantes = "";
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

        $("#userName").text(sessionStorage.getItem("nombre"));
        getAsignaciones();
        alumnos();
        $("#btnGuardar").click(function () {
              guardarProyecto();
             

        });


        $("#cerrarSesion").click(function () {
            sessionStorage.removeItem('matricula');
            sessionStorage.removeItem('nombre');
            sessionStorage.removeItem('user');
            window.location = "../Forms/login.aspx";

        });
    }

});

function alumnos() {
    var array = [];

    $.ajax({
        type: "POST",
        url: "../Forms/asignarProyectos.aspx/GetEstudiantes",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data= response.d;

            for (var i = 0; i < data.length; i++) {
                validar(data[i]);
            }

           

            
        }
    });
}

function validar(matricula) {
    var array = [];
    var arrayFinal = []; 
    for(var i = 0; i < validarSiEstudianteTieneProyectoAsignado.length; i++) {
        if (matricula.matricula == validarSiEstudianteTieneProyectoAsignado[i].matriculaAlumno) {          
            array.push(matricula);
        }        
    }    

    if (array.includes(matricula) == false) {
          var option = '<option value="' + matricula.matricula + '"> ' + matricula.nombre + ' </option>';
       
        $("#comboAlumnos").append(option);
    } 
}

function onChangeSelectAlumno(e) {
    // Recibe un evento correspondiente al elemento que lo disparó.
    //obtenemos el id del combo que fue seleccionado
    var idElem = "#" + e.target.id;
    //pasamos el id del combo y cachamos el valor seleccionado
    elemValue = $(idElem + " option:selected").val();
    

    switch (idElem) {

        case "#comboAlumnos":
            $("#comboProyectos").addClass("alert-danger");

           if (elemValue != -1) {
               getProyectosAlumno(elemValue);
               deleteOptionSelect("comboProyectos");

           }
            break;

        case "#comboProyectos":
            $("#comboProfesores").addClass("alert-danger");
            if (elemValue != -1) {
                getProfesores();
                deleteOptionSelect("comboProfesores");
            }
            break;
    }
}

function getProyectosAlumno(mtila) {

    var params = new Object();
    params.matricula = mtila;

    var matricula = JSON.stringify(params);
     

    $.ajax({
        type: "POST",
        url: "../Forms/asignarProyectos.aspx/getProyectos",
        data: matricula,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            var option = "";
            for (var i = 0; i < data.length; i++) {
                option += '<option value="' + data[i].idProyecto + '"> ' + data[i].nombre + ' </option>';

                periodo = data[i].periodo;
                matriculaAlumno = data[i].matricula;


            }   

            $("#comboProyectos").append(option);
            $("#comboProyectos").removeClass("alert-danger");

        }
    });

}

function guardarProyecto() {
    var comboAlumnos = $("#comboAlumnos").val();
    var comboProyectos = $("#comboProyectos").val();
    var comboProfesores = $("#comboProfesores").val();

    if (comboAlumnos != -1 && comboProyectos != -1 && comboProfesores !=-1) {
        var matriculaProfesor = sessionStorage.getItem("matricula");
        var parametros = new Object();
        parametros.periodo = periodo;
        parametros.nrcCurso = nrcCurso;
        parametros.progreso = horasAcumuladas;
        parametros.idProyecto = $("#comboProyectos").val();
        parametros.matriculaProfesor = $("#comboProfesores").val();
        parametros.matriculaEstudiante = $("#comboAlumnos").val();
        var mtrAlumno = $("#comboAlumnos").val();

        var items = new Object();
        items.asignacion = parametros;
        items = JSON.stringify(items);

        $.ajax({
            type: "POST",
            url: "../Forms/asignarProyectos.aspx/setAsignacion",
            data: items,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (response) {
                if (response.d > 0) {
                    /*$("#registroGuardado").modal("show");
                    $("#btnClose").addClass("alert-success");
                    $("#head-color").addClass("alert-success");
                    $("#msg").text('Registro guardado correctamente...');

                    deleteOptionSelect("comboAlumnos");
                    deleteOptionSelect("comboProyectos");
                    deleteOptionSelect("comboProfesores");*/
                   // $("#tbodyDatos").children("tr").remove();
                    
                    cosultarAsignacion(mtrAlumno)
                    //alumnos();

                    //getAsignaciones();

                } else {
                    $("#registroGuardado").modal("show");
                    $("#btnClose").addClass("alert-danger");
                    $("#head-color").addClass("alert-danger");
                    $("#msg").text('No se puede guardar el registro...');


                }

            }
        });

    } else {
        $("#registroGuardado").modal("show");
        $("#btnClose").addClass("alert-danger");
        $("#head-color").addClass("alert-danger");        
        $("#tituloheader").text('Alerta...');

        $("#msg").text('Debes completar los tres combos...');
    }
}
// Elimina todas las opciones de un combo de seleccion.
function deleteOptionSelect(idElem) {
    $("#" + idElem).children('option:not(:first)').remove();
}

function getProfesores() {
    

    $.ajax({
        type: "POST",
        url: "../Forms/asignarProyectos.aspx/getProfesores",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            var option = "";
            for (var i = 0; i < data.length; i++) {
                option += '<option value="' + data[i].matricula + '"> ' + data[i].usuario + ' </option>';


            }

            $("#comboProfesores").append(option);
            $("#comboProfesores").removeClass("alert-danger");
        }
    });


}

function cosultarAsignacion(matricula) {
    var params = new Object();
    params.matricula = matricula;

    var matricula = JSON.stringify(params);


    $.ajax({
        type: "POST",
        url: "../Forms/asignarProyectos.aspx/consultarAsignacion",
        data: matricula,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;
            guardarExpediente(data.idAsignacion);
            
            
        }
    });
}
function guardarExpediente(idAsignacion) {
    var params = new Object();
    params.id = idAsignacion;

    var id = JSON.stringify(params);
    $.ajax({
        type: "POST",
        url: "../Forms/asignarProyectos.aspx/guardarExpediente",
        data: id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            if (response.d > 0) {
                $("#registroGuardado").modal("show");
                $("#btnClose").addClass("alert-success");
                $("#head-color").addClass("alert-success");
                $("#msg").text('Registro guardado correctamente...');

                deleteOptionSelect("comboAlumnos");
                deleteOptionSelect("comboProyectos");
                deleteOptionSelect("comboProfesores");
                $("#tbodyDatos").children("tr").remove();

                getAsignaciones();
                alumnos();

                

            } else {
                $("#registroGuardado").modal("show");
                $("#btnClose").addClass("alert-danger");
                $("#head-color").addClass("alert-danger");
                $("#msg").text('No se puede guardar el registro...');


            }

        }
    });
}
function getAsignaciones() {
    $.ajax({
        type: "POST",
        url: "../Forms/asignarProyectos.aspx/GetAsignaciones",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            var data = response.d;

            validarSiEstudianteTieneProyectoAsignado = data;

            var tr = "";
            for (var i = 0; i < data.length; i++) {
                tr += '<tr> ';
                tr += '<td class="tdbody">' + data[i].nombreAlumno+ '</td>';
                tr += '<td>' + data[i].proyecto + '</td>';
                tr += '<td>' + data[i].profesor + '</td>';

                tr += '</tr> ';

            }

            $("#tbodyDatos").append(tr);
        }
    });


}
// Elimina todas las opciones de un combo de seleccion.
function deleteOptionSelect(idElem) {
    $("#" + idElem).children('option:not(:first)').remove();
    //disableCombo(idElem);
}

//sirve para desactivar algún combo y colocar su valor en -1.
function disableCombo(idElem) {

    $("#" + idElem).attr("disabled", "disabled");
    $("#" + idElem).val(-1);

}
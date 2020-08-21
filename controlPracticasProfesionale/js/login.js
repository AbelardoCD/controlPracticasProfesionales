$(document).ready(function () {

    $("#btn-inicio").click(function (e) {
        e.preventDefault();

        var hasErrors = $('form[name="cajaInput"]').validator('validate').has('.has-error').length;


        if (!hasErrors) {

            var parametros = new Object();
            parametros.matricula = $("#usuario").val();
            parametros.password = $("#password").val();

            parametros = JSON.stringify(parametros);

            $.ajax({
                type: "POST",
                url: "../Forms/login.aspx/getRegistro",
                data: parametros,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (response) {
                    var data = response.d;
                    var user = data.matricula;

                    if (user === null || user === 0 || user === undefined) {
                        $("#lblMensajes").text('No se encontró usuario y contraseña.');
                        $("#alert-operacion-fail").show("fast", function () {
                            setTimeout(function () {
                                $("#alert-operacion-fail").hide("fast");
                            }, 2000);
                        });

                    } else {
                        sessionStorage.setItem("matricula", data.matricula);
                        sessionStorage.setItem("nombre", data.nombre);
                        sessionStorage.setItem("user", data.tipoUsuario);

                        window.location = "../Forms/menuPrincipal.aspx";

                        //sessionStorage.removeItem('nombreusuario')

                    }
                }
            });

        }

    });
});
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="controlPracticasProfesionale.Forms.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" href="">
    <!-- Bootstrap Core CSS -->
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <!-- MetisMenu CSS -->
    <link href="../vendor/metisMenu/metisMenu.min.css" rel="stylesheet">
    <!-- DataTables CSS -->
    <link href="../vendor/datatables-plugins/dataTables.bootstrap.css" rel="stylesheet">
    <!-- DataTables Responsive CSS -->
    <link href="../vendor/datatables-responsive/dataTables.responsive.css" rel="stylesheet">
    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet">
    <!-- Custom Fonts -->
    <link href="../vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <!-- styles-->
    <link href="../vendor/Styles/login.css" rel="stylesheet" type="text/css">
    <link href="../vendor/font-awesome/css/font-awesome.css" rel="stylesheet" />

    
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body>
      <div id="login">

        <div id="loginSection">
            <div id="elementos" class="col-md-4">
                <div id="logintitulo">
                    <label>LOGIN </label>

                </div>
                <div id="imgLogin">
                    <img src="../images/estudiante.png" alt=""  />
                </div>
            </div>
        </div>

        <div id="cajaInput">
            <div id="registro" class="col-md-8">
                <div>
                    <label>Usuario:</label>
                    <input class="form-control input-sm" id="usuario" type="text" required data-required-error='Requerido' pattern="[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]{2,48}" data-pattern-error="Solo se aceptan letras"/>
                </div>
                <div>
                    <label>Contraseña:</label>
                    <input class="form-control input-sm" id="password" type="password" required data-required-error='Requerido' />
                </div>
                <div>
                    <button id="btn-inicio" class="btn btn-primary">Inicio</button>
                </div>
                <div id="alert-operacion-fail"">
                    <label id="lblMensajes" ></label>
                </div>
            </div>
        </div>

    </div>

   

    <!-- jQuery -->
    <script src="../vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="../vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="../vendor/metisMenu/metisMenu.min.js"></script>


    <!-- DataTables JavaScript -->
    <script src="../vendor/datatables/js/jquery.dataTables.min.js"></script>
    <script src="../vendor/datatables-plugins/dataTables.bootstrap.min.js"></script>
    <script src="../vendor/datatables-responsive/dataTables.responsive.js"></script>




    <!-- Custom Theme JavaScript -->
    <script src="../dist/js/sb-admin-2.js"></script>
    <script src="../js/validator.js"></script>
    <script src="../js/login.js"></script>

</body>
</html>

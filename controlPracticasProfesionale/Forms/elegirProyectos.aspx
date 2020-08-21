<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="elegirProyectos.aspx.cs" Inherits="controlPracticasProfesionale.Forms.elegirProyectos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link href="../vendor/Styles/styles_own.css" rel="stylesheet" type="text/css">
    <link href="../vendor/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link rel="stylesheet" media="only screen and (max-width: 480px)">
    <title></title>

</head>
<body class="body-expanded">

    <div id="sideMenu" class="menu-collapsed">
        <div id="header">
            <div id="title"><span>Proyectos</span></div>

        </div>
        <div id="perfil">
            <div id="foto">
                <img src="../images/perfil.jpg" alt="" />
            </div>
            <div id="name">
                <label id="userName"></label>
            </div>
        </div>
        <div id="menu-items">
             <div class="item">
                <a href="menuPrincipal.aspx">
                    <div class="icon">
                        <img src="../images/home.png" alt="" />
                    </div>
                    <div class="titulo">Menu Principal</div>
                </a>
            </div>
              <div class="item-separator">
            </div>
            <div class="item" id="itemproyectos">
                <a href="elegirProyectos.aspx">
                    <div class="icon">
                        <img src="../images/home.png" alt="" />
                    </div>
                    <div class="titulo">Proyectos</div>
                </a>
            </div>

          
            <div id="seccionAdmin">

                <div class="item">
                    <a href="asignarProyectos.aspx">
                        <div class="icon">
                            <img src="../images/home.png" alt="" />
                        </div>
                        <div class="titulo">Asignar Proyecto</div>
                    </a>
                </div>
                <div class="item-separator">
                </div>
                <div class="item">
                    <a href="usuarios.aspx">
                        <div class="icon">
                            <img src="../images/home.png" alt="" />
                        </div>
                        <div class="titulo">Usuarios</div>
                    </a>
                </div>
                <div class="item-separator">
                </div>

                <div class="item">
                    <a href="organizacion.aspx">
                        <div class="icon">
                            <img src="../images/home.png" alt="" />
                        </div>
                        <div class="titulo">Organizaciones</div>
                    </a>
                </div>
                <div class="item-separator">
                </div>
                 <div class="item">
                    <a href="encargadoProyecto.aspx">
                        <div class="icon">
                            <img src="../images/home.png" alt="" />
                        </div>
                        <div class="titulo">Encargado Proyecto</div>
                    </a>
                </div>
                <div class="item-separator">
                </div>
                <div class="item">
                    <a href="proyecto.aspx">
                        <div class="icon">
                            <img src="../images/home.png" alt="" />
                        </div>
                        <div class="titulo">Proyectos</div>
                    </a>
                </div>
            </div>
            </div>

    </div>

    <div id="main-container">
        <div id="head">
            <div class="col-md-11"></div>
            <div class="col-md-1" style="padding-top:10px;">
                <label class="btn btn-danger" id="cerrarSesion">
                    <img src="../images/logout.png" width="20" height="20"  />
                </label>
            </div>

        </div>
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-3" style="text-align: center;">
                <h3>Elige 3 proyectos</h3>
            </div>
            <div class="col-md-4"></div>

        </div>

        <div class="row">
            <form>
                <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-3 form-group" style="text-align: center;">
                        <label for="comboEstatus">
                            Primer Proyecto
                        </label>
                        <select class="form-control" id="comboProyectoUno">
                        </select>
                    </div>

                    <div class="col-md-3 form-group" style="text-align: center;">
                        <label for="comboEstatus">
                            Segundo Proyecto
                        </label>
                        <select class="form-control" id="comboProyectoDos">
                        </select>
                    </div>

                    <div class="col-md-3 form-group" style="text-align: center;">
                        <label for="comboEstatus">
                            Tercer Proyecto
                        </label>
                        <select class="form-control" id="comboProyectoTres">
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-5"></div>
                    <label id="btnGuardar" class="btn btn-success col-md-1" >Guardar</label>
                </div>
            </form>
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

    <script src="../js/elegirProyecto.js"></script>
    <script src="../js/validator.js"></script>


</body>
</html>

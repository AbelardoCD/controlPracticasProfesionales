<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="menuPrincipal.aspx.cs" Inherits="controlPracticasProfesionale.Forms.menuPrincipal" %>

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
            <div id="title"><span>Menu pricipal</span></div>

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
            <div class="col-md-1" style="padding-top: 10px;">
                <label class="btn btn-danger" id="cerrarSesion">
                    <img src="../images/logout.png" width="20" height="20" />
                </label>
            </div>

        </div>

        <div id="proyectoAsignado">
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8" id="cardProyecto" style="text-align: center; margin-top: 10px;">
                    <div>
                        <h2>
                            <label>Proyecto</label></h2>
                    </div>
                    <div class="row">

                        <div class="col-md-4">
                            <label id="">Alumno: </label>
                            <label id="nombre"></label>

                        </div>
                        <div class="col-md-4">
                            <label id="proyecto"></label>
                        </div>

                        <div class="col-md-4">
                            <label id="">Profesor: </label>
                            <label id="profesor"></label>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div id="subirReporte" style="margin-top: 10px;">
            <div class="col-md-2"></div>
            <div class="col-md-8 alert-success" style="border-radius: 10px;">
                <div class="row">
                    <div class="col-md-11 text-center">
                        <h1>
                            <label>Subir Reportes</label></h1>
                    </div>
                </div>
                <div class="row ">
                    <div class="col-md-11 text-center">

                        <label id="btnFormReporte" class="btn btn-primary">Reportes</label>
                    </div>
                </div>
            </div>
        </div>


           <div class="row">
            <div class="col-md-1"></div>
            <div class="col-md-11">
          
           <div id="seccionTablaEstudiantes" style="margin-top:50px;">
            <div class="row" ">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <h4><label class="alert-success">Alumnos</label> </h4>
                    
                    <div id="barra"></div>
                <table class="table table-bordered table-hover">
                    <thead >
                        <tr>
                            <th>Nombre</th>                       
                            <th>Correo</th>           
                            <th>Proyecto</th>                                                                                                          
                            <th></th>

                        </tr>
                    </thead>
                    <tbody id="tbodyDatosAlumno"></tbody>
                </table>
            </div>
                </div>
        </div>
                    </div>
            </div>

        


        <div class="row">
            <div class="col-md-1"></div>
            <div class="col-md-11">
          <div class="modal" id="tablaReportes">
               <div class="modal-content">
                <div class="modal-content">
                    <div class="modal-header">
                       <div id="seccionTabla" style="margin-top:50px;">
                        <div class="row" >
                            <div class="col-md-12">
                                <h4><label class="alert-success">REPORTES</label> </h4>
                    
                    <div id="barra"></div>
                <table class="table table-bordered table-hover">
                    <thead >
                        <tr>
                            <th style="width:12%;">Num R.</th>                       
                            <th>Proyecto</th>           
                            <th>Estudiante</th>                                                   
                            <th class="th-tamanio">Horas R.</th>                           
                            <th>Estado</th>
                            <th>Fecha I.</th>
                            <th>Fecha F.</th>
                            
                            <th></th>

                        </tr>
                    </thead>
                    <tbody id="tbodyDatos"></tbody>
                </table>
                                <div class="text-right">
                                <label class="btn btn-success text-align:center" id="cerrarModalReportes">Cerrar</label>
                                    </div>
            </div>
                </div>
        </div>
                    </div>
            </div>

        </div>  
             </div>
            </div>

        </div>  

        <div id="modalReporte" class="modal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <label>Capture los datos referentes a su reporte</label>
                    </div>
                    <div class="modal-body">
                        <form role="form" id="formDatos" name="formDatos" data-toggle="validator">
                           <div class="form-group">
                               <label>Numero de reporte</label>
                               <select class="form-control" id="comboNumeroReporte">
                                   <option value="1">1</option>
                                   <option value="2">2</option>
                                   <option value="3">3</option>
                                   <option value="4">4</option>

                               </select>
                           </div>
                            <div class="form-group">
                                <label>Horas Reportadas</label>
                                <input id="txthorasReportadas" class="no-simbolos-especiales form-control campo-editable" type="number" required data-required-error='Requerido'/>
           <div class="help-block with-errors"></div>

                            </div>
                            <div class="form-group">
                                <label>Estado</label>
                                <input id="txtestado" class="no-simbolos-especiales form-control campo-editable" type="text" required data-required-error='Requerido'/>
           <div class="help-block with-errors"></div>

                            </div>
                            <div class="form-group">
                                <label>Reporte</label>
                                <input id="fileReporte" class="no-simbolos-especiales form-control campo-editable" type="file" required data-required-error='Requerido'/>
           <div class="help-block with-errors"></div>

                            </div>
                            <div class="form-group">
                                <label>Fecha Inicio</label>
                                <input id="fechaIni" class="no-simbolos-especiales form-control campo-editable" type="date" required data-required-error='Requerido'/>
           <div class="help-block with-errors"></div>

                            </div>
                            <div class="form-group">
                                <label>Fecha Fin</label>
                                <input id="fechaFin" class="no-simbolos-especiales form-control campo-editable" type="date" required data-required-error='Requerido'/>
           <div class="help-block with-errors"></div>

                            </div>
                            <div class="form-group">
                                <label>Expediente</label>

                                <select class="form-control" id="comboExpediente">
                                </select>
                            </div>
                            <div class="form-group">
                                <label id="btnGuardar" class="btn btn-success">Guardar</label>
                                <label id="btnCancel" class="btn btn-default">Cancelar</label>

                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>

    </div>



      <!-- The Modal -->
    <div class="modal" id="registroGuardado">
        <div class="modal-dialog">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header" id="head-color">
                    <h4 class="modal-title">Modal Heading</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <label id="msg"></label>
                </div>

                <!-- Modal footer -->
                <div class="modal-footer">
                    <button id="btnClose" type="button" class="btn" data-dismiss="modal">Close</button>
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

    <script src="../js/menuPrincipal.js"></script>
    <script src="../js/validator.js"></script>


</body>
</html>

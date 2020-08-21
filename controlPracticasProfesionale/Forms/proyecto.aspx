<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="proyecto.aspx.cs" Inherits="controlPracticasProfesionale.Forms.proyecto" %>


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

      
            <div id="seccionTabla" style="margin:50px 10px 10px 10px;">
            <div class="row">
                
                <div class="col-md-12" >
                    <h4><label class="alert-success">PROYECTOS</label> <label id="btnNuevo" class="btn btn-primary pull-right col-md-2">Nuevo</label></h4>
                    
                    <div id="barra"></div>
                <table class="table table-bordered table-hover">
                    <thead >
                        <tr>
                            <th>Nombre</th>
                            <th>Descripcion</th>
                            <th>Alumnos</th>
                            <th>A.Asignados</th>
                            <th>Status</th>
                            <th>Organización</th>
                            <th>Encargado</th>

                            <th></th>
                            <th></th>

                        </tr>
                    </thead>
                    <tbody id="tbodyDatos"></tbody>
                </table>
            </div>
                </div>
        </div>
       


    </div>

      <div id="modal-proyecto" class="container-fluid">
        <header class="header">
            <label class="col-form-label">NUEVO PROYECTO</label>
        </header>
        <form role="form" id="formDatos" name="formDatos" data-toggle="validator">
            <div class="form-group">
                <label class="col-form-label">Nombre</label>
                <input id="txtNombre" class="form-control" type="text" required data-required-error='Requerido'/>
           <div class="help-block with-errors"></div>
                </div>
            <div class="form-group">
                <label class="col-form-label">Descripción</label>
                <input id="txtDescripcion" class="no-simbolos-especiales form-control campo-editable" type="text" required data-required-error='Requerido' />
                 <div class="help-block with-errors"></div>
            </div>
            <div class="form-group">
                <label class="col-form-label">Estudiantes</label>
                <input id="txtEstudiantes" class="no-simbolos-especiales form-control campo-editable" type="text" required data-required-error='Requerido'/>
                 <div class="help-block with-errors"></div>
            </div>
             <div class="form-group">
                <label class="col-form-label">Numero de Estudiantes Asignados </label>
                <input id="txtEstudiantesAsignados" class="no-simbolos-especiales form-control campo-editable" type="text" required data-required-error='Requerido'/>
                  <div class="help-block with-errors"></div>
            </div>
            <div class="form-group">
                <label class="col-form-label">Status</label>
                <input id="txtStatus" class="no-simbolos-especiales form-control campo-editable" type="text" required data-required-error='Requerido'>
                 <div class="help-block with-errors"></div>
            </div>
            
            <div class="form-group">
                <label class="col-form-label">Organización</label>
                <select class="form-control" id="comboOrganizacion" required data-required-error='Requerido' >                                               
                </select>
                 <div class="help-block with-errors"></div>
            </div>
            <div class="form-group">
                <label class="col-form-label">Encargado Proyecto</label>
                <select class="form-control" id="comboEncargado"  required data-required-error='Requerido'  >            
                </select>
                 <div class="help-block with-errors"></div>
            </div>
        </form>
        <button id="btnGuardar" class="btn btn-primary">Guardar</button>
        <button id="btnCancelar" class="btn btn-default">Cancelar</button>

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

    <script src="../js/proyecto.js"></script>
    <script src="../js/validator.js"></script>
</body>
</html>

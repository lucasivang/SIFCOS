<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Enviar2.aspx.cs" Inherits="SIFCOS.Enviar2" Async="true" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link href="metronic/assets/global/css/components.css" rel="stylesheet" type="text/css" />
 <link href="metronic/assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
 <link href="metronic/assets/admin/layout/css/layout.css" rel="stylesheet" type="text/css" />
 <link href="metronic/assets/admin/layout/css/themes/default.css" rel="stylesheet"
     type="text/css" id="style_color" />
 <link href="metronic/assets/admin/layout/css/custom.css" rel="stylesheet" type="text/css" />
 <!-- END THEME STYLES -->
 <link rel="shortcut icon" href="Resources/favicon.ico" />
    <link href="metronic/assets/global/plugins/font-awesome/css/font-awesome.min.css"
    rel="stylesheet" type="text/css" />
<link href="metronic/assets/global/plugins/font-awesome/css/font-awesome.css" rel="stylesheet"
    type="text/css" />
<link href="metronic/assets/global/plugins/simple-line-icons/simple-line-icons.min.css"
    rel="stylesheet" type="text/css" />
<link href="metronic/assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet"
    type="text/css" />
<link href="metronic/assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet"
    type="text/css" />
<link href="metronic/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css"
    rel="stylesheet" type="text/css" />
<!-- END GLOBAL MANDATORY STYLES -->
<!-- BEGIN PAGE LEVEL STYLES -->
<link rel="stylesheet" type="text/css" href="metronic/assets/global/plugins/select2/select2.css" />
<!-- END PAGE LEVEL SCRIPTS -->
<!-- BEGIN THEME STYLES -->
<link href="metronic/assets/global/css/components.css" rel="stylesheet" type="text/css" />
<link href="metronic/assets/global/css/plugins.css" rel="stylesheet" type="text/css" />
<link href="metronic/assets/admin/layout/css/layout.css" rel="stylesheet" type="text/css" />
<link href="metronic/assets/admin/pages/css/error.css" rel="stylesheet" type="text/css" />
<link href="metronic/assets/admin/layout/css/custom.css" rel="stylesheet" type="text/css" />
<link href="metronic/assets/global/plugins/bootstrap-datepicker/css/datepicker3.css"
    rel="stylesheet" type="text/css" />
<link href="Styles/Custom.css" rel="stylesheet" type="text/css" />
<link href="Styles/w3.css" rel="stylesheet" type="text/css" />
    
<script src="metronic/assets/global/plugins/jquery-1.11.0.min.js" type="text/javascript"></script>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form runat="server">
        <%--MODAL NOTIFICAICON ENVIO CIDI--%>
        <div id="modalNotificacionEnvioCIDI" runat="server" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-8">
                <header class="w3-container w3-teal">
                    <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                        <li style="float: left;">
                            <h3>Resultado notificación</h3>
                        </li>
                        <li style="float: right;">
                            <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                        </li>
                    </ul>
                </header>
                <div class="w3-container">
                    <div class="panel panel-default">
                        <div class="panel-body">
                    
                                <div class="row">
                                    <div class="col-md-12 alert alert-success alert-dismissable">
                                        <label>
                                            Sr Administrador, se realizó el envio de notificación CIDI con éxito.</label>
                                    </div>
                                </div>
                        
                        </div>
                    </div>
                </div>
                <footer class="w3-container w3-teal">
                    <asp:Button runat="server" ID="btnAceptar"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnAceptar_OnClick"/>
                </footer>
            </div>
        </div>
        <%--MODAL NOTIFICAICON NO ENVIO CIDI--%>
        <div id="modalNotificacionNOenvioCIDI" runat="server" class="w3-modal">
            <div class="w3-modal-content w3-animate-top w3-card-8">
                <header class="w3-container w3-teal">
                    <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                        <li style="float: left;">
                            <h3>Resultado notificación</h3>
                        </li>
                        <li style="float: right;">
                            <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                        </li>
                    </ul>
                </header>
                <div class="w3-container">
                    <div class="panel panel-default">
                        <div class="panel-body">
                    
                            <div class="row">
                                <div class="col-md-12 alert alert-success alert-dismissable">
                                    <label>
                                        Sr Administrador, Hubo un error en el envio de notificación CIDI ...Reintente mas tarde....</label>
                                </div>
                            </div>
                        
                        </div>
                    </div>
                </div>
                <footer class="w3-container w3-teal">
                    <asp:Button runat="server" ID="btnNoEnvio"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnNoEnvio_OnClick"/>
                </footer>
            </div>
        </div>
    </form>
    
</body>
</html>

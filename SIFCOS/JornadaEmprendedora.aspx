<%@ Page Title="JornadaEmprendedora" Language="C#" MasterPageFile="Metronic_sin_login2.Master" AutoEventWireup="true"
    CodeBehind="JornadaEmprendedora.aspx.cs" Inherits="SIFCOS.JornadaEmprendedora"
    UICulture="es" Culture="es-MX" EnableEventValidation="false" %>

<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="True"
        EnableScriptLocalization="True">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="UPCombrobanteOnline">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <img src="Resources/LogosNvos/HeaderJornadaEmprendedora.jpeg" style="height: 100%" />
                </div>
            </div>
            </div>
            <br />

            <%--MODAL REGISTRAR INGRESO --%>
            <div id="divModalRegistrarIngreso" runat="server" class="w3-modal">
                <div class="w3-modal-content w3-animate-top w3-card-2">
                    <header class="portlet box yellow2">
                        <ul style="list-style-type: none; margin: 0; padding: 0; overflow: hidden;">
                            <li style="text-align: center;color: white">
                                <h3>CHECK IN</h3>
                            </li>

                        </ul>
                    </header>
                    <div class="w3-container" style="text-align: center">
                        <div class="row">
                            <div class="col-md-12">
                                <div runat="server" id="DivAccesoPermitido" visible="false">
                                    <img src="Resources/LogosNvos/Acceso_Permitido.png" /><br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Apellido y Nombre:</label>
                                            <asp:Label runat="server" ID="lblApellidoyNombreAcceso" CssClass="form-control bold" Text=""></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <label>DNI:</label>
                                            <asp:Label runat="server" ID="lblDNIAcceso" CssClass="form-control bold" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label runat="server" ID="lblAccesoCena" CssClass="form-control bold" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                                    <div id="DivAccesoDenegado" runat="server" class="alert alert-info alert-dismissable" visible="false">
                                        <strong>EL INGRESO AL EVENTO ES A PARTIR DE LAS 18:45 HS.</strong><br />
                                        <img src="Resources/LogosNvos/Acceso_Denegado.png" />
                                    </div>
                            </div>
                        </div>
                        <br />
                    </div>
                    <footer class="portlet box yellow2" style="text-align: center">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Button runat="server" ID="btnSalir" class="btn gold" Style="margin-bottom: 10px; margin-top: 10px;" Text="SALIR" OnClick="btnSalir_OnClick" />
                            </div>
                        </div>
                    </footer>
                </div>
            </div>

            <%--MODAL ACCESO --%>
            <div id="DivAcceso" runat="server" class="w3-modal">
                <div class="w3-modal-content w3-animate-top w3-card-2">
                    <header class="portlet box yellow2">
                        <ul style="list-style-type: none; margin: 0; padding: 0; overflow: hidden;">
                            <li style="text-align: center;color: white">
                                <h3>INFORMACION</h3>
                            </li>

                        </ul>
                    </header>
                    <div class="w3-container">
                        <div class="row">
                            <div class="col-md-12">
                                <h3>EL INGRESO AL EVENTO ES EL DIA 10 DE OCTUBRE A PARTIR DE LAS 18:45 HS.
                                </h3>
                            </div>
                        </div>
                        <br />
                    </div>
                    <footer class="portlet box yellow2" style="text-align: center">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Button runat="server" ID="btnSalir2" class="btn gold" Style="margin-bottom: 10px; margin-top: 10px;" Text="SALIR" OnClick="btnSalir_OnClick" />
                            </div>
                        </div>
                    </footer>
                </div>
            </div>

            <div class="row" id="FormInscripcion" runat="server">
                <div class="col-md-12">
                    <div class="portlet box yellow2" id="form_wizard_4">
                        <div class="portlet-title">
                            <div class="caption" style="align-content: center">
                                <h2>INSCRIPCIÓN <b>"JORNADA EMPRENDEDORA DEL NOROESTE CORDOBES"</b></h2>
                                <h3><i class="fa fa-arrow-right"></i><b>Viernes 26 de Septiembre - 14h - SALON Y EL PASEO DE LA INDEPENDENCIA - VILLA DOLORES</b></h3>
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-wizard">
                                <div class="form-body">

                                    <div id="divMensajeError" runat="server" class="alert alert-danger alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                        </button>
                                        <strong>Error! </strong>
                                        <asp:Label runat="server" ID="lblMensajeError"></asp:Label>
                                    </div>
                                    <div id="divMensajeExito" runat="server" class="alert alert-success alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                        </button>
                                        <strong>Exito! </strong>
                                        <asp:Label runat="server" ID="lblMensajeExito"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="alert alert-block alert-info fade in">
                                                <h4 class="alert-heading">
                                                    <i class="fa fa-info-circle"></i>Información a tener en cuenta</h4>

                                                Para ingresar al evento se le solicitará la entrada con el código QR que se descargará al completar el formulario con sus datos.
                                                        <br />
                                                No es necesario imprimirlo en papel, simplemente tenerlo disponible en su teléfono celular.<br />
                                                El código QR es personal e intransferible.<br />


                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4 center-block">
                                            <h5>SELECCIONE LA OPCION CORRESPONDIENTE</h5>
                                            <asp:DropDownList ID="ddlFiltroBusqueda" CssClass="form-control select2me" runat="server"
                                                OnSelectedIndexChanged="ddlFiltroBusqueda_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Text="SELECCIONE OPCION" Value="00"></asp:ListItem>
                                                <asp:ListItem Text="EMPRENDEDOR" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="ESTUDIANTE" Value="02"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row" runat="server" id="divCUIT" visible="false">
                                        <%--<div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    Razón Social</label>
                                                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" AutoPostBack="True" Width="100%"
                                                    placeholder="Ingrese las primeras letras de la razon social y seleccione una"></asp:TextBox>
                                                <Ajax:AutoCompleteExtender ServiceMethod="BuscarRazonSocial" CompletionInterval="100"
                                                    EnableCaching="false" CompletionSetCount="10" TargetControlID="txtRazonSocial"
                                                    ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false" MinimumPrefixLength="3"
                                                    OnClientItemSelected="ace_Empresa" UseContextKey="true" OnClientShown="onDataShown">
                                                </Ajax:AutoCompleteExtender>

                                            </div>
                                        </div>--%>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    CUIT
                                                </label>
                                                <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                                                <Ajax:FilteredTextBoxExtender ID="txtCuit_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtCuit">
                                                </Ajax:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div runat="server" id="divCUIL" visible="false">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>CUIL</label>
                                                    <asp:TextBox ID="txtCUIL" runat="server" CssClass="form-control" MaxLength="11"
                                                        placeholder="INGRESE SU CUIL PERSONAL SIN GUIONES"></asp:TextBox>
                                                    <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="txtCUIL">
                                                    </Ajax:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>EMAIL</label>
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                                                        placeholder="ingrese email"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <label>Código área Tel.</label>
                                                    <asp:TextBox ID="txtCodAreaCel" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                    <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="txtCodAreaCel">
                                                    </Ajax:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Teléfono</label>
                                                    <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control" MaxLength="9"></asp:TextBox>
                                                    <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="txtCelular">
                                                    </Ajax:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                    <div class="form-actions" runat="server" id="divAcciones">
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="btnConsultar" CssClass="btn gold default form-control"
                                                Text="Consultar" OnClick="btnConsultar_OnClick" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="btnLimpiarFiltros" CssClass="btn gold default form-control"
                                                Text="Consultar Otro" OnClick="btnLimpiarFiltros_OnClick"></asp:Button>
                                        </div>
                                    </div>
                                    <div id="divInscripcion" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label>Nro Inscripción:</label>
                                                <asp:Label runat="server" ID="lblInscripcion" CssClass="form-control labelSecundario"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divResultados" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label>Razón Social:</label>
                                                <asp:Label runat="server" ID="lblRazonSocial" CssClass="form-control labelSecundario" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <label>C.U.I.T:</label>
                                                <asp:Label runat="server" ID="lblCuit" CssClass="form-control labelSecundario" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                    <div id="divResultados2" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label>Nombre y Apellido:</label>
                                                <asp:Label runat="server" ID="lblNombre" CssClass="form-control labelSecundario" Text=""></asp:Label>
                                                <asp:Label runat="server" ID="lblApellido" CssClass="form-control labelSecundario" Text=""></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <label>DNI:</label>
                                                <asp:Label runat="server" ID="lblDNI" CssClass="form-control labelSecundario" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label>EMAIL:</label>
                                                <asp:Label runat="server" ID="lblEmail" CssClass="form-control labelSecundario" Text=""></asp:Label>
                                            </div>
                                            <div class="col-md-2">
                                                <label>TELEFONO:</label>
                                                <asp:Label runat="server" ID="lblTelefono" CssClass="form-control labelSecundario" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Button runat="server" ID="btnImprimir" CssClass="btn gold default form-control"
                                                    Text="Descargar Entrada" OnClick="btnImprimir_OnClick" Visible="false" />
                                            </div>
                                            <%--<div class="col-md-2">
                                                <asp:Button runat="server" ID="btnEnviar" CssClass="btn gold default form-control"
                                                    Text="Enviar Entrada" OnClick="btnEnviar_OnClick" />
                                            </div>--%>
                                        </div>
                                        <br />
                                    </div>
                                    <div class="form-actions" runat="server" id="divRegistro" visible="false">
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="btnRegistrar" CssClass="btn gold default form-control"
                                                Text="Registrar" OnClick="btnRegistrar_OnClick" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="btnConsultarOtro2" CssClass="btn gold default form-control"
                                                Text="Salir" OnClick="btnLimpiarFiltros_OnClick"></asp:Button>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-6">
                                            <img src="Resources/LogosNvos/FeedJornadaEmprendedora.jpeg" style="width: 100%" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        function onDataShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 1000001;
            //sender._popupBehavior._element.style.left = "54px"; //setea la posicion desde la izq
            //sender._popupBehavior._element.style.top = "50px"; //setea la posicion desde arriba
        }
        function ace_Empresa(sender, e) {
            var cuit = e.get_value();
            $('#ContenedorPrincipal_txtCuit').val(cuit);
        }
    </script>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="Inscripcion.aspx.cs" Inherits="SIFCOS.Inscripcion" UICulture="es"
    Culture="es-MX" %>

<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h3 class="page-title">Inscripcion SIFCoS O Reempadronamientos SIFCoS
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li><i class="fa fa-home"></i><a href="#">Inicio</a> <i class="fa fa-angle-right"></i>
            </li>
            <li><a href="Inscripcion.aspx">Inscripcion SIFCoS O Reempadronamientos SIFCoS</a> <i
                class="fa fa-angle-right"></i></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True">
    </asp:ScriptManager>
    <div id="barraProgreso" style="display: none;">
        <div id="Background">
        </div>
        <div id="Progress">
            <h6>
                <p style="text-align: center">
                    <b>Procesando Datos, Espere por favor...
						<br />
                    </b>
                </p>
            </h6>
        </div>
    </div>
    <%--MODAL SALIR DEL TRAMITE --%>
    <div id="divModalSalirDelTramite" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-8">
            <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>CONFIRMACIÓN DE LA INFORMACIÓN</h3>
                    </li>
                    <li style="float: right;">
                      <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>   
                </ul>
            </header>
            <div class="w3-container">
                <br />
                <div class="row">
                    <h4>¿Está seguro que desea salir del trámite en curso?
                    </h4>
                    <div class="alert alert-warning alert-dismissable">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                        </button>
                        <strong>Importante </strong>
                        <label>
                            Se perderán los datos ingresados hasta el momento y el trámite quedará sin efetuarse.</label>
                    </div>
                </div>
                <br />
            </div>
            <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnAcepterYSalirTramite"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnAcepterYSalirTramite_OnClick"/>
                <asp:Button runat="server" ID="btnCancelarYSeguirTramite"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelarYSeguirTramite_OnClick"/>
         
        </footer>
        </div>
    </div>
    <%--MODAL MANTENIMIENTO DEL SISTEMA TEMPORAL --%>
    <div id="divModalMantSistema" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-8">
            <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>INFORMACION</h3>
                    </li>
                    <li style="float: right;">
                        <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>   
                </ul>
            </header>
            <div class="w3-container">
                <br />
                <div class="row">
                    <div class="alert alert-warning alert-dismissable">
                        <h4>EN ESTOS MOMENTOS ESTAMOS REALIZANDO MANTENIMIENTO DEL SISTEMA...
                        </h4>
                    </div>
                </div>
                <br />
            </div>
            <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnSalir2"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="SALIR" OnClick="btnSalir2_OnClick"/>
         
        </footer>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="upConfirmaTramite">
        <ContentTemplate>
            <%--MODAL FINALIZACION DEL TRAMITE --%>
            <div id="modalFinalizacionTramite" runat="server" class="w3-modal">
                <div class="w3-modal-content w3-animate-top w3-card-8">
                    <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>CONFIRMACIÓN DE LA INFORMACIÓN</h3>
                    </li>
                    <li style="float: right;">
                       <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>   
                </ul>
            </header>
                    <div class="w3-container">
                        <br />
                        <div class="row" style="background-color: gainsboro;">
                            <div class="col-md-12">
                                <b>
                                    <asp:Label runat="server" ID="lblTituloEmpresaModalConfirmacion"></asp:Label></b>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div id="divHtml_EncabezadoModalFinalizacion" runat="server">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <!-- BEGIN Portlet PORTLET-->
                                <div class="portlet gren">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="fa fa-check-square-o"></i>Datos ingresados
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="scroller" style="height: 300px">
                                            <form role="form">
                                                <h3 class="form-section">Nombre del Comercio</h3>
                                                <div class="row">
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Nombre :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblNombreComercio" runat="server" />
                                                    </div>

                                                </div>
                                                <div runat="server" id="MostrarBoca">
                                                    <h3 class="form-section">Boca de Recepción designada</h3>
                                                    <div class="row">
                                                        <div class="col-md-2 labelPrimario">
                                                            <label>
                                                                Boca Recepción :</label>
                                                        </div>
                                                        <div class="col-md-2 labelSecundario">
                                                            <asp:Label ID="lblBocaRecepcion" runat="server" />
                                                        </div>

                                                    </div>
                                                </div>
                                                <h3 class="form-section">Domicilio del Comercio</h3>
                                                <div class="row">
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Departamento :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblDepartamento" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Localidad :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblLocalidad" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Barrio :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblBarrio" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Calle :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblCalle" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Nro./ Km. :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblNroCalle" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Cod.Postal :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblCodPos" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Piso :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblPiso" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Dpto :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblNroDepto" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Torre :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblTorre" runat="server" />
                                                    </div>
                                                    <%--<div class="col-md-2 labelPrimario">
													<label>
														Oficina :</label>
												</div>
												<div class="col-md-2 labelSecundario">
													<asp:Label ID="lblOficina" runat="server" />
												</div>--%>
                                                </div>
                                                <%--<div class="row">
												<div class="col-md-2 labelPrimario">
													<label>
														Stand :</label>
												</div>
												<div class="col-md-2 labelSecundario">
													<asp:Label ID="lblStand" runat="server" />
												</div>
												<div class="col-md-2 labelPrimario">
													<label>
														Local :</label>
												</div>
												<div class="col-md-2 labelSecundario">
													<asp:Label ID="lblLocal" runat="server" />
												</div>
											</div>--%>
                                                <h3 class="form-section">Contacto del Comercio</h3>
                                                <div class="row">
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Email :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblEmailC" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Celular :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblCelular" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Tel. Fijo :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblTelFijo" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Pagina web:</label>
                                                    </div>
                                                    <div class="col-md-4 labelSecundario">
                                                        <asp:Label ID="lblWebPag" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Facebook:</label>
                                                    </div>
                                                    <div class="col-md-4 labelSecundario">
                                                        <asp:Label ID="lblFacebook" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <h3 class="form-section">Domicilio Legal Declarado</h3>
                                                <div class="row">
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Departamento :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblDepartamentoLegal" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Localidad :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblLocalidadLegal" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Barrio :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblBarrioLegal" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Calle :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblCalleLegal" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Nro./ Km. :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblNroCalleLegal" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Cod.Postal :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblCodPosLegal" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Piso :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblPisoLegal" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Dpto :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblNroDptoLegal" runat="server" />
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            Torre :</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblTorreLegal" runat="server" />
                                                    </div>
                                                </div>
                                                <h3 class="form-section">Información General</h3>
                                                <div class="row">
                                                    <div class="col-md-3 labelPrimario">
                                                        <label>
                                                            Inicio de Actividad:</label>
                                                    </div>
                                                    <div class="col-md-1 labelSecundario">
                                                        <asp:Label ID="lblFecInicioActividad" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3 labelPrimario">
                                                        <label>
                                                            Nro Hab Municipal:</label>
                                                    </div>
                                                    <div class="col-md-1 labelSecundario">
                                                        <asp:Label ID="lblNroHabilitacionMunicipal" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2 labelPrimario">
                                                        <label>
                                                            NRO. D.G.R.:</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblNroDGR" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3 labelPrimario">
                                                        <label>
                                                            M2 en Venta:</label>
                                                    </div>
                                                    <div class="col-md-1 labelSecundario">
                                                        <asp:Label ID="lblSuperficieVenta" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3 labelPrimario">
                                                        <label>
                                                            M2 en Depósito:</label>
                                                    </div>
                                                    <div class="col-md-1 labelSecundario">
                                                        <asp:Label ID="lblSuperficioDeposito" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3 labelPrimario">
                                                        <label>
                                                            M2 en Administración:</label>
                                                    </div>
                                                    <div class="col-md-1 labelSecundario">
                                                        <asp:Label ID="lblSuperficieAdministracion" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3 labelPrimario">
                                                        <label>
                                                            Inmueble:</label>
                                                    </div>
                                                    <div class="col-md-1 labelSecundario">
                                                        <asp:Label ID="lblInmueble_PropietarioInquil" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3 labelPrimario">
                                                        <label>
                                                            Rango Alquiler:</label>
                                                    </div>
                                                    <div class="col-md-3 labelSecundario">
                                                        <asp:Label ID="lblInmueble_RangoAlquiler" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-4 labelPrimario">
                                                        <label>
                                                            Personal del Establecimiento:</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblCantPersTotal" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4 labelPrimario">
                                                        <label>
                                                            Personal en Rel. de Dependencia:</label>
                                                    </div>
                                                    <div class="col-md-2 labelSecundario">
                                                        <asp:Label ID="lblCantPersRel" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-5 labelPrimario">
                                                        <label>
                                                            ¿Posee cobertura médica? :</label>
                                                        <br />
                                                        <asp:Label ID="lblPoseeCobert_SiNo" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-5 labelPrimario">
                                                        <label>
                                                            ¿Realizó capacitación para ultimo año? :</label>
                                                        <br />
                                                        <asp:Label ID="lblCapacitacionUltAnio" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-5 labelPrimario">
                                                        <label>
                                                            ¿Posee seguro para local? :</label>
                                                        <br />
                                                        <asp:Label ID="lblPoseeSeguro" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-md-5 labelPrimario">
                                                        <label>
                                                            Origen Proveedores :</label>
                                                        <br />
                                                        <asp:Label ID="lblOrigenProveedor" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <h3 class="form-section">Productos, Actividad Primaria y Secundaria</h3>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-4 labelPrimario">
                                                        <label>
                                                            Actividad Primaria :</label>
                                                    </div>
                                                    <div class="col-md-8 labelSecundario">
                                                        <asp:Label ID="lblActividadPrimaria" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 labelPrimario">
                                                        <label>
                                                            Actividad Secundaria :</label>
                                                    </div>
                                                    <div class="col-md-8 labelSecundario">
                                                        <asp:Label ID="lblActividadSecundaria" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="gvProductosActividades_ModalFinalizacion" CssClass="table table-striped table-bordered table-advance table-hover"
                                                            runat="server" DataKeyNames="IdProducto" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:BoundField DataField="IdProducto" HeaderText="Nro" />
                                                                <asp:BoundField DataField="NProducto" HeaderText="Producto" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                                <!-- END Portlet PORTLET-->
                            </div>
                        </div>
                    </div>
                    <footer class="w3-container w3-teal">
                <%--<asp:Button runat="server" ID="btnConfirmarImprimirTramite" OnClientClick = "SetTarget();" class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Confirmar e Imprimir" OnClick="btnConfirmarImprimirTramite_OnClick"/>--%>

              
                        <asp:Button runat="server" ID="btnConfirmarImprimirTramite"  class="btn btnBuscar  btn-circle"   style="margin-bottom: 10px;margin-top: 10px;" Text="Confirmar e Imprimir" OnClick="btnConfirmarImprimirTramite_OnClick"/>
                        <%--<cc1:BotonEnviar runat="server" OnClick="btnEnviar_Onclick" class="btn btnBuscar  btn-circle"  style="margin-bottom: 10px;margin-top: 10px;"  Text="Confirmar e Imprimir" TextoEnviando="Procesando...">
                        </cc1:BotonEnviar>--%>

<%--MODAL FINALIZACION CON EXITO DEL TRAMITE --%>                    
<div id="divModalFelicitacionesFin" runat="server" class="w3-modal">
    <div class="w3-modal-content w3-animate-top w3-card-8">
        <header class="w3-container w3-teal">
            <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                <li style="float: left;">
                    <h3><asp:Label runat="server" ID="Label1" Text="Nuevo Trámite"  ></asp:Label></h3>
                </li>
                <li style="float: right;">
                   <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                </li>
            </ul>
        </header>
        <div class="w3-container">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4>
                        HA FINALIZADO EL TRAMITE CON ÉXITO..FELICITACIONES !!</h4>
                              
                </div>
                    <div class="panel-body">
                    <%--Panel content--%>
                    <asp:Button runat="server"  ID="btnDescargarComprobante" Text="Descargar Comprobante" OnClick="btnDescargarComprobante_OnClick"/>
                    </div>
            </div>
        </div>
        <footer class="w3-container w3-teal">
            <asp:Button runat="server" ID="btnIrAMisTramites"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Ver Mis Trámites" OnClick="btnIrAMisTramites_OnClick"/>
            <asp:Button runat="server" ID="btnRealizarOtroTramite"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Realizar Otro Trámite" OnClick="btnRealizarOtroTramite_OnClick"/>
            </footer>
    </div>
</div>
                   
                
                <asp:Button runat="server" ID="btnVolverFinalizacionTramite"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Volver" OnClick="btnVolverFinalizacionTramite_OnClick"/>
         
        </footer>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upConfirmaTramite">
        <ProgressTemplate>
            <div id="Background">
            </div>
            <div id="Progress">
                <h6>
                    <p style="text-align: center">
                        <b>Procesando Datos, Espere por favor...
							<br />
                        </b>
                    </p>
                </h6>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%--MODAL INFORMACION INICIAL DEL TRAMITE --%>
    <div id="modalInformacionTituloTramite" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-8">
            <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3><asp:Label runat="server" ID="lblTituloVentanaModalPrincipal" Text="Nuevo Trámite"  ></asp:Label></h3>
                    </li>
                    <li style="float: right;">
                       <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>
                </ul>
            </header>
            <div class="w3-container">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4>Información sobre el Trámite que está iniciando...</h4>
                    </div>
                    <div class="panel-body">
                        <%--Panel content--%>
                        <div id="divMensaejeErrorModal" runat="server" class="alert alert-danger alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Importante! </strong>
                            <asp:Label runat="server" ID="lblMensajeErrorModal"></asp:Label>
                        </div>
                        <h4 class="form-section" style="font-weight: bold;">Datos del Comercio</h4>
                        <div class="row">
                            <div class="col-md-2">
                                <label>
                                    CUIT</label>
                                <asp:TextBox ID="txtModalCuit" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-5">
                                <label>
                                    Razón Social</label>
                                <asp:TextBox ID="txtModalRazonSocial" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-5">
                                <label>
                                    Nombre de Fantasía</label>
                                <asp:TextBox ID="txtModalNombreFantasia" Enabled="True" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <asp:Button runat="server" ID="btnVerSucursales"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="VER DIRECCIONES SUCURSALES REGISTRADAS" OnClick="btnVerSucursales_OnClick"/>
                    
                        <%--MODAL SUCURSALES COMERCIO --%>
                        <div id="modalSucursales" runat="server" class="w3-modal">
                            <div class="w3-modal-content w3-animate-top w3-card-8">
                                <header class="w3-container w3-teal">
                                    <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                                        <li style="float: left;">
                                            <h3>SUCURSALES DEL COMERCIO REGISTRADAS</h3>
                                        </li>
                                        <li style="float: right;">
                                            <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                                        </li>   
                                    </ul>
                                </header>
                                <div class="w3-container">
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvDirecciones" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                                AutoGenerateColumns="false"  AllowPaging="true" PageSize="10" DataKeyNames="Nro_Sifcos" 
                                                OnPageIndexChanging="gvDirecciones_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="Nro_Tramite" HeaderText="Nro Ultimo Tramite" />
                                                    <asp:BoundField DataField="domicilio" HeaderText="Domicilio" />
                                                    <asp:BoundField DataField="Nro_Sifcos" HeaderText="Nro Sifcos" />
                                    
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h6>
                                                <asp:Label runat="server" ID="lblTitulocantRegistrosDir" Text="Total de Direcciones encontradas : "></asp:Label><asp:Label
                                                    runat="server" ID="lblTotalRegistrosDir" Text="0"></asp:Label></h6>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <ul class="pagination pull-right">
                                                <asp:Repeater ID="rptBotonesPaginacionDirecciones" OnItemDataBound="rptBotonesPaginacionDirecciones_OnItemDataBound"
                                                    OnItemCommand="rptBotonesPaginacionDirecciones_OnItemCommand" runat="server">
                                                    <ItemTemplate>
                                                        <li class="paginate_button">
                                                            <asp:LinkButton ID="btnNroPaginaDirecciones" OnClick="btnNroPaginaDirecciones_OnClick" CommandArgument='<%# Bind("NroPagina") %>'
                                                                runat="server" class="btn btn-default "><%# Eval("NroPagina")%></asp:LinkButton>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <footer class="w3-container w3-teal">
                                    <asp:Button runat="server" ID="BtnCerrarSucursales"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="CERRAR" OnClick="btnCerrarSucursales_OnClick"/>
         
                            </footer>
                            </div>
                        </div>
                        <div class="row" style="display: none;">
                            <div class="col-md-6">
                                <div class="dashboard-stat red-intense">
                                    <div class="visual">
                                        <i class="fa fa-comments"></i>
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            ALTA
                                        </div>
                                        <div class="desc">
                                        </div>
                                    </div>
                                    <a class="more" href="#">Iniciar Inscripción <i class="m-icon-swapright m-icon-white"></i></a>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="dashboard-stat blue-madison">
                                    <div class="visual">
                                        <i class="fa fa-comments"></i>
                                    </div>
                                    <div class="details">
                                        <div class="number">
                                            REEMPADRONAMIENTO
                                        </div>
                                        <div class="desc" style="margin-top: 4px;">
                                            <label>
                                                Nro SIFCoS :
                                            </label>
                                            <asp:TextBox runat="server" placeholder="Ingrese nro SIFCoS..." disable="true" Style="color: black;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <a class="more" href="#">Iniciar Reempadronamiento <i class="m-icon-swapright m-icon-white"></i></a>
                                </div>
                            </div>
                        </div>
                        <div id="divSeccionInscripcionTramite" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="alert alert-block alert-info fade in">
                                        <label>
                                            Sr Comerciante, está por iniciar la Inscripción a SIFCoS. Si su comercio ya figura en la lista de direcciones 
                                            NO inicie tramite nuevo hasta no dar de baja el anterior sino seleccione "Aceptar y Continuar".</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div id="divSeccionReempadronamiento" runat="server" class="col-md-8">
                                <h4 class="form-section" style="font-weight: bold;">Reempadronamiento SIFCoS</h4>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                Para iniciar trámite de reempadronamiento ingrese Nro Sifcos :
                                            </label>
                                            <asp:TextBox runat="server" ID="txtNroSifcos" CssClass="form-control" placeholder="Nro Sifcos"></asp:TextBox>
                                            <Ajax:FilteredTextBoxExtender ID="txtNroSifcos_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtNroSifcos">
                                            </Ajax:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divCheckNuevaSucursal" runat="server" class="col-md-4">
                                <h4 class="form-section" style="font-weight: bold;">Nueva Sucursal</h4>
                                <br />
                                <asp:CheckBox runat="server" Text="Dar de Alta una Nueva Sucursal" ToolTip="Haz click sólo si desea cargar una nueva sucursal si tu sede
                                no aparece"
                                    AutoPostBack="True" CssClass=" w3-border-white pull-right" ID="chkNuevaSucursal"
                                    OnCheckedChanged="chkNuevaSucursal_OnCheckedChanged" />
                            </div>
                        </div>
                        <div id="divBotonImpirmirTRS" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label runat="server" ID="lblFechaUltimoTramite" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="alert alert-block alert-warning fade in">
                                        <h4 class="alert-heading">
                                            <asp:Label ID="lblCantTrsPagas" Text="0" runat="server"></asp:Label>
                                        </h4>
                                    </div>
                                </div>
                            </div>
                            <div id="divSeccionDebeTrs" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="alert alert-block alert-warning fade in">
                                            <h4 class="alert-heading">
                                                <asp:Label ID="lblCantTrsNoPagas" Text="0" runat="server"></asp:Label>
                                            </h4>
                                            <asp:Button ID="btnImprimirTRS" class="btn btnBuscar  btn-circle" Text="Imprimir (TRS)"
                                                runat="server" OnClick="btnImprimirTRS_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="alert alert-block alert-info fade in">
                                            <b>Recuerde que de abonada la/s Tasa/s Retributiva/s, dicho importe se acreditará a
												las 72hs hábiles de realizada. </b>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnAceptarTramiteARealizar"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar y Continuar" OnClick="btnAceptarTramiteARealizar_OnClick"/>
                <asp:Button runat="server" ID="btnVolverTramiteARealizar"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Volver" OnClick="btnVolverTramiteARealizar_OnClick"/>
                

        </footer>
        </div>
    </div>
    <%--ENCABEZADO DE LA EMPRESA --%>
    <div id="divEncabezadoDatosEmpresa" runat="server">
        <div class="row">
            <div class="form-group">
                <div class="col-md-2">
                    <label>
                        CUIT</label>
                    <asp:TextBox ID="txtCuitLeido" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                </div>
                <div class="col-md-5">
                    <label>
                        Razón Social</label>
                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-5">
                    <label>
                        Nombre de Fantasía</label>
                    <asp:TextBox ID="txtNomFantasia" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <br />
    <%--DIV DE CARGA INICIAL DEL TRAMITE --%>
    <div id="divVentanaInicioTramite" runat="server" class="row">
        <div class="col-md-12">
            <div class="portlet box yellow">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-home"></i>Trámite
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-body">
                        <div id="divMensajeErrorVentanaEncabezado" runat="server" class="alert alert-danger alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Error! </strong>
                            <asp:Label runat="server" ID="lblMensajeErrorVentanaEncabezado"></asp:Label>
                        </div>
                        <h3 class="form-section">Hola
                            <asp:Label ID="lblApeyNomUsuarioLogueado" runat="server" Text=""></asp:Label>
                        </h3>
                        <div class="row">
                            <%--		<div class="col-md-4">
								<div class="form-group">
									<asp:Label ID="lblNombreCidi" runat="server" Text="NOMBRE"></asp:Label>
									<asp:TextBox runat="server" ID="txtNombreCidi" CssClass="form-control disabled" Enabled="False"></asp:TextBox>
								</div>
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<asp:Label ID="lblApellidoCidi" runat="server" Text="APELLIDO"></asp:Label>
									<asp:TextBox runat="server" ID="txtApellidoCidi" CssClass="form-control disabled"
										Enabled="False"></asp:TextBox>
								</div>
							</div>--%>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblCuilCidi" runat="server" Text="CUIL del usuario logueado" Enabled="True"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtCuilCidi" CssClass="form-control disabled" Enabled="False"
                                        MaxLength="11"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <h3 class="form-section">Trámite</h3>
                        <div class="row" runat="server" id="DivSeleccionCuit">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblSeleccionCuit" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList ID="ddlSeleccionEntidad" CssClass="form-control select2me" runat="server" AutoPostBack="True"
                                         OnSelectedIndexChanged="ddlSeleccionEntidad_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" runat="server" id="DivIngresoCuit">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblTitularTramite" runat="server" Text=""></asp:Label>
                                    <asp:TextBox runat="server" ID="txtCuit" CssClass="form-control" placeholder="CUIT de la Empresa/Titular"
                                        MaxLength="11" Enabled="false" />
                                    <Ajax:FilteredTextBoxExtender ID="txtCuit_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" FilterType="Numbers" TargetControlID="txtCuit">
                                    </Ajax:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="form-actions" style="background-color: gainsboro">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Button runat="server" ID="btnIniciarTramite" CssClass="btn botonIniciarTramite btn-circle form-control"
                                    OnClick="btnIniciarTramite_OnClick" Text="Iniciar Trámite" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--DIV DEL PASO A PASO DE CREACION DEL UN TRAMITE --%>
    <div id="divVentanaPasosTramite" runat="server" class="row">
        <div class="col-md-12">
            <div class="portlet box yellow" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-file-text-o"></i>
                        <asp:Label runat="server" ID="lblTituloTramite"></asp:Label>
                        - <span class="step-title">
                            <asp:Label ID="lblPasos" runat="server" Text=""></asp:Label>
                        </span>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-wizard">
                        <div class="form-body">
                            <ul class="nav nav-pills nav-justified steps">
                                <li id="li_tab_1" runat="server" class=""><a class="step"><span class="number">1 </span>
                                    <span class="desc"><i class="fa fa-check"></i>Tasa Retributiva</span></a></li>
                                <li id="li_tab_2" runat="server" class=""><a class="step"><span class="number">2 </span>
                                    <span class="desc"><i class="fa fa-check"></i>Documentación</span></a> </li>
                                <li id="li_tab_3" runat="server" class=""><a class="step"><span class="number">3 </span>
                                    <span class="desc"><i class="fa fa-check"></i>Establecimiento</span></a></li>
                                <li id="li_tab_4" runat="server" class=""><a class="step"><span class="number">4 </span>
                                    <span class="desc"><i class="fa fa-check"></i>Información General</span></a></li>
                                <li id="li_tab_5" runat="server" class=""><a class="step"><span class="number">5 </span>
                                    <span class="desc"><i class="fa fa-check"></i>Declaración Jurada</span></a></li>
                            </ul>
                            <div class="progress progress-striped">
                                <div id="ProgressBar" runat="server" class="progress-bar progress-bar-success">
                                </div>
                            </div>
                            <div class="row">
                                <%--<div class="form-group">
									<div class="col-md-12">
										<div class="alert alert-block alert-info fade in">
											<%--<h4 class="alert-heading">
												<i class="fa fa-info-circle"></i>Información</h4>
											<p>
												Todos los campos marcados con (*) son obligatorios.
											</p>
										</div>
									</div>
								</div>--%>
                            </div>
                            <div id="divMensajeError" runat="server" class="alert alert-danger alert-dismissable">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                </button>
                                <strong>Error! </strong>
                                <asp:Label runat="server" ID="lblMensajeError"></asp:Label>
                            </div>
                            <div id="divMensajeExito" runat="server" class="alert alert-success alert-dismissable">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                </button>
                                <strong>Éxito! </strong>
                                <asp:Label runat="server" ID="lblMensajeExito"></asp:Label>
                            </div>
                            <div id="divPanel_1" runat="server" class="tab-pane active">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="alert alert-block alert-warning fade in">
                                            <a target="_blank" href="ListaTrsReempa.aspx" class="btn btnBuscar  btn-circle">Imprimir Tasa Retributiva</a>
                                            <br/><br/>
                                            <h4>Haga click en el boton y lo redirigira a una pagina para que pueda abonar la tasa, pero puede continuar con el trámite<br/><br/>
                                                   El trámite sera evaluado por el administrador, para cuando su tasa figure abonada, será aprobado.
                                            </h4>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="alert alert-block alert-info fade in">
                                            <b>Recuerde que de abonada la/s Tasa/s Retributiva/s, dicho importe impactará en el sistema entre
                                                48hs y 72hs hábiles de realizado el pago. </b>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divPanel_2" runat="server" class="tab-pane">
                                <%--AGREGAR DOCUMENTACION ADJUNTA--%>
                                <div runat="server" id="DivAdjuntar" visible="True">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="alert alert-block alert-info fade in">
                                                <h4 class="alert-heading">
                                                    <i class="fa fa-info-circle"></i>INFORMACION A TENER EN CUENTA</h4>
                                                <ul>
                                                    <li>ESCANEE PREVIAMENTE LA HABILITACION MUNICIPAL E INSCRIPCION DE AFIP EN FORMATO PDF
														Y ADJUNTELA.<br />
                                                        ESTOS DOCUMENTOS SON OBLIGATORIO PARA FINALIZAR EL TRAMITE DE ALTA O REEMPADRONAMIENTO<br />
                                                        PARA CONTROL DE LA INFORMACION REGISTRADA. </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>


                                    <%--ADJUNTO 1--%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-section">
                                                <h5>Adjuntar <strong>Habilitación municipal</strong></h5>
                                            </div>
                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                <div>
                                                    <asp:FileUpload ID="documento1" ClientIDMode="Predictable" AllowMultiple="false"
                                                        accept="application/pdf" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Button runat="server" ID="btnAdjuntar" Text="+ Adjuntar Documento" placeholder="Adjuntar Documentación"
                                                OnClick="BtnAdjuntar1_Click" CssClass="form-control btn blue"></asp:Button>

                                        </div>
                                        <div class="col-md-6">
                                            <div id="divMensajeErrorDocumentacion_1" runat="server" class="alert alert-danger" style="margin-bottom: 0px;" visible="False">
                                                <%--<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
											</button>--%>
                                                <div class="icon">
                                                    <i class="fa fa-times-circle"></i>
                                                    <strong>Error! </strong>
                                                    <asp:Label runat="server" ID="lblMensajeErrorDocumentacion_1"></asp:Label>

                                                </div>
                                            </div>
                                            <div id="divMensajeExitoDocumentacion_1" runat="server" class="alert alert-success" style="margin-bottom: 0px;" visible="False">
                                                <%--<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
											</button>--%>
                                                <div class="icon">
                                                    <i class="fa fa-check"></i>
                                                    <strong>Exito! </strong>
                                                    <asp:Label runat="server" ID="lblMensajeExitoDocumentacion_1"></asp:Label>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <%--otro del adjunto 1--%>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Button runat="server" ID="btnAdjuntarOtro1" Text="+ Cambiar Documento" OnClick="BtnAdjuntarOtro1_Click"
                                                CssClass="form-control btn default" Visible="False"></asp:Button>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <%--ADJUNTO 2--%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-section">
                                                <h5>Adjuntar <strong>Constancia de Inscripción de AFIP</strong></h5>
                                            </div>
                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                <div>
                                                    <asp:FileUpload ID="documento2" ClientIDMode="Predictable" AllowMultiple="false"
                                                        accept="application/pdf" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Button runat="server" ID="btnAdjuntar2" Text="+ Adjuntar Documento" placeholder="Adjuntar Documentación"
                                                OnClick="BtnAdjuntar2_Click" CssClass="form-control btn blue"></asp:Button>
                                        </div>
                                        <div class="col-md-6">
                                            <div id="divMensajeErrorDocumentacion_2" runat="server" class="alert alert-danger" visible="False">
                                                <div class="icon">
                                                    <i class="fa fa-times-circle"></i>
                                                    <strong>Error! </strong>
                                                    <asp:Label runat="server" ID="lblMensajeErrorDocumentacion_2"></asp:Label>
                                                </div>
                                            </div>
                                            <div id="divMensajeExitoDocumentacion_2" runat="server" class="alert alert-success" visible="False">
                                                <%--<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
												</button>--%>
                                                <div class="icon">
                                                    <i class="fa fa-check"></i>
                                                    <strong>Exito! </strong>
                                                    <asp:Label runat="server" ID="lblMensajeExitoDocumentacion_2"></asp:Label>
                                                </div>

                                            </div>
                                        </div>
                                    </div>



                                    <%--otro del adjunto 2--%>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Button runat="server" ID="btnAdjuntarOtro2" Text="+ Cambiar Documento" OnClick="BtnAdjuntarOtro2_Click"
                                                CssClass="form-control btn default" Visible="False"></asp:Button>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div id="divPanel_3" runat="server" class="tab-pane">
                                <br />
                                <asp:UpdatePanel ID="upDomicilio1" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="portlet box yellow" id="form_wizard_DMR">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <span id="lblDomReal">I - DOMICILIO COMERCIAL</span>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body form">
                                                        <div class="form-wizard">
                                                            <div class="form-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <iframe class="tamañoIframe" id="VIN_DOM_REAL" src="<%= UrlDomComercio %>" style="min-height: 800px; width: 100%;"></iframe>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <asp:Button runat="server" ID="btnModificarDomEstab" CssClass="btn blue" Style="margin-bottom: 10px; margin-top: 10px;"
                                                                            Text="MODIFICAR DOMICILIO COMERCIAL" OnClick="btnModificarDomComercio_OnClick" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="portlet box yellow" id="form_wizard_DML">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            <span id="lblDomLegal">II - DOMICILIO LEGAL</span>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body form">
                                                        <div class="form-wizard">
                                                            <div class="form-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <iframe class="tamañoIframe" id="VIN_DOM_LEGAL" src="<%= UrlDomLegal %>" style="min-height: 800px; width: 100%;"></iframe>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <asp:Button runat="server" ID="btnModificarDomLegal" CssClass="btn blue" Style="margin-bottom: 10px; margin-top: 10px;"
                                                                            Text="MODIFICAR DOMICILIO LEGAL" OnClick="btnModificarDomLegal_OnClick" />
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
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="portlet box yellow" id="form_wizard_contacto">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                    <span id="lblContacto">III - DATOS DE CONTACTO DEL COMERCIO</span>
                                                </div>
                                            </div>
                                            <div class="portlet-body form">
                                                <div class="form-wizard">
                                                    <div class="form-body">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <label>
                                                                    Teléfono Principal : (*)</label>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <label>
                                                                    <small>Celular </small>
                                                                </label>
                                                                <div class="row">
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtCelularCodArea" CssClass="form-control " placeholder="cod Área"
                                                                            runat="server" MaxLength="6"></asp:TextBox>
                                                                        <Ajax:FilteredTextBoxExtender ID="txtCelularCodArea_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtCelularCodArea">
                                                                        </Ajax:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtCelular" CssClass="form-control " placeholder="número de celular"
                                                                            runat="server" MaxLength="9"></asp:TextBox>
                                                                        <span class="help-block">Ingrese Teléfono Celular</span>
                                                                        <Ajax:FilteredTextBoxExtender ID="txtCelular_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtCelular">
                                                                        </Ajax:FilteredTextBoxExtender>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <label>
                                                                    <small>Fijo</small></label>
                                                                <div class="row">
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="txtTelFijoCodArea" CssClass="form-control" placeholder="cod area"
                                                                            runat="server" MaxLength="5"></asp:TextBox>
                                                                        <Ajax:FilteredTextBoxExtender ID="txtTelFijoCodArea_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtTelFijoCodArea">
                                                                        </Ajax:FilteredTextBoxExtender>
                                                                    </div>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtTelFijo" CssClass="form-control " placeholder="número de teléfono"
                                                                            runat="server" MaxLength="9"></asp:TextBox>
                                                                        <span class="help-block">Ingrese Teléfono Fijo</span>
                                                                        <Ajax:FilteredTextBoxExtender ID="txtTelFijo_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtTelFijo">
                                                                        </Ajax:FilteredTextBoxExtender>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>
                                                                        E-Mail :(*)</label>
                                                                    <asp:TextBox ID="txtEmail_Establecimiento" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <span id="spanMensajeEmail" class="help-block" style="color: red;">El mail ingresado
												es inválido.</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        Página WEB :</label>
                                                                    <asp:TextBox ID="txtWebPage" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>
                                                                        Facebook :</label>
                                                                    <asp:TextBox ID="txtRedSocial" CssClass="form-control" runat="server"></asp:TextBox>
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
                            <div id="divPanel_4" runat="server" class="tab-pane">
                                <h4 class="form-section">Productos del Comercio</h4>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="alert alert-block alert-info fade in">
                                                <h4 class="alert-heading">
                                                    <i class="fa fa-info-circle"></i>Información de Ayuda</h4>
                                                <ul>
                                                    <li>1) Primero seleccione el producto y/o servicio de la lista y luego haga click en
														el botón "Agregar Producto a la Lista". De ésta manera quedan identificados los
														productos que trabaja su comercio. </li>
                                                    <li>2) Al terminar de agregar, confirme la lista seleccionando la opción "Confirmar
														listado de productos".</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server" ID="upInformacionGeneral">
                                    <ContentTemplate>
                                        <div class="row">
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-5">
                                                    <asp:DropDownList runat="server" ID="ddlProductos" CssClass="form-control select2me"
                                                        placeholder="Seleccione Producto..." />
                                                    <%--<asp:TextBox ID="txtBuscarProducto" runat="server" CssClass="form-control" AutoPostBack="False"
                                                        Width="100%" placeholder="Ingrese El Producto / Servicio de su comercio">
                                                    </asp:TextBox>
                                                    <Ajax:AutoCompleteExtender ServiceMethod="BuscarProducto" CompletionInterval="100"
                                                        EnableCaching="false" CompletionSetCount="10" TargetControlID="txtBuscarProducto"
                                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" MinimumPrefixLength="3"
                                                        OnClientItemSelected="ace1_itemSelected">
                                                    </Ajax:AutoCompleteExtender>
                                                    <asp:HiddenField ID="ace1Value" runat="server" />--%>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Button class="btn btnAgregarProductoALista  btn-circle" ID="btnAgregarProd"
                                                        Text="+ Agregar Producto a la Lista" runat="server" OnClick="btnAgregarProd_Click"></asp:Button>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:GridView ID="gvProducto" CssClass="table table-striped table-bordered table-advance table-hover"
                                                                runat="server" AllowPaging="True" DataKeyNames="IdProducto" AutoGenerateColumns="False"
                                                                OnRowCommand="gvProducto_OnRowCommand" OnPageIndexChanging="gvProducto_PageIndexChanging">
                                                                <Columns>
                                                                    <%--<asp:BoundField DataField="IdProducto" HeaderText="Nro" />--%>
                                                                    <asp:BoundField DataField="NProducto" HeaderText="Producto" />
                                                                    <asp:TemplateField HeaderText="Quitar">
                                                                        <ItemStyle CssClass="grilla-columna-accion" Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Button ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                CommandName="QuitarProducto" CssClass="botonEliminar"></asp:Button>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <h4 class="form-section">Actividad Primaria y Secundaria del Comercio</h4>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="alert alert-block alert-info fade in">
                                                    <label>
                                                        Las Actividades son cargadas según los productos seleccionados. Para habilitar la
														opción de carga debe confirmar la lista de productos cargados tildando la opción
														"Confirmar listado de productos"</label>
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:CheckBox runat="server" ID="chkConfirmarListaDeProducto" CssClass=" checkNegritaGrande checkAceptCond"
                                                                AutoPostBack="True" Text="Confirmar listado de productos" OnCheckedChanged="chkConfirmarListaDeProducto_OnCheckedChanged" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label>
                                                    Actividad Primeria (*) :</label>
                                                <asp:DropDownList runat="server" ID="ddlRubroPrimario" CssClass="form-control select2me">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label>
                                                    Actividad Secundaria</label>
                                                <asp:DropDownList runat="server" ID="ddlRubroSecundario" CssClass="form-control select2me">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upInformacionGeneral">
                                    <ProgressTemplate>
                                        <div id="Background">
                                        </div>
                                        <div id="Progress">
                                            <h6>
                                                <p style="text-align: center">
                                                    <b>Procesando Datos, Espere por favor...
														<br />
                                                    </b>
                                                </p>
                                            </h6>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <h4 class="form-section">Información Adicional</h4>
                                    
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>Fecha de Inicio de Actividad :(*)</label>
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnCalFecIniAct" runat="server" ImageUrl="~/img/calendario.png"/>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFechaIniAct" CssClass="form-control me-1" runat="server" TextMode="DateTime" MaxLength="10"></asp:TextBox>
                                                    <Ajax:CalendarExtender ID="CalendarExtender1" PopupButtonID="btnCalFecIniAct" runat="server"
                                                                           TargetControlID="txtFechaIniAct" Format="dd/MM/yyyy" PopupPosition="Topleft">
                                                    </Ajax:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Nro Habilitación Municipal :(*)</label>
                                            <asp:TextBox ID="txtNroHabMun" CssClass="form-control" runat="server" MaxLength="15"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                Nro. de D.G.R. :(*)
                                            </label>
                                            <asp:TextBox ID="txtNroDGR" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                SUPERFICIE EN M2 USADA PARA VENTA:(*)</label>
                                            <asp:TextBox ID="txtM2Venta" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                            <span class="help-block">Valor entero positivo distinto de cero. </span>
                                            <Ajax:FilteredTextBoxExtender ID="txtM2Venta_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtM2Venta">
                                            </Ajax:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                SUPERFICIE EN M2 USADA PARA ADMINISTRACION:</label>
                                            <asp:TextBox ID="txtM2Admin" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                            <span class="help-block">Valor entero positivo distinto de cero. </span>
                                            <Ajax:FilteredTextBoxExtender ID="txtM2Admin_FilterTextBox" runat="server" Enabled="True"
                                                FilterType="Numbers" TargetControlID="txtM2Admin">
                                            </Ajax:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                SUPERFICIE EN M2 USADA PARA DEPOSITO:</label>
                                            <asp:TextBox ID="txtM2Dep" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                            <span class="help-block">Valor entero positivo distinto de cero. </span>
                                            <Ajax:FilteredTextBoxExtender ID="txtM2Dep_FilteredTextBoxExtender1" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtM2Dep">
                                            </Ajax:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label id="lblInmueble" runat="server">
                                                INMUEBLE:(*)</label>
                                            <br />
                                            <asp:RadioButton ID="rbPropietario" Text="propietario" CssClass="CorregirFocoRadioButton"
                                                GroupName="Inmueble" runat="server" />
                                            <asp:RadioButton ID="rbInquilino" Text="Inquilino" CssClass="CorregirFocoRadioButton"
                                                GroupName="Inmueble" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="divMostrarAlquiler" style="display: none;">
                                    <%--<div class="col-md-4">
                                                        <div class="form-group">
                                                        <label>SUPERFICIE EN M2 ALQUILER: </label>
                                                        <br/>
                                                        <asp:TextBox ID="txtSupAlquiler"  CssClass="form-control" MaxLength="5" runat="server"></asp:TextBox>
                                                        <Ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                Enabled="True" FilterType="Numbers" TargetControlID="txtSupAlquiler">
                                                            </Ajax:FilteredTextBoxExtender>
                                                        </div>
                                                      </div>--%>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>
                                                RANGO DE ALQUILER:(*)</label>
                                            <br />
                                            <asp:RadioButton ID="rb5" Text="MENOS DE $ 500.000" CssClass="CorregirFocoRadioButton"
                                                GroupName="RangoAlquiler" runat="server" />
                                            <asp:RadioButton ID="rb510" Text="$ 500.000 a $ 1.000.000" CssClass="CorregirFocoRadioButton"
                                                GroupName="RangoAlquiler" runat="server" />
                                            <asp:RadioButton ID="rb1015" Text="$ 1.000.000 a $ 3.000.000" CssClass="CorregirFocoRadioButton"
                                                GroupName="RangoAlquiler" runat="server" />
                                            <asp:RadioButton ID="rb1520" Text="MAS DE $ 3.000.000" CssClass="CorregirFocoRadioButton"
                                                GroupName="RangoAlquiler" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>
                                            CANTIDAD PERSONAL TOTAL DEL ESTABLECIMIENTO (Promedio Anual) (*):</label>
                                        <asp:TextBox ID="txtCantPersTotal" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                        <Ajax:FilteredTextBoxExtender ID="txtCantPersTotal_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txtCantPersTotal">
                                        </Ajax:FilteredTextBoxExtender>
                                    </div>
                                    <div class="col-md-6">
                                        <label>
                                            CANTIDAD DE PERSONAL EN RELACION DE DEPENDENCIA (Promedio Anual):</label>
                                        <asp:TextBox ID="txtCantPersRelDep" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                        <Ajax:FilteredTextBoxExtender ID="txtCantPersRelDep_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txtCantPersRelDep">
                                        </Ajax:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                ¿POSEE COBERTURA MEDICA? :</label>
                                            <br />
                                            <asp:RadioButton ID="rbSiCobertura" Text="Si" GroupName="Cobertura" runat="server" />
                                            <asp:RadioButton ID="rbNoCobertura" Text="No" GroupName="Cobertura" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                ¿REALIZO CAPACITACION EL ULTIMO AÑO? :</label>
                                            <br />
                                            <asp:RadioButton ID="rbSiCap" Text="Si" GroupName="Capacita" runat="server" />
                                            <asp:RadioButton ID="rbNoCap" Text="No" GroupName="Capacita" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                ¿POSEE SEGURO PARA EL LOCAL? :</label>
                                            <br />
                                            <asp:RadioButton ID="rbSiSeg" Text="Si" GroupName="Seguro" runat="server" />
                                            <asp:RadioButton ID="rbNoSeg" Text="No" GroupName="Seguro" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                ORIGEN PROVEEDORES:</label>
                                            <br />
                                            <asp:CheckBox CssClass="checkbox" ID="chkprov" Text="Provincial" runat="server" />
                                            <asp:CheckBox CssClass="checkbox" ID="ChkNacional" Text="Nacional" runat="server" />
                                            <asp:CheckBox CssClass="checkbox" ID="ChkInter" Text="Internacional" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divPanel_5" runat="server" class="tab-pane">
                                <asp:UpdatePanel runat="server" ID="upRepresentanteLegal">
                                    <ContentTemplate>
                                        <h3 class="form-section">
                                            <i class="fa fa-user"></i>Persona que inició el trámite</h3>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label>
                                                        Ud. es : (*)</label>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTipoGestor" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DdlTipoGestor_OnSelectedIndexChanged">
                                                        <asp:ListItem Text="Titular y Representante Legal de la Empresa" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Gestor que realiza el Trámite" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Contador de la Empresa" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="help-block">
                                                        Persona a la cual puede solicitarse información
                                                    </div>
                                                </div>
                                                <div id="divMatriculaContador" runat="server" visible="False" class="col-md-6">
                                                    <label>
                                                        Nro. Matrícula Contador</label>
                                                    <asp:TextBox ID="txtNroMatricula" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <label>
                                                        Nombre y Apellido :(*)</label>
                                                    <asp:TextBox ID="txtNomApeConta" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>
                                                        Teléfono :(*)</label>
                                                    <asp:TextBox ID="txtTelConta" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>
                                                            Sexo :(*)</label>
                                                        <asp:TextBox ID="txtSexoConta" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-md-4">
                                                    <label>
                                                        DNI :</label>
                                                    <asp:TextBox ID="txtDniConta" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-8">
                                                    <label>
                                                        Email :</label>
                                                    <asp:TextBox ID="txtEmailConta" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <%--OBTENER EL REP LEGAL--%>
                                        <div id="divRepresentanteLegal" visible="False" runat="server">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h3 class="form-section">
                                                        <i class="fa fa-user"></i>Representante Legal del Comercio
                                                    </h3>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtCuilRepresentante" runat="server" CssClass="form-control required"
                                                        AutoCompleteType="None" MaxLength="11"></asp:TextBox>
                                                    <span class="help-block">Ingrese CUIL de la persona.</span>
                                                    <Ajax:FilteredTextBoxExtender ID="txtCuilRepresentante_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="txtCuilRepresentante">
                                                    </Ajax:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList runat="server" CssClass="form-control required" ID="ddlSexoRP">
                                                        <asp:ListItem Text="Seleccione Sexo" Value="00"></asp:ListItem>
                                                        <asp:ListItem Text="Masculino" Value="01"></asp:ListItem>
                                                        <asp:ListItem Text="Femenino" Value="02"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnBuscarRepresentante" class="btn btnBuscar  btn-circle" Text="Buscar"
                                                        runat="server" OnClick="btnBuscarRepresentante_Click"></asp:Button>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label>
                                                        Cargo que ocupa :(*)</label>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCargoOcupa">
                                                        <asp:ListItem Text="Titular" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Gerente" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Socio Presidente" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Directivo" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="Apoderado" Value="5"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label>
                                                        Apellido :(*)</label>
                                                    <asp:TextBox ID="txtApellidoRepresentante" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>
                                                        Nombre :(*)</label>
                                                    <asp:TextBox ID="txtNombreRepresentante" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>
                                                            Sexo
                                                        </label>
                                                        <asp:TextBox ID="txtSexoRepresentante" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div id="divMjeErrorRepLegal" runat="server" class="alert alert-danger alert-dismissable">
                                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                                        </button>
                                                        <strong>Error! </strong>
                                                        <asp:Label runat="server" ID="lblErrorRepLegal"></asp:Label>
                                                    </div>
                                                    <div id="divMjeExitoRepLegal" runat="server" class="alert alert-success alert-dismissable">
                                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                                        </button>
                                                        <strong>Exito! </strong>
                                                        <asp:Label runat="server" ID="lblExitoRepLegal"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div id="divRepresentanteLegal" visible="False" runat="server">
                                            <h3 class="form-section">
                                                <i class="fa fa-user"></i>Representante Legal del Comercio</h3>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtCuilRepresentante" runat="server" CssClass="form-control" placeholder="Ingrese CUIL para obtener sus datos"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Button ID="btnBuscarRepresentante" class="btn btnBuscar  btn-circle" Text="Buscar"
                                                            runat="server" OnClick="btnBuscarRepresentante_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upRepresentanteLegal">
                                    <ProgressTemplate>
                                        <div id="Background">
                                        </div>
                                        <div id="Progress">
                                            <h6>
                                                <p style="text-align: center">
                                                    <b>Procesando Datos, Espere por favor...
														<br />
                                                    </b>
                                                </p>
                                            </h6>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <h3 class="form-section">
                                    <i class="fa fa-check-square-o"></i>Ud. está finalizando el Trámite</h3>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="note note-danger">
                                            <h2 class="block">DECLARACION JURADA</h2>
                                            <p style="font-size: x-large;">
                                                Declaro BAJO JURAMENTO: Que la información consignada en el presente formulario
												es correcta, completa y confeccionada sin omitir ni falsear dato alguno; siendo
												fiel expresión de la verdad. (Art.3 Decreto Reglamentario N° 1016/2010 Ley Provincial
												N° 9693)
                                            </p>
                                            <div class="row">
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-9">
                                                    <asp:CheckBox runat="server" ID="chkAceptarTerminosYCondiciones" Style="background-color: TRANSPARENT; border-color: TRANSPARENT;"
                                                        Text="ACEPTAR DECLARACIÓN JURADA" class="form-control checkNegritaGrande checkAceptCond" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-actions" style="background-color: gainsboro">
                    <br />
                    <div class="row">
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-3">
                            <asp:Button runat="server" ID="btnAtras" CssClass="btn botonAtras btn-circle form-control"
                                Text="Atrás" OnClick="btnAtras_OnClick" />
                        </div>
                        <div class="col-md-3">
                            <asp:Button runat="server" ID="btnSiguiente" CssClass="btn botonSiguiente btn-circle  form-control"
                                Text="Siguiente" OnClick="btnSiguiente_OnClick"></asp:Button>
                            <asp:Button runat="server" ID="btnFinalizar" CssClass="btn botonFinalizar btn-circle form-control"
                                Text="Finalizar" OnClick="btnFinalizar_OnClick" />
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnSalir" CssClass="btn botonSalir btn-circle  form-control"
                                Text="Salir" OnClick="btnSalir_OnClick"></asp:Button>
                        </div>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
    <script src="Scripts/mapaSifcos.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ace1_itemSelected(sender, e) {
            $('#ContenedorPrincipal_ace1Value').val(e.get_value());
        }

        function capturarf5(e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 116) {
                return false;
            }
        }

        document.onkeydown = capturarf5;


        $(document).ready(function () {


            $('#ContenedorPrincipal_rbPropietario').click(function () {
                if ($(this).is(':checked')) {
                    $("#divMostrarAlquiler").hide();
                }
            });
            $("#ContenedorPrincipal_rbInquilino").click(function () {
                if ($(this).is(':checked')) {
                    $("#divMostrarAlquiler").show();

                    //                    $("#ContenedorPrincipal_txtSupAlquiler").val('');
                    //                    $("#ContenedorPrincipal_rb1015").checked= false;
                    //                    $("#ContenedorPrincipal_rb1520").val('');
                    //                    $("#ContenedorPrincipal_rb5").val('');
                    //                    $("#ContenedorPrincipal_rb510").val('');

                }
            });


            $("#ContenedorPrincipal_txtLatitud").keydown(function (e) {
                return false;
            });

            $("#ContenedorPrincipal_txtLongitud").keydown(function (e) {
                return false;
            });


            $("#_txtDireccion").focus();

            function ejecutarTipoGestior() {

                //debo repetir el codigo para que cuando me traiga la pagina el servidor vuelva a ocultar las secciones ocultas que cuando se envió según los seleccionado en el DdltipoGestor.
                var val1 = $('#ContenedorPrincipal_ddlTipoGestor option:selected').val();
                if (val1 == "3") {
                    $("#divMatriculaContador").show();
                } else {
                    $("#divMatriculaContador").hide();
                }
                if (val1 == "1") {
                    $("#divRepresentanteLegal").hide();
                } else {
                    $("#divRepresentanteLegal").show();
                }

            }


            $("#ContenedorPrincipal_ddlTipoGestor").change(function () {
                ejecutarTipoGestior();
            });




            $("#spanMensajeEmail").hide();
            $("#spanFechaInicioAct").hide();


            $("#ContenedorPrincipal_txtFechaIniAct").focusout(function () {
                //string estará en formato dd/mm/yyyy (dí­as < 32 y meses < 13)
                var expReg = new ExpReg(/^([0][1-9]|[12][0-9]|3[01])(\/|-)([0][1-9]|[1][0-2])\2(\d{4})$/);
                var rta = (expReg.test(string));


                if (rta == false) {
                    $("#spanFechaInicioAct").show();
                }
                if (rta == true) {
                    $("#spanFechaInicioAct").hide();
                }
            });



            $("#ContenedorPrincipal_txtEmail_Establecimiento").focusout(function () {
                var emailAddress = $("#ContenedorPrincipal_txtEmail_Establecimiento").val();
                var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
                var rta = pattern.test(emailAddress);
                if (rta == false) {
                    $("#spanMensajeEmail").show();
                }
                if (rta == true) {
                    $("#spanMensajeEmail").hide();
                }
            });
            $('#_txtDireccion').live("keydown", function (e) {
                if (e.keyCode == 13) {
                    AutocompletarYubicarMarker();
                    return false; // prevent the button click from happening
                }
            });


            //inicializarMapa();

            //$(".select2").select2();

        });


    </script>
</asp:Content>


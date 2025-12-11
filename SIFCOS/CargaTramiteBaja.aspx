<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
	CodeBehind="CargaTramiteBaja.aspx.cs" Inherits="SIFCOS.CargaTramiteBaja" UICulture="es"
	Culture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="cc1" Namespace="BotonUnClick" Assembly="BotonUnClick" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
	<%--<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyCuxWCu971i-L-O1ui-jzI72cX1Tjr1kwU&v=3.exp&sensor=false&libraries=places"></script>--%>
	<%--<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyCuxWCu971i-L-O1ui-jzI72cX1Tjr1kwU&v=3.exp&sensor=false&libraries=places"></script>--%>
	<h3 class="page-title">
		Trámite SIFCoS
	</h3>
	<div class="page-bar">
		<ul class="page-breadcrumb">
			<li><i class="fa fa-home"></i><a href="#">Inicio</a> <i class="fa fa-angle-right"></i>
			</li>
			<li><a href="BajaDeComercio.aspx">Baja de Comercio</a> <i class="fa fa-angle-right">
			</i></li>
			<li><a href="CargaTramiteBaja.aspx">Carga Trámite de Baja</a> <i class="fa fa-angle-right">
			</i></li>
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
					<h4>
						¿Está seguro que desea salir del trámite en curso?
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
                <asp:Button runat="server" ID="btnSalir"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnSalir_OnClick"/>
         
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
						<h4>
							EN ESTOS MOMENTOS ESTAMOS REALIZANDO MANTENIMIENTO DEL SISTEMA...
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
											<i class="fa fa-gift"></i>Datos ingresados
										</div>
									</div>
									<div class="portlet-body">
										<div class="scroller" style="height: 300px">
											<form role="form">
											<h3 class="form-section">
												Confirmación de Baja de Trámite</h3>
											<div class="row">
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

              
                        <asp:Button runat="server" ID="btnConfirmarBaja"  class="btn btnBuscar  btn-circle"   style="margin-bottom: 10px;margin-top: 10px;" Text="Confirmar" OnClick="btnConfirmarBaja_OnClick"/>
                        

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
						<h4>
							Información sobre el Trámite que está iniciando...</h4>
					</div>
					<div class="panel-body">
						<%--Panel content--%>
						<div id="divMensaejeErrorModal" runat="server" class="alert alert-danger alert-dismissable">
							<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
							</button>
							<strong>Importante! </strong>
							<asp:Label runat="server" ID="lblMensajeErrorModal"></asp:Label>
						</div>
						<h4 class="form-section" style="font-weight: bold;">
							Datos del Comercio</h4>
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
								<asp:TextBox ID="txtModalNombreFantasia" Enabled="False" runat="server" CssClass="form-control"></asp:TextBox>
							</div>
						</div>
						<br />
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
									<a class="more" href="#">Iniciar Inscripción <i class="m-icon-swapright m-icon-white">
									</i></a>
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
									<a class="more" href="#">Iniciar Reempadronamiento <i class="m-icon-swapright m-icon-white">
									</i></a>
								</div>
							</div>
						</div>
						<div id="divSeccionInscripcionTramite" runat="server">
							<div class="row">
								<div class="col-md-12">
									<div class="alert alert-block alert-info fade in">
										<label>
											Sr Comerciante, está por iniciar la Inscripción a SIFCoS. Si los datos son correctos
											seleccione "Aceptar y Contituar".</label>
									</div>
								</div>
							</div>
						</div>
						<div class="row">
							<div id="divSeccionReempadronamiento" runat="server" class="col-md-8">
								<h4 class="form-section" style="font-weight: bold;">
									Reempadronamiento SIFCoS</h4>
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
								<h4 class="form-section" style="font-weight: bold;">
									Nueva Sucursal</h4>
								<br />
								<asp:CheckBox runat="server" Text="Dar de Alta una Nueva Sucursal" ToolTip="Haz click sólo si desea cargar una nueva sucursal si tu sede
                                no aparece" AutoPostBack="True" CssClass=" w3-border-white pull-right" ID="chkNuevaSucursal"
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
						<h3 class="form-section">
							Datos Usuario CIDI logueado</h3>
						<div class="row">
							<div class="col-md-4">
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
							</div>
							<div class="col-md-4">
								<div class="form-group">
									<asp:Label ID="lblCuilCidi" runat="server" Text="CUIL" Enabled="True"></asp:Label>
									<asp:TextBox runat="server" ID="txtCuilCidi" CssClass="form-control disabled" Enabled="False"
										MaxLength="11"></asp:TextBox>
								</div>
							</div>
						</div>
						<h3 class="form-section">
							Inicie el trámite</h3>
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label>
										INGRESE EL CUIT (*):
									</label>
									<asp:TextBox runat="server" ID="txtCuit" CssClass="form-control" placeholder="CUIT de la Empresa/Titular"
										MaxLength="11" />
									<Ajax:FilteredTextBoxExtender ID="txtCuit_FilteredTextBoxExtender" runat="server"
										Enabled="True" FilterType="Numbers" TargetControlID="txtCuit">
									</Ajax:FilteredTextBoxExtender>
								</div>
							</div>
						</div>
					</div>
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
							<div class="row">
								<div class="form-group">
									<div class="col-md-12">
										<div class="alert alert-block alert-info fade in">
											<h4 class="alert-heading">
												<i class="fa fa-info-circle"></i>Información</h4>
											<p>
												Todos los campos marcados con (*) son obligatorios.
											</p>
										</div>
									</div>
								</div>
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
							<div id="divPanel_1" runat="server" class="tab-pane">
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
													<Ajx:FilteredTextBoxExtender ID="txtCuilRepresentante_FilteredTextBoxExtender" runat="server"
														Enabled="True" FilterType="Numbers" TargetControlID="txtCuilRepresentante">
													</Ajx:FilteredTextBoxExtender>
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
									<i class="fa fa-check-square-o"></i>Documentación</h3>
								

								<div id="divCDD" runat="server" class="row">
									<div id="div1" runat="server" class="col-md-12">
										<%--AGREGAR DOCUMENTACION ADJUNTA--%>
										<div runat="server" id="DivAdjuntar" visible="True">
											<div class="row">
												<div class="col-md-12">
													<div class="alert alert-block alert-info fade in">
														<h4 class="alert-heading">
															<i class="fa fa-info-circle"></i>INFORMACION A TENER EN CUENTA</h4>
														<ul>
															<li>ESCANEE PREVIAMENTE EL CESE MUNICIPAL Y DENUNCIA POLICIAL DE EXTRAVIO EN FORMATO
																PDF Y ADJUNTELA.<br />
																SI NO TIENE LA DENUNCIA POLICIAL DE EXTRAVIO SUBA LA FOTO DE LA OBLEA
																<br />
																ESTOS DOCUMENTOS SON OBLIGATORIO PARA FINALIZAR EL TRAMITE DE BAJA<br />
																PARA CONTROL DE LA INFORMACION REGISTRADA. </li>
														</ul>
													</div>
												</div>
											</div>
											<%--ADJUNTO 1--%>
											<div class="row">
												<div class="col-md-12">
													<div class="form-section">
														
														<h5>
															Adjuntar <strong>Cese municipal</strong></h5>
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
											<div class="row">
												<div class="col-md-4">
													<asp:Button runat="server" ID="btnAdjuntar" Text="+ Adjuntar Documento" placeholder="Adjuntar Documentación"
														OnClick="BtnAdjuntar1_Click" CssClass="form-control btn blue"></asp:Button>
												</div>
												<div class="col-md-6">
													<div id="divMensajeErrorDocumentacion_1" runat="server" class="alert alert-danger"
														style="margin-bottom: 0px;" visible="False">
														<%--<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
											</button>--%>
														<div class="icon">
															<i class="fa fa-times-circle"></i><strong>Error! </strong>
															<asp:Label runat="server" ID="lblMensajeErrorDocumentacion_1"></asp:Label>
														</div>
													</div>
													<div id="divMensajeExitoDocumentacion_1" runat="server" class="alert alert-success"
														style="margin-bottom: 0px;" visible="False">
														<%--<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
											</button>--%>
														<div class="icon">
															<i class="fa fa-check"></i><strong>Exito! </strong>
															<asp:Label runat="server" ID="lblMensajeExitoDocumentacion_1"></asp:Label>
														</div>
													</div>
												</div>
											</div>
											<%--otro del adjunto 1--%>
											<div class="row">
												<div class="col-md-4">
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
														<h5>
															Adjuntar <strong>Denuncia policial de extravío o foto de oblea</strong></h5>
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
												<div class="col-md-4">
													<asp:Button runat="server" ID="btnAdjuntar2" Text="+ Adjuntar Documento" placeholder="Adjuntar Documentación"
														OnClick="BtnAdjuntar2_Click" CssClass="form-control btn blue"></asp:Button>
												</div>
												<div class="col-md-6">
													<div id="divMensajeErrorDocumentacion_2" runat="server" class="alert alert-danger"
														visible="False">
														<div class="icon">
															<i class="fa fa-times-circle"></i><strong>Error! </strong>
															<asp:Label runat="server" ID="lblMensajeErrorDocumentacion_2"></asp:Label>
														</div>
													</div>
													<div id="divMensajeExitoDocumentacion_2" runat="server" class="alert alert-success"
														visible="False">
														<%--<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
												</button>--%>
														<div class="icon">
															<i class="fa fa-check"></i><strong>Exito! </strong>
															<asp:Label runat="server" ID="lblMensajeExitoDocumentacion_2"></asp:Label>
														</div>
													</div>
												</div>
											</div>
											<%--otro del adjunto 2--%>
											<div class="row">
												<div class="col-md-4">
													<asp:Button runat="server" ID="btnAdjuntarOtro2" Text="+ Cambiar Documento" OnClick="BtnAdjuntarOtro2_Click"
														CssClass="form-control btn default" Visible="False"></asp:Button>
												</div>
											</div>
										</div>
									</div>
								</div>
								<h3 class="form-section">
									<i class="fa fa-check-square-o"></i>Ud. está finalizando el Trámite</h3>
								<div class="row">
									<div class="col-md-12">
										<div class="note note-danger">
											<h2 class="block">
												DECLARACION JURADA</h2>
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
													<asp:CheckBox runat="server" ID="chkAceptarTerminosYCondiciones" Style="background-color: TRANSPARENT;
														border-color: TRANSPARENT;" Text="ACEPTAR DECLARACIÓN JURADA" class="form-control checkNegritaGrande checkAceptCond" />
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
						<div class="col-md-4">
						</div>
						<div class="col-md-3">
							<asp:Button runat="server" ID="btnFinalizar" CssClass="btn botonFinalizar btn-circle form-control"
								Text="Finalizar" OnClick="btnFinalizar_OnClick" />
							<div class="col-md-3">
							</div>
							<%--<div class="col-md-3">
                                <asp:Button runat="server" ID="btnSalir" CssClass="btn botonSalir btn-circle  form-control"
                                    Text="Salir" OnClick="btnSalir_OnClick"></asp:Button></div>--%>
						</div>
						<br />
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
	<%--<script src="Scripts/mapaSifcos.js" type="text/javascript"></script>--%>
	<script type="text/javascript">

		function ace1_itemSelected(sender, e) {
			$('#ConenedorPrincipal_ace1Value').val(e.get_value());
		}

		function capturarf5(e) {
			var code = (e.keyCode ? e.keyCode : e.which);
			if (code == 116) {
				return false;
			}
		}

		document.onkeydown = capturarf5;


		$(document).ready(function () {


			$('#ConenedorPrincipal_rbPropietario').click(function () {
				if ($(this).is(':checked')) {
					$("#divMostrarAlquiler").hide();
				}
			});
			$("#ConenedorPrincipal_rbInquilino").click(function () {
				if ($(this).is(':checked')) {
					$("#divMostrarAlquiler").show();

					//                    $("#ConenedorPrincipal_txtSupAlquiler").val('');
					//                    $("#ConenedorPrincipal_rb1015").checked= false;
					//                    $("#ConenedorPrincipal_rb1520").val('');
					//                    $("#ConenedorPrincipal_rb5").val('');
					//                    $("#ConenedorPrincipal_rb510").val('');

				}
			});


			//            $("#ConenedorPrincipal_txtLatitud").keydown(function (e) {
			//                return false;
			//            });

			//            $("#ConenedorPrincipal_txtLongitud").keydown(function (e) {
			//                return false;
			//            });


			$("#_txtDireccion").focus();

			function ejecutarTipoGestior() {

				//debo repetir el codigo para que cuando me traiga la pagina el servidor vuelva a ocultar las secciones ocultas que cuando se envió según los seleccionado en el DdltipoGestor.
				var val1 = $('#ConenedorPrincipal_ddlTipoGestor option:selected').val();
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


			$("#ConenedorPrincipal_ddlTipoGestor").change(function () {
				ejecutarTipoGestior();
			});




			$("#spanMensajeEmail").hide();
			$("#spanFechaInicioAct").hide();


			$("#ConenedorPrincipal_txtFechaIniAct").focusout(function () {
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



			$("#ConenedorPrincipal_txtEmail_Establecimiento").focusout(function () {
				var emailAddress = $("#ConenedorPrincipal_txtEmail_Establecimiento").val();
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

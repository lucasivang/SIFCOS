<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Solicitud.aspx.cs" Inherits="SIFCOS.Solicitud" %>
<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
	<h3 class="page-title uppercase" style="font-size: 29px; font-weight: bold; color: #519CCB;">
		<img src="Img/inicio.png" style="width: 58px;" />
		Solicitud de Inscripción/Reempadronamiento SIFCOS
	</h3>
	<div class="page-bar">
		<ul class="page-breadcrumb">
			<li><i class="fa fa-home"></i><a href="Solicitud.aspx" class="font-blue-steel font-lg bold uppercase">
				Inicio</a> <i class="fa fa-angle-right"></i></li>
			<li><a href="Solicitud.aspx" class="font-blue-steel font-lg bold uppercase">Solicitud
				de Adhesión al Compre Córdoba</a> <i class="fa fa-angle-right"></i></li>
		</ul>
	</div>
</asp:Content>
<%-- / HEADER DE LA PAGINA --%>

<%-- BODY DE LA PAGINA --%>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
	
	<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
	                   EnableScriptLocalization="True">
	</asp:ScriptManager>
	
	<%-- DIV INSTRUCTIVO --%>
	<div id="divInstructivo" runat="server">
		<div class="row">
			<div class="col-md-12">
				<h3>
					¿CÓMO ME INSCRIBO O REEMPADRONO EN SIFCOS?
				</h3>
			</div>
		</div>
		<div class="row">
			<div class="form-wizard">
				<div class="form-body ">
					<ul class="nav nav-pills nav-justified steps">
						<li class="active"><a href="#tab1" data-toggle="tab" class="step active"><span class="number">
							1 </span><span class="desc"><i class="fa fa-check"></i>Primer Paso </span></a>
						</li>
						<li class=""><a href="#tab2" data-toggle="tab" class="step"><span class="number">2 </span>
							<span class="desc"><i class="fa fa-check"></i>Segundo Paso</span> </a></li>
					</ul>
					<div class="tab-content text">
						<div class="tab-pane active" id="tab1">
							<h4 class="block">
								<div class="panel-group accordion" id="accordion3">
									<div class="panel panel-info">
										<div class="panel-heading ">
											<h4 class="panel-title">
												<a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse"
													data-parent="#accordion3" href="#collapse_3_1"><i class="fa fa-check"></i><strong>OBTENER
														CIUDADANO DIGITAL - NIVEL 2</strong></a>
											</h4>
										</div>
										<div id="collapse_3_1" class="panel-collapse collapse" style="height: 0px;">
											<div class="panel-body alert-success">
												<div class="row">
													<div class="col-md-12">
														<h4>
															Para iniciar el trámite debes registrarte como Ciudadano Digital. Si todavía no
															estás registrado, ingresa a <a href="https://cidi.cba.gov.ar" target="_blank">https://cidi.cba.gov.ar</a></h4>
													</div>
												</div>
												<div class="row">
													<div class="col-md-12">
														<p class="caption-subject font-blue-sharp bold uppercase tituloHomeContent">
															<strong>¿PARA QUÉ ME SIRVE EL NIVEL 2?</strong></p>
														<h4>
															Este nivel permite una mayor personalización de la Plataforma, accediendo a una
															mayor cantidad de servicios y de información online que requieran esta categoría
															de seguridad.</h4>
													</div>
												</div>
												<div class="row">
													<div class="col-md-12">
														<p class="caption-subject font-blue-sharp bold uppercase tituloHomeContent">
															<strong>¿CÓMO ACCEDO AL NIVEL 2?</strong></p>
														<h4>
															Debes acercarte a un Centro de Constatación de Identidad (CCI) con tu DNI y constancia
															de CUIL, donde verificarán tus datos e identidad, registrarán tu fotografía y anexarán
															una copia digital del documento de identidad.
														</h4>
													</div>
												</div>
											</div>
										</div>
									</div>
									
									<div class="panel panel-info">
										<div class="panel-heading">
											<h4 class="panel-title">
												<a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse"
													data-parent="#accordion3" href="#collapse_3_3"><i class="fa fa-check"></i><strong>SER
														REPRESENTANTE LEGAL REGISTRADO DE LA EMPRESA</strong> </a>
											</h4>
										</div>
										<div id="collapse_3_3" class="panel-collapse collapse" style="height: 0px;">
											<div class="panel-body alert-success">
												<h4>
													Consiste en designar a un Ciudadano Digital como representante legal para que opere
													a tu nombre en relación a uno o varios servicios.
												</h4>
												<h4>
													Habilita a los representantes legales de personas jurídicas a nombrar personas físicas
													para que utilicen a nombre de éstas uno o más servicios.</h4>
												<p>
													<a class="btn btn-circle green" href="pdfs/Tutorial_SIFCoS.pdf"
													   target="_blank">Administrador de Relaciones</a>
													   
													 

												</p>
											</div>
										</div>
									</div>
								</div>
								<br>
								<br>
							</h4>
						</div>
						<div class="tab-pane" id="tab2">
							<div class="panel-group accordion" id="accordion4">
								<div class="panel panel-info">
									<div class="panel-heading ">
										<h4 class="panel-title">
											<a class="accordion-toggle accordion-toggle-styled collapsed" data-toggle="collapse"
												data-parent="#accordion4" href="#collapse_4_1"><i class="fa fa-check"></i><strong>INGRESÁ
													LOS DATOS NECESARIOS PARA REALIZAR TU SOLICITUD</strong></a>
										</h4>
									</div>
									<div id="collapse_4_1" class="panel-collapse collapse" style="height: 0px;">
										<div class="panel-body alert-info">
											<div class="row alert-success">
												<div class="col-md-12">
													<h4>
														- Hace click en el botón <strong>Inscripción / Reempadronamiento</strong> que figura en
														este sitio.<br>
														- Ingresa el número de CUIT de la razón social a inscribir o reempadronar.<br>
														- Hace click en <strong>Iniciar trámite.</strong><br>
													</h4>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
					<br>
				</div>
			</div>
		</div>
		<div class="row">
			<div class="col-md-3">
				<asp:Button runat="server" OnClick="IniciarSolicitud" ID="btnIniciarSolicitud" CssClass="btn btn-circle blue form-control"
					Text="Inscripción / Reempadronamiento" />
			</div>
		</div>
	</div>
	<%-- DIV ISNTRUCTIVO  --%>
	<%-- PRIMERA PARTE DEL FORMULARIO  --%>
	<div id="PrimerDiv" class="portlet box blue-steel" runat="server">
		<div class="portlet-title">
			<div class="caption">
				<i class="fa fa-home"></i>REGISTRO ESPECIAL DE BENEFICIARIOS
			</div>
		</div>
		<div class="portlet-body form">
			<div class="form-body">
				<h3 class="form-section">
					<span class="font-blue-steel font-lg bold uppercase">Hola
						<%=UsuarioLogueado.NombreFormateado %>
					</span>
				</h3>
				<div class="row">
					<div class="col-md-4">
						<div class="form-group">
							<label class="font-blue-steel font-lg bold uppercase">
								INGRESE EL CUIT (*):
							</label>
							<asp:TextBox runat="server" ID="txtCuit" autocomplete="off" CssClass="form-control input-circle"
								placeholder="CUIT de la Empresa/Titular" onfocus="Vef()" MaxLength="11" minlength="11"
								required oninvalid="this.setCustomValidity('Debe ingresar un CUIT de 11 caracteres')"
								oninput="this.setCustomValidity('')" />
							<Ajax:FilteredTextBoxExtender ID="txtCuit_FilteredTextBoxExtender" runat="server"
								Enabled="True" FilterType="Numbers" TargetControlID="txtCuit">
							</Ajax:FilteredTextBoxExtender>
						</div>
					</div>
					<div class="col-md-4">
						<div class="form-group">
							<label class="font-blue-ebonyclay font-lg bold uppercase">
								<u>RECUERDE QUE UD. DEBE CUMPLIR CON LOS SIGUIENTES REQUISITOS: </u>
							</label>
							<br />
							<div class="font-blue-ebonyclay font-lg bold uppercase">
								<ul>
									<li>SER REPRESENTANTE LEGAL DE LA EMPRESA</li>
									<li>SER USUARIO CIDI NIVEL 2</li>
								</ul>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="form-actions" style="background-color: gainsboro">
				<div class="row">
					<div class="col-md-3">
						<asp:Button runat="server" OnClick="IniciarTramite" ID="btnIniciarTramite" CssClass="btn btn-circle blue-steel form-control"
							Text="Iniciar Trámite" />
					</div>
				</div>
			</div>
		</div>
	</div>
	<%-- FIN PRIMERA PARTE DEL FORMULARIO  --%>
	<div id="divAprobado" class="alert alert-success" runat="server">
		<label class="font-blue-ebonyclay font-lg bold uppercase">
			<u>UD. CUMPLE CON LOS SIGUIENTES REQUISITOS:</u>
		</label>
		<br />
		<div class="font-blue-ebonyclay font-lg bold uppercase">
			<ul>
				<li>SER REPRESENTANTE LEGAL DE LA EMPRESA</li>
				<li>SER USUARIO CIDI NIVEL 2</li>
				<li>DEBE ESTAR ACTIVO EN ROPyCE</li>
			</ul>
		</div>
	</div>
	<div id="divInstrucciones1" class="alert alert-success" runat="server">
		<label class="font-blue-ebonyclay font-lg bold">
			Por favor, para continuar complete los datos referidos a la direccion de su Establecimiento.
			En el caso de que ya estén registrados, verificar que estén correctos.
		</label>
		<br />
	</div>
	<br />
	<br />
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
	<%-- MENSAJE ERROR  --%>
	<div id="error" runat="server" class="note note-danger">
		<h4 class="block font-red-thunderbird font-lg bold uppercase" runat="server" id="errorLabel">
			Error</h4>
		<br />
		<a href="Solicitud.aspx" class="block font-red-thunderbird font-lg bold uppercase"><b>
			<< Volver</b></a>
		<br />
	</div>
	<br />
	<%-- FIN MENSAJE ERROR  --%>
	<%-- ERROR NIV 2 CIDI  --%>
	<div id="divNivel2" class="portlet box blue-steel" runat="server">
		<div class="portlet-title">
			<div class="caption">
				<i class="fa fa-home"></i>Usted no cumple con los requisitos necesarios para adherir
				al Compre Córdoba
				<br />
			</div>
		</div>
		<div class="portlet-body form">
			<div class="form-body">
				<h3 class="form-section">
					Para poder realizar el trámite de adhesión al Compre Córdoba, usted debe cumplir
					los siguientes requisitos:</h3>
				<label>
					<strong>Poseer Ciudadano Digital (CiDi) Nivel 2 Verificado: </strong>
					<br />
					<br />
					A través de la verificación de identidad de cada ciudadano, el nivel 2 permite acceder
					a más cantidad de servicios y de información online que requieran esta categoría
					de seguridad.<br />
					El ciudadano interesado, deberá presentarse en un Centro de Constatación de Identidad
					(CCI) con su DNI y constancia de CUIL, donde se verificarán sus datos e identidad,
					se registrará su fotografía y se anexará una copia digital de su documento de identidad.
					<br />
					<br />
					Los Centros de Constatación de Identidad (CCI) son oficinas de Gobierno con puestos
					especiales para realizar ésta atención.
					<br />
					<a href="http://www.cba.gov.ar/centros-de-atencion-al-ciudadano/" target="_blank"><b>
						Mire el listado de CCI a los que puede concurrir aquí.</b></a>
					<br />
					<br />
					Una vez obtenido el Nivel 2, debe comprobar que usted es el Representante Legal
					para efectuar la gestión.
				</label>
			</div>
			<div class="form-actions" style="background-color: gainsboro">
				<div class="row">
					<div class="col-md-3">
					</div>
				</div>
			</div>
		</div>
	</div>
	<%-- ERROR NIV 2 CIDI  --%>
	
	
</asp:Content>

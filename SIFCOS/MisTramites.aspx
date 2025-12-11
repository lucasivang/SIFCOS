<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
	CodeBehind="MisTramites.aspx.cs" Inherits="SIFCOS.MisTramites" UICulture="es"
	Culture="es-MX" %>
<%@ Register TagPrefix="ajx" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
	<h3 class="page-title">
		Mis Trámites
	</h3>
	<div class="page-bar">
		<ul class="page-breadcrumb">
			<li><i class="fa fa-home"></i><a href="#">Inicio</a> <i class="fa fa-angle-right"></i>
			</li>
			<li><a href="MisTramites.aspx">Mis Trámites</a> <i class="fa fa-angle-right"></i>
			</li>
		</ul>
	</div>
	<div id="divMensajeFinalizacionTramiteSifcosExito" visible="False" runat="server"
		class="alert alert-success alert-dismissable">
		<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
		</button>
		<h2 style="font-weight: bold;">
			Finalizó el trámite con éxito!</h2>
		<h3>
			<img src="Img/numero_uno.png" />
			Haga click en "Descargar Comprobante".</h3>
		<asp:Button runat="server" ID="btnDescargarComprobante" Text="Descargar Comprobante"
			CssClass="btn btn-default" OnClick="btnDescargarComprobante_OnClick" />
		<div id="divBotonDescargarTRS" runat="server">
			<br />
			<h3>
				<img src="Img/numero_dos.png" />
				Descargue también la tasa generada, la cual debe presentar pagada en la Boca de
				Recepción cercana haciendo click en "Desargar TRS Generada"
				<br />
				Recuerde que de abonada la Tasa Retributiva, dicho importe se acreditará a las 72hs
				hábiles de realizada.</h3>
			<asp:Button runat="server" ID="btnDescargarTasa" Text="Descargar TRS Generada" CssClass="btn btn-default"
				OnClick="btnDescargarTasa_OnClick" />
		</div>
		<br />
		<h3>
			Lo encontrará en la parte inferior de su navegador (ver imagen) .</h3>
		<h3>
		</h3>
		<img src="Img/pantalla_comprobante.png" style="width: 40%;" />
		<h3>
			<img src="Img/numero_tres.png" />Para consultar sus trámites realizados ingrese
			el CUIT y haga click en CONSULTAR.</h3>
	</div>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
	<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="True"
		EnableScriptLocalization="true">
	</asp:ScriptManager>
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
	
	<%-- MODAL ADJUNTAR DOCUMENTACION --%>
	<div id="divModalAdjuntarDocumentacionHab" runat="server" class="w3-modal">
		<div class="w3-modal-content w3-animate-top w3-card-8">
				<header class="w3-container w3-teal">
				<ul style="list-style-type: none; margin: 0; padding: 0; overflow: hidden;">
					<li style="text-align: center;color: white">
						<h3>ADJUNTAR DOCUMENTACION</h3>
					</li>

				</ul>
			</header>
			<div class="w3-container">
				<div class="row">
					<div class="col-md-12">
						<div class="alert alert-block alert-info fade in">
							<h4 class="alert-heading">
								<i class="fa fa-info-circle"></i>INFORMACION A TENER EN CUENTA</h4>
							<ul>
								<li>ESCANEE PREVIAMENTE LA HABILITACION MUNICIPAL EN FORMATO PDF Y ADJUNTELA.LUEGO, PRESIONE GRABAR<br />
							</ul>
						</div>
					</div>
				</div>
				<%--exito y error de adjuntos--%>
				<div class="row">
					<div class="col-md-12">
						<div id="divMensajeErrorDocumentacionHab" runat="server" class="alert alert-danger alert-dismissable"
						     visible="False">
							<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
							</button>
							<strong>Error! </strong>
							<asp:Label runat="server" ID="lblMensajeErrorDocumentacionHab"></asp:Label>
						</div>
						<div id="divMensajeExitoDocumentacionHab" runat="server" class="alert alert-success alert-dismissable"
						     visible="False">
							<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
							</button>
							<strong>Exito! </strong>
							<asp:Label runat="server" ID="lblMensajeExitoDocumentacionHab"></asp:Label>
						</div>
					</div>
				</div>
				<%--ADJUNTO 1--%>
				<div class="row">
					<div class="col-md-12">
						<h5>
							Adjuntar <strong>Habilitación municipal</strong></h5>
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
				</div>
				<%--otro del adjunto 1--%>
				<div class="row">
					<div class="col-md-4">
						<asp:Button runat="server" ID="btnAdjuntarOtro1" Text="+ Cambiar Documento" OnClick="BtnAdjuntarOtro1_Click"
						            CssClass="form-control btn default"></asp:Button>
					</div>
				</div>
			</div>
			<footer class="w3-container w3-teal" style="text-align: center">
				<asp:Button runat="server" ID="btnCerrarVerDocHab" class="btn btnBuscar  btn-circle" Style="margin-bottom: 10px; margin-top: 10px;" Text="Cerrar" OnClick="btnCerrarVerDocHab_OnClick" />
				<asp:Button runat="server" ID="BtnActualizarAdjunto1"  class="btn btnBuscar  btn-circle" Style="margin-bottom: 10px; margin-top: 10px;" Text="Guardar" OnClick="btnActualizarAdjunto1Modal_OnClick" />
			</footer>
		</div>
	</div>
	

	<%-- MODAL ADJUNTAR DOCUMENTACION --%>
	<div id="divModalAdjuntarDocumentacionAFIP" runat="server" class="w3-modal">
	<div class="w3-modal-content w3-animate-top w3-card-8">
		<header class="w3-container w3-teal">
			<ul style="list-style-type: none; margin: 0; padding: 0; overflow: hidden;">
				<li style="text-align: center;color: white">
					<h3>ADJUNTAR CONSTANCIA DE AFIP</h3>
				</li>

			</ul>
		</header>
		<div class="w3-container">
			<div class="row">
				<div class="col-md-12">
					<div class="alert alert-block alert-info fade in">
						<h4 class="alert-heading">
							<i class="fa fa-info-circle"></i>INFORMACION A TENER EN CUENTA</h4>
						<ul>
							<li>ESCANEE PREVIAMENTE LA CONSTANCIA DE AFIP EN FORMATO PDF Y ADJUNTELA. LUEGO, PRESIONE GUARDAR<br />
						</ul>
					</div>
				</div>
			</div>
			<%--exito y error de adjuntos--%>
			<div class="row">
				<div class="col-md-12">
					<div id="divMensajeErrorDocumentacionAFIP" runat="server" class="alert alert-danger alert-dismissable"
					     visible="False">
						<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
						</button>
						<strong>Error! </strong>
						<asp:Label runat="server" ID="lblMensajeErrorDocumentacionAFIP"></asp:Label>
					</div>
					<div id="divMensajeExitoDocumentacionAFIP" runat="server" class="alert alert-success alert-dismissable"
					     visible="False">
						<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
						</button>
						<strong>Exito! </strong>
						<asp:Label runat="server" ID="lblMensajeExitoDocumentacionAFIP"></asp:Label>
					</div>
				</div>
			</div>
			<%--ADJUNTO 2--%>
			<div class="row">
				<div class="col-md-12">
					<h5>
						Adjuntar <strong>Constancia de Inscripción de AFIP</strong></h5>
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
			</div>
			
			<%--otro del adjunto 2--%>
			<div class="row">
				<div class="col-md-4">
					<asp:Button runat="server" ID="btnAdjuntarOtro2" Text="+ Cambiar Documento" OnClick="BtnAdjuntarOtro2_Click"
					            CssClass="form-control btn default"></asp:Button>
				</div>
			</div>
		</div>
		<footer class="w3-container w3-teal" style="text-align: center">
			<asp:Button runat="server" ID="btnCerrarVerDocAFIP" class="btn btnBuscar  btn-circle" Style="margin-bottom: 10px; margin-top: 10px;" Text="Cerrar" OnClick="btnCerrarVerDocAFIP_OnClick" />
			<asp:Button runat="server" ID="BtnActualizarAdjunto2" class="btn btnBuscar  btn-circle" Style="margin-bottom: 10px; margin-top: 10px;" Text="Guardar" OnClick="btnActualizarAdjunto2Modal_OnClick" />
		</footer>
			
	</div>
	</div>
	<%--MODAL CONSULTA TRAMITE--%>
	<div id="modalInformacionTituloTramite" runat="server" class="w3-modal">
		<div class="w3-modal-content w3-animate-top w3-card-8">
			<header class="w3-container w3-teal">
                <h3>Trámite SIFCoS</h3>
            </header>
			<div class="w3-container">
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4>
							Información detallado sobre el Trámite que realizó la consulta...</h4>
					</div>
					<div class="panel-body">
						<%--Panel content--%>
						<div id="divMensaejeErrorModal" runat="server" class="alert alert-danger alert-dismissable">
							<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
							</button>
							<strong>Importante! </strong>
							<asp:Label runat="server" ID="lblMensajeErrorModal"></asp:Label>
						</div>
						<h4 class="form-section">
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
						<div class="row">
							<div class="col-md-6">
								<label>
									Sede :</label>
								<asp:DropDownList ID="ddlSede" runat="server" CssClass="form-control">
								</asp:DropDownList>
								<span class="help-block">Seleccione la Sede por la que está inciando el trámite.
								</span>
							</div>
						</div>
					</div>
				</div>
			</div>
			<footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnAceptar"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Continuar" OnClick="btnAceptar_OnClick"/>
                
        </footer>
		</div>
	</div>
	<%--SECCION DE CONSULTA--%>
	<%--<div id="divPantallaConsulta" runat="server">
		<div class="portlet box yellow">
			<div class="portlet-title">
				<div class="caption">
					<i class="fa fa-search"></i>Consultar Mis Trámites - <span class="step-title">
						<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
					</span>
				</div>
			</div>
			<div class="portlet-body form">
				<!-- BEGIN FORM-->
				<div class="form-body">--%>
					<%--<div class="row">
						<div class="col-md-2">
							<div class="form-group">
								<label>
									CUIT</label>
								<asp:TextBox ID="txtFiltroCuit" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
								<ajx:FilteredTextBoxExtender ID="txtFiltroCuit_FilteredTextBoxExtender" runat="server"
									Enabled="True" FilterType="Numbers" TargetControlID="txtFiltroCuit">
								</ajx:FilteredTextBoxExtender>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label>
									Nro. de Tramite (opc)</label>
								<asp:TextBox ID="txtNroTramite" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
								<ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
									FilterType="Numbers" TargetControlID="txtFiltroCuit">
								</ajx:FilteredTextBoxExtender>
							</div>
						</div>
					</div>--%>
				<%--</div>
				<div class="form-actions">
					<div class="row">
						<div class="col-md-2">
							<asp:Button runat="server" ID="btnConsultar" CssClass="btn default form-control"
								Text="Consultar" OnClick="btnConsultar_OnClick" />
						</div>
					</div>
				</div>
				<!-- END FORM-->
			</div>
		</div>
	</div>--%>
	<%--INFORMACION PARA LOS CONTRIBUYENTES--%>
	<div id="divInformacion" runat="server" class="note note-info" visible="False">
		<h4 class="block">
			Información</h4>
		<p>
			<asp:Label runat="server" ID="lblInfoGeneral"></asp:Label>
		</p>
	</div>
	<%--RESULTADO DE CONSULTA--%>
	<div id="divPantallaResultado" runat="server">
		<div class="portlet box yellow">
			<div class="portlet-title">
				<div class="caption">
					<i class="fa fa-file-text-o"></i>Resultado de consulta <span class="step-title">
						<asp:Label ID="Label2" runat="server" Text=""></asp:Label>
					</span>
				</div>
			</div>
			<div class="portlet-body form">
				<!-- BEGIN FORM-->
				<div class="form-body">
					<div class="row">
						<div class="col-md-12" style="overflow: scroll;">
							<asp:GridView ID="gvResultado" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
								AutoGenerateColumns="false" DataKeyNames="NRO_TRAMITE" AllowPaging="true" PageSize="10"
								OnPageIndexChanging="gvResultado_PageIndexChanging" OnRowDataBound="gvResultado_RowDataBound"
								OnRowCommand="gvResultado_OnRowCommand">
								<Columns>
									<asp:BoundField DataField="NRO_TRAMITE" HeaderText="NRO TRAMITE" />
									<asp:BoundField DataField="NRO_SIFCOS" HeaderText="NRO SIFCOS" />
									<asp:BoundField DataField="RAZON_SOCIAL" HeaderText="RAZON SOCIAL" />
									<asp:BoundField DataField="DOMLOCAL" HeaderText="DOMICILIO" />
									<asp:BoundField DataField="TIPO_TRAMITE" HeaderText="TIPO DE TRAMITE" />
									<asp:BoundField DataField="INICIO_ACTIVIDAD" HeaderText="INICIO DE ACTIVIDAD" DataFormatString="{0:d}" />
									<asp:BoundField DataField="FEC_ALTA" HeaderText="FECHA DE ALTA" DataFormatString="{0:d}" />
									<asp:BoundField DataField="ESTADO" HeaderText="ESTADO DE TRAMITE" />
									<asp:BoundField DataField="DESC_ESTADO" HeaderText="DESCRIPCION ESTADO" />
									<asp:TemplateField HeaderText="Acciones">
										<ItemStyle CssClass="grilla-columna-accion" />
										<ItemTemplate>
											<asp:Button ID="BtnImprimirTasa" runat="server" Text="Imprimir TRS" CommandArgument="<%# Container.DataItemIndex %>"
												CommandName="ImprimirTasa" CssClass="botonImprimirTrs" ToolTip="Imprimir Tasa Retributiba de Servicio">
											</asp:Button>
											<asp:Button ID="BtnImprimirTramite" runat="server" Text="Imprimir Trámite" CommandArgument="<%# Container.DataItemIndex %>"
												CommandName="ImprimirTramite" CssClass="botonImprimir" ToolTip="Imprimir Inscripción del trámite">
											</asp:Button>
											<asp:Button ID="btnAdjuntarHab" runat="server" Text="Adj. Habilitacion" CssClass="botonCDD"
												CommandArgument="<%# Container.DataItemIndex %>" CommandName="AdjuntarHab" ToolTip="adjuntar Habilitacion de AFIP">
											</asp:Button>
											<asp:Button ID="btnAdjuntarAFIP" runat="server" Text="Adj. Const. AFIP" CssClass="botonCDD"
												CommandArgument="<%# Container.DataItemIndex %>" CommandName="AdjuntarAFIP" ToolTip="Adjuntar Constancia de AFIP">
											</asp:Button>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Habilitacion Municipal">
										<ItemStyle CssClass="grilla-columna-accion" Width="100px" />
										<ItemTemplate>
											<asp:Button ID="btnVerDoc1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
												CommandName="VerDocumentacion1" ToolTip="Descargar Documentacion Adjuntada" />
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Constancia AFIP">
										<ItemStyle CssClass="grilla-columna-accion" Width="100px" />
										<ItemTemplate>
											<asp:Button ID="btnVerDoc2" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
											            CommandName="VerDocumentacion2" ToolTip="Descargar Documentacion Adjuntada" />
										</ItemTemplate>
									</asp:TemplateField>
									

								</Columns>
							</asp:GridView>
						</div>
					</div>
					<div class="row">
						<div class="col-md-12">
							<h6>
								<asp:Label runat="server" ID="lblTitulocantRegistros" Text="Total de registros encontrados : "></asp:Label><asp:Label
									runat="server" ID="lblTotalRegistrosGrilla" Text="0"></asp:Label></h6>
						</div>
					</div>
					<div class="row">
						<div class="col-md-12">
							<ul class="pagination pull-right">
								<li>
									<h6>
										Páginas :</h6>
								</li>
								<asp:Repeater ID="rptBotonesPaginacion" OnItemDataBound="rptBotonesPaginacion_OnItemDataBound"
									OnItemCommand="rptBotonesPaginacion_OnItemCommand" runat="server">
									<ItemTemplate>
										<li class="paginate_button">
											<asp:LinkButton ID="btnNroPagina" OnClick="btnNroPagina_OnClick" CommandArgument='<%# Bind("nroPagina") %>'
												runat="server" class="btn btn-default "><%# Eval("nroPagina")%></asp:LinkButton>
										</li>
									</ItemTemplate>
								</asp:Repeater>
							</ul>
						</div>
					</div>
				</div>
				<div class="form-actions">
					<div class="row">
						<div class="col-md-2">
							<asp:Button runat="server" ID="btnNuevaInscripcion" CssClass="btn default form-control"
								Text="Nueva Inscripción" OnClick="btnNuevaInscripcion_OnClick" />
						</div>
					</div>
				</div>
				<!-- END FORM-->
			</div>
		</div>
	</div>
		
	
	
</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
	<script type="text/javascript">
		function newTab() {
			var form = document.createElement("form");
			form.method = "GET";
			form.action = "ReporteGeneral.aspx";

			form.target = "_blank";
			document.body.appendChild(form);
			form.submit();
		}

        
	</script>
</asp:Content>

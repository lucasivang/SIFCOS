<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" 
CodeBehind="BajaDeComercio.aspx.cs" Inherits="SIFCOS.BajaDeComercio" 
uiCulture="es" culture="es-MX"%>

<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
     <h3 class="page-title">
			Trámite de Baja de un Comercio Inscripto en SIFCoS.
			</h3>
            <div class="page-bar">
				<ul class="page-breadcrumb">
					<li>
						<i class="fa fa-home"></i>
						<a href="#">Inicio</a>
						<i class="fa fa-angle-right"></i>
					</li>
					<li>
						<a href="BajaDeComercio.aspx">Baja de Comercio</a>
						<i class="fa fa-angle-right"></i>
					</li>
					 
				</ul>
				 
			</div>
             
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="True" EnableScriptLocalization="True">
    </asp:ScriptManager>
     <div class="portlet box yellow">
		<div class="portlet-title">
			<div class="caption">
				 <i class="fa fa-search"></i> Datos del Comercio
			</div>
			 
		</div>
		<div class="portlet-body form">
			<!-- BEGIN FORM-->
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
                                <strong>Éxito! </strong>
                                <asp:Label runat="server" ID="lblMensajeExito"></asp:Label>
                            </div>
                            
                    <div class="row" runat="server" id="DivDatosIniciales">
                       <div class="col-md-2">
							<label>Fecha Cese Municipal :</label>
							<asp:TextBox ID="txtFechaCeseMunicipal" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
							<Ajax:CalendarExtender ID="CalendarExtender2" PopupButtonID="btnFechaCese" runat="server"
								TargetControlID="txtFechaCeseMunicipal" Format="dd/MM/yyyy" PopupPosition="TopLeft">
							</Ajax:CalendarExtender>
						</div>

							
                        <div class="col-md-2">
                            <label>CUIT del Comercio :</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtCuit" MaxLength="11"></asp:TextBox>
                            <Ajax:FilteredTextBoxExtender ID="txtCuit_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtCuit">
                                                </Ajax:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-2">
                            <label>Nro SIFCoS :</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNroSifcos" MaxLength="11"></asp:TextBox>
                             <Ajax:FilteredTextBoxExtender ID="txtNroSifcos_FilteredTextBoxExtender1" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtNroSifcos">
                                                </Ajax:FilteredTextBoxExtender>
                        </div>
                        
                    </div>
                    <br />
                    <div class="row">
                         <div class="col-md-3">
                            <asp:Button ID="btnConsultarEstado" Text="Consultar Estado del Comercio" runat="server"  CssClass="form-control btn blue" OnClick="btnConsultarEstado_OnClick"/>
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnLimpiar" Text="Limpiar" CssClass="form-control btn default" OnClick="btnLimpiar_OnClick"/>
                        </div>
                        
                    </div>
                    <br />
                     <div class="row" runat="server" id="DivDatosEntidad" visible="false">
                        <div class="col-md-4">
                            <label>Fecha Cese Municipal :</label>
                            <asp:label runat="server" CssClass="form-control labelSecundario" ID="lblFechaCese"></asp:label> 
                            
                        </div>
                        <div class="col-md-4">
                            <label>CUIT del Comercio :</label>
                            <asp:label runat="server" CssClass="form-control labelSecundario"  ID="lblCuit" ></asp:label>
                        </div>
                        <div class="col-md-4">
                            <label>Nro SIFCoS :</label>
                            <asp:label runat="server" CssClass="form-control labelSecundario"  ID="lblNroSifcos"></asp:label>
                        </div>
                    </div>
                    <br />
                    <div class="row" runat="server" id="divAcciones">
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnIrAReempadronar" Text="Ir a Reempadronar" CssClass="form-control btn red" Visible="False" OnClick="btnIrAReempadronar_OnClick"/>
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnCompletarDatos" Text="Completar Datos" CssClass="form-control btn red" Visible="False" OnClick="btnCompletarDatos_OnClick"/>
                        </div>
                    </div>
					<div id="divResultadoEstadoComercio" runat="server" >
						<%--<h4 class="block">Información</h4>--%>
						<p>
							<asp:Label runat="server" ID="lblResultadoEstadoComercio"></asp:Label>
						</p>
					</div>
					<div id="divCDD" runat="server" class="row" Visible="False">
						<div id="divPanel_1" runat="server" class="tab-pane active">
							
								<%--AGREGAR DOCUMENTACION ADJUNTA--%>
								<div runat="server" id="DivAdjuntar" visible="True">
									<div class="row">
										<div class="col-md-12">
											<div class="alert alert-block alert-info fade in">
												<h4 class="alert-heading">
													<i class="fa fa-info-circle"></i>INFORMACION A TENER EN CUENTA</h4>
												<ul>
													<li>ESCANEE PREVIAMENTE EL CESE MUNICIPAL Y DENUNCIA POLICIAL DE EXTRAVIO EN FORMATO PDF
														Y ADJUNTELA.<br />
														SI NO TIENE LA FOTO DE LA OBLEA SUBA LA DENUNCIA POLICIAL DE EXTRAVIO<br />
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
												<h5>Adjuntar <strong>Cese municipal</strong></h5>
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
										<div id="divMensajeErrorDocumentacion_1" runat="server" class="alert alert-danger" style="margin-bottom: 0px;" Visible="False">
											<%--<button type="button" class="close" data-dismiss="alert" aria-hidden="true">
											</button>--%>
											<div class="icon">
												<i class="fa fa-times-circle"></i>
												<strong>Error! </strong>
												<asp:Label runat="server" ID="lblMensajeErrorDocumentacion_1"></asp:Label>
												
											</div>
										</div>
										<div id="divMensajeExitoDocumentacion_1" runat="server" class="alert alert-success" style="margin-bottom: 0px;" Visible="False">
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
													Adjuntar <strong>foto de oblea o denuncia policial de extravío</strong></h5>
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
											<div id="divMensajeErrorDocumentacion_2" runat="server" class="alert alert-danger" Visible="False">
												<div class="icon">
													<i class="fa fa-times-circle"></i>
													<strong>Error! </strong>
													<asp:Label runat="server" ID="lblMensajeErrorDocumentacion_2"></asp:Label>
											    </div>
											</div>
											<div id="divMensajeExitoDocumentacion_2" runat="server" class="alert alert-success" Visible="False">
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
										<div class="col-md-4">
											<asp:Button runat="server" ID="btnAdjuntarOtro2" Text="+ Cambiar Documento" OnClick="BtnAdjuntarOtro2_Click"
											            CssClass="form-control btn default" Visible="False"></asp:Button>
										</div>
									</div>
									</div>
								
							</div>
					</div>

						<div class="row">
							<div class="col-md-12">
								<asp:GridView ID="gvResultado" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
								              AutoGenerateColumns="false"  AllowPaging="False" >
									<Columns>
										<asp:BoundField DataField="NroTransaccion" HeaderText="Nro Tasa" />
										<asp:BoundField DataField="Concepto" HeaderText="Concepto" />
										<asp:BoundField DataField="Importe" HeaderText="Importe" />
										<asp:TemplateField HeaderText="Descargar">
											<ItemStyle CssClass="grilla-columna-accion" />
											<ItemTemplate>
												<a href='<%# Eval("Link") %>' target="_blank">Descargar</a>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
						</div>
                     
                     
					</div>
                <div class="form-actions">
                   <div class="row">
                         <div class="col-md-3">
                            <asp:Button ID="btnBajaComercio" Text="DAR DE BAJA AL COMERCIO" runat="server"  CssClass="form-control btn red" OnClick="btnBajaComercioo_OnClick"/>
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnCancelar" Text="SALIR" CssClass="form-control btn default" OnClick="btnCancelar_OnClick"/>
                        </div>
                    </div>
                </div>
			<!-- END FORM-->
		</div>
	</div>

</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
    <script type="text/javascript">
  
    </script>
</asp:Content>


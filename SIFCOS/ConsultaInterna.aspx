<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
	CodeBehind="ConsultaInterna.aspx.cs" Inherits="SIFCOS.ConsultaInterna" UICulture="es"
	Culture="es-MX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
	<h3 class="page-title">
		Consulta Interna
	</h3>
	<div class="page-bar">
		<ul class="page-breadcrumb">
			<li><i class="fa fa-home"></i><a href="#">Inicio</a> <i class="fa fa-angle-right"></i>
			</li>
			<li><a href="MisTramites.aspx">Consulta Interna</a> <i class="fa fa-angle-right"></i>
			</li>
		</ul>
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
	<div id="divPantallaConsulta" runat="server">
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
				<div class="form-body">
					<div class="row">
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
									Nro Sifcos</label>
								<asp:TextBox ID="txtFiltroNroSifcos" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
								<ajx:FilteredTextBoxExtender ID="txtFiltroNroSifcos_FilteredTextBoxExtender1" runat="server"
								                             Enabled="True" FilterType="Numbers" TargetControlID="txtFiltroNroSifcos">
								</ajx:FilteredTextBoxExtender>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<label>
									Nro Trámite</label>
								<asp:TextBox ID="txtFiltroNroTramite" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
								<ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
									Enabled="True" FilterType="Numbers" TargetControlID="txtFiltroNroTramite">
								</ajx:FilteredTextBoxExtender>
							</div>
						</div>
					</div>
				</div>
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
								AutoGenerateColumns="false" DataKeyNames="ID_TRAMITE" AllowPaging="true" PageSize="10"
								OnPageIndexChanging="gvResultado_PageIndexChanging">
								<Columns>
									<asp:BoundField DataField="ID_TRAMITE" HeaderText="NRO TRAMITE" />
                                    <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
									<asp:BoundField DataField="RAZON_SOCIAL" HeaderText="RAZON SOCIAL" />
									<asp:BoundField DataField="NOMBRE_FANTASIA" HeaderText="NOMBRE DE FANTASIA" />
									<asp:BoundField DataField="nro_recsep" HeaderText="NRO SIFCOS" />
									<asp:BoundField DataField="DOMICILIO" HeaderText="DOMICILIO" />
									<asp:BoundField DataField="N_ESTADO_TRAMITE" HeaderText="ESTADO DE TRAMITE" />
									<asp:BoundField DataField="N_TIPO_TRAMITE" HeaderText="TIPO DE TRAMITE" />
									<asp:BoundField DataField="FECHA_PRESENTACION" HeaderText="FECHA PRESENTACION" DataFormatString="{0:d}" />
									<asp:BoundField DataField="ANO_OPERATIVO" HeaderText="ANIO OPERATIVO" />
									<asp:BoundField DataField="ANO_INFORMACION" HeaderText="ANIO DE INFORMACION" />

									
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
							<asp:Button runat="server" ID="btnLimpiarConsulta" CssClass="btn default form-control"
								Text="Limpiar" OnClick="btnLimpiarConsulta_OnClick" />
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
		

        
	</script>
</asp:Content>

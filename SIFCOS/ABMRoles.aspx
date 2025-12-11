<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
 CodeBehind="ABMRoles.aspx.cs" Inherits="SIFCOS.ABMRoles" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
     <h3 class="page-title">
			Configuración de Roles
			</h3>
            <div class="page-bar">
				<ul class="page-breadcrumb">
					<li>
						<i class="fa fa-home"></i>
						<a href="#">Inicio</a>
						<i class="fa fa-angle-right"></i>
					</li>
					<li>
						<a href="ABMRoles.aspx">Configuración de Roles</a>
						<i class="fa fa-angle-right"></i>
					</li>
					 
				</ul>
				 
			</div>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UPRoles" runat="server">
        <ContentTemplate>
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
   <%--MODAL ELIMINAR ROL USUARIO--%>
    <div id="modalEliminarRolUsuario" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-8">
            <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Eliminar Rol asignado</h3>
                    </li>
                    <li style="float: right;">
                       <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>
                </ul>
            </header>
            <div class="w3-container">
                <div class="panel panel-default">
                    <div class="panel-body">
                        
                        <div id="div2" runat="server" class="alert alert-danger alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Importante! </strong>
                            <asp:Label runat="server" ID="Label3"></asp:Label>
                        </div>
                        <div id="div3" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <label>Apellido y Nombre</label>
                                    <asp:TextBox ID="txtDiv2ApeYNom" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-4">
                                    <label>Cuil</label>
                                    <asp:TextBox ID="txtDiv2Cuil" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label>Rol</label>
                                    <asp:TextBox ID="txtDiv2Rol" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label>Fecha último acceso</label>
                                    <asp:TextBox ID="txtDiv2FechaUltAcceso" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12 alert alert-success alert-dismissable">
                                    <label>
                                        Sr Administrador, está por eliminar el rol al usuario seleccionado. Para confirmar la eliminación
                                        haga click en "Aceptar".</label>
                                </div>
                            </div>
                           </div>
                    </div>
                </div>
            </div>
            <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnConfirmarEliminar"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnConfirmarEliminar_OnClick"/>
                <asp:Button runat="server" ID="btnCancelarEliminar"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelarEliminar_OnClick"/>
            </footer>
        </div>
    </div>
    <%--MODAL MODIFICAR ROLES USUARIO--%>
    <div id="modalCambiarRolesUsuario" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-8">
            <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Modificar Rol Usuario</h3>
                    </li>
                    <li style="float: right;">
                       <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>
                </ul>
            </header>
            <div class="w3-container">
                <div class="panel panel-default">
                    <div class="panel-body">
                        
                        <div id="divMensaejeErrorModal" runat="server" class="alert alert-danger alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Importante! </strong>
                            <asp:Label runat="server" ID="lblMensajeErrorModal"></asp:Label>
                        </div>
                        <div id="divModificarRolUsuario" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <label>Apellido y Nombre</label>
                                    <asp:TextBox ID="txtDivApeYNom" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-4">
                                    <label>Cuil</label>
                                    <asp:TextBox ID="txtDivCuil" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label>Rol</label>
                                    <asp:TextBox ID="txtDivRol" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label>Fecha último acceso</label>
                                    <asp:TextBox ID="txtDivFechaUltAcceso" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12 alert alert-success alert-dismissable">
                                    <label>
                                        Sr Administrador, está por cambiar el rol al usuario seleccionado. Para confirmar la modificación
                                        haga click en "Aceptar".</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>
                                        Roles:</label>
                                    <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control required">
                                    </asp:DropDownList>
                                    <span class="help-block">Seleccione el rol para asignar. (*) </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnGuardar"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnGuardar_OnClick"/>
                <asp:Button runat="server" ID="btnCancelar"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelar_OnClick"/>
            </footer>
        </div>
    </div>
    <%--MODAL AGREGAR NUEVO USUARIO--%>
    <div id="modalAgregarNuevoUsuario" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-8">
            <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Nuevo Usuario</h3>
                    </li>
                    <li style="float: right;">
                      <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>
                </ul>
            </header>
            <div class="w3-container">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div id="div5" runat="server">
                            <div class="row">
                                <div class="col-md-12 alert alert-success alert-dismissable">
                                    <label>
                                        Sr Administrador, se va a insertar el rol seleccionado al nuevo usuario ingresado. Para confirmar la inserción
                                        haga click en "Aceptar".</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Cuil Usuario:</label>
                                    <asp:TextBox ID="txtCuilBuscar" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                                                <Ajx:FilteredTextBoxExtender ID="txtCuilBuscar_FilteredTextBoxExtender" runat="server"
                                                                             Enabled="True" FilterType="Numbers" TargetControlID="txtCuilBuscar">
                                                </Ajx:FilteredTextBoxExtender>
                                    <span class="help-block">Ingrese el Cuil del usuario cidi a insertar. (*) </span>
                                </div>
                                <div class="col-md-6">
                                    <label>Roles:</label>
                                    <asp:DropDownList ID="ddlRolesInsertar" runat="server" CssClass="form-control required">
                                    </asp:DropDownList>
                                    <span class="help-block">Seleccione el rol para asignar. (*) </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnAgregarNuevoUsuario"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnAgregarNuevoUsuario_OnClick"/>
                <asp:Button runat="server" ID="btnCancelarNuevoUsuario"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelarNuevoUsuario_OnClick"/>
            </footer>
        </div>
    </div>
    <%--SECCION DE CONSULTA--%>
    <div id="divPantallaConsulta" runat="server">
        <div class="portlet box yellow">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-search"></i>Administración de roles de usuarios SIFCoS - <span class="step-title">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </span>
                </div>
                <div class="actions">
			    <asp:Button runat="server" ID="btnNuevoRol" CssClass="btn  form-control" Text="+ Nuevo Usuario" OnClick="btnNuevoRol_OnClick"/>
			</div>
            </div>
            
            <div class="portlet-body form">
                <!-- BEGIN FORM-->
                <div class="form-body">
                    <div class="row">
                        <div class="col-md-12 center-block">
                            <h5>FILTRO DE BUSQUEDA</h5>
                        </div>
                    </div>
                    <div class="row">
                        
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Cuil</label>
                                <asp:TextBox ID="txtCuil" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                                <ajx:FilteredTextBoxExtender ID="txtCuil_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtCuil">
                                </ajx:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="col-md-6">
                           <div class="col-md-6">
                                    <label>Roles:</label>
                                    <asp:DropDownList ID="ddlRolesConsulta" runat="server" CssClass="form-control required">
                                    </asp:DropDownList>
                                    <span class="help-block">Seleccione el rol para asignar. (*) </span>
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
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnLimpiarFiltros" CssClass="btn default form-control"
                                Text="Limpiar Consulta" OnClick="btnLimpiarFiltros_OnClick"></asp:Button>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlOrdenConsulta" runat="server" CssClass="btn default form-control" />
                            <span class="help-block">Puede elegir el orden de la consulta.</span>
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
                        <div class="col-md-12">
                            <asp:GridView ID="gvResultado" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                AutoGenerateColumns="false" DataKeyNames="Cuil" AllowPaging="true" PageSize="10"
                                OnPageIndexChanging="gvResultado_PageIndexChanging" OnRowDataBound="gvResultado_RowDataBound"
                                OnRowCommand="gvResultado_OnRowCommand">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="id" />
                                    <asp:BoundField DataField="Cuil" HeaderText="CUIL" />
                                    <asp:BoundField DataField="NomYApe" HeaderText="Apellido Y Nombre" />
                                    <asp:BoundField DataField="FecUltAcceso" HeaderText="Ultimo Acceso" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="Rol" HeaderText="Rol" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnCambiarRol" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="CambiarRol" CssClass="botonEditar" ToolTip="Cambiar el Rol al Usuario"></asp:Button>
                                            <asp:Button ID="btnEliminarRol" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="EliminarRol" CssClass="botonEliminar" ToolTip="Eliminar Rol Al Usuario" ></asp:Button>
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
                                    OnItemCommand="rptBotonesPaginacion_OnItemCommand"  runat="server">
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
                <%--<div class="form-actions">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnImprimir" CssClass="btn default form-control" Text="Imprimir"
                                OnClick="btnImprimir_OnClick" />
                        </div>
                    </div>
                </div>--%>
                <!-- END FORM-->
            </div>
        </div>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UPRoles">
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
</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
</asp:Content>


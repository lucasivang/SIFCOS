<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="ABMUsuariosYOrganismos.aspx.cs" Inherits="SIFCOS.ABMUsuariosYOrganismos" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h3 class="page-title">
        Configuración de Organismos a Usuarios
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li><i class="fa fa-home"></i><a href="#">Inicio</a> <i class="fa fa-angle-right"></i>
            </li>
            <li><a href="ABMUsuariosYOrganismos.aspx">Asignación de Organismos a Usuarios</a> <i
                class="fa fa-angle-right"></i></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UPUsuariosYOrganismos" runat="server">
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
            <%--MODAL ELIMINAR ORGANISMOS USUARIO--%>
            <div id="modalEliminarOrgUsuario" runat="server" class="w3-modal">
                <div class="w3-modal-content w3-animate-top w3-card-8">
                    <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Eliminar Organismo asignado</h3>
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
                                            <label>
                                                Apellido y Nombre</label>
                                            <asp:TextBox ID="txtDiv2ApeYNom" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>
                                                Cuil</label>
                                            <asp:TextBox ID="txtDiv2Cuil" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Organismo</label>
                                            <asp:TextBox ID="txtDiv2Organismo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label>
                                                Fecha último acceso</label>
                                            <asp:TextBox ID="txtDiv2FechaUltAcceso" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12 alert alert-success alert-dismissable">
                                            <label>
                                                Sr Administrador, está por eliminar el Organismo asignado al usuario seleccionado.
                                                Para confirmar la eliminación haga click en "Aceptar".</label>
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
            <%--MODAL CAMBIAR ORGANISMO A USUARIO--%>
            <asp:UpdatePanel runat="server" ID="upAsignarOrganismoUsuarioSeleccionado">
                <ContentTemplate>
                    <div id="modalCambiarOrgUsuario" runat="server" class="w3-modal">
                        <div class="w3-modal-content w3-animate-top w3-card-8">
                            <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Cambiar Organismo a Usuario</h3>
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
                                        <div id="divModificarOrgUsuario" runat="server">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <label>
                                                        Apellido y Nombre</label>
                                                    <asp:TextBox ID="txtDivApeYNom" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label>
                                                        Cuil</label>
                                                    <asp:TextBox ID="txtDivCuil" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>
                                                        Organismo</label>
                                                    <asp:TextBox ID="txtDivOrganismo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>
                                                        Fecha último acceso</label>
                                                    <asp:TextBox ID="txtDivFechaUltAcceso" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12 alert alert-success alert-dismissable">
                                                    <label>
                                                        Sr Administrador, está por cambiar el organismo al usuario seleccionado. Para confirmar
                                                        el cambio haga click en "Aceptar".</label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label>
                                                        Organismo:</label>
                                                    <asp:DropDownList ID="ddlOrganismos" runat="server" CssClass="form-control required select2me">
                                                    </asp:DropDownList>
                                                    <span class="help-block">Seleccione el Organismo para asignar. (*) </span>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--MODAL AGREGAR NUEVA RELACION--%>
            <div id="modalAgregarNuevaRelacion" runat="server" class="w3-modal">
                <div class="w3-modal-content w3-animate-top w3-card-8">
                    <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Asignacion de Organismos a Usuarios</h3>
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
                                                Sr Administrador, se va a insertar el Organismo seleccionado al usuario ingresado.
                                                Para confirmar la inserción haga click en "Aceptar".</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>
                                                Cuil Usuario:</label>
                                            <asp:TextBox ID="txtCuilBuscar" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                                            <Ajx:FilteredTextBoxExtender ID="txtCuilBuscar_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtCuilBuscar">
                                            </Ajx:FilteredTextBoxExtender>
                                            <span class="help-block">Ingrese el Cuil del usuario cidi. (*) </span>
                                        </div>
                                        <div class="col-md-2">
                                            <label style="color: white;">
                                                .
                                            </label>
                                            <asp:Button runat="server" ID="btnBuscarPersona" CssClass="btn btnBuscar  btn-circle form-control"
                                                Text="Buscar" OnClick="btnBuscarPersona_OnClick" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-5">
                                            <label>
                                                Nombre:
                                            </label>
                                            <asp:TextBox ID="txtNombrePersona" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-5">
                                            <label>
                                                Apellido:
                                            </label>
                                            <asp:TextBox ID="txtApellidoPesona" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>
                                                Sexo:
                                            </label>
                                            <asp:TextBox ID="txtSexoPersona" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>
                                                Organismos:</label>
                                            <asp:DropDownList ID="ddlOrganismosInsertar" runat="server" CssClass="form-control required select2me">
                                            </asp:DropDownList>
                                            <span class="help-block">Seleccione el organismo para asignar. (*) </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnAgregarNuevaRelacion"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnAgregarNuevaRelacion_OnClick"/>
                <asp:Button runat="server" ID="btnCancelarNuevaRelacion"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelarNuevaRelacion_OnClick"/>
            </footer>
                </div>
            </div>
            <%--SECCION DE CONSULTA--%>
            <div id="divPantallaConsulta" runat="server">
                <div class="portlet box yellow">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-search"></i>Administración de relaciones entre organismos y usuarios
                            SIFCoS - <span class="step-title">
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </span>
                        </div>
                        <div class="actions">
                            <asp:Button runat="server" ID="btnNuevaRelacion" CssClass="btn  form-control" Text="+ Nueva Relacion"
                                OnClick="btnNuevaRelacion_OnClick" />
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <!-- BEGIN FORM-->
                        <div class="form-body">
                            <div class="row">
                                <div class="col-md-12 center-block">
                                    <h5>
                                        FILTRO DE BUSQUEDA</h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>
                                            Cuil</label>
                                        <asp:TextBox ID="txtCuil" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                                        <Ajx:FilteredTextBoxExtender ID="txtCuil_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" FilterType="Numbers" TargetControlID="txtCuil">
                                        </Ajx:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label>
                                        Organismos:</label>
                                    <asp:TextBox ID="txtBuscarOrganismo" runat="server" CssClass="form-control" AutoPostBack="False"
                                        Width="100%" placeholder=" Ingrese al menos tres letras del Organismo a buscar">
                                    </asp:TextBox>
                                    <Ajx:AutoCompleteExtender ServiceMethod="BuscarOrganismo" CompletionInterval="100"
                                        EnableCaching="false" CompletionSetCount="10" TargetControlID="txtBuscarOrganismo"
                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" MinimumPrefixLength="3"
                                        OnClientItemSelected="ace1_itemSelected">
                                    </Ajx:AutoCompleteExtender>
                                    <asp:HiddenField ID="ace1Value" runat="server" />
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
                                        AutoGenerateColumns="false" DataKeyNames="id" AllowPaging="true" PageSize="10"
                                        OnPageIndexChanging="gvResultado_PageIndexChanging" OnRowDataBound="gvResultado_RowDataBound"
                                        OnRowCommand="gvResultado_OnRowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="Cuil" HeaderText="CUIL" />
                                            <asp:BoundField DataField="NomYApe" HeaderText="Apellido Y Nombre" />
                                            <asp:BoundField DataField="FecUltAcceso" HeaderText="Ultimo Acceso" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="Organismo" HeaderText="Organismo" />
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemStyle CssClass="grilla-columna-accion" />
                                                <ItemTemplate>
                                                    <asp:Button ID="btnCambiarOrg" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                        CommandName="CambiarOrg" CssClass="botonEditar" ToolTip="Cambiar el organismo al Usuario">
                                                    </asp:Button>
                                                    <asp:Button ID="btnEliminarOrg" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                        CommandName="EliminarOrg" CssClass="botonEliminar" ToolTip="Eliminar Organimo Al Usuario">
                                                    </asp:Button>
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
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UPUsuariosYOrganismos">
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
    <script type="text/javascript">
        function ace1_itemSelected(sender, e) {
            $('#ConenedorPrincipal_ace1Value').val(e.get_value());
        }
    </script>
</asp:Content>

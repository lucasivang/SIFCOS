<%@ Page Title="Administración de trámites" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" 
    CodeBehind="AdministracionTramites.aspx.cs" Inherits="SIFCOS.AdministracionTramites" 
    UICulture="es" Culture="es-MX" %>

<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h3 class="page-title">Administración de Trámites
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li><i class="fa fa-home"></i><a href="#">Inicio</a> <i class="fa fa-angle-right"></i>
            </li>
            <li><a href="AdministracionTramites.aspx">Administración de Trámites</a> <i class="fa fa-angle-right"></i></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="True"
        EnableScriptLocalization="True">
    </asp:ScriptManager>


   <%-- <asp:UpdatePanel runat="server" ID="PanelPPAL">
        <ContentTemplate>--%>
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
    <%--SECCION INFORMACION BOCA--%>
    <div id="divPantallaInfoBoca" runat="server">
        <div class="portlet box yellow">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa  fa-file-text-o"></i>informacion de la boca - <span class="step-title">
                        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                    </span>
                </div>
            </div>
            <div class="portlet-body form">
                <!-- BEGIN FORM-->
                <div class="form-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>
                                    Boca de Recepción
                                </label>
                                <asp:TextBox ID="txtBocaRecepcion" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>
                                    Localidad</label>
                                <asp:TextBox ID="txtLocalidadBoca" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>
                                    Dependencia</label>
                                <asp:TextBox ID="txtDependencia" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--SECCION DE CONSULTA--%>
    <div id="divPantallaConsulta" runat="server">
        <div class="portlet box yellow">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-search"></i>Consultar Trámites cargados - <span class="step-title">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </span>
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    Razon Social
                                </label>
                                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                <%--<Ajax:AutoCompleteExtender ServiceMethod="BuscarRazonSocial" CompletionInterval="100"
                                    EnableCaching="false" CompletionSetCount="10" TargetControlID="txtRazonSocial"
                                    ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false" MinimumPrefixLength="3"
                                    OnClientItemSelected="ace_Comercio" UseContextKey="true" OnClientShown="onDataShown">
                                </Ajax:AutoCompleteExtender>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    Nro de Trámite</label>
                                <asp:TextBox ID="txtNroTramite" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="txtNroTramite_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtNroTramite">
                                </Ajax:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    Nro sifcos</label>
                                <asp:TextBox ID="txtNroSifcos" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                                <Ajax:FilteredTextBoxExtender ID="txtNroSifcos_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtNroSifcos">
                                </Ajax:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="col-md-3">
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
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    ESTADO DE TRAMITE
                                </label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlEstadoTramite">
                                    <asp:ListItem Text="SELECCIONE UNA OPCION" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="CARGADO" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="VERIFICADO BOCA" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="RECHAZADO BOCA" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="VERIFICADO MINISTERIO" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="RECHAZADO MINISTERIO" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="AUTORIZADO POR MINISTERIO" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="RECHAZADO SIN TASA PAGA" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="ADEUDA" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="MODIFICADO" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="ANULADO" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="RECHAZADO CON DEV DE TASA" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="REIMPRESION VERIFICADA" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="REIMPRESION AUTORIZADA" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="BAJA VERIFICADA" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="BAJA AUTORIZADA" Value="15"></asp:ListItem>
                                </asp:DropDownList>
                                <span class="help-block">Puede filtrar por estado de trámite.</span>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>
                                    TIPO DE TRAMITE
                                </label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTipoTramite">
                                    <asp:ListItem Text="SELECCIONE UNA OPCION" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="ALTA" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="REEMPADRONAMIENTO" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="BAJA" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="REIMPRESION DE OBLEA CON TRS PAGA" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="REIMPRESION DE OBLEA SIN TRS PAGA" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="REIMPRESION DE CERTIFICADO CON TRS PAGA" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="REIMPRESION DE CERTIFICADO SIN TRS PAGA" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="REIMPRESION DE OBLEA Y CERT CON TRS PAGA" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="REIMPRESION DE OBLEA Y CERT SIN TRS PAGA" Value="10"></asp:ListItem>
                                </asp:DropDownList>
                                <span class="help-block">Puede filtrar por tipo de trámite.</span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:CheckBox runat="server" ID="chkFiltroFecha" Text="Filtrar por Fecha" AutoPostBack="True" OnCheckedChanged="chkFiltroFecha_OnCheckedChanged" />
                            <span class="help-block">Para filtrar por fecha de carga haga click en Filtrar por Fecha.</span>
                        </div>
                    </div>
                    <div class="row" runat="server" id="divFiltroFecha" visible="False">
                        <div class="col-md-4 labelModificar">
                            <label>
                                Fecha Desde:
                            </label>
                            <br />
                            <div class="form-group">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnCalendarDesde2" runat="server" ImageUrl="~/img/calendar_24.png" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFechaDesde" CssClass="form-control" runat="server"
                                                TextMode="DateTime"></asp:TextBox>
                                            <Ajax:CalendarExtender ID="CalendarExtender2" PopupButtonID="btnCalendarDesde2" runat="server"
                                                TargetControlID="txtFechaDesde" Format="dd/MM/yyyy" PopupPosition="Topleft">
                                            </Ajax:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="col-md-4 labelModificar">
                            <label>
                                Fecha Hasta:
                            </label>
                            <br />
                            <div class="form-group">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnCalendarHasta2" runat="server" ImageUrl="~/img/calendar_24.png" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFechaHasta" CssClass="form-control" runat="server"
                                                TextMode="DateTime"></asp:TextBox>
                                            <Ajax:CalendarExtender ID="CalendarExtender3" PopupButtonID="btnCalendarHasta2" runat="server"
                                                TargetControlID="txtFechaHasta" Format="dd/MM/yyyy" PopupPosition="Topleft">
                                            </Ajax:CalendarExtender>
                                        </td>
                                    </tr>
                                </table>
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
                                Text="Limpiar" OnClick="btnLimpiarFiltros_OnClick"></asp:Button>
                        </div>
                        
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlOrdenConsulta">
                                <asp:ListItem Text="SELECCIONE UNA OPCION" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="NRO TRAMITE" Value="1"></asp:ListItem>
                                <asp:ListItem Text="NRO SIFCOS" Value="2"></asp:ListItem>
                                <asp:ListItem Text="CUIT" Value="3"></asp:ListItem>
                                <asp:ListItem Text="TIPO TRAMITE" Value="4"></asp:ListItem>
                                <asp:ListItem Text="RAZON SOCIAL" Value="5"></asp:ListItem>
                                <asp:ListItem Text="DOMICILIO" Value="6"></asp:ListItem>
                                <asp:ListItem Text="ESTADO" Value="7"></asp:ListItem>
                                <asp:ListItem Text="VTO_INSCRIPCION" Value="8"></asp:ListItem>
                            </asp:DropDownList>
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
                        <div class="col-md-12" style="overflow: scroll;">
                            <asp:GridView ID="gvResultado" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                AutoGenerateColumns="false" DataKeyNames="NRO_TRAMITE" AllowPaging="true" PageSize="200"
                                OnPageIndexChanging="gvResultado_PageIndexChanging" OnRowDataBound="gvResultado_RowDataBound"
                                OnRowCommand="gvResultado_OnRowCommand">
                                <Columns>
                                    <asp:BoundField DataField="NRO_TRAMITE" HeaderText="NRO TRAMITE" />
                                    <asp:BoundField DataField="NRO_SIFCOS" HeaderText="NRO SIFCOS" />
                                    <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
                                    <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="RAZON SOCIAL" />
                                    <asp:BoundField DataField="DOMLOCAL" HeaderText="DOMICILIO" />
                                    <asp:BoundField DataField="TIPO_TRAMITE" HeaderText="TRAMITE" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="ESTADO DE TRAMITE" />
                                    <asp:BoundField DataField="VTO_TRAMITE" HeaderText="VTO_INSCRIPCION" DataFormatString="{0:d}" />
                                  
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle CssClass="grilla-columna-accion" Width="200px" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnVer" runat="server" Text="Ver" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="Ver" CssClass="botonVer" ToolTip="Ver trámite"></asp:Button>
                                            <asp:Button ID="btnImprimir" runat="server" Text="Imprimir Trámite" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="Imprimir" CssClass="botonImprimir" ToolTip="Imprimir Trámite"></asp:Button>
                                            <asp:Button ID="btnCambiarEstado" runat="server" Text="Cambiar Estado" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="CambiarEstado" CssClass="botonCambiarEstadoTramite" ToolTip="cambiar estado al trámite"></asp:Button>
                                            <asp:Button ID="btnModificar" runat="server" Text="Modificar Trámite" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="Modificar" CssClass="botonModificarTramite" ToolTip="Modificar Inscripción Trámite"></asp:Button>
                                            <asp:Button ID="btnAsignarNroSifcos" runat="server" Text="Asignar Nro SIFCoS" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="AsignarNroSifcos" CssClass="botonAsignarNroSifcos" ToolTip="Asignar Nro. SIFCoS"></asp:Button>
                                            <asp:Button ID="BtnImprimirTasa" runat="server" Text="Imprimir TRS" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="ImprimirTasa" CssClass="botonImprimirTrs" ToolTip="Imprimir Tasa Retributiba de Servicio"></asp:Button>

                                            <asp:Button ID="btnVerDoc1" runat="server" CssClass="botonCDD" Text="Habilitación municipal" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="VerDocumentacion1" ToolTip="Descargar Documentacion Adjuntada" />
                                            <asp:Button ID="btnVerDoc2" runat="server" CssClass="botonCDD" Text="Constancia AFIP" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="VerDocumentacion2" ToolTip="Descargar Documentacion Adjuntada"></asp:Button>

                                            <asp:Button ID="btnVerDoc3" runat="server" CssClass="botonCDD" Text="Denuncia por extravío" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="VerDocumentacion3" ToolTip="Descargar Documentacion Adjuntada" />
                                            <asp:Button ID="btnVerDoc4" runat="server" CssClass="botonCDD" Text="Foto oblea" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="VerDocumentacion4" ToolTip="Descargar Documentacion Adjuntada" />
                                            <asp:HiddenField runat="server" ID="hiddenIdEstadoTramiteSeleccionado" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Consulta Tasas">
                                          <ItemStyle CssClass="grilla-columna-accion" Width="50px" />
                                          <ItemTemplate>
                                              <asp:ImageButton ID="btnDeuda" ImageUrl="~/Img/Logos/Tasas.png" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                  CommandName="Deuda" CssClass="botonTasas" ToolTip="Mostrar Tasas"></asp:ImageButton>
                                          </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mostrar Ubicacion">
                                        <ItemStyle CssClass="grilla-columna-accion" Width="50px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnGeo" ImageUrl="~/img/localizacion-32.png" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="GEO" CssClass="botonGeo" ToolTip="Mostrar Ubicacion" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Notificar CIDI">
                                        <ItemStyle CssClass="grilla-columna-accion" Width="50px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Img/Logos/notificacion.gif" Width="50px" ID="btnEnviar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
				                                CommandName="EnviarNotificacion" CssClass="botonNotificacion" ToolTip="Enviar Notificacion CIDI"  />
                                            <asp:ImageButton ImageUrl="~/Img/Logos/notificarTrsSinPagar.jpg" Width="50px" ID="btnEnviar2" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="EnviarNotificacionTRS" CssClass="botonNotificacion" ToolTip="Enviar Notificacion CIDI"  />
                                            <asp:ImageButton ImageUrl="~/Img/Logos/notificado.jpg" Width="50px" ID="btnNotificado" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="Notificado" CssClass="botonNotificacion" ToolTip="NOTIFICADO" />
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
                                    <h6>Páginas :</h6>
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

                <!-- END FORM-->
            </div>
        </div>

    </div>
    
    <%--MODAL MODIFICAR TRAMITE --%>
    <div id="modalModificarTramite" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-8">
            <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>MODIFICACION DEL TRAMITE SELECCIONADO</h3>
                    </li>
                    <li style="float: right;">
                        <img style="margin-top: 8px;" src="Img/Logos/SIFCOS_Web.png" alt="SIFCoS WEB" />
                    </li>
                </ul>
            </header>
            <div class="w3-container">
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <h3 class="form-section text-center">
                            <i class="fa fa-info-circle"></i>INFORMACION DE LA ENTIDAD A MODIFICAR
                        </h3>
                    </div>
                </div>
                <div id="divMensajeErrorModalModificar" runat="server" class="alert alert-danger alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    </button>
                    <strong>Error! </strong>
                    <asp:Label runat="server" ID="lblMensajeErrorModalModificar"></asp:Label>
                </div>
                <div id="divMensajeExitoModalModificar" runat="server" class="alert alert-success alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                    </button>
                    <strong>Exito! </strong>
                    <asp:Label runat="server" ID="lblMensajeExitoModalModificar"></asp:Label>
                </div>
                <%--Información del trámite a modificar--%>
                <div class="row">
                    <div class="col-md-3">
                        <label>
                            Nro Tramite</label>
                        <asp:TextBox ID="txtModalNroTramite" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label>
                            CUIT</label>
                        <asp:TextBox ID="txtModalCuit" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label>
                            Razón Social</label>
                        <asp:TextBox ID="txtModalRazonSocial" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <br />
                    <br />
                </div>
                <br />
                <div>
                    <div style="text-align: center">
                        <h4 class="block">ELIJA LA SECCION QUE DESEA MODIFICAR</h4>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Button CssClass="btn btn-circle blue btn-block" ID="btnDomEstab" Text="Domicilio del Establecimiento"
                                OnClick="btnDomEstab_Click" runat="server" />
                            <asp:Button CssClass="btn btn-circle lightgrey btn-block" ID="btnConEstab" Text="Contacto del Establecimiento"
                                OnClick="btnConEstab_Click" runat="server" />
                            <asp:Button CssClass="btn btn-circle w3-teal btn-block" ID="btnDomLegal" Text="Domicilio Legal Declarado"
                                OnClick="btnDomLegal_Click" runat="server" />
                        </div>
                        <div class="col-md-6">

                            <asp:Button CssClass="btn btn-circle blue btn-block" ID="btnInfoGral" Text="Información General"
                                OnClick="btnInfoGral_Click" runat="server" />
                            <asp:Button CssClass="btn btn-circle lightgrey btn-block" ID="btnProdAct" Text="Productos,Actividad Primaria y Secundaria"
                                OnClick="btnProdAct_Click" runat="server" />
                            <asp:Button CssClass="btn btn-circle lightgrey btn-block" ID="btnRubro" Text="Rubros Primarios y secundarios"
                                OnClick="btnRubro_Click" runat="server" />
                            <asp:Button CssClass="btn btn-circle w3-teal btn-block" ID="btnRepLegal" Text="Representante Legal del Comercio"
                                OnClick="btnRepLegal_Click" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <!-- BEGIN Portlet PORTLET-->
                        <div class="portlet">
                            <div class="portlet-body">
                                <div>
                                        <div id="divSeccionDomEstab" runat="server" visible="False">
                                            <h3 class="form-section">
                                                <i class="fa fa-home"></i>Domicilio del Establecimiento
                                            </h3>
                                            <div class="w3-container">
                                                <div id="divErrorCargaDomEstab" runat="server" class="alert alert-danger alert-dismissable"
                                                    visible="false">
                                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                                    </button>
                                                    <strong>Error! </strong>
                                                    <asp:Label runat="server" ID="lblErrorCargaDomEstab"></asp:Label>
                                                </div>
                                                <div id="divExitoCargaDomEstab" runat="server" class="alert alert-success alert-dismissable"
                                                    visible="false">
                                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                                    </button>
                                                    <strong>Exito! </strong>
                                                    <asp:Label runat="server" ID="lblMensajeExitoDomEstab"></asp:Label>
                                                </div>
                                                <div class="portlet box yellow" id="form_wizard_DE">
                                                    <%-- <div class="portlet-title">
                                                        <div class="caption">
                                                            <span id="lblDomEstab">DOMICILIO DEL ESTABLECIMIENTO</span>
                                                        </div>
                                                    </div>--%>
                                                    <div class="portlet-body form">
                                                        <div class="form-wizard">
                                                            <div class="form-body">
                                                                <div class="portlet-title">
                                                                </div>
                                                                <div class="portlet-body portlet-collapsed form" style="display: block;">
                                                                    <div class="form-body">
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                                <asp:Button runat="server" ID="btnModificarDomEstab" class="btn blue" Style="margin-bottom: 10px; margin-top: 10px;"
                                                                                    Text="MODIFICAR DOMICILIO ESTABLECIMIENTO" OnClick="btnModificarDomComercio_OnClick" />
                                                                            </div>
                                                                        </div>
                                                                        <br />
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <iframe class="tamañoIframe" src="<%= UrlDomComercio %>" id="VIN_DOM_REAL" style="min-height: 800px; width: 100%;"></iframe>
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
                                        <div id="divSeccionConEstab" runat="server" visible="False">
                                            <h3 class="form-section">
                                                <i class="fa fa-phone-square"></i>Contacto del Establecimiento
                                            </h3>
                                            <div class="row">
                                                <div class="col-md-2 labelModificar">
                                                    <label>
                                                        Cod.Area Cel.</label>
                                                    <asp:TextBox ID="txtCodAreaCelularMd" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                    <Ajax:FilteredTextBoxExtender ID="txtCodAreaCelularMd_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="txtCodAreaCelularMd">
                                                    </Ajax:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-md-4 labelModificar">
                                                    <label>
                                                        Celular</label>
                                                    <asp:TextBox ID="txtCelularMd" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                    <Ajax:FilteredTextBoxExtender ID="txtCelularMd_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="txtCelularMd">
                                                    </Ajax:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-md-2 labelModificar">
                                                    <label>
                                                        Cod. Area Tel.:
                                                    </label>
                                                    <asp:TextBox ID="txtCodAreaTelFijoMd" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                    <Ajax:FilteredTextBoxExtender ID="txtCodAreaTelFijoMd_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="txtCodAreaTelFijoMd">
                                                    </Ajax:FilteredTextBoxExtender>
                                                </div>
                                                <div class="col-md-4 labelModificar">
                                                    <label>
                                                        Telefono Fijo:</label>
                                                    <asp:TextBox ID="txtTelFijoMd" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                    <Ajax:FilteredTextBoxExtender ID="txtTelFijoMd_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="txtTelFijoMd">
                                                    </Ajax:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 labelModificar">
                                                    <label>
                                                        Email:
                                                    </label>
                                                    <asp:TextBox ID="txtEmailMd" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 labelModificar">
                                                    <label>
                                                        Pagina Web:
                                                    </label>
                                                    <asp:TextBox ID="txtWebPageMd" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 labelModificar">
                                                    <label>
                                                        Facebook:
                                                    </label>
                                                    <asp:TextBox ID="txtFacebookMd" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                        <div id="divSeccionDomLegal" runat="server" visible="False">
                                            <h3 class="form-section">
                                                <i class="fa fa-building"></i>Domicilio Legal Declarado
                                            </h3>
                                            <div class="w3-container">
                                                <div id="divErrorCargaDomLegal" runat="server" class="alert alert-danger alert-dismissable"
                                                    visible="false">
                                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                                    </button>
                                                    <strong>Error! </strong>
                                                    <asp:Label runat="server" ID="lblMensajeErrorDomLegal"></asp:Label>
                                                </div>
                                                <div id="divExitoCargaDomLegal" runat="server" class="alert alert-success alert-dismissable"
                                                    visible="false">
                                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                                    </button>
                                                    <strong>Exito! </strong>
                                                    <asp:Label runat="server" ID="lblExitoCargaDomLegal"></asp:Label>
                                                </div>
                                                <div class="portlet box yellow" id="form_wizard_DL">
                                                    <div class="portlet-body form">
                                                        <div class="form-wizard">
                                                            <div class="form-body">
                                                                <div class="portlet-title">
                                                                </div>
                                                                <div class="portlet-body portlet-collapsed form" style="display: block;">
                                                                    <div class="form-body">
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                                <asp:Button runat="server" ID="btnModificarDomLegal" class="btn blue" Style="margin-bottom: 10px; margin-top: 10px;"
                                                                                    Text="MODIFICAR DOMICILIO LEGAL" OnClick="btnModificarDomLegal_OnClick" />
                                                                            </div>
                                                                        </div>
                                                                        <br />
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <iframe class="tamañoIframe" src="<%= UrlDomLegal %>" id="VIN_DOM_LEGAL" style="min-height: 800px; width: 100%;"></iframe>
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
                                        <div id="divSeccionInfoGral" runat="server" visible="False">
                                            <h3 class="form-section">
                                                <i class="fa fa-file-text-o"></i>Información General
                                            </h3>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group">
                                                        <label>
                                                            Nombre Fantasía:</label>
                                                        <asp:TextBox ID="txtNombreFantasia" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label>Fecha de Inicio de Actividad :(*)</label>
                                                    <asp:TextBox ID="txtFechaIniAct" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                                    <Ajax:CalendarExtender ID="CalendarExtender1" PopupButtonID="btnCalendarDesde2" runat="server"
                                                                          TargetControlID="txtFechaIniAct" Format="dd/MM/yyyy" PopupPosition="Topleft">
                                                    </Ajax:CalendarExtender>
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
                                                            Nro. de D.G.R. :(*)</label>
                                                        <asp:TextBox ID="txtNroDGR" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
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
                                            <br />
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label id="lblInmueble" runat="server">
                                                            INMUEBLE:(*)</label>
                                                        <br />
                                                        <asp:RadioButton ID="rbPropietario" Text="propietario" CssClass="CorregirFocoRadioButton"
                                                            GroupName="Inmueble" runat="server" OnCheckedChanged="rbAlquiler_OnCheckedChanged" AutoPostBack="True"/>
                                                        <asp:RadioButton ID="rbInquilino" Text="Inquilino" CssClass="CorregirFocoRadioButton"
                                                            GroupName="Inmueble" runat="server" OnCheckedChanged="rbAlquiler_OnCheckedChanged" AutoPostBack="True"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="divMostrarAlquiler" runat="server" Visible="False">
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
                                            <br />
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
                                                    <asp:TextBox ID="txtCantPersRel" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                    <Ajax:FilteredTextBoxExtender ID="txtCantPersRel_FilteredTextBoxExtender" runat="server"
                                                        Enabled="True" FilterType="Numbers" TargetControlID="txtCantPersRel">
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
                                            <br />
                                        </div>
                                        <div id="divSeccionProdAct" runat="server" visible="False">
                                            <h3 class="form-section">
                                                <i class="fa fa-gift"></i>Productos
                                            </h3>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlProductos" CssClass="form-control select2me"
                                                            placeholder="Seleccione Producto..." />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Button class="btn btnBuscar  btn-circle" ID="btnAgregarProd" Text="+ Agregar Producto"
                                                            runat="server" OnClick="btnAgregarProd_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="gvProducto" CssClass="table table-striped table-bordered table-advance table-hover"
                                                            runat="server" AllowPaging="True" DataKeyNames="IdProducto" AutoGenerateColumns="False"
                                                            OnRowCommand="gvProducto_OnRowCommand" OnPageIndexChanging="gvProducto_PageIndexChanging">
                                                            <Columns>
                                                                <asp:BoundField DataField="IdProducto" HeaderText="Nro" />
                                                                <asp:BoundField DataField="NProducto" HeaderText="Producto" />
                                                                <asp:TemplateField HeaderText="Quitar">
                                                                    <ItemStyle CssClass="grilla-columna-accion" />
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
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-md-6">
                                                        <asp:CheckBox runat="server" ID="chkConfirmarListaDeProducto" Style="font-weight: bold;"
                                                            AutoPostBack="True" Text="Confirmar listado de productos" OnCheckedChanged="chkConfirmarListaDeProducto_OnCheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <h3 class="form-section">
                                                <i class="fa fa-child"></i>Actividad Primaria y Secundaria
                                            </h3>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:GridView ID="gvActividades" CssClass="table table-striped table-bordered table-advance table-hover"
                                                        runat="server" DataKeyNames="IdProducto" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:BoundField DataField="IdProducto" HeaderText="Nro" />
                                                            <asp:BoundField DataField="NProducto" HeaderText="Producto" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12 labelModificar">
                                                    <label>
                                                        Actividad Primaria:</label>
                                                    <asp:DropDownList runat="server" ID="ddlActividadPrimaria" CssClass="form-control select2me">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12 labelModificar">
                                                    <label>
                                                        Actividad Secundaria:</label>
                                                    <asp:DropDownList runat="server" ID="ddlActividadSecundaria" CssClass="form-control select2me">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <br />
                                        </div>
                                        <%--IB: Sección Rubros--%>
                                        <div id="divSeccionRubro" runat="server" visible="False">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <h3 class="form-section">
                                                        <i class="fa fa-gift"></i>Rubro principal:
                                                    <asp:Label runat="server" ID="lblRubroPriMsg"></asp:Label></h3>
                                                    <label>
                                                        Actividad Principal:
                                                    <asp:Label runat="server" ID="lblActividadPpal">NINGUNA</asp:Label></label>
                                                </div>
                                            </div>
                                            <asp:Panel runat="server" ID="pnlRubroPri">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        Estos son los rubros asociados a su actividad principal. Identifique uno y presione
                                                    seleccionar
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlRubrosPriAct" CssClass="form-control select2me" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Button class="btn btnBuscar  btn-circle" ID="btnSeleccionarRubrosPriAct" Text="+ Seleccionar"
                                                            runat="server" OnClick="btnSeleccionarRubrosPriAct_Click"></asp:Button>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <br />
                                                    </div>
                                                    <div class="col-md-12">
                                                        ...o bien identifique un rubro de esta lista completa de rubros y presione seleccionar
                                                    (quedarán asociados ese rubro con la actividad principal luego de guardar cambios)
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlRubrosPri" CssClass="form-control select2me" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Button class="btn btnBuscar  btn-circle" ID="btnSeleccionarRubrosPri" Text="+ Seleccionar"
                                                            runat="server" OnClick="btnSeleccionarRubrosPri_Click"></asp:Button>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:HiddenField runat="server" ID="hdRubroPriOrigen" />
                                                    <asp:HiddenField runat="server" ID="hdRubroPri" />
                                                </div>
                                            </asp:Panel>
                                            <br />
                                            <br />
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <h3 class="form-section">
                                                        <i class="fa fa-gift"></i>Rubro secundario:
                                                    <asp:Label runat="server" ID="lblRubroSecMsg"></asp:Label></h3>
                                                    <label>
                                                        Actividad secundaria:
                                                    <asp:Label runat="server" ID="lblActividadSec">NINGUNA</asp:Label></label>
                                                </div>
                                            </div>
                                            <asp:Panel runat="server" ID="pnlRubroSec">
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        Estos son los rubros asociados a su actividad secundaria. Identifique uno y presione
                                                    seleccionar
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlRubrosSecAct" CssClass="form-control select2me" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Button class="btn btnBuscar  btn-circle" ID="btnSeleccionarRubrosSecAct" Text="+ Seleccionar"
                                                            runat="server" OnClick="btnSeleccionarRubrosSecAct_Click"></asp:Button>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <br />
                                                    </div>
                                                    <div class="col-md-12">
                                                        ...o bien identifique un rubro de esta lista completa de rubros y presione seleccionar
                                                    (quedarán asociados ese rubro con la actividad secundaria luego de guardar cambios)
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:DropDownList runat="server" ID="ddlRubrosSec" CssClass="form-control select2me" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Button class="btn btnBuscar  btn-circle" ID="btnSeleccionarRubrosSec" Text="+ Seleccionar"
                                                            runat="server" OnClick="btnSeleccionarRubrosSec_Click"></asp:Button>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:HiddenField runat="server" ID="hdRubroSecOrigen" />
                                                    <asp:HiddenField runat="server" ID="hdRubroSec" />
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div id="divSeccionRepLegal" runat="server" visible="False">
                                            <h3 class="form-section">
                                                <i class="fa fa-user"></i>Representante Legal del Comercio
                                            </h3>
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
                                            <%--BUSCAR REP LEGAL--%>
                                            <div class="row">
                                                <div class="form-group">
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
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <label>
                                                        Cargo que ocupa :(*)</label>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCargoOcupa">
                                                        <asp:ListItem Text="Titular" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Presidente" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Gerente" Value="3"></asp:ListItem>
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
                                            <br />
                                        </div>
                                </div>
                            </div>
                        </div>
                        <!-- END Portlet PORTLET-->
                    </div>
                </div>
            </div>
            <footer class="w3-container w3-teal">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Button runat="server" ID="btnVolver"  CssClass="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Salir" OnClick="btnVolver_OnClick"/>        
                    </div>
                    <div class="col-md-2" id="divGuardarCambios" runat="server" visible="False">
                        <asp:Button runat="server"  ID="btnGuardarCambios" CssClass="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Guardar Cambios" OnClick="btnGuardarCambios_Click" />
                    
                    </div>
                </div>
            </footer>
        </div>
    </div>
   
    <%--MODAL MOSTRAR MAPA--%>
    <div id="divModalMostrarMapa" runat="server" class="w3-modal modal">

        <div class="w3-modal-content w3-animate-top w3-card-8">
            <header class="w3-container w3-teal">
                        <ul style="list-style-type: none; margin: 0; padding: 0; overflow: hidden;">
                            <li style="text-align: center;color: white">
                                <h3>Ubicacion Georeferenciada del Comercio:</h3>
                                
                            </li>

                        </ul>
                    </header>
            <div class="w3-container">

                <div class="row">
                    <div class="col-md-12">
                        <iframe class="tamañoIframe" src="<%= UrlDomComercio %>" id="VIN_DOM_COMERCIO" style="min-height: 800px; width: 100%;"></iframe>
                    </div>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-3">

                </div>
                <div class="col-md-3">
                    <asp:Button runat="server" ID="btnActualizarDomicilio" CssClass="btn blue  default form-control" Text="Actualizar Domicilio" OnClick="btnActualizarDomicilio_OnClick" />
                </div>
                <div class="col-md-3">
                    <asp:Button runat="server" ID="btnCerrarMapa" CssClass="btn blue  default form-control" Text="CERRAR" OnClick="btnCerrarMapa_OnClick" />
                 </div>
            </div>
        </div>

    </div>
    <%--MODAL CAMBIAR ESTADO TRAMITE--%>
    <div id="modalCambiarEstadoTramite" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-8">
            <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Cambiar estado Trámite</h3>
                    </li>
                    <li style="float: right;">
                      <img style="margin-top: 8px;" src="img/Logos/SIFCOS_Web.png" alt="SIFCoS WEB" />
                    </li>
                </ul>
            </header>
            <div class="w3-container">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div id="divMensajeErrorBaja" runat="server" class="alert alert-danger alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Error! </strong>
                            <asp:Label runat="server" ID="lblMensajeErrorBaja"></asp:Label>
                        </div>
                        <div id="divMensajeExitoBaja" runat="server" class="alert alert-success alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Exito! </strong>
                            <asp:Label runat="server" ID="lblMensajeExitoBaja"></asp:Label>
                        </div>
                        <div id="divMensaejeErrorModal" runat="server" class="alert alert-danger alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Importante! </strong>
                            <asp:Label runat="server" ID="lblMensajeErrorModal"></asp:Label>
                        </div>
                        <div id="divCambiarEstadoTramite" runat="server">
                            <div class="row">
                                <div class="col-md-2">
                                    <label>
                                        CUIT</label>
                                    <asp:TextBox ID="txtDivCuit" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-10">
                                    <label>
                                        Razón social</label>
                                    <asp:TextBox ID="txtDivRazonSocial" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-2">
                                    <label>
                                        Nro Trámite</label>
                                    <asp:TextBox ID="txtDivNroTramite" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Tipo Trámite</label>
                                    <asp:TextBox ID="txtDivTipoTramite" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label>
                                        Estado de Trámite</label>
                                    <asp:TextBox ID="txtDivEstadoTramite" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label>
                                        Fecha Ult. Cambio</label>
                                    <asp:TextBox ID="txtDivFechaUltimoCambio" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12 alert alert-success alert-dismissable">
                                    <label>
                                        Sr Administrador, está por cambiar el estado del trámite. Si va a cambiar el estado
                                        a Verificado debe seleccionar una Tasa Retributiva de Servicio abonada y luego seleccionar
                                        el estado y confirmar haciendo click en "Aceptar", Si no posee tasas pagas no puede
                                        cambiar el estado a Verificado. Recuerde que si desea dar de baja debe estar al
                                        día con los pagos de tasas.</label>
                                </div>
                            </div>
                            <div id="mostrarTRSPAGAS" runat="server" class="row">
                                <div class="col-md-12">
                                    <label>
                                        Tasas Retributivas Pagas:</label>
                                    <asp:DropDownList ID="ddlListaTRS" runat="server" CssClass="form-control required">
                                    </asp:DropDownList>
                                    <span class="help-block">Seleccione la trs Paga. (*) </span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label>
                                        Estado:</label>
                                    <asp:DropDownList ID="ddlEstados" runat="server" CssClass="form-control required"
                                        OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <span class="help-block">Seleccione el estado del trámite. (*) </span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label>
                                        Descripción:</label>
                                    <asp:TextBox ID="txtModalDescripcionEstado" Enabled="True" runat="server" CssClass="form-control required"
                                        TextMode="MultiLine" MaxLength="50" Rows="3"></asp:TextBox>
                                    <span class="help-block">Ingrese una descripción del estado. (*)</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnGuardarEstado"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnGuardarEstado_OnClick"/>
                <asp:Button runat="server" ID="btnCancelarEstado"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelarEstado_OnClick"/>
            </footer>
        </div>
    </div>
    <%--MODAL ASIGNAR NRO SIFCOS--%>
    <div id="modalAsignarNroSifcos" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-8">
            <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Asignar número SIFCoS al trámite</h3>
                    </li>
                    <li style="float: right;">
                     <img style="margin-top: 8px;" src="Img/Logos/SIFCOS_Web.png" alt="SIFCoS WEB" />
                    </li>
                </ul>
            </header>
            <div class="w3-container">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div id="divExitoAsignarNroSifcos" runat="server" class="alert alert-success alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Exito! </strong>
                            <h2>
                                <asp:Label runat="server" ID="lblMensajeExitoModalAsignarNroSifcos"></asp:Label></h2>
                        </div>
                        <div id="divErrorAsignarNroSifcos" runat="server" class="alert alert-danger alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Importante! </strong>
                            <asp:Label runat="server" ID="lblDivMensajeError1"></asp:Label>
                        </div>
                        <div id="div6" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <label>
                                        Razón social</label>
                                    <asp:TextBox ID="txtRazonSocial2" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-4">
                                    <label>
                                        Nro Trámite</label>
                                    <asp:TextBox ID="txtNroTramite2" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Estado Trámite</label>
                                    <asp:TextBox ID="txtEstadoTramite2" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <label>
                                        Fecha último cambio</label>
                                    <asp:TextBox ID="txtFechaUltCambio2" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12 alert alert-success alert-dismissable">
                                    <label>
                                        Sr Administrador, está por asignar el numero SIFCoS al trámite. Para confirmar la
                                        asignación haga click en "Aceptar", Recuerde que para asignar el número debe tener
                                        la tasa paga y la verificacion de la documentación presentada.</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label>
                                        Observaciones:</label>
                                    <asp:TextBox ID="txtDescripcion" Enabled="True" runat="server" CssClass="form-control required"
                                        TextMode="MultiLine" MaxLength="22"></asp:TextBox>
                                    <span class="help-block">Ingrese algun comentario si lo desea (opcional).</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnConfirmarNroSifcos"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnConfirmarNroSifcos_OnClick"/>
                <asp:Button runat="server" ID="btnCancelarAsignacionNroSifcos"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelarAsignacionNroSifcos_OnClick"/>
            </footer>
        </div>
    </div>
    <%--MODAL INFORMACION DEUDA TRAMITE--%>
    <div id="ModalInfoDeudaTramite" runat="server" class="w3-modal">
        <div class="w3-modal-content w3-animate-top w3-card-8">
            <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Tasas Retributivas de Servicios Utilizadas para el Trámite Seleccionado</h3>
                    </li>
                    <li style="float: right;">
                      <img style="margin-top: 8px;height:60px" src="Img/Logos/SIFCOS_Web.png" alt="SIFCoS WEB" />
                    </li>
                </ul>
            </header>
            <div class="w3-container">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div id="divMensajeExitoInfoDeuda" runat="server" class="alert alert-success alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Exito! </strong>
                            <h2>
                                <asp:Label runat="server" ID="lblMensajeExitoInfoDeuda"></asp:Label></h2>
                        </div>
                        <div id="divMensajeErrorInfoDeuda" runat="server" class="alert alert-danger alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Importante! </strong>
                            <asp:Label runat="server" ID="lblMensajeErrorInfoDeuda"></asp:Label>
                        </div>
                        <div id="divGrillaTasas" visible="false" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GVDeuda" CssClass="table table-striped table-bordered table-advance table-hover"
                                        runat="server" DataKeyNames="NRO_TRAMITE_SIFCOS" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="NRO_TRAMITE_SIFCOS" HeaderText="Nro Trámite" />
                                            <asp:BoundField DataField="CONCEPTO" HeaderText="Concepto" />
                                            <asp:BoundField DataField="NRO_TRS" HeaderText="Nro Tasa" />
                                            <asp:BoundField DataField="FECHA_COBRO" HeaderText="Fecha Pago" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="FECHA_EMISION" HeaderText="Impresión Tasa" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="IMPORTE" HeaderText="Importe" />
                                            <asp:BoundField DataField="VENCIMIENTO" HeaderText="Vencimiento" DataFormatString="{0:d}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12 alert alert-success alert-dismissable">
                                    <label>
                                        Sr Administrador, para poder dar de alta o la baja definitiva es requisito indispensable
                                        que pague al menos una tasa retributiva generada para el tramite consultado que
                                        aun no haya vencido.</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnCancelarInfoDeuda"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Volver" OnClick="btnCancelarInfoDeuda_OnClick"/>
            </footer>
        </div>
    </div>
   
    <%--SECCION VER INFORMACION TRAMITE --%>
    <div id="divPantallaVerTramite" runat="server">
        <div class="portlet box yellow">
            <div class="portlet-title">
                <div class="caption">
                    <i class="fa fa-search"></i>INFORMACION COMPLETA DE LA ENTIDAD - <span class="step-title">
                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    </span>
                </div>
            </div>
            <div class="portlet-body form">
                <!-- BEGIN FORM-->
                <div class="form-body">
                    <div class="row">
                        <div class="col-md-12">
                            <h3 class="form-section" style="background-color: rgb(278,230,200);">
                                <asp:Label ID="lblTituloEmpresa" runat="server"></asp:Label>
                            </h3>
                        </div>
                    </div>
                    <h3 class="form-section">Seguimiento del Trámite Nro:
                        <asp:Label ID="lblNroTramite" runat="server"></asp:Label>
                    </h3>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvHojaRuta" CssClass="table table-striped table-bordered table-advance table-hover"
                                runat="server" DataKeyNames="ult_cambio" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                    <asp:BoundField DataField="ult_cambio" HeaderText="Ultimo cambio" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Observaciones" />
                                    <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                                    <asp:BoundField DataField="organismo" HeaderText="Organismo" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <%--<h3 class="form-section">Tasas Retributivas usadas en el trámite</h3>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GVTrsUsadas" CssClass="table table-striped table-bordered table-advance table-hover"
                                runat="server" DataKeyNames="nro_liquidacion" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="nro_liquidacion" HeaderText="NRO LIQUIDACION" />
                                    <asp:BoundField DataField="nro_transaccion" HeaderText="NRO TRANSACCION" />
                                    <asp:BoundField DataField="pagado" HeaderText="PAGADA" Visible="false" />
                                    <asp:BoundField DataField="fecha_alta" HeaderText="FECHA CREACION" />
                                </Columns>
                            </asp:GridView>
                            <asp:Label runat="server" ID="lblSinResultado" CssClass="labelSecundario"></asp:Label>
                        </div>
                    </div>--%>
                    <h3 class="form-section">Dirección del Establecimiento</h3>
                    <div class="row">
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Provincia:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblDomProvincia" runat="server" />
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Departamento:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblDomDepartamento" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Localidad:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblDomLocalidad" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Barrio:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblDomBarrio" runat="server" />
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Calle:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblDomCalle" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Nro/Km:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblDomNro" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Código Postal:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblDomCodPostal" runat="server" />
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Depto:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblDomDepto" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Piso:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblDomPiso" runat="server"></asp:Label>
                        </div>
                    </div>
                    <h3 class="form-section">Contacto del Establecimiento</h3>
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
                                Pagina Web:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblWebPage" runat="server" />
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Facebook:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblFacebook" runat="server"></asp:Label>
                        </div>
                    </div>
                    <h3 class="form-section">Representante Legal Establecimiento</h3>
                    <div class="row">
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Apellido y Nombre:
                            </label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblApeYNomRL" runat="server" />
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>Dni:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblDniRL" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>Email Rep Legal:</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblEmailRepLegal" runat="server"></asp:Label>
                        </div>
                    </div>
                    <h3 class="form-section">Gestor que realizó el trámite</h3>
                    <div class="row">
                        <div class="col-md-2 labelPrimario">
                            <label>Apellido y Nombre:
                            </label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblApeYNomGestor" runat="server" />
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>Dni:
                            </label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblDniGestor" runat="server"></asp:Label>
                        </div>
                         <div class="col-md-2 labelPrimario">
                             <label>Email Gestor:
                             </label>
                         </div>
                         <div class="col-md-2 labelSecundario">
                             <asp:Label ID="lblEmailGestor" runat="server"></asp:Label>
                         </div>
                    </div>
                    <h3 class="form-section">Información General</h3>
                    <div class="row">
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Fecha de Alta Tramite :</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblFechaAltaTramite" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Nro. Hab Municipal :</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblNroHabilitacionMunicipal" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Nro. de D.G.R. :</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblNroDGR" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Inicio de Actividad :</label>
                        </div>
                        <div class="col-md-2 labelSecundario">
                            <asp:Label ID="lblFecInicioActividad" runat="server"></asp:Label>
                        </div>
                    </div>
                    <h3 class="form-section">Productos y Actividades Primaria y Secundaria</h3>
                    <br />
                    <div class="row">
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Actividad Primaria:
                            </label>
                        </div>
                        <div class="col-md-10 labelSecundario">
                            <asp:Label ID="lblActividadPrimaria" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 labelPrimario">
                            <label>
                                Actividad Secundaria:
                            </label>
                        </div>
                        <div class="col-md-10 labelSecundario">
                            <asp:Label ID="lblActividadSecundaria" runat="server"></asp:Label>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvProductos" CssClass="table table-striped table-bordered table-advance table-hover"
                                runat="server" DataKeyNames="IdProducto" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="IdProducto" HeaderText="Codigo" />
                                    <asp:BoundField DataField="NProducto" HeaderText="Producto" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="form-actions">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnVolverVer" CssClass="btn default form-control"
                                Text="Volver" OnClick="btnVolverVer_OnClick" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
       <%-- </ContentTemplate>
   </asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
    <script type="text/javascript">


        function ace_Comercio(sender, e) {
            var cuit = e.get_value();
            $('#ContenedorPrincipal_txtCuit').val(cuit);
        }

        function onDataShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 1000001;

        }
        $(document).ready(function () {


            $('#ContenedorPrincipal_rbPropietario').click(function () {
                if ($(this).is(':checked')) {
                    $("#divMostrarAlquiler").hide();
                }
            });
            $("#ContenedorPrincipal_rbInquilino").click(function () {
                if ($(this).is(':checked')) {
                    $("#divMostrarAlquiler").show();



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
            //inicializarMapa();


        });

    </script>
</asp:Content>
<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="ReporteSIFCoS.aspx.cs" Inherits="SIFCOS.ReporteSIFCoS" 
    uiCulture="es" culture="es-MX" %>
<%@ Register TagPrefix="Ajx" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h3 class="page-title">
        Reportes Gerenciales
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li><i class="fa fa-home"></i><a href="#">Inicio</a> <i class="fa fa-angle-right"></i>
            </li>
            <li><a href="ReporteSIFCoS.aspx">Reportes Gerenciales</a> <i class="fa fa-angle-right">
            </i></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="True"
        EnableScriptLocalization="True">
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
                            <i class="fa fa-search"></i>Página de Reportes SIFCoS - <span class="step-title">
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            </span>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <!-- BEGIN FORM-->
                        <div class="form-body">
                            <%--<div class="row" style="text-align: center">
                        <div class="col-md-12">
                            <h5>ELIJA SU TIPO DE REPORTE</h5>
                        </div>
                     </div>--%>
                            <%-- <div class="row">
                    <div class="col-md-6">
                      <asp:Button CssClass="btn btn-circle blue btn-block" ID="btnTipoTramite" Text="POR TIPO DE TRAMITE" OnClick="btnTipoTramite_Click"
                    runat="server" />
                      <br/>
                      <asp:Button CssClass="btn btn-circle lightgrey btn-block" ID="btnFecAlta" Text="POR FECHA DE ALTA" OnClick="btnFecAlta_Click"
                    runat="server" />
                      <br/>
                      <asp:Button CssClass="btn btn-circle w3-teal btn-block" ID="btnComercio" Text="POR DNI Y SEXO / CUIT / RAZON SOCIAL" OnClick="btnComercio_Click"
                    runat="server" />  
                    </div>
                    <div class="col-md-6">
                        <asp:Button CssClass="btn btn-circle blue btn-block" ID="btnRangoTramite" Text="POR RANGO DE NRO TRAMITE / NRO SIFCOS" OnClick="btnRangoTramite_Click"
                    runat="server" />
                    <br/>
                        <asp:Button CssClass="btn btn-circle lightgrey btn-block" ID="btnUbicacion" Text="ESTADISTICOS UBICACION" OnClick="btnUbicacion_Click"
                    runat="server" />
                    <br/>
                        <asp:Button CssClass="btn btn-circle w3-teal btn-block" ID="btnActividad" Text="ESTADISTICOS ACTIVIDAD" OnClick="btnActividad_Click"
                    runat="server" /> 
                    <br/>  
                    </div>
                </div>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- BEGIN Portlet PORTLET-->
                                    <div class="portlet">
                                        <div class="portlet-body">
                                            <div style="height: 300px">
                                                <form role="form">
                                                <div class="row">
                                                    <div class="col-md-4 labelModificar">
                                                        <div class="form-group">
                                                            <label>
                                                                TIPO DE TRAMITE
                                                            </label>
                                                            <asp:DropDownList ID="ddlTipoTramite" runat="server" CssClass="btn default form-control" />
                                                            <span class="help-block">Puede filtrar por tipo de trámite.</span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 labelModificar" id="divOrganismoPadre" runat="server" Visible="True">
                                                        <div class="form-group">
                                                            <label>
                                                                BOCA DE RECEPCION PADRE
                                                            </label>
                                                            <asp:DropDownList ID="ddlBocaRecepcionPadre" runat="server" CssClass="btn default form-control" OnSelectedIndexChanged="ddlBocaRecepcionPadre_OnSelectedIndexChanged" 
                                                            AutoPostBack="True" >
                                                            </asp:DropDownList>
                                                            <span class="help-block">Puede filtrar por Boca de Recepción independiente.</span>
                                                        </div>
                                                    </div>
                                                     <div class="col-md-4 labelModificar" id="divOrganismo" runat="server" Visible="True">
                                                        <div class="form-group">
                                                            <label>
                                                                BOCA DE RECEPCION
                                                            </label>
                                                            <asp:DropDownList ID="ddlBocaRecepcion" runat="server" CssClass="btn default form-control" />
                                                            <span class="help-block">Puede filtrar por Boca de Recepción dependiente.</span>
                                                        </div>
                                                    </div>
                                                    </div>
                                                    <div class="row">
                                                    <div class="col-md-4 labelModificar">
                                                        <div class="form-group">
                                                            <label>
                                                                RANGO NRO DE TRAMITE
                                                            </label>
                                                            <br />
                                                            <asp:TextBox ID="txtNroTramiteDesde" CssClass="form-control" runat="server" MaxLength="7"></asp:TextBox>
                                                            <span class="help-block">DESDE</span>
                                                            <br />
                                                            <asp:TextBox ID="txtNroTramiteHasta" CssClass="form-control" runat="server" MaxLength="7"></asp:TextBox>
                                                            <span class="help-block">HASTA</span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 labelModificar">
                                                        <div class="form-group">
                                                            <label>
                                                                RANGO NRO SIFCOS
                                                            </label>
                                                            <br />
                                                            <asp:TextBox ID="txtNroSifcosDesde" CssClass="form-control" runat="server" MaxLength="7"></asp:TextBox>
                                                            <span class="help-block">DESDE</span>
                                                            <br />
                                                            <asp:TextBox ID="txtNroSifcosHasta" CssClass="form-control" runat="server" MaxLength="7"></asp:TextBox>
                                                            <span class="help-block">HASTA</span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 labelModificar">
                                                        <div class="form-group">
                                                            <label>
                                                                ESTADO TRAMITE
                                                            </label>
                                                            <asp:DropDownList ID="ddlEstadoTramite" runat="server" CssClass="btn default form-control" />
                                                            <span class="help-block">Puede filtrar por estado de trámite.</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                    <h3>FILTRAR POR COMERCIO</h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 labelModificar">
                                                        <div class="form-group">
                                                            <label>
                                                                Razon Social
                                                            </label>
                                                            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 labelModificar">
                                                        <div class="form-group">
                                                            <label>
                                                                CUIT
                                                            </label>
                                                            <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                                                            <Ajx:FilteredTextBoxExtender ID="txtCuit_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Numbers" TargetControlID="txtCuit">
                                                            </Ajx:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                    <h3>FILTRAR POR UBICACION DEL COMERCIO</h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 labelModificar">
                                                        <label>
                                                            Departamento:
                                                        </label>
                                                        <asp:DropDownList ID="ddlDeptos" CssClass="form-control select2me" runat="server"
                                                            OnSelectedIndexChanged="ddlDeptos_SelectedIndexChanged" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4 labelModificar">
                                                        <label>
                                                            Localidad:
                                                        </label>
                                                        <asp:DropDownList ID="ddlLocalidad" CssClass="form-control select2me" runat="server"
                                                            AutoPostBack="False">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                 <div class="row">
                                                     <div class="col-md-12">
                                                    <h3>
                                                        FILTRAR POR FECHA DE ALTA DEL TRAMITE</h3>     
                                                     </div>
                                                    
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 labelModificar">
                                                        <label>
                                                            Fecha Desde:
                                                        </label>
                                                        <br />
                                                        <div class="form-group">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnCalendarDesde2" runat="server" ImageUrl="~/Resources/calendar_24.png" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFechaDesde" CssClass="form-control mascaraFecha" runat="server"
                                                                            TextMode="DateTime"></asp:TextBox>
                                                                        <Ajx:CalendarExtender ID="CalendarExtender2" PopupButtonID="btnCalendarDesde2" runat="server"
                                                                            TargetControlID="txtFechaDesde" Format="dd/MM/yyyy">
                                                                        </Ajx:CalendarExtender>
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
                                                                        <asp:ImageButton ID="btnCalendarHasta2" runat="server" ImageUrl="~/Resources/calendar_24.png" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFechaHasta" CssClass="form-control mascaraFecha" runat="server"
                                                                            TextMode="DateTime"></asp:TextBox>
                                                                        <Ajx:CalendarExtender ID="CalendarExtender3" PopupButtonID="btnCalendarHasta2" runat="server"
                                                                            TargetControlID="txtFechaHasta" Format="dd/MM/yyyy">
                                                                        </Ajx:CalendarExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
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
                                <div id="divGVResultado" class="col-md-12" style="overflow: scroll;" align="center" runat="server">
                                    <label>
                                        REPORTE GERENCIAL</label>
                                    <asp:GridView ID="GVResultadoReporte" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                        AutoGenerateColumns="false" DataKeyNames="Nro_tramite" AllowPaging="true" PageSize="10"
                                        OnPageIndexChanging="gvResultadoReporte_PageIndexChanging" OnRowDataBound="gvResultadoReporte_RowDataBound"
                                        OnRowCommand="gvResultado_OnRowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="BOCA_RECEPCION" HeaderText="BOCA_RECEPCION" />
                                            <asp:BoundField DataField="TIPO_TRAMITE" HeaderText="TIPO TRAMITE" />
                                            <asp:BoundField DataField="ESTADO_TRAMITE" HeaderText="ESTADO TRAMITE" />
                                            <asp:BoundField DataField="NRO_TRAMITE" HeaderText="NRO TRAMITE" />
                                            <asp:BoundField DataField="NRO_SIFCOS" HeaderText="NRO SIFCOS" Visible="False" />
                                            <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
                                            <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="RAZON SOCIAL" />
                                            
                                            <asp:BoundField DataField="FEC_ALTA" HeaderText="FECHA TRAMITE" />
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemStyle CssClass="grilla-columna-accion" />
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDetalle" runat="server" Text="Detalle" CommandArgument="<%# Container.DataItemIndex %>"
                                                        CommandName="Detalle" CssClass="btn" ToolTip="Mostrar mas detalles"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <ul class="pagination pull-right">
                                        
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
                            <div class="row">
                                <div class="col-md-12">
                                    <h6>
                                        <asp:Label runat="server" ID="lblTitulocantRegistros" Text="Cantidad total de trámites: "></asp:Label><asp:Label
                                            runat="server" ID="lblTotalRegistrosGrilla" Text="0"></asp:Label></h6>
                                </div>
                            </div>
                            <div class="row">
                                <div  id="divGVdetalle"  class="col-md-12" style="overflow: scroll;" align="center"  runat="server">
                                    <label>
                                        DETALLE TRAMITE SELECCIONADO</label>
                                    <asp:GridView ID="GVDetalleTramite" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                        AutoGenerateColumns="True" DataKeyNames="Nro_tramite" Visible="False" >
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="form-actions">
                           <div class="row">
                               <div class="col-md-2">
                               <asp:Button runat="server" ID="btnSalirDetalleSeleccionado" CssClass="form-control btn" Text="Salir Detalle" OnClick="btnSalirDetalleSeleccionado_OnClick"/>
                               </div>
                           </div>
                        </div>
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
    <div class="portlet box yellow" >
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-file-text-o"></i>Opciones de Descarga<span class="step-title">
                            </span>
                        </div>
                    </div>
                     <div class="form-body"> 
                         <%--NO HAY CUERPO--%>
                         </div>
                        
                    <div class="form-actions">
                        <div class="row" >
                            <div class="col-md-2">
                                    <asp:Button runat="server" ID="btnExcel" CssClass="btn default form-control" Text="Guardar en Excel"
                                        OnClick="btnExcel_OnClick" />
                                </div> 
                                <div class="col-md-2">
                                    <asp:Button runat="server" ID="btnPDF" CssClass="btn default form-control" Text="Guardar en PDF"
                                        OnClick="btnPDF_OnClick" Visible="False" />
                                </div>
                                
                            </div>
                    </div> 
                </div> 
</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
</asp:Content>

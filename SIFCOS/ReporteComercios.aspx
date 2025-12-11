<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="ReporteComercios.aspx.cs" Inherits="SIFCOS.ReporteComercios"
    uiCulture="es" culture="es-MX" %>

<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h3 class="page-title">
        Reportes de Comercios SIFCoS
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li><i class="fa fa-home"></i><a href="#">Inicio</a> <i class="fa fa-angle-right"></i>
            </li>
            <li><a href="ReporteComercios.aspx">Reporte de Comercios</a> <i class="fa fa-angle-right">
            </i></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="True"
        EnableScriptLocalization="True">
    </asp:ScriptManager>
    <%-- <asp:UpdatePanel ID="UPRoles" runat="server">
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
                            <i class="fa  fa-file-text-o"></i>Informacion de la boca - <span class="step-title">
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
                            <i class="fa fa-search"></i>Comercios SIFCoS   
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <!-- BEGIN FORM-->
                        <div class="form-body">
                           
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- BEGIN Portlet PORTLET-->
                                    <div class="portlet">
                                        <div class="portlet-body"> 
                                                <form role="form">
                                                
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
                                                        FILTRAR POR FECHA DE VENCIMIENTO</h3>     
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
                                                                        <Ajax:CalendarExtender ID="CalendarExtender2" PopupButtonID="btnCalendarDesde2" runat="server"
                                                                            TargetControlID="txtFechaDesde" Format="dd/MM/yyyy">
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
                                                                        <asp:ImageButton ID="btnCalendarHasta2" runat="server" ImageUrl="~/Resources/calendar_24.png" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFechaHasta" CssClass="form-control mascaraFecha" runat="server"
                                                                            TextMode="DateTime"></asp:TextBox>
                                                                        <Ajax:CalendarExtender ID="CalendarExtender3" PopupButtonID="btnCalendarHasta2" runat="server"
                                                                            TargetControlID="txtFechaHasta" Format="dd/MM/yyyy">
                                                                        </Ajax:CalendarExtender>     
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4 labelModificar">
                                                        <label>
                                                            Ordenar Por :
                                                        </label>
                                                        <br />
                                                        <asp:DropDownList runat="server" ID="ddlOrdenPor" CssClass="form-control" >
                                                            <asp:ListItem Text="" Value="0" />
                                                            <asp:ListItem Text="NRO SIFCOS" Value="NRO_SIFCOS" />
                                                            <asp:ListItem Text="DEBE" Value="DEBE" />
                                                            <asp:ListItem Text="FECHA DE VENCIMIENTO" Value="FECHA_VTO" />
                                                            <asp:ListItem Text="DEPARTAMENTO" Value="DEPARTAMENTO"  />
                                                            <asp:ListItem Text="LOCALIDAD" Value="LOCALIDAD" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                </form> 
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
                                     
                                    <asp:GridView ID="GVResultadoReporte" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                        AutoGenerateColumns="false" DataKeyNames="NRO_SIFCOS" AllowPaging="true" PageSize="50"
                                        OnPageIndexChanging="gvResultadoReporte_PageIndexChanging" OnRowDataBound="gvResultadoReporte_RowDataBound"
                                        OnRowCommand="gvResultado_OnRowCommand">
                                        <Columns>
                                           <%-- <asp:BoundField DataField="CUIT" HeaderText="CUIT"/>--%>
                                            <asp:BoundField DataField="NRO_SIFCOS" HeaderText="NRO_SIFCOS" />
                                            <asp:BoundField DataField="DEBE" HeaderText="DEBE" />
                                            <asp:BoundField DataField="FEC_VTO" HeaderText="FECHA VENCIMIENTO" DataFormatString="{0:D}" />
                                            <asp:BoundField DataField="DEPARTAMENTO" HeaderText="DEPARTAMENTO" />
                                            <asp:BoundField DataField="LOCALIDAD" HeaderText="LOCALIDAD" />
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemStyle CssClass="grilla-columna-accion" />
                                                <ItemTemplate>
                                                    <asp:Button ID="btnContacto" runat="server" Text="Contacto" CommandArgument="<%# Container.DataItemIndex %>"
                                                        CommandName="Contacto" CssClass="btn" ToolTip="Mostrar Contacto"></asp:Button>
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
                                <div id="divContacto" class="col-md-12" style="overflow: scroll;" align="center" runat="server" Visible="False">
                                     <h3>
                                         CONTACTO
                                     </h3>
                                     <asp:GridView ID="GvContacto" runat="server" CssClass="table table-striped"
                                        AutoGenerateColumns="false" DataKeyNames="NRO_SIFCOS" PageSize="50"
                                        OnPageIndexChanging="gvResultadoReporte_PageIndexChanging" OnRowDataBound="gvResultadoReporte_RowDataBound"
                                        OnRowCommand="gvResultado_OnRowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="NRO_SIFCOS" HeaderText="NRO_SIFCOS" Visible="False"/>
                                            <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" />
                                            <asp:BoundField DataField="TEL_PRINCIPAL" HeaderText="TEL PRINCIPAL" />
                                            <asp:BoundField DataField="CELULAR" HeaderText="CELULAR" />
                                            <asp:BoundField DataField="PAG_WEB" HeaderText="PAGINA WEB" />
                                            <asp:BoundField DataField="FACEBOOK" HeaderText="FACEBOOK" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                             </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-2">
                                    <asp:Button runat="server" ID="btnVolverReporte" CssClass="form-control btn" Text="Salir Detalle" OnClick="btnVolverReporte_OnClick"/>
                                    </div>
                                </div>
                            </div>
                             
                             
                                
                            </div>
                        </div>

                        <div class="form-actions">
                            
                        </div>
                        <!-- END FORM-->
                    </div>
                </div>
     <%-- </ContentTemplate>
    </asp:UpdatePanel>  --%>    
    <%-- <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UPRoles">
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
    </asp:UpdateProgress>--%>
    
    
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

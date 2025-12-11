<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"  
CodeBehind="ReporteTotalizados.aspx.cs" Inherits="SIFCOS.ReporteTotalizados" 
uiCulture="es" culture="es-MX"%>
<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
     <h3 class="page-title">
			Reporte de Trámites Generados
			</h3>
            <div class="page-bar">
				<ul class="page-breadcrumb">
					<li>
						<i class="fa fa-home"></i>
						<a href="#">Inicio</a>
						<i class="fa fa-angle-right"></i>
					</li>
					<li>
						<a href="ReporteTotalizados.aspx">Reporte Totalizados</a>
						<i class="fa fa-angle-right"></i>
					</li>
					 
				</ul>
				 
			</div>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="True" EnableScriptLocalization="True">
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
                    
                    <div class="row">
                        <div class="col-md-12 center-block">
                            <h5>FILTRO DE BUSQUEDA</h5>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <span class="help-block">Elegir el tipo de reporte.(Obligatorio)</span>
                            <asp:DropDownList ID="ddlTipoConsulta" runat="server" CssClass="btn default form-control" />
                            
                        </div>
                        <div class="col-md-3">
                            <label>Fecha Desde: </label>
                            <br/>
                            <div class="form-group">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnCalendarDesde" runat="server" ImageUrl="~/Resources/calendar_24.png" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFechaDesde" CssClass="form-control mascaraFecha" runat="server"
                                             MaxLength="10"></asp:TextBox>
                                            <Ajax:CalendarExtender ID="Calendar1" PopupButtonID="btnCalendarDesde" runat="server" TargetControlID="txtFechaDesde"
                                                Format="dd/MM/yyyy">
                                            </Ajax:CalendarExtender>
                                        </td>
                                     </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-3">
                            <label>Fecha Hasta: </label>
                            <br/>
                            <div class="form-group">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnCalendarHasta" runat="server" ImageUrl="~/Resources/calendar_24.png" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFechaHasta" CssClass="form-control mascaraFecha" runat="server"
                                             MaxLength="10"></asp:TextBox>
                                            <Ajax:CalendarExtender ID="CalendarExtender1" PopupButtonID="btnCalendarHasta" runat="server" TargetControlID="txtFechaHasta"
                                                Format="dd/MM/yyyy">
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
                        <div class="col-md-12">
                            <asp:GridView ID="gvResultado" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                AutoGenerateColumns="false"  AllowPaging="False" >
                                <Columns>
                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION" />
                                    <asp:BoundField DataField="CANTIDAD" HeaderText="CANTIDAD" />
                                    
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <h6>
                                <asp:Label runat="server" ID="lblTitulocantRegistros" Text="Cantidad total de trámites: "></asp:Label><asp:Label
                                    runat="server" ID="lblTotalRegistrosGrilla" Text="0"></asp:Label></h6>
                        </div>
                    </div>
                     
                </div>
                <div class="form-actions">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnImprimir" CssClass="btn default form-control" Text="Imprimir"
                                OnClientClick="javascript:window.print();" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnPDF" CssClass="btn default form-control" Text="Guardar en PDF" OnClick="btnPDF_OnClick"/>
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnExcel" CssClass="btn default form-control" Text="Guardar en Excel"  OnClick="btnExcel_OnClick"/>
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
</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
</asp:Content>
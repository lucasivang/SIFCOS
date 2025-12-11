<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="ABMProductos.aspx.cs" Inherits="SIFCOS.ABMProductos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
     <asp:ScriptManager ID="ScriptManager1" runat="server"
            EnablePageMethods = "true">
            </asp:ScriptManager>
    
    <div class="portlet box yellow" id="form_wizard_1">
        <div class="portlet-title">
            <div class="caption">
                <i class="fa fa-file-text-o"></i>Consulta de Productos - <span class="step-title">
                    <asp:Label ID="lblPasos" runat="server" Text=""></asp:Label>
                </span>
            </div>
        </div>
        <div class="portlet-body form">
            <div class="form-body"> 
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <div id="divErrorProd" runat="server" class="alert alert-danger alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                    </button>
                                    <strong>Error! </strong>
                                    <asp:Label runat="server" ID="lblErrorProd"></asp:Label>
                                </div>
                                <div id="divExitoProd" runat="server" class="alert alert-success alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                    </button>
                                    <strong>Exito! </strong>
                                    <asp:Label runat="server" ID="lblExitoProd"></asp:Label>
                                </div>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-4">
                            <label>
                                Ingrese Producto</label>
                            <asp:TextBox runat="server" ID="txtNombreProducto" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnConsultarProducto" CssClass="btn default form-control"
                                Text="Consultar" OnClick="btnConsultarProducto_OnClick" />
                        </div>
                        <%--<div class="col-md-2">
                            <asp:Button runat="server" ID="btnLimpiarFiltros" CssClass="btn default form-control"
                                Text="Limpiar" OnClick="btnLimpiarFiltros_OnClick"></asp:Button>
                        </div>--%>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnAgregarProd" CssClass="btn blue form-control"
                                Text="Agregar Productos" OnClick="btnAgregarProd_OnClick"></asp:Button>
                        </div>
                        
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-md-12" style="overflow: scroll;">
                            <asp:GridView ID="gvResultado" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                DataKeyNames="idProducto" AllowPaging="true" PageSize="10"
                                OnPageIndexChanging="gvResultado_PageIndexChanging" OnRowCommand="gvResultado_OnRowCommand" AutoGenerateColumns="false">
                                 <Columns>
                                    <asp:BoundField DataField="IdProducto" HeaderText="CODIGO" />
                                    <asp:BoundField DataField="NProducto" HeaderText="DESCRIPCION" />
                                <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnModificar" runat="server" Text="Modificar Producto" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="Modificar" CssClass="botonModificarTramite" ToolTip="Modificar Descripcion Producto">
                                            </asp:Button>
                                            <asp:Button ID="btnEliminar" Text="Eliminar Producto" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="Eliminar" CssClass="botonEliminar3" ToolTip="Eliminar Producto">
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
                                    <asp:Label runat="server" ID="titPaginas" Text="Páginas: " Visible="False"></asp:Label>
                                        
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
            </div>
        </div>
     </div>  
    
    <%--MODAL AGREGAR PRODUCTOS--%>
     <div id="modalAgregarProductos" runat="server" class="w3-modal">
                <div class="w3-modal-content w3-animate-top w3-card-8">
                    <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Agregar Productos</h3>
                    </li>
                    <li style="float: right;">
                      <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>
                </ul>
            </header>
                    <div class="w3-container">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12 alert alert-success alert-dismissable">
                                            <label>
                                                En esta sección se agregará productos nuevos.</label>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                             <asp:TextBox ID="txtProducto" runat="server" AutoPostBack="True" Width="100%"
                                                         placeholder="  Ingrese El Producto / Servicio de su comercio">
                                                    </asp:TextBox>
                                       </div>
                                    </div>
                           </div>
                        </div>
                    </div>
             <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnGuardarProd"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnGuardarProd_OnClick"/>
                <asp:Button runat="server" ID="btnCancelarProd"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelarProd_OnClick"/>
            </footer>
                </div>
    </div>
    <%--MODAL MODIFICAR PRODUCTO SELECCIONADO--%>
     <div id="modalModificarProducto" runat="server" class="w3-modal">
                <div class="w3-modal-content w3-animate-top w3-card-8">
                    <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Modificar Descripcion Producto</h3>
                    </li>
                    <li style="float: right;">
                       <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>
                </ul>
            </header>
                    <div class="w3-container">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12 alert alert-success alert-dismissable">
                                       <label>
                                           Sr Administrador, está por modificar la descripcion del producto seleccionado. Para confirmar el cambio
                                           haga click en "Aceptar".</label>
                                     </div>
                                 </div>
                                 <div class="row">
                                   <div class="col-md-6">
                                      <label>
                                         Codigo Producto:</label>
                                      <asp:TextBox ID="txtCodigoProd" Enabled="False" runat="server" CssClass="form-control" TextMode="SingleLine" ></asp:TextBox>
                                  </div>
                                  <div class="col-md-6">
                                      <label>
                                      Descripción:</label>
                                     <asp:TextBox ID="txtNombreProd" Enabled="True" runat="server" CssClass="form-control required" TextMode="SingleLine" MaxLength="50"></asp:TextBox>
                                    <span class="help-block">Ingrese el nombre del producto. (*)</span>
                                  </div>
                                </div>
                                </div>
                            </div>
                        </div>
                   
                    <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnAceptar"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnAceptar_OnClick"/>
                <asp:Button runat="server" ID="btnCancelar"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelar_OnClick"/>
            </footer>
                </div>
    </div>
    <%--MODAL ELIMINAR PRODUCTO SELECCIONADO--%>
     <div id="modalEliminarProducto" runat="server" class="w3-modal">
                <div class="w3-modal-content w3-animate-top w3-card-8">
                    <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Eliminar Producto</h3>
                    </li>
                    <li style="float: right;">
                       <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>
                </ul>
            </header>
                    <div class="w3-container">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12 alert alert-success alert-dismissable">
                                       <label>
                                           Sr Administrador, está por eliminar el producto seleccionado. Para confirmar la eliminación
                                           haga click en "Aceptar".</label>
                                     </div>
                                 </div>
                                 <div class="row">
                                   <div class="col-md-6">
                                      <label>
                                         Codigo Producto:</label>
                                      <asp:TextBox ID="txtCodigoProd2" Enabled="False" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                  </div>
                                  <div class="col-md-6">
                                      <label>
                                      Descripción:</label>
                                     <asp:TextBox ID="txtNombreProd2" Enabled="False" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                  </div>
                                </div>
                                </div>
                            </div>
                        </div>
                   
                    <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="AceptarEliminar"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnAceptarEliminar_OnClick"/>
                <asp:Button runat="server" ID="CancelarEliminar"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelarEliminar_OnClick"/>
            </footer>
                </div>
    </div>
</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
    <script type="text/javascript">

        function ace1_itemSelected(sender, e) {
            $('#ConenedorPrincipal_ace1Value').val(e.get_value());
        }
    </script>
</asp:Content>

<%@ Page Language="C#"  MasterPageFile="~/Principal.Master" AutoEventWireup="true" 
CodeBehind="ProductosYActividades.aspx.cs" Inherits="SIFCOS.ProductosYActividades" %>
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
                <i class="fa fa-file-text-o"></i>Productos Y Actividades<span class="step-title">
                    <asp:Label ID="lblPasos" runat="server" Text=""></asp:Label>
                </span>
            </div>
        </div>
        <div class="portlet-body form">
            <div class="form-body">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12">
                            <div id="modalError" runat="server" class="alert alert-danger alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                    </button>
                                    <strong>Error! </strong>
                                    <asp:Label runat="server" ID="lblErrorProdAct"></asp:Label>
                                </div>
                                <div id="modalExito" runat="server" class="alert alert-success alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                    </button>
                                    <strong>Exito! </strong>
                                    <asp:Label runat="server" ID="lblExitoProdAct"></asp:Label>
                                </div>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-4">
                            <label>
                                CONSULTAR POR:
                            </label>
                            <asp:DropDownList ID="ddlTipoConsulta" runat="server" CssClass="form-control required"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlTipoConsulta_SelectedIndexChanged">
                                            </asp:DropDownList>
                        </div>
                    </div>
                    <br/>
                    <br/>
                    <div id="divProd" runat="server" class="row">
                        <div class="col-md-4">
                            <label>
                                Ingrese Producto</label>
                            <asp:TextBox runat="server" ID="txtNombreProducto" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div id="divAct" runat="server" class="row">
                        <div class="col-md-4">
                            <label>
                                Ingrese Actividad</label>
                            <asp:TextBox runat="server" ID="txtNombreActividad" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnConsultarProducto" CssClass="btn default form-control"
                                Text="Consultar" OnClick="btnConsultarProducto_OnClick" />
                        </div>
                        
                        <div class="col-md-4">
                            <asp:Button runat="server" ID="btnAsociarProd" CssClass="btn blue form-control"
                                Text="Asociar productos a una actividad" OnClick="btnAsociarProd_OnClick"></asp:Button>
                        </div>
                        
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-md-12" style="overflow: scroll;">
                            <asp:GridView ID="gvResultado" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                DataKeyNames="id_producto" AllowPaging="true" PageSize="10" OnRowCommand="gvResultado_OnRowCommand" 
                                OnPageIndexChanging="gvResultado_PageIndexChanging" AutoGenerateColumns="False" PagerSettings-Visible="False">
                                 <Columns>
                                    <asp:BoundField DataField="Id_Producto" HeaderText="CODIGO PRODUCTO" Visible="False" />
                                    <asp:BoundField DataField="N_Producto" HeaderText="PRODUCTO" />
                                    <asp:BoundField DataField="Id_Actividad" HeaderText="CODIGO ACTIVIDAD" Visible="False" />
                                    <asp:BoundField DataField="N_Actividad" HeaderText="ACTIVIDAD" />
                                <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnModificar" runat="server" Text="Cambiar Actividades" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="ModificarRelacion" CssClass="botonModificarTramite" ToolTip="Cambiar las Actividades del Producto seleccionado">
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
   
    <%--MODAL AGREGAR PRODUCTOS ACTIVIDAD--%>
     <div id="modalAgregarProductosActividad" runat="server" class="w3-modal">
                <div class="w3-modal-content w3-animate-top w3-card-8">
                    <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Asociar productos a actividades</h3>
                    </li>
                    <li style="float: right;">
                       <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>
                </ul>
            </header>
                    <div class="w3-container">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                
                                <div id="div4" runat="server">
                                   
                                    <div class="row">
                                        <div class="col-md-12 alert alert-success alert-dismissable">
                                            <label>
                                                En esta sección se podrá asociar productos existentes que no han sido asociados y asociarlos a actividades existentes.</label>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                             <label>
                                                Productos sin actvidades relacionadas:</label>
                                            <asp:DropDownList ID="ddlProductos" runat="server" CssClass="form-control select2me required"
                                                AutoPostBack="False">
                                            </asp:DropDownList>
                                            <span class="help-block">Seleccione un producto. (*) </span>
                                        </div>
                                       
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>
                                                Actividades:</label>
                                            <asp:DropDownList ID="ddlActividades" runat="server" CssClass="form-control select2me required"
                                                 AutoPostBack="False">
                                            </asp:DropDownList>
                                            <span class="help-block">Seleccione una actividad. (*) </span>
                                        </div>
                                   </div>
                                   
                                </div>
                               
                            </div>
                        </div>
                    </div>
                    <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnGuardarProdAct2"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnGuardarProdAct_OnClick"/>
                <asp:Button runat="server" ID="btnCancelarProdAct2"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelarProdAct_OnClick"/>
            </footer>
                </div>
    </div>
    
    <%--MODAL MODIFICAR RELACIONES PRODUCTO SELECCIONADO--%>
     <div id="modalModificarRelacionProd" runat="server" class="w3-modal">
                <div class="w3-modal-content w3-animate-top w3-card-8">
                    <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Cambiar las relaciones del producto seleccionado</h3>
                    </li>
                    <li style="float: right;">
                      <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>
                </ul>
            </header>
                    <div class="w3-container">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                
                                <div id="div2" runat="server">
                                   
                                    <div class="row">
                                        <div class="col-md-12 alert alert-success alert-dismissable">
                                            <label>
                                                En esta sección se podrá cambiar las relaciones del producto seleccionado.Si la relacion esta siendo utiliazda en un tramite será imposible la modificación</label>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                   <div class="col-md-4">
                                      <label>
                                         Codigo Producto:</label>
                                      <asp:TextBox ID="txtCodigoProd" Enabled="False" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                  </div>
                                  <div class="col-md-4">
                                      <label>
                                      Descripción:</label>
                                     <asp:TextBox ID="txtNombreProd" Enabled="False" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                  </div>
                                  <div class="col-md-4">
                                      <asp:Button runat="server" ID="btnAgregarActividad"  class="btn blue form-control" style="margin-bottom: 10px;margin-top: 25px;" Text=" Agregar Actividad" OnClick="btnAgregarActividad_OnClick"/>
                                  </div>
                                        
                                </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblTitAct" runat="server"></asp:Label>
                                               
                                                <br/>
                                                <br/>
                                             <asp:GridView ID="GVActividades" Style="overflow: scroll;" runat="server" CssClass="table table-striped" DataKeyNames="Id_actividad" 
                                               OnPageIndexChanging="gvResultado_PageIndexChanging" AutoGenerateColumns="False"  OnRowCommand="gvActividades_OnRowCommand">
                                                 <Columns>
                                                    <asp:BoundField DataField="Id_Actividad" HeaderText="CODIGO ACTIVIDAD" />
                                                    <asp:BoundField DataField="N_Actividad" HeaderText="DESCRIPCION ACTIVIDAD" />
                                                <asp:TemplateField HeaderText="Acciones">
                                                        <ItemStyle CssClass="grilla-columna-accion" />
                                                        
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRow" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnQuitar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="QuitarRelacion" CssClass="botonEliminar" ToolTip="Quitar Relación">
                                                            </asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                              </asp:GridView>        
                                        </div>
                                   </div>
                                   
                                </div>
                               
                            </div>
                        </div>
                    </div>
                    <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnGuardarRelProdAct"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnGuardarRelProdAct_OnClick"/>
                <asp:Button runat="server" ID="btnCancelarRelProdAct"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnCancelarRelProdAct_OnClick"/>
            </footer>
                </div>
    </div>
    <%--MODAL MODIFICAR RELACIONES PRODUCTO SELECCIONADO--%>
     <div id="modalAgregarActividad" runat="server" class="w3-modal">
                <div class="w3-modal-content w3-animate-top w3-card-8">
                    <header class="w3-container w3-teal">
                <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                    <li style="float: left;">
                        <h3>Agregar Actividad al producto seleccionado</h3>
                    </li>
                    <li style="float: right;">
                       <img style="margin-top: 8px;" src="Resources/LogosNvos/logo_sifcos.png" alt="SIFCoS WEB">
                    </li>
                </ul>
            </header>
                    <div class="w3-container">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                
                                <div id="div3" runat="server">
                                   
                                    <div class="row">
                                        <div class="col-md-12 alert alert-success alert-dismissable">
                                            <label>
                                                En esta sección se podrá cambiar las relaciones del producto seleccionado.Si la relacion esta siendo utiliazda en un tramite será imposible la modificación</label>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                   <div class="col-md-6">
                                      <label>
                                         Codigo Producto:</label>
                                      <asp:TextBox ID="txtCodProdSel" Enabled="False" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                  </div>
                                  <div class="col-md-6">
                                      <label>
                                      Descripción:</label>
                                     <asp:TextBox ID="txtDesProdSel" Enabled="False" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                                  </div>
                                        
                                </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>
                                                Actividades:</label>
                                            <asp:DropDownList ID="ddlAct" runat="server" CssClass="form-control select2me required"
                                                 AutoPostBack="False">
                                            </asp:DropDownList>
                                            <span class="help-block">Seleccione una actividad. (*) </span>        
                                        </div>
                                   </div>
                                   
                                </div>
                               
                            </div>
                        </div>
                    </div>
                    <footer class="w3-container w3-teal">
                <asp:Button runat="server" ID="btnAgregar"  class="btn btnBuscar  btn-circle" style="margin-bottom: 10px;margin-top: 10px;" Text="Aceptar" OnClick="btnAgregar_OnClick"/>
                <asp:Button runat="server" ID="btnNoAgregar"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Cancelar" OnClick="btnNoAgregar_OnClick"/>
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
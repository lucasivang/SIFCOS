<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
 CodeBehind="ABMSuperficie.aspx.cs" Inherits="SIFCOS.ABMSuperficie" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
    <div id="divMensajeError" runat="server" class="alert alert-danger alert-dismissable">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
	    <strong>Error! </strong> <asp:Label runat="server" ID="lblMensajeError"></asp:Label>
    </div>
     <div id="divMensajeExito" runat="server" class="alert alert-success alert-dismissable">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true"></button>
	    <strong>Exito! </strong> <asp:Label runat="server" ID="lblMensajeExito"></asp:Label>
    </div>
    <%--SECCION DE CONSULTA--%>
    <div id="divPantallaConsulta" runat="server">
        <div class="portlet box yellow">
		<div class="portlet-title">
			<div class="caption">
				 <i class="fa fa-search"></i>Consultar Superficies - <span class="step-title">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </span>
			</div>
			<div class="actions">
			    <asp:Button runat="server" ID="btnNuevo" CssClass="btn  form-control botonSiguiente" Text="+ Nuevo" OnClick="btnNuevo_OnClick"/>
			</div>
		</div>
		<div class="portlet-body form">
			<!-- BEGIN FORM-->
				<div class="form-body">
				    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                            <label>Ingrese palabra de busqueda</label> 
                            <asp:TextBox ID="txtFiltroProd" runat="server"  CssClass="form-control"  ></asp:TextBox>
                            </div>
                        </div>
                                    
                    </div>
                    
				</div>
				<div class="form-actions">
					<div class="row">
						<div class="col-md-2">
							<asp:Button runat="server" ID="btnConsultar" CssClass="btn default form-control"  Text="Consultar" OnClick="btnConsultar_OnClick"/>
						</div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnLimpiarFiltros" CssClass="btn default form-control" Text="Limpiar Consulta" OnClick="btnLimpiarFiltros_OnClick" ></asp:Button>
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
                       <div class="col-md-12" >
                            <asp:GridView ID="gvResultado" style="overflow:scroll;" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="IdTipoSuperficie"
                            AllowPaging="true" PageSize="10"  OnPageIndexChanging="gvResultado_PageIndexChanging" OnRowDataBound="gvResultado_RowDataBound" OnRowCommand="gvResultado_OnRowCommand">
                            <Columns>
                                <asp:BoundField DataField="IdTipoSuperficie" HeaderText="Id_superficie" />
                                <asp:BoundField DataField="NTipoSuperficie" HeaderText="Descripcion" />
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemStyle CssClass="grilla-columna-accion"/>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="Editar" CssClass="botonEditar" >
																</asp:Button>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar">
                                    <ItemStyle CssClass="grilla-columna-accion"/>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                            CommandName="Eliminar" CssClass="botonEliminar"   />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
        
                </asp:GridView>
                        </div>
                    </div> 
                <div class="row">
                      <div class="col-md-12"  >
                     <h6><asp:Label runat="server" ID="lblTitulocantRegistros" Text="Total de registros encontrados : "></asp:Label><asp:Label runat="server" ID="lblTotalRegistrosGrilla" Text="0"></asp:Label></h6>           
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12"  >
                        
                        <ul class="pagination pull-right">
                            <li ><h6>Páginas :</h6></li>    
                            <asp:Repeater ID="rptBotonesPaginacion" OnItemDataBound="rptBotonesPaginacion_OnItemDataBound" OnItemCommand="rptBotonesPaginacion_OnItemCommand" runat="server"   >
                                <ItemTemplate>
                                    <li class="paginate_button">
                                    <asp:LinkButton ID="btnNroPagina" OnClick="btnNroPagina_OnClick" CommandArgument='<%# Bind("nroPagina") %>' runat="server" class="btn btn-default " ><%# Eval("nroPagina")%></asp:LinkButton>
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
                            <asp:Button runat="server" ID="btnImprimir" CssClass="btn default form-control" Text="Imprimir"/>
						</div>
					</div>
				</div>
			<!-- END FORM-->
		</div>
	    </div>
    </div>
    <%--SECCION DE ALTA--%>
  <div id="divPantallaABM" runat="server">
   <div class="portlet box yellow">
        <div class="portlet-title">
            <div class="caption">
                <i class="fa fa-file-text-o"></i><asp:Label runat="server" ID="lblTituloPantallaABM"></asp:Label> <span class="step-title">
                    
                </span>
            </div>
        </div>
        <div class="portlet-body form">
            <div class="form-body">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>
                                Codigo Superficie</label>
                            <asp:TextBox runat="server" ID="txtIdSuperficie" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-7">
                            <label>
                                Superficie</label>
                            <asp:TextBox ID="txtSuperficie" CssClass="form-control required" runat="server">
                            </asp:TextBox>
                        </div>
                       </div>
                </div>
            </div>
        </div>
        <div class="modal fade bs-modal-sm" id="small" tabindex="-1" role="dialog" aria-hidden="true" style="display: none;">
								<div class="modal-dialog modal-sm">
									<div class="modal-content">
										<div class="modal-header">
											<button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
											<h4 class="modal-title">Informacion</h4>
										</div>
										<div class="modal-body">
											 ¿Esta Seguro de guardar?
										</div>
										<div class="modal-footer">
											<button type="button" class="btn default" data-dismiss="modal">Cancelar</button>
											<button type="button" class="btn yellow">Guardar cambios</button>
										</div>
									</div>
									<!-- /.modal-content -->
								</div>
								<!-- /.modal-dialog -->
		</div>
        <div class="form-actions">
            <br />
            <div class="row">
                <div class="col-md-offset-2 col-md-12">
                     <asp:Button runat="server" ID="btnSalir" CssClass="btn botonSiguiente btn-circle" Text="Salir" OnClick="btnSalir_OnClick" />
                    <a class="btn botonSiguiente btn-circle" data-toggle="modal" href="#small">Guardar</a>
                    
                </div>
            </div>
            <br />
        </div>
 <%-- <div class="modalGuardar" id="DivModalGuardar">
            <div class="modal-dialog modal-sm">
				<div class="modal-content">
						<div class="modal-header">
						<h4 class="modal-title" style="text-align: center">GUARDAR REGISTRO</h4>
						</div>
				<div class="modal-body" style="text-align: center">
				 ¿Esta seguro de guardar?
				</div>
				<div class="modal-footer">
					<button class="btn botonAtras btn-circle" onclick="javascript:ocultarVentana();">Cancelar</button>
                    <asp:Button runat="server" ID="btnGuardar" CssClass="btn botonSiguiente btn-circle" Text="Guardar" OnClick="btnGuardar_OnClick" />
				</div>
				</div>
				<!-- /.modal-content -->
			</div>
            </div>--%>
				<!-- /.modal-dialog -->
		</div>
       <%-- <div class="modal-sm" id="DivModalFracaso" runat="server" Visible="false">
			<div class="modal-dialog modal-sm">
				<div class="modal-content">
						<div class="modal-header">
						<h4 class="modal-title" style="text-align: center">GUARDAR REGISTRO</h4>
						</div>
				<div class="modal-body" style="text-align: center">
				 ERROR EN EL GUARDADO DEL REGISTRO.
				</div>
				<div class="modal-footer">
					<asp:Button runat="server" ID="btnAceptar1" CssClass="btn botonSiguiente btn-circle" Text="Guardar" OnClick="btnAceptar_OnClick" />
                    
				</div>
				</div>
				<!-- /.modal-content -->
			</div>
				<!-- /.modal-dialog -->
		</div>--%>
      <%--  <div class="modal-sm" id="DivModalExito" runat="server" Visible="false">
			<div class="modal-dialog modal-sm">
				<div class="modal-content">
						<div class="modal-header">
						<h4 class="modal-title" style="text-align: center">GUARDAR REGISTRO</h4>
						</div>
				<div class="modal-body" style="text-align: center">
				 Se realizó el registro con éxito.
				</div>
				<div class="modal-footer">
					<asp:Button runat="server" ID="btnAceptar2" CssClass="btn botonSiguiente btn-circle" Text="Guardar" OnClick="btnAceptar_OnClick" />
				</div>
				</div>
				<!-- /.modal-content -->
			</div>
				<!-- /.modal-dialog -->
		</div>--%>
    </div>
 
    
</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
      
</asp:Content>

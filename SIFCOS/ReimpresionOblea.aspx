<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" 
CodeBehind="ReimpresionOblea.aspx.cs" Inherits="SIFCOS.ReimpresionOblea" 
uiCulture="es" culture="es-MX"%>
<%@ Register TagPrefix="ajx" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
     <h3 class="page-title">
			Trámite de Reimpresión de Oblea y/o Certificados en SIFCoS.
			</h3>
            <div class="page-bar">
				<ul class="page-breadcrumb">
					<li>
						<i class="fa fa-home"></i>
						<a href="#">Inicio</a>
						<i class="fa fa-angle-right"></i>
					</li>
					<li>
						<a href="ReimpresionOblea.aspx">Reimpresión de Oblea</a>
						<i class="fa fa-angle-right"></i>
					</li>
					 
				</ul>
				 
			</div>
             
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="True" EnableScriptLocalization="True">
    </asp:ScriptManager>
     <div class="portlet box yellow">
		<div class="portlet-title">
			<div class="caption">
				 <i class="fa fa-search"></i> Comercio
			</div>
			 
		</div>
		<div class="portlet-body form">
			<!-- BEGIN FORM-->
				<div class="form-body">
				    <div id="divMensajeErrorReimpresion" runat="server" class="alert alert-danger alert-dismissable">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                </button>
                                <strong>Error! </strong>
                                <asp:Label runat="server" ID="lblMensajeError"></asp:Label>
                            </div>
                            <div id="divMensajeExitoReimpresion" runat="server" class="alert alert-success alert-dismissable">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                </button>
                                <strong>Éxito! </strong>
                                <asp:Label runat="server" ID="lblMensajeExito"></asp:Label>
                            </div>
                    <div class="row">
                                <div class="col-md-4">
                                    <label>
                                        Documento que solicita:</label>
                                    <asp:DropDownList ID="ddlTipoDoc" runat="server" CssClass="form-control required"
                                         AutoPostBack="False" >
                                    </asp:DropDownList>
                                    <span class="help-block">Seleccione el Documento. (*) </span>
                            </div> 
                    </div>
                    <br/>
                    <div class="row">
                                <div class="col-md-4">
                                    <label>
                                        Tipo:</label>
                                    <asp:DropDownList ID="ddlTipoTramite" runat="server" CssClass="form-control required"
                                         OnSelectedIndexChanged="ddlTipoTramite_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <span class="help-block">Seleccione el Tipo de Trámite. (*) </span>
                            </div> 
                    </div>
                    <br/>        
                    <div class="row">
                        <div class="col-md-2">
                            <label>CUIT del Comercio :</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtCuit" MaxLength="11"></asp:TextBox>
                            <ajx:FilteredTextBoxExtender ID="txtCuit_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtCuit">
                                                </ajx:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-2" id="div_nro_sifcos" runat="server" Visible="False">
                            <label>Nro SIFCoS :</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNroSifcos" MaxLength="11" ></asp:TextBox>
                            <ajx:FilteredTextBoxExtender ID="txtNroSifcos_FilteredTextBoxExtender1" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtNroSifcos">
                                                </ajx:FilteredTextBoxExtender>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                         <div class="col-md-2">
                            <asp:Button ID="btnConsultar" Text="Consultar Comercio" runat="server"  CssClass="form-control btn blue" OnClick="btnConsultar_OnClick"/>
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnLimpiar" Text="Limpiar" CssClass="form-control btn default" OnClick="btnLimpiar_OnClick"/>
                        </div>
                        
                    </div>
                    <br />
                    <div id="divResultadoEstadoComercio" runat="server" class="note note-info">
								<h4 class="block">Información</h4>
								<p>
									 <asp:Label runat="server" ID="lblResultadoEstadoComercio"></asp:Label>
								</p>
							</div>
                    <div class="row" id="TITULO" runat="server">
                                <div class="col-md-12" style="text-align: center">
                                    <h4>SUCURSALES DEL COMERCIO:</h4>
                                    
                            </div> 
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvResultado" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                AutoGenerateColumns="false"  AllowPaging="true" PageSize="5" DataKeyNames="Nro_Sifcos" 
                                OnRowCommand="gvResultado_OnRowCommand" OnPageIndexChanging="gvResultado_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="Nro_Sifcos" HeaderText="Nro Sifcos"  />
                                    <asp:BoundField DataField="Nro_Tramite" HeaderText="Nro Ultimo Tramite" />
                                    <asp:BoundField DataField="domicilio" HeaderText="Domicilio" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnSeleccion" runat="server" Text="Ver" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="Seleccion" CssClass="botonAsignar" ToolTip="Seleccionar Sede"></asp:Button>
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
                    <div class="row" id="TituloRazonSocial" runat="server" Visible="False">
                        <div class="col-md-12">
                            <h4>Razon Social: </h4>
                            <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="tituloTramSeleccionado" runat="server" Visible="False">
                        <div class="col-md-12">
                            <h4>Sucursal Seleccionada:</h4>
                          
                        </div>
                    </div>
                    <div class="row" id="tituloTramEncontrada" runat="server" Visible="False">
                        <div class="col-md-12">
                            <h4>Sucursal Encontrada:</h4>
                        
                        </div>
                    </div>
                    <div class="row" id="seleccionDom" runat="server" Visible="False">
                        <div class="col-md-4">
                            <label>Nro. último trámite de la sucursal :</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtUltTramite" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                            <label>domicilio de la sucursal :</label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtDomicilio" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <br/>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GVtrs" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                                AutoGenerateColumns="false"  AllowPaging="False" >
                                <Columns>
                                    <asp:BoundField DataField="NroTransaccion" HeaderText="Nro Tasa" />
                                    <asp:BoundField DataField="Concepto" HeaderText="Concepto" />
                                    <asp:BoundField DataField="Importe" HeaderText="Importe" />
                                     <asp:TemplateField HeaderText="Descargar">
                                        <ItemStyle CssClass="grilla-columna-accion" />
                                        <ItemTemplate>
                                            <a href='<%# Eval("Link") %>' target="_blank">Descargar</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                     
                     
                </div>
                <div class="form-actions">
                   <div class="row">
                         <div class="col-md-4">
                            <asp:Button ID="btnReimpresion" Text="Solicitar Reimpresion de Oblea/Certificado" runat="server"  CssClass="form-control btn red" OnClick="btnReimpresion_OnClick"/>
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnCancelar" Text="SALIR" CssClass="form-control btn default" OnClick="btnCancelar_OnClick"/>
                        </div>
                    </div>
                </div>
			<!-- END FORM-->
		</div>
	</div>

</asp:Content>

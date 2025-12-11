<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" 
CodeBehind="CargarExcentoEnRentas.aspx.cs" Inherits="SIFCOS.CargarExcentoEnRentas" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
     <h3 class="page-title">
			Cargar Exento en Rentas.
			</h3>
            <div class="page-bar">
				<ul class="page-breadcrumb">
					<li>
						<i class="fa fa-home"></i>
						<a href="#">Inicio</a>
						<i class="fa fa-angle-right"></i>
					</li>
					<li>
						<a href="CargarExcentoEnRentas.aspx.aspx">Cargar Exento</a>
						<i class="fa fa-angle-right"></i>
					</li>
					 
				</ul>
				 
			</div>
             
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
     
     <div class="portlet box yellow">
		<div class="portlet-title">
			<div class="caption">
				 <i class="fa fa-search"></i> Cargar Exento en Rentas
			</div>
			 
		</div>
		<div class="portlet-body form">
			<!-- BEGIN FORM-->
				<div class="form-body">
				     <div id="divMensajeExito" runat="server" class="alert alert-success alert-dismissable">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                </button>
                                <strong>Éxito! </strong>
                                <asp:Label runat="server" ID="lblMensajeExito"></asp:Label>
                            </div>
                              <div id="divMensajeError" runat="server" class="alert alert-danger alert-dismissable">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                </button>
                                <strong>Error! </strong>
                                <asp:Label runat="server" ID="lblMensajeError"></asp:Label>
                            </div>
				    <div class="row">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="alert alert-block alert-info fade in">
                                            <h4 class="alert-heading">
                                                <i class="fa fa-info-circle"></i>Información</h4>
                                            <p>
                                                Ésta función permite cargar en base de datos de gobierno una entidad con los datos básicos. ES IMPORTANTE CORROBORAR LOS DATOS A GUARDAR YA QUE SE DIFICULTA EN UN FUTURO REVERTIR ALGUNA ENTIDAD.
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                    <div class="row">
                        <div class="form-group">
                        <div class="col-md-3">
                            <label>Ingrese CUIT :</label>
                            <asp:TextBox ID="txtCuit"  runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        
                        <div class="col-md-5">
                            <label>Ingrese RAZÓN SOCIAL :</label>
                            <asp:TextBox ID="txtRazonSocial"  runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-4">
                            <label>Ingrese NOMBRE DE FANTASÍA :</label>
                            <asp:TextBox ID="txtNombreFantasia"  runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        </div>
                        
                    </div>
                     
                    <br/>
                </div>
                <div class="form-actions">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button ID="btnGuardar" Text="Guardar"  runat="server" OnClick="btnGuardar_OnClick" CssClass="btn default form-control"></asp:Button>
                            <asp:Button runat="server" ID="btnVolver" CssClass="btn default form-control" Text="Salir"
                                OnClick="btnVolver_OnClick" />
                        </div>
                    </div>
                </div>
			<!-- END FORM-->
		</div>
	</div>

</asp:Content>

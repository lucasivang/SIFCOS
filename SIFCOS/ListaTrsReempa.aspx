<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="ListaTrsReempa.aspx.cs" Inherits="SIFCOS.ListaTrsReempa" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
     <h3 class="page-title">
			Listado de TRS generadas para descargar.
			</h3>
            <div class="page-bar">
				<ul class="page-breadcrumb">
					<li>
						<i class="fa fa-home"></i>
						<a href="#">Inicio</a>
						<i class="fa fa-angle-right"></i>
					</li>
					<li>
						<a href="ListaTrsReempa.aspx">Listado de TRS</a>
						<i class="fa fa-angle-right"></i>
					</li>
					 
				</ul>
				 
			</div>
             
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
     
     <div class="portlet box yellow">
		<div class="portlet-title">
			<div class="caption">
				 <i class="fa fa-search"></i> Descargar e Imprimir Tasas
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
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnVolver" CssClass="btn default form-control" Text="Finalizar y Volver"
                                OnClick="btnVolver_OnClick" />
                        </div>
                    </div>
                </div>
			<!-- END FORM-->
		</div>
	</div>

</asp:Content>

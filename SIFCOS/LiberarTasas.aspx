<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master"
    CodeBehind="LiberarTasas.aspx.cs" Inherits="SIFCOS.LiberarTasas" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h3 class="page-title">
        Gestión de Tasas.
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li><i class="fa fa-home"></i><a href="#">Inicio</a> <i class="fa fa-angle-right"></i>
            </li>
            <li><a href="LiberarTasas.aspx">Gestión de Tasas</a> <i class="fa fa-angle-right"></i>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
    <div class="portlet box yellow">
        <div class="portlet-title">
            <div class="caption">
                <i class="fa fa-search"></i>Liberar Tasas
            </div>
        </div>
        <div class="portlet-body form">
            <!-- BEGIN FORM-->
            <div class="form-body">
                <h3 class="form-section">
                                            <i class="fa fa-edit"></i> Liberar Tasas del VIEJO SIFCoS.
                </h3>
                <div class="row">
                    <div class="form-group">
                        <div class="col-md-4">
                            <label>
                                Ingrese Nro de Referencia/Liquidación</label>
                            <asp:TextBox ID="txtNroReferencia" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label style="color: white">
                                .</label>
                            <asp:Button ID="btnConsultarTasa" Text="Consultar Tasa" runat="server" OnClick="btnConsultarTasa_OnClick"
                                CssClass="form-control btn blue"></asp:Button>
                        </div>
                        <div class="col-md-4">
                            <label style="color: white">
                                .</label>
                            <asp:Button ID="btnLiberarTasa" Text="Liberar Tasa" ToolTip="Libera la Tasa ingresada según el Nro de Referencia/Liquidación."
                                runat="server" OnClick="btnLiberarTasa_OnClick" CssClass="form-control btn btn-danger">
                            </asp:Button>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <div class="col-md-4">
                            <label>
                                Ingrese CUIT</label>
                            <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label style="color: white">
                                .</label>
                            <asp:Button ID="btnConsultarTasaPorCuit" Text="Consultar Tasa Por Cuit" runat="server"
                                OnClick="btnConsultarTasaPorCuit_OnClick" CssClass="form-control btn blue"></asp:Button>
                        </div>
                    </div>
                </div>
               <h3 class="form-section">
                                            <i class="fa fa-edit"></i> Liberar Tasas del NUEVO SIFCoS.
                </h3>
                <br/>
                <div class="row">
                    <div class="form-group">
                        <div class="col-md-4">
                            <label>
                                Ingrese Nro de Liquidación: </label>
                            <asp:TextBox ID="txtNroReferenciaNS" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label style="color: white">.</label>
                            <asp:Button ID="btnConsultarTasaNS" Text="Consultar Tasa en nuevo SIFCoS" runat="server" OnClick="btnConsultarTasaNS_OnClick"
                                CssClass="form-control btn blue"></asp:Button>
                        </div>
                        <div class="col-md-4">
                            <label style="color: white">
                                .</label>
                            <asp:Button ID="btnLiberarTasaNS" Text="Liberar Tasa" ToolTip="Libera la Tasa ingresada del nuevo SIFCoS."
                                runat="server" OnClick="btnLiberarTasaNS_OnClick" CssClass="form-control btn btn-danger">
                            </asp:Button>
                        </div>
                    </div>
                </div>
                <br />
                <br/>
                    <div class="row">
                          <div class="form-group">
                        <div class="col-md-12">
                            <label>Resultado de la acción</label>
                            <asp:TextBox ID="txtResultado" BackColor="Green" Enabled="False" runat="server" style="color: white;" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    </div>
                <br/>
                <div class="row">
                    <div class="col-md-12" style="overflow: scroll;">
                        <asp:GridView ID="gvResultado" Style="overflow: scroll;" runat="server" CssClass="table table-striped"
                            AutoGenerateColumns="false" AllowPaging="False">
                            <Columns>
                                <asp:BoundField DataField="OBLIGACION" HeaderText="OBLIGACION" />
                                <asp:BoundField DataField="NROLIQUIDACIONORIGINAL" HeaderText="NROLIQUIDACIONORIGINAL" />
                                <asp:BoundField DataField="CUIT" HeaderText="CUIT" />
                                <asp:BoundField DataField="N_CONCEPTO" HeaderText="CONCEPTO" />
                                <asp:BoundField DataField="FECHA_VENCIMIENTO" HeaderText="FECHA_VENCIMIENTO" />
                                <asp:BoundField DataField="FECHA_COBRO" HeaderText="FECHA_COBRO" />
                                <asp:BoundField DataField="IMPORTE_TOTAL" HeaderText="MONTO" />
                                <asp:BoundField DataField="PAGADO" HeaderText="PAGADO" />
                                <asp:BoundField DataField="ENTE_RECAUDADOR" HeaderText="ENTE_RECAUDADOR" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="form-actions">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Button runat="server" ID="btnVolver" CssClass="btn default form-control" Text="Salir"
                            OnClick="btnVolver_OnClick" />
                    </div>
                </div>
            </div>
            <!-- END FORM-->
        </div>
    </div>
</asp:Content>

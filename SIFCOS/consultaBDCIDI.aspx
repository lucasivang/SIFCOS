<%@ Page Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true"
    CodeBehind="consultaBDCIDI.aspx.cs" Inherits="SIFCOS.consultaBDCIDI" UICulture="es"
    Culture="es-MX" %>

<%@ Register Assembly="BotonUnClick" Namespace="BotonUnClick" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <%--<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyCuxWCu971i-L-O1ui-jzI72cX1Tjr1kwU&v=3.exp&sensor=false&libraries=places"></script>--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
    <div class="portlet-body form">
        <!-- BEGIN FORM-->
        <div class="form-body">
            <div class="row">
                <div class="col-md-12" style="text-align: center">
                    <h3 class="form-section">
                        BASE DE DATOS
                    </h3>
                </div>
            </div>
            
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="form-actions">
                    <asp:ScriptManager runat="server" ID="ScriptManager1">
                    </asp:ScriptManager>
                     <div class="row">
                    <div class="col-md-12">
                        <h4 class="form-section">
                            <i class="fa fa-arrow-circle-right"></i>CONSULTAS A BASE
                        </h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>
                            SELECT</label>
                        <asp:TextBox runat="server" ID="txtSELECT" CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
                <br />
                 <div class="row">
                    <div class="col-md-12">
                        <label>
                            FROM</label>
                        <asp:TextBox runat="server" ID="txtFROM" CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
                <br />
                 <div class="row">
                    <div class="col-md-12">
                        <label>
                            WHERE</label>
                        <asp:TextBox runat="server" ID="txtWHERE" CssClass="form-control">
                        </asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-4">
                        <asp:Button ID="btnConsultaSQL" class="btn   btn-circle" Text="CONSULTAR" runat="server"
                            OnClick="btnConsultaSQL_OnClick"></asp:Button>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12" style="overflow: scroll;">
                        <label>
                            RESULTADO</label>
                        <asp:GridView ID="GVResultadoSQL" CssClass="table table-striped table-bordered table-advance table-hover"
                            runat="server" AutoGenerateColumns="True">
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <h6>
                            <asp:Label runat="server" ID="lblRegistrosSQL" Text="Total de registros encontrados : "></asp:Label><asp:Label
                                runat="server" ID="lblCantRegistrosSQL" Text="0"></asp:Label></h6>
                    </div>
                </div>
                <br/>
                <br/>

                <div class="row">
                    <div class="col-md-4">
                        <asp:Button ID="btnLimpiarSQL" class="btn   btn-circle" Text="Limpiar consulta" runat="server"
                            OnClick="btnLimpiarSQL_OnClick"></asp:Button>
                    </div>
                </div>
                  
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- END FORM-->
    </div>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/Principal_SinLogin.Master" AutoEventWireup="true"
    CodeBehind="Geolocalizacion.aspx.cs" Inherits="SIFCOS.Geolocalizacion" 
    uiCulture="es" culture="es-MX" %>

<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
   
    <h3 class="page-title">
        Trámite SIFCoS
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li><i class="fa fa-home"></i><a href="#">Inicio</a> <i class="fa fa-angle-right"></i>
            </li>
            <li><a href="Geolocalizacion.aspx">Geolocalización</a> <i class="fa fa-angle-right">
            </i></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="ContenedorPrincipal">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True">
    </asp:ScriptManager>
    <div id="barraProgreso" style="display: none;">
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
    </div>
    <%--SECCION INFORMACION BOCA--%>
   <%-- <div id="divPantallaInfoBoca" runat="server">
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
    </div>--%>
    <div id="divVentanaPrincipal" runat="server" class="row">
        <div class="col-md-12">
            <div class="portlet box yellow">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-home"></i>Geolocalización
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-body">
                        <div id="divMensajeError" runat="server" class="alert alert-danger alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Error! </strong>
                            <asp:Label runat="server" ID="lblMensajeError"></asp:Label>
                        </div>
                        <div id="divMensajeExito" runat="server" class="alert alert-success alert-dismissable">
                            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                            </button>
                            <strong>Éxito! </strong>
                            <asp:Label runat="server" ID="lblMensajeExito"></asp:Label>
                        </div>
                        <div class="row" style="color: white">
                            <div class="col-md-2">
                                <asp:Label runat="server" ID="lblIdsGeolocalizacion"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Departamento :</label>
                                    <asp:DropDownList ID="ddlDeptos" CssClass="form-control select2me" runat="server"
                                        OnSelectedIndexChanged="ddlDeptos_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>
                                        Localidad :</label>
                                    <asp:DropDownList ID="ddlLocalidad" CssClass="form-control select2me" runat="server"
                                        AutoPostBack="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-6">
                                    <label>
                                        Filtrar por Producto: 
                                    </label>
                                    <asp:TextBox ID="txtBuscarProducto" runat="server" AutoPostBack="False" Width="100%"
                                        CssClass="form-control" placeholder="  Ingrese El Producto / Rubro de su comercio">
                                    </asp:TextBox>
                                    <asp:Label ID="lblProdSel" Text="" runat="server" ></asp:Label>
                                    <Ajax:AutoCompleteExtender ServiceMethod="BuscarProducto" CompletionInterval="100"
                                        EnableCaching="false" CompletionSetCount="10" TargetControlID="txtBuscarProducto"
                                        ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" MinimumPrefixLength="3"
                                        OnClientItemSelected="ace1_itemSelected">
                                    </Ajax:AutoCompleteExtender>
                                    <asp:HiddenField ID="ace1Value" runat="server" />
                                </div>
                        </div>
                    </div>
                        <br/>
                        <br/>
                        <div runat="server" id="divResultado">
                             <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box yellow" id="form_wizard_4">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <span id="lblDatosEstablecimiento">RESULTADOS DE COMERCIOS ENCONTRADOS</span>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-wizard">
                                        <div class="form-body">
                                            <div class="row">
                                                <div class="col-md-4" runat="server" id="botonGEO" Visible="False">
                                                    <button id="probarMapa" type="button" class="btn btn-primary" style="width: 100%">
                                                        <img src="Img/localizacion-32.png" style="height: 100%; margin-bottom: 2px;" />
                                                        GEOLOCALIZACIÓN
                                                    </button>

                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12" style="overflow: auto">
                                                    <asp:GridView ID="gvResultado" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                        AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="ID_ENTIDAD" PageSize="20" OnPageIndexChanging="gvResultado_PageIndexChanging"
                                                        OnRowCommand="gvResultado_OnRowCommand">
                                                        <Columns>
                                                            <asp:BoundField DataField="razon_social" HeaderText="Razon Social" />
                                                            <asp:BoundField DataField="cuit" HeaderText="Cuit" />
                                                            <asp:BoundField DataField="domicilio" HeaderText="domicilio" />
                                                            
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <ul class="pagination pull-right">
                                                        <li>
                                                            <h6>Páginas :</h6>
                                                        </li>
                                                        <asp:Repeater ID="rptBotonesPaginacion" OnItemDataBound="rptBotonesPaginacion_OnItemDataBound"
                                                            OnItemCommand="rptBotonesPaginacion_OnItemCommand" runat="server">
                                                            <ItemTemplate>
                                                                <li class="paginate_button">
                                                                    <asp:LinkButton ID="btnNroPagina" OnClick="btnNroPagina_OnClick" CommandArgument='<%# Bind("nroPagina") %>'
                                                                        runat="server" CssClass="btn btn-default "><%# Eval("nroPagina")%></asp:LinkButton>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h6>
                                                        <asp:Label runat="server" ID="lblTituloTotal" Text="Total de registros encontrados : "></asp:Label><asp:Label
                                                            runat="server" ID="lblTotal" Text="0"></asp:Label></h6>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                        </div>
                   
                    <br />
                    <div class="form-actions" style="background-color: gainsboro">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Button runat="server" ID="btnConsultar" CssClass="btn default form-control"
                                    OnClick="Consultar_OnClick" Text="Consultar" />
                            </div>
                            <div class="col-md-4">
                                <asp:Button runat="server" ID="btnLimpiar" CssClass="btn default form-control"
                                            OnClick="btnLimpiar_OnClick" Text="Nueva Consulta" />
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">
    <script src="Scripts/markerclusterer.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //  inicializarMapa();

            $('#probarMapa').click(function () {
                var myWindow = window.open();
                var ids = document.getElementById("ContenedorPrincipal_lblIdsGeolocalizacion").innerText;
                var url = "https://maaysp-ws.cba.gov.ar/industria/comercios?map=13";

                myWindow.document.write('<title>:: SIFCOS - Gobierno de Córdoba ::</title>');

                myWindow.document.write('<style>body{margin:0px; overflow:hidden; width:100%; height:100%;}</style>');
                //utilizo el atributo name para pasar los ids como parámetro
                // myWindow.document.write('<iframe style="width:100%; height:100%" name="50" src="https://maaysp-ws.cba.gov.ar/industria/industrias?map=13"></iframe>');

                myWindow.document.write('<iframe style="width:100%; height:100%" name="' + ids + '" src="' + url + '"></iframe>');
                myWindow.document.close();

            });


        });
    </script>
    <script type="text/javascript">

        $("#ContenedorPrincipal_txtBuscarProducto").keyup(function () {
            $("#ContenedorPrincipal_ace1Value").val("");
            $('#<%= lblProdSel.ClientID %>').text("Ningún producto seleccionado");

        });

        $("#ContenedorPrincipal_txtBuscarRubro").keyup(function () {
            $("#ContenedorPrincipal_aceRubroValue").val("");
        });

        function ace1_itemSelected(sender, e) {
            $('#ContenedorPrincipal_ace1Value').val(e.get_value());
            $('#<%= lblProdSel.ClientID %>').text("Producto Seleccionado: " + e.get_text());
        }
        function acerubro_itemSelected(sender, e) {
            //$('#ContenedorPrincipal_aceRubroValue').innerText=e.get_value();
        }
        function capturarf5(e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 116) {
                return false;
            }
            return true;
        }

        
    </script>
   
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="PanelControl.aspx.cs" Inherits="SIFCOS.PanelControl" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
     <h3 class="page-title">
			Panel de Control
			</h3>
            <div class="page-bar">
				<ul class="page-breadcrumb">
					<li>
						<i class="fa fa-home"></i>
						<a href="#">Inicio</a>
						<i class="fa fa-angle-right"></i>
					</li>
					<li>
						<a href="PanelControl.aspx">Panel de Control</a>
						<i class="fa fa-angle-right"></i>
					</li>
					 
				</ul>
				 
			</div>
  
  <link href="metronic/assets/global/plugins/jtable/themes/lightcolor/orange/jtable.min.css" rel="stylesheet" type="text/css" />        
    <style type="text/css">
  a:link {     color: green; } a:hover {     color: red; } a:visited {     color: black; }
  </style>
</asp:Content>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorPrincipal" runat="server">
    <%--MODAL PAGINA CONSTRUCCION--%>
<div id="ModalPaginaConstruccion" runat="server" class="w3-modal">
    <div class="w3-modal-content w3-animate-top w3-card-8">
        <header class="w3-container w3-teal">
            <ul style="list-style-type: none;margin: 0;padding: 0;overflow: hidden;">
                <li style="float: left;">
                    <h3>DISCULPE LAS MOLESTIAS OCACIONADAS...</h3>
                </li>
                
            </ul>
        </header>
        <div class="w3-container">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <img src="Img/Pag_Const.jpg" alt="SIFCoS WEB" style="width:100%"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <footer class="w3-container w3-teal">
            <asp:Button runat="server" ID="btnPagConstruccion"  class="btn btn-circle default" style="margin-bottom: 10px;margin-top: 10px;" Text="Volver" OnClick="btnPagConstruccion_OnClick"/>
        </footer>
    </div>
</div>
    <div class="row">
    <%--(IB) Monitoreo--%>
    <div class="col-md-3">
                            <div class="dashboard-stat yellow-casablanca">
                                <div class="visual">
                                    <i class="fa fa-comments"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <span id="countAltasHist"></span>
                                    </div>
                                    <div class="desc"> Altas Históricas <a href="#" id="lnkObtenerAltasHist"><i class="fa fa-plus-square"></i></a> </div>
                                </div>
                            </div>
    </div>
    <div class="col-md-3">
                            <div class="dashboard-stat yellow-casablanca">
                                <div class="visual">
                                    <i class="fa fa-comments"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <span id="countBajasHist"></span>
                                    </div>
                                    <div class="desc"> Bajas Históricas <a href="#" id="lnkObtenerBajasHist"><i class="fa fa-plus-square"></i></a></div>
                                </div>
                            </div>
    </div>
    <div class="col-md-3">
                            <div class="dashboard-stat yellow-casablanca">
                                <div class="visual">
                                    <i class="fa fa-comments"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <span id="countComerciosAct"></span>
                                    </div>
                                    <div class="desc"> Comercios Activos <a href="#" id="lnkObtenerComerciosAct"><i class="fa fa-plus-square"></i></a></div>
                                </div>
                            </div>
    </div>
    <div class="col-md-3">
                            <div class="dashboard-stat yellow-casablanca">
                                <div class="visual">
                                    <i class="fa fa-comments"></i>
                                </div>
                                <div class="details">
                                    <div class="number">
                                        <span id="countReempapen"></span>
                                    </div>
                                    <div class="desc"> Reempad. Pendientes <a href="#" id="lnkObtenerReempaPen"><i class="fa fa-plus-square"></i></a></div>
                                </div>
                            </div>
    </div>
    </div>

    <div id="containers">
        <div id="TramitesContainer"></div>
        <div id="AltasHistContainer"></div>
        <div id="BajasHistContainer"></div>
        <div id="ComerciosActContainer"></div>
    </div>
    <div class="portlet box yellow" id="dvExportar" >
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-file-text-o"></i>Opciones de Descarga<span class="step-title">
                            </span>
                        </div>
                    </div>
                     <div class="form-body"> 
                         <%--NO HAY CUERPO--%>
                         </div>
                        
                    <div class="form-actions">
                        <div class="row" >
                            <div class="col-md-2">
                                <button type="button" id="btnGenerarPlanilla" class="btn default form-control" title="Generar Planilla">
                                    Generar Planilla <span class="glyphicon glyphicon-download-alt"></span> 
                                </button>
                                </div> 
                              
                            </div>
                    </div> 
                </div> 
                <div class="modal-footer" >
                                                        <div class="col-md-12  col-sm-offset-5 labelModificar" id="divOrganismo" runat="server" Visible="True">
                                                        <div class="row">
                                                        <div class="col-md-2"><label>Ver como...</label></div>
                                                        <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlBocaRecepcion" runat="server" 
                                                                CssClass="btn default form-control" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlBocaRecepcion_SelectedIndexChanged" />
                                                            <span class="help-block">Puede ver las estadìsticas como si fuera cada boca de recepción.</span>
                                                        </div>
                                                            
                                                            
                                                        </div>
                                                    </div>
</div>
    <hidden ID="hdCantReempapen"  Value="0" />
    <hidden ID="hdCantAltasHist"  Value="0" />
    <hidden ID="hdCantBajasHist"  Value="0" />
    <hidden ID="hdCantComerciosAct"  Value="0" />
    <hidden ID="hdVistaActiva"  Value="0" />

</asp:Content>
<asp:Content ID="ScriptContenedor" runat="server" ContentPlaceHolderID="ContentScript">

        <%--(IB) JTable--%>
    <!-- A helper library for JSON serialization -->
    <script type="text/javascript" src="metronic/assets/global/plugins/jtable/external/json2.js"></script>
    <script src="metronic/assets/global/plugins/jtable/jquery.jtable.min.js" type="text/javascript"></script>
    <script src="metronic/assets/global/plugins/jtable/extensions/jquery.jtable.aspnetpagemethods.min.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {

        $("#lnkObtenerReempaPen").hide();
        $("#lnkObtenerAltasHist").hide();
        $("#lnkObtenerComerciosAct").hide();
        $("#lnkObtenerBajasHist").hide();

        $('#TramitesContainer').jtable({
            title: 'Reempadronamientos pendientes',
            paging: true, //Enable paging
            pageSizeChangeArea: false,
            pageSize: 10, //Set page size (default: 10)
            sorting: false, //Enable sorting
            defaultSorting: 'fec_vencimiento ASC',
            selecting: true, //Enable selecting
            multiselect: false, //Allow multiple selecting
            selectingCheckboxes: false, //Show checkboxes on first column
            gotoPageArea: 'none',
            jqueryuiTheme: false,
            actions: {
                listAction: 'PanelControl.aspx/ObtenerReempaPen'
            },
            fields: {
                CUIT: {
                    key: true, visible: false,
                    title: 'Cuit',
                    list: true,
                    create: false,
                    edit: false,
                    width: '10%'
                },
                Nro_Sifcos: {
                    title: 'NSifcos',
                    width: '20%'
                },
                Razon_Social: {
                    title: 'Nombre',
                    width: '30%'
                }
                ,
                Nro_tramite: {
                    title: 'Tramite',
                    width: '30%'
                },
                fec_vencimiento: {
                    title: 'Fecha',
                    width: '20%',
                    type: 'date',
                    displayFormat: 'dd/mm/yy'
                }
            }
        }); //jtable

        $('#AltasHistContainer').jtable({
            title: 'Altas Históricas',
            paging: true, //Enable paging
            pageSizeChangeArea: false,
            pageSize: 10, //Set page size (default: 10)
            sorting: false, //Enable sorting
            defaultSorting: 'T.fec_alta ASC',
            selecting: true, //Enable selecting
            multiselect: false, //Allow multiple selecting
            selectingCheckboxes: false, //Show checkboxes on first column
            gotoPageArea: 'none',
            jqueryuiTheme: false,
            actions: {
                listAction: 'PanelControl.aspx/ObtenerAltasHist'
            },
            fields: {
                CUIT: {
                    key: true, visible: false,
                    title: 'Cuit',
                    list: true,
                    create: false,
                    edit: false,
                    width: '10%'
                },
                Nro_Sifcos: {
                    title: 'NSifcos',
                    width: '20%'
                },
                Razon_Social: {
                    title: 'Nombre',
                    width: '30%'
                }
                ,
                Nro_tramite: {
                    title: 'Tramite',
                    width: '30%'
                },
                fec_alta: {
                    title: 'Fecha',
                    width: '20%',
                    type: 'date',
                    displayFormat: 'dd/mm/yy'
                }
            }
        }); //jtable

        $('#BajasHistContainer').jtable({
            title: 'Bajas Históricas',
            paging: true, //Enable paging
            pageSizeChangeArea: false,
            pageSize: 10, //Set page size (default: 10)
            sorting: false, //Enable sorting
            defaultSorting: 'T.fec_alta ASC',
            selecting: true, //Enable selecting
            multiselect: false, //Allow multiple selecting
            selectingCheckboxes: false, //Show checkboxes on first column
            gotoPageArea: 'none',
            jqueryuiTheme: false,
            actions: {
                listAction: 'PanelControl.aspx/ObtenerBajasHist'
            },
            fields: {
                CUIT: {
                    key: true, visible: false,
                    title: 'Cuit',
                    list: true,
                    create: false,
                    edit: false,
                    width: '10%'
                },
                Nro_Sifcos: {
                    title: 'NSifcos',
                    width: '20%'
                },
                Razon_Social: {
                    title: 'Nombre',
                    width: '30%'
                }
                ,
                Nro_tramite: {
                    title: 'Tramite',
                    width: '30%'
                },
                fec_alta: {
                    title: 'Fecha',
                    width: '20%',
                    type: 'date',
                    displayFormat: 'dd/mm/yy'
                }
            }
        }); //jtable

        $('#ComerciosActContainer').jtable({
            title: 'Comercios Activos',
            paging: true, //Enable paging
            pageSizeChangeArea: false,
            pageSize: 10, //Set page size (default: 10)
            sorting: false, //Enable sorting
            defaultSorting: 'fec_vencimiento ASC',
            selecting: true, //Enable selecting
            multiselect: false, //Allow multiple selecting
            selectingCheckboxes: false, //Show checkboxes on first column
            gotoPageArea: 'none',
            jqueryuiTheme: false,
            actions: {
                listAction: 'PanelControl.aspx/ObtenerComerciosAct'
            },
            fields: {
                CUIT: {
                    key: true, visible: false,
                    title: 'Cuit',
                    list: true,
                    create: false,
                    edit: false,
                    width: '10%'
                },
                Nro_Sifcos: {
                    title: 'NSifcos',
                    width: '20%'
                },
                Razon_Social: {
                    title: 'Nombre',
                    width: '30%'
                }
                ,
                Nro_tramite: {
                    title: 'Tramite',
                    width: '30%'
                },
                fec_vencimiento: {
                    title: 'Fecha',
                    width: '20%',
                    type: 'date',
                    displayFormat: 'dd/mm/yy'
                }
            }
        }); //jtable

        $("#lnkObtenerReempaPen").click(function () {
            //alert("Detalle");
            $('#dvExportar').show();
            //$('#TramitesContainer').show();
            //alert($("#hdCantReempapen").val());
            ShowContainer('#TramitesContainer');
            $('#TramitesContainer').jtable('load', { pTotal: $("#hdCantReempapen").val() });
        });

        $("#lnkObtenerAltasHist").click(function () {
            //alert("Detalle");
            $('#dvExportar').show();
            //$('#AltasHistContainer').show();
            ShowContainer('#AltasHistContainer');
            //alert($("#AltasHistContainer").val());
            $('#AltasHistContainer').jtable('load', { pTotal: $("#hdCantAltasHist").val() });
        });

        $("#lnkObtenerComerciosAct").click(function () {
            $('#dvExportar').show();
            ShowContainer('#ComerciosActContainer');
            $('#ComerciosActContainer').jtable('load', { pTotal: $("#hdCantComerciosAct").val() });
        });
        $("#lnkObtenerBajasHist").click(function () {
            //alert("Detalle");
            $('#dvExportar').show();
            //$('#BajasHistContainer').show();
            ShowContainer('#BajasHistContainer');
            //alert($("#BajasHistContainer").val());
            $('#BajasHistContainer').jtable('load', { pTotal: $("#hdCantBajasHist").val() })
        });

        //Obtener cantidad de reempadronamientos pendientes
        //Autor: (IB)
        function ObtenerCantReempaPen() {
            $.ajax({
                type: "POST",
                url: "PanelControl.aspx/ObtenerCantReempaPen",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //console.log(msg);
                    oData = msg.d;
                    if (oData.Result == "OK") {
                        //alert(oData.ReempaPen);
                        $('#countReempapen').attr('data-value', oData.ReempaPen);
                        $('#countReempapen').counterUp();
                        $('#hdCantReempapen').val(oData.ReempaPen);
                        $("#lnkObtenerReempaPen").show();
                    }
                }
            });
        }
        //Obtener cantida de altas históricas
        //Autor: (IB)
        function ObtenerCantAltasHist() {
            $.ajax({
                type: "POST",
                url: "PanelControl.aspx/ObtenerCantAltasHist",
                async: true,
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //console.log(msg);
                    oData = msg.d;
                    if (oData.Result == "OK") {
                        $('#countAltasHist').attr('data-value', oData.AltasHist);
                        $('#countAltasHist').counterUp();
                        $('#hdCantAltasHist').val(oData.AltasHist);
                        $("#lnkObtenerAltasHist").show();
                    }
                }
            });
        }

        //Obtener cantidad de Comercios Activos
        //Autor: (IB)
        function ObtenerCantComerciosAct() {
            $.ajax({
                type: "POST",
                url: "PanelControl.aspx/ObtenerCantComerciosAct",
                async: true,
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //console.log(msg);
                    oData = msg.d;
                    if (oData.Result == "OK") {
                        $('#countComerciosAct').attr('data-value', oData.ComerciosAct);
                        $('#countComerciosAct').counterUp();
                        $('#hdCantComerciosAct').val(oData.ComerciosAct);
                        $("#lnkObtenerComerciosAct").show();
                    }
                }
            });
        }

        //Obtener cantidad de Bajas históricas	
        //Autor: (IB)
        function ObtenerCantBajasHist() {
            $.ajax({
                type: "POST",
                url: "PanelControl.aspx/ObtenerCantBajasHist",
                async: true,
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //console.log(msg);
                    oData = msg.d;
                    if (oData.Result == "OK") {
                        //alert(oData.ReempaPen);
                        $('#countBajasHist').attr('data-value', oData.BajasHist);
                        $('#countBajasHist').counterUp();
                        $('#hdCantBajasHist').val(oData.BajasHist);
                        $("#lnkObtenerBajasHist").show();
                    }
                }
            });
        }

        $('#dvExportar').hide();
        ShowContainer();


        function ShowContainer(container) {
            $('#containers').children().hide();

            if (container != "") {
                $(container).show();
            }

        };
        function GetCurrentContainer() {
            //alert("kk");
            var ret = "";
            $("#containers").children('div').each(function () {
                if ($(this).is(":visible")) {
                    //alert($(this).attr('Id'));
                    //return $(this).attr('Id');
                    ret = $(this).attr('Id');
                }

            });
            return ret;
        };


        $("#btnGenerarPlanilla").click(function () {



            //todo: mandar de parámetro lo que tengo
            var obj = new Object();
            obj.Origen = GetCurrentContainer();
            var params = JSON.stringify(obj);
            $.ajax({
                type: "POST",
                url: "PanelControl2Excel.aspx/SetVarsReporte",
                data: params,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $.download('PanelControl2Excel.aspx', '{data:0}', 'POST');
                }
            });

            /*
            var o = { FechaDesde: $("#FechaDesde").val(), FechaHasta: $("#FechaHasta").val(), IdActividadClanae: $("#hdActividadId").val(), ActividadClanae: $("#countries").val() };
            var MesDia = $('input:radio[name=optionsRadios]:checked').val();

            //objParams.IdCategoria = objIdProducto;
            var obj = new Object();
            obj.FechaDesde = o.FechaDesde;
            obj.FechaHasta = o.FechaHasta;
            obj.IdActividadClanae = o.IdActividadClanae;
            obj.ActividadClanae = o.ActividadClanae;
            if (obj.FechaDesde != "") {
            obj.FechaDesde = moment(convertirFechaToIso(obj.FechaDesde));
            }

            if (obj.FechaHasta != "") {
            obj.FechaHasta = moment(convertirFechaToIso(obj.FechaHasta));
            }

            obj.MesDia = MesDia;
            //console.log(objParams);


            var params = JSON.stringify(obj);
            $.ajax({
            type: "POST",
            url: "DashboardArchivo.aspx/SetVarsReporte",
            data: params,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
            $.download('DashboardArchivo.aspx', '{data:0}', 'POST');
            }
            });
            */
        });



        ObtenerCantReempaPen();
        ObtenerCantAltasHist();
        ObtenerCantComerciosAct();
        ObtenerCantBajasHist();




    });
</script>
      
</asp:Content>
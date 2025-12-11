<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Liquidaciones.aspx.cs" Inherits="SIFCOS.Liquidaciones" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="metronic/assets/global/plugins/jtable/themes/lightcolor/orange/jtable.min.css" rel="stylesheet" type="text/css" />        
    <style type="text/css">
     a:link {     color: green; } a:hover {     color: red; } a:visited {     color: black; }
    </style> 
    <h3 class="page-title">Liquidaciones</h3>
    <div class="page-bar">
		<ul class="page-breadcrumb">
			<li>
				<i class="fa fa-home"></i>
				<a href="#">Inicio</a>
				<i class="fa fa-angle-right"></i>
			</li>
			<li>
				<a href="Liquidaciones.aspx">Liquidaciones</a>
				<i class="fa fa-angle-right"></i>
			</li>
					 
		</ul>
				 
	</div>
    
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
    <div class="row"></div>
    <div class="container-fluid" id="pnlPrincipal">
        <h3><span class="text-info">Gestión de Liquidaciones</span></h3>

        <div id="divListado">
                    <form class="well" id="frmConsulta" action="">
                        <fieldset>            
                             <div class="row">
                                <div class="col-md-2"><label>Tipo de Liquidación</label></div>
                                <div class="col-md-3">
                                    <select id="cmbTipoLiq" class="btn default form-control" runat="server">
                                        <option value="1">ALTAS</option>
                                        <option value="4">REEMPADRONAMIENTOS</option>
                                    </select>
                                   
                                </div>
                                <div class="col-md-2"><label>Código Liquidación</label></div>
                                <div class="col-md-2">
                                    <input type="text" id="filIdCodLiq" name="filIdCodLiq" value="" class="btn default form-control" runat="server" /></div>
                                 <div class="col-md-3">
                                     <button type="button" id="btnConsultar" class="btn btn-success btn-circle btn-medio">
                                         <span class="fa fa-search"></span>Consultar
                                     </button>
                                                                          <button type="button" id="btnGenerar" class="btn btn-success btn-circle btn-medio">
                                         <span class="fa fa-plus"></span>Nueva
                                     </button>
                                 </div>
                            </div>
                        </fieldset>
                        
                        </form>
            <div class="row">
                <div class="col-md-12">
                     <div id="LiquidacionesTableContainer"></div>
                </div><!--/col-md-12-->
            </div><!--/row-->
        </div><!--/divListado-->
    </div>

        <%--DetalleLiquidación (actividades) MODAL --%>

<div id="tmplLiqDetalle-modal" class="fade modal">   
    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">×</button>
                <h3>Datos de la liquidación</h3>
            </div>
     
            <div class="modal-body">                         
                <div class="row">
                    <div class="col-md-12">
                        <div id="mensajestmplLiqDetalle"></div>
                    </div>
                </div> <%--/row--%>                           
                <div class="row">
                    <div class="col-md-12" id="pnltmplLiqDetalle">                
                    </div>
                </div>

            </div>  <%--/modal-body--%>
        </div> <%--/modal-content--%>
    </div> <%--/modal-dialog--%>
</div> <%--/tmplLiqDetalle-modal--%>

        <script id="tmplLiqDetalle" type="text/x-jquery-tmpl">     
            <div class="row">
                <div class="col-md-12">
                                      
                    <form class="well" id="frmDetLiquidacion" action="">
                    <fieldset>            
                        <legend>Codigo Liquidación: <strong class="text-info">${IdLiquidacion}</strong> </legend>                
                        <input name="IdLiquidacion" id="hdIdLiquidacion" type="hidden" value="${IdLiquidacion}" />
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-12">
                                    <label>Liquidación de: </label><strong class="text-info"> ${NTipoTramite}</strong>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>desde el Nro de Sifcos: </label><strong class="text-info"> ${NroSifcosDesde}</strong>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>hasta el Nro de Sifcos: </label><strong class="text-info"> ${NroSifcosHasta}</strong>
                                </div>
                            </div>                            
  
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Fecha Desde: </label><strong class="text-info"> ${$item.formatDate(FecDesde)}</strong>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Fecha Hasta: </label><strong class="text-info"> ${$item.formatDate(FecHasta)}</strong>
                                </div>
                            </div>
                            <legend>Otros datos</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <label>Número de Expediente: </label>
                                    <input type="text" id="NroExpediente" name="NroExpediente" class="form-control" placeholder="NroExpediente" value="${NroExpediente}" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label>Número de resolución: </label>
                                    <input type="text" id="NroResolucion" name="NroResolucion" class="form-control" placeholder="NroResolucion" value="${NroResolucion}" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                                                <label>Fecha Resolucion</label>
                            <input type="text" id="FechaResolucion" name="FechaResolucion" class="form-control" value="${$item.formatDate(FechaResolucion)}" />
                                </div>
                            </div>                                                        
                        </div>               
                    </fieldset>
                                <div class="form-group">
                <div class="pull-right">
                    <button type="button" id="btnBorrar" class="btn btn-danger btn-circle btn-medio">Borrar</button>
                    <button type="submit" id="btnGrabarOtrosDatos" class="btn btn-success btn-circle btn-medio">Grabar</button>

                </div>
            </div>
                    </form> <%--/form frmDetLiquidacion--%>
                </div> <%--/col frmDetLiquidacion--%>
            </div> <%--/row frmDetLiquidacion--%>
    <%--Div Principal de cada detalle--%>
    <%--Fin Div Principal de cada detalle--%>
        </script> <%--/tmplLiqDetalle--%>

</asp:Content>
<asp:Content ID="ScriptContenedor" ContentPlaceHolderID="ContentScript" runat="server">

        <!--AGREGADO: Plugin para Templates:-->
        <script type="text/javascript" src="../Scripts/jquery.tmpl.min.js"></script>
        <!--AGREGADO: Plugin para Validator:-->
        <script type="text/javascript" src="../Scripts/jquery.validate.js"></script>
        <!--AGREGADO: Plugin para Validator.dateITA (fecha en formato dd/mm/yyyy):-->
        <script type="text/javascript" src="../Scripts/jquery.validate.dateITA.js"></script>


        <%--(IB) JTable--%>
    <!-- A helper library for JSON serialization -->
    <script type="text/javascript" src="metronic/assets/global/plugins/jtable/external/json2.min.js"></script>
    <script src="metronic/assets/global/plugins/jtable/jquery.jtable.min.js" type="text/javascript"></script>
    <script src="metronic/assets/global/plugins/jtable/extensions/jquery.jtable.aspnetpagemethods.min.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $.datepicker.setDefaults($.datepicker.regional["es"]);


        $('#LiquidacionesTableContainer').jtable({
            title: 'Consulta de Liquidaciones',
            paging: true, //Enable paging
            pageSizeChangeArea: false,
            pageSize: 30, //Set page size (default: 10)
            sorting: true, //Enable sorting
            defaultSorting: 'NroSifcosDesde ASC',
            selecting: false, //Enable selecting
            multiselect: false, //Allow multiple selecting
            selectingCheckboxes: false, //Show checkboxes on first column
            gotoPageArea: 'none',
            jqueryuiTheme: false,
            actions: {
                listAction: 'Liquidaciones.aspx/GetLiquidaciones'
            },
            fields: {
                //CHILD TABLE "Organismos Sup de la LIQUIDACION"
                OrganismosSup: {
                    title: 'Bocas Sup',
                    width: '10%',
                    sorting: false,
                    edit: false,
                    //create: false,
                    //listClass: 'child-opener-image-column',
                    display: function (liqData) {
                        var $img = $('<button class="child-opener-image" title="Ver Bocas Sup de la Liquidacion"><span class="fa  fa-file-text-o"></span></button>');
                        //Open child table when user clicks the image
                        $img.click(function (ev) {
                            ev.stopPropagation();
                            ev.preventDefault();
                            $('#LiquidacionesTableContainer').jtable('openChildTable',
                                $img.closest('tr'), //Parent row
                                {
                                    title: 'Agrupado por Boca Superior. Código Liquidación: <b>' + liqData.record.IdLiquidacion + '</b>',
                                    paging: true, //Enable paging
                                    pageSizeChangeArea: false,
                                    pageSize: 30, //Set page size (default: 10)
                                    gotoPageArea: 'none',
                                    sorting: true, //Enable sorting
                                    selecting: false, //Enable selecting
                                    multiselect: false, //Allow multiple selecting
                                    selectingCheckboxes: false, //Show checkboxes on first column
                                    defaultSorting: 'IdOrganismo ASC',
                                    actions: {
                                        listAction: 'Liquidaciones.aspx/GetLiqOrganismosSup'
                                    },
                                    fields: {
                                        //Detalle por organismo
                                        Bocas: {
                                            title: 'Bocas',
                                            width: '10%',
                                            //list: true,
                                            sorting: false,
                                            edit: false,
                                            create: false,
                                            //listClass: 'child-opener-image-column',
                                            display: function (liqData) {
                                                var $img = $('<button class="child-opener-image" title="Ver Organismos de la Liquidacion"><span class="fa  fa-file-text-o"></span></button>');
                                                //Open child table when user clicks the image
                                                $img.click(function (ev) {
                                                    ev.stopPropagation();
                                                    ev.preventDefault();
                                                    $('#LiquidacionesTableContainer').jtable('openChildTable',
                                                        $img.closest('tr'), //Parent row
                                                        {
                                                            title: 'Agrupado por Organismos. Código Liquidación: <b>' + liqData.record.IdLiquidacion + '</b> Boca: ' + liqData.record.RazonSocial,
                                                            paging: true, //Enable paging
                                                            pageSizeChangeArea: false,
                                                            pageSize: 30, //Set page size (default: 10)
                                                            gotoPageArea: 'none',
                                                            sorting: true, //Enable sorting
                                                            selecting: false, //Enable selecting
                                                            multiselect: false, //Allow multiple selecting
                                                            selectingCheckboxes: false, //Show checkboxes on first column
                                                            defaultSorting: 'IdLiqOrganismo ASC',
                                                            actions: {
                                                                listAction: 'Liquidaciones.aspx/GetLiqOrganismos'
                                                            },
                                                            fields: {
                                                                AsignarAction: {
                                                                    title: '',
                                                                    width: '1%',
                                                                    sorting: false,
                                                                    create: false,
                                                                    edit: false,
                                                                    //list: true,
                                                                    display: function (data) {
                                                                        if (data.record) {
                                                                            //if (data.record.Pagado == 'S') {
                                                                            var $btn = $('<button class="child-opener-image btn-mini" title="Exportar Trámites a Excel" ><span class="fa  fa-hand-o-down"></span></button>');
                                                                            $btn.click(function (ev) {
                                                                                ev.stopPropagation();
                                                                                ev.preventDefault();
                                                                                //console.log(data.record);
                                                                                //renderFormAsignarTRS(data.record);
                                                                                //$("#AsignarTRS-modal").modal('show');


                                                                                //todo: mandar de parámetro lo que tengo
                                                                                var obj = new Object();
                                                                                obj.IdLiqOrg = data.record.IdLiqOrganismo;
                                                                                obj.DescriFiltro = "Detalle de trámites liquidados. Boca " + data.record.RazonSocial + " cod liq: " + liqData.record.IdLiquidacion;
                                                                                obj.NombreArchivo = "DetalleTramites_Liq_" + liqData.record.IdLiquidacion + "_Boca_" + data.record.IdOrganismo + "_IdLiqOrg_" + data.record.IdLiqOrganismo;
                                                                                var params = JSON.stringify(obj);
                                                                                $.ajax({
                                                                                    type: "POST",
                                                                                    url: "LiqTramites2Excel.aspx/SetVarsReporte",
                                                                                    data: params,
                                                                                    async: false,
                                                                                    contentType: "application/json; charset=utf-8",
                                                                                    dataType: "json",
                                                                                    success: function (msg) {
                                                                                        $.download('LiqTramites2Excel.aspx', '{data:0}', 'POST');
                                                                                    }
                                                                                });


                                                                            });
                                                                            //Return image to show on the person row
                                                                            return $btn;
                                                                            //}
                                                                        }
                                                                    }
                                                                },
                                                                IdLiquidacion: {
                                                                    title: 'Código Liquidación',
                                                                    type: 'hidden',
                                                                    defaultValue: liqData.record.IdLiquidacion
                                                                },
                                                                IdLiqOrganismo: {
                                                                    title: 'Id',
                                                                    width: '8%',
                                                                    key: true
                                                                },
                                                                RazonSocial: {
                                                                    title: 'Organismo',
                                                                    width: '55%'
                                                                },
                                                                TotalLiquidado: {
                                                                    title: 'Total',
                                                                    width: '10%',
                                                                    display: function (data) {
                                                                        return '<span class="right">$ ' + data.record.TotalLiquidado + '</span>';
                                                                    }
                                                                },
                                                                Cantidad: {
                                                                    title: 'Cantidad',
                                                                    width: '20%'
                                                                }
                                                            }
                                                        }, function (data) { //opened handler
                                                            //console.log(data);
                                                            data.childTable.css("margin", "10px");
                                                            data.childTable.jtable('load', {
                                                                pIdLiquidacion: liqData.record.IdLiquidacion,
                                                                pIdOrganismoSup: liqData.record.IdOrganismo////TODO SEGUIR AACA
                                                            });
                                                        });
                                                });
                                                //Return image to show on the person row
                                                return $img;
                                            }
                                        },
                                        //Fin Detalle por organismo
                                        ExportarAction: {
                                            title: '',
                                            width: '1%',
                                            sorting: false,
                                            create: false,
                                            edit: false,
                                            //list: true,
                                            display: function (data) {
                                                if (data.record) {
                                                    //if (data.record.Pagado == 'S') {
                                                    var $btn = $('<button class="child-opener-image btn-mini" title="Exportar Trámites a Excel" ><span class="fa  fa-hand-o-down"></span></button>');
                                                    $btn.click(function (ev) {
                                                        ev.stopPropagation();
                                                        ev.preventDefault();

                                                        var obj = new Object();
                                                        obj.IdLiquidacion = data.record.IdLiquidacion;
                                                        obj.IdOrganismo = data.record.IdOrganismo;

                                                        obj.DescriFiltro = "Detalle de trámites liquidados. Boca Sup" + data.record.RazonSocial + " cod liq: " + liqData.record.IdLiquidacion;
                                                        obj.NombreArchivo = "DetalleTramites_Liq_" + liqData.record.IdLiquidacion + "_BocaSup_" + data.record.IdOrganismo + "_IdLiqOrg_" + data.record.IdLiqOrganismo;
                                                        var params = JSON.stringify(obj);
                                                        $.ajax({
                                                            type: "POST",
                                                            url: "LiqTramites2Excel.aspx/SetVarsReporteBocaSup",
                                                            data: params,
                                                            async: false,
                                                            contentType: "application/json; charset=utf-8",
                                                            dataType: "json",
                                                            success: function (msg) {
                                                                $.download('LiqTramites2Excel.aspx', '{data:0}', 'POST');
                                                            }
                                                        });


                                                    });
                                                    //Return image to show on the person row
                                                    return $btn;
                                                    //}
                                                }
                                            }
                                        },
                                        IdLiquidacion: {
                                            title: 'Código Liquidación',
                                            type: 'hidden',
                                            defaultValue: liqData.record.IdLiquidacion
                                        },
                                        IdOrganismo: {
                                            type: 'hidden'
                                        },
                                        RazonSocial: {
                                            title: 'Organismo',
                                            width: '55%'
                                        },
                                        TotalLiquidado: {
                                            title: 'Total',
                                            width: '10%',
                                            display: function (data) {
                                                return '$ ' + data.record.TotalLiquidado;
                                            }
                                        },
                                        Cantidad: {
                                            title: 'Cantidad',
                                            width: '20%'
                                        }
                                    }
                                }, function (data) { //opened handler
                                    //console.log(data);
                                    data.childTable.css("margin", "10px");
                                    data.childTable.jtable('load', {
                                        pIdLiquidacion: liqData.record.IdLiquidacion
                                    });
                                });
                        });
                        //Return image to show on the person row
                        return $img;
                    }
                }, //Fin Organismos Sup
                IdLiquidacion: {
                    key: true,
                    title: 'Código Liquidación',
                    //list: true,
                    width: '10%'
                },
                NroSifcosDesde: {
                    title: 'Nro Desde',
                    width: '15%'
                    //list: true
                },
                NroSifcosHasta: {
                    title: 'Nro Hasta',
                    //list: true,
                    width: '15%'
                },
                FecDesde: {
                    title: 'Fec Desde',
                    type: 'date',
                    displayFormat: 'dd/mm/yy',
                    //list: true,
                    width: '15%'
                },
                FecHasta: {
                    title: 'Fec Hasta',
                    type: 'date',
                    //list: true,
                    displayFormat: 'dd/mm/yy',
                    width: '15%'
                },
                NTipoTramite: {
                    type: 'hidden'
                },
                IdTipoTramite: {
                    type: 'hidden'
                },
                NroExpediente: {
                    type: 'hidden'
                },
                NroResolucion: {
                    type: 'hidden'
                },
                FechaResolucion: {
                    type: 'hidden'
                },
                editModalAction: {
                    title: '',
                    width: '10%',
                    sorting: false,
                    //create: false,
                    edit: false,
                    //list: true,
                    display: function (data) {
                        if (data.record) {
                            var $btn = $('<button class="jtable-command-button fa fa-edit" data-toggle="modal" title="Editar" href="#tipo-modal" ></span></button>');
                            $btn.click(function (ev) {
                                ev.stopPropagation();
                                ev.preventDefault();

                                VerDetalleLiquidacion(data.record);


                            });
                            //Return image to show on the person row
                            return $btn;
                        }
                    }
                }

            }
        }); //jtable

        $("#btnConsultar").click(function () {
            IdTipoTramite = $('#<%=cmbTipoLiq.ClientID %> option:selected').val();
            CodLiq = $('#<%=filIdCodLiq.ClientID %>').val();

            consultarLiquidaciones(IdTipoTramite, CodLiq);
        });

        function consultarLiquidaciones(IdTipoTramite, CodigoLiq) {

            //console.log("COdigo liq " + CodigoLiq);
            //console.log("TipoTram " + IdTipoTramite);
            $('#LiquidacionesTableContainer').jtable('load', { pIdTipoTramite: IdTipoTramite, pCodigoLiq: CodigoLiq });

            //$('#LiquidacionesTableContainer').jtable('changeColumnWidth', 'IdLiquidacion', '15%');
            if (IdTipoTramite == "1") {
                $('#LiquidacionesTableContainer').jtable('changeColumnVisibility', 'FecDesde', 'hidden');
                $('#LiquidacionesTableContainer').jtable('changeColumnVisibility', 'FecHasta', 'hidden');
                $('#LiquidacionesTableContainer').jtable('changeColumnVisibility', 'NroSifcosDesde', 'visible');
                $('#LiquidacionesTableContainer').jtable('changeColumnVisibility', 'NroSifcosHasta', 'visible');
            } else {
                $('#LiquidacionesTableContainer').jtable('changeColumnVisibility', 'NroSifcosDesde', 'hidden');
                $('#LiquidacionesTableContainer').jtable('changeColumnVisibility', 'NroSifcosHasta', 'hidden');
                $('#LiquidacionesTableContainer').jtable('changeColumnVisibility', 'FecDesde', 'visible');
                $('#LiquidacionesTableContainer').jtable('changeColumnVisibility', 'FecHasta', 'visible');
            }

        };

        $("#btnGenerar").click(function () {
            window.location.href = 'LiquidacionesGenerar.aspx';
        });

        function VerDetalleLiquidacion(data) {
            //$("#dvDetalleLiq").show();


            $("#tmplLiqDetalle").tmpl(data, {
                formatDate: function (datetime) {
                    if (datetime == null)
                        return "";
                    //console.log(datetime);
                    //return datetime;
                    return moment(datetime).format('DD/MM/YYYY');
                    //var dt = formatJsonDate(datetime);
                    //return dt;
                }
            }).appendTo($("#pnltmplLiqDetalle").empty());


            $("#FechaResolucion").datepicker({
                rtl: Metronic.isRTL(),
                orientation: "left",
                autoclose: true,
                format: "dd/mm/yyyy",
                language: "es"
            });

            ValidacionesDetalleLiquidacion();

            $('#btnBorrar').click(function () {

                var objParams = {
                    IdLiquidacion: $("#hdIdLiquidacion").val()
                };

                var obj = new Object();
                obj.record = objParams;
                var params = JSON.stringify(obj);


                $.ajax({
                    type: "POST",
                    url: "Liquidaciones.aspx/BorrarUltimaLiquidacion",
                    async: false,
                    data: params,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //oData = JSON.parse(msg);
                        oData = msg.d;
                        if (oData.Result != "OK") {
                            $("#tmplLiqDetalle-modal").modal('hide');
                            alert(oData.Message);
                        } else {
                            alert("Liquidación eliminada");
                            $("#tmplLiqDetalle-modal").modal('hide');
                            $('#LiquidacionesTableContainer').jtable('reload');
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            });

            $("#tmplLiqDetalle-modal").modal('show');
        }
        function ValidacionesDetalleLiquidacion() {

            //Para Validar campos HIDDEN hay que limpiarle la lista de objetos a ignorar (esta incluido los hidden);
            $.validator.setDefaults({
                ignore: []
                // any other default options and/or rules
            });
            $.validator.addMethod("valueNotEquals", function (value, element, arg) {
                return arg != value;
            }, "Value must not equal arg.");

            $("#frmDetLiquidacion").validate({
                rules: {
                    'FechaResolucion': { required: false, dateITA: true }
                },
                messages: {
                    'FechaResolucion': 'Formato inválido Fecha resolución'
                },
                /*errorPlacement: function (error, element) {
                element.closest('.control-group').find('.help-block').html(error.text());
                },*/
                highlight: function (element) {
                    $(element).closest('.control-group').removeClass('success').addClass('error');
                },
                success: function (element) {
                    element
                        .text('').addClass('valid')
                        .closest('.control-group').removeClass('error').addClass('success');

                },
                showErrors: function (errorMap, errorList) {
                    if (this.numberOfInvalids() > 0) {
                        $("#mensajestmplLiqDetalle").show();
                        //alerta($("#mensajesDetalle"), "No se puede Grabar", "Existe(n) " + this.numberOfInvalids() + " error(es) en el formulario, que no permiten grabarlo. Vea los detalles indicados mas arriba");
                    }
                    else
                        $("#mensajestmplLiqDetalle").hide();
                    this.defaultShowErrors();
                },
                debug: true,
                submitHandler: function (form) {
                    DetalleLiquidacionConfirma();
                }
            });
        }
        function DetalleLiquidacionConfirma() {

            var objParams = {
                IdLiquidacion: $("#hdIdLiquidacion").val(),
                NroExpediente: $("#NroExpediente").val(),
                NroResolucion: $("#NroResolucion").val(),
                strFechaResolucion: $("#FechaResolucion").val()
            };

            //var objParams = $("#frmDetLiquidacion").serializeObject();
            //objParams.IdCategoria = objIdProducto;
            var obj = new Object();
            obj.record = objParams;
            //console.log(objParams);
            //////var params = JSON.stringify(objParams);

            var params = JSON.stringify(obj);
            console.log(params);

            /*
            var obj = new Object();
            obj.IdLiqOrg = data.record.IdLiqOrganismo;
            obj.DescriFiltro = "Detalle de trámites liquidados. Boca " + data.record.RazonSocial + " cod liq: " + liqData.record.IdLiquidacion;
            obj.NombreArchivo = "DetalleTramites_Liq_" + liqData.record.IdLiquidacion + "_Boca_" + data.record.IdOrganismo + "_IdLiqOrg_" + data.record.IdLiqOrganismo;
            var params = JSON.stringify(obj);
            */

            $.ajax({
                type: "POST",
                url: "Liquidaciones.aspx/GuardarDetalleLiquidacion",
                async: false,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //oData = JSON.parse(msg);
                    oData = msg.d;
                    console.log(oData);
                    if (oData.Result != "OK") {
                        $("#tmplLiqDetalle-modal").modal('hide');
                        alertaError(oData.Message);
                    } else {
                        $("#tmplLiqDetalle-modal").modal('hide');
                        $('#LiquidacionesTableContainer').jtable('reload');
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });

        }




    });
</script>
</asp:Content>

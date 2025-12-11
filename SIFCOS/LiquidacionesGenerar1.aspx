<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="LiquidacionesGenerar1.aspx.cs" Inherits="SIFCOS.LiquidacionesGenerar" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
    <h3 class="page-title">Liquidaciones Generar
    </h3>
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <i class="fa fa-home"></i>
                <a href="#">Inicio</a>
                <i class="fa fa-angle-right"></i>
            </li>
            <li>
                <a href="LiquidacionesGenerar.aspx">Liquidaciones Generar</a>
                <i class="fa fa-angle-right"></i>
            </li>

        </ul>

    </div>

    <link href="metronic/assets/global/plugins/jtable/themes/lightcolor/orange/jtable.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        a:link {
            color: green;
        }

        a:hover {
            color: red;
        }

        a:visited {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenedorPrincipal" runat="server">


    <div class="container-fluid" id="pnlPrincipal">
        <h3><span class="text-info">Gestión de Liquidaciones</span></h3>

        <div id="divListado">
            <form class="well" id="frmConsulta" action="">
                <fieldset>
                    <div class="row">
                        <div class="col-md-4">
                            <label>Tipo de Liquidación a generar</label></div>
                        <div class="col-md-3">
                            <select id="cmbTipoLiq" class="btn default form-control">
                                <option value="1">ALTAS</option>
                                <option value="4">REEMPADRONAMIENTOS</option>
                            </select>

                        </div>
                        <div class="col-md-3">
                            <button type="button" id="btnTipoLiqSiguiente" class="btn btn-success btn-circle btn-medio">
                                <span class="fa fa-arrow-right"></span>Siguiente
                            </button>
                        </div>
                    </div>
                </fieldset>
            </form>
        </div>
        <!--/divListado-->
        <div id="divGenerarLiqAlta">
            <form class="well" id="frmGenerarLiqAlta" action="">
                <fieldset>
                    <div class="row">
                        <div class="col-md-4">
                            <label>
                                Nro Sifcos Desde</label>
                        </div>
                        <div class="col-md-6">
                            <input type="text" id="NroSifcosDesde" name="NroSifcosDesde" class="form-control circle" placeholder="Sifcos Desde"
                                value="" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label>
                                Nro Sifcos Hasta</label>
                        </div>
                        <div class="col-md-6">
                            <input type="text" id="NroSifcosHasta" name="NroSifcosHasta" class="form-control circle" placeholder="Sifcos Hasta"
                                value="" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <button type="button" id="btnGenAlta" class="btn btn-success btn-circle btn-medio">
                                <span class="fa fa-bullseye"></span>Generar
                            </button>
                        </div>
                    </div>
                </fieldset>
            </form>
        </div>
        <div id="divGenerarLiqReempa">
            <form class="well" id="frmGenerarLiqReempa" action="">
                <fieldset>
                    <div class="row">
                        <div class="col-md-4">
                            <label>
                                Config liq Reempa</label>
                        </div>
                        <div class="col-md-4">
                            <input type="text" id="FechaHasta" name="FechaHasta" class="form-control circle"
                                placeholder="Liquidar hasta..." />
                        </div>
                        <div class="col-md-4">
                            <button type="button" id="btnGenReempa" class="btn btn-success btn-circle btn-medio">
                                <span class="fa fa-bullseye"></span>Generar
                            </button>
                        </div>
                    </div>
                </fieldset>
            </form>
        </div>
        <div id="loadingPanel" style="display: none;">
            <div>
                cargando...
            </div>
            <div class="progress">
                <div class="progress-bar progress-bar-info progress-bar-striped active" role="progressbar"
                    aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                </div>
            </div>
        </div>
    </div>
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

            $.ajaxSetup({
                beforeSend: function (xhr) {
                    // show progress spinner

                    $('#loadingPanel').show();
                },
                complete: function (xhr, status) {
                    // hide progress spinner
                    $('#loadingPanel').hide();
                }
            });
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#divGenerarLiqAlta").hide();
            $("#divGenerarLiqReempa").hide();



            //$("#FechaHasta").datepicker("option", "dateFormat", "dd/mm/yy");

            $("#btnTipoLiqSiguiente").click(function () {
                IdTipoTramite = $('#cmbTipoLiq  option:selected').val();

                if (IdTipoTramite == '1') {
                    ///Mostramos panel liq alta
                    $("#divGenerarLiqAlta").show();
                    $("#divGenerarLiqReempa").hide();

                    if ($('#NroSifcosDesde').val() == '')
                        ObtenerUltimoNroSifcos();
                }
                else {
                    //Mostramos panel liq reempa
                    $("#divGenerarLiqReempa").show();
                    $("#divGenerarLiqAlta").hide();
                    $("#FechaHasta").datepicker({
                        rtl: Metronic.isRTL(),
                        orientation: "left",
                        autoclose: true,
                        format: "dd/mm/yyyy",
                        language: "es"
                    });
                }

            });

            //Obtener el ultimo nro sifcos desde
            //Autor: (IB)
            function ObtenerUltimoNroSifcos() {

                $.ajax({
                    type: "POST",
                    url: "LiquidacionesGenerar.aspx/ObtenerLiqAltasUltimoNroSifcos",
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //console.log(msg);
                        oData = msg.d;
                        if (oData.Result == "OK") {
                            //alert(oData.ReempaPen);
                            $('#NroSifcosDesde').val(oData.NroSifcosDesde);
                        }
                    }
                });
            }


            $("#btnGenAlta").click(function () {

                var nroSifcosDesde = $("#NroSifcosDesde").val();
                var nroSifcosHasta = $("#NroSifcosHasta").val();

                if (!$("#NroSifcosDesde").val()) {
                    alert("Debe ingresar un valor numérico en número Sifcos Desde");
                    return;
                }
                if (!$("#NroSifcosHasta").val()) {
                    alert("Debe ingresar un valor numérico en número Sifcos Hasta");
                    return;
                }
                if (nroSifcosDesde >= nroSifcosHasta) {
                    alert("Rango inválido Sifcos Desde - Hasta");
                    return;
                }

                var objParams = { pNroSifcosDesde: nroSifcosDesde, pNroSifcosHasta: nroSifcosHasta };
                //var obj = new Object();
                //obj.record = objParams;

                var params = JSON.stringify(objParams);


                $.ajax({
                    type: "POST",
                    url: "LiquidacionesGenerar.aspx/GenerarAltas",
                    data: params,
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //console.log(msg);
                        oData = msg.d;
                        if (oData.Result == "OK") {
                            alert("Liquidación generada con éxito");
                            window.location.href = 'Liquidaciones.aspx?ultiq=1';

                        } else {
                            alert(oData.Mensaje);
                            //$('#Mensaje').val(oData.Mensaje);
                        }
                    }
                });
            });

            $("#btnGenReempa").click(function () {
                //FechaHasta
                var FechaHasta = $("#FechaHasta").val();
                var objParams = { pFecHasta: FechaHasta };
                var params = JSON.stringify(objParams);

                $.ajax({
                    type: "POST",
                    url: "LiquidacionesGenerar.aspx/GenerarReempa",
                    data: params,
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //console.log(msg);

                        oData = msg.d;
                        if (oData.Result == "OK") {
                            if (oData.IdLiquidacion != "0") {
                                alert("Liquidación generada con éxito");
                                window.location.href = 'Liquidaciones.aspx?ultiq=1';
                            } else {
                                alert(oData.Mensaje);
                                //$('#Mensaje').val(oData.Mensaje);
                            }
                        }
                    }
                });


            });
        });
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Principal_SinLogin.Master" AutoEventWireup="true" CodeBehind="Ubicacion.aspx.cs" Inherits="SIFCOS.Ubicacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"  crossorigin="anonymous"></script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ConenedorPrincipal" runat="server">
    <div class="container">

        <br/>
        <br/>
        <h1>Verificación de Ubicación</h1>

        <br/>
        <br/>
        <div class="row">
            <div class="col-md-4">
                <label>Latitud : </label>
                <input type="text" class="form-control" id="inputLat" value = '-31.4201799' placeholder="Latitud">
                <small style="color:grey;">  Ej: -31.4201799 </small>
            </div>
            <div class="col-md-4">
                <label>Longitud : </label>
                <input type="text" class="form-control" id="inputLon" value = '-64.1880839' placeholder="Longitud">
                <small style="color:grey;">  Ej: -64.1880839 </small>
            </div>
            <div class="col-md-4">
                <label style="color:white;">.</label>
                <button id = "verificar" class="form-control btn btn-primary">Verificar Dom</button><br/>
                <button id = "btnUbicarCom" class="form-control btn btn-primary">Ubicar</button><br/>
            </div>
        </div>
        <br/>
        <br/>
        <div class="row">
            <div class="col-md-12">
                <label id="mensaje"></label>
            </div>
        </div>
        
        <div class="row">
            <div class="col-md-12" >
                <label class="label" >MAPA </label>
                <div id="divMapa" stryle="overflow: scroll;">
                    <iframe name="mapa01" style="width:100%; height:600px; " ></iframe>
                </div>
            </div>
        </div>
    </div>
    
   
    
    
    
    
   
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        function verificarUbicacion(lat, lon) {
            // Definir límites de la localidad
            const limites = {
                norte: -29.438006,  // Latitud máxima al norte
                sur: -34.956497,    // Latitud mínima al sur
                este: -65.801831,   // Longitud máxima al este
                oeste: -61.771638   // Longitud mínima al oeste
            };

            // Verificar si la ubicación está dentro de los límites
            if (lat >= limites.sur && lat <= limites.norte && lon >= limites.oeste && lon <= limites.este) {
                return "Ubicación válida dentro de la localidad.";
            } else {
                return "Error: La ubicación está fuera de los límites establecidos.";
            }
        }

        // Ejemplo de uso con valores dinámicos de una página ASPX
        document.getElementById("verificar").addEventListener("click", function () {
            const lat = parseFloat(document.getElementById("inputLat").value);
            const lon = parseFloat(document.getElementById("inputLon").value);

            const resultado = verificarUbicacion(lat, lon);
            document.getElementById("mensaje").innerText = resultado;
        });

        $(document).ready(function () {


            function CargarMapa() {
                const status = document.querySelector('#status');
                //const mapLink = document.querySelector('#map-link');

                //mapLink.href = '';
                //mapLink.textContent = '';

                var url = "https://maaysp-ws.cba.gov.ar/industria/ca_comercios?map=13";

                var varIds = '';// document.getElementById("inputLat").innerText;



                var latComercio = "-31.4201799";
                var lonComercio = "-64.1880839";

                var latComercio = document.getElementById("inputLat").value;
                var lonComercio = document.getElementById("inputLon").value;

                /*Envio el post para cargar el mapa , con lat lon seteado por defecto.*/
                var objVista = new Object();
                objVista.LatComercio = latComercio;
                objVista.LongComercio = lonComercio;
                objVista.RangoMtrs = "500";

                var varIdDepartamentos = '';/*$('#inputDepartamentos').val();*/

                var params = {
                    ids: varIds,
                    idVistas: JSON.stringify(objVista),
                    idDepartamentos: varIdDepartamentos,
                    lonPunto: lonComercio,
                    latPunto: latComercio
                };

                $.post(url, params, function (htmlexterno) {

                    var myWindow2 = window.open("", "mapa01");
                    myWindow2.document.write(htmlexterno);
                    myWindow2.document.close();
                });


            };

            $("#btnUbicarCom").click(function () {
                CargarMapa();
            });

        });

    </script>
</asp:Content>

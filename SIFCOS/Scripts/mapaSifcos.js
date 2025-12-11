/**  Autor: Facundo Álvarez **/
/**  Fecha: Julio-2016**/



var gMapa = null;
var infoWindow = null;
var gMarker = null;
var autocomplete = null;
var input = null;


function openInfoWindow(marker) {
    var markerLatLng = marker.getPosition();

    $("#ConenedorPrincipal_txtLatitud").val(markerLatLng.lat());
    $("#ConenedorPrincipal_txtLongitud").val(markerLatLng.lng());
    setearLatLongEnTextBox();
    infoWindow.setContent([
        'SU COMERCIO SE ENCUENTRA AQUÍ. ',
        '<br/> Puede corregir la ubicación arratrando el Marcador.'
    ].join(''));
    infoWindow.open(gMapa, marker);
}


function setearLatLongEnTextBox() {
    //Para pasar parametros via object:
    var obj = new Object();
    obj.pLatitud = $("#ConenedorPrincipal_txtLatitud").val();
    obj.pLongitud = $("#ConenedorPrincipal_txtLongitud").val();

    var params = JSON.stringify(obj); //deserealiza el objeto en una cadena de caracteres.
    
    //es necesario setear el control texbox de latitud y long. para tomar los valores del lado del servidor.
    $.ajax({
        type: "POST",
        url: "Inscripcion.aspx/SetLatitudLongitud",
        async: false,
        data: params,
        contentType: "application/json; charset=utf-8",
        dataType: "json" 
    });

}

function ejecutarMarkerEnMapa() {
//    if (gMarker != null) {
//        gMarker.setMap(null);
//    }
    //Obtengo el objeto PlaceResult, que tiene información del lugar cuya dirección se seleccionó
    //var place = autocomplete.getPlace().name;
    if (autocomplete == null) {
        input = document.getElementById('_txtDireccion');
        autocomplete = new google.maps.places.Autocomplete(input);
    }

    var place = autocomplete.getPlace().formatted_address;
    if (place == null) {
        //si el place es nulo , es porque ingresó un texto y se dió un enter en la caja de texto txtDireccion. Entonces sólo tomamos el texto ingresado.
        place = autocomplete.getPlace().name;
    }
        
   


    var gCoder = new google.maps.Geocoder();
    var objInformacion = {
        address: place
    };

    gCoder.geocode(objInformacion, fn_coder);

    function fn_coder(datos) {
        var coordenadas = datos[0].geometry.location; // es un objeto LatLng de google.
        gMapa.setCenter(coordenadas);
        var config = {
            map: gMapa,
            draggable: true,
            position: coordenadas
        };
        if (gMarker == null) {
            gMarker = new google.maps.Marker(config);


        } else {
            gMarker.setPosition(new google.maps.LatLng(coordenadas.lat(), coordenadas.lng()));
        }
        

        $("#ConenedorPrincipal_txtLatitud").val(gMarker.getPosition().lat());
        $("#ConenedorPrincipal_txtLongitud").val(gMarker.getPosition().lng());
        setearLatLongEnTextBox();
        google.maps.event.addListener(gMarker, 'mouseup', function () {
            
            openInfoWindow(gMarker);

        });

        $("#barraProgreso").hide();
    


    };
}

;

function AutocompletarYubicarMarker() {
    //si ingresó a este metodo es porque se ejecuto el evento al teclear un enter en el txtDirección.
    bandBuscarSoloText = true; //prendo la bandera que va a indicar que tecleó un enter y debo tomar solo la dirección ingresada en la caja de texto.
    $("#barraProgreso").show();
    google.maps.event.trigger(autocomplete, 'place_changed');
   
};

function inicializarMapa() {
    var divMapa = document.getElementById('divMapa');
    input = document.getElementById('_txtDireccion');
    autocomplete = new google.maps.places.Autocomplete(input);

    

    //Agrego evento para que cuando se seleccione una dirección, se coloque allí un marcador y se centre el mapa en ese lugar
    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        bandBuscarSoloText = false; //apago la bandera.
        $("#barraProgreso").show();
        ejecutarMarkerEnMapa();
    });

    if (gMarker == null) {
        
        var lat;
        var lon;
        if ($("#ConenedorPrincipal_txtLatitud").val() == "") {
            //Coordenadas de ubicación de Cordoba.
            lat = '-31.42008329999999';
            lon = '-64.18877609999998';
        } else {
            lat = $("#ConenedorPrincipal_txtLatitud").val();
            lon = $("#ConenedorPrincipal_txtLongitud").val();
        }
    
    
    //esta información para el mapa dinamico debemos convertirlo en un objeto.

        //creo un objeto de google para esas coordenadas
        var gLatLon = new google.maps.LatLng(lat, lon);
//        gMarker = new google.maps.Marker({
//                position: gLatLon,
//                draggable: true,
//                map: gMapa,
//            title:"SU CUMERCIO SE UBICA AQUÍ"
//        });
        //creamos el mapa
        var objConfig = {
            zoom: 17,
            center: gLatLon
        };
        //renderizo e indico donde va el mapa y que configuración debe tener el mapa.
        gMapa = new google.maps.Map(divMapa, objConfig);
       
        infoWindow = new google.maps.InfoWindow();
       
    }


}

function inicializarMapaGEO() {
    map = new google.maps.Map(document.getElementById("divMapa"), {
        center: new google.maps.LatLng(-31.9420225, -64.1637883),
        zoom: 7

    });
    
    center = bounds.getCenter();
    map.fitBounds(bounds);
    
 
    


}







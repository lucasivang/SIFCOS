/**  Autor: Facundo Álvarez **/
/**  Fecha: Julio-2016**/


/**  Son 2 formas de mostrar la latutud y longitud de un punto en el google maps.  **/

/** 1. ASIGNAR UN PUNTO EN EL MAPA INDICANDO LA DIRECCIÓN DE UN TXTBOX **/

/** 2. ASIGNAR UN PUNTO HACIENDO CLICK EN EL MAPA  **/




/** 1. ASIGNAR UN PUNTO EN EL MAPA INDICANDO LA DIRECCIÓN DE UN TXTBOX **/
//namespace --> google.maps.ALGO
var divMapa = document.getElementById('mapa');

//pedimos al usuario sus coordenadas
navigator.geolocation.getCurrentPosition(fn_ok, fn_mal); //--> es una función asincrónica. Significa que al llamar al getCurrentPosition sigue ejecutando el script y cuando tenga una respuesta la envia.
//POr eso tiene dos funciones por parametro. fn_mal : SI EL USUARIO DECIDE NO COMPARTIR SU UBICACIÓN. fn_ok: si accede a compartir su ubicacion

function fn_mal() { }

var gMarker = null; //puntero que se utiliza en el mapa. Objeto de google.maps

//rta: nos va a dar la ubicacion del usuario.
function fn_ok(rta) {
    var lat = rta.coords.latitude;
    var lon = rta.coords.longitude;

    //esta información para el mapa dinamico debemos convertirlo en un objeto.

    //creo un objeto de google para esas coordenadas
    var gLatLon = new google.maps.LatLng(lat, lon);

    //creamos el mapa
    var objConfig = {
        zoom: 17,
        center: gLatLon
    };
    //renderizo e indico donde va el mapa y que configuración debe tener el mapa.
    var gMapa = new google.maps.Map(divMapa, objConfig);
    //                var objConfigMarker = {                    
    //                  position : gLatLon ,
    //                    map :gMapa ,
    //                    title : "USTED ESTÁ ACÁ."
    //                };
    //                var gMarker = new google.maps.Marker(objConfigMarker);


    var input = document.getElementById('_txtDireccion');
    //var input = '_txtDireccion';

    if (input) {
        autocomplete = new google.maps.places.Autocomplete(input);

        //Agrego evento para que cuando se seleccione una dirección, se coloque allí un marcador y se centre el mapa en ese lugar
        google.maps.event.addListener(autocomplete, 'place_changed', function () {

            //Obtengo el objeto PlaceResult, que tiene información del lugar cuya dirección se seleccionó
            var place = autocomplete.getPlace().name;

            var gCoder = new google.maps.Geocoder();
            var objInformacion = {
                address: place
            };

            gCoder.geocode(objInformacion, fn_coder);

            function fn_coder(datos) {
                var coordenadas = datos[0].geometry.location; // es un objeto LatLng de google.
                gMapa.center = coordenadas;
                var config = {
                    map: gMapa,
                    position: coordenadas,
                    draggable: true
                };
                gMarker = new google.maps.Marker(config);

                $("#txtLatutud").val(gMarker.getPosition().lat());
                $("#txtLongitud").val(gMarker.getPosition().lng());
                google.maps.event.addListener(marker, 'click', function () {
                    setLatitudLongitudMarker(marker);
                });
            };
        });
    }


}

function setLatitudLongitudMarker(marker) {
    $("#txtLatutud").val(marker.getPosition().lat());
    $("#txtLongitud").val(marker.getPosition().lng());
}






//function ubicarDireccionEnMapa() {
//    
//    if (input) {
//         

//        //gMarker.setMap(gMapa);
//        
//        //Obtengo el objeto PlaceResult, que tiene información del lugar cuya dirección se seleccionó
//        var place = autocomplete.getPlace();

//        var gCoder = new google.maps.Geocoder();
//        var objInformacion = {
//            address: place.name
//        };

//        gCoder.geocode(objInformacion, fn_coder);

//        function fn_coder(datos) {
//            var coordenadas = datos[0].geometry.location; // es un objeto LatLng de google.
//            //gMapa.center = coordenadas;
//            var config = {
//                map: gMapa,
//                position: coordenadas,
//                draggable: true
//            };
//            gMarker  = new google.maps.Marker(config);

//            $("#txtLatutud").val(gMarker.getPosition().lat());
//            $("#txtLongitud").val(gMarker.getPosition().lng());
//            google.maps.event.addListener(gMarker, 'click', function () {
//                setLatitudLongitudMarker(gMarker);
//            });
//        };
//         
//    }
//} 



/**  Son 2 formas de mostrar la latutud y longitud de un punto en el google maps.  **/

/** 1. ASIGNAR UN PUNTO EN EL MAPA INDICANDO LA DIRECCIÓN DE UN TXTBOX **/

/** 2. ASIGNAR UN PUNTO HACIENDO CLICK EN EL MAPA  **/




/**  ASIGNAR UN PUNTO EN EL MAPA INDICANDO LA DIRECCIÓN DE UN TXTBOX **/
//namespace --> google.maps.ALGO
//var divMapa = document.getElementById('mapa');

// 
//var gMarker = null; //puntero que se utiliza en el mapa. Objeto de google.maps


////por defecto : UBICACIÓN DE LA CIUDAD DE CÓRDOBA. 
//var lat = '-31.42008329999999';
//var lon = '-64.18877609999998';

////esta información para el mapa dinamico debemos convertirlo en un objeto.

////creo un objeto de google para esas coordenadas
//var gLatLon = new google.maps.LatLng(lat, lon);

////creamos el mapa
//var objConfig = {
//    zoom: 17,
//    center: gLatLon
//};
////renderizo e indico donde va el mapa y que configuración debe tener el mapa.
//var gMapa = new google.maps.Map(divMapa, objConfig);
//var objConfigMarker = {                    
//    position : gLatLon ,
//    map :gMapa ,
//    draggable: true,
//    title : "USTED ESTÁ ACÁ."
//};
//var gMarker = new google.maps.Marker(objConfigMarker);


//var input = document.getElementById('_txtDireccion');
////var input = '_txtDireccion';

//if (input) {
//    autocomplete = new google.maps.places.Autocomplete(input);

//    //Agrego evento para que cuando se seleccione una dirección, se coloque allí un marcador y se centre el mapa en ese lugar
//    google.maps.event.addListener(autocomplete, 'place_changed', function () {

//        //Obtengo el objeto PlaceResult, que tiene información del lugar cuya dirección se seleccionó
//        var place = autocomplete.getPlace().name;

//        var gCoder = new google.maps.Geocoder();
//        var objInformacion = {
//            address: place
//        };

//        gCoder.geocode(objInformacion, fn_coder);

//        function fn_coder(datos) {
//            var coordenadas = datos[0].geometry.location; // es un objeto LatLng de google.
//            gMapa.center = coordenadas;
//            var config = {
//                map: gMapa,
//                position: coordenadas,
//                draggable: true
//            };
//            gMarker = new google.maps.Marker(config);

//            $("#txtLatutud").val(gMarker.getPosition().lat());
//            $("#txtLongitud").val(gMarker.getPosition().lng());
//            google.maps.event.addListener(marker, 'click', function () {
//                setLatitudLongitudMarker(marker);
//            });
//        };
//    });
//}

// 

//function setLatitudLongitudMarker(marker) {
//    $("#txtLatutud").val(marker.getPosition().lat());
//    $("#txtLongitud").val(marker.getPosition().lng());
//}


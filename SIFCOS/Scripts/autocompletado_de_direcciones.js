/***** Autocompletado de direcciones con Google *****/

//Indico en que control funcionará el autocompletado de direcciones
//var input = document.getElementById($('input[id$=_txtDireccion]').attr('id'));
var input = document.getElementById('_txtDireccion');
//var input = '_txtDireccion';


var gMarker = null;		
if (input) {
	autocomplete = new google.maps.places.Autocomplete(input);
		   
	//Agrego evento para que cuando se seleccione una dirección, se coloque allí un marcador y se centre el mapa en ese lugar
	google.maps.event.addListener(autocomplete, 'place_changed', function () {
		
		//Obtengo el objeto PlaceResult, que tiene información del lugar cuya dirección se seleccionó
		var place = autocomplete.getPlace().name;

		//Guardo las coordenadas en el hidden
	   // $('#_hdnUbicacionMaps').val(place.geometry.location.toString());
		//$('input[id$=_hdnUbicacionMaps]').val(place.geometry.location.toString());

	    var gCoder = new google.maps.Geocoder();
	    var objInformacion = {
	        address: place
	    };
	    
	    gCoder.geocode(objInformacion, fn_coder);

	    function fn_coder(datos) {
	        var coordenadas = datos[0].geometry.location; // es un objeto LatLng de google.
	        var config = {
	            map: gMapa,
	            position: coordenadas
	        };
	        gMarker = new google.maps.Marker(config);
	    };
	});


	google.maps.event.addListener(gMarker, 'mouseup', function () {
	    openInfoWindow(gMarker);
	});
    
}
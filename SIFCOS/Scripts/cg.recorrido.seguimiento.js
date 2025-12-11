var SeguimientoTecnico = {
    map: null,
    mapOptions: {
        zoom: 15,
        mapTypeControl: false,
        streetViewControl: false,
        disableDefaultUI: true,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        center: new google.maps.LatLng(-31.432720, -64.161657)
    },
    markers: null,
    maxDistance: 1500,

    init: function (maxDistance) {
        SeguimientoTecnico.maxDistance = maxDistance || SeguimientoTecnico.maxDistance;

        $('.input-daterange').datepicker({ language: "es" });
        $('.input-daterange input, .form-control').removeAttr('style');

        $('[title]').tooltip({ container: 'body' });

        $('.panel-flotante').not('#panelMapa, #panelOverlay').each(function() {
            var height = $(this).innerHeight() - 110;
            $(this).find('.panel-contenido-scrollable').css('max-height', height + 'px');
        });

        var canvas = document.getElementById('map-canvas');
        SeguimientoTecnico.map = new google.maps.Map(canvas, SeguimientoTecnico.mapOptions);

        SeguimientoTecnico.initRecorrido();
        SeguimientoTecnico.initMarcadores();

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, e) {
            $('.input-daterange').datepicker({ language: "es" });

            Timeliner.initTabs();
            setTimeout(Timeliner.init, 2000);
        });
    },

    initRecorrido: function () {
        var bounds = new google.maps.LatLngBounds();
        var coords = $('input[id$=hdnCoordenadasRecorrido]').val().split(';');

        var pointPrev = null;
        var distance = 0;
        var dashed = false;
        var poly = SeguimientoTecnico.crearPolyline();

        for (var j = 0; j < coords.length; j++) {
            if (!coords[j]) {
                return;
            }

            var point = SeguimientoTecnico.crearPunto(coords[j]);

            if (pointPrev) {
                distance = google.maps.geometry.spherical.computeDistanceBetween(pointPrev, point);
            }

            if ((distance > SeguimientoTecnico.maxDistance && !dashed) || (distance <= SeguimientoTecnico.maxDistance && dashed)) {
                dashed = distance > SeguimientoTecnico.maxDistance;
                poly = SeguimientoTecnico.crearPolyline(dashed);
                poly.getPath().push(pointPrev);
            }

            poly.getPath().push(point);
            bounds.extend(point);

            pointPrev = point;
        }

        SeguimientoTecnico.map.fitBounds(bounds);

        var listener = google.maps.event.addListener(SeguimientoTecnico.map, "idle", function () {
            if (SeguimientoTecnico.map.getZoom() > 15) {
                SeguimientoTecnico.map.setZoom(15);
            }
            google.maps.event.removeListener(listener);
        });
    },

    crearPolyline: function(dashed) {
        var poly = new google.maps.Polyline({
            strokeColor: '#0e2c52',
            strokeOpacity: dashed ? 0 : 1,
            strokeWeight: dashed ? 0 : 3,

            icons: dashed ? [{
                icon: {
                    path: 'M 0,-1 0,1',
                    strokeOpacity: 1,
                    scale: 3
                },
                offset: '0',
                repeat: '15px'
            }] : null,

            map: SeguimientoTecnico.map
        });

        return poly;
    },

    initMarcadores: function () {
        // Centrar mapa cuando se hace click en el icono de la linea de tiempo
        $('[data-posicion]').on('click', function() {
            var pos = $(this).attr('data-posicion');
            var pto = SeguimientoTecnico.crearPunto(pos);

            SeguimientoTecnico.map.panTo(pto);
        });

        $('[data-posicion]').each(function() {
            SeguimientoTecnico.crearMarcador(this);
        });
    },

    crearMarcador: function (e) {
        // https://developers.google.com/chart/image/docs/gallery/dynamic_icons#pins

        var color = $(e).parent().css('backgroundColor');
        
        var icon = new google.maps.MarkerImage('http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|' + color,
            new google.maps.Size(21, 34),
            new google.maps.Point(0, 0),
            new google.maps.Point(10, 34));

        var info = $(e).parents('.cd-timeline-block').find('h2').text() + '<br/>';
        info += '<small>' + $(e).parents('.cd-timeline-block').find('.cd-date').text() + '</small>';

        var marker = new google.maps.Marker({
            map: SeguimientoTecnico.map,
            position: SeguimientoTecnico.crearPunto($(e).attr('data-posicion')),
            icon: icon,
            info: new google.maps.InfoWindow({
                content: info
            })
        });

        google.maps.event.addListener(marker, 'click', function () {
            marker.info.open(SeguimientoTecnico.map, marker);
        });
    },

    crearPunto: function (coordenadas) {
        var par = coordenadas.split(',');

        var latitud = parseFloat(par[0]);
        var longitud = parseFloat(par[1]);

        return new google.maps.LatLng(latitud, longitud);
    },

    abrirPanel: function (panel, updatePanel, referencia) {
        $('.panel-flotante:not(#panelMapa)').hide();
        if (panel === 'panelMapa') {
            $('#panelOverlay').hide();
        } else {
            $('#panelOverlay').show();
            $('#' + panel).fadeIn();
        }

        if (panel == 'panelLocacion' && updatePanel && referencia) {
            __doPostBack(updatePanel, referencia);
        }
    }
};

$.cssHooks.backgroundColor = {
    get: function (elem) {
        if (elem.currentStyle)
            var bg = elem.currentStyle["backgroundColor"];
        else if (window.getComputedStyle)
            var bg = document.defaultView.getComputedStyle(elem,
                null).getPropertyValue("background-color");
        if (bg.search("rgb") == -1)
            return bg;
        else {
            bg = bg.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
            function hex(x) {
                return ("0" + parseInt(x).toString(16)).slice(-2);
            }
            return /*"#" + */hex(bg[1]) + hex(bg[2]) + hex(bg[3]);
        }
    }
}
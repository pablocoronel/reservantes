function inicialar() {

    var mapCanvas = document.getElementById('map-canvas');


    var mapOptions = {
        center: new google.maps.LatLng(-34.6290089, -58.4071996),
        zoom: 12,
        //mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    }

    var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);

    var data = [
        { "Id": 1, "PlaceName": "Casa Rosada Restaurant", "GeoLong": "-34.6080556", "GeoLat": "-58.3702778" },
        //{ "Id": 2, "PlaceName": "McDonalds", "GeoLong": "-34.6088308783192", "GeoLat": "-58.37559700012207" },
        { "Id": 3, "PlaceName": "Museo del Jamon", "GeoLong": "-34.6083363769076", "GeoLat": "-58.38213086128235" },

    ];

    $.each(data, function (i, item) {
        var marker = new google.maps.Marker({
            'position': new google.maps.LatLng(item.GeoLong, item.GeoLat),
            'map': map,
            'title': item.PlaceName
        });

        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png')

        var infowindow = new google.maps.InfoWindow({
            content: "<div class='infoDiv'><h3>" + item.PlaceName + "</div></div>"
        });

        google.maps.event.addListener(marker, 'click', function () {
            infowindow.open(map, marker);
        });

    })

    var input = document.getElementById('pac-input');
    var searchBox = new google.maps.places.SearchBox(input);

    // Bias the SearchBox results towards current map's viewport.
    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });

    var markers = [];
    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        // Clear out the old markers.
        markers.forEach(function (marker) {
            marker.setMap(null);
        });
        markers = [];

        // For each place, get the icon, name and location.
        var bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {
            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }
            var icon = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            markers.push(new google.maps.Marker({
                map: map,
                icon: icon,
                title: place.name,
                position: place.geometry.location
            }));

            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });
        map.fitBounds(bounds);
    });

}



//<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAptrGun-vHjygAsSermQUGiDvxOGQ_qJ8&libraries=places&callback=inicialar" async defer</script>

<script src='http://maps.google.com/maps/api/js?key=AIzaSyCj0UbeSz2p0umVfycFjdqHJSKAS_cdLog&sensor=false&libraries=places' type="text/javascript"></script>


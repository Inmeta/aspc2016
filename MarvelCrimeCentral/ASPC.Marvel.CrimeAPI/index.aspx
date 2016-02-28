<!DOCTYPE html>
<html>
<head>
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="refresh" content="5">
    <script src="wwwroot/lib/jquery/dist/jquery.js"></script>
    <script src="wwwroot/lib/angular/angular.js"></script>

    <style>
        html, body { margin: 0; }
    </style>
</head>
<body>
    <article>
        <div id="map" style="width:100%; height:100%; position:absolute"></div>
    </article>

    <script>
        var map;
        var markers = [];

      function initMap() {
        map = new google.maps.Map(document.getElementById('map'), {
          center: {lat: 59.9, lng: 10.4},
          zoom: 12
        });

        if (navigator.geolocation)
        {
            navigator.geolocation.getCurrentPosition(success, error);
        }

        setInterval(function () {
            $.getJSON("/odata/Node", function (res) { update(res) });
        }, 3000);


      }


      function update(result) {
          console.log(result);
          deleteMarkers();
          $.each(result.value, function (i, field) {

              var url = '/ccu.png';
              if (field['odata.type'] == "ASPC.Marvel.CrimeAPI.Crime") { url = '/crime.jpg'; }

                  var marker = new google.maps.Marker({
                      id: field.Id,
                      position: { lat: field.Location.Latitude, lng: field.Location.Longitude },
                      map: map,
                      title: field.Name,
                      icon: url
                  });

                  markers.push(marker);

                  google.maps.event.addListener(marker, "click", function (e) {

                      if (field['odata.type'] == "ASPC.Marvel.CrimeAPI.Crime") {
                          var infoWindow = new google.maps.InfoWindow({
                              content:
                                  '<table>' +
                                  '<tr><td>Type</td><td>'+field.Name+'</td></tr>' +
                                  '<tr><td>Area Crime Index</td><td>'+field.CrimesPerYear+'</td></tr>' +
                                  '<tr><td>Latitude</td><td>'+field.Location.Latitude+'</td></tr>' +
                                  '<tr><td>Longitude</td><td>'+field.Location.Longitude+'</td></tr>' +
                                  '</table>'
                          });
                      }
                      else {
                          var infoWindow = new google.maps.InfoWindow({ 
                              content:
                                  '<table>' +
                                  '<tr><td>Agent</td><td>'+field.Name+'</td></tr>' +
                                  '<tr><td>BPM</td><td>'+field.BPM+'</td></tr>' +
                                  '<tr><td>UV</td><td>'+field.UV+'</td></tr>' +
                                  '<tr><td>GSR</td><td>'+field.GSR+'</td></tr>' +
                                  '<tr><td>Atm psi</td><td>' + field.Barometer + '</td></tr>' +
                                  '<tr><td>Latitude</td><td>'+field.Location.Latitude+'</td></tr>' +
                                  '<tr><td>Longitude</td><td>'+field.Location.Longitude+'</td></tr>' +
                                  '</table>'
                          });
                      }
                      infoWindow.open(map, marker);
                  });

          });
      }

      function findById(source, id) {
          for (var i = 0; i < source.length; i++) {
              if (source[i].id === id) {
                  return source[i];
              }
          }
          return null;
      }
      function setMapOnAll(map) {
          for (var i = 0; i < markers.length; i++) {
              markers[i].setMap(map);
          }
      }

      // Removes the markers from the map, but keeps them in the array.
      function clearMarkers() {
          setMapOnAll(null);
      }

      function deleteMarkers() {
          clearMarkers();
          markers = [];
      }

      function success(position)
      {
          console.log(position);
          map.setCenter({ lat: position.coords.latitude, lng: position.coords.longitude });
      }

      function error(msg)
      {
          console.log(msg);
      }

    </script>

    <script src="https://maps.google.com/maps/api/js?callback=initMap" async defer></script>
</body>
</html>

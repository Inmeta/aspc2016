<!DOCTYPE html>
<html>
<head>
    <title></title>
    <meta charset="utf-8" />

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
      function initMap() {
        map = new google.maps.Map(document.getElementById('map'), {
          center: {lat: 59.9, lng: 10.4},
          zoom: 12
        });

        if (navigator.geolocation)
        {
            navigator.geolocation.getCurrentPosition(success, error);
        }

        $.getJSON("/odata/Node", function (result) {
            console.log(result);
            $.each(result.value, function (i, field) {

                var url = '/ccu.png';
               if (field['odata.type'] == "ASPC.Marvel.CrimeAPI.Crime") { url = '/crime.jpg'; }

                var marker = new google.maps.Marker({
                    position: { lat: field.Location.Latitude, lng: field.Location.Longitude },
                    map: map,
                    title: field.Name,
                    icon: url
                });
            });
        });
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

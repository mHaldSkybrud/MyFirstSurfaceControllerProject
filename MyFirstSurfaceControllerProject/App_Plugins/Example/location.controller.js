angular.module("umbraco").controller("Example.LocationController",

    function ($scope, assetsService, notificationsService) 

        //API
        assetsService.loadJs("http://www.google.com/jsapi")
            .then(function () {

                google.load("maps", "3",
                    {
                        callback: initialize,
                        other_params: "key=AIzaSyCP-eDlmOnA_qswZSohznypAB3MrTSktiM"
                    });
            });

        //Method that renders map, marker and search field
        function initialize() {
            //Map
            var myLatLng = new google.maps.LatLng($scope.model.value.latitude, $scope.model.value.longitude);
            var mapOptions = new google.maps.Map(document.getElementById('map'), {
                zoom: 4,
                center: myLatLng
            });

            //Marker
            var marker = new google.maps.Marker({
                position: myLatLng,
                map: mapOptions,
                title: 'Hello World!',
                draggable: true
            });

            //Update lat and longitude upon marker movement
            google.maps.event.addListener(marker, "position_changed", function (e) {

                $scope.model.value.latitude = marker.getPosition().lat();
                $scope.model.value.longitude = marker.getPosition().lng();
            });

            //Search field
            var geocoder = new google.maps.Geocoder();

            document.getElementById('submit').addEventListener('click', function () {
                geocodeAddress(geocoder, map);
            });
        }

        //Search field functionality - doesn't work .
        function geocodeAddress(geocoder, resultsMap) {
            var address = document.getElementById('address').value;
            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status == 'OK') {
                    resultsMap.setCenter(results[0].geometry.location);
                    var marker = new google.maps.Marker({
                        map: resultsMap,
                        position: results[0].geometry.location
                    });
                }
                //Example of usage of notificationsService rather than just an alert
                else {
                    notificationsService.error("Geocode was not successful due to", status);
                }
            });
        }
    });



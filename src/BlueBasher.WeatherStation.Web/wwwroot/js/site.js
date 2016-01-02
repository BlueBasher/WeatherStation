var map = function () {
    var
        bingMap = null,
        pinInfobox = null,
        initialize = function (bingCredentials) {
            // Initialize the map
            try {
                bingMap = new Microsoft.Maps.Map(document.getElementById("map"),
                    {
                        credentials: bingCredentials,
                        mapTypeId: Microsoft.Maps.MapTypeId.road
                    });
                pinInfobox = new Microsoft.Maps.Infobox(new Microsoft.Maps.Location(0, 0), { visible: false });
                Microsoft.Maps.Events.addHandler(bingMap, 'viewchange', hideInfobox);
                bingMap.entities.push(pinInfobox);

                var geoLocationProvider = new Microsoft.Maps.GeoLocationProvider(bingMap);
                geoLocationProvider.getCurrentPosition({ successCallback: ZoomIn });

                function ZoomIn(args) {
                    bingMap.setView({
                        zoom: 5,
                        center: args.center
                    });
                }

                $.getJSON('api/values', null, function (locations) {
                    $.each(locations, function (index, location) {
                        var pushpin = new Microsoft.Maps.Pushpin(
                            new Microsoft.Maps.Location(location.Latitude, location.Longitude));
                        pushpin.deviceId = location.DeviceId;
                        pushpin.temperature = location.Temperature;
                        pushpin.setOptions({
                            visible: true
                        });
                        Microsoft.Maps.Events.addHandler(pushpin, 'click', displayInfobox);
                        bingMap.entities.push(pushpin);
                    });
                });
            }
            catch (err) {
                alert(err.message);
            }
        },
    displayInfobox = function (e) {
        pinInfobox.setOptions({
            offset: new Microsoft.Maps.Point(0, 25),
            visible: true
        });
        pinInfobox.setLocation(e.target.getLocation());
        var html = '<div id="infoboxText"';
        html = html + 'style="background-color:White; border-style:solid;border-width:medium; border-color:DarkOrange; min-height:100px; ';
        html = html + 'position:absolute;top:0px; left:23px; width:240px;">';
        html = html + '<b id="infoboxTitle" style="position:absolute; top:10px; left:10px; width:220px;">';
        html = html + e.target.deviceId + '</b>';
        html = html + '<a id="infoboxDescription" style="position:absolute; top:30px; left:10px; width:220px;">';
        html = html + e.target.temperature + ' deg C</a></div>';
        pinInfobox.setHtmlContent(html);
    },
    hideInfobox = function (e) {
        pinInfobox.setOptions({ visible: false });
    }

    return {
        initialize: initialize
    }
}();
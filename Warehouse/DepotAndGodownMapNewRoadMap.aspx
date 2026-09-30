<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepotAndGodownMapNewRoadMap.cs" Inherits="DepotAndGodownMapNewRoadMap" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>MP Road Network - 200km Proximity</title>
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css" />
    <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <style>
        body { font-family: 'Segoe UI', sans-serif; margin: 0; background: #f4f4f4; }
        .header { background: #1b5e20; color: white; padding: 15px; display: flex; justify-content: space-between; align-items: center; }
        #map { height: calc(100vh - 70px); width: 100%; border-top: 3px solid #1b5e20; }
        .btn-reset { background: #ffd600; color: #333; border: none; padding: 8px 15px; font-weight: bold; cursor: pointer; border-radius: 4px; }
        .road-tip { background: #2e7d32; color: white; padding: 5px 10px; border-radius: 4px; font-weight: bold; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        
        <div class="header">
            <div>
                <h3 style="margin:0;">MP Depot Road Map</h3>
                <span id="status">Click a depot to see routes within 200 km</span>
            </div>
            <button type="button" class="btn-reset" onclick="resetToFullMap()">Reset to Full MP View</button>
        </div>

        <div id="map"></div>

        <script type="text/javascript">
            var map, markerLayer, routeLayer, allDepots = [];
            // Strictly lock the view to Madhya Pradesh coordinates
            var mpBounds = [[17.8, 74.0], [26.9, 82.9]];

            $(document).ready(function () {
                map = L.map('map', {
                    maxBounds: mpBounds, // Lock to MP 
                    maxBoundsViscosity: 1.0
                }).setView([23.2599, 77.4126], 7);

                L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                    attribution: '© OpenStreetMap contributors',
                    minZoom: 6
                }).addTo(map);

                markerLayer = L.layerGroup().addTo(map);
                routeLayer = L.layerGroup().addTo(map);

                loadDepots();
            });

            function loadDepots() {
                PageMethods.GetDepots(function (response) {
                    allDepots = JSON.parse(response);
                    showStateOverview();
                });
            }

            function showStateOverview() {
                markerLayer.clearLayers();
                routeLayer.clearLayers();
                $('#status').text("State Overview: Click a red marker.");

                allDepots.forEach(function (d) {
                    var m = L.circleMarker([parseFloat(d.latitude), parseFloat(d.longitude)], {
                        radius: 8, color: '#d32f2f', fillColor: '#f44336', fillOpacity: 0.8
                    }).addTo(markerLayer);

                    m.bindTooltip(d.DepotName);
                    m.on('click', function () { filterByDistance(d); });
                });
            }

            function filterByDistance(origin) {
                markerLayer.clearLayers();
                routeLayer.clearLayers();
                $('#status').html("Showing <b>200km</b> road network from: " + origin.DepotName);

                var oLat = parseFloat(origin.latitude);
                var oLng = parseFloat(origin.longitude);
                var zoomBounds = [[oLat, oLng]];

                // Add Origin Marker (Primary)
                L.marker([oLat, oLng]).addTo(markerLayer).bindPopup("<b>" + origin.DepotName + "</b> (Origin)").openPopup();

                allDepots.forEach(function (target) {
                    if (target.DepotID === origin.DepotID) return;

                    var tLat = parseFloat(target.latitude);
                    var tLng = parseFloat(target.longitude);

                    // OSRM API Call for Road Distances 
                    var osrmUrl = `https://router.project-osrm.org/route/v1/driving/${oLng},${oLat};${tLng},${tLat}?overview=full&geometries=geojson`;

                    fetch(osrmUrl)
                        .then(res => res.json())
                        .then(data => {
                            if (data.routes && data.routes.length > 0) {
                                var route = data.routes[0];
                                var distKm = (route.distance / 1000).toFixed(2);

                                // APPLY 200KM RADIUS FILTER
                                if (distKm <= 200) {
                                    zoomBounds.push([tLat, tLng]);

                                    // Draw Road
                                    var path = L.geoJSON(route.geometry, {
                                        style: { color: '#2e7d32', weight: 5, opacity: 0.7 }
                                    }).addTo(routeLayer);

                                    // Distance on Hover 
                                    path.bindTooltip("Road Distance: " + distKm + " km", {
                                        sticky: true, className: 'road-tip'
                                    });

                                    // Add neighbor marker
                                    L.circleMarker([tLat, tLng], { radius: 6, color: '#01579b' })
                                        .addTo(markerLayer)
                                        .bindTooltip(target.DepotName + " (" + distKm + " km)");
                                }
                            }
                        });
                });

                // Auto-zoom into the 200km cluster 
                setTimeout(function () {
                    if (zoomBounds.length > 1) { map.fitBounds(zoomBounds, { padding: [80, 80] }); }
                }, 1000);
            }

            function resetToFullMap() {
                showStateOverview();
                map.setView([23.2599, 77.4126], 7);
            }
        </script>
    </form>
</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepotAndGodownMap.aspx.cs" Inherits="DepotAndGodownMap" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Depot & Godown Map Analysis</title>
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css" />
    <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <style>
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 20px; }
        #map { height: 600px; width: 100%; border-radius: 8px; border: 1px solid #ccc; }
        .controls { margin-bottom: 15px; }
        .btn-back { padding: 10px 20px; background-color: #2196F3; color: white; border: none; cursor: pointer; border-radius: 4px; display: none; }
        .btn-back:hover { background-color: #0b7dda; }
        #detailsTable { margin-top: 20px; width: 100%; border-collapse: collapse; }
        #detailsTable th, #detailsTable td { border: 1px solid #ddd; padding: 8px; text-align: left; }
        #detailsTable th { background-color: #f2f2f2; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        
        <h2>Depot to Godown Distance Mapping</h2>
        
        <div class="controls">
            <button type="button" id="btnBack" class="btn-back" onclick="loadMainMap()">← Back to All Depots</button>
        </div>

        <div id="map"></div>

        <div id="infoPanel">
            <h3 id="tableTitle">Depot Summary</h3>
            <div id="results"></div>
        </div>

        <script type="text/javascript">
            var map;
            var markerLayer = L.layerGroup();
            var lineLayer = L.layerGroup();

            $(document).ready(function () {
                // Initialize Map centered on India/Your region
                map = L.map('map').setView([22.9734, 78.6569], 5);
                L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                    attribution: '&copy; OpenStreetMap contributors'
                }).addTo(map);

                markerLayer.addTo(map);
                lineLayer.addTo(map);

                loadMainMap();
            });

            // State 1: Load all Depots from Database
            function loadMainMap() {
                markerLayer.clearLayers();
                lineLayer.clearLayers();
                $('#btnBack').hide();
                $('#tableTitle').text("All Depots Summary");
                $('#results').html("Loading depots...");

                PageMethods.GetDepots(function (response) {
                    var depots = JSON.parse(response);
                    renderDepotTable(depots);

                    depots.forEach(function (d) {
                        if (d.latitude && d.longitude) {
                            var marker = L.circleMarker([d.latitude, d.longitude], {
                                radius: 8, color: '#2196F3', fillOpacity: 0.8
                            }).addTo(markerLayer);

                            // Hover Tooltip: Show all columns dynamically
                            var tooltipContent = "<div style='line-height:1.5;'>";
                            for (var key in d) {
                                tooltipContent += "<b>" + key + ":</b> " + d[key] + "<br/>";
                            }
                            tooltipContent += "</div>";
                            marker.bindTooltip(tooltipContent);

                            // Click Event: Drill down
                            marker.on('click', function () {
                                loadGodownDistanceMap(d);
                            });
                        }
                    });
                }, onFail);
            }

            // State 2: Load Specific Depot and its Godowns with lines
            function loadGodownDistanceMap(depot) {
                markerLayer.clearLayers();
                lineLayer.clearLayers();
                $('#btnBack').show();
                $('#tableTitle').text("Godowns under Depot: " + depot.DepotName);
                $('#results').html("Calculating distances...");

                // Add Depot Marker
                var depotMarker = L.marker([depot.latitude, depot.longitude]).addTo(markerLayer);
                depotMarker.bindPopup("<b>" + depot.DepotName + "</b> (Base Depot)").openPopup();

                PageMethods.GetGodownsByDepot(depot.DepotID.toString(), function (response) {
                    var godowns = JSON.parse(response);
                    var bounds = [[depot.latitude, depot.longitude]];

                    if (godowns.length === 0) {
                        $('#results').html("<p style='color:red;'>No godowns linked to this depot in database.</p>");
                        return;
                    }

                    renderGodownTable(godowns);

                    godowns.forEach(function (g) {
                        var gLat = parseFloat(g.Latitude);
                        var gLng = parseFloat(g.Longitude);

                        // 1. Add Godown Marker
                        var gMarker = L.circleMarker([gLat, gLng], { radius: 5, color: 'red' }).addTo(markerLayer);
                        gMarker.bindTooltip("<b>" + g.Godown_Name + "</b><br/>Dist: " + g.distance + " km");

                        // 2. Draw Distance Line
                        L.polyline([[depot.latitude, depot.longitude], [gLat, gLng]], {
                            color: 'green', weight: 2, opacity: 0.5, dashArray: '5, 10'
                        }).addTo(lineLayer);

                        bounds.push([gLat, gLng]);
                    });

                    map.fitBounds(bounds, { padding: [50, 50] });
                }, onFail);
            }

            function renderDepotTable(data) {
                var html = "<table id='detailsTable'><tr><th>ID</th><th>Depot Name</th><th>Address</th></tr>";
                data.forEach(function (d) {
                    html += "<tr><td>" + d.DepotID + "</td><td>" + d.DepotName + "</td><td>" + d.DepotAddress + "</td></tr>";
                });
                html += "</table>";
                $('#results').html(html);
            }

            function renderGodownTable(data) {
                var html = "<table id='detailsTable'><tr><th>Godown ID</th><th>Godown Name</th><th>Type</th><th>Distance (km)</th></tr>";
                data.forEach(function (g) {
                    html += "<tr><td>" + g.Godown_ID + "</td><td>" + g.Godown_Name + "</td><td>" + g.Hired_Type + "</td><td>" + g.distance + "</td></tr>";
                });
                html += "</table>";
                $('#results').html(html);
            }

            function onFail(err) {
                alert("Error: " + err.get_message());
            }
        </script>
    </form>
</body>
</html>
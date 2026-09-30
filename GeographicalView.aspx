<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="GeographicalView.aspx.cs" Inherits="GeographicalView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type"text/css">
 #map{
      height: 600px;
      width: 100%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="banner" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
<section class="content_wrapper">
        <div class="container">
        <!-- Example row of columns -->
          <div class="row">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <div class="col-md-3">
       <div class="list-group">
          <h4 class="list-group-item active">CONTACT INFORMATION</h4>
          <a href="Contact.aspx" class="list-group-item">CORPORATE OFFICE </a>
          <a href="RegionalContact.aspx" class="list-group-item">REGIONAL OFFICES</a>
          <a href="BranchContact.aspx" class="list-group-item">BRANCH OFFICES</a>
          <a href="GeographicalView.aspx" class="list-group-item">GMAP MPWLC</a>

        </div>
    </div>
    <div class="col-md-9">
        <div class="row-fluid">
            <h3 class="red" style="letter-spacing:1px;">GMAP MPWLC</h3> <hr class="line-red"/>
          
           <div id="map"></div>
         <br />
         <br />
        </div>
     </div>
     <%-- <script type='text/javascript'> function initMap() {var options = {zoom: 7, center: { lat: 23.999994, lng: 77.947998 } }; var map = new google.maps.Map(document.getElementById('map'), options); function addMarker(prop) {var marker = new google.maps.Marker({position: prop.coordinates, map: map,  draggarble: false }); if (prop.iconImage) { marker.setIcon(prop.iconImage); } if (prop.content) {var information = new google.maps.InfoWindow({content: prop.content }); marker.addListener('click', function () {information.open(map, marker); }); } }addMarker({coordinates:{ lat: 23.790563, lng: 78.397933 },iconImage: 'assets/img/gpin.png',content: '<h4>Rahatgarh</h4><p><i>Surya Prakash Jain</i></p><p>choudhary dharmkanta
Rahatgarh</p>' });addMarker({coordinates: { lat: 23.236460, lng: 77.440936 }, iconImage: 'assets/img/Hoffice.png', content: '<h4>M.P. Warehousing And Logistics Corporation</h4><p><b>HEAD OFFICE</b></p><p>Offic Complex, Block A Gautam Nagar, Bhopal - 462023</p>'}); } </script>--%>
   <%-- <script type="text/javascript">
        function initMap() {
            var options = {
                zoom: 6,
                center: { lat: 23.473324, lng: 77.947998} //Coordinates of Madhya Pradesh 
            }
            var map = new google.maps.Map(document.getElementById('map'), options);

            function addMarker(prop) {
                var marker = new google.maps.Marker({
                    position: prop.coordinates, // Passing the coordinates
                    map: map, //Map that we need to add
                    draggarble: false// If set to true you can drag the marker
                });
                if (prop.iconImage) { marker.setIcon(prop.iconImage); }
                if (prop.content) {
                    var information = new google.maps.InfoWindow({
                        content: prop.content
                    });

                    marker.addListener('click', function () {
                        information.open(map, marker);
                    });
                }
            }

            //            addMarker({
            //                coordinates: { lat: 40.6782, lng: -73.9442 },
            //                iconImage: 'https://img.icons8.com/fluent/48/000000/marker-storm.png',
            //                content: '<h4>Brooklyn Marker</h4>'
            //            });
            // addMarker({ coordinates: { lat: 40.7831, lng: -73.9712} }); // Manhattan Coordinates

        }
    </script>--%>
    
          </div>

         </div> <!-- /container -->
   </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="widget" Runat="Server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" Runat="Server">
  
     <script async defer src="https://maps.googleapis.com/maps/api/js?v=3.exp&callback=initMap"></script>


</asp:Content>


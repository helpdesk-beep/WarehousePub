<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/MMaster.master" AutoEventWireup="true" CodeFile="GodownCordinates2.aspx.cs" Inherits="Mobile_GodownCordinates2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript">
         function validateData() {
             //clear textbox
             return true;
         }
    </script>
<script type="text/javascript"
src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBD06GL1pqqHCbfbKlrw8D6mOEvLszZP2o&sensor=false">
</script>
<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&libraries=places">
</script>
<script type="text/javascript">
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(success);
    } else {
        alert("Geo Location is not supported on your current browser!");
    }
    function success(position) {
        var lat = position.coords.latitude;
        var long = position.coords.longitude;
        var city = position.coords.locality;
        var myLatlng = new google.maps.LatLng(lat, long);
        var myOptions = {
            center: myLatlng,
            zoom: 12,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("map_load"), myOptions);
        var marker = new google.maps.Marker({
            position: myLatlng,
            title: "lat: " + lat + " long: " + long
        });

        marker.setMap(map);
        var infowindow = new google.maps.InfoWindow({ content: "<b>User Address</b><br/> Latitude:" + lat + "<br /> Longitude:" + long + "" });
        infowindow.open(map, marker);
        document.getElementById("txtlet").value = lat;
        document.getElementById("n_test").value = lat;
        document.getElementById("txtlong").value = long;
        document.getElementById("n_long").value = long;
    }
</script>
    <div id="map_load" style="width: 300px; height: 300px"></div>
    <table>
        <tr>
            <td>
                Godown Name:
            </td>
            <td>
                <asp:dropdownlist id="ddlgodown" runat="server"></asp:dropdownlist>
            </td>
        </tr>
        <tr>
            
            <td>Letitude:</td>
            <td>
                 <input id="txtlet"  type="text" class="radius2" />
                <input type="hidden" id="n_test" name="n_test" />
            </td>
        </tr>
         <tr>
            
            <td>Longitude:</td>
            <td>
             <input id="txtlong" type="text"  class="radius2" />
                 <input type="hidden" id="n_long" name="n_long" />
            </td>
        </tr>
        <tr>
            <td>

            </td>
            <td>
                 
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="btn-primary" OnClick="btnsubmit_Click" OnClientClick="return validateData();"/>
                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/MMaster.master" AutoEventWireup="true" CodeFile="GodwonCordinates.aspx.cs" Inherits="Mobile_GodwonCordinates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function validateData() {
            //clear textbox
            return true;
        }
    </script>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
<script type="text/javascript">
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (p) {
            var LatLng = new google.maps.LatLng(p.coords.latitude, p.coords.longitude);
            var mapOptions = {
                center: LatLng,
                zoom: 14,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
            var marker = new google.maps.Marker({
                position: LatLng,
                map: map,
                title: "<div style = 'height:60px;width:200px'><b>Your location:</b><br />Latitude: " + p.coords.latitude + "<br />Longitude: " + p.coords.longitude

            });
            google.maps.event.addListener(marker, "click", function (e) {
                var infoWindow = new google.maps.InfoWindow();
                infoWindow.setContent(marker.title);
                infoWindow.open(map, marker);
                document.getElementById("txtlet").value = p.coords.latitude;
                document.getElementById("n_test").value = p.coords.latitude;
                document.getElementById("txtlong").value = p.coords.longitude;
                document.getElementById("n_long").value = p.coords.longitude;
            });
        });
    } else {
        alert('Geo Location feature is not supported in this browser.');
    }
</script>
<div id="dvMap" style="width: 300px; height: 300px">
</div>
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


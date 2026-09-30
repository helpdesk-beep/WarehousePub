using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;
using System.Text.RegularExpressions;

public partial class GeographicalView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BranchGmap();
        }
    }
    public void BranchGmap()
    {
        DataTable dt = new Common().GetBranchGmap();
        if (dt.Rows.Count > 0)
        {
            string mvarfunction="";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["latitude"].ToString() != "" && dt.Rows[i]["longitude"].ToString() != "")
                {
                    string add = dt.Rows[i]["DepotAddress"].ToString();
                    add = add.Replace(System.Environment.NewLine, " ");
                   

                    string varfunction = "addMarker({coordinates:{ lat: " + dt.Rows[i]["latitude"].ToString() + ", lng: " + dt.Rows[i]["longitude"].ToString() + " },iconImage: 'assets/img/gpin.png',content: '<h4>" + dt.Rows[i]["DepotName"].ToString() + "</h4><p><b>" + dt.Rows[i]["NodalOfficeName"].ToString() + "</b></p><p>" + add + "</p>' });";
                    mvarfunction = mvarfunction + varfunction;
                }
             
            }
            string mpwlcbulding = "addMarker({coordinates: { lat: 23.236460, lng: 77.440936 }, iconImage: 'assets/img/Hoffice.png', content: '<h4>M.P. Warehousing And Logistics Corporation</h4><p><b>HEAD OFFICE</b></p><p>Offic Complex, Block A Gautam Nagar, Bhopal - 462023</p>'});";

            string scriptext = " <script type='text/javascript'> function initMap() {var options = {zoom: 7, center: { lat: 23.999994, lng: 77.947998 } }; var map = new google.maps.Map(document.getElementById('map'), options); function addMarker(prop) {var marker = new google.maps.Marker({position: prop.coordinates, map: map,  draggarble: false }); if (prop.iconImage) { marker.setIcon(prop.iconImage); } if (prop.content) {var information = new google.maps.InfoWindow({content: prop.content }); marker.addListener('click', function () {information.open(map, marker); }); } }" + mvarfunction + mpwlcbulding + " } </script>";
            
            Literal1.Text = scriptext;
           
        }

    }

}
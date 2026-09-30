using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class BranchPages_Godown_Latlong : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string lat = txtLatitude.Text;
        string lng = txtLongitude.Text;

        // Now you can use/save latitude and longitude
        Response.Write("Location received: Latitude = " + lat + ", Longitude = " + lng);
    }
}
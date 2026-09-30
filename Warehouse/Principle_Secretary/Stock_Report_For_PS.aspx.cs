using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Principle_Secretary_Stock_Report_For_PS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lang"] != null)
        {
            if (Session["lang"].ToString() == "Hindi")
            {
                ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("hindi", Server.MapPath("."), null);
            }
        }
    }
    protected void LinkButton202_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_Godown_Wise_Qty_Available_Last_10_Year.aspx\",\"_blank\")", true);
    }
    protected void LinkButton208_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_District_Wise_Qty_Available_Last_10_Year_All_Commodity.aspx\",\"_blank\")", true);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_Branch_Wise_Qty_Available_Last_10_Year.aspx\",\"_blank\")", true);
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_District_Wise_Qty_Available_Last_10_Year.aspx\",\"_blank\")", true);
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_District_Wise_Qty_Available_Last_10_Year_For_Storage_Type.aspx\",\"_blank\")", true);
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_Region_Wise_Qty_Available_Last_10_Year_For_Storage_Type.aspx\",\"_blank\")", true);
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_Branch_Wise_Qty_Available_Last_10_Year_Date_Wise.aspx\",\"_blank\")", true);
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise.aspx\",\"_blank\")", true);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_District_Wise_Qty_Avl_Complete_JVS.aspx\",\"_blank\")", true);
    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_SiloBags.aspx\",\"_blank\")", true);
    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_CAP.aspx\",\"_blank\")", true);
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_Godown_Capacity.aspx\",\"_blank\")", true);
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/ViewUpdateGodownDetails2023.aspx\",\"_blank\")", true);
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise.aspx\",\"_blank\")", true);
    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_HireType.aspx\",\"_blank\")", true);
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_DistrictWise_HiredTypeWise_GodownCapacity.aspx\",\"_blank\")", true);
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_SteelSilo.aspx\",\"_blank\")", true);
    }

    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_Owned_Godown_Details.aspx\",\"_blank\")", true);
    }

    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_All_Godown_Details_Capacity_And_Utilization.aspx\",\"_blank\")", true);
    }

    protected void LinkButton18_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"../Principle_Secretary/Rpt_District_Wise_Qty_Avl_Complete_JVS_Owned.aspx\",\"_blank\")", true);
    }
}
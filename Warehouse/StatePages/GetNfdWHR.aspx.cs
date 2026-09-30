using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class StatePages_GetNfdWHR : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    public void FillGrid()
    {
        string query = "select distinct (select distinct District_Name from tbl_MetaData_DISTRICT where tbl_MetaData_DISTRICT.District_Id=WHR.District_Id) as District_Name,(select distinct DepotName from tbl_MetaData_DEPOT where tbl_MetaData_DEPOT.BranchId=WHR.BranchID) as Branch,WHR.Depositor_WHR_Id,CONVERT(varchar(10),WHR.WHR_Issue_Date,103) AS whrdate,WHR.Depositor_Name,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,WHR.TotalBags_Received AS Bags,convert(decimal(18,2),WHR.Total_Qty_Received) AS Qty,tbl_MetaData_GODOWN.Godown_Name from tbl_storage_Depositor_WHR_Relation AS WHR  join tbl_storage_Stacking_Details as sd on WHR.Depositor_WHR_Id = sd.WHRId JOIN tbl_MetaData_STORAGE_COMMODITY on WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_MetaData_GODOWN on sd.Godown_ID = tbl_MetaData_GODOWN.Godown_ID where WHR.Depositor_WHR_Id='" + txtWHRSerach.Text.ToString() + "' and WHR.DepositorID='10535' and WHR.Depositor_Name='NAFED'";
        cmd = new SqlCommand(query, con);
        da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Session["ds_GridInfo"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            gv.DataSource = ds;
            gv.DataBind();
            //lblRowCount.Text = "";
            //lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
            //lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            gv.DataSource = null;
            gv.DataBind();
        }
    }
}

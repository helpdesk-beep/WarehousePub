using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class BranchPages_Update_WHR_Moisture : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                GetBranch();
               // GetBranchData();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void GetBranchData()
    {
        try
        {
            string qry = "";
            //qry = "select WHR.BranchID,GD.Godown_Name,GD.Godown_ID,WHR.Depositor_WHR_Id,convert(varchar(10),WHR_Issue_Date,103) as WHR_Issue_Date,WHR.TotalBags_Received,Total_Qty_Received,Depositor_Name,WHR.Remark from tbl_storage_Depositor_WHR_Relation as WHR inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=WHR.GodownID where Commodity_Id in ('63','92','27') and CropYear='2022-23' and Arrival_Source='01' and Depositor_Name in('NAFED','DMO Markfed') and WHR.BranchID='" + Session["BranchId"].ToString() + "'";
            //qry = "select WHR.BranchID,GD.Godown_Name,GD.Godown_ID,WHR.Depositor_WHR_Id,convert(varchar(10),WHR_Issue_Date,103) as WHR_Issue_Date,WHR.TotalBags_Received,Total_Qty_Received,Depositor_Name,WHR.Remark,WHR.AvgMoisture_Content,WHR.AvgMoisture_Content_To from tbl_storage_Depositor_WHR_Relation as WHR inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=WHR.GodownID where CropYear='2023-24' and Arrival_Source='01' and GD.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
            //qry = "select WHR.BranchID,GD.Godown_Name,GD.Godown_ID,WHR.Depositor_WHR_Id,convert(varchar(10),WHR_Issue_Date,103) as WHR_Issue_Date,WHR.TotalBags_Received,Total_Qty_Received,Depositor_Name,WHR.Remark,WHR.AvgMoisture_Content,WHR.AvgMoisture_Content_To from tbl_storage_Depositor_WHR_Relation as WHR inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=WHR.GodownID where CropYear='2025-26' and Arrival_Source='01' and GD.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
            qry = "select WHR.BranchID,GD.Godown_Name,GD.Godown_ID,WHR.Depositor_WHR_Id,convert(varchar(10),WHR_Issue_Date,103) as WHR_Issue_Date,WHR.TotalBags_Received,Total_Qty_Received,Depositor_Name,WHR.Remark,WHR.AvgMoisture_Content,WHR.AvgMoisture_Content_To from tbl_storage_Depositor_WHR_Relation as WHR inner join tbl_MetaData_GODOWN_2018 as GD on GD.Godown_ID=WHR.GodownID where CropYear='2026-27' and Arrival_Source='01' and GD.Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";

            //SqlCommand cmd = new SqlCommand("Get_WHR_Remark_Updation", con);
            SqlCommand cmd = new SqlCommand(qry, con);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
            //cmd.Parameters.AddWithValue("@BranchID", "2328001");
            // cmd.Parameters.AddWithValue("@StorageType", ddlWST.SelectedValue.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
                // trmobtxt.Visible = true;
                //lbldistrictid.Text = ds.Tables[0].Rows[0]["DistrictId"].ToString();
                //lblbranchid.Text = ds.Tables[0].Rows[0]["BranchID"].ToString();
                //showgrid.Visible = true;
                grdshow.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                //trbtnhide.Visible = false;
                //trmobtxt.Visible = false;
               // showgrid.Visible = false;
                grdshow.Visible = false;
                Depositor_Gridview.DataSource = null;
                Depositor_Gridview.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }
    public int GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }



    protected void ddlWST_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchData();
    }
   
    protected void Depositor_Gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = Depositor_Gridview.Rows[rowIndex];

            //Fetch value of Name.
            //string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            string hdnWHID = (row.FindControl("hdnWHID") as HiddenField).Value;
            //string ddlEWC = (row.FindControl("ddlEWC") as DropDownList).SelectedValue;
            string AvgMoisture_Content = (row.FindControl("lblAvgMoisture_Content") as TextBox).Text;
            string AvgMoisture_Content_To = (row.FindControl("lblAvgMoisture_ContentTO") as TextBox).Text;
            //string ddlrailsaided = (row.FindControl("ddlrailsaided") as DropDownList).SelectedValue;
            //string ddlrailsaidedcount = (row.FindControl("ddlrailsaidedcount") as DropDownList).SelectedValue;

            //Session["hdnId"] = hdnId.ToString();
            Session["hdnWHID"] = hdnWHID.ToString();
            //Session["ddlEWC"] = ddlEWC.ToString();
            Session["lblAvgMoisture_Content"] = AvgMoisture_Content.ToString();
            Session["lblAvgMoisture_ContentTO"] = AvgMoisture_Content_To.ToString();
            //Session["ddlrailsaided"] = ddlrailsaided.ToString();
            //Session["ddlrailsaidedcount"] = ddlrailsaidedcount.ToString();
             Update(hdnWHID, AvgMoisture_Content, AvgMoisture_Content_To, Session["BranchId"].ToString());
            // RemoveRowJVS(hdnId);

        }
    }
    public void Update(string whid,string AvgMoisture_Content,string AvgMoisture_Content_To, string BranchID)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("Update_WHR_Moisture", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@WHRID", whid);
            cmd.Parameters.AddWithValue("@AvgMoisture_Content", AvgMoisture_Content);
            cmd.Parameters.AddWithValue("@AvgMoisture_Content_To", AvgMoisture_Content_To);
            cmd.Parameters.AddWithValue("@Createt_By", localIP.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "WHR Moisture Update Successfully |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                Depositor_Gridview.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                GetBranchData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
    protected void GrdOfficerPreviousInsp_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {

    }

    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetBranch(DropDownList1.SelectedValue.ToString());
    //}
    //public void getdistrict()
    //{
    //    string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name ";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        DropDownList1.DataSource = ds.Tables[0];
    //        DropDownList1.DataTextField = "District_Name";
    //        DropDownList1.DataValueField = "District_Id";
    //        DropDownList1.DataBind();
    //        DropDownList1.Items.Insert(0, "--Select--");
    //    }
    //}
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodowndata();
    }
    public void GetBranch()
    {

        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where BranchId ='" + Session["BranchId"].ToString() + "' order by DepotName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBranch.DataSource = ds.Tables[0];
            ddlBranch.DataTextField = "DepotName";
            ddlBranch.DataValueField = "BranchId";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, "--Select--");
        }
    }

    public void GetGodowndata()
    {

        string qry = "";
        qry = "select Godown_Name,Godown_ID  from tbl_MetaData_GODOWN_2018 where BranchId ='" + Session["BranchId"].ToString() + "' order by Godown_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchData();
    }
}
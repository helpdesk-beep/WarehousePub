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

public partial class StatePages_View_Godown_Compalint : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();
    public string LicDate = "";
    //public string WManagerId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Session["UserName"] != null)
            {
                if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
                {
                    if (!IsPostBack)
                    {
                        fillgrid();
                        FillDistrict();
                        //GetRegion();
                    }
                }
            }
            else
            {
                Response.Redirect("~/SessionExpired.htm");
            }
            //fillgrid();
            //GetRegion();
        }
    }
    //private void GetRegion()
    //{
    //    string strDist = "";
    //    strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
    //    SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlregion.DataSource = ds.Tables[0];
    //        ddlregion.DataTextField = "Regionnm";
    //        ddlregion.DataValueField = "Region_ID";
    //        ddlregion.DataBind();
    //        ddlregion.Items.Insert(0, "--Select--");
    //    }
    //    else
    //    {
    //        ddlregion.Items.Insert(0, "--Select--");
    //    }
    //}

    private void FillDistrict()
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID= '" + Session["Region_ID"].ToString() + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }
        else
        {
            ddldistrict.Items.Insert(0, "--Select--");
        }
    }

    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Fill_Branch", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_Id", ddldistrict.SelectedValue);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            con.Close();
        }
    }
    //protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillDistrict();
    //}
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranchDetails();
    }

    protected void fillgrid()
    {
        Decimal opcloavg = 0;
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Complaint_For_Region", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", Session["Region_ID"].ToString());
                if (ddldistrict.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@DistrictID", 0);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@DistrictID", ddldistrict.SelectedValue);
                }
                
                if (ddlbranch.SelectedValue == "-- Select Branch --")
                {
                    cmd.Parameters.AddWithValue("@BranchID", 0);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                        else
                        {

                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];

            //Fetch value of Name.
            string hdnGodownID = (row.FindControl("hdnGodownID") as HiddenField).Value;
            string hdnid = (row.FindControl("hdnid") as HiddenField).Value;
            string lblRMRemark = (row.FindControl("lblRMRemark") as TextBox).Text;
            //string lblFirstWHRDate = (row.FindControl("flupslip") as TextBox).Text;
            Session["hdnGodownID"] = hdnGodownID.ToString();
            Session["hdnid"] = hdnid.ToString();
            Session["lblRMRemark"] = lblRMRemark.ToString();
            Update(hdnid, hdnGodownID, lblRMRemark);
        }
    }
    public void Update(string ID, string Godownid,string RMRemark)
    {
        SqlCommand cmd1 = new SqlCommand();
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
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

            SqlCommand cmd = new SqlCommand("Update_Complaint_Remark_by_Region", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@GodownID", Godownid);
            cmd.Parameters.AddWithValue("@RM_Remark", RMRemark);
            cmd.Parameters.AddWithValue("@RM_IP_Address", IPAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Godow Remark Update Successfully |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                GrdOfficerPreviousInsp.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                fillgrid();
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

}

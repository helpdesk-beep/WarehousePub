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
using AjaxControlToolkit;

public partial class BranchPages_Update_IssueCenterLatLong : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;
    string TheResult = "";
    //string BranchID = Session["BranchId"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["BranchId"].ToString() != null)
        {
                GetBranchData();
                GetDistrictName();
                GetIssueCenterData();
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
            string BranchID = Session["BranchId"].ToString();
            qry = "select BranchID, BranchName from MetaDataBranchWithIssueCenter where BranchID = '" + BranchID + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                    txtBranchName.Text = ds.Tables[0].Rows[0]["BranchName"].ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);

            }
        }
        catch (Exception ex)
        {

        }
    }

    public void GetDistrictName()
    {
        try
        {
            string qry = "";
            string BranchID = Session["BranchId"].ToString();
            qry = "select MD.District_Name [DistrictName] from Tbl_Metadata_Depot MDD INNER JOIN  tbl_MetaData_DISTRICT MD ON MDD.DistrictId = MD.District_Id where DepotID = '" + BranchID + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDistrictName.Text = ds.Tables[0].Rows[0]["DistrictName"].ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);

            }
        }
        catch (Exception ex)
        {

        }
    }

    public void GetIssueCenterData()
    {
        try
        {
            string qry = "";
            string BranchID = Session["BranchId"].ToString();
            qry = "select IssueCenterId [IssueCenterId], IssueCenterName [IssueCenterName] from MetaDataBranchWithIssueCenter where BranchID =  '" + BranchID + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtIssueCenterID.Text = ds.Tables[0].Rows[0]["IssueCenterId"].ToString();
                txtIssueCenterName.Text = ds.Tables[0].Rows[0]["IssueCenterName"].ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void Display(object sender, EventArgs e)
    {
        //int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        ////GridViewRow row = Depositor_Gridview.Rows[rowIndex];

        //// lblWHID.Text = (row.FindControl("lblWHID") as Label).Text;
        //string hdnWHID = (row.FindControl("hdnWHID") as HiddenField).Value;
        //string hdnBranchId = (row.FindControl("hdnBranchId") as HiddenField).Value;
        ////lblgodownname.Text = (row.FindControl("lblGodown_Name") as Label).Text;
        ////txtGdwnID.Text = (row.FindControl("hdnWHID") as HiddenField).Value;
        //txtlat.Text = (row.FindControl("lblLatitude") as Label).Text;
        //txtlong.Text = (row.FindControl("lblLongitude") as Label).Text;
        ////Adding Registration ID 
        ////txtRegistrationID.Text = (row.FindControl("lblRegistrationID") as Label).Text;
        //Session["hdnWHID"] = (row.FindControl("hdnWHID") as HiddenField).Value;
        //Session["hdnBranchId"] = (row.FindControl("hdnBranchId") as HiddenField).Value;
        //divNewInsp.Visible = true;
        //ModalPopupExtender1.Show();
    }

    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        string BranchID = Session["BranchId"].ToString();
        string Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            if (txtlat.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Latitude of Issue Center')", true);
            }

            else if (txtlong.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Longitude of Issue Center')", true);
            }
            else if (txtIssueCenterID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Issue Center ID of Issue Center')", true);
            }

            else
            {
                sqltrans = con.BeginTransaction();
                //  con.Open();
                cmd = new SqlCommand("Add_IssueCenter_LatLong", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltrans;
                cmd.Parameters.AddWithValue("@IssueCenterID", txtIssueCenterID.Text);
                cmd.Parameters.AddWithValue("@BranchID", BranchID);
                cmd.Parameters.AddWithValue("@Latitude", Convert.ToDecimal(txtlat.Text));
                cmd.Parameters.AddWithValue("@Longitude", Convert.ToDecimal(txtlong.Text));
                cmd.Parameters.AddWithValue("@ClientIP", Client_Ip);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Details Added Successfully')", true);
                    sqltrans.Commit();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record not Added')", true);

                }
            }
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error')", true);
        }
        finally
        {
            con.Close();
        }
    }

    
}

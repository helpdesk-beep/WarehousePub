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
using System.Drawing;
using System.IO;

public partial class Reports_Region_Black_List_Godown_in_FIFO : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;
    SqlDataAdapter da = new SqlDataAdapter();
    int gridcount;
    int ZeroCount;
    int valuecount;
    int rownumber = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                GetBranchData();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void GetBranchData()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Black_List_Godown_List_in_FIFO", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", Session["Region_ID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet dt = new DataSet())
                    {
                        int Storage_Value = 0;
                        sda.Fill(dt);
                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            // Session["BranchName"] = ddlbranch.SelectedItem.ToString();
                           
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                            showgrid.Visible = true;
                       
                        }
                        else
                        {
                            showgrid.Visible = false;

                            Depositor_Gridview.DataSource = null;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void Depositor_Gridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
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


    protected void Depositor_Gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = Depositor_Gridview.Rows[rowIndex];

            //Fetch value of Name.
            string hdnGodownID = (row.FindControl("hdnGodownID") as HiddenField).Value;
            string hdnBranchId = (row.FindControl("hdnBranchId") as HiddenField).Value;
            string hdnbranch = (row.FindControl("hdnbranch") as HiddenField).Value;
            string hdndistrict = (row.FindControl("hdndistrict") as HiddenField).Value;
            string hdncategory = (row.FindControl("hdncategory") as HiddenField).Value;
            string hdnregionname = (row.FindControl("hdnregionname") as HiddenField).Value;
            string hdngodownname = (row.FindControl("hdngodownname") as HiddenField).Value;
            //string ddlEWC = (row.FindControl("ddlEWC") as DropDownList).SelectedValue;
            //string hdnBranchId = (row.FindControl("hdnBranchId") as TextBox).Text;
            string ddlcategory = (row.FindControl("ddlcategory") as DropDownList).SelectedValue;
            string lblDelivery_mode = (row.FindControl("lblDelivery_mode") as Label).Text;
            //string lblFirstWHRDate = (row.FindControl("flupslip") as TextBox).Text;
            Session["hdnbranch"] = hdnbranch.ToString();
            Session["hdndistrict"] = hdndistrict.ToString();
            Session["hdncategory"] = hdncategory.ToString();
            Session["hdnregionname"] = hdnregionname.ToString();
            Session["hdngodownname"] = hdngodownname.ToString();
            Update(hdnBranchId, hdnGodownID);
             Response.Redirect("/Warehouse/Reports/Region/Print_BlackList_Godown_Order.aspx");

        }
    }
    public void Update(string BranchID, string Godownid)
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

            SqlCommand cmd = new SqlCommand("Blacklist_Godown_by_Region", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@GodownID", Godownid);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Godow Blacklsted Successfully |||";
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

   
}
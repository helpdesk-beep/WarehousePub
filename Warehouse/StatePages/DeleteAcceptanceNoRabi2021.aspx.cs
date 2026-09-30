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

public partial class StatePages_DeleteAcceptanceNoRabi2021 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = null;
    SqlTransaction sqltrans;
    string RegionID = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

 
    public void GetAcceptanceData()
    {
        try
        {
            //string qry = "";
            //qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "'";
            cmd = new SqlCommand("dbo.Get_Accepatance_Details_by_Acceptance_Number", con, sqltrans);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Acceptance_No", txtacceptanceno.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gdnacceptance.DataSource = ds;
                gdnacceptance.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                gdnacceptance.DataSource = null;
                gdnacceptance.DataBind();
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
    protected void txtacceptanceno_TextChanged(object sender, EventArgs e)
    {
        GetAcceptanceData();
    }
    protected void gdnacceptance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = gdnacceptance.Rows[rowIndex];

            //Fetch value of Name.
            string hdnGodownId = (row.FindControl("hdnGodownId") as HiddenField).Value;
            string hdnAcceptance_No = (row.FindControl("hdnAcceptance_No") as HiddenField).Value;
            string hdnCommodity_Id = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
            Session["hdnGodownId"] = hdnGodownId.ToString();
            Session["hdnAcceptance_No"] = hdnAcceptance_No.ToString();
            Session["hdnCommodity_Id"] = hdnCommodity_Id.ToString();
            RemoveRow(hdnGodownId, hdnAcceptance_No, hdnCommodity_Id);
            // RemoveRowJVS(hdnId);

        }
    }
    public void RemoveRow(string Godownid, string Acceptance_No,string Commodity_Id)
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

            SqlCommand cmd = new SqlCommand("Delete_Receive_Proc_Rabi2021", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godownid", Godownid.ToString());
            cmd.Parameters.AddWithValue("@Acceptance_No", Acceptance_No.ToString());
            cmd.Parameters.AddWithValue("@Commodity_Id", Commodity_Id.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Delete Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                GetAcceptanceData();
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
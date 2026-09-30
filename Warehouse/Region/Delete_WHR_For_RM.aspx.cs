using System;
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

public partial class StatePages_Delete_WHR_For_RM : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                getdistrict();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_WHR_Details_Deletion_For_Jabalpur_District", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@District_Id", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@BranchID", ddlBranch.SelectedValue);
              //  cmd.Parameters.AddWithValue("@WHR_ID", txttwhrno.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                        }
                        else
                        {
                            Depositor_Gridview.DataSource = null;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
    }

    public void GetBranch(string distID)
    {

        string qry = "";
        qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId ='" + distID + "' order by DepotName";
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
    protected void Depositor_Gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = Depositor_Gridview.Rows[rowIndex];

            //Fetch value of Name.
            string hdngodownid = (row.FindControl("hdngodownid") as HiddenField).Value;
            string hdnReq_id = (row.FindControl("hdnReq_id") as HiddenField).Value;
            string hdnBranchID = (row.FindControl("hdnBranchID") as HiddenField).Value;
            string hdncommodity_id = (row.FindControl("hdncommodity_id") as HiddenField).Value;
            string hdndepositerid = (row.FindControl("hdndepositerid") as HiddenField).Value;
            string lblWhr_No = (row.FindControl("lblWhr_No") as Label).Text;
            Session["hdnBranchID"] = hdnBranchID.ToString();
            Session["hdnReq_id"] = hdnReq_id.ToString();
            Session["lblWhr_No"] = lblWhr_No.ToString();
            Session["hdngodownid"] = hdngodownid.ToString();
            DeleteWHR(hdnBranchID, hdnReq_id, lblWhr_No);

        }
    }
    public void DeleteWHR(string Branchid, string Req_Id, string WHR_ID)
    {
        string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        SqlCommand cmd1 = new SqlCommand();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();
            }
            SqlCommand cmd = new SqlCommand("Delete_WHR_State", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@WHRID", WHR_ID.ToString());
            cmd.Parameters.AddWithValue("@BranchID", Branchid.ToString());
            cmd.Parameters.AddWithValue("@Req_id", Req_Id.ToString());
            cmd.Parameters.AddWithValue("@IPAddress", localIP);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove WHR Details Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillScheduleInsp_Grid();
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
  public void getdistrict()
    {
        string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT where District_Id ='2333'  order by District_Name ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DropDownList1.DataSource = ds.Tables[0];
            DropDownList1.DataTextField = "District_Name";
            DropDownList1.DataValueField = "District_Id";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "--Select--");
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranch(DropDownList1.SelectedValue.ToString());
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
}

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_Inspection_Officer_Dead_Stock_Inspection : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string PFID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        if (!IsPostBack)
        {
            if (Session["UserId"] != null)
            {
                PFID = Session["UserId"].ToString();
                GetEmployeeInspectionDetails(PFID);
                fillBranchDetails();
            }
            else
            {
                Response.Redirect("~/Inspections/Default.aspx");
            }
        }
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
    public void GetEmployeeInspectionDetails(string PFID)
    {
        SqlCommand cmd = new SqlCommand("[dbo].[Get_Inspection_Quater_Details]", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Employee_ID", PFID);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            hdninspectionid.Value = dt.Rows[0]["ID"].ToString();
        }

    }
    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Branch_Name_For_DF", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PFID_ID", PFID);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "Depo_Name";
            ddlbranch.DataValueField = "Branch_ID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            con.Close();
        }
    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("dbo.Get_Dead_Stock_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
               // cmd.Parameters.AddWithValue("@InspectionID", hdninspectionid.Value);
                cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
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
                            btnfinalsubmit.Visible = true;
                        }
                        else
                        {
                            btnfinalsubmit.Visible = false;
                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void OnTextChanged(object sender, EventArgs e)
    {

        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        TextBox txtUsable = (TextBox)row.FindControl("txtUsable");
        TextBox txtUnusable = (TextBox)row.FindControl("txtUnusable");
        Label lblTotal = (Label)row.FindControl("lblTotal");
        if (string.IsNullOrEmpty(txtUsable.Text))
        {
            txtUsable.Text = "0";
        }
        if (string.IsNullOrEmpty(txtUnusable.Text))
        {
            txtUnusable.Text = "0";
        }
        lblTotal.Text = (Convert.ToInt32(txtUsable.Text) + Convert.ToInt32(txtUnusable.Text)).ToString();
    }
    public void Submit(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            foreach (GridViewRow row in GrdOfficerPreviousInsp.Rows)
            {
                TextBox txtUsable = (TextBox)row.FindControl("txtUsable");
                TextBox txtUnusable = (TextBox)row.FindControl("txtUnusable");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                HiddenField hdnCategory = (HiddenField)row.FindControl("hdnCategory");
                Label lblTotal = (Label)row.FindControl("lblTotal");
                if (lblTotal.Text != "0")
                {
                    conStr.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[Insert_Dead_Stock_Inspection]", conStr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Inspection_ID", hdninspectionid.Value);
                    cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                    cmd.Parameters.AddWithValue("@Category_ID", hdnCategory.Value);
                    cmd.Parameters.AddWithValue("@Usable", txtUsable.Text);
                    cmd.Parameters.AddWithValue("@Unusable", txtUnusable.Text);
                    cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                    cmd.Parameters.AddWithValue("@Insert_By", IPAddress);
                    cmd.Parameters.AddWithValue("@EmployeeID", Session["UserId"].ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        count++;
                    }
                    conStr.Close();
                }
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Dead Stock Inspection Completed')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Dead Stock Inspection Completed')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            con.Close();
        }
    }
    protected void GrdOfficerPreviousInsp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}
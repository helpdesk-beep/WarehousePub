using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspections_State_PVInspectionReport : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["MPWLCInspection"].ConnectionString;
    string currentStackID = "";
    int totalOnline = 0;
    int totalPV = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Mobile"] == null || Session["Mobile"].ToString() == "")
                {
                    Response.Redirect("~/Inspections/State/Login_Special_PV.aspx", false);
                    return;
                }

                LoadBranch();
            }
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }
    private void LoadBranch()
    {
        try
        {
            string empId = Session["Mobile"].ToString();
            string Query = "select distinct ent.Branch_ID,depo.DepotName from MPWLCInspection.dbo.Insp_Stack_Block_Wise_Godown_Entry ent INNER JOIN Intergrated_MP_STORAGE.dbo.tbl_MetaData_DEPOT depo ON ent.Branch_ID=depo.BranchId WHERE ent.Emp_ID=@Emp_ID";
            using (SqlConnection conn = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Emp_ID", SqlDbType.VarChar, 12).Value = empId;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                ddlBranch.DataSource = dt;
                ddlBranch.DataTextField = "DepotName";
                ddlBranch.DataValueField = "Branch_ID";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }
    private void LoadGodown(int branchId)
    {
        try
        {
            string branch = ddlBranch.SelectedItem.Value;
            if (branch == "0")
            {
                ClientScript.RegisterStartupScript(
                   this.GetType(),
                  "alert",
                   "alert('Please Select Branch');",
                   true
               );
            }
            ddlGodown.Items.Clear();
            string empId = Session["Mobile"].ToString();
            string Query = "select distinct ent.Godown_ID,gdn.Godown_Name,ent.Godown_Submit_date from MPWLCInspection.dbo.Insp_Stack_Block_Wise_Godown_Entry ent LEFT JOIN Intergrated_MP_STORAGE.dbo.tbl_MetaData_GODOWN_2018 gdn ON ent.Godown_ID=gdn.Godown_ID WHERE ent.Emp_ID=@Emp_ID and ent.Branch_ID=@Branch_ID";
            using (SqlConnection conn = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@Emp_ID", SqlDbType.VarChar, 12).Value = empId;
                cmd.Parameters.Add("@Branch_ID", SqlDbType.VarChar, 20).Value = branch;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                ddlGodown.DataSource = dt;
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();

                ddlGodown.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int branchId = Convert.ToInt32(ddlBranch.SelectedValue);
            LoadGodown(branchId);
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGodown.SelectedValue == "0")
            {
                ShowAlert("Please select Godown");
                return;
            }

            BindReport();
            lblGeneratedOn.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }
    private void BindReport()
    {
        try
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand("Proc_Get_Godown_Stack_PV_Difference", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Emp_ID", SqlDbType.VarChar).Value = Session["Mobile"].ToString();
                    cmd.Parameters.Add("@Godown_ID", SqlDbType.VarChar).Value = ddlGodown.SelectedValue;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            if (dt.Rows.Count > 0)
            {
                gvReport.DataSource = dt;
                gvReport.DataBind();
            }
            else
            {
                gvReport.DataSource = null;
                gvReport.DataBind();
                ShowAlert("No Record Found");
            }
        }
        catch (Exception ex)
        {
            ShowAlert(ex.Message);
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int diff = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Difference_Bags"));

                if (diff > 0)
                {
                    e.Row.Cells[12].ForeColor = System.Drawing.Color.Green;
                }
                else if (diff < 0)
                {
                    e.Row.Cells[12].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch
        {

        }
    }
    private void ShowAlert(string message)
    {
        ClientScript.RegisterStartupScript(
            this.GetType(),
            "alert",
            "alert('" + message.Replace("'", "") + "');",
            true);
    }
}
using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Region_ViewEmployee : System.Web.UI.Page
{
    DataSet ds = null;
    string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Expires = 0;

        if (Session["username"] == null)
        {
            Response.Redirect("/Login/Login.aspx");
        }

        if (!IsPostBack)
        {
            ShowData();
            GetDist(Session["Regionid"].ToString());
        }
    }
    //Connection String from web.config File  
    string cs = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
    SqlConnection con;
    SqlDataAdapter adapt;
    DataTable dt;

    //ShowData method for Displaying Data in Gridview  
    protected void ShowData()
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        con.Open();

        SqlCommand com = new SqlCommand("Get_Employee_Details_For_RM_Verification", con);
        com.CommandType = System.Data.CommandType.StoredProcedure;
        com.Parameters.AddWithValue("@RegionID", Session["Regionid"].ToString());
        // com.Parameters.AddWithValue("@userPassword", password);
        SqlDataAdapter adapter = new SqlDataAdapter(com);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        con.Close();
    }
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = GridView1.Rows[rowIndex];

        Session["ID"] = (row.FindControl("hdnId") as HiddenField).Value;
        Session["EmpID"] = (row.FindControl("hdnEmpID") as HiddenField).Value;
        //fillScheduleInsp_Grid(Session["S_PFID"].ToString());
        GetEmployeeDetails();
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }
    protected void GetEmployeeDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Employee_Details_For_Verification_by_RM", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Emp_ID", Session["ID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblempname.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Name"].ToString()) ? ds.Tables[0].Rows[0]["Name"].ToString() : "");
                            lbldesignation.Text = HttpUtility.HtmlEncode(!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Designation"].ToString()) ? ds.Tables[0].Rows[0]["Designation"].ToString() : "");
                            Gridview_OfficerPreviousInsp.DataSource = ds;
                            Gridview_OfficerPreviousInsp.DataBind();
                            lblTotalInsp.Text = Convert.ToString(ds.Tables[0].Rows.Count);

                        }
                        else
                        {
                            Gridview_OfficerPreviousInsp.DataSource = null;
                            Gridview_OfficerPreviousInsp.DataBind();
                            lblTotalInsp.Text = "0";
                        }
                    }
                }
            }
        }
    }
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepot(ddl_dist.SelectedValue.ToString());
        ModalPopupExtender1.Show();
    }
    private void GetDepot(string DistID)
    {
        string strDist = "";
        //if (btn_saveInspDate.Text == "Submit")
        //{
        //   // strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' and CategoryID='Y' and BranchID not in (select Distinct Branch_ID from Inspection_Scheduled_For_Officer) order by Depotname";
        //    strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' and BranchID not in (select Distinct Branch_ID from Inspection_Scheduled_For_Officer) order by Depotname";
        //}
        //else if (btn_saveInspDate.Text == "Update")
        //{
        //   // strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' and CategoryID='Y' order by Depotname";
        //    strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' order by Depotname";
        //}
        strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' order by Depotname";
        SqlDataAdapter da = new SqlDataAdapter(strDist, constr);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_branch.DataSource = ds.Tables[0];
            ddl_branch.DataTextField = "Depotname";
            ddl_branch.DataValueField = "BranchID";
            ddl_branch.DataBind();
            ddl_branch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_branch.Items.Insert(0, "--Select--");
        }
        ModalPopupExtender1.Show();
    }
    private void GetDist(string region)
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID='" + region + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, constr);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_dist.DataSource = ds.Tables[0];
            ddl_dist.DataTextField = "District_Name";
            ddl_dist.DataValueField = "District_Id";
            ddl_dist.DataBind();
            ddl_dist.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_dist.Items.Insert(0, "--Select--");
        }
        ModalPopupExtender1.Show();
    }
}
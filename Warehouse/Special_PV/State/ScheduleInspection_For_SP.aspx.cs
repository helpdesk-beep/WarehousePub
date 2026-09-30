using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
public partial class Special_PV_State_ScheduleInspection_For_SP : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string con_WLC2 = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    private object con;
    private object ob_value;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        // Session["UserName"].ToString() = Session["UserName"].ToString();
        if (!IsPostBack)
        {
            FillGrid();
        }
    }
    protected void FillGrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC2))
        {
            SqlCommand cmd = new SqlCommand("Get_Special_PV_Inspection_Officer_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Region_ID", Session["UserId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                Gridview_IsnpOff.DataSource = dt;
                Gridview_IsnpOff.DataBind();
                lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
                //lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
                //ViewState["Region"] = dt;
            }
            else
            {
                Gridview_IsnpOff.DataSource = null;
                Gridview_IsnpOff.DataBind();
                lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
                //lblOfficerList.Text = "0";
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
        if (btn_saveInspDate.Text == "Submit")
        {
            strDist = "SELECT Depotname,BranchID FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_Depot where DistrictID='" + DistID + "' order by Depotname";
        }
        else if (btn_saveInspDate.Text == "Update")
        {
            strDist = "SELECT Depotname,BranchID FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_Depot where DistrictID='" + DistID + "' order by Depotname";
        }
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_JVS);
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
        strDist = "SELECT District_Name,District_Id FROM Intergrated_MP_STORAGE.dbo.tbl_MetaData_DISTRICT order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_JVS);
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
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Gridview_IsnpOff.Rows[rowIndex];

        Session["S_PFID"] = (row.FindControl("hdnpfid") as HiddenField).Value;
        GetDist(Session["UserName"].ToString());
        txtInspOffName.Text = (row.FindControl("lbname") as Label).Text;
        txtCug.Text = (row.FindControl("lblPersonal_Mobile_No") as Label).Text; ;
        txtDesig.Text = (row.FindControl("lblDesignation") as Label).Text;

        //fillScheduleInsp_Grid(Session["S_PFID"].ToString());
        // GetEmployeeDetails();
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }
    protected void Gridview_IsnpOff_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridViewRow gvr = Gridview_IsnpOff.SelectedRow;
        //Session["S_PFID"] = gvr.Cells[0].Text;
        //GetDist(Session["UserName"].ToString());
        //txtInspOffName.Text = gvr.Cells[1].Text;
        //txtDesig.Text = gvr.Cells[2].Text;
        //txtCug.Text = gvr.Cells[4].Text;
        //fillScheduleInsp_Grid(gvr.Cells[0].Text);
        //divNewInsp.Visible = true;
    }
    protected void GetEmployeeDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Employee_Details_For_Inspection_For_SP", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employee_ID", Session["S_PFID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {

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
    public void checkvalidation()
    {
        if (txtInspOffName.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Officer Name CanNot be Blank...')", true);
            txtInspOffName.Focus();
            return;
        }
        else if (txtCug.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Officer CUG Mobile No. CanNot be Blank...')", true);
            txtCug.Focus();
            return;
        }
        else if (ddl_dist.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District...')", true);
            ddl_dist.Focus();
            return;
        }
        else if (ddl_branch.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Branch...')", true);
            ddl_branch.Focus();
            return;
        }
        else if (txt_InspDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Inspection Date...')", true);
            txt_InspDate.Focus();
            return;
        }

    }
    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];

        try
        {
            checkvalidation();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Inspection_Allot_Branch_For_Officer_Special_PV", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Inspection_Type", ddlquater.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Officer_Name", txtInspOffName.Text);
            cmd.Parameters.AddWithValue("@Designation", txtDesig.Text);
            cmd.Parameters.AddWithValue("@Mobile_No", txtCug.Text);
            cmd.Parameters.AddWithValue("@District_ID", ddl_dist.SelectedValue);
            cmd.Parameters.AddWithValue("@Branch_ID", ddl_branch.SelectedValue);
            cmd.Parameters.AddWithValue("@Order_Number", txt_OrderNo.Text);
            cmd.Parameters.AddWithValue("@Order_Date", getDate_MDY(txt_InspDate.Text));
            cmd.Parameters.AddWithValue("@Created_By", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@CreatedBy_Ip", ipAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Allotted Branch For Inspection Officer For Special PV Successfully |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                ModalPopupExtender1.Show();
                GetEmployeeDetails();
                //Getstoragedutysubmision();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
            }
        }
        catch (Exception ex)
        {

            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('col 1 Error  .');", true);
            Console.WriteLine(ex.Message);
        }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
}
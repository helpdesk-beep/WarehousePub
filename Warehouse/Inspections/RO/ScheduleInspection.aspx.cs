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

public partial class Inspections_RO_ScheduleInspection : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //  public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["Inspectioncon_JVSing"].ToString());
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        // Session["UserName"].ToString() = Session["UserName"].ToString();
        if (!IsPostBack)
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('यहाँ सुविधा अभी कुछ समय के लिए रोकी गई हैं ,क्योकि नये फाइनेंसियल ईयर के लिए अभी सॉफ्टवेर में कम चल रहा हैं...')", true);
            //fillInpOff_Grid();
            fillFinsncilYear();
        }

    }
    public void fillFinsncilYear()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Fianancial_Year_For_inspection", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            ddlfinancialyear.DataSource = cmd.ExecuteReader();
            ddlfinancialyear.DataTextField = "Financial_Year";
            ddlfinancialyear.DataValueField = "Financial_Year";
            ddlfinancialyear.DataBind();
            ddlfinancialyear.Items.Insert(0, new ListItem("--Select Financial Year--", "0"));
            con.Close();
        }
    }
    public void fillInpOff_Grid()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Get_Employee_Details_Region_Wise_For_Schedule_Inspection", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Region_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Team", ddlteam.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                Gridview_IsnpOff.DataSource = dt;
                Gridview_IsnpOff.DataBind();
                lblOfficerList.Text = Convert.ToString(dt.Rows.Count);
                divdetails.Visible = true;
            }
            else
            {
                divdetails.Visible = false;
                Gridview_IsnpOff.DataSource = null;
                Gridview_IsnpOff.DataBind();
                lblOfficerList.Text = "0";
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        { if (conStr.State == ConnectionState.Open) { conStr.Close(); } }
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
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID='" + region + "' order by District_Name";
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
    protected void GetEmployeeDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Employee_Details_For_Inspection", con))
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
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Gridview_IsnpOff.Rows[rowIndex];

        Session["S_PFID"] = (row.FindControl("hdnpfid") as HiddenField).Value;
        GetDist(Session["UserId"].ToString());
        txtInspOffName.Text = (row.FindControl("lbname") as Label).Text;
        txtDesig.Text = (row.FindControl("lbldesignation") as Label).Text; ;
        txtCug.Text = (row.FindControl("lblmobilenumber") as Label).Text;

        //fillScheduleInsp_Grid(Session["S_PFID"].ToString());
        GetEmployeeDetails();
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }
    protected void Gridview_IsnpOff_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        else if (txtManagerNM.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Branch Manager Name...')", true);
            txtManagerNM.Focus();
            return;
        }
        else if (txtcugno.Text.Length != 10)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter 10 Digit Branch Manager Mobile No...')", true);
            txtcugno.Focus();
            return;
        }
        else if (ddlfinancialyear.SelectedItem.Text == "--Select Financial Year--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select financial year...')", true);
            ddlfinancialyear.Focus();
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
            SqlCommand cmd = new SqlCommand("Inspection_Allot_Branch_For_Officer_Insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Employee_ID", Session["S_PFID"].ToString());
            cmd.Parameters.AddWithValue("@District_ID", ddl_dist.SelectedValue);
            cmd.Parameters.AddWithValue("@Branch_ID", ddl_branch.SelectedValue);
            cmd.Parameters.AddWithValue("@Inspection_type_ID", ddlquater.SelectedValue);
            cmd.Parameters.AddWithValue("@Inspection_month_ID", ddlmonth.SelectedValue);
            cmd.Parameters.AddWithValue("@Verification_Type", ddlverification.SelectedValue);
            cmd.Parameters.AddWithValue("@Order_No", txt_OrderNo.Text);
            cmd.Parameters.AddWithValue("@Order_Date", getDate_MDY(txt_InspDate.Text));
            cmd.Parameters.AddWithValue("@Branch_Manager_Name", txtManagerNM.Text);
            cmd.Parameters.AddWithValue("@Manager_CUG_No", txtcugno.Text);
            cmd.Parameters.AddWithValue("@IP_Adress", ipAddress);
            cmd.Parameters.AddWithValue("@Financial_year", ddlfinancialyear.SelectedValue);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Allotted Branch For Inspection Officer Successfully |||";
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
    public string chkAID(string PFID)
    {
        string MaxAID = "";
        string QueryMax = "select MAX(AID) as AID from tbl_Inpection_Scheduled_Date where PF_ID='" + PFID + "'";
        SqlCommand cmd = new SqlCommand(QueryMax, con_JVS);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "")
        {
            MaxAID = Convert.ToString(Convert.ToInt32(str3) + 1);
        }
        else
        {
            MaxAID = Convert.ToString(1);
        }
        return MaxAID;
    }

    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "";
        qry = "select NodalOfficeName,NodalOfficerphone from tbl_MetaData_DEPOT where BranchId='" + ddl_branch.SelectedValue.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(qry, con_WLC);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            txtManagerNM.Text = dt.Rows[0]["NodalOfficeName"].ToString();
            txtcugno.Text = dt.Rows[0]["NodalOfficerphone"].ToString();
        }
        else
        {
            txtManagerNM.Text = "";
            txtcugno.Text = "";
        }
        ModalPopupExtender1.Show();
    }
    protected void Gridview_OfficerPreviousInsp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void Gridview_OfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = Gridview_OfficerPreviousInsp.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            string hdnEmpID = (row.FindControl("hdnEmpID") as HiddenField).Value;
            Session["hdnId"] = hdnId.ToString();
            fillInformation(hdnId, hdnEmpID);
            ModalPopupExtender1.Show();
        }
        //if (e.CommandName == "RemoveRow")
        //{
        //    //Determine the RowIndex of the Row whose Button was clicked.
        //    int rowIndex = Convert.ToInt32(e.CommandArgument);

        //    //Reference the GridView Row.
        //    GridViewRow row = Gridview_OfficerPreviousInsp.Rows[rowIndex];

        //    //Fetch value of Name.
        //    string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
        //    string hdnEmpID = (row.FindControl("hdnEmpID") as HiddenField).Value;
        //    Session["hdnId"] = hdnId.ToString();
        //    RemoveRow(hdnId, hdnEmpID);
        //    ModalPopupExtender1.Show();

        //}
    }

    public void fillInformation(string id, string hdnEmpid)
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();
            }
            SqlCommand cmd = new SqlCommand("Get_Employee_Data_For_Edit", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id.ToString());
            cmd.Parameters.AddWithValue("@Employee_ID", hdnEmpid.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }
            if (dt.Rows.Count > 0)
            {

                btn_saveInspDate.Text = "Update";
                ddlquater.SelectedValue = dt.Rows[0]["Inspection_type_ID"].ToString();
                fillMonthQuarterWise();
                ddlmonth.SelectedValue = dt.Rows[0]["Inspection_month_ID"].ToString();
                ddlverification.SelectedValue = dt.Rows[0]["Verification_Type"].ToString();
                ddl_dist.SelectedValue = dt.Rows[0]["District_ID"].ToString();
                ddl_dist_SelectedIndexChanged(null, null);
                ddl_branch.SelectedValue = dt.Rows[0]["Branch_ID"].ToString();
                txt_InspDate.Text = dt.Rows[0]["Order_Date"].ToString();
                txtManagerNM.Text = dt.Rows[0]["Branch_Manager_Name"].ToString();
                txtcugno.Text = dt.Rows[0]["Manager_CUG_No"].ToString();
                txt_OrderNo.Text = dt.Rows[0]["Order_No"].ToString();

                Session["S_InspID"] = id.ToString();
                ModalPopupExtender1.Show();
                GetEmployeeDetails();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('!')", true);
        }
        finally
        { if (conStr.State == ConnectionState.Open) { conStr.Close(); } }

    }

    public void RemoveRow(string id, string hdnEmpid)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {


            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();
            }

            SqlCommand cmd = new SqlCommand("Employee_Data_Remove_Rows", conStr
                );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id.ToString());
            cmd.Parameters.AddWithValue("@Employee_ID", hdnEmpid.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                GetEmployeeDetails();
                ModalPopupExtender1.Show();
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
    protected void Gridview_OfficerPreviousInsp_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/RO/ScheduleInspection.aspx");
    }

    public void cleardata()
    {
        txt_InspDate.Text = "";
        txt_OrderNo.Text = "";
        txtCug.Text = ""; ddl_branch.SelectedIndex = -1; ddl_dist.SelectedIndex = -1; ddlquater.SelectedIndex = -1;
        txtcugno.Text = ""; txtDesig.Text = ""; txtInspOffName.Text = ""; txtManagerNM.Text = ""; ddlquater.SelectedIndex = -1;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/RO/ScheduleInspection.aspx");
    }
    public void fillMonthQuarterWise()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Month_Quater_Wise", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Quarter_ID", ddlquater.SelectedValue);
            con.Open();
            ddlmonth.DataSource = cmd.ExecuteReader();
            ddlmonth.DataTextField = "Month_Name";
            ddlmonth.DataValueField = "ID";
            ddlmonth.DataBind();
            ddlmonth.Items.Insert(0, new ListItem("-- Select Month --", "0"));
            con.Close();
        }
    }
    protected void ddlquater_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMonthQuarterWise();
        ModalPopupExtender1.Show();
    }

    protected void ddlteam_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillInpOff_Grid();
    }
}
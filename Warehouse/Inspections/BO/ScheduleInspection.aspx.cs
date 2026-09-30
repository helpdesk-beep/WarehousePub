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
            fillInpOff_Grid();
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
            SqlCommand cmd = new SqlCommand("Get_Employee_Details_Region_Wise", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Region_ID", Session["UserId"].ToString());
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
            }
            else
            {

                Gridview_IsnpOff.DataSource = null;
                Gridview_IsnpOff.DataBind();
                lblOfficerList.Text = "0";
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('............!')", true);
        }
        finally
        { if (conStr.State == ConnectionState.Open) { conStr.Close(); } }
    }
    //public void fillInpOff_Grid()
    //{
    //    string strsql = "select PF_ID,Officer_Name,Designation,Rec_Office,CUG_mobileNo from tbl_metadata_Inspection_officer where IsActive='Y' ORDER BY Officer_Name";
    //    SqlCommand cmd = new SqlCommand(strsql, con_JVS);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        Gridview_IsnpOff.DataSource = ds;
    //        Gridview_IsnpOff.DataBind();
    //        lblOfficerList.Text = Convert.ToString(ds.Tables[0].Rows.Count);
    //    }
    //    else
    //    {
    //        Gridview_IsnpOff.DataSource = null;
    //        Gridview_IsnpOff.DataBind();
    //        lblOfficerList.Text = "0";
    //    }
    //}
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
            strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' and CategoryID='Y' and BranchID not in (select Distinct Branch_ID from tbl_Inpection_Scheduled_Date) order by Depotname";
        }
        else if (btn_saveInspDate.Text == "Update")
        {
            strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' and CategoryID='Y' order by Depotname";
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
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
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

    public void fillScheduleInsp_Grid(string PFID)
    {
        string strsql = "select Inspection_ID,PF_ID,(select Officer_Name  from tbl_metadata_Inspection_officer where PF_ID=ISD.PF_ID) as Officer_Name,(select district_name from tbl_metadata_district as MDDIS where MDDIS.District_id=ISD.District_ID) as distirct_name,(select Depotname from tbl_metadata_depot as MDD where MDD.branchID=ISD.Branch_ID) as Depotname,convert(varchar(10),Order_Date,103) as Inspection_Date, Inspection_Status,Insp_Type,Insp_Period from tbl_Inpection_Scheduled_Date as ISD where PF_ID='" + PFID + "' order by Inspection_Date desc";
        SqlCommand cmd = new SqlCommand(strsql, con_JVS);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
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
    //protected void Display(object sender, EventArgs e)
    //{
    //    GridViewRow gvr = Gridview_IsnpOff.SelectedRow;
    //    Session["S_PFID"] = gvr.Cells[0].Text;
    //    GetDist(Session["UserName"].ToString());
    //    txtInspOffName.Text = gvr.Cells[1].Text;
    //    txtDesig.Text = gvr.Cells[2].Text;
    //    txtCug.Text = gvr.Cells[4].Text;
    //    fillScheduleInsp_Grid(gvr.Cells[0].Text);
    //    divNewInsp.Visible = true;
    //}

    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Gridview_IsnpOff.Rows[rowIndex];

        Session["S_PFID"] = (row.FindControl("hdnpfid") as HiddenField).Value;
        GetDist(Session["UserName"].ToString());
        txtInspOffName.Text = (row.FindControl("lbname") as Label).Text;
        txtDesig.Text = (row.FindControl("lbldesignation") as Label).Text; ;
        txtCug.Text = (row.FindControl("lblcugmobile") as Label).Text;
     
        fillScheduleInsp_Grid(Session["S_PFID"].ToString());
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
    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        string client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con_JVS.State == ConnectionState.Closed)
        {
            con_JVS.Open();
        }
        if (txtInspOffName.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Officer Name CanNot be Blank...')", true);
        }
        else if (txtCug.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Officer CUG Mobile No. CanNot be Blank...')", true);
        }
        else if (ddl_dist.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District...')", true);
        }
        else if (ddl_branch.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Branch...')", true);
        }
        else if (txt_InspDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Inspection Date...')", true);
        }
        else if (txtManagerNM.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Branch Manager Name...')", true);
        }
        else if (txtcugno.Text.Length != 10)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter 10 Digit Branch Manager Mobile No...')", true);
        }
        else
        {
            if (btn_saveInspDate.Text == "Submit")
            {
                GridViewRow gvr = Gridview_IsnpOff.SelectedRow;
                string PFID = gvr.Cells[0].Text;
                string AID = "";
                AID = chkAID(PFID);
                string inspID = "";
                inspID = PFID + AID;
                string strsql = "INSERT INTO [tbl_Inpection_Scheduled_Date] ([Inspection_ID],[PF_ID],[District_ID],[Branch_ID],[Order_Date],[Inspection_Status],[CreatedBy],[CreatedDate],[AID],[BranchManagerName],[BranchManagerCUGNo],[Order_No],[Insp_Period],[Insp_Type])     VALUES ('" + inspID + "' ,'" + PFID + "','" + ddl_dist.SelectedValue.ToString() + "','" + ddl_branch.SelectedValue.ToString() + "','" + getDate_MDY(txt_InspDate.Text) + "','Pending','" + client_IP + "',GETDATE(),'" + AID + "','" + txtManagerNM.Text + "','" + txtcugno.Text + "','" + txt_OrderNo.Text + "','" + ddl_InspPeriod.SelectedItem.Text + "','" + ddl_insptype.SelectedValue + "') ";
                SqlCommand cmd = new SqlCommand(strsql, con_JVS);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    cleardata();
                    fillScheduleInsp_Grid(PFID);
                    btn_saveInspDate.Enabled = false;
                    //pnlofferpopup.Visible = true;
                    ModalPopupExtender1.Show();
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Save...')", true);
                }
            }
            else if (btn_saveInspDate.Text == "Update")
            {
                string inspID = Session["S_InspID"].ToString();
                GridViewRow gvr = Gridview_IsnpOff.SelectedRow;
                string PFID = gvr.Cells[0].Text;
                string strsql = "Update [tbl_Inpection_Scheduled_Date] set [District_ID]='" + ddl_dist.SelectedValue.ToString() + "',[Branch_ID]='" + ddl_branch.SelectedValue.ToString() + "',[Order_Date]='" + getDate_MDY(txt_InspDate.Text) + "',[Inspection_Status]='Pending',UpdateBy='" + client_IP + "', UpdatedDate=GETDATE(),[BranchManagerName]='" + txtManagerNM.Text + "',[BranchManagerCUGNo]='" + txtcugno.Text + "',[Order_No]='" + txt_OrderNo.Text + "',[Insp_Period]='" + ddl_InspPeriod.SelectedItem.Text + "',[Insp_Type]='" + ddl_insptype.SelectedValue + "' where Inspection_ID='" + inspID + "' and PF_ID='" + PFID + "' ";
                SqlCommand cmd = new SqlCommand(strsql, con_JVS);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    fillScheduleInsp_Grid(PFID);
                    btn_saveInspDate.Enabled = false;
                    cleardata();
                    // ModalPopupExtender1.Show();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Update ...')", true);
                }
            }
        }
        con_JVS.Close();
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
        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        string inspid = Gridview_OfficerPreviousInsp.DataKeys[row.RowIndex].Value.ToString();

        if (con_JVS.State == ConnectionState.Closed)
        {
            con_JVS.Open();
        }
        if (e.CommandName == "Delete")
        {
            string qry = " Delete tbl_Inpection_Scheduled_Date where Inspection_ID='" + inspid + "'";
            SqlCommand cmd = new SqlCommand(qry, con_JVS);
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Successfully Deleted...')", true);
                fillScheduleInsp_Grid(Session["S_PFID"].ToString());
            }
            else
            {

            }
        }
        else if (e.CommandName == "Edit")
        {
            string qry = "select Inspection_ID,District_ID,Branch_ID,convert(varchar(10),Order_Date,103) as Order_Date,BranchManagerName,BranchManagerCUGNo,Order_No,Insp_Type,Insp_Period from tbl_Inpection_Scheduled_Date where Inspection_ID='" + inspid + "'";
            SqlDataAdapter da = new SqlDataAdapter(qry, con_JVS);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                btn_saveInspDate.Text = "Update";
                ddl_dist.SelectedValue = dt.Rows[0]["District_ID"].ToString();
                ddl_dist_SelectedIndexChanged(null, null);
                ddl_branch.SelectedValue = dt.Rows[0]["Branch_ID"].ToString();
                txt_InspDate.Text = dt.Rows[0]["Order_Date"].ToString();
                txtManagerNM.Text = dt.Rows[0]["BranchManagerName"].ToString();
                txtcugno.Text = dt.Rows[0]["BranchManagerCUGNo"].ToString();
                txt_OrderNo.Text = dt.Rows[0]["Order_No"].ToString();

                Session["S_InspID"] = inspid;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error To Fatch Record According this Inspection ID / No record Found...')", true);
            }
        }
    }
    protected void Gridview_OfficerPreviousInsp_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ScheduleInspection.aspx");
    }
    protected void ddl_insptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_insptype.SelectedItem.Text == "Physical Verification")
        {
            ddl_InspPeriod.Items.Clear();
            ddl_InspPeriod.Items.Insert(0, "--Select--");
            ddl_InspPeriod.Items.Insert(1, "Apr-Jun");
            ddl_InspPeriod.Items.Insert(2, "Jul-Sep");
            ddl_InspPeriod.Items.Insert(3, "Oct-Dec");
            ddl_InspPeriod.Items.Insert(4, "Jan-Mar");
        }
        else if (ddl_insptype.SelectedItem.Text == "Half Yearly Inspection")
        {
            ddl_InspPeriod.Items.Clear();
            ddl_InspPeriod.Items.Insert(0, "--Select--");
            ddl_InspPeriod.Items.Insert(1, "Apr-Sep");
            ddl_InspPeriod.Items.Insert(2, "Oct-Mar");
        }
        else if (ddl_insptype.SelectedItem.Text == "Special PV/INSP")
        {
            ddl_InspPeriod.Items.Clear();
            ddl_InspPeriod.Items.Insert(0, "--Select--");
            ddl_InspPeriod.Items.Insert(1, "2019-20");
        }
        else
        {
            ddl_InspPeriod.Items.Clear();
        }
        ModalPopupExtender1.Show();
    }
    public void cleardata()
    {
        txt_InspDate.Text = "";
        txt_OrderNo.Text = "";
        txtCug.Text = ""; ddl_branch.SelectedIndex = -1; ddl_dist.SelectedIndex = -1; ddl_InspPeriod.SelectedIndex = -1;
        txtcugno.Text = ""; txtDesig.Text = ""; txtInspOffName.Text = ""; txtManagerNM.Text = ""; ddl_insptype.SelectedIndex = -1;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("ScheduleInspection.aspx");
    }
}
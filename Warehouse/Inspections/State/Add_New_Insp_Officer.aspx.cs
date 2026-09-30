using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;

public partial class Inspections_State_Add_New_Insp_Officer : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        lbl_user.Text = Session["UserName"].ToString();
        if (!IsPostBack)
        {
            fillInpOff_Grid();
            GetDist(lbl_user.Text);
        }
    }
   
    protected void Display(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/State_Welcome.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/Default.aspx");
    }
    private void GetDist(string region)
    {
        try
        {
            string strDist = "";
            if (region != "HOMPWLC")
            {
                strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Regionnm='" + region + "'order by District_Name";
            }
            else
            {
                strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
            }
            SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
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
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    protected void ddl_recoffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_recoffice.SelectedValue == "RO" || ddl_recoffice.SelectedValue == "HO")
        {
            trDist.Visible = false;           
        }
        else
        {
            trDist.Visible = true;          
        }
        ModalPopupExtender1.Show();
    }
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepot(ddl_dist.SelectedValue.ToString());
        ModalPopupExtender1.Show();
    }
    private void GetDepot(string DistID)
    {
        try
        {
            string strDist = "";
            strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' and  CategoryID='Y'order by Depotname";
            SqlDataAdapter da = new SqlDataAdapter(strDist, conStr);
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
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btn_addnewoff_Click(object sender, EventArgs e)
    {
        string client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string chkcugno = "";
        string str = "";
        int strCUG = 0;
        string AID = "";
        string strDist = "";
        string strBranch = "";
        string strRegion = "";
        if (txtcug.Text != "")
        {
            // chkcugno = txtcug.Text.Substring(0, 7);
            strCUG = Convert.ToInt32(txtcug.Text.Length);
        }
        if (ddl_recoffice.SelectedValue != "HO" && ddl_recoffice.SelectedValue != "RO")
        {
            strDist = ddl_dist.SelectedValue.ToString();
            strBranch = ddl_branch.SelectedValue.ToString();
            con_WLC.Open();
            string QueryMax = "SELECT Region_ID FROM tbl_MetaData_DISTRICT where District_Id='" + strDist + "'";
            SqlCommand cmd = new SqlCommand(QueryMax, con_WLC);
            strRegion = cmd.ExecuteScalar().ToString();
            con_WLC.Close();
            // strRegion = Session["UserId"].ToString();
        }
        else
        {
            strDist = "";
            strBranch = "";
            strRegion = "";
        }

        if (txtofficername.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter CUG mobile No...')", true);
        }
        else if (txtMob.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Personal Mobile No...')", true);
        }
        else if (txtMob.Text.Length != 10)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter 10 Digit Mobile No...')", true);
        }
        else if (txtcug.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter CUG/Alternate mobile No...')", true);
        }
        else if (strCUG != 10)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter 10 DIgit CUG/Alternate mobile No...')", true);
        }
        //else if (txtdob.Text == "")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter DOB...')", true);
        //}
        else if (ddl_recoffice.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Recruitment Office...')", true);
        }
        else if (ddl_Desig.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Designation...')", true);
        }
        else if (ddl_recoffice.SelectedValue == "DO" && ddl_dist.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District...')", true);
        }
        
        //else if (txt_doj.Text == "")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Date Of Joining...')", true);
        //}
        else
        {
            conStr.Open();
            int chkpfid = ChkPF_ID();
            //if (chkpfid == 0)
            //{
                AID = chkAID();
                SqlCommand cmd = new SqlCommand("tbl_metadata_Inspection_officer_Insert", conStr);
                cmd.CommandType = CommandType.StoredProcedure;
                //conStr.Open();
                cmd.Parameters.AddWithValue("@Officer_Name", txtofficername.Text);
                cmd.Parameters.AddWithValue("@Per_MobileNo", txtMob.Text);
                cmd.Parameters.AddWithValue("@CUG_mobileNo", txtcug.Text);
                //cmd.Parameters.AddWithValue("@DOB", getDate_MDY(txtdob.Text));
                cmd.Parameters.AddWithValue("@Rec_Office", ddl_recoffice.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Designation", ddl_Desig.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@Region_ID", strRegion);
                cmd.Parameters.AddWithValue("@District_ID", strDist);
                cmd.Parameters.AddWithValue("@Branch_ID", strBranch);
                cmd.Parameters.AddWithValue("@PF_ID", txtMob.Text);
               // cmd.Parameters.AddWithValue("@DOJ", getDate_MDY(txt_doj.Text));
                cmd.Parameters.AddWithValue("@CreatedBy", client_IP);
                cmd.Parameters.AddWithValue("@AID", AID);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    SqlCommand cmd1 = new SqlCommand("Insp_Officer_login_Insert", conStr);
                    cmd1.CommandType = CommandType.StoredProcedure;
                   // conStr.Open();
                    cmd1.Parameters.AddWithValue("@Region_ID", strRegion);
                    cmd1.Parameters.AddWithValue("@OfficerName", txtofficername.Text);
                    cmd1.Parameters.AddWithValue("@PF_ID", txtMob.Text);
                    cmd1.Parameters.AddWithValue("@O_Password", "Insp@2018");
                    cmd1.Parameters.AddWithValue("@Master_Password", "nic");
                    cmd1.Parameters.AddWithValue("@CreatedBy", client_IP);                    
                    cmd1.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd1.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd1.ExecuteNonQuery();
                    string TheResult1 = cmd1.Parameters["@TheResult"].Value.ToString();

                    if (TheResult1.StartsWith("SUCCESS"))
                    {
                    fillInpOff_Grid();
                    cleardata();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Save...')", true);
                    }
                }                
           // }
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('This PF ID Already Exist...')", true);
            //}
           // conStr.Close();
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
    public int ChkPF_ID()
    {
        int chk = 0;
        string strsql = "select * from tbl_metadata_Inspection_officer where PF_ID='" + txtMob.Text + "'";
        SqlCommand cmd = new SqlCommand(strsql, conStr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 1)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }
        return chk;
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        //  Response.Redirect("AddNewInspOfficer.aspx");
        cleardata();
    }
    public string chkAID()
    {
        string MaxInsID = "";
        string QueryMax = "select MAX(A_ID) as A_ID from tbl_metadata_Inspection_officer";
        SqlCommand cmd = new SqlCommand(QueryMax, conStr);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "")
        {
            MaxInsID = Convert.ToString(Convert.ToInt32(str3) + 1);
        }
        else
        {
            MaxInsID = Convert.ToString(1);
        }
        return MaxInsID;
    }

    public void fillInpOff_Grid()
    {
       // string strsql = "select PF_ID,Officer_Name,Designation,Rec_Office,CUG_mobileNo from tbl_metadata_Inspection_officer where IsActive='Y' ORDER BY Officer_Name";
        SqlCommand cmd = new SqlCommand("Get_Employee_Details_For_Update", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Gridview_IsnpOff.DataSource = ds;
            Gridview_IsnpOff.DataBind();
            lblOfficerList.Text = Convert.ToString(ds.Tables[0].Rows.Count);
        }
        else
        {
            Gridview_IsnpOff.DataSource = null;
            Gridview_IsnpOff.DataBind();
            lblOfficerList.Text = "0";
        }
    }

  
    public void cleardata()
    {
       // txt_doj.Text = "";  
        txtcug.Text = ""; 
        //txtdob.Text = ""; 
        txtMob.Text = ""; txtofficername.Text = "";
        ddl_branch.SelectedIndex = -1; ddl_Desig.SelectedIndex = -1; ; ddl_recoffice.SelectedIndex = -1; ddl_dist.SelectedIndex = -1;
    }

    protected void Display2(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Gridview_IsnpOff.Rows[rowIndex];

        Session["S_PFID"] = (row.FindControl("hdnpfid") as HiddenField).Value;
        GetDist(Session["UserId"].ToString());
        
        ddl_recoffice.SelectedValue = (row.FindControl("lblRec_Office") as Label).Text;
        if (ddl_recoffice.SelectedValue == "RO" || ddl_recoffice.SelectedValue == "HO")
        {
            trDist.Visible = false;
        }
        else
        {
            trDist.Visible = true;
            ddl_dist.SelectedValue = (row.FindControl("hdndstid") as HiddenField).Value;
            ddl_dist_SelectedIndexChanged(null, null);

        }
        ddl_branch.SelectedValue = (row.FindControl("hdnbranchid") as HiddenField).Value;
        ddl_Desig.SelectedValue = (row.FindControl("lbldesignation") as Label).Text;
       // txt_doj.Text = (row.FindControl("hdndoj") as HiddenField).Value;
        txtofficername.Text = (row.FindControl("lbname") as Label).Text;
        txtMob.Text = (row.FindControl("hdnmobileno") as HiddenField).Value;
        txtcug.Text = (row.FindControl("lblcugmobile") as Label).Text;
       // txtdob.Text = (row.FindControl("hdndob") as HiddenField).Value;
        //fillScheduleInsp_Grid(Session["S_PFID"].ToString());
        btn_addnewoff.Text = "Update";
        ModalPopupExtender1.Show();
    }
   
}
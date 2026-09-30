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

public partial class Inspections_BO_Add_New_Insp_Officer : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd;
    DataTable dt = new DataTable();

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
        Response.Redirect("State_Welcome.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("InspectionLogin.aspx");
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
            //strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' and  CategoryID='Y'order by Depotname";
            strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' order by Depotname";
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
       // string strRegion = "";
        if (txtcug.Text != "")
        {
            // chkcugno = txtcug.Text.Substring(0, 7);
            strCUG = Convert.ToInt32(txtcug.Text.Length);
        }
        //if (ddl_recoffice.SelectedValue != "HO" && ddl_recoffice.SelectedValue != "RO")
        //{
        //    strDist = ddl_dist.SelectedValue.ToString();
        //    strBranch = ddl_branch.SelectedValue.ToString();
        //    con_WLC.Open();
        //    string QueryMax = "SELECT Region_ID FROM tbl_MetaData_DISTRICT where District_Id='" + strDist + "'";
        //    SqlCommand cmd = new SqlCommand(QueryMax, con_WLC);
        //    strRegion = cmd.ExecuteScalar().ToString();
        //    con_WLC.Close();
        //    // strRegion = Session["UserId"].ToString();
        //}
        //else
        //{
        //    strDist = "";
        //    strBranch = "";
        //    strRegion = "";
        //}

        if (txtofficername.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter CUG mobile No...')", true);
            txtofficername.Focus();
            ModalPopupExtender1.Show();
        }
        else if (txtMob.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Personal Mobile No...')", true);
            txtMob.Focus();
            ModalPopupExtender1.Show();
        }
        else if (txtMob.Text.Length != 10)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter 10 Digit Mobile No...')", true);
            txtMob.Focus();
            ModalPopupExtender1.Show();
        }
        else if (txtcug.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter CUG/Alternate mobile No...')", true);
            txtcug.Focus();
            ModalPopupExtender1.Show();
        }
        else if (strCUG != 10)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter 10 DIgit CUG/Alternate mobile No...')", true);           
            ModalPopupExtender1.Show();
        }
        else if (txtdob.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter DOB...')", true);
            txtdob.Focus();
            ModalPopupExtender1.Show();
        }
        else if (ddl_recoffice.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Recruitment Office...')", true);
            ddl_recoffice.Focus();
            ModalPopupExtender1.Show();
        }
        else if (ddl_Desig.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Designation...')", true);
            ddl_Desig.Focus();
            ModalPopupExtender1.Show();
        }
        else if (ddl_recoffice.SelectedValue == "DO" && ddl_dist.SelectedItem.Text == "--Select--")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District...')", true);
            ModalPopupExtender1.Show();
        }
        else if (txt_pfid.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Unique ID / PF ID...')", true);
            txt_pfid.Focus();
            ModalPopupExtender1.Show();
        }
        else if (txt_doj.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Date Of Joining...')", true);
            txt_doj.Focus();
            ModalPopupExtender1.Show();
        }
        else
        {
            conStr.Open();
            int chkpfid = ChkPF_ID();
            if (chkpfid == 0)
            {
                AID = chkAID();
                str = "INSERT INTO [tbl_metadata_Inspection_officer] ([Officer_Name],[Per_MobileNo],[CUG_mobileNo],[DOB],[Rec_Office],[Designation],[Region_ID],[District_ID],[Branch_ID],[PF_ID],[DOJ],[CreatedBy],[CreatedDate],[IsActive],[A_ID]) VALUES ('" + txtofficername.Text + "','" + txtMob.Text + "','" + txtcug.Text + "','" + getDate_MDY(txtdob.Text) + "','" + ddl_recoffice.SelectedValue.ToString() + "','" + ddl_Desig.SelectedValue.ToString() + "','" + Session["UserId"].ToString() + "','" + strDist + "','" + strBranch + "','" + txt_pfid.Text + "','" + getDate_MDY(txt_doj.Text) + "','" + client_IP + "',GETDATE(),'Y','" + AID + "') ";
                SqlCommand cmd = new SqlCommand(str, conStr);
                int a = cmd.ExecuteNonQuery();
                if (a == 1)
                {
                    string strsql = "";
                    strsql = "INSERT INTO [Insp_Officer_login] ([Region_ID],[OfficerName],[PF_ID],[O_Password],[Master_Password],[CreatedBy],[CreatedDate]) VALUES ('" + Session["UserId"].ToString() + "','" + txtofficername.Text + "','" + txt_pfid.Text + "','Insp@2018','nic','" + client_IP + "',GETDATE() )";
                    SqlCommand cmd1 = new SqlCommand(strsql, conStr);
                    int a1 = cmd1.ExecuteNonQuery();
                    if (a1 == 1)
                    {
                        fillInpOff_Grid();
                        cleardata();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Save...')", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('This PF ID Already Exist...')", true);
            }
            conStr.Close();
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
        string strsql = "select * from tbl_metadata_Inspection_officer where PF_ID='" + txt_pfid.Text + "'";
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
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Get_Employee_Details_Branch_Wise", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
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
    //    SqlCommand cmd = new SqlCommand(strsql, conStr);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
            
    //    }
    //    else
    //    {
           
    //    }
    //}

  
    public void cleardata()
    {
        txt_doj.Text = ""; txt_pfid.Text = ""; txtcug.Text = ""; txtdob.Text = ""; txtMob.Text = ""; txtofficername.Text = "";
        ddl_branch.SelectedIndex = -1; ddl_Desig.SelectedIndex = -1; ; ddl_recoffice.SelectedIndex = -1; ddl_dist.SelectedIndex = -1;
    }

}
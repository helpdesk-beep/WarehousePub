using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Inspections_State_Godown_Entry_For_Data : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        lbl_user.Text = Session["UserName"].ToString();
        if (!IsPostBack)
        {
            fillFinsncilYear();
            GetDist();
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके गोदाम का fumigation  लंबित है , कृपया fumigation की कार्यवाही करे')", true);
        }
        // lbl_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
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
    //protected void LinkButton1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("InspectionLogin.aspx");
    //}
    //protected void btnNewReg_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("/Inspections/BO/Add_New_Insp_Officer.aspx");
    //}
    //protected void btnPaymentReg_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("ScheduleInspection.aspx");
    //}
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
    public void fillEMPDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Employee_For_Branch_Wise", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
            con.Open();
            ddlemp.DataSource = cmd.ExecuteReader();
            ddlemp.DataTextField = "Officer_Name";
            ddlemp.DataValueField = "Employee_ID";
            ddlemp.DataBind();
            ddlemp.Items.Insert(0, new ListItem("-- Select Employee --", "0"));
            con.Close();
        }
    }
    protected void btnupdatereg_Click(object sender, EventArgs e)
    {
        if (ddlbranch.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch Name')", true);
            ddlbranch.Focus();
            return;
        }

        if (ddlemp.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Employee Name')", true);
            ddlemp.Focus();
            return;
        }

        if (ddlverification.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Inspection Type')", true);
            ddlverification.Focus();
            return;
        }
        if (ddlquater.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Quarter')", true);
            ddlquater.Focus();
            return;
        }

        if (string.IsNullOrEmpty(txtdob.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Inspection Date')", true);
            txtdob.Focus();
            return;
        }
        if (ddlfinancialyear.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Financial Year')", true);
            ddlfinancialyear.Focus();
            return;
        }
        else
        {
            //fillgrid();
            CheckAllreadyExist();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("State_ViewFillAnexB.aspx");
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
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Sync_Stack_Wise_Data_Insert_Inspection_officer", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtdob.Text));
                cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                cmd.Parameters.AddWithValue("@Employee_ID", ddlemp.SelectedValue);
                cmd.Parameters.AddWithValue("@Inspection_Type_ID", ddlverification.SelectedValue);
                cmd.Parameters.AddWithValue("@Quater_Type", ddlquater.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {

                        sda.Fill(dt);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('data inserted successfully !')", true);
                        //if (dt.Rows.Count > 0)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('data inserted successfully !')", true);
                        //}
                        //Session["dateofclossing"] = getDate_MDY(txtdob.Text);
                        //Session["Financialyear"] = ddlfinancialyear.SelectedValue;
                        //Session["Employeeid"] = ddlemp.SelectedValue;
                        //Session["Verificaitontype"] = ddlverification.SelectedValue;
                        //Session["quaterid"] = ddlquater.SelectedValue;
                        // Response.Redirect("/Warehouse/Inspections/BO/Owned_Godown_wise_Stock_Details.aspx");
                    }
                }
            }
        }
    }
    public void CheckAllreadyExist()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("[dbo].[Check_Data_in_Sync_table]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
        cmd.Parameters.AddWithValue("@Employee_ID", ddlemp.SelectedValue);
        cmd.Parameters.AddWithValue("@Inspection_Type_ID", ddlverification.SelectedValue);
        cmd.Parameters.AddWithValue("@Quater_Type", ddlquater.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            // txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();
            if (dt.Rows[0]["Employee_ID"].ToString() == "YES")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
            }
            else if (dt.Rows[0]["Employee_ID"].ToString() == "NO")
            {
                fillgrid();
            }
        }

    }
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("State_ViewAnexBFillStock_AvlStock_.aspx");
    //}

    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strDist = "";
        strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + ddl_dist.SelectedValue + "' order by Depotname"; ;
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_dist.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillEMPDetails();
    }
}
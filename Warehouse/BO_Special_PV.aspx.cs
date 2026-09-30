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
public partial class Special_PV_BO_Special_PV : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (Session["role"] != null)
        {
            if (!IsPostBack)
            {
                // fillEMPDetails();
                //fillFinsncilYear();
                //lbl_user.Text = Session["UserName"].ToString();
            }
        }
        else
        {
            Session.Abandon();
            Response.Redirect("/Warehouse/Special_PV/Special_PV_Default.aspx");
        }
    }
    //public void fillFinsncilYear()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        SqlCommand cmd = new SqlCommand("Get_Fianancial_Year_For_inspection", con);
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        con.Open();
    //        ddlfinancialyear.DataSource = cmd.ExecuteReader();
    //        ddlfinancialyear.DataTextField = "Financial_Year";
    //        ddlfinancialyear.DataValueField = "Financial_Year";
    //        ddlfinancialyear.DataBind();
    //        ddlfinancialyear.Items.Insert(0, new ListItem("--Select Financial Year--", "0"));
    //        con.Close();
    //    }
    //}
    //public void fillEMPDetails()
    //{
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        SqlCommand cmd = new SqlCommand("Get_Employee_For_Branch_Wise", con);
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
    //        con.Open();
    //        ddlemp.DataSource = cmd.ExecuteReader();
    //        ddlemp.DataTextField = "Officer_Name";
    //        ddlemp.DataValueField = "Employee_ID";
    //        ddlemp.DataBind();
    //        ddlemp.Items.Insert(0, new ListItem("-- Select Employee --", "0"));
    //        con.Close();
    //    }
    //}
    //protected void btnupdatereg_Click(object sender, EventArgs e)
    //{
    //    //if (ddlemp.SelectedValue == "0")
    //    //{
    //    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Employee Name')", true);
    //    //    ddlemp.Focus();
    //    //    return;
    //    //}

    //    if (ddlverification.SelectedValue == "0")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Inspection Type')", true);
    //        ddlverification.Focus();
    //        return;
    //    }
    //    //if (ddlquater.SelectedValue == "0")
    //    //{
    //    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Quarter')", true);
    //    //    ddlquater.Focus();
    //    //    return;
    //    //}

    //    if (string.IsNullOrEmpty(txtdob.Text))
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Inspection Date')", true);
    //        txtdob.Focus();
    //        return;
    //    }
    //    if (ddlfinancialyear.SelectedValue == "0")
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Financial Year')", true);
    //        ddlfinancialyear.Focus();
    //        return;
    //    }
    //    else
    //    {
    //        fillgrid();
    //    }
    //}
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("State_ViewFillAnexB.aspx");
    //}
    //protected string getDate_MDY(string inDate)
    //{
    //    if (inDate == "" || inDate == null)
    //    {
    //        return "01/01/1919";
    //    }
    //    else
    //    {
    //        //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
    //        string converted = "";
    //        string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
    //        converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
    //        return converted;
    //    }
    //}
    //protected void fillgrid()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("Sync_Stack_Wise_Data_Insert_Inspection_officer_For_Special_PV", con))
    //        {
    //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
    //            cmd.Parameters.AddWithValue("@FromDate", getDate_MDY(txtdob.Text));
    //            cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
    //            //cmd.Parameters.AddWithValue("@Employee_ID", ddlemp.SelectedValue);
    //            cmd.Parameters.AddWithValue("@Inspection_Type_ID", ddlverification.SelectedValue);
    //            //cmd.Parameters.AddWithValue("@Quater_Type", ddlquater.SelectedValue);
    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;
    //                using (DataTable dt = new DataTable())
    //                {

    //                    sda.Fill(dt);
    //                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('data inserted successfully !')", true);
    //                }
    //            }
    //        }
    //    }
    //}
    //public void CheckAllreadyExist()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    //    SqlConnection con = new SqlConnection(constr);
    //    SqlCommand cmd = new SqlCommand("[dbo].[Check_Data_in_Sync_table_For_Special_PV]", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
    //    cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
    //    //cmd.Parameters.AddWithValue("@Employee_ID", ddlemp.SelectedValue);
    //    cmd.Parameters.AddWithValue("@Inspection_Type_ID", ddlverification.SelectedValue);
    //   // cmd.Parameters.AddWithValue("@Quater_Type", ddlquater.SelectedValue);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        // txt_inspdate.Text = dt.Rows[0]["Inspection_Date"].ToString();
    //        if (dt.Rows[0]["Employee_ID"].ToString() == "YES")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('आपके द्वारा यहाँ ऑप्शन पहले ही चलाया जा चूका हैं यदि आपके ब्रांच के स्टॉक में कोई त्रुटी हैं तो इसके लिए HO MPWLC की Technical टीम से बात करे !')", true);
    //        }
    //        else if (dt.Rows[0]["Employee_ID"].ToString() == "NO")
    //        {
    //            fillgrid();
    //        }
    //    }

    //}
}
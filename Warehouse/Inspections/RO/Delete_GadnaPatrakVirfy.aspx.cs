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

public partial class Inspections_RO_Delete_GadnaPatrakVirfy : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            GetDist();
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
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillEMPDetails();
    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT where Region_ID='" + Session["UserId"].ToString() + "' order by District_Name";
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
   

    public void fillEMPDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Employee_Details_District_Wise", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_ID", ddl_dist.SelectedValue);
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
            using (SqlCommand cmd = new SqlCommand("Get_Gadana_Patrak_District_Branch_Emp_Delete", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
                cmd.Parameters.AddWithValue("@empId", ddlemp.SelectedValue);
                cmd.Parameters.AddWithValue("@Inspection_type_ID", ddlquater.SelectedValue);
                cmd.Parameters.AddWithValue("@Verification_Type", ddlverification.SelectedValue);
                cmd.Parameters.AddWithValue("@FinancialYear", ddlfinancialyear.SelectedValue);
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
   protected void Gridview_OfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = Gridview_OfficerPreviousInsp.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
           
            Session["hdnId"] = hdnId.ToString();
            RemoveRow(hdnId);
           // RemoveRowJVS(hdnId);

        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ScheduleInspection.aspx");
    }
    public void RemoveRow(string Godownid)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con_JVS.State == ConnectionState.Closed)
        {
            con_JVS.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con_JVS.State == ConnectionState.Closed)
            {
                con_JVS.Open();
            }

            SqlCommand cmd = new SqlCommand("Delete_Gadana_Patrak_District_Branch_Emp_Delete", con_JVS);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Godown_ID", Godownid.ToString());
            cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
            cmd.Parameters.AddWithValue("@Inspection_type_ID", ddlquater.SelectedValue);
            cmd.Parameters.AddWithValue("@Verification_Type", ddlverification.SelectedValue);
            cmd.Parameters.AddWithValue("@empId", ddlemp.SelectedValue);
            cmd.Parameters.AddWithValue("@FinancialYear", ddlfinancialyear.SelectedValue);
            //cmd.Parameters.AddWithValue("@Insert_By", localIP.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                GetEmployeeDetails();
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
    protected void btn_show_Click(object sender, EventArgs e)
    {
        GetEmployeeDetails();
    }

    protected void ddlemp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void ddl_dist_SelectedIndexChanged1(object sender, EventArgs e)
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

    protected void ddlemp_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //ddlverification.SelectedValue
    }
}
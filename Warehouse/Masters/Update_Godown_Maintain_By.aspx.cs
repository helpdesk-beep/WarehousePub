using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;

public partial class Masters_Update_Godown_Maintain_By : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
        GetBranchData();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
   
    public void GetBranchData()
    {
        try
        {
            string qry = "";
          //  qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + Session["BranchId"].ToString() + "' and IsActive='Y'";
            SqlCommand cmd = new SqlCommand("Get_Godown_Details_For_Update_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
           // cmd.Parameters.AddWithValue("@StorageType", ddlWST.SelectedValue.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
               // trmobtxt.Visible = true;
                //lbldistrictid.Text = ds.Tables[0].Rows[0]["DistrictId"].ToString();
                //lblbranchid.Text = ds.Tables[0].Rows[0]["BranchID"].ToString();
                showgrid.Visible = true;
           
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                //trbtnhide.Visible = false;
                //trmobtxt.Visible = false;
                showgrid.Visible = false;

                Depositor_Gridview.DataSource = null;
                Depositor_Gridview.DataBind();
               
            }
        }
        catch (Exception ex)
        {

        }
    }
    public int GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }



    protected void ddlWST_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchData();
    }
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Depositor_Gridview.Rows[rowIndex];

        // lblWHID.Text = (row.FindControl("lblWHID") as Label).Text;
        string hdnWHID = (row.FindControl("hdnWHID") as HiddenField).Value;
        string hdnBranchId = (row.FindControl("hdnBranchId") as HiddenField).Value;
        lblgodownname.Text = (row.FindControl("lblGodown_Name") as Label).Text;
        txtGdwnID.Text = (row.FindControl("hdnWHID") as HiddenField).Value;
        txtlat.Text = (row.FindControl("lblLatitude") as Label).Text;
        txtlong.Text = (row.FindControl("lblLongitude") as Label).Text;

        Session["hdnWHID"] = (row.FindControl("hdnWHID") as HiddenField).Value;
        Session["hdnBranchId"] = (row.FindControl("hdnBranchId") as HiddenField).Value;
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }


    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            if (txtGdwnID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown')", true);
            }

            else if (txtlat.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Lat')", true);
            }
            else if (txtlong.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Long')", true);
            }
            //else if (ddlmaintainby.SelectedValue == "-1")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Maintain By')", true);
            //}
            else
            {
                sqltrans = con.BeginTransaction();
                //  con.Open();
                cmd = new SqlCommand("Update_Lat_Long_Maintain_by_GODOWN_2018", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltrans;
                cmd.Parameters.AddWithValue("@Godown_ID", txtGdwnID.Text);
                cmd.Parameters.AddWithValue("@Latitude", txtlat.Text);
                cmd.Parameters.AddWithValue("@Longitude", txtlong.Text);
                //cmd.Parameters.AddWithValue("@Maintain_By", ddlmaintainby.SelectedValue);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Update Details Successfully')", true);
                    sqltrans.Commit();
                    lblgodownname.Text = "";
                    txtGdwnID.Text = "";
                    Depositor_Gridview.DataSource = "";
                    Depositor_Gridview.DataBind();
                    GetBranchData();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record not Updatet')", true);

                }
            }
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error')", true);
        }
        finally
        {
            con.Close();
        }
    }
}

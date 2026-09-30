using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;

public partial class JointVentureScheme_WarehousePayment : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        try
        {
            lbluser.Text = Session["fname"].ToString() + " " + Session["mname"].ToString() + " " + Session["lname"].ToString();
            lblAuthPerson.Text = Session["fname"].ToString();
            // lblEmail.Text = Session["email"].ToString();
            lblMob.Text = Session["mobile"].ToString();
            //lblDate.Text = Session["DOB"].ToString();
            lblAppType.Text = Session["AppType"].ToString();
            lblDistrict.Text = Session["District"].ToString();
            if (!IsPostBack)
            {
                gerreg();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("UserReg.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //int chk_P = getpayment();
        //if (chk_P == 0)
        //{
            Response.Redirect("https://www.onlinesbi.sbi/sbicollect/icollecthome.htm?corpID=329338");
        //}
        //else if (chk_P==1)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Already Submit Registration Fee ')", true);
        //}
    }

    public void gerreg()
    {
        try
        {
            string regno = Session["Reg_No"].ToString();
            string qry = "select Warehouse_Name,Registration_Id,RegCapacity,CASE WHEN FeesStatus = 'N' THEN 'Pending' ELSE 'Complete'  END as FeesStatus,RegAmt from tbl_WarehouseRegistration where Registration_Id='" + regno + "' ";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblAuthPerson.Text = ds.Tables[0].Rows[0]["Warehouse_Name"].ToString();
                lblRegNo.Text = ds.Tables[0].Rows[0]["Registration_Id"].ToString();
                lblCapt.Text = ds.Tables[0].Rows[0]["RegCapacity"].ToString();
                lblFeestatus.Text = ds.Tables[0].Rows[0]["FeesStatus"].ToString();
                lblTotalfee.Text = ds.Tables[0].Rows[0]["RegAmt"].ToString();
                lblRegFee.Text = ds.Tables[0].Rows[0]["RegAmt"].ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Registration Not Completed /No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
    public int getpayment()
    {
        int chk = 0;
        string regno = Session["Reg_No"].ToString();
        string qry = "select distinct REGISTRATIONID,SUM(FEE) as DepositeFEE from tbl_Payment_Status where REGISTRATIONID='" + regno + "' and CategoryName='REGISTRATION FEE' group by REGISTRATIONID";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositeFEE"].ToString()) >= Convert.ToDecimal(lblTotalfee.Text))
            {
                chk = 1; 
            }
        }
        return chk;
    }
}

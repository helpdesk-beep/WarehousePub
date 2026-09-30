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
using Microsoft.Reporting.WebForms;
using System.Security.Principal;

public partial class BranchPages_Private_Warehouse_Related_Information : System.Web.UI.Page
{
    

    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    decimal ChargeOfTotal = 0;
    decimal RebateAmount = 0;
    decimal NetAmount = 0;
    decimal AccruedNetAmount = 0;
    //string NetAmountWord = "";
    //string Bill_No = "";
    decimal Discount = 0;
    decimal Service_Tax = 0;
    //string Bill_Type = "";
    //int BID = 0;
    decimal ChargeOfTotalWeight = 0;
    DateTime StartDate = new DateTime();
    DateTime EndDate = new DateTime();
    string Crop_Year = "";

    public SqlConnection JVScon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //SqlCommand cmd = new SqlCommand();
    //DataTable dt = new DataTable();
    //DataSet ds = new DataSet();
    //SqlDataAdapter da = new SqlDataAdapter();
    //string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                //FillGrid();
                fillGodownDetails();
                GetRegID();
                trJVSGodownRent.Visible = true;
                
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
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

    public void clear()
    {
        ddlObstruction_BYGO.SelectedValue = "0";
        ddlWarehouse_Infested_Status.SelectedValue = "0";
        ddlRegID.SelectedValue = "0";
        //txtWarehouse_Infested_Date.Text = "";
        txtDate_Of_Obstruction.Text = "";
        txtInfested_Remark.Text = "";
        //txtWarehouse_Infested_Date.Text = "";
        ddl_gdwn.SelectedValue = "0";
        txtDate_Of_Obstruction.Text = "";
        txtInfested_Remark.Text = "";
        //txtInfested_Remark.Text = "";
        txtWarehouse_Infested_Date.Text="";
        

    }

    public void fillGodownDetails()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name_Preivate_Godown", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
            con.Open();
            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }

    //private void FillGrid()
    //{
    //    SqlCommand cmdd = new SqlCommand("Get_Insp_Private_Warehouse_Related_Information",con);
    //    cmdd.CommandType = CommandType.StoredProcedure;
    //    cmdd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
    //    //conn.Open();
    //    SqlDataAdapter da = new SqlDataAdapter(cmdd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {
    //        gvshow.DataSource = dt;
    //        gvshow.DataBind();
    //        //btnUpdate.Visible = false;
    //    }
    //}


    protected void ddlDepositPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlObstruction_BYGO.SelectedValue == "0")
        {
            show1.Visible = false;
        }
       else if (ddlObstruction_BYGO.SelectedValue=="Y")
        {
            show1.Visible = true;
        }
       else if (ddlObstruction_BYGO.SelectedValue == "N")
        {
            show1.Visible = false;
        }
    }

    protected void btnSumbmi_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlObstruction_BYGO.SelectedValue == "0" )
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('please select required field ')", true);
            }
            if (ddlWarehouse_Infested_Status.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('please select required field ')", true);
            }
            if (ddl_gdwn.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('please select Godown Name ')", true);
            }
            if (ddlRegID.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('please select Registration ID ')", true);
            }

           if(ddlObstruction_BYGO.SelectedValue == "Y" && txtObstruction_Remark.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please enter Remark')", true);

            }

            if (ddlWarehouse_Infested_Status.SelectedValue == "Y" && txtInfested_Remark.Text=="")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please enter Remark')", true);

            }

            if (ddlRegID.SelectedValue == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('please select Registration ID ')", true);
            }

            SqlCommand cmd = new SqlCommand("Insp_Private_Warehouse_Related_Information", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString()); 
            cmd.Parameters.AddWithValue("@Godown_Id", ddl_gdwn.SelectedValue);
            cmd.Parameters.AddWithValue("@Registration_ID", ddlRegID.SelectedValue);
            cmd.Parameters.AddWithValue("@Obstruction_BYGO", ddlObstruction_BYGO.SelectedValue);
            cmd.Parameters.AddWithValue("@Date_Of_Obstruction", getDate_MDY(txtDate_Of_Obstruction.Text));
            cmd.Parameters.AddWithValue("@Obstruction_Remark", txtObstruction_Remark.Text);
            cmd.Parameters.AddWithValue("@Warehouse_Infested_Status", ddlWarehouse_Infested_Status.SelectedValue);
            cmd.Parameters.AddWithValue("@Warehouse_Infested_Date", getDate_MDY (txtWarehouse_Infested_Date.Text));
            cmd.Parameters.AddWithValue("@Infested_Remark", txtInfested_Remark.Text);
            //cmd.Parameters.AddWithValue("@Unit", ddlUnitStock.SelectedValue);
            cmd.Parameters.AddWithValue("@Create_by", Request.UserHostAddress);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
           

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Warehouse Details Successfully submitted|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
            }
            //fillGrid();

        
        }
        catch (Exception ex)
        {
            string except = ex.Message.ToString();

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + except + "')", true);
        }
        finally
        {
            con.Close();
        }

        


    }

    protected void ddlWarehouseFoundInfested_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWarehouse_Infested_Status.SelectedValue == "0")
        {
            Tbody1.Visible = false;
        }
        else if (ddlWarehouse_Infested_Status.SelectedValue == "Y")
        {
            Tbody1.Visible = true;
        }
        else if (ddlWarehouse_Infested_Status.SelectedValue == "N")
        {
            Tbody1.Visible = false;
        }
    }
    public void GetRegID()
    {
        ddlRegID.DataSource = "";
        string qry = "";
        qry = "select Registration_ID,UPPER(Warehouse_name) +' ( '+ Registration_ID +' )' as Warehouse_name from tbl_warehouseRegistration as WREG where WREG.BranchId='" + Session["BranchId"].ToString() + "' and RegCapacity>0 order by Warehouse_name";
        SqlCommand cmd = new SqlCommand(qry, JVScon);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRegID.DataSource = ds.Tables[0];
            ddlRegID.DataTextField = "Warehouse_name";
            ddlRegID.DataValueField = "Registration_ID";
            ddlRegID.DataBind();
            ddlRegID.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlRegID.Items.Insert(0, "--Select--");
        }
    }
    }

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

public partial class IssueCenterLevel_Storage_Fill_DayWise_Stock : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Depot_DepotID"].ToString() != "")
        {
            if (!IsPostBack)
            {
                GetGodownWHMS();
              //  GetCommodity();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    public void GetGodownWHMS()
    {
        string qry = "select WHMS_GodownName +' ('+ WHMS_GodownID +')' as GodownName,WHMS_GodownID as Godown_ID from tbl_Stock_Deposite_Gdwn_2018 where BranchID='" + Session["BranchId"].ToString() + "' order by GodownName";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlGodWHMS.DataSource = ds.Tables[0];
            ddlGodWHMS.DataTextField = "GodownName";
            ddlGodWHMS.DataValueField = "Godown_ID";
            ddlGodWHMS.DataBind();
            ddlGodWHMS.Items.Insert(0, "--Select--");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Godown Inialization First Inialization then Submit Opening and Deposite Quantity')", true);
        }

    }

    public void GetCommodity()
    {
        string strDist = "select Commodity_Name,Commodity_Id from tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name asc";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCommodity.DataSource = ds.Tables[0];
            ddlCommodity.DataTextField = "Commodity_Name";
            ddlCommodity.DataValueField = "Commodity_Id";
            ddlCommodity.DataBind();
            ddlCommodity.Items.Insert(0, "--Select--");
        }
    }

    public void GetDepositor()
    {
        string strDist = "SELECT BranchId,DepotName FROM tbl_MetaData_DEPOT where DistrictId='" + ddlCommodity.SelectedValue.ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDepositor.DataSource = ds.Tables[0];
            ddlDepositor.DataTextField = "DepotName";
            ddlDepositor.DataValueField = "BranchId";
            ddlDepositor.DataBind();
            ddlDepositor.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlDepositor.Items.Insert(0, "--Select--");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        if (ddlGodWHMS.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown Name(WHMS)...'); </script> ");
        }
        else if (txtDate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Date...'); </script> ");
        }
        else if (txtStorageCpt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Storage Capacity...'); </script> ");

        }
        else if (txtClosingCpt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Opening Balance....'); </script> ");
        }
        else if (txtDelQty.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Delievery Quantity....'); </script> ");
        }
        else
        {
            if (btnSubmit.Text == "Submit")
            {
                int chkgdwnentry = CheckGodowntoday();
                if (chkgdwnentry == 0)
                {
                    string qry = "INSERT INTO [tbl_DailyStockDepositeEntry_2018] ([WHMS_GodownID],[Deposite_Qty_Today],[Deposite_Date],[Deposite_Qty_PreDay],[CreatedBy],[CreatedDate],[BranchID],[Delievery_Qty_PerDay]) VALUES('" + ddlGodWHMS.SelectedValue.ToString() + "','" + txtStorageCpt.Text + "','" + getDate_MDY(txtDate.Text) + "','" + txtClosingCpt.Text + "','" + ip + "',GETDATE(),'" + Session["BranchId"].ToString() + "','" + txtDelQty.Text + "')";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(qry, con);
                    int a = cmd.ExecuteNonQuery();
                    con.Close();
                    if (a == 1)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Submitted Successfully!!!'); </script> ");
                        txtSelectDate.Text = txtDate.Text;
                        Search();
                    }
                }
                else if (chkgdwnentry == 1)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Deposite Quantity Already Submit For Selected Date !!!'); </script> ");
                    txtSelectDate.Text = txtDate.Text;
                    Search();
                }
            }
            else if (btnSubmit.Text == "Update")
            {
                string qry = "Update tbl_DailyStockDepositeEntry_2018 set Deposite_Qty_Today='" + txtStorageCpt.Text + "',Deposite_Qty_PreDay='" + txtClosingCpt.Text + "',UpdatedBy='" + ip + "',UpdatedDate=GETDATE(),[Delievery_Qty_PerDay]='" + txtDelQty.Text +"' where WHMS_GodownID='" + ddlGodWHMS.SelectedValue.ToString() + "' and convert(varchar(10),Deposite_Date,101)='" + getDate_MDY(txtDate.Text) + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(qry, con);
                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a == 1)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Updated Successfully!!!'); </script> ");
                    txtSelectDate.Text = txtDate.Text;
                    Search();
                }
            }
        }
    }
    protected void  gdnewproc_SelectedIndexChanged(object sender, EventArgs e)
    {
         btnSubmit.Text = "Update";
         GridViewRow gvr = gdnewproc.SelectedRow;
         ddlGodWHMS.SelectedValue = gvr.Cells[2].Text;
         ddlGodWHMS.Enabled = false;
         txtDate.Text = gvr.Cells[3].Text;
         txtDate.Enabled = false;
         txtStorageCpt.Text = gvr.Cells[5].Text;
         txtClosingCpt.Text = gvr.Cells[4].Text;
         txtDelQty.Text = gvr.Cells[6].Text;

         
    }
    protected void ddlCommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepositor();
    }

    public void Search()
    {
        string qry = "select (select Godown_Name from tbl_MetaData_GODOWN as MG where MG.Godown_ID=DSD.WHMS_GodownID ) as Godown_Name,WHMS_GodownID,CONVERT(varchar(10),Deposite_Date,103) as Deposite_Date,Deposite_Qty_Today,Deposite_Qty_PreDay,Delievery_Qty_PerDay from tbl_DailyStockDepositeEntry_2018 as DSD where convert(varchar(10),Deposite_Date,101)='" + getDate_MDY(txtSelectDate.Text) + "' and BranchID='" + Session["BranchId"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gdnewproc.DataSource = ds.Tables[0];
            gdnewproc.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Data Found')", true);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
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
    public int CheckGodowntoday()
    {
        int chk = 0;
        string strsql = "select * from tbl_DailyStockDepositeEntry_2018 where WHMS_GodownID='"+ ddlGodWHMS.SelectedValue.ToString() +"' and convert(varchar(10),Deposite_Date,101)='" + getDate_MDY(txtDate.Text) + "'";
        SqlCommand cmd = new SqlCommand(strsql,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }
        return chk;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenterLevel/Storage/Fill_DayWise_Stock.aspx");
    }
}


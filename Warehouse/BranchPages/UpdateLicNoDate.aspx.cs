using System;
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

public partial class BranchPages_UpdateLicNoDate : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection JVScon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                //getdistrict();
                GetBranchData();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
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
    public void GetBranchData()
    {
        try
        {
           // string qry = "";
            //qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Capacity,Closing_Balance,LicNum,convert(varchar(10),LicDate,103) as LicDate,Godown_Scientific_Capacity  from tbl_metadata_godown_2018 where BranchID='" + Session["BranchId"].ToString() + "' and IsActive='Y'";
            SqlCommand cmd = new SqlCommand("Get_Godown_Data_For_Updatation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
                trmobtxt.Visible = true;
                tr1.Visible = true;
                tr2.Visible = true;
                trbtnhide.Visible = true;
                txtGdwnID.Text = "";
                txtclosing.Text = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                trbtnhide.Visible = false;
                trmobtxt.Visible = false;
                trbtnhide.Visible = false;
                tr1.Visible = false;
                tr2.Visible = false;
                Depositor_Gridview.DataSource = null;
                Depositor_Gridview.DataBind();
                txtGdwnID.Text = "";
                txtclosing.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }
    //public void GetBranch(string distID)
    //{

    //    string qry = "";
    //    qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId ='" + distID + "' order by DepotName";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlBranch.DataSource = ds.Tables[0];
    //        ddlBranch.DataTextField = "DepotName";
    //        ddlBranch.DataValueField = "BranchId";
    //        ddlBranch.DataBind();
    //        ddlBranch.Items.Insert(0, "--Select--");
    //    }
    //}
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
            else if (txtclosing.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Closing Balance')", true);
            }
            else if (txtlicno.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Licence Expiry Date')", true);
            }
            else if (txtlicdate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Licence Expiry Date')", true);
            }
            else if (txtscieCPT.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Scientific Capacity')", true);
            }
            else if (ddlRegID.SelectedValue == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select JVS Registration ID')", true);
            }
            else
            {
                sqltrans = con.BeginTransaction();
              //  con.Open();
                cmd = new SqlCommand("Insert_Gdn_Data_in_2023", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqltrans;
                cmd.Parameters.AddWithValue("@Godown_ID", txtGdwnID.Text);
                cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                cmd.Parameters.AddWithValue("@Godown_Name", lblgodownname.Text);
                cmd.Parameters.AddWithValue("@Closing_Balance", txtclosing.Text.Trim());
                cmd.Parameters.AddWithValue("@LicNum", txtlicno.Text);
                cmd.Parameters.AddWithValue("@LicDate", getDate_MDY(txtlicdate.Text));
                cmd.Parameters.AddWithValue("@Hired_Type", ddllst_hired.SelectedValue);
                cmd.Parameters.AddWithValue("@Storage_Type", ddllst_storage.SelectedValue);
                cmd.Parameters.AddWithValue("@Godown_Scientific_Capacity", txtscieCPT.Text);
                cmd.Parameters.AddWithValue("@Godown_Capacity", txtMaxCpt.Text);
                cmd.Parameters.AddWithValue("@IsActive", ddlactiveinactive.SelectedValue);
                cmd.Parameters.AddWithValue("@Maintain_By", ddlmaintainby.SelectedValue);
                cmd.Parameters.AddWithValue("@jvseRegid", ddlRegID.SelectedValue);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                // qryDel = "update tbl_Warehousing_Contact set Mobile_No='" + txtDFNo.Text + "'  where Branch_Id='" + gvr.Cells[0].Text + "' ";
                //qryDel = "insert into tbl_MetaData_GODOWN_log select * from tbl_metadata_godown where Godown_ID='" + txtGdwnID.Text + "' and BranchID='" + ddlBranch.SelectedValue.ToString() + "'";
                //SqlCommand cmdDel = new SqlCommand(qryDel, con, sqltrans);
                //int b = cmdDel.ExecuteNonQuery();
                //if (b == 1)
                //{
                //    //string qryupdate = "update tbl_metadata_godown_2018 set Closing_Balance='" + txtclosing.Text.Trim() + "', LicNum='" + txtlicno.Text.Trim() + "',LicDate='" + getDate_MDY(txtlicdate.Text.Trim()) + "',Hired_Type='" + ddllst_hired.SelectedItem.Text.Trim() + "',Storage_Type='" + ddllst_storage.SelectedItem.Text.Trim() + "',Godown_Scientific_Capacity='" + txtscieCPT.Text.Trim() + "',UpdatedDate=GETDATE() where Godown_ID='" + txtGdwnID.Text + "' and BranchID='" + ddlBranch.SelectedValue.ToString() + "'";
                //    string qryupdate = "update tbl_metadata_godown_2018 set Godown_Name='" + lblgodownname.Text + "', Closing_Balance='" + txtclosing.Text.Trim() + "', LicNum='" + txtlicno.Text.Trim() + "',LicDate='" + getDate_MDY(txtlicdate.Text.Trim()) + "',Hired_Type='" + ddllst_hired.SelectedItem.Text.Trim() + "',Storage_Type='" + ddllst_storage.SelectedItem.Text.Trim() + "',Godown_Scientific_Capacity='" + txtscieCPT.Text.Trim() + "',Godown_Capacity='" + txtMaxCpt.Text.Trim() + "',UpdatedDate=GETDATE() where Godown_ID='" + txtGdwnID.Text + "' and BranchID='" + ddlBranch.SelectedValue.ToString() + "'";

                //    SqlCommand cmdupdate = new SqlCommand(qryupdate, con, sqltrans);
                //    int c = cmdupdate.ExecuteNonQuery();
                //    if (c == 1)
                //    {
                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Insert Successfully')", true);
                        sqltrans.Commit();
                        trmobtxt.Visible = false;
                        trbtnhide.Visible = false;
                        lblgodownname.Text = "";
                        txtGdwnID.Text = "";
                        txtclosing.Text = "";
                        txtlicno.Text = "";
                        txtlicdate.Text = "";
                        Depositor_Gridview.DataSource = "";
                        Depositor_Gridview.DataBind();
                        GetBranchData();

                    }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record not Inserted')", true);

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
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Depositor_Gridview.Rows[rowIndex];

        lblgodownname.Text = (row.FindControl("lblGodown_Name") as Label).Text;
        txtGdwnID.Text = (row.FindControl("lblGodown_ID") as Label).Text;
        txtMaxCpt.Text = (row.FindControl("lblGodown_Capacity") as Label).Text; ;
        txtscieCPT.Text = (row.FindControl("lblGodown_Scientific_Capacity") as Label).Text;
        //if ((row.FindControl("lblHired_Type") as Label).Text.Equals("JointVenture(JV)"))
        //    (row.FindControl("lblHired_Type") as Label).Text = "Joint Venture(JV)";
        //if ((row.FindControl("lblHired_Type") as Label).Text.Equals("SteelSilo"))
        //    (row.FindControl("lblHired_Type") as Label).Text = "Steel Silo";
        //if ((row.FindControl("lblHired_Type") as Label).Text.Equals("SiloBags"))
        //    (row.FindControl("lblHired_Type") as Label).Text = "Silo Bags";
        fillGodownType();
        ddllst_hired.SelectedValue = (row.FindControl("lblHired_Type") as Label).Text;
        if ((row.FindControl("lblStorage_Type") as Label).Text.Equals("SiloBags"))
            (row.FindControl("lblStorage_Type") as Label).Text = "Silo Bags";
        ddllst_storage.SelectedValue = (row.FindControl("lblStorage_Type") as Label).Text;
        txtclosing.Text = (row.FindControl("lblClosing_Balance") as Label).Text;
        txtlicno.Text = (row.FindControl("lblLicNum") as Label).Text;
        txtlicdate.Text = (row.FindControl("lblLicDate") as Label).Text;
        divNewInsp.Visible = true;
        GetRegID();
        ModalPopupExtender1.Show();
    }
    //protected void Depositor_Gridview_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GridViewRow gvr = Depositor_Gridview.SelectedRow;
    //    txtGdwnID.Text = gvr.Cells[0].Text;
    //    txtclosing.Text = gvr.Cells[6].Text;
    //    txtlicno.Text = gvr.Cells[7].Text;
    //    txtlicdate.Text = gvr.Cells[8].Text;
    //    txtscieCPT.Text = gvr.Cells[4].Text;
    //    txtMaxCpt.Text = gvr.Cells[5].Text;
    //    ddllst_hired.SelectedValue = gvr.Cells[2].Text.Trim();
    //    ddllst_storage.SelectedValue = gvr.Cells[3].Text.Trim();
    //}
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetBranch(DropDownList1.SelectedValue.ToString());
    //}
    //public void getdistrict()
    //{
    //    string qry = "select District_Name,District_Id from tbl_MetaData_DISTRICT order by District_Name ";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        DropDownList1.DataSource = ds.Tables[0];
    //        DropDownList1.DataTextField = "District_Name";
    //        DropDownList1.DataValueField = "District_Id";
    //        DropDownList1.DataBind();
    //        DropDownList1.Items.Insert(0, "--Select--");
    //    }
    //}
    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetBranchData();
    //}
    private void fillGodownType()
    {
        try
        {
            string query = "";
            query = "SELECT  [Gid],[GodownType],[TypeValue] FROM [dbo].[GodownTypeMaster]";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddllst_hired.Items.Clear();
                ddllst_hired.DataSource = ds.Tables[0];
                ddllst_hired.DataTextField = "GodownType";
                ddllst_hired.DataValueField = "GodownType";
                ddllst_hired.DataBind();
            }
            else
            {

            }
        }
        catch (Exception)
        {
        }
    }

}

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


public partial class Region_UpdateJVSGodownLicNoExpDate : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection JVScon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlTransaction sqltrans;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
               //
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
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Depositor_Gridview.DataSource = ds;
                Depositor_Gridview.DataBind();
                trmobtxt.Visible = true;
                trbtnhide.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                trbtnhide.Visible = false;
                trmobtxt.Visible = false;
                trbtnhide.Visible = false;
                Depositor_Gridview.DataSource = null;
                Depositor_Gridview.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void GetBranch()
    {
        string qry = "";
        if (ddlgdwntype.SelectedValue == "JVS")
        {
            qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId in (select MD.District_Id from tbl_MetaData_DISTRICT as MD where MD.Region_ID='" + Session["Region_ID"].ToString() + "') order by DepotName";
        }
        else if (ddlgdwntype.SelectedValue == "OWN")
        {
            qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId ='"+ DDLDistrict.SelectedValue.ToString() +"' order by DepotName";
        }
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ddlgdwntype.SelectedValue == "JVS")
            {
                ddlBranch.DataSource = ds.Tables[0];
                ddlBranch.DataTextField = "DepotName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, "--Select--");
            }
            else if (ddlgdwntype.SelectedValue == "OWN")
            {
                ddlbranchOther.DataSource = ds.Tables[0];
                ddlbranchOther.DataTextField = "DepotName";
                ddlbranchOther.DataValueField = "BranchId";
                ddlbranchOther.DataBind();
                ddlbranchOther.Items.Insert(0, "--Select--");
            }
        }
    }
    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        string Client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            if (txtlicno.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Licence No')", true);
            }
            else if (txtlicexpdate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Licence Exp Date')", true);
            }
            else if (lblgdid.Text == "Select")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select WHMS Godown_id')", true);
            }
            else if (lblgdid.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select WHMS Godown_id')", true);
            }
                sqltrans = con.BeginTransaction();
                string qryDel = "";
                // qryDel = "update tbl_Warehousing_Contact set Mobile_No='" + txtDFNo.Text + "'  where Branch_Id='" + gvr.Cells[0].Text + "' ";
                qryDel = "insert into tbl_MetaData_GODOWN_2018_log select * from tbl_metadata_godown_2018 where Godown_ID='" + ddlWHMSGdwnID.SelectedValue.ToString() + "' and BranchID='" + ddlBranch.SelectedValue.ToString() + "'";
                SqlCommand cmdDel = new SqlCommand(qryDel, con, sqltrans);
                int b = cmdDel.ExecuteNonQuery();
                if (b == 1)
                {
                    string qryupdate = "update tbl_metadata_godown_2018 set LicNum='" + txtlicno.Text.Trim() + "',LicDate='" + getDate_MDY(txtlicexpdate.Text.Trim()) + "',UpdatedDate=GETDATE(),UpdatedBy='" + Client_IP + "' where Godown_ID='" + ddlWHMSGdwnID.SelectedValue.ToString() + "' and BranchID='" + ddlBranch.SelectedValue.ToString() + "'";
                    SqlCommand cmdupdate = new SqlCommand(qryupdate, con, sqltrans);
                    int c = cmdupdate.ExecuteNonQuery();
                    if (c == 1)
                    {
                        qryDel = "insert into tbl_MetaData_GODOWN_log select * from tbl_metadata_godown where Godown_ID='" + ddlWHMSGdwnID.SelectedValue.ToString() + "' and BranchID='" + ddlBranch.SelectedValue.ToString() + "'";
                        SqlCommand cmd1 = new SqlCommand(qryDel, con, sqltrans);
                        int b1 = cmd1.ExecuteNonQuery();
                        if (b1 == 1)
                        {
                            string qryupdate1 = "update tbl_metadata_godown set LicNum='" + txtlicno.Text.Trim() + "',LicDate='" + getDate_MDY(txtlicexpdate.Text.Trim()) + "',UpdatedDate=GETDATE(),UpdatedBy='" + Client_IP + "' where Godown_ID='" + ddlWHMSGdwnID.SelectedValue.ToString() + "' and BranchID='" + ddlBranch.SelectedValue.ToString() + "'";
                            SqlCommand cmdupdate1 = new SqlCommand(qryupdate1, con, sqltrans);
                            int c1 = cmdupdate1.ExecuteNonQuery();
                            if (c1 == 1)
                            {
                                sqltrans.Commit();
                                lblgdid.Text = "";
                                txtlicno.Text = "";
                                txtlicexpdate.Text = "";
                                GetGodown();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Update')", true);
                            }
                        }
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
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    protected void Depositor_Gridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        GetGodown();
        if (gvr.Cells[2].Text == "Yes" && gvr.Cells[5].Text=="No")
        {
            txtlicno.Text=gvr.Cells[3].Text;
            txtlicexpdate.Text=gvr.Cells[4].Text;
        }
        else if (gvr.Cells[2].Text == "No" && gvr.Cells[5].Text == "Yes")
        {
            txtlicno.Text = gvr.Cells[6].Text;
            txtlicexpdate.Text = gvr.Cells[7].Text;
        }

        tr1.Visible = true;
        trbtnhide.Visible = true;
        trmobtxt.Visible = true;

    }
    protected void ddlGdwnExixtance_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchData();
    }
    protected void btnGenerateBill_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Welcome.aspx");
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRegID();
    }

    public void GetRegID()
    {
        if (ddlgdwntype.SelectedValue == "JVS")
        {
            string qry = "";
            qry = "select Distinct INSP.Registration_ID,INSP.Registration_ID +' ('+ UPPER((select Warehouse_name from tbl_warehouseRegistration as WREG where WREG.Registration_id=INSP.Registration_id)) +')' as Warehouse_name  from tbl_godown_inspection as INSP where INSP.BranchId='" + ddlBranch.SelectedValue.ToString() + "' and createdDate>'02/18/2019' order by Warehouse_name";
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
        }
    }
    private void GetDist()
    {
        string strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT as MD Where MD.Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DDLDistrict.DataSource = ds.Tables[0];
            DDLDistrict.DataTextField = "District_Name";
            DDLDistrict.DataValueField = "District_Id";
            DDLDistrict.DataBind();
            DDLDistrict.Items.Insert(0, "---Select---");
        }
        else
        {
            DDLDistrict.Items.Insert(0, "---Select---");
        }
    }

    private void GetGodown()
    {

        if (ddlgdwntype.SelectedValue == "JVS")
        {
            string strDist = "select Godown_ID +' ( '+ Godown_Name +' )' as Godown_Name ,Godown_ID from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Hired_Type in ('WDRA','Joint Venture(JV)') order by Godown_Name";
            SqlDataAdapter da = new SqlDataAdapter(strDist, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlWHMSGdwnID.DataSource = ds.Tables[0];
                ddlWHMSGdwnID.DataTextField = "Godown_Name";
                ddlWHMSGdwnID.DataValueField = "Godown_ID";
                ddlWHMSGdwnID.DataBind();
                ddlWHMSGdwnID.Items.Insert(0, "---Select---");
            }
            else
            {
                ddlWHMSGdwnID.Items.Insert(0, "---Select---");
            }
        }
        else if (ddlgdwntype.SelectedValue == "OWN")
        {
            string strDist = "select Godown_ID +' ( '+ Godown_Name +' )' as Godown_Name ,Godown_ID from tbl_metadata_godown_2018 where DistrictID='" + DDLDistrict.SelectedValue.ToString() + "' and Hired_Type not in ('WDRA','Joint Venture(JV)') and BranchID='"+ ddlbranchOther.SelectedValue.ToString() +"' order by Godown_Name";
            SqlDataAdapter da = new SqlDataAdapter(strDist, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlwhmsOthergdwn.DataSource = ds.Tables[0];
                ddlwhmsOthergdwn.DataTextField = "Godown_Name";
                ddlwhmsOthergdwn.DataValueField = "Godown_ID";
                ddlwhmsOthergdwn.DataBind();
                ddlwhmsOthergdwn.Items.Insert(0, "---Select---");
            }
            else
            {
                ddlwhmsOthergdwn.Items.Insert(0, "---Select---");
            }
        }


    }
    private void GetLicNoDateOther()
    {
        if (ddlgdwntype.SelectedValue == "OWN")
        {
            string qry = "";
            qry = "select WHR_ID +' ('+ UPPER(Whr_Name) +')' as 'Whr_Name',WHR_ID as 'LicNo' from tbl_LiencesRegistrationOld where District='" + DDLDistrict.SelectedItem.Text.Trim() + "' union select WHR_ID +' ('+ UPPER(Whr_Name) +')' as 'Whr_Name',WHR_ID as 'LicNo' from tbl_LiencesRegistrationNew where IssuedAnugytikramank is null and District_ID='" + DDLDistrict.SelectedValue.ToString() + "' order by LicNo";
            SqlCommand cmd = new SqlCommand(qry, JVScon);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlOtherJvs.DataSource = ds.Tables[0];
                ddlOtherJvs.DataTextField = "Whr_Name";
                ddlOtherJvs.DataValueField = "LicNo";
                ddlOtherJvs.DataBind();
                ddlOtherJvs.Items.Insert(0, "--Select--");
            }
        }
    }
    protected void ddlgdwntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgdwntype.SelectedValue == "JVS")
        {
            trjvslic.Visible = true;
            trownlic.Visible = false;
            GetBranch();
        }
        else if (ddlgdwntype.SelectedValue == "OWN")
        {
            trjvslic.Visible = false;
            trownlic.Visible = true;
            GetDist();
        }
    }
    protected void ddlRegID_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "select (select Warehouse_name from tbl_warehouseRegistration as WREG where WREG.Registration_id=INSP.Registration_id) as Warehouse_name, Godown_No,Case when Present_Validity_WDRA='1' then 'Yes' Else 'No' END Present_Validity_WDRA,WDRA_LicenseNo,convert(varchar(10),WDRA_LicenseDate,103) as WDRA_LicenseDate,Case when WDRAL_Present_Validity='1' then 'Yes' Else 'No' END WDRAL_Present_Validity,Warehouse_LicenseNo,convert(varchar(10),Warehouse_licenseDate,103) as Warehouse_licenseDate from tbl_godown_inspection as INSP where INSP.Registration_id='" + ddlRegID.SelectedValue.ToString() + "' and createdDate>'02/18/2019' order by Godown_No ";
        SqlCommand cmd = new SqlCommand(qry, JVScon);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Depositor_Gridview.DataSource = ds;
            Depositor_Gridview.DataBind();
        }
    }
    protected void ddlWHMSGdwnID_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblgdid.Text = ddlWHMSGdwnID.SelectedValue.ToString();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateJVSGodownLicNoExpDate.aspx");
    }
    protected void ddlbranchOther_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodown();
    }
    protected void DDLDistrict_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GetBranch();
        GetLicNoDateOther();
    }
    protected void ddlOtherJvs_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "select  UPPER(Whr_Name) as 'Whr_Name',UPPER(Name_of_Owner) as 'Name_of_Owner',Whr_ID,convert(varchar(10),IssuedAnugyptivalidityDate,103) as 'ExpDate',Whr_Address,Total_capicity from tbl_LiencesRegistrationOld where Whr_ID='" + ddlOtherJvs.SelectedValue.ToString() + "'  union select  UPPER(Whr_Name) as 'Whr_Name',UPPER(Name_of_Owner) as 'Name_of_Owner',Whr_ID,convert(varchar(10),IssuedAnugyptivalidityDate,103) as 'ExpDate',Whr_Address ,Total_capicity from tbl_LiencesRegistrationNew where IssuedAnugytikramank is null and  Whr_ID='" + ddlOtherJvs.SelectedValue.ToString() + "'  ";
        SqlCommand cmd = new SqlCommand(qry, JVScon);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        GridViewRow gvr = GridView1.SelectedRow;
        txtlicnoother.Text = gvr.Cells[2].Text;
        txtlicexpother.Text = gvr.Cells[3].Text;
        tr3.Visible = true;
        tr4.Visible = true;
        tr5.Visible = true;
    }
    protected void ddlwhmsOthergdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblothergdwnid.Text = ddlwhmsOthergdwn.SelectedValue.ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (ddlgdwntype.SelectedValue.ToString()=="OWN")
        {
            GridViewRow gvr = GridView1.SelectedRow;
            string Client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                if (txtlicnoother.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Licence No')", true);
                }
                else if (txtlicexpother.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Licence Exp Date')", true);
                }
                else if (lblothergdwnid.Text == "Select")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select WHMS Godown_id')", true);
                }
                else if (lblothergdwnid.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select WHMS Godown_id')", true);
                }
                sqltrans = con.BeginTransaction();
                string qryDel = "";
                // qryDel = "update tbl_Warehousing_Contact set Mobile_No='" + txtDFNo.Text + "'  where Branch_Id='" + gvr.Cells[0].Text + "' ";
                qryDel = "insert into tbl_MetaData_GODOWN_2018_log select * from tbl_metadata_godown_2018 where Godown_ID='" + ddlwhmsOthergdwn.SelectedValue.ToString() + "' and BranchID='" + ddlbranchOther.SelectedValue.ToString() + "'";
                SqlCommand cmdDel = new SqlCommand(qryDel, con, sqltrans);
                int b = cmdDel.ExecuteNonQuery();
                if (b == 1)
                {
                    string qryupdate = "update tbl_metadata_godown_2018 set LicNum='" + txtlicnoother.Text.Trim() + "',LicDate='" + getDate_MDY(txtlicexpother.Text.Trim()) + "',UpdatedDate=GETDATE(),UpdatedBy='" + Client_IP + "' where Godown_ID='" + ddlwhmsOthergdwn.SelectedValue.ToString() + "' and BranchID='" + ddlbranchOther.SelectedValue.ToString() + "'";
                    SqlCommand cmdupdate = new SqlCommand(qryupdate, con, sqltrans);
                    int c = cmdupdate.ExecuteNonQuery();
                    if (c == 1)
                    {
                        qryDel = "insert into tbl_MetaData_GODOWN_log select * from tbl_metadata_godown where Godown_ID='" + ddlwhmsOthergdwn.SelectedValue.ToString() + "' and BranchID='" + ddlbranchOther.SelectedValue.ToString() + "'";
                        SqlCommand cmd1 = new SqlCommand(qryDel, con, sqltrans);
                        int b1 = cmd1.ExecuteNonQuery();
                        if (b1 == 1)
                        {
                            string qryupdate1 = "update tbl_metadata_godown set LicNum='" + txtlicnoother.Text.Trim() + "',LicDate='" + getDate_MDY(txtlicexpother.Text.Trim()) + "',UpdatedDate=GETDATE(),UpdatedBy='" + Client_IP + "' where Godown_ID='" + ddlwhmsOthergdwn.SelectedValue.ToString() + "' and BranchID='" + ddlbranchOther.SelectedValue.ToString() + "'";
                            SqlCommand cmdupdate1 = new SqlCommand(qryupdate1, con, sqltrans);
                            int c1 = cmdupdate1.ExecuteNonQuery();
                            if (c1 == 1)
                            {
                                sqltrans.Commit();
                                txtlicnoother.Text = "";
                                txtlicexpother.Text = "";
                                lblothergdwnid.Text = "";
                              //  ddlwhmsOthergdwn.SelectedIndex = -1;
                                GetGodown();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Update')", true);
                            }
                        }
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
    protected void btnGenerateBill_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/Welcome.aspx");
    }
}

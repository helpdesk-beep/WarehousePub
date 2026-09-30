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

public partial class BranchPages_DeleteInActiveGodown : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                //GetBranch();
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
            //if (ddlGdwnExixtance.Text == "Y")
            //{
            //    qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Scientific_Capacity,Premise_capacity,Closing_Balance from tbl_metadata_godown_2018 where BranchID='" + ddlBranch.SelectedValue.ToString() + "' and IsActive='Y'";
            //}
            //else if (ddlGdwnExixtance.Text == "N")
            //{
            qry = "select Godown_ID,Godown_Name,Hired_Type,Storage_Type,Godown_Scientific_Capacity,Premise_capacity,Closing_Balance from tbl_MetaData_GODOWN_2018_NonExist where BranchID='" + Session["Depot_DepotID"].ToString() + "' and IsActive='N'";
            //}
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
                txtGdwnID.Text = "";
                txtGdwnName.Text = "";
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Record Found')", true);
                trbtnhide.Visible = false;
                trmobtxt.Visible = false;
                trbtnhide.Visible = false;
                Depositor_Gridview.DataSource = null;
                Depositor_Gridview.DataBind();
                txtGdwnID.Text = "";
                txtGdwnName.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }
    //public void GetBranch()
    //{
    //    string qry = "";
    //    qry = "select DepotName,BranchId  from tbl_MetaData_DEPOT where DistrictId in (select MD.District_Id from tbl_MetaData_DISTRICT as MD where MD.Region_ID='" + Session["Region_ID"].ToString() + "') order by DepotName";
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
        string Client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            if (txtGdwnID.Text != "" && gvr.Cells[0].Text == txtGdwnID.Text)
            {
                //if (ddlGdwnExixtance.SelectedValue.ToString() == "Y")
                //{
                //    string qryDel = "";
                //    sqltrans = con.BeginTransaction();
                //    ///   qryDel = "insert into tbl_MetaData_GODOWN_2018_log SELECT [Godown_ID],[StateId],[DistrictId],[DepotId],[Godown_Name],[Godown_Formation_Date],[Godown_Updation_Date],[Godown_Capacity],[Remarks],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + Client_IP + "',GETDATE(),[Hired_Type],[Storage_Type],[Godown_Scientific_Capacity],[Godown_APN],[Godown_Email],[Godown_Mobile],[Godown_Address],[BranchID],[LicNum],[LicDate],[PAN],[Bank_ID],[AccNo],[IFSC_Code],[Bank_Add],[Latitude],[Longitude],[GodownNum],[Khasranum],[Rakwanum],[TehshilID],[VillageName],[Org_Name],[GInchargeName],[GInchargeAddress],[GInchargeMobile],[GInchargeEmail],[WeightmentType],[LicIssueDate],[Godown_Reg_No],[IsActive],[LR_TehsilCode],[LR_VillageCode],[Lenght],[Width],[Height],[Premise_capacity],[Closing_Balance],[LicOwnerNM],[LicCapacity],[JVS_RegNo],[WH_Name],[WH_Type]  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN_2018]  where Godown_ID='" + txtGdwnID.Text + "' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID='" + gvr.Cells[0].Text + "'";
                //    qryDel = "insert into tbl_MetaData_GODOWN_2018_log SELECT * FROM tbl_MetaData_GODOWN_2018 where Godown_ID='" + txtGdwnID.Text + "' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID='" + gvr.Cells[0].Text + "'";
                //    SqlCommand cmdDel = new SqlCommand(qryDel, con, sqltrans);
                //    int b = cmdDel.ExecuteNonQuery();
                //    if (b == 1)
                //    {
                //        string qryDel2 = "";
                //        qryDel2 = "DELETE FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN_2018]  where Godown_ID='" + txtGdwnID.Text + "' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID='" + gvr.Cells[0].Text + "'";
                //        SqlCommand cmdDel2 = new SqlCommand(qryDel2, con, sqltrans);
                //        int b2 = cmdDel2.ExecuteNonQuery();
                //        if (b2 == 1)
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Delete')", true);
                //            trmobtxt.Visible = false;
                //            trbtnhide.Visible = false;
                //            txtGdwnID.Text = "";
                //            txtGdwnName.Text = "";
                //            Depositor_Gridview.DataSource = "";
                //            Depositor_Gridview.DataBind();
                //            sqltrans.Commit();
                //            ddlGdwnExixtance_SelectedIndexChanged(sender, e);
                //            // sqltrans.Commit();
                //        }
                //    }
                //}
                //else if (ddlGdwnExixtance.SelectedValue.ToString() == "N")
                //{
                    string qryDel = "";
                    string BranchID = Session["Depot_DepotID"].ToString();
                    sqltrans = con.BeginTransaction();
                    //  qryDel = "insert into tbl_MetaData_GODOWN_2018_NonExist_log SELECT [Godown_ID],[StateId],[DistrictId],[DepotId],[Godown_Name],[Godown_Formation_Date],[Godown_Updation_Date],[Godown_Capacity],[Remarks],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + Client_IP + "',GETDATE(),[Hired_Type],[Storage_Type],[Godown_Scientific_Capacity],[Godown_APN],[Godown_Email],[Godown_Mobile],[Godown_Address],[BranchID],[LicNum],[LicDate],[PAN],[Bank_ID],[AccNo],[IFSC_Code],[Bank_Add],[Latitude],[Longitude],[GodownNum],[Khasranum],[Rakwanum],[TehshilID],[VillageName],[Org_Name],[GInchargeName],[GInchargeAddress],[GInchargeMobile],[GInchargeEmail],[WeightmentType],[LicIssueDate],[Godown_Reg_No],[IsActive],[LR_TehsilCode],[LR_VillageCode],[Lenght],[Width],[Height],[Premise_capacity],[Closing_Balance]  FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN_2018_NonExist]  where Godown_ID='" + txtGdwnID.Text + "' and BranchID='" + ddlBranch.SelectedValue.ToString() + "' and Godown_ID='" + gvr.Cells[0].Text + "'";
                    //qryDel = "insert into tbl_MetaData_GODOWN_2018_NonExist_log SELECT * FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN_2018_NonExist]  where Godown_ID='" + txtGdwnID.Text + "' and BranchID='" + BranchID + "' and Godown_ID='" + gvr.Cells[0].Text + "'";
                    qryDel = "insert into tbl_MetaData_GODOWN_2018_NonExist_log SELECT [Godown_ID],[StateId],[DistrictId],[DepotId],[Godown_Name],[Godown_Formation_Date],[Godown_Updation_Date],[Godown_Capacity],[Remarks],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate],'" + Client_IP + "',GETDATE(),[Hired_Type],[Storage_Type],[Godown_Scientific_Capacity],[Godown_APN],[Godown_Email],[Godown_Mobile],[Godown_Address],[BranchID],[LicNum],[LicDate],[PAN],[Bank_ID],[AccNo],[IFSC_Code],[Bank_Add],[Latitude],[Longitude],[GodownNum],[Khasranum],[Rakwanum],[TehshilID],[VillageName],[Org_Name],[GInchargeName],[GInchargeAddress],[GInchargeMobile],[GInchargeEmail],[WeightmentType],[LicIssueDate],[Godown_Reg_No],[IsActive],[LR_TehsilCode],[LR_VillageCode],[Lenght] ,[Width],[Height],[Premise_capacity],[Closing_Balance] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN_2018_NonExist]  where Godown_ID='" + txtGdwnID.Text + "' and BranchID='" + BranchID + "' and Godown_ID='" + gvr.Cells[0].Text + "'";
                    SqlCommand cmdDel = new SqlCommand(qryDel, con, sqltrans);
                    int b = cmdDel.ExecuteNonQuery();
                    if (b == 1)
                    {
                        string qryDel2 = "";
                        //qryDel2 = "DELETE FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN_2018_NonExist]  where Godown_ID='" + txtGdwnID.Text + "' and BranchID='" + BranchID + "' and Godown_ID='" + gvr.Cells[0].Text + "'";
                        qryDel2 = "DELETE FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN_2018_NonExist]  where Godown_ID='" + txtGdwnID.Text + "' and BranchID='" + BranchID + "' and Godown_ID='" + gvr.Cells[0].Text + "'";

                        SqlCommand cmdDel2 = new SqlCommand(qryDel2, con, sqltrans);
                        int b2 = cmdDel2.ExecuteNonQuery();
                        if (b2 == 1)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Delete')", true);
                            trmobtxt.Visible = false;
                            trbtnhide.Visible = false;
                            txtGdwnID.Text = "";
                            txtGdwnName.Text = "";
                            Depositor_Gridview.DataSource = "";
                            Depositor_Gridview.DataBind();
                            sqltrans.Commit();
                            ddlGdwnExixtance_SelectedIndexChanged(sender, e);
                            //sqltrans.Commit();
                        }
                    }
                //}
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Enter Mobile No.')", true);
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
    protected void Depositor_Gridview_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        txtGdwnID.Text = gvr.Cells[0].Text;
        txtGdwnName.Text = gvr.Cells[1].Text;

    }
    protected void ddlGdwnExixtance_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBranchData();
    }
    protected void btnGenerateBill_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Branch_Welcome.aspx");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class Reports_States_Rpt_Delete_Uploaded_DSC : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC")
        {
            try
            {
                if (!IsPostBack)
                {
                    fillDistrict();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void fillDistrict()
    {
        try
        {
            string query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDSCDetail();
    }
    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
        }
        else if (ddlDepotList.SelectedIndex != 0)
        {
            //Getgodowns();
            GetDSCDetail();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch First..')", true);
        }
    }
  
    private void GetDSCDetail()
    {
        try
        {
            string BranchId = ddlDepotList.SelectedValue.ToString();
            string DistrictId = ddlDistrict.SelectedValue.ToString();
            //string qry = "select PW.Login_Id,PW.Godown_Name,PW.Godown_Id,PW.Access_Restrict,SA.Storage_Agency,MD.DepotName as IssueCenter,MD.BranchId as IssueCenterId,PW.Is_W19_RegID as W_Reg_No,PW.Is_W19_Agreement as Is_Agreement from Pvt_Warehouse_Login as PW inner join Storage_Agency_type as SA on SA.Storage_Agency_ID=PW.GodownTypeId inner join tbl_MetaData_DEPOT as MD on MD.BranchId=PW.BranchID where PW.BranchID='" + BranchId + "' and PW.DistrictId='" + DistrictId + "'";
           // string qry = "select Aid, (select Depotname from tbl_metadata_depot as MD Where MD.BranchID=DSC.Branch_ID) as Branch,BG_Name as Branch_Godown_Name,SUBSTRING(SUBSTRING(SubjectName,0,CHARINDEX(',',SubjectName)), 4, Len(SubjectName)) as DSC_HOLDER_NAME,SerialNumber,SUBSTRING(SUBSTRING(IssuerName,0,CHARINDEX(',',IssuerName)), 4, Len (IssuerName)) as DSC_IssuerName,convert(varchar(10),NotAfter,103) as ValidUpto,convert(varchar(10),Created_Date,103) as DSC_UploadDate,case when User_Type='G' then 'Godown'  when User_Type='B' then 'Branch'  end User_Type,Verification_Status from tbl_DSC_User_Upload_Detail as DSC where DSC.District_Id='" + DistrictId + "' Order by Branch,BG_Name";
            SqlCommand cmd = new SqlCommand("Get_Delete_DSC_Reports", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District_ID", ddlDistrict.SelectedValue);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //lblRowCount.Text = "Total records are : " + ds.Tables[0].Rows.Count.ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                //godown_GridView.DataSource = ds.Tables[0];
                //godown_GridView.DataBind();
                Session["dsGodown"] = ds;
                fillGrid(ds);

            }
            else
            {
                //Session["dsGodown"] = null;
                //fillGrid(ds);
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillGrid(DataSet ds)
    {
        godown_GridView.DataSource = ds.Tables[0];
        godown_GridView.DataBind();
        // lblRowCount.Text = "Total records are : " + godown_GridView.Rows.Count.ToString();
    }
    
}

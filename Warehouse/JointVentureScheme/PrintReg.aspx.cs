using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class JointVentureScheme_PrintReg : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    public string App_Id = "";
    public string ImgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string sess = Session["Reg_No"].ToString();
            if (sess != "")
            {
                App_Id = sess;
                if (!IsPostBack)
                {
                    GetRegisterationData();
                    //get_Region_Detail();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "PrintDiv()", true);
                }
            }
            else
            {
                Response.Redirect("UserReg.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("UserReg.aspx");
        }
    }
    public void GetRegisterationData()
    {
        qry = "SELECT  WP.[Tid],[Auth_Person],[MobileNo],WP.[EmailID],CONVERT(varchar(10),DOB,103) as DOB,[Password],AT.Applicant_Type,dt.District_Name,WR.Warehouse_Name,WR.Warehouse_Address,WR.EmailID,WR.Mobile_No,WR.Registration_Id,WR.RegAmt,WR.RegCapacity,WP.PAN_No,WP.Aadhar_No,MDD.DepotName,TH.Tehsil_Name,WR.DistFNBranch,CONVERT(varchar(10),WR.Registration_Date,103) as Registration_Date,(select distinct Block_Name from tbl_Branch_Block_Mapping as BLO where BLO.Block_ID=WR.W_Block) as Block_Name  FROM [tbl_Warehouse_PreReg] as WP inner join tbl_Metadata_ApplicantType as AT on AT.Applicant_TypeId=WP.ApplicantType inner join tbl_MetaData_DISTRICT as dt on dt.District_Id=WP.DistrictID inner join tbl_WarehouseRegistration as WR on WR.Registration_Id=WP.Reg_No inner join tbl_MetaData_DEPOT as MDD on WR.BranchId=MDD.BranchId inner join Tehsils as TH on WR.TehsilID=TH.TehsilCode where WR.Registration_Id='" + Session["Reg_No"].ToString() + "'";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            App_Id = dt.Rows[0]["Registration_Id"].ToString();
            lblRegId.Text = App_Id;
            lblRegEmail.Text = dt.Rows[0]["EmailID"].ToString();
            //lblDOB.Text = dt.Rows[0]["DOB"].ToString();
            lblRegMobile.Text = dt.Rows[0]["MobileNo"].ToString();
            lblAppType.Text = dt.Rows[0]["Applicant_Type"].ToString();
            lblRegFee.Text = dt.Rows[0]["RegAmt"].ToString();

            lblWName.Text = dt.Rows[0]["Warehouse_Name"].ToString();
            lblWAddres.Text = dt.Rows[0]["Warehouse_Address"].ToString();
            //lblWEmail.Text = dt.Rows[0]["EmailID"].ToString();
            lblWtehsil.Text = dt.Rows[0]["Tehsil_Name"].ToString();
            lblWMobile.Text = dt.Rows[0]["Mobile_No"].ToString();
            lblWDist.Text = dt.Rows[0]["District_Name"].ToString();
            lblWBranch.Text = dt.Rows[0]["DepotName"].ToString();
            lblWBranchDist.Text = dt.Rows[0]["DistFNBranch"].ToString();
            lblPanNo.Text = dt.Rows[0]["PAN_No"].ToString();
            lblAadharNo.Text = dt.Rows[0]["Aadhar_No"].ToString();
            lblRegCpt.Text = dt.Rows[0]["RegCapacity"].ToString();
            lblRegDate.Text = dt.Rows[0]["Registration_Date"].ToString();
            lblAppDate.Text = DateTime.Now.ToString();
            lblblocknew.Text = dt.Rows[0]["Block_Name"].ToString();
            //lblDistanceQty.Text = dt.Rows[0]["DistFromTO"].ToString() + '/' + dt.Rows[0]["QuantityOfForm"].ToString();

            //Fech_Image();
        }
    }
    //protected void lnkLogout_Click(object sender, EventArgs e)
    //{
    //    Session.Abandon();
    //    Response.Redirect("UserReg.aspx");
    //}
}

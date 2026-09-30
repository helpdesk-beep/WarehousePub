using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class JointVentureScheme_PrintOffer : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    public string App_Id = "";
    public string ImgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string sess = Session["Reg_no"].ToString();
            if (sess != "")
            {
                if (!IsPostBack)
                {
                    GetRegisterationData();
                    GetOfferGdwn();
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
        //JVS2021  ,(select distinct Block_Name from tbl_Branch_Block_Mapping as BLO where BLO.Block_ID=WR.W_Block) as Block_Name
        //qry = "SELECT  WP.[Auth_Person],[MobileNo],WP.[EmailID],AT.Applicant_Type,dt.District_Name,WR.Warehouse_Name,WR.Warehouse_Address,WR.Mobile_No,WR.Registration_Id,MDD.DepotName,TH.Tehsil_Name,WR.DistFNBranch,WOF.OfferAmt,CONVERT(decimal(18,2),WOF.Offer_Capacity) as Offer_Capacity,convert(varchar(10),WOF.CreatedDate,103) as CreatedDate,(select distinct Block_Name from tbl_Branch_Block_Mapping as BLO where BLO.Block_ID=WR.W_Block) as Block_Name  FROM [tbl_Warehouse_PreReg] as WP inner join tbl_Metadata_ApplicantType as AT on AT.Applicant_TypeId=WP.ApplicantType inner join tbl_MetaData_DISTRICT as dt on dt.District_Id=WP.DistrictID inner join tbl_WarehouseRegistration as WR on WR.Registration_Id=WP.Reg_No inner join tbl_MetaData_DEPOT as MDD on WR.BranchId=MDD.BranchId inner join Tehsils as TH on WR.TehsilID=TH.TehsilCode inner join tbl_Warehouse_Capacity_Offer_2020 as WOF on WR.Registration_Id=WOF.Registration_Id where WR.Registration_Id='" + Session["Reg_no"].ToString() + "'";
        qry = "SELECT  WP.[Auth_Person],[MobileNo],WP.[EmailID],AT.Applicant_Type,dt.District_Name,WR.Warehouse_Name,WR.Warehouse_Address,WR.Mobile_No,WR.Registration_Id,MDD.DepotName,TH.Tehsil_Name,WR.DistFNBranch,WOF.OfferAmt,CONVERT(decimal(18,2),WOF.Offer_Capacity) as Offer_Capacity,convert(varchar(10),WOF.CreatedDate,103) as CreatedDate  FROM [tbl_Warehouse_PreReg] as WP inner join tbl_Metadata_ApplicantType as AT on AT.Applicant_TypeId=WP.ApplicantType inner join tbl_MetaData_DISTRICT as dt on dt.District_Id=WP.DistrictID inner join tbl_WarehouseRegistration as WR on WR.Registration_Id=WP.Reg_No inner join tbl_MetaData_DEPOT as MDD on WR.BranchId=MDD.BranchId inner join Tehsils as TH on WR.TehsilID=TH.TehsilCode inner join tbl_Warehouse_Capacity_Offer_2022 as WOF on WR.Registration_Id=WOF.Registration_Id where WR.Registration_Id='" + Session["Reg_no"].ToString() + "'";


        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            lblRegId.Text = dt.Rows[0]["Registration_Id"].ToString();
            lblRegEmail.Text = dt.Rows[0]["EmailID"].ToString();
          //  lblDOB.Text = dt.Rows[0]["DOB"].ToString();
            lblRegMobile.Text = dt.Rows[0]["MobileNo"].ToString();
            lblAppType.Text = dt.Rows[0]["Applicant_Type"].ToString();
           // lblRegFee.Text = dt.Rows[0]["RegAmt"].ToString();
            lblWName.Text = dt.Rows[0]["Warehouse_Name"].ToString();
            lblWAddres.Text = dt.Rows[0]["Warehouse_Address"].ToString();
            lblWOwn.Text = dt.Rows[0]["Auth_Person"].ToString();
            lblWtehsil.Text = dt.Rows[0]["Tehsil_Name"].ToString();
            lblWMobile.Text = dt.Rows[0]["Mobile_No"].ToString();
            lblWDist.Text = dt.Rows[0]["District_Name"].ToString();
            lblWBranch.Text = dt.Rows[0]["DepotName"].ToString();
            lblWBranchDist.Text = dt.Rows[0]["DistFNBranch"].ToString();
            lblOfrCpt.Text = dt.Rows[0]["Offer_Capacity"].ToString();
            lbltotalamt.Text = dt.Rows[0]["OfferAmt"].ToString();
            lblofrdate.Text = dt.Rows[0]["CreatedDate"].ToString();
            lblAppDate.Text = DateTime.Now.ToString();
           // lblblocknew.Text = dt.Rows[0]["Block_Name"].ToString();
        }
    }
    public void GetOfferGdwn()
    {
        //qry = "select Godown_No,CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,Offer_Category,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type from tbl_Warehouse_Godown_Offer_2020 where Registration_Id='" + Session["Reg_no"].ToString() + "' order by Godown_No ";
        qry = "select Godown_No,CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,case when Offer_Category='AB' then N'A-ब'  when Offer_Category='BB' then N'B-ब' when Offer_Category='A' then 'A' when Offer_Category='B' then 'B' end as Offer_Category,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type from tbl_Warehouse_Godown_Offer_2022 where Registration_Id='" + Session["Reg_no"].ToString() + "' order by Godown_No ";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvGodown.DataSource = ds;
            gvGodown.DataBind();
        }
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class JointVentureScheme_PrintOfferJVS : System.Web.UI.Page
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
           // if (sess != "")
           // {
                if (!IsPostBack)
                {
                    GetRegisterationData();
                    GetRegGodownParticialOfr();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "PrintDiv()", true);
                }
          //  }
           // else
           // {
              //  Response.Redirect("UserReg.aspx");
          //  }
        }
        catch (Exception ex)
        {
            GetRegisterationData();
            GetRegGodownParticialOfr();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "PrintDiv()", true);
          //  Response.Redirect("UserReg.aspx");
        }
    }
    public void GetRegisterationData()
    {
        //JVS2021
        //qry = "SELECT  WP.[Auth_Person],[MobileNo],WP.[EmailID],AT.Applicant_Type,dt.District_Name,WR.Warehouse_Name,WR.Warehouse_Address,WR.Mobile_No,WR.Registration_Id,MDD.DepotName,TH.Tehsil_Name,WR.DistFNBranch,WOF.OfferAmt,CONVERT(decimal(18,2),WOF.Offer_Capacity) as Offer_Capacity,convert(varchar(10),WOF.CreatedDate,103) as CreatedDate,(select distinct Block_Name from tbl_Branch_Block_Mapping as BLO where BLO.Block_ID=WR.W_Block) as Block_Name  FROM [tbl_Warehouse_PreReg] as WP inner join tbl_Metadata_ApplicantType as AT on AT.Applicant_TypeId=WP.ApplicantType inner join tbl_MetaData_DISTRICT as dt on dt.District_Id=WP.DistrictID inner join tbl_WarehouseRegistration as WR on WR.Registration_Id=WP.Reg_No inner join tbl_MetaData_DEPOT as MDD on WR.BranchId=MDD.BranchId inner join Tehsils as TH on WR.TehsilID=TH.TehsilCode inner join tbl_Warehouse_Capacity_Offer_2020 as WOF on WR.Registration_Id=WOF.Registration_Id where WR.Registration_Id='" + Session["Reg_no"].ToString() + "'";
        qry = "SELECT  WP.[Auth_Person],[MobileNo],WP.[EmailID],AT.Applicant_Type,dt.District_Name,WR.Warehouse_Name,WR.Warehouse_Address,WR.Mobile_No,WR.Registration_Id,MDD.DepotName,TH.Tehsil_Name,WR.DistFNBranch,WOF.OfferAmt,CONVERT(decimal(18,2),WOF.Offer_Capacity) as Offer_Capacity,convert(varchar(10),WOF.CreatedDate,103) as CreatedDate,(select distinct Block_Name from tbl_Branch_Block_Mapping as BLO where BLO.Block_ID=WR.W_Block) as Block_Name  FROM [tbl_Warehouse_PreReg] as WP inner join tbl_Metadata_ApplicantType as AT on AT.Applicant_TypeId=WP.ApplicantType inner join tbl_MetaData_DISTRICT as dt on dt.District_Id=WP.DistrictID inner join tbl_WarehouseRegistration as WR on WR.Registration_Id=WP.Reg_No inner join tbl_MetaData_DEPOT as MDD on WR.BranchId=MDD.BranchId inner join Tehsils as TH on WR.TehsilID=TH.TehsilCode inner join tbl_Warehouse_Capacity_Offer_2021 as WOF on WR.Registration_Id=WOF.Registration_Id where WR.Registration_Id='" + Session["Reg_no"].ToString() + "'";


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
            //lblOfrCpt.Text = dt.Rows[0]["Offer_Capacity"].ToString();
           // lbltotalamt.Text = dt.Rows[0]["OfferAmt"].ToString();
            lblofrdate.Text = dt.Rows[0]["CreatedDate"].ToString();
            lblAppDate.Text = DateTime.Now.ToString();
            lblblocknew.Text = dt.Rows[0]["Block_Name"].ToString();
        }
    }
    //public void GetOfferGdwn()
    //{
    //    //qry = "select Godown_No,CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,Offer_Category,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type from tbl_Warehouse_Godown_Offer_2020 where Registration_Id='" + Session["Reg_no"].ToString() + "' order by Godown_No ";
    //    qry = "select Godown_No,CONVERT(decimal(18,2),G_OfferCapacity) as G_OfferCapacity,Offer_Category,case when Capacity_Type = 'F' then 'Full Capacity' else 'Partial Capacity' end Capacity_Type from tbl_Warehouse_Godown_Offer_2021 where Registration_Id='" + Session["Reg_no"].ToString() + "' order by Godown_No ";

    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        gvGodown.DataSource = ds;
    //        gvGodown.DataBind();
    //    }
    //}


    private void GetRegGodownParticialOfr()
    {
        string regno = Session["Reg_No"].ToString();
        //  Update on 19/02/2021
        string qry1 = "select sum(G_ScientificCapacity) as TCap from tbl_WarehouseGodown_Reg where Registration_Id='" + regno + "'";
        SqlCommand cmd1 = new SqlCommand(qry1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        Totalcaphdn.Value = ds1.Tables[0].Rows[0]["TCap"].ToString();
        Label2.Text = ds1.Tables[0].Rows[0]["TCap"].ToString();



        string qry12 = "select Choice from Tbl_JVS_Choise_Filling where  Reg_ID='" + regno + "'";
        SqlCommand cmd12 = new SqlCommand(qry12, con);
        SqlDataAdapter da12 = new SqlDataAdapter(cmd12);
        DataSet ds12 = new DataSet();
        da12.Fill(ds12);
        string ch = ds12.Tables[0].Rows[0]["Choice"].ToString();
        if (ch == "A")
        {
        Label1.Text = "अ";
        }
        else
        {
       Label1.Text = "ब";
        }




        string qry = "SELECT GR.Godown_ID,GR.Godown_No,convert(Decimal(10,2), G_Length) as G_Length,convert(Decimal(10,2), G_Width) as G_Width,convert(Decimal(10,2),G_Height) as G_Height,convert(Decimal(10,4),G_ScientificCapacity) as G_ScientificCapacity, isnull(convert(decimal(18,4),G_ScientificCapacity),0) -isnull(convert(decimal(18,4),G_OfferCapacity),0) as AvlOfferedCpt FROM tbl_WarehouseGodown_Reg AS GR LEFT JOIN (select POFR.Godown_ID,isnull(OfrCpt,0)-isnull(IspeOfrCpt,0) G_OfferCapacity from (select OFR.Godown_ID,convert(decimal(18,4),isnull(SUM(OFR.G_OfferCapacity),0)) OfrCpt from tbl_Warehouse_Godown_Offer_2021 as OFR where OFR.Registration_Id='" + regno + "' group by OFR.Godown_ID) as POFR left join (select INSP.GodownId,isnull(sum(WGO.G_OfferCapacity),0) IspeOfrCpt from tbl_Godown_Inspection as INSP inner join tbl_Warehouse_Godown_Offer_2021 as WGO on WGO.Godown_Offer_Id=INSP.Godown_Offer_Id  where INSP.CreatedDate < CONVERT(varchar(10),'10/19/2018',101) and Fit_Unfit='UNFIT' and INSP.Registration_Id ='" + regno + "' group by INSP.GodownId) as INSPOFR on INSPOFR.GodownId=POFR.Godown_ID)  as GD on GR.Godown_ID=GD.Godown_ID where G_ScientificCapacity != 0 and GR.Registration_Id='" + regno + "' order by Godown_No asc";

        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GodId.Value = ds.Tables[0].Rows[0]["Godown_ID"].ToString();
        GodCAp.Value = ds.Tables[0].Rows[0]["G_ScientificCapacity"].ToString();

        if (ds.Tables[0].Rows.Count > 0)
        {
            GridViewParticialCpt.DataSource = ds;
            GridViewParticialCpt.DataBind();
            this.GridViewParticialCpt.Columns[0].Visible = false;
        }
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Godown Available for Offer')", true);
        //}
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
}

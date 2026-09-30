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

public partial class JointVentureScheme_Region_FumigationWorkReport : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranchID != "" && SessBranch != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessBranch;
            }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "AllOfferCapacity" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        RegGrid.Attributes["style"] = "border-collapse:separate";
        toexport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
    protected void ddlfumigation_SelectedIndexChanged(object sender, EventArgs e)
    {
        String qry = "";
        RegGrid.DataSource = "";
        if (ddlfumigation.SelectedValue.ToString() != "All")
        {
            //qry = "select Regionnm,District_Name,DepotName,REG.Registration_ID,Warehouse_Name,RegCapacity,sum(OfrCpt) as OfrCpt,case when FumigationWork='Y' then N'सहमत' else N'असहमत' end FumigationWork,sum(Agree_Capacity) as Agree_Capacity from (SELECT Registration_id,sum(Offer_Capacity) as OfrCpt,FumigationWork FROM tbl_Warehouse_Capacity_Offer_2020 group by Registration_id,FumigationWork ) as OFR inner join  (Select DistrictID,BranchID,Registration_id,Warehouse_Name,regCapacity from tbl_WarehouseRegistration ) as REG on REG.Registration_id=OFR.Registration_ID inner join tbl_metadata_district as MD on MD.District_id=REG.DistrictID inner join tbl_metadata_depot as MDD on MDD.branchID=REG.BranchID left join  (select registration_id,sum(Agree_Capacity) as Agree_Capacity from tbl_Godown_Agreement where CreatedDate>'03/18/2020' group by registration_id)as AGR on AGR.registration_id=REG.Registration_id where MD.Region_ID='" + Session["UserId"].ToString() + "' and FumigationWork='" + ddlfumigation.SelectedValue.ToString() + "' group by Regionnm,District_Name,DepotName,REG.Registration_ID,Warehouse_Name,regCapacity,FumigationWork ";
            qry = "select Regionnm,District_Name,DepotName,REG.Registration_ID,Warehouse_Name,RegCapacity,sum(OfrCpt) as OfrCpt,case when FumigationWork='Y' then N'सहमत' else N'असहमत' end FumigationWork,sum(Agree_Capacity) as Agree_Capacity from (SELECT Registration_id,sum(Offer_Capacity) as OfrCpt,FumigationWork FROM tbl_Warehouse_Capacity_Offer_Rabi_2026_27 group by Registration_id,FumigationWork ) as OFR inner join  (Select DistrictID,BranchID,Registration_id,Warehouse_Name,regCapacity from tbl_WarehouseRegistration ) as REG on REG.Registration_id=OFR.Registration_ID inner join tbl_metadata_district as MD on MD.District_id=REG.DistrictID inner join tbl_metadata_depot as MDD on MDD.branchID=REG.BranchID left join  (select registration_id,Crop_Year,sum(Agree_Capacity) as Agree_Capacity from tbl_Godown_Agreement where CreatedDate>'03/18/2020' group by registration_id,Crop_Year)as AGR on AGR.registration_id=REG.Registration_id where MD.Region_ID='" + Session["UserId"].ToString() + "' and FumigationWork='" + ddlfumigation.SelectedValue.ToString() + "' And AGR.Crop_Year='2026-27' group by Regionnm,District_Name,DepotName,REG.Registration_ID,Warehouse_Name,regCapacity,FumigationWork ";
        }
        else if (ddlfumigation.SelectedValue.ToString() == "All")
        {
            //qry = "select Regionnm,District_Name,DepotName,REG.Registration_ID,Warehouse_Name,RegCapacity,sum(OfrCpt) as OfrCpt,case when FumigationWork='Y' then N'सहमत' else N'असहमत' end FumigationWork,sum(Agree_Capacity) as Agree_Capacity from (SELECT Registration_id,sum(Offer_Capacity) as OfrCpt,FumigationWork FROM tbl_Warehouse_Capacity_Offer_2020 group by Registration_id,FumigationWork ) as OFR inner join  (Select DistrictID,BranchID,Registration_id,Warehouse_Name,regCapacity from tbl_WarehouseRegistration ) as REG on REG.Registration_id=OFR.Registration_ID inner join tbl_metadata_district as MD on MD.District_id=REG.DistrictID inner join tbl_metadata_depot as MDD on MDD.branchID=REG.BranchID left join  (select registration_id,sum(Agree_Capacity) as Agree_Capacity from tbl_Godown_Agreement where CreatedDate>'03/18/2020' group by registration_id)as AGR on AGR.registration_id=REG.Registration_id where MD.Region_ID='" + Session["UserId"].ToString() + "' group by Regionnm,District_Name,DepotName,REG.Registration_ID,Warehouse_Name,regCapacity,FumigationWork ";
            qry = "select Regionnm,District_Name,DepotName,REG.Registration_ID,Warehouse_Name,RegCapacity,sum(OfrCpt) as OfrCpt,case when FumigationWork='Y' then N'सहमत' else N'असहमत' end FumigationWork,sum(Agree_Capacity) as Agree_Capacity from (SELECT Registration_id,sum(Offer_Capacity) as OfrCpt,FumigationWork FROM tbl_Warehouse_Capacity_Offer_Rabi_2026_27 group by Registration_id,FumigationWork ) as OFR inner join  (Select DistrictID,BranchID,Registration_id,Warehouse_Name,regCapacity from tbl_WarehouseRegistration ) as REG on REG.Registration_id=OFR.Registration_ID inner join tbl_metadata_district as MD on MD.District_id=REG.DistrictID inner join tbl_metadata_depot as MDD on MDD.branchID=REG.BranchID left join  (select registration_id,Crop_Year,sum(Agree_Capacity) as Agree_Capacity from tbl_Godown_Agreement where CreatedDate>'03/18/2020' group by registration_id,Crop_Year)as AGR on AGR.registration_id=REG.Registration_id where MD.Region_ID='" + Session["UserId"].ToString() + "' And AGR.Crop_Year='2026-27' group by Regionnm,District_Name,DepotName,REG.Registration_ID,Warehouse_Name,regCapacity,FumigationWork ";
        }
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RegGrid.DataSource = ds;
                RegGrid.DataBind();
            }
            else
            {
                RegGrid.DataSource = "";
                RegGrid.DataBind();
            }
    }
}

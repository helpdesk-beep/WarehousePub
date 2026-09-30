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

public partial class JVS_DSO_Home : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string DistID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        lbluser.Text = Session["UserName"].ToString();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        try
        {
            if (!IsPostBack)
            {

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("UserReg.aspx");
        }
    }
    public void gerreg()
    {
        try
        {
            DistID = Session["UserId"].ToString();
            string qry = "";
            //Rabi 19-20
            //qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,Auth_Person,WARPRE.MobileNo,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer_2019 as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and MDDIS.District_Id='" + DistID + "' order by District_Name,DepotName";
            //Rabi 20-21
            qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,Auth_Person,WARPRE.MobileNo,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer_2022 as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and MDDIS.District_Id='" + DistID + "' order by District_Name,DepotName";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RegGrid.DataSource = ds;
                RegGrid.DataBind();
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;
                Label3.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                decimal sum = 0;
                for (int i = 0; i < RegGrid.Rows.Count; i++)
                {
                    sum += Convert.ToDecimal(RegGrid.Rows[i].Cells[8].Text.ToString());
                }
                Label5.Text = Convert.ToString(sum);
                decimal RegCsum = 0;
                for (int i = 0; i < RegGrid.Rows.Count; i++)
                {
                    RegCsum += Convert.ToDecimal(RegGrid.Rows[i].Cells[8].Text.ToString());
                }
                Label5.Text = Convert.ToString(sum);
                DataTable dt = ds.Tables[0];
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Offer_Capacity"));
                RegGrid.FooterRow.Cells[1].Text = "Total";
                RegGrid.FooterRow.Cells[8].Text = total.ToString("N2");
                decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("RegCapacity"));
                RegGrid.FooterRow.Cells[7].Text = total1.ToString("N2");

            }
            else
            {
                Label2.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                Label5.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void getBranchOffer()
    {
        try
        {
            DistID = Session["UserId"].ToString();
            string qry = "";
            //Rabi-19-20
            //qry = "select MDDIS.District_Name,DepotName,count(WAR.Registration_Id) as NoOfRegistration,sum(convert(Decimal(18,2),RegCapacity)) as RegCapacity,sum(convert(Decimal(18,2),Offer_Capacity)) as Offer_Capacity from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer_2019 as COFF on WAR.Registration_Id=COFF.Registration_Id inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and MDDIS.District_Id='" + DistID + "' group by District_Name,DepotName  order by District_Name,DepotName";
            //Rabi-20-21
            qry = "select MDDIS.District_Name,DepotName,count(WAR.Registration_Id) as NoOfRegistration,sum(convert(Decimal(18,2),RegCapacity)) as RegCapacity,sum(convert(Decimal(18,2),Offer_Capacity)) as Offer_Capacity from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer_2022 as COFF on WAR.Registration_Id=COFF.Registration_Id inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and MDDIS.District_Id='" + DistID + "' group by District_Name,DepotName  order by District_Name,DepotName";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridBranch.DataSource = ds;
                GridBranch.DataBind();
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;
                decimal sum = 0;
                for (int i = 0; i < GridBranch.Rows.Count; i++)
                {
                    sum += Convert.ToDecimal(GridBranch.Rows[i].Cells[5].Text.ToString());
                }
                Label5.Text = Convert.ToString(sum);
                int Regsum = 0;
                for (int i = 0; i < GridBranch.Rows.Count; i++)
                {
                    Regsum += Convert.ToInt32(GridBranch.Rows[i].Cells[3].Text.ToString());
                }
                Label3.Text = Convert.ToString(Regsum);
                DataTable dt = ds.Tables[0];
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Offer_Capacity"));
                GridBranch.FooterRow.Cells[1].Text = "Total";
                GridBranch.FooterRow.Cells[5].Text = total.ToString("N2");
                decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("RegCapacity"));
                GridBranch.FooterRow.Cells[4].Text = total1.ToString("N2");
            }
            else
            {
                Label2.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                Label5.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void getDistrict()
    {

    }
    private void getbranch()
    {

    }
    protected void rdoBranch_CheckedChanged(object sender, EventArgs e)
    {
        getBranchOffer();
        GBranch.Visible = true;
        GAll.Visible = false;
        RegGrid.DataSource = null;
        RegGrid.DataBind();
    }
    protected void rdoAll_CheckedChanged(object sender, EventArgs e)
    {
        gerreg();
        GAll.Visible = true;
        GridBranch.DataSource = null;
        GridBranch.DataBind();
        GBranch.Visible = false;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {


    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (rdoAll.Checked == true)
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
        else if (rdoBranch.Checked == true)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "BranchWiseOfferCapacity" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridBranch.Attributes["style"] = "border-collapse:separate";
            toexportBranch.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    public void getsessionwisedata()
    {
        try
        {
            DistID = Session["UserId"].ToString();
            string qry = "";
            //Rabi-19-20
            //qry = "select MDDIS.District_Name,DepotName,count(WAR.Registration_Id) as NoOfRegistration,sum(convert(Decimal(18,2),RegCapacity)) as RegCapacity,sum(convert(Decimal(18,2),Offer_Capacity)) as Offer_Capacity from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer_2019 as COFF on WAR.Registration_Id=COFF.Registration_Id inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and MDDIS.District_Id='" + DistID + "' group by District_Name,DepotName  order by District_Name,DepotName";
            //Rabi-20-21
            if (ddl_session.SelectedValue == "JVSR2223")
            {
                qry = "select MDDIS.District_Name,DepotName,count(WAR.Registration_Id) as NoOfRegistration,sum(convert(Decimal(18,2),RegCapacity)) as RegCapacity,sum(convert(Decimal(18,2),Offer_Capacity)) as Offer_Capacity from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer_2022 as COFF on WAR.Registration_Id=COFF.Registration_Id inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and MDDIS.District_Id='" + DistID + "' group by District_Name,DepotName  order by District_Name,DepotName";
            }
            //Kharib
            else if (ddl_session.SelectedValue == "JVSK2223")
            {
                qry = "select MDDIS.District_Name,DepotName,count(WAR.Registration_Id) as NoOfRegistration,sum(convert(Decimal(18,2),RegCapacity)) as RegCapacity,sum(convert(Decimal(18,2),Offer_Capacity)) as Offer_Capacity from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer_Kharif2022 as COFF on WAR.Registration_Id=COFF.Registration_Id inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and MDDIS.District_Id='" + DistID + "' group by District_Name,DepotName  order by District_Name,DepotName";
            }
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridBranch.DataSource = ds;
                GridBranch.DataBind();
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;
                decimal sum = 0;
                for (int i = 0; i < GridBranch.Rows.Count; i++)
                {
                    sum += Convert.ToDecimal(GridBranch.Rows[i].Cells[5].Text.ToString());
                }
                Label5.Text = Convert.ToString(sum);
                int Regsum = 0;
                for (int i = 0; i < GridBranch.Rows.Count; i++)
                {
                    Regsum += Convert.ToInt32(GridBranch.Rows[i].Cells[3].Text.ToString());
                }
                Label3.Text = Convert.ToString(Regsum);
                DataTable dt = ds.Tables[0];
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Offer_Capacity"));
                GridBranch.FooterRow.Cells[1].Text = "Total";
                GridBranch.FooterRow.Cells[5].Text = total.ToString("N2");
                decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("RegCapacity"));
                GridBranch.FooterRow.Cells[4].Text = total1.ToString("N2");
            }
            else
            {
                Label2.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                Label5.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
    {
            getsessionwisedata();
            GBranch.Visible = true;
            GAll.Visible = false;
            RegGrid.DataSource = null;
            RegGrid.DataBind();
        
    }
}
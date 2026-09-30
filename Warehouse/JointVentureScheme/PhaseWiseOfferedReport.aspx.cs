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

public partial class JointVentureScheme_PhaseWiseOfferedReport : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
                if (!IsPostBack)
                {
                    getDist();
                    ddlDistrict_SelectedIndexChanged(null, null);
                }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    } 
    public void gerreg()
    {
        try
        {
            string qry = "";
            if (ddlDist.SelectedItem.Text == "--Select--")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Phase ')", true);
            }
            else
            {
                if (ddl_session.SelectedValue == "2018")
                {
                    if (ddlDist.SelectedItem.Text != "All" && ddlDistrict.SelectedItem.Text == "All")
                    {
                        qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,Auth_Person,MobileNo,CONVERT(varchar(10),Registration_Date,103) as RegDate,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and COFF.Phase='" + ddlDist.SelectedValue.ToString() + "' order by District_Name,DepotName";
                    }
                    else if (ddlDist.SelectedItem.Text == "All" && ddlDistrict.SelectedItem.Text == "All")
                    {
                        qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,Auth_Person,MobileNo,CONVERT(varchar(10),Registration_Date,103) as RegDate,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' order by District_Name,DepotName";
                    }
                    else if (ddlDist.SelectedItem.Text == "All" && ddlDistrict.SelectedItem.Text != "All")
                    {
                        qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,Auth_Person,MobileNo,CONVERT(varchar(10),Registration_Date,103) as RegDate,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and MDDIS.District_Id='" + ddlDistrict.SelectedValue.ToString() + "' order by District_Name,DepotName";
                    }
                    else if (ddlDist.SelectedItem.Text != "All" && ddlDistrict.SelectedItem.Text != "All")
                    {
                        qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,Auth_Person,MobileNo,CONVERT(varchar(10),Registration_Date,103) as RegDate,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and COFF.Phase='" + ddlDist.SelectedValue.ToString() + "' and MDDIS.District_Id='" + ddlDistrict.SelectedValue.ToString() + "' order by District_Name,DepotName";
                    }
                }
                else if (ddl_session.SelectedValue == "2019")
                {
                    if (ddlDist.SelectedItem.Text != "All" && ddlDistrict.SelectedItem.Text != "All")
                    {
                        qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,Auth_Person,MobileNo,CONVERT(varchar(10),Registration_Date,103) as RegDate,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer_2019 as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join  tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and COFF.Phase='" + ddlDist.SelectedValue.ToString() + "'   and MDDIS.District_Id='" + ddlDistrict.SelectedValue.ToString() + "' order by District_Name,DepotName";
                    }
                    else if (ddlDist.SelectedItem.Text != "All" && ddlDistrict.SelectedItem.Text == "All")
                    {
                        qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,Auth_Person,MobileNo,CONVERT(varchar(10),Registration_Date,103) as RegDate,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer_2019 as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join  tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and COFF.Phase='" + ddlDist.SelectedValue.ToString() + "'  order by District_Name,DepotName";
                    }
                }

                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["dsReg"] = ds;
                    RegGrid.DataSource = ds;
                    RegGrid.DataBind();

                    DataTable dt = ds.Tables[0];
                    decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Offer_Capacity"));
                    RegGrid.FooterRow.Cells[1].Text = "Total";
                    RegGrid.FooterRow.Cells[9].Text = total.ToString("N2");

                    decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("RegCapacity"));
                    RegGrid.FooterRow.Cells[7].Text = total1.ToString("N2");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
                    RegGrid.DataSource = "";
                    RegGrid.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void getPhase()
    {
        //string query = "";
        //if (ddl_session.SelectedValue.ToString() == "2018")
        //{
        //    query = "select distinct Phase from tbl_Warehouse_Capacity_Offer  order by phase";
        //}
        //else if (ddl_session.SelectedValue.ToString() == "2019")
        //{
        //    query = "select distinct Phase from tbl_Warehouse_Capacity_Offer_2019 order by phase";
        //}
        //SqlCommand cmd = new SqlCommand(query, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlDist.Items.Clear();
        //    ddlDist.DataSource = ds.Tables[0];
        //    ddlDist.DataTextField = "Phase";
        //    ddlDist.DataValueField = "Phase";
        //    ddlDist.DataBind();
        //    ddlDist.Items.Insert(0, "--Select--");
        //    ddlDist.Items.Insert(1, "All");
        //    ddlDist.SelectedValue = "1";
        //}
    }
    private void getDist()
    {
        string query = "select District_Id,District_Name from tbl_MetaData_DISTRICT  order by District_Name";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.DataSource = ds.Tables[0];
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_Id";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "--Select--");
            ddlDistrict.Items.Insert(1, "All");
            ddlDistrict.SelectedIndex = 1;

        }
    }

    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDist.SelectedItem.Text == "--Select--" )
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Phase :...'); </script> ");
        }
        else
        {
            gerreg();
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDist.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Phase :...'); </script> ");
        }
        else if (ddlDistrict.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select District :...'); </script> ");
        }
        else
        {
            gerreg();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (RegGrid.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "OfferCapacity" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            RegGrid.Attributes["style"] = "border-collapse:separate";
            toexportDist.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Data in GridView to Export  :...'); </script> ");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string qry = "";
        //if (ddl_session.SelectedValue.ToString() == "2018")
        //{
        //    getPhase();
        //}
        //else if (ddl_session.SelectedValue.ToString() == "2019")
        //{
        //    getPhase();
        //}
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("DistrictWiseJVSOffer.aspx");
    }
}

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

public partial class JointVentureScheme_MDLoginReport : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
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
            string qry = "";
            qry = "select MDDIS.District_Name,MDD.DepotName,Warehouse_Name,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,RegAmt,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,OfferAmt,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and COFF.Phase='1' order by District_Name,DepotName";
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
                    sum += Convert.ToDecimal(RegGrid.Rows[i].Cells[7].Text.ToString());
                }
                Label5.Text = Convert.ToString(sum);
                decimal RegCsum = 0;
                for (int i = 0; i < RegGrid.Rows.Count; i++)
                {
                    RegCsum += Convert.ToDecimal(RegGrid.Rows[i].Cells[7].Text.ToString());
                }
                Label5.Text = Convert.ToString(sum);
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
    public void getDistwiseOffer()
    {
        try
        {
            string qry = "";
            qry = "select MDDIS.District_Name,count(WAR.Registration_Id) as NoOfRegistration,sum(convert(Decimal(18,2),RegCapacity)) as RegCapacity,sum(convert(Decimal(18,2),Offer_Capacity)) as Offer_Capacity from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer as COFF on WAR.Registration_Id=COFF.Registration_Id  where RegCapacity !='0.00' and COFF.Phase='1' group by District_Name  order by District_Name";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridDist.DataSource = ds;
                GridDist.DataBind();
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;
               // Label3.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                decimal sum = 0;
                for (int i = 0; i < GridDist.Rows.Count; i++)
                {
                    sum += Convert.ToDecimal(GridDist.Rows[i].Cells[4].Text.ToString());
                }
                Label5.Text = Convert.ToString(sum);
                int Regsum = 0;
                for (int i = 0; i < GridDist.Rows.Count; i++)
                {
                    Regsum += Convert.ToInt32(GridDist.Rows[i].Cells[2].Text.ToString());
                }
                Label3.Text = Convert.ToString(Regsum);
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
            string qry = "";
            qry = "select MDDIS.District_Name,DepotName,count(WAR.Registration_Id) as NoOfRegistration,sum(convert(Decimal(18,2),RegCapacity)) as RegCapacity,sum(convert(Decimal(18,2),Offer_Capacity)) as Offer_Capacity from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join  tbl_Warehouse_Capacity_Offer as COFF on WAR.Registration_Id=COFF.Registration_Id inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and COFF.Phase='1' group by District_Name,DepotName  order by District_Name,DepotName";
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
                // Label3.Text = Convert.ToString(ds.Tables[0].Rows.Count);
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
        //string query = "select District_Id,District_Name from tbl_MetaData_DISTRICT order by District_Name";
        //SqlCommand cmd = new SqlCommand(query, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlDist.Items.Clear();
        //    ddlDist.DataSource = ds.Tables[0];
        //    ddlDist.DataTextField = "District_Name";
        //    ddlDist.DataValueField = "District_Id";
        //    ddlDist.DataBind();
        //    ddlDist.Items.Insert(0, "--Select--");
        //}
    }
    private void getbranch()
    {
        //string query = "select BranchId,DepotName from tbl_MetaData_DEPOT order by DepotName";
        //SqlCommand cmd = new SqlCommand(query, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlDist.Items.Clear();
        //    ddlDist.DataSource = ds.Tables[0];
        //    ddlDist.DataTextField = "DepotName";
        //    ddlDist.DataValueField = "BranchId";
        //    ddlDist.DataBind();
        //    ddlDist.Items.Insert(0, "--Select--");
        //}
    }

    protected void rdoDist_CheckedChanged(object sender, EventArgs e)
    {
        //Label1.Text = "Select District :";
        //Label1.Visible = true;
        //ddlDist.Visible = true;
        //RegGrid.DataSource = null;
        //RegGrid.DataBind();
        //Label2.Visible = false;
        //Label3.Visible = false;
        //Label4.Visible = false;
        //Label5.Visible = false;
        //getDistrict();
        getDistwiseOffer();
        GDist.Visible = true;
        GBranch.Visible = false;
        GAll.Visible = false;
        RegGrid.DataSource = null;
        RegGrid.DataBind();
        GridBranch.DataSource = null;
        GridBranch.DataBind();
    }
    protected void rdoBranch_CheckedChanged(object sender, EventArgs e)
    {
        //Label1.Text = "Select Branch :";
        //Label1.Visible = true;
        //ddlDist.Visible = true;
        //RegGrid.DataSource = null;
        //Label2.Visible = false;
        //Label3.Visible = false;
        //Label4.Visible = false;
        //Label5.Visible = false;
        //RegGrid.DataBind();
        //getbranch();
        getBranchOffer();
        GBranch.Visible = true;
        GDist.Visible = false;
        GAll.Visible = false;
        RegGrid.DataSource = null;
        RegGrid.DataBind();
        GridDist.DataSource = null;
        GridDist.DataBind();
    }
    //protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlDist.SelectedItem.Text == "--Select--")
    //    {
    //        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select  :...'); </script> ");
    //    }
    //    else
    //    {
    //        gerreg();
    //    }
    //}
    protected void rdoAll_CheckedChanged(object sender, EventArgs e)
    {
        //Label1.Visible = false;
        //ddlDist.Visible = false;
        //ddlDist.Items.Clear();
        //ddlDist.DataSource = null;
        //ddlDist.DataBind();
        gerreg();
        GAll.Visible = true;
        GDist.Visible = false;
        GridDist.DataSource = null;
        GridDist.DataBind();
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
        else if (rdoDist.Checked == true)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "DistWiseOfferCapacity" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridDist.Attributes["style"] = "border-collapse:separate";
            toexportDist.RenderControl(htmltextwrtter);
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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
}

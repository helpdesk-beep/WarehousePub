using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;

public partial class JointVentureScheme_Govt_RegistrationReport : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
//    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string Branch = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAgency();
           // gerreg();
        }
    }
    public void gerreg(string qrychk)
    {
        try
        {

            SqlCommand cmd = new SqlCommand(qrychk, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RegGrid.DataSource = null;
                RegGrid.DataBind();
                RegGrid.DataSource = ds;
                RegGrid.DataBind();

                DataTable dt = ds.Tables[0];
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("OfferCapacity"));
                RegGrid.FooterRow.Cells[1].Text = "Total";
                RegGrid.FooterRow.Cells[7].Text = total.ToString("N2");

                decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("InspectedCpt"));
                RegGrid.FooterRow.Cells[8].Text = total1.ToString("N2");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {


    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (GAll.Visible == true)
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
        else if (GDist.Visible == true)
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
            GridDist.Attributes["style"] = "border-collapse:separate";
            toexport.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }

    public void gerregDist(string qrydist)
    {
           // string qry = "";
           // qry = "select MDDIS.District_Name,count(WAR.Registration_Id) as NoOfRegistration,sum(convert(Decimal(18,0),RegCapacity)) as RegCapacity,isnull(sum(convert(Decimal(18,0),Offer_Capacity)),0) as Offer_Capacity from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId left join  (select SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt) as  OfferAmt,Registration_Id from tbl_Warehouse_Capacity_Offer_2019  group by Registration_Id) as COFF on WAR.Registration_Id=COFF.Registration_Id where RegCapacity !='0.00' and WAR.Registration_Id in (select distinct REGISTRATIONID from tbl_Payment_Status)  group by District_Name  order by District_Name";
            SqlCommand cmd = new SqlCommand(qrydist, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridDist.DataSource = ds;
                GridDist.DataBind();

                decimal sum = 0;
                //for (int i = 0; i < GridDist.Rows.Count; i++)
                //{
                //    sum += Convert.ToDecimal(GridDist.Rows[i].Cells[4].Text.ToString());
                //}
                int Regsum = 0;
                for (int i = 0; i < GridDist.Rows.Count; i++)
                {
                    Regsum += Convert.ToInt32(GridDist.Rows[i].Cells[2].Text.ToString());
                }

                DataTable dt = ds.Tables[0];

                decimal total6 = dt.AsEnumerable().Sum(row => row.Field<int>("NoOfWarehouseReg"));
                GridDist.FooterRow.Cells[2].Text = total6.ToString("N2");

                decimal total5 = dt.AsEnumerable().Sum(row => row.Field<decimal>("RegCapacity"));
                GridDist.FooterRow.Cells[3].Text = total5.ToString("N2");
            }
            else
            {
                GridDist.DataSource = null;
                GridDist.DataBind();
               
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "";
        if (ddlreporttype.SelectedValue == "1" && ddlDistrict.SelectedItem.Text != "All")
        {
            qry = "select District_Name,isnull(NoOfWarehouseReg,0) NoOfWarehouseReg,isnull(RegCapacity,0) RegCapacity from tbl_MetaData_DISTRICT as MD left join (select W.DistrictId,COUNT(Registration_Id) as NoOfWarehouseReg,sum(isnull(w.RegCapacity,0)) as RegCapacity from tbl_WarehouseRegistration w where w.WarehouseHiredType is not null and WarehouseHiredtype='" + ddlDistrict.SelectedValue.ToString() + "' group by W.DistrictId) as MDDIS on MDDIS.DistrictId=MD.District_Id ";
            GDist.Visible = true;
            GAll.Visible = false;
            gerregDist(qry);
        }
        else if (ddlreporttype.SelectedValue == "1" && ddlDistrict.SelectedItem.Text == "All")
        {
            qry = "select District_Name,isnull(NoOfWarehouseReg,0) NoOfWarehouseReg,isnull(RegCapacity,0) RegCapacity from tbl_MetaData_DISTRICT as MD left join (select W.DistrictId,COUNT(Registration_Id) as NoOfWarehouseReg,sum(isnull(w.RegCapacity,0)) as RegCapacity from tbl_WarehouseRegistration w where w.WarehouseHiredType is not null group by W.DistrictId) as MDDIS on MDDIS.DistrictId=MD.District_Id ";
            GDist.Visible = true;
            GAll.Visible = false;
            gerregDist(qry);
        }
        else if (ddlreporttype.SelectedValue == "2" && ddlDistrict.SelectedItem.Text == "All")
        {
            qry = "Select(select d.District_Name from tbl_MetaData_DISTRICT d where d.District_Id=w.DistrictId) as District,(Select b.DepotName from tbl_MetaData_DEPOT b where b.BranchId=w.BranchId) as Branch,w.Warehouse_Name,w.Registration_Id,(Select Agency_Name from tbl_Storage_Agency where SAID=w.WarehouseHiredType) as Agency,(select count(Godown_No) from tbl_WarehouseGodown_Reg g where g.Registration_Id=w.Registration_Id group by g.Registration_Id) as No_Of_Godown,w.Warehouse_Address,w.RegCapacity,CONVERT(varchar(10),Registration_Date,103) as RegisteredDate,w.WarehouseHiredType from  tbl_WarehouseRegistration w where w.WarehouseHiredType is not null ";
            GDist.Visible = false;
            GAll.Visible = true;
            gerreg(qry);
        }
        else if (ddlreporttype.SelectedValue == "2" && ddlDistrict.SelectedItem.Text != "All")
        {
            qry = "Select(select d.District_Name from tbl_MetaData_DISTRICT d where d.District_Id=w.DistrictId) as District,(Select b.DepotName from tbl_MetaData_DEPOT b where b.BranchId=w.BranchId) as Branch,w.Warehouse_Name,w.Registration_Id,(Select Agency_Name from tbl_Storage_Agency where SAID=w.WarehouseHiredType) as Agency,(select count(Godown_No) from tbl_WarehouseGodown_Reg g where g.Registration_Id=w.Registration_Id group by g.Registration_Id) as No_Of_Godown,w.Warehouse_Address,w.RegCapacity,CONVERT(varchar(10),Registration_Date,103) as RegisteredDate,w.WarehouseHiredType from  tbl_WarehouseRegistration w where w.WarehouseHiredType is not null and WarehouseHiredtype='"+ ddlDistrict.SelectedValue.ToString() +"'";
            GDist.Visible = false;
            GAll.Visible = true;
            gerreg(qry);
        }
        

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("DistrictWiseJVSOffer.aspx");
    }
    public void BindAgency()
    {
        string str = "Select '0' SAID,'All' Agency_Name from tbl_Storage_Agency union Select SAID,Agency_Name from tbl_Storage_Agency order by Agency_Name";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrict.DataSource = ds.Tables[0];
            ddlDistrict.DataTextField = "Agency_Name";
            ddlDistrict.DataValueField = "SAID";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "---Select---");
        }
        else
        {

            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, "---Select---");
        }
    }
}


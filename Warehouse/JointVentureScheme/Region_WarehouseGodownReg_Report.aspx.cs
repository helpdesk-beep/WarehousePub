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

public partial class JointVentureScheme_Region_WarehouseGodownReg_Report : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string Branch = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    public void gerreg(String Cpttype)
    {
        try
        {
            //Branch = Session["UserId"].ToString();
            string qry = "";
            //if (rdoAll.Checked == true)
            //{
            //    qry = "select Registration_Id,Offer_Id,convert(decimal(18,2),(G_OfferCapacity)) as InspectedCpt,Fit_Unfit,AutoFit_Unfit ,(select Warehouse_Name from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Warehouse_Name,(select Mobile_No from tbl_WarehouseRegistration where Registration_Id=INSP.Registration_Id) as Mobile_No,(select DepotName from tbl_MetaData_DEPOT where BranchId=INSP.BranchId) as DepotName,(select District_Name from tbl_MetaData_DISTRICT where District_Id=INSP.DistrictId) as District_Name,(select convert(decimal(18,2),(G_OfferCapacity))    from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId) as OfferCapacity,(select  G_Scheme from tbl_Warehouse_Godown_Offer where Godown_ID=INSP.GodownId)  G_Scheme from tbl_Godown_Inspection as INSP";
            //}
            //else if (rdoBranch.Checked == true)
            //{
            //    qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where WR.BranchId='" + Branch + "' ) as FinalOfferList where RegDepositeFee!=0 and OfferDepositeFee!=0 and (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee)  order by OfferedDate";
            //}
            if (Cpttype == "2001")
            {
                qry = "select District,Branch,Warehouse_Name,case when Agency IS NULL then 'JVS' else Agency end Agency_Name,No_Of_Godown,CONVERT(decimal(18,0),RegCapacity) as RegCapacity from (Select(select d.District_Name from tbl_MetaData_DISTRICT d where d.District_Id=w.DistrictId) as District,(Select b.DepotName from tbl_MetaData_DEPOT b where b.BranchId=w.BranchId) as Branch,w.Warehouse_Name,w.Registration_Id,(Select case when Agency_Name IS NULL then 'JVS' else Agency_Name end Agency_Name from tbl_Storage_Agency where SAID=w.WarehouseHiredType) as Agency,(select count(Godown_No) from tbl_WarehouseGodown_Reg g where g.Registration_Id=w.Registration_Id group by g.Registration_Id) as No_Of_Godown,w.Warehouse_Address,w.RegCapacity,CONVERT(varchar(10),Registration_Date,103) as RegisteredDate,w.WarehouseHiredType from tbl_WarehouseRegistration w where DistrictId in (select MDD.District_Id from tbl_MetaData_DISTRICT as MDD where MDD.Region_ID='" + Session["UserId"].ToString() + "')) as mrg where RegCapacity>=2000 and  RegCapacity>0";
            }
            else if (Cpttype == "1999")
            {
                qry = "select District,Branch,Warehouse_Name,case when Agency IS NULL then 'JVS' else Agency end Agency_Name,No_Of_Godown,CONVERT(decimal(18,0),RegCapacity) as RegCapacity from (Select(select d.District_Name from tbl_MetaData_DISTRICT d where d.District_Id=w.DistrictId) as District,(Select b.DepotName from tbl_MetaData_DEPOT b where b.BranchId=w.BranchId) as Branch,w.Warehouse_Name,w.Registration_Id,(Select case when Agency_Name IS NULL then 'JVS' else Agency_Name end Agency_Name from tbl_Storage_Agency where SAID=w.WarehouseHiredType) as Agency,(select count(Godown_No) from tbl_WarehouseGodown_Reg g where g.Registration_Id=w.Registration_Id group by g.Registration_Id) as No_Of_Godown,w.Warehouse_Address,w.RegCapacity,CONVERT(varchar(10),Registration_Date,103) as RegisteredDate,w.WarehouseHiredType from tbl_WarehouseRegistration w where DistrictId in (select MDD.District_Id from tbl_MetaData_DISTRICT as MDD where MDD.Region_ID='" + Session["UserId"].ToString() + "')) as mrg where RegCapacity<2000 and  RegCapacity>0";
            }



         //   qry = "select District,Branch,Warehouse_Name,case when Agency IS NULL then 'JVS' else Agency end Agency_Name,No_Of_Godown,CONVERT(decimal(18,0),RegCapacity) as RegCapacity from (Select(select d.District_Name from tbl_MetaData_DISTRICT d where d.District_Id=w.DistrictId) as District,(Select b.DepotName from tbl_MetaData_DEPOT b where b.BranchId=w.BranchId) as Branch,w.Warehouse_Name,w.Registration_Id,(Select case when Agency_Name IS NULL then 'JVS' else Agency_Name end Agency_Name from tbl_Storage_Agency where SAID=w.WarehouseHiredType) as Agency,(select count(Godown_No) from tbl_WarehouseGodown_Reg g where g.Registration_Id=w.Registration_Id group by g.Registration_Id) as No_Of_Godown,w.Warehouse_Address,w.RegCapacity,CONVERT(varchar(10),Registration_Date,103) as RegisteredDate,w.WarehouseHiredType from tbl_WarehouseRegistration w where DistrictId in (select MDD.District_Id from tbl_MetaData_DISTRICT as MDD where MDD.Region_ID='" + Session["UserId"].ToString() + "')) as mrg where RegCapacity>0";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RegGrid.DataSource = null;
                RegGrid.DataBind();
                RegGrid.DataSource = ds;
                RegGrid.DataBind();
                //Label2.Visible = true;
                //Label3.Visible = true;
                //Label4.Visible = true;
                //Label5.Visible = true;



                //Label3.Text = Convert.ToString(ds.Tables[0].Rows.Count);

                //decimal sum = 0;
                //for (int i = 0; i < RegGrid.Rows.Count; i++)
                //{
                //    sum += Convert.ToDecimal(RegGrid.Rows[i].Cells[6].Text.ToString());
                //}
                //Label5.Text = Convert.ToString(sum);

                //decimal RegCsum = 0;
                //for (int i = 0; i < RegGrid.Rows.Count; i++)
                //{
                //    RegCsum += Convert.ToDecimal(RegGrid.Rows[i].Cells[6].Text.ToString());
                //}
                //Label5.Text = Convert.ToString(sum);

                DataTable dt = ds.Tables[0];
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("OfferCapacity"));
                RegGrid.FooterRow.Cells[1].Text = "Total";
                RegGrid.FooterRow.Cells[7].Text = total.ToString("N2");

                decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("InspectedCpt"));
                RegGrid.FooterRow.Cells[8].Text = total1.ToString("N2");
            }
            else
            {
                //Label2.Visible = false;
                //Label3.Visible = false;
                //Label4.Visible = false;
                //Label5.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    //protected void rdoAll_CheckedChanged(object sender, EventArgs e)
    //{
    //    gerreg();
    //    GAll.Visible = true;
    //}

    public override void VerifyRenderingInServerForm(Control control)
    {


    }
    protected void Button1_Click1(object sender, EventArgs e)
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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        gerreg(ddlgodown.SelectedValue.ToString());
    }
}

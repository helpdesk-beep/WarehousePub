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

public partial class JointVentureScheme_JVSBranchReport_Self_Pms : System.Web.UI.Page
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
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranch != "" && SessBranchID !="")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessBranch;
                gerreg();
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }
    public void gerreg()
    {
        try
        {
            Branch = Session["UserId"].ToString();
            string qry = "";

            // Rabi 2018-19
            //if (rdoAll.Checked == true && ddl_session.SelectedValue.ToString() == "JVS2020_21")
            //{
            //    qry = "select Warehouse_Name,Auth_Person,WARPRE.MobileNo,WAR.Registration_Id,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),COFF.CreatedDate,103) as OfferedDate from tbl_WarehouseRegistration as WAR inner join tbl_Warehouse_PreReg as WARPRE on WAR.Registration_Id=WARPRE.Reg_No inner Join tbl_MetaData_DISTRICT  as MDDIS on MDDIS.District_Id=WAR.DistrictId  inner join tbl_Warehouse_Capacity_Offer_2020 as COFF on WAR.Registration_Id=COFF.Registration_Id  inner join tbl_MetaData_DEPOT as MDD on MDD.BranchId=WAR.BranchId where RegCapacity !='0.00' and COFF.Phase < 9 and WAR.BranchId='" + Branch + "'";
            //}
            
            //if (rdoBranch.Checked == true && ddl_session.SelectedValue.ToString() == "Rab2023_24")
            {
                qry = "select Warehouse_Name,Auth_Person,MobileNo,Registration_Id,RegCapacity,Offer_Capacity,OfferedDate,RegAmt,OfferAmt from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity,convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate, convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate >= convert(varchar(10),'02/27/2023',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_Rabi2023 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where CO.OfferSeason='Rab2023_24' and  WR.BranchId='" + Branch + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee) order by OfferedDate ";
            }

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
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;
                Button1.Visible = true;
                Label3.Text = Convert.ToString(ds.Tables[0].Rows.Count);
                decimal sum = 0;
                for (int i = 0; i < RegGrid.Rows.Count; i++)
                {
                    sum += Convert.ToDecimal(RegGrid.Rows[i].Cells[6].Text.ToString());
                }
                Label5.Text = Convert.ToString(sum);
                decimal RegCsum = 0;
                for (int i = 0; i < RegGrid.Rows.Count; i++)
                {
                    RegCsum += Convert.ToDecimal(RegGrid.Rows[i].Cells[6].Text.ToString());
                }
                Label5.Text = Convert.ToString(sum);
                DataTable dt = ds.Tables[0];
                decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Offer_Capacity"));
                RegGrid.FooterRow.Cells[1].Text = "Total";
                RegGrid.FooterRow.Cells[6].Text = total.ToString("N2");
                decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("RegCapacity"));
                RegGrid.FooterRow.Cells[5].Text = total1.ToString("N2");
            }
            else
            {
                Label2.Visible = false;
                Label3.Visible = false;
                Label4.Visible = false;
                Label5.Visible = false;
                Button1.Visible = false;
                RegGrid.DataSource = null;
                RegGrid.DataBind();
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
    //    //GAll.Visible = true;
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
        Response.Redirect("Logins.aspx");
    }
    //protected void rdoBranch_CheckedChanged(object sender, EventArgs e)
    //{
    //    gerreg();
    //}
    //protected void rdoAll_CheckedChanged(object sender, EventArgs e)
    //{
    //    gerreg();
    //}
}

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
using System.Data.SqlClient;
using System.Globalization;

public partial class JointVentureScheme_Warehouse_Self_And_Pms_Choise_Felling : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["email"] != null) && (Session["mobile"] != null))
        {
            lbluser.Text = Session["fname"].ToString() + " " + Session["mname"].ToString() + " " + Session["lname"].ToString();
            if (!IsPostBack)
            {
                gerreg();
                //toexport.Visible = false;
            }
        }
        else
        {
            Response.Redirect("UserReg.aspx");
        }
    }
    public void gerreg()
    {
        try
        {
            string qry = "";
            //qry = "select REG.Registration_Id,RegCapacity,RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No left join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from  tbl_Warehouse_Capacity_Offer_2020 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>CONVERT(varchar(10),'02/19/2020',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID where REG.Registration_Id='" + Session["Reg_No"].ToString() + "'";
            //  qry = "select REG.Registration_Id,RegCapacity,RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No left join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from  tbl_Warehouse_Capacity_Offer_2021 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>CONVERT(varchar(10),'02/19/2020',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID where REG.Registration_Id='" + Session["Reg_No"].ToString() + "'";
            //qry = "select REG.Registration_Id,RegCapacity,RegAmt ,isnull(REGPMT.DepositedRegAmount,0) DepositedRegAmt,case when RegAmt<=isnull(REGPMT.DepositedRegAmount,0) then 'Confirmed' else 'Pending' end RegPaymentStatus,isnull(Offer_Capacity,0)Offer_Capacity,isnull(OfferAmt,0)OfferAmt,isnull(OFRPMT.DepositedOfferAmount,0) ofrdepositedamt,case when OfferAmt<=isnull(OFRPMT.DepositedOfferAmount,0) then 'Confirmed' else 'Pending' end OfferPaymentStatus from tbl_WarehouseRegistration as REG inner join tbl_Warehouse_PreReg as PREG on REG.Registration_Id=PREG.Reg_No left join (select Registration_Id,SUM(Offer_Capacity) as Offer_Capacity,SUM(OfferAmt)as OfferAmt from  tbl_Warehouse_Capacity_Offer_Kharif2022 group by Registration_Id) as OFR on OFR.Registration_Id=REG.Registration_Id left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedRegAmount from tbl_Payment_Status as PYT where CategoryName='REGISTRATION FEE' group by REGISTRATIONID) REGPMT on REG.Registration_Id=REGPMT.REGISTRATIONID left join (select REGISTRATIONID,isnull(SUM(FEE),0) DepositedOfferAmount from tbl_Payment_Status as PYT where CategoryName='OFFER FEES' and TransactionDate>CONVERT(varchar(10),'02/19/2022',101) group by REGISTRATIONID) OFRPMT on REG.Registration_Id=OFRPMT.REGISTRATIONID where REG.Registration_Id='" + Session["Reg_No"].ToString() + "'";

            SqlCommand cmd = new SqlCommand("Warehouse_Self_Pms_Choise_Filling", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Registration_Id", Session["Reg_No"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RegGrid.DataSource = ds;
                RegGrid.DataBind();
                //toexport.Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Wrong ')", true);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }



    public void Update(string hdnregid, string ddlChoise)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("Update_Warehouse_Self_Pms_Choise_Filling", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Registration_Id", hdnregid);
            cmd.Parameters.AddWithValue("@Choice", ddlChoise);
            cmd.Parameters.AddWithValue("@insert_By", localIP);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                gerreg();
                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Choice Filling  Successfully')", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }



    protected void RegGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = RegGrid.Rows[rowIndex];

            //Fetch value of Name.
            string hdnregid = (row.FindControl("hdnregid") as HiddenField).Value;
            //string ddlEWC = (row.FindControl("ddlEWC") as DropDownList).SelectedValue;
            //string hdnBranchId = (row.FindControl("hdnBranchId") as TextBox).Text;
            string ddlChoise = (row.FindControl("ddlChoise") as DropDownList).SelectedValue;
            //string lblFirstWHRDate = (row.FindControl("flupslip") as TextBox).Text;

            //Session["hdnId"] = hdnId.ToString();
            Session["hdnregid"] = hdnregid.ToString();
            Session["ddlChoise"] = ddlChoise.ToString();

            Update(hdnregid, ddlChoise);

        }
    }
}


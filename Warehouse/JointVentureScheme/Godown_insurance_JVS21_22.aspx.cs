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
public partial class JointVentureScheme_Godown_insurance_JVS21_22 : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    public string qry = "";
    public string App_Id = "";
    public string ImgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        string SessBranch = Session["UserName"].ToString();
        string SessBranchID = Session["UserId"].ToString();
        if (SessBranch != "" && SessBranchID != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessBranch;


                gerreg();
                // lblDate.Text = DateTime.Now.ToString();
                //  fillPaddygridedata();
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }



    public void gerreg()
    {
        //string qry = qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity, convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'02/19/2020',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2020 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where  (CO.OfferSeason='JVS2020_21' or CO.OfferSeason='JVS2020_21' ) and  WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee) order by Warehouse_Name ";
        string qry = qry = "select distinct Warehouse_Name +' (' +Registration_Id +')' as Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity, convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'02/19/2021',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_2021 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where  (CO.OfferSeason='JVS2021_22' or CO.OfferSeason='JVS2021_22' ) and  WR.BranchId='" + Session["UserId"].ToString() + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee) order by Warehouse_Name ";


        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlWarName.DataSource = ds.Tables[0];
            ddlWarName.DataTextField = "Warehouse_Name";
            ddlWarName.DataValueField = "Registration_Id";
            ddlWarName.DataBind();
            ddlWarName.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
        }
    }



    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("Logins.aspx");
    }


    protected void ddlinsurance_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWarName.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Warehouse Name ')", true);
        }
        else
        {
            if (ddlinsurance.SelectedValue == "YES")
            {
                GAll.Visible = true;
                Table1.Visible = false;

            }

            else

            {
                GAll.Visible = false;
                Table1.Visible = true;

            }
        }
    }

    protected void ddlWarName_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ddlinsurance.Items.Clear();
        ddlinsurance.SelectedValue = "0";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {



        if (txtSLDate.Text != "" && txtInsuranceCapacity.Text != "" && txtvaluestk.Text != "" && txtPremium.Text != "" && ddlinsurance.SelectedValue!="0")
        {



            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;

            qry = "select * from Tbl_jvs_insurance_Detail where RegID='" + ddlWarName.SelectedValue + "'";
            SqlDataAdapter da = new SqlDataAdapter(qry, constr);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(qry, constr);
            ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Warehouse Already Exists..'); </script> ");
            }

            else
            {

                using (SqlConnection con1 = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("Sp_JvsInsurance", con1))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", ddlinsurance.SelectedValue);
                        cmd.Parameters.AddWithValue("@RegID", ddlWarName.SelectedValue);
                        cmd.Parameters.AddWithValue("@Idate", txtSLDate.Text.Trim());
                        cmd.Parameters.AddWithValue("@InsuranceCapacity", txtInsuranceCapacity.Text.Trim());
                        cmd.Parameters.AddWithValue("@valuestk", txtvaluestk.Text.Trim());
                        cmd.Parameters.AddWithValue("@Premium", txtPremium.Text.Trim());
                        cmd.Parameters.AddWithValue("@createby", ip);
                        cmd.Parameters.AddWithValue("@Idatevaladity", TextBox1.Text.Trim());                      

                        con1.Open();
                        int k = cmd.ExecuteNonQuery();
                        con1.Close();
                        ddlinsurance.SelectedValue = "0";
                        ddlWarName.SelectedValue = "0";
                        txtSLDate.Text = "";
                        txtInsuranceCapacity.Text = "";
                        txtvaluestk.Text = "";
                        txtPremium.Text = "";
                        TextBox1.Text = "";

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Insert Successfully'); </script> ");
                    }
                }
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Fill All Fields'); </script> ");
        }
       
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string qry1;
        qry1 = "select * from Tbl_jvs_insurance_Detail where RegID='" + ddlWarName.SelectedValue + "'";
        SqlDataAdapter da1 = new SqlDataAdapter(qry1, constr);
        DataSet ds1 = new DataSet();
        da1 = new SqlDataAdapter(qry1, constr);
        ds1 = new DataSet();
        da1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Warehouse Already Exists..'); </script> ");
        }
        else
        {

            qry = "insert into Tbl_jvs_insurance_Detail(RegID,create_date,Jvs_Flag,create_by) values('" + ddlWarName.SelectedValue + "',getdate(),'" + ddlinsurance.SelectedValue + "','" + ip + "')";
            SqlDataAdapter da = new SqlDataAdapter(qry, constr);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(qry, constr);
            ds = new DataSet();
            da.Fill(ds);
           // if (ds.Tables[0].Rows.Count > 0)
            //{
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Insert Successfully..'); </script> ");
           // }
        }
    }
}
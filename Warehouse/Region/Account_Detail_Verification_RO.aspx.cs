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
using System.Security.Principal;

public partial class Region_Account_Detail_Verification_RO : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    decimal ChargeOfTotal = 0;
    decimal RebateAmount = 0;
    decimal NetAmount = 0;
    decimal AccruedNetAmount = 0;
    string NetAmountWord = "";
    string Bill_No = "";
    decimal Discount = 0;
    decimal Service_Tax = 0;
    string Bill_Type = "";
    int BID = 0;
    public string GenerateOTP = "", OTPSMS = "";
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Region_ID"].ToString() != null)
        {
            if (!IsPostBack)
            {
                GetDSCDetail();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void User_Ver(string Ver_Type, string DSCID, string Serial_No)
    {
        if ((Session["Region_ID"] != null))
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //string Dist_id = Session["Depot_DistID"].ToStringvDSCUserVer_RowCommandg().Substring(2, 2);
            string Region_Id = Session["Region_ID"].ToString();
            SqlTransaction sqltran = null;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqltran = con.BeginTransaction();
                string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
                //string str = "update tbl_Beneficiary_Account_Details set GO_Approval_Status='" + Ver_Type + "',GO_Approval_Date=GETDATE(),GO_Approval_IP='" + ip + "' where Beneficiary_Id='" + DSCID + "' and Account_No='" + Serial_No + "'";
                string str = "update tbl_Beneficiary_Account_Details set RO_Approval_Status='" + Ver_Type + "',RO_Approval_Date=GETDATE(),RO_Approval_IP='" + ip + "' where Beneficiary_Id='" + DSCID + "' and Account_No='" + Serial_No + "'";

                cmd = new SqlCommand(str, con, sqltran);
                int req = cmd.ExecuteNonQuery();
                if (req > 0)
                {
                    sqltran.Commit();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Account Successfully Verified....')", true);

                    GetDSCDetail();
                }
                else
                {
                    //lbl_message.Text = "WHR record saved successfully";
                }
            }
            catch (Exception ex)
            {
                sqltran.Rollback();
                Response.Write(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
    }
    private void GetDSCDetail()
    {
        try
        {
            //string Godown_Id = Session["GodownID_New"].ToString();
            //string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            //string str = " select Beneficiary_Id,Beneficiary_Name,tbl_Beneficiary_Account_Details.Account_No,tbl_Beneficiary_Account_Details.IFSC_Code,PAN,GST,Mobile,Email,Aadhar_No,Adress1+' '+Adress2+' '+Adress_City as Address from tbl_Beneficiary_Account_Details inner join tbl_Godown_Owner_Account_Details on tbl_Godown_Owner_Account_Details.Account_No=tbl_Beneficiary_Account_Details.Account_No where tbl_Beneficiary_Account_Details.GO_Approval_Status!='Y' and tbl_Godown_Owner_Account_Details.Godown_Id='" + Godown_Id + "'";
            string str = "select distinct Beneficiary_Id,Beneficiary_Name,tbl_Beneficiary_Account_Details.Account_No,tbl_Beneficiary_Account_Details.IFSC_Code,PAN,GST,Mobile,Email,Aadhar_No,Adress1+' '+Adress2+' '+Adress_City as Address from tbl_Beneficiary_Account_Details inner join tbl_Godown_Owner_Account_Details on tbl_Godown_Owner_Account_Details.Account_No=tbl_Beneficiary_Account_Details.Account_No where tbl_Beneficiary_Account_Details.GO_Approval_Status!='Y' and tbl_Beneficiary_Account_Details.RO_Approval_Status!='Y' and tbl_Beneficiary_Account_Details.Account_No in (select Account_No from tbl_Godown_Owner_Account_Details as AC inner join tbl_MetaData_GODOWN_2018 as G on G.Godown_ID=AC.Godown_Id where G.Hired_Type='Hired') and tbl_Beneficiary_Account_Details.District_Id in (select District_Id from tbl_MetaData_DISTRICT where Region_ID='" + Session["Region_ID"].ToString() + "')";

            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDSCUserVer.DataSource = ds.Tables[0];
                gvDSCUserVer.DataBind();
            }
            else
            {
                gvDSCUserVer.DataSource = "";
                gvDSCUserVer.DataBind();
            }

        }

        catch (Exception ex)
        {

        }
    }
    protected void gvDSCUserVer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string DSC_Id = "";
        string Ser_Id = "";
        if (e.CommandName == "Approve")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvDSCUserVer.Rows[rowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                DSC_Id = gvDSCUserVer.Rows[rowIndex].Cells[1].Text.ToString();
                Ser_Id = gvDSCUserVer.Rows[rowIndex].Cells[3].Text.ToString();
                //User_Ver("Approve", DSC_Id, Ser_Id);
                User_Ver("Y", DSC_Id, Ser_Id);
            }

        }
        else if (e.CommandName == "Reject")
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvDSCUserVer.Rows[rowIndex];

            if (row.RowType == DataControlRowType.DataRow)
            {
                DSC_Id = gvDSCUserVer.Rows[rowIndex].Cells[1].Text.ToString();
                Ser_Id = gvDSCUserVer.Rows[rowIndex].Cells[6].Text.ToString();
                //User_Ver("Reject", DSC_Id, Ser_Id);
                User_Ver("N", DSC_Id, Ser_Id);
            }
        }
    }
}
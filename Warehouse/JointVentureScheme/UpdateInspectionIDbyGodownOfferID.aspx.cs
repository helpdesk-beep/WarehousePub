using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class JointVentureScheme_UpdateInspectionIDbyGodownOfferID : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltran;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            FiiDistrict();
            //lbluser.Text = SessRegion;
        }
        //string SessRegion = Session["UserName"].ToString();
        //string SessRegionid = Session["UserId"].ToString();
        //if (SessRegion != "" && SessRegionid != "")
        //if (Session["UserName"] != null && Session["UserId"] != null)
        //{
        //    if (!string.IsNullOrEmpty(Session["UserName"].ToString()) && !string.IsNullOrEmpty(Session["UserId"].ToString()))
        //    {
        //        if (!IsPostBack)
        //        {
        //            //lbluser.Text = SessRegion;
        //        }
        //    }
        //    else
        //    {
        //        Response.Redirect("Logins.aspx");
        //    }
        //}
        //else
        //{
        //    Response.Redirect("Logins.aspx");
        //}
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddldistrict.SelectedValue != "--Select--" && ddlbranch.SelectedValue != "--Select--" && ddlWarName.SelectedValue != "--Select--" && ddlgodown.SelectedValue != "--Select--")
        {
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Update_Inspection_For_Agreement_Kharif_2022_23", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@RegistrationID", ddlWarName.SelectedValue);
                cmd.Parameters.AddWithValue("@GodownID", ddlgodown.SelectedValue);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = TheResult;
                    //fillgrid();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    //Clear();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter/Select Registration ID/Season .....')", true);
        }
    }
    //public void Delete(string InspectionID)
    //{
    //    string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    //    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
    //    using (SqlConnection constr = new SqlConnection(CS))
    //    {
    //        SqlCommand cmd = new SqlCommand("Delete_JVS_Godown_Inspection", constr);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        constr.Open();
    //        cmd.Parameters.AddWithValue("@InspectionID", InspectionID);
    //        cmd.Parameters.AddWithValue("@Season", ddl_session.SelectedValue);
    //        cmd.Parameters.AddWithValue("@DeletedBy", IPAddress);
    //        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
    //        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //        cmd.ExecuteNonQuery();
    //        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

    //        if (TheResult.StartsWith("SUCCESS"))
    //        {
    //            string strMsg = TheResult;
    //            //fillgrid();
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
    //            //Clear();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + TheResult + "');", true);
    //        }
    //    }
    //}
    public void FiiDistrict()
    {
        string qry = qry = "select District_Id,District_Name from tbl_MetaData_DISTRICT order by District_Name";


        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddldistrict.DataSource = ds.Tables[0];
            ddldistrict.DataTextField = "District_Name";
            ddldistrict.DataValueField = "District_Id";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, "--Select--");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
        }
    }
    public void FiiBranch()
    {
        string qry = qry = "select BranchId,DepotName from tbl_MetaData_DEPOT where DistrictId='" + ddldistrict.SelectedValue + "' ";


        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "BranchId";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, "--Select--");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
        }
    }
    public void gerreg()
    {
        string qry = qry = "select distinct Warehouse_Name,Registration_Id from (select WR.Registration_Id,Warehouse_Name,Auth_Person,PR.MobileNo,convert(Decimal(18,2),RegCapacity) as RegCapacity, convert(Decimal(18,2),Offer_Capacity) as Offer_Capacity,CONVERT(varchar(10),CO.CreatedDate,103) as OfferedDate,  convert(Decimal(18,0),RegAmt) as RegAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='REGISTRATION FEE' and REGISTRATIONID=wr.Registration_Id) as RegDepositeFee ,convert(Decimal(18,0),CO.OfferAmt) as OfferAmt,(select isnull(convert(Decimal(18,0),SUM(fee)),0) from tbl_Payment_Status where CategoryName='OFFER FEES' and REGISTRATIONID=wr.Registration_Id and TransactionDate > convert(varchar(10),'02/19/2022',101)) as  OfferDepositeFee from tbl_WarehouseRegistration as WR  inner join tbl_Warehouse_Capacity_Offer_Kharif2022 as CO on CO.Registration_Id=WR.Registration_Id inner Join tbl_Warehouse_PreReg as PR on PR.Reg_No=WR.Registration_Id where  (CO.OfferSeason='KRF2022_23' or CO.OfferSeason='KRF2022_23' ) and  WR.BranchId='" + ddlbranch.SelectedValue + "' ) as FinalOfferList   where (RegAmt+OfferAmt) <= (OfferDepositeFee+RegDepositeFee) order by Warehouse_Name ";


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
            ddlWarName.Items.Insert(0, "--Select--");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
        }
    }
    public void GetOfferGodown()
    {
        if (ddlWarName.SelectedItem.Text != "--Select--")
        {
            qry = "select Godown_Offer_Id,Godown_No from tbl_Warehouse_Godown_Offer_Kharif2022 where Registration_Id='" + ddlWarName.SelectedValue.ToString() + "' and Godown_Offer_Id not in (select INSP.Godown_Offer_Id from tbl_Godown_Agreement as INSP where INSP.Registration_Id ='" + ddlWarName.SelectedValue.ToString() + "')";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodown.DataSource = ds.Tables[0];
                ddlgodown.DataTextField = "Godown_No";
                ddlgodown.DataValueField = "Godown_Offer_Id";
                ddlgodown.DataBind();
                ddlgodown.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlgodown.Items.Clear();
                ddlgodown.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Another Godown Found')", true);
               // gerreg();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Warehouse Name First ')", true);
        }
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        gerreg();
    }
    protected void ddlWarName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOfferGodown();
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FiiBranch();
    }
}
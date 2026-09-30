using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Text;


public partial class Region_Total_Sulk_Opening : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Region_ID"].ToString() != null)
        {
            if (!IsPostBack)
            {
                GetDepositor();
                GetDistrict();
                Getlambitrashi();
             //   txtdate.Text = DateTime.Now.Date.ToShortDateString();
            }
        }
        else
        {

            Response.Redirect("Logout.aspx");

        }
    }

    private void GetDepositor()
    {
        try
        {
           string qry = "SELECT  [Did],[DepositorName] FROM [DepositorsTbl] order by Did";
            cmd = new SqlCommand(qry, con);
           IDataAdapter da = new SqlDataAdapter(cmd);
           DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldeposiotr.DataSource = ds.Tables[0];
                ddldeposiotr.DataTextField = "DepositorName";
                ddldeposiotr.DataValueField = "Did";
                ddldeposiotr.DataBind();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
        }
    }
    private void GetDistrict()
    {
        try
        {
            string qry = "SELECT  [District_Id],[District_Name],[District_Name_HI] ,[Region_ID],[Regionnm] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_DISTRICT] where Region_ID='" + Session["Region_ID"].ToString() + "'";
            cmd = new SqlCommand(qry, con);
            IDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldist.DataSource = ds.Tables[0];
                ddldist.DataTextField = "District_Name";
                ddldist.DataValueField = "District_Id";
                ddldist.DataBind();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
        }
    }
    private void Getlambitrashi()
    {
        try
        {
            string qry = "SELECT isnull(sum([OldRemaining])-sum([PraptRashi]),0) as lambitrashi FROM [Intergrated_MP_STORAGE].[dbo].[BhandaranRashiDtl] where DistrictID='"+ddldist.SelectedValue.ToString()+"' and DepositorID='"+ddldeposiotr.SelectedValue.ToString()+"'";
            cmd = new SqlCommand(qry, con);
            IDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbllambitrashi.Text = ds.Tables[0].Rows[0]["lambitrashi"].ToString();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.Message.ToString();
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtdate.Text != "")
        {
            string datestring = txtdate.Text;
            string[] tempsplit = datestring.Split('/');
            string joinstring = "/";
            string newdatefrom = tempsplit[2] + joinstring + tempsplit[1] + joinstring + tempsplit[0];
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string sql4 = "insert into [BhandaranRashiDtl] ([BDate],[BranchID],[DistrictID],[DepositorID],[OldRemaining],[PrastutRashi] ,[PraptRashi],[Remark],[Status],[RegionID],[CreatedBy] ,[CreatedDate],[Rokigairashi]) values(@BDate,@BranchID,@DistrictID,@DepositorID,@OldRemaining,@PrastutRashi ,@PraptRashi,@Remark,@Status,@RegionID,@CreatedBy ,@CreatedDate,@Rokigairashi)";

            SqlCommand cmd4 = new SqlCommand(sql4, con);
            SqlParameter[] prms4 = new SqlParameter[13];


            prms4[0] = new SqlParameter("@BDate", SqlDbType.DateTime);
            prms4[0].Value = newdatefrom;
            prms4[1] = new SqlParameter("@BranchID", SqlDbType.VarChar, 20);
            prms4[1].Value = "NA";
            prms4[2] = new SqlParameter("@DistrictID", SqlDbType.VarChar, 10);
            prms4[2].Value = ddldist.SelectedValue.ToString();
            prms4[3] = new SqlParameter("@DepositorID", SqlDbType.VarChar, 10);
            prms4[3].Value = ddldeposiotr.SelectedValue.ToString();
            prms4[4] = new SqlParameter("@OldRemaining", SqlDbType.Decimal);
            prms4[4].Value = txtoldlambitrashi.Text;
            prms4[5] = new SqlParameter("@PrastutRashi", SqlDbType.Decimal);
            prms4[5].Value = txtprastutrashi.Text;
            prms4[6] = new SqlParameter("@PraptRashi", SqlDbType.Decimal);
            prms4[6].Value = txtpraptrashi.Text;
            prms4[7] = new SqlParameter("@Remark", SqlDbType.NVarChar, 100);
            prms4[7].Value = txtremark.Text;
            prms4[8] = new SqlParameter("@Status", SqlDbType.VarChar, 2);
            prms4[8].Value = "O";
            prms4[9] = new SqlParameter("@RegionID", SqlDbType.VarChar, 5);
            prms4[9].Value = Session["Region_ID"].ToString();
            prms4[10] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20);
            prms4[10].Value = ip.ToString();
            prms4[11] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
            prms4[11].Value = DateTime.Now;
            prms4[12] = new SqlParameter("@Rokigairashi", SqlDbType.NVarChar, 250);
            prms4[12].Value = txtrokigairashi.Text;
            int CT4 = 0;
            cmd4.Parameters.AddRange(prms4);
            con.Open();
            CT4 = cmd4.ExecuteNonQuery();
            con.Close();
            if (CT4 > 0)
            {
                lblmsg.Text = "Record Inserted";
                Getlambitrashi();
                txtprastutrashi.Text = "0";
                txtpraptrashi.Text = "0";
                txtoldlambitrashi.Text = "0";
            }
        }
        else
        {
            lblmsg.Text = "Please Select Date";
        }
    }
    protected void ddldeposiotr_SelectedIndexChanged(object sender, EventArgs e)
    {
        Getlambitrashi();
    }
    protected void ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        Getlambitrashi();
    }
}
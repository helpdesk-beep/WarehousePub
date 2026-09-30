using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inspection_GodownInspection : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    string AID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["login"] != null && Session["UserID"].ToString() != null && Session["Auid"].ToString()!=null)
        {
            if (!IsPostBack)
            {
                GodownList();
               
            }
        }
        else
        {
            Response.Redirect("InspectionLogin.aspx");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("InspectionLogin.aspx");
    }
    protected void GodownList()
    {
        string query = "SELECT  [Godown_ID],[Godown_Name] FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where BranchID='" + Session["UserID"].ToString() + "' and Remarks='Y'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {

            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_id";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");



        }

    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CKGID = "";
        CKGID = ChkGodownAudit();
        if (CKGID == "N")
        {
            string query = "SELECT [Stack_ID],(select Stack_Name from [tbl_MetaData_STACK] where Stack_ID=[tbl_storage_Stacking_Details].Stack_ID) as stackname,DepositorName,(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=[tbl_Storage_Receipt_Details].Commodity_Id) as commname FROM [Intergrated_MP_STORAGE].[dbo].[tbl_storage_Stacking_Details] inner join [tbl_Storage_Receipt_Details] on [tbl_storage_Stacking_Details].StorageReceipt_Id=[tbl_Storage_Receipt_Details].StorageReceipt_Id where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' group by [Stack_ID],[tbl_storage_Stacking_Details].[Branchid],[tbl_Storage_Receipt_Details].Commodity_Id ,DepositorName";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvstackdtl.DataSource = ds.Tables[0];
                GVGAUDIT.Visible = true;
                gvstackdtl.DataBind();
                btnsubmit.Visible = true;

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Not Found...'); </script> ");
                GVGAUDIT.Visible = false;
                btnsubmit.Visible = false;
            }
        }
        else if(CKGID == "Y")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('This Godown has already Audit...'); </script> ");
            GVGAUDIT.Visible = false;
            btnsubmit.Visible = false;
        }
    }
    public string ChkGodownAudit()
    {
        string GF = "N";
        string query = "select GodownId from BranchAuditGodownDtl where AuId='" + Session["Auid"].ToString() + "' and GodownId='" + ddlgodown.SelectedValue.ToString() + "'";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GF = "Y";
        }
        else
        {
            GF = "N";
        }
        return GF;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (gvstackdtl.Rows.Count > 0)
        {
            int j;
            for (j = 0; j < gvstackdtl.Rows.Count; j++)
            {

                int BichanLambai = int.Parse(((TextBox)gvstackdtl.Rows[j].FindControl("txtbichanlambai")).Text.ToString());
                int BichanChodai = int.Parse(((TextBox)gvstackdtl.Rows[j].FindControl("txtbichanchodai")).Text.ToString());
                int Atirict = int.Parse(((TextBox)gvstackdtl.Rows[j].FindControl("txtatirikt")).Text.ToString());
                int LayerKiUchai = int.Parse(((TextBox)gvstackdtl.Rows[j].FindControl("txtleyaruchai")).Text.ToString());
                int Block = int.Parse(((TextBox)gvstackdtl.Rows[j].FindControl("txtblocknum")).Text.ToString());
                int atririktboriupper = int.Parse(((TextBox)gvstackdtl.Rows[j].FindControl("txtatiriktboriupper")).Text.ToString());
                int atiriktborineeche = int.Parse(((TextBox)gvstackdtl.Rows[j].FindControl("txtatiriktboriniche")).Text.ToString());
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                //string sql4 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[BranchAuditGodownDtl] ([AuId],[BranchId],[StackID],[DeposioterName],[Commid],[BichanLambai],[BichanChodai],[Atirict],[LayerKiUchai] ,[Block],[atririktboriupper],[atiriktborineeche]) VALUES(@AuId,@BranchId,@StackID,@DeposioterName,@Commid,@BichanLambai,@BichanChodai,@Atirict,@LayerKiUchai ,@Block,@atririktboriupper,@atiriktborineeche  )";
                string sql4 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[BranchAuditGodownDtl] ([AuId],[BranchId],[StackID],[DeposioterName],[Commid],[BichanLambai],[BichanChodai],[Atirict],[LayerKiUchai] ,[Block],[atririktboriupper],[atiriktborineeche],GodownId,CreatedDate,CreatedBy) VALUES(@AuId,@BranchId,@StackID,@DeposioterName,@Commid,@BichanLambai,@BichanChodai,@Atirict,@LayerKiUchai ,@Block,@atririktboriupper,@atiriktborineeche,@GodownId,@CreatedDate,@CreatedBy)";

                SqlCommand cmd4 = new SqlCommand(sql4, con);
                SqlParameter[] prms4 = new SqlParameter[15];


                prms4[0] = new SqlParameter("@AuId", SqlDbType.VarChar, 20);
                prms4[0].Value = Session["Auid"].ToString();
                //prms4[0].Value = 88;
                prms4[1] = new SqlParameter("@BranchId", SqlDbType.VarChar, 20);
                prms4[1].Value = Session["UserID"].ToString();
                prms4[2] = new SqlParameter("@StackID", SqlDbType.VarChar, 20);
                prms4[2].Value = gvstackdtl.Rows[j].Cells[3].Text.ToString();
                prms4[3] = new SqlParameter("@DeposioterName", SqlDbType.VarChar, 30);
                prms4[3].Value = gvstackdtl.Rows[j].Cells[0].Text.ToString();
                prms4[4] = new SqlParameter("@Commid", SqlDbType.VarChar, 30);
                prms4[4].Value = gvstackdtl.Rows[j].Cells[1].Text.ToString();
                prms4[5] = new SqlParameter("@BichanLambai", SqlDbType.Int);
                prms4[5].Value = BichanLambai;
                prms4[6] = new SqlParameter("@BichanChodai", SqlDbType.Int);
                prms4[6].Value = BichanChodai;
                prms4[7] = new SqlParameter("@Atirict", SqlDbType.Int);
                prms4[7].Value = Atirict;
                prms4[8] = new SqlParameter("@LayerKiUchai", SqlDbType.VarChar, 30);
                prms4[8].Value = LayerKiUchai;
                prms4[9] = new SqlParameter("@Block", SqlDbType.VarChar, 30);
                prms4[9].Value = Block;
                prms4[10] = new SqlParameter("@atririktboriupper", SqlDbType.VarChar, 30);
                prms4[10].Value = atririktboriupper;
                prms4[11] = new SqlParameter("@atiriktborineeche", SqlDbType.VarChar, 30);
                prms4[11].Value = atiriktborineeche;
                prms4[12] = new SqlParameter("@GodownId", SqlDbType.VarChar, 20);
                prms4[12].Value = ddlgodown.SelectedValue.ToString();
                 prms4[13] = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
                 prms4[13].Value = DateTime.Now;
                prms4[14] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20);
                prms4[14].Value = ip;
                int CT4 = 0;
                cmd4.Parameters.AddRange(prms4);
                con.Open();
                CT4 = cmd4.ExecuteNonQuery();
                con.Close();
                if (CT4 > 0)
                {

                }
            }

        }
        Session["Auid"] = Session["Auid"].ToString();
        Session["GodownId"] = ddlgodown.SelectedValue.ToString();
        Response.Redirect("GodownInspecPrint.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("OldBranchInsp.aspx");
    }
}

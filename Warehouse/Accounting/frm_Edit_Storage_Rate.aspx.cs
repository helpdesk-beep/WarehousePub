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

public partial class HOWLC_frm_Edit_Storage_Rate: System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDepositioType();
            GetPackingType();
            GetVerity();
            GetWeight();
        }
    }
    void GetVerity()
    {
        string query = "select verity_code,verity_Eng from dbo.tbl_MetaData_Verity";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlverity.DataSource = ds.Tables[0];
            ddlverity.DataTextField = "verity_Eng";
            ddlverity.DataValueField = "verity_code";
            ddlverity.DataBind();
            ddlverity.Items.Insert(0, "--Select--");
        }
    }
    void GetDepositioType()
    {
        string query = "select Depositor_Type,Depositor_Type_Id from dbo.tbl_MetaData_Depositor_Type";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddldepositor.DataSource = ds.Tables[0];
            ddldepositor.DataTextField = "Depositor_Type";
            ddldepositor.DataValueField = "Depositor_Type_Id";
            ddldepositor.DataBind();
            ddldepositor.Items.Insert(0, "--Se|ect--");
        }
    }
    void GetCommodity()
    {
        string verty = ddlverity.SelectedValue;
        string query = "select Commodity_ID,Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY_RList where Rep_Grp_Code='" + verty + "'";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcomodity.DataSource = ds.Tables[0];
            ddlcomodity.DataTextField = "Commodity_Name";
            ddlcomodity.DataValueField = "Commodity_ID";
            ddlcomodity.DataBind();
            ddlcomodity.Items.Insert(0, "--Se|ect--");
        }
    }
    void GetPackingType()
    {
        string query = "select Packing_Id,Packing_Name + '('+rtrim(Remarks)+')' as Packing_Name from dbo.Packing_type ";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlpacktype.DataSource = ds.Tables[0];
            ddlpacktype.DataTextField = "Packing_Name";
            ddlpacktype.DataValueField = "Packing_Id";
            ddlpacktype.DataBind();
            ddlpacktype.Items.Insert(0, "--Select--");
        }
    }
    void GetWeight()
    {
        string query = "select Weigt_ID,Weight_Type from dbo.tbl_MetaData_WeightType";
        SqlCommand cmd = new SqlCommand(query, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlweight.DataSource = ds.Tables[0];
            ddlweight.DataTextField = "Weight_Type";
            ddlweight.DataValueField = "Weigt_ID";
            ddlweight.DataBind();
            ddlweight.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlverity_SelectedIndexChanged(object sender, EventArgs e)
    {
         GetCommodity();
         fillgrid();
         ddlpacktype.SelectedValue = null;
         ddlweight.SelectedValue = null;
         txtrate.Text = "";

         fromdate.Text = "";
         ddlcomodity.SelectedValue = null;

         txtremark.Text = "";
    }
    void fillgrid()
    {
        try
        {
            string qry = "select MDER.Id,MSCR.Commodity_Name,PT.Packing_Name,MDWT.Weight_Type,convert(varchar(10),MDER.Rate_Effective_Date,103) as EDates,MDER.Rate,MDER.Commodity_Id,MDER.Packing_Id,MDER.Weight_ID,MDER.remark from tbl_MetaData_STORAGE_COMMODITY_RList as MSCR join tbl_MetaDataEffectiveRateDetail as MDER on (MDER.Commodity_Id=MSCR.Commodity_Id) join tbl_MetaData_WeightType as MDWT on (MDWT.Weigt_ID=MDER.Weight_ID) join Packing_type as PT on PT.Packing_Id=MDER.Packing_Id where MDER.Commodity_Type='" + ddlverity.SelectedValue.ToString() + "' and MDER.Depositor_Type='" + ddldepositor.SelectedValue.ToString() + "' order by MSCR.Commodity_Name";
            SqlCommand cmd = new SqlCommand(qry, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();

               
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }   
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    public static DateTime FixDateTime(object valueToFix)
    {
        return FixDate(valueToFix);
    }
    public static DateTime FixDate(object valueToFix)
    {
        if (valueToFix == null)
            return new DateTime(1899, 1, 1);
        else if (Convert.IsDBNull(valueToFix))
            return new DateTime(1899, 1, 1);
        else
        {
            try
            {
                return Convert.ToDateTime(valueToFix);
            }
            catch
            {
                return new DateTime(1899, 1, 1);
            }
        }
    }
    decimal CheckNull(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        decimal ValF = decimal.Parse(ValS);
        return ValF;

    }
    Int32 CheckNullInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        int ValF = int.Parse(ValS);
        return ValF;

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (ddlverity.SelectedItem.Text == "--Select--" || ddlpacktype.SelectedItem.Text == "--Select--" || ddlweight.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "msg1", "<script language=javascript>alert('Please Select Verity/Commodity/Packing Type/Weight');</script>");
        }
        else
        {
            string vcode = ddlverity.SelectedValue;
            string comdty = ddlcomodity.SelectedValue;
            string packtype = ddlpacktype.SelectedValue;
            string weight = ddlweight.SelectedValue;
            string mfromdate = getDate_MDY(fromdate.Text);
            decimal rate = CheckNull(txtrate.Text);
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string st = "A";
            string tid = "";
            string mremarks = txtremark.Text;
            //string qrylogins = "insert into dbo.tbl_MetaDataEffectiveRateDetail_log select [Depositor_Type],[Rate_Effective_Date],[Commodity_Type],[Commodity_Id],[Rate],[Created_date],[Client_IP],Packing_Id,Weight_ID,remark,getdate(),'" + ip + "' from tbl_MetaDataEffectiveRateDetail where Depositor_Type='" + ddldepositor.SelectedValue.ToString() + "' and Commodity_Type='" + ddlverity.SelectedValue.ToString() + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Rate_Effective_Date='" + mfromdate + "'";
            string qrylogins = "insert into dbo.tbl_MetaDataEffectiveRateDetail_log select Id,[Depositor_Type],[Rate_Effective_Date],[Commodity_Type],[Commodity_Id],[Rate],[Created_date],[Client_IP],Packing_Id,Weight_ID,remark,getdate(),'" + ip + "',[Deleted_By],[Deleted_Date] from tbl_MetaDataEffectiveRateDetail where Depositor_Type='" + ddldepositor.SelectedValue.ToString() + "' and Commodity_Type='" + ddlverity.SelectedValue.ToString() + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Id='" + ViewState["SID"].ToString() + "'";
            cmd.CommandText = qrylogins;
            cmd.Connection = Con;
            try
            {
                Con.Open();
                cmd.ExecuteNonQuery();
                Con.Close();

                string updateRate = "Update dbo.tbl_MetaDataEffectiveRateDetail set Packing_Id='" + packtype + "',Weight_ID='" + weight + "',Rate_Effective_Date='" + mfromdate + "',Rate='" + rate + "',Modified_date=getdate(),Modified_IP='" + ip + "',remark='" + mremarks + "' where Depositor_Type='" + ddldepositor.SelectedValue.ToString() + "' and Commodity_Type='" + ddlverity.SelectedValue.ToString() + "' and Commodity_Id='" + ddlcomodity.SelectedValue.ToString() + "' and Id='" + ViewState["SID"].ToString() + "'";
                cmd.CommandText = updateRate;
                cmd.Connection = Con;
                try
                {
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    ViewState["SID"] = null;
                    btnsave.Enabled = false;
                    fillgrid();

                    ddlpacktype.SelectedValue = null;
                    ddlweight.SelectedValue = null;
                    txtrate.Text = "";

                    fromdate.Text = "";
                    ddlcomodity.SelectedValue = null;

                    txtremark.Text = "";
                   
                }
                catch (Exception Ex)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = Ex.Message;
                }
                finally
                {
                    Con.Close();
                    ViewState["SID"] = null;
                }
               
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Updated Successfully'); </script> ");
            }
            catch (Exception Ex)
            {
                lblmsg.Visible = true;
                lblmsg.Text = Ex.Message;
            }
            finally
            {
                Con.Close();
                ViewState["SID"] = null;

            }
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenterLevel/Storage/Report_Region.aspx");

    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/frm_Edit_Storage_Rate.aspx");
    }
    public string getdate(string DDDate)
    {
        return Convert.ToDateTime(DDDate).ToString("dd/MM/yyyy");
    }  
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
        string getedate = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string griddate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "EDates"));
            string gdate1 = getDate_MDY(griddate);
            getedate = getdate(gdate1);
            Label lble = (Label)e.Row.FindControl("lbldate");
            lble.Text = getedate;
        }
        }
        catch(Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text=ex.Message;
        }
    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            ddlpacktype.SelectedValue = GridView1.SelectedRow.Cells[8].Text;
            ddlweight.SelectedValue = GridView1.SelectedRow.Cells[9].Text;
            txtrate.Text = GridView1.SelectedRow.Cells[6].Text;
            Label lbldt;
            lbldt = (Label)(GridView1.SelectedRow.Cells[5].FindControl("lbldate"));
            fromdate.Text = lbldt.Text;
            ddlcomodity.SelectedValue = GridView1.SelectedRow.Cells[7].Text;
            lbldt = (Label)(GridView1.SelectedRow.Cells[10].FindControl("lblrmk"));
            txtremark.Text = lbldt.Text.Trim();
            btnsave.Enabled = true;
            ///get id////
            string SId=GridView1.SelectedRow.Cells[11].Text;
            ViewState["SID"] = SId.ToString();

        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int _rowindex = e.RowIndex;
            string Rid = GridView1.DataKeys[_rowindex].Value.ToString();          
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //string log_qry = "insert into tbl_MetaDataEffectiveRateDetail_log select * from tbl_MetaDataEffectiveRateDetail where Id='" + Rid + "'";   
            string log_qry = "insert into tbl_MetaDataEffectiveRateDetail_log select [Id],[Depositor_Type],[Rate_Effective_Date],[Commodity_Type],[Commodity_Id],[Rate],[Created_date],[Client_IP],[Packing_Id],[Weight_ID],[Remark],[Modified_date],[Modified_IP],'" + ip + "',getdate() from tbl_MetaDataEffectiveRateDetail where Id='" + Rid + "'";                    
     
                  cmd = new SqlCommand(log_qry, Con);
                  Con.Open();
                  int s = cmd.ExecuteNonQuery();
                  Con.Close();                  
                  if (s > 0)
                  {
                   string qry = "Delete from tbl_MetaDataEffectiveRateDetail where Id='" + Rid + "'";
                   cmd = new SqlCommand(qry, Con);
                   Con.Open();
                   int d = cmd.ExecuteNonQuery();
                   Con.Close();  
                   if (d > 0)
                   {
                       fillgrid();
                       ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Record Deleted Successfully...'); </script> ");
                   }
                  }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...'); </script> ");
        }
        finally
        {
            Con.Close();
        }
    }
}

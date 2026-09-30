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

public partial class HOWLC_frmStorage_Rate : System.Web.UI.Page
{
    DataTable Dt1 = new DataTable();
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    public string qry = "";
    DataSet ds = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDepositioType();
            GetPackingType();
            GetVerity();
            GetWeight();
            Session["dt1"] = null;
        }
    }
    void GetDepositioType()
    {
        string qry = "select Depositor_Type,Depositor_Type_Id from dbo.tbl_MetaData_Depositor_Type";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
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
    void GetVerity()
    {
        string qry = "select verity_code,verity_Eng from dbo.tbl_MetaData_Verity";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
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
    void GetPackingType()
    {
        string qry = "select Packing_Id,Packing_Name + '('+rtrim(Remarks)+')' as Packing_Name from dbo.Packing_type ";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
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
        string qry = "select Weigt_ID,Weight_Type from dbo.tbl_MetaData_WeightType";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
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
    void GetCommodityChk()
    {
        CheckBoxList1.Items.Clear();
        string verty = ddlverity.SelectedValue;
        string query = "select Commodity_ID,Commodity_Name from tbl_MetaData_STORAGE_COMMODITY_RList where Rep_Grp_Code='" + verty + "' order by Commodity_Name";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Connection = con;
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            ListItem lst = new ListItem();
            lst.Text = dr["Commodity_Name"].ToString();
            lst.Value = dr["Commodity_ID"].ToString();
            CheckBoxList1.Items.Add(lst);
        }
    }
    protected void ddlverity_SelectedIndexChanged(object sender, EventArgs e)
    {
         GetCommodityChk();
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
        if (ddlverity.SelectedItem.Text == "--Select--" || ddlpacktype.SelectedItem.Text == "--Select--" || ddlweight.SelectedItem.Text == "--Select--" || CheckBoxList1.Items.Count < 1)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Verity/Commodity/Packing Type/Weight');", true);
        }
        else
        {
            string fdate = getDate_MDY(fromdate.Text);
            string vdate = getDate_MDY(revdate.Text);

            if (fdate == "" || vdate == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Date.. ');", true);
            }
            else
            {
                string vcode = ddlverity.SelectedValue;
                string comdty = "";
                string packtype = ddlpacktype.SelectedValue;
                string weight = ddlweight.SelectedValue;
                string mfromdate = fdate;
                decimal rate = CheckNull(txtrate.Text);
                string mrevdate = vdate;
                decimal revrate = CheckNull(txtrevrate.Text);
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string mremarks = txtremark.Text;
                string temp = "NNN";
                string DupChk = "NNN";
                string createddate = getDate_MDY(DateTime.Now.ToString("dd/MM/yyyy"));
                try
                {
                    for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                    {
                        if (CheckBoxList1.Items[i].Selected == true)
                        {
                            comdty = CheckBoxList1.Items[i].Value;
                            string chkdup = "Select * from dbo.tbl_MetaDataEffectiveRateDetail where [Depositor_Type]='" + ddldepositor.SelectedValue.ToString() + "' and [Rate_Effective_Date]='" + mfromdate + "' and [Commodity_Type]='" + vcode + "'and [Commodity_Id]='" + comdty + "'and [Rate]='" + rate + "' and [Packing_Id]='" + packtype + "' and [Weight_ID]='" + weight + "'";
                            da = new SqlDataAdapter(chkdup, con);
                            Dt1 = new DataTable();
                            da.Fill(Dt1);
                            if (Dt1.Rows.Count == 0 && fromdate.Text != "" && txtrate.Text != "")
                            {
                                string qry2 = "insert into dbo.tbl_MetaDataEffectiveRateDetail([Depositor_Type],[Rate_Effective_Date],[Commodity_Type],[Commodity_Id],[Rate],[Created_date],[Client_IP],Packing_Id,Weight_ID,remark)values('" + ddldepositor.SelectedValue.ToString() + "','" + mfromdate + "'," + vcode + "," + comdty + "," + rate + ",'" + createddate + "','" + ip + "','" + packtype + "','" + weight + "','" + mremarks + "')";
                                cmd.CommandText = qry2;
                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                temp = "YYY";
                            }
                            else
                            {
                                DupChk = "YYY";
                            }

                            string RDate = "";
                            decimal Revised_Rate = 0;
                            if (gvRevisedDate.Rows.Count > 0)
                            {
                                for (int s = 0; s < gvRevisedDate.Rows.Count; s++)
                                {
                                    RDate = gvRevisedDate.Rows[s].Cells[0].Text.ToString();
                                    Revised_Rate = decimal.Parse(gvRevisedDate.Rows[s].Cells[1].Text.ToString());
                                    string Revised_Dates = getDate_MDY(RDate).ToString();

                                    chkdup = "Select * from dbo.tbl_MetaDataEffectiveRateDetail where [Depositor_Type]='" + ddldepositor.SelectedValue.ToString() + "' and [Rate_Effective_Date]='" + Revised_Dates + "' and [Commodity_Type]='" + vcode + "'and [Commodity_Id]='" + comdty + "'and [Rate]='" + Revised_Rate + "' and [Packing_Id]='" + packtype + "' and [Weight_ID]='" + weight + "'";
                                    da = new SqlDataAdapter(chkdup, con);
                                    Dt1 = new DataTable();
                                    da.Fill(Dt1);
                                    if (Dt1.Rows.Count == 0)
                                    {
                                        string qryDate = "insert into dbo.tbl_MetaDataEffectiveRateDetail([Depositor_Type],[Rate_Effective_Date],[Commodity_Type],[Commodity_Id],[Rate],[Created_date],[Client_IP],Packing_Id,Weight_ID,remark)values('" + ddldepositor.SelectedValue.ToString() + "','" + Revised_Dates + "'," + vcode + "," + comdty + "," + Revised_Rate + ",'" + createddate + "','" + ip + "','" + packtype + "','" + weight + "','" + mremarks + "')";
                                        cmd.CommandText = qryDate;
                                        cmd.Connection = con;
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        temp = "YYY";
                                    }
                                    else
                                    {
                                        DupChk = "YYY";
                                    }
                                }
                            }
                            else if (revdate.Text != "" && txtrevrate.Text != "")
                            {
                                chkdup = "Select * from dbo.tbl_MetaDataEffectiveRateDetail where [Depositor_Type]='" + ddldepositor.SelectedValue.ToString() + "' and [Rate_Effective_Date]='" + mrevdate + "' and [Commodity_Type]='" + vcode + "'and [Commodity_Id]='" + comdty + "'and [Rate]='" + revrate + "' and [Packing_Id]='" + packtype + "' and [Weight_ID]='" + weight + "'";
                                da = new SqlDataAdapter(chkdup, con);
                                Dt1 = new DataTable();
                                da.Fill(Dt1);
                                if (Dt1.Rows.Count == 0)
                                {
                                    string qry7 = "insert into dbo.tbl_MetaDataEffectiveRateDetail([Depositor_Type],[Rate_Effective_Date],[Commodity_Type],[Commodity_Id],[Rate],[Created_date],[Client_IP],Packing_Id,Weight_ID,remark)values('" + ddldepositor.SelectedValue.ToString() + "','" + mrevdate + "'," + vcode + "," + comdty + "," + revrate + ",'" + createddate + "','" + ip + "','" + packtype + "','" + weight + "','" + mremarks + "')";
                                    cmd.CommandText = qry7;
                                    cmd.Connection = con;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    temp = "YYY";
                                }
                                else
                                {
                                    DupChk = "YYY";
                                }
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = Ex.Message;
                }
                    finally
                    {
                        con.Close();
                    }              

                if (temp == "YYY")
                {
                    btnsave.Enabled = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Saved Successfully');", true);
                }
                else if (DupChk == "YYY")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Duplicate Entry Does Not Allowed'); </script> ");
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Commodity or Date & Rate'); </script> ");
                }
            }
        }
    }  
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IssueCenterLevel/Storage/Report_Region.aspx");
    }
    protected void btnnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Accounting/frmStorage_Rate.aspx");
    }
    protected void addRevisedDate_Click(object sender, EventArgs e)
    {
        try
        {
            bool checkstatus = false;

            if (revdate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No of Bags to be added in Stack is required!')", true);
                return;
            }
            else if (txtrevrate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Bags Weight to be added in Stack is required!')", true);
                return;
            }
            if (Session["dt1"] == null)
            {
                Dt1 = CreateTable();
                Session["dt1"] = Dt1;
            }
            // adding rows to the datatable
            DataRow dr = ((DataTable)Session["dt1"]).NewRow();
            ((DataTable)Session["dt1"]).AcceptChanges();
            int s = 1;
            dr["Id"] = s;
            dr["RevisedDate"] = revdate.Text.Trim();
            dr["Rate"] = txtrevrate.Text.Trim();
            if (gvRevisedDate.Rows.Count > 0)
            {
                if (checkstatus == false)
                {
                    ((DataTable)Session["dt1"]).Rows.Add(dr);
                    ((DataTable)Session["dt1"]).AcceptChanges();
                    gvRevisedDate.DataSource = (DataTable)Session["dt1"];
                    gvRevisedDate.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entry for this stack is already done')", true);
                }
            }
            else
            {
                ((DataTable)Session["dt1"]).Rows.Add(dr);
                ((DataTable)Session["dt1"]).AcceptChanges();
                gvRevisedDate.DataSource = (DataTable)Session["dt1"];
                gvRevisedDate.DataBind();
            }
        }
        catch(Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }
  public DataTable CreateTable()
    { 
        DataTable dt = new DataTable();//DataTable is created
        DataColumn Id = new DataColumn("Id", Type.GetType("System.String"));
        DataColumn RevisedDate = new DataColumn("RevisedDate", Type.GetType("System.String"));
        DataColumn Rate = new DataColumn("Rate", Type.GetType("System.Decimal"));
        dt.Columns.Add(Id);//Column is added to the DataTable
        dt.Columns.Add(RevisedDate);//Column is added to the DataTable
        dt.Columns.Add(Rate);//Column is added to the DataTable
        dt.AcceptChanges();
        return dt;
    }
    protected void BindGrid()
    {
        gvRevisedDate.DataSource = ViewState["dt"] as DataTable;
        gvRevisedDate.DataBind();
    }
    protected void gvRevisedDate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = e.RowIndex;
            if (gvRevisedDate.Rows.Count < 1)
            {
                ViewState["ckstat"] = "Delete";
            }
            ((DataTable)Session["dt1"]).Rows[i].Delete();
            ((DataTable)Session["dt1"]).AcceptChanges();

            gvRevisedDate.DataSource = (DataTable)Session["dt1"];
            gvRevisedDate.DataBind();
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
        }
    }
}

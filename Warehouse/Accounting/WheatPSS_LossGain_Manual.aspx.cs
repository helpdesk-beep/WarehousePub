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

public partial class Accounting_WheatPSS_LossGain_Manual : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    SqlDataAdapter da = new SqlDataAdapter();
    string qry = "";
    string Branch_Id = "";
    string District_Id = "";
    string Client_Ip = "";
    string WLG_Id = "";
    DataTable Dt1 = new DataTable();
    string SCropYr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                FillCropYear();
                //FillIssueCropYear();
                GetRegionId();
                Session["dt1"] = null;
            }
            District_Id = Session["Depot_DistID"].ToString();
            Branch_Id = Session["BranchID"].ToString();
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    public void FillCropYear()
    {
        //ddlcropyear.Items.Clear();
        ListItem[] items = new ListItem[7];
        items[0] = new ListItem("--Select--", "0");
        items[1] = new ListItem("2016-17","1");
        items[2] = new ListItem("2015-16", "2");
        items[3] = new ListItem("2014-15", "3");
        items[4] = new ListItem("2013-14", "4");
        items[5] = new ListItem("2012-13", "5");
        items[6] = new ListItem("2011-12", "6");
        ddlcropyear.SelectedIndex = 0;
        ddlcropyear.Items.AddRange(items);
        ddlcropyear.DataBind();
    }
    public void FillIssueCropYear()
    {
        ddlIssuecropyear.Items.Clear();
        string[] CropYr = new string[6] { "2016-17", "2015-16", "2014-15", "2013-14", "2012-13", "2011-12" };
        
        int y = 0;
        int c = Convert.ToInt32(ddlcropyear.SelectedValue);
        ListItem[] items = new ListItem[c];
        for (int i = c; i >= 1; i--)
        {
           
            items[y] = new ListItem(CropYr[i - 1], i.ToString());
           y=y+1;
           if (y == c)
           {
               ddlIssuecropyear.Items.AddRange(items);
               ddlIssuecropyear.DataBind();
           }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
            if (txtClose1.Text.ToString() == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid Closing Weight...'); </script> ");
            }
            else if (txtClose2.Text.ToString() == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid Closing Weight...'); </script> ");
            }
            else if (txtClose3.Text.ToString() == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid Closing Weight...'); </script> ");
            }
            else if (txtClose4.Text.ToString() == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid Closing Weight...'); </script> ");
            }
            else if (ddlcropyear.SelectedItem.Text=="--Select--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Crop Year...'); </script> ");
            }
            else
            {
                try
                {
                    bool checkstatus = false;
                    string GridHead = "वर्ष " + ViewState["SCropYr"].ToString() +" मे भंडारित Wheat-PSS की मासांत पर शेष मात्रा(क्विंटल मे) का वर्षवार(भुगतान वर्षवार) विवरण";
                    Label5.Text = GridHead;
                    if (Session["dt1"] == null)
                    {
                        Dt1 = CreateTable();
                        Session["dt1"] = Dt1;
                    }
                    // adding rows to the datatable
                    DataRow dr = ((DataTable)Session["dt1"]).NewRow();
                    ((DataTable)Session["dt1"]).AcceptChanges();
                    int s = 1;
                    //dr["Id"] = s;
                    dr["Year"] = ddlIssuecropyear.SelectedItem.Text;
                    dr["June"] = txtClose1.Text;
                    dr["July"] = txtClose2.Text;
                    dr["December"] = txtClose3.Text;
                    dr["March"] = txtClose4.Text;

                    if (gvMonthClosing.Rows.Count > 0)
                    {
                        if (checkstatus == false)
                        {
                            ((DataTable)Session["dt1"]).Rows.Add(dr);
                            ((DataTable)Session["dt1"]).AcceptChanges();
                            gvMonthClosing.DataSource = (DataTable)Session["dt1"];
                            gvMonthClosing.DataBind();
                            gvMonthClosing.Visible = true;
                            trGridHead.Visible = true;
                            txtClose1.Text = "0";
                            txtClose2.Text = "0";
                            txtClose3.Text = "0";
                            txtClose4.Text = "0";
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Entry for this is already done')", true);
                        }

                    }
                    else
                    {
                        ((DataTable)Session["dt1"]).Rows.Add(dr);
                        ((DataTable)Session["dt1"]).AcceptChanges();
                        gvMonthClosing.DataSource = (DataTable)Session["dt1"];
                        gvMonthClosing.DataBind();
                        trGridHead.Visible = true;
                        txtClose1.Text = "0";
                        txtClose2.Text = "0";
                        txtClose3.Text = "0";
                        txtClose4.Text = "0";

                    }
                }
                //}
                catch (Exception ex)
                {
                    //lblmsg.Visible = true;
                    //lblmsg.Text = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
    }
    public DataTable CreateTable()
    {
        DataTable dt = new DataTable();//DataTable is created
        //DataColumn Id = new DataColumn("Id", Type.GetType("System.String"));
        DataColumn Issue_CropYear = new DataColumn("Year", Type.GetType("System.String"));
        DataColumn Closing_Balance1 = new DataColumn("June", Type.GetType("System.String"));
        DataColumn Closing_Balance2 = new DataColumn("July", Type.GetType("System.String"));
        DataColumn Closing_Balance3 = new DataColumn("December", Type.GetType("System.String"));
        DataColumn Closing_Balance4 = new DataColumn("March", Type.GetType("System.String"));
        //dt.Columns.Add(Id);//Column is added to the DataTable
        dt.Columns.Add(Issue_CropYear);//Column is added to the DataTable
        dt.Columns.Add(Closing_Balance1);//Column is added to the DataTable
        dt.Columns.Add(Closing_Balance2);//Column is added to the DataTable
        dt.Columns.Add(Closing_Balance3);
        dt.Columns.Add(Closing_Balance4);
       
        dt.AcceptChanges();
        return dt;
    }
    protected void gvMonthClosing_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int i = e.RowIndex;
            if (gvMonthClosing.Rows.Count < 1)
            {
                ViewState["ckstat"] = "Delete";
            }
            ((DataTable)Session["dt1"]).Rows[i].Delete();
            ((DataTable)Session["dt1"]).AcceptChanges();

            gvMonthClosing.DataSource = (DataTable)Session["dt1"];
            gvMonthClosing.DataBind();

            if (gvMonthClosing.Rows.Count < 1)
            {
                trGridHead.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //lblmsg.Visible = true;
            //lblmsg.Text = ex.Message;
        }
    }
    protected void btnPSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtRWeight.Text.ToString() == "0" || txtRWeight.Text.ToString() == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid Deposit Weight...'); </script> ");
            }
            else if (gvMonthClosing.Rows.Count < 1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Add Closing by using ADD button...'); </script> ");
            }
            else if (txtTGain.Text.ToString() == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid Gain Weight...'); </script> ");
            }
            else if (txtTLoss.Text.ToString() == "")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Valid Loss Weight...'); </script> ");
            }
            else
            {
                WLGnomax();
                Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string qrym = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_WheatPSS_GainLoss_Detail] ([WLG_Id],[Region_Id],[District_Id],[Branch_Id],[Depositor_Id],[Commodity_Id],[Storage_Type],[Deposit_CropYear],[Deposit_Weight],[Total_Gain],[Total_Loss],[Created_By],[Created_Date],PID,Remark) VALUES ('" + ViewState["WLG_Id"].ToString() + "','" + ViewState["Region_ID"].ToString() + "','" + District_Id + "','" + Branch_Id + "','" + ddldepos_name.SelectedValue.ToString() + "','" + ddlCommodity.SelectedValue.ToString() + "','" + ddlStrType.SelectedItem.Text + "','" + ddlcropyear.SelectedItem.Text + "','" + txtRWeight.Text + "','" + txtTGain.Text + "','" + txtTLoss.Text + "','" + Client_Ip + "',getdate(),'" + ViewState["PID"].ToString() + "',N'"+ txtResion.Text +"')";
                SqlCommand cmdm = new SqlCommand(qrym, con);
                con.Open();
                int c = cmdm.ExecuteNonQuery();
                con.Close();
                if (c > 0)
                {
                    if (gvMonthClosing.Rows.Count > 0)
                    {
                        for (int i = 0; i < gvMonthClosing.Rows.Count; i++)
                        {
                            string Issue_CropYear = gvMonthClosing.Rows[i].Cells[1].Text.ToString();
                            decimal Closing1 = Convert.ToDecimal(gvMonthClosing.Rows[i].Cells[2].Text.ToString());
                            decimal Closing2 = Convert.ToDecimal(gvMonthClosing.Rows[i].Cells[3].Text.ToString());
                            decimal Closing3 = Convert.ToDecimal(gvMonthClosing.Rows[i].Cells[4].Text.ToString());
                            decimal Closing4 = Convert.ToDecimal(gvMonthClosing.Rows[i].Cells[5].Text.ToString());

                            string qry = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_WheatPSS_YearWise_Issue]([WLG_Id],[Issue_Year],[Closing_Balance1],[Closing_Balance2],[Closing_Balance3],[Closing_Balance4]) values ('" + ViewState["WLG_Id"].ToString() + "','" + Issue_CropYear + "','" + Closing1 + "','" + Closing2 + "','" + Closing3 + "','" + Closing4 + "')";
                            SqlCommand cmd = new SqlCommand(qry, con);
                            con.Open();
                            int d = cmd.ExecuteNonQuery();
                            con.Close();
                            if (d > 0)
                            {

                            }
                        }
                    }
                    Session["dt1"] = null;
                    gvMonthClosing.DataSource = null;
                    gvMonthClosing.DataBind();
                    Empty();
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }
    }
    public void GetRegionId()
    {
        qry = "select Region_ID from tbl_MetaData_DISTRICT where District_Id='" + Session["Depot_DistID"].ToString() + "'";
        da = new SqlDataAdapter(qry, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ViewState["Region_ID"] = dt.Rows[0]["Region_ID"].ToString();
        }
        else
        {
            //ViewState["Region_ID"] = dt.Rows[0]["Region_ID"].ToString();
        }
    }
    public void WLGnomax()
    {
        try
        {
            if (Branch_Id != "" && Branch_Id != null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string QueryMax = "select isnull(Max(PID),0)+1 from tbl_WheatPSS_GainLoss_Detail where Branch_Id='" + Branch_Id + "' ";
                cmd = new SqlCommand(QueryMax, con); // check
                string str3 = cmd.ExecuteScalar().ToString();
                if ((str3 == String.Empty) || str3 == "")
                {
                    str3 = "0";
                }
                if (Convert.ToInt64(str3) != 0)
                {
                    WLG_Id = Branch_Id + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
                }
                else
                {
                    WLG_Id = Branch_Id + System.DateTime.Now.Date.ToString("yy") + Convert.ToString(Convert.ToInt64(str3));
                }
                ViewState["WLG_Id"] = WLG_Id.ToString();
                ViewState["PID"] = str3.ToString();
            }
            else
            {           
                return;
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            con.Close();
        }
    }
    public void Empty()
    {
        txtRWeight.Text = "0";
        txtClose1.Text = "0";
        txtClose2.Text = "0";
        txtClose3.Text = "0";
        txtClose4.Text = "0";
        txtTGain.Text = "0";
        txtTLoss.Text = "0";
        txtResion.Text = "";
        trGridHead.Visible = false;
        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Inserted Successfully...'); </script> ");

        //FillCropYear();
        //FillIssueCropYear();
    }
    protected void btnPCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void ddlcropyear_SelectedIndexChanged(object sender, EventArgs e)
    {  
        ViewState["SCropYr"]=ddlcropyear.SelectedItem.Text;
        string ClosHead = "वर्ष " + ViewState["SCropYr"].ToString() + " मे भंडारित स्कंध के भुगतान उपरांत मासांत पर शेष स्कंध की मात्रा(क्विंटल मे) का विवरण";
        Label1.Text = ClosHead;
        string LGHead = "वर्ष " + ViewState["SCropYr"].ToString() + " मे कुल भंडारित मात्रा के विरुद्ध परिलक्षित कुल भंडारण आधिक्य(Gain)/कमी(Shortage) की मात्रा(क्विंटल मे) का विवरण";
        Label2.Text = LGHead;
        if (trGridHead.Visible == true)
        {
            string GridHead = "वर्ष " + ViewState["SCropYr"].ToString() + " मे भंडारित Wheat-PSS की मासांत पर शेष मात्रा(क्विंटल मे) का वर्षवार(भुगतान वर्षवार) विवरण";
            Label5.Text = GridHead;
        }
        FillIssueCropYear();
    }
    protected void ddlStrType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["dt1"] = null;
        gvMonthClosing.DataSource = null;
        gvMonthClosing.DataBind();
        txtRWeight.Text = "0";
        txtClose1.Text = "0";
        txtClose2.Text = "0";
        txtClose3.Text = "0";
        txtClose4.Text = "0";
        txtTGain.Text = "0";
        txtTLoss.Text = "0";
        txtResion.Text = "";
        trGridHead.Visible = false;
        //FillCropYear();
        Label5.Text = "";
        Label2.Text = "";
        Label1.Text = "";
    }
}

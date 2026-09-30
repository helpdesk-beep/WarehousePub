using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NCCF_NCCF_Deduction_For_Final_Storage_Bill : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
        }
    }
    public void fillgrid()
    {
        using (SqlConnection con = new SqlConnection(con_WLC1))
        {
            SqlCommand cmd = new SqlCommand("Get_Pass_Amount_For_Deduction_For_NCCF", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Created_By", Session["UserID"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }

            if (dt.Rows.Count > 0)
            {
                GrdBills.DataSource = dt;
                GrdBills.DataBind();
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();
            }
        }
    }
    protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ErrorMsg = "";
        lblmsg.Text = "";
        try
        {
            if (e.CommandName == "Submit")
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                Label RowNumber = (Label)row.FindControl("lblRowNumber");
                //DataTable dt = (DataTable)ViewState["ingvdata"];
                string strFileName = "", strExtension = "", strTimeStamp = "";
                int iFailedCntExt = 0; int iFailedCntSize = 0;
                for (int i = 0; i < GrdBills.Rows.Count; i++)
                {
                    FileUpload IdFileUpload = (FileUpload)row.FindControl("IdFileUpload");

                    if (!IdFileUpload.HasFile)
                    {
                        lblmsg.Text = "❌ Upload PDF file.";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + lblmsg.Text + "');", true);
                        return;   // ✅ INSERT TAK NAHI JAYEGA
                    }

                    string fileExt = Path.GetExtension(IdFileUpload.FileName).ToLower();

                    if (fileExt != ".pdf")
                    {
                        lblmsg.Text = "Document Should be in PDF Format Only";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + lblmsg.Text + "');", true);
                        return;   // ✅ INSERT RUK JAYEGA
                    }
                    else if (IdFileUpload.PostedFile.ContentLength > 512000) // 500 KB
                    {
                        lblmsg.Text = "pdf Should be Under 500KB";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + lblmsg.Text + "');", true);
                        return;   // ✅ INSERT RUK JAYEGA
                    }
                    else
                    {
                        strFileName = IdFileUpload.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);

                        strTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);

                        strFileName = strName + "_" + strTimeStamp + strExtension;

                        string path = Path.Combine(Server.MapPath("../NCCF/Document_Image/"), strFileName);
                        IdFileUpload.SaveAs(path);
                        ViewState["ingvdata"] = strFileName;   // ✅ File name yahi set rahega
                    }
                }
                //    else
                //{
                //    ViewState["ingvdata"] = "File not Available";
                //}

                if (lblmsg.Text == "")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    string qry = "";
                    int ICount = 0;
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    con.Open();
                    HiddenField Bill_Number = (HiddenField)GrdBills.Rows[row.RowIndex].FindControl("hdnBill_Number");
                    //(row.FindControl("hdnBill_Number") as HiddenField).Value;
                    string CommodityID = (row.FindControl("hdnCommodityId") as HiddenField).Value;
                    string Crop_Year = (row.FindControl("lblCrop_Year") as Label).Text;
                    string Bill_Count = (row.FindControl("lblBill_Count") as Label).Text;
                    string Net_Amount = (row.FindControl("lblNet_Amount") as Label).Text;
                    string Pass_Amount = (row.FindControl("lblPass_Amount") as Label).Text;
                    string Deduction_Amount = (row.FindControl("txtDeduction_Amount") as TextBox).Text;
                    //string Final_Amount = (row.FindControl("lblFinalAmount") as Label).Text;
                    //string Final_Amount = (row.FindControl("hdnFinalAmount") as HiddenField).Value;
                    HiddenField Final_Amount = (HiddenField)GrdBills.Rows[row.RowIndex].FindControl("hdnFinalAmount");
                    string UTR_No = (row.FindControl("txtUpdate_UTR") as TextBox).Text;
                    string Bank_Date = (row.FindControl("txtPayment_Date") as TextBox).Text;
                    string File_Upload = "";
                    if (ViewState["ingvdata"] != null)
                    {
                        File_Upload = ViewState["ingvdata"].ToString();
                    }
                    else
                    {
                        File_Upload = "File not Available";
                    }
                    qry = "INSERT INTO tbl_NCCF_Deduction_On_Processed_Bill(Bill_Number,Commodity_Id,Crop_Year,Bill_Count,Total_Bill_Amount,Process_Amount,Deduction_Amount,Final_Amount,Deduction_Doc,UTR_No,Payment_Date,Created_By,CreatedBy_Ip,Created_On) values(@Bill_Number,@Commodity_Id,@Crop_Year,@Bill_Count,@Total_Bill_Amount,@Process_Amount,@Deduction_Amount,@Final_Amount,@Deduction_Doc,@UTR_No,@Payment_Date,@Created_By,@CreatedBy_Ip,getdate())";
                    SqlCommand cmd2 = new SqlCommand(qry, con);
                    cmd2.Parameters.AddWithValue("@Bill_Number", Bill_Number.Value);
                    cmd2.Parameters.AddWithValue("@Commodity_Id", CommodityID);
                    cmd2.Parameters.AddWithValue("@Crop_Year", Crop_Year);
                    cmd2.Parameters.AddWithValue("@Bill_Count", Bill_Count);
                    cmd2.Parameters.AddWithValue("@Total_Bill_Amount", Net_Amount);
                    cmd2.Parameters.AddWithValue("@Process_Amount", Pass_Amount);
                    cmd2.Parameters.AddWithValue("@Deduction_Amount", Deduction_Amount);
                    cmd2.Parameters.AddWithValue("@Final_Amount", Final_Amount.Value);
                    cmd2.Parameters.AddWithValue("@Deduction_Doc", File_Upload);
                    cmd2.Parameters.AddWithValue("@UTR_No", UTR_No);
                    cmd2.Parameters.AddWithValue("@Payment_Date", getDate_MDY(Bank_Date));
                    cmd2.Parameters.AddWithValue("@Created_By", Session["UserID"].ToString());
                    cmd2.Parameters.AddWithValue("@CreatedBy_Ip", ip);
                    int i = cmd2.ExecuteNonQuery();
                    ICount = ICount + i;
                    ViewState["ingvdata"] = null;
                    string strMsg = "Payment Done Successfully |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    ViewState["ingvdata"] = "";
                    ViewState["finalAmount"] = "";
                    fillgrid();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + lblmsg.Text + "');", true);
            }

        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
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
    protected void GrdBills_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPass = (Label)e.Row.FindControl("lblPass_Amount");
            Label lblFinal = (Label)e.Row.FindControl("lblFinalAmount");
            HiddenField hdnFinal = (HiddenField)e.Row.FindControl("hdnFinalAmount");

            if (lblPass != null && lblFinal != null && hdnFinal != null)
            {
                decimal passAmount = 0;
                decimal.TryParse(lblPass.Text, out passAmount);

                lblFinal.Text = passAmount.ToString("0.00");
                hdnFinal.Value = passAmount.ToString("0.00");
            }
        }
    }
}
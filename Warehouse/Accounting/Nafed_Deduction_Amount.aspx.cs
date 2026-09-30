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

public partial class Accounting_Nafed_Deduction_Amount : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand("Get_Pass_Amount_For_Deduction", con);
            cmd.CommandType = CommandType.StoredProcedure;
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

                    FileUpload IdFileUpload = (FileUpload)GrdBills.Rows[i].FindControl("IdFileUpload");
                    if (IdFileUpload.HasFile)// CHECK IF ANY FILE HAS BEEN SELECTED.
                    {
                        string fileExt = Path.GetExtension(IdFileUpload.FileName);
                        string[] supportedTypes = { ".pdf", ".PDF" };
                        if (!supportedTypes.Contains(fileExt))
                            iFailedCntExt += 1;
                        else if (IdFileUpload.FileBytes.Length > 512000) // 512000  = 1024 * 500 KB
                            iFailedCntSize += 1;
                        else
                        {
                            strFileName = IdFileUpload.FileName.ToString();
                            strExtension = Path.GetExtension(strFileName);
                            strTimeStamp = DateTime.Now.ToString();
                            strTimeStamp = strTimeStamp.Replace("/", "");
                            strTimeStamp = strTimeStamp.Replace(" ", "");
                            strTimeStamp = strTimeStamp.Replace(":", "");
                            string strName = Path.GetFileNameWithoutExtension(strFileName);
                            strFileName = strName + strTimeStamp + strExtension;
                            string path = Path.Combine(Server.MapPath("../DocumentImage/"), strFileName);
                            IdFileUpload.SaveAs(path);
                            ViewState["ingvdata"] = strFileName;
                            //dt.Rows[i]["Doc"] = strFileName;
                            //ViewState["gvalldata"] = dt;
                        }
                    }
                    else
                    {
                        //dt.Rows[i]["Doc"] = "Not Available";
                        //ViewState["ingvdata"] = dt;
                    }
                }
                if (iFailedCntExt == 0)
                {
                    if (iFailedCntSize == 0)
                    {
                        //dt.AcceptChanges();
                    }
                    else lblmsg.Text = "pdf Should be Under 500KB";
                }
                else lblmsg.Text = "Document Should be in PDF Format Only";
                if (lblmsg.Text == "")
                {
                    //ErrorMsg += !string.IsNullOrEmpty(ViewState["ingvdata"].ToString()) ? "" : "Upload Pdf File. \\n";
                    //if (ViewState["ingvdata"] != null)
                    //{
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                    string qry = "";
                    int ICount = 0;
                    string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    con.Open();
                    string Bill_Number = (row.FindControl("hdnBill_Number") as HiddenField).Value;
                    string CommodityID = (row.FindControl("hdnCommodityId") as HiddenField).Value;
                    string Crop_Year = (row.FindControl("lblCrop_Year") as Label).Text;
                    string Bill_Count = (row.FindControl("lblBill_Count") as Label).Text;
                    string Net_Amount = (row.FindControl("lblNet_Amount") as Label).Text;
                    string Pass_Amount = (row.FindControl("lblPass_Amount") as Label).Text;
                    string Deduction_Amount = (row.FindControl("txtDeduction_Amount") as TextBox).Text;
                    string UTR_No = (row.FindControl("txtUpdate_UTR") as TextBox).Text;
                    string Bank_Date = (row.FindControl("txtPayment_Date") as TextBox).Text;
                    if (ViewState["ingvdata"] != null)
                    {
                        ViewState["ingvdata"] = "File not Available";
                    }
                    else
                    {
                        ViewState["ingvdata"] = strFileName;
                    }
                    string File_Upload = ViewState["ingvdata"].ToString();
                    //if (Deduction_Amount != "0.00")
                    //{
                    qry = "INSERT INTO tbl_Nafed_Deduction_On_Processed_Bill(Bill_Number,Commodity_Id,Crop_Year,Bill_Count,Total_Bill_Amount,Process_Amount,Deduction_Amount,Deduction_Doc,UTR_No,Payment_Date,Created_By,CreatedBy_Ip,Created_On) values('" + Bill_Number + "','" + CommodityID + "','" + Crop_Year + "','" + Bill_Count + "','" + Net_Amount + "','" + Pass_Amount + "','" + Deduction_Amount + "','" + File_Upload + "','" + UTR_No + "','" + getDate_MDY(Bank_Date) + "','" + Session["State_Logid"].ToString() + "','" + ip + "',getdate())";
                    SqlCommand cmd2 = new SqlCommand(qry, con);
                    int i = cmd2.ExecuteNonQuery();
                    ICount = ICount + i;
                    ViewState["ingvdata"] = null;
                    string strMsg = "Bill Submit Successfully |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    ViewState["ingvdata"] = "";
                    fillgrid();
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Deduction Amount')", true);
                    //}
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload Pdf File')", true);
                    //}
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + lblmsg.Text + "');", true);
                }
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
}
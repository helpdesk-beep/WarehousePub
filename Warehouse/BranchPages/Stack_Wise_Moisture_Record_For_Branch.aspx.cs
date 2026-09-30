using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_Stack_Wise_Moisture_Record_For_Branch : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                txtbranch.Text = Session["UserName"].ToString();
                fillGodownType();
            }
        }
    }
    private void fillGodownType()
    {
        try
        {
            string query = "";
            query = "Select Distinct Hired_Type from tbl_MetaData_GODOWN_2018 Order By Hired_Type ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodowntype.DataSource = ds.Tables[0];
                ddlgodowntype.DataTextField = "Hired_Type";
                ddlgodowntype.DataValueField = "Hired_Type";
                ddlgodowntype.DataBind();
                ddlgodowntype.Items.Insert(0, "Select");
            }
            else
            {
                ddlgodowntype.Items.Clear();
                ddlgodowntype.Items.Insert(0, "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlgodowntype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
    }
    private void fillGodown()
    {
        try
        {
            string query = "";
            query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID ='" + Session["BranchId"].ToString() + "' And Hired_Type='" + ddlgodowntype.SelectedItem.Text + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "Select");
            }
            else
            {
                ddlGodown.Items.Clear();
                ddlGodown.Items.Insert(0, "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Stack_Wise_Moisture_Entry_Data", con))
            {
                cmd.CommandTimeout = 180; // 180 seconds
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
                cmd.Parameters.AddWithValue("@FinacialYEar", ddlfinancialyear.SelectedValue);
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdMoisture.DataSource = dt;
                            grdMoisture.DataBind();
                            Div1.Visible = true;
                        }
                        else
                        {
                            grdMoisture.DataSource = null;
                            grdMoisture.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void grdMoisture_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            string ErrorMsg = "";
            ErrorMsg += ddlgodowntype.SelectedIndex > 0 ? "" : "Please Select Godown Type... \\n";
            ErrorMsg += ddlGodown.SelectedIndex > 0 ? "" : "Please Select Godown... \\n";
            ErrorMsg += ddlfinancialyear.SelectedIndex > 0 ? "" : "Please Select Financial Year... \\n";
            ErrorMsg += ddlmonth.SelectedIndex > 0 ? "" : "Please Select Month... \\n";
            if (ErrorMsg == "")
            {
                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                Label RowNumber = (Label)row.FindControl("lblRowNumber");
                string strFileName = "", strExtension = "", strTimeStamp = "";
                int iFailedCntExt = 0; int iFailedCntSize = 0;
                for (int i = 0; i < grdMoisture.Rows.Count; i++)
                {

                    FileUpload IdFileUpload = (FileUpload)grdMoisture.Rows[i].FindControl("IdFileUpload");
                    if (IdFileUpload.HasFile)// CHECK IF ANY FILE HAS BEEN SELECTED.
                    {
                        string fileExt = Path.GetExtension(IdFileUpload.FileName);
                        string[] supportedTypes = { ".pdf", ".PDF" };
                        if (!supportedTypes.Contains(fileExt))
                            iFailedCntExt += 1;
                        //else if (IdFileUpload.FileBytes.Length > 512000) // 512000  = 1024 * 500 KB
                        //    iFailedCntSize += 1;
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
                            string path = Path.Combine(Server.MapPath("../Moisture_Document/"), strFileName);
                            IdFileUpload.SaveAs(path);
                            ViewState["ingvdata"] = strFileName;
                            //dt.Rows[i]["Doc"] = strFileName;
                            //ViewState["gvalldata"] = dt;
                        }
                    }
                    else
                    {

                        ViewState["ingvdata"] = "NULL";
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
                    string Stack_ID = (row.FindControl("lblStack_ID") as Label).Text;
                    string Stack_Name = (row.FindControl("lblStack_Name") as Label).Text;
                    //string Commodity_ID = (row.FindControl("hdnCommodity_Id") as HiddenField).Value;
                    string Stack_Moisture_Per = (row.FindControl("txtStack_Moisture_Per") as TextBox).Text;
                    string Moisture_Date = (row.FindControl("txtDate") as TextBox).Text;
                    string File_Upload = ViewState["ingvdata"].ToString();
                    if (Moisture_Date != "")
                    {
                        if (Stack_Moisture_Per != "")
                        {
                            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                            SqlCommand cmd = new SqlCommand("Insert_Stack_Wise_Moisture_For_Branch", con1);
                            cmd.CommandType = CommandType.StoredProcedure;
                            con1.Open();
                            cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
                            cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
                            cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
                            cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
                            cmd.Parameters.AddWithValue("@Hired_Type", ddlgodowntype.SelectedValue);
                            cmd.Parameters.AddWithValue("@Stack_ID", Stack_ID);
                            cmd.Parameters.AddWithValue("@Stack_Name", Stack_Name);
                            cmd.Parameters.AddWithValue("@Commodity_Id", 0);
                            cmd.Parameters.AddWithValue("@Stack_Moisture_Per", Convert.ToDecimal(Stack_Moisture_Per).ToString());
                            cmd.Parameters.AddWithValue("@DM_Record_Date", getDate_MDY(Moisture_Date));
                            cmd.Parameters.AddWithValue("@Moisture_Documents", File_Upload);
                            cmd.Parameters.AddWithValue("@Create_By", Session["BranchId"].ToString());
                            cmd.Parameters.AddWithValue("@Createby_Ip", ipAddress);
                            cmd.Parameters.AddWithValue("@IP_Adress", ipAddress);
                            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                            if (TheResult.StartsWith("SUCCESS"))
                            {
                                string strMsg = "Data Submit Successfully |||";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                                fillgrid();
                                ViewState["ingvdata"] = null;
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                            }
                        }
                        else
                        {
                            string strMsg = "Enter Stack Moisture Percentage|||";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        }
                    }
                    else
                    {
                        string strMsg = "Please Select Date |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + lblmsg.Text + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
            }
        }
        if (e.CommandName == "Delete")
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label RowNumber = (Label)row.FindControl("lblRowNumber");
            // string id = (row.FindControl("hdnId") as HiddenField).Value;
            //Determine the RowIndex of the Row whose Button was clicked.
            //int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            //GridViewRow row = grdJitbill.Rows[rowIndex];

            //Fetch value of Name.
            string Stack_ID = (row.FindControl("lblStack_ID") as Label).Text;
            Session["Stack_ID"] = Stack_ID.ToString();
            RemoveRow(Stack_ID);
            // RemoveRowJVS(hdnId);

        }
    }
    public void RemoveRow(string Stack_ID)
    {

        SqlCommand cmd1 = new SqlCommand();
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlCommand cmd = new SqlCommand("Delete_Stack_Wise_Moisture_Entry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Stack_ID", Stack_ID.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Delete Record Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillgrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "";
        }
        else
        {
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
}
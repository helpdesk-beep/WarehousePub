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

public partial class BranchPages_Godown_Wise_Moisture_For_Branch : System.Web.UI.Page
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
                FillGrid();
            }
        }
    }
    private void fillGodownType()
    {
        try
        {
            string query = "";
            query = "Select Distinct Hired_Type from tbl_Stack_Wise_Moisture_Entry_By_BM Order By Hired_Type ASC";
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
    private void fillFinancialYear()
    {
        try
        {
            string query = "";
            query = "Select Distinct Financial_Year from tbl_Stack_Wise_Moisture_Entry_By_BM Where Godown_ID ='" + ddlGodown.SelectedValue + "'";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlfinancialyear.DataSource = ds.Tables[0];
                ddlfinancialyear.DataTextField = "Financial_Year";
                ddlfinancialyear.DataValueField = "Financial_Year";
                ddlfinancialyear.DataBind();
                ddlfinancialyear.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlfinancialyear.Items.Clear();
                ddlfinancialyear.Items.Insert(0, "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    private void fillMonth()
    {
        try
        {
            string query = "";
            query = "Select Distinct Month,(CASE WHEN Month ='1' THEN 'January' WHEN Month ='2' THEN 'February' WHEN Month ='3' THEN 'March' WHEN Month ='4' THEN 'April' WHEN Month ='5' THEN 'May' WHEN Month ='6' THEN 'June' WHEN Month ='7' THEN 'July' WHEN Month ='8' THEN 'August' WHEN Month ='9' THEN 'September' WHEN Month ='10' THEN 'October' WHEN Month ='11' THEN 'November' WHEN Month ='12' THEN 'December' END) As Month_Name from tbl_Stack_Wise_Moisture_Entry_By_BM Where Godown_ID ='" + ddlGodown.SelectedValue + "' And Financial_Year='" + ddlfinancialyear.SelectedValue + "'";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlmonth.DataSource = ds.Tables[0];
                ddlmonth.DataTextField = "Month_Name";
                ddlmonth.DataValueField = "Month";
                ddlmonth.DataBind();
                ddlmonth.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlmonth.Items.Clear();
                ddlmonth.Items.Insert(0, "Select");
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
            query = "Select DISTINCT SW.Godown_ID,MG.Godown_Name from tbl_Stack_Wise_Moisture_Entry_By_BM SW inner join tbl_MetaData_GODOWN_2018 MG on SW.Godown_ID=MG.Godown_ID Where SW.Branch_ID ='" + Session["BranchId"].ToString() + "' And SW.Hired_Type='" + ddlgodowntype.SelectedItem.Text + "' Order By Godown_Name ASC";
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
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillFinancialYear();
    }
    protected void ddlfinancialyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMonth();
    }
    protected string MoistureDocument()
    {
        string result = "";
        try
        {
            string strFileName = "", strExtension = "", strTimeStamp = "";
            if (IdFileUpload.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
                int iFailedCntExt = 0;
                // int iFailedCntSize = 0;
                string fileExt = System.IO.Path.GetExtension(IdFileUpload.FileName).Substring(1);
                string[] supportedTypes = { "pdf", "PDF" };
                if (!supportedTypes.Contains(fileExt))
                {
                    iFailedCntExt += 1;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert ('Godown Moisture Document Upload in PDF Format Only')", true);
                    result = "Godown Moisture Document Upload in PDF Format Only";
                }
                //else if (IdFileUpload.PostedFile.ContentLength > 1048576) // 1 MB = 1024 * 100
                //{
                //    iFailedCntSize += 1;
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert ('Candidate Image Should be Under 500KB')", true);
                //    result = "Candidate Image Should be Under 500KB";
                //}
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
                    string path = Path.Combine(Server.MapPath("../Godown_Moisture_Document/"), strFileName);
                    IdFileUpload.SaveAs(path);
                    ViewState["Godown_Moisture_Document"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }
            }
            else
            {
                string path3 = Path.Combine(Server.MapPath("../Godown_Moisture_Document/"), strFileName);
                if (File.Exists(path3))
                {
                    File.Delete(path3);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return result;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        MoistureDocument();
        String Godown_Moisture_Document = ViewState["Godown_Moisture_Document"].ToString();
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        SqlCommand cmd = new SqlCommand("Insert_Godown_Wise_Moisture_Document_Upload", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
        cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
        cmd.Parameters.AddWithValue("@Godown_Type", ddlgodowntype.SelectedValue);
        cmd.Parameters.AddWithValue("@Financial_Year", ddlfinancialyear.SelectedValue);
        cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
        cmd.Parameters.AddWithValue("@Moisture_Document", Godown_Moisture_Document);
        cmd.Parameters.AddWithValue("@Created_By", Session["BranchId"].ToString());
        cmd.Parameters.AddWithValue("@CreatedBy_IP", ip);
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
        if (TheResult.StartsWith("SUCCESS"))
        {
            string strMsg = "Document Upload Successfully |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            FillGrid();
            TextClear();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
            TextClear();
        }
    }
    protected void TextClear()
    {
        ddlGodown.ClearSelection();
        ddlgodowntype.ClearSelection();
        ddlfinancialyear.ClearSelection();
        ddlmonth.ClearSelection();
    }
    protected void FillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Wise_Moisture_Document", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchId"].ToString());
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
}
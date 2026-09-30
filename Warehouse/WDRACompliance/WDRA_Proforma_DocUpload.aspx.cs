using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;

public partial class WDRACompliance_WDRA_Proforma_DocUpload : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if ((Session["BranchID"] != null))
        //{
        if (!IsPostBack)
        {
            fillDetailsInGrid();
        }
        //}
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    rowIndex++;
                }
            }
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string ErrorMsg = "";
        lblMsg.Text = "";
        ErrorMsg += ddlWHOwnerDocType.SelectedIndex > 0 ? "" : "Please Select Document Type\\n";
        ErrorMsg += !string.IsNullOrEmpty(FUDocument.HasFile.ToString()) ? "" : "Please Select Document\\n";
        ErrorMsg += !string.IsNullOrEmpty(txtRemarks.Text) ? "" : " Please Enter Remarks\\n";
        if (ErrorMsg == "")
        {
            //string BranchID = Session["BranchID"].ToString();
            //string GodownID = "";
            string GodownID = Session["GodownID_New"].ToString();
            string WHOwnerDocType = ddlWHOwnerDocType.SelectedItem.ToString();
            string WHFileDocument = FUDocument.PostedFile.FileName.ToString();
            string str = GodownID + '_' + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + '_' + FUDocument.FileName;
            FUDocument.PostedFile.SaveAs(Server.MapPath("/WDRACompliance/UploadedDocs/" + str));
            string file = "/Warehouse/WDRACompliance/UploadedDocs/" + str.ToString();
            string filePath = FUDocument.PostedFile.FileName;
            string WHFileContentType = Path.GetExtension(FUDocument.PostedFile.FileName.ToString());
            string Remarks = txtRemarks.Text.ToString();
            string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
            SqlCommand cmd = new SqlCommand("usp_InsertWDRA_WHUploadedDocumentInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Godown_ID", GodownID);
            cmd.Parameters.AddWithValue("@WH_DocumentType", WHOwnerDocType);
            cmd.Parameters.AddWithValue("@WH_DocumentName", WHFileDocument);
            cmd.Parameters.AddWithValue("@WH_DocumentContentType", WHFileContentType);
            cmd.Parameters.AddWithValue("@WH_DocumentPath", WHFileDocument);
            cmd.Parameters.AddWithValue("@WH_Remarks", Remarks);
            cmd.Parameters.AddWithValue("@CreatedBy", ClientIP);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Record Inserted Successfully";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Try Again !');", true);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(' " + ErrorMsg.ToString() + "')", true);
        }
        fillDetailsInGrid();
    }

    protected void fillDetailsInGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        string GodownID = Session["GodownID_New"].ToString();
        using (SqlConnection con = new SqlConnection(constr))
        {
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand("usp_Get_WDRA_WHUploadedDocumentInfo", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@BranchID", BranchID);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    DataTable MainTable = ds.Tables[0];

                    if (MainTable.Rows.Count > 0)
                    {
                        GV_EntryDone.DataSource = MainTable;
                        GV_EntryDone.DataBind();
                    }
                    else
                    {
                        GV_EntryDone.DataSource = null;
                        GV_EntryDone.DataBind();
                    }

                }
            }
        }
    }

    protected void GV_EntryDone_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GV_EntryDone.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            Session["hdnId"] = hdnId.ToString();
            RemoveRow(hdnId);

        }
    }

    public void RemoveRow(string id)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd1 = new SqlCommand();
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlCommand cmd = new SqlCommand("usp_DeleteWDRA_WHUploadedDocumentInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Deleted Successfully!');", true);
                fillDetailsInGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Deleted!');", true);
            }

        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }

    protected void Upload(object sender, EventArgs e)
    {
        string GodownID = Session["GodownID_New"].ToString();
        string DocumentType = ddlWHOwnerDocType.SelectedItem.ToString();
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        string filename = Path.GetFileName(FUDocument.PostedFile.FileName);
        string contentType = FUDocument.PostedFile.ContentType;
        decimal size = Math.Round(((decimal)FUDocument.PostedFile.ContentLength / (decimal)1024), 2);
        //if (size > 100)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Image size must not exceed 100 KB ..!')", true);
        //    return;
        //}
        using (Stream fs = FUDocument.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {

                byte[] bytes12th = br.ReadBytes((Int32)fs.Length);
                string type = String.Empty;
                string ext = Path.GetExtension(FUDocument.PostedFile.FileName);
                switch (ext) // this switch code validate the files which allow to upload only PDF file   
                {
                    case ".PDF":
                        type = "application/pdf";
                        break;
                    case ".pdf":
                        type = "application/pdf";
                        break;

                }
                if (type != String.Empty)
                {
                    SqlCommand cmd = new SqlCommand("usp_InsertWDRA_WHUploadedDocumentInfo", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Godown_ID", GodownID);
                    cmd.Parameters.AddWithValue("@WH_DocumentType", DocumentType);
                    cmd.Parameters.AddWithValue("@WH_DocumentName", filename);
                    cmd.Parameters.AddWithValue("@WH_DocumentContentType", contentType);
                    cmd.Parameters.AddWithValue("@WH_DocumentPath", bytes12th);
                    cmd.Parameters.AddWithValue("@CreatedBy", IPAddress);
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        string strMsg = "Record Insert Successfully";

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        fillDetailsInGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please try again !');", true);
                    }

                }
                else
                {
                    string strMsg2 = "Upload Only PDF Files!!!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
                }
            }
        }
    }

    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName, contentType;
        SqlCommand cmd = new SqlCommand("get_Upload_All_Documents", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Connection = con;
        con.Open();
        using (SqlDataReader sdr = cmd.ExecuteReader())
        {
            sdr.Read();
            bytes = (byte[])sdr["Intermarksheet"];
            contentType = sdr["ContentType"].ToString();
            fileName = sdr["Name"].ToString();
        }
        con.Close();

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

    protected void btnClkNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_CommodityInfo.aspx");
    }

    protected void btnClkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/WDRACompliance/WDRA_Proforma_FireSafetyInfo.aspx");
    }
}



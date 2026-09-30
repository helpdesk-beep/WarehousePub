using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Inspections_Inspection_Officer_UploadDocuments_New : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    string PFID = "";
    string branchid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["University_E"] == null)
        //{
        //    Response.Redirect("/VC_Application/Default.aspx");
        //}
        PFID = Session["UserId"].ToString();
       // branchid = Session["hdnbranchid"].ToString();
        if (!IsPostBack)
        {
            fillBranchDetails();
            fillFinsncilYear();
            Fill12thmarksheetdata();
            //Fill12thmarksheetdata();
            //FillGeneralInformation();
        }
    }
    public void fillFinsncilYear()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Fianancial_Year_For_inspection", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            con.Open();
            ddlfinancialyear.DataSource = cmd.ExecuteReader();
            ddlfinancialyear.DataTextField = "Financial_Year";
            ddlfinancialyear.DataValueField = "Financial_Year";
            ddlfinancialyear.DataBind();
            ddlfinancialyear.Items.Insert(0, new ListItem("--Select Financial Year--", "0"));
            con.Close();
        }
    }
    public void fillBranchDetails()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("Get_Branch_Name_For_DF", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PFID_ID", PFID);
            con.Open();
            ddlbranch.DataSource = cmd.ExecuteReader();
            ddlbranch.DataTextField = "Depo_Name";
            ddlbranch.DataValueField = "Branch_ID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("-- Select Branch --", "0"));
            ddlbranch.SelectedValue = branchid.ToString();
            //ddlbranch.Enabled = false;
            con.Close();
        }
    }
    //public void FillGeneralInformation()
    //{
    //    if (con.State == ConnectionState.Closed)
    //    {
    //        con.Open();
    //    }
    //    SqlCommand cmd = new SqlCommand("Check_Final_Submission", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@U_ID", Session["U_ID"].ToString());
    //    cmd.Parameters.AddWithValue("@vc_id", Session["VC_ID"].ToString());
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (con.State == ConnectionState.Open)
    //    { con.Close(); }
    //    if (dt.Rows.Count > 0)
    //    {
    //        divdocument.Visible = false;
    //        //divmsg.Visible = true;
    //        //divnext.Visible = false;
    //    }
    //}
    public void Fill12thmarksheetdata()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("Show_Uploaded_Document", con);
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@EmployeeID", PFID.ToString());
        //cmd.Parameters.AddWithValue("@QuaterID", Session["hdninsp_type_id"].ToString());
        //cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    protected void Upload(object sender, EventArgs e)
    {
        string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        string str =  ddlbranch.SelectedItem.ToString() + '_' + Session["UserName"].ToString() + '_' + FileUpload1.FileName;
        FileUpload1.PostedFile.SaveAs(Server.MapPath("/Warehouse/Inspections/Upload/" + str));
        string file = "/Warehouse/Inspections/Upload/" + str.ToString();
        string filePath = FileUpload1.PostedFile.FileName;
        string fileName = Path.GetFileName(filePath);
        string ext = Path.GetExtension(fileName);
        string type = String.Empty;

        if (FileUpload1.HasFile)
        {
            try
            {
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

                    Stream fsHn = FileUpload1.PostedFile.InputStream;
                    BinaryReader brHn = new BinaryReader(fsHn); //reads the binary files  
                    Byte[] bytesHn = brHn.ReadBytes((Int32)fsHn.Length); //counting the file length into bytes  


                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        SqlCommand cmd = new SqlCommand("Inspection_Document_Upload", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
                        cmd.Parameters.AddWithValue("@EmployeeID", PFID.ToString());
                        //cmd.Parameters.AddWithValue("@QuaterID", Session["hdninsp_type_id"].ToString());
                        //cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
                        cmd.Parameters.AddWithValue("@QuaterID", ddlquater.SelectedValue);
                        cmd.Parameters.AddWithValue("@InspectionTypeID", ddlverification.SelectedValue);
                        cmd.Parameters.AddWithValue("@Year", ddlfinancialyear.SelectedValue);
                        cmd.Parameters.AddWithValue("@Doc_Type", ddldoctype.SelectedValue);
                        cmd.Parameters.AddWithValue("@Name", file);
                        //cmd.Parameters.AddWithValue("@ContentType", contentType);
                        cmd.Parameters.AddWithValue("@VC_Upload_Document", file);
                        cmd.Parameters.AddWithValue("@IP_Address", IPAddress);
                        cmd.Parameters.AddWithValue("@FinancialYear", ddlfinancialyear.SelectedValue);
                        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                        if (TheResult.StartsWith("SUCCESS"))
                        {
                            string strMsg = "Record Insert Successfully|||";

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                            Fill12thmarksheetdata();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
                        }
                        //SqlCommand cmd = new SqlCommand("insert_whatsNew", con);
                        //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@DocTypeId", ddlDocType.SelectedValue);
                        //cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = txtTitleHn.InnerText;
                        //cmd.Parameters.Add("@releaseDate", SqlDbType.Date).Value = relDate;
                        //cmd.Parameters.Add("@expireDate", SqlDbType.Date).Value = expDate;
                        //cmd.Parameters.AddWithValue("@fileName", file);
                        //cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                        //con.Open();
                        //cmd.ExecuteNonQuery();
                        //con.Close();

                        //lblErr.ForeColor = System.Drawing.Color.Green;
                        //lblErr.Text = "File Uploaded Successfully!!!";

                    }
                }
                else
                {
                    string strMsg2 = "Upload Only PDF Files!!!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
                }

            }
            catch (Exception ex)
            {
                string strMsg2 = "Error: " + ex.Message.ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
            }

          
        }
        else
        {
            string strMsg2 = "Please Upload Your File";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
    //protected void Upload(object sender, EventArgs e)
    //{
    //    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
    //    string filename = Path.GetFileName(upload12thmarksheet.PostedFile.FileName);
    //    string contentType = upload12thmarksheet.PostedFile.ContentType;
    //    decimal size = Math.Round(((decimal)upload12thmarksheet.PostedFile.ContentLength / (decimal)1024), 2);
    //    //if (size > 100)
    //    //{
    //    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Image size must not exceed 100 KB ..!')", true);
    //    //    return;
    //    //}
    //    using (Stream fs = upload12thmarksheet.PostedFile.InputStream)
    //    {
    //        using (BinaryReader br = new BinaryReader(fs))
    //        {

    //            byte[] bytes12th = br.ReadBytes((Int32)fs.Length);
    //            string type = String.Empty;
    //            string ext = Path.GetExtension(upload12thmarksheet.PostedFile.FileName);
    //            string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    //            switch (ext) // this switch code validate the files which allow to upload only PDF file   
    //            {
    //                case ".PDF":
    //                    type = "application/pdf";
    //                    break;
    //                case ".pdf":
    //                    type = "application/pdf";
    //                    break;

    //            }
    //            if (type != String.Empty)
    //            {
    //                using (SqlConnection con = new SqlConnection(constr))
    //                {

    //                    SqlCommand cmd = new SqlCommand("Inspection_Document_Upload", con);
    //                    cmd.CommandType = CommandType.StoredProcedure;
    //                    con.Open();
    //                    cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
    //                    cmd.Parameters.AddWithValue("@EmployeeID", PFID.ToString());
    //                    //cmd.Parameters.AddWithValue("@QuaterID", Session["hdninsp_type_id"].ToString());
    //                    //cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
    //                    cmd.Parameters.AddWithValue("@QuaterID", ddlquater.SelectedValue);
    //                    cmd.Parameters.AddWithValue("@InspectionTypeID", ddlverification.SelectedValue);
    //                    cmd.Parameters.AddWithValue("@Year", ddlfinancialyear.SelectedValue);
    //                    cmd.Parameters.AddWithValue("@Doc_Type", ddldoctype.SelectedValue);
    //                    cmd.Parameters.AddWithValue("@Name", filename);
    //                    cmd.Parameters.AddWithValue("@ContentType", contentType);
    //                    cmd.Parameters.AddWithValue("@VC_Upload_Document", bytes12th);
    //                    cmd.Parameters.AddWithValue("@IP_Address", IPAddress);
    //                    cmd.Parameters.AddWithValue("@FinancialYear", ddlfinancialyear.SelectedValue);
    //                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
    //                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //                    cmd.ExecuteNonQuery();

    //                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

    //                    if (TheResult.StartsWith("SUCCESS"))
    //                    {
    //                        string strMsg = "Record Insert Successfully|||";

    //                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
    //                        Fill12thmarksheetdata();
    //                    }
    //                    else
    //                    {
    //                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                string strMsg2 = "Upload Only PDF Files!!!";
    //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
    //            }
    //        }
    //    }
    //}

    
    public void RemoveRow(string id)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("Inspection_Remove_Uploaded_Document", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                Fill12thmarksheetdata();
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {       
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GridView1.Rows[rowIndex];

            //Fetch value of Name.
            string hdnid = (row.FindControl("hdnid") as HiddenField).Value;
            Session["hdnid"] = hdnid.ToString();
            RemoveRow(hdnid);

        }
    }
}
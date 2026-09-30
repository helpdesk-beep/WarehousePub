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
using System.Collections.Generic;

public partial class StatePages_JIT_Bill_Process_MPWLCHO_TO_MPSCSCHO : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            fillRegion();
            FillGrid();
        }
    }
    private void fillRegion()
    {
        try
        {
            string query = "";
            query = "Select Distinct Region_ID,Regionnm From tbl_MetaData_DISTRICT Order By Regionnm ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlregion.DataSource = ds.Tables[0];
                ddlregion.DataTextField = "Regionnm";
                ddlregion.DataValueField = "Region_ID";
                ddlregion.DataBind();
                ddlregion.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlregion.Items.Clear();
                ddlregion.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistrict();
    }
    private void fillDistrict()
    {
        try
        {
            string query = "";
            query = "Select District_Id,District_Name From tbl_MetaData_DISTRICT Where Region_ID='" + ddlregion.SelectedValue + "' Order By District_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddldistrict.DataSource = ds.Tables[0];
                ddldistrict.DataTextField = "District_Name";
                ddldistrict.DataValueField = "District_Id";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddldistrict.Items.Clear();
                ddldistrict.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }
    private void fillBranch()
    {
        try
        {
            string query = "";
            query = "Select BranchId,DepotName From tbl_MetaData_DEPOT Where DistrictId='" + ddldistrict.SelectedValue + "' Order By DepotName ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlbranch.Items.Clear();
                ddlbranch.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGodown();
    }
    private void fillGodown()
    {
        try
        {
            string query = "";
            query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN Where BranchID ='" + ddlbranch.SelectedValue + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodown.DataSource = ds.Tables[0];
                ddlgodown.DataTextField = "Godown_Name";
                ddlgodown.DataValueField = "Godown_ID";
                ddlgodown.DataBind();
                ddlgodown.Items.Insert(0, "Select");
            }
            else
            {
                ddlgodown.Items.Clear();
                ddlgodown.Items.Insert(0, "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string strFileName = "", strExtension = "", strTimeStamp = "";
        if (!IdFileUpload.HasFile)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select PDF Document!');", true);
            return;
        }

        string[] allowedExtensions = { ".pdf" };
        string extension = Path.GetExtension(IdFileUpload.FileName).ToLower();
        int fileSize = IdFileUpload.PostedFile.ContentLength;

        // Validate extension
        if (Array.IndexOf(allowedExtensions, extension) < 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select PDF file Only!');", true);
            return;
        }
        // Prepare save path
        string folder = Server.MapPath("~/JIT_Bill_PDF_Upload/");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string fileName = Path.GetFileName(IdFileUpload.FileName);
        string fullPath = Path.Combine(folder, fileName);
        strFileName = IdFileUpload.FileName.ToString();
        strExtension = Path.GetExtension(strFileName);
        strTimeStamp = DateTime.Now.ToString();
        strTimeStamp = strTimeStamp.Replace("/", "");
        strTimeStamp = strTimeStamp.Replace(" ", "");
        strTimeStamp = strTimeStamp.Replace(":", "");
        string strName = Path.GetFileNameWithoutExtension(strFileName);
        strFileName = strName + strTimeStamp + strExtension;
        string path = Path.Combine(Server.MapPath("../JIT_Bill_PDF_Upload/"), strFileName);
        IdFileUpload.SaveAs(path);
        ViewState["JIT_Bill_PDF_Upload"] = strFileName;
        path = "";
        strFileName = "";
        strName = "";

        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

        SqlCommand cmd = new SqlCommand("Insert_JIT_Bill_Process_MPWLC_HO_TO_MPSCSC_HO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@Region_ID", ddlregion.SelectedValue);
        cmd.Parameters.AddWithValue("@District_ID", ddldistrict.SelectedValue);
        cmd.Parameters.AddWithValue("@Branch_ID", ddlbranch.SelectedValue);
        cmd.Parameters.AddWithValue("@Godown_ID", ddlgodown.SelectedValue);
        cmd.Parameters.AddWithValue("@No_Of_Bill", txtnoofbill.Text.Trim());
        cmd.Parameters.AddWithValue("@Bill_Amount", txtBillAmount.Text.Trim());
        cmd.Parameters.AddWithValue("@Jit_Bill_PDF_Document", ViewState["JIT_Bill_PDF_Upload"]);
        cmd.Parameters.AddWithValue("@Created_By", Session["State_Logid"].ToString());
        cmd.Parameters.AddWithValue("@CreatedBy_IP", ip);
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
        if (TheResult.StartsWith("SUCCESS"))
        {
            string strMsg = "Data Submited Successfully |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            TextClear();
            FillGrid();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
            TextClear();
        }
    }
    protected void TextClear()
    {
        //ddlregion.ClearSelection();
        //ddldistrict.ClearSelection();
        //ddlbranch.ClearSelection();
        ddlgodown.ClearSelection();
        txtnoofbill.Text = "";
        txtBillAmount.Text = "";
    }
    protected void FillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_JIT_Bill_Process_MPWLC_HO_TO_MPSCSC_HO", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdJitbill.DataSource = dt;
                            grdJitbill.DataBind();
                            Div1.Visible = true;
                            grdJitbill.FooterRow.Style.Add("text-align", "center");
                            grdJitbill.FooterRow.Cells[4].Text = "Total";
                            grdJitbill.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_Of_Bill")).ToString();
                            grdJitbill.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Bill_Amount")).ToString();
                            
                        }
                        else
                        {
                            grdJitbill.DataSource = null;
                            grdJitbill.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void grdJitbill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            string ipAddress;
            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddress == "" || ipAddress == null)
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label RowNumber = (Label)row.FindControl("lblRowNumber");
            string Region_ID = (row.FindControl("hdnRegion_ID") as HiddenField).Value;
            string District_ID = (row.FindControl("hdnDistrict_ID") as HiddenField).Value;
            string Branch_ID = (row.FindControl("hdnBranch_ID") as HiddenField).Value;
            string Godown_Name = (row.FindControl("lblGodown_Name") as Label).Text;
            string Godown_ID = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
            string No_Of_Bill = (row.FindControl("lblNo_Of_Bill") as Label).Text;
            string Bill_Amount = (row.FindControl("lblBill_Amount") as Label).Text;
            string fileuploadName = (row.FindControl("hdnDoc") as HiddenField).Value;
            if (fileuploadName != "")
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Send_JIT_Bill_MPWLC_HO_TO_MPSCSC_HO", con1);
                cmd.CommandType = CommandType.StoredProcedure;
                con1.Open();
                cmd.Parameters.AddWithValue("@Region_ID", Region_ID);
                cmd.Parameters.AddWithValue("@District_ID", District_ID);
                cmd.Parameters.AddWithValue("@Branch_ID", Branch_ID);
                cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
                cmd.Parameters.AddWithValue("@No_Of_Bill", No_Of_Bill);
                cmd.Parameters.AddWithValue("@Bill_Amount", Bill_Amount);
                cmd.Parameters.AddWithValue("@Jit_Bill_PDF_Document", fileuploadName);
                cmd.Parameters.AddWithValue("@Send_To_MPSCSC", 'Y');
                cmd.Parameters.AddWithValue("@Created_By", Session["State_Logid"].ToString());
                cmd.Parameters.AddWithValue("@CreatedBy_IP", ipAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Bill Submit To MPSCSC HO |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    ///TextClear();
                    FillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    //TextClear();
                }
            }
        }
        if (e.CommandName == "updateRow")
        {
            string ipAddress;
            ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddress == "" || ipAddress == null)
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label RowNumber = (Label)row.FindControl("lblRowNumber");
            string Godown_Name = (row.FindControl("lblGodown_Name") as Label).Text;
            string Godown_ID = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
            string HO_Remark = (row.FindControl("txtHO_Remark") as TextBox).Text;
            if (HO_Remark != "")
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Update_JIT_Bill_Process_Record", con1);
                cmd.CommandType = CommandType.StoredProcedure;
                con1.Open();
                cmd.Parameters.AddWithValue("@Godown_ID", Godown_ID);
                cmd.Parameters.AddWithValue("@HO_Remark", HO_Remark);
                cmd.Parameters.AddWithValue("@Update_By", Session["State_Logid"].ToString());
                cmd.Parameters.AddWithValue("@UpdateBy_IP", ipAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Bill Update Succesfully |||";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    ///TextClear();
                    FillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    //TextClear();
                }
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
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            Session["hdnId"] = hdnId.ToString();
            RemoveRow(hdnId);
            // RemoveRowJVS(hdnId);

        }
    }
    public void RemoveRow(string id)
    {

        SqlCommand cmd1 = new SqlCommand();
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlCommand cmd = new SqlCommand("Delete_Wrong_JIT_Bill_Entry", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@ID", id.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Delete Record Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                FillGrid();
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
    protected void grdJitbill_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
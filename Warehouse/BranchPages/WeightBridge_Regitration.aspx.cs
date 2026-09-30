using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Net;
using System.Net.Sockets;
using System.Web.UI.WebControls;
using System.Linq;

public partial class BranchPages_WeightBridge_Regitration : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand("usp_GetWeightBridgeByBranch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchID"].ToString());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvWB.DataSource = dt;
                gvWB.DataBind();
            }
        }
    }
    private string GenerateWBID(string branchId, string wbSerial)
    {
        string last2Digit = wbSerial.Length >= 2 ? wbSerial.Substring(wbSerial.Length - 2) : wbSerial.PadLeft(2, '0');
        int autoNo = 0;
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand(@"
            SELECT ISNULL(MAX(CAST(RIGHT(WB_ID,3) AS INT)),0) + 1 
            FROM tbl_WeightBridge_Entry 
            WHERE Branch_ID = @BranchID", con);

            cmd.Parameters.AddWithValue("@BranchID", branchId);

            con.Open();
            autoNo = Convert.ToInt32(cmd.ExecuteScalar());
        }

        return "WB" + branchId + last2Digit + autoNo.ToString("000");
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string branchId = Session["BranchID"].ToString(); // ya dropdown se
        string wbSerial = txtwb_serial.Text.Trim();
        UploadImage();
        string wbId = GenerateWBID(branchId, wbSerial);
        if (!Page.IsValid)
            return;
        if (btnsave.Text == "SUBMIT")
        {
            string fileupload1 = ViewState["UploadImage"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_Insert_WeightBridge", con);
            //SqlCommand cmd = new SqlCommand("SP_Insert_WeightBridge", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Branch_ID", Session["BranchID"].ToString());
            cmd.Parameters.AddWithValue("@WB_Serial_No", txtwb_serial.Text);
            cmd.Parameters.AddWithValue("@WB_Address", txtWBAddress.Text);
            cmd.Parameters.AddWithValue("@Latitude", txtlat.Text);
            cmd.Parameters.AddWithValue("@Longitude", txtlong.Text);
            cmd.Parameters.AddWithValue("@Capacity_MT", txtCapacity.Text);
            cmd.Parameters.AddWithValue("@WB_Status", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@Contact_Person", txtContactPerson.Text);
            cmd.Parameters.AddWithValue("@Mobile_No", txtMobile.Text);
            cmd.Parameters.AddWithValue("@WB_Image", fileupload1);
            cmd.Parameters.AddWithValue("@Created_By", Session["BranchID"].ToString());
            cmd.Parameters.AddWithValue("@CreatedBy_IP", GetLocalIPAddress());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
            if (TheResult.StartsWith("SUCCESS"))
            {
                //BindGrid();
                string strMsg = "Purchase Item Insert Successfully WB ID : " + wbId + " |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                BindGrid();
                TextClear();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
            }

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/BranchPages/WeightBridge_Regitration.aspx");
    }
    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        String ipaddress = string.Empty;
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipaddress = ip.ToString();
            }
        }
        return ipaddress.Length > 0 ? ipaddress : null;
    }
    protected void gvWB_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvWB.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void gvWB_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvWB.EditIndex = -1;
        BindGrid();
    }
    protected void gvWB_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string wbId = gvWB.DataKeys[e.RowIndex].Value.ToString();
        GridViewRow row = gvWB.Rows[e.RowIndex];

        string Serial_No = ((TextBox)row.Cells[1].Controls[0]).Text;
        string status = ((TextBox)row.Cells[2].Controls[0]).Text;
        string lat = ((TextBox)row.Cells[3].Controls[0]).Text;
        string lng = ((TextBox)row.Cells[4].Controls[0]).Text;
        string contact = ((TextBox)row.Cells[5].Controls[0]).Text;
        string mobile = ((TextBox)row.Cells[6].Controls[0]).Text;
        string capacity = ((TextBox)row.Cells[7].Controls[0]).Text;
        string address = ((TextBox)row.Cells[8].Controls[0]).Text;

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand(
                @"UPDATE tbl_WeightBridge_Entry 
                SET WB_Serial_No=@WB_Serial_No,
                    WB_Status=@WB_Status,
                    Latitude=@Latitude,
                    Longitude=@Longitude,
                    Contact_Person=@Contact_Person,
                    Mobile_No=@Mobile_No,
                    Capacity_MT=@Capacity_MT,
                    WB_Address=@WB_Address 
                        WHERE WB_ID=@WB_ID", con);
            cmd.Parameters.AddWithValue("@WB_Serial_No", Serial_No);
            cmd.Parameters.AddWithValue("@WB_Status", status);
            cmd.Parameters.AddWithValue("@Latitude", lat);
            cmd.Parameters.AddWithValue("@Longitude", lng);
            cmd.Parameters.AddWithValue("@Contact_Person", contact);
            cmd.Parameters.AddWithValue("@Mobile_No", mobile);
            cmd.Parameters.AddWithValue("@Capacity_MT", capacity);
            cmd.Parameters.AddWithValue("@WB_Address", address);
            cmd.Parameters.AddWithValue("@WB_ID", wbId);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        gvWB.EditIndex = -1;
        BindGrid();
    }
    protected void gvWB_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string wbId = gvWB.DataKeys[e.RowIndex].Value.ToString();

        //using (SqlConnection con = new SqlConnection(conStr))
        //{
        //    SqlCommand cmd = new SqlCommand(
        //        "DELETE FROM tbl_WeightBridge_Entry WHERE WB_ID=@WB_ID", con);

        //    cmd.Parameters.AddWithValue("@WB_ID", wbId);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //}

        //BindGrid();
        string wbId = gvWB.DataKeys[e.RowIndex].Value.ToString();
        string imagePath = "";

        using (SqlConnection con = new SqlConnection(conStr))
        {
            con.Open();

            // 1️⃣ Get Image Name from DB
            SqlCommand cmdImg = new SqlCommand(
                "SELECT WB_Image FROM tbl_WeightBridge_Entry WHERE WB_ID=@WB_ID", con);
            cmdImg.Parameters.AddWithValue("@WB_ID", wbId);

            object imgObj = cmdImg.ExecuteScalar();
            if (imgObj != null && imgObj.ToString() != "")
            {
                imagePath = imgObj.ToString();
            }

            // 2️⃣ Delete DB Record
            SqlCommand cmdDel = new SqlCommand(
                "DELETE FROM tbl_WeightBridge_Entry WHERE WB_ID=@WB_ID", con);
            cmdDel.Parameters.AddWithValue("@WB_ID", wbId);
            cmdDel.ExecuteNonQuery();
        }

        // 3️⃣ Delete Image from Folder
        if (!string.IsNullOrEmpty(imagePath))
        {
            string fullPath = Server.MapPath("../WB/" + imagePath);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        // 4️⃣ Refresh Grid
        BindGrid();
    }
    protected string UploadImage()
    {
        string result = "";
        try
        {
            string strFileNameR = "", strExtensionR = "", strTimeStampR = "";
            if (FileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {
                int iFailedCntExtR = 0;
                int iFailedCntSizeR = 0;
                string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName).Substring(1);
                strFileNameR = FileUpload1.FileName.ToString();
                strExtensionR = Path.GetExtension(strFileNameR);
                strTimeStampR = DateTime.Now.ToString();
                strTimeStampR = strTimeStampR.Replace("/", "-");
                strTimeStampR = strTimeStampR.Replace(" ", "-");
                strTimeStampR = strTimeStampR.Replace(":", "-");
                string strName = Path.GetFileNameWithoutExtension(strFileNameR);
                strFileNameR = strName + strTimeStampR + strExtensionR;
                string path = Path.Combine(Server.MapPath("../WB/"), strFileNameR);
                FileUpload1.SaveAs(path);
                ViewState["UploadImage"] = strFileNameR;
                //path = "";
                //strFileNameR = "";
                //strName = "";
            }
            else
            {
                string path3 = Path.Combine(Server.MapPath("../WB/"), strFileNameR);
                if (File.Exists(path3))
                {
                    File.Delete(path3);
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Error: " + ex.Message.ToString();
        }
        return result;
    }
    protected void TextClear()
    {
        ddlStatus.ClearSelection();
        txtwb_serial.Text = "";
        txtlat.Text = "";
        txtlong.Text = "";
        txtMobile.Text = "";
        txtContactPerson.Text = "";
        txtCapacity.Text = "";
        txtWBAddress.Text = "";
    }
}
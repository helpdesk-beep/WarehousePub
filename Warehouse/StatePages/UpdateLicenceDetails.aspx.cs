using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class StatePages_UpdateLicenceDetails : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string connStr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDistricts();
            // Pehle purane results hide karein
            pnlWDRA.Visible = false;
            pnlNonWDRA.Visible = false;

        }
    }
    private void FillDistricts()
    {
        try
        {
            using (SqlConnection conDist = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString))
            {
                string query = "SELECT District_Id, District_Name FROM tbl_MetaData_DISTRICT ORDER BY District_Name";
                SqlCommand cmd = new SqlCommand(query, conDist);
                conDist.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                popDistrict.DataSource = rdr;
                popDistrict.DataTextField = "District_Name";
                popDistrict.DataValueField = "District_Id"; // Ya District_Name agar aap naam save kar rahe hain
                popDistrict.DataBind();

                // Default item add karein
                popDistrict.Items.Insert(0, new ListItem("-- Select District --", "0"));
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = "District Load Error: " + ex.Message;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            // 1. Sabse pehle dono panels ko hide karein aur GridViews ko clear karein
            // Taaki naye search par purana data screen se gayab ho jaye
            pnlWDRA.Visible = false;
            pnlNonWDRA.Visible = false;
            gvWDRA.DataSource = null;
            gvWDRA.DataBind();
            gvNonWDRA.DataSource = null;
            gvNonWDRA.DataBind();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Get_Licence_Details_For_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Licencenumber", txtLicenceNo.Text.Trim());
                cmd.Parameters.AddWithValue("@Type", ddllicenceType.SelectedValue);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (ddllicenceType.SelectedValue == "1") // WDRA Logic
                {
                    pnlWDRA.Visible = true; // Panel hamesha dikhayenge taaki 'No Record' message dikhe
                    gvWDRA.DataSource = dt;
                    gvWDRA.DataBind();
                }
                else if (ddllicenceType.SelectedValue == "2") // Non-WDRA Logic
                {
                    pnlNonWDRA.Visible = true;
                    gvNonWDRA.DataSource = dt;
                    gvNonWDRA.DataBind();
                }
                else
                {
                    // Agar user ne dropdown select nahi kiya
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select License Type!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            // Error handling ke liye
            Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
        }
    }
    protected void Edit_Command(object sender, CommandEventArgs e)
    {
        FillDistricts();
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        GridView gv = (ddllicenceType.SelectedValue == "1") ? gvWDRA : gvNonWDRA;

        divMobile.Visible = false;
        divApplicationCode.Visible = false;
        // License Code (Hamesha Cell 1 mein hai)
        string licenseCode = gv.Rows[rowIndex].Cells[1].Text.Replace("&nbsp;", "").Trim();
        hfSelectedId.Value = licenseCode;
        popLicenseCode.Text = licenseCode;

        if (ddllicenceType.SelectedValue == "1") // WDRA Logic
        {
            divMobile.Visible = true; // Mobile No dikhayein
            popWhName.Text = gv.Rows[rowIndex].Cells[2].Text.Replace("&nbsp;", "").Trim();
            //string dist = gv.Rows[rowIndex].Cells[3].Text.Replace("&nbsp;", "").Trim();
            // District logic inside Edit_Command
            string distName = gv.Rows[rowIndex].Cells[ddllicenceType.SelectedValue == "1" ? 3 : 4].Text.Replace("&nbsp;", "").Trim();

            if (!string.IsNullOrEmpty(distName))
            {
                // Pehle purana selection clear karein
                popDistrict.ClearSelection();

                // Agar list mein wo district hai toh use select karein
                ListItem item = popDistrict.Items.FindByText(distName);
                if (item != null)
                {
                    item.Selected = true;
                }
            }
            else
            {
                popDistrict.SelectedIndex = 0; // Agar null hai toh "Select District" dikhaye
            }
            popOwner.Text = gv.Rows[rowIndex].Cells[4].Text.Replace("&nbsp;", "").Trim();

            // Capacity (TemplateField se control find karna)
            Label lblCap = (Label)gv.Rows[rowIndex].FindControl("lblCapacity");
            popCapacity.Text = lblCap != null ? lblCap.Text : "";

            popMobile.Text = gv.Rows[rowIndex].Cells[6].Text.Replace("&nbsp;", "").Trim();
            popIssueDate.Text = gv.Rows[rowIndex].Cells[7].Text.Replace("&nbsp;", "").Trim();
            popValidTill.Text = gv.Rows[rowIndex].Cells[8].Text.Replace("&nbsp;", "").Trim();
        }
        else // Non-WDRA Logic (Yahan index alag ho sakte hain)
        {
            divApplicationCode.Visible = true; // App Code dikhayein
            popAppCode.Text = gv.Rows[rowIndex].Cells[2].Text.Replace("&nbsp;", "").Trim();
            popWhName.Text = gv.Rows[rowIndex].Cells[3].Text.Replace("&nbsp;", "").Trim();
            string dist = gv.Rows[rowIndex].Cells[4].Text.Replace("&nbsp;", "").Trim();
            popCapacity.Text = gv.Rows[rowIndex].Cells[5].Text.Replace("&nbsp;", "").Trim();
            popOwner.Text = gv.Rows[rowIndex].Cells[6].Text.Replace("&nbsp;", "").Trim();
            popIssueDate.Text = gv.Rows[rowIndex].Cells[7].Text.Replace("&nbsp;", "").Trim();
            popValidTill.Text = gv.Rows[rowIndex].Cells[8].Text.Replace("&nbsp;", "").Trim();

        }

        // District Dropdown set karein
        popDistrict.SelectedIndex = 0; // Default
        string currentDist = gv.Rows[rowIndex].Cells[ddllicenceType.SelectedValue == "1" ? 3 : 4].Text.Replace("&nbsp;", "").Trim();
        if (popDistrict.Items.FindByText(currentDist) != null)
        {
            popDistrict.ClearSelection();
            popDistrict.Items.FindByText(currentDist).Selected = true;
        }

        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateModalScript", "showModal();", true);
    }
    protected void btnFinalUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Update_Licence_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // ... (Saare Parameters pass karein jaisa aap kar rahe the) ...
                cmd.Parameters.AddWithValue("@LicenseID", hfSelectedId.Value);
                cmd.Parameters.AddWithValue("@Type", ddllicenceType.SelectedValue);
                cmd.Parameters.AddWithValue("@WhName", popWhName.Text.Trim());
                cmd.Parameters.AddWithValue("@District", popDistrict.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Capacity", popCapacity.Text.Trim());
                cmd.Parameters.AddWithValue("@Owner", popOwner.Text.Trim());
                cmd.Parameters.AddWithValue("@IssueDate", popIssueDate.Text.Trim());
                cmd.Parameters.AddWithValue("@ValidTill", popValidTill.Text.Trim());

                if (ddllicenceType.SelectedValue == "1")
                    cmd.Parameters.AddWithValue("@Mobile", popMobile.Text.Trim());
                if (ddllicenceType.SelectedValue == "2")
                {
                    cmd.Parameters.AddWithValue("@AppCode", popAppCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@District_ID", popDistrict.SelectedValue);
                }
                con.Open();
                int result = cmd.ExecuteNonQuery();

                // Result check: -1 ya > 0 dono success maante hain Stored Procedure mein
                if (result > 0 || result == -1)
                {
                    string strMsg = "Data Updated Successfully|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);

                    // 3. Grid ko hide karein
                    pnlWDRA.Visible = false;
                    pnlNonWDRA.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
}
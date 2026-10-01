using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WLCBusinessLayer;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Globalization;

public partial class Region_Employee_Details : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillDistrict();
            fillDesiugnation();
            fillData();
            fillRegion();
        }
    }

    public void fillRegion()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Region_Name", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RegionID", Session["Regionid"].ToString());
            con.Open();

            ddlregion.DataSource = cmd.ExecuteReader();
            ddlregion.DataTextField = "Regionnm";
            ddlregion.DataValueField = "Region_ID";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, new ListItem("-- Select Region --", "0"));
            con.Close();
        }
    }
    //public void fillRegion()
    //{
    //    ListItem item = new ListItem("Select", "0");

    //    ddlregion.Items.Clear();
    //    ddlregion.Items.Add(item);

    //    DataTable dt = new Common().GetRO();
    //    if (dt.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            item = new ListItem();
    //            item.Value = dt.Rows[i]["Region_Id"].ToString();
    //            item.Text = dt.Rows[i]["region"].ToString();

    //            ddlregion.Items.Add(item);
    //        }


    //    }

    //}
    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDistrict();
    }
    private void fillDistrict()
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("spGetDistrict", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@regid", Session["Regionid"].ToString());
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    ddldistrict.DataSource = dt;
                    ddldistrict.DataTextField = "District_Name";
                    ddldistrict.DataValueField = "District_Id";
                    ddldistrict.DataBind();
                    ddldistrict.Items.Insert(0, "--Select--");
                }
                con.Close();
            }
        }
    }

    public void fillBranch()
    {
        ListItem item = new ListItem("Select", "0");

        ddlbranch.Items.Clear();
        ddlbranch.Items.Add(item);

        DataTable dt = WebsiteLookups.GetBranches(ddldistrict.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ListItem();
                item.Value = dt.Rows[i]["BranchId"].ToString();
                item.Text = dt.Rows[i]["DepotName"].ToString();

                ddlbranch.Items.Add(item);
            }


        }

    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBranch();
    }
    public void fillDesiugnation()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("Get_Designation", con);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        if (dt.Rows.Count > 0)
        {
            DDLdegingnation.DataSource = dt;
            DDLdegingnation.DataTextField = "Designation_Name";
            DDLdegingnation.DataValueField = "id";
            DDLdegingnation.DataBind();
            DDLdegingnation.Items.Insert(0, new ListItem("-- Select Designantion --", "0"));
        }
    }

    private void fillData()
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Employee_Details_Region_Wise", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_ID", Session["Regionid"].ToString());
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    gdImage.DataSource = dt;
                    gdImage.DataBind();
                }
                con.Close();
            }
        }
    }
    public void Clear()
    {
        ddldistrict.SelectedValue = "0";
        ddlbranch.SelectedValue = "0";
        lblname.Text = "";
        DDLdegingnation.SelectedValue = "0";
        txtdob.Text = "";
        txtdoj.Text = "";
        txtmobileno.Text = "";
        txtEmail.Text = "";
    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        string randomnum = string.Format("{0:ddMMyyHHmmss}", DateTime.Now);
        string str = FileUpload1.FileName;
        FileUpload1.PostedFile.SaveAs(Server.MapPath("../Upload/" + str));
        string image = "../Upload/" + str.ToString();
        string DistictID = ddldistrict.SelectedValue;
        string BranchID = ddlbranch.SelectedValue;
        string EmpNameH = lblname.Text;
        string degingnation = DDLdegingnation.SelectedValue;
        string DOB = txtdob.Text;
        string DOJ = txtdoj.Text;
        string Mobile = txtmobileno.Text;
        string Email = txtEmail.Text;

        string filePath = FileUpload1.PostedFile.FileName;
        string fileName = Path.GetFileName(filePath + "_" + randomnum);
        string fileName1 = Path.GetFileName(filePath);
        string ext = Path.GetExtension(fileName1);
        string type = String.Empty;


        if (FileUpload1.HasFile)
        {
            try
            {
                switch (ext) // this switch code validate the files which allow to upload only PDF file   
                {
                    case ".JPG":
                        type = "application/JPG";
                        break;
                    case ".jpg":
                        type = "application/jpg";
                        break;
                    case ".PNG":
                        type = "application/PNG";
                        break;
                    case ".png":
                        type = "application/png";
                        break;
                    case ".JPEG":
                        type = "application/JPEG";
                        break;
                    case ".jpeg":
                        type = "application/jpeg";
                        break;
                    case ".GIF":
                        type = "application/GIF";
                        break;
                    case ".gif":
                        type = "application/gif";
                        break;
                }
                if (type != String.Empty)
                {

                    Stream fs = FileUpload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs); //reads the binary files  
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                    using (SqlConnection con = new SqlConnection(constr))
                    {

                        SqlCommand cmd = new SqlCommand("insert_employee_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@DistrictID", DistictID.ToString());
                        cmd.Parameters.AddWithValue("@BranchID", BranchID.ToString());
                        cmd.Parameters.AddWithValue("@EmpName", EmpNameH.ToString());
                        cmd.Parameters.AddWithValue("@DOB", getDate_MDY(DOB.ToString()));
                        cmd.Parameters.AddWithValue("@DOJ", getDate_MDY(DOJ.ToString()));
                        cmd.Parameters.AddWithValue("@Designation", degingnation.ToString());
                        cmd.Parameters.AddWithValue("@Mobile", Mobile.ToString());
                        cmd.Parameters.AddWithValue("@Email", Email.ToString());
                        cmd.Parameters.AddWithValue("@Image", image.Trim());
                        cmd.Parameters.AddWithValue("@Office_Type", ddlrole.SelectedValue);
                        cmd.Parameters.AddWithValue("@Region_ID", ddlregion.SelectedValue);
                        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                        if (TheResult.StartsWith("SUCCESS"))
                        {
                            string strMsg = "Record Save Successfully |||";

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                            fillData();
                            Clear();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                        }

                    }
                }
                else
                {
                    //lblErr.ForeColor = System.Drawing.Color.Red;
                    //lblErr.Text = "Select Only jpg/jpeg/png or gif Files!!!"; // if file is other than speified extension   
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Only jpg/jpeg/png or gif Files!!!');", true);
                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message.ToString() + "');", true);
                //lblErr.Text = "Error: " + ex.Message.ToString();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Upload your Image');", true);
            //lblErr.Text = "Please Upload your Image";
            //lblErr.ForeColor = System.Drawing.Color.Red;
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
    protected void ddlrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrole.SelectedValue.ToString() == "1")
        {
            fillRegion();
            fillDistrict();
            divdistrict.Visible = true;
            divBranch.Visible = false;
        }
        else if (ddlrole.SelectedValue.ToString() == "2")
        {
            fillRegion();
            fillDistrict();
            fillBranch();
            divdistrict.Visible = true;
            divBranch.Visible = true;
        }
        else
        {
            divdistrict.Visible = false;
            divBranch.Visible = false;
        }
    }
    protected void gdImage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Overallinsp")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            //Reference the GridView Row.
            GridViewRow row = gdImage.Rows[rowIndex];
            string hdnid = (row.FindControl("hdnid") as HiddenField).Value;
            Session["hdnid"] = hdnid.ToString();
            DeleteEmployeeDetails(hdnid);

        }
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DataRowView dr = (DataRowView)e.Row.DataItem;
        //    string imageUrl = "http://localhost:49791/" + Convert.ToBase64String((byte[])dr["Image"]);
        //    (e.Row.FindControl("Image1") as Image).ImageUrl = imageUrl;
        //}
    }
    public void DeleteEmployeeDetails(string id)
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

            SqlCommand cmd = new SqlCommand("Delete_Employee_Details", con);
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
                fillData();
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
}
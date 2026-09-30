using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class StatePages_UpdateLicNoDate : System.Web.UI.Page
{
    private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["State_Logid"] != null && Session["State_Logid"].ToString() != "")
        {
            if (!IsPostBack)
            {
                getdistrict();
                fillGodownType();
            }
        }
        else { Response.Redirect("~/login.aspx"); }
    }

    public void getdistrict()
    {
        string qry = "SELECT District_Name, District_Id FROM tbl_MetaData_DISTRICT ORDER BY District_Name";
        FillDropDown(DropDownList1, qry, "District_Name", "District_Id");
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string qry = "SELECT DepotName, BranchId FROM tbl_MetaData_DEPOT WHERE DistrictId ='" + DropDownList1.SelectedValue + "' ORDER BY DepotName";
        FillDropDown(ddlBranch, qry, "DepotName", "BranchId");
    }

    private void FillDropDown(DropDownList ddl, string query, string text, string value)
    {
        using (SqlDataAdapter da = new SqlDataAdapter(query, con))
        {
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddl.DataSource = ds;
            ddl.DataTextField = text;
            ddl.DataValueField = value;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e) { BindGrid(); }

    protected void btnSearch_Click(object sender, EventArgs e) { BindGrid(txtSearch.Text.Trim()); }

    private void BindGrid(string searchId = "")
    {
        string qry = "SELECT Godown_ID, Godown_Name, Hired_Type, Storage_Type, Godown_Capacity, Closing_Balance, LicNum, convert(varchar(10), LicDate, 103) as LicDate, Godown_Scientific_Capacity " +
                     "FROM tbl_metadata_godown_2018 WHERE IsActive='Y'";

        if (!string.IsNullOrEmpty(searchId))
            qry += " AND Godown_ID = '" + searchId + "'";
        else
            qry += " AND BranchID='" + ddlBranch.SelectedValue + "'";

        using (SqlDataAdapter da = new SqlDataAdapter(qry, con))
        {
            DataSet ds = new DataSet();
            da.Fill(ds);
            Depositor_Gridview.DataSource = ds;
            Depositor_Gridview.DataBind();
        }
    }

    protected void Display(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

        txtGdwnID.Text = ((Label)row.FindControl("lblGodown_ID")).Text;
        lblgodownname.Text = ((Label)row.FindControl("lblGodown_Name")).Text;
        txtMaxCpt.Text = ((Label)row.FindControl("lblGodown_Capacity")).Text;
        txtscieCPT.Text = ((Label)row.FindControl("lblGodown_Scientific_Capacity")).Text;
        txtclosing.Text = ((Label)row.FindControl("lblClosing_Balance")).Text;
        txtlicno.Text = ((Label)row.FindControl("lblLicNum")).Text;
        txtlicdate.Text = ((Label)row.FindControl("lblLicDate")).Text;

        // Correctly handling the Hired Type label from the grid
        string hired = ((Label)row.FindControl("lblHired_Type")).Text;
        if (ddllst_hired.Items.FindByValue(hired) != null)
        {
            ddllst_hired.ClearSelection();
            ddllst_hired.SelectedValue = hired;
        }

        string storage = ((Label)row.FindControl("lblStorage_Type")).Text;
        if (ddllst_storage.Items.FindByValue(storage) != null)
        {
            ddllst_storage.ClearSelection();
            ddllst_storage.SelectedValue = storage;
        }

        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }

    protected void btnAddCompany_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("Update_Gdn_Name_Licence", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID", txtGdwnID.Text);
                cmd.Parameters.AddWithValue("@Branch_ID", ddlBranch.SelectedValue);
                cmd.Parameters.AddWithValue("@Godown_Name", lblgodownname.Text);
                cmd.Parameters.AddWithValue("@Closing_Balance", txtclosing.Text.Trim());
                cmd.Parameters.AddWithValue("@LicNum", txtlicno.Text.Trim());
                cmd.Parameters.AddWithValue("@LicDate", getDate_MDY(txtlicdate.Text.Trim()));
                cmd.Parameters.AddWithValue("@Hired_Type", ddllst_hired.SelectedValue);
                cmd.Parameters.AddWithValue("@Storage_Type", ddllst_storage.SelectedValue);
                cmd.Parameters.AddWithValue("@Godown_Scientific_Capacity", txtscieCPT.Text.Trim());
                cmd.Parameters.AddWithValue("@Godown_Capacity", txtMaxCpt.Text.Trim());

                SqlParameter outputParam = new SqlParameter("@TheResult", SqlDbType.VarChar, 250) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outputParam);

                cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + outputParam.Value.ToString() + "');", true);
                BindGrid();
            }
        }
        catch (Exception ex) { ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ex.Message + "');", true); }
        finally { con.Close(); }
    }

    protected string getDate_MDY(string inDate)
    {
        DateTime dt = DateTime.ParseExact(inDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        return dt.ToString("MM/dd/yyyy");
    }

    private void fillGodownType()
    {
        string query = "SELECT GodownType FROM GodownTypeMaster";
        using (SqlDataAdapter da = new SqlDataAdapter(query, con))
        {
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddllst_hired.DataSource = ds;
            ddllst_hired.DataTextField = "GodownType";
            ddllst_hired.DataValueField = "GodownType";
            ddllst_hired.DataBind();
            ddllst_hired.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
}
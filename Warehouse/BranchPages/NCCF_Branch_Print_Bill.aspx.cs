using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

public partial class BranchPages_NCCF_Branch_Print_Bill : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    public string qry = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                GetBranch();
                Fillgodown();
                ClearAllBillData(); // Clear all bill data on page load
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    public void GetBranch()
    {
        try
        {
            qry = "select DepotName,BranchId from tbl_MetaData_DEPOT where BranchId='" + Session["BranchId"].ToString() + "' order by DepotName";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtbranch.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                Session["DepotName"] = ds.Tables[0].Rows[0]["DepotName"].ToString();
            }
            else
            {
                txtbranch.Text = "";
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            txtbranch.Text = "";
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    protected void Fillgodown()
    {
        try
        {
            ddlgdwn.Items.Clear(); // Clear existing items

            qry = "select Godown_name + ' ('+ Godown_ID +')' as Godown_name, Godown_ID from tbl_metadata_godown_2018 where BranchID='" + Session["BranchId"].ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgdwn.DataSource = ds.Tables[0];
                ddlgdwn.DataTextField = "Godown_name";
                ddlgdwn.DataValueField = "Godown_ID";
                ddlgdwn.DataBind();
                ddlgdwn.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlgdwn.Items.Insert(0, "--No Godown Found--");
            }
        }
        catch (Exception ex)
        {
            ddlgdwn.Items.Clear();
            ddlgdwn.Items.Insert(0, "--Error--");
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    protected void ddlgdwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Clear all previous data first
            ClearBillDetails();
            ClearBillNumberDropdown();

            if (ddlgdwn.SelectedIndex > 0) // If a valid godown is selected (not "--Select--")
            {
                string qry = "select distinct RN.Bill_Number from tbl_Bill_Institution_Daily_Charges_NCCF RN " +
                            "inner join tbl_Institution_NCCF_Storage_Bill_Details DN on RN.Bill_Number = DN.Bill_Number " +
                            "where DN.Godown_Id = '" + ddlgdwn.SelectedValue.ToString() + "'";

                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlactualbillno.Enabled = true;
                    ddlactualbillno.DataSource = ds.Tables[0];
                    ddlactualbillno.DataTextField = "Bill_Number";
                    ddlactualbillno.DataValueField = "Bill_Number";
                    ddlactualbillno.DataBind();
                    ddlactualbillno.Items.Insert(0, "--Select--");
                }
                else
                {
                    // No data found
                    ddlactualbillno.Items.Clear();
                    ddlactualbillno.Items.Insert(0, "--No Bill Found--");
                    ddlactualbillno.Enabled = false;

                    // Hide bill section
                    divrent.Visible = false;
                }
            }
            else
            {
                // If "--Select--" is selected, clear everything
                ClearBillNumberDropdown();
                ClearBillDetails();
                divrent.Visible = false;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            ClearBillNumberDropdown();
            ClearBillDetails();
            divrent.Visible = false;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    public void GetBillData()
    {
        try
        {
            // Clear existing grid data first
            GD1.DataSource = null;
            GD1.DataBind();

            if (ddlactualbillno.SelectedIndex > 0 && !string.IsNullOrEmpty(ddlactualbillno.SelectedValue))
            {
                qry = "SELECT convert(varchar(10), BDSC.Dates, 103) as Date, " +
                      "BDSC.Opening_Weight as Opening_Weight, " +
                      "BDSC.Receive_Weight as Receive_Weight, " +
                      "BDSC.Issue_Weight as Issue_Weight, " +
                      "BDSC.Closing_Weight as Closing_Weight, " +
                      "BDSC.Per_Day_Rate as Per_Day_Rate, " +
                      "BDSC.Total_Charges as Total_Charges " +
                      "FROM tbl_Bill_Institution_Daily_Charges_NCCF as BDSC " +
                      "WHERE BDSC.Bill_Number = '" + ddlactualbillno.SelectedValue.ToString() + "'";

                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GD1.DataSource = ds;
                    GD1.DataBind();
                    divrent.Visible = true;

                    // Calculate total in footer
                    DataTable dt = ds.Tables[0];
                    GD1.FooterRow.Cells[1].Text = "महायोग :-";

                    decimal total1 = dt.AsEnumerable().Sum(row => row.Field<Decimal>("Total_Charges"));
                    GD1.FooterRow.Cells[7].Text = total1.ToString("N2");
                }
                else
                {
                    // No data found
                    GD1.DataSource = null;
                    GD1.DataBind();
                    divrent.Visible = false;
                }
            }
            else
            {
                // No bill selected
                GD1.DataSource = null;
                GD1.DataBind();
                divrent.Visible = false;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            GD1.DataSource = null;
            GD1.DataBind();
            divrent.Visible = false;
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    public void GetBillOtherData()
    {
        try
        {
            // Clear all labels first
            ClearBillLabels();

            if (ddlactualbillno.SelectedIndex > 0 && !string.IsNullOrEmpty(ddlactualbillno.SelectedValue))
            {
                qry = "select (CONVERT(varchar(10), b.BId) + '/' + CONVERT(varchar(10), b.Created_Date, 105)) as ReceiptNo, " +
                      "b.Bill_Number, b.District_Id, b.Branch_Id, mg.Godown_ID, " +
                      "GodownNum as Godown_No, mg.Godown_APN as Godown_Owner, mg.Godown_Name, " +
                      "cast((mg.Godown_Scientific_Capacity/10) as int) as Godown_Scientific_Capacity, " +
                      "mg.Hired_Type, b.Commodity_Id, " +
                      "(select a.Commodity_Name from tbl_MetaData_STORAGE_COMMODITY as a where a.Commodity_Id = b.Commodity_Id) as Commodity_Name, " +
                      "(SELECT DateName(mm, DATEADD(mm, b.Month, -1)) as [MonthName]) as Month, " +
                      "CONVERT(varchar(10), b.From_Date, 103) as FromDate, " +
                      "CONVERT(varchar(10), b.To_Date, 103) as ToDate, " +
                      "b.Financial_Year, b.Commodity_Rate, b.Net_Amount, b.Sub_Amount, " +
                      "b.Service_Tax_Perc, b.Service_Tax_Amt, b.Rebate_Perc, b.Rebate_Amt, " +
                      "b.Per_Day_Rate, b.Crop_Year " +
                      "from tbl_Institution_NCCF_Storage_Bill_Details as b " +
                      "inner join tbl_MetaData_GODOWN_2018 as mg on mg.Godown_ID = b.Godown_Id " +
                      "where b.Bill_Number = '" + ddlactualbillno.SelectedValue.ToString() + "'";

                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    lblgdwnname.Text = dt.Rows[0]["Godown_Name"].ToString();
                    lblcmd.Text = dt.Rows[0]["Commodity_Name"].ToString();
                    lblbillmonth.Text = dt.Rows[0]["Month"].ToString();
                    lblbillno.Text = dt.Rows[0]["Bill_Number"].ToString();
                    lblGdnum.Text = dt.Rows[0]["Godown_ID"].ToString(); // Using Godown_ID
                    lblbranch.Text = Session["DepotName"] != null ? Session["DepotName"].ToString() : "";
                }
                else
                {
                    ClearBillLabels();
                }
            }
            else
            {
                ClearBillLabels();
            }
        }
        catch (Exception ex)
        {
            ClearBillLabels();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }
    }

    // Helper method to clear bill number dropdown
    private void ClearBillNumberDropdown()
    {
        ddlactualbillno.Items.Clear();
        ddlactualbillno.Items.Insert(0, "--Select--");
        ddlactualbillno.Enabled = true;
    }

    // Helper method to clear all bill labels
    private void ClearBillLabels()
    {
        lblgdwnname.Text = "";
        lblcmd.Text = "";
        lblbillmonth.Text = "";
        lblbillno.Text = "";
        lblGdnum.Text = "";
        lblbranch.Text = Session["DepotName"] != null ? Session["DepotName"].ToString() : "";
    }

    // Helper method to clear bill details (grid and visibility)
    private void ClearBillDetails()
    {
        GD1.DataSource = null;
        GD1.DataBind();
        divrent.Visible = false;
    }

    // Helper method to clear all bill data completely
    private void ClearAllBillData()
    {
        ClearBillLabels();
        ClearBillDetails();
        // Don't clear dropdowns here as they need to show data
    }

    protected void ddlactualbillno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlactualbillno.SelectedIndex > 0)
        {
            GetBillData();
            GetBillOtherData();
        }
        else
        {
            ClearBillDetails();
            ClearBillLabels();
        }
    }

    protected void btnviewbill_Click(object sender, EventArgs e)
    {
        if (ddlactualbillno.SelectedIndex > 0)
        {
            GetBillData();
            GetBillOtherData();
        }
        else
        {
            // Show message or clear data
            ClearBillDetails();
            ClearBillLabels();
            // Optional: Show alert
            // ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a bill number.');", true);
        }
    }
}
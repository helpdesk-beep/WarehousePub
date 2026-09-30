using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.UI;

public partial class Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDetails();
        }
    }

    private void FillDetails()
    {
        // Safe default values
        SetDefaultValues();
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            using (SqlCommand cmd = new SqlCommand("Get_Branch_Godown_Details_For_Website", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds);
                    if (HasRow(ds, 0))
                    {
                        lblbranchcount.Text = GetSafeInteger(ds.Tables[0].Rows[0]["TotalBranch"]);
                    }
                    if (HasRow(ds, 1))
                    {
                        lblTotalGodown.Text = GetSafeInteger(ds.Tables[1].Rows[0]["TotalGodown"]);
                        lblTotalcapacity.Text = GetSafeDecimal(ds.Tables[1].Rows[0]["TotalCapacity"]);
                    }
                    if (HasRow(ds, 2))
                    {
                        lblTotalownedGodown.Text = GetSafeInteger(ds.Tables[2].Rows[0]["OTotalGodown"]);
                        lblOwnedGodownCapcity.Text = GetSafeDecimal(ds.Tables[2].Rows[0]["OTotalCapacity"]);
                    }
                    if (HasRow(ds, 3))
                    {
                        lblPrivateGodown.Text = GetSafeInteger(ds.Tables[3].Rows[0]["PTotalGodown"]);
                        lblPrivateGodownCapacity.Text = GetSafeDecimal(ds.Tables[3].Rows[0]["PTotalCapacity"]);
                    }
                }
            }
        }
        catch (SqlException)
        {
            SetDefaultValues();
        }
        catch (Exception)
        {
            SetDefaultValues();
        }
    }

    /// <summary>
    /// Checks whether requested DataTable exists
    /// and contains at least one row.
    /// </summary>
    private bool HasRow(DataSet ds, int tableIndex)
    {
        return ds != null && ds.Tables.Count > tableIndex && ds.Tables[tableIndex] != null && ds.Tables[tableIndex].Rows.Count > 0;
    }

    /// <summary>
    /// Only allows integer values from database.
    /// Any HTML/JavaScript value will fail validation.
    /// </summary>
    private string GetSafeInteger(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "0";
        }

        long result;

        if (long.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture),NumberStyles.Integer,CultureInfo.InvariantCulture,out result))
        {
            return result.ToString(CultureInfo.InvariantCulture);
        }

        return "0";
    }

    /// <summary>
    /// Only allows numeric/decimal values.
    /// Prevents arbitrary HTML/JavaScript from being rendered.
    /// </summary>
    private string GetSafeDecimal(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return "0";
        }

        decimal result;

        if (decimal.TryParse(
            Convert.ToString(value, CultureInfo.InvariantCulture),
            NumberStyles.Any,
            CultureInfo.InvariantCulture,
            out result))
        {
            return result.ToString(
                "0.##",
                CultureInfo.InvariantCulture);
        }

        return "0";
    }

    /// <summary>
    /// If future database fields contain actual text,
    /// use this before displaying the value.
    /// </summary>
    private string GetSafeText(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return string.Empty;
        }

        return HttpUtility.HtmlEncode(
            Convert.ToString(value));
    }

    private void SetDefaultValues()
    {
        lblbranchcount.Text = "0";

        lblTotalGodown.Text = "0";
        lblTotalcapacity.Text = "0";

        lblTotalownedGodown.Text = "0";
        lblOwnedGodownCapcity.Text = "0";

        lblPrivateGodown.Text = "0";
        lblPrivateGodownCapacity.Text = "0";
    }
}
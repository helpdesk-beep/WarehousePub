using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class BranchPages_WebBridgeMappingWithGodown : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString();
    SqlCommand cmd = new SqlCommand();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["BranchId"] == null)
        {
            Response.Redirect("~/Login.aspx", true);
            return;
        }
        if (!IsPostBack)
        {
            Fillgodown();
            BindWeightBridge();
            BindMappingGrid();
        }
    }

    // 🔹 Godown Dropdown
    protected void Fillgodown()
    {
        string qry = "";
        qry = "select gdn.Godown_Name,gdn.Godown_ID from tbl_MetaData_GODOWN_2018 gdn INNER JOIN tbl_MetaData_DEPOT MD ON gdn.BranchID=MD.BranchId INNER JOIN tbl_MetaData_DISTRICT dst ON MD.DistrictId=dst.District_Id INNER JOIN ( Select distinct GodownID from tbl_storage_Depositor_WHR_Relation where CropYear='2025-26' and Commodity_Id='22' and Arrival_Source='01' )tt ON gdn.Godown_ID=tt.GodownID INNER JOIN Godown_WeightBridge_Details WD ON gdn.Godown_ID=WD.Godown_Id Where MD.BranchId='" + Session["BranchId"].ToString() + "' And gdn.Godown_ID NOT IN (Select Godown_ID From tbl_Godown_WB_Mapping where WB_ID!='--Select--')";
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
    }

    // 🔹 Weight Bridge Dropdown
    private void BindWeightBridge()
    {
        string qry = "";
        qry = "SELECT WB_ID, WB_Name FROM tbl_WeightBridge_Entry Where Branch_ID='" + Session["BranchId"].ToString() + "' ORDER BY WB_Name";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlWB.DataSource = ds.Tables[0];
            ddlWB.DataTextField = "WB_Name";
            ddlWB.DataValueField = "WB_ID";
            ddlWB.DataBind();
            ddlWB.Items.Insert(0, "--Select--");
        }
    }

    // 🔹 Save Mapping
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        using (SqlConnection conn = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand(
                @"INSERT INTO tbl_Godown_WB_Mapping
                  (Godown_ID, WB_ID)
                  VALUES (@Godown_ID, @WB_ID)", con);

            cmd.Parameters.AddWithValue("@Godown_ID", ddlgdwn.SelectedValue);
            cmd.Parameters.AddWithValue("@WB_ID", ddlWB.SelectedValue);

            con.Open();
            cmd.ExecuteNonQuery();
            BindMappingGrid();
        }
        //lblMsg.Text = "✔ Godown and Weight Bridge mapped successfully!";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('✔ Godown and Weight Bridge mapped successfully!');", true);
        ddlgdwn.SelectedIndex = 0;
        ddlWB.SelectedIndex = 0;
    }
    private void BindMappingGrid()
    {
        using (SqlConnection conn = new SqlConnection(conStr))
        {
            string qry = @"
            SELECT m.Mapping_ID,g.Godown_name,w.WB_Name FROM tbl_Godown_WB_Mapping m INNER JOIN tbl_metadata_godown_2018 g ON m.Godown_ID = g.Godown_ID
            INNER JOIN tbl_WeightBridge_Entry w ON m.WB_ID = w.WB_ID WHERE g.BranchID = @BranchID ORDER BY m.Mapping_ID DESC";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvMapping.DataSource = dt;
            gvMapping.DataBind();
        }
    }
    protected void gvMapping_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveMapping")
        {
            int mappingId = Convert.ToInt32(e.CommandArgument);
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM tbl_Godown_WB_Mapping WHERE Mapping_ID = @Mapping_ID",
                    conn);
                cmd.Parameters.Add("@Mapping_ID", SqlDbType.Int).Value = mappingId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            //lblMsg.Text = "❌ Mapping removed successfully";
            //lblMsg.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('❌ Mapping removed successfully!');", true);
            BindMappingGrid(); // Refresh grid
        }
    }
}
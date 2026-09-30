using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_PrintGodownMasterByID : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlTransaction sqltrans;
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Depot_DepotID"].ToString() != "")
            {
                string depotId = Session["Depot_DepotID"].ToString();
                GetGodown(depotId);
            }
        }
    }
    private void GetGodown(string depotId)
    {
        try
        {
            string BranchId = Session["BranchID"].ToString();
            string qry = "select GD.Godown_ID,GD.Godown_Name,GD.Godown_Capacity,GD.Godown_Scientific_Capacity,GD.Hired_Type,GD.Storage_Type,CONVERT(varchar(10),GD.LicDate,103) as Licence_Validity,GD.LicNum as Licence_No from dbo.tbl_MetaData_GODOWN_2018 as GD where GD.BranchId='" + BranchId + "' and GD.Godown_ID in (select Godown_ID from tbl_MetaData_GODOWN where BranchID='" + BranchId + "') AND CONVERT(DATE,GD.CreatedDate,103)>='2021-03-22' order by GD.Godown_Name";

            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddllGodown.DataSource = ds.Tables[0];
                ddllGodown.DataTextField = "Godown_Name";
                ddllGodown.DataValueField = "Godown_ID";
                ddllGodown.DataBind();
                ddllGodown.Items.Insert(0, "---Select---");

            }
        }
        catch (Exception)
        {
            //////
        }
    }


    protected void ddllGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_GODOWN_For_Print", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID", ddllGodown.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Session["GodownID"] = ddllGodown.SelectedValue;
                            Response.Redirect("~/Masters/PrintGodownMaster.aspx");
                        }
                        else
                        {
                            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown Not Found'); </script> ");
                        }
                    }
                }
            }
        }
    }
}
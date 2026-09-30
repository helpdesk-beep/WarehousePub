using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WarehouseLevel_WLC_Procurement_ANB_Acceptance_Details_For_Delete : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGodown();
            GetData();
        }
    }

    void FillGodown()
    {
        try
        {
            string qry = @"select Godown_ID,Godown_Name 
                           from tbl_MetaData_GODOWN_2018 
                           where BranchID=@BranchID
                           order by Godown_Name";

            SqlCommand cmd = new SqlCommand(qry, con);

            cmd.Parameters.AddWithValue("@BranchID", Session["BranchID"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            ddlGodown.DataSource = dt;
            ddlGodown.DataTextField = "Godown_Name";
            ddlGodown.DataValueField = "Godown_ID";
            ddlGodown.DataBind();

            ddlGodown.Items.Insert(0, "--All Godown--");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    void GetData()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("Get_ANB_Acceptance_Details_For_Delete", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BranchID", Session["BranchID"].ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            if (ddlGodown.SelectedIndex > 0)
            {
                DataView dv = dt.DefaultView;

                dv.RowFilter = "Godown_ID='" + ddlGodown.SelectedValue + "'";

                gvDetails.DataSource = dv.ToTable();
            }
            else
            {
                gvDetails.DataSource = dt;
            }

            gvDetails.DataBind();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        GetData();
    }

    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetData();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            foreach (GridViewRow row in gvDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDelete");

                HiddenField hf = (HiddenField)row.FindControl("hfAcceptanceNo");

                if (chk.Checked)
                {
                    sb.Append(hf.Value + ",");
                }
            }

            if (sb.Length > 0)
            {
                string AcceptanceNos = sb.ToString().TrimEnd(',');

                SqlCommand cmd = new SqlCommand("Delete_AcceptanceNo_Details", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AcceptanceNo", AcceptanceNos);

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();

                ScriptManager.RegisterClientScriptBlock(this,this.GetType(),"msg","alert('Record Deleted Successfully');",true);
                GetData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this,this.GetType(),"msg","alert('Please Select Record');",true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this,this.GetType(),"msg","alert('" + ex.Message.Replace("'", "") + "');",true);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
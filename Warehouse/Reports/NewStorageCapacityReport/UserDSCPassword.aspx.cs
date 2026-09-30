using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class UserDSCPassword : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string type = ddlType.SelectedValue;
        string userId = txtUserId.Text.Trim();

        if (type == "" || userId == "")
        {
            return;
        }

        string query = "";

        if (type == "Godown")
        {
            query = "select [login_id], Godown_Name as User_Name, [Password], [Godown_ID] as User_Id, [DistrictId], [BranchID], [Access_Restrict], GodownTypeId from Pvt_Warehouse_Login where [Godown_ID] = '"+userId+"'";
        }
        else if (type == "Branch")
        {
            query = "Select login_id, User_Name, Password, DistrictId, Fname, Lname, Scope, Access_Restrict, BranchID as User_Id from Storage_Login where BranchID = '"+userId+"'";
        }
        else if (type == "ROAC")
        {
            query = "select User_Name, Password, MR.Region_Id as User_Id, '' as DistrictId, '' as BranchID from RegionState_Login as R inner join tbl_MetaData_Region as MR on MR.region = R.User_Name where MR.Region_Id = '"+userId+"'";
        }
        else if (type == "RM")
        {
            query = @"select User_Name, Lname as Password, MR.Region_Id as User_Id, '' as DistrictId, '' as BranchID from RegionState_Login as R inner join tbl_MetaData_Region as MR on MR.region = R.User_Name where MR.Region_Id = '"+userId+"'";
        }

        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    lblMsg.Text = "❌ No Record Found!";
                    GridView1.Visible = false;
                    return;
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < GridView1.Columns.Count; i++)
            {
                string headerText = GridView1.HeaderRow.Cells[i].Text.Trim().ToLower();

                if (headerText == "password" || headerText == "user_id" || headerText == "user id")
                {
                    e.Row.Cells[i].CssClass = "highlightCell";
                }
            }
        }
    }

}

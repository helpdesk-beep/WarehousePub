using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class District_Rpt_District_Wise_Fumigation : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string con_WLC1 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlCommand cmd = new SqlCommand();
    SqlCommand cmd1 = new SqlCommand();
    DataTable dt = new DataTable();
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null))
        {
            if (!IsPostBack)
            {
                txtdistrict.Text = Session["UserName"].ToString();
                GetBranch();
                fillgridForBranch();
            }
        }

    }
    public void GetBranch()
    {
        string qry = "";
        qry = "select distinct DepotID,DepotName from tbl_MetaData_DEPOT where DistrictId='" + Session["Depot_DistID"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlbranch.DataSource = ds.Tables[0];
            ddlbranch.DataTextField = "DepotName";
            ddlbranch.DataValueField = "DepotID";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {

        }
    }
    public void fillgridForBranch()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Rpt_Branch_Wise_Stack_Wise_Fumigation_For_State", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@District_Id", Session["Depot_DistID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdbranch.DataSource = dt;
                            grdbranch.DataBind();
                            divBranch.Visible = true;
                            divStack.Visible = false;
                            grdbranch.FooterRow.Style.Add("text-align", "right");
                            grdbranch.FooterRow.Cells[1].Text = "Total";
                            grdbranch.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("Total_Stack")).ToString();
                            grdbranch.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("Total_Fumigated")).ToString();
                            grdbranch.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("Pending_Stack_For_Fumigation")).ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Payment Received .');", true);
                            grdbranch.DataSource = null;
                            grdbranch.DataBind();
                        }
                    }
                }
            }
        }
    }
    public void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Rpt_Stack_Wise_Fumigation_For_District", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlbranch.SelectedValue == "ALL")
                {
                    cmd.Parameters.AddWithValue("@Branch_Id", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Branch_Id", ddlbranch.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GrdStack.DataSource = dt;
                            GrdStack.DataBind();
                            divStack.Visible = true;
                            divBranch.Visible = false;
                            GrdStack.FooterRow.Style.Add("text-align", "right");
                            GrdStack.FooterRow.Cells[2].Text = "Total";
                            GrdStack.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("Total_Stack")).ToString();
                            GrdStack.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("Total_Fumigated")).ToString();
                            GrdStack.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<Int32>("Pending_Stack_For_Fumigation")).ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Payment Received .');", true);
                            GrdStack.DataSource = null;
                            GrdStack.DataBind();
                        }
                    }

                    //cmd.Connection = con;
                    //sda.SelectCommand = cmd;
                    //using (DataSet DS = new DataSet())
                    //{
                    //    sda.Fill(DS);
                    //    if (DS.Tables[0].Rows.Count > 0)
                    //    {
                    //        GrdStack.DataSource = DS.Tables[0];
                    //        GrdStack.DataBind();
                    //        divStack.Visible = true;
                    //        divBranch.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        grdbranch.DataSource = DS.Tables[0];
                    //        grdbranch.DataBind();
                    //        divStack.Visible = true;
                    //        divBranch.Visible = false;
                    //    }
                    //}
                }
            }
        }


    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbranch.SelectedItem.ToString() == "All")
        {
            fillgridForBranch();
        }
        else
        {
            fillgrid();
        }
    }
}
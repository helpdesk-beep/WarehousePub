using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;
using System.Net.Http;
using System.Text;

public partial class Masters_Add_Godown_in_AePDS : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                GetDist();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_For_Add_to_AePDS_For_State", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BranchID", ddl_branch.SelectedValue);
               // cmd.Parameters.AddWithValue("@Godown_ID", txtgodownid.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            godown_GridView.DataSource = dt;
                            godown_GridView.DataBind();
                            btnDeleteRecord.Visible = true;
                        }
                        else
                        {
                            godown_GridView.DataSource = null;
                            godown_GridView.DataBind();
                            btnDeleteRecord.Visible = false;
                        }
                    }
                }
            }
        }
    }

    protected void godown_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = (DataSet)Session["dsGodown"];
        godown_GridView.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void godown_GridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = godown_GridView.SelectedRow;
    }
    //protected void Edit(object sender, EventArgs e)
    //{
    //    using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
    //    {
    //        String Godown_ID = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
    //        String Godown_Name = (row.FindControl("hdngodownname") as HiddenField).Value;
    //        String Branch_ID = (row.FindControl("hdnbranchid") as HiddenField).Value;
    //        //AddGodown();
    //        AddGodown(Godown_ID, Godown_Name, Branch_ID);
    //    }

    //}
    //protected async void AddGodown()
    //{
    //    var client = new HttpClient();
    //    var requestbody = "{" + "\"godown_id\"" + ": " + "\"2328003030152\"" + "," + "\"godown_name\"" + ": " + "\"Test Godown Don't Use\"" + "," + "\"depot_id\"" + ": " + "\"232800303\"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";
    //    var requestJson = new StringContent(requestbody, Encoding.UTF8, "application/json");
    //    HttpResponseMessage response = await client.PostAsync("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown", requestJson);
    //    var content = await response.Content.ReadAsStringAsync(); 
    //}
    //protected async void AddGodown(string GodownID,string GodownName,)
    protected async void AddGodown(string GodownID, string GodownName, string BranchID)
    //protected async void AddGodown()
    {
        //var client = new HttpClient();
        //var requestbody = "{" + "\"godown_id\"" + ": " + "\"2328003030152\"" + "," + "\"godown_name\"" + ": " + "\"Test Godown Don't Use\"" + "," + "\"depot_id\"" + ": " + "\"232800303\"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";
        //var requestJson = new StringContent(requestbody, Encoding.UTF8, "application/json");
        //HttpResponseMessage response = await client.PostAsync("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown", requestJson);
        //var content = await response.Content.ReadAsStringAsync();
        Uri myUri = new Uri("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown", UriKind.Absolute);
        //WebClient client = new WebClient();
        //client.OpenRead(myUri);

        var client = new HttpClient();
        var requestbody = "{" + "\"godown_id\"" + ": " + "\"" + GodownID + "\"" + "," + "\"godown_name\"" + ": " + "\"" + GodownName + "\"" + "," + "\"depot_id\"" + ": " + "\"" + BranchID + "\"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";
        //var requestbody = "{" + "\"godown_id\"" + ": " + "\"23350040060\"" + "," + "\"godown_name\"" + ": " + "\"19 PMS PUSHPA WAREHOUSE\"" + "," + "\"depot_id\"" + ": " + "\"2328001\"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(requestbody)", true);

        var requestJson = new StringContent(requestbody, Encoding.UTF8, "application/json");
        //HttpResponseMessage response = await client.PostAsync("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown", requestJson);
        HttpResponseMessage response = await client.PostAsync(myUri, requestJson);

        var content = await response.Content.ReadAsStringAsync();
    }
    protected void ddl_dist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepot(ddl_dist.SelectedValue.ToString());
    }
    private void GetDepot(string DistID)
    {
        string strDist = "";

        strDist = "SELECT Depotname,BranchID FROM tbl_MetaData_Depot where DistrictID='" + DistID + "' order by Depotname";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_branch.DataSource = ds.Tables[0];
            ddl_branch.DataTextField = "Depotname";
            ddl_branch.DataValueField = "BranchID";
            ddl_branch.DataBind();
            ddl_branch.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_branch.Items.Insert(0, "--Select--");
        }
    }
    private void GetDist()
    {
        string strDist = "";
        strDist = "SELECT District_Name,District_Id FROM tbl_MetaData_DISTRICT order by District_Name";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_dist.DataSource = ds.Tables[0];
            ddl_dist.DataTextField = "District_Name";
            ddl_dist.DataValueField = "District_Id";
            ddl_dist.DataBind();
            ddl_dist.Items.Insert(0, "--Select--");
        }
        else
        {
            ddl_dist.Items.Insert(0, "--Select--");
        }
    }
    protected void btnDeleteRecord_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow grow in godown_GridView.Rows)
        {
            //Searching CheckBox("chkDel") in an individual row of Grid  
            CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
            //If CheckBox is checked than delete the record with particular empid  
            if (chkdel.Checked)
            {
                String Branch_ID = Convert.ToString(grow.Cells[4].Text);
                String Godown_ID = Convert.ToString(grow.Cells[5].Text);
                String Godown_Name = Convert.ToString(grow.Cells[6].Text);

                con.Open();
                cmd = new SqlCommand("[dbo].[Add_Godown_to_AePDS_MPWLC]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GodownID", Godown_ID.ToString());
                cmd.Parameters.AddWithValue("@godown_name", Godown_Name.ToString());
                cmd.Parameters.AddWithValue("@depot_id", Branch_ID.ToString());
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))

                {
                    count++;
                    AddGodown(Godown_ID, Godown_Name, Branch_ID);
                }
                conStr.Close();

                //AddGodownMPWLC(Godown_ID, Godown_Name, Branch_ID);
            }
        }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Godown Add Successfully..')", true);
                fillgrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT Delete')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            conStr.Close();
        }
    }

    //public void AddGodownMPWLC(string GodownID, string GodownName, string BranchID)
    //{
    //    SqlCommand cmd1 = new SqlCommand();
    //    if (conStr.State == ConnectionState.Closed)
    //    {
    //        conStr.Open();
    //    }
    //    try
    //    {
    //        string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
    //        if (conStr.State == ConnectionState.Closed)
    //        {
    //            conStr.Open();
    //        }

    //        SqlCommand cmd = new SqlCommand("Add_Godown_to_AePDS_MPWLC", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@GodownID", GodownID.ToString());
    //        cmd.Parameters.AddWithValue("@godown_name", GodownName.ToString());
    //        cmd.Parameters.AddWithValue("@depot_id", BranchID.ToString());
    //        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
    //        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //        cmd.ExecuteNonQuery();
    //        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

    //        if (TheResult.StartsWith("SUCCESS"))
    //        {
    //            string strMsg = "Godown Add Successfully|||";

    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
    //            fillgrid();
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string strMsg2 = ex.Message.ToString();
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
    //    }


    //}
}

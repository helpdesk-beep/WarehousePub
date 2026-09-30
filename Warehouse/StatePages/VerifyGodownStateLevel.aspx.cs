using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;


public partial class StatePages_VerifyGodownStateLevel : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string qry = "";
    SqlCommand cmd = null;
    DataSet ds = null;
    SqlDataAdapter da = null;
    SqlTransaction sqltrans;
    string Bill_Type = "";
    string Ref_Number = "";
    string Ref_Aid = "";
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                fillDistrict();
                fillgrid();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void fillDistrict()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC")
            {
                query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            }
            else
            {
                if (Session["RoleId"].ToString() == "2")
                {
                    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where Region_ID='" + Session["Region_ID"].ToString() + "' order by District_Name asc";
                }
                else
                {
                    query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] where District_Id ='" + Session["Depot_DistID"].ToString() + "' order by District_Name asc";

                }
            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "--Select--");


            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void fillIssuecenter()
    {
        try
        {
            string region = "";
            if (Session["UserName"].ToString() != "MPSWLC")
            {

                //if (Session["Region_ID"].ToString() != null)
                //{
                //    region = Session["Region_ID"].ToString();

                //}
            }

            string query = "";
            if (Session["UserName"].ToString() == "MPSWLC" || Session["Region_ID"].ToString() != null)
            {
                query = "SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "'";
            }
            else
            {
                query = "  SELECT DepotID,DistrictId,DepoTypeID,DepotName,BranchId FROM [tbl_MetaData_DEPOT] where DistrictId='" + ddlDistrict.SelectedValue + "' and DepoTypeID='4'";

            }
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.DataSource = ds.Tables[0];
                ddlbranch.DataTextField = "DepotName";
                ddlbranch.DataValueField = "BranchId";
                ddlbranch.DataBind();
                ddlbranch.Items.Insert(0, "--Select--");

            }
            else
            {
                ////
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillIssuecenter();
    }


    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_For_Verification", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (ddlbranch.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@BranchID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BranchID", ddlbranch.SelectedValue);
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
                            godown_GridView.DataSource = dt;
                            godown_GridView.DataBind();
                        }
                        else
                        {
                            godown_GridView.DataSource = null;
                            godown_GridView.DataBind();
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
    protected void Edit(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            String Godown_ID = (row.FindControl("hdnGodown_ID") as HiddenField).Value;
            String Godown_Name = (row.FindControl("hdngodownname") as HiddenField).Value;
            String Branch_ID = (row.FindControl("hdnbranchid") as HiddenField).Value;
            // Verify(Godown_ID);
            //AddGodown();
            RemoveGodown(Godown_ID);
        }

    }
    //protected async void AddGodown()
    //{
    //    var client = new HttpClient();
    //    var requestbody = "{" + "\"godown_id\"" + ": " + "\"2328003030152\"" + "," + "\"godown_name\"" + ": " + "\"Test Godown Don't Use\"" + "," + "\"depot_id\"" + ": " + "\"232800303\"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";
    //    var requestJson = new StringContent(requestbody, Encoding.UTF8, "application/json");
    //    HttpResponseMessage response = await client.PostAsync("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown", requestJson);
    //    var content = await response.Content.ReadAsStringAsync(); 
    //}
    //protected async void AddGodown(string GodownID,string GodownName,)
    protected async void RemoveGodown(string GodownID)
    //protected async void AddGodown()
    {
        //var client = new HttpClient();
        //var requestbody = "{" + "\"godown_id\"" + ": " + "\"2328003030152\"" + "," + "\"godown_name\"" + ": " + "\"Test Godown Don't Use\"" + "," + "\"depot_id\"" + ": " + "\"232800303\"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";
        //var requestJson = new StringContent(requestbody, Encoding.UTF8, "application/json");
        //HttpResponseMessage response = await client.PostAsync("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown", requestJson);
        //var content = await response.Content.ReadAsStringAsync();
        Uri myUri = new Uri("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/godownstatus", UriKind.Absolute);
        //WebClient client = new WebClient();
        //client.OpenRead(myUri);

        var client = new HttpClient();
        var requestbody = "{" + "\"godown_id\"" + ": " + "\"" + GodownID + "\"" + "," + "\"status\"" + ": " + "\" D \"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";
        //var requestbody = "{" + "\"godown_id\"" + ": " + "\"23350040060\"" + "," + "\"godown_name\"" + ": " + "\"19 PMS PUSHPA WAREHOUSE\"" + "," + "\"depot_id\"" + ": " + "\"2328001\"" + "," + "\"password\"" + ": " + "\"d18b40d204f55b6f92f44c6e847a7a9df34c4ed503cf6085cb006b3422038743\"" + "}";

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(requestbody)", true);

        var requestJson = new StringContent(requestbody, Encoding.UTF8, "application/json");
        //HttpResponseMessage response = await client.PostAsync("https://scm.mp.gov.in/getgodowndetails/GodownService/GodownApp/newgodown", requestJson);
        HttpResponseMessage response = await client.PostAsync(myUri, requestJson);

        var content = await response.Content.ReadAsStringAsync();
    }
    protected void Delete(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            String Godown_ID = (row.FindControl("hdnGodown_ID2") as HiddenField).Value;
            Deleted(Godown_ID);
        }
    }
    public void Verify(String Godown_ID)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con != null)
        {
            con.Open();
            cmd = new SqlCommand("Verify_Godown", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GodownID", Godown_ID);
            cmd.Parameters.AddWithValue("@VerifyBy", ip);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown Verified Successfully..'); </script> ");
                fillgrid();


            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown NOT Verified '); </script> ");
            }
        }
    }

    public void Deleted(String Godown_ID)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (con != null)
        {
            con.Open();
            cmd = new SqlCommand("Delete_Godown_before_Verify", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GodownID", Godown_ID);
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown Deleted Successfully..'); </script> ");
                fillgrid();
            }
            else
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Godown NOT Delete '); </script> ");
            }
        }
    }
}
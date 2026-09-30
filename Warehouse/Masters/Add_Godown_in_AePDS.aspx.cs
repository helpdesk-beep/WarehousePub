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
            using (SqlCommand cmd = new SqlCommand("Get_Godown_For_Add_to_AePDS", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Godown_ID", txtgodownid.Text.ToString());
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());
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
            //AddGodown();
            AddGodown(Godown_ID, Godown_Name, Branch_ID);
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
  
}

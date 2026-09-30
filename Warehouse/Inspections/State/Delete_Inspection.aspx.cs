using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

public partial class Inspections_State_Delete_Inspection : System.Web.UI.Page
{
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //  public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["Inspectioncon_JVSing"].ToString());
    public SqlConnection con_JVS = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    SqlCommand cmd;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        // Session["UserName"].ToString() = Session["UserName"].ToString();
        if (!IsPostBack)
        {
            GetRegionFrom();
            //fillInspection();
        }

    }
    protected void fillInspection()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Inspection_Details_For_Delete_HO", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Region_Id", ddlregion.SelectedValue);
                //cmd.Parameters.AddWithValue("@PF_ID", PFID);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GrdOfficerPreviousInsp.DataSource = dt;
                            GrdOfficerPreviousInsp.DataBind();
                            // lblTotalInsp.Text = Convert.ToString(dt.Rows[0].Count);
                            //this.GrdOfficerPreviousInsp.Columns[12].Visible = false;
                            //this.GrdOfficerPreviousInsp.Columns[13].Visible = false;
                            divinspection.Visible = true;
                        }
                        else
                        {

                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                            // lblTotalInsp.Text = "0";
                        }
                    }
                }
            }
        }
    }
    public void RemoveRow(string id)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (conStr.State == ConnectionState.Closed)
        {
            conStr.Open();
        }
        try
        {


            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();
            }

            SqlCommand cmd = new SqlCommand("Delete_Inspection", conStr
                );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Inspection Deleted Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillInspection();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
    protected void GrdOfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdOfficerPreviousInsp.Rows[rowIndex];

            //Fetch value of Name.
            string hdnid = (row.FindControl("hdnid") as HiddenField).Value;
            string hdnInspection_ID = (row.FindControl("hdnInspection_ID") as HiddenField).Value;
            Session["hdnId"] = hdnid.ToString();
            RemoveRow(hdnid);
        }
    }
    private void GetRegionFrom()
    {
        string strDist = "";
        strDist = "Select Region_Id,region from tbl_MetaData_Region order by region";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlregion.DataSource = ds.Tables[0];
            ddlregion.DataTextField = "region";
            ddlregion.DataValueField = "Region_Id";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlregion.Items.Insert(0, "--Select--");
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        fillInspection();
    }
}
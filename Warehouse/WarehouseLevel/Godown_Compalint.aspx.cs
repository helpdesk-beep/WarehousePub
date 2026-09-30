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

public partial class WarehouseLevel_PvtGodownProfile : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();
    public string LicDate = "";
    //public string WManagerId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lang"].ToString() == "Hindi")
        {
            
        }

       
        if (Session["UserName"] != null && Session["GodownID_New"]!=null)
        {
            string user = Session["UserName"].ToString();
        }

        if (!IsPostBack)
        {
            fillGrid();
        }
    }
    protected void fillGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Complaint_For_Godown", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@GodownID", Session["GodownID_New"].ToString());
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
                        }
                        else
                        {

                            GrdOfficerPreviousInsp.DataSource = null;
                            GrdOfficerPreviousInsp.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string CS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insert_Compalint", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@Godown_ID", Session["GodownID_New"].ToString());
                cmd.Parameters.AddWithValue("@Remark", txtremark.InnerText);
                cmd.Parameters.AddWithValue("@IP_Address", IPAddress);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Complaint Details Successfully submitted|||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    txtremark.InnerText = "";
                    fillGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }
}

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

public partial class Inspections_State_DeleteScheduleInspection : System.Web.UI.Page
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
            fillInpOff_Grid();
        }

    }
    public void fillInpOff_Grid()
    {
        string strsql = "select PF_ID,Officer_Name,Designation,Rec_Office,CUG_mobileNo from tbl_metadata_Inspection_officer where IsActive='Y' ORDER BY Officer_Name";
        SqlCommand cmd = new SqlCommand(strsql, con_JVS);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Gridview_IsnpOff.DataSource = ds;
            Gridview_IsnpOff.DataBind();
            lblOfficerList.Text = Convert.ToString(ds.Tables[0].Rows.Count);
            txtSearch_IsnpOff.Visible = true;   // <-- yaha true ho jayega
        }
        else
        {
            Gridview_IsnpOff.DataSource = null;
            Gridview_IsnpOff.DataBind();
            lblOfficerList.Text = "0";
        }
    }
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Gridview_IsnpOff.Rows[rowIndex];

        Session["S_PFID"] = (row.FindControl("hdnpfid") as HiddenField).Value;
        //GetDist(Session["UserName"].ToString());
        //txtInspOffName.Text = (row.FindControl("lbname") as Label).Text;
        //txtDesig.Text = (row.FindControl("lbldesignation") as Label).Text; ;
        //txtCug.Text = (row.FindControl("lblcugmobile") as Label).Text;

        //fillScheduleInsp_Grid(Session["S_PFID"].ToString());
        GetEmployeeDetails();
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }
    protected void Gridview_IsnpOff_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridViewRow gvr = Gridview_IsnpOff.SelectedRow;
        //Session["S_PFID"] = gvr.Cells[0].Text;
        //GetDist(Session["UserName"].ToString());
        //txtInspOffName.Text = gvr.Cells[1].Text;
        //txtDesig.Text = gvr.Cells[2].Text;
        //txtCug.Text = gvr.Cells[4].Text;
        //fillScheduleInsp_Grid(gvr.Cells[0].Text);
        //divNewInsp.Visible = true;
    }

   protected void GetEmployeeDetails()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Employee_Details_For_Inspection_Deletion", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Employee_ID", Session["S_PFID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            Gridview_OfficerPreviousInsp.DataSource = ds;
                            Gridview_OfficerPreviousInsp.DataBind();
                            lblTotalInsp.Text = Convert.ToString(ds.Tables[0].Rows.Count);

                        }
                        else
                        {
                            Gridview_OfficerPreviousInsp.DataSource = null;
                            Gridview_OfficerPreviousInsp.DataBind();
                            lblTotalInsp.Text = "0";
                        }
                    }
                }
            }
        }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/1919";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            return converted;
        }
    }
    public string chkAID(string PFID)
    {
        string MaxAID = "";
        string QueryMax = "select MAX(AID) as AID from tbl_Inpection_Scheduled_Date where PF_ID='" + PFID + "'";
        SqlCommand cmd = new SqlCommand(QueryMax, con_JVS);
        string str3 = cmd.ExecuteScalar().ToString();
        if ((str3 != String.Empty) || str3 != "")
        {
            MaxAID = Convert.ToString(Convert.ToInt32(str3) + 1);
        }
        else
        {
            MaxAID = Convert.ToString(1);
        }
        return MaxAID;
    }

    protected void Gridview_OfficerPreviousInsp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void Gridview_OfficerPreviousInsp_RowCommand(object sender, GridViewCommandEventArgs e)
    {       
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = Gridview_OfficerPreviousInsp.Rows[rowIndex];

            //Fetch value of Name.
            string hdnId = (row.FindControl("hdnId") as HiddenField).Value;
            string hdnEmpID = (row.FindControl("hdnEmpID") as HiddenField).Value;
            Session["hdnId"] = hdnId.ToString();
            RemoveRow(hdnId, hdnEmpID);
            ModalPopupExtender1.Show();

        }
    }
    protected void Gridview_OfficerPreviousInsp_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/ScheduleInspection.aspx");
    }

   public void RemoveRow(string id, string hdnEmpid)
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

            SqlCommand cmd = new SqlCommand("Employee_Data_Remove_Rows", conStr
                );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id.ToString());
            cmd.Parameters.AddWithValue("@Employee_ID", hdnEmpid.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                GetEmployeeDetails();
                ModalPopupExtender1.Show();
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
}
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BranchPages_BM_Fumigation_Entry : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    SqlDataAdapter da = new SqlDataAdapter();
    Decimal TT1, TT2, TT3;
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["BranchId"] != null))
        {
            if (!IsPostBack)
            {
                txtbranch.Text = Session["UserName"].ToString();
                fillGodown();
            }
        }
    }
    private void fillGodown()
    {
        try
        {

            string query = "";

            //query = "SELECT Commodity_Id,Commodity_Name FROM  dbo.tbl_MetaData_STORAGE_COMMODITY";
            query = "Select Godown_ID,Godown_Name from tbl_MetaData_GODOWN_2018 Where BranchID ='" + Session["BranchId"].ToString() + "' Order By Godown_Name ASC";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddlGodown.DataSource = ds.Tables[0];
                ddlGodown.DataTextField = "Godown_Name";
                ddlGodown.DataValueField = "Godown_ID";
                ddlGodown.DataBind();
                ddlGodown.Items.Insert(0, "Select");
            }
            else
            {
                ddlGodown.Items.Clear();
                ddlGodown.Items.Insert(0, "Select");
            }
        }
        catch (Exception)
        {
            //////
        }
    }
    protected void ddlGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Stack_Wise_Fumigation_Data", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Godown_ID", ddlGodown.SelectedValue);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grdFumigation.DataSource = dt;
                            grdFumigation.DataBind();
                            Div1.Visible = true;
                        }
                        else
                        {
                            grdFumigation.DataSource = null;
                            grdFumigation.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void grdFumigation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            Label RowNumber = (Label)row.FindControl("lblRowNumber");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
            string qry = "";
            int ICount = 0;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            con.Open();
            string Branch_ID = Session["BranchId"].ToString();
            string Godown_ID = ddlGodown.SelectedValue;
            string Stack_ID = (row.FindControl("lblStack_ID") as Label).Text;
            string Stack_Name = (row.FindControl("lblStack_Name") as Label).Text;
            string Stack_capacity = (row.FindControl("lblStack_capacity") as Label).Text;
            string Fumigation_Date = (row.FindControl("txtDate") as TextBox).Text;
            string Remark = (row.FindControl("txtRemark") as TextBox).Text;
            if (Fumigation_Date != "")
            {
                qry = "INSERT INTO tbl_Stack_Wise_Fumigation_By_Branch(Branch_Id,Godown_ID,Stack_ID,Stack_Name,Stack_capacity,Fumigation_Date,Remark,Created_By,Createdby_Ip,Created_on) values('" + Branch_ID + "','" + Godown_ID + "','" + Stack_ID + "','" + Stack_Name + "','" + Stack_capacity + "','" + getDate_MDY(Fumigation_Date) + "','" + Remark + "','" + Session["BranchId"].ToString() + "','" + ip + "',getdate())";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                cmd2.ExecuteNonQuery();
                string strMsg = "Data Submit Successfully |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillgrid();
            }
            else
            {
                string strMsg = "Please Select Date |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            }
        }
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd/MM/yyyy");
            return converted;
        }
    }
}
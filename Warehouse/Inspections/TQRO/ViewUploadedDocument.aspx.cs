using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class Inspections_TQRO_ViewUploadedDocument : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
    SqlCommand cmd;
    DataTable dt = new DataTable();
    string PFID = "";
    string branchid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["University_E"] == null)
        //{
        //    Response.Redirect("/VC_Application/Default.aspx");
        //}
        PFID = Session["UserId"].ToString();
       // branchid = Session["hdnbranchid"].ToString();
        if (!IsPostBack)
        {
           
            Fill12thmarksheetdata();
        }
    }
    public void Fill12thmarksheetdata()
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("View_Document_For_TQRO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@BranchID", Session["hdnbranchid"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeID", Session["hdnemployeeid"].ToString());
        cmd.Parameters.AddWithValue("@QuaterID", Session["hdnquatertype"].ToString());
        cmd.Parameters.AddWithValue("@InspectionTypeID", Session["hdnVerificationType"].ToString());
        cmd.Parameters.AddWithValue("@FinancialYear", Session["hdnfinancialyear"].ToString());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (con.State == ConnectionState.Open)
        { con.Close(); }
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
   protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {       
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GridView1.Rows[rowIndex];

            //Fetch value of Name.
            string hdnid = (row.FindControl("hdnid") as HiddenField).Value;
            Session["hdnid"] = hdnid.ToString();

        }
    }
}
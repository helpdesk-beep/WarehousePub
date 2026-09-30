using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WarehouseLevel_NewStackMaster_W : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["GodownID_New"] as string))
        {
            if (!IsPostBack)
            {
                if (Session["GodownID_New"] == null)
                {
                    FillGridView();
                }
                else
                {
                    FillGridViewgodown();

                }
            }
        }
        else
        {
            Response.Redirect("../login.aspx ");
        }
    }
    public void FillGridView()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string str = "SELECT [Godown_ID],[Godown_Name],(SELECT COUNT([Stack_ID]) FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_STACK] WHERE tbl_MetaData_STACK.Godown_ID=tbl_MetaData_GODOWN.Godown_ID and tbl_MetaData_STACK.Stack_Killed = 'N') as countstak FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where BranchID='" + Session["BranchId"] + "' and Remarks='Y' order by Godown_Name";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            else
            { 
            
            }
        }
        catch(Exception)
        { 
        
        }
    
    }
    int total1 = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            total1 += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "countstak"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmount1 = (Label)e.Row.FindControl("lbl_Stackcount");
            lblAmount1.Text = total1.ToString();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "b")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                string GodownId = Convert.ToString(GridView1.DataKeys[row.RowIndex].Value);

                if (GodownId != "")
                {
                    Session["GodownId"] = GodownId;
                    Response.Redirect("~/WarehouseLevel/StackMaster_W.aspx");
                }
                else
                {

                }

            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
    }
    public void FillGridViewgodown()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //string str = "SELECT [Godown_ID],[Godown_Name],(SELECT COUNT([Stack_ID]) FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_STACK] WHERE tbl_MetaData_STACK.Godown_ID=tbl_MetaData_GODOWN.Godown_ID and tbl_MetaData_STACK.Stack_Killed = 'N') as countstak FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where BranchID='" + Session["BranchId"] + "' and Remarks='Y' and Godown_ID='" + Session["GodownID_New"].ToString() + "' order by Godown_Name";
            string str = "SELECT [Godown_ID],[Godown_Name],(SELECT COUNT([Stack_ID]) FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_STACK] WHERE tbl_MetaData_STACK.Godown_ID=tbl_MetaData_GODOWN.Godown_ID and tbl_MetaData_STACK.Stack_Killed = 'N') as countstak FROM [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN] where Remarks='Y' and Godown_ID='" + Session["GodownID_New"].ToString() + "' order by Godown_Name";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            else
            {

            }
        }
        catch (Exception)
        {

        }

    }
}
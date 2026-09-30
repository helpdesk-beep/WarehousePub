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
using System.Security.Principal;

public partial class Inspections_RO_Ro_Received_Insecticide_Entry : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = null;
    DataSet ds = null;
    public string qry = "";
    //public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    private object con;
    private object ob_value;

    public object GridView1 { get; private set; }
    public Label HiddenField { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["UserId"] != null))
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    private void FillGrid()
    {
         SqlCommand cmdd = new SqlCommand("Sp_Get_Ro_Received_Insecticide_Distributed_By_HO", con_WLC);
        cmdd.CommandType = CommandType.StoredProcedure;
        cmdd.Parameters.AddWithValue("@Region_ID", Session["UserId"].ToString());
        //conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GVOfStock.DataSource = dt;
            GVOfStock.DataBind();
        }
    }

    
    protected void GVOfStock_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Overallinsp")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GVOfStock.Rows[rowIndex];
            //Fetch value of Name.           
            string hdnID = (row.FindControl("hdnID") as HiddenField).Value;
            string hdninsID = (row.FindControl("hdninsID") as HiddenField).Value;
            string hdnunitid = (row.FindControl("hdnunitid") as HiddenField).Value;
            string ob_quantity = (row.FindControl("ob_quantity") as Label).Text;
            string ob_market_value = (row.FindControl("ob_market_value") as Label).Text;
            string ob_value = (row.FindControl("ob_value") as Label).Text;
            string ob_Expriy = (row.FindControl("ob_Expriy") as Label).Text;
            Session["hdnID"] = hdnID.ToString();
            Session["hdninsID"] = hdninsID.ToString();
            Session["hdnunitid"] = hdnunitid.ToString();
            Session["ob_quantity"] = ob_quantity.ToString();
            Session["ob_market_value"] = ob_market_value.ToString();
            Session["ob_value"] = ob_value.ToString();
            Session["ob_Expriy"] = ob_Expriy.ToString();
            //Response.Redirect("~/Inspections/RO/Ro_Received_Insecticide_Submit.aspx");
            Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('/Warehouse/Inspections/RO/Ro_Received_Insecticide_Submit.aspx','_newtab');", true);
        }
    }
    protected void GVOfStock_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        GVOfStock.EditIndex = e.NewEditIndex;
        FillGrid();
    }
    protected void GVOfStock_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {

        //HiddenField id = GVOfStock.Rows[e.RowIndex].FindControl("hdnpfid") as HiddenField;
        ////TextBox Unit_Name = GVOfStock.Rows[e.RowIndex].FindControl("lbl_Unit_Name") as TextBox;
        //TextBox ob_quantity = GVOfStock.Rows[e.RowIndex].FindControl("txt_ob_quantity") as TextBox;
        //TextBox ob_market_value = GVOfStock.Rows[e.RowIndex].FindControl("txt_op_market_value") as TextBox;
        //TextBox ob_value = GVOfStock.Rows[e.RowIndex].FindControl("txt_ob_value") as TextBox;

        //con_WLC.Open();
        ////updating the record  
        //SqlCommand cmd = new SqlCommand("SP_Get_Fertilizer_update_Entry", con_WLC);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@ID", id.Value);
        ////cmd.Parameters.AddWithValue("@op_market_value", Unit_Name.Text);
        //cmd.Parameters.AddWithValue("@Opening_Balance_quantity", ob_quantity.Text);
        //cmd.Parameters.AddWithValue("@Opening_Balance_market_value", ob_market_value.Text);
        //cmd.Parameters.AddWithValue("@Opening_Balance_value", ob_value.Text);
        //cmd.ExecuteNonQuery();
        //con_WLC.Close();
        ////Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        //GVOfStock.EditIndex = -1;
        ////Call ShowData method for displaying updated data  
        //FillGrid();
    }
    protected void GVOfStock_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        GVOfStock.EditIndex = -1;


    }


    protected void GVOfStock_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GVOfStock.EditIndex = -1;
        FillGrid();
    }

  
}

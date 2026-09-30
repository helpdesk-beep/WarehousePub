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

public partial class Inspections_State_Ho_To_Rm_Tranfer_Entry : System.Web.UI.Page
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
                clear();
                GetRegionFrom();
                GetRegionTo();
                //btnUpdate.Visible = false;
                if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
                {
                    FillGrid();
                    //btnUpdate.Visible = false;
                }
                if (Session["hdnID"] != null)
                {
                    if (!String.IsNullOrEmpty(Session["hdnID"].ToString()))
                    {
                        //FillDataforUpdate();

                    }
                }
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    private void GetRegionFrom()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlregion.DataSource = ds.Tables[0];
            ddlregion.DataTextField = "Regionnm";
            ddlregion.DataValueField = "Region_ID";
            ddlregion.DataBind();
            ddlregion.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlregion.Items.Insert(0, "--Select--");
        }
    }

    private void GetRegionTo()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist,con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlregionTo.DataSource = ds.Tables[0];
            ddlregionTo.DataTextField = "Regionnm";
            ddlregionTo.DataValueField = "Region_ID";
            ddlregionTo.DataBind();
            ddlregionTo.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlregion.Items.Insert(0, "--Select--");
        }
    }
    private void FillGrid()
    {
        //SqlCommand cmdd = new SqlCommand("SP_Ho_Get_Fertilizer_Entry", con_WLC);
        SqlCommand cmdd = new SqlCommand("Sp_Get_Ho_To_Rm_Fertilizer_Entry", con_WLC);
        cmdd.CommandType = CommandType.StoredProcedure;
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlinsecticide.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('select ')", true);
            }
            SqlCommand cmd = new SqlCommand("Sp_Ho_To_Rm_Fertilizer_Entry", con_WLC);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Region", ddlregion.SelectedValue);
            cmd.Parameters.AddWithValue("@RegionTo", ddlregionTo.SelectedValue);
            cmd.Parameters.AddWithValue("@Ho_Dispatch_Date", getDate_MDY(txtDispatchDate.Text));
            cmd.Parameters.AddWithValue("@Ho_Order_No",txtOrderNo.Text);
            cmd.Parameters.AddWithValue("@Insecticide_ID", ddlinsecticide.SelectedValue);
            cmd.Parameters.AddWithValue("@Opening_Balance_quantity", txtQuantity.Text);
            cmd.Parameters.AddWithValue("@Opening_Balance_market_value", txtMarketRed.Text);
            cmd.Parameters.AddWithValue("@Opening_Balance_value", txtValue.Value);
            cmd.Parameters.AddWithValue("@Unit", ddlUnitStock.SelectedValue);
            cmd.Parameters.AddWithValue("@created_by", Request.UserHostAddress);
            con_WLC.Open();
            cmd.ExecuteNonQuery();
            con_WLC.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Save Sucessfully')", true);
            FillGrid();
            clear();
        }
        catch (Exception ex)
        {
            string except = ex.Message.ToString();

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + except + "')", true);
        }
        finally
        {
            con_WLC.Close();
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
    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlinsecticide.SelectedValue == "0")
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('select ')", true);
    //        }

    //        SqlCommand cmd = new SqlCommand("SP_Ho_Get_Fertilizer_update_Entry", con_WLC);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@ID", Session["hdnID"].ToString());
    //        cmd.Parameters.AddWithValue("@Date_Purchase", getDate_MDY(txtPurchase.Text));
    //        cmd.Parameters.AddWithValue("@Opening_Balance_quantity", txtQuantity.Text);
    //        cmd.Parameters.AddWithValue("@Opening_Balance_value", txtValue.Value);
    //        cmd.Parameters.AddWithValue("@Opening_Balance_market_value", txtMarketRed.Text);
    //        cmd.Parameters.AddWithValue("@update_by", Request.UserHostAddress);
    //        cmd.Parameters.AddWithValue("@Date_Expriy", getDate_MDY(txtExpriy.Text));
    //        con_WLC.Open();
    //        cmd.ExecuteNonQuery();
    //        con_WLC.Close();
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Update Sucessfully')", true);
    //        btnUpdate.Visible = false;
    //        Session["hdntblID"] = "0";
    //        btnSubmit.Visible = true;
    //        FillGrid();
    //        clear();
    //    }
    //    catch (Exception ex)
    //    {
    //        string except = ex.Message.ToString();

    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + except + "')", true);
    //    }
    //    finally
    //    {
    //        con_WLC.Close();
    //    }
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    public void clear()
    {
        txtOrderNo.Text = "";
        ddlinsecticide.SelectedValue = "0";
        ddlUnitStock.SelectedValue = "0";
        //ddlregion.SelectedValue = "0";
        //ddlregionTo.SelectedValue="0";
        txtQuantity.Text = "";
        txtMarketRed.Text = "";
        txtValue.Value = "";
        txtDispatchDate.Text = "";
    }


    protected void txtMarketRed_TextChanged(object sender, EventArgs e)
    {
        decimal val1 = Convert.ToDecimal(txtQuantity.Text);
        decimal val2 = Convert.ToDecimal(txtMarketRed.Text);
        decimal val3 = val1 * val2;
        txtValue.Value = val3.ToString();
    }
    //public void FillDataforUpdate()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("SP_Ho_Get_Fertilizer_Entry_ID", con))
    //        {
    //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@ID", Session["hdnID"].ToString());
    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;
    //                using (DataTable dt = new DataTable())
    //                {
    //                    sda.Fill(dt);
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        ddlinsecticide.SelectedValue = dt.Rows[0]["Insecticide_ID"].ToString();
    //                        // ddlinsecticide.Enabled = true;
    //                        ddlUnitStock.SelectedValue = dt.Rows[0]["Unit"].ToString();
    //                        // ddlUnitStock.Enabled = true;
    //                        txtPurchase.Text = dt.Rows[0]["Date_Purchase"].ToString();
    //                        txtQuantity.Text = dt.Rows[0]["Opening_Balance_quantity"].ToString();
    //                        txtMarketRed.Text = dt.Rows[0]["Opening_Balance_market_value"].ToString();
    //                        txtValue.Value = dt.Rows[0]["Opening_Balance_value"].ToString();
    //                        txtExpriy.Text = dt.Rows[0]["Date_Expriy"].ToString();
    //                        btnUpdate.Visible = true;
    //                        Session["hdntblID"] = "0";
    //                        btnSubmit.Visible = false;
    //                    }
    //                    else
    //                    {
    //                        //ddlinsecticide.Enabled = false;
    //                        //ddlUnitStock.Enabled = false;
    //                        ddlinsecticide.SelectedValue = "0";
    //                        ddlUnitStock.SelectedValue = "0";
    //                        txtQuantity.Text = "";
    //                        txtMarketRed.Text = "";
    //                        txtValue.Value = "";
    //                        btnUpdate.Visible = false;

    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
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
            Session["hdnID"] = hdnID.ToString();
            Response.Redirect("~/Inspections/State/Ho_Insecticide_Entry.aspx");
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
        FillGrid();
    }


    protected void GVOfStock_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        GVOfStock.EditIndex = -1;
        FillGrid();
    }
}

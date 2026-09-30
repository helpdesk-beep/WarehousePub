using System;
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

public partial class StatePages_UpdateStackinDatainWHR : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd = null;
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                filldepositer();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_WHR_Wise_StackData", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;               
                cmd.Parameters.AddWithValue("@WHRID", txttwhrno.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                        }
                        else
                        {
                            Depositor_Gridview.DataSource = null;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void fillDel()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_WHR_Wise_StackData_For_Del_Grid", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WHRID", txttwhrno.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            grddel.DataSource = dt;
                            grddel.DataBind();
                        }
                        else
                        {
                            grddel.DataSource = null;
                            grddel.DataBind();
                        }
                    }
                }
            }
        }
    }
    public void filldepositer()
    {
        string query2 = "";
        query2 = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR]";
        SqlCommand cmd2 = new SqlCommand(query2, con);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2);
        if (ds2.Tables[0].Rows.Count > 0)
        {
            ddlDepositor.DataSource = ds2;
            ddlDepositor.DataTextField = "Depositor_Name";
            ddlDepositor.DataValueField = "Depositor_ID";
            ddlDepositor.DataBind();
            ddlDepositor.Items.Insert(0, "--Select--");
            ddlDepositor.Enabled = false;
        }
    }
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = Depositor_Gridview.Rows[rowIndex];

        lblgodownname.Text = (row.FindControl("lblGodown_Name") as Label).Text;
        txtGdwnID.Text = (row.FindControl("hdngodownid") as HiddenField).Value;
        txtwhrno.Text = (row.FindControl("lblWhr_No") as Label).Text;
        txtstackid.Text = (row.FindControl("lblStack_ID") as Label).Text;
        txtbags.Text = (row.FindControl("lblBags") as Label).Text;
        txtweight.Text = (row.FindControl("lblWeight") as Label).Text;
        txtdelbags.Text = (row.FindControl("lblDelBags") as Label).Text;
        txtdelqty.Text = (row.FindControl("lblDelQty") as Label).Text;
        txtavlbags.Text = (row.FindControl("lblAvlBags") as Label).Text;
        txtavlqty.Text = (row.FindControl("lblAvlQty") as Label).Text;

        Session["RecBags"] = (row.FindControl("lblBags") as Label).Text;
        Session["Recqty"] = (row.FindControl("lblWeight") as Label).Text;
        Session["Delbags"] = (row.FindControl("lblDelBags") as Label).Text;
        Session["Delqty"] = (row.FindControl("lblDelQty") as Label).Text;
        Session["hdncommodityid"] = (row.FindControl("hdncommodoty") as HiddenField).Value;
        
        ddlDepositor.SelectedValue = (row.FindControl("hdndepositerid") as HiddenField).Value;
        divNewInsp.Visible = true;
        ModalPopupExtender1.Show();
    }
    
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
  
    protected void btnAddCompany_Click1(object sender, EventArgs e)
    {
        GridViewRow gvr = Depositor_Gridview.SelectedRow;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            if (txtGdwnID.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Godown')", true);
            }

            else if (txtwhrno.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter WHR No')", true);
            }
            else if (txtbags.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Bags ')", true);
            }
            else if (txtweight.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Weight')", true);
            }
            else
            {
                //if (Session["RecBags"].ToString() != txtbags.Text || Session["RecQty"].ToString() != txtweight.Text)
                //{
                    sqltrans = con.BeginTransaction();
                    //  con.Open();
                    cmd = new SqlCommand("Update_Statck_Data_in_WHR", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = sqltrans;
                    cmd.Parameters.AddWithValue("@Godown_ID", txtGdwnID.Text);
                    cmd.Parameters.AddWithValue("@WHR_No", txtwhrno.Text);
                    cmd.Parameters.AddWithValue("@Depositer_ID", ddlDepositor.SelectedValue);
                    cmd.Parameters.AddWithValue("@CommodityID", Session["hdncommodityid"].ToString());
                    cmd.Parameters.AddWithValue("@Stack_ID", txtstackid.Text.ToString());
                    cmd.Parameters.AddWithValue("@Bags", txtbags.Text.ToString());
                    cmd.Parameters.AddWithValue("@Weight", txtweight.Text.ToString());
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('WHR Data Update in Stack Successfully')", true);
                        sqltrans.Commit();
                        lblgodownname.Text = "";
                        txtGdwnID.Text = "";
                        Depositor_Gridview.DataSource = "";
                        Depositor_Gridview.DataBind();
                        fillScheduleInsp_Grid();

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record not Updatet')", true);

                    }
                //}
                //if (Session["DelBags"].ToString() != txtdelbags.Text || Session["DelQty"].ToString() != txtdelqty.Text)
                //{
                //    sqltrans = con.BeginTransaction();
                //    //  con.Open();
                //    cmd = new SqlCommand("Update_Statck_Data_in_WHR_Delivery", con);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Transaction = sqltrans;
                //    cmd.Parameters.AddWithValue("@Godown_ID", txtGdwnID.Text);
                //    cmd.Parameters.AddWithValue("@WHR_No", txtwhrno.Text);
                //    cmd.Parameters.AddWithValue("@Depositer_ID", ddlDepositor.SelectedValue);
                //    cmd.Parameters.AddWithValue("@CommodityID", Session["hdncommodityid"].ToString());
                //    cmd.Parameters.AddWithValue("@Stack_ID", txtstackid.Text.ToString());
                //    cmd.Parameters.AddWithValue("@Bags", txtdelbags.Text.ToString());
                //    cmd.Parameters.AddWithValue("@Weight", txtdelqty.Text.ToString());
                //    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                //    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                //    cmd.ExecuteNonQuery();
                //    TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                //    if (TheResult.StartsWith("SUCCESS"))
                //    {
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('WHR Data Update in Stack Successfully')", true);
                //        sqltrans.Commit();
                //        lblgodownname.Text = "";
                //        txtGdwnID.Text = "";
                //        Depositor_Gridview.DataSource = "";
                //        Depositor_Gridview.DataBind();
                //        fillScheduleInsp_Grid();

                //    }
                //    else
                //    {
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record not Updatet')", true);

                //    }
                //}
            }
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error')", true);
        }
        finally
        {
            con.Close();
        }
    }
    //protected void grddel_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    //{
    //    string ipAddress;
    //    ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //    if (ipAddress == "" || ipAddress == null)
    //        ipAddress = Request.ServerVariables["REMOTE_ADDR"];
    //    //Finding the controls from Gridview for the row which is going to update  
    //    HiddenField hdngodownid = grddel.Rows[e.RowIndex].FindControl("hdngodownid") as HiddenField;
    //    HiddenField hdncommodoty = grddel.Rows[e.RowIndex].FindControl("hdncommodoty") as HiddenField;
    //    HiddenField hdndepositerid = grddel.Rows[e.RowIndex].FindControl("hdndepositerid") as HiddenField;
    //    Label lblWhr_No = grddel.Rows[e.RowIndex].FindControl("lblWhr_No") as Label;
    //    Label lblStack_ID = grddel.Rows[e.RowIndex].FindControl("lblStack_ID") as Label;
    //    TextBox txt_DelBags = grddel.Rows[e.RowIndex].FindControl("txt_DelBags") as TextBox;
    //    TextBox txt_DelQty = grddel.Rows[e.RowIndex].FindControl("txt_DelQty") as TextBox;

    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
    //    SqlCommand cmd = new SqlCommand("Update_Statck_Data_in_WHR_Delivery", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    con.Open();
    //    cmd.Parameters.AddWithValue("@Godown_ID", hdngodownid.Value);
    //    cmd.Parameters.AddWithValue("@WHR_No", lblWhr_No.Text);
    //    cmd.Parameters.AddWithValue("@Depositer_ID", hdndepositerid.Value);
    //    cmd.Parameters.AddWithValue("@CommodityID", hdncommodoty.Value);
    //    cmd.Parameters.AddWithValue("@Stack_ID", lblStack_ID.Text.ToString());
    //    cmd.Parameters.AddWithValue("@Bags", txt_DelBags.Text.ToString());
    //    cmd.Parameters.AddWithValue("@Weight", txt_DelQty.Text.ToString());
    //    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
    //    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
    //    cmd.ExecuteNonQuery();
    //    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

    //    if (TheResult.StartsWith("SUCCESS"))
    //    {
    //        string strMsg = "Delivery Stack Details Update Successfully |||";
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
    //        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
    //        grddel.EditIndex = -1;
    //        //Call ShowData method for displaying updated data  
    //        fillDel();
    //    }
    //}

    protected void grddel_RowUpdating(object sender, GridViewUpdateEventArgs e)

    {

        Label lblWhr_No = (Label)grddel.Rows[e.RowIndex].FindControl("lblWhr_No");
        Label lblStack_ID = (Label)grddel.Rows[e.RowIndex].FindControl("lblStack_ID");
        TextBox txt_DelBags = (TextBox)grddel.Rows[e.RowIndex].FindControl("txt_DelBags");
        TextBox txt_DelQty = (TextBox)grddel.Rows[e.RowIndex].FindControl("txt_DelQty");
        TextBox designation = (TextBox)grddel.Rows[e.RowIndex].FindControl("txtdesignation");
        HiddenField hdncommodoty = (HiddenField)grddel.Rows[e.RowIndex].FindControl("hdncommodoty");
        HiddenField hdndepositerid = (HiddenField)grddel.Rows[e.RowIndex].FindControl("hdndepositerid");
        HiddenField hdngodownid = (HiddenField)grddel.Rows[e.RowIndex].FindControl("hdngodownid");
        Label lblGatePass_No = (Label)grddel.Rows[e.RowIndex].FindControl("lblGatePass_No");

        //HiddenField hdngodownid = grddel.Rows[e.RowIndex].FindControl("hdngodownid") as HiddenField;
        //    HiddenField hdncommodoty = grddel.Rows[e.RowIndex].FindControl("hdncommodoty") as HiddenField;
        //    HiddenField hdndepositerid = grddel.Rows[e.RowIndex].FindControl("hdndepositerid") as HiddenField;
        //    Label lblWhr_No = grddel.Rows[e.RowIndex].FindControl("lblWhr_No") as Label;
        //    Label lblStack_ID = grddel.Rows[e.RowIndex].FindControl("lblStack_ID") as Label;
        //    TextBox txt_DelBags = grddel.Rows[e.RowIndex].FindControl("txt_DelBags") as TextBox;
        //    TextBox txt_DelQty = grddel.Rows[e.RowIndex].FindControl("txt_DelQty") as TextBox;

        string WhrNo = lblWhr_No.Text;
        string Stack_ID = lblStack_ID.Text;
        string DelBags = txt_DelBags.Text;
        string DelQty = txt_DelQty.Text;
        string Commodit = hdncommodoty.Value;
        string Depositer = hdndepositerid.Value;
        string godownid = hdngodownid.Value;
        string Gatepass = lblGatePass_No.Text;

        Updateemployee(godownid, WhrNo, Stack_ID, Commodit, Depositer, DelBags, DelQty, Gatepass);
        grddel.EditIndex = -1;
        fillDel();

    }
    protected void Updateemployee(string godownid, string WhrNo, string Stack_ID, string Commodit,string Depositer,string DelBags,string DelQty,string GatePass_No)

    {
        string ipAddress;
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("Update_Statck_Data_in_WHR_Delivery", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        cmd.Parameters.AddWithValue("@Godown_ID", godownid);
        cmd.Parameters.AddWithValue("@WHR_No", WhrNo);
        cmd.Parameters.AddWithValue("@Depositer_ID", Depositer);
        cmd.Parameters.AddWithValue("@CommodityID", Commodit);
        cmd.Parameters.AddWithValue("@Stack_ID", Stack_ID);
        cmd.Parameters.AddWithValue("@Bags", DelBags);
        cmd.Parameters.AddWithValue("@Weight", DelQty);
        cmd.Parameters.AddWithValue("@GatePass_No", GatePass_No);
        cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
        cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

        if (TheResult.StartsWith("SUCCESS"))
        {
            string strMsg = "Delivery Stack Details Update Successfully |||";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            grddel.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            fillDel();
        }
        
    }
    protected void grddel_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
        grddel.EditIndex = -1;
        fillScheduleInsp_Grid();
    }
    protected void grddel_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        grddel.EditIndex = e.NewEditIndex;
        fillScheduleInsp_Grid();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
        fillDel();
    }
}

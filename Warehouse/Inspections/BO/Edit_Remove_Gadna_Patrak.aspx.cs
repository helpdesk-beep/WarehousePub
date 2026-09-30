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
using System.Collections.Generic;

public partial class Inspections_BO_Godown_wise_Stock_Details : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    //public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string PFID = "";
    SqlCommand cmd = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        PFID = Session["UserId"].ToString();

        if (!IsPostBack)
        {
            // fillCropYear();
            fillGodownDetails();
        }
    }

    public void fillGodownDetails()
    {
        string conStr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(conStr2))
        {
            SqlCommand cmd = new SqlCommand("Get_Godown_Name", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Branch_ID", Session["UserId"].ToString());
            con.Open();

            ddl_gdwn.DataSource = cmd.ExecuteReader();
            ddl_gdwn.DataTextField = "Godown_Name";
            ddl_gdwn.DataValueField = "Godown_ID";
            ddl_gdwn.DataBind();
            ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            // ddl_gdwn.Items.Insert(0, new ListItem("-- Select Godown --", "0"));
            con.Close();
        }
    }
    public void fillScheduleInsp_Grid()
    {
        // SqlCommand cmd = new SqlCommand("Rpt_Insp_Get_Stack_Block_Wise_Details", conStr);
        SqlCommand cmd = new SqlCommand("Get_Gadna_Patrak_For_BO-Edit_Remove", conStr);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Godown_ID", ddl_gdwn.SelectedValue);
        // cmd.Parameters.AddWithValue("@cropyear", ddlcropyear.SelectedValue);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GD_StackBal.Caption = @"<b style=""font-weight: bold;"">Godown Name: " + "  -   " + ddl_gdwn.SelectedItem.ToString() + "</b> ";
            divshow.Visible = true;
            tblbtn.Visible = true;
            GD_StackBal.DataSource = dt;
            GD_StackBal.DataBind();
            GD_StackBal.FooterRow.Style.Add("text-align", "center");
            GD_StackBal.FooterRow.Cells[11].Text = "Total";
            GD_StackBal.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Up")).ToString();
            GD_StackBal.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Below")).ToString();
            GD_StackBal.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Bags")).ToString();
            GD_StackBal.FooterRow.Cells[16].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spillage_bag")).ToString();
        }
        else
        {
            divshow.Visible = true;
            tblbtn.Visible = false;
            GD_StackBal.DataSource = null;
            GD_StackBal.DataBind();
            string strMsg = "इस गोदाम के द्वारा वर्ष " + ddl_gdwn.SelectedItem.Value + " की एंट्री गड़ना पत्रक में नहीं की गई हैं ,पहले गोदाम के द्वारा गड़ना पत्रक की एंट्री करनी होगी |";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "')", true);

        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnFinalsubmit = (HiddenField)e.Row.FindControl("hdnFinalsubmit");
            Button btnRemove = (Button)e.Row.FindControl("btnRemove");
            Button btnEdit = (Button)e.Row.FindControl("btnEdit");
            if (hdnFinalsubmit.Value == "0")
            {
                btnEdit.Visible = true;
                // btnRemove.Visible = true;               
                btnPrint.Visible = false;
            }
            else if (hdnFinalsubmit.Value == "1")
            {
                btnEdit.Visible = false;
                //btnRemove.Visible = false;
                btnPrint.Visible = true;
            }
        }

    }
    protected void GD_StackBal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GD_StackBal.Rows[rowIndex];

            //Fetch value of Name.
            string hdnid = (row.FindControl("hdnID") as HiddenField).Value;
            string lblstack_id = (row.FindControl("lblstack_id") as Label).Text;
            Session["hdnid"] = hdnid.ToString();
            Session["lblstack_id"] = lblstack_id.ToString();
            RemoveRowvcpgqualificationbygridviewAEPRH(hdnid, lblstack_id);

        }
        if (e.CommandName == "EditRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GD_StackBal.Rows[rowIndex];

            //Fetch value of Name.
            string hdnID = (row.FindControl("hdnID") as HiddenField).Value;
            Session["hdnID"] = hdnID.ToString();
            // Response.Redirect("/warehouse/Inspections/BO/Owned_Edit_Bolck_Wise_Entry.aspx");
            Page.ClientScript.RegisterStartupScript(
  this.GetType(), "OpenWindow", "window.open('/warehouse/Inspections/BO/Update_Gadna_Patrak.aspx','_newtab');", true);
        }

    }
    public void RemoveRowvcpgqualificationbygridviewAEPRH(string id, string Stackid)
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

            SqlCommand cmd = new SqlCommand("Insp_Get_Stack_Block_Wise_Details_For_Remove_Rows", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id.ToString());
           // cmd.Parameters.AddWithValue("@StackID", Stackid.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                fillScheduleInsp_Grid();
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
    protected void GD_StackBal_RowCreated(object sender, GridViewRowEventArgs e)
    {


        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    GridView HeaderGrid = (GridView)sender;
        //    GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell HeaderCell = new TableCell();

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 6;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "लम्बाई";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "चौड़ाई ";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "अतिरिक्त";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "ऊपर";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "निचे";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);


        //    GD_StackBal.Controls[0].Controls.AddAt(0, HeaderGridRow);
        //    HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        //}
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    GridView HeaderGrid = (GridView)sender;
        //    GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
        //    TableCell HeaderCell = new TableCell();

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "क्रमांक";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "जमाकर्ता का नाम";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "स्कंध का नाम";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "वर्ष";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "स्टैक आईडी";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "स्टैक नाम";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "स्टेक प्लानिंग बिछान";
        //    HeaderCell.ColumnSpan = 3;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "योग (7+8+9)";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "बोरो के लेयर की ऊंचाई";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "ब्लॉक क्र./संख्या";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "बोरियो की संख्या (10*11*12)";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "अतिरिक्त पाई गई बोरियो की संख्या";
        //    HeaderCell.ColumnSpan = 2;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "टोटल बौरे";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);

        //    HeaderCell = new TableCell();
        //    HeaderCell.Text = "Spillage Bag";
        //    HeaderCell.ColumnSpan = 1;
        //    HeaderCell.CssClass = "alert alert-info";
        //    HeaderCell.Font.Bold = false;
        //    HeaderCell.Font.Size = 10;
        //    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
        //    HeaderGridRow.Cells.Add(HeaderCell);


        //    GD_StackBal.Controls[0].Controls.AddAt(0, HeaderGridRow);
        //    HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        //}
    }



    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow row in GD_StackBal.Rows)
            {
                CheckBox chk_Sum = (CheckBox)(row.FindControl("chk_Sum"));
                HiddenField hdncheckID = (HiddenField)(row.FindControl("hdncheckID"));
                if (chk_Sum.Checked == true)
                {
                    conStr.Open();
                    cmd = new SqlCommand("[dbo].[Insp_Get_Stack_Block_Wise_Details_For_Remove_Rows]", conStr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", hdncheckID.Value);
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                    if (TheResult.StartsWith("SUCCESS"))

                    {
                        count++;
                    }
                    conStr.Close();
                }
            }
            if (count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Delete successfully..')", true);
                fillScheduleInsp_Grid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record NOT Delete')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.ToString() + "')", true);
        }
        finally
        {
            conStr.Close();
        }
    }
}
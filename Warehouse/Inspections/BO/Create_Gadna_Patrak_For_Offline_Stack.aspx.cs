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
using System.Configuration;
using System;

public partial class Inspections_BO_Create_Gadna_Patrak_For_Offline_Stack : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string GodownID = "";
    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    string depositername = "";
    string commodityname = "";
    string stackid = "";
    string stackname = "";
    string BranchID = "";
    string DepositerID = "";
    string CommodityID = "";
    string Cropyear = "";
    string noofbags = "";
    string weight = "";
    string godownname = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        BranchID = Session["hdnbranchid"].ToString();
        GodownID = Session["hdnGodown_ID"].ToString();
        DepositerID = Session["hdnDepositor_ID"].ToString();
        CommodityID = Session["hdnCommodity_Id"].ToString();
        Cropyear = Session["hdnCropYear"].ToString();

        commodityname = Session["lblcommodity"].ToString();
        depositername = Session["lblDepositor_Name"].ToString();
        stackid = Session["lblstack_id"].ToString();
        stackname = Session["lblStack_Name"].ToString();
        noofbags = Session["lblrecbags"].ToString();
        weight = Session["lblRecWeight"].ToString();
        godownname = Session["lblGodown_Name"].ToString();
        if (!IsPostBack)
        {
            lbldepositername.Text = depositername.ToString();
            lblcommodityname.Text = commodityname.ToString();
            lblstackid.Text = stackid.ToString();
            lblstackname.Text = stackname.ToString();
            lblnoofbags.Text = noofbags.ToString();
            lblweight.Text = weight.ToString();
            lblcropyear.Text = Cropyear.ToString();
            lblgodownname.Text = godownname.ToString();
            //if (lblstackid.Text.ToString() != "0")
            //{
                GetdataForGrid();
            //}
            txtTotal.Attributes.Add("readonly", "readonly");
            lbltotalbags.Attributes.Add("readonly", "readonly");

        }

    }
    protected void GetdataForGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_offline_Stack_Details", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Stack_id", Session["lblstack_id"].ToString());
                cmd.Parameters.AddWithValue("@GodownID", GodownID.ToString());
                //cmd.Parameters.AddWithValue("@CropYear", Session["hdnCropYear"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            tr_griddata.Visible = true;
                            GD_StackBal.DataSource = dt;
                            GD_StackBal.DataBind();

                            GD_StackBal.FooterRow.Style.Add("text-align", "center");
                            GD_StackBal.FooterRow.Cells[9].Text = "Total";
                            GD_StackBal.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Number_Of_Block")).ToString();
                            GD_StackBal.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
                            GD_StackBal.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Up")).ToString();
                            GD_StackBal.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Below")).ToString();
                            GD_StackBal.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Bags")).ToString();
                            GD_StackBal.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spillage_bag")).ToString();
                        }
                        else
                        {

                            tr_griddata.Visible = false;
                            GD_StackBal.DataSource = null;
                            GD_StackBal.DataBind();
                        }
                    }
                }
            }
        }
    }

    //public void GetdataForGrid()
    //{
    //    SqlCommand cmd = new SqlCommand("Get_offline_Stack_Details", conStr);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Stack_id", Session["lblstack_id"].ToString());
    //    cmd.Parameters.AddWithValue("@GodownID", GodownID.ToString());
    //    cmd.Parameters.AddWithValue("@CropYear", Session["hdnCropYear"].ToString());
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    //DataSet ds = new DataSet();
    //    da.Fill(dt);
    //    //if (ds.Tables[0].Rows.Count > 0)
    //        if (dt.Rows.Count > 0)
    //    {
    //        tr_griddata.Visible = true;
    //        GD_StackBal.DataSource = dt;
    //        GD_StackBal.DataBind();

    //        GD_StackBal.FooterRow.Style.Add("text-align", "center");
    //        GD_StackBal.FooterRow.Cells[9].Text = "Total";
    //        GD_StackBal.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Number_Of_Block")).ToString();
    //        GD_StackBal.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<int>("No_of_Bags")).ToString();
    //        GD_StackBal.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Up")).ToString();
    //        GD_StackBal.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Below")).ToString();
    //        GD_StackBal.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Bags")).ToString();
    //        GD_StackBal.FooterRow.Cells[15].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Spillage_bag")).ToString();
    //    }
    //    else
    //    {
    //        tr_griddata.Visible = false;
    //        GD_StackBal.DataSource = null;
    //        GD_StackBal.DataBind();
    //        // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found...')", true);
    //    }
    //}

    public void checkvalidation()
    {
        if (txtLendth.Text == "" || txtLendth.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Length!....')", true);
            txtLendth.Focus();
            return;
        }
        else if (txtwidth.Text == "" || txtwidth.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Width!....')", true);
            txtwidth.Focus();
            return;
        }
        else if (txtextralendth.Text == "" || txtextralendth.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Extra Length!....')", true);
            txtwidth.Focus();
            return;
        }
        else if (txtheight.Text == "" || txtheight.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Height!....')", true);
            txtheight.Focus();
            return;
        }
        else if (txtnoofblock.Text == "" || txtnoofblock.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter No. of Blocks!....')", true);
            txtnoofblock.Focus();
            return;
        }
        else if (txtup.Text == "" || txtup.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('ऊपर रखे हुए बोरो की एंट्री करें !....')", true);
            txtup.Focus();
            return;
        }
        else if (txtbelow.Text == "" || txtbelow.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('नीचे रखे हुए बोरो की एंट्री करें!....')", true);
            txtbelow.Focus();
            return;
        }
        else if (txtspillagebag.Text == "" || txtspillagebag.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Spillag Bag की एंट्री करें!....')", true);
            txtspillagebag.Focus();
            return;
        }
    }

    public void Clear()
    {
        txtLendth.Text = "";
        txtwidth.Text = "";
        txtextralendth.Text = "";
        txtTotal.Text = "";
        txtheight.Text = "";
        txtnoofblock.Text = "";
        lbltotalbags.Text = "";
        txtup.Text = "";
        txtbelow.Text = "";
        txttotalnoofbags.Text = "";
        txtremark.Text = "";
    }
    protected void btn_saveInspDate_Click(object sender, EventArgs e)
    {
        SqlTransaction tn = null;
        foreach (GridViewRow row in GD_StackBal.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnID = row.FindControl("hdnID") as HiddenField;
                HiddenField hdnDepositer_ID = row.FindControl("hdnDepositer_ID") as HiddenField;
                HiddenField hdnCommodity_ID = row.FindControl("hdnCommodity_ID") as HiddenField;
                HiddenField hdncropyear = row.FindControl("hdncropyear") as HiddenField;
                Label lblstack_id = row.FindControl("lblstack_id") as Label;

                string ipAddress;
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipAddress == "" || ipAddress == null)
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                try
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Insp_Stack_Block_Wise_Godown_Entry_Final_Submit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    tn = con.BeginTransaction();
                    cmd.Transaction = tn;
                    cmd.Parameters.AddWithValue("@ID", hdnID.Value);
                    cmd.Parameters.AddWithValue("@Final_Bubmit", 1);
                    cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                    cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                    if (TheResult.StartsWith("SUCCESS"))
                    {

                        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString);
                        SqlCommand cmd2 = new SqlCommand("JVS_Godown_Depo_Stack_Crop_Year_Wise_Entry_Insert", con2);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        con2.Open();
                        cmd2.Parameters.AddWithValue("@Godown_ID", GodownID.ToString());
                        cmd2.Parameters.AddWithValue("@Stack_ID", lblstack_id.Text);
                        cmd2.Parameters.AddWithValue("@Depositer_ID", hdnDepositer_ID.Value);
                        cmd2.Parameters.AddWithValue("@Commodity_ID", hdnCommodity_ID.Value);
                        cmd2.Parameters.AddWithValue("@Crop_Year", hdncropyear.Value);
                        cmd2.Parameters.AddWithValue("@Inseted_By", ipAddress);
                        cmd2.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                        cmd2.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                        cmd2.ExecuteNonQuery();
                        TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                        if (TheResult.StartsWith("SUCCESS"))
                        {
                            tn.Commit();
                            string strMsg = "data has been final submitted successfully";
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Inspections/Godown/Godown_Stack_wise_Stock_Details.aspx';", true);
                            // GetdataForGrid();
                        }
                        con2.Close();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    tn.Rollback();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
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

    protected void txtextralendth_TextChanged(object sender, EventArgs e)
    {
        int A = int.Parse(txtLendth.Text);
        int B = int.Parse(txtwidth.Text);
        int C = int.Parse(txtextralendth.Text);

        int i = A + B + C;
        i = A + B + C;
        txtTotal.Text = i.ToString();

    }
    protected void txtnoofblock_TextChanged(object sender, EventArgs e)
    {
        int Total = int.Parse(txtTotal.Text);
        int height = int.Parse(txtheight.Text);
        int noofblock = int.Parse(txtnoofblock.Text);

        int NoofBags = Total * height * noofblock;
        NoofBags = Total * height * noofblock;
        lbltotalbags.Text = NoofBags.ToString();
    }

    protected void txtbelow_TextChanged(object sender, EventArgs e)
    {
        int Totalbag = int.Parse(lbltotalbags.Text);
        int UP = int.Parse(txtup.Text);
        int Below = int.Parse(txtbelow.Text);

        int TotalNoofBags = Totalbag + UP + Below;
        TotalNoofBags = Totalbag + UP + Below;
        txttotalnoofbags.Text = TotalNoofBags.ToString();
    }

    protected void btnsaveprofile_Click(object sender, EventArgs e)
    {
        try
        {
            checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insp_Stack_Block_Wise_Godown_Entry_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@Branch_ID", BranchID.ToString());
                cmd.Parameters.AddWithValue("@Godown_ID", GodownID.ToString());
                cmd.Parameters.AddWithValue("@Stack_ID", lblstackid.Text.ToString());
                cmd.Parameters.AddWithValue("@Stack_Name", lblstackname.Text.ToString());
                cmd.Parameters.AddWithValue("@Depositer_Name", lbldepositername.Text.ToString());
                cmd.Parameters.AddWithValue("@Depositer_ID", DepositerID.ToString());
                cmd.Parameters.AddWithValue("@Commodity_Name", lblcommodityname.Text.ToString());
                cmd.Parameters.AddWithValue("@Commodity_ID", CommodityID.ToString());
                cmd.Parameters.AddWithValue("@Crop_Year", Cropyear.ToString());
                cmd.Parameters.AddWithValue("@Length", txtLendth.Text.ToString());
                cmd.Parameters.AddWithValue("@Width", txtwidth.Text.ToString());
                cmd.Parameters.AddWithValue("@Extra", txtextralendth.Text.ToString());
                cmd.Parameters.AddWithValue("@Total_L_W_E", txtTotal.Text.ToString());
                cmd.Parameters.AddWithValue("@Height", txtheight.Text.ToString());
                cmd.Parameters.AddWithValue("@Number_Of_Block", txtnoofblock.Text.ToString());
                cmd.Parameters.AddWithValue("@No_of_Bags", lbltotalbags.Text.ToString());
                cmd.Parameters.AddWithValue("@Up", txtup.Text.ToString());
                cmd.Parameters.AddWithValue("@Below", txtbelow.Text.ToString());
                cmd.Parameters.AddWithValue("@Total_Bags", txttotalnoofbags.Text.ToString());
                cmd.Parameters.AddWithValue("@Inserted_By", IPAddress);
                cmd.Parameters.AddWithValue("@Spillage_bag", txtspillagebag.Text.ToString());
                cmd.Parameters.AddWithValue("@Remark", txtremark.Text.ToString());
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Record Save Successfully |||";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    GetdataForGrid();
                    Clear();
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
    public void RemoveRowvcpgqualificationbygridviewAEPRH(string id,string stackid)
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
            cmd.Parameters.AddWithValue("@id", id);
            //cmd.Parameters.AddWithValue("@StackID", stackid.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "Remove Row Successfully|||";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                GetdataForGrid();
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
    protected void GD_StackBal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GD_StackBal.Rows[rowIndex];

            //Fetch value of Name.
            string hdnID = (row.FindControl("hdnID") as HiddenField).Value;
            Session["hdnID"] = hdnID.ToString();
            Response.Redirect("/warehouse/Inspections/BO/Owned_Edit_Bolck_Wise_Entry.aspx");

        }
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
    }
    protected void GD_StackBal_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "लम्बाई";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "चौड़ाई ";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "अतिरिक्त";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "ऊपर";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "निचे";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);


            GD_StackBal.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "क्रमांक";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "जमाकर्ता का नाम";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "स्कंध का नाम";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "स्टैक आईडी";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "स्टैक नाम";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "स्टेक प्लानिंग बिछान";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "योग (6 +7+8)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "बोरो के लेयर की ऊंचाई";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "ब्लॉक क्र./संख्या";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "बोरियो की संख्या (9*10*11)";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "अतिरिक्त पाई गई बोरियो की संख्या";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "टोटल बौरे";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Spillage Bag";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Remark";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.CssClass = "alert alert-info";
            HeaderCell.Font.Bold = false;
            HeaderCell.Font.Size = 10;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            GD_StackBal.Controls[0].Controls.AddAt(0, HeaderGridRow);
            HeaderGridRow = new GridViewRow(2, 1, DataControlRowType.Header, DataControlRowState.Insert);
        }
    }

    protected void GD_StackBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnFinalsubmit = (HiddenField)e.Row.FindControl("hdnFinalSubmitByIO");
            Button btnEdit = (Button)e.Row.FindControl("btnEdit");
            Button btnRemove = (Button)e.Row.FindControl("btnRemove");
            if (hdnFinalsubmit.Value == "0")
            {
                btnEdit.Visible = true;
                btnRemove.Visible = true;
            }
            else if (hdnFinalsubmit.Value == "1")
            {
                btnEdit.Visible = false;
                btnRemove.Visible = false;
            }
        }

    }
}
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
using System.Text;

public partial class IssueCenterLevel_Storage_madeupbags : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataReader DObj = null;
    protected Common ComObj = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Session["Depot_DepotID"] as string))
        {
            ComObj = new Common(ConfigurationManager.AppSettings["connect_warehouse"].ToString());
            if (Session["lang"].ToString() == "Hindi")
            {
                //lblMadeupheader.Text = Resources.hindi.lblMadeupheader;
                lblCommodity.Text = Resources.hindi.lblCommodity;
                lblGodownNo.Text = Resources.hindi.lblGodownNo;
                lblStackNo.Text = Resources.hindi.lblStackNo;
                lblCollectionDate.Text = Resources.hindi.lblCollectionDate;
                lblmadeupBagsNo.Text = Resources.hindi.lblmadeupBagsNo;
                lblMadeForDo.Text = Resources.hindi.lblMadeForDo;

                lbl_gunnybag_type_category.Text = Resources.hindi.lbl_gunnybag_type_category;
                lblWHRNumber.Text = Resources.hindi.lblWHRNumber;

            }
            if (!IsPostBack)
            {
                Session["RefreshButton"] = "No";
                string PopMsg = "";
                PopMsg = Request.QueryString["PopMsg"];
                if (PopMsg != null)
                {
                    //The Page is Reloaded with Message
                    StringBuilder str = new StringBuilder();
                    str.Append("<script>");
                    str.Append("alert('" + PopMsg + "');</script>");
                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());

                }
                if (Session["Depot_DepotID"].ToString() != "")
                {
                    string depotId = Session["Depot_DepotID"].ToString();
                    //GetWhrNo(depotId);
                    GetCropYear(depotId);
                    GetMadeUpBags(depotId);
                    GetCommodity(depotId);
                }
                btnsave.Attributes.Add("OnClick", " return AskForComment()");
            }
        }
        else
        {
            Response.Redirect("../login.aspx ");
        }
    }
    private void GetMadeUpBags(string depotId)
    {
        DObj = new DataReader(ComObj);
        string qry = "SELECT tbl_Storage_Made_Up_Bags.Bagsid,tbl_Storage_Made_Up_Bags.No_Made_Up_Bags, convert(varchar(15) ,tbl_Storage_Made_Up_Bags.Collection_Date,103) Collection_Date,tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STACK.Stack_Name, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name ,WHR_NO FROM tbl_Storage_Made_Up_Bags INNER JOIN tbl_MetaData_GODOWN ON tbl_Storage_Made_Up_Bags.Godown_Id = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON tbl_Storage_Made_Up_Bags.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_STACK ON tbl_MetaData_STACK.stack_id = tbl_Storage_Made_Up_Bags.stack_id INNER JOIN   tbl_storage_Depositor_WHR_Relation on tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id=tbl_Storage_Made_Up_Bags.Whrid where tbl_Storage_Made_Up_Bags.BranchID = '" + Session["BranchId"].ToString() + "' Order by tbl_Storage_Made_Up_Bags.CreatedDate  Desc";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {

        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["dsBags"] = ds;
            }
            fillGrid(ds);
        }
    }
    private void fillGrid(DataSet ds)
    {
        GridView_MadeUpBags.DataSource = ds.Tables[0];
        GridView_MadeUpBags.DataBind();
    }
    private void GetCropYear(string depotId)
    {
        DObj = new DataReader(ComObj);
        //string qry = "select '0' as Depositor_WHR_Id,' -select- ' as Whr_No  FROM tbl_storage_Depositor_WHR_Relation union SELECT tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id, tbl_storage_Depositor_WHR_Relation.Whr_No FROM tbl_storage_Depositor_WHR_Relation INNER JOIN whr_status ON tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id = whr_status.WhrId WHERE (whr_status.Statusflag = 'N') and tbl_storage_Depositor_WHR_Relation.BranchID = '"+Session["BranchId"].ToString()+"'";
        string qry = "select distinct CropYear from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["BranchId"].ToString() + "'";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {

        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcropYear.DataSource = ds.Tables[0];
                ddlcropYear.DataTextField = "CropYear";
                ddlcropYear.DataValueField = "CropYear";
                ddlcropYear.DataBind();
            }
        }
    }

    private void GetCommodity(string depotId)
    {
        DObj = new DataReader(ComObj);
        //string qry = "select '0' as Depositor_WHR_Id,' -select- ' as Whr_No  FROM tbl_storage_Depositor_WHR_Relation union SELECT tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id, tbl_storage_Depositor_WHR_Relation.Whr_No FROM tbl_storage_Depositor_WHR_Relation INNER JOIN whr_status ON tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id = whr_status.WhrId WHERE (whr_status.Statusflag = 'N') and tbl_storage_Depositor_WHR_Relation.BranchID = '"+Session["BranchId"].ToString()+"'";
        // string qry = "select distinct CropYear from tbl_storage_Depositor_WHR_Relation where BranchID='" + Session["BranchId"].ToString() + "'";
        string qry = "select distinct msc.commodity_id [commodity_id] ,[Commodity_Name] from tbl_MetaData_STORAGE_COMMODITY as msc join tbl_storage_Depositor_WHR_Relation as wr on wr.Commodity_Id=msc.Commodity_Id where BranchID ='" + Session["BranchId"].ToString() + "'";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {

        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcommodity.DataSource = ds.Tables[0];
                ddlcommodity.DataTextField = "Commodity_Name";
                ddlcommodity.DataValueField = "commodity_id";
                ddlcommodity.DataBind();
            }
        }
    }
    private void GetWhrNo()
    {
        DObj = new DataReader(ComObj);
        //string qry = "select '0' as Depositor_WHR_Id,' -select- ' as Whr_No  FROM tbl_storage_Depositor_WHR_Relation union SELECT tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id, tbl_storage_Depositor_WHR_Relation.Whr_No FROM tbl_storage_Depositor_WHR_Relation INNER JOIN whr_status ON tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id = whr_status.WhrId WHERE (whr_status.Statusflag = 'N') and tbl_storage_Depositor_WHR_Relation.BranchID = '"+Session["BranchId"].ToString()+"'";
        string qry = " select distinct tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id as Depositor_WHR_Ids,tbl_storage_Depositor_WHR_Relation.WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation inner join whr_status on tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id=whr_status.WhrId where tbl_storage_Depositor_WHR_Relation.Depotid='" + Session["Depot_DepotID"].ToString() + "' and whr_status.Statusflag = 'N' AND tbl_storage_Depositor_WHR_Relation.CropYear='" + ddlcropYear.SelectedValue.ToString() + "' AND tbl_storage_Depositor_WHR_Relation.Commodity_Id='" + ddlcommodity.SelectedValue.ToString() + "' order by tbl_storage_Depositor_WHR_Relation.WHR_Issue_Date desc";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {

        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlwhrlist.DataSource = ds.Tables[0];
                ddlwhrlist.DataTextField = "Depositor_WHR_Ids";
                ddlwhrlist.DataValueField = "Depositor_WHR_Ids";
                ddlwhrlist.DataBind();
            }
        }
    }
    protected void ddlwhrlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodownSelection();
        GetStackSelection();
        //try
        //{
        //    DObj = new DataReader(ComObj);
        //    string qry = "select msc.commodity_id [commodity_id] ,[Commodity_Name] from tbl_MetaData_STORAGE_COMMODITY as msc join tbl_storage_Depositor_WHR_Relation as wr on wr.Commodity_Id=msc.Commodity_Id where depositor_whr_id ='" + ddlwhrlist.SelectedValue + "'";
        //    DataSet ds = DObj.selectAny(qry);
        //    if (ds == null)
        //    {
        //    }
        //    else
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            ddlcommodity.DataSource = ds.Tables[0];
        //            ddlcommodity.DataTextField = "Commodity_Name";
        //            ddlcommodity.DataValueField = "commodity_id";
        //            ddlcommodity.DataBind();
        //        }
        //    }
        //    GetGodownSelection();
        //    GetStackSelection();
        //}
        //catch (Exception ex)
        //{
        //    StringBuilder str = new StringBuilder();
        //    str.Append("<script>");
        //    str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
        //    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        //}
    }
    private void GetGodownSelection()
    {
        DObj = new DataReader(ComObj);
        string qry = "select Godown_ID,Godown_Name from tbl_MetaData_GODOWN where Godown_ID in (select Godown_ID from tbl_storage_Stacking_Details where WHRId='" + ddlwhrlist.SelectedValue + "' )";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {

        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlgodown.DataSource = ds.Tables[0];
                ddlgodown.DataTextField = "Godown_Name";
                ddlgodown.DataValueField = "Godown_ID";
                ddlgodown.DataBind();
            }

        }
    }
    private void GetStackSelection()
    {

        DObj = new DataReader(ComObj);
        string qry = "select Stack_ID,Stack_Name from tbl_MetaData_STACK where Stack_ID in (select Stack_ID from tbl_storage_Stacking_Details where WHRId='" + ddlwhrlist.SelectedValue + "' and Godown_ID='" + ddlgodown.SelectedValue + "')";
        DataSet ds = DObj.selectAny(qry);
        if (ds == null)
        {

        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlstack.DataSource = ds.Tables[0];
                ddlstack.DataTextField = "Stack_Name";
                ddlstack.DataValueField = "Stack_ID";
                ddlstack.DataBind();
            }
        }
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetStackSelection();
    }
    protected void CVWhr_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string depotId = "";
        if (Session["Depot_DepotID"].ToString() != "")
        {
            depotId = Session["Depot_DepotID"].ToString();
        }
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd_go = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd_go.Connection = con;
            cmd_go.CommandText = "SELECT tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id FROM tbl_storage_Depositor_WHR_Relation INNER JOIN whr_status ON tbl_storage_Depositor_WHR_Relation.Depositor_WHR_Id = whr_status.WhrId WHERE (whr_status.Statusflag = 'N') and tbl_storage_Depositor_WHR_Relation.BranchID = '" + Session["BranchId"] + "' ";
            cmd_go.CommandType = CommandType.Text;
            da.SelectCommand = cmd_go;
            da.Fill(ds, "tbl_storage_Depositor_WHR_Relation");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                string whr;
                args.IsValid = false;    // Assume False
                // Loop through table and compare each record against user's entry
                foreach (DataRowView datarow in dv)
                {
                    // Extract e-mail address from the current row
                    whr = datarow["Depositor_WHR_Id"].ToString();
                    // Compare e-mail address against user's entry
                    if (whr == args.Value)
                    {
                        args.IsValid = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void CVcomm_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd_go = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd_go.Connection = con;
            cmd_go.CommandText = "select msc.commodity_id  from tbl_MetaData_STORAGE_COMMODITY as msc join tbl_storage_Depositor_WHR_Relation as wr on wr.Commodity_Id=msc.Commodity_Id where depositor_whr_id ='" + ddlwhrlist.SelectedValue + "' ";
            cmd_go.CommandType = CommandType.Text;
            da.SelectCommand = cmd_go;
            da.Fill(ds, "tbl_MetaData_STORAGE_COMMODITY");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                string comm;
                args.IsValid = false;    // Assume False
                // Loop through table and compare each record against user's entry
                foreach (DataRowView datarow in dv)
                {
                    // Extract e-mail address from the current row
                    comm = datarow["commodity_id"].ToString();
                    // Compare e-mail address against user's entry
                    if (comm == args.Value)
                    {
                        args.IsValid = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void CVGodown_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd_go = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd_go.Connection = con;
            cmd_go.CommandText = "select Godown_ID from tbl_MetaData_GODOWN where Godown_ID in (select Godown_ID from tbl_storage_Stacking_Details where WHRId='" + ddlwhrlist.SelectedValue + "' ) ";
            cmd_go.CommandType = CommandType.Text;
            da.SelectCommand = cmd_go;
            da.Fill(ds, "tbl_MetaData_GODOWN");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                string gd;
                args.IsValid = false;    // Assume False
                // Loop through table and compare each record against user's entry
                foreach (DataRowView datarow in dv)
                {
                    // Extract e-mail address from the current row
                    gd = datarow["Godown_ID"].ToString();
                    // Compare e-mail address against user's entry
                    if (gd == args.Value)
                    {
                        args.IsValid = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void CVstack_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd_go = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd_go.Connection = con;
            cmd_go.CommandText = "select Stack_ID from tbl_MetaData_STACK where Stack_ID in (select Stack_ID from tbl_storage_Stacking_Details where WHRId='" + ddlwhrlist.SelectedValue + "' and Godown_ID='" + ddlgodown.SelectedValue + "') ";
            cmd_go.CommandType = CommandType.Text;
            da.SelectCommand = cmd_go;
            da.Fill(ds, "tbl_MetaData_STACK");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                string stk;
                args.IsValid = false;    // Assume False
                // Loop through table and compare each record against user's entry
                foreach (DataRowView datarow in dv)
                {
                    // Extract e-mail address from the current row
                    stk = datarow["Stack_ID"].ToString();
                    // Compare e-mail address against user's entry
                    if (stk == args.Value)
                    {
                        args.IsValid = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void CVgunny_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd_go = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd_go.Connection = con;
            cmd_go.CommandText = "select Gunny_Master_Id from tbl_MetaData_GunnyBags ";
            cmd_go.CommandType = CommandType.Text;
            da.SelectCommand = cmd_go;
            da.Fill(ds, "tbl_MetaData_GunnyBags");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                string gunny;
                args.IsValid = false;    // Assume False
                // Loop through table and compare each record against user's entry
                foreach (DataRowView datarow in dv)
                {
                    // Extract e-mail address from the current row
                    gunny = datarow["Gunny_Master_Id"].ToString();
                    // Compare e-mail address against user's entry
                    if (gunny == args.Value)
                    {
                        args.IsValid = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void CVDel_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (args.Value == "0")
            {
                args.IsValid = true;
            }
            else if (args.Value == "1")
            {
                args.IsValid = true;
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred , try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void GridView_MadeUpBags_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblSerial = (Label)e.Row.FindControl("lblSerial");
        //    int i = e.Row.RowIndex + 1;
        //    lblSerial.Text = i.ToString();
        //}
        //DataRowView drv = e.Row.DataItem as DataRowView;
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
        //    {
        //        TextBox dp = (TextBox)e.Row.FindControl("TextBox1");
        //    }
        //}
    }
    protected void GridView_MadeUpBags_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string depotId = Session["Depot_DepotID"].ToString();
        int i;
        try
        {
            i = e.RowIndex;
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            Int32 madeupbags = CheckInt(((TextBox)GridView_MadeUpBags.Rows[i].FindControl("TextBox1")).Text);
            string collectiondate = getDate_MDY(((TextBox)GridView_MadeUpBags.Rows[i].FindControl("TextBox2")).Text.ToString());
            Int64 bagsid = CheckBigInt(GridView_MadeUpBags.DataKeys[i].Value.ToString());
            if (con != null)
            {
                con.Open();
                #region UpdateMadeupBags
                SqlCommand cmd_go = new SqlCommand("sp_noofmadeupbagupdate", con);
                cmd_go.CommandType = CommandType.StoredProcedure;
                cmd_go.Parameters.Add("@Collection_Date", SqlDbType.SmallDateTime);
                cmd_go.Parameters["@Collection_Date"].Value = collectiondate;

                cmd_go.Parameters.Add("@No_Made_Up_Bags", SqlDbType.Int);
                cmd_go.Parameters["@No_Made_Up_Bags"].Value = madeupbags;

                cmd_go.Parameters.Add("@Bagsid", SqlDbType.BigInt);
                cmd_go.Parameters["@Bagsid"].Value = bagsid;

                cmd_go.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 20);
                cmd_go.Parameters["@UpdatedBy"].Value = ip;

                int _sts = cmd_go.ExecuteNonQuery();
                if (_sts == 1)
                {
                    GetMadeUpBags(depotId);
                    StringBuilder str = new StringBuilder();
                    str.Append("<script>");
                    str.Append("alert('" + "Record updated Successfully" + "');</script>");
                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                }

                #endregion
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void GridView_MadeUpBags_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView_MadeUpBags.EditIndex = e.NewEditIndex;
    }
    Int32 CheckInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        Int32 ValF = Int32.Parse(ValS);
        return ValF;
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }
    Int64 CheckBigInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        Int64 ValF = Int64.Parse(ValS);
        return ValF;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No";// Session["RefreshButton"];
    }
    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string distid = Session["Depot_DistID"].ToString();
        string depotId = Session["Depot_DepotID"].ToString();
        try
        {
            if (con != null)
            {
                con.Open();
                if (Session["RefreshButton"].ToString() != ViewState["RefreshButton"].ToString())
                {
                    Response.Redirect("DepositorwithWHR.aspx?PopMsg=" + "Record Already Saved! Do not Refresh again!!" + "");
                }
                else
                {
                    #region InsertMadeupBags
                    SqlCommand cmd_go = new SqlCommand("sp_noofbagsinsert", con);
                    cmd_go.CommandType = CommandType.StoredProcedure;
                    cmd_go.Parameters.Add("@WhrId", SqlDbType.NVarChar, 30);
                    cmd_go.Parameters["@WhrId"].Value = ddlwhrlist.SelectedValue;

                    cmd_go.Parameters.Add("@DepotId", SqlDbType.NVarChar, 20);
                    cmd_go.Parameters["@DepotId"].Value = depotId;
                    cmd_go.Parameters.Add("@District_Id", SqlDbType.NVarChar, 20);
                    cmd_go.Parameters["@District_Id"].Value = distid;
                    cmd_go.Parameters.Add("@Godown_Id", SqlDbType.NVarChar, 20);
                    cmd_go.Parameters["@Godown_Id"].Value = ddlgodown.SelectedValue;
                    cmd_go.Parameters.Add("@Stack_Id", SqlDbType.NVarChar, 30);
                    cmd_go.Parameters["@Stack_Id"].Value = ddlstack.SelectedValue;

                    cmd_go.Parameters.Add("@Commodity_Id", SqlDbType.Int);
                    cmd_go.Parameters["@Commodity_Id"].Value = ddlcommodity.SelectedValue;
                    cmd_go.Parameters.Add("@Collection_Date", SqlDbType.SmallDateTime);
                    cmd_go.Parameters["@Collection_Date"].Value = getDate_MDY(txtcollectiondate.Text.ToString());
                    cmd_go.Parameters.Add("@No_Made_Up_Bags", SqlDbType.Int);
                    cmd_go.Parameters["@No_Made_Up_Bags"].Value = txtmadebags.Text.ToString();
                    cmd_go.Parameters.Add("@Gunny_Master_ID", SqlDbType.Int);
                    if (ddl_gunnybag_type_category.Items.Count == 0 || ddl_gunnybag_type_category.SelectedValue == "")
                    {
                        cmd_go.Parameters["@Gunny_Master_ID"].Value = DBNull.Value;
                    }
                    else
                    {
                        cmd_go.Parameters["@Gunny_Master_ID"].Value = CheckInt(ddl_gunnybag_type_category.SelectedValue);
                    }
                    cmd_go.Parameters.Add("@Made_For_Do", SqlDbType.Char, 1);
                    cmd_go.Parameters["@Made_For_Do"].Value = ddlMadeForDo.SelectedValue;

                    cmd_go.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 20);
                    cmd_go.Parameters["@CreatedBy"].Value = ip;

                    cmd_go.Parameters.Add("@Quantity", SqlDbType.NVarChar, 20);
                    cmd_go.Parameters["@Quantity"].Value = txt_quantity.Text.Trim();

                    int _sts = cmd_go.ExecuteNonQuery();
                    cmd_go.Dispose();
                    //_sqlCon.Close();                    
                    if (_sts == 5)
                    {
                        GetMadeUpBags(depotId);
                        StringBuilder str = new StringBuilder();
                        str.Append("<script>");
                        str.Append("alert('" + "Record saved Successfully" + "');</script>");
                        this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                        txtcollectiondate.Text = "";
                        txtmadebags.Text = "";
                        txt_quantity.Text = "";
                        Session["RefreshButton"] = "Yes";
                    }
                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void btnclose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }

    protected void ddlcropYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlcommodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        string depotId = Session["Depot_DepotID"].ToString();
        GetWhrNo();
    }
}

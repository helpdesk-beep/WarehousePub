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
using System.Text;
using System.Globalization;
using System.Threading;
using System.Resources;
using System.Reflection;

public partial class Masters_pCommoditymaster : System.Web.UI.Page
{
    SqlConnection _sqlCon = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        try
        {
            if (Session["lang"] != null)
            {
                if (Session["lang"].ToString() == "Hindi")
                {
                    lblCommodityMas.Text = Resources.hindi.lblCommodityMas;
                    btnaddnew.Text = Resources.hindi.btnaddnew;
                    btninsert.Text = Resources.hindi.btninsert;
                    btncancel.Text = Resources.hindi.btncancel;
                }
            }
        }
        catch (Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred ,try again!" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        if (!IsPostBack)
        {
            grid_bind();
            Commodity_GridView.EditIndex = -1;
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Commodity_Detailsupdate.Visible = true;
        Commodity_detailsinsert.Visible = false;
        btninsert.Visible = false;
        btnaddnew.Visible = true;
        btncancel.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Commodity_detailsinsert.Visible = true;
        btninsert.Visible = true;
        btncancel.Visible = true;
        paneladdcmd.Visible = true;
        btnaddnew.Visible = false;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {

        btnaddnew.Visible = true;
        paneladdcmd.Visible = false;
        Commodity_detailsinsert.Visible = false;
        btninsert.Visible = false;
        btncancel.Visible = false;
    }
    protected void btninsert_Click(object sender, EventArgs e)
    {
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        TextInfo textInfo = cultureInfo.TextInfo;
        //to vaidte page then funcationality of insert record
        Page.Validate("validation");
        if (Page.IsValid)
        {
            DropDownList cmbcommgroup = (DropDownList)Commodity_detailsinsert.Rows[0].FindControl("ddl_Commgroup");
            try
            {
                if (_sqlCon.State == ConnectionState.Closed)
                {
                    _sqlCon.Open();
                }
                SqlCommand _cmd = new SqlCommand("sp_commodityinsert", _sqlCon);
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@CommodityName", SqlDbType.NVarChar, 20);
                _cmd.Parameters["@CommodityName"].Value = textInfo.ToTitleCase(((TextBox)Commodity_detailsinsert.Rows[0].FindControl("TextBox1")).Text.ToString().Trim());
                //added by manoj for status and query order

                _cmd.Parameters.Add("@status", SqlDbType.NVarChar, 2);
                _cmd.Parameters["@status"].Value = ((DropDownList)Commodity_detailsinsert.Rows[0].FindControl("ddl_status")).SelectedValue;

                //added on 9 march tdo insert comm group
                //manoj:09/03/10
                _cmd.Parameters.Add("@commodity_group", SqlDbType.NVarChar, 50);
                _cmd.Parameters["@commodity_group"].Value = cmbcommgroup.SelectedValue.ToString();

                int _sts = _cmd.ExecuteNonQuery();
                _cmd.Dispose();
                _sqlCon.Close();
                if (_sts == 1)
                {
                    grid_bind();
                    btnaddnew.Visible = true;
                    Commodity_detailsinsert.Visible = false;
                    btninsert.Visible = false;
                    ((TextBox)Commodity_detailsinsert.Rows[0].FindControl("TextBox1")).Text = null;

                    //((DropDownList)Commodity_detailsinsert.Rows[0].FindControl("ddl_status")).ClearSelection;

                    btncancel.Visible = false;
                    StringBuilder str = new StringBuilder();
                    str.Append("<script>");
                    str.Append("alert('" + "Record saved Successfully" + "');</script>");
                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                }
                else if (_sts == -1)
                {
                    btnaddnew.Visible = false;
                    StringBuilder str = new StringBuilder();
                    str.Append("<script>");
                    str.Append("alert('" + "This Commodity Name already exist" + "');</script>");
                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                }
                else
                {
                    btnaddnew.Visible = true;
                    StringBuilder str = new StringBuilder();
                    str.Append("<script>");
                    str.Append("alert('" + "Some error has occurred" + "');</script>");
                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                }
            }
            catch (Exception ex)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "Some error has occurred" + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            }
        }
        else
        {
            Session["errDesc"] = "Invalid input data";
            Server.Transfer("~/CustomError.aspx");
        }
        //end on 4 june 2010
    }
    protected void SqlDataSource1_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Record updated successfully" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            Commodity_GridView.DataBind();
        }
        else if (e.AffectedRows == 0)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Commodity Name already exists" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            Commodity_GridView.DataBind();
        }
        else
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lb = (LinkButton)(e.Row.FindControl("LinkButton2"));
            if (lb.Text == "Delete")
            {
                lb.Attributes.Add("onclick", "javascript:return " +
         "confirm('Are you sure you want to delete this record ')");
            }
        }
        
        
        
    }
    

    protected void Commodity_GridView_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void Commodity_GridView_PreRender(object sender, EventArgs e)
    {
        int _count = Commodity_GridView.Rows.Count;
        if (_count > 0)
        {
            Label2.Text = " ";
        }
        else
        {
            Label2.Text = "Currently no record is present";
        }
    }

    protected void Commodity_GridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        int _index = e.NewSelectedIndex;
        ViewState["commid"] = Commodity_GridView.DataKeys[_index].Value.ToString();
        Session["id"] = Commodity_GridView.DataKeys[_index].Value.ToString();
        Session["name"] = Commodity_GridView.Rows[_index].Cells[2].Text.ToString();
        Response.Write(Session["id"].ToString() + "" + Session["name"].ToString());
    }
    
    protected void bind_commgroup()
    {
        SqlCommand cmd = new SqlCommand("select Comm_Group_id,Group_name from tbl_MetaData_STORAGE_COMMODITY_Group", _sqlCon);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (GridViewRow row in Commodity_GridView.Rows)
            {
                
                ((DropDownList)row.Cells[4].FindControl("ddl_Commoditygroup")).DataSource = ds;
                ((DropDownList)row.Cells[4].FindControl("ddl_Commoditygroup")).DataBind();
            }
        }
        cmd.Dispose();
    }
   //by manoj to bind the grid
    protected void grid_bind()
    {
        //bind_commgroup();
        
        try
        {
            if (_sqlCon.State == ConnectionState.Open)
            {
                _sqlCon.Close();
            }
            else
            {
                _sqlCon.Open();
            }
            //changed on 3 march
            //SqlCommand _cmd = new SqlCommand("SELECT Commodity_Id,Commodity_Name,Status FROM tbl_MetaData_STORAGE_COMMODITY ", _sqlCon);
            //SqlCommand _cmd = new SqlCommand("SELECT Commodity_Id,Commodity_Name,Status,case Status when 'Y' then 'Yes' when 'N' then 'No' end as 'ShowStatus'  FROM tbl_MetaData_STORAGE_COMMODITY order by Qry_Order", _sqlCon);

            SqlCommand _cmd = new SqlCommand(@"SELECT c.Commodity_Id,c.Commodity_Name,cg.Group_name,c.Status,cg.Comm_Group_id,
                                                case c.Status when 'Y' then 'Yes' when 'N' then 'No' end as 'ShowStatus'  
                                                FROM tbl_MetaData_STORAGE_COMMODITY c join  tbl_MetaData_STORAGE_COMMODITY_Group cg 
                                                on c.Rep_Grp_Code=cg.Comm_Group_id order by c.Qry_Order", _sqlCon);
            //end
            SqlDataAdapter sda = new SqlDataAdapter(_cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Commodity_GridView.DataSource = ds;
                Commodity_GridView.DataBind();
            }
            _cmd.Dispose();
        }
        catch
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Some error has occurred" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        _sqlCon.Close();
        
    }
    //end by manoj
    protected void Commodity_sdsgridshow_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {
        //yogesh..in acse of nay problem in updating plz refer stored procedure sp_updatecommditymaster
    }

    protected void Commodity_GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Commodity_GridView.EditIndex = e.NewEditIndex;
        grid_bind();
    }

    protected void Commodity_GridView_RowUpdated(object sender, GridViewUpdateEventArgs e)
    {
        //manoj-to update a row inside a grid
        //to validate page on 4 june
        Page.Validate("validation");
        if (Page.IsValid)
        {
            if (_sqlCon.State == ConnectionState.Closed)
            {
                _sqlCon.Open();
            }
            int _index = e.RowIndex;
            //bind_commgroup();
            DropDownList cmbstatus = (DropDownList)Commodity_GridView.Rows[e.RowIndex].FindControl("ddl_status1");
            DropDownList cmbcommgroup = (DropDownList)Commodity_GridView.Rows[e.RowIndex].FindControl("ddl_Commoditygroup");

            try
            {
                SqlCommand _cmd = new SqlCommand("sp_updatecommditymaster", _sqlCon);
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Add("@Commodity_Name", SqlDbType.NVarChar, 20);
                _cmd.Parameters["@Commodity_Name"].Value = ((TextBox)Commodity_GridView.Rows[e.RowIndex].FindControl("txt_Commodity_Name")).Text.ToString().Trim();

                _cmd.Parameters.Add("@status", SqlDbType.NVarChar, 2);
                _cmd.Parameters["@status"].Value = cmbstatus.SelectedValue.ToString();

                _cmd.Parameters.Add("@Commodity_Id", SqlDbType.NVarChar, 2);
                _cmd.Parameters["@Commodity_Id"].Value = Commodity_GridView.DataKeys[_index].Value.ToString();

                //added by manoj on 8- march for comm group

                _cmd.Parameters.Add("@commodity_group", SqlDbType.NVarChar, 50);
                _cmd.Parameters["@commodity_group"].Value = cmbcommgroup.SelectedValue.ToString();
                //end by manoj 

                int i = _cmd.ExecuteNonQuery();
                _cmd.Dispose();
                _sqlCon.Close();
                if (i == 1)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("<script>");
                    str.Append("alert('" + "Record updated sucessfully" + "');</script>");
                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                }
                else
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("<script>");
                    str.Append("alert('" + "some error occured" + "');</script>");
                    this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
                }
                //manoj-to set grid mode in normal and bind grid
                Commodity_GridView.EditIndex = -1;
                grid_bind();

                //end by manoj 
            }
            catch
            {
                StringBuilder str = new StringBuilder();
                str.Append("<script>");
                str.Append("alert('" + "some error occured" + "');</script>");
                this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
            }
        }
        else
        {
            Session["errDesc"] = "Invalid input data";
            Server.Transfer("~/CustomError.aspx");
        }
    }

    protected void Commodity_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int _index = e.RowIndex;
        if (_sqlCon.State == ConnectionState.Closed)
        {
            _sqlCon.Open();
        }
        SqlCommand _cmd = new SqlCommand("sp_delete_commodity", _sqlCon);
        _cmd.CommandType = CommandType.StoredProcedure;
        _cmd.Parameters.Add("@Commodity_Id", SqlDbType.NVarChar, 2);
        _cmd.Parameters["@Commodity_Id"].Value = Commodity_GridView.DataKeys[_index].Value;
        int i = _cmd.ExecuteNonQuery();
        if (i == 1)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Record deleted Successfully" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
        else
        {
            StringBuilder str = new StringBuilder();
            str.Append("<script>");
            str.Append("alert('" + "Record cannot be deleted as it used further" + "');</script>");
            this.Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ClientScript", str.ToString());
        }
         grid_bind();
        _cmd.Dispose();
        _sqlCon.Close();
    }

    protected void Commodity_GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Commodity_GridView.EditIndex = -1;
        grid_bind();
    }

    //to validate page against databse values or collections

    protected void custom_validate_commgroup(object source, ServerValidateEventArgs args)
    {
        try
        {
            string comm_group = "select Comm_Group_id,Group_name from tbl_MetaData_STORAGE_COMMODITY_Group";
            if (_sqlCon.State == ConnectionState.Closed)
            {
                _sqlCon.Open();
            }
            SqlCommand _cmd = new SqlCommand(comm_group, _sqlCon);
            SqlDataAdapter sda = new SqlDataAdapter(_cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                string commgroup;
                args.IsValid = false;    // Assume False
                // Loop through table and compare each record against user's entry
                foreach (DataRowView datarow in dv)
                {
                    // Extract commgroup from the current row
                    commgroup = datarow["Comm_Group_id"].ToString();
                    // Compare commgroup against user's entry
                    if (commgroup == args.Value)
                    {
                        args.IsValid = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void custom_validate(object source, ServerValidateEventArgs args)
    {
        try
        {
            args.IsValid = false;    // Assume False

            if (args.Value == "Y" || args.Value == "N")
            {
                args.IsValid = true;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void Commodity_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Commodity_GridView.PageIndex = e.NewPageIndex;
        grid_bind();
    }
}


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

public partial class Masters_MillerMaster : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        txtMillerName.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
        txtLicenceNo.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
        txtRemarks.Attributes.Add("onkeypress", "return Spc_characteralpha(this);");
      

        if (Session["lang"] != null)
        {

            if (Session["lang"].ToString() == "Hindi")
            {
                lblMilerMaster.Text = Resources.hindi.lblMilerMaster;
                btninsert.Text = Resources.hindi.btninsert;
                btnaddnew.Text = Resources.hindi.btnaddnew;
                btncancel.Text = Resources.hindi.btncancel;
                btnaddnew.Text = Resources.hindi.btnaddnew;
            }
        }
        
        if (!IsPostBack)
        {
            if (Session["Depot_DepotID"].ToString() != "")
            {
                string depotId = Session["Depot_DepotID"].ToString();
                GetMiller(depotId);
            }
        }
    }

    private void GetMiller(string depotId)
    {
        try
        {
            string qry = "SELECT [Miller_Name], [Licence_No], [Remarks], [Miller_Id] FROM [tbl_MetaData_MILLER] where Depot_Id='" + depotId + "' ";
            cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["dsMiller"] = ds;
                fillGrid(ds);
                lbl_count.Visible = false;
                lbl_count.Text = "";
            }
            else
            {
                lbl_count.Visible = true;
                lbl_count.Text = "There is No Miller Found in this branch";
                Millergrid.DataSource = null;
                Millergrid.DataBind();
            }
        }
        catch (Exception)
        {
           ///////
        } 
    }

    private void fillGrid(DataSet ds)
    {
        Millergrid.DataSource = ds.Tables[0];
        Millergrid.DataBind();
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        txtLicenceNo.Text = "";
        txtMillerName.Text = "";
        txtRemarks.Text = "";
        PanelMiller.Visible = false;
        btn_Close.Visible = true;
        btnaddnew.Visible = true;
    }

    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        lbl_Head.Text = "Add New Miller Details";
        btninsert.Text = "Insert";
        txtLicenceNo.Text = "";
        txtMillerName.Text = "";
        txtRemarks.Text = "";
        PanelMiller.Visible = true;
        btn_Close.Visible = false;
        btnaddnew.Visible = false;
    }
    
    protected void btninsert_Click(object sender, EventArgs e)
    {
        string millername = txtMillerName.Text.Trim();
        string licenceno = txtLicenceNo.Text.Trim();
        string remarks = txtRemarks.Text.Trim();
        string depotId = Session["Depot_DepotID"].ToString();
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        if (btninsert.Text == "Insert")
        {
            string stateid = Session["State_StateID"].ToString();
            string distid = Session["Depot_DistID"].ToString();
            try
            {
                if (con != null)
                {
                    con.Open();
                    cmd = new SqlCommand("sp_InsertMiller", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@State_id", stateid);
                    cmd.Parameters.AddWithValue("@District_id", distid);
                    cmd.Parameters.AddWithValue("@Depot_Id", depotId);
                    cmd.Parameters.AddWithValue("@Miller_Name", millername);
                    cmd.Parameters.AddWithValue("@Licence_No", licenceno);
                    cmd.Parameters.AddWithValue("@Remarks", remarks);
                    cmd.Parameters.AddWithValue("@Createdby", ip);
                    int res = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (res == -1)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Unable to Save Record,Name may exists!'); </script> ");
                        btn_Close.Visible = true;
                        btnaddnew.Visible = true;
                    }
                    else if (res == 1)
                    {
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Saved successfully!'); </script> ");
                        btn_Close.Visible = true;
                        btnaddnew.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text=ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }            
        }
        else if (btninsert.Text == "Update")
        {
            string millerId = Millergrid.SelectedRow.Cells[4].Text.Trim();
            try
            {
                if (con != null)
                {
                    string query = "UPDATE tbl_MetaData_MILLER  SET Miller_Name = case when ( '" + millername + "' not in (Select distinct Miller_Name from tbl_metadata_miller where [miller_Id] <> '" + millerId + "')  and '" + millerId + "'  not in ( Select Distinct Miller_Name from tbl_Storage_Arrival_Stock  )) then  '" + millername + "'  else [Miller_Name] end, Licence_No = case when ('" + licenceno + "' =  '" + millerId + "') then [Licence_No]  else '" + licenceno + "' end, [Remarks] = case when ('" + remarks + "' = '" + millerId + "') then Remarks else '" + remarks + "' end WHERE [miller_Id] = '" + millerId + "' ";
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data updated Successfully......'); </script> ");
                    btn_Close.Visible = true;
                    btnaddnew.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message.ToString();
            }
        }
        GetMiller(depotId);
        txtLicenceNo.Text = "";
        txtMillerName.Text = "";
        txtRemarks.Text = "";
        PanelMiller.Visible = false;
    }

    protected void Millergrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["dsMiller"];
        Millergrid.PageIndex = e.NewPageIndex;
        fillGrid(ds);
    }

    protected void Millergrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_Close.Visible = false;
        btnaddnew.Visible = false;
        lbl_Head.Text = "Update Miller Details";
        btninsert.Text = "Update";
        PanelMiller.Visible = true;
        string mid = Millergrid.SelectedRow.Cells[5].Text;
        if (mid != "" && mid != "&nbsp")
        {
            txtMillerName.Text = Millergrid.SelectedRow.Cells[2].Text.Trim();
            txtLicenceNo.Text = Millergrid.SelectedRow.Cells[3].Text.Trim(); 
            txtRemarks.Text = Millergrid.SelectedRow.Cells[4].Text.Trim();
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }

    protected void Millergrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deletes")
        {
            string depotId = Session["Depot_DepotID"].ToString();
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            string mid = Millergrid.DataKeys[row.RowIndex].Value.ToString();
            string query = "delete from tbl_MetaData_MILLER where Miller_Id='" + mid + "' and Depot_Id = '" + depotId.ToString() + "'";
            try
            {
                if (con != null)
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data deleted Successfully......'); </script> ");
                    GetMiller(depotId);
                    btn_Close.Visible = true;
                    btnaddnew.Visible = true;
                }
                else
                {
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Data not deleted...'); </script> ");
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
        }
    }
}

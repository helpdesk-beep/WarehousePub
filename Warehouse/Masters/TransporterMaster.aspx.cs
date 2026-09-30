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

public partial class Masters_TransporterMaster : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["lang"].ToString() == "Hindi")
        {
            btnaddnew.Text = Resources.hindi.btnaddnew;
            btninsert.Text = Resources.hindi.btninsert;
            lblTransporterMaster.Text = Resources.hindi.lblTransporterMaster;
            Label3.Text = Resources.hindi.lblTransporterName;
            btncancel.Text = Resources.hindi.btncancel;
        }
        if (!IsPostBack)
        {
            if (Session["Depot_DepotID"].ToString() != "")
            {
                string depotId = Session["Depot_DepotID"].ToString();
                GetTransporter(depotId);
            }
        }
    }

    private void GetTransporter(string depotid)
    {
        try
        {
            string qry = "select * from dbo.tbl_metadata_transport where DepotId='" + depotid + "' ";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["dsTransport"] = ds;
                fillGrid(ds);
                lbl_msg.Visible = false;
            }
            else
            {
                lbl_msg.Visible = true;
                lbl_msg.Text = "There is no Transporter Available";
            }
        }
        catch (Exception)
        {
            //////
        }
    }

    private void fillGrid(DataSet ds)
    {
        transport_GridView.DataSource = ds.Tables[0];
        transport_GridView.DataBind();
        lblRowCount.Text = "Total records are : " + transport_GridView.Rows.Count.ToString();
    }

    protected void btninsert_Click(object sender, EventArgs e)
    {
        string transpoter = txtTransporter.Text.ToString().Trim();
        string depotid = Session["Depot_DepotID"].ToString();
        if (txtTransporter.Text != "")
        {
            try
            {
                con.Open();
                if (btninsert.Text == "Insert")
                {
                    SqlCommand cmd_sp = new SqlCommand("sp_transporterinsert", con);
                    cmd_sp.CommandType = CommandType.StoredProcedure;
                    cmd_sp.Parameters.Add("@Transpoter_Name", SqlDbType.VarChar, 50);
                    cmd_sp.Parameters["@Transpoter_Name"].Value = transpoter;
                    cmd_sp.Parameters.Add("@DepotID", SqlDbType.NVarChar, 20);
                    cmd_sp.Parameters["@DepotID"].Value = depotid;
                    int _sts = cmd_sp.ExecuteNonQuery();
                    if (_sts == 1)
                    {
                        btn_Can.Visible = true;
                        btnaddnew.Visible = true;
                        GetTransporter(depotid);
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record saved Successfully'); </script> ");
                    }
                    else if (_sts == -1)
                    {
                        btnaddnew.Visible = false;
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('This  Name already exist or records exists for this trasporter'); </script> ");
                    }
                }
                else if (btninsert.Text == "Update")
                {
                    string transporterid = ViewState["Transport_id"].ToString();
                    SqlCommand cmd_sp = new SqlCommand("sp_transporterupdate", con);
                    cmd_sp.CommandType = CommandType.StoredProcedure;
                    cmd_sp.Parameters.Add("@Transpoter_Name", SqlDbType.VarChar, 50);
                    cmd_sp.Parameters["@Transpoter_Name"].Value = transpoter;
                    cmd_sp.Parameters.Add("@DepotID", SqlDbType.NVarChar, 20);
                    cmd_sp.Parameters["@DepotID"].Value = depotid;
                    cmd_sp.Parameters.Add("@Transporter_Id", SqlDbType.Int);
                    cmd_sp.Parameters["@Transporter_Id"].Value = CheckInt(transporterid);
                    int _sts = cmd_sp.ExecuteNonQuery();
                    if (_sts == 1)
                    {
                        btn_Can.Visible = true;
                        btnaddnew.Visible = true;
                        GetTransporter(depotid);
                        Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record update Successfully'); </script> ");
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occurred,Please Try Again..'); </script> ");
            }
            finally
            {
                con.Close();
            }
            txtTransporter.Text = "";
            Panel_Transpoter.Visible = false;
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        txtTransporter.Text = "";
        Panel_Transpoter.Visible = false;
        btn_Can.Visible = true;
        btnaddnew.Visible = true;
    }

    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        lbl_Head.Text = "Add New Transporter Details";
        Panel_Transpoter.Visible = true;
        btn_Can.Visible = false;
        btnaddnew.Visible = false;
    }

    protected void transport_GridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_Head.Text = "Update Transporter Details";
        Panel_Transpoter.Visible = true;
        btninsert.Text = "Update";
        string transpoter = transport_GridView.SelectedRow.Cells[3].Text;
        txtTransporter.Text = transpoter;
        string transporterid = transport_GridView.SelectedRow.Cells[4].Text;
        ViewState["Transport_id"] = transporterid;
        btn_Can.Visible = false;
        btnaddnew.Visible = false;
    }

    protected void transport_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerial = (Label)e.Row.FindControl("Label1");
            int i = e.Row.RowIndex + 1;
            lblSerial.Text = i.ToString();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lb = new LinkButton();
            lb = (LinkButton)e.Row.Cells[0].Controls[0];
            lb.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete this row?');");
        }
    }

    protected void transport_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet ds = (DataSet)ViewState["dsTransport"];
        transport_GridView.PageIndex = e.NewPageIndex;
        fillGrid(ds);
    }

    Int32 CheckInt(string Val)
    {
        string st = "";
        string ValS = ((Val != st) ? (Val) : "0");
        Int32 ValF = Int32.Parse(ValS);
        return ValF;
    }

    protected void transport_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string depotid = Session["Depot_DepotID"].ToString();
        int _rowindex = e.RowIndex;
        string tid = transport_GridView.DataKeys[_rowindex].Value.ToString();
        try
        {
            con.Open();
            if (tid != "")
            {
                Int32 transporterid = CheckInt(tid);
                SqlCommand cmd_sp = new SqlCommand("sp_delete_transport", con);
                cmd_sp.CommandType = CommandType.StoredProcedure;
                cmd_sp.Parameters.Add("@Transporter_Id", SqlDbType.Int);
                cmd_sp.Parameters["@Transporter_Id"].Value = transporterid;
                int _sts = cmd_sp.ExecuteNonQuery();
                if (_sts == 1)
                {
                    GetTransporter(depotid);
                    Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record deleted Successfully'); </script> ");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occurred'); </script> ");
        }
        finally
        {
            con.Close();
        }
    }

    protected void btn_Can_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
}

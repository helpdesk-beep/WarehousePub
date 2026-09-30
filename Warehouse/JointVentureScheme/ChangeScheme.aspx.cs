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

public partial class JointVentureScheme_ChangeScheme : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltran;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Search()
    {
        qry = "Select Registration_ID,(select Warehouse_Name from tbl_WarehouseRegistration WR where WGO.Registration_Id=WR.Registration_Id) as WarehouseName,Godown_Id,Godown_No,G_OfferCapacity,G_Scheme from tbl_Warehouse_Godown_Offer WGO where WGO.Registration_Id = '" + txtSearch.Text + "' ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvGodown.DataSource = ds.Tables[0];
            gvGodown.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found Please Enter Correct Registration No.')", true);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        GridViewRow gvr = gvGodown.SelectedRow;
        
        if (txtSearch.Text != "" && txtSearch.Text.Length > 5)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string qryGodownInsert = "insert into tbl_Warehouse_Godown_Offer_Log select * from tbl_Warehouse_Godown_Offer where Registration_Id = '" + txtSearch.Text + "' and Godown_Id='" + gvr.Cells[3].Text + "'";
            SqlCommand cmd = new SqlCommand(qryGodownInsert, con);
            int i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                string qryUpdate = "update tbl_Warehouse_Godown_Offer set G_Scheme='" + ddlScheme.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and Godown_Id='" + gvr.Cells[3].Text + "'";
                SqlCommand cmd2 = new SqlCommand(qryUpdate, con);
                int j = cmd2.ExecuteNonQuery();
                if (j == 1)
                {
                    string qryInspInsert = "insert into tbl_Godown_Inspection_Log select * from tbl_Godown_Inspection where Registration_Id = '" + txtSearch.Text + "' and GodownId='" + gvr.Cells[3].Text + "'";
                    SqlCommand cmd3 = new SqlCommand(qryInspInsert, con);
                    int k = cmd3.ExecuteNonQuery();
                    if (k == 1)
                    {
                        string qryInspUpdate = "update tbl_Godown_Inspection set Offer_Scheme='" + ddlScheme.SelectedValue + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId='" + gvr.Cells[3].Text + "'";
                        SqlCommand cmd4 = new SqlCommand(qryInspUpdate, con);
                        int L = cmd4.ExecuteNonQuery();
                    }
                    string qryAgreeInsert = "insert into tbl_Godown_Agreement_Log select * from tbl_Godown_Agreement where Registration_Id = '" + txtSearch.Text + "' and GodownId='" + gvr.Cells[3].Text + "'";
                    SqlCommand cmd5 = new SqlCommand(qryAgreeInsert, con);
                    int M = cmd5.ExecuteNonQuery();
                    if (M == 1)
                    {
                        string qryAgreeUpdate = "update tbl_Godown_Agreement set G_Scheme_Rate='" + ddlScheme.SelectedValue + "',Godown_Type='" + ddlScheme.SelectedItem.Text + "' ,UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId='" + gvr.Cells[3].Text + "'";
                        SqlCommand cmd6 = new SqlCommand(qryAgreeUpdate, con);
                        int N = cmd6.ExecuteNonQuery();
                    }
                    if (sqlcon.State == ConnectionState.Closed)
                    {
                        sqlcon.Open();
                    }
                    string qryAgreeMapInsert = "insert into tbl_Agreemented_Godown_Mapping_Log select * from tbl_Agreemented_Godown_Mapping where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "'";
                    SqlCommand cmd7 = new SqlCommand(qryAgreeMapInsert, sqlcon);
                    int O = cmd7.ExecuteNonQuery();
                    if (O == 1)
                    {
                        string qryAgreeMapUpdate = "update tbl_Agreemented_Godown_Mapping set Godown_Type='" + ddlScheme.SelectedItem.Text + "' ,UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "'";
                        SqlCommand cmd8 = new SqlCommand(qryAgreeMapUpdate, sqlcon);
                        int P = cmd8.ExecuteNonQuery();
                    }
                    string qryInspMapInsert = "insert into tbl_Inspected_Godown_Mapping_Log select * from tbl_Inspected_Godown_Mapping where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "'";
                    SqlCommand cmd9 = new SqlCommand(qryInspMapInsert, sqlcon);
                    int Q = cmd9.ExecuteNonQuery();
                    if (Q == 1)
                    {
                        string qryInspMapUpdate = "update tbl_Inspected_Godown_Mapping set Godown_Type='" + ddlScheme.SelectedItem.Text + "' ,UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "'";
                        SqlCommand cmd10 = new SqlCommand(qryInspMapUpdate, sqlcon);
                        int R = cmd10.ExecuteNonQuery();
                    }

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Successfully Save...'); </script> ");
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Registration ID .....')", true);
        }
        Search();
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        if (sqlcon.State == ConnectionState.Open)
        {
            sqlcon.Close();
        }
    }

    protected void gvGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = gvGodown.SelectedRow;
        if (gvr.Cells[6].Text == "55")
        {
            ddlScheme.SelectedValue = "55";
            //ddlScheme.SelectedItem.Text = "NON-WDRA";
        }
        else if (gvr.Cells[6].Text == "60")
        {
            ddlScheme.SelectedValue = "60";
               // ddlScheme.SelectedItem.Text = "WDRA";
        }
        if (gvGodown.SelectedRow != null)
        {
            gv.Visible = true;
            btnUpdate.Visible = true;
        }
        else
        {
            gv.Visible = false;
            btnUpdate.Visible = false;
        }
    }
}

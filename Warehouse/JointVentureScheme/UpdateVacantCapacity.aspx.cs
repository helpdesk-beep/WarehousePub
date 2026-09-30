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

public partial class JointVentureScheme_UpdateVacantCapacity : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltran;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text != "" && txtSearch.Text.Length > 5)
        {
            Search();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Registration ID .....')", true);
        }
    }
    public void Search()
    {
        qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer I where I.Godown_ID=INS.GodownId ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity from tbl_Godown_Inspection INS left join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId where INS.Registration_Id= '" + txtSearch.Text + "' ";
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

    protected void gvGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = gvGodown.SelectedRow;
        txtVacantCptUpdate.Text = gvr.Cells[7].Text;
        txtAgreeCpt.Text = gvr.Cells[10].Text;
        //ddlFitUnfit.SelectedItem.Text = gvr.Cells[8].Text;
        if (gvr.Cells[8].Text == "FIT")
        {
            ddlFitUnfit.SelectedValue = "1";        
        }
        else if (gvr.Cells[8].Text == "UNFIT")
        {
            ddlFitUnfit.SelectedValue = "2";
        }
        if (gvGodown.SelectedRow != null)
        {
            gv.Visible = true;
            btnUpdateCpt.Visible = true;
        }
        else
        {
            gv.Visible = false;
            btnUpdateCpt.Visible = false;
        }
    }
    protected void btnUpdateCpt_Click(object sender, EventArgs e)
    {
        {
            string Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            GridViewRow gvr = gvGodown.SelectedRow;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (txtSearch.Text != "" && txtSearch.Text.Length > 5)
            {
                int chkAgr = checkAgreement();
                if (chkAgr == 0)
                {
                    string qryInsert = "insert into tbl_Godown_Inspection_Log select * from tbl_Godown_Inspection where Registration_Id = '" + txtSearch.Text + "' and Inspection_Id='" + gvr.Cells[1].Text + "' and GodownId='" + gvr.Cells[3].Text + "'";
                    SqlCommand cmd = new SqlCommand(qryInsert, con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        string qryUpdate = "Update tbl_Godown_Inspection set Vacant_Capacity='" + txtVacantCptUpdate.Text + "',Fit_Unfit = '" + ddlFitUnfit.SelectedItem.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and Inspection_Id='" + gvr.Cells[1].Text + "' and GodownId='" + gvr.Cells[3].Text + "'";
                        SqlCommand cmd1 = new SqlCommand(qryUpdate, con);
                        int j = cmd1.ExecuteNonQuery();
                        if (j == 1)
                        {
                            string qryInsert3 = "insert into tbl_Inspected_Godown_Mapping_log select * from tbl_Inspected_Godown_Mapping where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "'";
                            sqlcon.Open();
                            SqlCommand cmd4 = new SqlCommand(qryInsert3, sqlcon);
                            int m = cmd4.ExecuteNonQuery();
                            if (m == 1)
                            {
                                string qryUpdate3 = "Update tbl_Inspected_Godown_Mapping set Vacant_Capacity='" + txtVacantCptUpdate.Text + "',Fit_Unfit='" + ddlFitUnfit.SelectedItem.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "'";
                                SqlCommand cmd5 = new SqlCommand(qryUpdate3, sqlcon);
                                int n = cmd5.ExecuteNonQuery();
                                sqlcon.Close();
                            }
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Updated Successfully')", true);
                        }
                    }
                }
                else if (chkAgr == 1 && CheckBox1.Checked==true)
                {
                    if (txtAgreeCpt.Text != null && txtAgreeCpt.Text != "")
                    {
                        string qryInsert = "insert into tbl_Godown_Inspection_Log select * from tbl_Godown_Inspection where Registration_Id = '" + txtSearch.Text + "' and Inspection_Id='" + gvr.Cells[1].Text + "' and GodownId='" + gvr.Cells[3].Text + "'";
                        SqlCommand cmd = new SqlCommand(qryInsert, con);
                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                        {
                            string qryUpdate = "Update tbl_Godown_Inspection set Vacant_Capacity='" + txtVacantCptUpdate.Text + "',Fit_Unfit = '" + ddlFitUnfit.SelectedItem.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and Inspection_Id='" + gvr.Cells[1].Text + "' and GodownId='" + gvr.Cells[3].Text + "'";
                            SqlCommand cmd1 = new SqlCommand(qryUpdate, con);
                            int j = cmd1.ExecuteNonQuery();
                            if (j == 1)
                            {
                                string qryInsert2 = "insert into tbl_Godown_Agreement_Log select * from tbl_Godown_Agreement where Registration_Id = '" + txtSearch.Text + "' and Inspection_Id='" + gvr.Cells[1].Text + "' and GodownId='" + gvr.Cells[3].Text + "'";
                                SqlCommand cmd2 = new SqlCommand(qryInsert2, con);
                                int k = cmd2.ExecuteNonQuery();
                                if (k == 1)
                                {
                                    string qryUpdate2 = "Update tbl_Godown_Agreement set Insp_Capacity='" + txtVacantCptUpdate.Text + "',Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and Inspection_Id='" + gvr.Cells[1].Text + "' and GodownId='" + gvr.Cells[3].Text + "'";
                                    SqlCommand cmd3 = new SqlCommand(qryUpdate2, con);
                                    int l = cmd3.ExecuteNonQuery();
                                    if (l == 1)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Updated Successfully')", true);
                                    }
                                }
                                string qryInsert3 = "insert into tbl_Inspected_Godown_Mapping_log select * from tbl_Inspected_Godown_Mapping where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "'";
                                sqlcon.Open();
                                SqlCommand cmd4 = new SqlCommand(qryInsert3, sqlcon);
                                int m = cmd4.ExecuteNonQuery();
                                if (m == 1)
                                {
                                    string qryUpdate3 = "Update tbl_Inspected_Godown_Mapping set Vacant_Capacity='" + txtVacantCptUpdate.Text + "',Fit_Unfit='" + ddlFitUnfit.SelectedItem.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "'";
                                    SqlCommand cmd5 = new SqlCommand(qryUpdate3, sqlcon);
                                    int n = cmd5.ExecuteNonQuery();
                                }
                                string qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_log select * from tbl_Agreemented_Godown_Mapping where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "'";
                                SqlCommand cmd6 = new SqlCommand(qryInsert4, sqlcon);
                                int o = cmd6.ExecuteNonQuery();
                                if (o == 1)
                                {
                                    string qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "'";
                                    SqlCommand cmd7 = new SqlCommand(qryUpdate4, sqlcon);
                                    int n = cmd7.ExecuteNonQuery();
                                }
                                sqlcon.Close();
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Agreement Capacity')", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Agreement Is updated online Please Check it and Delete')", true);

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
        }
    }
    public int checkAgreement()
    {
        GridViewRow gvr = gvGodown.SelectedRow;      
        int chk = 0;
        string strsql = " select * from tbl_Godown_Agreement where Registration_Id = '" + txtSearch.Text + "' and Inspection_Id='" + gvr.Cells[1].Text + "' and GodownId='" + gvr.Cells[3].Text + "'";
        SqlCommand cmd = new SqlCommand(strsql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            chk = 1;
        }
        else
        {
            chk = 0;
        }
        return chk;
    }
}


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

public partial class WarehouseLevel_DeleteRequestform_PvtW : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    DataSet ds = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    string Branch = "";
    string Distid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillCommodity();
            fillGodnList();
        }
    }
    private void fillGodnList()
    {
        if (Session["Depot_DistID"] != null)
        {
            if (Session["BranchType"].ToString() == "G" && Session["GodownID_New"].ToString() != null)
            {
                //string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN where DistrictId ='" + Session["Depot_DistID"].ToString() + "' and  BranchID  ='" + Session["G_BranchId"].ToString() + "' order by Godown_Name Asc";
                string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN where DistrictId ='" + Session["Depot_DistID"].ToString() + "' and  BranchID  ='" + Session["G_BranchId"].ToString() + "' and Godown_ID='" + Session["GodownID_New"].ToString() + "' order by Godown_Name Asc";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_godown.DataSource = ds.Tables[0];
                    ddl_godown.DataTextField = "Godown_Name";
                    ddl_godown.DataValueField = "Godown_ID";
                    ddl_godown.DataBind();
                    ddl_godown.Items.Insert(0, "--Select--");
                }
            }
            else
            {
                string query = " SELECT Godown_ID,Godown_Name  FROM tbl_MetaData_GODOWN where DistrictId ='" + Session["Depot_DistID"].ToString() + "' and  BranchID  ='" + Session["G_BranchId"].ToString() + "' order by Godown_Name Asc";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_godown.DataSource = ds.Tables[0];
                    ddl_godown.DataTextField = "Godown_Name";
                    ddl_godown.DataValueField = "Godown_ID";
                    ddl_godown.DataBind();
                    ddl_godown.Items.Insert(0, "--Select--");
                }
            }
        }
    }

    private void fillCommodity()
    {
        string query = "SELECT Commodity_Id, Commodity_Name FROM tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name Asc";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlcommodity.Items.Clear();
            ddlcommodity.DataSource = ds.Tables[0];
            ddlcommodity.DataTextField = "Commodity_Name";
            ddlcommodity.DataValueField = "Commodity_Id";
            ddlcommodity.DataBind();
            ddlcommodity.Items.Insert(0, "--Select--");
            ddlcommodity.SelectedValue = "22";
        }
    }

    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtdellist.SelectedValue == "01")
        {
            FillGridOpening();
        }
        else if (rbtdellist.SelectedValue == "02")
        {
            FillGridreceiptdetails();
        }
        else if (rbtdellist.SelectedValue == "03")
        {
            FillGridwhr();
        }
        else if (rbtdellist.SelectedValue == "04")
        {
            fillGatepassGrid();
        }
        else if (rbtdellist.SelectedValue == "05")
        {
            filldeliveryorder();
        }
        else if (rbtdellist.SelectedValue == "06")
        {
            GetStack();
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        //string whrdate = "";
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string Reqid = "";
        string ComReqid = "";
        string Branch = Session["G_BranchId"].ToString();
        string Dist = Session["Depot_DistID"].ToString();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        if (rbtdellist.SelectedValue == "01")
        {
            string ComQueryMax = "select isnull(Max(Req_id),0) from tbl_Opening_Delete_Req";
            cmd = new SqlCommand(ComQueryMax, con); // 
            string str4 = cmd.ExecuteScalar().ToString();
            if (Convert.ToInt64(str4) != 0)
            {
                ComReqid = Convert.ToString(Convert.ToInt64(str4) + 1);
            }
            else
            {
                ComReqid = "1000";
            }

            if (gv_Opening.Rows.Count > 0)
            {
                foreach (GridViewRow gr2 in gv_Opening.Rows)
                {
                    CheckBox chk_Delete = new CheckBox();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == false || chk_Delete.Enabled == false)
                    {
                    }
                    else
                    {
                        string WHRID = Convert.ToString(gv_Opening.DataKeys[gr2.RowIndex].Value);
                        string Depositor = gr2.Cells[3].Text.ToString();
                        string whrdate = getDate_MDY(gr2.Cells[2].Text.ToString());
                        int Bags = Convert.ToInt32(gr2.Cells[5].Text.ToString());
                        decimal Weigt = Convert.ToDecimal(gr2.Cells[6].Text.ToString());
                        string QueryMax = "select isnull(Max(CONVERT (int,Req_id)),0) from tbl_Opening_Delete_Req";
                        cmd = new SqlCommand(QueryMax, con); // 
                        string str3 = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt64(str3) != 0)
                        {
                            Reqid = Convert.ToString(Convert.ToInt64(str3) + 1);
                        }
                        else
                        {
                            Reqid = "1000";
                        }
                        if (Reqid != string.Empty)
                        {
                            string qry = "Insert Into tbl_Opening_Delete_Req (Req_id,ComReq_Id ,Districtid,Depotid,Depositor_Name,whr_id,commodity_id,Godown_id,whr_Date,No_of_Bags,Quantity,Operator_Name,Opeartor_Mob,Bm_Name,Bm_Mob,Req_date,Status,ip,DeleteStatus) values ('" + Reqid.ToString() + "','" + ComReqid.ToString() + "','" + Dist + "','" + Branch + "','" + Depositor + "','" + WHRID + "','" + ddlcommodity.SelectedValue.ToString() + "','" + ddl_godown.SelectedValue.ToString() + "','" + whrdate + "','" + Bags + "','" + Weigt + "','" + txtopname.Text.Trim().ToString() + "','" + txtopmobile.Text.Trim().ToString() + "','" + txtbmname.Text.Trim().ToString() + "','" + txtbmmob.Text.Trim().ToString() + "',getdate(),'O','" + ip + "','N')";
                            cmd = new SqlCommand(qry, con);
                            int c = cmd.ExecuteNonQuery();
                            if (c > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Request saved Successfully...')", true);
                                btnPrint.Visible = true;
                                Session["ComReq_Id"] = ComReqid;
                            }
                        }
                    }
                }
            }
        }
        else if (rbtdellist.SelectedValue == "02")
        {
            string AceptDate = "";
            string Chalanno = "";
            string Truckno = "";
            string AcceptNo = "";
            string ComQueryMax = "select isnull(Max(convert(int,Req_id)),0) from tbl_Receipt_Delete_Req";
            cmd = new SqlCommand(ComQueryMax, con); // 
            string str4 = cmd.ExecuteScalar().ToString();
            if (Convert.ToInt64(str4) != 0)
            {
                ComReqid = Convert.ToString(Convert.ToInt64(str4) + 1);
            }
            else
            {
                ComReqid = "1000";
            }

            if (gv_Receiptdetails.Rows.Count > 0)
            {
                foreach (GridViewRow gr2 in gv_Receiptdetails.Rows)
                {
                    CheckBox chk_Delete = new CheckBox();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == false || chk_Delete.Enabled == false)
                    {
                    }
                    else
                    {
                        string ArrivalID = Convert.ToString(gv_Receiptdetails.DataKeys[gr2.RowIndex].Value);
                        string Receiptid = gr2.Cells[9].Text.ToString();
                        if (gr2.Cells[1].Text.ToString() != "&nbsp;")
                        {
                            Chalanno = gr2.Cells[1].Text.ToString();
                        }
                        else
                        {
                            Chalanno = null;
                        }

                        if (gr2.Cells[2].Text.ToString() != "&nbsp;")
                        {
                            Truckno = gr2.Cells[2].Text.ToString();
                        }
                        else
                        {
                            Truckno = null;
                        }

                        if (gr2.Cells[3].Text.ToString() != "&nbsp;")
                        {
                            AcceptNo = gr2.Cells[3].Text.ToString();
                        }
                        else
                        {
                            AcceptNo = null;
                        }

                        if (gr2.Cells[4].Text.ToString() != "&nbsp;")
                        {
                            AceptDate = getDate_MDY(gr2.Cells[4].Text.ToString());
                        }
                        else
                        {
                            AceptDate = getDate_MDY("01/01/2015");
                        }

                        //string AceptDate = getDate_MDY(gr2.Cells[4].Text.ToString());
                        //AceptDate = gr2.Cells[4].Text.ToString();
                        string Depositdate = getDate_MDY(gr2.Cells[5].Text.ToString());
                        int Bags = Convert.ToInt32(gr2.Cells[6].Text.ToString());
                        decimal Weigt = Convert.ToDecimal(gr2.Cells[7].Text.ToString());
                        string QueryMax = "select isnull(Max(convert(int,Req_id)),0) from tbl_Receipt_Delete_Req";
                        cmd = new SqlCommand(QueryMax, con); // 
                        string str3 = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt64(str3) != 0)
                        {
                            Reqid = Convert.ToString(Convert.ToInt64(str3) + 1);
                        }
                        else
                        {
                            Reqid = "1000";
                        }
                        if (Reqid != string.Empty)
                        {
                            string qry = "Insert Into tbl_Receipt_Delete_Req (Req_id,ComReq_Id ,Districtid,Depotid,Receipt_id,Arrivalstock_id,commodity_id,Godown_id,TC_No,Truck_No,AcceptanceNo,Accept_Date,No_of_Bags,Quantity,Deposit_Date,Operator_Name,Opeartor_Mob,Bm_Name,Bm_Mob,Req_date,ip,DeleteStatus) values ('" + Reqid.ToString() + "','" + ComReqid.ToString() + "','" + Dist + "','" + Branch + "','" + Receiptid + "','" + ArrivalID + "','" + ddlcommodity.SelectedValue.ToString() + "','" + ddl_godown.SelectedValue.ToString() + "','" + Chalanno + "','" + Truckno + "','" + AcceptNo + "','" + AceptDate + "','" + Bags + "','" + Weigt + "','" + Depositdate + "','" + txtopname.Text.Trim().ToString() + "','" + txtopmobile.Text.Trim().ToString() + "','" + txtbmname.Text.Trim().ToString() + "','" + txtbmmob.Text.Trim().ToString() + "',getdate(),'" + ip + "','N')";
                            cmd = new SqlCommand(qry, con);
                            int c = cmd.ExecuteNonQuery();
                            if (c > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Request saved Successfully...')", true);
                                btnPrint.Visible = true;
                                Session["ComReq_Id"] = ComReqid;
                            }
                        }
                    }
                }
            }
        }
        else if (rbtdellist.SelectedValue == "03")
        {
            string ComQueryMax = "select isnull(Max(CONVERT (int,Req_id)),0) from tbl_Opening_Delete_Req";
            cmd = new SqlCommand(ComQueryMax, con); // 
            string str4 = cmd.ExecuteScalar().ToString();
            if (Convert.ToInt64(str4) != 0)
            {
                ComReqid = Convert.ToString(Convert.ToInt64(str4) + 1);
            }
            else
            {
                ComReqid = "1000";
            }
            if (gv_whr.Rows.Count > 0)
            {
                foreach (GridViewRow gr2 in gv_whr.Rows)
                {
                    CheckBox chk_Delete = new CheckBox();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == false || chk_Delete.Enabled == false)
                    {
                    }
                    else
                    {
                        string WHRID = Convert.ToString(gv_whr.DataKeys[gr2.RowIndex].Value);
                        string Depositor = gr2.Cells[3].Text.ToString();
                        string whrdate = getDate_MDY(gr2.Cells[2].Text.ToString());
                        int Bags = Convert.ToInt32(gr2.Cells[5].Text.ToString());
                        decimal Weigt = Convert.ToDecimal(gr2.Cells[6].Text.ToString());
                        string QueryMax = "select isnull(Max(CONVERT (int,Req_id)),0) from tbl_Opening_Delete_Req";
                        cmd = new SqlCommand(QueryMax, con); // 
                        string str3 = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt64(str3) != 0)
                        {
                            Reqid = Convert.ToString(Convert.ToInt64(str3) + 1);
                        }
                        else
                        {
                            Reqid = "1000";
                        }
                        if (Reqid != string.Empty)
                        {
                            string qry = "Insert Into tbl_Opening_Delete_Req (Req_id,ComReq_Id ,Districtid,Depotid,Depositor_Name,whr_id,commodity_id,Godown_id,whr_Date,No_of_Bags,Quantity,Operator_Name,Opeartor_Mob,Bm_Name,Bm_Mob,Req_date,Status,ip,DeleteStatus) values ('" + Reqid.ToString() + "','" + ComReqid + "','" + Dist + "','" + Branch + "','" + Depositor + "','" + WHRID + "','" + ddlcommodity.SelectedValue.ToString() + "','" + ddl_godown.SelectedValue.ToString() + "','" + whrdate + "','" + Bags + "','" + Weigt + "','" + txtopname.Text.Trim().ToString() + "','" + txtopmobile.Text.Trim().ToString() + "','" + txtbmname.Text.Trim().ToString() + "','" + txtbmmob.Text.Trim().ToString() + "',getdate(),'D','" + ip + "','N')";
                            cmd = new SqlCommand(qry, con);
                            int c = cmd.ExecuteNonQuery();
                            if (c > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Request saved Successfully...')", true);
                                btnPrint.Visible = true;
                                Session["ComReq_Id"] = ComReqid;
                            }
                        }
                    }
                }
            }
        }
        else if (rbtdellist.SelectedValue == "04")
        {
            string ComQueryMax = "select isnull(Max(convert(int,Req_id)),0) from tbl_Gatepass_Delete_Req";
            cmd = new SqlCommand(ComQueryMax, con); // 
            string str4 = cmd.ExecuteScalar().ToString();
            if (Convert.ToInt64(str4) != 0)
            {
                ComReqid = Convert.ToString(Convert.ToInt64(str4) + 1);
            }
            else
            {
                ComReqid = "1000";
            }
            if (gv_gatepass.Rows.Count > 0)
            {
                foreach (GridViewRow gr2 in gv_gatepass.Rows)
                {
                    CheckBox chk_Delete = new CheckBox();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == false || chk_Delete.Enabled == false)
                    {
                    }
                    else
                    {
                        string Gatepassno = Convert.ToString(gv_gatepass.DataKeys[gr2.RowIndex].Value);
                        string Gatepassdate = getDate_MDY(gr2.Cells[5].Text.ToString());
                        int Bags = Convert.ToInt32(gr2.Cells[3].Text.ToString());
                        decimal Weigt = Convert.ToDecimal(gr2.Cells[4].Text.ToString());
                        string QueryMax = "select isnull(Max(CONVERT (int,Req_id)),0) from tbl_Gatepass_Delete_Req";
                        cmd = new SqlCommand(QueryMax, con); // 
                        string str3 = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt64(str3) != 0)
                        {
                            Reqid = Convert.ToString(Convert.ToInt64(str3) + 1);
                        }
                        else
                        {
                            Reqid = "1000";
                        }
                        if (Reqid != string.Empty)
                        {
                            string qry = "Insert Into tbl_Gatepass_Delete_Req (Req_id ,ComReq_Id,Districtid,Depotid,GatepassNo,commodity_id,Godown_id,Gatepass_Date,No_of_Bags,Quantity,Operator_Name,Opeartor_Mob,Bm_Name,Bm_Mob,Req_date,ip,DeleteStatus) values ('" + Reqid.ToString() + "','" + ComReqid.ToString() + "','" + Dist + "','" + Branch + "','" + Gatepassno + "','" + ddlcommodity.SelectedValue.ToString() + "','" + ddl_godown.SelectedValue.ToString() + "','" + Gatepassdate + "','" + Bags + "','" + Weigt + "','" + txtopname.Text.Trim().ToString() + "','" + txtopmobile.Text.Trim().ToString() + "','" + txtbmname.Text.Trim().ToString() + "','" + txtbmmob.Text.Trim().ToString() + "',getdate(),'" + ip + "','N')";
                            cmd = new SqlCommand(qry, con);
                            int c = cmd.ExecuteNonQuery();
                            if (c > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Request saved Successfully...')", true);
                                btnPrint.Visible = true;
                                Session["ComReq_Id"] = ComReqid;
                            }
                        }
                    }
                }
            }
        }
        else if (rbtdellist.SelectedValue == "05")
        {
            string ComQueryMax = "select isnull(Max(CONVERT (int,Req_id)),0) from tbl_Deliveryorder_Delete_Req";
            cmd = new SqlCommand(ComQueryMax, con);
            string str4 = cmd.ExecuteScalar().ToString();
            if (Convert.ToInt64(str4) != 0)
            {
                ComReqid = Convert.ToString(Convert.ToInt64(str4) + 1);
            }
            else
            {
                ComReqid = "1000";
            }
            if (gvDeliveryOrder.Rows.Count > 0)
            {
                foreach (GridViewRow gr2 in gvDeliveryOrder.Rows)
                {
                    CheckBox chk_Delete = new CheckBox();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == false || chk_Delete.Enabled == false)
                    {
                    }
                    else
                    {
                        string Deliveryid = Convert.ToString(gvDeliveryOrder.DataKeys[gr2.RowIndex].Value);
                        //string Gatepassno = gr2.Cells[7].Text.ToString();
                        string Depositor = gr2.Cells[2].Text.ToString();
                        string DOdate = getDate_MDY(gr2.Cells[3].Text.ToString());
                        int Bags = Convert.ToInt32(gr2.Cells[4].Text.ToString());
                        decimal Weigt = Convert.ToDecimal(gr2.Cells[5].Text.ToString());
                        string QueryMax = "select isnull(Max(CONVERT (int,Req_id)),0) from tbl_Deliveryorder_Delete_Req";
                        cmd = new SqlCommand(QueryMax, con); // 
                        string str3 = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt64(str3) != 0)
                        {
                            Reqid = Convert.ToString(Convert.ToInt64(str3) + 1);
                        }
                        else
                        {
                            Reqid = "1000";
                        }
                        if (Reqid != string.Empty)
                        {
                            //string qry = "Insert Into tbl_Deliveryorder_Delete_Req (Req_id ,Districtid,Depotid,Deliveryorderid,Gatepassno,Depositor,commodity_id,Godown_id,DO_Date,No_of_Bags,Quantity,Operator_Name,Opeartor_Mob,Bm_Name,Bm_Mob,Req_date,ip) values ('" + Reqid.ToString() + "','" + Dist + "','" + Branch + "','" + Deliveryid + "','" + Gatepassno + "','" + Depositor + "','" + ddlcommodity.SelectedValue.ToString() + "','" + ddl_godown.SelectedValue.ToString() + "','" + DOdate + "','" + Bags + "','" + Weigt + "','" + txtopname.Text.Trim().ToString() + "','" + txtopmobile.Text.Trim().ToString() + "','" + txtbmname.Text.Trim().ToString() + "','" + txtbmmob.Text.Trim().ToString() + "',getdate(),'" + ip + "')";

                            string qry = "Insert Into tbl_Deliveryorder_Delete_Req (Req_id ,ComReq_Id,Districtid,Depotid,Deliveryorderid,Gatepassno,Depositor,commodity_id,Godown_id,DO_Date,No_of_Bags,Quantity,Operator_Name,Opeartor_Mob,Bm_Name,Bm_Mob,Req_date,ip) values ('" + Reqid.ToString() + "','" + ComReqid.ToString() + "','" + Dist + "','" + Branch + "','" + Deliveryid + "','','" + Depositor + "','','" + ddl_godown.SelectedValue.ToString() + "','" + DOdate + "','" + Bags + "','" + Weigt + "','" + txtopname.Text.Trim().ToString() + "','" + txtopmobile.Text.Trim().ToString() + "','" + txtbmname.Text.Trim().ToString() + "','" + txtbmmob.Text.Trim().ToString() + "',getdate(),'" + ip + "')";
                            cmd = new SqlCommand(qry, con);
                            int c = cmd.ExecuteNonQuery();
                            if (c > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Request saved Successfully...')", true);
                                btnPrint.Visible = true;
                                Session["ComReq_Id"] = ComReqid;
                            }
                        }
                    }
                }
            }
        }
        else if (rbtdellist.SelectedValue == "06")
        {
            string ComQueryMax = "select isnull(Max(convert(int,ComReq_id)),0) from tbl_Deliveryorder_Delete_Req";
            cmd = new SqlCommand(ComQueryMax, con); // 
            string str4 = cmd.ExecuteScalar().ToString();
            if (Convert.ToInt64(str4) != 0)
            {
                ComReqid = Convert.ToString(Convert.ToInt64(str4) + 1);
            }
            else
            {
                ComReqid = "1000";
            }
            if (stack_GridView.Rows.Count > 0)
            {
                foreach (GridViewRow gr2 in stack_GridView.Rows)
                {
                    CheckBox chk_Delete = new CheckBox();
                    chk_Delete = (CheckBox)gr2.Cells[0].FindControl("chk_Delete");
                    if (chk_Delete.Checked == false || chk_Delete.Enabled == false)
                    {
                    }
                    else
                    {
                        string Stackid = Convert.ToString(stack_GridView.DataKeys[gr2.RowIndex].Value);
                        string QueryMax = "select isnull(Max(CONVERT (int,Req_id)),0) from tbl_Stack_Delete_Req";
                        cmd = new SqlCommand(QueryMax, con); // 
                        string str3 = cmd.ExecuteScalar().ToString();
                        if (Convert.ToInt64(str3) != 0)
                        {
                            Reqid = Convert.ToString(Convert.ToInt64(str3) + 1);
                        }
                        else
                        {
                            Reqid = "1000";
                        }
                        if (Reqid != string.Empty)
                        {
                            string qry = "Insert Into tbl_Stack_Delete_Req (Req_id ,Districtid,Depotid,Stackid,commodity_id,Godown_id,Operator_Name,Opeartor_Mob,Bm_Name,Bm_Mob,Req_date,ip) values ('" + Reqid.ToString() + "','" + Dist + "','" + Branch + "','" + Stackid + "','" + ddlcommodity.SelectedValue.ToString() + "','" + ddl_godown.SelectedValue.ToString() + "','" + txtopname.Text.Trim().ToString() + "','" + txtopmobile.Text.Trim().ToString() + "','" + txtbmname.Text.Trim().ToString() + "','" + txtbmmob.Text.Trim().ToString() + "',getdate(),'" + ip + "')";
                            cmd = new SqlCommand(qry, con);
                            int c = cmd.ExecuteNonQuery();
                            if (c > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Request saved Successfully...')", true);
                                btnPrint.Visible = true;
                                Session["ComReq_Id"] = ComReqid;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please select Delete Request for...')", true);
        }
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }

    public void FillGridreceiptdetails()
    {
        if (Session["G_DepotID"].ToString() != "")
        {
            Branch = Session["G_BranchId"].ToString();
            Distid = Session["Depot_DistID"].ToString();
            string query = "select DISTINCT AST.ArrivalStock_Id,AST.Receipt_ID,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,AST.Challan_No,AST.Truck_No,SRD.Acpt_FCIRO_No,convert(nvarchar(10),SRD.Acpt_FCIRO_Date,103) AS AcceptDate,(SRD.Qty_Rvd_No_of_Bags) AS Bags,CONVERT(DECIMAL(18,2),SRD.Qty_Rvd_Weight) AS Weight,convert(nvarchar(10),AST.DepositDate,103) AS DepositDate from tbl_Storage_Arrival_Stock as AST join tbl_Storage_Receipt_Details AS SRD on AST.Receipt_ID = SRD.StorageReceipt_Id JOIN tbl_MetaData_STORAGE_COMMODITY ON AST.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where SRD.WHR_Flag='N' and SRD.WHR_Id is null AND AST.BranchID = '" + Branch + "' AND AST.District_Id = '" + Distid + "' and AST.Commodity_Id = '" + ddlcommodity.SelectedValue.ToString() + "' ORDER BY AST.ArrivalStock_Id";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Session["ds_GridInfo"] = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {
                stack_GridView.DataSource = null;
                stack_GridView.DataBind();
                gv_whr.DataSource = null;
                gv_whr.DataBind();
                gv_Opening.DataSource = null;
                gv_Opening.DataBind();
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
                gvDeliveryOrder.DataSource = null;
                gvDeliveryOrder.DataBind();
                gv_Receiptdetails.DataSource = ds;
                gv_Receiptdetails.DataBind();
                lblRowCount.Text = "";
                lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
                //Btn_Delete.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
                lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
                gv_Receiptdetails.DataSource = null;
                gv_Receiptdetails.DataBind();
            }
        }
    }

    public void FillGridOpening()
    {
        if (Session["G_DepotID"].ToString() != "")
        {
            Branch = Session["G_BranchId"].ToString();
            Distid = Session["Depot_DistID"].ToString();
            string query = "select DISTINCT WHR.Depositor_WHR_Id,CONVERT(varchar(10),WHR.WHR_Issue_Date,103) AS whrdate,WHR.Depositor_Name,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,WHR.TotalBags_Received AS Bags,convert(decimal(18,2),WHR.Total_Qty_Received) AS Qty,tbl_MetaData_GODOWN.Godown_Name,tbl_MetaData_GODOWN.Godown_ID,tbl_Storage_Receipt_Details.StorageReceipt_Id from tbl_storage_Depositor_WHR_Relation AS WHR join tbl_Storage_Receipt_Details on WHR.Depositor_WHR_Id = tbl_Storage_Receipt_Details.WHR_Id join tbl_Storage_Arrival_Stock on WHR.Depositor_WHR_Id = tbl_Storage_Arrival_Stock.ArrivalStock_Id join tbl_storage_Stacking_Details on WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId JOIN tbl_MetaData_STORAGE_COMMODITY on WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_MetaData_GODOWN on tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID where WHR.BranchID = '" + Branch + "' and WHR.District_Id = '" + Distid + "' and tbl_MetaData_GODOWN.Godown_ID = '" + ddl_godown.SelectedValue.ToString() + "' AND tbl_Storage_Arrival_Stock.Sender_District = 0 and WHR.Commodity_Id = '" + ddlcommodity.SelectedValue.ToString() + "' order by WHR.Depositor_WHR_Id asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Session["ds_GridInfo"] = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {
                stack_GridView.DataSource = null;
                stack_GridView.DataBind();
                gv_Receiptdetails.DataSource = null;
                gv_Receiptdetails.DataBind();
                gv_whr.DataSource = null;
                gv_whr.DataBind();
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
                gvDeliveryOrder.DataSource = null;
                gvDeliveryOrder.DataBind();
                gv_Opening.DataSource = ds;
                gv_Opening.DataBind();
                lblRowCount.Text = "";
                lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
                //Btn_Delete.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
                lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
                gv_Opening.DataSource = null;
                gv_Opening.DataBind();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    public void FillGridwhr()
    {
        if (Session["G_DepotID"].ToString() != "")
        {
            Branch = Session["G_BranchId"].ToString();
            Distid = Session["Depot_DistID"].ToString();
            string query = "select DISTINCT WHR.Depositor_WHR_Id,CONVERT(varchar(10),WHR.WHR_Issue_Date,103) AS whrdate,WHR.Depositor_Name,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,WHR.TotalBags_Received AS Bags,convert(decimal(18,2),WHR.Total_Qty_Received) AS Qty,tbl_MetaData_GODOWN.Godown_Name from tbl_storage_Depositor_WHR_Relation AS WHR join tbl_Storage_Receipt_Details on WHR.Depositor_WHR_Id = tbl_Storage_Receipt_Details.WHR_Id join tbl_storage_Stacking_Details on WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId JOIN tbl_MetaData_STORAGE_COMMODITY on WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id JOIN tbl_MetaData_GODOWN on tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID where WHR.BranchID = '" + Branch + "' and WHR.District_Id = '" + Distid + "' and tbl_MetaData_GODOWN.Godown_ID = '" + ddl_godown.SelectedValue.ToString() + "' AND tbl_Storage_Receipt_Details.WHR_Flag = 'Y' AND WHR.Commodity_Id ='" + ddlcommodity.SelectedValue.ToString() + "' order by WHR.Depositor_WHR_Id asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Session["ds_GridInfo"] = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {
                stack_GridView.DataSource = null;
                stack_GridView.DataBind();
                gv_Receiptdetails.DataSource = null;
                gv_Receiptdetails.DataBind();
                gv_Opening.DataSource = null;
                gv_Opening.DataBind();
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
                gvDeliveryOrder.DataSource = null;
                gvDeliveryOrder.DataBind();
                gv_whr.DataSource = ds;
                gv_whr.DataBind();
                lblRowCount.Text = "";
                lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No WHR Found...')", true);
                lblRowCount.Text = "Total No. Of records are : " + ds.Tables[0].Rows.Count.ToString();
                gv_whr.DataSource = null;
                gv_whr.DataBind();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    protected void fillGatepassGrid()
    {
        if (Session["G_DepotID"].ToString() != "")
        {
            Branch = Session["G_BranchId"].ToString();
            Distid = Session["Depot_DistID"].ToString();
            //string query = "select DISTINCT SGE.GatePass_No,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,SGE.NO_of_Bage,CONVERT (DECIMAL(18,2),SGE.Weight) AS Weight,convert(NVARCHAR(10),SGE.issue_Date,103) as Issue_Date from tbl_Storage_GatePass_Enrty AS SGE  join tbl_Storage_Final_Stock_Delivery_GatePass AS FSGE ON SGE.GatePass_No = FSGE.GatePass_No JOIN tbl_MetaData_STORAGE_COMMODITY on SGE.Commodity_ID = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where issue_Source='RO' and SGE.Status !='Cancel' and SGE.GatePass_No not in (select GatePass_No from tbl_Storage_Final_Stock_Delivery_Order) AND SGE.District_ID = '" + Distid + "' AND SGE.Depot_ID = '" + Branch + "' AND SGE.Commodity_ID ='" + ddlcommodity.SelectedValue.ToString() + "' ORDER BY SGE.GatePass_No";
            string query = "select DISTINCT SGE.GatePass_No,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,SGE.NO_of_Bage,CONVERT (DECIMAL(18,2),SGE.Weight) AS Weight,convert(NVARCHAR(10),SGE.issue_Date,103) as Issue_Date from tbl_Storage_GatePass_Enrty AS SGE  join tbl_Storage_Final_Stock_Delivery_GatePass AS FSGE ON SGE.GatePass_No = FSGE.GatePass_No JOIN tbl_MetaData_STORAGE_COMMODITY on SGE.Commodity_ID = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where issue_Source='RO' and SGE.Status !='Cancel' AND SGE.District_ID = '" + Distid + "' AND SGE.BranchID = '" + Branch + "' AND SGE.Commodity_ID ='" + ddlcommodity.SelectedValue.ToString() + "' ORDER BY SGE.GatePass_No";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_whr.DataSource = null;
                gv_whr.DataBind();
                gv_Receiptdetails.DataSource = null;
                gv_Receiptdetails.DataBind();
                gv_Opening.DataSource = null;
                gv_Opening.DataBind();
                gvDeliveryOrder.DataSource = null;
                gvDeliveryOrder.DataBind();
                stack_GridView.DataSource = null;
                stack_GridView.DataBind();
                gv_gatepass.DataSource = ds.Tables[0];
                gv_gatepass.DataBind();
                lblRowCount.Text = "Total No. of Records -" + gv_gatepass.Rows.Count.ToString();
            }
            else
            {
                lblRowCount.Text = "Total No. of Records -" + gv_gatepass.Rows.Count.ToString();
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
            }
        }
    }

    private void filldeliveryorder()
    {
        if (Session["G_DepotID"].ToString() != "")
        {
            Branch = Session["G_BranchId"].ToString();
            Distid = Session["Depot_DistID"].ToString();
            // string query = "select SFSD.StockDeliveryOrder_Id,SGE.[Depositor/Issuer_Name],com.Commodity_Name,convert(varchar(15),SFSD.CreatedDate,103)DODate,SFSD.GatePass_No as GatePassNO,SFSD.Qty_Issued_No_Bags_Sound as Qty,SFSD.Qty_Issued_Weight as  Wet from tbl_Storage_Final_Stock_Delivery_Order as SFSD inner join tbl_Storage_GatePass_Enrty AS SGE on SFSD.GatePass_No=SGE.GatePass_No join tbl_MetaData_STORAGE_COMMODITY as com on SGE.Commodity_ID = com.Commodity_Id and SFSD.District_Id='" + Distid + "' and SFSD.DepotId='" + Branch + "' AND SGE.Commodity_Id ='" + ddlcommodity.SelectedValue.ToString() + "' order by DODate Asc";
            //string query = "select DISTINCT SGE.GatePass_No,tbl_MetaData_STORAGE_COMMODITY.Commodity_Name,SGE.NO_of_Bage,CONVERT (DECIMAL(18,2),SGE.Weight) AS Weight,convert(NVARCHAR(10),SGE.issue_Date,103) as Issue_Date from tbl_Storage_GatePass_Enrty AS SGE  join tbl_Storage_Final_Stock_Delivery_GatePass AS FSGE ON SGE.GatePass_No = FSGE.GatePass_No JOIN tbl_MetaData_STORAGE_COMMODITY on SGE.Commodity_ID = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id where issue_Source='DO' and SGE.Status !='Cancel' AND SGE.District_ID = '" + Distid + "' AND SGE.Depot_ID = '" + Branch + "' AND SGE.Commodity_ID ='" + ddlcommodity.SelectedValue.ToString() + "' ORDER BY SGE.GatePass_No";
            string query = "select SFSD.StockDeliveryOrder_Id,SGE.[Depositor/Issuer_Name],convert(varchar(10),SFSD.Delivery_Order_Date,103) as DO_Date,sum(SFSD.Qty_Issued_No_Bags_Sound) as QtyBags,sum(SFSD.Qty_Issued_Weight) as QtyWeight from tbl_Storage_Final_Stock_Delivery_Order as SFSD join tbl_Storage_GatePass_Enrty as SGE on SFSD.Delivery_Order_No=SGE.Issue_Source_ID join tbl_MetaData_STORAGE_COMMODITY as SC on SC.Commodity_Id=SGE.Commodity_ID where SGE.Issue_Source='DO' and SFSD.District_Id='" + Distid + "' and SFSD.BranchID='" + Branch + "' and SGE.Godown_ID='" + ddl_godown.SelectedValue.ToString() + "' group by SFSD.StockDeliveryOrder_Id,SGE.[Depositor/Issuer_Name],convert(varchar(10),SFSD.Delivery_Order_Date,103)";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_whr.DataSource = null;
                gv_whr.DataBind();
                gv_Receiptdetails.DataSource = null;
                gv_Receiptdetails.DataBind();
                gv_Opening.DataSource = null;
                gv_Opening.DataBind();
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
                stack_GridView.DataSource = null;
                stack_GridView.DataBind();
                gvDeliveryOrder.DataSource = ds.Tables[0];
                gvDeliveryOrder.DataBind();
                lblRowCount.Text = "Total Record -" + gvDeliveryOrder.Rows.Count.ToString();
            }
            else
            {
                gvDeliveryOrder.DataSource = null;
                gvDeliveryOrder.DataBind();
                lblRowCount.Text = "Total Record -" + gvDeliveryOrder.Rows.Count.ToString();
            }
        }
    }

    private void GetStack()
    {
        if (Session["G_DepotID"].ToString() != "")
        {
            Branch = Session["G_BranchId"].ToString();
            Distid = Session["Depot_DistID"].ToString();
            string qry = "SELECT tbl_MetaData_STACK.Stack_ID,tbl_MetaData_STACK.Godown_ID,tbl_MetaData_STACK.Category_Id,tbl_MetaData_STACK.Commodity_Id, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STACK.Stack_Name, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name, convert(decimal(18,3),tbl_MetaData_STACK.Stack_capacity) as Stack_capacity, tbl_MetaData_STACK.Storage_Type,tbl_MetaData_STACK.Hired_type, (SELECT CASE WHEN COUNT(*) > 0 THEN 'Stacked' ELSE 'Empty' END AS Expr1 FROM tbl_storage_Stacking_Details WHERE (Stack_ID = tbl_MetaData_STACK.Stack_ID)) AS 'StackingStatus', convert(decimal(18,3),(SELECT  Isnull((SUM(Weight)-isnull(SUM(Bags_Weight),0)),0) FROM tbl_storage_Stacking_Details left join tbl_Delivery_Stacking_Details on tbl_storage_Stacking_Details.Stack_ID= tbl_Delivery_Stacking_Details.Stack_ID WHERE tbl_storage_Stacking_Details.Stack_ID=tbl_MetaData_STACK.Stack_ID)) AS 'Current_Capacity' FROM tbl_MetaData_STACK INNER JOIN tbl_MetaData_GODOWN ON tbl_MetaData_STACK.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON tbl_MetaData_STACK.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON tbl_MetaData_STACK.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id WHERE (tbl_MetaData_STACK.BranchId = '" + Branch + "') AND (tbl_MetaData_STACK.Stack_Killed = 'N') and tbl_MetaData_STACK.Godown_ID='" + ddl_godown.SelectedValue.ToString() + "' AND tbl_MetaData_STACK.Commodity_Id = '" + ddlcommodity.SelectedValue.ToString() + "' order by tbl_MetaData_GODOWN.Godown_Name ,tbl_MetaData_STACK.Stack_Name ";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_whr.DataSource = null;
                gv_whr.DataBind();
                gv_Receiptdetails.DataSource = null;
                gv_Receiptdetails.DataBind();
                gv_Opening.DataSource = null;
                gv_Opening.DataBind();
                gv_gatepass.DataSource = null;
                gv_gatepass.DataBind();
                gvDeliveryOrder.DataSource = null;
                gvDeliveryOrder.DataBind();
                stack_GridView.DataSource = ds.Tables[0];
                stack_GridView.DataBind();
                lblRowCount.Text = "Total records are : " + stack_GridView.Rows.Count.ToString();
            }
            else
            {
                stack_GridView.DataSource = null;
                stack_GridView.DataBind();
            }
        }
    }

    protected void rbtdellist_SelectedIndexChanged(object sender, EventArgs e)
    {
        trgdnlist.Visible = true;
        EmptyGrid();
        ddl_godown.SelectedIndex = 0;
    }

    private void EmptyGrid()
    {
        gv_whr.DataSource = null;
        gv_whr.DataBind();
        gv_Receiptdetails.DataSource = null;
        gv_Receiptdetails.DataBind();
        gv_Opening.DataSource = null;
        gv_Opening.DataBind();
        gv_gatepass.DataSource = null;
        gv_gatepass.DataBind();
        gvDeliveryOrder.DataSource = null;
        gvDeliveryOrder.DataBind();
        stack_GridView.DataSource = null;
        stack_GridView.DataBind();
    }

    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

        Session["Requestfor"] = rbtdellist.SelectedValue.ToString();
        Response.Redirect("~/BranchPages/PrintDeleteRequest.aspx");

    }
}

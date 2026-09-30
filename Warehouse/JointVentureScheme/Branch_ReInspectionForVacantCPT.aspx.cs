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
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Globalization;

public partial class JointVentureScheme_Branch_ReInspectionForVacantCPT : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlTransaction sqltran;
    public string qry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        string SessRegion = Session["UserName"].ToString();
        string SessRegionid = Session["UserId"].ToString();
        if (SessRegion != "" && SessRegionid != "")
        {
            if (!IsPostBack)
            {
                lbluser.Text = SessRegion;
                //  Search();
                // BindddlDistrict(SessRegionid);
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    protected void gvGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gv = gvGodown.SelectedRow;
        string inspID = gv.Cells[1].Text;
        if (gvGodown.SelectedRow != null)
        {
            gv.Visible = true;
            txtCommodityName.Text = gv.Cells[8].Text;
            txtDepositorName.Text = gv.Cells[9].Text;
            txtCurrentStoredComm.Text = gv.Cells[10].Text;
            txtvacantcpt.Text = gv.Cells[11].Text;
            TRHide.Visible = true;

        }
        else
        {
            gv.Visible = false;
            TRHide.Visible = false;
        }
    }
    public void Search()
    {
        if (ddl_session.SelectedValue == "JVS2021")
        {
            // qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities,Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity  from tbl_godown_inspection as INSP where Fit_Unfit='PENDING' and Insp_Date < DATEADD(DAY, -15, GETDATE()) and BranchId='" + Session["UserId"].ToString() + "'";
            qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities, Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity   from tbl_godown_inspection as INSP where Fit_Unfit='FIT' and INSP.CreatedDate > '02/18/2019' and  INSP.registration_id='" + txtSearch.Text + "' and INSP.BranchID='" + Session["UserId"].ToString() + "'";
        }
        else if (ddl_session.SelectedValue == "JVS2022")
        {
            qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities, Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity   from tbl_godown_inspection as INSP where Fit_Unfit='FIT' and INSP.CreatedDate > '02/18/2021' and  INSP.registration_id='" + txtSearch.Text + "' and INSP.BranchID='" + Session["UserId"].ToString() + "'";
        }
        else if (ddl_session.SelectedValue == "JVS2023")
        {
            qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities, Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity   from tbl_godown_inspection as INSP where Fit_Unfit='FIT' and INSP.CreatedDate > '02/18/2022' and  INSP.registration_id='" + txtSearch.Text + "' and INSP.BranchID='" + Session["UserId"].ToString() + "'";
        }
        else if (ddl_session.SelectedValue == "Kharif_2022_23")
        {
            qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities, Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity   from tbl_Godown_Inspection_Kharif_2022 as INSP where Fit_Unfit='FIT' and INSP.CreatedDate > '11/15/2022' and  INSP.registration_id='" + txtSearch.Text + "' and INSP.BranchID='" + Session["UserId"].ToString() + "'";
        }
        else if (ddl_session.SelectedValue == "Rab2023_24")
        {
            qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities, Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity   from tbl_Godown_Inspection_2023 as INSP where Fit_Unfit='FIT' and INSP.CreatedDate >= '02/27/2023' and  INSP.registration_id='" + txtSearch.Text + "' and INSP.BranchID='" + Session["UserId"].ToString() + "' AND INSP.Jvs_session=' Rab2023_24'";
        }
        else if (ddl_session.SelectedValue == "Kharif2023_24")
        {
            qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities, Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity   from tbl_Godown_Inspection_2023 as INSP where Fit_Unfit='FIT' and INSP.CreatedDate >= '02/27/2023' and  INSP.registration_id='" + txtSearch.Text + "' and INSP.BranchID='" + Session["UserId"].ToString() + "' AND INSP.Jvs_session=' Kharif2023_24'";
        }
        else if (ddl_session.SelectedValue == "Rab2024_25")
        {
            qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities, Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity   from tbl_Godown_Inspection_2024 as INSP where Fit_Unfit='FIT' and INSP.CreatedDate >= '03/15/2024' and  INSP.registration_id='" + txtSearch.Text + "' and INSP.BranchID='" + Session["UserId"].ToString() + "' AND INSP.Jvs_session=' Rab2024_25'";
        }
        else if (ddl_session.SelectedValue == "Kharif2024_25")
        {
            qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities, Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity   from tbl_Godown_Inspection_2024_Kharif as INSP where Fit_Unfit='FIT' and INSP.CreatedDate >= '12/09/2024' and  INSP.registration_id='" + txtSearch.Text + "' and INSP.BranchID='" + Session["UserId"].ToString() + "' AND INSP.Jvs_session='Kharif2024_25'";
        }
        else if (ddl_session.SelectedValue == "Rab2025_26")
        {
            qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities, Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity   from tbl_Godown_Inspection_2025 as INSP where Fit_Unfit='FIT' and INSP.CreatedDate >= '03/18/2025' and  INSP.registration_id='" + txtSearch.Text + "' and INSP.BranchID='" + Session["UserId"].ToString() + "' AND INSP.Jvs_session='Rab2025_26'";
        }
        else if (ddl_session.SelectedValue == "Rab2026_27")
        {
            qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities, Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity   from tbl_Godown_Inspection_2026 as INSP where Fit_Unfit='FIT' and INSP.CreatedDate >= '04/01/2026' and  INSP.registration_id='" + txtSearch.Text + "' and INSP.BranchID='" + Session["UserId"].ToString() + "' AND INSP.Jvs_session='Rab2026_27'";
        }
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvGodown.DataSource = ds.Tables[0];
            gvGodown.DataBind();
            gvGodown.Columns[1].Visible = false;
            gvGodown.Columns[2].Visible = false;
            gvGodown.Columns[12].Visible = false;
            //gvGodown.Columns[14].Visible = false;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No Record Found Please Enter Correct Registration No.')", true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        GridViewRow gv = gvGodown.SelectedRow;
        if (txtvacantcpt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('वर्तमान में गोदाम की रिक्त क्षमता दर्ज करे :...'); </script> ");
            txtvacantcpt.Focus();
        }
        else if (txtInspDate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('पुनः निरीक्षण दिनांक  दुर्ज करें हें ...'); </script> ");
        }
        else if (Convert.ToDecimal(txtvacantcpt.Text) > Convert.ToDecimal(gv.Cells[13].Text))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('ऑफर केपेसिटि से ज्यादा इन्सपैक्शन केपेसिटि दुर्ज करना संबव नहीं हें ...'); </script> ");
        }
        else if (ddl_session.SelectedValue == "Kharif_2022_23")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string Client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string inspID = gv.Cells[1].Text;
            string GdwDI = gv.Cells[5].Text;
            string GdwOfferDI = gv.Cells[2].Text;
            //qry = "Insert into tbl_Godown_Inspection_Kharif_2022_log select * from tbl_Godown_Inspection where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            qry = "Insert into tbl_Godown_Inspection_Kharif_2022_log select * from tbl_Godown_Inspection_Kharif_2022 where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            int a = cmd1.ExecuteNonQuery();
            if (a == 1)
            {
                qry = "update tbl_Godown_Inspection_Kharif_2022 set Insp_Date='" + getDate_MDY(txtInspDate.Text.Trim()) + "',Stored_Commodities='" + txtCommodityName.Text.Trim() + "',Stored_Com_Depositor='" + txtDepositorName.Text.Trim() + "',Utilized_Capacity='" + txtCurrentStoredComm.Text.Trim() + "',Vacant_Capacity='" + txtvacantcpt.Text.Trim() + "',UpdateBy='" + Client_IP + "' , UpdatedDate=GETDATE(),RE_Inspe='Y' where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "' ";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                int a2 = cmd2.ExecuteNonQuery();
                if (a2 == 1)
                {
                    ModalPopupExtender1.Show();
                }
            }
            con.Close();
        }
        else if (ddl_session.SelectedValue == "Rab2023_24")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string Client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string inspID = gv.Cells[1].Text;
            string GdwDI = gv.Cells[5].Text;
            string GdwOfferDI = gv.Cells[2].Text;
            //qry = "Insert into tbl_Godown_Inspection_Kharif_2022_log select * from tbl_Godown_Inspection where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            qry = "Insert into tbl_Godown_Inspection_2023_log select * from tbl_Godown_Inspection_2023 where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            int a = cmd1.ExecuteNonQuery();
            if (a == 1)
            {
                qry = "update tbl_Godown_Inspection_2023 set Insp_Date='" + getDate_MDY(txtInspDate.Text.Trim()) + "',Stored_Commodities='" + txtCommodityName.Text.Trim() + "',Stored_Com_Depositor='" + txtDepositorName.Text.Trim() + "',Utilized_Capacity='" + txtCurrentStoredComm.Text.Trim() + "',Vacant_Capacity='" + txtvacantcpt.Text.Trim() + "',UpdateBy='" + Client_IP + "' , UpdatedDate=GETDATE(),RE_Inspe='Y' where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "' ";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                int a2 = cmd2.ExecuteNonQuery();
                if (a2 == 1)
                {
                    ModalPopupExtender1.Show();
                }
            }
            con.Close();
        }
        else if (ddl_session.SelectedValue == "Kharif2023_24")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string Client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string inspID = gv.Cells[1].Text;
            string GdwDI = gv.Cells[5].Text;
            string GdwOfferDI = gv.Cells[2].Text;
            //qry = "Insert into tbl_Godown_Inspection_Kharif_2022_log select * from tbl_Godown_Inspection where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            qry = "Insert into tbl_Godown_Inspection_2023_log select * from tbl_Godown_Inspection_2023 where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            int a = cmd1.ExecuteNonQuery();
            if (a == 1)
            {
                qry = "update tbl_Godown_Inspection_2023 set Insp_Date='" + getDate_MDY(txtInspDate.Text.Trim()) + "',Stored_Commodities='" + txtCommodityName.Text.Trim() + "',Stored_Com_Depositor='" + txtDepositorName.Text.Trim() + "',Utilized_Capacity='" + txtCurrentStoredComm.Text.Trim() + "',Vacant_Capacity='" + txtvacantcpt.Text.Trim() + "',UpdateBy='" + Client_IP + "' , UpdatedDate=GETDATE(),RE_Inspe='Y' where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "' ";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                int a2 = cmd2.ExecuteNonQuery();
                if (a2 == 1)
                {
                    ModalPopupExtender1.Show();
                }
            }
            con.Close();
        }
        else if (ddl_session.SelectedValue == "Rab2024_25")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string Client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string inspID = gv.Cells[1].Text;
            string GdwDI = gv.Cells[5].Text;
            string GdwOfferDI = gv.Cells[2].Text;
            //qry = "Insert into tbl_Godown_Inspection_Kharif_2022_log select * from tbl_Godown_Inspection where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            qry = "Insert into tbl_Godown_Inspection_2024_log select * from tbl_Godown_Inspection_2024 where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            int a = cmd1.ExecuteNonQuery();
            if (a == 1)
            {
                qry = "update tbl_Godown_Inspection_2024 set Insp_Date='" + getDate_MDY(txtInspDate.Text.Trim()) + "',Stored_Commodities='" + txtCommodityName.Text.Trim() + "',Stored_Com_Depositor='" + txtDepositorName.Text.Trim() + "',Utilized_Capacity='" + txtCurrentStoredComm.Text.Trim() + "',Vacant_Capacity='" + txtvacantcpt.Text.Trim() + "',UpdateBy='" + Client_IP + "' , UpdatedDate=GETDATE(),RE_Inspe='Y' where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "' ";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                int a2 = cmd2.ExecuteNonQuery();
                if (a2 == 1)
                {
                    ModalPopupExtender1.Show();
                }
            }
            con.Close();
        }
        else if (ddl_session.SelectedValue == "Kharif2024_25")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string Client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string inspID = gv.Cells[1].Text;
            string GdwDI = gv.Cells[5].Text;
            string GdwOfferDI = gv.Cells[2].Text;
            //qry = "Insert into tbl_Godown_Inspection_Kharif_2022_log select * from tbl_Godown_Inspection where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            qry = "Insert into tbl_Godown_Inspection_2024_Kharif_log select * from tbl_Godown_Inspection_2024_Kharif where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            int a = cmd1.ExecuteNonQuery();
            if (a == 1)
            {
                qry = "update tbl_Godown_Inspection_2024_Kharif set Insp_Date='" + getDate_MDY(txtInspDate.Text.Trim()) + "',Stored_Commodities='" + txtCommodityName.Text.Trim() + "',Stored_Com_Depositor='" + txtDepositorName.Text.Trim() + "',Utilized_Capacity='" + txtCurrentStoredComm.Text.Trim() + "',Vacant_Capacity='" + txtvacantcpt.Text.Trim() + "',UpdateBy='" + Client_IP + "' , UpdatedDate=GETDATE(),RE_Inspe='Y' where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "' ";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                int a2 = cmd2.ExecuteNonQuery();
                if (a2 == 1)
                {
                    ModalPopupExtender1.Show();
                }
            }
            con.Close();
        }
        else if (ddl_session.SelectedValue == "Rab2025_26")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string Client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string inspID = gv.Cells[1].Text;
            string GdwDI = gv.Cells[5].Text;
            string GdwOfferDI = gv.Cells[2].Text;
            //qry = "Insert into tbl_Godown_Inspection_Kharif_2022_log select * from tbl_Godown_Inspection where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            qry = "Insert into tbl_Godown_Inspection_2025_log select * from tbl_Godown_Inspection_2025 where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            int a = cmd1.ExecuteNonQuery();
            if (a == 1)
            {
                qry = "update tbl_Godown_Inspection_2025 set Insp_Date='" + getDate_MDY(txtInspDate.Text.Trim()) + "',Stored_Commodities='" + txtCommodityName.Text.Trim() + "',Stored_Com_Depositor='" + txtDepositorName.Text.Trim() + "',Utilized_Capacity='" + txtCurrentStoredComm.Text.Trim() + "',Vacant_Capacity='" + txtvacantcpt.Text.Trim() + "',UpdateBy='" + Client_IP + "' , UpdatedDate=GETDATE(),RE_Inspe='Y' where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "' ";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                int a2 = cmd2.ExecuteNonQuery();
                if (a2 == 1)
                {
                    ModalPopupExtender1.Show();
                }
            }
            con.Close();
        }
        else if (ddl_session.SelectedValue == "Rab2026_27")
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string Client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string inspID = gv.Cells[1].Text;
            string GdwDI = gv.Cells[5].Text;
            string GdwOfferDI = gv.Cells[2].Text;
            //qry = "Insert into tbl_Godown_Inspection_Kharif_2022_log select * from tbl_Godown_Inspection where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            qry = "Insert into tbl_Godown_Inspection_2026_log select * from tbl_Godown_Inspection_2026 where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            int a = cmd1.ExecuteNonQuery();
            if (a == 1)
            {
                qry = "update tbl_Godown_Inspection_2026 set Insp_Date='" + getDate_MDY(txtInspDate.Text.Trim()) + "',Stored_Commodities='" + txtCommodityName.Text.Trim() + "',Stored_Com_Depositor='" + txtDepositorName.Text.Trim() + "',Utilized_Capacity='" + txtCurrentStoredComm.Text.Trim() + "',Vacant_Capacity='" + txtvacantcpt.Text.Trim() + "',UpdateBy='" + Client_IP + "' , UpdatedDate=GETDATE(),RE_Inspe='Y' where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "' ";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                int a2 = cmd2.ExecuteNonQuery();
                if (a2 == 1)
                {
                    ModalPopupExtender1.Show();
                }
            }
            con.Close();
        }
        else
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string Client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            string inspID = gv.Cells[1].Text;
            string GdwDI = gv.Cells[5].Text;
            string GdwOfferDI = gv.Cells[2].Text;
            qry = "Insert into tbl_Godown_Inspection_log select * from tbl_Godown_Inspection where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "'";
            SqlCommand cmd1 = new SqlCommand(qry, con);
            int a = cmd1.ExecuteNonQuery();
            if (a == 1)
            {
                qry = "update tbl_Godown_Inspection set Insp_Date='" + getDate_MDY(txtInspDate.Text.Trim()) + "',Stored_Commodities='" + txtCommodityName.Text.Trim() + "',Stored_Com_Depositor='" + txtDepositorName.Text.Trim() + "',Utilized_Capacity='" + txtCurrentStoredComm.Text.Trim() + "',Vacant_Capacity='" + txtvacantcpt.Text.Trim() + "',UpdateBy='" + Client_IP + "' , UpdatedDate=GETDATE(),RE_Inspe='Y' where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "' ";
                SqlCommand cmd2 = new SqlCommand(qry, con);
                int a2 = cmd2.ExecuteNonQuery();
                if (a2 == 1)
                {
                    ModalPopupExtender1.Show();
                }
            }
            con.Close();
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Branch_ReInspectionForVacantCPT.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text != "" && txtSearch.Text.Length > 4)
        {
            Search();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Registration ID .....')", true);
        }
    }
}

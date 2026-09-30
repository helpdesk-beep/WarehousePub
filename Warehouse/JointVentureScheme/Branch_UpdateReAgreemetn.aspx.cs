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

public partial class JointVentureScheme_Branch_UpdateReAgreemetn : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
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
            }
        }
        else
        {
            Response.Redirect("Logins.aspx");
        }
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
    public void Search()
    {
        if (ddl_session.SelectedValue.ToString() == "Kharif1819")
        {
            qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer_2019 I where I.Godown_ID=INS.GodownId and INS.Godown_Offer_Id=I.Godown_Offer_Id ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity,Agreement_Id from tbl_Godown_Inspection INS inner join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId and INS.Godown_Offer_Id=GA.Godown_Offer_Id where INS.CreatedDate>'03/01/2018' and INS.Registration_Id= '" + txtSearch.Text + "' and INS.BranchID='" + Session["UserId"].ToString() + "'";
        }
        else if (ddl_session.SelectedValue.ToString() == "JVS2022_23")
        {
            qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer_2022 I where I.Godown_ID=INS.GodownId and INS.Godown_Offer_Id=I.Godown_Offer_Id ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity,Agreement_Id from tbl_Godown_Inspection INS inner join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId and INS.Godown_Offer_Id=GA.Godown_Offer_Id where INS.CreatedDate>'03/01/2022' and INS.Registration_Id= '" + txtSearch.Text + "' and INS.BranchID='" + Session["UserId"].ToString() + "'";
        }
        else if (ddl_session.SelectedValue.ToString() == "JVS2021_22")
        {
            qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer_2021 I where I.Godown_ID=INS.GodownId and INS.Godown_Offer_Id=I.Godown_Offer_Id ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity,Agreement_Id from tbl_Godown_Inspection INS inner join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId and INS.Godown_Offer_Id=GA.Godown_Offer_Id where INS.CreatedDate>'03/01/2021' and INS.Registration_Id= '" + txtSearch.Text + "' and INS.BranchID='" + Session["UserId"].ToString() + "'";
        }
        else if (ddl_session.SelectedValue.ToString() == "Kharif_2022_23")
        {
            qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer_Kharif2022 I where I.Godown_ID=INS.GodownId and INS.Godown_Offer_Id=I.Godown_Offer_Id ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity,Agreement_Id from tbl_Godown_Inspection_Kharif_2022 INS inner join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId and INS.Godown_Offer_Id=GA.Godown_Offer_Id where INS.CreatedDate>'11/15/2022' and INS.Registration_Id= '" + txtSearch.Text + "' and INS.BranchID='" + Session["UserId"].ToString() + "'";
        }
        else if (ddl_session.SelectedValue.ToString() == "Rab2023_24")
        {
            qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer_Rabi2023 I where I.Godown_ID=INS.GodownId and INS.Godown_Offer_Id=I.Godown_Offer_Id ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity,Agreement_Id from tbl_Godown_Inspection_2023 INS inner join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId and INS.Godown_Offer_Id=GA.Godown_Offer_Id and GA.Jvs_session='Rab2023_24' where INS.CreatedDate>='02/27/2023' and INS.Registration_Id= '" + txtSearch.Text + "' and INS.BranchID='" + Session["UserId"].ToString() + "' AND INS.Jvs_session=' Rab2023_24'";
        }
        else if (ddl_session.SelectedValue.ToString() == "Kharif2023_24")
        {
            qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer_Rabi2023 I where I.Godown_ID=INS.GodownId and INS.Godown_Offer_Id=I.Godown_Offer_Id ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity,Agreement_Id from tbl_Godown_Inspection_2023 INS inner join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId and INS.Godown_Offer_Id=GA.Godown_Offer_Id and GA.Jvs_session='Kharif2023_24' where INS.CreatedDate>='02/27/2023' and INS.Registration_Id= '" + txtSearch.Text + "' and INS.BranchID='" + Session["UserId"].ToString() + "' AND INS.Jvs_session=' Kharif2023_24'";
        }
        else if (ddl_session.SelectedValue.ToString() == "Rab2024_25")
        {
            qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer_Rabi_2024_25 I where I.Godown_ID=INS.GodownId and INS.Godown_Offer_Id=I.Godown_Offer_Id ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity,Agreement_Id from tbl_Godown_Inspection_2024 INS inner join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId and INS.Godown_Offer_Id=GA.Godown_Offer_Id and GA.Jvs_session='Rab2024_25' where INS.CreatedDate>='03/15/2024' and INS.Registration_Id= '" + txtSearch.Text + "' and INS.BranchID='" + Session["UserId"].ToString() + "' AND INS.Jvs_session='Rab2024_25'";
        }
        else if (ddl_session.SelectedValue == "Kharif2024_25")
        {
            qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer_Rabi_2024_25 I where I.Godown_ID=INS.GodownId and INS.Godown_Offer_Id=I.Godown_Offer_Id ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity,Agreement_Id from tbl_Godown_Inspection_2024_Kharif INS inner join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId and INS.Godown_Offer_Id=GA.Godown_Offer_Id and GA.Jvs_session='Kharif2024_25' where INS.CreatedDate>='12/09/2024' and INS.Registration_Id= '" + txtSearch.Text + "' and INS.BranchID='" + Session["UserId"].ToString() + "' AND INS.Jvs_session='Kharif2024_25'";
            //qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehousename, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities, Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity   from tbl_Godown_Inspection_2024_Kharif as INSP where Fit_Unfit='FIT' and INSP.CreatedDate >= '12/09/2024' and  INSP.registration_id='" + txtSearch.Text + "' and INSP.BranchID='" + Session["UserId"].ToString() + "' AND INSP.Jvs_session='Kharif2024_25'";
        }
        else if (ddl_session.SelectedValue.ToString() == "Rab2025_26")
        {
            qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer_Rabi_2025_26 I where I.Godown_ID=INS.GodownId and INS.Godown_Offer_Id=I.Godown_Offer_Id ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity,Agreement_Id from tbl_Godown_Inspection_2025 INS inner join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId and INS.Godown_Offer_Id=GA.Godown_Offer_Id and GA.Jvs_session='Rab2025_26' where INS.CreatedDate>='03/05/2025' and INS.Registration_Id= '" + txtSearch.Text + "' and INS.BranchID='" + Session["UserId"].ToString() + "' AND INS.Jvs_session='Rab2025_26'";
        }
        else if (ddl_session.SelectedValue.ToString() == "Rab2026_27")
        {
            qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer_Rabi_2026_27 I where I.Godown_ID=INS.GodownId and INS.Godown_Offer_Id=I.Godown_Offer_Id ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity,Agreement_Id from tbl_Godown_Inspection_2026 INS inner join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId and INS.Godown_Offer_Id=GA.Godown_Offer_Id and GA.Jvs_session='Rab2026_27' where INS.CreatedDate>='04/01/2026' and INS.Registration_Id= '" + txtSearch.Text + "' and INS.BranchID='" + Session["UserId"].ToString() + "' AND INS.Jvs_session='Rab2026_27'";
        }
        else
        {
            qry = "select INS.Inspection_Id,INS.Registration_Id,INS.GodownId,(select Warehouse_Name from tbl_WarehouseRegistration WR where INS.Registration_Id=WR.Registration_Id) as WarehouseName,INS.Godown_No,(select convert(decimal(18,2),G_OfferCapacity) from tbl_Warehouse_Godown_Offer_Rabi_2026_27 I where I.Godown_ID=INS.GodownId and INS.Godown_Offer_Id=I.Godown_Offer_Id ) G_OfferCapacity,Vacant_Capacity,Fit_Unfit,Remark,Agree_Capacity,Agreement_Id from tbl_Godown_Inspection_2026 INS inner join tbl_Godown_Agreement GA on GA.Registration_Id=INS.Registration_Id AND GA.GodownId=INS.GodownId and INS.Godown_Offer_Id=GA.Godown_Offer_Id where INS.CreatedDate>'04/01/2026' and INS.Registration_Id= '" + txtSearch.Text + "' and INS.BranchID='" + Session["UserId"].ToString() + "' AND INS.Jvs_session='Rab2026_27'";
        }
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvGodown.DataSource = ds.Tables[0];
            gvGodown.DataBind();
            gvGodown.Columns[11].Visible = false;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Inspection Not Find Please Check Registration No.')", true);
        }
    }

    protected void gvGodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        TRHide.Visible = true;
        GridViewRow gvr = gvGodown.SelectedRow;
        // txtVacantCptUpdate.Text = gvr.Cells[7].Text;

        if (gvr.Cells[10].Text != "")
        {
            txtAgreeCpt.Text = gvr.Cells[10].Text;
        }
        else
        {
            txtAgreeCpt.Text = "";
        }
        //ddlFitUnfit.SelectedItem.Text = gvr.Cells[8].Text;
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
            if (txtSearch.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Registration ID .....')", true);
            }
            else if (Convert.ToDecimal(txtAgreeCpt.Text) > Convert.ToDecimal(gvr.Cells[7].Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You Cannot Update Agreemente capacity More then vacant Capacity  .....')", true);
            }
            else
            {
                int chkAgr = checkAgreement();
                if (chkAgr == 0)
                {

                }
                else if (chkAgr == 1)
                {
                    if (txtAgreeCpt.Text != null && txtAgreeCpt.Text != "")
                    {
                        string qryInsert2 = "insert into tbl_Godown_Agreement_Log select * from tbl_Godown_Agreement where Registration_Id = '" + txtSearch.Text + "' and Inspection_Id='" + gvr.Cells[1].Text + "' and GodownId='" + gvr.Cells[3].Text + "' and Jvs_session='" + ddl_session.SelectedValue + "'";
                        SqlCommand cmd2 = new SqlCommand(qryInsert2, con);
                        int k = cmd2.ExecuteNonQuery();
                        if (k == 1)
                        {
                            string qryUpdate2 = "Update tbl_Godown_Agreement set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and Inspection_Id='" + gvr.Cells[1].Text + "' and GodownId='" + gvr.Cells[3].Text + "' and Jvs_session='" + ddl_session.SelectedValue + "'";
                            SqlCommand cmd3 = new SqlCommand(qryUpdate2, con);
                            int l = cmd3.ExecuteNonQuery();
                            if (l == 1)
                            {
                                ModalPopupExtender1.Show();
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Updated Successfully')", true);
                            }
                        }

                        string qryInsert4 = "";
                        if (ddl_session.SelectedValue.ToString() == "Kharif1819")
                        {
                            qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_2020_log select * from tbl_Agreemented_Godown_Mapping_2020 where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "'";
                        }
                        else if (ddl_session.SelectedValue.ToString() == "JVS2022_23")
                        {
                            qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_2020_log select * from tbl_Agreemented_Godown_Mapping_2020 where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='2022-23'";
                        }
                        else if (ddl_session.SelectedValue.ToString() == "JVS2021_22")
                        {
                            qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_2020_log select * from tbl_Agreemented_Godown_Mapping_2020 where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='2021-22'";
                        }
                        else if (ddl_session.SelectedValue.ToString() == "Kharif_2022_23")
                        {
                            qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_2020_log select * from tbl_Agreemented_Godown_Mapping_2020 where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Kharif_2022_23'";
                        }
                        else if (ddl_session.SelectedValue.ToString() == "Rab2023_24")
                        {
                            qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_2020_log select * from tbl_Agreemented_Godown_Mapping_2020 where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Rab2023_24'";
                        }
                        else if (ddl_session.SelectedValue.ToString() == "Kharif2023_24")
                        {
                            qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_2020_log select * from tbl_Agreemented_Godown_Mapping_2020 where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Kharif2023_24'";
                        }
                        else if (ddl_session.SelectedValue.ToString() == "Rab2024_25")
                        {
                            qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_2020_log select * from tbl_Agreemented_Godown_Mapping_2020 where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Rab2024_25'";
                        }
                        else if (ddl_session.SelectedValue.ToString() == "Kharif2024_25")
                        {
                            qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_2020_log select * from tbl_Agreemented_Godown_Mapping_2020 where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Kharif2024_25'";
                        }
                        else if (ddl_session.SelectedValue.ToString() == "Rab2025_26")
                        {
                            qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_2020_log select * from tbl_Agreemented_Godown_Mapping_2020 where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Rab2025_26'";
                        }
                        else if (ddl_session.SelectedValue.ToString() == "Rab2026_27")
                        {
                            qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_2020_log select * from tbl_Agreemented_Godown_Mapping_2020 where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Rab2026_27'";
                        }
                        else
                        {
                            qryInsert4 = "insert into tbl_Agreemented_Godown_Mapping_2019_log select * from tbl_Agreemented_Godown_Mapping_2019 where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "'";
                        }

                        SqlCommand cmd6 = new SqlCommand(qryInsert4, sqlcon);
                        sqlcon.Open();
                        int o = cmd6.ExecuteNonQuery();
                        if (o == 1)
                        {
                            string qryUpdate4 = "";
                            if (ddl_session.SelectedValue.ToString() == "Kharif1819")
                            {
                                qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping_2019 set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "'";
                            }
                            else if (ddl_session.SelectedValue.ToString() == "JVS2022_23")
                            {
                                qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping_2020 set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='2022-23'";
                            }
                            else if (ddl_session.SelectedValue.ToString() == "JVS2021_22")
                            {
                                qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping_2020 set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='2021-22'";
                            }

                            else if (ddl_session.SelectedValue.ToString() == "Kharif_2022_23")
                            {
                                qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping_2020 set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Kharif_2022_23'";
                            }
                            else if (ddl_session.SelectedValue.ToString() == "Rab2023_24")
                            {
                                qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping_2020 set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Rab2023_24'";
                            }
                            else if (ddl_session.SelectedValue.ToString() == "Kharif2023_24")
                            {
                                qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping_2020 set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Kharif2023_24'";
                            }
                            else if (ddl_session.SelectedValue.ToString() == "Rab2024_25")
                            {
                                qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping_2020 set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Rab2024_25'";
                            }
                            else if (ddl_session.SelectedValue.ToString() == "Kharif2024_25")
                            {
                                qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping_2020 set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Kharif2024_25'";
                            }
                            else if (ddl_session.SelectedValue.ToString() == "Rab2025_26")
                            {
                                qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping_2020 set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Rab2025_26'";
                            }
                            else if (ddl_session.SelectedValue.ToString() == "Rab2026_27")
                            {
                                qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping_2020 set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "' and JVS_Year='Rab2026_27'";
                            }
                            else
                            {
                                qryUpdate4 = "Update tbl_Agreemented_Godown_Mapping_2020 set Agree_Capacity='" + txtAgreeCpt.Text + "',UpdateBy='" + Client_Ip + "',UpdatedDate=GetDate() where Registration_Id = '" + txtSearch.Text + "' and GodownId_JVS='" + gvr.Cells[3].Text + "' and Agreement_Id='" + gvr.Cells[11].Text + "'";
                            }

                            SqlCommand cmd7 = new SqlCommand(qryUpdate4, sqlcon);
                            int n = cmd7.ExecuteNonQuery();
                        }
                        sqlcon.Close();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ऑनलाइन एग्रीमेंट अपडेट किया जा चुका हे क्रिप्या एग्रीमेंट  क्षमता '", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ऑनलाइन एग्रीमेंट अपडेट किया जा चुका हे कृपया चैक बॉक्स पे क्लिक करके अपडेट करें ।')", true);
                }
            }
            // Search();
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
        string strsql = "select * from tbl_Godown_Agreement where Registration_Id = '" + txtSearch.Text + "' and Inspection_Id='" + gvr.Cells[1].Text + "' and GodownId='" + gvr.Cells[3].Text + "' and Jvs_session='" + ddl_session.SelectedValue + "' ";
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
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Branch_UpdateReAgreemetn.aspx");
    }
}

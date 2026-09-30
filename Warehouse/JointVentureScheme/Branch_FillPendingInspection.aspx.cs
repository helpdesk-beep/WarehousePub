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

public partial class JointVentureScheme_Branch_FillPendingInspection : System.Web.UI.Page
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
                Search();
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
            ddlWarePrivateDepositor.SelectedValue = gv.Cells[12].Text;
            ddlWarePrivateDepositor_SelectedIndexChanged(null, null);
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
        //qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities,Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity  from tbl_godown_inspection as INSP where Fit_Unfit='PENDING' and Insp_Date < DATEADD(DAY, -15, GETDATE()) and BranchId='" + Session["UserId"].ToString() + "'";
        qry = "select Inspection_id,Godown_Offer_id,(Select Warehouse_name from tbl_WarehouseRegistration as WR where WR.Registration_ID=INSP.registration_id ) as Warehouse_name, Registration_id,Godownid,Godown_no,convert(varchar(10),Insp_Date,103) as InspDate, Stored_Commodities,Stored_Com_Depositor,Utilized_Capacity,Vacant_Capacity,isWareS_Comm_NGovt,G_OfferCapacity  from tbl_godown_inspection as INSP where Fit_Unfit='PENDING' and Insp_Date < DATEADD(DAY, -5, GETDATE()) and BranchId='" + Session["UserId"].ToString() + "'";

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
    protected void ddlWarePrivateDepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWarePrivateDepositor.SelectedItem.Text == "Yes")
        {
            txtSystemFitunfit.Text = "PENDING";
        }
        else if (ddlWarePrivateDepositor.SelectedItem.Text == "No")
        {
            txtSystemFitunfit.Text = "FIT";
        }
        else
        {
            txtSystemFitunfit.Text = "";
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
       else if (txtSystemFitunfit.Text == "")
       {
           ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गोदाम पात्र/अपात्र हे दर्ज करे :...'); </script> ");
       }
       else if (ddlWarePrivateDepositor.SelectedItem.Text == "--Select--")
       {
           ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('क्या गोदाम में पूर्व से शासकीय अथवा अन्य जमाकर्ता का स्कंध भण्डारित है दर्ज करे : ...'); </script> ");
       }
       else if (Convert.ToDecimal(txtvacantcpt.Text) >= Convert.ToDecimal(gv.Cells[13].Text))
       {
           ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('ऑफर केपेसिटि से ज्यादा इन्सपैक्शन केपेसिटि दुर्ज करना संबव नहीं हें ...'); </script> ");
       }
       else if (txtInspDate.Text=="")
       {
           ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' निरीक्षण दिनांक दर्ज करे ...'); </script> ");
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
               qry = "update tbl_Godown_Inspection set Insp_Date='" + getDate_MDY(txtInspDate.Text.Trim()) + "',Stored_Commodities='" + txtCommodityName.Text.Trim() + "',Stored_Com_Depositor='" + txtDepositorName.Text.Trim() + "',Utilized_Capacity='" + txtCurrentStoredComm.Text.Trim() + "',Vacant_Capacity='" + txtvacantcpt.Text.Trim() + "',isWareS_Comm_NGovt='" + ddlWarePrivateDepositor.SelectedValue.ToString() + "',UpdateBy='" + Client_IP + "' , UpdatedDate=GETDATE(),AutoFit_Unfit='" + txtSystemFitunfit.Text.Trim() + "',Fit_Unfit='" + txtSystemFitunfit.Text.Trim() + "' where Inspection_Id='" + inspID + "' and GodownId='" + GdwDI + "' and Godown_Offer_Id='" + GdwOfferDI + "' ";
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
        Response.Redirect("Branch_FillPendingInspection.aspx");
    }
}

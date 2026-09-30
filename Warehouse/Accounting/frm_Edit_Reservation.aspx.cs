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
using System.Globalization;

public partial class Accounting_frm_Edit_Reservation : System.Web.UI.Page
{
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataSet ds = null;
    DataSet ds2 = null;
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["Depot_DistID"] != null) && (Session["Depot_DepotID"] != null))
        {
            if (!IsPostBack)
            {
                GetReservationList();
            }
        }
        else
        {
            Response.Redirect("../Logout.aspx");
        }
    }
    void GetReservationList()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
       //qry = "SELECT [Receipt_no],[Depositor_Name],[Commodity_ID],[Quantity],[Bags],convert(varchar(10),[From_Date],103) as FromD,convert(varchar(10),[To_Date],103) as ToD,[Net_Amount],[Amount_Deposited],[Godown],[Credit_amt] FROM [Intergrated_MP_STORAGE].[dbo].[Godown_Reservation] where Depot_ID='" + Session["BranchID"].ToString() + "' and District_Code='" + Dist_id + "'";
        qry = "SELECT GR.[Receipt_no],MD.Depositor_Name,CM.Commodity_Name,GR.[Quantity],GR.[Bags],convert(varchar(10),GR.[From_Date],103) as FromD,convert(varchar(10),GR.[To_Date],103) as ToD,GR.[Net_Amount],GR.[Amount_Deposited],GR.[Godown],GR.[Credit_amt] FROM [Intergrated_MP_STORAGE].[dbo].[Godown_Reservation] as GR join tbl_MetaData_DEPOSITOR as MD on (MD.Depositor_ID=GR.Depositor_Name) join tbl_MetaData_STORAGE_COMMODITY_RList as CM on (CM.Commodity_Id=GR.Commodity_ID) where GR.Depot_Id='" + Session["BranchID"].ToString() + "' and GR.district_code='" + Dist_id + "'";
        da = new SqlDataAdapter(qry, con);
        Dt1 = new DataTable();
        da.Fill(Dt1);
        if (Dt1.Rows.Count==0)
        {
            lbl_msg.Visible = true;
            lbl_msg.Text = "No Records Found...!";
        }
        else
        {
            GvuEditReservation.DataSource = Dt1;
            GvuEditReservation.DataBind();
        }
    }
    protected void GvuEditReservation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GvuEditReservation.EditIndex = e.NewEditIndex;
        GetReservationList();
    }
    protected void GvuEditReservation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GvuEditReservation.EditIndex = -1;
        GetReservationList();
    }
    protected void GvuEditReservation_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            TextBox txtRecieptNo = (TextBox)GvuEditReservation.Rows[e.RowIndex].FindControl("txtrno");
            TextBox txtTodates = (TextBox)GvuEditReservation.Rows[e.RowIndex].FindControl("txttodate");
            string ToDate = getDate_MDY(txtTodates.Text);
            string log_qry = "insert into Godown_Reservation_log select [district_code],[Depot_Id],[Receipt_no],[Entry_Date],[Depositor_type],[Depositor_Name],[Depositor_Add],[Depositor_Cat],[Commodity_Verity],[Commodity_ID],[Quantity],[Bags],[Rate],[Amount],[Advance_Res],[Advance_Pay],[Doc_Submitted],[From_Date],[To_Date],[Dis_percent],[Discount],[Tax_Tds_Invoked],[Service_Tax],[TDS],[Net_Amount],[Amount_Deposited],[Godown],[payment_mode],[DD_chq_no],[DD_chq_date],[Bank_id],[Remarks],[Transuction],[Created_Date],[IP],[IsReserved],[Credit_amt],getdate(),'" + ip + "' from Godown_Reservation where Receipt_no='" + txtRecieptNo.Text + "'";
            SqlCommand cmdl = new SqlCommand(log_qry, con);
            con.Open();
            cmdl.ExecuteNonQuery();
            con.Close();

            string qrys = "update Godown_Reservation set To_Date='" + ToDate + "',ModifiedDate=getdate(),M_IP='" + ip + "' where Receipt_no='" + txtRecieptNo.Text + "'";
            SqlCommand cmds = new SqlCommand(qrys, con);
            con.Open();
            cmds.ExecuteNonQuery();
            con.Close();
            GvuEditReservation.EditIndex = -1;
            GetReservationList();
        }
        catch (Exception ex)
        {
            lbl_msg.Visible = true;
            lbl_msg.Text = ex.Message;
        }
        finally
        {
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
    public static DateTime FixDateTime(object valueToFix)
    {
        return FixDate(valueToFix);
    }
    public static DateTime FixDate(object valueToFix)
    {
        if (valueToFix == null)
            return new DateTime(1899, 1, 1);
        else if (Convert.IsDBNull(valueToFix))
            return new DateTime(1899, 1, 1);
        else
        {
            try
            {
                return Convert.ToDateTime(valueToFix);
            }
            catch
            {
                return new DateTime(1899, 1, 1);
            }
        }
    }
}

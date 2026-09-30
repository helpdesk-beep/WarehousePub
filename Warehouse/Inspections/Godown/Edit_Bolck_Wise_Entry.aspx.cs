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
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System;

public partial class Inspections_Godown_Edit_Bolck_Wise_Entry : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string GodownID = "";
    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    string depositername = "";
    string commodityname = "";
    string stackid = "";
    string stackname = "";
    string BranchID = "";
    string DepositerID = "";
    string CommodityID = "";
    string Cropyear = "";
    string noofbags = "";
    string weight = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        BranchID = Session["hdnbranchid"].ToString();
        GodownID = Session["hdnGodown_ID"].ToString();
        DepositerID = Session["hdnDepositor_ID"].ToString();
        CommodityID = Session["hdnCommodity_Id"].ToString();
        Cropyear = Session["hdnCropYear"].ToString();

        commodityname = Session["lblcommodity"].ToString();
        depositername = Session["lblDepositor_Name"].ToString();
        stackid = Session["lblstack_id"].ToString();
        stackname = Session["lblStack_Name"].ToString();
        noofbags = Session["lblrecbags"].ToString();
        weight = Session["lblRecWeight"].ToString();
        if (!IsPostBack)
        {
            lbldepositername.Text = depositername.ToString();
            lblcommodityname.Text = commodityname.ToString();
            lblstackid.Text = stackid.ToString();
            lblstackname.Text = stackname.ToString();
            lblnoofbags.Text = noofbags.ToString();
            lblweight.Text = weight.ToString();
            lblcropyear.Text = Cropyear.ToString();
          
            txtTotal.Attributes.Add("readonly", "readonly");
            lbltotalbags.Attributes.Add("readonly", "readonly");
            fillvcDEPPECbygridview(Session["hdnID"].ToString());


        }

    }
    public void checkvalidation()
    {
        if (txtLendth.Text == "" || txtLendth.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Length!....')", true);
            txtLendth.Focus();
            return;
        }
       else if (txtwidth.Text == "" || txtwidth.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Width!....')", true);
            txtwidth.Focus();
            return;
        }
        else if (txtextralendth.Text == "" || txtextralendth.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Extra Length!....')", true);
            txtwidth.Focus();
            return;
        }
        else if (txtheight.Text == "" || txtheight.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Height!....')", true);
            txtheight.Focus();
            return;
        }
        else if (txtnoofblock.Text == "" || txtnoofblock.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter No. of Blocks!....')", true);
            txtnoofblock.Focus();
            return;
        }
        else if (txtup.Text == "" || txtup.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('ऊपर रखे हुए बोरो की एंट्री करें !....')", true);
            txtup.Focus();
            return;
        }
        else if (txtbelow.Text == "" || txtbelow.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('नीचे रखे हुए बोरो की एंट्री करें!....')", true);
            txtbelow.Focus();
            return;
        }
        else if (txtspillagebag.Text == "" || txtspillagebag.Text == String.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Spillag Bag की एंट्री करें!....')", true);
            txtspillagebag.Focus();
            return;
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

    protected void txtextralendth_TextChanged(object sender, EventArgs e)
    {
        int A =int.Parse(txtLendth.Text);
        int B = int.Parse(txtwidth.Text);
        int C = int.Parse(txtextralendth.Text);

        int i = A + B + C ;
        i = A + B + C;
        txtTotal.Text = i.ToString();

        // total
        int Total = int.Parse(txtTotal.Text);
        int height = int.Parse(txtheight.Text);
        int noofblock = int.Parse(txtnoofblock.Text);

        int NoofBags = Total * height * noofblock;
        NoofBags = Total * height * noofblock;
        lbltotalbags.Text = NoofBags.ToString();

        int Totalbag = int.Parse(lbltotalbags.Text);
        int UP = int.Parse(txtup.Text);
        int Below = int.Parse(txtbelow.Text);

        int TotalNoofBags = Totalbag + UP + Below;
        TotalNoofBags = Totalbag + UP + Below;
        txttotalnoofbags.Text = TotalNoofBags.ToString();
    }
    protected void txtLendth_TextChanged(object sender, EventArgs e)
    {
        int A = int.Parse(txtLendth.Text);
        int B = int.Parse(txtwidth.Text);
        int C = int.Parse(txtextralendth.Text);

        int i = A + B + C;
        i = A + B + C;
        txtTotal.Text = i.ToString();

        // total
        int Total = int.Parse(txtTotal.Text);
        int height = int.Parse(txtheight.Text);
        int noofblock = int.Parse(txtnoofblock.Text);

        int NoofBags = Total * height * noofblock;
        NoofBags = Total * height * noofblock;
        lbltotalbags.Text = NoofBags.ToString();

        int Totalbag = int.Parse(lbltotalbags.Text);
        int UP = int.Parse(txtup.Text);
        int Below = int.Parse(txtbelow.Text);

        int TotalNoofBags = Totalbag + UP + Below;
        TotalNoofBags = Totalbag + UP + Below;
        txttotalnoofbags.Text = TotalNoofBags.ToString();
    }

    protected void txtwidth_TextChanged(object sender, EventArgs e)
    {
        int A = int.Parse(txtLendth.Text);
        int B = int.Parse(txtwidth.Text);
        int C = int.Parse(txtextralendth.Text);

        int i = A + B + C;
        i = A + B + C;
        txtTotal.Text = i.ToString();

        // total
        int Total = int.Parse(txtTotal.Text);
        int height = int.Parse(txtheight.Text);
        int noofblock = int.Parse(txtnoofblock.Text);

        int NoofBags = Total * height * noofblock;
        NoofBags = Total * height * noofblock;
        lbltotalbags.Text = NoofBags.ToString();

        int Totalbag = int.Parse(lbltotalbags.Text);
        int UP = int.Parse(txtup.Text);
        int Below = int.Parse(txtbelow.Text);

        int TotalNoofBags = Totalbag + UP + Below;
        TotalNoofBags = Totalbag + UP + Below;
        txttotalnoofbags.Text = TotalNoofBags.ToString();
    }
    protected void txtheight_TextChanged(object sender, EventArgs e)
    {
        int Total = int.Parse(txtTotal.Text);
        int height = int.Parse(txtheight.Text);
        int noofblock = int.Parse(txtnoofblock.Text);

        int NoofBags = Total * height * noofblock;
        NoofBags = Total * height * noofblock;
        lbltotalbags.Text = NoofBags.ToString();
    }
    protected void txtnoofblock_TextChanged(object sender, EventArgs e)
    {
        int Total = int.Parse(txtTotal.Text);
        int height = int.Parse(txtheight.Text);
        int noofblock = int.Parse(txtnoofblock.Text);

        int NoofBags = Total * height * noofblock;
        NoofBags = Total * height * noofblock;
        lbltotalbags.Text = NoofBags.ToString();
    }
    protected void txtup_TextChanged(object sender, EventArgs e)
    {
        int Totalbag = int.Parse(lbltotalbags.Text);
        int UP = int.Parse(txtup.Text);
        int Below = int.Parse(txtbelow.Text);

        int TotalNoofBags = Totalbag + UP + Below;
        TotalNoofBags = Totalbag + UP + Below;
        txttotalnoofbags.Text = TotalNoofBags.ToString();
    }
    protected void txtbelow_TextChanged(object sender, EventArgs e)
    {
        int Totalbag = int.Parse(lbltotalbags.Text);
        int UP = int.Parse(txtup.Text);
        int Below = int.Parse(txtbelow.Text);

        int TotalNoofBags = Totalbag + UP + Below;
        TotalNoofBags = Totalbag + UP + Below;
        txttotalnoofbags.Text = TotalNoofBags.ToString();
    }
 public void fillvcDEPPECbygridview(string ID)
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();
            }
            SqlCommand cmd = new SqlCommand("Insp_Get_Stack_Block_Wise_Details_For_Edit", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", ID.ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (con.State == ConnectionState.Open)
            { con.Close(); }
            if (dt.Rows.Count > 0)
            {
                txtLendth.Text = dt.Rows[0]["Length"].ToString();
                txtwidth.Text = dt.Rows[0]["Width"].ToString();
                txtextralendth.Text = dt.Rows[0]["Extra"].ToString();
                txtTotal.Text = dt.Rows[0]["Total_L_W_E"].ToString();
                txtheight.Text = dt.Rows[0]["Height"].ToString();
                txtnoofblock.Text = dt.Rows[0]["Number_Of_Block"].ToString();
                lbltotalbags.Text = dt.Rows[0]["No_of_Bags"].ToString();
                txtup.Text = dt.Rows[0]["Up"].ToString();
                txtbelow.Text = dt.Rows[0]["Below"].ToString();
                txttotalnoofbags.Text = dt.Rows[0]["Total_Bags"].ToString();
                txtspillagebag.Text = dt.Rows[0]["Spillage_bag"].ToString();
                txtremark.Text = dt.Rows[0]["Remark"].ToString();
                btnupdate.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('"+ ex.Message + "')", true);
        }
        finally
        { if (conStr.State == ConnectionState.Open) { conStr.Close(); } }

    }
  
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            checkvalidation();
            string CS = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            using (SqlConnection constr = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insp_Stack_Block_Wise_Godown_Entry_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.AddWithValue("@ID", Session["hdnID"].ToString());
                cmd.Parameters.AddWithValue("@Length", txtLendth.Text.ToString());
                cmd.Parameters.AddWithValue("@Width", txtwidth.Text.ToString());
                cmd.Parameters.AddWithValue("@Extra", txtextralendth.Text.ToString());
                cmd.Parameters.AddWithValue("@Total_L_W_E", txtTotal.Text.ToString());
                cmd.Parameters.AddWithValue("@Height", txtheight.Text.ToString());
                cmd.Parameters.AddWithValue("@Number_Of_Block", txtnoofblock.Text.ToString());
                cmd.Parameters.AddWithValue("@No_of_Bags", lbltotalbags.Text.ToString());
                cmd.Parameters.AddWithValue("@Up", txtup.Text.ToString());
                cmd.Parameters.AddWithValue("@Below", txtbelow.Text.ToString());
                cmd.Parameters.AddWithValue("@Total_Bags", txttotalnoofbags.Text.ToString());
                cmd.Parameters.AddWithValue("@Inserted_By", IPAddress);
                cmd.Parameters.AddWithValue("@Spillage_bag", txtspillagebag.Text);
                cmd.Parameters.AddWithValue("@Remark", txtremark.Text.ToString());
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    string strMsg = "Record Update Successfully |||";

                   // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + strMsg + "');window.location ='/warehouse/Inspections/Godown/Bolck_Wise_Entry.aspx';", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('You Have Allready Submitted!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }
    }





   
}
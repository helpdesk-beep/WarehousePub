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

public partial class Accounting_frm_Reservation_Register : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlCommand cmd = new SqlCommand();
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    SqlDataAdapter da = new SqlDataAdapter();
    string qry = "";
    string Branch_Id = "";
    string District_Id = "";
    DateTime FromDate = new DateTime();
    DateTime ToDate = new DateTime();
    string Register_No = "";
    string Register_Type = "";
    int RId = 0;
    string Client_Ip = "";
    string Godown_Id = "";
    string GPID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
         if ((Session["BranchID"] != null))
        {
            if (!IsPostBack)
            {
                ddlRegType.Enabled = false;
                fillCropYear();
                GetDepositioType();
                fillBankList();
                //Get_Bank();
                //fillFinancialYear();
               
            }
            District_Id = Session["Depot_DistID"].ToString().Substring(2, 2);
            Branch_Id = Session["BranchID"].ToString();
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void ddldepositor_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDepositorName();
    }
    void GetDepositioType()
    {
        qry = "select Depositor_Type,Depositor_Type_Id from dbo.tbl_MetaData_Depositor_Type";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddldepositor.DataSource = ds.Tables[0];
            ddldepositor.DataTextField = "Depositor_Type";
            ddldepositor.DataValueField = "Depositor_Type_Id";
            ddldepositor.DataBind();
            ddldepositor.Items.Insert(0, "--Select--");
        }
    }
    protected void fillCropYear()
    {
        ddlcropyr.Items.Insert(0, "--Select--");
        ddlcropyr.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));

        //ddlcropyr.SelectedIndex = 0;
    }
    void GetDepositorName()
    {
        string dtype = ddldepositor.SelectedItem.Text;
        if (dtype == "Institution")
        {
            //qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC','MPWLC','FCI') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + Session["BranchID"].ToString() + "' and Depositor_Type ='Institution'";
            qry = "select [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE Depositor_Name  in ('MPSCSC') union SELECT [Depositor_ID], [Depositor_Name] FROM [tbl_MetaData_DEPOSITOR] WHERE  BranchId='" + Session["BranchID"].ToString() + "' and Depositor_Type ='Institution'";
            da = new SqlDataAdapter(qry, con);
            ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {
                ddldepos_name.DataSource = ds.Tables[0];
                ddldepos_name.DataTextField = "Depositor_Name";
                ddldepos_name.DataValueField = "Depositor_ID";
                ddldepos_name.DataBind();
                ddldepos_name.Items.Insert(0, "--Select--");
            }
        }
        else
        {
            string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
            qry = "select Depositor_Name,Depositor_ID from dbo.tbl_MetaData_DEPOSITOR where Depot_ID='" + Session["BranchID"].ToString() + "' and District_ID='23" + Dist_id + "' and Depositor_Type='" + dtype + "'";
            da = new SqlDataAdapter(qry, con);
            ds = new DataSet();
            da.Fill(ds);
            if (ds == null)
            {
            }
            else
            {
                ddldepos_name.DataSource = ds.Tables[0];
                ddldepos_name.DataTextField = "Depositor_Name";
                ddldepos_name.DataValueField = "Depositor_ID";
                ddldepos_name.DataBind();
                ddldepos_name.Items.Insert(0, "--Select--");
            }
        }
    }
    protected void ddldepos_name_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetVerity();
    }
    void GetVerity()
    {
        qry = "select verity_code,Verity_Eng from dbo.tbl_MetaData_Verity";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlverity.DataSource = ds.Tables[0];
            ddlverity.DataTextField = "Verity_Eng";
            ddlverity.DataValueField = "verity_code";
            ddlverity.DataBind();
            ddlverity.Items.Insert(0, "--Select--");
        }
    }
    void GetCommodity()
    {
        string verity = ddlverity.SelectedValue;
        qry = "select Commodity_ID,Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY_RList where Rep_Grp_Code='" + verity + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
        }
        else
        {
            ddlcomodity.DataSource = ds.Tables[0];
            ddlcomodity.DataTextField = "Commodity_Name";
            ddlcomodity.DataValueField = "Commodity_ID";
            ddlcomodity.DataBind();
            ddlcomodity.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlverity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCommodity();
    }
    void GetPackingType()
    {
        string qry = "select Packing_Id,Packing_Name + '('+rtrim(Remarks)+')' as Packing_Name from dbo.Packing_type ";
        //string qry = "select Packing_Id,Packing_Name + '('+rtrim(Remarks)+')' as Packing_Name from dbo.Packing_type where Packing_Id='1'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlpacktype.DataSource = ds.Tables[0];
            ddlpacktype.DataTextField = "Packing_Name";
            ddlpacktype.DataValueField = "Packing_Id";
            ddlpacktype.DataBind();
            ddlpacktype.Items.Insert(0, "--Select--");
        }
    }
    void GetWeight()
    {
        string qry = "select Weigt_ID,Weight_Type from dbo.tbl_MetaData_WeightType";
       // string qry = "select Weigt_ID,Weight_Type from dbo.tbl_MetaData_WeightType where Weigt_ID='5'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlweight.DataSource = ds.Tables[0];
            ddlweight.DataTextField = "Weight_Type";
            ddlweight.DataValueField = "Weigt_ID";
            ddlweight.DataBind();
            ddlweight.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlpacktype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetWeight();
    }
    protected void ddlweight_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlcomodity_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPackingType();
    }
    public void Insert_Reservation_Register()
    {
        int r = 0;
        //qry = "insert into tbl_Reservation_Register(Register_No,Demand_Lett_No,Confirm_Lett_No,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Confirmed_By,Packing_Type,Weight_Type,Reserved_Quantity,Created_Date) values ('" + Register_No + "','" + txtDltr.Text + "','" + txtCltr.Text + "','" + ddldepositor.SelectedValue.ToString() + "','" + ddldepos_name.SelectedValue.ToString() + "','" + ddlverity.SelectedValue.ToString() + "','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(txtfdate.Text) + "','" + getDate_MDY(txttodate.Text) + "','" + ddlConfirmBy.SelectedItem.Text +"','" + ddlpacktype.SelectedValue.ToString() +"','" + ddlweight.SelectedValue.ToString() +"','"+ txtrunit.Text +"',getdate())";
        qry = "insert into tbl_Reservation_Register(Register_No,Demand_Lett_No,Confirm_Lett_No,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Confirmed_By,Packing_Type,Weight_Type,Reserved_Quantity,Created_Date,Demand_Lett_Date,Confirm_Lett_Date) values ('" + Register_No + "','" + txtDltr.Text + "','" + txtCltr.Text + "','" + ddldepositor.SelectedValue.ToString() + "','" + ddldepos_name.SelectedValue.ToString() + "','" + ddlverity.SelectedValue.ToString() + "','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(txtfdate.Text) + "','" + getDate_MDY(txttodate.Text) + "','" + ddlConfirmBy.SelectedItem.Text + "','" + ddlpacktype.SelectedValue.ToString() + "','" + ddlweight.SelectedValue.ToString() + "','" + txtrunit.Text + "',getdate(),'" + getDate_MDY(txtDltrd.Text) + "','" + getDate_MDY(txtCltrd.Text) + "')";
        cmd.CommandText = qry;
        cmd.Connection = con;
        con.Open();
        r=cmd.ExecuteNonQuery();
        con.Close();
        if (r > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Record Inserted Successfully...!'); </script> ");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occur...!'); </script> ");
        }
    }
    public void Insert_Hired_Godown_Register()
    {
        //string VO = "";
        //string VD1 = "";
        //string VD2 = "";
        
        //if (txtvon.Text != ""&& txtvond.Text != "" && txtVacDate.Text != "")
        //{
        //    VD1 = getDate_MDY(txtvon.Text);
        //    VD2 = getDate_MDY(txtVacDate.Text);
        //    VO = txtvon.Text;
        //}
        int r = 0;
        int s = 0;
         //qry = "insert into tbl_Register_Hired_Godown(Register_No,NameofOwner,Godown_No,Sanction_ONo,Sanction_Date,TOP_Date,Rent_M,Storage_Capacity,Created_Date,PAN,Bank_ID,AccNo,IFSC_Code,Bank_Add,Is_Active,remark,Vacation_ONo,Vacation_Date,DateOfVacant) values ('" + Register_No + "','" + txtnow.Text + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtson.Text + "','" + getDate_MDY(txtsond.Text) + "','" + getDate_MDY(txtsond.Text) + "','" + txtrent.Text + "','" + txtscapacity.Text + "',getdate(),'" + txtPan.Text + "','" + ddlBank.SelectedValue.ToString() + "','" + txtAcc.Text + "','" + txtIfsc.Text + "','" + txtBAdd.Text + "','Y','" + txtremark.Text + "','" + txtvon.Text + "','" + getDate_MDY(txtvond.Text) + "','" + getDate_MDY(txtVacDate.Text) + "')";
        if (txtvon.Text != "" && txtvond.Text !="" && txtVacDate.Text != "")
        {
            qry = "insert into tbl_Register_Hired_Godown(Register_No,Godown_No,Sanction_ONo,Sanction_Date,TOP_Date,Rent_M,Storage_Capacity,Created_Date,Is_Active,remark,Vacation_ONo,Vacation_Date,DateOfVacant) values ('" + Register_No + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtson.Text + "','" + getDate_MDY(txtsond.Text) + "','" + getDate_MDY(txtdvon.Text) + "','" + txtrent.Text + "','" + txtscapacity.Text + "',getdate(),'N','" + txtremark.Text + "','" + txtvon.Text + "','" + getDate_MDY(txtvond.Text) + "','" + getDate_MDY(txtVacDate.Text) + "')";
        }
        else
        {
            qry = "insert into tbl_Register_Hired_Godown(Register_No,Godown_No,Sanction_ONo,Sanction_Date,TOP_Date,Rent_M,Storage_Capacity,Created_Date,Is_Active,remark) values ('" + Register_No + "','" + ddlgodown.SelectedValue.ToString() + "','" + txtson.Text + "','" + getDate_MDY(txtsond.Text) + "','" + getDate_MDY(txtdvon.Text) + "','" + txtrent.Text + "','" + txtscapacity.Text + "',getdate(),'Y','" + txtremark.Text + "')";
        }
        cmd.CommandText = qry;
        cmd.Connection = con;
        con.Open();
        r = cmd.ExecuteNonQuery();
        con.Close();
        if (r > 0)
        {
            string qry2 = "update tbl_MetaData_GODOWN set [Godown_APN]='" + txtnow.Text + "',[Godown_Email]='" + txtEmail.Text + "',[Godown_Mobile]='" + txtMob.Text + "',[Godown_Address]='" + txtaddress.Text + "',[PAN] ='"+ txtPan.Text +"',[Bank_ID]='" + ddlBank.SelectedValue.ToString() + "',[AccNo]='" + txtAcc.Text + "',[IFSC_Code]='" + txtIfsc.Text + "',[Bank_Add]='" + txtBAdd.Text + "' where [Godown_ID]='" + ddlgodown.SelectedValue.ToString() + "'";
            cmd.CommandText = qry2;
            cmd.Connection = con;
            con.Open();
            s = cmd.ExecuteNonQuery();
            con.Close();
            if (r > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Record Save Successfully...!'); </script> ");
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occur...!'); </script> ");
        }
    }
    public void Insert_Register_Master()
    {
        if (ddlRegType.SelectedValue.ToString() == "1")
        {
            Register_Type = "RR";
        }
        else if (ddlRegType.SelectedValue.ToString() == "2")
        {
            Register_Type = "HG";
        }
        Client_Ip= Request.ServerVariables["REMOTE_ADDR"].ToString();
        int r = 0;
        qry = "insert into tbl_Register_Master_Detail(Register_No,District_Id,Branch_Id,Register_Type,Financial_Year,Created_Date,Client_IP,RId) values ('" + Register_No + "','" + District_Id + "','" + Branch_Id + "','" + Register_Type + "','" + ddlcropyr.SelectedItem.Text + "',getdate(),'" + Client_Ip + "','" + RId + "')";
        //qry = "insert into tbl_Reservation_Register(Register_No,Demand_Lett_No,Confirm_Lett_No,Depositor_Type_Id,Depositor_Id,Commodity_Type_Id,Commodity_Id,From_Date,To_Date,Confirmed_By,Packing_Type,Weight_Type,Reserved_Quantity,Created_Date) values ('" + Register_No + "','" + txtDltr.Text + "','" + txtCltr.Text + "','" + ddldepositor.SelectedValue.ToString() + "','" + ddldepos_name.SelectedValue.ToString() + "','" + ddlverity.SelectedValue.ToString() + "','" + ddlcomodity.SelectedValue.ToString() + "','" + getDate_MDY(txtfdate.Text) + "','" + getDate_MDY(txttodate.Text) + "','" + ddlConfirmBy.SelectedItem.Text + "','" + ddlpacktype.SelectedValue.ToString() + "','" + ddlweight.SelectedValue.ToString() + "','" + txtrunit.Text + "',getdate())";
        cmd.CommandText = qry;
        cmd.Connection = con;
        con.Open();
        r = cmd.ExecuteNonQuery();
        con.Close();
        if (r > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Record Inserted Successfully...!'); </script> ");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occur...!'); </script> ");
        }
    }
    public void GetRegisterNo()
    {
        if (ddlRegType.SelectedValue.ToString() == "1")
        {
            Register_Type = "RR";
        }
        else if (ddlRegType.SelectedValue.ToString() == "2")
        {
            Register_Type = "HG";
        } 
        string BranchID = Session["BranchID"].ToString();
        qry = "select max(RId) as BId from tbl_Register_Master_Detail where branch_Id='" + BranchID + "' and Register_Type='" + Register_Type + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                RId = Convert.ToInt32(dt.Rows[0]["BId"]);
                int SubBN = RId + 1;
                Register_No = BranchID + "" + Register_Type + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + SubBN.ToString();
                RId = SubBN;
            }
            else
            {
                Register_No = BranchID + "" + Register_Type + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
                RId = 1;
            }
        }
        else
        {
            Register_No = BranchID + "" + Register_Type + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
            RId = 1;
        }
        //ViewState["RegNo"] = Register_No;
        //ViewState["RID"] = RId;
        //ViewState["RegNo"] = Register_No;
        //ViewState["RID"] = RId;
    }
    public void GetGPaymentID()
    {

        Godown_Id = ddlgodown.SelectedValue.ToString();
        string BranchID = Session["BranchID"].ToString();
        qry = "select max(PID) as BId from tbl_Godown_Rent_Payment_Detail where Godown_ID='" + ddlgodown.SelectedValue.ToString() + "' and Financial_Year='" + ddlcropyr.SelectedItem.Text + "'";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count != 0 && dt.Rows[0]["BId"].ToString() != null && dt.Rows[0]["BId"].ToString() != "")
        {
            if (dt.Rows[0]["BId"].ToString() != "" || Convert.ToInt32(dt.Rows[0]["BId"]) != 0)
            {
                RId = Convert.ToInt32(dt.Rows[0]["BId"]);
                int SubBN = RId + 1;
                GPID = Godown_Id + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + SubBN.ToString();
                RId = SubBN;
            }
            else
            {
                GPID = Godown_Id + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
                RId = 1;
            }
        }
        else
        {
            GPID = Godown_Id + "" + ((DateTime.Now.Year).ToString()).Substring(2, 2) + "1";
            RId = 1;
        }
        //ViewState["RegNo"] = Register_No;
        //ViewState["RID"] = RId;
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
    protected void ddlRegType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegType.SelectedValue.ToString() == "1")
        {
            trReservationRegister.Visible = true;
            trHiredGodownRegister.Visible = false;
            trHiredGodownPayment.Visible = false;
        }
        else if (ddlRegType.SelectedValue.ToString() == "2")
        {
            trReservationRegister.Visible = false;
            trHiredGodownRegister.Visible = true;
            GetGodown();
            trHiredGodownPayment.Visible = true;
            //ddlGodownType.SelectedValue = "2";
            //ddlGodownType.Enabled = false;
        }
    }
    public void GetGodown()
    {
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["BranchID"].ToString();
        ddlgodown.Items.Clear();
        //qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN where DistrictId='23" + Dist_id + "' and DepotId='" + Session["BranchID"].ToString() + "'";
        qry = "select Godown_Name,Godown_ID from dbo.tbl_MetaData_GODOWN where DistrictId='23" + Dist_id + "' and DepotId='" + Session["BranchID"].ToString() + "' and Hired_Type in ('Hired')";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlgodown.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlgodown.DataSource = ds.Tables[0];
            ddlgodown.DataTextField = "Godown_Name";
            ddlgodown.DataValueField = "Godown_ID";
            ddlgodown.DataBind();
            ddlgodown.Items.Insert(0, "--Select--");
        }
    }
    //protected void fillFinancialYear()
    //{
    //    ddlFyear.Items.Insert(0, "2015");
    //    ddlFyear.Items.Insert(1, "2014");
    //    ddlFyear.Items.Insert(2, "2013");
    //    ddlFyear.Items.Insert(3, "2012");
    //    ddlFyear.Items.Insert(4, "2011");
    //    ddlFyear.Items.Insert(5, "2010");
    //    ddlFyear.Items.Insert(6, "2009");
    //    ddlFyear.Items.Insert(7, "Before 2009");
    //    ddlFyear.SelectedIndex = 0;
    //}
    protected void fillBankList()
    {
        ddlBank.Items.Add(new ListItem("--Select--", "0"));
        ddlBank.Items.Add(new ListItem("ALLAHABAD BANK", "1"));
        ddlBank.Items.Add(new ListItem("ANDHRA BANK", "2"));
        ddlBank.Items.Add(new ListItem("AXIS BANK", "3"));
        ddlBank.Items.Add(new ListItem("BANK OF BARODA", "4"));
        ddlBank.Items.Add(new ListItem("BANK OF INDIA", "5"));
        ddlBank.Items.Add(new ListItem("BANK OF MAHARASHTRA", "6"));
        ddlBank.Items.Add(new ListItem("CANARA BANK", "7"));
        ddlBank.Items.Add(new ListItem("CENTRAL BANK OF INDIA", "8"));
        ddlBank.Items.Add(new ListItem("DENA BANK", "9"));
        ddlBank.Items.Add(new ListItem("DHANALAKSHMI BANK", "10"));
        ddlBank.Items.Add(new ListItem("HDFC BANK", "11"));
        ddlBank.Items.Add(new ListItem("IDBI BANK","12"));
        ddlBank.Items.Add(new ListItem("ORIENTAL BANK OF COMMERCE","13"));
        ddlBank.Items.Add(new ListItem("PUNJAB NATIONAL BANK", "14"));
        ddlBank.Items.Add(new ListItem("STATE BANK OF INDIA", "15"));
        ddlBank.SelectedIndex = 0;
    }
    protected void btnSHired_Click(object sender, EventArgs e)
    {
        if (ddlgodown.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown...!'); </script> ");
        }
        else if (txtnow.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Name of the Owner...!'); </script> ");
        }
        else if (txtgno.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Godown No....!'); </script> ");
        }
        else if (txtMob.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Your Mobile No....!'); </script> ");
        }
        else if (txtEmail.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Your Email Address....!'); </script> ");
        }
        else if (txtson.Text == "" || txtsond.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter RO Sanction Order No. & Date...!'); </script> ");
        }
        else if (txtdvon.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Date of Taking over Possession...!'); </script> ");
        }
        else if (txtrent.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Monthly Rent...!'); </script> ");
        }
        else
        {
            if (btnSHired.Text == "Update")
            {
                Update_Godown_Register();
            }
            else
            {
                GetRegisterNo();
                Insert_Hired_Godown_Register();
                Insert_Register_Master();
                //Update_Godown_Register();
                //Update_Godown_Detail();
                btnSHired.Enabled = false;
            }
        }
    }
    protected void btnRSubmit_Click(object sender, EventArgs e)
    {
        if (ddldepositor.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Depositor Type...!'); </script> ");
        }
        else if (ddldepos_name.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Depositor...!'); </script> ");
        }
        else if (ddlverity.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Commodity Verity...!'); </script> ");
        }
        else if (ddlcomodity.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Commodity...!'); </script> ");
        }
        else if (txtfdate.Text == "" || txttodate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Reservation Period...!'); </script> ");
        }
        else if (ddlpacktype.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Packing Type...!'); </script> ");
        }
        else if (ddlweight.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Weight Type...!'); </script> ");
        }
        else if (txtrunit.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Reserve Quantity...!'); </script> ");
        }
        else if (ddlConfirmBy.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Confirmation By...!'); </script> ");
        }
        else if (txtDltr.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Demand Letter No....!'); </script> ");
        }
        else if (txtDltrd.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Demand Letter Date....!'); </script> ");
        }
        else if (txtCltr.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Confirmation Letter No....!'); </script> ");
        }
        else if (txtCltrd.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Confirmation Letter Date....!'); </script> ");
        }
        else
        {
            GetRegisterNo();
            Insert_Reservation_Register();
            Insert_Register_Master();
            btnRSubmit.Enabled = false;
        }
    }
    protected void btnRCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void btnCHired_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Branch_Welcome.aspx");
    }
    protected void ddlgodown_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetGodownOwner();
        //fillBankList();
        //Get_Bank();
    }
    public void GetGodownOwner()
    {
        string IsActive="";
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["BranchID"].ToString();
        //qry = "select Godown_ID,RIGHT(Godown_ID,3) as Godown_No,Godown_APN,Godown_Name,Godown_APN,Godown_Address,Godown_Email,Godown_Mobile from dbo.tbl_MetaData_GODOWN where DistrictId='23" + Dist_id + "' and DepotId='" + Session["BranchID"].ToString() + "' and Hired_Type in ('Hired') and Godown_ID='"+ ddlgodown.SelectedValue.ToString() +"'";
        //qry = "select Godown_ID,RIGHT(Godown_ID,3) as Godown_No,Godown_APN,Godown_Name,Godown_Scientific_Capacity,Godown_APN,Godown_Mobile,Godown_Address,Godown_Email,Bank_ID,AccNo,IFSC_Code,Bank_Add,PAN from dbo.tbl_MetaData_GODOWN where DistrictId='23" + Dist_id + "' and DepotId='" + Session["BranchID"].ToString() + "' and Hired_Type in ('Hired') and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        qry = "select RHG.Register_No,MDG.Godown_ID,RIGHT(MDG.Godown_ID,3) as Godown_No,MDG.Godown_APN,MDG.Godown_Name,MDG.Godown_Scientific_Capacity,MDG.Godown_APN,MDG.Godown_Mobile,MDG.Godown_Address,MDG.Godown_Email,MDG.Bank_ID,MDG.AccNo,MDG.IFSC_Code,MDG.Bank_Add,MDG.PAN,RHG.Rent_M,RHG.Sanction_ONo,(CONVERT(varchar(10),RHG.Sanction_Date,103)) as Sanction_Date,CONVERT(varchar(10),RHG.TOP_Date,103) as Possession_Date,CONVERT(varchar(10),RHG.DateOfVacant,103) as DateOfVacant,RHG.Vacation_ONo,CONVERT(varchar(10),RHG.Vacation_Date,103) as Vacation_Date,RHG.Is_Active from tbl_MetaData_GODOWN as MDG left outer join tbl_Register_Hired_Godown as RHG on RHG.Godown_No=MDG.Godown_ID where DistrictId='23" + Dist_id + "' and DepotId='" + Session["BranchID"].ToString() + "' and Hired_Type in ('Hired') and Godown_ID='" + ddlgodown.SelectedValue.ToString() + "'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {
            ddlgodown.Items.Insert(0, "--Select--");
        }
        else
        {
            Register_No = ds.Tables[0].Rows[0]["Register_No"].ToString();

            txtnow.Text = ds.Tables[0].Rows[0]["Godown_APN"].ToString();
            txtaddress.Text = ds.Tables[0].Rows[0]["Godown_Address"].ToString();
            txtMob.Text = ds.Tables[0].Rows[0]["Godown_Mobile"].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0]["Godown_Email"].ToString();
            txtgno.Text = ds.Tables[0].Rows[0]["Godown_No"].ToString();

           string BankId = ds.Tables[0].Rows[0]["Bank_ID"].ToString();
           if (BankId != null && BankId != "")
           {
               ddlBank.SelectedValue = BankId;
           }
           else
           {
               ddlBank.SelectedValue = "0";
           }
            txtAcc.Text = ds.Tables[0].Rows[0]["AccNo"].ToString();
            txtIfsc.Text = ds.Tables[0].Rows[0]["IFSC_Code"].ToString();
            txtBAdd.Text = ds.Tables[0].Rows[0]["Bank_Add"].ToString();
            txtPan.Text = ds.Tables[0].Rows[0]["PAN"].ToString();
            decimal ScientCap=0;
            ScientCap=Convert.ToDecimal(ds.Tables[0].Rows[0]["Godown_Scientific_Capacity"]);
            txtscapacity.Text = (ScientCap / 10).ToString();
            //////////////from hired register
            IsActive = ds.Tables[0].Rows[0]["Is_Active"].ToString();
            if (IsActive == "Y")
            {
                txtrent.Text = ds.Tables[0].Rows[0]["Rent_M"].ToString();
                txtson.Text = ds.Tables[0].Rows[0]["Sanction_ONo"].ToString();
                txtsond.Text = ds.Tables[0].Rows[0]["Sanction_Date"].ToString();
                txtdvon.Text = ds.Tables[0].Rows[0]["Possession_Date"].ToString();
                txtvon.Text = ds.Tables[0].Rows[0]["Vacation_ONo"].ToString();
                if (ds.Tables[0].Rows[0]["Vacation_Date"].ToString() != "01/01/1919" && ds.Tables[0].Rows[0]["DateOfVacant"].ToString() != "01/01/1919")
                {
                    txtvond.Text = ds.Tables[0].Rows[0]["Vacation_Date"].ToString();
                    txtVacDate.Text = ds.Tables[0].Rows[0]["DateOfVacant"].ToString();
                }
                else
                {
                    txtvond.Text = "";
                    txtVacDate.Text = "";
                }
                if (Register_No != "" && Register_No != null)
                {
                    ViewState["Register_No"] = Register_No;
                    btnSHired.Text = "Update";
                }
                else
                {
                    btnSHired.Text = "Submit";
                }
            }
            else
            {
                txtrent.Text = "";
                txtson.Text = "";
                txtsond.Text = "";
                txtdvon.Text = "";
                txtVacDate.Text = "";
                txtvon.Text = "";
                txtvond.Text = "";
                btnSHired.Text = "Submit";
            }
            
           
        }
    }
    public void Update_Godown_Register()
    {
        string Is_Active = "Y";
        int r = 0;
        string Dist_id = Session["Depot_DistID"].ToString().Substring(2, 2);
        string sid = Session["BranchID"].ToString();
        //qry = "update tbl_MetaData_GODOWN set Godown_APN='"+ txtnow.Text +"',Godown_Address='"+ txtaddress.Text +"',Godown_Mobile='"+ txtMob.Text +"',Godown_Email='" + txtEmail.Text + "' where Godown_ID='"+ ddlgodown.SelectedValue.ToString() +"'";
        if (txtvon.Text != "" && txtvond.Text != "" && txtVacDate.Text != "")
        {
            Is_Active = "N";
        }
        qry = "update tbl_Register_Hired_Godown set Sanction_ONo='" + txtson.Text + "',Sanction_Date='" + getDate_MDY(txtsond.Text) + "',TOP_Date='" + getDate_MDY(txtdvon.Text) + "',Vacation_ONo='" + txtvon.Text + "',Vacation_Date='" + getDate_MDY(txtvond.Text) + "',DateOfVacant='" + getDate_MDY(txtVacDate.Text) + "',Rent_M=" + txtrent.Text + ",Is_Active='"+ Is_Active +"' where Register_No='" + ViewState["Register_No"].ToString() + "'";
        cmd.CommandText = qry;
        cmd.Connection = con;
        con.Open();
        r=cmd.ExecuteNonQuery();
        con.Close();
        if (r > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Record Updated...!'); </script> ");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Record Not Updated...!'); </script> ");
        }
    }
    //void Get_Bank()
    //{
    //    string qry = "SELECT [BankID],[BankName],[Status] FROM [BankMaster] order by [BankName]";
    //    da = new SqlDataAdapter(qry, con);
    //    ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlBank.DataSource = ds.Tables[0];
    //        ddlBank.DataTextField = "BankName";
    //        ddlBank.DataValueField = "BankID";
    //        ddlBank.DataBind();
    //        ddlBank.Items.Insert(0, "--Select--");
    //    }
    //    else
    //    {
    //        ddlBank.Items.Insert(0, "--Select--");
    //    }
    //}
    protected void ddlcropyr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcropyr.SelectedItem.Text != "--Select--" && ddlRegType.SelectedItem.Text == "--Select--")
        {
            ddlRegType.Enabled = true;
        }
        else if (ddlRegType.SelectedItem.Text != "--Select--")
        {
            ddlRegType.ClearSelection();
            trHiredGodownPayment.Visible = false;
            trHiredGodownRegister.Visible = false;
            trReservationRegister.Visible = false;
        }
    }
    protected void txtbTo_TextChanged(object sender, EventArgs e)
    {
        if (ddlgodown.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown...'); </script> ");
        }
        else if (txtbFrom.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
        }
        else if (txtbTo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
        }
        else if (Convert.ToDateTime(getDate_MDY(txtbFrom.Text)) > Convert.ToDateTime(getDate_MDY(txtbTo.Text)))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Date(To Date should be grater then From date)...'); </script> ");
        }
        else
        {
            GetHiredGodownBillAmount();
        }
    }
    public void GetHiredGodownBillAmount()
    {
        decimal Claimed_Amt = 0;
        qry = "select Sub_Amount,Bill_Number from tbl_Storage_Bill_Details where Bill_Type='HG' and From_Date='" + getDate_MDY(txtbFrom.Text) + "' and To_Date='" + getDate_MDY(txtbTo.Text) + "' and Godown_Id='"+ ddlgodown.SelectedValue.ToString() +"'";
        da = new SqlDataAdapter(qry, con);
        ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Bill Amount Found for this Date...'); </script> ");
            txtCAmt.Text = Claimed_Amt.ToString();
        }
        else
        {
            
            Claimed_Amt = Convert.ToDecimal(ds.Tables[0].Rows[0]["Sub_Amount"]);
            txtCAmt.Text = Claimed_Amt.ToString();
            ViewState["Bill_Number"] = ds.Tables[0].Rows[0]["Bill_Number"].ToString();
        }
    }
   
    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlOPType.Items.Clear();
        if (ddlPayType.SelectedValue.ToString() == "CD")
        {
            ddlOPType.Items.Add(new ListItem("Cheque/Draft", "CHEQE"));
            ddlOPType.Enabled = false;
        }
        else if (ddlPayType.SelectedValue.ToString() == "CP")
        {
            ddlOPType.Items.Add(new ListItem("CASH", "CASH"));
            ddlOPType.Enabled = false;
        }
        else if (ddlPayType.SelectedValue.ToString() == "OP")
        {
            ddlOPType.Enabled = true;
            ddlOPType.Items.Add(new ListItem("--Select--", "0"));
            ddlOPType.Items.Add(new ListItem("RTGS", "RTGS"));
            ddlOPType.Items.Add(new ListItem("NEFT", "NEFT"));
        }
        else
        {
            ddlOPType.ClearSelection();
            ddlOPType.Enabled = false;
        }
           
    }
    protected void btnPSubmit_Click(object sender, EventArgs e)
    {
        if (txtbFrom.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Bill From Date...!'); </script> ");
        }
        else if (txtbTo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Bill From Date....!'); </script> ");
        }
        else if (txtCAmt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Rent Claimed Amount...!'); </script> ");
        }
        else if (txtPAmt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Rent Pass Amount...!'); </script> ");
        }
        else if (txtNetAmt.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Rent Net Amount...!'); </script> ");
        }
        else if (ddlPayType.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Payment Type...!'); </script> ");
        }
        else if (txtRefNo.Text=="")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter RTGS/NEFT/CHEQUE/DD No....!'); </script> ");
        }
        else if (txtPDate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Payment Date...!'); </script> ");
        }
        else if (txtAdvNo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Advise No...!'); </script> ");
        }
        else if (txtAdvDate.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Advise Date...!'); </script> ");
        }
        else
        {
             GetGPaymentID();
            Insert_Rent_Payment_Detail();
        }
    }
    public void Insert_Rent_Payment_Detail()
    {
        int r = 0;
        Client_Ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        //qry = "insert into tbl_Godown_Rent_Payment_Detail(GPID,Godown_ID,From_Date,To_Date,Amt_Claimed,Amt_Passed,TDS_Amt,SD_Amt,Resource_Amt,Other_Amt,Net_Amt,Pay_Mode,Pay_Sub_Mode,Pay_Ref_No,Payment_Date,Financial_Year,RO_Adv_ONo,RO_Adv_ODate,Created_Date,Client_IP,PID) values ('" + GPID + "','" + ddlgodown.SelectedValue.ToString() + "','" + getDate_MDY(txtbFrom.Text) + "','" + getDate_MDY(txtbTo.Text) + "',"+ txtCAmt.Text +","+ txtPAmt.Text +","+ txtTDS.Text +","+ txtSD.Text +","+ txtResources.Text +","+ txtOther.Text +","+ txtNetAmt.Text +",'"+ ddlPayType.SelectedValue.ToString() +"','"+ ddlOPType.SelectedValue.ToString() +"','"+ txtRefNo.Text +"','"+ getDate_MDY(txtPDate.Text) +"','"+ ddlcropyr.SelectedItem.Text +"','"+ txtAdvNo.Text +"','"+ getDate_MDY(txtAdvDate.Text) +"',getdate(),'"+ Client_Ip +"','"+ RId +"')";
        qry = "insert into tbl_Godown_Rent_Payment_Detail(GPID,Godown_ID,From_Date,To_Date,Amt_Claimed,Amt_Passed,TDS_Amt,SD_Amt,Resource_Amt,Other_Amt,Net_Amt,Pay_Mode,Pay_Sub_Mode,Pay_Ref_No,Payment_Date,Financial_Year,RO_Adv_ONo,RO_Adv_ODate,Created_Date,Client_IP,PID,Bill_No) values ('" + GPID + "','" + ddlgodown.SelectedValue.ToString() + "','" + getDate_MDY(txtbFrom.Text) + "','" + getDate_MDY(txtbTo.Text) + "'," + txtCAmt.Text + "," + txtPAmt.Text + "," + txtTDS.Text + "," + txtSD.Text + "," + txtResources.Text + "," + txtOther.Text + "," + txtNetAmt.Text + ",'" + ddlPayType.SelectedValue.ToString() + "','" + ddlOPType.SelectedValue.ToString() + "','" + txtRefNo.Text + "','" + getDate_MDY(txtPDate.Text) + "','" + ddlcropyr.SelectedItem.Text + "','" + txtAdvNo.Text + "','" + getDate_MDY(txtAdvDate.Text) + "',getdate(),'" + Client_Ip + "','" + RId + "','" + ViewState["Bill_Number"] + "')";
        cmd.CommandText = qry;
        cmd.Connection = con;
        con.Open();
        r = cmd.ExecuteNonQuery();
        con.Close();
        if (r > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Record Save Successfully...!'); </script> ");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some error has occured...!'); </script> ");
        }
    }

    protected void txtbFrom_TextChanged(object sender, EventArgs e)
    {
        if (ddlgodown.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown...'); </script> ");
        }
        else if (txtbFrom.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select From Date...'); </script> ");
        }
        else if (txtbTo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select To Date...'); </script> ");
        }
        else if (Convert.ToDateTime(getDate_MDY(txtbFrom.Text)) > Convert.ToDateTime(getDate_MDY(txtbTo.Text)))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Invalid Date(To Date should be grater then From date)...'); </script> ");
        }
        else
        {
            GetHiredGodownBillAmount();
        }
    }
}

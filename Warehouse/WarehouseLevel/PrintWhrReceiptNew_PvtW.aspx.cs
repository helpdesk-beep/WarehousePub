using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class WarehouseLevel_PrintWhrReceiptNew_PvtW : System.Web.UI.Page
{
    public SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    String District = "";
    String Depot = "";
    string Language = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //to aboide multiple click
            string var = ClientScript.GetPostBackEventReference(btnprint, "").ToString();
            btnprint.Attributes.Add("onClick", "javascript :if ( Page_ClientValidate() ){this.disabled=true; this.value='Please Wait...';" + var + "};");
            fillwhrList();
            Panel1.Visible = false;
            btnprint.Visible = false;
            btnbmpassword.Enabled = false;

        }
        Printcurrentdate();
        ListView2.DataSource = null;
        ListView2.DataBind();
        lvoffice.DataSource = null;
        lvoffice.DataBind();
    }
    private void fillwhrList()
    {
        if (Session["Depot_DistID"] != null)
        {
            if (Session["BranchType"].ToString() == "G" && Session["GodownID_New"].ToString() != null)
            {
                District = Session["Depot_DistID"].ToString();
                Depot = Session["G_DepotID"].ToString();
                //string query = "select distinct Depositor_whr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.BranchID='" + Session["G_BranchId"] + "' order by tbl_storage_Depositor_WHR_Relation.WHR_Issue_Date desc";
                //string query = "select distinct Depositor_whr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.BranchID='" + Session["G_BranchId"] + "' and tbl_storage_Depositor_WHR_Relation.GodownID='" + Session["GodownID_New"] + "' order by tbl_storage_Depositor_WHR_Relation.WHR_Issue_Date desc";
                
                // Previous Update
               // string query = "select distinct Depositor_whr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.BranchID='" + Session["G_BranchId"] + "' and tbl_storage_Depositor_WHR_Relation.GodownID='" + Session["GodownID_New"] + "' and Gid is not null order by tbl_storage_Depositor_WHR_Relation.WHR_Issue_Date desc";
                // Update on 06/11/2018
                //string query = "select distinct Depositor_whr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.BranchID='" + Session["G_BranchId"] + "' and tbl_storage_Depositor_WHR_Relation.GodownID='" + Session["GodownID_New"] + "' and Gid is not null and tbl_storage_Depositor_WHR_Relation.Depositor_whr_id not in ( select distinct WHR_Id from tbl_Storage_Receipt_Details as RD inner join Receive_Proc_Kharif2018 as RPK on RPK.StorageReceipt_Id=RD.StorageReceipt_Id where RD.Branchid='" + Session["G_BranchID"].ToString() + "' )  order by tbl_storage_Depositor_WHR_Relation.WHR_Issue_Date desc";
                string query = "select distinct Depositor_whr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.BranchID='" + Session["G_BranchId"] + "' and tbl_storage_Depositor_WHR_Relation.GodownID='" + Session["GodownID_New"] + "' and Gid is not null  order by tbl_storage_Depositor_WHR_Relation.WHR_Issue_Date desc";
                
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDLwhr.DataSource = ds.Tables[0];
                    DDLwhr.DataTextField = "Depositor_whr_id";
                    DDLwhr.DataValueField = "Depositor_whr_id";
                    DDLwhr.DataBind();
                    DDLwhr.Items.Insert(0, "--Select--");
                }
                else
                {
                    DDLwhr.Items.Insert(0, "--Select--");
                }
            }
            else
            {
                District = Session["Depot_DistID"].ToString();
                Depot = Session["G_DepotID"].ToString();
                string query = "select distinct Depositor_whr_id,WHR_Issue_Date from tbl_storage_Depositor_WHR_Relation where tbl_storage_Depositor_WHR_Relation.District_id ='" + District + "' and tbl_storage_Depositor_WHR_Relation.BranchID='" + Session["G_BranchId"] + "' order by tbl_storage_Depositor_WHR_Relation.WHR_Issue_Date desc";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DDLwhr.DataSource = ds.Tables[0];
                    DDLwhr.DataTextField = "Depositor_whr_id";
                    DDLwhr.DataValueField = "Depositor_whr_id";
                    DDLwhr.DataBind();
                    DDLwhr.Items.Insert(0, "--Select--");
                }
                else
                {
                    DDLwhr.Items.Insert(0, "--Select--");
                }
            }
        }
    }

    protected void Printcurrentdate()
    {
        try
        {
            string query = "SELECT  getdate() as 'Date1'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbldatetime.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
                lblcuurentdateoff.Text = ds.Tables[0].Rows[0]["Date1"].ToString();
            }
        }
        catch (Exception ex)
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('" + "Some error has occurred , try again!" + "'); </script> ");
        }
    }
    private void whrdetail()
    {
        if (Session["Depot_DistID"] != null)
        {
            try
            {
                string query = "";
                string whr = "";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                if (DDLwhr.SelectedIndex != 0)
                {
                    whr = DDLwhr.SelectedItem.Text;
                }
                else
                {
                    whr = TextBox1.Text;
                }
                District = Session["Depot_DistID"].ToString();
                Depot = Session["G_DepotID"].ToString();
                string BranchId = Session["G_BranchId"].ToString();


                //query = "SELECT DISTINCT WHR.Depositor_WHR_Id,WHR.Commodity_Id,('(' +CONVERT(NVARCHAR(50),(convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.Total_Qty_Received))) + ')'+ dbo.[AnkNumberToWords](convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.Total_Qty_Received))) as MktValue_of_Commodity,convert(decimal(18,2),WHR.MktValue_of_Commodity) as MktValue_of_Commodityno, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, WHR.Depositor_Name, LicenseNo,LicenseDate ,tbl_MetaData_DEPOT.DepotName, CONVERT(NVARCHAR(10), WHR.Date_of_Deposit, 103) AS Depositdate,CONVERT(NVARCHAR(10), WHR.WHR_Issue_Date, 103) AS whrDate ,CONVERT(NVARCHAR(10), WHR.SangrahadDate, 103) AS StorageRateDate,('Moisture%'+CONVERT(NVARCHAR(250),((convert(decimal(18,2),WHR.AvgMoisture_Content)+convert(decimal(18,2),WHR.AvgMoisture_Content_To))/2))+' '+WHR.Remark) as Remark ,WHR.TotalBags_Received,  convert(decimal(18,4),WHR.Total_Qty_Received) as Total_Qty_Received, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name FROM   tbl_storage_Depositor_WHR_Relation AS WHR INNER JOIN tbl_storage_Stacking_Details ON WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON WHR.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_GODOWN ON tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_DEPOT ON WHR.Depotid = tbl_MetaData_DEPOT.DepotID WHERE (WHR.Depositor_WHR_Id = '" + whr + "' and WHR.BranchID='" + BranchId + "'  )";
                query = "SELECT DISTINCT WHR.Depositor_WHR_Id,WHR.Commodity_Id,('(' +CONVERT(NVARCHAR(50),(convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.Total_Qty_Received))) + ')'+ dbo.[AnkNumberToWords](convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.Total_Qty_Received))) as MktValue_of_Commodity,convert(decimal(18,2),WHR.MktValue_of_Commodity) as MktValue_of_Commodityno, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, WHR.Depositor_Name, LicenseNo,LicenseDate ,tbl_MetaData_DEPOT.DepotName, CONVERT(NVARCHAR(10), WHR.Date_of_Deposit, 103) AS Depositdate,CONVERT(NVARCHAR(10), WHR.WHR_Issue_Date, 103) AS whrDate ,CONVERT(NVARCHAR(10), WHR.SangrahadDate, 103) AS StorageRateDate,('Moisture%'+CONVERT(NVARCHAR(250),((convert(decimal(18,2),WHR.AvgMoisture_Content)+convert(decimal(18,2),WHR.AvgMoisture_Content_To))/2))+' '+WHR.Remark) as Remark ,WHR.TotalBags_Received,  convert(decimal(18,4),WHR.Total_Qty_Received) as Total_Qty_Received, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name FROM   tbl_storage_Depositor_WHR_Relation AS WHR INNER JOIN tbl_storage_Stacking_Details ON WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON WHR.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_GODOWN ON tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_DEPOT ON WHR.BranchID = tbl_MetaData_DEPOT.BranchID WHERE (WHR.Depositor_WHR_Id = '" + whr + "' and WHR.BranchID='" + BranchId + "'  )";

                cmd = new SqlCommand(query, Con);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Commodity_Id"].ToString() == "25" || ds.Tables[0].Rows[0]["Commodity_Id"].ToString() == "29" || ds.Tables[0].Rows[0]["Commodity_Id"].ToString() == "96" || ds.Tables[0].Rows[0]["Commodity_Id"].ToString() == "97")
                    {
                        query = "SELECT DISTINCT WHR.Depositor_WHR_Id,WHR.Commodity_Id,('(' +CONVERT(NVARCHAR(50),(convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.TotalBags_Received))) + ')'+ dbo.[AnkNumberToWords](convert(decimal(18,2),WHR.MktValue_of_Commodity)*convert(decimal(18,2),WHR.TotalBags_Received))) as MktValue_of_Commodity,convert(decimal(18,2),WHR.MktValue_of_Commodity) as MktValue_of_Commodityno, tbl_MetaData_STORAGE_COMMODITY.Commodity_Name, WHR.Depositor_Name, LicenseNo,LicenseDate ,tbl_MetaData_DEPOT.DepotName, CONVERT(NVARCHAR(10), WHR.Date_of_Deposit, 103) AS Depositdate,CONVERT(NVARCHAR(10), WHR.WHR_Issue_Date, 103) AS whrDate ,CONVERT(NVARCHAR(10), WHR.SangrahadDate, 103) AS StorageRateDate,('Moisture%'+CONVERT(NVARCHAR(250),((convert(decimal(18,2),WHR.AvgMoisture_Content)+convert(decimal(18,2),WHR.AvgMoisture_Content_To))/2))+' '+WHR.Remark) as Remark ,WHR.TotalBags_Received,  convert(decimal(18,4),WHR.Total_Qty_Received) as Total_Qty_Received, tbl_MetaData_GODOWN.Godown_Name, tbl_MetaData_STORAGE_CATEGORY.Category_Name FROM   tbl_storage_Depositor_WHR_Relation AS WHR INNER JOIN tbl_storage_Stacking_Details ON WHR.Depositor_WHR_Id = tbl_storage_Stacking_Details.WHRId INNER JOIN tbl_MetaData_STORAGE_CATEGORY ON WHR.Category_Id = tbl_MetaData_STORAGE_CATEGORY.Category_Id INNER JOIN tbl_MetaData_STORAGE_COMMODITY ON WHR.Commodity_Id = tbl_MetaData_STORAGE_COMMODITY.Commodity_Id INNER JOIN tbl_MetaData_GODOWN ON tbl_storage_Stacking_Details.Godown_ID = tbl_MetaData_GODOWN.Godown_ID INNER JOIN tbl_MetaData_DEPOT ON WHR.Depotid = tbl_MetaData_DEPOT.DepotID WHERE (WHR.Depositor_WHR_Id = '" + whr + "' and WHR.BranchID='" + BranchId + "'  )";
                        cmd = new SqlCommand(query, Con);
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Panel1.Visible = true;
                            //  btnbmpassword.Enabled = true;
                            //  lblwhr.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();
                            lblwhrno.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();
                            lblwhrNooff.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();

                            //branch Name
                            string qry1 = "select  Hired_Type,Godown_ID,BranchId from [tbl_MetaData_GODOWN] where Godown_ID= (select max(Godown_ID) from  [tbl_storage_Stacking_Details] where WHRId='" + whr + "')";
                            SqlCommand cmd1 = new SqlCommand(qry1, Con);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                string storagetype = ds1.Tables[0].Rows[0][0].ToString();
                                if (storagetype == "OtherAgency")
                                {
                                    lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                                    lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                                }


                            }
                            // lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                            //lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();


                            // lbldatetime.Text = DateTime.Now.Date.ToString();
                            //lbldepositorname1.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();


                            lbldatefrom.Text = ds.Tables[0].Rows[0]["StorageRateDate"].ToString();
                            lblstordateoff.Text = ds.Tables[0].Rows[0]["StorageRateDate"].ToString();

                            lbldepositor2.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();
                            lbldepositopoffice.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();

                            lblgodownname.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                            lblgodownoffice.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();

                            lblwhrdateoffice.Text = ds.Tables[0].Rows[0]["whrDate"].ToString();
                            lbldate3.Text = ds.Tables[0].Rows[0]["whrDate"].ToString();

                            string ddd = "https://chart.googleapis.com/chart?chs=100x100&amp;cht=qr&amp;chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                            Image3.ImageUrl = "https://chart.googleapis.com/chart?chs=100x100&cht=qr&chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                            Image4.ImageUrl = "https://chart.googleapis.com/chart?chs=100x100&cht=qr&chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                            ListView1.DataSource = ds.Tables[0];

                            ListView1.DataBind();

                            lvofficem.DataSource = ds.Tables[0];
                            lvofficem.DataBind();
                            // WHR.Style.Add("background-image", "../../images/whrback.png");


                        }

                    }
                    else
                    {
                        Panel1.Visible = true;
                        //  btnbmpassword.Enabled = true;
                        //  lblwhr.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();
                        lblwhrno.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();
                        lblwhrNooff.Text = ds.Tables[0].Rows[0]["Depositor_WHR_Id"].ToString();

                        //branch Name
                        string qry1 = "select  Hired_Type,Godown_ID,BranchId from [tbl_MetaData_GODOWN] where Godown_ID= (select max(Godown_ID) from  [tbl_storage_Stacking_Details] where WHRId='" + whr + "')";
                        SqlCommand cmd1 = new SqlCommand(qry1, Con);
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            string storagetype = ds1.Tables[0].Rows[0][0].ToString();
                            if (storagetype == "OtherAgency")
                            {
                                lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                                lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                            }


                        }
                        // lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                        //lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();


                        // lbldatetime.Text = DateTime.Now.Date.ToString();
                        //lbldepositorname1.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();


                        lbldatefrom.Text = ds.Tables[0].Rows[0]["StorageRateDate"].ToString();
                        lblstordateoff.Text = ds.Tables[0].Rows[0]["StorageRateDate"].ToString();

                        lbldepositor2.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();
                        lbldepositopoffice.Text = ds.Tables[0].Rows[0]["Depositor_Name"].ToString();

                        lblgodownname.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                        lblgodownoffice.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();

                        lblwhrdateoffice.Text = ds.Tables[0].Rows[0]["whrDate"].ToString();
                        lbldate3.Text = ds.Tables[0].Rows[0]["whrDate"].ToString();

                        string ddd = "https://chart.googleapis.com/chart?chs=100x100&amp;cht=qr&amp;chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                        Image3.ImageUrl = "https://chart.googleapis.com/chart?chs=100x100&cht=qr&chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                        Image4.ImageUrl = "https://chart.googleapis.com/chart?chs=100x100&cht=qr&chl=WHR No:'" + lblwhrno.Text + "'Commodity: '" + ds.Tables[0].Rows[0]["Commodity_Name"].ToString() + "' WHR Date: '" + lbldate3.Text + "'Depositor:'" + lbldepositor2.Text + "'Godown:'" + lblgodownname.Text + "'WHR Qty:'" + ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString() + "' ";
                        ListView1.DataSource = ds.Tables[0];

                        ListView1.DataBind();

                        lvofficem.DataSource = ds.Tables[0];
                        lvofficem.DataBind();
                        // WHR.Style.Add("background-image", "../../images/whrback.png");

                    }

                }

                else
                {
                    Panel1.Visible = false;

                    btnprint.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No record found...')", true);
                }
                string query2 = "SELECT [Depositor_Type] from [tbl_MetaData_DEPOSITOR] WHERE ([Depositor_Name] = '" + lbldepositor2.Text + "')";
                SqlCommand cmd2 = new SqlCommand(query2, Con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    string depositorType = ds2.Tables[0].Rows[0]["Depositor_Type"].ToString();
                    if (depositorType == "Institution")
                    {
                        lblinstType.Text = "अपरक्राम्य";
                        instypeEng.Text = "Not Negotiable";
                        lblnegohoff.Text = "अपरक्राम्य";
                        lblnegooff.Text = "Not Negotiable";

                    }
                    else
                    {
                        lblinstType.Text = "परक्राम्य";
                        instypeEng.Text = "Negotiable";
                        lblnegohoff.Text = "परक्राम्य";
                        lblnegooff.Text = "Negotiable";
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }
    }
    private static string NumbersToWords(int inputNumber)
    {
        int inputNo = inputNumber;

        if (inputNo == 0)
            return "Zero";

        int[] numbers = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (inputNo < 0)
        {
            sb.Append("Minus ");
            inputNo = -inputNo;
        }

        string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
        string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
        string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
        string[] words3 = { "Thousand ", "Lakh ", "Crore " };

        numbers[0] = inputNo % 1000; // units
        numbers[1] = inputNo / 1000;
        numbers[2] = inputNo / 100000;
        numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
        numbers[3] = inputNo / 10000000; // crores
        numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

        for (int i = 3; i > 0; i--)
        {
            if (numbers[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (numbers[i] == 0) continue;
            u = numbers[i] % 10; // ones
            t = numbers[i] / 10;
            h = numbers[i] / 100; // hundreds
            t = t - 10 * h; // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }
        return sb.ToString().TrimEnd();

    }
    private void qtyissuedtl()
    {
        if (Session["Depot_DistID"] != null)
        {
            District = Session["Depot_DistID"].ToString();
            Depot = Session["G_DepotID"].ToString();
            //string query = "SELECT mg.Godown_Name,ms.Stack_Name,[No_Of_Bags],[Bags_Weight],CONVERT(VARCHAR(10),ssd.[CreatedDate],103) as ID from [tbl_Delivery_Stacking_Details_GatePass] as ssd inner join tbl_MetaData_GODOWN as mg on ssd.Godown_ID=mg.Godown_ID inner join tbl_MetaData_STACK as ms on ssd.Stack_ID=ms.Stack_ID  where Depositor_WHR_Id='" + DDLwhr.SelectedItem.Text + "'";
            string whr = "";
            if (DDLwhr.SelectedIndex != 0)
            {
                whr = DDLwhr.SelectedItem.Text;
            }
            else
            {
                whr = TextBox1.Text;
            }
            // string query = "WITH cte(Godown_Name, Stack_Name, No_Of_Bags, Bags_Weight,[Bags_Weight_iswsue],loss,gain ,CreatedDate, rn) AS (SELECT mg.Godown_Name,ms.Stack_Name,[No_Of_Bags],[Bags_Weight],(WHR.Total_Qty_Received-loss+gain-[Bags_Weight]) as [Bags_Weight_iswsue],Loss,Gain,CONVERT(VARCHAR(10),sge.Issue_Date,103) as ID,Row_number()OVER(ORDER BY sge.Issue_Date) rn from [tbl_Delivery_Stacking_Details_GatePass] as ssd inner join tbl_MetaData_GODOWN as mg on ssd.Godown_ID=mg.Godown_ID inner join tbl_MetaData_STACK as ms on ssd.Stack_ID=ms.Stack_ID inner join tbl_storage_Depositor_WHR_Relation AS WHR on ssd.Depositor_WHR_Id=WHR.Depositor_WHR_Id inner join dbo.tbl_Storage_GatePass_Enrty as sge on ssd.GatePass_No=sge.GatePass_No where WHR.Depositor_WHR_Id='" + whr + "'),cte1 AS (SELECT TOP 1 Godown_Name,Stack_Name,No_Of_Bags,Bags_Weight, loss,gain,rn,CreatedDate,cast([Bags_Weight_iswsue] as numeric(18,2)) AS available_quantity FROM   cte ORDER  BY rn UNION ALL SELECT a.Godown_Name,a.Stack_Name,a.No_Of_Bags,a.[Bags_Weight],a.loss,a.gain,a.rn,a.CreatedDate, cast(b.available_quantity - a.[Bags_Weight]-a.loss+a.gain as numeric(18,2)) AS available_quantity FROM   cte a INNER JOIN cte1 b ON a.rn - 1 = b.rn)SELECT CreatedDate,Godown_Name,Stack_Name,No_Of_Bags,Bags_Weight,available_quantity FROM   cte1 ";
            // string query = "SELECT mg.Godown_Name,ms.Stack_Name,[No_Of_Bags],[Bags_Weight],WHR.Total_Qty_Received,WHR.TotalBags_Received,(WHR.Total_Qty_Received-loss+gain-[Bags_Weight]) as [Bags_Weight_iswsue],Loss,Gain,CONVERT(VARCHAR(10),sge.Issue_Date,103) as ID,Row_number()OVER(ORDER BY sge.Issue_Date) rn from [tbl_Delivery_Stacking_Details_GatePass] as ssd inner join tbl_MetaData_GODOWN as mg on ssd.Godown_ID=mg.Godown_ID inner join tbl_MetaData_STACK as ms on ssd.Stack_ID=ms.Stack_ID inner join tbl_storage_Depositor_WHR_Relation AS WHR on ssd.Depositor_WHR_Id=WHR.Depositor_WHR_Id inner join dbo.tbl_Storage_GatePass_Enrty as sge on ssd.GatePass_No=sge.GatePass_No where WHR.Depositor_WHR_Id='" + whr + "' order by sge.Issue_Date asc";
            string query = "SELECT mg.Godown_Name,ms.Stack_Name,isnull([No_Of_Bags],0)[No_Of_Bags], isnull([Bags_Weight],0)Bags_Weight,isnull(WHR.Total_Qty_Received,0) Total_Qty_Received,isnull(WHR.TotalBags_Received,0)TotalBags_Received,(WHR.Total_Qty_Received-loss+gain-[Bags_Weight]) as [Bags_Weight_iswsue],Loss,Gain,CONVERT(VARCHAR(10),sge.Issue_Date,103) as ID,Row_number()OVER(ORDER BY sge.Issue_Date) rn from [tbl_Delivery_Stacking_Details_GatePass] as ssd inner join tbl_MetaData_GODOWN as mg on ssd.Godown_ID=mg.Godown_ID inner join tbl_MetaData_STACK as ms on ssd.Stack_ID=ms.Stack_ID inner join tbl_storage_Depositor_WHR_Relation AS WHR on ssd.Depositor_WHR_Id=WHR.Depositor_WHR_Id inner join dbo.tbl_Storage_GatePass_Enrty as sge on ssd.GatePass_No=sge.GatePass_No where WHR.Depositor_WHR_Id='" + whr + "' order by sge.Issue_Date asc";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dss = new DataTable();
            dss.Columns.Add("Date", typeof(string));
            dss.Columns.Add("DelQty", typeof(string));
            dss.Columns.Add("AvlQty", typeof(string));
            dss.Columns.Add("DelBags", typeof(string));
            dss.Columns.Add("AvlBags", typeof(string));
            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal avilableQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Qty_Received"].ToString());
                int avilableBags = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalBags_Received"].ToString()); ;
                DDLwhr.DataSource = ds.Tables[0];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    avilableQty = avilableQty - Convert.ToDecimal(ds.Tables[0].Rows[i]["Bags_Weight"].ToString()) - Convert.ToDecimal(ds.Tables[0].Rows[i]["Loss"].ToString()) + Convert.ToDecimal(ds.Tables[0].Rows[i]["Gain"].ToString());
                    avilableBags = avilableBags - Convert.ToInt32(ds.Tables[0].Rows[i]["No_Of_Bags"].ToString());


                    dss.Rows.Add(ds.Tables[0].Rows[i]["ID"].ToString(), ds.Tables[0].Rows[i]["Bags_Weight"].ToString(), avilableQty.ToString(), ds.Tables[0].Rows[i]["No_Of_Bags"].ToString(), avilableBags);


                }


                //foreach (ListViewDataItem di in ListView2.Items)
                //{


                //}
                ListView2.DataSource = dss;

                ListView2.DataBind();

                lvoffice.DataSource = dss;
                lvoffice.DataBind();

            }
            else
            {

            }
        }
    }

    //27/02/2014......
    // private void GetAvilableqty()
    //{
    //    try
    //    {
    //            if (Session["Depot_DistID"] != null)
    //    {
    //        string whr = "";
    //        if (DDLwhr.SelectedIndex != 0)
    //        {
    //            whr = DDLwhr.SelectedItem.Text;
    //        }
    //        else
    //        {
    //            whr = TextBox1.Text;
    //        }


    //        District = Session["Depot_DistID"].ToString();
    //        Depot = Session["G_DepotID"].ToString();
    //        string query = "SELECT mg.Godown_Name,ms.Stack_Name,[No_Of_Bags],[Bags_Weight],WHR.Total_Qty_Received as [Bags_Weight_iswsue],CONVERT(VARCHAR(10),ssd.[CreatedDate],103) as ID from [tbl_Delivery_Stacking_Details_GatePass] as ssd inner join tbl_MetaData_GODOWN as mg on ssd.Godown_ID=mg.Godown_ID inner join tbl_MetaData_STACK as ms on ssd.Stack_ID=ms.Stack_ID inner join tbl_storage_Depositor_WHR_Relation AS WHR on ssd.Depositor_WHR_Id=WHR.Depositor_WHR_Id where WHR.Depositor_WHR_Id='232700201914163'";
    //        SqlCommand cmd = new SqlCommand(query, Con);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {


    //            DDLwhr.DataSource = ds.Tables[0];
    //            ListView2.DataSource = ds.Tables[0];

    //            ListView2.DataBind();

    //        }
    //        else
    //        {

    //        }
    //    }

    //    }
    //    catch (Exception ex)
    //    {


    //    }


    //}
    private void GetDepotBelongs(string depotId)
    {
        try
        {
            string whr = "";
            //string BranchType = "";
            if (DDLwhr.SelectedIndex != 0)
            {
                whr = DDLwhr.SelectedItem.Text;
            }
            else
            {
                whr = TextBox1.Text;
            }
                        ////////////////////////////
                        //string qry = "select * from tbl_MetaData_GODOWN where Godown_ID='" + Session["GodownID_New"].ToString() + "' and BranchID='" + Session["G_BranchId"].ToString() + "'";
                        string qry = "select tbl_MetaData_GODOWN.*,tbl_MetaData_DEPOT.DepotName,tbl_MetaData_DEPOT.DepotAddress from tbl_MetaData_GODOWN inner join tbl_MetaData_DEPOT on tbl_MetaData_DEPOT.BranchId=tbl_MetaData_GODOWN.BranchID where tbl_MetaData_GODOWN.Godown_ID='" + Session["GodownID_New"].ToString() + "' and tbl_MetaData_GODOWN.BranchID='" + Session["G_BranchId"].ToString() + "'";
                        SqlCommand cmd = new SqlCommand(qry, Con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblcorpnamehnd.Visible = true;
                            lblcorpnamehndof.Visible = true;
                            //lbldepositorname1.Text = "MPWLC";
                            lbldepositorname1.Text = ds.Tables[0].Rows[0]["Org_Name"].ToString();
                            Label9.Text = ds.Tables[0].Rows[0]["Org_Name"].ToString();
                            //lblcorpnamehnd.Text = "c1";
                            //lblcorpnamehndof.Text = ds.Tables[0].Rows[0]["Org_Name"].ToString();
                            //lblcorpnamehndof.Text = "c2";
                            //lblcorpname
                            //Image2.Visible = true;
                            //Image1.Visible = true;
                            mpwlcAothO.Visible = false;
                            mpwlcothOf.Visible = false;

                            lblbrachname1.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();
                            lblbranchoff.Text = ds.Tables[0].Rows[0]["DepotName"].ToString();

                            //lblcorpname.Text = "(Madhya Pradesh Warehousing and Logistics Corporation)";
                            //lblcorpnameof.Text = "(Madhya Pradesh Warehousing and Logistics Corporation)";

                            lblcorpname.Text = ds.Tables[0].Rows[0]["Org_Name"].ToString();
                            lblcorpnameof.Text = ds.Tables[0].Rows[0]["Org_Name"].ToString();


                            //ddlDepoBeloggsTo.SelectedItem.Text = ds.Tables[0].Rows[0]["DepoBelongs"].ToString();
                            // txtTehsilName.Text = ds.Tables[0].Rows[0]["TehsilName"].ToString();
                            lbladdr.Text = ds.Tables[0].Rows[0]["Godown_Address"].ToString();
                            lbladdoffice.Text = ds.Tables[0].Rows[0]["Godown_Address"].ToString();
                            //  txtLocationPhoneNo.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                            // txtLocationFaxNo.Text = ds.Tables[0].Rows[0]["FaxNo"].ToString();
                            // txtLocationEMailAddress.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                            lblgodampal.Text = ds.Tables[0].Rows[0]["GInchargeName"].ToString();
                            lblgodampaloffice.Text = ds.Tables[0].Rows[0]["GInchargeName"].ToString();
                            // lbllicensedate.Text = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                            //lbllicensno.Text = ds.Tables[0].Rows[0]["LincenseNo"].ToString();


                            string licNo = ds.Tables[0].Rows[0]["LicNum"].ToString();
                            if (licNo == "")
                            {
                                lbllicensno.Text = "__________________";
                                lbllicofficeno.Text = "__________________";
                            }
                            else
                            {
                                lbllicensno.Text = ds.Tables[0].Rows[0]["LicNum"].ToString();
                                lbllicofficeno.Text = ds.Tables[0].Rows[0]["LicNum"].ToString();
                            }
                            //txtlicDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LicDate"]).ToString("dd/MM/yyyy");
                            //string LicDate = ds.Tables[0].Rows[0]["LicenceDate"].ToString();
                            string LicDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["LicDate"]).ToString("dd/MM/yyyy");
                            if (LicDate == "")
                            {
                                lbllicensedate.Text = "_________________";
                                lbllicsnceoffedate.Text = "_________________";
                            }
                            else
                            {
                                lbllicensedate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LicDate"]).ToString("dd/MM/yyyy");
                                lbllicsnceoffedate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["LicDate"]).ToString("dd/MM/yyyy");
                            }
                        }

        }
        catch (Exception)
        {
            /////////////
        }

    }

    private void checkifprinted()
    {
        if (Session["Depot_DistID"] != null)
        {
            string whr = "";
            if (DDLwhr.SelectedIndex != 0)
            {
                whr = DDLwhr.SelectedItem.Text;
            }
            else
            {
                whr = TextBox1.Text;
            }

            District = Session["Depot_DistID"].ToString();
            Depot = Session["G_DepotID"].ToString();
            string query = "SELECT PrintStatus FROM [Intergrated_MP_STORAGE].[dbo].[whrprintstatus] where WHRID='" + whr + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string whrstatus = ds.Tables[0].Rows[0]["PrintStatus"].ToString();
                if (whrstatus == "1st")
                {
                    lblcopytype.Text = "ORIGNAL COPY";
                    Table1.Visible = true;
                    lblorico.Visible = true;
                    // lbldatefrom.Text = TextBox1.Text;
                    //Label mylabel = (Label)ListView1.FindControl("Label2");
                    //mylabel.Text = TextBox2.Text;
                    WHR.Style.Add("background-image", "url('../../images/origanal.png')");
                    WHR.Style.Add("background-repeat", "repeat");
                    Table1.Style.Add("background-image", "url('../../images/officecopy.png')");
                    Table1.Style.Add("background-repeat", "repeat");
                    // imgori.ImageUrl = "../../images/origanal.png";
                    btnbmpassword.Enabled = true;
                }
                else if (whrstatus == "2nd")
                {
                    btnprint.Visible = false;
                    btnbmpassword.Enabled = false;
                    lblcopytype.Text = "PHOTO COPY";
                    Table1.Visible = false;
                    lblorico.Visible = false;
                    // WHR.Style.Add("background-image", "url('../../images/officecopy.png')");
                    //WHR.Style.Add("background-repeat", "repeat");
                    WHR.Style.Add("background-image", "url('../../images/photocopy.png')");
                    WHR.Style.Add("background-repeat", "repeat");
                    Table1.Style.Add("background-image", "url('../../images/photocopy.png')");
                    Table1.Style.Add("background-repeat", "repeat");
                }
                else if (whrstatus == "4th")
                {
                    btnprint.Visible = false;
                    btnbmpassword.Enabled = true;
                    lblcopytype.Text = "DUPLICATE COPY";
                    lblorico.Text = "DUPLICATE COPY";
                    Table1.Visible = false;
                    lblorico.Visible = false;
                    // WHR.Style.Add("background-image", "url('../../images/officecopy.png')");
                    //WHR.Style.Add("background-repeat", "repeat");
                    WHR.Style.Add("background-image", "url('../../images/dupli.png')");
                    WHR.Style.Add("background-repeat", "repeat");
                    Table1.Style.Add("background-image", "url('../../images/photocopy.png')");
                    Table1.Style.Add("background-repeat", "repeat");
                }
                else
                {
                    btnprint.Visible = false;
                    btnbmpassword.Enabled = false;
                    lblcopytype.Text = "PHOTO COPY";
                    Table1.Visible = false;
                    WHR.Style.Add("background-image", "url('../../images/photocopy.png')");
                    WHR.Style.Add("background-repeat", "repeat");
                    Table1.Style.Add("background-image", "url('../../images/photocopy.png')");
                    Table1.Style.Add("background-repeat", "repeat");
                }
            }
            else
            {

            }
        }
    }
    protected void DDLwhr_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        Panel1.Visible = true;
        ListView1.DataSource = null;

        ListView1.DataBind();
        ListView2.DataSource = null;

        ListView2.DataBind();
        lvofficem.DataSource = null;
        lvofficem.DataBind();
        lvoffice.DataSource = null;
        lvoffice.DataBind();
        btnprint.Visible = false;
        whrdetail();
        qtyissuedtl();
        checkifprinted();
        GetDepotBelongs(Session["G_DepotID"].ToString());
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {

        string whr = "";
        if (DDLwhr.SelectedIndex != 0)
        {
            whr = DDLwhr.SelectedItem.Text;
        }
        else
        {
            whr = TextBox1.Text;
        }
        string query1 = "SELECT PrintStatus FROM [Intergrated_MP_STORAGE].[dbo].[whrprintstatus] where WHRID='" + lblwhrno.Text + "'";
        SqlCommand cmd = new SqlCommand(query1, Con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string whrstatus = ds.Tables[0].Rows[0]["PrintStatus"].ToString();
            if (whrstatus == "1st")
            {
                string query = "update [whrprintstatus] set PrintStatus='2nd' where WHRID='" + whr + "'";
                Con.Open();
                SqlCommand cmd2 = new SqlCommand(query, Con);

                cmd2.ExecuteNonQuery();
                Con.Close();
                //whrdetail();
                //qtyissuedtl();
                // btnprint.Attributes.Add("OnClientClick", "PrintDiv();self.opener=null;self.close();return false;");
                Panel1.Visible = false;
                whrdetail();
                qtyissuedtl();
                checkifprinted();
                //GetDepotBelongs(Session["G_DepotID"].ToString());

            }
            else if (whrstatus == "4th")
            {
                string query = "update [whrprintstatus] set PrintStatus='2nd' where WHRID='" + whr + "'";
                Con.Open();
                SqlCommand cmd2 = new SqlCommand(query, Con);

                cmd2.ExecuteNonQuery();
                Con.Close();
                //whrdetail();
                //qtyissuedtl();
                // btnprint.Attributes.Add("OnClientClick", "PrintDiv();self.opener=null;self.close();return false;");
                Panel1.Visible = false;
                whrdetail();
                qtyissuedtl();
                checkifprinted();
                //GetDepotBelongs(Session["G_DepotID"].ToString());

            }
            else
            {
                string query = "update [whrprintstatus] set PrintStatus='3rd' where WHRID='" + whr + "'";
                Con.Open();
                SqlCommand cmd2 = new SqlCommand(query, Con);

                cmd2.ExecuteNonQuery();
                Con.Close();
                Panel1.Visible = false;
                //whrdetail();
                //qtyissuedtl();
                whrdetail();
                qtyissuedtl();
                checkifprinted();
                //GetDepotBelongs(Session["G_DepotID"].ToString());

            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //string query2 = "SELECT [BranchID],GodownTypeId,GM_pwd FROM Pvt_Warehouse_Login where BranchID='" + Session["G_BranchId"].ToString() + "' and GM_pwd='" + txtPassword.Text.Trim() + "' and Godown_Id='" + Session["GodownID_New"].ToString() + "'";
        //string query2 = "select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + TextBox1.Text.ToString() + "' and GodownID='" + Session["GodownID_New"].ToString() + "' and BranchID='" + Session["G_BranchId"].ToString() + "'";
        //Previous Update
        //string query2 = "select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + TextBox1.Text.ToString() + "' and GodownID='" + Session["GodownID_New"].ToString() + "' and Gid is not null and BranchID='" + Session["G_BranchId"].ToString() + "'";
        //Update On 06/11/2018
        string query2 = "select Depositor_WHR_Id from tbl_storage_Depositor_WHR_Relation where Depositor_WHR_Id='" + TextBox1.Text.ToString() + "' and GodownID='" + Session["GodownID_New"].ToString() + "' and Gid is not null and tbl_storage_Depositor_WHR_Relation.Depositor_whr_id not in ( select distinct WHR_Id from tbl_Storage_Receipt_Details as RD inner join Receive_Proc_Kharif2018 as RPK on RPK.StorageReceipt_Id=RD.StorageReceipt_Id where RD.Branchid='" + Session["G_BranchID"].ToString() + "' ) ";
        SqlCommand cmdd = new SqlCommand(query2, Con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmdd);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            DDLwhr.SelectedIndex = 0;
            // Panel1.Visible = true;
            ListView1.DataSource = null;

            ListView1.DataBind();
            ListView2.DataSource = null;

            ListView2.DataBind();

            lvofficem.DataSource = null;
            lvofficem.DataBind();
            lvoffice.DataSource = null;
            lvoffice.DataBind();
            btnprint.Visible = false;
            whrdetail();
            qtyissuedtl();
            checkifprinted();
            GetDepotBelongs(Session["G_DepotID"].ToString());
            //TextBox2.Text= NumbersToWords(512000012);
        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Invalid WHR NO.'); </script> ");
        }
        //////
        

    }

    protected void btnsubmitpwd_Click(object sender, EventArgs e)
    {
        //string query2 = "SELECT [BranchID],[BranchTypeID],[BranchPwd] FROM [dbo].[MetaDataBranchWithIssueCenter] where BranchID='" + Session["G_BranchId"].ToString() + "' and [BranchPwd]='" + txtPassword.Text.Trim() + "'";
        string query2 = "SELECT [BranchID],GodownTypeId,GM_pwd FROM Pvt_Warehouse_Login where BranchID='" + Session["G_BranchId"].ToString() + "' and GM_pwd='" + txtPassword.Text.Trim() + "' and Godown_Id='" + Session["GodownID_New"].ToString() + "'";
        SqlCommand cmdd = new SqlCommand(query2, Con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmdd);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {

            //  btnprint.Text = "Print";
            btnprint.Visible = true;
            btnbmpassword.Enabled = false;
            //  whrdetail();
            qtyissuedtl();

        }
        else
        {
            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Wrong Password'); </script> ");
            btnprint.Visible = false;
        }
    }
}

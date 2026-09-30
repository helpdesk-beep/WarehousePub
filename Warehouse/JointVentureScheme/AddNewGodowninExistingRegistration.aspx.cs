using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Text;

public partial class JointVentureScheme_AddNewGodowninExistingRegistration : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    DataTable Dt1 = new DataTable();
    DataTable EditStack = new DataTable();
    DataTable EditStackNonMPSCSC = new DataTable();
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();
    string Todaydate = "";
    string CheckValid = "";
    string receiptid = string.Empty;
    string gatePassid = string.Empty;
    string ArrivalStockid = string.Empty;
    string Registration_No = "";
    int BID = 0;
    SqlCommand cmd = null;
    SqlTransaction sqltran;
    string R_Phase = "";
    string Reg_season = "";
    SqlTransaction sqltrans;
    string RegionID = "";
    string TheResult = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();

        // CalendarExtender1.StartDate = DateTime.Now;
        //   CalendarExtender2.StartDate = DateTime.Now; 

        if (Session["Reg_No"] == null || Session["Reg_No"] == "")
        {
            //lbluser.Text = Session["fname"].ToString() + " " + Session["mname"].ToString() + " " + Session["lname"].ToString();
            //lblAuthPerson.Text = Session["fname"].ToString();
            //lblEmail.Text = Session["email"].ToString();
            //lblMob.Text = Session["mobile"].ToString();
            ////lblDate.Text = Session["DOB"].ToString();
            //lblAppType.Text = Session["AppType"].ToString();
            //lblDistrict.Text = Session["District"].ToString();
            R_Phase = "1";
            //Reg_season = "R2019";
            //Reg_season = "K2019";
            Reg_season = "JVS2020_21";
            if (!IsPostBack)
            {
                SetInitialRow();
            }
        }

        //if ((Session["email"] != null) && (Session["mobile"] != null))
        //{
        //    if (Session["Reg_No"] == null || Session["Reg_No"]=="")
        //    {
        //        lbluser.Text = Session["fname"].ToString() + " " + Session["mname"].ToString() + " " + Session["lname"].ToString();
        //        lblAuthPerson.Text = Session["fname"].ToString();
        //        lblEmail.Text = Session["email"].ToString();
        //        lblMob.Text = Session["mobile"].ToString();
        //        //lblDate.Text = Session["DOB"].ToString();
        //        lblAppType.Text = Session["AppType"].ToString();
        //        lblDistrict.Text = Session["District"].ToString();
        //        R_Phase = "1";
        //        //Reg_season = "R2019";
        //        //Reg_season = "K2019";
        //        Reg_season = "JVS2020_21";
        //        if (!IsPostBack)
        //        {
        //            SetInitialRow();
        //        }
        //    }
        //    else
        //    {
        //      //  ModalPopupExtender1.Show();
        //    } 
        //}
        //else
        //{

        //    Response.Redirect("UserReg.aspx");
        //}  
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
    public int CheckEmail()
    {
        int ch = 0;
        string strsql = "select distinct EmailID from tbl_WarehouseRegistration where Registration_Id='" + txtregistrationid.Text.ToString() + "'";

        SqlDataAdapter da = new SqlDataAdapter(strsql, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('आपका रजिस्ट्रेशन हो चुका है कृपया चेक करे...'); </script> ");
            //txtREgemail.Focus();
            ch = 1;
        }
        else
        {
            ch = 0;

        }
        return ch;
    }
    public string Tcheckdatetimes()
    {
        DateTime ServerDate = new DateTime();
        //Test
        //DateTime _effective_date = Convert.ToDateTime("02/18/2018 00:05:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("02/26/2018 05:00:00 PM");
        //Actual

        //DateTime _effective_date = Convert.ToDateTime("10/14/2019 11:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("10/30/2019 11:59:00 PM");

        //server
        DateTime _effective_date = Convert.ToDateTime("02/20/2021 11:59:00 AM");
        DateTime _Closing_date = Convert.ToDateTime("02/19/2028 11:59:00 PM");

        ////Local pages savan
        //DateTime _effective_date = Convert.ToDateTime("20/12/2021 11:59:00 AM");
        //DateTime _Closing_date = Convert.ToDateTime("28/01/2028 11:59:00 PM");
        ////Old
        //   DateTime _effective_date = Convert.ToDateTime("2018-01-18 12:55:05.870");
        //   DateTime _Closing_date = Convert.ToDateTime("2020-01-18 12:55:05.870");

        string S = "";
        string QueryMax = "select getdate() as CDateTime";
        cmd = new SqlCommand(QueryMax, con);
        con.Open();
        string str3 = cmd.ExecuteScalar().ToString();
        con.Close();

        if ((str3 != String.Empty) || str3 != "")
        {
            ServerDate = Convert.ToDateTime(str3);
            ///Manage Time        
            //ServerDate = ServerDate.AddMinutes(-4);
            ServerDate = ServerDate.AddMinutes(-2);
        }
        if (ServerDate < _effective_date)
        {
            S = "NS";
        }
        else if (ServerDate > _Closing_date)
        {
            S = "NE";
        }
        else
        {
            S = "Y";
        }
        return S;
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string TStatus = Tcheckdatetimes();

        if (TStatus == "Y")
        {
            //DateTime STDate = Convert.ToDateTime(getDate_MDY(txtSLDate.Text));
            //DateTime WDDate = Convert.ToDateTime(getDate_MDY(txtWDRALDate.Text));
            int SK = 0;
            SK = CheckEmail();
            if (SK == 1)
            {
                int chklic = checklicnodatevalidation();
                if (chklic == 1)
                {
                    Insert_Registration_Detail();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Check Godown Entered data...'); </script> ");
                }

            }
            else
            {
                Response.Redirect("DistrictWiseJVSOffer.aspx");
            }
        }
        else if (TStatus == "NS")
        {
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You Can't Login Before 14/06/2016 11 AM ...!'); </script> ");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('You can not Register before 20/02/2021 11:59:00 AM'); </script> ");

        }
        else if (TStatus == "NE")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration for Warehouse under Joint Venture Scheme has been closed..!'); </script> ");
        }
    }


    public void Insert_Registration_Detail()
    {
        string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
        Insert_Godown_Detail();
        GetGdwn();
        tblupdateregistration.Visible = true;
        //Insert_Registration_Detail2();
        ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Save Successfully...'); </script> ");

    }



    public void Insert_Godown_Detail()
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
        for (int j = 0; j < gvGodown.Rows.Count; j++)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (((CheckBox)gvGodown.Rows[j].FindControl("ckstack")).Checked == true)
            {
                TextBox Lenghts = (TextBox)gvGodown.Rows[j].Cells[1].FindControl("txtLenght");
                TextBox Widths = (TextBox)gvGodown.Rows[j].Cells[2].FindControl("txtWidth");
                TextBox Heights = (TextBox)gvGodown.Rows[j].Cells[3].FindControl("txtHeight");
                TextBox Capacitys = (TextBox)gvGodown.Rows[j].Cells[5].FindControl("txtCapacity");
                TextBox ConstructionYears = (TextBox)gvGodown.Rows[j].Cells[6].FindControl("txtConstY");
                String qryGd = "select top 1 Godown_ID,Godown_No from tbl_WarehouseGodown_Reg where Registration_Id='" + txtregistrationid.Text.Trim() + "' order by Godown_No desc";
                SqlDataAdapter da = new SqlDataAdapter(qryGd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["Godownid"] = ds.Tables[0].Rows[0]["Godown_ID"].ToString().Trim();
                    Session["Godownno"] = ds.Tables[0].Rows[0]["Godown_No"].ToString().Trim();
                }
                Int64 newGodownno = 0;
                Int64 newGodownID = 0;
                int GodownNo = Convert.ToInt32(gvGodown.Rows[j].Cells[0].Text.ToString());
                int GodownNNo = Convert.ToInt32(Session["Godownno"]);
                Int64 GodownNID = Convert.ToInt64(Session["Godownid"]);
                decimal Length = Convert.ToDecimal(Lenghts.Text);
                decimal Width = Convert.ToDecimal(Widths.Text);
                decimal Height = Convert.ToDecimal(Heights.Text);
                decimal Capacity = Convert.ToDecimal(Capacitys.Text);
                string ConstructionY = ConstructionYears.Text;
                string GodownId = Registration_No + (j + 1);
                //Session["newGodownno"] = GodownNNo + GodownNo;
                newGodownID = GodownNNo + GodownNo;
                newGodownno = GodownNID + 1;
                //string qryGd = "INSERT INTO [tbl_WarehouseGodown_Reg] ([Registration_Id],[Godown_ID],[DistrictId],[BranchId],[Registration_Date],[Godown_Name],[Godown_No],[G_Length],[G_Width],[G_Height],[G_ScientificCapacity],[G_MaxCapacity],[G_ConstructedYear],[CreatedBy],[CreatedDate],[IsActive],[AId],LicNo,LicType,LicIssueDate,LicValidityDate) VALUES ('" + Registration_No + "','" + GodownId + "','" + ddlWarDistrict.SelectedValue + "','" + ddlBranch.SelectedValue + "',GETDATE(),'" + txtWarehouseName.Value + "','" + GodownNo + "','" + Length + "','" + Width + "','" + Height + "','" + Capacity + "'," + Convert.ToDecimal(0) + ",'" + ConstructionY + "','" + ClientIP + "',GETDATE(),'Y','" + Convert.ToDecimal(0) + "','" + ((TextBox)gvGodown.Rows[j].FindControl("Gtxtlicno")).Text.ToString().Trim() + "','" + ((DropDownList)gvGodown.Rows[j].FindControl("ddlgdwntype")).SelectedValue.ToString() + "','" + getDate_MDY(((TextBox)gvGodown.Rows[j].FindControl("GtxtLicIssuedate")).Text.ToString().Trim()) + "' ,'" + getDate_MDY(((TextBox)gvGodown.Rows[j].FindControl("GtxtLicExpdate")).Text.ToString().Trim()) + "')";
                //string qryGd = "";
                cmd = new SqlCommand("Insert_Add_Godown_in_Registraion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegID", txtregistrationid.Text.Trim());
                cmd.Parameters.AddWithValue("@Godown_ID", newGodownno);
                cmd.Parameters.AddWithValue("@GodownNo", newGodownID);
                cmd.Parameters.AddWithValue("@G_Length", Length);
                cmd.Parameters.AddWithValue("@G_Width", Width);
                cmd.Parameters.AddWithValue("@G_Height", Height);
                cmd.Parameters.AddWithValue("@G_ScientificCapacity", Capacity);
                cmd.Parameters.AddWithValue("@G_ConstructedYear", ConstructionY);
                cmd.Parameters.AddWithValue("@CreatedBy", ClientIP);
                cmd.Parameters.AddWithValue("@LICType", ((DropDownList)gvGodown.Rows[j].FindControl("ddlgdwntype")).SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@LicNo", ((TextBox)gvGodown.Rows[j].FindControl("Gtxtlicno")).Text.ToString().Trim());
                //cmd.Parameters.AddWithValue("@LicIssueDate", getDate_MDY(((TextBox)gvGodown.Rows[j].FindControl("GtxtLicIssuedate")).Text.ToString().Trim()));
                //cmd.Parameters.AddWithValue("@LicValidityDate", getDate_MDY(((TextBox)gvGodown.Rows[j].FindControl("GtxtLicExpdate")).Text.ToString().Trim()));

                int c = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                //Registration_No = Registration_No;
                if (c > 0)
                {

                }
            }
        }

    }


    protected void FileUploadComplete(object sender, EventArgs e)
    {
        //string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
        //AsyncFileUpload1.SaveAs(Server.MapPath(this.UploadFolderPath) + filename);
    }
    protected string getDate_MDY(string inDate)
    {
        if (inDate == "" || inDate == null)
        {
            return "01/01/2000";
        }
        else
        {
            //string sd = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            string converted = "";
            string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy", "dd-MMM-yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy", "dd-MM-yy", "yyyy/MM/dd", "MM/dd/yyyy" };
            converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM/dd/yyyy");
            // converted = DateTime.ParseExact(inDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd");
            return converted;
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    private void AddNewRowToGrid()
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                drCurrentRow["Lenght"] = 0;

                //drCurrentRow["RptAmt"] = 0;
                drCurrentRow["Width"] = 0;
                drCurrentRow["Height"] = 0;
                drCurrentRow["Capacity"] = 0;
                drCurrentRow["ConstY"] = 0;

                drCurrentRow["LNo"] = "";
                drCurrentRow["LIssueDate"] = "";
                drCurrentRow["LExpDate"] = "";

                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["CurrentTable"] = dtCurrentTable;

                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    //if (((CheckBox)gvGodown.Rows[i].FindControl("ckstack")).Checked == true)
                    //{
                    //extract the DropDownList Selected Items
                    //DropDownList ddl1 = (DropDownList)gvImprest.Rows[i].Cells[1].FindControl("DropDownList1");
                    TextBox Lenght = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtLenght");
                    TextBox Width = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtWidth");
                    TextBox Height = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtHeight");
                    TextBox Capacity = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtCapacity");
                    TextBox ConstY = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("txtConstY");

                    DropDownList ddlgdwntype = (DropDownList)gvGodown.Rows[i].Cells[1].FindControl("ddlgdwntype");
                    TextBox LNo = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("Gtxtlicno");
                    TextBox LIssueDate = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("GtxtLicIssuedate");
                    TextBox LExpDate = (TextBox)gvGodown.Rows[i].Cells[1].FindControl("GtxtLicExpdate");


                    //dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                    //dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["Lenght"] = Convert.ToDecimal(Lenght.Text);
                    dtCurrentTable.Rows[i]["Width"] = Convert.ToDecimal(Width.Text);
                    dtCurrentTable.Rows[i]["Height"] = Convert.ToDecimal(Height.Text);
                    dtCurrentTable.Rows[i]["Capacity"] = Convert.ToDecimal(Capacity.Text);
                    dtCurrentTable.Rows[i]["ConstY"] = Convert.ToDecimal(ConstY.Text);


                    dtCurrentTable.Rows[i]["ddlgdwntype"] = ddlgdwntype.SelectedItem.Text;
                    dtCurrentTable.Rows[i]["LNo"] = LNo.Text;
                    dtCurrentTable.Rows[i]["LIssueDate"] = LIssueDate.Text;
                    dtCurrentTable.Rows[i]["LExpDate"] = LExpDate.Text;


                }

                //Rebind the Grid with the current data
                gvGodown.DataSource = dtCurrentTable;
                gvGodown.DataBind();

                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    if (Convert.ToDecimal(((TextBox)gvGodown.Rows[i].FindControl("txtCapacity")).Text) != 0)
                    {
                        ((CheckBox)gvGodown.Rows[i].FindControl("ckstack")).Checked = true;
                        ((TextBox)gvGodown.Rows[i].FindControl("txtLenght")).Enabled = false;
                        ((TextBox)gvGodown.Rows[i].FindControl("txtWidth")).Enabled = false;
                        ((TextBox)gvGodown.Rows[i].FindControl("txtHeight")).Enabled = false;
                    }

                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Set the Previous Selected Items on Each DropDownList on Postbacks
                    //DropDownList ddl1 = (DropDownList)gvGodown.Rows[rowIndex].Cells[1].FindControl("DropDownList1");
                    DropDownList ddl1 = (DropDownList)gvGodown.Rows[rowIndex].Cells[1].FindControl("ddlgdwntype");
                    //Fill the DropDownList with Data
                    FillDropDownList(ddl1);
                    if (i < dt.Rows.Count - 1)
                    {
                        ddl1.ClearSelection();
                        ddl1.Items.FindByText(dt.Rows[i]["ddlgdwntype"].ToString()).Selected = true;
                    }

                    rowIndex++;
                }
            }
        }
    }
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;


        //Define the Columns
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Lenght", typeof(string)));
        dt.Columns.Add(new DataColumn("Width", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Height", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Capacity", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ConstY", typeof(decimal)));

        // add Licence Data

        dt.Columns.Add(new DataColumn("ddlgdwntype", typeof(string)));
        dt.Columns.Add(new DataColumn("LNo", typeof(string)));
        dt.Columns.Add(new DataColumn("LIssueDate", typeof(string)));
        dt.Columns.Add(new DataColumn("LExpDate", typeof(string)));


        // End Licence Data


        //Add a Dummy Data on Initial Load
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Lenght"] = 0;
        dr["Width"] = 0;
        dr["Height"] = 0;
        dr["Capacity"] = 0;
        dr["ConstY"] = 0;


        //
        dr["ddlgdwntype"] = "";
        dr["LNo"] = "";
        dr["LIssueDate"] = "";
        dr["LExpDate"] = "";
        //

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;
        //Bind the DataTable to the Grid
        gvGodown.DataSource = dt;
        gvGodown.DataBind();

        //Extract and Fill the DropDownList with Data
        DropDownList ddl1 = (DropDownList)gvGodown.Rows[0].Cells[1].FindControl("ddlgdwntype");
        FillDropDownList(ddl1);
    }
    private void FillDropDownList(DropDownList ddlgdwntype)
    {
        ArrayList arr = GetDummyData();
        foreach (ListItem item in arr)
        {
            ddlgdwntype.Items.Add(item);
        }
    }
    private ArrayList GetDummyData()
    {
        ArrayList arr = new ArrayList();
        arr.Add(new ListItem("WDRA", "68"));
        arr.Add(new ListItem("NON WDRA", "63"));
        arr.Add(new ListItem("APPLIED For WDRA", "0"));
        arr.Add(new ListItem("APPLIED For NON WDRA", "00"));
        return arr;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["RefreshButton"] = "No"; //Session["RefreshButton"];
    }
    //private void getCheck()
    //{

    //}
    protected void ckstack_CheckedChanged(object sender, EventArgs e)
    {
        //try
        //{
        //bool calculationflag = true;
        int s;
        int Count_Rows;
        string RowNumber;
        //string WHR_ID;
        decimal TotalCapt = 0;
        decimal TotalCapacity = 0;
        Count_Rows = gvGodown.Rows.Count;
        Server.ScriptTimeout = 11500;
        decimal TotalRCapacity = 0;
        for (s = 0; s < gvGodown.Rows.Count; s++)
        {
            //lblTotalCapacity.Text = "0";
            if (((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == true)
            //if (((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Enabled==true)
            {

                if (Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Text) >= 4 && Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Text) <= 18)
                {
                    RowNumber = gvGodown.Rows[s].Cells[0].Text.ToString();
                    //WHR_ID = gdstackdetail.Rows[s].Cells[3].Text.ToString();
                    decimal Length = Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtLenght")).Text.ToString());
                    decimal Width = Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtWidth")).Text.ToString());
                    decimal Height = Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Text.ToString());
                    // Formula for Capacity = [Length*Breadth*(Height-3)/80] 
                    decimal calculate = (Length * Width * (Height - 3) / 80);
                    TotalCapacity = calculate;
                    //decimal Avai_Wgt = Convert.ToDecimal(gvGodown.Rows[s].Cells[6].Text.ToString());
                    ((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked = true;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtLenght")).Enabled = false;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtWidth")).Enabled = false;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Enabled = false;

                    if (lblTotalCapacity.Text != null && lblTotalCapacity.Text != "")
                    {
                        TotalCapt = TotalCapacity + (Convert.ToDecimal(lblTotalCapacity.Text));
                    }
                    else
                    {
                        TotalCapt = TotalCapacity;
                    }
                    lblTotalCapacity.Text = TotalCapt.ToString();
                    ((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text = calculate.ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Height should be between 4 ft to 18 ft.')", true);
                    ((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked = false;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtLenght")).Enabled = true;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtWidth")).Enabled = true;
                    ((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Enabled = true;
                }
            }
            else
            {
                ((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked = false;
                ((TextBox)gvGodown.Rows[s].FindControl("txtLenght")).Enabled = true;
                ((TextBox)gvGodown.Rows[s].FindControl("txtWidth")).Enabled = true;
                ((TextBox)gvGodown.Rows[s].FindControl("txtHeight")).Enabled = true;
                //((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text = "0";
                //lblTotalCapacity.Text = TotalCapt.ToString();
            }
            //decimal TotalRCapacity = 0;
            TotalRCapacity = (Convert.ToDecimal(TotalRCapacity) + Convert.ToDecimal(((TextBox)gvGodown.Rows[s].FindControl("txtCapacity")).Text.ToString()));
        }
        lblTotalCapacity.Text = TotalRCapacity.ToString();
        lblTotalRegAmt.Text = Math.Round((TotalRCapacity * Convert.ToDecimal(.40)), 0).ToString();
    }
    protected void gvGodown_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    public int checklicnodatevalidation()
    {
        int WF = 0;
        int ch = 0;
        var formatedDate = DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        for (int i = 0; gvGodown.Rows.Count > i; i++)
        {
            if (((CheckBox)gvGodown.Rows[i].FindControl("ckstack")).Checked == true)
            {
                var Issuedate = getDate_MDY(((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Text.ToString().Trim());
                var Validdate = getDate_MDY(((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Text.ToString().Trim());
                if (((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedItem.Text == "--Select--")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown Licence Type..!'); </script> ");
                    break;
                }
                else if (((TextBox)gvGodown.Rows[i].FindControl("Gtxtlicno")).Text.ToString().Trim() == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Licence Number..!'); </script> ");
                    ((TextBox)gvGodown.Rows[i].FindControl("Gtxtlicno")).Focus();
                    break;
                }
                else if (((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Text.ToString().Trim() == "")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Licence Issue Date..!'); </script> ");
                    ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Focus();
                    break;
                }
                else if (Convert.ToDateTime(Issuedate) > Convert.ToDateTime(formatedDate))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Godown Licence issue date/Application Date is cannot be greater then today...'); </script> ");
                    ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicIssuedate")).Focus();
                    break;
                }
                else if (((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Text.ToString().Trim() == "" && ((((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedItem.Text.Trim() == "WDRA") || ((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedItem.Text.Trim() == "NON WDRA"))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Licence Expiry Date..!'); </script> ");
                    ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Focus();
                    break;
                }
                else if (((((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedItem.Text.Trim() == "WDRA") || ((DropDownList)gvGodown.Rows[i].FindControl("ddlgdwntype")).SelectedItem.Text.Trim() == "NON WDRA") && Convert.ToDateTime(Validdate) < Convert.ToDateTime(formatedDate))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Godown Licence Expiry Date is cannot be Less then today...'); </script> ");
                    ((TextBox)gvGodown.Rows[i].FindControl("GtxtLicExpdate")).Focus();
                    break;
                }
                else
                {
                    ch = ch + 1;
                    if (gvGodown.Rows.Count == ch)
                    {
                        WF = 1;
                    }
                }
            }
        }
        return WF;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://www.onlinesbi.sbi/sbicollect/icollecthome.htm?corpID=329338");
    }
    protected void btnpayment_Click(object sender, EventArgs e)
    {
        string strsql = "select Registration_Id,Auth_Person,OreReg.MobileNo,OreReg.EmailID,RegCapacity,RegAmt from tbl_WarehouseRegistration as Reg Inner join tbl_Warehouse_PreReg as OreReg on Reg.Registration_Id = OreReg.Reg_No  where Reg.Registration_Id='" + Session["Reg_No"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(strsql, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            //lblRegID.Text = dt.Rows[0]["Registration_Id"].ToString().Trim();
            //lblOwn.Text = dt.Rows[0]["Auth_Person"].ToString().Trim();
            //lblcontact.Text = dt.Rows[0]["MobileNo"].ToString().Trim();
            //lblemailid.Text = dt.Rows[0]["EmailID"].ToString().Trim();
            //lblRegCapacity.Text = dt.Rows[0]["RegCapacity"].ToString().Trim();
            //lblRegFee.Text = dt.Rows[0]["RegAmt"].ToString().Trim();
        }
        //ModalPopupExtender2.Show();
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        //  Response.Redirect("PrintReg.aspx");
        string Roid = "../JointVentureScheme/PrintReg.aspx?src=RO&vu=" + Session["Reg_No"].ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append("<script>");
        sb.Append("window.open(");
        sb.Append("'" + Roid + "'");
        sb.Append(",'MyWindow', 'height=800,width=1100');");
        sb.Append("</script>");
        this.Page.ClientScript.RegisterClientScriptBlock(GetType(), "sb", sb.ToString());
    }
    protected void btncloseconfrm_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseHome.aspx");
    }
    public void GetGdwn()
    {
        //qry = "select Godown_No,CONVERT(decimal(18,2),G_Length) as G_Length,CONVERT(decimal(18,2),G_Width) as G_Width,CONVERT(decimal(18,2),G_Height) as G_Height,CONVERT(decimal(18,2),G_ScientificCapacity) as G_ScientificCapacity,G_ConstructedYear,case when LicType='68' then 'WDRA' when LicType='63' then 'NON WDRA' when LicType='0' then 'APPLIED For WDRA' when LicType='00' then 'APPLIED For NON WDRA' end LicType,LicNo,convert(varchar(10),LicIssueDate,103) as LicIssueDate,convert(varchar(10),LicValidityDate,103) as LicValidityDate from tbl_WarehouseGodown_Reg where G_ScientificCapacity > 0 and Registration_Id='" + Session["Reg_no"].ToString() + "' order by Godown_No ";
        qry = "select Godown_No,Godown_ID,CONVERT(decimal(18,2),G_Length) as G_Length,CONVERT(decimal(18,2),G_Width) as G_Width,CONVERT(decimal(18,2),G_Height) as G_Height,CONVERT(decimal(18,2),G_ScientificCapacity) as G_ScientificCapacity,G_ConstructedYear,case when LicType='68' then 'WDRA' when LicType='63' then 'NON WDRA' when LicType='0' then 'APPLIED For WDRA' when LicType='00' then 'APPLIED For NON WDRA' end LicType,LicNo,convert(varchar(10),LicIssueDate,103) as LicIssueDate,convert(varchar(10),LicValidityDate,103) as LicValidityDate from tbl_WarehouseGodown_Reg where G_ScientificCapacity > 0 and Registration_Id='" + txtregistrationid.Text.ToString() + "' order by Godown_No ";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //string strsql = "select Registration_Id,Auth_Person,OreReg.MobileNo,OreReg.EmailID,RegCapacity,RegAmt from tbl_WarehouseRegistration as Reg Inner join tbl_Warehouse_PreReg as OreReg on Reg.Registration_Id = OreReg.Reg_No  where Reg.Registration_Id='" + Session["Reg_No"].ToString() + "'";
        SqlCommand cmd = new SqlCommand("Get_Warehosue_Registration_Details", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@RegistrationID", txtregistrationid.Text.Trim());
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GetGdwn();
            tblupdateregistration.Visible = true;
            lblAuthPerson.Text = dt.Rows[0]["Auth_Person"].ToString().Trim();
            lblEmail.Text = dt.Rows[0]["EmailID"].ToString().Trim();
            lblMob.Text = dt.Rows[0]["Mobile_No"].ToString().Trim();
            lblWarehousename.Text = dt.Rows[0]["Warehouse_Name"].ToString().Trim();
            lblDistrict.Text = dt.Rows[0]["District_Name"].ToString().Trim();
            lblbranch.Text = dt.Rows[0]["DepotName"].ToString().Trim();
            lblCapt.Text = dt.Rows[0]["RegCapacity"].ToString().Trim();
            lblRegFee.Text = dt.Rows[0]["RegAmt"].ToString().Trim();
            Session["Warehouse_Name"] = lblWarehousename.Text;
            Session["Registration_ID"] = txtregistrationid.Text.Trim();
        }
    }
    protected void Display(object sender, EventArgs e)
    {
        int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
        GridViewRow row = GridView1.Rows[rowIndex];

        txtWarehouse.Text = Session["Warehouse_Name"].ToString();
        txtGdwnID.Text = (row.FindControl("lblGodown_ID") as Label).Text;
        txtLength.Text = (row.FindControl("lblG_Length") as Label).Text;
        txtWidth.Text = (row.FindControl("lblG_Width") as Label).Text;
        txtheight.Text = (row.FindControl("lblG_Height") as Label).Text;
        txtCapacity.Text = (row.FindControl("lblG_ScientificCapacity") as Label).Text;
        txtConstruction.Text = (row.FindControl("lblG_ConstructedYear") as Label).Text;
        if ((row.FindControl("lblLicType") as Label).Text.Equals("APPLIED For NON WDRA"))
            (row.FindControl("lblLicType") as Label).Text = "APPLIED For NON WDRA";
        if ((row.FindControl("lblLicType") as Label).Text.Equals("APPLIED For WDRA"))
            (row.FindControl("lblLicType") as Label).Text = "APPLIED For WDRA";
        if ((row.FindControl("lblLicType") as Label).Text.Equals("NON WDRA"))
            (row.FindControl("lblLicType") as Label).Text = "NON WDRA";
        if ((row.FindControl("lblLicType") as Label).Text.Equals("WDRA"))
            (row.FindControl("lblLicType") as Label).Text = "WDRA";
        ddllictype.SelectedItem.Text = (row.FindControl("lblLicType") as Label).Text;
        //ddllictype.SelectedValue = (row.FindControl("lblLicType") as DropDownList).Text;
        txtlicno.Text = (row.FindControl("lblLicNo") as Label).Text;
        txtlicdate.Text = (row.FindControl("lblLicIssueDate") as Label).Text;
        txtExpire.Text = (row.FindControl("lblLicValidityDate") as Label).Text;
        //ddlDepositorfill.SelectedValue = (row.FindControl("hdndepositerid") as HiddenField).Value;
        //divNewInsp.Visible = true;
        txtGdwnID.Visible = false;
        ModalPopupExtender1.Show();
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        try
        {
            if (txtregcapacity.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Registration Capacity')", true);
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                cmd = new SqlCommand("Update_Warehouse_Registration_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Registration_Id", Session["Registration_ID"].ToString());
                cmd.Parameters.AddWithValue("@Godown_ID", txtGdwnID.Text.Trim());
                cmd.Parameters.AddWithValue("@G_Length", txtLength.Text.Trim());
                cmd.Parameters.AddWithValue("@G_Width", txtWidth.Text.Trim());
                cmd.Parameters.AddWithValue("@G_Height", txtheight.Text.Trim());
                cmd.Parameters.AddWithValue("@G_ScientificCapacity", txtCapacity.Text);
                cmd.Parameters.AddWithValue("@LicNo", txtlicno.Text);
                cmd.Parameters.AddWithValue("@LicType", ddllictype.SelectedValue);
                cmd.Parameters.AddWithValue("@LicIssueDate", getDate_MDY(txtlicdate.Text));
                cmd.Parameters.AddWithValue("@LicValidityDate", getDate_MDY(txtExpire.Text));
                cmd.Parameters.AddWithValue("@Warehouse_Capacity", txtregcapacity.Text);
                cmd.Parameters.AddWithValue("@G_ConstructedYear", txtConstruction.Text);
                cmd.Parameters.AddWithValue("@RegCapacity", txtregcapacity.Text);
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                TheResult = cmd.Parameters["@TheResult"].Value.ToString();
                if (TheResult.StartsWith("SUCCESS"))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Data Update Successfully')", true);
                    GetGdwn();
                    txtWarehouse.Text = "";
                    txtLength.Text = "";
                    txtWidth.Text = "";
                    txtheight.Text = "";
                    txtCapacity.Text = "";
                    txtConstruction.Text = "";
                    ddllictype.ClearSelection();
                    txtlicno.Text = "";
                    txtlicdate.Text = "";
                    txtExpire.Text = "";
                    txtregcapacity.Text = "";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record not Inserted')", true);
                }
            }
        }
        catch (Exception ex)
        {
            sqltrans.Rollback();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error')", true);
        }
        finally
        {
            con.Close();
        }
    }
}



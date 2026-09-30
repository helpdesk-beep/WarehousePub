using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Data;
using DataAccess;
using System.Diagnostics;
using System.Resources;
using System.Drawing;
using System.IO;

public partial class BranchPages_Update_Remark_In_FIFO_For_FCI : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    SqlTransaction sqltrans;
    string RegionID = "";
    SqlCommand cmd;
    DataTable dt = new DataTable();
    private object localIP;

    int gridcount;
    int ZeroCount;
    int valuecount;
    int rownumber = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                GetBranchData();
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void GetBranchData()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Godown_Details_For_FIFO_Remark_FCI", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BranchID", Session["BranchId"].ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet dt = new DataSet())
                    {
                        int Storage_Value = 0;
                        sda.Fill(dt);
                        if (dt.Tables[0].Rows.Count > 0)
                        {
                            // Session["BranchName"] = ddlbranch.SelectedItem.ToString();
                            gridcount = dt.Tables[0].Rows.Count;
                            foreach (DataRow dr in dt.Tables[0].Rows)
                            {
                                Storage_Value = Convert.ToInt32(dr["FIFOFrizwedStock"]);
                                if (Storage_Value == 0)
                                {
                                    ZeroCount = ZeroCount + 1;
                                }
                                else if (Storage_Value > 0)
                                {
                                    valuecount = valuecount + 1;
                                }
                                //valuecount = valuecount+Convert.ToInt32(dr["DeleveryDone"]);
                            }
                            //GridView1.DataSource = dt;
                            //GridView1.DataBind();
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                            showgrid.Visible = true;
                            // GridView1.Caption = @"<b style=""font-weight: bold; text-align:center;""> M.P. WAREHOUSING & LOGISTICS CORPORATION" + "</br> " + "गोदाम वार FIFO नीति से स्टॉक के उठाव  की स्थिति" + "</br> " + "शाखा का नाम" + "  -   " + ddlbranch.SelectedItem.ToString();
                            //GridView1.Columns[1].Visible = false;
                            //// GridView1.columns.RemoveAt(1);
                            //lblsyncdate.Text = dt.Rows[0]["StockPositionAsOnDate"].ToString();

                            //GridView1.FooterRow.Style.Add("text-align;font-weight: bold;text-align:right;color:red;", "right");
                            //GridView1.FooterRow.Cells[1].Text = "Total";
                            //GridView1.FooterRow.Cells[2].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("AvlBalance")).ToString();

                        }
                        else
                        {
                            showgrid.Visible = false;

                            Depositor_Gridview.DataSource = null;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void Depositor_Gridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //  HiddenField hdnVerificationType = (HiddenField)e.Row.FindControl("hdnVerificationType");
            HiddenField hdndiffirence = (HiddenField)e.Row.FindControl("hdndiffirence");
            HiddenField hdnQty = (HiddenField)e.Row.FindControl("hdnQty");

            rownumber = rownumber + 1;
            string checkvalue = hdnQty.Value;
            if (gridcount == ZeroCount)
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#FFFF00");//e9716b Yellow
            }
            else if (Convert.ToInt32(hdndiffirence.Value) > 0 && valuecount > 0)
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#28b779");//e9716b Green
                valuecount = valuecount - 1;
            }
            else if (Convert.ToInt32(hdndiffirence.Value) == 0 && valuecount > 0)
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#e9716b");//e9716b Red
            }
            else if (Convert.ToInt32(hdndiffirence.Value) == 0 && valuecount == 0)
            {
                e.Row.BackColor = ColorTranslator.FromHtml("#FFFF00");//e9716b yeloow
            }

        }

    }

    public int GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
    protected string getDate_MDY(string inDate)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DateTime dtProjectStartDate = Convert.ToDateTime(inDate);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        return (Convert.ToDateTime(dtProjectStartDate).ToString("MM/dd/yyyy"));
    }


    protected void Depositor_Gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = Depositor_Gridview.Rows[rowIndex];

            //Fetch value of Name.
            string hdnGodownID = (row.FindControl("hdnGodownID") as HiddenField).Value;
            //string ddlEWC = (row.FindControl("ddlEWC") as DropDownList).SelectedValue;
            //string hdnBranchId = (row.FindControl("hdnBranchId") as TextBox).Text;
            string ddlcategory = (row.FindControl("ddlcategory") as DropDownList).SelectedValue;
            string lblRemark = (row.FindControl("lblRemark") as TextBox).Text;
            //string lblFirstWHRDate = (row.FindControl("flupslip") as TextBox).Text;
            FileUpload fu = (FileUpload)row.FindControl("flupslip");

            if (ddlcategory.ToString() != "4")
            {

                string str = DateTime.Now.ToString("dd_MM_yyyy") + '_' + hdnGodownID + '_' + fu.FileName;
                fu.PostedFile.SaveAs(Server.MapPath("/BranchPages/FIFOUploadedDocument/" + str));
                string file = "/BranchPages/FIFOUploadedDocument/" + str.ToString();

                string filePath = fu.PostedFile.FileName;
                string fileName = Path.GetFileName(filePath);
                string ext = Path.GetExtension(fileName);
                string type = String.Empty;
                if (fu.HasFile)
                {

                    switch (ext) // this switch code validate the files which allow to upload only PDF file   
                    {
                        case ".PDF":
                            type = "application/pdf";
                            break;
                        case ".pdf":
                            type = "application/pdf";
                            break;
                    }
                }
                else
                {
                    string strMsg2 = "Upload Only PDF Files!!!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
                }
                //string ddlrailsaided = (row.FindControl("ddlrailsaided") as DropDownList).SelectedValue;
                //string ddlrailsaidedcount = (row.FindControl("ddlrailsaidedcount") as DropDownList).SelectedValue;

                //Session["hdnId"] = hdnId.ToString();
                Session["hdnGodownID"] = hdnGodownID.ToString();
                Session["ddlcategory"] = ddlcategory.ToString();
                Session["lblRemark"] = lblRemark.ToString();
                Session["str"] = str.ToString();
                //Session["lblFirstWHRDate"] = lblFirstWHRDate.0ToString();
                if (type != String.Empty)
                {

                    Stream fsHn = fu.PostedFile.InputStream;
                    BinaryReader brHn = new BinaryReader(fsHn); //reads the binary files  
                    Byte[] bytesHn = brHn.ReadBytes((Int32)fsHn.Length); //counting the file length into bytes  


                    if (ddlcategory.ToString() == "0")
                    {
                        string strMsg = "Select Category |||";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                    }
                    else if (ddlcategory.ToString() == "3")
                    {
                        if (String.IsNullOrEmpty(lblRemark))
                        {
                            string strMsg = "Enter Remark |||";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                        }
                        else
                        {
                            Update(Session["BranchId"].ToString(), hdnGodownID, ddlcategory, lblRemark, str);
                        }
                    }
                    else
                    {
                        Update(Session["BranchId"].ToString(), hdnGodownID, ddlcategory, lblRemark, str);
                    }
                    // RemoveRowJVS(hdnId);
                }
            }
            else
            {
                Update(Session["BranchId"].ToString(), hdnGodownID, ddlcategory, lblRemark, "F");
            }
        }
    }
    public void Update(string BranchID, string Godownid, string categoryid, string Remark, string filepath)
    {
        SqlCommand cmd1 = new SqlCommand();
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand("Insert_Godown_Wise_Remark", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchID", BranchID);
            cmd.Parameters.AddWithValue("@GodownID", Godownid);
            // cmd.Parameters.AddWithValue("@WHR_Oldest_Date", whrolddate);
            cmd.Parameters.AddWithValue("@CategoryID", categoryid);
            cmd.Parameters.AddWithValue("@Prob_Remark", Remark.ToString());
            cmd.Parameters.AddWithValue("@Inserted_IP", localIP.ToString());
            cmd.Parameters.AddWithValue("@Delivery_mode", "F");
            cmd.Parameters.AddWithValue("@UploadFile", filepath.ToString());
            cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
            cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

            if (TheResult.StartsWith("SUCCESS"))
            {
                string strMsg = "WHR Remark Update Successfully |||";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg + "')", true);
                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                Depositor_Gridview.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                GetBranchData();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('कृपया पुनः प्रयास करें !');", true);
            }
        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
    protected void GrdOfficerPreviousInsp_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {

    }

}
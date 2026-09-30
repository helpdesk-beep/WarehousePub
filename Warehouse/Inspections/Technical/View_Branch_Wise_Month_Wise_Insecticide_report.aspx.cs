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
using System.Collections;

public partial class Inspections_Technical_View_Branch_Wise_Month_Wise_Insecticide_report : System.Web.UI.Page
{
    public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public SqlConnection con_WLC = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    string constr2 = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
    string PFID = "";
    SqlTransaction sqltran;
    string client_IP = "";
    int a_id = 0;
    string Region_ID = "";
    string Insp_ID = "";
    string Veri_ID = "";

    
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            //fillGrdAllumion();
            //fillGrdMathalin();
            //fillGrdDeltamathirin();
            fillFinancialYear();
            fillMonth();
            GetRegion();
        }

    }
    protected void fillMonth()
    {
        //ddlmonth.ClearSelection();
        ddlmonth.Items.Clear();
        ddlmonth.Items.Add(new ListItem("--Select--", "0"));
        ddlmonth.Items.Add(new ListItem("January", "1"));
        ddlmonth.Items.Add(new ListItem("February", "2"));
        ddlmonth.Items.Add(new ListItem("March", "3"));
        ddlmonth.Items.Add(new ListItem("April", "4"));
        ddlmonth.Items.Add(new ListItem("May", "5"));
        ddlmonth.Items.Add(new ListItem("June", "6"));
        ddlmonth.Items.Add(new ListItem("July", "7"));
        ddlmonth.Items.Add(new ListItem("August", "8"));
        ddlmonth.Items.Add(new ListItem("September", "9"));
        ddlmonth.Items.Add(new ListItem("October", "10"));
        ddlmonth.Items.Add(new ListItem("November", "11"));
        ddlmonth.Items.Add(new ListItem("December", "12"));
        ddlmonth.SelectedIndex = 0;
    }
    protected void fillFinancialYear()
    {

        ddlFyear.Items.Add((DateTime.Now.Year).ToString());
        ddlFyear.Items.Add((DateTime.Now.Year - 1).ToString());
        ddlFyear.Items.Add((DateTime.Now.Year - 2).ToString());
        ddlFyear.Items.Add((DateTime.Now.Year - 3).ToString());
        ddlFyear.Items.Add((DateTime.Now.Year - 4).ToString());
        ddlFyear.Items.Add((DateTime.Now.Year - 5).ToString());
    }

    private void GetRegion()
    {
        string strDist = "";
        strDist = "SELECT distinct Regionnm,Region_ID FROM tbl_MetaData_DISTRICT order by Regionnm";
        SqlDataAdapter da = new SqlDataAdapter(strDist, con_WLC);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlRegion.DataSource = ds.Tables[0];
            ddlRegion.DataTextField = "Regionnm";
            ddlRegion.DataValueField = "Region_ID";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlRegion.Items.Insert(0, "--Select--");
        }
    }
 
    protected void fillGrdAllumion()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Insecticide_Branch_Wise_Month_Wise_Aluminum_Phosphide", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Year", ddlFyear.SelectedValue);
                cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
                cmd.Parameters.AddWithValue("@Insecticide", ddlInsecticide.SelectedValue);
                if (ddlRegion.SelectedValue == "--Select--")
                {
                    cmd.Parameters.AddWithValue("@RegionID", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            //GrdOfficerPreviousInsp.Caption = @"<b style=""font-weight: bold;"">Branch Name: " + "  -   " + ddlbranch.SelectedItem.ToString() + "</b> ";
                            divshow.Visible = true;
                           // btnPrint.Visible = true;
                            GrdAllumion.DataSource = ds.Tables[0];
                            GrdAllumion.DataBind();
                           // GrdAllumion.Columns[1].Visible = false;
                           // GrdAllumion.Caption = @"<b style=""font-weight: bold; margin-left:20px;""> 01. Aluminum Phosphide" + "</b> ";
                            GrdAllumion.Caption = @"<b style=""font-weight: bold; margin-left:20px;""> मध्य प्रदेश वेयरहाउसिंग लॉजिस्टिक कार्पोरेशन क्षेत्रिय कार्लयालय" + "  " + ddlRegion.SelectedItem.ToString() + "</br> "   + " की ब्रांच वार" + " - " + ddlInsecticide.SelectedItem.ToString() + " वर्ष  " + " - " + ddlFyear.SelectedValue.ToString() + "   -  " + "महीना" + " - " + ddlmonth.SelectedItem.ToString() + " " + "की जानकारी";
                            GrdAllumion.FooterRow.Style.Add("text-align", "center");
                            GrdAllumion.FooterRow.Cells[2].Text = @"<b style=""font-weight: bold;""> Total " + "</b> ";
                            GrdAllumion.FooterRow.Cells[3].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Opening_Balance_quantity")).ToString();
                            GrdAllumion.FooterRow.Cells[4].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Receipt_Balance_quantity")).ToString();
                            GrdAllumion.FooterRow.Cells[5].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CBQ1")).ToString();
                            GrdAllumion.FooterRow.Cells[6].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TBQ")).ToString();
                            GrdAllumion.FooterRow.Cells[7].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TJQ")).ToString();
                            GrdAllumion.FooterRow.Cells[8].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Quantity")).ToString();
                        }
                        else
                        {
                            divshow.Visible = false;
                           // btnPrint.Visible = false;
                            GrdAllumion.DataSource = null;
                            GrdAllumion.DataBind();
                        }
                    }
                }
            }
        }
    }

    //protected void fillGrdMathalin()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("Get_Insecticide_Branch_Wise_Month_Wise", con))
    //        {
    //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@Year", ddlFyear.SelectedValue);
    //            cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
    //            cmd.Parameters.AddWithValue("@Insecticide", ddlInsecticide.SelectedValue);
    //            if (ddlRegion.SelectedValue == "--Select--")
    //            {
    //                cmd.Parameters.AddWithValue("@RegionID", "0");
    //            }
    //            else
    //            {
    //                cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);
    //            }
    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;
    //                using (DataSet ds = new DataSet())
    //                {
    //                    sda.Fill(ds);
    //                    if (ds.Tables.Count > 0)
    //                    {
    //                        //GrdOfficerPreviousInsp.Caption = @"<b style=""font-weight: bold;"">Branch Name: " + "  -   " + ddlbranch.SelectedItem.ToString() + "</b> ";
    //                        divshow.Visible = true;
    //                        // btnPrint.Visible = true;
    //                        GrdMathalin.DataSource = ds.Tables[1];
    //                        GrdMathalin.DataBind();
    //                        //GrdMathalin.Columns[1].Visible = false;
    //                        GrdMathalin.Caption = @"<b style=""font-weight: bold;""> 02. Malathion " + "</b> ";
    //                        GrdMathalin.FooterRow.Style.Add("text-align", "center");
    //                        GrdMathalin.FooterRow.Cells[2].Text = @"<b style=""font-weight: bold;""> Total " + "</b> ";
    //                        GrdMathalin.FooterRow.Cells[3].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Opening_Balance_quantity")).ToString();
    //                        GrdMathalin.FooterRow.Cells[4].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Receipt_Balance_quantity")).ToString();
    //                        GrdMathalin.FooterRow.Cells[5].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CBQ1")).ToString();
    //                        GrdMathalin.FooterRow.Cells[6].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TBQ")).ToString();
    //                        GrdMathalin.FooterRow.Cells[7].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TJQ")).ToString();
    //                        GrdMathalin.FooterRow.Cells[8].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Quantity")).ToString();
    //                    }
    //                    else
    //                    {
    //                        divshow.Visible = false;
    //                        // btnPrint.Visible = false;
    //                        GrdMathalin.DataSource = null;
    //                        GrdMathalin.DataBind();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //protected void fillGrdDeltamathirin()
    //{
    //    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("Get_Insecticide_Branch_Wise_Month_Wise", con))
    //        {
    //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //            cmd.Parameters.AddWithValue("@Year", ddlFyear.SelectedValue);
    //            cmd.Parameters.AddWithValue("@Month", ddlmonth.SelectedValue);
    //            cmd.Parameters.AddWithValue("@Insecticide", ddlInsecticide.SelectedValue);
    //            if (ddlRegion.SelectedValue == "--Select--")
    //            {
    //                cmd.Parameters.AddWithValue("@RegionID", "0");
    //            }
    //            else
    //            {
    //                cmd.Parameters.AddWithValue("@RegionID", ddlRegion.SelectedValue);
    //            }
    //            using (SqlDataAdapter sda = new SqlDataAdapter())
    //            {
    //                cmd.Connection = con;
    //                sda.SelectCommand = cmd;
    //                using (DataSet ds = new DataSet())
    //                {
    //                    sda.Fill(ds);
    //                    if (ds.Tables.Count > 0)
    //                    {
    //                        //GrdOfficerPreviousInsp.Caption = @"<b style=""font-weight: bold;"">Branch Name: " + "  -   " + ddlbranch.SelectedItem.ToString() + "</b> ";
    //                        divshow.Visible = true;
    //                        // btnPrint.Visible = true;
    //                        GrdDeltamathirin.DataSource = ds.Tables[2];
    //                        GrdDeltamathirin.DataBind();
    //                        //GrdDeltamathirin.Columns[1].Visible = false;
    //                        GrdDeltamathirin.Caption = @"<b style=""font-weight: bold;""> 03. Deltamethrin " + "</b> ";
    //                        GrdDeltamathirin.FooterRow.Style.Add("text-align", "center");
    //                        GrdDeltamathirin.FooterRow.Cells[2].Text = @"<b style=""font-weight: bold;""> Total " + "</b> ";
    //                        GrdDeltamathirin.FooterRow.Cells[3].Text = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Opening_Balance_quantity")).ToString();
    //                        GrdDeltamathirin.FooterRow.Cells[4].Text = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Receipt_Balance_quantity")).ToString();
    //                        GrdDeltamathirin.FooterRow.Cells[5].Text = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("CBQ1")).ToString();
    //                        GrdDeltamathirin.FooterRow.Cells[6].Text = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("TBQ")).ToString();
    //                        GrdDeltamathirin.FooterRow.Cells[7].Text = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("TJQ")).ToString();
    //                        GrdDeltamathirin.FooterRow.Cells[8].Text = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Quantity")).ToString();
    //                    }
    //                    else
    //                    {
    //                        divshow.Visible = false;
    //                        // btnPrint.Visible = false;
    //                        GrdDeltamathirin.DataSource = null;
    //                        GrdDeltamathirin.DataBind();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

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



    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/Technical/EmpCorner.aspx");
    }




    protected void btnview_Click(object sender, EventArgs e)
    {
        fillGrdAllumion();
        divAluminumPhosphide.Visible = true;
        //if (ddlInsecticide.SelectedValue == "0")
        //{
        //    fillGrdAllumion();
        //    divAluminumPhosphide.Visible = true;
        //    divMalathion.Visible = false;
        //    divDeltamethrin.Visible = false;
        //}
        //else if (ddlInsecticide.SelectedValue == "1")
        //{
        //    fillGrdAllumion();
        //    divAluminumPhosphide.Visible = true;
        //    divMalathion.Visible = false;
        //    divDeltamethrin.Visible = false;
        //}
        //else if (ddlInsecticide.SelectedValue == "2")
        //{
        //    fillGrdMathalin();
        //    divAluminumPhosphide.Visible = false;
        //    divMalathion.Visible = true;
        //    divDeltamethrin.Visible = false;
        //}
        //else if (ddlInsecticide.SelectedValue == "3")
        //{
        //    fillGrdDeltamathirin();
        //    divAluminumPhosphide.Visible = false;
        //    divMalathion.Visible = false;
        //    divDeltamethrin.Visible = true;
        //}
        //else
        //{
        //    divAluminumPhosphide.Visible = false;
        //    divMalathion.Visible = false;
        //    divDeltamethrin.Visible = false;
        //}

    }
}

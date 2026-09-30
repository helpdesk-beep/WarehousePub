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

public partial class Inspections_Technical_View_Region_Wise_Insecticide_report : System.Web.UI.Page
{
    public SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
    // public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["InspectionConString"].ToString());
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
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
            fillGrdAllumion();
            fillGrdMathalin();
            fillGrdDeltamathirin();
        }

    }
    protected void fillGrdAllumion()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Insecticide_Region_Wise_For_dashboard", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                            GrdAllumion.Caption = @"<b style=""font-weight: bold;""> 01. Aluminum Phosphide" + "</b> ";
                            GrdAllumion.FooterRow.Style.Add("text-align", "center");
                            GrdAllumion.FooterRow.Cells[1].Text = "Total";
                            GrdAllumion.FooterRow.Cells[2].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Opening_Balance_quantity")).ToString();
                            GrdAllumion.FooterRow.Cells[3].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Receipt_Balance_quantity")).ToString();
                            GrdAllumion.FooterRow.Cells[4].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CBQ1")).ToString();
                            GrdAllumion.FooterRow.Cells[5].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TBQ")).ToString();
                            GrdAllumion.FooterRow.Cells[6].Text = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Quantity")).ToString();
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

    protected void fillGrdMathalin()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Insecticide_Region_Wise_For_dashboard", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                            GrdMathalin.DataSource = ds.Tables[1];
                            GrdMathalin.DataBind();
                            //GrdMathalin.Columns[1].Visible = false;
                            GrdMathalin.Caption = @"<b style=""font-weight: bold;""> 02. Malathion " + "</b> ";
                            GrdMathalin.FooterRow.Style.Add("text-align", "center");
                            GrdMathalin.FooterRow.Cells[1].Text = "Total";
                            GrdMathalin.FooterRow.Cells[2].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Opening_Balance_quantity")).ToString();
                            GrdMathalin.FooterRow.Cells[3].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Receipt_Balance_quantity")).ToString();
                            GrdMathalin.FooterRow.Cells[4].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CBQ1")).ToString();
                            GrdMathalin.FooterRow.Cells[5].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TBQ")).ToString();
                            GrdMathalin.FooterRow.Cells[6].Text = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Quantity")).ToString();
                        }
                        else
                        {
                            divshow.Visible = false;
                            // btnPrint.Visible = false;
                            GrdMathalin.DataSource = null;
                            GrdMathalin.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void fillGrdDeltamathirin()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Insecticide_Region_Wise_For_dashboard", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                            GrdDeltamathirin.DataSource = ds.Tables[2];
                            GrdDeltamathirin.DataBind();
                            //GrdDeltamathirin.Columns[1].Visible = false;
                            GrdDeltamathirin.Caption = @"<b style=""font-weight: bold;""> 03. Deltamethrin " + "</b> ";
                            GrdDeltamathirin.FooterRow.Style.Add("text-align", "center");
                            GrdDeltamathirin.FooterRow.Cells[1].Text = "Total";
                            GrdDeltamathirin.FooterRow.Cells[2].Text = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Opening_Balance_quantity")).ToString();
                            GrdDeltamathirin.FooterRow.Cells[3].Text = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Receipt_Balance_quantity")).ToString();
                            GrdDeltamathirin.FooterRow.Cells[4].Text = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("CBQ1")).ToString();
                            GrdDeltamathirin.FooterRow.Cells[5].Text = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("TBQ")).ToString();
                            GrdDeltamathirin.FooterRow.Cells[6].Text = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Quantity")).ToString();
                        }
                        else
                        {
                            divshow.Visible = false;
                            // btnPrint.Visible = false;
                            GrdDeltamathirin.DataSource = null;
                            GrdDeltamathirin.DataBind();
                        }
                    }
                }
            }
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



    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Warehouse/Inspections/Technical/EmpCorner.aspx");
    }
}

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
using System.IO;
using System.Data.SqlClient;

public partial class JointVentureScheme_Rpt_JVS2022_23_Cap_OfferSummary : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string DistID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            fillgrid();
        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {


    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }



    private void fillgrid()
    {
        try
        {


            //string query1 = "select Region_Id,region from tbl_MetaData_Region where Region_Id= '" + Session["UserId"].ToString() + "'";
            //SqlCommand cmd1 = new SqlCommand(query1, con);
            //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //DataSet ds1 = new DataSet();
            //da1.Fill(ds1);
            //if (ds1.Tables[0].Rows.Count > 0)
            //{

            //    ddlregion.DataSource = ds1.Tables[0];
            //    ddlregion.DataTextField = "region";
            //    ddlregion.DataValueField = "Region_Id";
            //    ddlregion.DataBind();
            //    ddlregion.Items.Insert(0, new ListItem("Region चुने", "0"));

            //}



            string query1 = "select District_Id, District_Name from tbl_metadata_district MD left  join tbl_MetaData_Region MR on MR.Region_Id = MD.Region_ID where MD.Region_ID ='" + Session["UserId"].ToString() + "'";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ddldist.DataSource = ds1.Tables[0];
                ddldist.DataTextField = "District_Name";
                ddldist.DataValueField = "District_Id";
                ddldist.DataBind();
                ddldist.Items.Insert(0, new ListItem("जिला चुने", "0"));
            }

                string query = "";
             query = "select dst.Regionnm,dst.District_Name,COUNT(WR.Registration_Id) AS TotalRegistrasion,sum(WR.Warehouse_Capacity) AS Total_Registrasion_Warehouse_Capacity, COUNT(CF.Reg_ID) AS TotalChoice,sum(CF.WareHouse_Cap) AS Total_Choice_Filling_WareHouse_Cap, COUNT(CASE WHEN CF.Choice = 'A' THEN CF.Reg_ID END) AS ChoiceA,sum(CASE WHEN CF.Choice = 'A' THEN CF.WareHouse_Cap END) AS ChoiceA_WareHouse_Cap,COUNT(CASE WHEN CF.Choice = 'B' THEN CF.Reg_ID END) AS ChoiceB,sum(CASE WHEN CF.Choice = 'B' THEN CF.WareHouse_Cap END) AS ChoiceB_WareHouse_Cap, COUNT(CASE WHEN CF.Choice = 'BR' THEN CF.Reg_ID END) AS ChoiceBR,sum(CASE WHEN CF.Choice = 'BR' THEN CF.WareHouse_Cap END) AS ChoiceBR_WareHouse_Cap,((COUNT(WR.Registration_Id)) - (COUNT(CF.Reg_ID))) AS RamaningForChoices,((sum(WR.Warehouse_Capacity)) - (sum(CF.WareHouse_Cap))) AS Ramaning_For_WareHouse_Cap from tbl_MetaData_DISTRICT dst LEFT JOIN tbl_WarehouseRegistration WR  ON dst.District_Id = WR.DistrictId LEFT JOIN Tbl_JVS_Choise_Filling CF ON WR.Registration_Id = CF.Reg_ID where(dst.Region_ID = '" + Session["UserId"].ToString() + "')  group by dst.Regionnm,dst.District_Name order by dst.Regionnm,dst.District_Name";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                //GridView1.FooterRow.Style.Add("text-align", "center");
                //GridView1.FooterRow.Cells[2].Text = "Total";
                //GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalRegistrasion")).ToString();
                //GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Registrasion_Warehouse_Capacity")).ToString();

                //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalChoice")).ToString();
                //GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Choice_Filling_WareHouse_Cap")).ToString();

                //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceA")).ToString();
                //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceA_WareHouse_Cap")).ToString();

                //GridView1.FooterRow.Cells[9].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceB")).ToString();
                //GridView1.FooterRow.Cells[10].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceB_WareHouse_Cap")).ToString();

                //GridView1.FooterRow.Cells[11].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceBR")).ToString();
                //GridView1.FooterRow.Cells[12].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceBR_WareHouse_Cap")).ToString();

                //GridView1.FooterRow.Cells[13].Text = dt.AsEnumerable().Sum(row => row.Field<int>("RamaningForChoices")).ToString();
                //GridView1.FooterRow.Cells[14].Text = dt.AsEnumerable().Sum(row => row.Field<int>("Ramaning_For_WareHouse_Cap")).ToString();



               
                //GridView1.FooterRow.Cells[2].Text = "Total";
              

                //decimal total6 = dt.AsEnumerable().Sum(row => row.Field<int>("TotalRegistrasion"));
                //GridView1.FooterRow.Cells[3].Text = total6.ToString("N2");

                //decimal total5 = dt.AsEnumerable().Sum(row => row.Field<decimal>("Total_Registrasion_Warehouse_Capacity"));
                //GridView1.FooterRow.Cells[4].Text = total5.ToString("N2");

                //decimal total4 = dt.AsEnumerable().Sum(row => row.Field<int>("TotalChoice"));
                //GridView1.FooterRow.Cells[5].Text = total4.ToString("N2");

                //decimal total3 = dt.AsEnumerable().Sum(row => row.Field<int>("Total_Choice_Filling_WareHouse_Cap"));
                //GridView1.FooterRow.Cells[6].Text = total3.ToString("N2");

                //decimal total1 = dt.AsEnumerable().Sum(row => row.Field<decimal>("ChoiceA"));
                //GridView1.FooterRow.Cells[7].Text = total1.ToString("N2");

                //decimal total2 = dt.AsEnumerable().Sum(row => row.Field<decimal>("ChoiceA_WareHouse_Cap"));
                //GridView1.FooterRow.Cells[8].Text = total2.ToString("N2");

                //decimal total7 = dt.AsEnumerable().Sum(row => row.Field<decimal>("ChoiceB"));
                //GridView1.FooterRow.Cells[9].Text = total7.ToString("N2");

                //decimal total8 = dt.AsEnumerable().Sum(row => row.Field<decimal>("ChoiceB_WareHouse_Cap"));
                //GridView1.FooterRow.Cells[10].Text = total8.ToString("N2");

                //decimal total9 = dt.AsEnumerable().Sum(row => row.Field<decimal>("ChoiceBR"));
                //GridView1.FooterRow.Cells[11].Text = total9.ToString("N2");

                //decimal total10 = dt.AsEnumerable().Sum(row => row.Field<decimal>("ChoiceBR_WareHouse_Cap"));
                //GridView1.FooterRow.Cells[12].Text = total10.ToString("N2");

                //decimal total11 = dt.AsEnumerable().Sum(row => row.Field<decimal>("RamaningForChoices"));
                //GridView1.FooterRow.Cells[13].Text = total11.ToString("N2");

                //decimal total12 = dt.AsEnumerable().Sum(row => row.Field<decimal>("Ramaning_For_WareHouse_Cap"));
                //GridView1.FooterRow.Cells[14].Text = total12.ToString("N2");



            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception)
        {
            //////
        }
    }



    protected void SearchID_Click(object sender, EventArgs e)
    {

        if (ddldist.SelectedValue != "0")
        {
            SqlCommand cmdd = new SqlCommand("Sp_Capacity_Wise_Choise_Filling", con);
            cmdd.CommandType = CommandType.StoredProcedure;
            cmdd.Parameters.AddWithValue("@RegionID", Session["UserId"].ToString());
            cmdd.Parameters.AddWithValue("@DistrictID", ddldist.SelectedValue);


            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                //GridView1.FooterRow.Style.Add("text-align", "center");
                //GridView1.FooterRow.Cells[2].Text = "Total";
                //GridView1.FooterRow.Cells[3].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalRegistrasion")).ToString();
                //GridView1.FooterRow.Cells[4].Text = dt.AsEnumerable().Sum(row => row.Field<int>("TotalChoice")).ToString();

                //GridView1.FooterRow.Cells[5].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceA")).ToString();
                //GridView1.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceB")).ToString();
                //GridView1.FooterRow.Cells[7].Text = dt.AsEnumerable().Sum(row => row.Field<int>("ChoiceBR")).ToString();
                //GridView1.FooterRow.Cells[8].Text = dt.AsEnumerable().Sum(row => row.Field<int>("RamaningForChoices")).ToString();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }   


    }
}



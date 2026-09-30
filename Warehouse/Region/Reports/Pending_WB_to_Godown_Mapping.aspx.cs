using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

public partial class Region_Reports_Pending_WB_to_Godown_Mapping : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString();
    int totalWB = 0;           // Grand total
    int districtWBCount = 0;   // District subtotal
    string currentDistrict = ""; // Track current district

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (!IsPostBack)
            {
                BindReport();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }

    

    protected void BindReport()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Pending_Godown_With_WH_Mapping", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RegionID", Session["Region_ID"].ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gvReport.DataSource = dt;
                            gvReport.DataBind();                            
                        }
                        else
                        {
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);
                            dt.Rows.Add(null, null, null, null);

                            gvReport.DataSource = dt;
                            gvReport.DataBind();
                        }
                    }
                }
            }
        }
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
            "attachment;filename=Weighbridge_Null_Name_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                gvReport.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required for Excel Export
    }
}
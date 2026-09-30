using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_PurchaseRateMaster : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    public string qry = "";
    SqlCommand cmd = null;
    string depotid = "";
    SqlTransaction sqltran;
    string depottype = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCommodity();
            fillcropyr();
            Getrates();
        }
    }

    private void GetCommodity()
    {
        try
        {
            qry = "select * from dbo.tbl_MetaData_STORAGE_COMMODITY order by Commodity_Name";
            cmd = new SqlCommand(qry, con);
          IDataAdapter  da = new SqlDataAdapter(cmd);
           DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcomm.DataSource = ds.Tables[0];
                ddlcomm.DataValueField = "Commodity_Id";
                ddlcomm.DataTextField = "Commodity_Name";
                ddlcomm.DataBind();
            }
        }
        catch (Exception ex)
        {
           // lblMsg.Text = ex.Message.ToString();
        }
    }

    protected void fillcropyr()
    {
        ddlcropyr.Items.Insert(0, "2016-17");
        ddlcropyr.Items.Insert(1, "2015-16");
        ddlcropyr.Items.Insert(2, "2014-15");
        ddlcropyr.Items.Insert(3, "2013-14");
        ddlcropyr.Items.Insert(4, "2012-13");
        ddlcropyr.Items.Insert(5, "2011-12");
        ddlcropyr.Items.Insert(6, "2010-11");
        ddlcropyr.Items.Insert(7, "2009-10");
        ddlcropyr.Items.Insert(8, "Before 2009");
        ddlcropyr.SelectedIndex = 0;
        //for (int i = DateTime.Now.Year + 1; i > 2009; i--)
        //{
        //    ddlcropyr.Items.Add(i.ToString());

        //}

    }

    private void Getrates()
    {
        try
        {
            qry = "SELECT [ComID],(select Commodity_Name from dbo.tbl_MetaData_STORAGE_COMMODITY where Commodity_Id=ComID) as comm,[CropYear],[Rate] FROM [Intergrated_MP_STORAGE].[dbo].[PurchaseRateMaster] order by comm";
            cmd = new SqlCommand(qry, con);
            IDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvratelist.DataSource = ds.Tables[0];

                gvratelist.DataBind();
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = ex.Message.ToString();
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string ClientIP = Request.ServerVariables["REMOTE_ADDR"];
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        qry = "insert into [PurchaseRateMaster] ([ComID],[Rate],[CropYear],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate]) values ('" + ddlcomm.SelectedValue.ToString() + "','" + txtrate.Text + "','" + ddlcropyr.SelectedItem.Text + "','" + ClientIP + "',getdate(),'" + ClientIP + "',getdate())";
        cmd = new SqlCommand(qry, con);
        int c = cmd.ExecuteNonQuery();
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        if (c > 0)
        {

            Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Record Inserted '); </script> ");

        }
    }
}
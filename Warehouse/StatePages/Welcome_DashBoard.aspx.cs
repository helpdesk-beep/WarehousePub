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

public partial class StatePages_Welcome_DashBoard : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
    string query = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillgrid();
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            string CS = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
            using (SqlConnection constr = new SqlConnection(CS))
            {
                cmd = new SqlCommand("Update_Stock_Position", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                constr.Open();
                cmd.Parameters.Add("@TheResult", SqlDbType.VarChar, 250);
                cmd.Parameters["@TheResult"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string TheResult = cmd.Parameters["@TheResult"].Value.ToString();

                if (TheResult.StartsWith("SUCCESS"))
                {
                    Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Update...');if(alert){ window.location='Welcome_DashBoard.aspx';}</script>");
                }
                else
                {
                    Page.RegisterStartupScript("UserMsg", "<script>alert('Error..');if(alert){ window.location='Welcome_DashBoard.aspx';}</script>");
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('" + ex.Message + "'); </script> ");
        }
    }
    protected void fillgrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Stock_Position", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Commodity_Id", 0);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Wheat.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Wheat"].ToString()).ToString();
                                WheatCovered.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["WheatCovered"].ToString()).ToString();
                                WheatCap.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["WheatCap"].ToString()).ToString();
                                Paddy.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Paddy"].ToString()).ToString();
                                PaddyCovered.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["PaddyCovered"].ToString()).ToString();
                                PaddyCap.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["PaddyCap"].ToString()).ToString();
                                Rice.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Rice"].ToString()).ToString();
                                RiceCovered.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["RiceCovered"].ToString()).ToString();
                                RiceCap.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["RiceCap"].ToString()).ToString();
                                Maze.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Maze"].ToString()).ToString();
                                MazeCovered.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["MazeCovered"].ToString()).ToString();
                                MazeCap.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["MazeCap"].ToString()).ToString();
                                Bajra.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Bajra"].ToString()).ToString();
                                BajraCovered.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["BajraCovered"].ToString()).ToString();
                                BajraCap.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["BajraCap"].ToString()).ToString();
                                Jowar.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Jowar"].ToString()).ToString();
                                JowarCovered.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["JowarCovered"].ToString()).ToString();
                                JowarCap.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["JowarCap"].ToString()).ToString();
                                Gram.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Gram"].ToString()).ToString();
                                GramCovered.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["GramCovered"].ToString()).ToString();
                                GramCap.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["GramCap"].ToString()).ToString();
                                Lentil.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Lentil"].ToString()).ToString();
                                LentilCovered.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["LentilCovered"].ToString()).ToString();
                                LentilCap.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["LentilCap"].ToString()).ToString();
                                hdnDate.Value = ds.Tables[0].Rows[0]["Date"].ToString();
                                if (MazeCovered.Text == "")
                                {
                                    MazeCovered.Text = "000";
                                }
                                if (MazeCap.Text == "")
                                {
                                    MazeCap.Text = "000";
                                }
                                if (BajraCovered.Text == "")
                                {
                                    BajraCovered.Text = "000";
                                }
                                if (BajraCap.Text == "")
                                {
                                    BajraCap.Text = "000";
                                }
                                if (JowarCovered.Text == "")
                                {
                                    JowarCovered.Text = "000";
                                }
                                if (JowarCap.Text == "")
                                {
                                    JowarCap.Text = "000";
                                }
                                if (LentilCovered.Text == "")
                                {
                                    LentilCovered.Text = "000";
                                }
                                if (LentilCap.Text == "")
                                {
                                    LentilCap.Text = "000";
                                }
                            }
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                GD1.DataSource = ds.Tables[1];
                                GD1.DataBind();
                            }
                            else
                            {
                                GD1.DataSource = null;
                                GD1.DataBind();
                            }
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                GD2.DataSource = ds.Tables[2];
                                GD2.DataBind();
                            }
                            else
                            {
                                GD2.DataSource = null;
                                GD2.DataBind();
                            }
                            if (ds.Tables[3].Rows.Count > 0)
                            {
                                GD3.DataSource = ds.Tables[3];
                                GD3.DataBind();
                            }
                            else
                            {
                                GD3.DataSource = null;
                                GD3.DataBind();
                            }
                            if (ds.Tables[4].Rows.Count > 0)
                            {
                                GD4.DataSource = ds.Tables[4];
                                GD4.DataBind();
                            }
                            else
                            {
                                GD4.DataSource = null;
                                GD4.DataBind();
                            }
                            if (ds.Tables[5].Rows.Count > 0)
                            {
                                GD5.DataSource = ds.Tables[5];
                                GD5.DataBind();
                            }
                            else
                            {
                                GD5.DataSource = null;
                                GD5.DataBind();
                            }
                            if (ds.Tables[6].Rows.Count > 0)
                            {
                                GD6.DataSource = ds.Tables[6];
                                GD6.DataBind();
                            }
                            else
                            {
                                GD6.DataSource = null;
                                GD6.DataBind();
                            }
                            if (ds.Tables[7].Rows.Count > 0)
                            {
                                GD7.DataSource = ds.Tables[7];
                                GD7.DataBind();
                            }
                            else
                            {
                                GD7.DataSource = null;
                                GD7.DataBind();
                            }
                            if (ds.Tables[8].Rows.Count > 0)
                            {
                                GD8.DataSource = ds.Tables[8];
                                GD8.DataBind();
                            }
                            else
                            {
                                GD8.DataSource = null;
                                GD8.DataBind();
                            }
                            if (ds.Tables[9].Rows.Count > 0)
                            {
                                GD9.DataSource = ds.Tables[9];
                                GD9.DataBind();
                            }
                            else
                            {
                                GD9.DataSource = null;
                                GD9.DataBind();
                            }
                            if (ds.Tables[10].Rows.Count > 0)
                            {
                                GD10.DataSource = ds.Tables[10];
                                GD10.DataBind();
                            }
                            else
                            {
                                GD10.DataSource = null;
                                GD10.DataBind();
                            }
                            if (ds.Tables[11].Rows.Count > 0)
                            {
                                GD11.DataSource = ds.Tables[11];
                                GD11.DataBind();
                            }
                            else
                            {
                                GD11.DataSource = null;
                                GD11.DataBind();
                            }
                            if (ds.Tables[12].Rows.Count > 0)
                            {
                                GD12.DataSource = ds.Tables[12];
                                GD12.DataBind();
                            }
                            else
                            {
                                GD12.DataSource = null;
                                GD12.DataBind();
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
        }
    }
    protected void GD2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD4_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD5_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD6_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD7_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD8_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD9_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD10_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD11_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GD12_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}
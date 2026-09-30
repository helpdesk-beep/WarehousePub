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

public partial class JointVentureScheme_Warehouse_ChoiseFillingGodown : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    private object txtRemark;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["email"] != null) && (Session["mobile"] != null))
        {
           
            if (!IsPostBack)
            {
                fillcc();
            }
        }
        else
        {
            Response.Redirect("UserReg.aspx");
        } 
    }
   
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        HiddenField hdngodownid = GridView1.Rows[e.RowIndex].FindControl("hdngodownid") as HiddenField;
        // TextBox GodownScientificcap = GridView1.Rows[e.RowIndex].FindControl("GodownScientificcap") as TextBox;
        // TextBox backcapacity = GridView1.Rows[e.RowIndex].FindControl("backcapacity") as TextBox;
        TextBox txtRemark = GridView1.Rows[e.RowIndex].FindControl("txtRemark") as TextBox;
        DropDownList ddlflag = GridView1.Rows[e.RowIndex].FindControl("ddlflag") as DropDownList;

        if (ddlflag.SelectedValue != "0")
        {
            if (txtRemark.Text != "")
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                    SqlCommand cmd = new SqlCommand("Sp_Choise_filling_New", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@regid", hdngodownid.Value);
                    cmd.Parameters.AddWithValue("@Godown_flag", ddlflag.SelectedValue);
                    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
                    cmd.Parameters.AddWithValue("@Update_by", Request.UserHostAddress);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }


                fillcc();

                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('श्रेणी Update Successfully')", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Enter Your Remark')", true);


            }
        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Pls select श्रेणी')", true);
        }

    }

    protected void fillcc()
    {

        string query1 = "select TMR.region, MD.District_Name,TMD.DepotName,TWR.Warehouse_Name,JCF.* ,case when JCF.Choice = 'A' then N'अ' when JCF.Choice = 'BR' then N'ब्रांच से रिजेक्‍ट' else N'ब ' end Cho from Tbl_JVS_Choise_Filling JCF left join tbl_MetaData_DISTRICT MD on MD.District_Id = JCF.Dist_ID left join tbl_MetaData_DEPOT TMD on TMD.BranchId = JCF.Branch_ID left join tbl_MetaData_Region TMR on TMR.Region_Id = MD.Region_ID left join tbl_WarehouseRegistration TWR on TWR.Registration_Id = JCF.Reg_ID  where  JCF.Reg_ID = '" + Session["Reg_No"].ToString()+ "'";
        SqlCommand cmd1 = new SqlCommand(query1, con);
        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1);
        GridView1.DataSource = ds1.Tables[0];
        GridView1.DataBind();
    }

    protected void searchid_Click(object sender, EventArgs e)
    {
        fillcc();
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("JointVentureSchemeApp.aspx");
    }
}

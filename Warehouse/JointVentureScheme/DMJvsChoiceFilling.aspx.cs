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
public partial class JointVentureScheme_DMJvsChoiceFilling : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlTransaction sqltran;
    DataTable Dt1 = new DataTable();
    DataSet ds1 = new DataSet();
    SqlCommand cmd = null;
    string Branch = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["UserId"] != null)
        {
           ///comment Savan  08 march 2022
            //if (!IsPostBack)
            //{
                
            //    //lbluser.Text = Session["UserName"].ToString();
            //    string query = "select BranchId,DepotName from tbl_metadata_depot where DistrictId ='" + Session["UserId"] + "'";
            //    SqlCommand cmd = new SqlCommand(query, con);
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        ddlbranchname.DataSource = ds.Tables[0];
            //        ddlbranchname.DataTextField = "DepotName";
            //        ddlbranchname.DataValueField = "BranchId";
            //        ddlbranchname.DataBind();

            //        ddlbranchname.Items.Insert(0, new ListItem("ब्रांच चुने ","0"));
            //    }
            //    fillcc();
            //}
        }
        else
        {
            Response.Redirect("logins.aspx");
        }
    }
    


    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        HiddenField hdngodownid = GridView1.Rows[e.RowIndex].FindControl("hdngodownid") as HiddenField;
        // TextBox GodownScientificcap = GridView1.Rows[e.RowIndex].FindControl("GodownScientificcap") as TextBox;
        // TextBox backcapacity = GridView1.Rows[e.RowIndex].FindControl("backcapacity") as TextBox;
        DropDownList ddlflag = GridView1.Rows[e.RowIndex].FindControl("ddlflag") as DropDownList;
        TextBox txtRemark = GridView1.Rows[e.RowIndex].FindControl("txtRemark") as TextBox;

        if (ddlflag.SelectedValue != "0")
        {
            if (txtRemark.Text != "")
            {

                try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Sp_DM_Change_Choise_filling", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@regid", hdngodownid.Value);
                cmd.Parameters.AddWithValue("@Godown_flag", ddlflag.SelectedValue);
                cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);

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
                ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Please Enter Remark')", true);
            }

        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('Please select श्रेणी')", true);
        }

    }

   
    protected void searchid_Click(object sender, EventArgs e)
    {
        fillcc();
    }

    protected void fillcc()
    {
        if (ddlbranchname.SelectedValue == "0")
        {
            string query1 = "select TMR.region, MD.District_Name,TMD.DepotName,JCF.*,case when JCF.Choice='A' then N'अ' else N'ब ' end Cho from Tbl_JVS_Choise_Filling  JCF left join tbl_MetaData_DISTRICT MD on MD.District_Id = JCF.Dist_ID left join  tbl_MetaData_DEPOT TMD on TMD.BranchId = JCF.Branch_ID left join tbl_MetaData_Region TMR on TMR.Region_Id = MD.Region_ID where JCF.Choice='A' and JCF.Dist_ID = '" + Session["UserId"] + "'";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            GridView1.DataSource = ds1.Tables[0];
            GridView1.DataBind();

        }
        else {

            string query1 = "select TMR.region, MD.District_Name,TMD.DepotName,JCF.*,case when JCF.Choice='A' then N'अ' else N'ब ' end Cho from Tbl_JVS_Choise_Filling  JCF left join tbl_MetaData_DISTRICT MD on MD.District_Id = JCF.Dist_ID left join  tbl_MetaData_DEPOT TMD on TMD.BranchId = JCF.Branch_ID left join tbl_MetaData_Region TMR on TMR.Region_Id = MD.Region_ID where JCF.Choice='A' and JCF.Branch_ID = '" + ddlbranchname.SelectedValue + "'";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            GridView1.DataSource = ds1.Tables[0];
            GridView1.DataBind();
        }
        
    }
}


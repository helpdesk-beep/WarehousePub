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
using System.Text;

public partial class JointVentureScheme_Godown_ChoiceFillingJvs : System.Web.UI.Page
{
    public String GodownID; public decimal GodownCap;

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlCommand cmd = null;
    SqlTransaction sqltran;
    string R_Phase = "";
    string Reg_season = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoStore();
		 //ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('चॉइस फिल्लिंग की सुविधा अब बंद कर दी गई हें...'); </script> ");
            //Response.Redirect("WarehouseHome.aspx");
        if (Session["Reg_No"] != null && Session["Reg_No"] != "")
        {
            R_Phase = "1";
            //Reg_season = "R2019";
            //Reg_season = "K2019";
            Reg_season = "JVS2021_22";
            if (!IsPostBack)
            {
                string strsql = "";
                
               // strsql = "select Choice AS Choices,case when Choice='A' then N'अ' else N'ब ' end Choice from Tbl_JVS_Choise_Filling where Reg_ID='" + Session["Reg_No"].ToString() + "' and Choice<>'BR'";
                strsql = "select case when Choice_2022_23 is null then Choice else Choice_2022_23 end Choise from Tbl_JVS_Choise_Filling where Reg_ID='" + Session["Reg_No"].ToString() + "' and Choice<>'BR'";
                SqlCommand cmd = new SqlCommand(strsql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    Session["Choise"] = dt.Rows[0]["Choise"].ToString();
                    if (dt.Rows[0]["Choise"].ToString() == "A")
                    {
                        RadioButton1.Checked = true;
                       
                    }
                    else if (dt.Rows[0]["Choise"].ToString() == "B")
                    {
                        RadioButton2.Checked = true;
                    }
                }
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Registration Required for Offer...'); </script> ");
            //Response.Redirect("WarehouseHome.aspx");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //string TStatus = Tcheckdatetimes();
        //if (TStatus == "Y")
        //{
        string client_IP = Request.ServerVariables["REMOTE_ADDR"].ToString();
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;

        //if (CheckBox1.Checked == true)
        //{

        if (RadioButton1.Checked == true)
        {
            RedioHiddenField.Value = "A";
        }

        if (RadioButton2.Checked == true)
        {
            RedioHiddenField.Value = "B";
        }


           // if (RedioHiddenField.Value != "" )
            if (RedioHiddenField.Value != "" && Session["Choise"].ToString()!="B")
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Update_Choice_For_Godown", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Reg_ID", Session["Reg_No"].ToString());
                    cmd.Parameters.AddWithValue("@Choice", RedioHiddenField.Value);
                    con.Open();
                    int k = cmd.ExecuteNonQuery();
                    con.Close();

                    //fill();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Data Insert Successfully'); </script> ");
                    btnsubmit.Visible = false;
                }
            }
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('B श्रेणी का चयन A में नहीं किया जा सकता हैं'); </script> ");
        }
        //}
        //else
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('चेक बॉक्स का चयन करे'); </script> ");
        //}
        //}

        //else
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Date for Choice Filling has been Finished...'); </script> ");
        //}
    }

    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
}

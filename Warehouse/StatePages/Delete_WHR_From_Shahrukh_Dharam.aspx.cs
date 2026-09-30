using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StatePages_Delete_WHR_From_Shahrukh_Dharam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() != null)
        {
            if (!IsPostBack)
            {
                
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }
    protected void fillScheduleInsp_Grid()
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_WHR_To_Delete", con))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WHRID", txttwhrno.Text.ToString());
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            Depositor_Gridview.DataSource = dt;
                            Depositor_Gridview.DataBind();
                        }
                        else
                        {
                            Depositor_Gridview.DataSource = null;
                            Depositor_Gridview.DataBind();
                        }
                    }
                }
            }
        }
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
            string hdnWhr_No = (row.FindControl("hdnWhr_No") as HiddenField).Value;           
            Session["hdnWhr_No"] = hdnWhr_No.ToString();          
            RemoveRow(hdnWhr_No.ToString());

        }
    }
    public void RemoveRow(string WHRNo)
    {
        string constr = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }

        SqlCommand cmd1 = new SqlCommand();
        try
        {
            string localIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlCommand cmd = new SqlCommand("Delete_WHR_State_One_by_one", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@WHRID", WHRNo.ToString());
            cmd.Parameters.AddWithValue("@IPAddress", localIP.ToString());       
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Deleted Successfully!');", true);
                fillScheduleInsp_Grid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Record Not Deleted!');", true);
            }

        }
        catch (Exception ex)
        {
            string strMsg2 = ex.Message.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mymsg1", "alert('" + strMsg2 + "')", true);
        }


    }
    protected void Depositor_Gridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        fillScheduleInsp_Grid();
    }
}
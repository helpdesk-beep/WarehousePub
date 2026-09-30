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

public partial class JointVentureScheme_AppealUnfitGdwn : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    public string qry = "";
    SqlCommand cmd = null;
    SqlTransaction sqltran;
    string R_Phase = "";
    string Reg_season = "";
    string regno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["email"] != null) && (Session["mobile"] != null))
        {
            R_Phase = "1";
            Reg_season = "R2019";
            if (!IsPostBack)
            {
                lbluser.Text = Session["fname"].ToString() + " " + Session["mname"].ToString() + " " + Session["lname"].ToString();
            }
        }
        else
        {
            Response.Redirect("UserReg.aspx");
        }  
    }
    private void GetunfitGodown()
    {
        try
        {
            gvGodown.DataSource = "";
            gvGodown.DataBind();
            regno = Session["Reg_No"].ToString();
            string qry = "select GOFR.Godown_ID,GOFR.Godown_No,GOFR.G_OfferCapacity, Convert(varchar(10),GOFR.Offer_Date,103) as Offer_Date,GOFR.G_Scheme,INSP.Fit_Unfit,Convert(varchar(10),INSP.Insp_Date,103) as Insp_Date,GOFR.Godown_Offer_Id,INSP.Inspection_Id from tbl_Warehouse_Godown_Offer_2019 as GOFR inner join tbl_Godown_Inspection as INSP on GOFR.Godown_Offer_Id=INSP.Godown_Offer_Id where GOFR.Registration_Id='" + regno + "' and Fit_Unfit='UNFIT' AND INSP.CreatedDate>CONVERT(VARCHAR(10),'02/17/2019',101) and DATEADD(DAY, 5, INSP.CreatedDate)>GETDATE() and INSP.Inspection_Id not in (select distinct APL.Inspection_ID from tbl_Godown_Appeal as APL where APL.Registration_ID='" + regno + "'  ) order by GOFR.Godown_No";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGodown.DataSource = ds;
                gvGodown.DataBind();
                this.gvGodown.Columns[0].Visible = false;
                this.gvGodown.Columns[7].Visible = false;
                this.gvGodown.Columns[8].Visible = false;
            }
            else
            {
                gvGodown.DataSource = "";
                gvGodown.DataBind();
                trUnfit.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch(Exception ex)
        {

        }
    }

    private void GetShemeGodown()
    {
        try
        {
            regno = Session["Reg_No"].ToString();
            string qry = "select GOFR.Godown_ID,GOFR.Godown_No,GOFR.G_OfferCapacity, Convert(varchar(10),GOFR.Offer_Date,103) as Offer_Date,GOFR.G_Scheme,INSP.Insp_Offer_Scheme,INSP.Fit_Unfit,Convert(varchar(10),INSP.Insp_Date,103) as Insp_Date,GOFR.Godown_Offer_Id from tbl_Warehouse_Godown_Offer_2019 as GOFR inner join tbl_Godown_Inspection as INSP on GOFR.Godown_Offer_Id=INSP.Godown_Offer_Id where  Fit_Unfit='FIT' AND INSP.CreatedDate>CONVERT(VARCHAR(10),'02/17/2019',101) and G_Scheme!=Insp_Offer_Scheme and GOFR.Registration_Id='" + regno + "' order by GOFR.Godown_No";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                this.GridView1.Columns[0].Visible = false;
                this.GridView1.Columns[8].Visible = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('No Data Found ')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ckstack_CheckedChanged(object sender, EventArgs e)
    {
        int s;
        int Count_Rows;
        int chkcount = 0;
        string GdwnID;
        regno = Session["Reg_No"].ToString();
        Count_Rows = gvGodown.Rows.Count;
        for (s = 0; s < gvGodown.Rows.Count; s++)
        {
                if (((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == true)
                {
                    chkcount = chkcount+1;
                    GdwnID = gvGodown.Rows[s].Cells[0].Text.ToString();
                    regno = Session["Reg_No"].ToString();
                    qry = "select INSP.Fit_Unfit,Convert(varchar(10),INSP.Insp_Date,103) as Insp_Date ,case when IsWare_Under_Constr=1 then 'Yes' else 'No' end IsWare_Under_Constr, case when IsWare_Disputed=1 then 'Yes' else 'No' end IsWare_Disputed,case when IsWare_Damage=1 then 'Yes' else 'No' end IsWare_Damage,case when IsWareS_Comm_NGovt=1 then 'Yes' else 'No' end IsWareS_Comm_NGovt,case when IsWare_BlackListed=1 then 'Yes' else 'No' end IsWare_BlackListed,case when IsOnline_Info_Wrong=1 then 'Yes' else 'No' end IsOnline_Info_Wrong, case when IsMandatory_Condi_NAvail=1 then 'Yes' else 'No' end IsMandatory_Condi_NAvail,case when IsWH_CplessthenFiveHundred=1 then 'Yes' else 'No' end  IsWH_CplessthenFiveHundred ,Remark from tbl_Godown_Inspection as INSP where INSP.Registration_Id='" + regno + "' and GodownId='" + GdwnID + "' and Fit_Unfit='UNFIT' ";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        lblnirman.Text = dt.Rows[0]["IsWare_Under_Constr"].ToString();
                        lblvivad.Text = dt.Rows[0]["IsWare_Disputed"].ToString();
                        lblchatigrast.Text = dt.Rows[0]["IsWare_Damage"].ToString();
                        lblpurwaskandh.Text = dt.Rows[0]["IsWareS_Comm_NGovt"].ToString();
                        lblblack.Text = dt.Rows[0]["IsWare_BlackListed"].ToString();
                        lblnottake.Text = dt.Rows[0]["IsWH_CplessthenFiveHundred"].ToString();
                        lbldanege.Text = dt.Rows[0]["IsMandatory_Condi_NAvail"].ToString();
                        lblwrongdetail.Text = dt.Rows[0]["IsOnline_Info_Wrong"].ToString();
                        lblremark.Text = dt.Rows[0]["Remark"].ToString();
                    }
                    for (int j = 0; j < gvGodown.Rows.Count; j++)
                    {
                        if (s != j)
                        {
                            ((CheckBox)gvGodown.Rows[j].FindControl("ckstack")).Checked = false;
                        }
                    }
                }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("UserReg.aspx");
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        trUnfit.Visible = true;
        trscheme.Visible = false;
        GetunfitGodown();
        
    }
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        trUnfit.Visible = false;
        GetShemeGodown();
        trscheme.Visible = true;
    }

    protected void chkschemegdwn_CheckedChanged(object sender, EventArgs e)
    {
        int s;
        int Count_Rows;
        Count_Rows = GridView1.Rows.Count;
        for (s = 0; s < GridView1.Rows.Count; s++)
        {
            if (((CheckBox)GridView1.Rows[s].FindControl("chkschemegdwn")).Checked == true)
            {
                qry = "select case when [Elec_Weigh_Avail]=1 then 'Yes' else 'No' end Elec_Weigh_Avail, case when G_Gates_Condition=1 then 'Yes' else 'No' end G_Gates_Condition,case when [RoadType]=1 then 'BT' when [RoadType]=2 then 'CC'  when [RoadType]=3 then 'WBM'  else 'Other' end [RoadType], case when W_BoundaryType=1 then 'Boundary Wall' when W_BoundaryType=2 then 'channeling Fencing'  when W_BoundaryType=4 then 'Barbed Wire Fancing'  else 'Other' end W_BoundaryType from tbl_Godown_Inspection as INSP where INSP.CreatedDate>CONVERT(VARCHAR(10),'02/17/2019',101) and INSP.Registration_Id='" + Session["Reg_No"].ToString() + "' and GodownId='" + GridView1.Rows[s].Cells[0].Text.ToString() + "' and Godown_Offer_Id='" + GridView1.Rows[s].Cells[8].Text.ToString() + "'  and Fit_Unfit='FIT'";
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblgate.Text = dt.Rows[0]["G_Gates_Condition"].ToString();
                    lblWeibrige.Text = dt.Rows[0]["Elec_Weigh_Avail"].ToString();
                    lblboundry.Text = dt.Rows[0]["W_BoundaryType"].ToString();
                    lblroadtype.Text = dt.Rows[0]["W_BoundaryType"].ToString();
                }
            }
        }
    }
    protected void btnUnfit_Click(object sender, EventArgs e)
    {
        if (RadioButton1.Checked == true)
        {
            if (txtRemark.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Discription For Appeal')", true);
            }
            else
            {
                int s;
                int Count_Rows;
                string GdwnID;
                regno = Session["Reg_No"].ToString();
                Count_Rows = gvGodown.Rows.Count;
                string ClientIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                for (s = 0; s < gvGodown.Rows.Count; s++)
                {
                    if (((CheckBox)gvGodown.Rows[s].FindControl("ckstack")).Checked == true)
                    {
                        GdwnID = gvGodown.Rows[s].Cells[0].Text.ToString();
                        regno = Session["Reg_No"].ToString();
                        qry = "INSERT INTO [JointVentureScheme2018].[dbo].[tbl_Godown_Appeal] ([Inspection_ID],[Godown_Offer_ID],[Registration_ID],[Godown_ID],[Appeal_Type],[Appeal_Remark],[CreatedBy],[CreatedDate],[Appeal_Status]) VALUES ('"+ gvGodown.Rows[s].Cells[8].Text.ToString() +"','"+ gvGodown.Rows[s].Cells[7].Text.ToString() +"','"+ regno +"','"+ gvGodown.Rows[s].Cells[0].Text.ToString() +"','UNFIT',N'"+ txtRemark.Value.Trim() +"','"+ ClientIP +"',GETDATE(),'A')";
                        con.Open();
                        SqlCommand cmd = new SqlCommand(qry, con);
                        int a = cmd.ExecuteNonQuery();
                        con.Close();
                        if (a==1)
                        {
                            ModalPopupExtender1.Show();
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Submit Appeal')", true);
                            //RadioButton1_CheckedChanged(null, null);
                            //lblnirman.Text = "";
                            //lblvivad.Text = "";
                            //lblchatigrast.Text = "";
                            //lblpurwaskandh.Text = "";
                            //lblblack.Text = "";
                            //lblwrongdetail.Text = "";
                            //lbldanege.Text = "";
                            //lblnottake.Text = "";
                            //lblremark.Text = "";
                        }
                    }
                }
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("WarehouseHome.aspx");
    }

}


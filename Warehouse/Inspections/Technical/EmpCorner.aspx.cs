using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmpCorner : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
    public SqlConnection conStr = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillInpOff_Grid();
            fillInsecticide();
            fillInsecticideRO();
            fillInsecticideBO();
        }
    }

    protected void fillInsecticide()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Insecticide_HO_Entry_Transfer_Details_For_dashboard", con))
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
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblAPHOE.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["HOE_OBQ_AP"].ToString()).ToString();
                                lblHOTRM.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["RMT_OBQ_AP"].ToString()).ToString();
                                lblHOBalance.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Balance_AP"].ToString()).ToString();
                            }
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                lblHOEMelaphion.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["HOE_OBQ_M"].ToString()).ToString();
                                lblHOTRMMelaphion.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["RMT_OBQ_M"].ToString()).ToString();
                                lblBalanceMelaphion.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["Balance_M"].ToString()).ToString();
                            }
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                lblHOEDeltamethrin.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["HOE_OBQ_D"].ToString()).ToString();
                                lblHOTRMDeltamethrin.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["RMT_OBQ_D"].ToString()).ToString();
                                lblHOBDeltamethrin.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["Balance_D"].ToString()).ToString();
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

    protected void fillInsecticideRO()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Insecticide_RO_Entry_Transfer_Details_For_dashboard", con))
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
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblAPHOE1.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["HOE_OBQ_AP"].ToString()).ToString();
                                lblHOTRM1.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["RMT_OBQ_AP"].ToString()).ToString();
                                lblHOBalance1.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Balance_AP"].ToString()).ToString();
                            }
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                lblHOEMelaphion1.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["HOE_OBQ_M"].ToString()).ToString();
                                lblHOTRMMelaphion1.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["RMT_OBQ_M"].ToString()).ToString();
                                lblBalanceMelaphion1.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["Balance_M"].ToString()).ToString();
                            }
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                lblHOEDeltamethrin1.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["HOE_OBQ_D"].ToString()).ToString();
                                lblHOTRMDeltamethrin1.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["RMT_OBQ_D"].ToString()).ToString();
                                lblHOBDeltamethrin1.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["Balance_D"].ToString()).ToString();
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

    protected void fillInsecticideBO()
    {
        string constr = ConfigurationManager.ConnectionStrings["JVSGodownConString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("Get_Insecticide_Branch_Receving_Transfer_Details_For_dashboard", con))
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
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                lblOB1.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Opening_Balance_quantity"].ToString()).ToString();
                                lblRI1.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Receipt_Balance_quantity"].ToString()).ToString();
                                lblCon1.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["CBQ1"].ToString()).ToString();
                                lblt1.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["TBQ"].ToString()).ToString();
                                lblBalance1.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["Quantity"].ToString()).ToString();
                            }
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                lblOB2.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["Opening_Balance_quantity"].ToString()).ToString();
                                lblRI2.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["Receipt_Balance_quantity"].ToString()).ToString();
                                lblCon2.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["CBQ1"].ToString()).ToString();
                                lblt2.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["TBQ"].ToString()).ToString();
                                lblbalalnce2.Text = Convert.ToDecimal(ds.Tables[1].Rows[0]["Quantity"].ToString()).ToString();
                            }
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                lblOB3.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["Opening_Balance_quantity"].ToString()).ToString();
                                lblRI3.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["Receipt_Balance_quantity"].ToString()).ToString();
                                lblCon3.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["CBQ1"].ToString()).ToString();
                                lblt3.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["TBQ"].ToString()).ToString();
                                lblbalance3.Text = Convert.ToDecimal(ds.Tables[2].Rows[0]["Quantity"].ToString()).ToString();
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
    public void fillInpOff_Grid()
    {
        try
        {
            if (conStr.State == ConnectionState.Closed)
            {
                conStr.Open();

            }
            SqlCommand cmd = new SqlCommand("Get_Total_Complite_Pending_Insp", conStr);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (conStr.State == ConnectionState.Open)
            { conStr.Close(); }

            if (dt.Rows.Count > 0)
            {
                lbltiarm.Text = dt.Rows[0]["Totalallotedinspection"].ToString();
                lblinspdone.Text = dt.Rows[0]["totalcompliteinspection"].ToString();
                lblpendinginsp.Text = dt.Rows[0]["totalPendinginspection"].ToString();
                lblgi.Text = dt.Rows[0]["GI"].ToString();
                lblpvi.Text = dt.Rows[0]["PVI"].ToString();
                lblboth.Text = dt.Rows[0]["Both"].ToString();

                lblgic.Text = dt.Rows[0]["GIC"].ToString();
                lblpviC.Text = dt.Rows[0]["PVIC"].ToString();
                lblbothc.Text = dt.Rows[0]["BothC"].ToString();

                lblgip.Text = dt.Rows[0]["GIP"].ToString();
                lblpviP.Text = dt.Rows[0]["PVIP"].ToString();
                lblbothP.Text = dt.Rows[0]["BothP"].ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('" + ex.Message + "')", true);
        }
        finally
        { if (conStr.State == ConnectionState.Open) { conStr.Close(); } }
    }
}

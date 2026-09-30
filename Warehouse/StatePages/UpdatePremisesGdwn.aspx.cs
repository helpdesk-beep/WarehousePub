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

public partial class StatePages_UpdatePremisesGdwn : System.Web.UI.Page
{
    SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString.ToString());
    string qry = "";
    string sts = "";
    string str3 = "";
    string P_Godown_ID = "";
    SqlCommand cmd = null;
    string PREMISES_ID = "";
    String bmname = "";
    DataSet ds = null;
    SqlDataAdapter da = null;
    string p_gdwn_id = "";
    string MaxId = "";
    string prmHired = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"].ToString() == "MPSWLC" || Session["UserName"].ToString() == "Business(MPWLC)")
        {
            try
            {
                if (!IsPostBack)
                {
                    fillDistrict();
                    PrefillDistrict();
                }
            }
            catch (Exception ex)
            {
                Page.RegisterClientScriptBlock("mymsg2", "<script language=javascript> alert('Some error has occured, try again'); </script> ");
            }
        }
        else
        {
            Response.Redirect("~/SessionExpired.htm");
        }
    }

    private void fillDistrict()
    {
        try
        {
            string query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_Id";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, "---Select---");
            }
            else
            {
            }
        }
        catch (Exception)
        {
        }
    }

    private void getDepot(string distId)
    {
        try
        {
            string query = "";
            query = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' order by DepotName asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepotList.DataSource = ds.Tables[0];
                ddlDepotList.DataTextField = "DepotName";
                ddlDepotList.DataValueField = "BranchId";
                ddlDepotList.DataBind();
                ddlDepotList.Items.Insert(0, "---Select---");
            }
            else
            {
                ddlDepotList.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception)
        {
        }
    }
    private void getDepotbmname()
    {
        try
        {
            string query = "";
            query = "select NodalOfficeName from tbl_MetaData_DEPOT where BranchId='" + ddlDepotList.SelectedValue.ToString() + "' order by DepotName asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
               gdwnAPN.Text = ds.Tables[0].Rows[0]["NodalOfficeName"].ToString();
            }
            else
            {

            }
        }
        catch (Exception)
        {
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex != 0)
        {
            getDepot(ddlDistrict.SelectedValue.ToString());
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select District')", true);
        }
    }

    private void GetGodown()
    {
        try
        {
            gv.DataSource = null;
            gv.DataBind();
            string BranchId = ddlDepotList.SelectedValue.ToString();
            string DistrictId = ddlDistrict.SelectedValue.ToString();
            string qry = "select P_Godown_Id,Godown_Name,GodownNum,Godown_Capacity,Godown_Scientific_Capacity,MDG.Hired_Type,Closing_Balance,Vacant_Capacity,Premises_Name,Premises_No,MDP.Premises_Id,Godown_ID,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID=MDG.Godown_ID)as Whr_Name,Closing_Balance_Sep2017 from tbl_MetaData_GODOWN_2017 as MDG join tbl_MetaData_Premises as MDP on MDP.Premises_Id=MDG.Premises_Id where MDG.BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and MDG.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["dsGodown"] = ds;
                gv.DataSource = ds.Tables[0];
                gv.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void ddlDepotList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
        }
        else if (ddlDepotList.SelectedIndex != 0)
        {
            GetGodown();
            Panel1.Visible = false;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch First..')", true);
        }
    }

    private void fillGrid(DataSet ds)
    {
        gv.DataSource = ds;
        gv.DataBind();
    }
    protected void Newgdwn_btn_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDistrict.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District First..')", true);
            }
            else if (ddlDepotList.SelectedIndex != 0)
            {
                btnupdate.Text = "Save";
                if (btnupdate.Text == "Save")
                {
                    clrdata();
                    Panel1.Visible = true;
                    header_addgdwntxt.Text = "Add New Godown";
                    fillPremises();
                    GetgodownsWlc();
                    
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branch First..')", true);
            }
        }
        catch(Exception)
        {

        }
    }
    private void fillPremises()
    {
        try
        {
            if (Session["UserName"].ToString() == "MPSWLC" || Session["UserName"].ToString() == "Business(MPWLC)")
            {
                string query = "select MP.Premises_Name+'-'+MP.Premises_No as Premises,MP.Premises_Id from tbl_MetaData_Premises as MP where MP.BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and MP.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlPremises.DataSource = ds.Tables[0];
                    ddlPremises.DataTextField = "Premises";
                    ddlPremises.DataValueField = "Premises_Id";
                    ddlPremises.DataBind();
                    ddlPremises.Items.Insert(0, "--Select--");
                }
                else
                {
                }
            }
        }
        catch (Exception)
        {
        }
    }
    protected void ddlPremises_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void GetgodownsWlc()
    {
        try
        {
            gdwn_name_softtxt.DataSource = null;
            gdwn_name_softtxt.DataBind();
            string str = "select Godown_Name,Godown_id from tbl_MetaData_GODOWN where BranchId='" + ddlDepotList.SelectedValue.ToString() + "' and DistrictId = '" + ddlDistrict.SelectedValue.ToString() + "' and Remarks='Y' order by Godown_Name ";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gdwn_name_softtxt.DataSource = ds.Tables[0];
                gdwn_name_softtxt.DataTextField = "Godown_Name";
                gdwn_name_softtxt.DataValueField = "Godown_id";
                gdwn_name_softtxt.DataBind();
                gdwn_name_softtxt.Items.Insert(0, "--Select--");
            }
            else
            {

            }
        }
        catch(Exception)
        {
        }
    }

    protected void gv_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            clrdata();
            btnupdate.Text = "Update";
            if (btnupdate.Text == "Update")
            {
                header_addgdwntxt.Text = "Update Godown";
                Panel1.Visible = true;
                fillPremises();
                GetgodownsWlc();
                p_gdwn_id = gv.SelectedRow.Cells[2].Text;
                P_GdwnID.Text = gv.SelectedRow.Cells[2].Text;
                string query = "select P_Godown_Id,Godown_Name,GodownNum,Godown_Capacity,Godown_Scientific_Capacity,Godown_APN,MDG.Hired_Type,Closing_Balance,Vacant_Capacity,Premises_Name,Premises_No,MDP.Premises_Id,Godown_ID,(select Godown_Name from tbl_MetaData_GODOWN where Godown_ID=MDG.Godown_ID)as Whr_Name,Closing_Balance_Sep2017 from tbl_MetaData_GODOWN_2017 as MDG join tbl_MetaData_Premises as MDP on MDP.Premises_Id=MDG.Premises_Id where MDG.BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and MDG.DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' and MDG.P_Godown_Id='" + P_GdwnID.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gdwnnamenewtxt.Text = ds.Tables[0].Rows[0]["Godown_Name"].ToString();
                    gdwnnonewtxt.Text = ds.Tables[0].Rows[0]["GodownNum"].ToString();
                    gdwncpttxt.Text = ds.Tables[0].Rows[0]["Godown_Capacity"].ToString();
                    gdwnsctcpttxt.Text = ds.Tables[0].Rows[0]["Godown_Scientific_Capacity"].ToString();
                    ddlPremises.SelectedValue = ds.Tables[0].Rows[0]["Premises_Id"].ToString();
                    PreClosing.Text = ds.Tables[0].Rows[0]["Closing_Balance"].ToString();
                    closingbaltxt.Text = ds.Tables[0].Rows[0]["Closing_Balance_Sep2017"].ToString();
                    gdwnAPN.Text = ds.Tables[0].Rows[0]["Godown_APN"].ToString();
                    var gdwnid = ds.Tables[0].Rows[0]["Godown_ID"].ToString();
                    try
                    {
                        if (gdwnid != "")
                        {
                            gdwn_name_softtxt.SelectedValue = ds.Tables[0].Rows[0]["Godown_ID"].ToString();
                        }
                        else
                        {
                            
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Filling Godown Name In List Error...')", true);
                    }
                }
                else
                {
                }
            }
        }
        catch(Exception)
        {

        }
    }

    private void clrdata()
    {
        gdwnnamenewtxt.Text = "";
        gdwnnonewtxt.Text = "";
        gdwncpttxt.Text = "";
        gdwnsctcpttxt.Text = "";
        ddlPremises.Items.Clear();
        closingbaltxt.Text = "";
        PreClosing.Text = "";
        gdwn_name_softtxt.Items.Clear();
        ddlhiredtype.SelectedIndex = -1;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StatePages/StateWelcome.aspx");
    }
    protected void gdwn_name_softtxt_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_gdid.Text = gdwn_name_softtxt.SelectedItem.Value.ToString();
    }

    protected void ddlhiredtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        gdwnnamenewtxt.Text = ddlhiredtype.SelectedItem.ToString();
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlPremises.SelectedIndex != 0 && gdwncpttxt.Text != "" && closingbaltxt.Text != "" && gdwnnamenewtxt.Text != "" && ddlhiredtype.SelectedIndex!=0 && gdwnnonewtxt.Text!="")
            {
                var gdwnid = "";
                var Mapstatus = "";
                if (gdwn_name_softtxt.SelectedIndex != 0)
                {
                    gdwnid = gdwn_name_softtxt.SelectedValue.ToString();
                    Mapstatus = "Y";
                }
                else
                {
                    gdwnid = "";
                    Mapstatus = "N";
                }
                string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string ornm = ddlPremises.SelectedItem.ToString();
                string[] splittedArray = ornm.Split('-');
                string orgenizationname = splittedArray[0].ToString();
                Double vcntcpt = Convert.ToDouble(gdwncpttxt.Text) - Convert.ToDouble(closingbaltxt.Text);
                if (vcntcpt < 0)
                {
                    vcntcpt = 0.00;
                }
                if (btnupdate.Text == "Save")
                {
                    GetGodownId();
                    if (ddlPremises.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Premises...')", true);
                    }
                    else if (gdwnnamenewtxt.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Godown Name...')", true);
                    }
                    else if (gdwnnonewtxt.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Godown No...')", true);
                    }
                    else if (ddlhiredtype.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Hired Type...')", true);
                    }
                    else if (gdwnnonewtxt.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Godown No...')", true);
                    }
                    else if (gdwncpttxt.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Godown Capacity...')", true);
                    }
                    else if (gdwnsctcpttxt.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Godown Scientific Capacity...')", true);
                    }
                    else if (closingbaltxt.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Enter Closing Balance...')", true);
                    }
                    else
                    {
                        string query = "INSERT INTO [tbl_MetaData_GODOWN_2017] ([Godown_ID],[StateId],[DistrictId],[DepotId],[Godown_Name],[Godown_Capacity],[CreatedBy],[CreatedDate],[Hired_Type],[Storage_Type],[Godown_Scientific_Capacity],[Godown_APN],[BranchID],[GodownNum],[Org_Name],[IsActive],[Premises_Id],[Closing_Balance],[Vacant_Capacity],[P_Godown_Id],[GID],[Closing_Balance_Sep2017],[Mapping_Status]) VALUES ('" + gdwnid + "','23','" + ddlDistrict.SelectedValue.ToString() + "','" + ddlDepotList.SelectedValue.ToString() + "','" + gdwnnamenewtxt.Text + "','" + gdwncpttxt.Text + "','" + ip + "',getdate(),'" + ddlhiredtype.SelectedItem.ToString() + "','" + ddlstoragetype.SelectedItem.ToString() + "','" + gdwnsctcpttxt.Text + "','" + gdwnAPN.Text + "','" + ddlDepotList.SelectedValue.ToString() + "','" + gdwnnonewtxt.Text + "','" + orgenizationname + "','Y','" + ddlPremises.SelectedValue.ToString() + "','" + PreClosing.Text + "','" + vcntcpt + "','" + P_Godown_ID + "','" + MaxId + "','" + closingbaltxt.Text + "','" + Mapstatus + "')";
                        SqlCommand cmdP = new SqlCommand(query, con);
                        con.Open();
                        int a = cmdP.ExecuteNonQuery();
                        if (a > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Godown Add...')", true);
                        }
                        con.Close();
                    }
                }

                else if (btnupdate.Text == "Update")
                {
                    if (ddlhiredtype.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Select Hired Type...')", true);
                    }
                    else
                    {
                        string query = "INSERT INTO tbl_MetaData_GODOWN_2017_log SELECT *  FROM tbl_MetaData_GODOWN_2017 WHERE P_Godown_Id='" + P_GdwnID.Text + "' and BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and DistrictId='" + ddlDistrict.SelectedValue.ToString() + "'";
                        SqlCommand cmdP = new SqlCommand(query, con);
                        con.Open();
                        int a = cmdP.ExecuteNonQuery();
                        if (a > 0)
                        {
                            con.Close();
                            string qrye = "UPDATE [tbl_MetaData_GODOWN_2017] SET [Godown_ID]='" + gdwnid + "',[Godown_Name]='" + gdwnnamenewtxt.Text + "',[Godown_Capacity]='" + gdwncpttxt.Text + "',[UpdatedBy]='" + ip + "',[UpdatedDate]=getdate(),[Hired_Type]='" + ddlhiredtype.SelectedItem.ToString() + "',[Storage_Type]='" + ddlstoragetype.SelectedItem.ToString() + "',[Godown_Scientific_Capacity]='" + gdwnsctcpttxt.Text + "',[Godown_APN]='" + gdwnAPN.Text + "',[GodownNum]='" + gdwnnonewtxt.Text + "',[Org_Name]='" + orgenizationname + "',[Premises_Id]='" + ddlPremises.SelectedValue.ToString() + "',[Vacant_Capacity]='" + vcntcpt + "',[Closing_Balance_Sep2017]='" + closingbaltxt.Text + "',[Mapping_Status]='"+Mapstatus+"' WHERE P_Godown_Id='" + P_GdwnID.Text + "' AND BranchID='" + ddlDepotList.SelectedValue.ToString() + "' AND DistrictId='" + ddlDistrict.SelectedValue.ToString() + "'";
                            SqlCommand cmd = new SqlCommand(qrye, con);
                            con.Open();
                            int B = cmd.ExecuteNonQuery();
                            if (B > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Update Godown...')", true);
                            }
                            con.Close();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error...')", true);
                        }

                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Fill Details ...')", true);
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            con.Close();
        }
    }
    public void GetGodownId()
    {
        try
        {
            string qry = "select Max(GID) from tbl_MetaData_GODOWN_2017 where BranchId='" + ddlDepotList.SelectedValue.ToString() + "' and DistrictId='" + ddlDistrict.SelectedValue.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            string str3 = cmd.ExecuteScalar().ToString();
            con.Close();
            if ((str3 == String.Empty) || str3 == "")
            {
                str3 = "0";
            }
            MaxId = Convert.ToString(Convert.ToInt64(str3) + 1);
            PREMISES_ID = ddlPremises.SelectedValue.ToString();
            P_Godown_ID = PREMISES_ID + MaxId;
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Error for Premises Godown ID ...')", true);
        }

    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int _rowindex = e.RowIndex;
            P_GdwnID.Text = gv.DataKeys[_rowindex].Value.ToString();
            string query = "INSERT INTO tbl_MetaData_GODOWN_2017_log SELECT *  FROM tbl_MetaData_GODOWN_2017 WHERE P_Godown_Id='" + P_GdwnID.Text + "' and BranchID='" + ddlDepotList.SelectedValue.ToString() + "' and DistrictId='" + ddlDistrict.SelectedValue.ToString() + "'";
            SqlCommand cmdP = new SqlCommand(query, con);
            con.Open();
            int a = cmdP.ExecuteNonQuery();
            if (a > 0)
            {
                con.Close();
                string qrye = "Delete from [tbl_MetaData_GODOWN_2017] WHERE P_Godown_Id='" + P_GdwnID.Text + "' AND BranchID='" + ddlDepotList.SelectedValue.ToString() + "' AND DistrictId='" + ddlDistrict.SelectedValue.ToString() + "'";
                SqlCommand cmd = new SqlCommand(qrye, con);
                con.Open();
                int B = cmd.ExecuteNonQuery();
                if (B > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Delete Godown...')", true);
                }
                con.Close();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error For Deleting...')", true);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }

    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StatePages/StateWelcome.aspx");
    }

    //Premises Related code here
    //Premises Related code here


    private void PrefillDistrict()
    {
        try
        {
            string query = "SELECT [District_Id],[District_Name] FROM [tbl_MetaData_DISTRICT] order by District_Name asc";
            cmd = new SqlCommand(query, con);
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                prm_ddldist.Items.Clear();
                prm_ddldist.DataSource = ds.Tables[0];
                prm_ddldist.DataTextField = "District_Name";
                prm_ddldist.DataValueField = "District_Id";
                prm_ddldist.DataBind();
                prm_ddldist.Items.Insert(0, "--Select--");
            }
            else
            {
            }
        }
        catch (Exception)
        {
        }
    }

    private void Pre_getDepot(string distId)
    {
        try
        {
            string query = "";
            query = "select DepotName,BranchId from tbl_MetaData_DEPOT where DistrictId='" + prm_ddldist.SelectedValue.ToString() + "' order by DepotName asc";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                prm_ddlbranch.DataSource = ds.Tables[0];
                prm_ddlbranch.DataTextField = "DepotName";
                prm_ddlbranch.DataValueField = "BranchId";
                prm_ddlbranch.DataBind();
                prm_ddlbranch.Items.Insert(0, "--Select--");
            }
            else
            {
                prm_ddlbranch.Items.Insert(0, "---Select---");
            }
        }
        catch (Exception)
        {
        }
    }

    public void gv_GetPremisesDtl()
    {
        try
        {
            prm_gv.DataSource = null;
            prm_gv.DataBind();
            string qry = "select Premises_Name,PRM.Premises_Id,Premises_No,Owner,PRM.Org_Name,COUNT(PGDWN.P_Godown_Id) as No_of_GDWN from tbl_MetaData_Premises as PRM left Join tbl_MetaData_GODOWN_2017 as PGDWN on PRM.Premises_Id=PGDWN.Premises_Id where PRM.BranchID='" + prm_ddlbranch.SelectedValue.ToString() + "' and PRM.DistrictId='" + prm_ddldist.SelectedValue.ToString() + "' group by Premises_Name,PRM.Premises_Id,Premises_No,Owner,PRM.Org_Name";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["dsGodown"] = ds;
                prm_gv.DataSource = ds.Tables[0];
                prm_gv.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }

    protected void prm_ddldist_SelectedIndexChanged(object sender, EventArgs e)
    {
        Pre_getDepot(prm_ddldist.SelectedValue.ToString());
    }
    protected void prm_ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_GetPremisesDtl();
        Panel4.Visible = false;
        clearprmdata();
    }

    protected void prm_newbtn_Click(object sender, EventArgs e)
    {
        if (prm_ddldist.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select District...')", true);
        }
        else if (prm_ddlbranch.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Branhc...')", true);
        }
        else
        {
            Panel4.Visible = true;
        }
    }

    public void prm_srchprmid()
    {
        try
        {
            str3 = "";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string QueryMax = "select isnull(Max(Pid),0)+1 from tbl_MetaData_Premises where DistrictId='" + prm_ddldist.SelectedValue.ToString() + "' and BranchID='" + prm_ddlbranch.SelectedValue.ToString() +"'";
            SqlCommand cmd = new SqlCommand(QueryMax, con);
            str3 = cmd.ExecuteScalar().ToString();
            if ((str3 == String.Empty) || str3 == "")
            {
                str3 = "0";
            }
            if (Convert.ToInt64(str3) != 0)
            {
                PREMISES_ID = "P" + prm_ddlbranch.SelectedValue.ToString() + Convert.ToString(Convert.ToInt64(str3));
            }
            else
            {
                PREMISES_ID = "P" + prm_ddlbranch.SelectedValue.ToString() + Convert.ToString(Convert.ToInt64(str3));
            }
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
        }
        finally
        {
            con.Close();
        }
    }
    protected void btn_prmsave_Click(object sender, EventArgs e)
    {
        try
        {
            prm_srchprmid();
            string ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (prm_hiretype.SelectedIndex == 3)
            {
                prmHired = "P";
            }
            else if (prm_hiretype.SelectedIndex != 3 && prm_hiretype.SelectedIndex != 0)
            {
                prmHired = "G";
            }
            if (prm_hiretype.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Select Hire Type...')", true);
            }
            else  if (Prm_nametxt.Text== "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Fill Premises Name...')", true);
            }
            else if (Prm_notxt.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Fill Premises Number...')", true);
            }
            else if (Prm_ownertxt.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Fill Premises Owner ...')", true);
            }
            else if (Prm_orgnametxt.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Please Fill Premises Organization Name...')", true);
            }
            else
            {
                string query = "INSERT INTO [tbl_MetaData_Premises] ([Premises_Id],[Premises_No],[Premises_Name],[DistrictId],[BranchID],[Owner],[Org_Name],[IsActive],[CreatedBy],[CreatedDate],[Pid],[Premises_Type]) VALUES ('" + PREMISES_ID + "','" + Prm_notxt.Text + "','" + Prm_nametxt.Text + "','" + prm_ddldist.SelectedValue.ToString() + "','" + prm_ddlbranch.SelectedValue.ToString() + "','" + Prm_ownertxt.Text + "','" + Prm_orgnametxt.Text + "','Y','" + ip + "',getdate(),'" + str3 + "','" + prmHired + "')";
                SqlCommand cmdP = new SqlCommand(query, con);
                con.Open();
                int a = cmdP.ExecuteNonQuery();
                if (a > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Premises Add...')", true);
                }
            }
        }
        catch(Exception)
        {
        }
        con.Close();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Panel3.Visible = false;
        Panel2.Visible = true;
    }
    protected void prm_hiretype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (prm_hiretype.SelectedIndex != 3 && prm_hiretype.SelectedIndex!=0)
        {
            string ornm = prm_hiretype.SelectedItem.ToString();
            string[] splittedArray = ornm.Split('-');
            string orgenizationname = splittedArray[0].ToString();
            Prm_nametxt.Text = Prm_ownertxt.Text = Prm_orgnametxt.Text = orgenizationname;
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StatePages/StateWelcome.aspx");
    }

    private void clearprmdata()
    {
        Prm_nametxt.Text = "";
        Prm_notxt.Text = "";
        Prm_ownertxt.Text = "";
        Prm_orgnametxt.Text = "";
        prm_hiretype.SelectedIndex = -1;
    }
    protected void prm_gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var prm_id="";
        try
        {
            int _rowindex = e.RowIndex;
            prm_id = prm_gv.DataKeys[_rowindex].Value.ToString();
            string query1 = "select * from tbl_MetaData_GODOWN_2017 where Godown_ID !='' and  Premises_Id ='" + prm_id + "'  and BranchID= '" + prm_ddlbranch.SelectedValue.ToString() + "' and DistrictId= '" + prm_ddldist.SelectedValue.ToString() + "'";
            SqlCommand cmdP1 = new SqlCommand(query1, con);
            SqlDataAdapter da = new SqlDataAdapter(cmdP1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                con.Close();
                string query2 = "insert into tbl_MetaData_GODOWN_2017_log select * from tbl_MetaData_GODOWN_2017 where Premises_Id ='" + prm_id + "'  and BranchID= '" + prm_ddlbranch.SelectedValue.ToString() + "' and DistrictId= '" + prm_ddldist.SelectedValue.ToString() + "'";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                con.Open();
                int a2 = cmd2.ExecuteNonQuery();
                if (a2 > 0)
                {
                    con.Close();
                    string query3 = "insert into tbl_MetaData_Premises_log select * from tbl_MetaData_Premises where Premises_Id ='" + prm_id + "'  and BranchID= '" + prm_ddlbranch.SelectedValue.ToString() + "' and DistrictId= '" + prm_ddldist.SelectedValue.ToString() + "'";
                    SqlCommand cmd3 = new SqlCommand(query3, con);
                    con.Open();
                    int a3 = cmd3.ExecuteNonQuery();
                    if (a3 > 0)
                    {
                        con.Close();
                        string query4 = "Delete from tbl_MetaData_GODOWN_2017_log where Premises_Id ='" + prm_id + "'  and BranchID= '" + prm_ddlbranch.SelectedValue.ToString() + "' and DistrictId= '" + prm_ddldist.SelectedValue.ToString() + "'";
                        SqlCommand cmd4 = new SqlCommand(query4, con);
                        con.Open();
                        int a4 = cmd4.ExecuteNonQuery();
                        if (a4 > 0)
                        {
                            con.Close();
                            string query5 = "Delete from tbl_MetaData_Premises where Premises_Id ='" + prm_id + "'  and BranchID= '" + prm_ddlbranch.SelectedValue.ToString() + "' and DistrictId= '" + prm_ddldist.SelectedValue.ToString() + "'";
                            SqlCommand cmd5 = new SqlCommand(query5, con);
                            con.Open();
                            int a5 = cmd5.ExecuteNonQuery();
                            if (a5 > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Successfully Delete Premises...')", true);
                            }
                            con.Close();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error For Deleting Premises Godown ...')", true);
                        }
                        con.Close();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Something Error For Deleting Premises...')", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mymsg1", "alert('Some Warehouse Godown Link with This Premises Godown...')", true);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }

    }
}

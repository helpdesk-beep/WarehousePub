using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Branch_Welcome : System.Web.UI.Page
{
    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FCIConnectionString"].ToString());
  //  public SqlConnection JVScon = new SqlConnection(ConfigurationManager.ConnectionStrings["JVSGodownConString"].ToString());
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    public string qry = "";
    decimal total1 = 0;
    decimal total2 = 0;
    decimal total3 = 0;
    decimal total4 = 0;
    decimal total5 = 0;
    decimal total6 = 0;
    decimal total7 = 0;
    decimal total8 = 0;
    decimal total9 = 0;
    int CountSub = 0;
    decimal SciCapacity = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //lbl_start.Text = System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
       
        if (!string.IsNullOrEmpty(Session["Depot_DepotID"] as string))
        {
            if (Session["UserName"].ToString() != "")
            {
                //UxName.Text = Session["UserName"].ToString();
            }

            if (Session["IsSussess"] == "Success")
            {               
                Session.Remove("IsSussess");
            }
            if (!IsPostBack)
            {
                //comment in Local only
                //GetLatiLongi();
                fillCropYear();     
                fillGodownType();
                //ModalPopupExtender1.Show();
                //fillGodnList();
                GetRegID();
              //  Get_tehsil();
                Get_Blocks();
            }
        }
        else
        {
            Response.Redirect("Logout.aspx");
        }
    }

    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["reporturl"] = "";
        Session["reporturl"] = "BranchWiseProcDtl";
        ClientScript.RegisterClientScriptBlock(typeof(Page), "OpenWindow", "window.open(\"IssueCenterLevel/Storage/ReportViewer_Depot.aspx\",\"_blank\")", true);
  
    }
    protected void GetSummary()
    {
        //string query = "select Commodity_Name,sum(RecBags) as RecBags,SUM(RecQty) as RecQty,SUM(DelBags) as DelBags,SUM(DelQty) as DelQty,SUM(RecBags-DelBags) as AvaBags,sum(RecQty-DelQty) as AvaQty, COUNT(Depositor_whr_id) as WHR from View_WHRcurrentstock where BranchID='" + Session["BranchId"].ToString() + "' group by Commodity_Name order by sum(RecQty-DelQty) desc";
        string query = "select Mdg.Godown_Name,Mdg.Hired_Type,CONVERT(decimal(18,2),Mdg.Godown_Capacity) as Godown_Capacity,sum(WS.RecBags)as RecBags,CONVERT(decimal(18,2),sum(ws.RecQty)) as RecQty,SUM(ws.DelBags) as DelBags,CONVERT(decimal(18,2),sum(ws.DelQty)) as DelQty,(sum(WS.RecBags)-SUM(ws.DelBags)) as AvaBags,CONVERT(decimal(18,2),(sum(ws.RecQty)-sum(ws.DelQty))) as AvaQty,ROUND(CONVERT(decimal(18,2),((sum(ws.RecQty)-sum(ws.DelQty))*100)/Mdg.Godown_Capacity),2) as UtilPerc,CONVERT(decimal(18,2),Mdg.Godown_Capacity-(sum(ws.RecQty)-sum(ws.DelQty))) as AvaCapacity from View_WHRcurrentstock as WS inner join tbl_MetaData_GODOWN as Mdg on WS.Godown_ID=Mdg.Godown_ID inner join tbl_MetaData_DEPOT as tmdd on tmdd.BranchId=WS.BranchID where tmdd.BranchId = '" + Session["BranchId"].ToString() + "' and Mdg.Remarks='Y' group by Mdg.Godown_Name,Mdg.Hired_Type,Mdg.Godown_ID,Mdg.Godown_Capacity order by Mdg.Godown_Name asc";

        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        da.Fill(dt);
        /////
        foreach (DataRow dataRow in dt.Rows)
        {
            if (Convert.ToDecimal(dataRow["AvaQty"]) == Convert.ToDecimal(0.00))
            {
                dataRow.Delete();
                CountSub = CountSub + 1;
            }
        }

        if (dt.Rows.Count == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Record Found...'); </script> ");
            gvsummary.DataSource = null;
            gvsummary.DataBind();
            lblCount.Text = "No of Record : " + ((Convert.ToInt32(dt.Rows.Count)) - CountSub);
        }
        else
        {
            gvsummary.DataSource = dt;
            gvsummary.DataBind();
            lblCount.Text = "No of Record : " + ((Convert.ToInt32(dt.Rows.Count)) - CountSub);
        }
      
    }
    protected void fillCropYear()
    {
        ddlcropyr.Items.Insert(0, "--All Year--");
        ddlcropyr.Items.Add((DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 2) + "-" + (DateTime.Now.Year - 1).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 3) + "-" + (DateTime.Now.Year - 2).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 4) + "-" + (DateTime.Now.Year - 3).ToString().Substring(2, 2));
        ddlcropyr.Items.Add((DateTime.Now.Year - 5) + "-" + (DateTime.Now.Year - 4).ToString().Substring(2, 2));
    }
    //protected void bindchart()
    //{
    //    string query = "SELECT mg.Godown_Name, cast(ISNULL(mg.Godown_Capacity,0) as decimal(18,2)) as Godown_Capacity,(round((SELECT ISNULL(SUM(RecQty) - SUM(DelQty), 0) AS Expr1 FROM View_WHRcurrentstock WHERE (BranchID = mg.BranchID) AND (Godown_ID = mg.Godown_ID))*100/ NULLIF(cast(ISNULL(mg.Godown_Capacity,0) as decimal(18,2)),0),2))  as utilization FROM  tbl_MetaData_GODOWN AS mg where mg.Remarks='Y' and mg.BranchID='" + Session["BranchId"].ToString() + "'";
                 
    //            SqlCommand cmd = new SqlCommand(query, con);
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            DataSet ds = new DataSet();
    //            da.Fill(ds);
    //            DataTable dt = new DataTable();
    //            dt = ds.Tables[0];

    //            string category = "";
        
    //            decimal[] values = new decimal[dt.Rows.Count];
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                category = category + "," + dt.Rows[i]["Godown_Name"].ToString();
    //                values[i] = Convert.ToDecimal(dt.Rows[i]["utilization"]);
    //            }

    //            BarChart1.CategoriesAxis = category.Remove(0, 1);
       
    //            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values, BarColor = "#2fd1f9", Name = "Godown" });


    //}
    protected void ddlcropyr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcropyr.SelectedItem.Text == "--All Year--")
        {
            GetSummary();
        }
        else
        {
            //string query = "select Commodity_Name,sum(RecBags) as RecBags,SUM(RecQty) as RecQty,SUM(DelBags) as DelBags,SUM(DelQty) as DelQty,SUM(RecBags-DelBags) as AvaBags,sum(RecQty-DelQty) as AvaQty, COUNT(Depositor_whr_id) as WHR from View_WHRcurrentstock where BranchID='" + Session["BranchId"].ToString() + "' and CropYear='"+ ddlcropyr.SelectedItem.Text +"' group by Commodity_Name order by sum(RecQty-DelQty) desc";
            string query = "select Mdg.Godown_Name,Mdg.Hired_Type,CONVERT(decimal(18,2),Mdg.Godown_Capacity) as Godown_Capacity,sum(WS.RecBags)as RecBags,CONVERT(decimal(18,2),sum(ws.RecQty)) as RecQty,SUM(ws.DelBags) as DelBags,CONVERT(decimal(18,2),sum(ws.DelQty)) as DelQty,(sum(WS.RecBags)-SUM(ws.DelBags)) as AvaBags,CONVERT(decimal(18,2),(sum(ws.RecQty)-sum(ws.DelQty))) as AvaQty,ROUND(CONVERT(decimal(18,2),((sum(ws.RecQty)-sum(ws.DelQty))*100)/Mdg.Godown_Capacity),2) as UtilPerc,CONVERT(decimal(18,2),Mdg.Godown_Capacity-(sum(ws.RecQty)-sum(ws.DelQty))) as AvaCapacity from View_WHRcurrentstock as WS inner join tbl_MetaData_GODOWN as Mdg on WS.Godown_ID=Mdg.Godown_ID inner join tbl_MetaData_DEPOT as tmdd on tmdd.BranchId=WS.BranchID where tmdd.BranchId = '" + Session["BranchId"].ToString() + "' and CropYear='" + ddlcropyr.SelectedItem.Text + "' and Mdg.Remarks='Y' group by Mdg.Godown_Name,Mdg.Hired_Type,Mdg.Godown_ID,Mdg.Godown_Capacity order by Mdg.Godown_Name asc";

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(dt);
            /////
            foreach (DataRow dataRow in dt.Rows)
            {
                if (Convert.ToDecimal(dataRow["AvaQty"]) == Convert.ToDecimal(0.00))
                {
                    dataRow.Delete();
                    CountSub = CountSub + 1;
                }
            }

            if (dt.Rows.Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('No Record Found...'); </script> ");
                gvsummary.DataSource = null;
                gvsummary.DataBind();
                lblCount.Text = "No of Record : " + ((Convert.ToInt32(dt.Rows.Count)) - CountSub);
            }
            else
            {
                gvsummary.DataSource = dt;
                gvsummary.DataBind();
                lblCount.Text = "No of Record : " + ((Convert.ToInt32(dt.Rows.Count)) - CountSub);
            }
        }
    }
    protected void gvsummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total1 += (DataBinder.Eval(e.Row.DataItem, "Godown_Capacity") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Godown_Capacity")) : 0;

            total2 += (DataBinder.Eval(e.Row.DataItem, "RecBags") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RecBags")) : 0;

            total3 += (DataBinder.Eval(e.Row.DataItem, "RecQty") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RecQty")) : 0;

            total4 += (DataBinder.Eval(e.Row.DataItem, "DelBags") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DelBags")) : 0;

            total5 += (DataBinder.Eval(e.Row.DataItem, "DelQty") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DelQty")) : 0;

            total6 += (DataBinder.Eval(e.Row.DataItem, "AvaBags") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AvaBags")) : 0;

            total7 += (DataBinder.Eval(e.Row.DataItem, "AvaQty") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AvaQty")) : 0;

            total8 += (DataBinder.Eval(e.Row.DataItem, "UtilPerc") != System.DBNull.Value) ? Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "UtilPerc")) : 0;

            total9 += (DataBinder.Eval(e.Row.DataItem, "AvaCapacity") != System.DBNull.Value) ? Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AvaCapacity")) : 0;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            

            Label lblGodown_Capacity = (Label)e.Row.FindControl("Godown_Capacity");

            lblGodown_Capacity.Text = total1.ToString();

            Label lblRecBags = (Label)e.Row.FindControl("RecBags");

            lblRecBags.Text = total2.ToString();

            Label lblRecQty = (Label)e.Row.FindControl("RecQty");

            lblRecQty.Text = total3.ToString();

            Label lblDelBags = (Label)e.Row.FindControl("DelBags");

            lblDelBags.Text = total4.ToString();

            Label lblDelQty = (Label)e.Row.FindControl("DelQty");

            lblDelQty.Text = total5.ToString();

            Label lblAvaBags = (Label)e.Row.FindControl("AvaBags");

            lblAvaBags.Text = total6.ToString();

            Label lblAvaQty = (Label)e.Row.FindControl("AvaQty");

            lblAvaQty.Text = total7.ToString();

            Label lblUtilPerc = (Label)e.Row.FindControl("UtilPerc");
            total8 = Math.Round((((total3 - total5) * 100) / total1),2);
            lblUtilPerc.Text = total8.ToString()+" %";

            Label lblAvaCapacity = (Label)e.Row.FindControl("AvaCapacity");
            lblAvaCapacity.Text = total9.ToString();
        }
    }
    protected void btn1_Click(object sender, EventArgs e)
    {
        //ViewState["SchCapacity"] = txtSciCap.Text;
        if (ddl_godown.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select Godown..'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (ddlRegID.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select JVS Registration ID..'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if ( Convert.ToDecimal(txtSciCap.Text) == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter L*W*H...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (Convert.ToDecimal(txtPremiseCpt.Text) == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('परिसर में कुल गोदामो की भण्डारण क्षमता दर्ज करें |...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (txtClosing.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('15/06/2019 की स्थिति मे गोदाम में भण्डारित स्कंध का Closing Balance दर्ज करें |...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (ddlHiredType.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Godown Hired Type|...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (ddlStorageType.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Enter Storage Type |...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (DDLTehsil.SelectedItem.Text=="--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('तेह्सिल दर्ज करें |...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (ddlVillage.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('गाँव का नाम दर्ज करें |...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (ddlblocknew.SelectedItem.Text == "--Select--")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert(' विकास खण्ड दर्ज करें |...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (lblwhname.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Select JVS Registration ID...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (txtGNo.Text == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Enter Godown Number...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else if (lblGodownName.Text=="")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Please Select Godown Name...'); </script> ");
            ModalPopupExtender1.Show();
        }
        else
        {
            string IP = Request.ServerVariables["REMOTE_ADDR"].ToString();

            string GodownId = ddl_godown.SelectedValue.ToString();
            string GodownName = ddl_godown.SelectedItem.Text;
            decimal GodownSciCapacityPre = Convert.ToDecimal(txtSciCap.Text);
            decimal GodownSciCapacity = GodownSciCapacityPre * 10;
            decimal GodownMaxCapacity = ((GodownSciCapacity * 125) / 100);
            if (ddlHiredType.SelectedItem.Text == "Owned")
            {
                lblwhname.Text = "MPWLC";
            }
            else if (ddlHiredType.SelectedItem.Text == "CWC")
            {
                lblwhname.Text = "CWC";
            }
            else if (ddlHiredType.SelectedItem.Text == "Markfed")
            {
                lblwhname.Text = "MARKFED";
            }
            else if (ddlHiredType.SelectedItem.Text == "FCI")
            {
                lblwhname.Text = "FCI";
            }
            string qry2 = "";
            qry2 = "INSERT INTO [Intergrated_MP_STORAGE].[dbo].[tbl_MetaData_GODOWN_2019]([Godown_ID],[StateId],[DistrictId],DepotId,[Godown_Name],[Godown_Capacity],[CreatedBy],[CreatedDate],[Hired_Type],[Storage_Type],[Godown_Scientific_Capacity],[BranchID],Lenght,Width,Height,[Premise_capacity],[Closing_Balance],[LR_TehsilCode],[LR_VillageCode],[Block_ID],[JVS_RegNo],[WH_Name],[GodownNum],[WeightmentType]) VALUES('" + GodownId + "','22','" + Session["Depot_DistID"].ToString() + "','" + Session["Depot_DepotID"].ToString() + "',N'" + lblGodownName.Text.Trim() + "','" + GodownMaxCapacity + "','" + IP + "',getDate(),'" + ddlHiredType.SelectedItem.Text + "','" + ddlStorageType.SelectedItem.Text + "','" + GodownSciCapacity + "','" + Session["BranchId"].ToString() + "','" + Convert.ToDecimal(txtLenght.Text) + "','" + Convert.ToDecimal(txtWidth.Text) + "','" + Convert.ToDecimal(txtHeight.Text) + "','" + Convert.ToDecimal(txtPremiseCpt.Text) * 10 + "','" + Convert.ToDecimal(txtClosing.Text) * 10 + "','" + DDLTehsil.SelectedValue.ToString() + "','" + ddlVillage.SelectedValue.ToString() + "','" + ddlblocknew.SelectedValue.ToString() + "','" + ddlRegID.SelectedValue.ToString() + "','" + lblwhname.Text.Trim() + "','" + txtGNo.Text.Trim() + "','" + ddlWB.SelectedValue.ToString() + "')";
            con.Open();
            cmd = new SqlCommand(qry2, con);
            int b = cmd.ExecuteNonQuery();
            con.Close();
            if (b > 0)
            {
               // fillGodnList();
                ClearData();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mymsg2", "<script language=javascript> alert('Some Error has occured...'); </script>");
                ModalPopupExtender1.Show();
            }

        }
    }
   
    //private void fillGodnList()
    //{
    //    if (Session["Depot_DistID"] != null)
    //    {
    //        //string query = "SELECT (MG.Godown_Name+' ('+MG.Godown_ID+')') as Godown_Name,MG.Godown_ID FROM [tbl_MetaData_GODOWN] as MG WHERE MG.BranchID ='" + Session["BranchId"].ToString() + "' and MG.DistrictId='" + Session["Depot_DistID"].ToString() + "' and MG.Remarks='Y' and MG.Godown_ID not in (select Godown_ID from tbl_MetaData_GODOWN_2017 where BranchID='" + Session["BranchId"].ToString() + "' and DistrictId='" + Session["Depot_DistID"].ToString() + "') ORDER BY [Godown_Name] Asc ";
    //        string query = "SELECT (MG.Godown_Name+' ('+MG.Godown_ID+')') as Godown_Name,MG.Godown_ID FROM [tbl_MetaData_GODOWN_2018] as MG WHERE MG.BranchID ='" + Session["BranchId"].ToString() + "' and MG.DistrictId='" + Session["Depot_DistID"].ToString() + "' and Godown_ID not in (select MGG.Godown_ID from tbl_MetaData_GODOWN_2019 as MGG where MGG.BranchID ='" + Session["BranchId"].ToString() + "')  ORDER BY [Godown_Name] Asc";

    //        SqlCommand cmd = new SqlCommand(query, con);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet ds = new DataSet();
    //        da.Fill(ds);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddl_godown.DataSource = ds.Tables[0];
    //            ddl_godown.DataTextField = "Godown_Name";
    //            ddl_godown.DataValueField = "Godown_ID";
    //            ddl_godown.DataBind();
    //            ddl_godown.Items.Insert(0, "--Select--");
    //            ModalPopupExtender1.Show();
    //        }
    //        else
    //        {
    //            pnllogin.Visible = false;
    //            ModalPopupExtender1.Hide();
    //        }
    //    }
    //}
    public void GetLatiLongi()
    {
        try
        {
            Session["latitude"] = Request.Cookies["Lati"].Value.ToString();
            Session["longitude"] = Request.Cookies["Longi"].Value.ToString();
            //Session["latitude"] = Convert.ToString(Request.Cookies["Lati"].Value);
            //Session["longitude"] = Convert.ToString(Request.Cookies["Longi"].Value);
        }
        catch (Exception ex)
        {
           
        }
        
    }
  
    protected void ddlPBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void ddlExist_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void txtLenght_TextChanged(object sender, EventArgs e)
    {
        SciCapacity = Convert.ToDecimal(txtLenght.Text) * Convert.ToDecimal(txtWidth.Text) * ((Convert.ToDecimal(txtHeight.Text) - 3) / 80);
        txtSciCap.Text = Math.Round(SciCapacity, 2).ToString();
        ModalPopupExtender1.Show();

    }
    protected void txtWidth_TextChanged(object sender, EventArgs e)
    {
        SciCapacity = Convert.ToDecimal(txtLenght.Text) * Convert.ToDecimal(txtWidth.Text) * ((Convert.ToDecimal(txtHeight.Text) - 3) / 80);
        txtSciCap.Text = Math.Round(SciCapacity, 2).ToString();
        ModalPopupExtender1.Show();
    }
    protected void txtHeight_TextChanged(object sender, EventArgs e)
    {
        SciCapacity = Convert.ToDecimal(txtLenght.Text) * Convert.ToDecimal(txtWidth.Text) * ((Convert.ToDecimal(txtHeight.Text) - 3) / 80);
        txtSciCap.Text = Math.Round(SciCapacity, 2).ToString();
        ModalPopupExtender1.Show();
    }
    protected void ddl_godown_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblGodownId.Text = ddl_godown.SelectedValue.ToString();
        string GdName = ddl_godown.SelectedItem.Text;
        string[] GodownNM = GdName.Split('(');
        lblGodownName.Text = GodownNM[0].Trim();
        ModalPopupExtender1.Show();
    }
    private void fillGodownType()
    {
        try
        {
            string BranchType = Session["BranchType"].ToString();
            string query = "";
            query = "SELECT  [Gid],[GodownType],[TypeValue] FROM [dbo].[GodownTypeMaster]";
            cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlHiredType.Items.Clear();
                ddlHiredType.DataSource = ds.Tables[0];
                ddlHiredType.DataTextField = "GodownType";
                ddlHiredType.DataValueField = "TypeValue";
                ddlHiredType.DataBind();
                ddlHiredType.Items.Insert(0, "--Select--");
            }
            else
            {
            }
        }
        catch (Exception)
        {
        }
    }
    public void GetRegID()
    {
        //ddlRegID.DataSource = "";
        //string qry = "";
        //qry = "select Registration_ID,UPPER(Warehouse_name) +' ( '+ Registration_ID +' )' as Warehouse_name from tbl_warehouseRegistration as WREG where WREG.BranchId='" + Session["BranchId"].ToString() + "' and RegCapacity>0 order by Warehouse_name";
        //SqlCommand cmd = new SqlCommand(qry, JVScon);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlRegID.DataSource = ds.Tables[0];
        //    ddlRegID.DataTextField = "Warehouse_name";
        //    ddlRegID.DataValueField = "Registration_ID";
        //    ddlRegID.DataBind();
        //    ddlRegID.Items.Insert(0, "--Select--");
        //    ddlRegID.Items.Insert(1, "Not In List");
        //}
        //else
        //{
        //    ddlRegID.Items.Insert(0, "--Select--");
        //    ddlRegID.Items.Insert(1, "Not In List");
        //}

    }
    public void Get_Blocks()
    {
        //string DistrictId = Session["Depot_DistID"].ToString();
        //string qry = "select distinct Block_ID,Block_Name from tbl_Branch_Block_Mapping where District_ID='" + DistrictId + "' order by Block_Name";
        //SqlCommand cmd = new SqlCommand(qry, JVScon);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        //da.Fill(ds);
        //if (ds == null)
        //{

        //}
        //else
        //{
        //    ddlblocknew.DataSource = ds.Tables[0];
        //    ddlblocknew.DataTextField = "Block_Name";
        //    ddlblocknew.DataValueField = "Block_ID";
        //    ddlblocknew.DataBind();
        //    ddlblocknew.Items.Insert(0, "--Select--");
        //}
    }
    //public void Get_tehsil()
    //{
    //    string DistrictId = Session["Depot_DistID"].ToString();
    //    string qry = "SELECT [TehsilCode],[Tehsil_Name] FROM [Tehsils] where District_Code='" + DistrictId + "' order by [Tehsil_Name]";
    //    SqlCommand cmd = new SqlCommand(qry, con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);
    //    if (ds == null)
    //    {

    //    }
    //    else
    //    {
    //        DDLTehsil.DataSource = ds.Tables[0];
    //        DDLTehsil.DataTextField = "Tehsil_Name";
    //        DDLTehsil.DataValueField = "TehsilCode";
    //        DDLTehsil.DataBind();
    //        DDLTehsil.Items.Insert(0, "--Select--");
    //    }
    //}

    public void Get_tehsil_Village()
    {
        string qry = "select villagenameh,newvillcode from VillageLR where Tehsil_ID='" + DDLTehsil.SelectedValue.ToString() +"' order by villagenameh";
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds == null)
        {

        }
        else
        {
            ddlVillage.DataSource = ds.Tables[0];
            ddlVillage.DataTextField = "villagenameh";
            ddlVillage.DataValueField = "newvillcode";
            ddlVillage.DataBind();
            ddlVillage.Items.Insert(0, "--Select--");
        }
    }
    protected void DDLTehsil_SelectedIndexChanged(object sender, EventArgs e)
    {
        Get_tehsil_Village();
        ModalPopupExtender1.Show();
    }
    protected void ddlRegID_SelectedIndexChanged(object sender, EventArgs e)
    {
        string WH_RegName = ddlRegID.SelectedItem.Text;
        string[] WHName = WH_RegName.Split('(');
        lblwhname.Text = WHName[0].Trim();
        ModalPopupExtender1.Show();
    }
    public void ClearData()
    {
        ddlRegID.SelectedIndex = -1;
        lblwhname.Text = "";
        txtLenght.Text = "0";
        txtWidth.Text = "0";
        txtHeight.Text = "0";
        txtSciCap.Text = "0";
        ddlHiredType.SelectedIndex = -1;
        ddlStorageType.SelectedIndex = -1;
        DDLTehsil.SelectedIndex = -1;
        ddlblocknew.SelectedIndex = -1;
        txtPremiseCpt.Text = "0";
        txtClosing.Text = "0";
        txtGNo.Text = "";
        ddlVillage.SelectedIndex = -1;
        ddlWB.SelectedIndex = -1;
    }
    
}
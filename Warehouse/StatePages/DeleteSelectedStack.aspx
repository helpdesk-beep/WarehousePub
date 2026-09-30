<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Configuration" %>

<!DOCTYPE html>
<script runat="server">
    private string connString = ConfigurationManager.ConnectionStrings["FCIConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            // Query to fetch warehouse stacking details based on Stack ID
            string query = @"
                select C.District_Name, D.DepotName, B.Godown_Name + '(' + B.Godown_ID +N')'as Godown_Name, 
                A.Stack_ID, A.StorageReceipt_Id, A.Autoid, WHRId as WHR_Id 
                from tbl_storage_Stacking_Details A
                Inner join tbl_MetaData_GODOWN_2018 B on A.Godown_ID = b.Godown_ID 
                inner join tbl_MetaData_DISTRICT C on B.DistrictId = c.District_Id  
                inner join tbl_MetaData_DEPOT D on B.BranchID = D.BranchId 
                where A.Stack_ID=@stackid and whrid is null";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@stackid", txtStackSearch.Text.Trim());
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    gvStackingDetails.DataSource = dt;
                    gvStackingDetails.DataBind();

                    if (gvStackingDetails.Rows.Count > 0)
                    {
                        gvStackingDetails.UseAccessibleHeader = true;
                        gvStackingDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }
                }
            }
        }
    }

    protected void gvStackingDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int autoID = Convert.ToInt32(gvStackingDetails.DataKeys[rowIndex].Values["autoid"]);
            string receiptID = gvStackingDetails.DataKeys[rowIndex].Values["StorageReceipt_id"].ToString();
            string stackID = gvStackingDetails.DataKeys[rowIndex].Values["Stack_ID"].ToString();

            ManageStackingDetails(stackID, receiptID, autoID);
            BindGrid();
        }
    }

    public void ManageStackingDetails(string stackID, string receiptID, int autoID)
    {
        using (SqlConnection con = new SqlConnection(connString))
        {
            using (SqlCommand cmd = new SqlCommand("sp_ManageStackingDetails", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@StackID", SqlDbType.VarChar, 50).Value = stackID;
                cmd.Parameters.Add("@ReceiptID", SqlDbType.VarChar, 50).Value = receiptID;
                cmd.Parameters.Add("@AutoID", SqlDbType.Int).Value = autoID;

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Record Deleted Successfully.');", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "alert('Error: " + ex.Message.Replace("'", "") + "');", true);
                }
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Stacking Details</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css" />
    <style>
        body { background-color: #f8f9fa; font-family: 'Segoe UI', sans-serif; }
        .card { border: none; border-radius: 10px; box-shadow: 0 4px 6px rgba(0,0,0,0.1); }
        .header-custom { background: #004a99; color: white; padding: 15px; border-radius: 10px 10px 0 0; }
        .btn-search { background-color: #004a99 !important;  font-weight: bold; color: #fff !important; }
        .btn-search:hover { background-color: #27F5D3 !important; color: white; }
        .grid-header { background-color: #f1f3f5 !important; font-weight: bold; color: #333; }
        .status-removed { color: #dc3545; font-weight: bold; }
    </style>
</head>
<body>
    <%-- The DefaultButton property ensures Enter key triggers the search button --%>
    <form id="form1" runat="server" defaultbutton="btnSearch"> 
        <div class="container py-5">
            <div class="card mb-4">
                <div class="header-custom">
                    <h5 class="mb-0"><i class="bi bi-search me-2"></i>Filter WHR Records</h5>
                </div>
                <div class="card-body">
                    <div class="row g-3 align-items-end">
                        <div class="col-md-6">
                            <label class="form-label fw-bold small">STACK ID</label>
                            <%-- Typing here and pressing Enter will now trigger btnSearch --%>
                            <asp:TextBox runat="server" ID="txtStackSearch" CssClass="form-control" placeholder="Enter Stack ID..." />
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnSearch" runat="server" Text="Fetch Records" CssClass="btn btn-search w-100 py-2" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvStackingDetails" runat="server" DataKeyNames="autoid,StorageReceipt_id,Stack_ID"
                            AutoGenerateColumns="false" OnRowCommand="gvStackingDetails_RowCommand" 
                            CssClass="table table-hover table-bordered align-middle" Width="100%">
                            <HeaderStyle CssClass="grid-header" />
                            <Columns>
                                <asp:BoundField DataField="District_Name" HeaderText="District" />
                                <asp:BoundField DataField="DepotName" HeaderText="Depot" />
                                <asp:BoundField DataField="Godown_Name" HeaderText="Godown" />
                                <asp:BoundField DataField="StorageReceipt_Id" HeaderText="Receipt ID" />
                                <asp:BoundField DataField="Stack_ID" HeaderText="Stack ID" />
                                <asp:TemplateField HeaderText="WHR ID">
                                    <ItemTemplate>
                                        <%-- Displays "Removed" in red if WHR ID is null or empty --%>
                                        <%# string.IsNullOrEmpty(Eval("WHR_Id").ToString()) ? 
                                            "<span class='status-removed'>Removed</span>" : Eval("WHR_Id") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRowDelete" runat="server"
                                            CommandName="DeleteRow"
                                            OnClientClick="return confirm('Confirm deletion of this stack?');"
                                            CommandArgument='<%# Container.DataItemIndex %>'
                                            CssClass="btn btn-danger btn-sm fw-bold">
                                            <i class="bi bi-trash"></i> Delete
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="text-center p-4 text-muted">No records matching that Stack ID.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
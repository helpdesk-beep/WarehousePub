<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_Wise_Pending_Rent_Bill_Details_Summary_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_District_Wise_Pending_Rent_Bill_Details_Summary_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
<div class="container-fluid mt-4">

    <!-- ===== PAGE HEADER ===== -->
    <div class="text-center mb-4">
        <h2 class="fw-bold">District Wise Rent Pending Bills For Generation at Godown Level</h2>
        <small class="text-muted">View pending storage charges bills for selected financial year</small>
    </div>

    <!-- ===== FILTER SECTION ===== -->
    <div class="row justify-content-center mb-4">
        <div class="col-md-4">
            <label class="fw-bold">Select Financial Year</label>
            <asp:DropDownList ID="ddlfinancial" CssClass="form-control show-loader-ddl" runat="server" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlfinancial_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <!-- ===== GRID SECTION ===== -->
    <div class="row">
        <div class="col-12 table-responsive">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                CssClass="table table-bordered table-striped table-hover"
                AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">

                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Regionnm" HeaderText="Region" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" />
                    <asp:BoundField DataField="District_Name" HeaderText="District" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%" />

                    <asp:TemplateField HeaderText="No of Storage Charges Bills Pending For Generation at Branch Level" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlPendingBills" runat="server" Target="_blank" 
                                NavigateUrl='<%#"~/Reports/States/Godown_Wise_Pending_Rent_Bill_Status.aspx?Region_ID=" + Eval("District_Id")%>'
                                title="Amount" Text='<%# Eval("TotalBillPendingForGeneration") %>' ForeColor="Blue"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                </Columns>

                <FooterStyle Font-Bold="True" BackColor="#f1f1f1" ForeColor="Black" />
            </asp:GridView>
        </div>
    </div>

</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>



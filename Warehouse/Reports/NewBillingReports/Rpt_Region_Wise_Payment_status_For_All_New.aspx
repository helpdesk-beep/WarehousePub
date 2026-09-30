<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Region_Wise_Payment_status_For_All_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_Region_Wise_Payment_status_For_All_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">

<div class="content-wrapper">

    <!-- ================= HEADER & FILTER ================= -->
    <fieldset class="border rounded p-3 mb-4">
        <legend class="px-2 fw-bold text-center">
            Region Wise Payment Status (Amount in Cr.) – All Godown
        </legend>

        <div class="row justify-content-center mt-3">
            <div class="col-md-2">
                <label class="form-label fw-semibold">Select Financial Year</label>
                </div>
            <div class="col-md-4">
                <asp:DropDownList ID="ddlFY" runat="server"
                    CssClass="form-control show-loader-ddl"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlFY_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
    </fieldset>

    <!-- ================= REGION WISE GRID ================= -->
    <fieldset class="border rounded p-3 mb-4">
        <legend class="px-2 fw-bold">Region Wise Payment Status</legend>

        <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False"
                ShowFooter="true"
                CssClass="table table-bordered table-hover datatable"
                OnDataBound="OnDataBound">

                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Region Name">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkRegion" runat="server"
                                Target="_blank"
                                NavigateUrl='<%#"~/Reports/States/Rpt_District_Wise_Payment_status_For_JVS.aspx?Region_ID=" + Eval("Region_ID")%>'
                                Text='<%# Eval("Regionnm") %>'
                                ForeColor="Blue" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="TotalGodown" HeaderText="Total Godown" />
                    <asp:BoundField DataField="TotalStorageBill" HeaderText="Generated Bill (Count)" />
                    <asp:BoundField DataField="Amount" HeaderText="Generated Bill Amount" />
                    <asp:BoundField DataField="TotalBillReceivedPayment" HeaderText="Payment Received (Count)" />
                    <asp:BoundField DataField="Gross_Amount" HeaderText="Amount To Be Received" />
                    <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction" />
                    <asp:BoundField DataField="OtherDeduction" HeaderText="Other Deduction" />
                    <asp:BoundField DataField="Payable_Amount" HeaderText="Net Amount Credit to MPWLC" />
                    <asp:BoundField DataField="PendindAtMPSCSC" HeaderText="Pending Amount at MPSCSC" />
                </Columns>

                <FooterStyle Font-Bold="true" />
            </asp:GridView>
        </div>
    </fieldset>

    <!-- ================= FY WISE GRID ================= -->
    <fieldset class="border rounded p-3">
        <legend class="px-2 fw-bold">
            Financial Year Wise Payment Status (Amount in Cr.)
        </legend>

        <div class="table-responsive">
            <asp:GridView ID="GridView2" runat="server"
                AutoGenerateColumns="False"
                ShowFooter="true"
                CssClass="table table-bordered table-hover datatable"
                OnDataBound="GridView2_DataBound">

                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                    <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" />
                    <asp:BoundField DataField="TotalGodown" HeaderText="Total Godown" />
                    <asp:BoundField DataField="TotalStorageBill" HeaderText="Generated Bill (Count)" />
                    <asp:BoundField DataField="Amount" HeaderText="Generated Bill Amount" />
                    <asp:BoundField DataField="TotalBillReceivedPayment" HeaderText="Payment Received (Count)" />
                    <asp:BoundField DataField="Gross_Amount" HeaderText="Amount To Be Received" />
                    <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction" />
                    <asp:BoundField DataField="OtherDeduction" HeaderText="Other Deduction" />
                    <asp:BoundField DataField="Payable_Amount" HeaderText="Net Amount Credit to MPWLC" />
                    <asp:BoundField DataField="PendindAtMPSCSC" HeaderText="Pending Amount at MPSCSC" />
                </Columns>

                <FooterStyle Font-Bold="true" />
            </asp:GridView>
        </div>
    </fieldset>

</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        $(document).ready(function () {
            // Initialize all visible grids on page load
            setTimeout(function () {
                initializeDataTables();
            }, 500); // थोड़ा delay दें ताकि GridView पूरी तरह render हो जाए

            // Reinitialize on PostBack
            if (typeof (Sys) !== 'undefined') {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                    setTimeout(function () {
                        initializeDataTables();
                    }, 500);
                });
            }
        });

        function initializeDataTables() {
            console.log("Initializing DataTables...");

            // Check each grid
            var grids = [
                '#<%= GridView1.ClientID %>',
                '#<%= GridView2.ClientID %>'
            ];

            grids.forEach(function (gridId) {
                var $grid = $(gridId);
                console.log("Checking grid:", gridId, "Exists:", $grid.length, "Visible:", $grid.is(':visible'));

                if ($grid.length > 0 && $grid.is(':visible')) {
                    try {
                        // Destroy existing DataTable if exists
                        if ($.fn.DataTable.isDataTable($grid)) {
                            console.log("Destroying existing DataTable for:", gridId);
                            $grid.DataTable().destroy();
                        }

                        // Remove existing thead if exists
                        $grid.find('thead').remove();

                        // Make sure we have data rows
                        if ($grid.find('tr').length > 0) {
                            // Create and prepend thead
                            var $headerRow = $grid.find('tr:first');
                            if ($headerRow.length) {
                                var $thead = $('<thead></thead>').append($headerRow.clone());
                                $grid.prepend($thead);

                                // Remove the original header row from tbody
                                $headerRow.remove();

                                // Wrap the remaining rows in tbody if not exists
                                if ($grid.find('tbody').length === 0) {
                                    var $rows = $grid.find('tr');
                                    $grid.append($('<tbody></tbody>').append($rows));
                                }

                                // Apply DataTable with your settings
                                console.log("Applying DataTable to:", gridId);
                                BindDatatable($grid);
                            }
                        }
                    } catch (e) {
                        console.error("Error initializing DataTable for " + gridId + ":", e);
                    }
                }
            });
        }
    </script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Godown_Bill_Wise_Payment_Status_From_MPSCSC_New.aspx.cs" Inherits="Reports_NewBillingReports_Godown_Bill_Wise_Payment_Status_From_MPSCSC_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <style>
        /* Hide toggle button */
        #toggleBtn {
            display: none !important;
        }

        /* Hide sidebar completely */
        #sidebar {
            display: none !important;
        }

        /* Remove margin from main body to make it full width */
        #mainBody {
            margin-left: 0 !important;
            width: 100% !important;
        }

        /* Adjust navbar brand margin when toggle button is hidden */
        .navbar-brand {
            margin-left: 15px !important;
        }

        /* Make content area full width */
        .content {
            width: 100% !important;
        }

        /* Adjust container fluid */
        .container-fluid {
            padding-left: 15px !important;
            padding-right: 15px !important;
            max-width: 100% !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid py-3">
        <!-- Header Section -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="d-flex justify-content-center align-items-center border-bottom pb-2">
                    <div>
                        <h1 class="h4 text-primary mb-0">M.P. WAREHOUSING & LOGISTICS CORPORATION</h1>
                        <h2 class="h5 text-dark mb-0">Godown Bill Wise Payment Status From MPSCSC</h2>
                    </div>
                </div>
            </div>
        </div>

        <!-- Data Grid Section -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="grddivision" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                CssClass="table table-bordered table-striped table-hover mb-0">

                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" Width="5%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Division">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="District">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Branch">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepotName" runat="server" Text='<%# Eval("DepotName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Godown">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGodown" runat="server" Text='<%# Eval("Godown") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Crop Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCrop_Year" runat="server" Text='<%# Eval("Crop_Year") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Financial Year">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFinancial_Year" runat="server" Text='<%# Eval("Financial_Year") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Storage Bills">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStorageBillNumber" runat="server" Text='<%# Eval("TotalBill") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="fw-bold">
                                                <asp:Label ID="lblTotalBills" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill Amount (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStorageAmount" runat="server" Text='<%# Eval("BillAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="fw-bold">
                                                <asp:Label ID="lblTotalBillAmount" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bills Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHOMPSCSCBillPaymentReceived" runat="server" Text='<%# Eval("TotalReceivedBill") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="fw-bold">
                                                <asp:Label ID="lblTotalReceivedBills" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount to be Received (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGross_Amount" runat="server" Text='<%# Eval("Gross_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="fw-bold">
                                                <asp:Label ID="lblTotalGrossAmount" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount Received (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPayable_Amount" runat="server" Text='<%# Eval("Payable_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="fw-bold">
                                                <asp:Label ID="lblTotalPayableAmount" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="TDS Deduction (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTDS_Amt" runat="server" Text='<%# Eval("TDS_Amt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="fw-bold">
                                                <asp:Label ID="lblTotalTDS" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Other Deduction (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOtherDeduction" runat="server" Text='<%# Eval("OtherDeduction") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="fw-bold">
                                                <asp:Label ID="lblTotalOtherDeduction" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle fw-bold" />
                                        <ItemStyle CssClass="text-end align-middle fw-bold" />
                                    </asp:TemplateField>
                                </Columns>

                                <HeaderStyle CssClass="bg-primary text-white" />
                                <FooterStyle CssClass="bg-light fw-bold" />
                                <RowStyle CssClass="align-middle" />
                                <AlternatingRowStyle CssClass="bg-light" />
                                <SelectedRowStyle CssClass="table-warning" />
                                <PagerStyle CssClass="pagination justify-content-center my-3" />

                                <EmptyDataTemplate>
                                    <div class="alert alert-info text-center m-3">
                                        <i class="bi bi-info-circle me-2"></i>No bill payment status data available for display.
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= grddivision.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>

    <script>
        $(document).ready(function () {
            // 1. Hide toggle button
            $('#toggleBtn').hide();

            // 2. Collapse/Close sidebar
            $('#sidebar').hide();

            // 3. Adjust main content to full width
            if ($('#main-content').length) {
                $('#main-content').css({
                    'margin-left': '0',
                    'width': '100%'
                });
            }

            // 4. Disable the toggleNav function
            window.toggleNav = function () {
                return false;
            };

            // 5. Force close any open collapsible menus in sidebar
            $('.sidebar-nav .collapse').removeClass('show');

            // 6. Remove click events from sidebar links
            $('.sidebar-link').off('click');

            // 7. Also close sidebar if there's a close function
            if (typeof closeSidebar === 'function') {
                closeSidebar();
            }

            // 8. Override Bootstrap collapse events
            $('[data-bs-toggle="collapse"]').each(function () {
                $(this).attr('data-bs-toggle', '');
                $(this).off('click');
            });
        });

        var grid = $('#<%= grddivision.ClientID %>');
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);
    </script>
</asp:Content>

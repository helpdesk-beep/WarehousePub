<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Stack_Wise_Moisture_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_Stack_Wise_Moisture_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">

    <div class="content-wrapper">

        <!-- ================= FILTER SECTION ================= -->
        <fieldset class="border rounded p-3 mb-4">
            <legend class="px-2 fw-bold">Moisture Report</legend>

            <div class="row g-3 align-items-end">

                <div class="col-md-2">
                    <label class="form-label">Financial Year</label>
                    <asp:DropDownList ID="ddlfinancialyear" runat="server"
                        CssClass="form-control show-loader-ddl"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlfinancialyear_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>2019-20</asp:ListItem>
                        <asp:ListItem>2020-21</asp:ListItem>
                        <asp:ListItem>2021-22</asp:ListItem>
                        <asp:ListItem>2022-23</asp:ListItem>
                        <asp:ListItem>2023-24</asp:ListItem>
                        <asp:ListItem>2024-25</asp:ListItem>
                        <asp:ListItem>2025-26</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <label class="form-label">Month</label>
                    <asp:DropDownList ID="ddlmonth" runat="server"
                        CssClass="form-control show-loader-ddl"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <label class="form-label">Region</label>
                    <asp:DropDownList ID="ddlregion" runat="server"
                        CssClass="form-control show-loader-ddl"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                        <asp:ListItem Value="0">All</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label class="form-label">District</label>
                    <asp:DropDownList ID="ddldistrict" runat="server"
                        CssClass="form-control show-loader-ddl"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                        <asp:ListItem Value="0">All</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label class="form-label">Branch</label>
                    <asp:DropDownList ID="ddlbranch" runat="server"
                        CssClass="form-control show-loader-ddl"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                        <asp:ListItem Value="0">All</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>
        </fieldset>

        <!-- ================= DETAILS SECTION ================= -->
        <fieldset class="border rounded p-3">
            <legend class="px-2 fw-bold">Details</legend>

            <!-- REGION GRID -->
            <div class="row mb-3" runat="server" id="DivRegion" visible="false">
                <div class="col-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="GrdRegion" FooterStyle-Font-Bold="true" FooterStyle-="Center" ShowFooter="true"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Regional Office" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Stack" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotal_Stack" Text='<%# Eval("Total_Stack") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No of Stack Moisture Entry By BM" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotal_Fumigated" Text='<%# Eval("Total_Moisture") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pending Stack For Moisture Entry By BM" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPending_Stack_For_Fumigation" Text='<%# Eval("Pending_Stack_For_Moisture") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <!-- DISTRICT GRID -->
            <div class="row mb-3" runat="server" id="divdistrict" visible="false">
                <div class="col-12">
                    <div class="table-responsive">

                        <asp:GridView runat="server" ID="grddistrict" FooterStyle-Font-Bold="true" ShowFooter="true"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Stack" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotal_Stack" Text='<%# Eval("Total_Stack") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No of Stack Moisture Entry By BM" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotal_Fumigated" Text='<%# Eval("Total_Moisture") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pending Stack For Moisture Entry By BM" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPending_Stack_For_Fumigation" Text='<%# Eval("Pending_Stack_For_Moisture") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <!-- BRANCH GRID -->
            <div class="row mb-3" runat="server" id="divBranch" visible="false">
                <div class="col-12">
                    <div class="table-responsive">

                        <asp:GridView runat="server" ID="grdbranch" FooterStyle-Font-Bold="true" ShowFooter="true"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Stack" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotal_Stack" Text='<%# Eval("Total_Stack") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No of Stack Moisture Entry By BM" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotal_Fumigated" Text='<%# Eval("Total_Moisture") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pending Stack For Moisture Entry By BM" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPending_Stack_For_Fumigation" Text='<%# Eval("Pending_Stack_For_Moisture") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>

                    </div>
                </div>
            </div>

            <!-- STACK GRID -->
            <div class="row" runat="server" id="divStack" visible="false">
                <div class="col-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" FooterStyle-Font-Bold="true" ID="GrdStack" ShowFooter="true"
                            CssClass="table table-bordered table-hover datatable" AutoGenerateColumns="False">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Regional Office" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("Regionnm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBranch_Name" Text='<%# Eval("Branch_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Godown Name" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Stack" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotal_Stack" Text='<%# Eval("Total_Stack") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No of Stack Moisture Entry By BM" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTotal_Fumigated" Text='<%# Eval("Total_Moisture") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pending Stack For Moisture Entry By BM" HeaderStyle-BackColor="LightBlue">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPending_Stack_For_Fumigation" Text='<%# Eval("Pending_Stack_For_Moisture") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </fieldset>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        $(document).ready(function () {
            // Initialize all visible grids
            initializeDataTables();

            // Reinitialize on PostBack (if using UpdatePanel)
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                initializeDataTables();
            });
        });

        function initializeDataTables() {
            // Check each grid and apply DataTable if visible
            var grids = [
                '#<%= GrdRegion.ClientID %>',
                '#<%= grddistrict.ClientID %>',
                '#<%= grdbranch.ClientID %>',
                '#<%= GrdStack.ClientID %>'
            ];

            grids.forEach(function (gridId) {
                var $grid = $(gridId);

                // Check if grid exists and is visible
                if ($grid.length > 0 && $grid.is(':visible')) {

                    // Check if DataTable is already applied
                    if ($.fn.DataTable.isDataTable($grid)) {
                        $grid.DataTable().destroy();
                        $grid.find('thead').remove();
                    }

                    // Convert GridView header row into THEAD
                    if ($grid.find('thead').length === 0) {
                        $grid.prepend($("<thead></thead>").append($grid.find("tr:first")));
                    }

                    // Apply DataTable
                    BindDatatable($grid);
                }
            });
        }
    </script>
</asp:Content>


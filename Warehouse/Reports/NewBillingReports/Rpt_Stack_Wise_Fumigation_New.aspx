<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master"
    AutoEventWireup="true"
    CodeFile="Rpt_Stack_Wise_Fumigation_New.aspx.cs"
    Inherits="Reports_NewBillingReports_Rpt_Stack_Wise_Fumigation_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server" />

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">

<div class="content-wrapper">

    <!-- ================= FILTER SECTION ================= -->
    <fieldset class="border rounded p-3 mb-3">
        <legend class="px-2 fw-bold">Stack Wise Fumigation Report</legend>

        <div class="row align-items-end">

            <div class="col-md-3">
                <label class="form-label">Region Name</label>
                <asp:DropDownList ID="ddlregion" runat="server"
                    CssClass="form-control show-loader-ddl"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlregion_SelectedIndexChanged">
                    <asp:ListItem Value="0">All</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-3">
                <label class="form-label">District Name</label>
                <asp:DropDownList ID="ddldistrict" runat="server"
                    CssClass="form-control show-loader-ddl"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                    <asp:ListItem Value="0">All</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-3">
                <label class="form-label">Branch Name</label>
                <asp:DropDownList ID="ddlbranch" runat="server"
                    CssClass="form-control  show-loader-ddl"
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
        <div class="row" runat="server" id="DivRegion" visible="false">
            <div class="col-md-12 table-responsive">
                <asp:GridView ID="GrdRegion" runat="server"
                    CssClass="table table-bordered table-hover datatable"
                    AutoGenerateColumns="False"
                    ShowFooter="true"
                    FooterStyle-Font-Bold="true">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Regionnm" HeaderText="Regional Office" />
                        <asp:BoundField DataField="Total_Stack" HeaderText="Total Stack" />
                        <asp:BoundField DataField="Total_Fumigated" HeaderText="Total Fumigated Stack" />
                        <asp:BoundField DataField="Pending_Stack_For_Fumigation" HeaderText="Pending Stack For Fumigation" />
                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>

        <!-- DISTRICT GRID -->
        <div class="row" runat="server" id="divdistrict" visible="false">
            <div class="col-md-12 table-responsive">
                <asp:GridView ID="grddistrict" runat="server"
                    CssClass="table table-bordered table-hover datatable"
                    AutoGenerateColumns="False"
                    ShowFooter="true"
                    FooterStyle-Font-Bold="true">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        <asp:BoundField DataField="Total_Stack" HeaderText="Total Stack" />
                        <asp:BoundField DataField="Total_Fumigated" HeaderText="Total Fumigated Stack" />
                        <asp:BoundField DataField="Pending_Stack_For_Fumigation" HeaderText="Pending Stack For Fumigation" />
                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>

        <!-- BRANCH GRID -->
        <div class="row" runat="server" id="divBranch" visible="false">
            <div class="col-md-12 table-responsive">
                <asp:GridView ID="grdbranch" runat="server"
                    CssClass="table table-bordered table-hover datatable"
                    AutoGenerateColumns="False"
                    ShowFooter="true"
                    FooterStyle-Font-Bold="true">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Branch_Name" HeaderText="Branch Name" />
                        <asp:BoundField DataField="Total_Stack" HeaderText="Total Stack" />
                        <asp:BoundField DataField="Total_Fumigated" HeaderText="Total Fumigated Stack" />
                        <asp:BoundField DataField="Pending_Stack_For_Fumigation" HeaderText="Pending Stack For Fumigation" />
                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>

        <!-- STACK GRID -->
        <div class="row" runat="server" id="divStack" visible="false">
            <div class="col-md-12 table-responsive">
                <asp:GridView ID="GrdStack" runat="server"
                    CssClass="table table-bordered table-hover datatable"
                    AutoGenerateColumns="False"
                    ShowFooter="true"
                    FooterStyle-Font-Bold="true">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Regionnm" HeaderText="Region" />
                        <asp:BoundField DataField="District_Name" HeaderText="District" />
                        <asp:BoundField DataField="Branch_Name" HeaderText="Branch" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />
                        <asp:BoundField DataField="Total_Stack" HeaderText="Total Stack" />
                        <asp:BoundField DataField="Total_Fumigated" HeaderText="Total Fumigated Stack" />
                        <asp:BoundField DataField="Pending_Stack_For_Fumigation" HeaderText="Pending Stack For Fumigation" />
                    </Columns>
                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>

    </fieldset>

</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server" />
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

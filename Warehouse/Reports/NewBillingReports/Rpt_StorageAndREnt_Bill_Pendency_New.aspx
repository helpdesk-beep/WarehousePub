<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_StorageAndREnt_Bill_Pendency_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_StorageAndREnt_Bill_Pendency_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">

    <div class="container-fluid mt-3">

        <!-- ===== PAGE HEADER ===== -->
        <div class="card mb-4">
            <div class="card-body text-center bg-primary text-white">
                <h4 class="mb-1">Storage & Rent Bill Pending Status</h4>
                <small>Financial Year Wise Summary</small>
            </div>
        </div>

        <!-- ===== FILTER SECTION ===== -->
        <div class="card mb-4">
            <div class="card-header fw-bold bg-light">
                Search Criteria
            </div>
            <div class="card-body">
                <div class="row align-items-end justify-content-center">

                    <div class="col-md-2">
                        <label class="fw-bold">Financial Year</label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlfinancial"
                            CssClass="form-control"
                            runat="server"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlfinancial_SelectedIndexChanged">
                            <asp:ListItem Text="Select"></asp:ListItem>
                            <asp:ListItem Text="2018-2019"></asp:ListItem>
                            <asp:ListItem Text="2019-2020"></asp:ListItem>
                            <asp:ListItem Text="2020-2021"></asp:ListItem>
                            <asp:ListItem Text="2021-2022"></asp:ListItem>
                            <asp:ListItem Text="2022-2023"></asp:ListItem>
                            <asp:ListItem Text="2023-2024"></asp:ListItem>
                            <asp:ListItem Text="2024-2025"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
            </div>
        </div>

        <!-- ===== GRID SECTION ===== -->
        <div class="card">
            <div class="card-header fw-bold bg-light">
                Storage & Rent Bill Pendency Report
            </div>

            <div class="card-body table-responsive">
                <asp:GridView ID="GV_StockReport" runat="server"
                    AutoGenerateColumns="False"
                    ShowFooter="true"
                    CssClass="table table-bordered table-striped table-hover"
                    AlternatingRowStyle-CssClass="alt"
                    PagerStyle-CssClass="pgr">

                    <Columns>

                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Regionnm" HeaderText="Region Name" />
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                        <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" />

                        <asp:BoundField DataField="TotalSCBillGenerated"
                            HeaderText="Total Storage Bill Generated"
                            ItemStyle-HorizontalAlign="Right" />

                        <asp:BoundField DataField="TotalRentBillGenerated"
                            HeaderText="Total Rent Bill Generated"
                            ItemStyle-HorizontalAlign="Right" />

                        <asp:BoundField DataField="PendingRentBillForGenerate"
                            HeaderText="Pending Rent Bill For Generate"
                            ItemStyle-HorizontalAlign="Right" />

                    </Columns>

                    <FooterStyle Font-Bold="true" BackColor="#f1f1f1" />
                </asp:GridView>
            </div>
        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GV_StockReport.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>


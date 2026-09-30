<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Godown_Depositor_Wise_Stock_position_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Godown_Depositor_Wise_Stock_position_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid py-3">
        <!-- Header Section -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center border-bottom pb-2">
                    <div>
                        <h1 class="h4 text-primary mb-0">M.P. WAREHOUSING & LOGISTICS CORPORATION</h1>
                        <h2 class="h5 text-dark mb-0">District, Godown, Depositor, Commodity Wise Stock Position</h2>
                        <div class="mt-2">
                            <span class="badge bg-danger text-white p-2">All quantities in Metric Tons (MT)</span>
                        </div>
                    </div>
                    <div class="text-end">
                        <asp:Label ID="lblmsg" runat="server" CssClass="alert alert-warning d-none mb-0"></asp:Label>
                        <asp:HiddenField ID="hfId" Value="0" runat="server" />
                        <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Filter Section -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Date</label>
                                <asp:TextBox ID="txtpaymentdate" AutoComplete="off" runat="server"
                                    CssClass="form-control datepicker"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Depositor</label>
                                <%--<asp:DropDownList ID="ddlDepositor" runat="server"
                                    CssClass="form-control">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:ListBox ID="ddlDepositor" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Commodity</label>
                                <%--<asp:DropDownList ID="ddlcommodity" runat="server"
                                    CssClass="form-control">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:ListBox ID="ddlcommodity" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
                            </div>
                        </div>

                        <!-- Search Button -->
                        <div class="row mt-4">
                            <div class="col-12 text-center">
                                <asp:Button ID="btnSearch" CssClass="btn btn-success px-4 btn-sm show-loader" ValidationGroup="A" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                        </div>
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
                            <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                CssClass="table table-bordered table-striped table-hover mb-0"
                                ShowFooter="true"
                                OnRowDataBound="GV_StockPositionDetails_OnRowDataBound" 
                                OnRowCreated="GV_StockPositionDetails_RowCreated">--%>
                            <asp:GridView ID="GridView1" runat="server"
                                AutoGenerateColumns="False"
                                OnDataBound="GridView1_DataBound"
                                ShowFooter="true"
                                CssClass="table table-bordered table-striped table-hover">

                                <Columns>
                                    <asp:BoundField DataField="SN" HeaderText="S.No."
                                        HeaderStyle-CssClass="bg-primary text-white text-center align-middle"
                                        ItemStyle-CssClass="text-center align-middle" />

                                    <asp:BoundField DataField="District" HeaderText="District Name"
                                        HeaderStyle-CssClass="bg-primary text-white align-middle"
                                        ItemStyle-CssClass="align-middle" />

                                    <asp:BoundField DataField="GodownName" HeaderText="Godown Name"
                                        HeaderStyle-CssClass="bg-primary text-white align-middle"
                                        ItemStyle-CssClass="align-middle" />

                                    <asp:BoundField DataField="Godown_ID" HeaderText="Godown ID"
                                        HeaderStyle-CssClass="bg-primary text-white text-center align-middle"
                                        ItemStyle-CssClass="text-center align-middle" />

                                    <asp:TemplateField HeaderText="Commodity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("CommodityName") %>'>0</asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Crop Year [2017-18]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY1" runat="server" Text='<%# Eval("[2017-18]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Crop Year [2018-19]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY2" runat="server" Text='<%# Eval("[2018-19]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Crop Year [2019-20]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY3" runat="server" Text='<%# Eval("[2019-20]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Crop Year [2020-21]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY4" runat="server" Text='<%# Eval("[2020-21]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Crop Year [2021-22]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY5" runat="server" Text='<%# Eval("[2021-22]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Crop Year [2022-23]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY6" runat="server" Text='<%# Eval("[2022-23]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Crop Year [2023-24]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY7" runat="server" Text='<%# Eval("[2023-24]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Crop Year [2024-25]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCY8" runat="server" Text='<%# Eval("[2024-25]") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                        </ItemTemplate>
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
                                        <i class="bi bi-info-circle me-2"></i>Select filters and click "Search" to view stock position data.
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Summary Information -->
        <div class="row mt-3">
            <div class="col-12">
                <div class="alert alert-secondary">
                    <strong class="d-block mb-2">Note:</strong>
                    <ul class="list-unstyled mb-0">
                        <li class="mb-1"><i class="bi bi-dot"></i>All quantities are displayed in Metric Tons (MT)</li>
                        <li class="mb-1"><i class="bi bi-dot"></i>Stock position across multiple crop years (2017-18 to 2024-25)</li>
                        <li class="mb-1"><i class="bi bi-dot"></i>Filter by Date, Depositor and Commodity for specific results</li>
                        <li><i class="bi bi-dot"></i>Shows district-wise, godown-wise, and depositor-wise stock details</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        $(document).ready(function () {
            var grid = $('#<%= GridView1.ClientID %>');

            if (grid.find("thead").length > 0) {
                BindDatatable(grid);
            }
        });
    </script>
</asp:Content>



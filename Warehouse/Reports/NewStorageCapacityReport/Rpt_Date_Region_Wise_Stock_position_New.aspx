<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Date_Region_Wise_Stock_position_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Date_Region_Wise_Stock_position_New" %>

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
                        <h2 class="h5 text-dark mb-0">Date, Region, District, Depositor, Commodity Wise Stock Position</h2>
                    </div>
                    <div class="text-end">
                        <span class="badge bg-danger text-white p-2">All quantities in Metric Tons (MT)</span>
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
                            <!-- First Row -->
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Date</label>
                                <asp:TextBox ID="txtpaymentdate" runat="server"
                                    CssClass="form-control datepicker" AutoComplete="off" placeholder="MM/DD/YYYY"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Select Report Type</label>
                                <asp:DropDownList CssClass="form-control" ID="ddldistrict" AutoPostBack="false"
                                    runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Region" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="District" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Depositor" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Godown Type</label>
                                <asp:ListBox ID="ddlgodowntype" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
                            </div>
                        </div>

                        <!-- Second Row -->
                        <div class="row g-3 mt-3">
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Depositor Name</label>
                                <asp:ListBox ID="ddlDepositor" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Commodity</label>
                                <asp:ListBox ID="ddlcommodity" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
                            </div>
                            <div class="col-md-4 d-flex align-items-end">
                                <asp:Button ID="btnshow" Text="Show Details" runat="server"
                                    CssClass="btn btn-primary btn-sm show-loader" OnClick="btnshow_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Region Wise Grid -->
        <asp:Panel ID="divdivision" runat="server" Visible="false">
            <div class="row mb-4">
                <div class="col-12">
                    <div class="card shadow-sm">
                        <div class="card-header bg-primary text-white">
                            <h5 class="mb-0">Region Wise Stock Position</h5>
                        </div>
                        <div class="card-body p-0">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                    CssClass="table table-bordered table-striped table-hover mb-0"
                                    OnRowDataBound="GridView1_RowDataBound"
                                    OnRowCreated="GridView1_RowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                            <ItemStyle CssClass="text-center align-middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Region" HeaderText="Region">
                                            <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Region_ID" HeaderText="Region ID">
                                            <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                            <ItemStyle CssClass="text-center align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Commodity" HeaderText="Commodity">
                                            <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="A" HeaderText="2016-17">
                                            <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="B" HeaderText="2017-18">
                                            <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="C" HeaderText="2018-19">
                                            <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="D" HeaderText="2019-20">
                                            <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="E" HeaderText="2020-21">
                                            <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="F" HeaderText="2021-22">
                                            <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="G" HeaderText="2022-23">
                                            <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="H" HeaderText="2023-24">
                                            <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="I" HeaderText="2024-25">
                                            <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Total">
                                            <HeaderStyle CssClass="bg-primary text-white text-end align-middle fw-bold" />
                                            <ItemStyle CssClass="text-end align-middle fw-bold" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="bg-primary text-white" />
                                    <FooterStyle CssClass="bg-light fw-bold" />
                                    <RowStyle CssClass="align-middle" />
                                    <AlternatingRowStyle CssClass="bg-light" />
                                    <SelectedRowStyle CssClass="table-warning" />
                                    <EmptyDataTemplate>
                                        <div class="alert alert-info text-center m-3">
                                            No data available for Region wise stock position.
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- District Wise Grid -->
        <asp:Panel ID="divdistrivt" runat="server" Visible="false">
            <div class="row mb-4">
                <div class="col-12">
                    <div class="card shadow-sm">
                        <div class="card-header bg-success text-white">
                            <h5 class="mb-0">District Wise Stock Position</h5>
                        </div>
                        <div class="card-body p-0">
                            <div class="table-responsive">
                                <asp:GridView ID="GrdDistrict" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                    CssClass="table table-bordered table-striped table-hover mb-0"
                                    OnRowDataBound="GrdDistrict_RowDataBound"
                                    OnRowCreated="GrdDistrict_RowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-success text-white text-center align-middle" />
                                            <ItemStyle CssClass="text-center align-middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="District" HeaderText="District">
                                            <HeaderStyle CssClass="bg-success text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="District_Id" HeaderText="District ID">
                                            <HeaderStyle CssClass="bg-success text-white text-center align-middle" />
                                            <ItemStyle CssClass="text-center align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Commodity" HeaderText="Commodity">
                                            <HeaderStyle CssClass="bg-success text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="A" HeaderText="2016-17">
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="B" HeaderText="2017-18">
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="C" HeaderText="2018-19">
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="D" HeaderText="2019-20">
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="E" HeaderText="2020-21">
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="F" HeaderText="2021-22">
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="G" HeaderText="2022-23">
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="H" HeaderText="2023-24">
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="I" HeaderText="2024-25">
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Total">
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle fw-bold" />
                                            <ItemStyle CssClass="text-end align-middle fw-bold" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="bg-success text-white" />
                                    <FooterStyle CssClass="bg-light fw-bold" />
                                    <RowStyle CssClass="align-middle" />
                                    <AlternatingRowStyle CssClass="bg-light" />
                                    <SelectedRowStyle CssClass="table-warning" />
                                    <EmptyDataTemplate>
                                        <div class="alert alert-info text-center m-3">
                                            No data available for District wise stock position.
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Depositor Wise Grid -->
        <asp:Panel ID="divDepositor" runat="server" Visible="false">
            <div class="row mb-4">
                <div class="col-12">
                    <div class="card shadow-sm">
                        <div class="card-header bg-info text-white">
                            <h5 class="mb-0">Depositor Wise Stock Position</h5>
                        </div>
                        <div class="card-body p-0">
                            <div class="table-responsive">
                                <asp:GridView ID="GrdDepositor" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                    CssClass="table table-bordered table-striped table-hover mb-0"
                                    OnRowDataBound="GrdDepositor_RowDataBound"
                                    OnRowCreated="GrdDepositor_RowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-info text-white text-center align-middle" />
                                            <ItemStyle CssClass="text-center align-middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name">
                                            <HeaderStyle CssClass="bg-info text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DepositorID" HeaderText="Depositor ID">
                                            <HeaderStyle CssClass="bg-info text-white text-center align-middle" />
                                            <ItemStyle CssClass="text-center align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Commodity" HeaderText="Commodity">
                                            <HeaderStyle CssClass="bg-info text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="A" HeaderText="2016-17">
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="B" HeaderText="2017-18">
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="C" HeaderText="2018-19">
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="D" HeaderText="2019-20">
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="E" HeaderText="2020-21">
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="F" HeaderText="2021-22">
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="G" HeaderText="2022-23">
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="H" HeaderText="2023-24">
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="I" HeaderText="2024-25">
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Total">
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle fw-bold" />
                                            <ItemStyle CssClass="text-end align-middle fw-bold" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="bg-info text-white" />
                                    <FooterStyle CssClass="bg-light fw-bold" />
                                    <RowStyle CssClass="align-middle" />
                                    <AlternatingRowStyle CssClass="bg-light" />
                                    <SelectedRowStyle CssClass="table-warning" />
                                    <EmptyDataTemplate>
                                        <div class="alert alert-info text-center m-3">
                                            No data available for Depositor wise stock position.
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Summary Information -->
        <div class="row mt-3">
            <div class="col-12">
                <div class="alert alert-secondary">
                    <strong class="d-block mb-2">Note:</strong>
                    <ul class="list-unstyled mb-0">
                        <li class="mb-1"><i class="bi bi-dot"></i>All quantities are displayed in Metric Tons (MT)</li>
                        <li class="mb-1"><i class="bi bi-dot"></i>Select Report Type to view Region, District or Depositor wise data</li>
                        <li class="mb-1"><i class="bi bi-dot"></i>Stock position across multiple crop years (2016-17 to 2025-26)</li>
                        <li><i class="bi bi-dot"></i>Use filters to narrow down the report as per your requirements</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        $(document).ready(function () {
            var grid = $('#<%= GridView1.ClientID %>');

            if (grid.find("thead").length > 0) {
                BindDatatable(grid);
            }
        });
    </script>
</asp:Content>

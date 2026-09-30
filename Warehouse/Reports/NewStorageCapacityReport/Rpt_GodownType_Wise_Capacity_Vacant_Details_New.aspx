<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_GodownType_Wise_Capacity_Vacant_Details_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_GodownType_Wise_Capacity_Vacant_Details_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid py-3">
        <!-- Header Section -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center border-bottom pb-2">
                    <div class="d-flex align-items-center">
                        <div>
                            <h1 class="h4 text-primary mb-0">M.P. WAREHOUSING & LOGISTICS CORPORATION</h1>
                            <h2 class="h5 text-dark mb-0">Godown Wise Capacity and Available Stock Position</h2>
                        </div>
                    </div>
                    <div class="text-end">
                        <div class="badge bg-light text-dark p-2">
                            <strong>Date:</strong>
                            <asp:Label ID="labelName" runat="server" CssClass="fw-bold ms-1"></asp:Label>
                        </div>
                        <div class="mt-2">
                            <span class="badge bg-danger text-white p-2">Quantity in Metric Tons (MT)</span>
                        </div>
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
                            <div class="col-md-2"></div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Godown Type</label>
                                <asp:ListBox ID="ddlstorageType" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Storage Type</label>
                                <asp:ListBox ID="ddlGodownType" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
                            </div>
                            <div class="col-md-3 d-flex align-items-end">
                                <asp:Button ID="btnshow" Text="Show Details" runat="server"
                                    CssClass="btn btn-primary btn-sm show-loader" OnClick="btnshow_Click" />
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
                        <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                            AutoGenerateColumns="false"
                            CssClass="table table-bordered table-striped table-hover mb-0">

                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                    <ItemStyle CssClass="text-center align-middle" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="District_Name" HeaderText="District Name">
                                    <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                    <ItemStyle CssClass="align-middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="DepotName" HeaderText="Branch">
                                    <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                    <ItemStyle CssClass="align-middle" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TypeofGodown" HeaderText="Type of Godown">
                                    <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                    <ItemStyle CssClass="align-middle" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Godown Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalGodown") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                    <ItemStyle CssClass="align-middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Godown Capacity (MT)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("GodownCapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                    <ItemStyle CssClass="text-end align-middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Current Stock (MT)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Quantityofstockstoredinwarehouse") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                    <ItemStyle CssClass="text-end align-middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Vacant Capacity (MT)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCVC" runat="server" Text='<%# Eval("currentlyvacantcapacity") %>'></asp:Label>
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
                                    <i class="bi bi-info-circle me-2"></i>No data available. Please select filters to view capacity details.
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
                    <li class="mb-1"><i class="bi bi-dot"></i>Godown Capacity: Total storage capacity of the godown</li>
                    <li class="mb-1"><i class="bi bi-dot"></i>Current Stock: Quantity of stock currently stored</li>
                    <li><i class="bi bi-dot"></i>Vacant Capacity: Available space for additional storage</li>
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

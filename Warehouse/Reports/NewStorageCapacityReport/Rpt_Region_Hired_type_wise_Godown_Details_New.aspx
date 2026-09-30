<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Region_Hired_type_wise_Godown_Details_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Region_Hired_type_wise_Godown_Details_New" %>

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
                            <h2 class="h5 text-dark mb-0">District Hired Type Wise Godown Capacity and Available Stock Position</h2>
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

        <!-- Location Filters -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Division Name</label>
                                <asp:DropDownList CssClass="form-control show-loader-ddl" ID="ddldivision" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">District Name</label>
                                <asp:DropDownList CssClass="form-control show-loader-ddl" ID="ddldistrict" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Branch Name</label>
                                <asp:DropDownList CssClass="form-control show-loader-ddl" ID="ddlbranch" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-6">
                                <label class="form-label fw-bold text-secondary mb-1">Godown Type</label>
                                <asp:DropDownList ID="ddlstorageType" runat="server"
                                    CssClass="form-control show-loader-ddl"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlstorageType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label fw-bold text-secondary mb-1">Storage Type</label>
                                <asp:DropDownList ID="ddlGodownType" runat="server"
                                    CssClass="form-control show-loader-ddl"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlGodownType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Status Message -->
        <div class="row mb-3">
            <div class="col-12">
                <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-warning d-flex align-items-center mb-0 d-none"></asp:Label>
            </div>
        </div>

        <!-- Region Wise Grid -->
        <asp:Panel ID="divdivision" runat="server" Visible="false">
            <div class="row mb-4">
                <div class="col-12">
                    <div class="card shadow-sm">
                        <div class="card-header bg-primary text-white">
                            <h5 class="mb-0">Region Wise Capacity & Stock Position</h5>
                        </div>
                        <div class="card-body p-0">
                            <div class="table-responsive">
                                <asp:GridView runat="server" ID="GridView1" ShowFooter="true"
                                    OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                                    AutoGenerateColumns="false"
                                    CssClass="table table-bordered table-striped table-hover mb-0">

                                    <Columns>
                                        <asp:BoundField DataField="Regionnm" HeaderText="Region">
                                            <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Region_ID" HeaderText="Region ID">
                                            <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                            <ItemStyle CssClass="text-center align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TypeofGodown" HeaderText="Type of Godown">
                                            <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Total Godowns">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalGodown") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
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
                                    <EmptyDataTemplate>
                                        <div class="alert alert-info text-center m-3">
                                            No data available for Region wise view.
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
        <asp:Panel ID="divdistrict" runat="server" Visible="false">
            <div class="row mb-4">
                <div class="col-12">
                    <div class="card shadow-sm">
                        <div class="card-header bg-success text-white">
                            <h5 class="mb-0">District Wise Capacity & Stock Position</h5>
                        </div>
                        <div class="card-body p-0">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                    CssClass="table table-bordered table-striped table-hover mb-0"
                                    OnRowDataBound="GridView2_RowDataBound"
                                    OnRowCreated="GridView2_RowCreated">

                                    <Columns>
                                        <asp:BoundField DataField="District_Name" HeaderText="District Name">
                                            <HeaderStyle CssClass="bg-success text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TypeofGodown" HeaderText="Type of Godown">
                                            <HeaderStyle CssClass="bg-success text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Total Godowns">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalGodown") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Godown Capacity (MT)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("GodownCapacity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Stock (MT)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Quantityofstockstoredinwarehouse") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vacant Capacity (MT)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCVC" runat="server" Text='<%# Eval("currentlyvacantcapacity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-success text-white text-end align-middle fw-bold" />
                                            <ItemStyle CssClass="text-end align-middle fw-bold" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <HeaderStyle CssClass="bg-success text-white" />
                                    <FooterStyle CssClass="bg-light fw-bold" />
                                    <RowStyle CssClass="align-middle" />
                                    <AlternatingRowStyle CssClass="bg-light" />
                                    <SelectedRowStyle CssClass="table-warning" />
                                    <EmptyDataTemplate>
                                        <div class="alert alert-info text-center m-3">
                                            No data available for District wise view.
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Branch Wise Grid -->
        <asp:Panel ID="divBranch" runat="server" Visible="false">
            <div class="row mb-4">
                <div class="col-12">
                    <div class="card shadow-sm">
                        <div class="card-header bg-info text-white">
                            <h5 class="mb-0">Branch Wise Capacity & Stock Position</h5>
                        </div>
                        <div class="card-body p-0">
                            <div class="table-responsive">
                                <asp:GridView ID="grdbranch" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                    CssClass="table table-bordered table-striped table-hover mb-0"
                                    OnRowDataBound="grdbranch_RowDataBound"
                                    OnRowCreated="grdbranch_RowCreated">

                                    <Columns>
                                        <asp:BoundField DataField="Branch_Name" HeaderText="Branch">
                                            <HeaderStyle CssClass="bg-info text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TypeofGodown" HeaderText="Type of Godown">
                                            <HeaderStyle CssClass="bg-info text-white align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Total Godowns">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAQ" runat="server" Text='<%# Eval("TotalGodown") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Godown Capacity (MT)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("GodownCapacity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Stock (MT)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Quantityofstockstoredinwarehouse") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vacant Capacity (MT)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCVC" runat="server" Text='<%# Eval("currentlyvacantcapacity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-info text-white text-end align-middle fw-bold" />
                                            <ItemStyle CssClass="text-end align-middle fw-bold" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <HeaderStyle CssClass="bg-info text-white" />
                                    <FooterStyle CssClass="bg-light fw-bold" />
                                    <RowStyle CssClass="align-middle" />
                                    <AlternatingRowStyle CssClass="bg-light" />
                                    <SelectedRowStyle CssClass="table-warning" />
                                    <EmptyDataTemplate>
                                        <div class="alert alert-info text-center m-3">
                                            No data available for Branch wise view.
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Godown Wise Grid -->
        <asp:Panel ID="DivGodown" runat="server" Visible="false">
            <div class="row mb-4">
                <div class="col-12">
                    <div class="card shadow-sm">
                        <div class="card-header bg-warning text-dark">
                            <h5 class="mb-0">Godown Wise Capacity & Stock Position</h5>
                        </div>
                        <div class="card-body p-0">
                            <div class="table-responsive">
                                <asp:GridView ID="GrdGodown" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                    CssClass="table table-bordered table-striped table-hover mb-0"
                                    OnRowDataBound="GrdGodown_RowDataBound"
                                    OnRowCreated="GrdGodown_RowCreated">

                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-warning text-dark text-center align-middle" />
                                            <ItemStyle CssClass="text-center align-middle" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name">
                                            <HeaderStyle CssClass="bg-warning text-dark align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TypeofGodown" HeaderText="Type of Godown">
                                            <HeaderStyle CssClass="bg-warning text-dark align-middle" />
                                            <ItemStyle CssClass="align-middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Godown Capacity (MT)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWHRQ" runat="server" Text='<%# Eval("GodownCapacity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-warning text-dark text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Stock (MT)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAverg" runat="server" Text='<%# Eval("Quantityofstockstoredinwarehouse") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-warning text-dark text-end align-middle" />
                                            <ItemStyle CssClass="text-end align-middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vacant Capacity (MT)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCVC" runat="server" Text='<%# Eval("currentlyvacantcapacity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="bg-warning text-dark text-end align-middle fw-bold" />
                                            <ItemStyle CssClass="text-end align-middle fw-bold" />
                                        </asp:TemplateField>
                                    </Columns>

                                    <HeaderStyle CssClass="bg-warning text-dark" />
                                    <FooterStyle CssClass="bg-light fw-bold" />
                                    <RowStyle CssClass="align-middle" />
                                    <AlternatingRowStyle CssClass="bg-light" />
                                    <SelectedRowStyle CssClass="table-warning" />
                                    <EmptyDataTemplate>
                                        <div class="alert alert-info text-center m-3">
                                            No data available for Godown wise view.
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
                        <li class="mb-1"><i class="bi bi-dot"></i>Hired type wise analysis of godown capacity and stock position</li>
                        <li class="mb-1"><i class="bi bi-dot"></i>View at Region, District, Branch or Godown level based on filters</li>
                        <li><i class="bi bi-dot"></i>Export options available for PDF and Excel formats</li>
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
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>


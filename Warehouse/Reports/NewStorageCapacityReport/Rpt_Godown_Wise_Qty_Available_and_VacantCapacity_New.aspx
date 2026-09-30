<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Godown_Wise_Qty_Available_and_VacantCapacity_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Godown_Wise_Qty_Available_and_VacantCapacity_New" %>

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
                        <h2 class="h5 text-dark mb-0">Branch & Godown & Date wise Stock Position</h2>
                    </div>
                    <div class="text-end">
                        <div class="badge bg-danger text-white p-2">
                            <strong>All quantities in Metric Tons (MT)</strong>
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
                        <!-- First Row -->
                        <div class="row g-3 mb-3">
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Date (DD/MM/YYYY)</label>
                                <asp:TextBox ID="txtpaymentdate" CssClass="form-control datepicker"
                                    AutoComplete="off" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Crop Year</label>
                                <asp:DropDownList ID="ddlcropyear" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Depositor Name</label>
                                <asp:DropDownList ID="ddlDepositor" runat="server" CssClass="form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlDepositor_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Division Name</label>
                                <asp:DropDownList CssClass="form-control" ID="ddldivision" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <!-- Second Row -->
                        <div class="row g-3 mb-3">
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">District Name</label>
                                <asp:DropDownList CssClass="form-control" ID="ddldistrict" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Branch Name</label>
                                <asp:DropDownList CssClass="form-control" ID="ddlbranch" AutoPostBack="false"
                                    runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Godown Type</label>
                                <%--<asp:DropDownList CssClass="form-control" ID="ddlgodowntype" AutoPostBack="false"
                                    runat="server" OnSelectedIndexChanged="ddlgodowntype_SelectedIndexChanged">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:ListBox ID="ddlgodowntype" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Storage Type</label>
                                <%--<asp:DropDownList CssClass="form-control" ID="ddlStorageType" AutoPostBack="false" 
                                    runat="server">
                                    <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:ListBox ID="ddlStorageType" runat="server" SelectionMode="Multiple"
                                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
                            </div>
                        </div>

                        <!-- Show Button -->
                        <div class="row mt-3">
                            <div class="col-12 text-center">
                                <asp:Button ID="btnshow" Text="Show Details" runat="server"
                                    CssClass="btn btn-success btn-sm px-4 show-loader" OnClick="btnshow_Click" />
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
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ShowFooter="true"
                                CssClass="table table-bordered table-striped table-hover mb-0">

                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" Width="5%" />
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
                                        <i class="bi bi-info-circle me-2"></i>Select filters and click "Show Details" to view data.
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
                        <li class="mb-1"><i class="bi bi-dot"></i>Select date range and other filters to view stock position</li>
                        <li class="mb-1"><i class="bi bi-dot"></i>Shows available quantity and vacant capacity for each godown</li>
                        <li><i class="bi bi-dot"></i>Report includes branch-wise and godown-wise detailed analysis</li>
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




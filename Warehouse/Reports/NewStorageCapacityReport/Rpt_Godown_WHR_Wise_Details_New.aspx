<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Godown_WHR_Wise_Details_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Godown_WHR_Wise_Details_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
    <div class="container-fluid py-3">
        <!-- Header Section -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center border-bottom pb-2">
                    <div>
                        <h1 class="h4 text-primary mb-0">M.P. WAREHOUSING & LOGISTICS CORPORATION</h1>
                        <h2 class="h5 text-dark mb-0">Godown WHR Wise Details (Quantity in Quintals)</h2>
                    </div>
                    <div class="text-end">
                        <div class="badge bg-light text-dark p-2">
                            <strong>Date:</strong> 
                            <asp:Label ID="labelName" runat="server" CssClass="fw-bold ms-1"></asp:Label>
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
                        <div class="row g-3 align-items-end">
                            <div class="col-md-5 col-lg-4">
                                <label class="form-label fw-bold text-secondary mb-1">Crop Year</label>
                                <asp:DropDownList ID="ddlCropYear" runat="server" 
                                    CssClass="form-control"
                                    AutoPostBack="true" 
                                    OnSelectedIndexChanged="ddlCropYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-5 col-lg-4">
                                <label class="form-label fw-bold text-secondary mb-1">Depositor</label>
                                <asp:DropDownList ID="ddldepositor" runat="server" 
                                    CssClass="form-control"
                                    AutoPostBack="true" 
                                    OnSelectedIndexChanged="ddldepositor_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-lg-4">
                                <div class="text-center text-md-end">
                                    <span class="badge bg-info fs-6 p-2">Quantity in Quintals</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Status Message -->
        <asp:Panel ID="pnlMessage" runat="server" Visible="false">
            <div class="row mb-3">
                <div class="col-12">
                    <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-warning d-flex align-items-center mb-0"></asp:Label>
                </div>
            </div>
        </asp:Panel>

        <!-- Data Grid Section -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="GrdGodown" runat="server" 
                                AutoGenerateColumns="false"
                                CssClass="table table-bordered table-striped table-hover mb-0"
                                ShowFooter="true">
                                <Columns>
                                    <asp:BoundField DataField="SNO" HeaderText="S.No." 
                                        HeaderStyle-CssClass="bg-primary text-white text-center align-middle" 
                                        ItemStyle-CssClass="text-center align-middle" />
                                    
                                    <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="WHR No." 
                                        HeaderStyle-CssClass="bg-primary text-white text-center align-middle" 
                                        ItemStyle-CssClass="text-center align-middle" />
                                    
                                    <asp:BoundField DataField="WHRDATE" HeaderText="WHR Date" 
                                        HeaderStyle-CssClass="bg-primary text-white text-center align-middle" 
                                        ItemStyle-CssClass="text-center align-middle" 
                                        DataFormatString="{0:dd/MM/yyyy}" />
                                    
                                    <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor" 
                                        HeaderStyle-CssClass="bg-primary text-white align-middle" 
                                        ItemStyle-CssClass="align-middle" />
                                    
                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity" 
                                        HeaderStyle-CssClass="bg-primary text-white align-middle" 
                                        ItemStyle-CssClass="align-middle" />
                                    
                                    <asp:BoundField DataField="CropYear" HeaderText="Crop Year" 
                                        HeaderStyle-CssClass="bg-primary text-white text-center align-middle" 
                                        ItemStyle-CssClass="text-center align-middle" />
                                    
                                    <asp:BoundField DataField="TotalBags_Received" HeaderText="Total Bags Received" 
                                        HeaderStyle-CssClass="bg-primary text-white text-end align-middle" 
                                        ItemStyle-CssClass="text-end align-middle" 
                                        DataFormatString="{0:N0}" />
                                    
                                    <asp:BoundField DataField="Total_Qty_Received" HeaderText="Total Qty. Received (Qtl)" 
                                        HeaderStyle-CssClass="bg-primary text-white text-end align-middle" 
                                        ItemStyle-CssClass="text-end align-middle" 
                                        DataFormatString="{0:N2}" />
                                    
                                    <asp:BoundField DataField="No_Of_Bags" HeaderText="Bags Delivered" 
                                        HeaderStyle-CssClass="bg-primary text-white text-end align-middle" 
                                        ItemStyle-CssClass="text-end align-middle" 
                                        DataFormatString="{0:N0}" />
                                    
                                    <asp:TemplateField HeaderText="Weight Delivered (Qtl)" 
                                        HeaderStyle-CssClass="bg-primary text-white text-end align-middle"
                                        ItemStyle-CssClass="text-end align-middle">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkWeightDelivered" runat="server" 
                                                Target="_blank" 
                                                CssClass="text-decoration-none"
                                                NavigateUrl='<%# "~/StatePages/Rpt_WHRwiseGetpassDetail.aspx?WHRNo=" + Eval("Depositor_WHR_Id") %>'
                                                title="Click to view Getpass Details"
                                                Text='<%# string.Format("{0:N2}", Eval("Bags_Weight")) %>'>
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="AvailableBags" HeaderText="Available Bags" 
                                        HeaderStyle-CssClass="bg-primary text-white text-end align-middle" 
                                        ItemStyle-CssClass="text-end align-middle" 
                                        DataFormatString="{0:N0}" />
                                    
                                    <asp:BoundField DataField="AvailableQty" HeaderText="Available Qty. (Qtl)" 
                                        HeaderStyle-CssClass="bg-primary text-white text-end align-middle" 
                                        ItemStyle-CssClass="text-end align-middle" 
                                        DataFormatString="{0:N2}" />
                                </Columns>
                                
                                <HeaderStyle CssClass="bg-primary text-white" />
                                <FooterStyle CssClass="bg-light fw-bold" />
                                <RowStyle CssClass="align-middle" />
                                <AlternatingRowStyle CssClass="bg-light" />
                                <SelectedRowStyle CssClass="table-warning" />
                                <PagerStyle CssClass="pagination justify-content-center my-3" />
                                <EmptyDataTemplate>
                                    <div class="alert alert-info text-center m-3">
                                        <i class="bi bi-info-circle me-2"></i>No records found for the selected criteria.
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
                        <li class="mb-1"><i class="bi bi-dot"></i> All quantities are displayed in Quintals (Qtl)</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> Click on "Weight Delivered" values to view detailed Getpass information</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> Available quantities are calculated as: Total Received - Total Delivered</li>
                        <li><i class="bi bi-dot"></i> For any discrepancies, please contact the warehouse administrator</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GrdGodown.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>
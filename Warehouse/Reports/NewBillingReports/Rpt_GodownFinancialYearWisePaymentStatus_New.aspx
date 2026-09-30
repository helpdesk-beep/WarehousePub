<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_GodownFinancialYearWisePaymentStatus_New.aspx.cs" Inherits="SRV_Storage_Reports_NewBillingReport_Rpt_GodownFinancialYearWisePaymentStatus_New" %>

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
                        <h2 class="h5 text-dark mb-0">Payment Status - Godown Type & Financial Year Wise</h2>
                    </div>
                    <div class="text-end">
                        <span class="badge bg-danger text-white p-2">Amounts in Crores (Cr.)</span>
                    </div>
                </div>
            </div>
        </div>

        <!-- Filter Section -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <div class="row g-3 justify-content-center">
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Select Godown Type</label>
                                <asp:DropDownList ID="ddlcommodity" CssClass="form-control show-loader-ddl" runat="server" 
                                    OnSelectedIndexChanged="ddlcommodity_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">Select Financial Year</label>
                                <asp:DropDownList ID="ddlFY" CssClass="form-control show-loader-ddl" runat="server" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlFY_SelectedIndexChanged">
                                </asp:DropDownList>
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
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                CssClass="table table-bordered table-striped table-hover mb-0" 
                                OnRowDataBound="GridView1_RowDataBound">
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="GodownType" HeaderText="Godown Type">
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year">
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="Month" HeaderText="Bill Month">
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="SubmittedBillAmount" HeaderText="Bill Submitted to MPSCSC">
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="GrossAmountReceivedFromMPSCSC" HeaderText="Gross Received From MPSCSC">
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="TDSDeductionFromMPSCSC" HeaderText="TDS Deduction">
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="OtherDeductionFromMPSCSC" HeaderText="Other Deduction">
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="PayableAmountReceivedFromMPSCSC" HeaderText="Amount Received After Deduction">
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="RentBillAmount" HeaderText="Rent Bill Amount">
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="PaytoGodownOwnerFromMPWLC" HeaderText="Paid to Godown Owner">
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle fw-bold" />
                                        <ItemStyle CssClass="text-end align-middle fw-bold" />
                                    </asp:BoundField>
                                </Columns>
                                
                                <HeaderStyle CssClass="bg-primary text-white" />
                                <FooterStyle CssClass="bg-light fw-bold" />
                                <RowStyle CssClass="align-middle" />
                                <AlternatingRowStyle CssClass="bg-light" />
                                <SelectedRowStyle CssClass="table-warning" />
                                <PagerStyle CssClass="pagination justify-content-center my-3" />
                                
                                <EmptyDataTemplate>
                                    <div class="alert alert-info text-center m-3">
                                        <i class="bi bi-info-circle me-2"></i>Select Godown Type and Financial Year to view payment status.
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Legend Section -->
        <div class="row mt-3">
            <div class="col-12">
                <div class="alert alert-secondary">
                    <strong class="d-block mb-2">Report Description:</strong> 
                    <ul class="list-unstyled mb-0">
                        <li class="mb-1"><i class="bi bi-dot"></i> Shows payment status for different godown types across financial years</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> Month-wise bill generation, submission, receipt and pending amounts</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> All amounts are displayed in Crores (Cr.)</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> MPSCSC: Madhya Pradesh State Civil Supplies Corporation</li>
                        <li><i class="bi bi-dot"></i> MPWLC: M.P. Warehousing & Logistics Corporation</li>
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
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>


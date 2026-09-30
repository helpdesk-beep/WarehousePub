<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Penging_Rent_Bill_Generation_Against_Received_Amount_From_MPSCSC_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_Penging_Rent_Bill_Generation_Against_Received_Amount_From_MPSCSC_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <style>
        .report-header {
            background: linear-gradient(135deg, #1e90ff 0%, #00bfff 100%);
            color: white;
            padding: 20px;
            border-radius: 8px;
            margin-bottom: 20px;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
        }
        
        .search-panel {
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 8px;
            margin-bottom: 20px;
            border: 1px solid #dee2e6;
        }
        
        .grid-container {
            background: white;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            overflow: hidden;
        }
        
        .grid-header {
            background-color: #343a40;
            color: white;
            padding: 15px;
            border-bottom: 1px solid #dee2e6;
        }
        
        .amount-cell {
            text-align: right;
            font-family: 'Courier New', monospace;
            font-weight: 500;
        }
        
        .total-row {
            background-color: #e8f4fd !important;
            font-weight: bold;
        }
        
        .region-cell {
            font-weight: 600;
            color: #2c3e50;
        }
        
        .btn-custom {
            min-width: 100px;
        }
        
        @media (max-width: 768px) {
            .form-control {
                margin-bottom: 10px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid py-3">
        
        <!-- Report Header -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="report-header text-center">
                    <h2 class="mb-2">M.P. Warehousing & Logistics Corporation</h2>
                    <h4 class="mb-0">Region Wise JVS Payment Status</h4>
                    <p class="mb-0 mt-2">Against Received Payment From MPSCSC (From August 2020)</p>
                </div>
            </div>
        </div>
        
        <!-- Search Panel -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="search-panel">
                    
                    <div class="row align-items-end justify-content-center mb-4">
                        <div class="col-md-3">
                            <label class="form-label">From Date</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtdatefrom" runat="server" 
                                    CssClass="form-control datepicker" 
                                    placeholder="DD/MM/YYYY"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-md-3">
                            <label class="form-label">To Date</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtdateto" runat="server" 
                                    CssClass="form-control datepicker" 
                                    placeholder="DD/MM/YYYY"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-md-2">
                            <asp:Button ID="btnDateSearch" runat="server" Text="Search" 
                                OnClick="btnDateSearch_Click" 
                                CssClass="btn btn-primary btn-custom w-100 show-loader" />
                        </div>
                    </div>
                    <div class="row">
                        
                        
                        <div class="col-md-12 text-center">
                            <h4 class="text-muted mb-0">- OR -</h4>
                        </div>
                    </div>
                    
                    <div class="row align-items-end justify-content-center">
                        <div class="col-md-4">
                            <label class="form-label">Search by UTR Number</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtUTRNo" runat="server" 
                                    onkeypress="return NumberOnly(event);"
                                    CssClass="form-control" 
                                    placeholder="Enter UTR Number"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-md-2">
                            <asp:Button ID="btnUTRSearch" runat="server" Text="Search" 
                                OnClick="btnUTRSearch_Click" 
                                CssClass="btn btn-success btn-custom w-100 show-loader" />
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Data Grid -->
        <div class="row">
            <div class="col-12">
                <div class="grid-container">
                    
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" 
                            OnDataBound="OnDataBound"
                            CssClass="table table-bordered table-striped table-hover Grid" 
                            GridLines="None"
                            EmptyDataText="No records found for the selected criteria.">
                            
                            <HeaderStyle CssClass="table-dark" />
                            <RowStyle CssClass="align-middle" />
                            <AlternatingRowStyle CssClass="bg-light" />
                            <FooterStyle CssClass="total-row" />
                            
                            <Columns>
                                <asp:TemplateField HeaderText="#" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <span class="badge bg-secondary">
                                            <%# Container.DataItemIndex + 1 %>
                                        </span>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <strong>TOTAL</strong>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="Regionnm" HeaderText="Region" 
                                    ItemStyle-CssClass="region-cell" 
                                    HeaderStyle-Width="1500px" />
                                
                                <asp:TemplateField HeaderText="No. of Bills" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTR2BillNo" runat="server" 
                                            Text='<%# Eval("UTR2BillNo", "{0:N0}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalUTR2BillNo" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Gross Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTR2Gross_Amount" runat="server" 
                                            Text='<%# Eval("UTR2Gross_Amount", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalUTR2Gross_Amount" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Pending Bills" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTR9RBillNo" runat="server" 
                                            Text='<%# Eval("UTR9RBillNo", "{0:N0}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalUTR9RBillNo" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Pending Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTR9R_Amount" runat="server" 
                                            Text='<%# Eval("UTR9R_Amount", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalUTR9R_Amount" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Other Deduction" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTR2other_Deduction" runat="server" 
                                            Text='<%# Eval("UTR2other_Deduction", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalUTR2other_Deduction" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Received Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTR2Payable_Amount" runat="server" 
                                            Text='<%# Eval("UTR2Payable_Amount", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalUTR2Payable_Amount" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Rent Bill Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTR3DEDTBill_Amount" runat="server" 
                                            Text='<%# Eval("UTR3DEDTBill_Amount", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalUTR3DEDTBill_Amount" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Passed by AM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOJVS_Bill_Amount" runat="server" 
                                            Text='<%# Eval("POJVS_Bill_Amount", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalPOJVS_Bill_Amount" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="After Deduction" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTR4POJVS_Net_Amount" runat="server" 
                                            Text='<%# Eval("UTR4POJVS_Net_Amount", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalUTR4POJVS_Net_Amount" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Pending AM Approval" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingPassingOrder" runat="server" 
                                            Text='<%# Eval("PendingPassingOrder", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalPendingPassingOrder" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Signed by RM" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTR5RPONet_Amount" runat="server" 
                                            Text='<%# Eval("UTR5RPONet_Amount", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalUTR5RPONet_Amount" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Generated File" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTR6PFCredit_Amount" runat="server" 
                                            Text='<%# Eval("UTR6PFCredit_Amount", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalUTR6PFCredit_Amount" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Pending Generation" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPendingforGenerateFile" runat="server" 
                                            Text='<%# Eval("PendingforGenerateFile", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalPendingforGenerateFile" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Payment to Owner" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="amount-cell">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUTR7PFCredit_Amount" runat="server" 
                                            Text='<%# Eval("UTR7PFCredit_Amount", "{0:N2}") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalUTR7PFCredit_Amount" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            
                            <EmptyDataRowStyle CssClass="text-center py-5" />
                            <EmptyDataTemplate>
                                <div class="text-center py-5">
                                    <i class="fas fa-database fa-3x text-muted mb-3"></i>
                                    <h5 class="text-muted">No records found</h5>
                                    <p class="text-muted">Please try different search criteria</p>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
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


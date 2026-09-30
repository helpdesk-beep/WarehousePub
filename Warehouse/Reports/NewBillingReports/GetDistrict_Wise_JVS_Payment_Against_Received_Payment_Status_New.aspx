<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="GetDistrict_Wise_JVS_Payment_Against_Received_Payment_Status_New.aspx.cs" Inherits="Reports_NewBillingReports_GetDistrict_Wise_JVS_Payment_Against_Received_Payment_Status_New" %>

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
                        <h2 class="h5 text-dark mb-0">District Wise JVS Payment Status Against Received Payment From MPSCSC</h2>
                    </div>
                    <div class="text-end">
                        <span class="badge bg-info text-white p-2">From August 2020 Onwards</span>
                    </div>
                </div>
            </div>
        </div>

        <!-- Search Options -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <!-- Date Range Search -->
                        <div class="row g-3 justify-content-center">
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Payment Date From</label>
                                <asp:TextBox ID="txtdatefrom" runat="server" 
                                    CssClass="form-control datepicker">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label class="form-label fw-bold text-secondary mb-1">Payment Date To</label>
                                <asp:TextBox ID="txtdateto" runat="server" 
                                    CssClass="form-control datepicker" >
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2 d-flex align-items-end">
                                <asp:Button ID="btnDateSearch" runat="server" Text="Search" 
                                    CssClass="btn btn-primary w-100 show-loader" OnClick="btnDateSearch_Click" />
                            </div>
                        </div>
                        
                        <!-- OR Separator -->
                        <div class="row mt-4 mb-4">
                            <div class="col-12">
                                <div class="text-center">
                                    <span class="text-muted fw-bold">OR</span>
                                </div>
                            </div>
                        </div>
                        
                        <!-- UTR Number Search -->
                        <div class="row g-3 justify-content-center">
                            <div class="col-md-4">
                                <label class="form-label fw-bold text-secondary mb-1">UTR Number</label>
                                <asp:TextBox ID="txtUTRNo" runat="server" 
                                    CssClass="form-control"
                                    onkeypress="return NumberOnly(event);">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-2 d-flex align-items-end">
                                <asp:Button ID="btnUTRSearch" runat="server" Text="Search" 
                                    CssClass="btn btn-secondary w-100 show-loader" OnClick="btnUTRSearch_Click" />
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
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                                CssClass="table table-bordered table-striped table-hover mb-0">
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" Width="5%" />
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="Regionnm" HeaderText="Region">
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="District_Name" HeaderText="District">
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:TemplateField HeaderText="SC Bills Received">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUTR2BillNo" runat="server" Text='<%# Eval("UTR2BillNo") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Gross Bill Amt (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUTR2Gross_Amount" runat="server" Text='<%# Eval("UTR2Gross_Amount") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Other Deduction (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUTR2other_Deduction" runat="server" Text='<%# Eval("UTR2other_Deduction") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Received SC Amt (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUTR2Payable_Amount" runat="server" Text='<%# Eval("UTR2Payable_Amount") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty3" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Pending Rent Bills">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUTR9RBillNo" runat="server" Text='<%# Eval("UTR9RBillNo") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Pending Rent Amt (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUTR9R_Amount" runat="server" Text='<%# Eval("UTR9R_Amount") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Received Rent Amt (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUTR3DEDTBill_Amount" runat="server" Text='<%# Eval("UTR3DEDTBill_Amount") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty4" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Passed Amt After Ded (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUTR4POJVS_Net_Amount" runat="server" Text='<%# Eval("UTR4POJVS_Net_Amount") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty6" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Pending at RM (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPendingforGenerateFile" runat="server" Text='<%# Eval("PendingatRM") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty10" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Payment to Owner (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUTR7PFCredit_Amount" runat="server" Text='<%# Eval("UTR7PFCredit_Amount") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty12" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
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
                                        <i class="bi bi-info-circle me-2"></i>Select search criteria and click "Search" to view district wise payment status.
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <!-- Abbreviation Legend -->
        <div class="row mt-3">
            <div class="col-12">
                <div class="alert alert-secondary">
                    <strong class="d-block mb-2">Abbreviations:</strong> 
                    <ul class="list-unstyled mb-0">
                        <li class="mb-1"><i class="bi bi-dot"></i> <strong>JVS:</strong> Joint Venture Statement</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> <strong>MPSCSC:</strong> Madhya Pradesh State Civil Supplies Corporation</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> <strong>UTR:</strong> Unique Transaction Reference</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> <strong>SC:</strong> Storage Charges</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> <strong>RM:</strong> Regional Manager</li>
                        <li><i class="bi bi-dot"></i> <strong>PVT:</strong> Private</li>
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
        $(document).ready(function () {
            var grid = $('#<%= GridView1.ClientID %>');

            // Convert first row to THEAD for DataTable
            grid.prepend($("<thead></thead>").append(grid.find("tr:first")));

            BindDatatable(grid);
        });
    </script>
</asp:Content>


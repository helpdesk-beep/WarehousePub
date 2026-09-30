<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Update_Loss_Gain_by_RM_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_Update_Loss_Gain_by_RM_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
    <div class="container-fluid py-3">
        <!-- Header Section -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="d-flex justify-content-between align-items-center border-bottom pb-2">
                    <div class="d-flex align-items-center">
                        <div>
                            <h1 class="h4 text-primary mb-0">M.P. WAREHOUSING & LOGISTICS CORPORATION</h1>
                            <h2 class="h5 text-dark mb-0">क्षेत्रीय कार्यालय स्तर से 1% आधिक्य के विरुद्ध <br />गोदाम संचालको के देयकों से 20% रोकी गई राशि की जानकारी अपडेट</h2>
                        </div>
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


        <!-- Status Message -->
        <div class="row mb-3">
            <div class="col-12">
                <asp:Label ID="lblMsg" runat="server" CssClass="alert alert-warning d-flex align-items-center mb-0 d-none"></asp:Label>
            </div>
        </div>

        <!-- Data Grid Section -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
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
                                    
                                    <asp:BoundField DataField="Crop_Year" HeaderText="Crop Year">
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:BoundField>
                                    
                                    <asp:TemplateField HeaderText="20% Deduction Amount (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeductionAmount" runat="server" Text='<%# Eval("DeductionAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="20% Deduction Payment (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountQ" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Loss/Gain Deduction (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount_DALG" runat="server" Text='<%# Eval("Amount_DALG") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Other Deductions (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOther_Deduction" runat="server" Text='<%# Eval("Other_Deduction") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="fw-bold">
                                                <asp:Label ID="lblTotalOther" runat="server" />
                                            </div>
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
                                        <i class="bi bi-info-circle me-2"></i>No deduction data available for display.
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <!-- Information Legend -->
        <div class="row mt-3">
            <div class="col-12">
                <div class="alert alert-secondary">
                    <strong class="d-block mb-2">रिपोर्ट विवरण:</strong> 
                    <ul class="list-unstyled mb-0">
                        <li class="mb-1"><i class="bi bi-dot"></i> क्षेत्रीय कार्यालय स्तर से 1% आधिक्य के विरुद्ध गोदाम संचालको के देयकों से 20% राशि रोकी गई है</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> <strong>20% Deduction Amount:</strong> गोदाम संचालकों के देयकों से रोकी गई 20% राशि</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> <strong>20% Deduction Payment:</strong> रोकी गई 20% राशि का भुगतान</li>
                        <li class="mb-1"><i class="bi bi-dot"></i> <strong>Loss/Gain Deduction:</strong> मूल वजन में कमी एवं गेन में कमी के विरुद्ध काटी गई राशि</li>
                        <li><i class="bi bi-dot"></i> <strong>Other Deductions:</strong> अन्य कारणों से काटी गई राशि</li>
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

            grid.prepend($("<thead></thead>").append(grid.find("tr:first")));

            BindDatatable(grid);
        });
    </script>
</asp:Content>


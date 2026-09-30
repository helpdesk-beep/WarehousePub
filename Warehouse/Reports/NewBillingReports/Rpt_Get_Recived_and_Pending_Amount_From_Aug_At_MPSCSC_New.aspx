<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="~/Reports/NewBillingReports/Rpt_Get_Recived_and_Pending_Amount_From_Aug_At_MPSCSC_New.aspx.cs" Inherits="SRV_Storage_Reports_NewBillingReport_Rpt_Get_Recived_and_Pending_Amount_From_Aug_At_MPSCSC_New" %>

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
                        <h2 class="h5 text-dark mb-0">Region Wise Payment Received Details From MPSCSC</h2>
                        <div class="mt-2">
                            <span class="badge bg-info text-white p-2">From August Onwards</span>
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
                                    <asp:BoundField DataField="Regionnm" HeaderText="Region">
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Region_ID" HeaderText="Region ID">
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="District_Name" HeaderText="District">
                                        <HeaderStyle CssClass="bg-primary text-white align-middle" />
                                        <ItemStyle CssClass="align-middle" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Submitted Bills">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoOfSUBBill" runat="server" Text='<%# Eval("NoOfSUBBill") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-center align-middle" />
                                        <ItemStyle CssClass="text-center align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bill Amount (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSUBBillAmt" runat="server" Text='<%# Eval("SUBBillAmt") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="TDS Deduction (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblqty" runat="server" Text='<%# Eval("TDSDeduction") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty2" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Other Deduction (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblqty" runat="server" Text='<%# Eval("OtherDeduction") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty3" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Received Amount (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblqty" runat="server" Text='<%# Eval("PaymentReceivedTilldate") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty4" runat="server" Font-Bold="true" />
                                        </FooterTemplate>
                                        <HeaderStyle CssClass="bg-primary text-white text-end align-middle" />
                                        <ItemStyle CssClass="text-end align-middle" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pending at MPSCSC (₹)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPendingatMPSCSC" runat="server" Text='<%# Eval("PendingatMPSCSC") %>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalqty4" runat="server" Font-Bold="true" />
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
                                        <i class="bi bi-info-circle me-2"></i>No payment data available from MPSCSC.
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
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


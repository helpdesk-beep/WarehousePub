<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="~/Reports/NewStorageCapacityReport/Check_Godown_Status_For_All_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Check_Godown_Status_For_All_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid p-3">

        <!-- Main Heading -->
        <div class="row mb-4">
            <div class="col-12 text-center">
                <h1 class="text-primary fw-bold">
                    <asp:Label ID="lblGodownMaster" runat="server" Text="Godown Details"></asp:Label>
                </h1>
            </div>
        </div>

        <!-- Search Section -->
        <div class="row mb-4 justify-content-center">
            <div class="col-md-8">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <div class="row align-items-center">
                            <div class="col-md-3 text-md-end">
                                <label class="form-label fw-bold mb-2 mb-md-0">Godown ID :</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox runat="server"
                                    CssClass="form-control form-control-lg"
                                    ID="txtGodownID"
                                    placeholder="Enter Godown ID">
                                </asp:TextBox>
                            </div>
                            <div class="col-md-3 mt-3 mt-md-0">
                                <asp:Button runat="server"
                                    Text="Check Status"
                                    ID="btnCheck"
                                    OnClick="btnCheck_Click"
                                    CssClass="btn btn-success btn-sm w-100 show-loader" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Section 1: Godown Details -->
        <div class="row mb-5">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-header bg-primary text-white py-3">
                        <h4 class="mb-0">
                            <i class="fas fa-warehouse me-2"></i>
                            <asp:Label ID="Label1" runat="server" Text="Godown Details in (Qtl.)"></asp:Label>
                        </h4>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="Depositor_Gridview" runat="server"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered table-hover1 mb-0"
                                HeaderStyle-CssClass="table-primary">

                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <span class="badge bg-secondary">
                                                <%# Container.DataItemIndex + 1 %>
                                            </span>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="District Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("District_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Branch Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("DepotName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Godown ID">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Godown_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Registration No">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("GodownReg_No") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Godown Password">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("GodownPassword") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Godown Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Godown_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Scientific Capacity (Qtl.)">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Godown_Scientific_Capacity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-end" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Maximum Capacity (Qtl.)">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("Godown_Capacity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-end" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Hired Type">
                                        <ItemTemplate>
                                            <span class='badge <%# Eval("Hired_Type").ToString().Contains("Hired") ? "bg-warning" : "bg-info" %>'>
                                                <asp:Label runat="server" Text='<%# Eval("Hired_Type") %>'></asp:Label>
                                            </span>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Is Active">
                                        <ItemTemplate>
                                            <span class='badge <%# Eval("IsActive").ToString() == "True" ? "bg-success" : "bg-danger" %>'>
                                                <asp:Label runat="server" Text='<%# Eval("IsActive") %>'></asp:Label>
                                            </span>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Created Date">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%# Eval("CreatedDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                </Columns>

                                <HeaderStyle CssClass="table-primary" />
                                <RowStyle CssClass="align-middle" />
                                <AlternatingRowStyle CssClass="table-light" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Section 2: Available Stock Position -->
        <div class="row mb-5">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-header bg-info text-white py-3">
                        <h4 class="mb-0">
                            <i class="fas fa-box me-2"></i>
                            <asp:Label ID="lblGodownTxndtls" runat="server" Text="Godown Available Stock Position in (MT)"></asp:Label>
                        </h4>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="GV_Capacity" runat="server"
                                AutoGenerateColumns="False"
                                ShowFooter="true"
                                CssClass="table table-bordered table-hover1 mb-0">

                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <span class="badge bg-secondary">
                                                <%# Container.DataItemIndex + 1 %>
                                            </span>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Godown ID">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGodown_ID" Text='<%# Eval("Godown_ID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Godown Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGodown_Name" Text='<%# Eval("Godown_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Godown Vacant Capacity (MT)">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblGodown_Vacant_Capacity" Text='<%# Eval("Godown_Vacant_Capacity")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-end fw-bold text-primary" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Crop Year">
                                        <ItemTemplate>
                                            <span class="badge bg-info">
                                                <asp:Label runat="server" ID="lblCropYear" Text='<%# Eval("CropYear")%>'></asp:Label>
                                            </span>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="WHR Quantity (MT)">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblWHR_Quantity" Text='<%# Eval("WHR_Quantity")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-end fw-bold text-success" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="table-warning fw-bold" />
                                <HeaderStyle CssClass="table-info" />
                                <RowStyle CssClass="align-middle" />
                                <AlternatingRowStyle CssClass="table-light" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Section 3: All Stock Position -->
        <div class="row mb-5">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-header bg-info text-white py-3">
                        <h4 class="mb-0">
                            <i class="fas fa-exchange-alt me-2"></i>
                            <asp:Label ID="Label2" runat="server"
                                Text="Godown All Stock Position - Deposit, Delivery and Available Stock in (MT)">
                            </asp:Label>
                        </h4>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" runat="server"
                                AutoGenerateColumns="False"
                                ShowFooter="true"
                                CssClass="table table-bordered table-hover1 mb-0">

                                <Columns>

                                    <asp:TemplateField HeaderText="Godown Name">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlGodownDetails" runat="server"
                                                Target="_blank"
                                                CssClass="text-decoration-none fw-bold"
                                                NavigateUrl='<%#"~/SRV/Storage_Reports/Inspenctions/Rpt_Godown_WHR_Wise_Details_New.aspx?GodownID="+ Eval("GodownID").ToString()%>'
                                                Text='<%# Eval("Godown_Name") %>'
                                                Style="display: inline-flex; align-content: center; justify-content: center; text-align: center; width: 100%; height: 100%; padding: 8px;">
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="GodownID" HeaderText="Godown ID" />
                                    <asp:BoundField DataField="CropYear" HeaderText="Crop Year" ItemStyle-CssClass="text-center" />
                                    <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name" />
                                    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" />
                                    <asp:BoundField DataField="Total_Qty_Received" HeaderText="Total Qty Received"
                                        ItemStyle-CssClass="text-end fw-bold text-success" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="DeliveryWeight" HeaderText="Delivery Weight"
                                        ItemStyle-CssClass="text-end fw-bold text-danger" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="AvailableQty" HeaderText="Available Qty"
                                        ItemStyle-CssClass="text-end fw-bold text-primary" DataFormatString="{0:N2}" />
                                </Columns>

                                <FooterStyle CssClass="table-warning fw-bold" />
                                <HeaderStyle CssClass="table-warning" />
                                <RowStyle CssClass="align-middle" />
                                <AlternatingRowStyle CssClass="table-light" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Section 4: Payment Status -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-header bg-info text-white py-3">
                        <h4 class="mb-0">
                            <i class="fas fa-file-invoice-dollar me-2"></i>
                            <asp:Label ID="Label3" runat="server"
                                Text="Godown Payment Status (Amount in Lakhs)">
                            </asp:Label>
                        </h4>
                    </div>
                    <div class="card-body p-0">
                        <div class="table-responsive" style="max-height: 600px; overflow-y: auto;">
                            <asp:GridView ID="GrdGodownBill" runat="server"
                                AutoGenerateColumns="false"
                                ShowFooter="true"
                                OnRowCommand="GrdGodownBill_RowCommand"
                                CssClass="table table-bordered table-hover1 mb-0">

                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <span class="badge bg-secondary">
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </span>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center" Width="40px" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Regionnm" HeaderText="Region Name" />
                                    <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                    <asp:BoundField DataField="DepotName" HeaderText="Branch Name" />
                                    <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" />

                                    <asp:BoundField DataField="TotalStorageBill" HeaderText="Total Storage Bill"
                                        ItemStyle-CssClass="text-end" />
                                    <asp:BoundField DataField="Amount" HeaderText="Storage Bill Amount"
                                        ItemStyle-CssClass="text-end" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="TotalSubmittedBill" HeaderText="Submitted to MPSCSC"
                                        ItemStyle-CssClass="text-end" />
                                    <asp:BoundField DataField="SubmittedBillAmount" HeaderText="Amount Submitted"
                                        ItemStyle-CssClass="text-end" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="PendingForSubmission" HeaderText="Pending Submission"
                                        ItemStyle-CssClass="text-end" />
                                    <asp:BoundField DataField="PendingForSubmissionAmount" HeaderText="Pending Amount"
                                        ItemStyle-CssClass="text-end" DataFormatString="{0:N2}" />

                                    <asp:BoundField DataField="TotalBillReceivedPayment" HeaderText="Bills Received"
                                        ItemStyle-CssClass="text-end" />
                                    <asp:BoundField DataField="Gross_Amount" HeaderText="Gross Amount"
                                        ItemStyle-CssClass="text-end" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction"
                                        ItemStyle-CssClass="text-end" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="OtherDeduction" HeaderText="Other Deduction"
                                        ItemStyle-CssClass="text-end" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="Payable_Amount" HeaderText="Net Payable"
                                        ItemStyle-CssClass="text-end fw-bold" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="NoofBillPendingatMPSCSC" HeaderText="Bills Pending"
                                        ItemStyle-CssClass="text-end" />
                                    <asp:BoundField DataField="NoofBillAmountPendingatMPSCSC" HeaderText="Pending Amount"
                                        ItemStyle-CssClass="text-end" DataFormatString="{0:N2}" />

                                    <asp:BoundField DataField="TotalRentBill" HeaderText="Total Rent Bill"
                                        ItemStyle-CssClass="text-end" />
                                    <asp:BoundField DataField="RentBillAmt" HeaderText="Rent Bill Amount"
                                        ItemStyle-CssClass="text-end" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="ReceivedRentBill" HeaderText="Rent Bills Received"
                                        ItemStyle-CssClass="text-end" />
                                    <asp:BoundField DataField="ReceivedRentBillAmount" HeaderText="Rent Amount Received"
                                        ItemStyle-CssClass="text-end" DataFormatString="{0:N2}" />

                                    <asp:BoundField DataField="PayBilltoGodownOwner" HeaderText="Bills to Pay"
                                        ItemStyle-CssClass="text-end" />
                                    <asp:BoundField DataField="PaytoGodownOwner" HeaderText="Payment to Owner"
                                        ItemStyle-CssClass="text-end" DataFormatString="{0:N2}" />
                                    <asp:BoundField DataField="PendingBillatMPWLC" HeaderText="Bills Pending"
                                        ItemStyle-CssClass="text-end" />
                                    <asp:BoundField DataField="PendingBillAmountatMPWLC" HeaderText="Amount Pending"
                                        ItemStyle-CssClass="text-end" DataFormatString="{0:N2}" />

                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:Button ID="btnView" runat="server"
                                                Text="View Details"
                                                CssClass="btn btn-sm btn-outline-info"
                                                CommandName="View"
                                                CommandArgument='<%# Eval("Godown_ID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center" Width="120px" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="table-danger fw-bold" />
                                <HeaderStyle CssClass="table-danger" />
                                <RowStyle CssClass="align-middle" />
                                <AlternatingRowStyle CssClass="table-light" />
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
    <!-- No extra JavaScript - Pure Bootstrap layout -->
</asp:Content>

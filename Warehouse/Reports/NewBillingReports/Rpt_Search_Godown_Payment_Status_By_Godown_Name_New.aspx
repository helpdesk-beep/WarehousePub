<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Search_Godown_Payment_Status_By_Godown_Name_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_Search_Godown_Payment_Status_By_Godown_Name_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid1 mt-4">

        <!-- ===== HEADER ===== -->
        <div class="text-center mb-4">
            <h2 class="fw-bold">M.P. Warehousing & Logistics Corporation</h2>
            <h4 class="fw-normal">Godown Wise Payment Summary (IN LAKH)</h4>
        </div>

        <!-- ===== FILTER / SEARCH ===== -->
        <div class="row justify-content-center mb-4">
            <div class="col-md-2">
                <label class="fw-bold">Godown Name:</label>
            </div>
            <div class="col-md-3 input-group">
                <asp:TextBox ID="txtgodownname" runat="server" CssClass="form-control"
                    Placeholder="Enter Godown Name"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <button class="btn btn-primary h-100 show-loader" id="btn_Search" runat="server"
                    onserverclick="btn_Search_Click">
                    Search</button>
            </div>
        </div>

        <!-- ===== GRID ===== -->
        <div class="row">
            <div class="col-12 table-responsive">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"
                    CssClass="table table-bordered table-striped table-hover" AlternatingRowStyle-CssClass="alt"
                    AllowPaging="false" PagerStyle-CssClass="pgr">

                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="3%">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="Regionnm" HeaderText="Region Name" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="District_Name" HeaderText="District Name" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="DepotName" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name" ItemStyle-HorizontalAlign="Left" />

                        <asp:BoundField DataField="TotalStorageBill" HeaderText="Total Storage Bill" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Amount" HeaderText="Storage Bill Amount" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TotalSubmittedBill" HeaderText="No of Storage Bill Submitted" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="SubmittedBillAmount" HeaderText="Storage Bill Amount Submitted" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PendingForSubmission" HeaderText="Pending Storage Bills" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PendingForSubmissionAmount" HeaderText="Pending Storage Amount" ItemStyle-HorizontalAlign="Right" />

                        <asp:BoundField DataField="TotalBillReceivedPayment" HeaderText="Total Bill Received Payment" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Gross_Amount" HeaderText="Gross Amount to be Received" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="OtherDeduction" HeaderText="Other Deduction" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Payable_Amount" HeaderText="Amount Credited to MPWLC" ItemStyle-HorizontalAlign="Right" />

                        <asp:BoundField DataField="TotalRentBill" HeaderText="Total Rent Bill" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="RentBillAmt" HeaderText="Rent Bill Amount" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="ReceivedRentBill" HeaderText="No of Rent Bill Against Storage Bills" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="ReceivedRentBillAmount" HeaderText="Rent Bill Amount Against Storage Bills" ItemStyle-HorizontalAlign="Right" />

                        <asp:BoundField DataField="PayBilltoGodownOwner" HeaderText="Pay Bill to Godown Owner" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PaytoGodownOwner" HeaderText="Payment Credited to Godown Owner" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PendingBillatMPWLC" HeaderText="Pending Bills at MPWLC" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PendingBillAmountatMPWLC" HeaderText="Pending Amount at MPWLC" ItemStyle-HorizontalAlign="Right" />

                        <asp:TemplateField HeaderText="View Godown Wise Bill Details" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-sm btn-primary" CommandName="View" CommandArgument='<%# Eval("Godown_ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <FooterStyle Font-Bold="True" BackColor="#f1f1f1" ForeColor="Black" />
                </asp:GridView>
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


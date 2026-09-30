<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Payment_Status_OF_Finacial_Year_hired_Type_Wise_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Payment_Status_OF_Finacial_Year_hired_Type_Wise_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div style="height: 500px;">
        <br />
        <div style="text-align: center; font-size: large;">
            <h2 class="header">Financial Year wise Payment Status (Bill Amount) all Amount in Cr. </h2>
        </div>
        <div class="col-md-12 mb-3">
            <div class="table-responsive">

                <div style="overflow-x: scroll;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        CssClass="table table-bordered table-hover Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnDataBound="OnDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="NoOfGenerateBill" HeaderText="No Of Generated Bill by Branch Manager" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="BillAmt" HeaderText="No Of Generated Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="NoOfSUBBill" HeaderText="No Of Submit Bill by Branch Manager" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="SUBBillAmt" HeaderText="No Of Submit Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingForSubmision" HeaderText="Pending Bill For Submision at Branch Laval" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingAmountForSubmision" HeaderText="Pending Bill Amount For Submision at Branch Laval" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="NoofbillPaymentReceivedFromMPSCSC" HeaderText="No of bill Payment Received to be MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Gross_Amount" HeaderText="No of bill Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction from MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaymentDecuctionbyMPSCSC" HeaderText="Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaymentReceivedAfterDeduction" HeaderText="Amount Credit to MPWLC After All Deduction" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingatMPSCSC" HeaderText="Pending Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />

                            <%-- Private Warehouse Details--%>

                            <asp:BoundField DataField="Gross_Amount" HeaderText="Private Warehouse bill Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TDS_Amt" HeaderText="Private Warehouse TDS Deduction from MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaymentDecuctionbyMPSCSC" HeaderText="Private Warehouse Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaymentReceivedAfterDeduction" HeaderText="Private Warehouse Amount Credit to MPWLC Account After All Deduction" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingatMPSCSC" HeaderText="Private Warehouse Pending Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="NoOfBillPayment" HeaderText="Private Warehouse Total Rent Bill Generated" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="RentBillAmt" HeaderText="Private Warehouse Total Rent Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PayToMPWLC" HeaderText="Payment Credit to Godown Owner Account After Deduction" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingatMPWLC" HeaderText="Pending Payment at MPWLC" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div style="text-align: center; font-size: large;">
            <h2 class="header">Financial Year wise Payment Status (Only Amount) all Amount in Cr. </h2>
        </div>
        <div class="col-md-12 mb-3">
            <div class="table-responsive">

                <div style="overflow-x: scroll;">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        CssClass="table table-bordered table-hover Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnDataBound="GridView2_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                            <%--<asp:BoundField DataField="NoOfGenerateBill" HeaderText="No Of Generated Bill by Branch Manager" ItemStyle-HorizontalAlign="Right" />--%>
                            <asp:BoundField DataField="BillAmt" HeaderText="No Of Generated Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="SUBBillAmt" HeaderText="No Of Submit Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <%--<asp:BoundField DataField="NoofbillPaymentReceivedFromMPSCSC" HeaderText="No of bill Payment Received to be MPSCSC" ItemStyle-HorizontalAlign="Right" />--%>
                            <asp:BoundField DataField="Gross_Amount" HeaderText="No of bill Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction from MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaymentDecuctionbyMPSCSC" HeaderText="Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaymentReceivedAfterDeduction" HeaderText="Amount Credit to MPWLC After All Deduction" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingatMPSCSC" HeaderText="Pending Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />

                            <%-- Private Warehouse Details--%>

                            <%--  <asp:BoundField DataField="Gross_Amount" HeaderText="Private Warehouse bill Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Left" />
                         <asp:BoundField DataField="TDS_Amt" HeaderText="Private Warehouse TDS Deduction from MPSCSC" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PaymentDecuctionbyMPSCSC" HeaderText="Private Warehouse Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                         <asp:BoundField DataField="PaymentReceivedAfterDeduction" HeaderText="Private Warehouse Amount Credit to MPWLC Account After All Deduction" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PendingatMPSCSC" HeaderText="Private Warehouse Pending Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />--%>

                            <%--<asp:BoundField DataField="NoOfBillPayment" HeaderText="Private Warehouse Total Rent Bill Generated" ItemStyle-HorizontalAlign="Right" />--%>
                            <asp:BoundField DataField="RentBillAmt" HeaderText="Private Warehouse Total Rent Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <%--<asp:BoundField DataField="PayBilltoGodownOwner" HeaderText="Pay Bill to Godown Owner" ItemStyle-HorizontalAlign="Right" />--%>
                            <asp:BoundField DataField="PayToMPWLC" HeaderText="Payment Credit to Godown Owner Account After Deduction" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingatMPWLC" HeaderText="Pending Payment at MPWLC" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div style="text-align: center; font-size: large;">
            <h2 class="header">Financial Year wise Owned Godown Payment Status (Only Amount) all Amount in Cr. </h2>
        </div>
        <div class="col-md-12 mb-3">
            <div class="table-responsive">

                <div style="overflow-x: scroll;">
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        CssClass="table table-bordered table-hover Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnDataBound="GridView3_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                            <%--<asp:BoundField DataField="NoOfGenerateBill" HeaderText="No Of Generated Bill by Branch Manager" ItemStyle-HorizontalAlign="Right" />--%>
                            <asp:BoundField DataField="BillAmt" HeaderText="No Of Generated Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="SUBBillAmt" HeaderText="No Of Submit Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <%--<asp:BoundField DataField="NoofbillPaymentReceivedFromMPSCSC" HeaderText="No of bill Payment Received to be MPSCSC" ItemStyle-HorizontalAlign="Right" />--%>
                            <asp:BoundField DataField="Gross_Amount" HeaderText="No of bill Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction from MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaymentDecuctionbyMPSCSC" HeaderText="Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaymentReceivedAfterDeduction" HeaderText="Amount Credit to MPWLC After All Deduction" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingatMPSCSC" HeaderText="Pending Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />

                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
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


<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Region_Wise_Payment_status_For_JVS_New.aspx.cs" Inherits="SRV_Storage_Reports_NewBillingReport_Rpt_Region_Wise_Payment_status_For_JVS_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div style="height: 500px;">
        <br />
        <div style="text-align: center; font-size: large;">
            <h2 class="header">Region wise Payment Status (Bill Amount) all Amount in Cr.(Only JVS Godown) </h2>
        </div>
        <div class="container py-4">
            <div class="row">
                <div class="col-sm-3"></div>
                <div class="col-sm-2">
                    <asp:Label ID="Label2" runat="server" Text="Select Financial Year"></asp:Label></div>
                <div class="col-sm-3">
                    <asp:DropDownList ID="ddlFY" CssClass="form-control show-loader-ddl" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFY_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

            <tr>
                <td style="text-align: left;" colspan="10">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        CssClass="table table-bordered table-striped table-hover Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnDataBound="OnDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Region Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Reports/States/Rpt_District_Wise_Payment_status_For_JVS.aspx?Region_ID="+ (Eval("Region_ID").ToString())%>'
                                        title="Region Name" Text=' <%# Eval("Regionnm") %>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TotalGodown" HeaderText="Total Godown" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="TotalStorageBill" HeaderText="No Of Generated Bill by Branch Manager" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Amount" HeaderText="No Of Generated Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TotalBillReceivedPayment" HeaderText="No of bill Payment Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Gross_Amount" HeaderText="No of bill Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction from MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="OtherDeduction" HeaderText="Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Payable_Amount" HeaderText="Amount Credit to MPWLC After All Deduction" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendindAtMPSCSC" HeaderText="Pending Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />

                            <%-- Private Warehouse Details--%>
                            <asp:BoundField DataField="TotalRentBill" HeaderText="Private Warehouse Total Rent Bill Generated" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="RentBillAmt" HeaderText="Private Warehouse Total Rent Bill Amount" ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="ReceivedRentBill" HeaderText="Private Warehouse no. of bill Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="ReceivedRentBillAmount" HeaderText="Private Warehouse Amount Received from MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingatMPSCSC" HeaderText="Private Warehouse Pending Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />


                            <asp:BoundField DataField="PayBilltoGodownOwner" HeaderText="No. of Bill Paid to Godown Owner" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaytoGodownOwner" HeaderText="No. of Bill Amount Paid to Godown Owner" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingatMPWLC" HeaderText="Pending Payment at MPWLC" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <div style="text-align: center; font-size: large;">
            <h2 class="header">Financial Year wise Payment Status (Only Amount) all Amount in Cr. (Only JVS Godown)</h2>
        </div>
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

            <tr>
                <td style="text-align: left;" colspan="10">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        CssClass="table table-bordered table-striped table-hover Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnDataBound="GridView2_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Regionnm" HeaderText="Region" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="Financial_Year" HeaderText="Financial_Year" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="TotalGodown" HeaderText="Total Godown" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="TotalStorageBill" HeaderText="No Of Generated Bill by Branch Manager" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Amount" HeaderText="No Of Generated Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TotalBillReceivedPayment" HeaderText="No of bill Payment Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Gross_Amount" HeaderText="No of bill Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction from MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="OtherDeduction" HeaderText="Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Payable_Amount" HeaderText="Amount Credit to MPWLC After All Deduction" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendindAtMPSCSC" HeaderText="Pending Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />

                            <%-- Private Warehouse Details--%>
                            <asp:BoundField DataField="TotalRentBill" HeaderText="Private Warehouse Total Rent Bill Generated" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="RentBillAmt" HeaderText="Private Warehouse Total Rent Bill Amount" ItemStyle-HorizontalAlign="Right" />

                            <asp:BoundField DataField="ReceivedRentBill" HeaderText="Private Warehouse no. of bill Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="ReceivedRentBillAmount" HeaderText="Private Warehouse Amount Received from MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingatMPSCSC" HeaderText="Private Warehouse Pending Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />


                            <asp:BoundField DataField="PayBilltoGodownOwner" HeaderText="No. of Bill Paid to Godown Owner" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaytoGodownOwner" HeaderText="No. of Bill Amount Paid to Godown Owner" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingatMPWLC" HeaderText="Pending Payment at MPWLC" ItemStyle-HorizontalAlign="Right" />
                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </td>
            </tr>
        </table>

        <div style="text-align: center; font-size: large;">
            <h2 class="header">Financial Year wise Owned Godown Payment Status (Only Amount) all Amount in Cr. </h2>
        </div>
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

            <tr>
                <td style="text-align: left;" colspan="10">
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        CssClass="Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnDataBound="GridView3_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="BillAmt" HeaderText="No Of Generated Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="SUBBillAmt" HeaderText="No Of Submit Bill Amount" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Gross_Amount" HeaderText="No of bill Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction from MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaymentDecuctionbyMPSCSC" HeaderText="Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PaymentReceivedAfterDeduction" HeaderText="Amount Credit to MPWLC After All Deduction" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PendingatMPSCSC" HeaderText="Pending Amount at MPSCSC" ItemStyle-HorizontalAlign="Right" />

                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


   <script type="text/javascript">
       $(document).ready(function () {

           var grid = $('#<%= GridView1.ClientID %>');

        // safety check
        if (grid.length > 0) {

            // THEAD fix for ASP.NET GridView
            if (grid.find("thead").length == 0) {
                grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
            }

            BindDatatable(grid);
        }
    });
   </script>



</asp:Content>


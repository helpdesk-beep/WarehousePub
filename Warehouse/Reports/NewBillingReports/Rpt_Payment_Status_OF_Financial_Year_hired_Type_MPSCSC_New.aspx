<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Payment_Status_OF_Financial_Year_hired_Type_MPSCSC_New.aspx.cs" Inherits="SRV_Storage_Reports_NewBillingReport_Rpt_Payment_Status_OF_Financial_Year_hired_Type_MPSCSC_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div style="height: 500px;">
        <br />
        <div style="text-align: center; font-size: large;">
            <h2 class="header">Financial Year wise Payment Status (Bill Amount) all Amount in Cr.(All Godowns) </h2>
        </div>
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

            <tr>
                <td style="text-align: left;" colspan="10">
                    <asp:GridView ID="GridView1" runat="server"
                        AutoGenerateColumns="False"
                        ShowFooter="true"
                        CssClass="table table-bordered table-striped table-hover Grid"
                        ShowHeaderWhenEmpty="true"
                        AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr"
                        OnDataBound="OnDataBound"
                        OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px"
                                HeaderStyle-HorizontalAlign="Center" />

                            <asp:BoundField DataField="NoOfSUBBill" HeaderText="No of Bill Submitted by BM"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-Width="120px"
                                HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />

                            <asp:BoundField DataField="SUBBillAmt" HeaderText="Submitted Bill Amount by BM"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px"
                                HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" />

                            <asp:BoundField DataField="NoofbillPaymentReceivedFromMPSCSC" HeaderText="No of Bill Payment Received"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-Width="130px"
                                HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N0}" />

                            <asp:BoundField DataField="Gross_Amount" HeaderText="Bill Amount to be Received"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px"
                                HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" />

                            <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-Width="120px"
                                HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" />

                            <asp:BoundField DataField="PaymentDecuctionbyMPSCSC" HeaderText="Other Deduction"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-Width="120px"
                                HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" />

                            <asp:BoundField DataField="PaymentReceivedAfterDeduction" HeaderText="Amount Credit to MPWLC"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px"
                                HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" />

                            <asp:BoundField DataField="PendingatMPSCSC" HeaderText="Pending Amount at MPSCSC"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px"
                                HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" />
                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" BackColor="#E3E3E8" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#3AC0F2" Font-Bold="True" ForeColor="White" Height="30px" />
                        <RowStyle HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />




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

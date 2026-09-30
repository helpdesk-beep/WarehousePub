<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Date_Wise_Payment_Received_From_MPSCSC_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_Date_Wise_Payment_Received_From_MPSCSC_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div style="height: 500px;">
        <br />
        <div style="text-align: center; font-size: large;">
            <h2 class="header">Date wise Payment Received From MPSCSC (Amount in Cr.) </h2>

        </div>
        <div class="row justify-content-center align-items-center text-center" style="font-size: large;">
    <div class="col-md-auto">
        <asp:Label ID="Label2" runat="server" Text="Select Year"></asp:Label>
    </div>
    <div class="col-md-3">
        <asp:DropDownList 
            ID="ddlFY" 
            runat="server" 
            CssClass="form-control show-loader-ddl"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddlFY_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
</div>

        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

            <tr>
                <td style="text-align: left;" colspan="10">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        CssClass="table table-bordered table-striped table-hover mb-0 Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Year" HeaderText="Year" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Received Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" />

                            <asp:BoundField DataField="Gross_Amount" HeaderText="No of bill Amount to be Received From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="TDS_Amt" HeaderText="TDS Deduction from MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="OtherDeduction" HeaderText="Other Deduction From MPSCSC" ItemStyle-HorizontalAlign="Right" />
                            <%--<asp:BoundField DataField="Payable_Amount" HeaderText="Amount Credit to MPWLC After All Deduction" ItemStyle-HorizontalAlign="Right" />--%>
                            <asp:TemplateField HeaderText="Amount Credit to MPWLC After All Deduction">
                                <ItemTemplate>
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Reports/NewBillingReports/Rpt_Get_Godown_Wise_Date_Wise_Payment_Status_Date_Wise_All_New.aspx?PaymentDate="+ (Eval("PaymentDate").ToString())%>'
                                        title="Amount" Text=' <%# Eval("Payable_Amount") %>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle Font-Bold="True" ForeColor="Black" />
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


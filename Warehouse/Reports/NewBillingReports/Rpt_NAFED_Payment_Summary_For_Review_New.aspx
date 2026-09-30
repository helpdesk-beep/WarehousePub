<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_NAFED_Payment_Summary_For_Review_New.aspx.cs" Inherits="Reports_NewBillingReports_Rpt_NAFED_Payment_Summary_For_Review_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
    <div>
    <fieldset>
        <legend align="center">Region Wise NAFED Summary From 1 April 2024(Only Pvt. Warehouse) Amount <label style="color: red">(in Cr.)</label></legend>
        
        <div id="divdivision" runat="server" visible="false" class="row" style="margin-top: 15px">
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                    CssClass="table table-bordered table-striped table-hover GridViewScrollHeader" 
                    AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.(1)">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Region" HeaderText="Region(2)" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="AmountRecivedFromNAfed" HeaderText="SC Amount Recived From NAfed(3)" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TotalRentBillAmountD" HeaderText="Total Rent Bill Amount(4)" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TotalAmountPassedbyRMAfterAllDeduction" HeaderText="Total Passed Rent Amt. by RM(5)" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TotalDeductionbyRM" HeaderText="Total Deduction by RM(6)" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PendingatRM" HeaderText="Pending at RM For Passing Order(7=4-5+6)" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PaytogodownOwner" HeaderText="Pay to Godown Owner(8)" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PendingatRMForPaytogodownOwner" HeaderText="Pending at RM for Pay to godown Owner(9=5-8)" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                    <FooterStyle BackColor="#666633" Font-Bold="True" ForeColor="Black" />
                </asp:GridView>
            </div>
        </div>

    </fieldset>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageWidget" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">


    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);

    </script>
</asp:Content>


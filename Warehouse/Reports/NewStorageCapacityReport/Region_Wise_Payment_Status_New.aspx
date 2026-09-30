<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Region_Wise_Payment_Status_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Region_Wise_Payment_Status_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
    <div>
    <div style="text-align: center; font-size: large;">
        <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
        <h4 class="header">Region Wise Payment Status</h4>
    </div>
   <div class="col-md-12">
    <div class="table-responsive">

        <div style="overflow-x: scroll;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                    CssClass="table table-bordered table-hover Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" >
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Region Name">
                            <ItemTemplate>
                                <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Reports/States/District_Wise_Payment_Status.aspx?Region_ID="+ (Eval("Region_ID").ToString())%>'
                                    title="Region Name" Text=' <%# Eval("RegionName") %>' ForeColor="Blue"></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="District_Name" HeaderText="District Name"  ItemStyle-HorizontalAlign="Left"/>--%>
                        <asp:BoundField DataField="TotalBillPresentedinFY" HeaderText="वित्‍तीय वर्ष में प्रस्तुत देयकों की संख्या" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TotalBillAmountPresented" HeaderText="वित्‍तीय वर्ष में प्रस्तुत देयकों की राशि (राशि Cr.रुपये में)" ItemStyle-HorizontalAlign="Right" />

                        <asp:BoundField DataField="TotalAmountReceivedInFY" HeaderText="प्राप्त राशि का विवरण (राशि Cr.रुपये में" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="RemainingAmountFromDepositor" HeaderText="शेष लंबित राशि (राशि Cr.रुपये में)" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                    <FooterStyle Font-Bold="True" ForeColor="Black" />
                </asp:GridView>
            </div>
        </div>
    </div>
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



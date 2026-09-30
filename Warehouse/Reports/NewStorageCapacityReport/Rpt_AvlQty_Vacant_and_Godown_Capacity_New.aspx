<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_AvlQty_Vacant_and_Godown_Capacity_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_AvlQty_Vacant_and_Godown_Capacity_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" Runat="Server">
    <div>
    <fieldset>
        <legend align="center">District Wise Capacity & Avl. Stock & Vacant Capacity
            <label style="color: red">(In M.T.)</label></legend>
        <div class="row" style="margin-top: 15px">
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                    OnRowCreated="GridView1_RowCreated"
                    CssClass="table-bordered table-hover Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Region Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblRegion_Name" Text='<%# Eval("Region") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="District Name">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDistrict_Name" Text='<%# Eval("District_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Godown Capacity">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGodownCapacity" Text='<%# Eval("GodownCapacity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Available Quantity">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAvlQty" Text='<%# Eval("AvlQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vacant Capacity">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblVacant_Capacity" Text='<%# Eval("Vacant_Capacity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle Font-Bold="True" ForeColor="Black" />
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


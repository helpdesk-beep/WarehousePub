<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WareHouseMaster.master" CodeFile="~/Reports/NewStorageCapacityReport/Rpt_Godown_Wise_Qty_Available_Last_10_Year.aspx.cs" Inherits="Reports_States_Rpt_Branch_Wise_Qty_Available_Last_10_Year" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
    <%--    <script src="../../JS/gridviewscroll.js"></script>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="row justify-content-center">
        <div class="col-md-12 ml-2">
            <div class="p-3 mb-4 bg-light shadow-sm rounded">
                <h3 class="text-center text-danger mb-0">
                    <i class="fas fa-warehouse me-2"></i>
                    Region-wise Report of Commodity (Paddy-Common, Bajra, jowar)
                </h3>
            </div>

        </div>

        <div class="form-group" style="text-align: left; font-size: large; margin-left: 12px;">
            <h4 class="header text-center">District,Branch and Godown Wise Stock Position In Qtl.</h4>
        </div>
        <div class="form-group">
            <div class="col-md-2"></div>
            <div class="col-md-2 fw-bold text-end align-middle">
                <asp:Label ID="Label2" runat="server" Text="Select Commodity"></asp:Label>
            </div>
            <div class="col-md-3">
                <asp:ListBox ID="drpDwnCommodity" runat="server" SelectionMode="Multiple"
                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success show-loader" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div class="col-md-12 ml-12">
            <div class="form-group">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ShowFooter="true"
                    CssClass="table-bordered table-hover" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                    <HeaderStyle CssClass="GridViewheader" />
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle Font-Bold="True" ForeColor="Black" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPageWidget" runat="Server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPageScript" runat="Server">
    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);
    </script>
</asp:Content>


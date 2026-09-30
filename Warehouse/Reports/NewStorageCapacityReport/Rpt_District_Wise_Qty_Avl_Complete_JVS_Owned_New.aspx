<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_Wise_Qty_Avl_Complete_JVS_Owned_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_District_Wise_Qty_Avl_Complete_JVS_Owned_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="m-1">
        <fieldset>
            <legend align="center">Region,District wise Total Capacity,Available Capacity & Vacant Capacity Report<label style="color: red">(In M.T.)</label></legend>
            <div class="row mb-4">
                <div class="col-md-1"></div>
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server">Select Commodity</asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:ListBox ID="drpDwnCommodity" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblHiredType" runat="server">Select Hired Type</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlHiredType" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                        CssClass="btn btn-success btn-sm show-loader" OnClick="btnSearch_Click" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
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



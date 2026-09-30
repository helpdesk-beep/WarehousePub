<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_Wise_DCC_Stock_Position_Entry_by_BM_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_District_Wise_DCC_Stock_Position_Entry_by_BM_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="py-1">
        <div style="text-align: center; font-size: large;">
            <h2 class="header">Distrct wise DCC Stock Position Entry by BM &nbsp;&nbsp;<h3>
                <asp:Label ID="lbldate" runat="server"></asp:Label></h3>
            </h2>
        </div>
        <div class="row mb-3">
            <div class="col-md-1"></div>
            <div class="col-md-1">
                <asp:Label ID="Label3" runat="server" Text="Region : "></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control show-loader-ddl" AutoPostBack="true" selectionmode="Multiple" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:Label ID="Label2" runat="server" Text="Crop Year : "></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlcropyear" runat="server" CssClass="form-control show-loader-ddl" AutoPostBack="true" selectionmode="Multiple" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:Label ID="Label1" runat="server" Text="Commodity : "></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:ListBox ID="ddlComodity" runat="server" SelectionMode="Multiple"
                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-success show-loader" OnClick="btnshow_Click" />
            </div>
        </div>
        <div class="col-md-12">
            <div class="table-responsive">

                <div style="overflow-x: scroll;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table table-bordered table-hover Grid GridViewScrollHeader" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="2%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="District_Name" HeaderText="District" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="Commodity" HeaderText="Commodity" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="DCCStockEntryByBM" HeaderText="DCC Stock Entry By BM" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%" />
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


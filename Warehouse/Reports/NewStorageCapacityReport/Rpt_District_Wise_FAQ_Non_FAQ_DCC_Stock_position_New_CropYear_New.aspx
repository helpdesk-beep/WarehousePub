<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_Wise_FAQ_Non_FAQ_DCC_Stock_position_New_CropYear_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_District_Wise_FAQ_Non_FAQ_DCC_Stock_position_New_CropYear_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <fieldset>
            <legend align="center">District wise FAQ, Non-FAQ and DCC Stock Entry by BM
            <label style="color: red">(In M.T.)</label></legend>
            <div class="row" style="text-align: center; font-size: large;">

                <div class="col-md-2"></div>
                <div class="col-md-1">
                    <asp:Label ID="Label2" runat="server" Font-Size="10pt" Font-Bold="true">Commodity :</asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:ListBox ID="ddlComodity" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="true">Crop Year :</asp:Label>
                </div>
                <div class="col-md-1">
                    <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="true" CssClass="form-control show-loader-ddl" selectionmode="Multiple" OnSelectedIndexChanged="ddlcropyear_SelectedIndexChanged1">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-success btn-sm show-loader" OnClick="btnshow_Click" />
                </div>
            </div>
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table table-bordered table-hover Grid GridViewScrollHeader" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="District_Name" HeaderText="District Name" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="DCC_Stock" HeaderText="DCC Stock Entry By BM" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="infestedstock" HeaderText="कीटग्रस्‍त स्‍कंध की मात्रा" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="doughformation" HeaderText="आटा फारमेशन की मात्रा" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="foreignmatter" HeaderText="फारेन मेटर युक्‍त स्‍कंध की मात्रा" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="badgunnybags" HeaderText="खराब बारदाने में भंडारित स्‍कंध की मात्रा" ItemStyle-HorizontalAlign="Right" />
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


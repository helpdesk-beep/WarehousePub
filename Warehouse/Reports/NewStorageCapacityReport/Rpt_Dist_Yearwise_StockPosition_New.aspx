<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Dist_Yearwise_StockPosition_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Dist_Yearwise_StockPosition_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div class="container-fluid1">
        <!-- Title -->
        <div class="row mb-3">
            <div class="col-12 text-center">
                <h5 class="fw-bold">Report for Stock Position District Wise – Year Wise
                <span class="text-danger">(In M.T.)</span>
                </h5>
            </div>
        </div>

        <!-- Row 1 -->
        <div class="row mb-3 align-items-center">

            <div class="col-lg-2 col-md-3 col-sm-4">
                <asp:Label ID="lblCropYear" runat="server" CssClass="fw-bold" Text="Crop Year"></asp:Label>
                <asp:DropDownList ID="ddlCropYear" runat="server" CssClass="form-control">
                    <asp:ListItem Text="2018-19" />
                    <asp:ListItem Text="2019-20" />
                    <asp:ListItem Text="2020-21" />
                    <asp:ListItem Text="2021-22" />
                    <asp:ListItem Text="2022-23" />
                    <asp:ListItem Text="2023-24" />
                    <asp:ListItem Text="2024-25" />
                </asp:DropDownList>
            </div>

            <div class="col-lg-3 col-md-4 col-sm-4">
                <asp:Label ID="lblRegion" runat="server" CssClass="fw-bold" Text="Region"></asp:Label>
                <asp:DropDownList ID="ddlRegion" runat="server"
                    CssClass="form-control"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="col-lg-3 col-md-4 col-sm-4">
                <asp:Label ID="lblDistrict" runat="server" CssClass="fw-bold" Text="District"></asp:Label>
                <asp:DropDownList ID="ddlDistrict" runat="server"
                    CssClass="form-control"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-lg-3 col-md-4 col-sm-6">
                <asp:Label ID="lblCommodity" runat="server" CssClass="fw-bold" Text="Commodity"></asp:Label>
                <asp:ListBox ID="ddlCommodity" runat="server"
                    SelectionMode="Multiple"
                    CssClass="checkbox-multiselect form-control">

                    <asp:ListItem Text="Jowar" Value="11" />
                    <asp:ListItem Text="Bajra" Value="8" />
                    <asp:ListItem Text="Maize" Value="12" />
                    <asp:ListItem Text="Rice" Value="3" />
                    <asp:ListItem Text="Wheat-PSS" Value="22" />

                </asp:ListBox>
            </div>

            <div class="col-lg-1 col-md-3 col-sm-4" style="margin-top: 17px;">
                <asp:Button ID="btnView"
                    runat="server"
                    CssClass="btn btn-success w-100 btn-sm show-loader"
                    Text="View"
                    Font-Bold="true"
                    OnClick="btnView_Click" />
            </div>

        </div>
        <table>
            <tr>
                <td style="text-align: left;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                        CssClass="table-bordered table-hover Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnSelectedIndexChanged="GV_StockReport_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                            <asp:BoundField DataField="BranchName/IssueCenter" HeaderText="Branch" />
                            <asp:BoundField DataField="GodownName" HeaderText="Godown" />
                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year" />
                            <asp:BoundField DataField="Depositor" HeaderText="Depositor" />
                            <asp:BoundField DataField="ArrivalSourceDetails" HeaderText="Arrival Source" />
                            <asp:BoundField DataField="ReceivedQty" HeaderText="Received Qty" />
                            <asp:BoundField DataField="DeliveredQty" HeaderText="Delivered Qty" />
                            <asp:BoundField DataField="BalanceQty" HeaderText="Balance Qty" />
                            <asp:BoundField DataField="Remark" HeaderText="Remark/StockCondition" ItemStyle-Width="15%" />

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
    <script>
        var grid = $('#<%= GridView1.ClientID %>');

        // Convert GridView header row into THEAD (DataTable requirement)
        grid.prepend($("<thead></thead>").append(grid.find("tr:first")));
        BindDatatable(grid);
    </script>
</asp:Content>


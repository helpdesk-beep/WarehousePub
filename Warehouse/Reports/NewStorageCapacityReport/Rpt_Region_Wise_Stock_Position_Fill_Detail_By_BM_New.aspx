<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Region_Wise_Stock_Position_Fill_Detail_By_BM_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Region_Wise_Stock_Position_Fill_Detail_By_BM_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <div style="text-align: center; font-size: large;">
            <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h4 class="header">गोदामो में भण्‍डारित स्‍कंध की अवधि अनुसार जानकारी (in MT)</h4>
        </div>
        <div class="col-md-2"></div>
        <div class="col-md-1">
            <asp:Label ID="Label1" runat="server" Text="Commodity"></asp:Label>
        </div>
        <div class="col-md-2">
            <asp:ListBox ID="ddlcommodity" runat="server" SelectionMode="Multiple"
                CssClass="checkbox-multiselect form-control"></asp:ListBox>
        </div>
        <div class="col-md-1">
            <asp:Label ID="Label2" runat="server" Text="Crop Year"></asp:Label>
        </div>
        <div class="col-md-2">
            <asp:DropDownList ID="ddlddlCropYear" CssClass="form-control" runat="server" >
            </asp:DropDownList>
        </div>
        <div class="col-md-1">
            <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-success btn-sm show-loader" OnClick="btnshow_Click" />
        </div>
        <table style="border: solid 5px #e3e3e8; width: 100%; vertical-align: central;">

            <tr>
                <td style="text-align: left;">
                    <asp:GridView ID="GridView1" runat="server"
                        AutoGenerateColumns="False"
                        ShowFooter="true"
                        UseAccessibleHeader="true"
                        OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" OnDataBound="OnDataBound"
                        CssClass="table table-bordered table-hover Grid">
                        <Columns>
                            <asp:BoundField DataField="RegionName" HeaderText="Region Name" />
                            <asp:BoundField DataField="RegionID" HeaderText="RegionID" />
                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year" />

                            <asp:TemplateField HeaderText="6 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTG" runat="server" Text='<%# Eval("StockPositionIn6Months") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="9 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblnoofgdwn" runat="server" Text='<%# Eval("StockPositionIn9Months") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty1" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="12 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingNooGodown" runat="server" Text='<%# Eval("StockPositionIn12Months") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblPTotalqty2" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="18 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalBills" runat="server" Text='<%# Eval("StockPositionIn18Months") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblPTotalqty3" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="24 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblNoOfGenerateBill" runat="server" Text='<%# Eval("StockPositionIn24Months") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty4" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="30 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblNoofBillGenerateAmt" runat="server" Text='<%# Eval("StockPositionIn30Months") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty5" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="36 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingNoOfGenerateBill" runat="server" Text='<%# Eval("StockPositionIn36Months") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblPTotalqty6" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="5 वर्ष से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblPendingNoOfGenerateAmt" runat="server" Text='<%# Eval("StockPositionFiveYear") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblPTotalqty20" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="5 वर्ष से अधिक से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblNoOfDSCSingBill" runat="server" Text='<%# Eval("StockPositionGreaterthanFiveYear") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty8" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="कुल भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblNoofBillAmtDSC" runat="server" Text='<%# Eval("Total") %>' />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div style="text-align: right;">
                                        <asp:Label ID="lblTotalqty9" runat="server" Font-Bold="true" />
                                    </div>
                                </FooterTemplate>
                            </asp:TemplateField>
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

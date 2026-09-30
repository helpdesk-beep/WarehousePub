<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_CropYear_Wise_StockPosition_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_District_CropYear_Wise_StockPosition_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <div style="text-align: center; font-size: large;">
            <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h4 class="header">Report For Review of District , Crop Year Wise, Commodity Wise Stock Position Report (Month wise in MT) </h4>
        </div>
        <div class="col-md-12">
            <div class="form-group">
                <div class="col-sm-8 col-sm-offset-4">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hfId" Value="0" runat="server" />
                    <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-1">
                    <asp:Label ID="Label1" runat="server" Text="Region"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control show-loader-ddl" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="Label2" runat="server" Text="District"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddldistrict" runat="server" CssClass="form-control show-loader-ddl" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="Label3" runat="server" Text="Branch"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlbranch" runat="server" CssClass="form-control show-loader-ddl" AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" selectionmode="Multiple">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="Label4" runat="server" Text="Depositer"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlDepositor" runat="server" CssClass="form-control" selectionmode="Multiple">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                    <asp:Label ID="Label5" runat="server" Text="Commodity"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ddlcommodity" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="Label6" runat="server" Text="Crop Year"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlCropYear" CssClass="form-control" runat="server" selectionmode="Multiple">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="2017-18">2017-18</asp:ListItem>
                        <asp:ListItem Value="2018-19">2018-19</asp:ListItem>
                        <asp:ListItem Value="2019-20">2019-20</asp:ListItem>
                        <asp:ListItem Value="2020-21">2020-21</asp:ListItem>
                        <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                        <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                        <asp:ListItem Value="2023-24">2023-24</asp:ListItem>
                        <asp:ListItem Value="2024-25">2024-25</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSearch" CssClass="btn btn-success btn-sm show-loader" ValidationGroup="A"
                        runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>
            </div>
            <div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
            <div class="col-md-12">
                <div class="table-responsive">

                    <div style="overflow-x: scroll;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                            EnableModelValidation="True" CssClass="table table-bordered table-hover GridViewScrollHeader"
                            CellPadding="4" rules="all" ForeColor="#333333" ShowFooter="true"
                            OnRowDataBound="GV_StockPositionDetails_OnRowDataBound">
                            <RowStyle BackColor="White" CssClass="ADFieldsGridText table-responsive" HorizontalAlign="Left" />
                            <Columns>

                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Region">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("RegionName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DistrictName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BranchName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Crop Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCropYear" runat="server" Height="21px" Text='<%# Eval("CropYear") %>'> 
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Depositor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepositor" runat="server" Text='<%# Eval("DepositorName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Commodity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCommodity" runat="server" Text='<%# Eval("CommodityName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="6 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSixMonth" runat="server" Text='<%# Eval("StockPositionIn6Months") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="9 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNineMonth" runat="server" Text='<%# Eval("StockPositionIn9Months") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="12 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTwelveMonth" runat="server" Text='<%# Eval("StockPositionIn12Months") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="18 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEighteenMonth" runat="server" Text='<%# Eval("StockPositionIn18Months") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="24 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTwentyFourMonth" runat="server" Text='<%# Eval("StockPositionIn24Months") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="30 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblThirtyMonth" runat="server" Text='<%# Eval("StockPositionIn30Months") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="36 माह से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblThirtySixMonth" runat="server" Text='<%# Eval("StockPositionIn36Months") %>'>0</asp:Label>
                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="पांच वर्ष से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblThirtyMonth" runat="server" Text='<%# Eval("StockPositionFiveYear") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="पांच वर्ष से अधिक से भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblThirtyMonth" runat="server" Text='<%# Eval("StockPositionGreaterthanFiveYear") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="कुल भण्‍डारित मात्रा" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#719cb6" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" Font-Size="12pt" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                Height="20px" Font-Size="12pt" />
                            <AlternatingRowStyle BackColor="#eeeeee" />
                        </asp:GridView>
                    </div>
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


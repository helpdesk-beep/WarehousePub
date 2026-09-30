<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_DistrictAll_CropYear_Depositorwise_StockPosition_NAFED_Dlahan_Tilhan_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_DistrictAll_CropYear_Depositorwise_StockPosition_NAFED_Dlahan_Tilhan_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <div style="text-align: center; font-size: large;">
            <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h4 class="header">Summary Report for Review of Depositor (DMO MARKFED, NAFED, NCCF), CropYear Wise, Commodity Wise Stock Position (Quantity in MT) </h4>
        </div>
        <div class="col-md-12">
            <div class="form-group">
                <div class="col-sm-8 col-sm-offset-4">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hfId" Value="0" runat="server" />
                    <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
                </div>
            </div>
            <div>
                <div class="col-md-3"></div>
                <div class="col-md-1">
                    <asp:Label ID="Label1" runat="server">Commodity :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ddlcommodity" runat="server"
                        SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control">

                        <asp:ListItem Text="Dlahan" Value="Dlahan"></asp:ListItem>
                        <asp:ListItem Text="Tilhan" Value="Tilhan"></asp:ListItem>

                    </asp:ListBox>

                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnSearch" CssClass="btn btn-success btn-sm show-loader" ValidationGroup="A"
                        runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>



                <br />


            </div>
            <div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
            <div class="col-md-12">
                <div class="table-responsive">

                    <div style="overflow-x: scroll;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                            EnableModelValidation="True"
                            CellPadding="4" rules="all" ShowFooter="true"
                            OnRowDataBound="GV_StockPositionDetails_OnRowDataBound" CssClass="table-bordered table-hover "
                            OnRowCreated="GV_StockPositionDetails_OnRowCreated">
                            <RowStyle BackColor="White" CssClass="ADFieldsGridText table-responsive" HorizontalAlign="Left" />
                            <Columns>

                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Depositor">
                                    <ItemTemplate>

                                        <asp:HyperLink ID="lblDepositor" runat="server" Target="_blank" NavigateUrl='<%#"~/Reports/States/Rpt_District_CropYear_Depositorwise_StockPosition_NAFED_NCCF_DMOMARKFED.aspx?DepositorID="+ (Eval("Depositor_ID").ToString())+ "&CommodityID="+ (Eval("Commodity_Id").ToString())%>'
                                            title="DepositorName" Text=' <%# Eval("DepositorName") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Commodity">
                                    <ItemTemplate>

                                        <asp:HyperLink ID="lblCommodity" runat="server" Target="_blank" NavigateUrl='<%#"~/Reports/States/Rpt_District_CropYear_Depositorwise_StockPosition_NAFED_NCCF_DMOMARKFED.aspx?CommodityID="+ (Eval("Commodity_Id").ToString())+ "&DepositorID="+ (Eval("Depositor_ID").ToString())%>'
                                            title="CommodityName" Text=' <%# Eval("CommodityName") %>' ForeColor="Blue"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year [2017-18]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCY1" runat="server" Text='<%# Eval("[2017-18]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year [2018-19]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCY2" runat="server" Text='<%# Eval("[2018-19]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year [2019-20]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCY3" runat="server" Text='<%# Eval("[2019-20]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year [2020-21]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCY4" runat="server" Text='<%# Eval("[2020-21]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Crop Year [2021-22]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCY5" runat="server" Text='<%# Eval("[2021-22]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year [2022-23]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCY6" runat="server" Text='<%# Eval("[2022-23]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year [2023-24]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCY7" runat="server" Text='<%# Eval("[2023-24]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Crop Year [2024-25]" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCY8" runat="server" Text='<%# Eval("[2024-25]") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
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


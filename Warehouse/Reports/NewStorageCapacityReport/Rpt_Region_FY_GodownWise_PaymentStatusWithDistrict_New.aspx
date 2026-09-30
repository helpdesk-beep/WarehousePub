<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Region_FY_GodownWise_PaymentStatusWithDistrict_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Region_FY_GodownWise_PaymentStatusWithDistrict_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <div style="text-align: center; font-size: large;">
            <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h4 class="header">Report For Review of Region,&nbsp; Financial Year Wise, Godown wise Payment Position Report (Amount in Cr Rs) </h4>
        </div>
        <div class="col-md-12">
            <div class="form-group">
                <div class="col-sm-8 col-sm-offset-4">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hfId" Value="0" runat="server" />
                    <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
                </div>
            </div>

            <div class="row col-md-12 mb-3">
                <div class="col-md-1">
                    <asp:Label ID="lblRegion" Font-Bold="true" runat="server" ForeColor="Navy">संभाग:</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlRegion" runat="server" selectionmode="Multiple" CssClass="form-control show-loader-ddl" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblDistrict" Font-Bold="true" runat="server" ForeColor="Navy">जिला:</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddldistrict" runat="server" AutoPostBack="true" CssClass="form-control show-loader-ddl" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged" selectionmode="Multiple">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblBranch" Font-Bold="true" runat="server" ForeColor="Navy">शाखा:</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlbranch" runat="server" AutoPostBack="true" CssClass="form-control show-loader-ddl" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged" selectionmode="Multiple">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblFY" Font-Bold="true" runat="server" ForeColor="Navy">वित्‍तीय वर्ष:</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control" selectionmode="Multiple">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="2010-11">2010-11</asp:ListItem>
                        <asp:ListItem Value="2011-12">2011-12</asp:ListItem>
                        <asp:ListItem Value="2012-13">2012-13</asp:ListItem>
                        <asp:ListItem Value="2013-14">2013-14</asp:ListItem>
                        <asp:ListItem Value="2014-15">2014-15</asp:ListItem>
                        <asp:ListItem Value="2015-16">2015-16</asp:ListItem>
                        <asp:ListItem Value="2016-17">2016-17</asp:ListItem>
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
            </div>
            <div class="row col-md-12 mb-3">
                <div class="col-md-1">
                    <asp:Label ID="lblGodownType" Font-Bold="true" runat="server" ForeColor="Navy">गोदाम का प्रकार:</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlGodownType" runat="server" CssClass="form-control" selectionmode="Multiple">
                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnSearch" CssClass="btn btn-success btn-sm show-loader" ValidationGroup="A"
                        runat="server" Text="Search" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-1"></div>
            </div>

            <div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
            <div class="col-md-12">
                <div class="table-responsive">

                    <div style="overflow-x: scroll;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" BackColor="White"
                            EnableModelValidation="True" CssClass="table table-bordered table-hover GridViewScrollHeader"
                            CellPadding="4" rules="all" ForeColor="#333333" ShowFooter="true"
                            OnRowDataBound="GV_FYGodownWisePayment_OnDataBound">
                            <RowStyle BackColor="White" CssClass="ADFieldsGridText table-responsive" HorizontalAlign="Left" />
                            <Columns>

                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="संभाग">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("RegionName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="जिला">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DistrictName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="शाखा">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BranchName") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="वित्‍तीय वर्ष">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFinancialYear" runat="server" Height="21px" Text='<%# Eval("FinancialYear") %>'> 
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="गोदाम का प्रकार">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGodownType" runat="server" Text='<%# Eval("GodownType") %>'>0</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="वित्‍तीय वर्ष में किराये की कुल राशि (राशि Cr.रुपये में)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFY_GodownRentAmount" Enabled="false" runat="server" Text='<%# Eval("TotalAmountInRentInFY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="वित्‍तीय वर्ष में भुगतान राशि (राशि Cr.रुपये में)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFY_AmountPaymentToGodown" Enabled="false" runat="server" Text='<%# Eval("TotalAmountPaidToGodownOwnerInFY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="गोदाम संचालक की शेष लंबित राशि (राशि Cr.रुपये में)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGodownOwnerPendingAmount" Enabled="false" runat="server" Text='<%# Eval("RemainingAmountOfGodownOwner") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
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

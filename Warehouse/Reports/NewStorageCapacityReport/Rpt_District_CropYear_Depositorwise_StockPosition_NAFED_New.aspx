<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_CropYear_Depositorwise_StockPosition_NAFED_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_District_CropYear_Depositorwise_StockPosition_NAFED_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <div style="text-align: center; font-size: large;">
            <h2 class="header">M.P. WAREHOUSING & LOGISTICS CORPORATION</h2>
            <h4 class="header">Report For Review of District, Depositor, CropYear Wise, Commodity Wise Stock Availability Position Report Till Date(Quantity in MT) </h4>
        </div>
        <div class="col-md-12">
            <div class="form-group">
                <div class="col-sm-8 col-sm-offset-4">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <asp:HiddenField ID="hfId" Value="0" runat="server" />
                    <asp:HiddenField ID="hfFileName" Value="0" runat="server" />
                </div>
            </div>
            <div class="col-sm-2"></div>
            <div class="col-sm-1">
                <asp:Label ID="lblDepositer" runat="server" Text="Depositer:"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:DropDownList ID="ddlDepositor" runat="server" CssClass="form-control" selectionmode="Multiple">
                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-1">
                <asp:Label ID="lblCommodity" runat="server" Text="Commodity:"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:ListBox ID="ddlcommodity" runat="server" SelectionMode="Multiple"
                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
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
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" Width="100%" BackColor="White"
                        EnableModelValidation="True"
                        CellPadding="4" rules="all" ForeColor="#333333" ShowFooter="true"
                        CssClass="table table-bordered table-hover"
                        OnRowDataBound="GV_StockPositionDetails_OnRowDataBound">
                        <RowStyle BackColor="White" CssClass="ADFieldsGridText table-responsive" HorizontalAlign="Left" />
                        <Columns>

                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
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



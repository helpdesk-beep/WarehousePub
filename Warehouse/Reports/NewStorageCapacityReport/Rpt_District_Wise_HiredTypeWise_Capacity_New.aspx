<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_Wise_HiredTypeWise_Capacity_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_District_Wise_HiredTypeWise_Capacity_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <div style="text-align: center; font-size: large;">
            <h2 class="header">District wise, Hired Type wise Godown Count and Capacity Report</h2>
        </div>
        <div class="row py-2">
            <div class="col-sm-1">
                <asp:Label ID="lblHiredType" runat="server" Text="District"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control show-loader-ddl" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="Label1" runat="server" Text="Select Hired Type"></asp:Label>
            </div>
            <div class="col-sm-2">
                <asp:ListBox ID="ddlHiredType" runat="server" SelectionMode="Multiple"
                    CssClass="checkbox-multiselect form-control"></asp:ListBox>
            </div>
            <div class="col-sm-2">
                <asp:Label ID="Label2" runat="server" Text="Storage Type"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlstoragetype" runat="server" CssClass="form-control show-loader-ddl" AutoPostBack="true" OnSelectedIndexChanged="ddlstoragetype_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-1">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Visible="true" Width="100px" ValidationGroup="A"
                    CssClass="btn btn-success btn-sm show-loader" OnClick="btnSearch_Click" />
            </div>
        </div>
        <div class="col-md-12">
            <div class="table-responsive">

                <div style="overflow-x: scroll;">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table table-bordered table-hover Grid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DistrictName" HeaderText="District Name" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Hired Type" HeaderText="Hired Type" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="TotalGodown" HeaderText="Total No. Of Godowns" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="GodownScientificCapacity" HeaderText="GodownScientificCapacity(In MT)" ItemStyle-HorizontalAlign="Right" />
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


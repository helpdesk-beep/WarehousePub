<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Branch_Commodity_Wise_Stock_Position_For_Spicial_PV_DistrictWIse_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Branch_Commodity_Wise_Stock_Position_For_Spicial_PV_DistrictWIse_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <fieldset>
            <legend align="center"> <i class="fas fa-warehouse me-2"></i> District,Branch,Godown,Commodity Stock Position<label style="color: red">(In M.T.)</label></legend>
            <div class="row" style="text-align: center; font-size: large;">
                <div class="col-md-2" style="margin-top: 5px">
                    <asp:Label ID="lblDistrict" runat="server" Font-Size="10pt" Font-Bold="true">Date (DD-MM-YYYY) :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtpaymentdate" CssClass="datepicker form-control" AutoComplete="off" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="true">District :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ddlRegion" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-success btn-sm show-loader" OnClick="btnshow_Click" />
                </div>
            </div>
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover" AlternatingRowStyle-CssClass="alt"
                        OnRowDataBound="GridView1_OnRowDataBound"
                        OnRowCreated="GridView1_OnRowCreated" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:BoundField DataField="Godown" HeaderText="Godown Name" />
                            <asp:BoundField DataField="Godown_ID" HeaderText="Godown_ID" />
                            <asp:BoundField DataField="Commodity_Name" HeaderText="commodity" />
                            <asp:BoundField DataField="CropYear" HeaderText="Crop Year" />


                            <asp:TemplateField HeaderText="Bag Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblBag_Balance" runat="server" Text='<%# Eval("Bag_Balance") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Weight Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
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


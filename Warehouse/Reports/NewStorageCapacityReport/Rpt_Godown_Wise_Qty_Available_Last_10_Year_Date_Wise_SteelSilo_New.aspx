<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_SteelSilo_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_SteelSilo_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <fieldset>
            <legend align="center">Branch,Godown Date wise Stock Position SteelSilo<label style="color: red">(In M.T.)</label></legend>
            <div class="row" style="text-align: center; font-size: large;">
                <div class="col-md-2">
                    <asp:Label ID="lblDistrict" runat="server" Font-Size="10pt" Font-Bold="true">Date (DD-MM-YYYY) :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtpaymentdate" CssClass="datepicker form-control" AutoComplete="off" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="true">Commodity :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ddlComodity" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-success btn-sm show-loader" OnClick="btnshow_Click" />
                </div>
            </div>
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" ShowFooter="true"
                        CssClass="table-bordered table-hover" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
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


<%@ Page Title="" Language="C#" MasterPageFile="~/WareHouseMaster.master" AutoEventWireup="true" CodeFile="Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_Depositor_New.aspx.cs" Inherits="SRV_Storage_Reports_Inspenctions_Rpt_District_Wise_Qty_Available_Last_10_Year_Date_Wise_Depositor_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPageHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPageBody" runat="Server">
    <div>
        <fieldset>
            <legend align="center">District, Depositor, Commodity And Date Wise Stock Position<label style="color: red">(In M.T.)</label></legend>
            <div class="row" style="text-align: center; font-size: large;">
                <div class="col-md-2" style="margin-top: 5px">
                    <asp:Label ID="lblDistrict" runat="server" Font-Size="10pt" Font-Bold="true">Date (DD-MM-YYYY) :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox CssClass="datepicker form-control" ID="txtpaymentdate" AutoComplete="off" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="Label1" runat="server" Font-Size="10pt" Font-Bold="true">Commodity :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ddlComodity" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" Font-Size="10pt" Font-Bold="true">Depositor :</asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:ListBox ID="ddlDepositor" runat="server" SelectionMode="Multiple"
                        CssClass="checkbox-multiselect form-control"></asp:ListBox>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnshow" Text="Show Details" runat="server" CssClass="btn btn-success btn-sm show-loader" OnClick="btnshow_Click" />
                </div>
            </div>
            <div class="row" style="margin-top: 15px">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        CssClass="table-bordered table-hover GridViewScrollHeader" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="District Name">
                                <ItemTemplate>
                                    <asp:HyperLink ID="sdffsft" runat="server" Target="_blank" NavigateUrl='<%#"~/Inspections/Reports/Rpt_Godown_Wise_Qty_Available_Last_10_Year_Date_Wise_New.aspx?District_Id="+ (Eval("District_Id").ToString())%>'
                                        title="District Name" Text=' <%# Eval("District") %>' ForeColor="Blue"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="A" HeaderText="[2016-17]" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="B" HeaderText="[2017-18]" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="C" HeaderText="[2018-19]" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="D" HeaderText="[2019-20]" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="E" HeaderText="[2020-21]" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="F" HeaderText="[2021-22]" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="G" HeaderText="[2022-23]" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="H" HeaderText="[2023-24]" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="I" HeaderText="[2024-25]" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right" />
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


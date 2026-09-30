<%@ Page Language="C#" MasterPageFile="~/MasterPage/WarehouseApplication.master"
    AutoEventWireup="true" CodeFile="DeleteDeliveryOrder.aspx.cs" Inherits="IssueCenterLevel_Storage_DeleteDeliveryOrder"
    Title="Delete Delivery Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td style="width: 100px; height: 24px;">
                <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Bold="True"></asp:Label></td>
            <td style="border-right: balck 1px solid; border-top: balck 1px solid; border-left: balck 1px solid;
                width: 110px; border-bottom: balck 1px solid; border-collapse: collapse; height: 24px;
                text-align: left">
                <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                </asp:DropDownList></td>
            <td style="width: 100px; height: 24px;">
                <asp:Label ID="lblDepot" runat="server" Text="Branch" Font-Bold="True"></asp:Label></td>
            <td style="border-right: balck 1px solid; border-top: balck 1px solid; border-left: balck 1px solid;
                width: 110px; border-bottom: balck 1px solid; border-collapse: collapse; height: 24px;
                text-align: left">
                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="4" style="height: 24px">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="For delete delivery oreder first select from list."></asp:Label></td>
        </tr>
        <tr>
            <td colspan="4" style="height: 18px" valign="top">
                <asp:Panel ID="panelContainer" runat="server" BorderColor="WhiteSmoke" Height="250px"
                    ScrollBars="Vertical" Width="100%">
                    <asp:GridView ID="gvDeliveryOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                        GridLines="Vertical" DataKeyNames="StockDeliveryOrder_Id" Width="561px" OnSelectedIndexChanged="gvDeliveryOrder_SelectedIndexChanged">
                        <RowStyle BackColor="#F7F7DE" />
                        <Columns>
                            <asp:CommandField HeaderText="Action" ShowSelectButton="True">
                                <ItemStyle ForeColor="#0000C0" />
                            </asp:CommandField>
                            <asp:BoundField DataField="StockDeliveryOrder_Id" HeaderText="DO Id." Visible="False" />
                            <asp:BoundField DataField="Delivery_Order_No" HeaderText="DO No.">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DODate" HeaderText="DO_Date">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Qty" HeaderText="Qty">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Wet" HeaderText="Weight">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GatePassNO" HeaderText="Gate Pass No.">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" BorderColor="#404040"
                            BorderStyle="Solid" BorderWidth="1px" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 10px">
                &nbsp;<asp:HiddenField ID="hfGatePassNo" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="height: 14px">
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </td>
            <td style="height: 14px">
                <asp:ImageButton ID="imgDelete" runat="server" Height="31px" OnClick="imgDelete_Click"
                    Width="137px" ImageUrl="~/images/delNew.jpg" Visible="false" /></td>
            <td style="height: 14px">
                <asp:HiddenField ID="hfQty" runat="server" />
                <asp:ImageButton ID="ImageButton1" runat="server" Height="21px" ImageUrl="~/images/close.jpg"
                    OnClick="ImageButton1_Click" Width="37px" /></td>
            <td style="height: 14px">
                <asp:HiddenField ID="hfBags" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

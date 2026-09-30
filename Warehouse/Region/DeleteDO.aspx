<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="DeleteDO.aspx.cs" Inherits="Region_DeleteDO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <span style="color: White; font-size: 12pt; font-weight: bold">Delete Delivery Order</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 150px" align="left">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="tb6">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 100px" align="left">
                                                    <asp:Label ID="lblBranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlDepotList_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    &nbsp;</td>
                                                <td style="width: 150px" align="left">
                                                    &nbsp;</td>
                                                <td style="width: 100px" align="left">
                                                    &nbsp;</td>
                                                <td style="width: 200px" align="left">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    Commodity:</td>
                                                <td style="width: 150px" align="left">
                                                    <asp:DropDownList ID="ddlcommodity" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 100px" align="left">
                                                    &nbsp;</td>
                                                <td style="width: 200px" align="left">
                                                    <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Submit" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    &nbsp;</td>
                                                <td style="width: 150px" align="left">
                                                    &nbsp;</td>
                                                <td style="width: 100px" align="left">
                                                    &nbsp;</td>
                                                <td style="width: 200px" align="left">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    &nbsp;</td>
                                                <td style="width: 150px" align="left">
                                                    OR</td>
                                                <td style="width: 100px" align="left">
                                                    &nbsp;</td>
                                                <td style="width: 200px" align="left">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" >
                                                    Delevery Order Number:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdonum" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btndonum" runat="server" Text="Submit" OnClick="btndonum_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lbl_notfound" runat="server" Text="District" Font-Bold="True" Font-Size="15pt"
                                ForeColor="red" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lbl_Count" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="navy"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Panel ID="panelContainer" runat="server" Height="400px" ScrollBars="Vertical"
                                BorderColor="navy" BorderWidth="1px" Width="100%">
                                <asp:GridView ID="gvDeliveryOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#DEDFDE" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                    Font-Size="10pt" GridLines="Vertical" DataKeyNames="StockDeliveryOrder_Id" Width="100%" 
                                            AllowSorting="True" >
                                    <RowStyle BackColor="#F7F7DE" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Delete" runat="server" Enabled="true" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <ControlStyle Width="15px" />
                                        </asp:TemplateField>
                                     
                                        <asp:BoundField DataField="StockDeliveryOrder_Id" HeaderText="Delivery Order No.">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Depositor/Issuer_Name" HeaderText="Depositor Name">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Delivery_Order_Date" HeaderText="DO_Date">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Qty" HeaderText="No. of Bags">
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Wet" HeaderText="Quantity">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GatePassNO" HeaderText="GatePass No.">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GPDate" HeaderText="GP_Date">
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
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btn_Delete" runat="server" Text="Delete Record" Width="100px" OnClick="btn_Delete_Click"
                                CssClass="BTNBLUE" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" OnClick="btn_Close_Click"
                                CssClass="BTNBLUE" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>


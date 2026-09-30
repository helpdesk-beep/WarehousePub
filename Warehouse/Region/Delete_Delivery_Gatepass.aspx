<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true"
    CodeFile="Delete_Delivery_Gatepass.aspx.cs" Inherits="IssueCenterLevel_Storage_Delete_Delivery_Gatepass"
    Title="Delete Delivery Gatepass ::" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="6" align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblPendingGatePAssList" runat="server" Font-Bold="True" Font-Size="12pt"
                                                        ForeColor="whitesmoke" Text="Delete Delivery Gate Pass"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <asp:Label ID="lblmsg" runat="server" ForeColor="red" Visible="False" Font-Bold="True"></asp:Label>
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
                                                    <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlDepotList_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lbl_Empty" runat="server" Text="" Font-Bold="true" Font-Size="12pt"
                                ForeColor="red" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                Font-Size="10pt"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" colspan="4">
                            <asp:Panel ID="panelContainer" runat="server" Height="350px" ScrollBars="Vertical"
                                Width="100%" BorderColor="navy" BorderWidth="1px">
                                <asp:GridView ID="gv_gatepass" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CellPadding="2" Font-Names="Verdana" Font-Size="8pt" DataKeyNames="GatePass_No"
                                    BackColor="#FFFBD6">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Delete" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <ControlStyle Width="15px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="GatePass_No" HeaderText="GatePass No.">
                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                            <HeaderStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NO_of_Bage" HeaderText="No. of Bags">
                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                            <HeaderStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Weight" HeaderText="Quantity(In Qtls)">
                                            <ItemStyle Width="200px" HorizontalAlign="Right" />
                                            <HeaderStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Issue_Date" HeaderText="GatePass Issue Date">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#719cb6" Font-Bold="True" ForeColor="White" HorizontalAlign="center"
                                        Height="20px" Font-Size="10pt" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="Btn_Delete" runat="server" Text="Delete Record" Width="100px" OnClick="Btn_Delete_Click"
                                CssClass="BTNBLUE" />
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" OnClick="btn_Close_Click"
                                CssClass="BTNBLUE" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>

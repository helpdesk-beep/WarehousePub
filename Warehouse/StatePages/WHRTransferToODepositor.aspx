<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="WHRTransferToODepositor.aspx.cs" Inherits="StatePages_WHRTransferToODepositor" Title="WHR Transfer To Other Depositor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                                    <span style="color: White; font-size: 12pt; font-weight: bold">Transfer WHR from one Depositor to Another(NAFED to MARKFED)</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="4">
                                                    <span style="color: Navy; font-size: 10pt; font-weight: bold">Note :- WHR Will be
                                                        Transfer on the basis of WHR No. This will Transfer all record that belongs to selected
                                                        WHR No.</span>
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
                                            <tr>
                                                <td style="height: 10px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="Label2" runat="server" Text="Godown No./Name" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:DropDownList ID="ddlGodown" runat="server" AutoPostBack="True" Height="25px"
                                                        Width="400px" CssClass="tb6" OnSelectedIndexChanged="ddlGodown_SelectedIndexChanged">
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
                        <td style="height: 5px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Text="Total Record "
                                Font-Size="10pt" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lbl_notfound" runat="server" Text="District" Font-Bold="True" Font-Size="15pt"
                                ForeColor="red" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" valign="top">
                            <asp:Panel ID="panelContainer" runat="server" Height="380px" ScrollBars="Vertical"
                                Width="100%" BorderColor="navy" BorderWidth="1px">
                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="10pt"
                                    DataKeyNames="Depositor_WHR_Id">
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
                                        <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="WHR No.">
                                            <ItemStyle Width="120px" HorizontalAlign="center" />
                                            <HeaderStyle Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="whrdate" HeaderText="WHR Date">
                                            <ItemStyle Width="100px" HorizontalAlign="center" />
                                            <HeaderStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor">
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name">
                                            <ItemStyle Width="200px" HorizontalAlign="center" />
                                            <HeaderStyle Width="100px" HorizontalAlign="center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Bags" HeaderText="No. of Bags">
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Qty" HeaderText="Quantity (in Qtls.)">
                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Godown_Name" HeaderText="Godown Name">
                                            <ItemStyle Width="400px" HorizontalAlign="center" VerticalAlign="Bottom" />
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
                        <td style="height: 15px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="Btn_Delete" runat="server" Text="Transfer" Width="100px" OnClick="Btn_Delete_Click"
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


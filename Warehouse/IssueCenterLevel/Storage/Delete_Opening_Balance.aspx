<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master"
    AutoEventWireup="true" CodeFile="Delete_Opening_Balance.aspx.cs" Inherits="IssueCenterLevel_Storage_Delete_Opening_Balance"
    Title="Delete Opening Balance ::" %>

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
                                                    <span style="color: White; font-size: 12pt; font-weight: bold">Delete Opening Balance</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="center">
                                                    <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label></td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="155px" 
                                                        AutoPostBack="True" onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                                <td style="width: 100px" align="center">
                                                    <asp:Label ID="lblBranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true"></asp:Label></td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px" 
                                                        AutoPostBack="True" 
                                                        onselectedindexchanged="ddlDepotList_SelectedIndexChanged">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Sorted By Datewise" Font-Size="10pt"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDateWise" runat="server" AutoPostBack="True" Width="155px"
                                                        OnSelectedIndexChanged="ddlDateWise_SelectedIndexChanged" Height="25px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center" colspan="2" valign="baseline">
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
                        <td align="right" colspan="4">
                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text="Total Record "
                                Font-Size="10pt"></asp:Label>
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
                        <td colspan="4">
                            <asp:Panel ID="panelContainer" runat="server" Height="350px" ScrollBars="Vertical"
                                Width="100%" BorderColor="navy" BorderWidth="1px">
                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2"
                                    Font-Names="Verdana" Font-Size="8pt" DataKeyNames="WHRID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Delete" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                Width="80px" />
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <ControlStyle Width="15px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CreatDate" HeaderText=" Date">
                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                            <HeaderStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CommdName" HeaderText="Commodity">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CatName" HeaderText="Category">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            <HeaderStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WHRNo" HeaderText="WHRNo">
                                            <ItemStyle Width="200px" ForeColor="#0000C0" HorizontalAlign="Right" VerticalAlign="Bottom" />
                                            <HeaderStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Bags" HeaderText="Bags">
                                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Qty" HeaderText="Qty">
                                            <ItemStyle HorizontalAlign="Right" Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GodownName" HeaderText="Godwon">
                                            <ItemStyle Width="300px" HorizontalAlign="Right" VerticalAlign="Bottom" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="StackName" HeaderText="Stack">
                                            <ItemStyle Width="50px" HorizontalAlign="Right" />
                                            <HeaderStyle Width="50px" />
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
                        <td style="height: 10px" colspan="4">
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
                    <tr>
                        <td align="center" colspan="4">
                           <asp:HiddenField ID="hdn_whr_id" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>

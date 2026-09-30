<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Search_do.aspx.cs" 
Inherits="Region_Search_do" Title="Search DO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <fieldset style="width: 1000px; border: 2px solid navy;" >
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
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
                                                            <span style="color: White; font-size: 12pt; font-weight: bold">Delete Receipt Details</span>
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
                                                            <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px" CssClass="tb6" >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px" align="left">
                                                            <asp:Label ID="Label1" runat="server" Text="DO Number" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td style="width: 150px" align="left">
                                                            <asp:TextBox ID="txtdonumber" runat="server" Width="250px" ></asp:TextBox>
                                                        </td>
                                                        <td style="width: 100px" align="left" colspan="2">
                                                            <asp:Button ID="btnsearch" runat="server" Text="Search" Width="100px" 
                                                                CssClass="BTNBLUE" onclick="btnsearch_Click" />
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
                                        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" GridLines="both"
                                            DataKeyNames="Trans_ID" Width="100%" Font-Size="9pt" CellPadding="4">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_Delete" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                        Width="50px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                                    <ControlStyle Width="15px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Release_Order_No" HeaderText="Delivery Order">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Release_Order_Date" HeaderText="DO Date">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RO_Quantity" HeaderText="DO Quantity">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="FPS" HeaderText="FPS Name">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Truckno" HeaderText="Truck No.">
                                                    <ItemStyle HorizontalAlign="Right" Width="100px"/>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GatePass_No" HeaderText="GatePass No">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="allotment_month" HeaderText="Month">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                  <asp:BoundField DataField="allotment_year" HeaderText="Year">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Transparent" />
                                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
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
                                    <asp:Button ID="Btn_Delete" runat="server" Text="Delete Record" Width="100px" OnClick="Btn_Delete_Click"
                                        CssClass="BTNBLUE" />
                                    <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px" OnClick="btn_Close_Click"
                                        CssClass="BTNBLUE" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </fieldset>
</asp:Content>


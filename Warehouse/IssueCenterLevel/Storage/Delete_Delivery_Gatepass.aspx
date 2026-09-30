<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master"
    AutoEventWireup="true" CodeFile="Delete_Delivery_Gatepass.aspx.cs" Inherits="IssueCenterLevel_Storage_Delete_Delivery_Gatepass"
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
                                                    <asp:Label ID="lblmsg" runat="server" ForeColor="red" Visible="False" Font-Bold="True"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 200px" align="left">
                                                    <asp:Label ID="lblDepositorType" runat="server" Text="Depositor Type" Font-Bold="true"
                                                        Font-Size="10pt" ForeColor="navy"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddldepositortype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldepositortype_SelectedIndexChanged"
                                                        TabIndex="1" Width="155px" Height="25px">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CVDepositorType" runat="server" ControlToValidate="ddldepositortype"
                                                        ErrorMessage="DepositorType" OnServerValidate="CVDepositorType_ServerValidate"
                                                        ValidationGroup="SearchValid"></asp:CustomValidator>
                                                </td>
                                                <td style="width: 150px" align="left">
                                                    <asp:Label ID="lblDepositorName" runat="server" Text="Depositor Name" Font-Bold="true"
                                                        Font-Size="10pt" ForeColor="navy"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlDepositor" runat="server" AutoPostBack="True" Height="25px"
                                                        Width="155px" TabIndex="2" 
                                                        onselectedindexchanged="ddlDepositor_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CVDepositor" runat="server" ControlToValidate="ddlDepositor"
                                                        ErrorMessage="Depositor" OnServerValidate="CVDepositor_ServerValidate" ValidationGroup="SearchValid"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 5px" colspan="4">
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
                            <asp:Panel ID="panelContainer" runat="server" Height="200px" ScrollBars="Vertical"
                                Width="600px" BorderColor="navy" BorderWidth="1px">
                                <asp:GridView ID="gv_gatepass" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CellPadding="2" Font-Names="Verdana" Font-Size="8pt" DataKeyNames="GatePass_No"
                                    OnRowCommand="gv_gatepass_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_Select" runat="server" CommandName="Deletes" ForeColor="red">Delete</asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            <ControlStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="gatepass_no" HeaderText="Gate Pass ID">
                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                            <HeaderStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GP_No" HeaderText="Gate Pass Number">
                                            <ItemStyle Width="200px" />
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
                    <tr id="plnCancel" runat="server" visible="false">
                        <td colspan="4" align="center" valign="top">
                            <fieldset style="width: 980px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="8" align="center">
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="whitesmoke"
                                                        Text="Details of Selected Pending Delivery Gate Pass"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="8" style="height: 5px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="8">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#C00000" Text="Note :- To cancel this Gate Pass please give your reasons for cancelation and submit"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="8" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px">
                                                    <asp:Label ID="lblGPNo" runat="server" Text="Gate Pass No."></asp:Label></td>
                                                <td style="width: 120px">
                                                    <asp:TextBox ID="txtGPNo" runat="server" Enabled="False" BackColor="#FFFFC0" Width="100px"></asp:TextBox>
                                                </td>
                                                <td style="width: 150px;">
                                                    <asp:Label ID="lblBags" runat="server" Text="NO of Bags"></asp:Label>
                                                </td>
                                                <td style="width: 120px">
                                                    <asp:TextBox ID="txtBags" runat="server" Enabled="False" Width="100px" BackColor="#FFFFC0"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblwt" runat="server" Text="Weight"></asp:Label>
                                                </td>
                                                <td style="width: 120px">
                                                    <asp:TextBox ID="txtWt" runat="server" Enabled="False" Width="100px" BackColor="#FFFFC0"></asp:TextBox>
                                                </td>
                                                <td style="width: 120px">
                                                    <asp:Label ID="lblArrTime" runat="server" Text="Arrival Time"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtArrTime" runat="server" Enabled="False" Width="85px" BackColor="#FFFFC0"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="8" style="height: 5px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblVehicleNO" runat="server" Text="Vehicle No."></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtVehicleNO" runat="server" Enabled="False" Width="100px" BackColor="#FFFFC0"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblDriver" runat="server" Text="Driver Name"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDriver" runat="server" Enabled="False" BackColor="#FFFFC0"></asp:TextBox>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:Label ID="lblDepositor" runat="server" Text="Depositor Name"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtDepositor" runat="server" Enabled="False" BackColor="#FFFFC0"
                                                        Width="100px"></asp:TextBox>
                                                </td>
                                              
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="8" style="height: 5px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top" style="width: 250px">
                                                    <asp:Label ID="lblAvailPapers" runat="server" Text="Available Papers of Gatepass in Truck :"></asp:Label>
                                                </td>
                                                <td align="left" valign="top" colspan="2">
                                                    <asp:TextBox ID="txtAvailPapers" runat="server" Enabled="False" TextMode="MultiLine"
                                                        Height="50px" BackColor="#FFFFC0"></asp:TextBox>
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:Label ID="lblReasonforCancelation" runat="server" Text="Reason for Deleting"></asp:Label></td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtReasonforCancelation" runat="server" TextMode="MultiLine" Width="300px"
                                                        Height="50px" TabIndex="7"></asp:TextBox>
                                                </td>
                                                <td align="left" valign="top">
                                                    <span style="color: #990000">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                                        runat="server" ErrorMessage="Reasons for cancelation  is required" ControlToValidate="txtReasonforCancelation"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="8" style="height: 10px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="8" style="height: 5px">
                                                    <asp:Button ID="Button1" runat="server" Text="Delete" Font-Size="10pt" OnClick="Button1_Click"
                                                        TabIndex="8" Width="100px" CssClass="BTNBLUE" />
                                                    <asp:Button ID="btn_Close" runat="server" Text="Close" Font-Size="10pt" TabIndex="9"
                                                        Width="100px" OnClick="btn_Close_Click" CssClass="BTNBLUE" CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </fieldset>
</asp:Content>

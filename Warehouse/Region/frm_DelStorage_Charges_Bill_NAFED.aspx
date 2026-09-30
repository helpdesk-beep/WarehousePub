<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="frm_DelStorage_Charges_Bill_NAFED.aspx.cs" Inherits="Region_frm_DelStorage_Charges_Bill_NAFED" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td colspan="6" align="center" valign="top">
                            <fieldset style="width: 1100px; border: 1px solid navy;">
                                <center>
                                    <div>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr style="background-color: #0bb6e6; height: 25px">
                                                <td colspan="6" align="center">
                                                    <span style="color: White; font-size: 12pt; font-weight: bold">Delete NAFED Bill Details</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6"></td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="6">
                                                    <span style="color: Navy; font-size: 10pt; font-weight: bold">Note :- Record Will be
                                                                Deleted on the basis of Bill Number.This will remove all records that belongs to selected
                                                                Bill Number of NAFED.</span>
                                                     <span style="color: Navy; font-size: 10pt; font-weight: bold">
                                                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 1- Storage बिल डिलीट करने से पहले यहाँ जाँच कर लेवे, जिस बिलको डिलीट कर रहे हे उसको HO NAFED को सबमिट तो नहीं किया हैं |</span>
                                                     <span style="color: Navy; font-size: 10pt; font-weight: bold">
                                                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2- Storage बिल डिलीट करने के लिए यहाँ दिख नहीं रहा हैं तो वह बिल HO NAFED को सबमिट किया जा चूका हैं|</span>
                                                    <br />
                                                    <span style="color: red; font-size: 15pt; font-weight: bold">
                                                        1. स्टोरेज बिल डिलीट के लिए बिल टाइप में FD सेलेक्ट करे !!<br />
                                                       
                                                        </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="6">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6" align="center">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 150px" align="left">
                                                    <asp:Label ID="lblDistrict" runat="server" Text="District" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 150px" align="left">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        CssClass="tb6" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 100px" align="left">
                                                    <asp:Label ID="lblBranch" runat="server" Text="Branch" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px" AutoPostBack="false"
                                                        CssClass="tb6">
                                                    </asp:DropDownList>
                                                </td>
                                                 <td style="width: 100px" align="left">
                                                    <asp:Label ID="Label1" runat="server" Text="Bill Type" Font-Size="10pt" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 200px" align="left">
                                                    <asp:DropDownList ID="ddlbillType" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        CssClass="tb6"
                                                        OnSelectedIndexChanged="ddlbillType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>

                                        </table>
                                    </div>
                                </center>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="6"></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Text="Total Record "
                                Font-Size="10pt" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px" colspan="4"></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lbl_notfound" runat="server" Text="" Font-Bold="True" Font-Size="15pt"
                                ForeColor="red" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center" valign="top">
                            <asp:Panel ID="panelContainer" runat="server" Height="380px" ScrollBars="Vertical"
                                Width="100%" BorderColor="navy" BorderWidth="1px">
                                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="10pt"
                                    DataKeyNames="Bill_Number">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Delete" runat="server" />
                                                <asp:HiddenField ID="hdnFinBill_No" runat="server" Value='<%# Eval("Fin_Bill_number") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="12pt" HorizontalAlign="Center"
                                                Width="40px" />
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                            <ControlStyle Width="15px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Bill_Number" HeaderText="Bill No.">
                                            <ItemStyle Width="100px" HorizontalAlign="center" />
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Bill_Type" HeaderText="Bill Type">
                                            <ItemStyle Width="50px" HorizontalAlign="center" />
                                            <HeaderStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Bill_Name" HeaderText="Bill Name">
                                            <ItemStyle Width="200px" HorizontalAlign="center" />
                                            <HeaderStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name">
                                            <ItemStyle Width="150px" HorizontalAlign="center" />
                                            <HeaderStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DateOfBill" HeaderText="Date Of Bill">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
                                        </asp:BoundField>
                                        <%--   <asp:BoundField DataField="To_Date" HeaderText="To Date">
                                                    <ItemStyle Width="200px" HorizontalAlign="center"/>
                                                      <HeaderStyle Width="100px" HorizontalAlign="center"/>
                                                </asp:BoundField>--%>

                                        <asp:BoundField DataField="Net_Amount" HeaderText="Net Amount">
                                            <ItemStyle HorizontalAlign="center" Width="50px" />
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
                        <td style="height: 15px" colspan="4"></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="Btn_Delete" runat="server" Text="Delete Record" Width="100px"
                                CssClass="BTNBLUE" OnClick="Btn_Delete_Click" />
                            <asp:Button ID="btn_Close" runat="server" Text="Close" Width="100px"
                                CssClass="BTNBLUE" OnClick="btn_Close_Click" />
                        </td>
                    </tr>
                </table>
            </div>

        </center>
    </fieldset>
</asp:Content>

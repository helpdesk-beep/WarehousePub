<%@ Page Title="Deleted Bill Details" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="frm_Deleted_Final_Bill.aspx.cs" Inherits="Accounting_frm_Deleted_Final_Bill" %>

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
                                                <td colspan="4" align="center">
                                                    <span style="color: White; font-size: 12pt; font-weight: bold">Deleted Bill Details</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4"></td>
                                            </tr>
                                            <%--<tr>
                                                <td align="left" colspan="4">
                                                    <span style="color: Navy; font-size: 10pt; font-weight: bold">Note :- Record Will be
                                                                Deleted on the basis of Bill No.This will remove all records that belongs to selected
                                                                Bill No.</span>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td align="left" colspan="4">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="4" align="center">&nbsp;</td>
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
                                                    <asp:DropDownList ID="ddlDepotList" runat="server" Height="25px" Width="155px" AutoPostBack="True"
                                                        CssClass="tb6"
                                                        OnSelectedIndexChanged="ddlDepotList_SelectedIndexChanged">
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
                        <td style="height: 5px" colspan="4"></td>
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
                                        <asp:BoundField DataField="Bill_Number" HeaderText="Bill No.">
                                            <ItemStyle Width="100px" HorizontalAlign="center" />
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Depositor_Name" HeaderText="Depositor Name">
                                            <ItemStyle Width="150px" HorizontalAlign="center" />
                                            <HeaderStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Net_Amount" HeaderText="Net Amount">
                                            <ItemStyle HorizontalAlign="center" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DeletedDate" HeaderText="Date Of Deleted Bill">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" />
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
                </table>
            </div>

        </center>
    </fieldset>
</asp:Content>


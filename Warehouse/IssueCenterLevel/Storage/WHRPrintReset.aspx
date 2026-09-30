<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="WHRPrintReset.aspx.cs" Inherits="BranchPages_WHRPrintReset" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
           
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="4" align="center">
                                    <asp:Label ID="lblhead" runat="server" Text="Print Reset Request Form" Font-Size="12pt"
                                        ForeColor="WhiteSmoke" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 10px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblopname" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                        Text="Operator Name"></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <asp:TextBox ID="txtopname" runat="server" MaxLength="50" Width="300px" Height="20px"
                                        CssClass="tb6"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                        ValidationGroup="SaveValid" ErrorMessage="Please Enter Operator Name" SetFocusOnError="True"
                                        ControlToValidate="txtopname">*</asp:RequiredFieldValidator>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtopname"
                                        ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblopmobile" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                        Text="Operator Mobile No."></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <asp:TextBox ID="txtopmobile" runat="server" MaxLength="12" Width="200px" Height="20px"
                                        CssClass="tb6"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                        ValidationGroup="SaveValid" ErrorMessage="Please Enter Operator Mobile Number"
                                        SetFocusOnError="True" ControlToValidate="txtopmobile">*</asp:RequiredFieldValidator>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtopmobile"
                                        ValidChars="0123456789" >
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblbmname" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                        Text="Branch Manager"></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <asp:TextBox ID="txtbmname" runat="server" MaxLength="50" Width="300px" Height="20px"
                                        CssClass="tb6"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtbmname"
                                        ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWSYZ.">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblbmmob" runat="server" Font-Size="8pt" ForeColor="navy" Font-Bold="true"
                                        Text="B.M Mobile No."></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <asp:TextBox ID="txtbmmob" runat="server" MaxLength="12" Width="200px" Height="20px"
                                        CssClass="tb6"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtbmmob"
                                        ValidChars="0123456789">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 150px">
                                    &nbsp;</td>
                                <td align="left" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr id="trgdnlist" runat="server" >
                                <td align="left">
                                    <asp:Label ID="lblCommodity" runat="server" Text="Commodity" ForeColor="navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td align="left" valign="middle">
                                    <asp:DropDownList ID="ddlcommodity" runat="server" Width="305px" Height="25px" TabIndex="4"
                                        CssClass="tb6" onselectedindexchanged="ddlcommodity_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblGodown" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                        Text="Godown Name"></asp:Label>
                                </td>
                                <td align="left" valign="middle">
                                    <asp:DropDownList ID="ddl_godown" runat="server" Width="205px" AutoPostBack="True"
                                        CssClass="tb6" Height="25px" OnSelectedIndexChanged="ddl_godown_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <asp:Label ID="lblRowCount" runat="server" ForeColor="navy" Font-Bold="true" Text=""
                                        Font-Size="10pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    <asp:GridView ID="gv_whr" runat="server" AutoGenerateColumns="False" Width="100%"
                                        Font-Size="10pt" DataKeyNames="Depositor_WHR_Id">
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
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="height: 15px">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <asp:Button ID="btnsave" runat="server" Text="Submit" Width="120px" CssClass="BTNBLUE"
                                        ValidationGroup="SaveValid" OnClick="btnsave_Click" OnClientClick="this.disabled = true; this.value='Please wait...'" UseSubmitBehavior="false" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="120px" CssClass="BTNBLUE"
                                        CausesValidation="false" Visible="false" onclick="btnPrint_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="SaveValid" />
                                   
                                </td>
                            </tr>
                           
                        </table>
                    </div>
               
        </center>
    </fieldset>

</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master"
    AutoEventWireup="true" CodeFile="TaxMaster.aspx.cs" Inherits="Masters_TaxMaster"
    Title="Tax Master::" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="background-color: #0bb6e6; height: 25px">
                                <td colspan="2" align="center">
                                    <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="WhiteSmoke"
                                        Text="Tax Master"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="lblmsg" runat="server" Font-Italic="True" ForeColor="Maroon" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label6" runat="server" Text="Service Tax" ForeColor="Navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtsertax" runat="server" Width="150px" Height="20px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsertax"
                                        ErrorMessage=" Service Tax Percent Required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="10px" Text="in(%age)"
                                        Width="21px"></asp:Label>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtsertax"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label10" runat="server" Text="TDS" ForeColor="Navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txttds" runat="server" Width="150px" Height="20px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttds"
                                        ErrorMessage="TDS Percent required" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="10px" Text="in(%age)"></asp:Label>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txttds"
                                        ValidChars="0123456789.">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="Label13" runat="server" Text="Remarks" ForeColor="Navy" Font-Bold="true"
                                        Font-Size="8pt"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnnew" runat="server" OnClick="btnnew_Click" Text="New" Width="100px" />
                                    <asp:Button ID="btnsave" runat="server" Text="Save" Width="100px" OnClick="btnsave_Click"
                                        ValidationGroup="1" />
                                    <asp:Button ID="btnclose" runat="server" OnClick="btnclose_Click" Text="Close" Width="100px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="1" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </fieldset>
</asp:Content>

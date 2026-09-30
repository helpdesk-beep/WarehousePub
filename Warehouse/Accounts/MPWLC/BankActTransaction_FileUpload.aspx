<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Account_MPWLC_Master.master" AutoEventWireup="true" CodeFile="BankActTransaction_FileUpload.aspx.cs" Inherits="Accounting_BankActTransaction_FileUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy">
        <center>
            <div>
                <table width="1000px">
                    <tr id="msg">
                        <td colspan="6">
                            <asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #0bb6e6; height: 25px">
                        <td colspan="6" align="center">
                            <asp:Label ID="lblheading" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                Text="Bank Transaction File Upload"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="8pt" ForeColor="Navy"
                                Text="File Upload:"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:FileUpload ID="upFile" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="upFile" ValidationGroup="A" ErrorMessage="* required" ForeColor="Red" runat="server"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
            <table width="100%">
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" Visible="true" CssClass="BTNBLUE" OnClick="btnUpload_Click"
                            ValidationGroup="A" />
                    </td>
                </tr>
            </table>
        </center>
        </fieldset>
</asp:Content>

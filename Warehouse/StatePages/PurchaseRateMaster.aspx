<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="PurchaseRateMaster.aspx.cs" Inherits="StatePages_PurchaseRateMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center">
        <tr>
            <td>
                Commodity:

            </td>
<td>
    <asp:dropdownlist runat="server" ID="ddlcomm"></asp:dropdownlist>
</td>
            
        </tr>

        <tr>
                <td>
                    Crop Year:
                </td>
            <td>
                <asp:dropdownlist runat="server" ID="ddlcropyr"></asp:dropdownlist>
            </td>
            </tr>
<tr>
    <td>
        Rate(In Qntl.):
    </td>
    <td>
        <asp:textbox runat="server" ID="txtrate"></asp:textbox>
    </td>
</tr>
        <tr>
            <td>

            </td>
            <td>
                <asp:button runat="server" text="Submit"  ID="btnsubmit" OnClick="btnsubmit_Click" />
            </td>
        </tr>
    </table>

    <center>
        <asp:gridview runat="server" ID="gvratelist"></asp:gridview>
    </center>
</asp:Content>


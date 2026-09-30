<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_PrintBills.aspx.cs" Inherits="Accounting_frm_PrintBills" Title="Print Bills" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
        <div>
<table width="100%">
<tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center">
            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Re-Print Bills"></asp:Label></td>
    </tr>
    <tr>
<td colspan="2" align="right">Select Bill Unit(In Bag/M.T.).
</td>
<td colspan="2" align="left">
<asp:DropDownList Enabled="true" ID="ddlBType" runat="server" AutoPostBack="false" Visible="true" 
                 TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
                 >
                <asp:ListItem Value="-1">--Select--</asp:ListItem>
                <asp:ListItem Value="B">In Bag</asp:ListItem>
                <asp:ListItem Value="W">In M.T. </asp:ListItem>
            </asp:DropDownList>
</td>
</tr>
<tr>
<td>Select Bill No.
</td>
<td>
<asp:DropDownList runat="server" ID="ddlBillNo" Width="150px" AutoPostBack="true"
        onselectedindexchanged="ddlBillNo_SelectedIndexChanged"></asp:DropDownList>
</td>
<td style="color:Green; font-size:large">OR</td>
<td align="center">Enter Bill No.
<asp:TextBox ID="txtBillNo" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="BTNBLUE" onclick="btnSubmit_Click" 
       />
</td>
</tr>
    <tr id="trReportsView" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
<center>
<div style="overflow: scroll; height: 500px; overflow-x: hidden">

 
    <rsweb:ReportViewer ID="rvPrintBill" runat="server" Width="100%">
    </rsweb:ReportViewer>
   
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


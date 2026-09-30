<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_CashReceipt.aspx.cs" Inherits="Accounting_frm_CashReceipt" Title="Cash Receipt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
        <div>
<table width="100%">
<tr>
<td>Enter Bill No
</td>
<td>
<asp:TextBox ID="txtBillNo" runat="server"></asp:TextBox>
</td>
<td colspan="2" align="center">
<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="BTNBLUE" onclick="btnSubmit_Click" 
       />
</td>
</tr>
<tr>

</tr>
    <tr id="trReportsView" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
<center>
<div style="overflow: scroll; height: 500px; overflow-x: hidden">

    <rsweb:ReportViewer ID="RVCR" runat="server" Width="100%">
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


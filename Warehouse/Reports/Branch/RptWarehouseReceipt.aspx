<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptWarehouseReceipt.aspx.cs"
    Inherits="Reports_Branch_RptWarehouseReceipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Warehouse Receipt</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <center>
        <div style="width: 1000px">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td align="left">
                        <asp:Label ID="lbldate" runat="server" Text="Date From" ForeColor="Navy" Font-Bold="true"
                            Font-Size="10pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtdate" Width="150px" runat="server" ReadOnly="true"></asp:TextBox>
                        <asp:ImageButton ID="Imgpop" runat="server" ImageUrl="~/images/cal.gif" CausesValidation="false" />
                        <asp:CalendarExtender ID="tctrcdate_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="txtdate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="Imgpop">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbldateto" runat="server" Text="Date To" ForeColor="Navy" Font-Bold="true"
                            Font-Size="10pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtdateto" Width="150px" runat="server" ReadOnly="true"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cal.gif" CausesValidation="false" />
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtdateto"
                            Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="ImageButton1">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnview" runat="server" Text="Get WHR" Width="150px" OnClick="btnview_Click" />
                    </td>
                    <td align="left">
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/IssueCenterLevel/Storage/Reports.aspx"
                            ForeColor="indianred" Font-Bold="true">पिछले पृष्ठ पर जाये</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblwhr" runat="server" Text="Select WHR - " ForeColor="Navy" Font-Bold="true"
                            Font-Size="10pt"></asp:Label>
                    </td>
                    <td align="left" colspan="5">
                        <asp:DropDownList ID="DDLwhr" runat="server" Height="25px" Width="250px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <rsweb:ReportViewer ID="ReportViewer_Depot" runat="server" Width="100%" ProcessingMode="Remote"
                            Height="600px">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>

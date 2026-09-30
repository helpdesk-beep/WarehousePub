<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Depositorformreceipt.aspx.cs"
    Inherits="Reports_Branch_Rpt_Depositorformreceipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DEPOSITOR FORM RECEIPT(DATEWISE)</title>
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
                        <asp:Label ID="lbldate" runat="server" Text="Select Date" ForeColor="Navy" Font-Bold="true"
                            Font-Size="10pt"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtdate" Width="150px" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="Imgpop" runat="server" ImageUrl="~/images/cal.gif" CausesValidation="false" />
                        <asp:CalendarExtender ID="tctrcdate_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="txtdate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="Imgpop">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left">
                        <asp:Button ID="btnview" runat="server" Text="View Report" Width="150px" onclick="btnview_Click" 
                            />
                    </td>
                    <td align="left">
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/IssueCenterLevel/Storage/Reports.aspx"
                            ForeColor="indianred" Font-Bold="true">पिछले पृष्ठ पर जाये</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
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

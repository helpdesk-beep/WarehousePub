<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RPT_DepositorWiseSummry.aspx.cs" Inherits="Reports_States_RPT_DepositorWiseSummry" %>

<!DOCTYPE html>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Depositor wise Utilization</title>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <center>
        <div style="width: 1010px">
            <Center><h3>Depositor wise summary</h3></Center>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td align="right" colspan="4">
                        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/IssueCenterLevel/Storage/Report_Region.aspx"
                            ForeColor="indianred" Font-Bold="true">पिछले पृष्ठ पर जाये</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date from:
                    </td>
                    <td>
                        <asp:TextBox ID="txtdatefrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtdatefrom_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtdatefrom" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                    <td>To:</td>
                    <td>
                        <asp:TextBox ID="txtdateto" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtdateto_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtdateto" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server"  Text="Submit" OnClick="Button1_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <rsweb:ReportViewer ID="ReportViewer_Region" runat="server" SizeToReportContent="True"
                            Width="100%" ProcessingMode="Remote" ZoomMode="PageWidth" Height="600px">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>

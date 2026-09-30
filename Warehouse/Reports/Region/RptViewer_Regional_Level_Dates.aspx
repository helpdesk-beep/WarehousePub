<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptViewer_Regional_Level_Dates.aspx.cs" Inherits="Reports_Region_RptViewer_Regional_Level_Dates" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Report Between 2 Dates</title>
    <style type="text/css">
        .style1
        {
            width: 124px;
            height: 23px;
        }
        .style2
        {
            width: 149px;
            height: 23px;
        }
        .style3
        {
            width: 93px;
            height: 23px;
        }
        .style4
        {
            height: 23px;
        }
        .style5
        {
            width: 417px;
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
      <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
    <div>
    <center><h2>Regional Reports Between Two Dates</h2></center>
    <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 100%; background-color: #ece9d8;" id="">
            <tr>
                <td align="left" style="vertical-align: middle; " class="style1">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt"
                        Text="From Date"></asp:Label></td>
                <td align="left" class="style2">
                <asp:TextBox ID="fromDate" runat="server" Width="111px"></asp:TextBox>
                  <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="fromDate"></cc1:CalendarExtender>
                
                </td>
                <td align="right" style="vertical-align: middle; " class="style3">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt"
                        Text="To Date"></asp:Label></td>
                <td align="left" class="style4">
                 <asp:TextBox ID="toDate" runat="server" Width="116px"></asp:TextBox>
                 <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="toDate"></cc1:CalendarExtender>
                </td>
                <td align="left" class="style5">
                    &nbsp;<asp:Button ID="btnViewReport" runat="server" Text="View Report" 
                        Width="147px" onclick="btnViewReport_Click"/></td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
        </table>
            <a href="Reports.aspx">Go Back</a>
     <rsweb:reportviewer id="ReportViewer_DepoDate" runat="server" SizeToReportContent="True" Width="100%"  ProcessingMode="Remote"
    ZoomMode="PageWidth" Height="600px"></rsweb:reportviewer>
        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label><asp:Label
            ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label><br />
    </div>
    </form>
</body>
</html>

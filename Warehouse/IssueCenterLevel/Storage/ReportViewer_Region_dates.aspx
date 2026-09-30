<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer_Region_dates.aspx.cs" Inherits="IssueCenterLevel_Storage_ReportViewer_Region_dates" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script  type="text/javascript" src="../../JS/popcalendar.js"></script>
 <script  type="text/javascript" src="../../JS/ShowCalender.js"></script>
 <script  type="text/javascript" src="../../JS/ValdationsClientSide.js"></script>
    <title>Report between Tw Dates</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 100%; background-color: #ece9d8;" id="">
           <%-- <tr>
                <td align="center" colspan="5" style="font-weight: bolder; font-size: 15pt; color: teal">
                    WareHouse Capacity and Utilization between dates</td>
            </tr>--%>
            <tr>
                <td align="left" style="vertical-align: middle; width: 124px; height: 23px">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt"
                        Text="From Date"></asp:Label></td>
                <td style="width: 149px;" align="left">
                <asp:TextBox ID="fromDate" runat="server" Width="111px"></asp:TextBox>
                    <a onclick="ShowCalendar(form1.fromDate,form1.fromDate);" href="javascript:;">
                                    <img height="16" id="imgProc" runat="server" alt="Click Here to Pick up the date" src="../../images/cal.gif" width="16"	border="0" /></a>
                
                </td>
                <td align="right" style="vertical-align: middle; width: 93px; height: 23px">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt"
                        Text="To Date"></asp:Label></td>
                <td align="left">
                 <asp:TextBox ID="toDate" runat="server" Width="116px"></asp:TextBox>
                   <a onclick="ShowCalendar(form1.toDate,form1.toDate);" href="javascript:;">
                                    <img height="16" id="img1" runat="server" alt="Click Here to Pick up the date" src="../../images/cal.gif" width="16"	border="0" /></a>
                </td>
                <td align="left" style="width: 417px; height: 23px">
                    &nbsp;<asp:Button ID="btnViewReport" runat="server" Text="View Report" OnClick="btnViewReport_Click" Width="147px" /></td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
        </table>
     <rsweb:reportviewer id="ReportViewer_Region" runat="server" SizeToReportContent="True" Width="100%"  ProcessingMode="Remote"
    ZoomMode="PageWidth" Height="600px"></rsweb:reportviewer>
        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label><asp:Label
            ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label><br />
    </div>
    </form>
</body>
</html>

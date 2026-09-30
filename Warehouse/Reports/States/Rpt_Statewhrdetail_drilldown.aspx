<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Statewhrdetail_drilldown.aspx.cs"
    Inherits="Reports_States_Rpt_Statewhrdetail_drilldown" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reports States</title>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div style="width:900px">
            <rsweb:ReportViewer ID="ReportViewer_Region" runat="server" SizeToReportContent="True"
                Width="100%" ProcessingMode="Remote" ZoomMode="PageWidth" Height="600px">
            </rsweb:ReportViewer>
        </div>
    </center>
    </form>
</body>
</html>

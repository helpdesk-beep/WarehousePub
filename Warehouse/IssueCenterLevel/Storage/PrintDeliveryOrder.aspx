<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintDeliveryOrder.aspx.cs" Inherits="IssueCenterLevel_Storage_PrintDeliveryOrder" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     
        <rsweb:reportviewer id="ReportViewer_Region" runat="server" SizeToReportContent="True" Width="100%"  ProcessingMode="Remote"
    ZoomMode="PageWidth" Height="600px"></rsweb:reportviewer>
     
    </div>
    </form>
</body>
</html>

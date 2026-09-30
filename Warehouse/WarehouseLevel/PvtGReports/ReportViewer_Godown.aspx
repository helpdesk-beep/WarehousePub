<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer_Godown.aspx.cs" Inherits="WarehouseLevel_PvtGReports_ReportViewer_Godown" Title="Pvt Godown Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Reports</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a href="PvtGodownReports.aspx">Go Back</a>
        <rsweb:reportviewer id="ReportViewer_Godown" runat="server" Width="100%"  ProcessingMode="Remote"
          Height="600px"></rsweb:reportviewer>
        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label></div>
        
    </form>
</body>
</html>

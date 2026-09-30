<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer_Region_VacStorage_Morethan_1_Lac_mt.aspx.cs" Inherits="Region_ReportViewer_Region_VacStorage_Morethan_1_Lac_mt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reports</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a href="../Welcome.aspx">Go Back</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        

        &nbsp;<rsweb:reportviewer id="ReportViewer_Region_VacStorage_Morethan_1_Lac_mt" runat="server" Width="100%"  ProcessingMode="Remote"
          Height="600px"></rsweb:reportviewer>
        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="Regionid" runat="server" Text="Label" Visible="False"></asp:Label>
        </div>

        
    </form>
</body>
</html>
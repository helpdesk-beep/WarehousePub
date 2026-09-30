<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer_Branch_New_Reports.aspx.cs" Inherits="Reports_Branch_ReportViewer_Branch_New_Reports" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Reports</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    <a href="Branch_New_Reports.aspx">Go Back</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_gdn" runat="server" Text="Godown :"></asp:Label>
        &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddl_Godown" runat="server" AutoPostBack="true"  Height="16px" OnSelectedIndexChanged="ddl_Godown_SelectedIndexChanged" Width="166px">
        </asp:DropDownList>
        <rsweb:reportviewer id="ReportViewer_Branch_New_Reports" runat="server" Width="100%"  ProcessingMode="Remote" ShowBackButton="true"
          Height="600px"></rsweb:reportviewer>
        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="Godown" runat="server" Text="lable" Visible="false"></asp:Label>
          
            </div>
        
    </form>
</body>

</html>

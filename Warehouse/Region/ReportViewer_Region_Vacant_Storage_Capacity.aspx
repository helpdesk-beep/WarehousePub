<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer_Region_Vacant_Storage_Capacity.aspx.cs" Inherits="Region_ReportViewer_Region_Vacant_Storage_Capacity" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reports</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a href="../Welcome.aspx">Go Back</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_hidtype" runat="server" Text="Hired Type:"></asp:Label>
        <asp:DropDownList ID="ddl_hidtyp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_hidtyp_SelectedIndexChanged">
        </asp:DropDownList>


        &nbsp;&nbsp; <asp:Label ID="lbl_stgtype" runat="server" Text="Stored Type :"></asp:Label>
        <asp:DropDownList ID="ddl_stgtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_stgtype_SelectedIndexChanged">
        </asp:DropDownList>
         &nbsp;&nbsp;&nbsp;&nbsp;


        &nbsp;<asp:Button ID="btn_viewrpt" runat="server" Text="View Report" Width="95px" OnClick="btn_viewrpt_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        

        &nbsp;<rsweb:reportviewer id="ReportViewer_Region_Vacant_Storage_Capacity" runat="server" Width="100%"  ProcessingMode="Remote"
          Height="600px"></rsweb:reportviewer>
        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="Rgnid" runat="server" Text="Label" Visible="False"></asp:Label>
        </div>

        
    </form>
</body>
</html>
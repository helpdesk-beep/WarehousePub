<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ReportViewer_Godown_Vacant_Storage_Cpty.aspx.cs" Inherits="Reports_States_ReportViewer_Godown_Vacant_Storage_Cpty" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reports</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family: 'Book Antiqua';  font-size: 20px; text-align: left; color: #0000FF;">
    <a href="StateReports.aspx">Go Back</a>&nbsp;&nbsp;&nbsp;<br /> </div>
        <div style="background-color: #C0C0C0; font-size: large; text-decoration: underline; text-align: center; color: #CC0099; font-weight: bold">  Vacant Storage Capacity </div>
       <br />
        <br />
       <div style="text-align:center">
           <asp:Label ID="lbl_hidtype" runat="server" Text="Hired Type:"></asp:Label>
         &nbsp;<asp:DropDownList ID="ddl_hidtyp"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_hidtyp_SelectedIndexChanged">
        </asp:DropDownList>


        &nbsp;&nbsp; <asp:Label ID="lbl_stgtype" runat="server" Text="Stored Type :"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddl_stgtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_stgtype_SelectedIndexChanged">
        </asp:DropDownList>
       

        &nbsp;
       

        &nbsp;<asp:Button ID="btn_viewrpt" runat="server" Text="View Report" Width="95px" OnClick="btn_viewrpt_Click" />
       
        
        </div>
        &nbsp;<rsweb:reportviewer id="ReportViewer_Godown_Vacant_Storage_Cpty" runat="server" Width="100%"  ProcessingMode="Remote"
          Height="600px" ShowBackButton="true"></rsweb:reportviewer>
        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label>
        

        
    </form>
</body>
</html>

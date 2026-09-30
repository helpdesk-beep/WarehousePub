<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptReceivingDatailbwDates.aspx.cs" Inherits="Reports_Branch_rptReceivingDatailbwDates" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        
    Date From(Deposit Date): <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>  
        <cc1:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="TextBox1">
        </cc1:CalendarExtender>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  Date To:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <cc1:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" 
            Enabled="True" TargetControlID="TextBox2">
        </cc1:CalendarExtender>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  
        <asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click" />

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="~/IssueCenterLevel/Storage/Reports.aspx">पिछले पेज पर जाएँ</asp:HyperLink>

        <rsweb:reportviewer id="ReportViewer_Depot" runat="server" Width="100%"  ProcessingMode="Remote"
          Height="600px"></rsweb:reportviewer>
    
    </div>
    </form>
</body>
</html>

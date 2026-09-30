<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer_MPSCSC_TillDate.aspx.cs" Inherits="IssueCenterLevel_Storage_ReportViewer_MPSCSC_TillDate" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPSCSC Report Till Date</title>
</head>
<body>
        <form id="form1" runat="server">
      <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
    <div>
    <center><h2 style="background-color:Silver">MPSCSC Stock Reports Till Date</h2></center>
    <table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width: 100%; background-color: #ece9d8;" id="">
            <tr>
                <td align="left" style="vertical-align: middle; " class="style1">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt"
                        Text="Crop Year"></asp:Label></td>
                <td align="left" class="style2">
               <asp:DropDownList ID="ddlcropyear" runat="server" AutoPostBack="false" Width="150px">
                   <asp:ListItem Text="2019-20"></asp:ListItem>
              <asp:ListItem Text="2018-19"></asp:ListItem>
              <asp:ListItem Text="2017-18"></asp:ListItem>
               <asp:ListItem Text="2016-17"></asp:ListItem>
                <asp:ListItem Text="2015-16"></asp:ListItem>
                 <asp:ListItem Text="2014-15"></asp:ListItem>
                  <asp:ListItem Text="2013-14"></asp:ListItem>
                   <asp:ListItem Text="2012-13"></asp:ListItem>
                 
                                                            </asp:DropDownList>
                
                </td>
                <td align="right" style="vertical-align: middle; " class="style3">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="10pt"
                        Text="Till Date"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                <td align="left" class="style4">
                 <asp:TextBox ID="toDate" runat="server" Width="116px"></asp:TextBox>
                 <cc1:CalendarExtender ID="CalendarExtender2" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="toDate"></cc1:CalendarExtender>
                &nbsp; 
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
           <%-- <a href="MPSCSC_Reports.aspx">Go Back</a>--%>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Back</asp:LinkButton>
        <rsweb:ReportViewer ID="ReportViewer_MPSCSCSDate" runat="server" SizeToReportContent="True" Width="100%"  ProcessingMode="Remote"
    ZoomMode="PageWidth" Height="600px">
        </rsweb:ReportViewer>

        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label
            ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label><br />
            </div>
            </form>
</body>
</html>

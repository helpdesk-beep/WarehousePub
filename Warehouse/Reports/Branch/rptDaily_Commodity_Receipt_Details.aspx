<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDaily_Commodity_Receipt_Details.aspx.cs" Inherits="Reports_Branch_rptDaily_Commodity_Receipt_Details" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily issue commodity wise</title>
    <style type="text/css">
        .style1
        {
            width: 187px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <center>
        <div style="width: 1200px">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
             <tr>
                                    <td align="center">
                                        &nbsp;</td>
                                    <td align="left" class="style1">
                                        &nbsp;</td>
                                    <td align="center">
                                        &nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                <tr>
                <td>
                                        <asp:Label ID="Label3" runat="server" Text="Date: -" Font-Bold="true"></asp:Label>
                                    </td>
                <td class="style1">
                    <%-- <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txttodate"
                                            Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </asp:CalendarExtender>--%>
                <asp:TextBox ID="txttodate" runat="server" Width="150px"></asp:TextBox>
                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                        TargetControlID="txttodate">
                    </cc1:CalendarExtender>
                </td>
                <td>
                                        <asp:Button ID="BtnViewReport" runat="server" Text="View Report" onclick="BtnViewReport_Click"  
                                             />
                                    </td>
                    <td align="right">
                        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/IssueCenterLevel/Storage/Reports.aspx"
                            ForeColor="indianred" Font-Bold="true">पिछले पृष्ठ पर जाये</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <%-- <rsweb:ReportViewer ID="ReportViewer_Depot" runat="server" Width="100%" ProcessingMode="Remote"
                            Height="600px">
                        </rsweb:ReportViewer>--%>

                        

                        <rsweb:ReportViewer ID="ReportViewer_Depot" runat="server" Width="100%" ProcessingMode="Remote"
                            Height="600px">
                        </rsweb:ReportViewer>

                        

                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label>
                        <asp:Label ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label>

                        

                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>

                        

                    </td>
                </tr>
            </table>
        </div>
    </center>
    </div>
    </form>
</body>
</html>

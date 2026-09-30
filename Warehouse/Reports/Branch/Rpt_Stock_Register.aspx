<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Stock_Register.aspx.cs" Inherits="Reports_Branch_Rpt_Stock_Register" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset style="width: 1050px; border: 2px solid navy;">
    <center>
      
        <div style="width: 1200px">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
             <tr>
                                    <td align="center">
                                        <asp:Label ID="Label2" runat="server" Text="Select Depositor Name -" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddldepositor" Height="25px" Width="300px" runat="server" 
                                            AutoPostBack="true" onselectedindexchanged="ddldepositor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label3" runat="server" Text="Select WHR No. -" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:DropDownList ID="ddlwhr" Height="25px" Width="300px" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                <tr>
                <td>
                 <asp:Label ID="Label1" runat="server" Text="To Date" Font-Bold="true"></asp:Label>
                </td>
                <td>
                <asp:TextBox ID="txttodate" runat="server" Width="150px"></asp:TextBox>
                                       <cc1:CalendarExtender ID="txttodate_CalendarExtender" 
                        runat="server" TargetControlID="txttodate">
                    </cc1:CalendarExtender>
                                       <%-- <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txttodate"
                                            Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </asp:CalendarExtender>--%>
                </td>
                <td>
                                        <asp:Button ID="BtnViewReport" runat="server" Text="View Report" 
                                            onclick="BtnViewReport_Click" />
                                    </td>
                    <td colspan="4" align="right">
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
            </table>
        </div>
    </center>
    </fieldset>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

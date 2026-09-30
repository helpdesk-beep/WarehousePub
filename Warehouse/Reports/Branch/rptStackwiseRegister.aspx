<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptStackwiseRegister.aspx.cs"
    Inherits="Reports_Branch_rptStackwiseRegister" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stackwise Register::</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <center>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <fieldset style="width: 1050px; border: 2px solid navy;">
                    <center>
                        <div>
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="Label2" runat="server" Text="Select Godown -" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlgodown" Height="25px" Width="300px" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlgodown_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label3" runat="server" Text="Select Stack -" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:DropDownList ID="ddlstack" Height="25px" Width="300px" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="height: 5px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lbl_Fromdate" runat="server" Text="From Date" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txTFrom" runat="server" Width="150px"></asp:TextBox>
                                        <asp:CalendarExtender ID="tctrcdate_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="txTFrom" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label1" runat="server" Text="To Date" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttodate" runat="server" Width="150px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txttodate"
                                            Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnViewReport" runat="server" Text="View Report" OnClick="BtnViewReport_Click" />
                                    </td>
                                    <td align="center">
                                        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/IssueCenterLevel/Storage/Reports.aspx"
                                            ForeColor="indianred" Font-Bold="true">पिछले पृष्ठ पर जाये</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 2px; background-color: Navy" colspan="6">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top" colspan="6">
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
    </center>
    </form>
</body>
</html>

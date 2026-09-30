<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer_Region.aspx.cs"
    Inherits="IssueCenterLevel_Storage_ReportViewer_Region" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Warehouse Reports</title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div>
                <table cellpadding="0" cellspacing="0" style="width: 1000px; border-width: 1px; border-color: Navy"
                    border="1px">
                    <tr style="background-color: #95ccff;">
                        <td align="left">
                            <asp:Label ID="Label4" runat="server" Text="Language - " ForeColor="navy" Font-Bold="true"
                                Font-Size="8pt"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddl_Language" runat="server" Width="155px" Height="25px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddl_Language_SelectedIndexChanged">
                                <asp:ListItem Value="2">English</asp:ListItem>
                                <asp:ListItem Value="1">Hindi</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:LinkButton ID="LinkButton1" runat="server" 
                                ForeColor="indianred" Font-Bold="true" OnClick="LinkButton1_Click">पिछले पृष्ठ पर जाये</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" colspan="2">
                            <rsweb:ReportViewer ID="ReportViewer_Region" runat="server" SizeToReportContent="True"
                                Width="1000px" ProcessingMode="Remote" ZoomMode="PageWidth" Height="550px">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label><asp:Label
                    ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label><br />
            </div>
        </center>
    </form>
</body>
</html>

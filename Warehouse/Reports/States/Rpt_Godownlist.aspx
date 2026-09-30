<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_Godownlist.aspx.cs" Inherits="Reports_States_Rpt_Godownlist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
   Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>GodownList Districtwise & Branchwise Details</title>
</head>
<body>
    <form id="form1" runat="server">
     <center>
        <div style="width: 1000px">
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td align="right">
                        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/login.aspx"
                            ForeColor="indianred" Font-Bold="true">पिछले पृष्ठ पर जाये</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <rsweb:ReportViewer ID="ReportViewer_Region" runat="server" SizeToReportContent="True"
                            Width="100%" ProcessingMode="Remote" ZoomMode="PageWidth" Height="600px">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </div>
    </center>
    </form>
</body>
</html>

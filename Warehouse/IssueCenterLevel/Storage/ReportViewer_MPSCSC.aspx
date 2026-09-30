<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportViewer_MPSCSC.aspx.cs" Inherits="IssueCenterLevel_Storage_ReportViewer_MPSCSC" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPSCSC Report</title>
</head>
<body>
   <form id="form1" runat="server">
    <div>
      <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 0px; "
            border="0px">
            <tr style="background-color: #0bb6e6; height: 25px">
            <td>
            <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/IssueCenterLevel/Storage/MPSCSC_Reports.aspx"
                                ForeColor="Maroon" Font-Bold="true">पिछले पृष्ठ पर जाये</asp:LinkButton>
            </td>
                <td colspan="2" align="left">
                  
                    <asp:Label ID="lblMPSCSCReports" runat="server" Text="MPSCSC Stock Report" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>                      
                </td>
            </tr>
            <tr>
            <td colspan="2">
          <rsweb:ReportViewer ID="ReportsViewer_MPSCSC" runat="server" SizeToReportContent="True" Width="100%"  ProcessingMode="Remote"
    ZoomMode="PageWidth" Height="600px">
    </rsweb:ReportViewer>
    <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label><asp:Label
            ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label><br />
    </td>
            </tr>
     </table>
    
    </div>
  
    </form>
</body>
</html>

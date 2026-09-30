<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_MPSCSC_Commodity_Region.aspx.cs" Inherits="Reports_States_Rpt_MPSCSC_Commodity_Region" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Commodity Details MPSCSC Region</title>
</head>
<body>
    <form id="form1" runat="server">
          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
       <center>
        
        <div>
            <table style="width: 100%">
                 <tr>
                    <td align="center">
                        <rsweb:ReportViewer ID="RV_MPSCSCRegion" runat="server" SizeToReportContent="True"
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

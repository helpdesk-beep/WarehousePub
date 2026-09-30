<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Branch_ReportsViewer.aspx.cs" Inherits="Reports_Branch_Branch_ReportsViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title>Branch Report</title>
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" class="style1" height="700px" width="100%">
        <tr>

            <td align="center" width="100%" height="600px" valign="top">
               <a href="../../Branch_Welcome.aspx" >Go Back</a>
               <div style="height:10px"></div>
               <table>
               <tr>
               <td width="1100px">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="True" Width="100%"  ProcessingMode="Remote"
             ZoomMode="PageWidth" Height="600px">
            </rsweb:ReportViewer>
               </td>
               </tr>
               </table>
            </td>
        </tr>
     </table>
    </div>
    </form>
</body>
</html>

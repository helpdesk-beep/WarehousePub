<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockReport.aspx.cs" Inherits="Reports_States_StockReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MPSCSC Stock Report</title>
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
            <td width="0%">
                &nbsp;
            </td>
            <td align="center" width="100%" height="600px" valign="top">
                <table cellpadding="0" cellspacing="0" class="style1">
                    <tr>
                        <td align="center" width="1100px" height="40px" style="border-style: none none solid none;
                            border-width: 1px; border-color: #008080; color: #003535; font-size: x-large">
                            <table>
                            <tr>
                            <td><asp:Label ID="Label2" runat="server" Text="MPSCSC Available Stock Report"></asp:Label></td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                    <div style="height:10px"></div>
                    <%--<table border="1" cellpadding="0" cellspacing="0" style="border-style:double; border-width:3; padding:1; BORDER-COLLAPSE: collapse; border-color :Maroon  ; width:800px; background-color: #ece9d8;" id="" >
                    
                    <tr>
                        <td width="200px" align="right" class="style1" Height="25px">
                            <asp:Label ID="Label1" runat="server" Text="Crop Year" ></asp:Label>&nbsp &nbsp
                        </td>
                        
                        <td width="200px" align="center" class="style1">&nbsp &nbsp &nbsp
                          <asp:DropDownList ID="DDL_Year" runat="server" Width="150px" Height="20px">
                          </asp:DropDownList>
                        </td>
                                    <td width="200px" align="center" class="style1">
                                     <asp:Button ID="Button1" runat="server" Text="Generate Report" Width="121px" Height="22px" />             
                                    </td>
                                    <td width="100px" align="right" class="style1" align="center">
                                    
                                      <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="True" Font-Size="10pt" 
                                            Width="163px" onclick="LinkButton2_Click1"   >पिछले पृष्ठ पर जाये</asp:LinkButton>
                                   </td>                        
                    </tr>
               </table>--%>
               <div style="height:10px"></div>
               <table>
               <tr>
               <td width="1100px">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="True" Width="100%"  ProcessingMode="Remote"
             ZoomMode="PageWidth" Height="500px">
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

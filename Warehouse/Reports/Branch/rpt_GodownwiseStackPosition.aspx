<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_GodownwiseStackPosition.aspx.cs" Inherits="Reports_Branch_rpt_GodownwiseStackPosition" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Godown wise stack position</title>
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
                                    <td align="left">
                                        &nbsp;</td>
                                    <td align="center">
                                        &nbsp;</td>
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
            </table>

        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label>

        </div>
    </center>
    </div>
    </form>
</body>
</html>

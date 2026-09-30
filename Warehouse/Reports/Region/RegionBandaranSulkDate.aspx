<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegionBandaranSulkDate.aspx.cs" Inherits="RegionBandaranSulkDate" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
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
                                    <td align="right" colspan="3">
                        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/IssueCenterLevel/Storage/Reports.aspx"
                            ForeColor="indianred" Font-Bold="true">पिछले पृष्ठ पर जाये</asp:LinkButton>
                                    </td>
            </tr>
              <tr>
             <td align="center">
                                        Region</td>
                                    <td align="left">
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="Label4" runat="server" Text="Date:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:TextBox ID="txttodate" runat="server" Height="22px" Width="216px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                            TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
            </tr>
            <tr>
            <td colspan="2" align="right">
             <asp:Button ID="BtnViewReport" runat="server" Text="View Report" onclick="BtnViewReport_Click"  
                                            />
            </td>
            <td colspan="2" align="right">
                        &nbsp;</td>
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

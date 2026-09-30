<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Rpt_WHR_Pending_Days.aspx.cs" Inherits="Reports_Branch_Rpt_WHR_Pending_Days" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WHR Pending Days Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="Branch_New_Reports.aspx">Go Back</a>
         
      <table width="100%">
          <tr>
              <td align="center">
                  <asp:Label ID="lbl_Header" runat="server" Text="WHR Pending Days Report" Visible="False" Font-Bold="true" Font-Size="Large" ForeColor="#cc0066"></asp:Label>
              </td>
          </tr>
         <tr align="center">
             <td>
                 <%--<asp:Button ID="btn_Show" runat="server" Text="Show" OnClick="btn_Show_Click" />--%>
                <%--<asp:Label ID="lbl_ddlBranch" runat="server" Text="Branch :"></asp:Label>
                <asp:DropDownList ID="ddl_Branch" runat="server" AutoPostBack="true"  Height="16px" OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged" Width="166px">
                </asp:DropDownList>--%>
                 <rsweb:ReportViewer id="ReportViewer_WHRPendingDays" runat="server" Width="100%"  ProcessingMode="Remote" ShowBackButton="true"
                 Height="600px"> </rsweb:ReportViewer>
               
                <asp:Label ID="lbl_Depot" runat="server" Text="Label" Visible="False"></asp:Label>
                 
                 
            </td>
        </tr>
      </table>
                   </div>

           </form>
</body></html>




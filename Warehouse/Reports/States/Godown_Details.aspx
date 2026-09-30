<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Godown_Details.aspx.cs" Inherits="Reports_States_Godown_Details" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %> 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Godown Information</title>
    <style type="text/css">
        .auto-style1 {
            height: 39px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <table style="width: 100%">
            <tr>
                <td class="auto-style1" align="center" style="background-color: #CCFF99">
                    
                    <asp:Label ID="lbl_title" runat="server" Text="Godown Information" BackColor="#CCFF99" Font-Bold="True" Font-Size="XX-Large" ForeColor="#000099"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td align="left">                
                    <a href="StateReports.aspx" style="color: #0000FF; font-weight: bold;">Go Back</a>
                    </td>

            </tr>
            <tr>
                                <td align="center">
                    <asp:Label ID="lbl_rgn" runat="server" Text="Region :"></asp:Label>
                    &nbsp;
                    &nbsp;<asp:DropDownList ID="ddl_region" runat="server" AutoPostBack="true" Height="23px" OnSelectedIndexChanged="ddl_region_SelectedIndexChanged" Width="133px">
                          </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;<asp:Label ID="lbl_dst" runat="server" Text="District  :"></asp:Label>
                    &nbsp;
                    &nbsp;<asp:DropDownList ID="ddl_dist" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_dist_SelectedIndexChanged">
                          </asp:DropDownList>
                </td>
            </tr>

        </table>              
    
       <rsweb:reportviewer id="ReportViewer_Godown_Details" runat="server" Width="100%"  ProcessingMode="Remote" ShowBackButton="true"
          Height="600px"></rsweb:reportviewer>
        <asp:Label ID="District" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="Depot" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lbl_dt" runat="server" Text="Label" Visible="False"></asp:Label>
          
            </div>
        
    </form>
</body>

</html>
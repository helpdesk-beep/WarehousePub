<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="StateReports.aspx.cs" Inherits="Reports_States_StateReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 1050px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green"
            border="1px">
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="lblRegionReports" runat="server" Text="State Reports" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>                        
             
            
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                    <asp:LinkButton ID="lnk_paytnotrecd" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt" onclick="lnk_paytnotrecd_Click1">Payment Not Received</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                    <asp:LinkButton ID="lnk_paytrecd" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt" onclick="lnk_paytrecd_Click1">Payment Received</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                    <asp:LinkButton ID="lnk_googlemap" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt" onclick="lnk_googlemap_Click1">Distance from Procurement Centre to Godown on Google Map</asp:LinkButton>
                </td>
            </tr>           
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                    <asp:LinkButton ID="lnk_issuedqty" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt" onclick="lnk_issuedqty_Click1">Godown Wise Issued Quantity Report</asp:LinkButton>
                </td>
            </tr>                    
              <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                    <asp:LinkButton ID="lnk_Vctcpty" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt" onclick="lnk_Vctcpty_Click1">Godown Vacant Storage Capacity with Graph</asp:LinkButton>
                </td>
            </tr>          
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                    <asp:LinkButton ID="lnk_vctmorethan1lac" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt" onclick="lnk_vctmorethan1lac_Click1">Godown Vacant Storage Capacity More Than 1 Lac with Graph</asp:LinkButton>
                </td>
            </tr> 
            
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                <td>
                    <asp:LinkButton ID="lnk_gdningo" runat="server" ForeColor="navy" Font-Bold="true" Font-Size="10pt" onclick="lnk_gdningo_Click1">Godown Details Districtwise</asp:LinkButton>
                </td>
            </tr>                               
                                                         
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <script language="JavaScript1.2">
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 10px;
        }
    </style>
</asp:Content>
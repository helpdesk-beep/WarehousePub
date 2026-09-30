<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Branch_New_Reports.aspx.cs" Inherits="Reports_Branch_Branch_New_Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 1000px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green" border="1px">
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="lblStorageReports" runat="server" Text="शाखा रिपोर्ट सूची" ForeColor="whitesmoke"
                    Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            
            <tr style="background-color:Green; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" runat="server" Text="गोदामो की रिपोर्ट" ForeColor="whitesmoke"
                      Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            
           

            <tr>
                <td style="width: 10px" align="center">
                     <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="lnk_paytnotrecd" runat="server" ForeColor="Navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="lnk_paytnotrecd_Click">Payment Not Recieved</asp:LinkButton>
                </td>
            </tr>

            <tr>
                <td style="width: 10px" align="center">
                     <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="lnk_whrpendingdays" runat="server" ForeColor="Navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="lnk_whrpendingdays_Click">WHR Pending Days from Acceptance Date </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="Region_New_Reports.aspx.cs" Inherits="Region_New_Reports" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div style="width: 1000px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green" border="1px">
            
            
            <tr style="background-color:Green; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label3" runat="server" Text="गोदामो की रिपोर्ट" ForeColor="whitesmoke"
                      Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            
                    
            
            <tr>
                <td style="width: 10px" align="center">
                   <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                   <asp:LinkButton ID="LinkButton2" runat="server"  ForeColor="navy" Font-Bold="true"
                    Font-Size="10pt" onclick="lnk_issuedqtygdnwise_Click">Godown Wise Issued Quantity Report From To Date</asp:LinkButton>
                </td>
            </tr>              
                     
                      
           
        </table>
    </div>
</asp:Content>


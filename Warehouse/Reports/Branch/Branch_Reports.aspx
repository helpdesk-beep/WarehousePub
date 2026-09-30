<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="Branch_Reports.aspx.cs" Inherits="Reports_Branch_Branch_Reports" Title="Branch Reports" %>

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
                    <asp:LinkButton ID="LinkButton52" runat="server"  ForeColor="navy" Font-Bold="true"
                     Font-Size="10pt" OnClick="LinkButton52_Click" >Godown Type wise Godown list </asp:LinkButton>
                </td>
            </tr>
           
            
            <tr>
                <td style="width: 10px" align="center">
                   <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                   <asp:LinkButton ID="LinkButton80" runat="server"  ForeColor="navy" Font-Bold="true"
                    Font-Size="10pt" onclick="LinkButton80_Click">Godown Wise Stack Capacity</asp:LinkButton>
                </td>
            </tr>
                     
            <tr style="background-color:Maroon; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label3" runat="server" Text="गोदामो की क्षमता एवं उपयोगिता संबन्धित रिपोर्ट" ForeColor="whitesmoke"
                     Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            
             <tr>
                <td style="width: 10px" align="center">
                  <strong><span style="color: Navy">1.</span></strong></td>
                <td>
                    <asp:LinkButton ID="LinkButton34" runat="server"  ForeColor="navy" Font-Bold="true"
                    Font-Size="10pt" onclick="LinkButton34_Click"   >Godown Wise Current Capacity </asp:LinkButton>
                </td>
            </tr>
           
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                     <asp:LinkButton ID="LinkButton55" runat="server"  ForeColor="navy" Font-Bold="true"
                     Font-Size="10pt" OnClick="LinkButton55_Click"   >Godown type wise current capacity </asp:LinkButton>
                </td>
            </tr>
            
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                     <asp:LinkButton ID="LinkButton62" runat="server"  ForeColor="navy" Font-Bold="true"
                     Font-Size="10pt" onclick="LinkButton62_Click">All Godowns Capacity & Utilization </asp:LinkButton>
               </td>
            </tr>
            
           
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                     <asp:LinkButton ID="LinkButton69" runat="server"  ForeColor="navy" Font-Bold="true"
                     Font-Size="10pt" onclick="LinkButton69_Click">Commodity Wise Godown wise Stock Report</asp:LinkButton>
                </td>
            </tr>
            
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">5.</span></strong>
                </td>
                <td>
                     <asp:LinkButton ID="LinkButton70" runat="server"  ForeColor="navy" Font-Bold="true"
                     Font-Size="10pt" onclick="LinkButton70_Click">Godown Wise Commodity Summary</asp:LinkButton>
                </td>
            </tr>
            
          
            
            <tr style="background-color:Teal; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label4" runat="server" Text="रजिस्टर रिपोर्ट" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                   <asp:LinkButton ID="LinkButton49" runat="server"  ForeColor="navy" Font-Bold="true"                         
                        Font-Size="10pt" onclick="LinkButton49_Click" >Godown Register</asp:LinkButton>
                </td>
            </tr>   
              <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>        
                    <asp:LinkButton ID="LinkButton20" runat="server" OnClick="LinkButton20_Click" ForeColor="navy"
                        Font-Bold="true" Font-Size="10pt">Stock Register (Godwn and Commodity Wise)</asp:LinkButton>
                </td>
            </tr>      
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>                    
                    <asp:LinkButton ID="LinkButton3" runat="server" Width="304px" OnClick="LinkButton3_Click"
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt">Stack Register</asp:LinkButton>
                </td>
            </tr>
            
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">4.</span></strong>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton39" runat="server"  ForeColor="navy" Font-Bold="true"                         
                        Font-Size="10pt" onclick="LinkButton39_Click" 
                        PostBackUrl="~/Reports/Branch/Depositerlezer.aspx">Depositer Lazer(DL)</asp:LinkButton>
                </td>
            </tr>
            
            <tr>
                <td style="width: 10px" align="center">
                   <strong><span style="color: Navy">5.</span></strong>
                </td>
                <td>
                   <asp:LinkButton ID="LinkButton40" runat="server"  ForeColor="navy" Font-Bold="true"                         
                    Font-Size="10pt" onclick="LinkButton39_Click" PostBackUrl="~/Reports/Branch/StockRegister.aspx">Stock Register(SR)</asp:LinkButton>
                </td>
            </tr>                     
                      
           
        </table>
    </div>
</asp:Content>


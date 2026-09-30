<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMasterNafed.master" AutoEventWireup="true" CodeFile="StateReportsNafed.aspx.cs" Inherits="StatePages_StateReportsNafed" Title="Nafed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div style="width: 1050px; margin-left: 0px">
        <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green"
            border="1px">
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="lblRegionReports" runat="server" Text="State Report " ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
             <tr style="background-color:Olive; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label5" runat="server" Text="Dalhan WHR Report 2022-23" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton15" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton15_Click">Branch wise Chana,Masoor and Sarson WHR Report</asp:LinkButton></td>
               </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton16" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton16_Click">Branch wise Chana,Masoor and Sarson WHR Report Details</asp:LinkButton></td>
               </tr>
             <tr style="background-color:Olive; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label4" runat="server" Text="Dalhan WHR Report 2021-22" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>

            </tr>


            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton10" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton10_Click">Branch wise Chana,Masoor and Sarson WHR Report</asp:LinkButton></td>
               </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton11" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton11_Click">Pending Acceptance Detail for Generate WHR.</asp:LinkButton></td>
               </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton12" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton12_Click">New Generated WHR Aginst Deleted WHR Dalhan WHR 2021-22</asp:LinkButton></td>
               </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton13" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton13_Click">Moong,Udad WHR Summary Report 2021-22</asp:LinkButton></td>
               </tr>
             <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton14" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" OnClick="LinkButton14_Click">Moong,Udad WHR Detail Report 2021-22</asp:LinkButton></td>
               </tr>
             <tr style="background-color:Olive; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label3" runat="server" Text="e-WHR Report 2020-21" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton9" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton9_Click">Branch wise Chana,Masoor and Sarson e-WHR Report</asp:LinkButton></td>
               </tr>
            <tr style="background-color:Maroon; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" runat="server" Text="e-WHR Report 2019-20" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton3" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton3_Click">e-WHR Branch wise Report</asp:LinkButton></td>
               </tr>
               <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton4" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton4_Click">e-WHR District wise Report</asp:LinkButton></td>
               </tr>
                <tr>
               <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton5" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton5_Click">e-WHR Online Submission and Print Detail</asp:LinkButton></td>
               </tr>
                <tr>
               <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton6" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton6_Click">Commodity wise e-WHR Register</asp:LinkButton></td>
               </tr>
                <tr>
               <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton7" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton7_Click">Arahar e-WHR District wise Report</asp:LinkButton></td>
               </tr>
                <tr>
               <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton8" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton8_Click">Commodity wise e-WHR Online Submission and Print Detail</asp:LinkButton></td>
               </tr>
            <tr style="background-color:Lime;">
                <td colspan="2" align="center" style="height: 25px">
                    <asp:Label ID="Label2" runat="server" Text="Procurement 2018-19" ForeColor="WhiteSmoke"
                        Font-Bold="True" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">1.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton128" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton128_Click">GRAM Procurement 2018-19</asp:LinkButton></td>
               </tr> 
                <tr>
                <td style="width: 10px; height: 28px;" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">2.</span></td>
                <td style="height: 28px">
                        <asp:LinkButton ID="LinkButton129" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton129_Click">Sarson Procurement 2018-19</asp:LinkButton></td>
               </tr> 
               <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">3.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton130" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton130_Click">Masoor Procurement 2018-19</asp:LinkButton></td>
               </tr>      
              <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">4.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton135" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton135_Click">District wise Summary of Chana,Sarson,Masoor Procurement 2018-19</asp:LinkButton></td>
               </tr> 
               <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">5.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton136" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton136_Click"> Comparative Report of Provisional D.F , Final D.F & WHR Issued Quantity Chana,Sarson,Masoor Procurement 2018-19</asp:LinkButton></td>
               </tr> 
               <tr>
                <td style="width: 10px" align="center">
                   <span style="color: Navy; font-weight: bold; font-size: 10pt">6.</span></td>
                <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton1_Click"> District,Branch and WHR wise deposit report for Chana,Sarson,Masoor Procurement 2018-19</asp:LinkButton></td>
               </tr> 
               <tr>
                    <td style="width: 10px" align="center">
                        <span style="color: Navy; font-weight: bold; font-size: 10pt">7.</span></td>
                    <td>
                        <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="lik2" 
                        ForeColor="navy" Font-Bold="true" 
                        Font-Size="10pt" onclick="LinkButton2_Click" > District,Branch and WHR Wise Deposit Report for Dalhan Tilhan 2018-19</asp:LinkButton>
                    </td>
               </tr>                
               
            </div>
</asp:Content>


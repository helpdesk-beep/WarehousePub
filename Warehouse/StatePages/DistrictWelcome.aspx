<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMasterMfd.master" AutoEventWireup="true" CodeFile="DistrictWelcome.aspx.cs" Inherits="StatePages_DistrictWelcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <table cellpadding="5" cellspacing="0" style="width: 100%; border-width: 1px; border-color: Green"
            border="1px">
            <tr style="background-color: #0bb6e6; height: 25px">
                <td colspan="2" align="center">
                    <asp:Label ID="lblStorageReports" runat="server" Text="Storage Reports" ForeColor="whitesmoke"
                        Font-Bold="true" Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">1.</span></strong>
                </td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" 
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton1_Click" 
                        >WHR Current Status Crop yearly</asp:LinkButton></td>
            </tr>
            <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">2.</span></strong>
                </td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton12" runat="server" 
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton12_Click" 
                        >Paddy procyrement 2015-16</asp:LinkButton></td>
            </tr>

       <tr>
                <td style="width: 10px" align="center">
                    <strong><span style="color: Navy">3.</span></strong>
                </td>
                <td>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" 
                        ForeColor="navy" Font-Bold="true" Font-Size="10pt" OnClick="LinkButton2_Click" 
                        >Pending WHR for Dalhan 2021-22</asp:LinkButton></td>
            </tr>
          
        </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
     <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>


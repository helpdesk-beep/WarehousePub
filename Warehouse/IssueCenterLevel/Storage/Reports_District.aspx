<%@ Page Language="C#" MasterPageFile="~/MasterPage/WarehouseApplication.master" AutoEventWireup="true" CodeFile="Reports_District.aspx.cs" Inherits="IssueCenterLevel_Storage_Reports_District" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table style="width: 594px; background-image: url(../../images/images[26].jpg); ">
            <tr>
                <td colspan="2" style="background-color: dimgray ;border-collapse: collapse; border:solid 1px white;height: 18px;">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;border-collapse: collapse; border:solid 1px white; height: 24px;">
                    <span style="font-size: 10pt; color: maroon; font-family: Microsoft Sans Serif"><strong>
                        <asp:Label ID="lblDistrictReports" runat="server" Text="Region Reports District wise"></asp:Label></strong></span></td>
            </tr>
            
            
            
            <tr><td><b>1:</b>
                </td><td><asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Commodity Details(MPWLC)</asp:LinkButton></td></tr>
                <tr><td><b>2:</b>
                </td><td><asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Warehouse Capacity and Utilization(MPWLC)</asp:LinkButton></td></tr>
                <tr><td><b>3:</b>
                </td><td><asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Warehouse Depositors Details(MPWLC)</asp:LinkButton></td></tr>
     <tr>
         <td>
             <strong>4:</strong></td>
         <td>
             <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Warehouse Capacity and Utilization(MPWLC till now)</asp:LinkButton></td>
     </tr>
     
      <tr>
         <td>
             <strong>5:</strong></td>
         <td>
             <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click" >Warehouse Scientific and Maximum Capacity and Utilization(MPWLC till now)</asp:LinkButton></td>
     </tr>
     
     
            </table>
</asp:Content>


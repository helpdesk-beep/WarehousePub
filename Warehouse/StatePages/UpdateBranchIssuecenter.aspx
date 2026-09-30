<%@ Page Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="UpdateBranchIssuecenter.aspx.cs" Inherits="StatePages_UpdateBranchIssuecenter" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr>
<td>
    <asp:Label ID="lbldist" runat="server" Text="District Name"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" onselectedindexchanged="ddlDistrict_SelectedIndexChanged" 
        >
    </asp:DropDownList>
</td>
<td>
    <asp:Label ID="Label1" runat="server" Text="Branch Name"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlbranch" runat="server" 
       >
    </asp:DropDownList>
</td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Issue center linked with branch"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlissuecenter" runat="server">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td>
    <asp:Button ID="btnsubmit" runat="server" Text="Submit" 
        onclick="btnsubmit_Click" />
</td>
</tr>
</table>
<br />
<br />
<center><h3 style="background-color:Gray;">Depositor Transfer </h3></center>
<table>
<tr>
<td colspan="4">
<h4>Transfer From Branch :</h4>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label3" runat="server" Text="District Name"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="ddlDistrict2" runat="server" AutoPostBack="True" onselectedindexchanged="ddlDistrict2_SelectedIndexChanged" 
        >
    </asp:DropDownList>
</td>
<td>
    <asp:Label ID="Label4" runat="server" Text="Branch Name"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlBranch2" runat="server" onselectedindexchanged="ddlBranch2_SelectedIndexChanged" AutoPostBack="true"
       >
    </asp:DropDownList>
</td>
</tr>
<br />
<tr>
<td colspan="4">
<h4>Transfer To Branch :</h4>
</td>
<br />
</tr>
<tr>
<td>
    <asp:Label ID="Label5" runat="server" Text="District Name"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="ddlDistrict3" runat="server" onselectedindexchanged="ddlDistrict3_SelectedIndexChanged" AutoPostBack="true"
        >
    </asp:DropDownList>
</td>
<td>
    <asp:Label ID="Label6" runat="server" Text="Branch Name"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlBranch3" runat="server"
       >
    </asp:DropDownList>
</td>
</tr>
<tr>
<td colspan="4">
<h4>Depositor List :</h4>
</td>
<br />
</tr>
<tr>
<td>
<asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:CheckBoxList>
</td>
</tr>
<tr>
<td colspan="4" align="center">
   <asp:Button ID="btnSubmit2" runat="server" Text="Submit" 
        onclick="btnSubmit2_Click"/>
</td>
</tr>
</table>
</asp:Content>


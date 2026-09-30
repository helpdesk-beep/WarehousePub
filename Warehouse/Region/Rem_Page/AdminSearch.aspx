<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Region_Master.master" AutoEventWireup="true" CodeFile="AdminSearch.aspx.cs" Inherits="AdminSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">



.tb6 {
	border: 3px double #CCCCCC;
	width: 230px;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr>
<td>
District:
</td>
<td>

                                                        <asp:DropDownList ID="DDL_Dist" runat="server" 
                                                            AutoPostBack="True"                                                             Width="205px" 
        Height="30px" Font-Bold="true" ForeColor="Navy" 
                                                            CssClass="tb6" onselectedindexchanged="DDL_Dist_SelectedIndexChanged">
                                                        </asp:DropDownList>

</td>
<td>
Depot:
</td>
<td>
                                                        <asp:DropDownList ID="DDL_Depot" runat="server" Width="205px" Height="30px" Font-Bold="true"
                                                            ForeColor="Navy" 
    CssClass="tb6">
                                                        </asp:DropDownList>
                                                    
</td>
</tr>
<tr>
<td>
Search DO:
</td>
<td>
    <asp:TextBox ID="txtdo" runat="server"></asp:TextBox>
    <asp:Button ID="submitdo" runat="server"
        Text="submit" onclick="submitdo_Click" />
</td>
<td>Search Challan:</td>
<td>
<asp:TextBox ID="txtchallan" runat="server" Height="122px" TextMode="MultiLine" 
        Width="339px"></asp:TextBox>
    <asp:Button ID="submitchallan"
    runat="server" Text="submit" onclick="submitchallan_Click" />
    (Please type after select * from)</td>

</tr>

<tr>
<td>
    Delete Godown:</td>
<td>
    <asp:TextBox ID="txtgodownid" runat="server"></asp:TextBox>
    <asp:Button ID="btndelete" runat="server" Text="Submit" 
        onclick="btndelete_Click" />
</td>
<td>&nbsp;</td>
<td>
    &nbsp;</td>

</tr>

</table>
<table>
<tr>
<td>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
</td>
</tr>

</table>
</asp:Content>


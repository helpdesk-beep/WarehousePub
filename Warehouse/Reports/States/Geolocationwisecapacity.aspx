<%@ Page Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="Geolocationwisecapacity.aspx.cs" Inherits="Reports_States_Geolocationwisecapacity" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
 <div>
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
Branch:
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
Longitude:
</td>
<td>
    <asp:TextBox ID="txtlong" runat="server" Height="29px" Width="126px"></asp:TextBox>
    ex:78.209267</td>
</tr>
<tr>
<td>
Latitude:
</td>
<td>
    <asp:TextBox ID="txtlatitude" runat="server"></asp:TextBox>
    ex:26.203194</td>
</tr>
<tr>
<td>
</td>
<td>
    <asp:Button ID="btnsubmit" runat="server" Text="Submit" 
        onclick="btnsubmit_Click" />
</td>
</tr>
</table>

     <asp:GridView ID="GridView1" runat="server" 
         onselectedindexchanged="GridView1_SelectedIndexChanged">
         <Columns>
             <asp:CommandField ShowSelectButton="True" />
         </Columns>
     </asp:GridView>
    </div>

</asp:Content>


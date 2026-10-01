<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/StateMaster.master" AutoEventWireup="true" CodeFile="AddBranch.aspx.cs" Inherits="StatePages_AddBranch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <%--Update New--%>
 <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
 <link href="../assets/New/css/select2.min.css" rel="stylesheet" />
 <script type="text/javascript" src="../assets/New/js/select2.min.js"></script>
 <script type="text/javascript">
     $(function () {
         $("[id*=ddlDistrict]").select2();
     });
 </script>
 <script type="text/javascript">
     $(function () {
         $("[id*=ddlissuecenter]").select2();
     });
 </script>
 <script type="text/javascript">
     $(function () {
         $("[id*=ddlbranchtype]").select2();
     });
      <%--Grid Filter--%>
 </script>
      <script type="text/javascript">
          function filterGrid() {

              var input = document.getElementById("<%= txtSearch.ClientID %>");
          var filter = input.value.toLowerCase();

          var table = document.getElementById("<%= gvbranch.ClientID %>");
              var trs = table.getElementsByTagName("tr");

              for (var i = 1; i < trs.length; i++) { // skip header row
                  var display = false;
                  var tds = trs[i].getElementsByTagName("td");

                  for (var j = 0; j < tds.length; j++) {
                      var cell = tds[j];
                      if (cell && cell.textContent.toLowerCase().indexOf(filter) > -1) {
                          display = true;
                          break;
                      }
                  }

                  trs[i].style.display = display ? "" : "none";
              }
          }
      </script>
    <table style="margin-top:20px; margin-left:10px">
    
<tr valign="top" style="height:30px">
<td style="padding-top:5px">
    <asp:Label ID="lbldist" runat="server" Text="District Name"></asp:Label>
</td>
<td>
 <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
    </asp:DropDownList>
</td>
<td></td>
<td style="padding-top:5px">
    <asp:Label ID="Label2" runat="server" Text="Issue center linked with branch"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlissuecenter" runat="server">
    </asp:DropDownList>
</td>
</tr>
<tr valign="top" style="height:40px">
<td style="padding-top:10px">
    <asp:Label ID="Label1" runat="server" Text="Branch Name"></asp:Label>
</td>
<td style="padding-top:10px">
    <asp:TextBox ID="txtbranch" runat="server"></asp:TextBox>
</td>
<td style="padding-left:50px"> </td>
<td style="padding-top:10px">
    Branch Type</td>
<td style="padding-top:10px">
    <asp:DropDownList ID="ddlbranchtype" runat="server" 
        onselectedindexchanged="ddlbranchtype_SelectedIndexChanged">
       
    </asp:DropDownList>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td>

    <asp:Label ID="lblbranchid" runat="server"></asp:Label>

</td>
<td align="right">
    <asp:Button ID="btnsubmit" runat="server" Text="Submit" 
        onclick="btnsubmit_Click" Width="77px"/>
</td>
<td style="width:30px"></td>
<td>
    <asp:Button ID="btncancel" runat="server" Text="Cancel" 
        onclick="btncancel_Click" Width="71px" />
</td>
</tr>
</table>
   <table style="margin-top:20px ; margin-left:10px">
        <tr>

     <td align="left">
         <asp:TextBox ID="txtSearch" runat="server"
             placeholder="Search here..."
             Style="width: 190px; height: 36px; margin-bottom: 10px; padding: 0 15px; font-size: 15px; border: 1px solid #000; border-radius: 8px; outline: none; transition: all 0.25s ease; box-shadow: 0 2px 6px rgba(0,0,0,0.08);"
             onkeyup="filterGrid();" />

     </td>
 </tr>
   <tr>
   <td>
       <asp:GridView ID="gvbranch" runat="server" AutoGenerateColumns="False" 
           CellPadding="4" ForeColor="#333333" GridLines="None" 
           onselectedindexchanged="gvbranch_SelectedIndexChanged">
           <AlternatingRowStyle BackColor="White" />
           <Columns>
               <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
               <asp:BoundField DataField="IssueCenterName" HeaderText="Issue Center" />
               <asp:BoundField DataField="BranchId" HeaderText="Branch Id" />
               <asp:BoundField DataField="IssueCenterId" HeaderText="Issue Center ID" />
               <asp:CommandField HeaderText="Update" ShowHeader="True" 
                   ShowSelectButton="True" />
           </Columns>
          
       </asp:GridView>
   </td>
   </tr>
   </table>

</asp:Content>


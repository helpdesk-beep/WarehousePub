<%@ Page Language="C#" MasterPageFile="~/Inspections/Masters/State.master" AutoEventWireup="true" CodeFile="Get_District_Password.aspx.cs" Inherits="Get_District_Password" Title="District Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <%--Dropdown New--%>
   <script src="<%= ResolveUrl("~/NEW_CSS/js/jquery-1.12.4.js") %>"></script>
   <link href="../../assets/New/css/select2.min.css" rel="stylesheet" />
   <script type="text/javascript" src="../../assets/New/js/select2.min.js"></script>
   <script type="text/javascript">
       $(function () {
           $("[id*=ddlDistrict]").select2();
       });
   </script>
 <%--  <script type="text/javascript">
       $(function () {
           $("[id*=District_Id]").select2();
       });--%>
   </script>
   <%--Dropdown End--%>
    <fieldset style="width: 98%; border: 2px solid #0bb6e6; margin: 10px; padding: 10px; border-radius: 8px; background-color: #fff;">
        <table style="width: 100%; border-collapse: collapse;">
            <tr style="background-color: #0bb6e6; height: 40px">
                <td align="center" style="border-radius: 5px 5px 0 0;">
                    <asp:Label ID="lblTitle" runat="server" Text="DISTRICT PASSWORD MANAGEMENT" Font-Bold="true" ForeColor="White" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" style="padding: 25px; background-color: #fcfcfc; border-bottom: 1px solid #eee;">
                    <span style="font-weight: bold; color: #333;">Select District:</span> 
                    <asp:DropDownList ID="ddlDistrict" runat="server" Width="250px" Height="30px" CssClass="dropdown"></asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="Search Records" CssClass="BTNBLUE" OnClick="btnSearch_Click" style="padding: 5px 20px; cursor: pointer;" />
                </td>
            </tr>
            <tr>
                <td style="padding-top: 20px;">
                   <asp:GridView ID="gvDistrict" runat="server" AutoGenerateColumns="False" Width="100%" 
    BackColor="White" BorderColor="#e0e0e0" BorderStyle="Solid" BorderWidth="1px" 
    CellPadding="10" EmptyDataText="No details found for the selected district.">
    <Columns>
        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%" HeaderStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="Regionnm" HeaderText="Region" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="District_Name" HeaderText="District Name" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="District_Id" HeaderText="District ID" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" />
        <asp:BoundField DataField="DMPassword" HeaderText="DM Password" ItemStyle-Width="25%" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" />
        
        <%-- FIXED: DataField matches DSOPassord from your table --%>
        <asp:BoundField DataField="DSOPassord" HeaderText="DSO Password" ItemStyle-Width="25%" ItemStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" />
    </Columns>
    
    <%-- Primary Header Styling --%>
    <HeaderStyle BackColor="#444" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Height="35px" />
    
    <RowStyle HorizontalAlign="Center" Height="35px" ForeColor="#333" />
    <AlternatingRowStyle BackColor="#f2f9fc" />
</asp:GridView>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
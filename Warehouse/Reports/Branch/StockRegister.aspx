<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="StockRegister.aspx.cs" Inherits="Reports_Branch_StockRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
<tr>
    
<td>
<asp:Label ID="Label1" runat="server" Text="Select Depositer"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddldepositer" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddldepositer_SelectedIndexChanged">
    </asp:DropDownList>
</td>
<td>
    <asp:Label ID="Label2" runat="server" Text="Commodity"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlcomm" runat="server">
    </asp:DropDownList>
</td>
<td>
Godown:
</td>
<td>
    <asp:DropDownList ID="ddlgodown" runat="server">
    </asp:DropDownList>
</td>
<td>
    <asp:Button ID="btnsubmit" runat="server" Text="Submit" 
        onclick="btnsubmit_Click" />
</td>
<td>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnexel" runat="server"  Text="Import To Exel" 
        onclick="btnexel_Click" />
</td>
</tr>
<tr>
<td colspan="2">
    <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" 
        Text="Depositer Wise" AutoPostBack="True" GroupName="rb" 
        oncheckedchanged="RadioButton1_CheckedChanged" />
    <asp:RadioButton ID="RadioButton2"
        runat="server" Text="Godown Wise" AutoPostBack="True" GroupName="rb" 
        oncheckedchanged="RadioButton2_CheckedChanged" />
</td>

</tr>
</table>
<div id="gv" runat="server">

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="2" CellSpacing="5">
        
        <Columns>
            <asp:BoundField DataField="Depositor_Name" HeaderText="Depositer" />
            <asp:BoundField DataField="commodity" HeaderText="Description of goods" />
            <asp:BoundField DataField="recbags" HeaderText="Received Bags/Unit" />
            <asp:BoundField DataField="recweght" HeaderText="Received Weight" />
            <asp:BoundField DataField="Mode_of_weighment" HeaderText="% of weightment" />
            <asp:BoundField DataField="Category_Id" HeaderText="Grade" />
            <asp:BoundField DataField="Moisture" HeaderText="% of moisture" />
            <asp:BoundField DataField="WHR_Issue_Date" HeaderText="WHR Date" />
            <asp:BoundField DataField="Depositor_WHR_Id" HeaderText="WHR No" />
            <asp:BoundField DataField="delbags" HeaderText="Delivered Bags" />
            <asp:BoundField DataField="delwght" HeaderText="Delivered Weight" />
            <asp:BoundField DataField="Mode_of_weighment" HeaderText="% of weightment2" />
            <asp:BoundField DataField="Loss" HeaderText="Loss" />
            <asp:BoundField DataField="Gain" HeaderText="Gain" />
            <asp:BoundField HeaderText="Total Weight" />
            <asp:BoundField DataField="Delmoisture" HeaderText="% of moisture2" />
            <asp:BoundField DataField="Issue_Source_ID" HeaderText="Delivery order No" />
            <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery Date" />
            <asp:BoundField HeaderText="Balance Bags" />
            <asp:BoundField HeaderText="Balance Weight" />
            <asp:BoundField DataField="MktValue_of_Commodity" 
                HeaderText="Market Price of goods" />
            <asp:BoundField HeaderText="Value at the time of Deposit" />
            <asp:BoundField HeaderText="Progressive value of stock" />
            <asp:BoundField HeaderText="Initials of Branch Manager" />
        </Columns>
    </asp:GridView>
    <h3>M.P. Warehousing & logistics corporation</h3><br />
    <h4>Stock Register</h4>
<table>
<tr>
<td>
Branch:
</td>
<td>
<asp:Label ID="lblbranch" runat="server" Text="Branch"></asp:Label>
</td>
<td>
Godown:
</td>
<td>
<asp:Label ID="lblgodown" runat="server" Text="Godown"></asp:Label>
</td>
</tr>

    

</table>
    
    <asp:ListView ID="ListView1" runat="server">
    <LayoutTemplate>
    <table align="center"  style="border-collapse: collapse;border-style: solid; border-width: thin">
    <tr style="border-style: solid; border-width: thin">
   <td rowspan="2" style="border-style: solid; border-width: thin">
   Name of Depositor
   </td>
    <td colspan="6" style="border-style: solid; border-width: thin">
    Description of goods received
    </td>
    <td colspan="8" style="border-style: solid; border-width: thin">
    Description of goods released or delivered
    </td>
    <td rowspan="2" style="border-style: solid; border-width: thin">
    Delivery order No.
    </td>
    <td rowspan="2" style="border-style: solid; border-width: thin">
    Delivery Date
    </td>
    <td colspan="2" style="border-style: solid; border-width: thin">
    Balance of Stock
    </td>
    <td rowspan="2" style="border-style: solid; border-width: thin">
    Market price of goods on the date deposit
    </td>
    <td rowspan="2" style="border-style: solid; border-width: thin">
    Progressive Value of stock
    </td>
    <td rowspan="2" style="border-style: solid; border-width: thin">
    Initials of Branch Manager
    </td>
    </tr>
    <tr style="border-style: solid; border-width: thin">
    <td>
    Name of Commodity
    </td>
    <td>
    No. of unit/Bags
    </td>
    <td style="border-style: solid; border-width: thin">
    Weight(Qt. Kg. Gms.)
    </td>
    <td style="border-style: solid; border-width: thin">
    % of Weightment
    </td>
    <td style="border-style: solid; border-width: thin">
    Grade 
    </td>
    <td style="border-style: solid; border-width: thin">
    % of moisture
    </td>
     <td style="border-style: solid; border-width: thin">
    WHR Date.
    </td>
     <td style="border-style: solid; border-width: thin">
    WHR No
    </td>
    <td style="border-style: solid; border-width: thin">
    No of Unit or Bags
    </td>
    <td style="border-style: solid; border-width: thin">
    Weight
    </td>
    <td style="border-style: solid; border-width: thin">
    Loss
    </td>
    <td style="border-style: solid; border-width: thin">
    Gain
    </td>
    <td style="border-style: solid; border-width: thin">
    Total Weight
    </td>
    <td style="border-style: solid; border-width: thin">
    % moisture
    </td>
    <td style="border-style: solid; border-width: thin">
    No. Unit/Bags
    </td>
    <td style="border-style: solid; border-width: thin">
    Weight
    </td>
    </tr>
    <tr id="itemPlaceholder" runat="server" style="border: thin solid #000000;"></tr>
    </table>
    
    </LayoutTemplate>
    <ItemTemplate>
   
    <td style="border-style: solid; border-width: thin">
     <%# Eval("Depositer")%>
    </td >
    <td style="border-style: solid; border-width: thin">
     <%# Eval("Description of goods")%>
    </td >
    <td style="border-style: solid; border-width: thin">
     <%# Eval("Received Bags/Unit")%>
    </td >
     <td style="border-style: solid; border-width: thin">
     <%# Eval("Received Weight")%>
    </td >
     <td style="border-style: solid; border-width: thin">
     <%# Eval("% of weightment")%>
    </td >
     <td style="border-style: solid; border-width: thin">
     <%# Eval("Grade")%>
    </td >
      <td style="border-style: solid; border-width: thin">
     <%# Eval("% of moisture")%>
    </td >
     <td style="border: thin solid #000000;">
   <%# Eval("WHR Date")%>
    
    </td>
     <td style="border-style: solid; border-width: thin">
     <%# Eval("WHR No")%>
    </td>
     <td style="border-style: solid; border-width: thin">
     <%# Eval("Delivered Bags")%>
    </td >
     <td style="border-style: solid; border-width: thin">
     <%# Eval("Delivered Weight")%>
    </td >
    
     <td style="border-style: solid; border-width: thin">
     <%# Eval("Loss")%>
    </td >
     <td style="border-style: solid; border-width: thin">
     <%# Eval("Gain")%>
    </td >
    
    <td style="border-style: solid; border-width: thin">
     <%# Eval("Total Weight")%>
    </td >
     <td style="border-style: solid; border-width: thin">
     <%# Eval("% of moisture2")%>
    </td >
      <td style="border-style: solid; border-width: thin">
     <%# Eval("Delivery order No")%>
    </td >
     <td style="border-style: solid; border-width: thin">
     <%# Eval("Delivery Date")%>
    </td >
        <td style="border-style: solid; border-width: thin">
     <%# Eval("Balance Bags")%>
    </td >
        <td style="border-style: solid; border-width: thin">
     <%# Eval("Balance Weight")%>
    </td >
        
        <td style="border-style: solid; border-width: thin">
     <%# Eval("Value at the time of Deposit")%>
    </td >
    <td style="border-style: solid; border-width: thin">
     <%# Eval("Progressive value of stock")%>
    </td >
        <td style="border-style: solid; border-width: thin">
     <%# Eval("Initials of Branch Manager")%>
    </td >
    </tr>

    </ItemTemplate>
    </asp:ListView>
    
    </div>
</asp:Content>


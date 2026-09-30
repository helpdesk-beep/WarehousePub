<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Storage_Charges_Bill.aspx.cs" Inherits="Accounting_frm_Storage_Charges_Bill" Title="Storage Charges Bill" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
        <div>
<table style="width: 100%;">
<tr id="msg">
<td colspan="6"><asp:Label ID="lblmsg" Text="" ForeColor="red" Font-Bold="true" runat="server"></asp:Label></td>
</tr>
 <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center">
            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Storage Charges Bill"></asp:Label></td>
    </tr>
    
    <tr>
<td>
    <asp:Label ID="lbltod" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Type Of Depositor"></asp:Label>
    </td>
<td valign="middle"> 
    <asp:DropDownList ID="ddldepositor" runat="server" AutoPostBack="True" 
        TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
        onselectedindexchanged="ddldepositor_SelectedIndexChanged">
    </asp:DropDownList></td>
<td> 
    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Name of Depositor"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddldepos_name" runat="server" TabIndex="1" Height="25px" 
            Width="200px" Font-Size="10pt"
            AutoPostBack="True" 
            onselectedindexchanged="ddldepos_name_SelectedIndexChanged">
        </asp:DropDownList></td>
</tr>
<tr>
    <td> 
    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lblbilltype" runat="server" Text="Type of Bill"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddlBillType" runat="server" TabIndex="1" Height="25px" 
            Width="200px" Font-Size="10pt"
            AutoPostBack="True" 
            onselectedindexchanged="ddlBillType_SelectedIndexChanged">
            <asp:ListItem Value="-1">--Select--</asp:ListItem>
            <asp:ListItem Value="1">Accrued or Actual Bill</asp:ListItem>
             <asp:ListItem Value="2">Reservation Bill</asp:ListItem>
            <asp:ListItem Value="3">Over & Above Bill</asp:ListItem>            
        </asp:DropDownList></td>
        <td> 
    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="toc" runat="server" Text="Type of Calculation"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddlCalcTy" runat="server" TabIndex="1" Height="25px" 
            Width="200px" Font-Size="10pt"
            AutoPostBack="True" onselectedindexchanged="ddlCalcTy_SelectedIndexChanged" 
          >
         <%--  <asp:ListItem Value="-1">--Select--</asp:ListItem>
            <asp:ListItem Value="1">Per Day</asp:ListItem>
            <asp:ListItem Value="2">Per Month</asp:ListItem>
            <asp:ListItem Value="3">Up To 15 Days</asp:ListItem>--%>
        </asp:DropDownList></td>
    </tr>
<tr>
        <td>
            <asp:Label ID="lblcc" runat="server" Text="Commodity Type" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td><asp:DropDownList ID="ddlverity" runat="server" AutoPostBack="True" 
                TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
                onselectedindexchanged="ddlverity_SelectedIndexChanged">
            </asp:DropDownList>
            </td>
        <td>
            <asp:Label ID="lblcommodity" runat="server" Text="Commodity Name" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td><asp:DropDownList ID="ddlcomodity" runat="server" Width="200px" Height="25px" 
                AutoPostBack="True" onselectedindexchanged="ddlcomodity_SelectedIndexChanged">
            </asp:DropDownList>
            </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblFromDate" runat="server" Text="From Date" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtfdate" runat="server" BackColor="LemonChiffon" AutoPostBack="true"
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
                ontextchanged="txtfdate_TextChanged" ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtfdate"></cc1:CalendarExtender>
            </td>
        <td>
            <asp:Label ID="lblTodate" runat="server" Text="To Date" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
        <asp:TextBox ID="txttodate" runat="server" AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
                ontextchanged="txttodate_TextChanged1" ></asp:TextBox>
         <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
        TargetControlID="txttodate"></cc1:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy"  ID="lblpacking" runat="server" Text="Packing Type"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlpacktype" runat="server" TabIndex="1" Height="25px" 
                Width="200px" Font-Size="10pt" 
                >
            </asp:DropDownList></td>
            <td>
            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy"  ID="lblweight" runat="server" Text="Weight"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlweight" runat="server" TabIndex="1" Height="25px" AutoPostBack="true"
            Width="200px" Font-Size="10pt" onselectedindexchanged="ddlweight_SelectedIndexChanged" 
               >
            </asp:DropDownList></td>
    </tr>
<tr>
<td>
<asp:Label ID="lblrunit" runat="server" Visible="false" Text="No of Bags to Reserve" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtrunit" Visible="false" ReadOnly="false" 
        AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" ontextchanged="txtrunit_TextChanged" 
        ></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtrunit"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
</td>
 <td style="position: static;">
            <asp:Label ID="lblm1" Visible="false" runat="server" Text="Total Months: " Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label>
            </td>
        <td style=" position: static;">
            <asp:Label ID="lblmonth" Visible="false" runat="server" Font-Bold="True" ForeColor="blue"></asp:Label>
            </td>
</tr>
<tr>
<td>
     <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="Crop Year:"></asp:Label>
    </td>
<td valign="middle"> 
       <asp:DropDownList ID="ddlcropyr" runat="server" AutoPostBack="True" 
           TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
           onselectedindexchanged="ddlcropyr_SelectedIndexChanged" >
                                                            </asp:DropDownList></td>
<td>
<asp:Label ID="lblcrate" Visible="true" runat="server" Text="Commodity Rate(Month/Day)" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtcomrate" Visible="true" ReadOnly="false" AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" 
        ></asp:TextBox>
        <asp:TextBox runat="server" ID="txtCPRate" Visible="true" ReadOnly="false" AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="90px" Height="20px" 
        ></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtcomrate"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
</td>

</tr>
 <tr visible="true">
              <td>
            <asp:Label Font-Size="8pt" Visible="false" Font-Bold="true" ForeColor="navy" ID="lblDepoCategory" runat="server" Text="Depositor Category"></asp:Label></td>
        <td >
            <asp:DropDownList Enabled="true" ID="ddlDeposCategory" runat="server" AutoPostBack="True" Visible="false" 
                 TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
                onselectedindexchanged="ddlDeposCategory_SelectedIndexChanged" >
                <asp:ListItem Value="-1">--Select--</asp:ListItem>
                <asp:ListItem Value="1">GEN/OBC</asp:ListItem>
                <asp:ListItem Value="2">SC</asp:ListItem>
                <asp:ListItem Value="3">ST</asp:ListItem>
            </asp:DropDownList></td>
         <td>
            <asp:Label ID="lblrebates" Visible="false" runat="server" Text="Rebate %" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtdiscount" Visible="false" runat="server" ReadOnly="false" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="190px" Height="20px"></asp:TextBox>
              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtdiscount"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
            </td>
      
    </tr>
 <tr id="tramount" visible="true">
        <td>
            <asp:Label ID="lblronb" Visible="false" runat="server" Text="Rebate on Bags" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
            <asp:DropDownList Enabled="true" ID="ddlronbags" runat="server" AutoPostBack="True" Visible="false" 
                 TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
                 >
                <asp:ListItem Value="-1">--Select--</asp:ListItem>
                <asp:ListItem Value="0">0 Bags</asp:ListItem>
                <asp:ListItem Value="200">200 Bags(Above 50 Kg)</asp:ListItem>
                <asp:ListItem Value="400">400 Bags(Up To 50 Kg)</asp:ListItem>
            </asp:DropDownList></td>
            <td>
            <asp:Label ID="lblkhasrano"  Visible="false" runat="server" Text="Khasra No" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtkhasrano"  Visible="false" runat="server" ReadOnly="false" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="190px" Height="20px"></asp:TextBox>
             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtkhasrano"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
            </td>
    </tr>
    <tr>
<td>
<asp:Label ID="lblrpn" Visible="false" runat="server" Text="Rin Pustika No." Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtrpn" Visible="false" ReadOnly="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtrpn"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
</td>
<td>
<asp:Label ID="lblCastCert" Visible="false" runat="server" Text="Cast Certificate Ref. No." Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtCastCert" Visible="false" ReadOnly="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px"></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtdisc"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
</td>
</tr>
<tr>
<td>
<asp:Label ID="lblstax" Visible="true" runat="server" Text="GST %" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtstax" Visible="true" ReadOnly="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" 
        ></asp:TextBox>
       <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtstax"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
</td>
<td>
<asp:Label ID="lbldisc" Visible="false" runat="server" Text="Discount %" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtdisc" Visible="false" ReadOnly="false" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px"></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtdisc"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
</td>
</tr>
<tr>
<td>
<asp:Label ID="Label5" Visible="true" runat="server" Text="Bill Unit (In Bag/M.T.)" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
 <asp:DropDownList Enabled="true" ID="ddlBType" runat="server" AutoPostBack="false" Visible="true" 
                 TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
                 >
                <asp:ListItem Value="-1">--Select--</asp:ListItem>
                <asp:ListItem Value="B">In Bag</asp:ListItem>
                <asp:ListItem Value="W">In M.T. </asp:ListItem>
            </asp:DropDownList>
</td>
<td>

</td>
<td>

</td>
</tr>
<tr>

<td>
<asp:Label ID="lblNetAmt" runat="server" Visible="false" Text="Total Amount" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtNetAmt" Visible="false" ReadOnly="true" 
        AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" 
       ></asp:TextBox>
</td>
<td>
<asp:Label ID="lblremark" runat="server" Visible="false" Text="Remark" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtremark" Visible="false" ReadOnly="false" 
        AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" Width="190px" Height="20px" TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="btnAddmore" Visible="false" runat="server" Text="Add More" 
        onclick="btnAddmore_Click"/>
</td>
</tr>
<tr>
    <td align="center" colspan="4">
    
<asp:GridView ID="gvReservation" runat="server" AutoGenerateColumns="False" Visible="false"
             BackColor="White" 
            BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" 
            CellSpacing="1" GridLines="None" onrowdeleting="gvReservation_RowDeleting">
    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
<Columns>
    <asp:BoundField DataField="Commodity_Name" HeaderText="Commodity Name" ItemStyle-Width="100" />
    <asp:BoundField DataField="Commodity_Type" HeaderText="Commodity Type" ItemStyle-Width="100" />
    <asp:BoundField DataField="Commodity" HeaderText="Commodity" ItemStyle-Width="100" />
    <asp:BoundField DataField="FromDate" HeaderText="FromDate" ItemStyle-Width="150" />
    <asp:BoundField DataField="ToDate" HeaderText="ToDate" ItemStyle-Width="150" />
    <asp:BoundField DataField="PackingType" HeaderText="Packing Type" ItemStyle-Width="100" />
    <asp:BoundField DataField="Weight" HeaderText="Weight" ItemStyle-Width="100" />
    <asp:BoundField DataField="FYear" HeaderText="FYear" ItemStyle-Width="100" />
    <asp:BoundField DataField="NofUnit" HeaderText="Storage Qty" ItemStyle-Width="100" />
    <asp:BoundField DataField="CRate" HeaderText="Rate" ItemStyle-Width="100" />
    <asp:BoundField DataField="RMonth" HeaderText="Month" ItemStyle-Width="200"/>
   <%-- <asp:BoundField DataField="Rdays" HeaderText="Days" ItemStyle-Width="100" />--%>
    <asp:BoundField DataField="NetAmt" HeaderText="Total Amount" ItemStyle-Width="100" />
    <asp:BoundField DataField="Remark" HeaderText="Remark" ItemStyle-Width="150" />
    <asp:BoundField DataField="Discount" HeaderText="Discount %" ItemStyle-Width="100" />
    <asp:BoundField DataField="STax" HeaderText="GST %" ItemStyle-Width="100" />
    <asp:TemplateField>
        <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" Text="Delete" runat="server" CommandName="Delete" />
        </ItemTemplate>
        <EditItemTemplate>
            <asp:LinkButton ID="LinkButton2" Text="Update" runat="server" />
            <asp:LinkButton ID="LinkButton3" Text="Cancel" runat="server" />
        </EditItemTemplate>
    </asp:TemplateField>
</Columns>
    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
</asp:GridView>

                                       
                                                        </td>
    </tr>
<tr>
<td align="center" colspan="6">
        <asp:Button ID="btnGenerateBill" runat="server" Text="Submit" Visible="true" Width="100px" 
        CssClass="BTNBLUE" onclick="btnGenerateBill_Click" />
     <%--   <asp:Button ID="btnGVReserveBill123" Visible="true" runat="server" Text="Generate Bills" 
        CssClass="BTNBLUE" />--%>
        <asp:Button ID="btnGVReserveBill" runat="server" Text="Generate Bills" 
            Visible="false" CssClass="BTNBLUE" onclick="btnGVReserveBill_Click" 
            style="width: 137px" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" 
        CssClass="BTNBLUE" onclick="btnCancel_Click"/>
</td>
</tr>
<tr id="trReportsView" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
<center>
<div style="overflow: scroll; height: 500px; overflow-x: hidden">
  
    <rsweb:ReportViewer ID="ReportViewer_SC" runat="server" Width="100%" >
    </rsweb:ReportViewer>
</div>
</center> 
</fieldset>
</td> 
</tr>
<tr id="trRentBill" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 400px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Storage Bill Detail"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                             <asp:GridView ID="gvIStorageCharge" runat="server" AutoGenerateColumns="false">
                                                             <Columns>
                                                             <asp:BoundField HeaderText="Date" dataformatstring="{0:dd/MM/yyyy}" DataField="Deposit_Date" />
                                                             <asp:BoundField HeaderText="Opening Balance" DataField="Opening_Balance" />
                                                             <asp:BoundField HeaderText="Receive Bags" DataField="Receive_Bags" />
                                                             <asp:BoundField HeaderText="Issue Bags" DataField="Issue_Bags" />
                                                             <asp:BoundField HeaderText="Closing Bag Balance" DataField="Closing_Balance" />
                                                             <asp:BoundField HeaderText="Per Day Rate(For Bag)" DataField="Per_Day_Rate" />
                                                             <asp:BoundField HeaderText="Per Day Charges" DataField="Charges" />
                                                             
                                                             <asp:BoundField HeaderText="Godown_Id" DataField="Godown_Id" />
                                                              <asp:BoundField HeaderText="Opening Weight" DataField="Opening_Weight" />
                                                             <asp:BoundField HeaderText="Receive Weight" DataField="Receive_Weight" />
                                                             <asp:BoundField HeaderText="Issue Weight" DataField="Issue_Weight" />
                                                             <asp:BoundField HeaderText="Closing Weight Balance" DataField="Closing_Weight" />
                                                             <asp:BoundField HeaderText="Per Day Rate(For MT)" DataField="Per_Day_Rate_Weight" />
                                                             <asp:BoundField HeaderText="Per Day Charges" DataField="Charges_Weight" />
                                                             </Columns>
                                                             </asp:GridView>
                                                           </td>
                                                           </tr>
                                                           
    
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                            <asp:Button ID="btnGenBill" runat="server" Text="Generate Bill" Visible="false" 
                                                                CssClass="BTNBLUE" onclick="btnGenBill_Click" />
                                                                <asp:Button ID="btncancel2" runat="server" Text="Cancel" Visible="false" Width="110px" 
                                                                CssClass="BTNBLUE" onclick="btncancel2_Click" />
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                            </td>
</tr>
<tr id="trOverAboveDaily" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 400px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Over & Above Bill Detail"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                             <asp:GridView ID="gvOverAboveDaily" runat="server" AutoGenerateColumns="false">
                                                             <Columns>
                                                             <asp:BoundField HeaderText="Date" dataformatstring="{0:dd/MM/yyyy}" DataField="Deposit_Date" />
                                                             <asp:BoundField HeaderText="Opening Balance" DataField="Opening_Balance" />
                                                             <asp:BoundField HeaderText="Receive Bags" DataField="Receive_Bags" />
                                                             <asp:BoundField HeaderText="Issue Bags" DataField="Issue_Bags" />
                                                             <asp:BoundField HeaderText="Closing Bag Balance" DataField="Closing_Balance" />
                                                             <asp:BoundField HeaderText="Reservation In Bag" DataField="Reservation_In_Bag" />
                                                              <asp:BoundField HeaderText="Over Bags" DataField="Over_Bags" />
                                                             <asp:BoundField HeaderText="Per Day Rate" DataField="Per_Day_Rate" />
                                                             <asp:BoundField HeaderText="Total Charges" DataField="Charges" />
                                                             
                                                              <asp:BoundField HeaderText="Godown_Id" DataField="Godown_Id" />
                                                              <asp:BoundField HeaderText="Opening Weight" DataField="Opening_Weight" />
                                                             <asp:BoundField HeaderText="Receive Weight" DataField="Receive_Weight" />
                                                             <asp:BoundField HeaderText="Issue Weight" DataField="Issue_Weight" />
                                                             <asp:BoundField HeaderText="Closing Weight Balance" DataField="Closing_Weight" />
            
                                                             </Columns>
                                                             </asp:GridView>
                                                           </td>
                                                           </tr>
                                                           
    
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                            <asp:Button ID="btnOSubmit" runat="server" Text="Generate Bill" Visible="false" 
                                                                CssClass="BTNBLUE" onclick="btnOSubmit_Click"/>
                                                                <asp:Button ID="btnOCancel" runat="server" Text="Cancel" Visible="false" Width="110px" 
                                                                CssClass="BTNBLUE" onclick="btnOCancel_Click" />
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                            </td>
</tr>
<tr id="TrAccruedOverNAbove" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 400px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Accrued Over & Above Bill Detail"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                             <asp:GridView ID="gvAccruedOverNAbove" runat="server" AutoGenerateColumns="false">
                                                             <Columns>
                                                             <asp:BoundField HeaderText="Date" dataformatstring="{0:dd/MM/yyyy}" DataField="Deposit_Date" />
                                                             <asp:BoundField HeaderText="Opening Balance" DataField="Opening_Balance" />
                                                             <asp:BoundField HeaderText="Receive Bags" DataField="Receive_Bags" />
                                                             <asp:BoundField HeaderText="Issue Bags" DataField="Issue_Bags" />
                                                             <asp:BoundField HeaderText="Closing Bag Balance" DataField="Closing_Balance" />
                                                             <asp:BoundField HeaderText="Reservation In Bag" DataField="Reservation_In_Bag" />
                                                              <asp:BoundField HeaderText="Over Bags" DataField="Over_Bags" />
                                                               <asp:BoundField HeaderText="Storage Period" DataField="Storage_Period" />
                                                               <asp:BoundField HeaderText="Accrued Period" DataField="Accrued_Period" />
                                                             <asp:BoundField HeaderText="Monthly Rate" DataField="Monthly_Rate" />
                                                             <asp:BoundField HeaderText="Total Charges" DataField="Charges" />
                                                             
                                                              <asp:BoundField HeaderText="Godown_Id" DataField="Godown_Id" />
                                                              <asp:BoundField HeaderText="Opening Weight" DataField="Opening_Weight" />
                                                             <asp:BoundField HeaderText="Receive Weight" DataField="Receive_Weight" />
                                                             <asp:BoundField HeaderText="Issue Weight" DataField="Issue_Weight" />
                                                             <asp:BoundField HeaderText="Closing Weight Balance" DataField="Closing_Weight" />
                                                             
                                                              <%-- <asp:BoundField HeaderText="Accrued Period" DataField="Accrued_Period" />--%>
                                                       <%--         <asp:BoundField HeaderText=" New Over Bags" DataField="NOver_Bags" />--%>
                                                             </Columns>
                                                             </asp:GridView>
                                                           </td>
                                                           </tr>
                                                           
    
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                            <asp:Button ID="btnAccruedAbove" runat="server" Text="Generate Bill" Visible="false" 
                                                                CssClass="BTNBLUE" onclick="btnAccruedAbove_Click"/>
                                                                <asp:Button ID="btnCanAcrAbove" runat="server" Text="Cancel" Visible="false" Width="110px" 
                                                                CssClass="BTNBLUE" onclick="btnCanAcrAbove_Click" />
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                            </td>
</tr>
<tr id="TrAccrued" runat="server" visible="false">
                                                    <td colspan="6" valign="top">
                                                     <fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 400px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Accrued Storage Charges Detail"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                             <asp:GridView ID="gvAccruedBill" runat="server" AutoGenerateColumns="false">
                                                            <Columns>
                                                             <asp:BoundField HeaderText="Commodity Name" DataField="Commodity_Name" />
                                                             <asp:BoundField HeaderText="WHR_No"  DataField="Depositor_WHR_Id" />
                                                             <asp:BoundField HeaderText="Date of Deposite" dataformatstring="{0:dd/MM/yyyy}" DataField="Date_of_Deposit" />
                                                             <asp:BoundField HeaderText="Quantity Deposited in Bags" DataField="Deposit_Bags" />
                                                             <asp:BoundField HeaderText="Date of Delivery" dataformatstring="{0:dd/MM/yyyy}" DataField="Delivery_Order_Date" />
                                                             <asp:BoundField HeaderText="Quantity Delivered in Bags" DataField="Deliver_Bags" />
                                                             <asp:BoundField HeaderText="Period Of Storage" DataField="PeriodOfStorage" />
                                                             <asp:BoundField HeaderText="Accrued Period Of Storage" DataField="ChargeOnPeriod" />
                                                             <asp:BoundField HeaderText="Rebate_Bags" DataField="Rebate_Bags" />
                                                             <%--<asp:BoundField HeaderText="Rate of Storage charges " DataField="MonthlyRate" />--%>
                                                             <asp:TemplateField HeaderText="Rate of Storage charges">
                                                                  <ItemTemplate>
                                                                        <asp:TextBox ID="txtMonthlyRate" Text='<%#Bind("MonthlyRate") %>' runat="server" Width="60px" onblur="Spc_validatorInt(this)" MaxLength="5">0</asp:TextBox>
                                                                  </ItemTemplate>
                                                                       <ItemStyle Width="80px" />
                                                             </asp:TemplateField>
                                                            <%--<asp:BoundField HeaderText="Amount of Storage Charges" DataField="Charges" />--%>
                                                              <asp:TemplateField HeaderText="Rebate %">
                                                                  <ItemTemplate>
                                                                        <asp:TextBox ID="txtRebate" runat="server" Width="60px" Text='<%#Bind("Rebate_Perc") %>' onblur="Spc_validatorInt(this)" MaxLength="5"></asp:TextBox>
                                                                  </ItemTemplate>
                                                                       <ItemStyle Width="80px" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Service Tax %">
                                                                  <ItemTemplate>
                                                                       <asp:TextBox ID="txtSTax" runat="server" Text='<%#Bind("Service_Tax") %>' Width="65px" onblur="Spc_validatorInt(this)" MaxLength="12"></asp:TextBox>
                                                                  </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:BoundField HeaderText="Commodity_Id" DataField="Commodity_Id" />
                                                             <asp:BoundField HeaderText="Godown_Id" DataField="Godown_Id" />
                                                             <asp:BoundField HeaderText="Deposit Weight" DataField="Deposit_Weight" />
                                                             <asp:BoundField HeaderText="Deliver Weight" DataField="Deliver_Weight" />
                                                             <asp:BoundField HeaderText="SP_In_Months" DataField="SP_In_Months" />
                                                             <asp:BoundField HeaderText="SP_In_Days" DataField="SP_In_Days" />
                                                             <asp:BoundField HeaderText="AP_In_Months" DataField="AP_In_Months" />
                                                             <asp:BoundField HeaderText="AP_In_Days" DataField="AP_In_Days" />
                                                             </Columns>
                                                             </asp:GridView>
                                                           </td>
                                                           </tr>
                                                           
    
                                                           </table>
                                                             
                                                            </div>
                                                            <table width="100%">
                                                            <tr>
                                                            <td align="center" colspan="2">
                                                            <asp:Button ID="btnAccruedBill" runat="server" Text="Generate Bill" Visible="false" 
                                                                CssClass="BTNBLUE" onclick="btnAccruedBill_Click" />
                                                                <asp:Button ID="btnAccruedCancel" runat="server" Text="Cancel" Visible="false" Width="110px" 
                                                                CssClass="BTNBLUE" onclick="btnAccruedCancel_Click" />
                                                            </td>
                                                           
                                                            </tr>
                                                            </table>
                                                            </center>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
<tr id="trReservationBill" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
<center>
<div style="overflow: scroll; height: 170px; overflow-x: hidden">
                                                           
</div>
</center>
</fieldset>
</td>
</tr>
<%--  <tr>
   <td colspan="2" align="right"><asp:Button ID="btnsubmit" Visible="false" 
           runat="server" Text="Submit" CssClass="BTNBLUE" onclick="btnsubmit_Click"/>
   </td>
   <td colspan="2" align="left"><asp:Button ID="btncancel" Visible="false" 
           runat="server" Text="Cancel" CssClass="BTNBLUE" onclick="btncancel_Click"/>
   </td>
   </tr>--%>
</table>
</div>
        </center>
        </fieldset>
</asp:Content>


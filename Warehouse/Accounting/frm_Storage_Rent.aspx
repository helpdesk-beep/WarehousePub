<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Storage_Rent.aspx.cs" Inherits="Accounting_frm_Storage_Rent" Title="Untitled Page" %>
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
                                        Text="Depositor Rent"></asp:Label></td>
    </tr>
    <tr>
<td>
    <asp:Label ID="lbltod" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Type Of Depositor"></asp:Label>
    </td>
<td align="left" valign="middle"> 
    <asp:DropDownList ID="ddldepositor" runat="server" AutoPostBack="True" 
        TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" 
        onselectedindexchanged="ddldepositor_SelectedIndexChanged">
    </asp:DropDownList></td>
<td> 
    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Name of Depositor"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddldepos_name" runat="server" TabIndex="1" Height="25px" Width="200px" Font-Size="10pt"
            AutoPostBack="True">
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
            <asp:Label ID="lblcommodity" runat="server" Text="Commodity" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td><asp:DropDownList ID="ddlcomodity" runat="server" Width="200px" Height="25px" 
                AutoPostBack="True" onselectedindexchanged="ddlcomodity_SelectedIndexChanged">
            </asp:DropDownList>
            </td>
    </tr>
<tr>
<td>
     <asp:Label ID="lblcropyear" runat="server" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy" Text="Crop Year:"></asp:Label>
    </td>
<td align="left" valign="middle"> 
       <asp:DropDownList ID="ddlcropyr" runat="server" AutoPostBack="True" TabIndex="1" Height="25px" Width="200px" Font-Size="10pt" >
                                                            </asp:DropDownList></td>
<td> 
    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lblbilltype" runat="server" Text="Type of Bill"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddlBillType" runat="server" TabIndex="1" Height="25px" 
            Width="200px" Font-Size="10pt"
            AutoPostBack="True" 
            onselectedindexchanged="ddlBillType_SelectedIndexChanged">
            <asp:ListItem Value="-1">--Select--</asp:ListItem>
            <asp:ListItem Value="1">Reservation Bill</asp:ListItem>
            <asp:ListItem Value="2">Storage Bill</asp:ListItem>
        </asp:DropDownList></td>
</tr>
<tr>
<td>
<asp:Label ID="lblcrate" Visible="true" runat="server" Text="Commodity Rate" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label>
</td>
<td>
<asp:TextBox runat="server" ID="txtcomrate" Visible="true" ReadOnly="true" AutoPostBack="true" BackColor="LemonChiffon" 
        TabIndex="9" CssClass="tb6" Width="190px" Height="20px" 
        ontextchanged="txtcomrate_TextChanged"></asp:TextBox>
         <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtcomrate"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
</td>
</tr>
<tr id="trRentBill" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 200px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Storage Detail"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                             <asp:GridView ID="NewGrid" runat="server" AutoGenerateColumns="false"
                                                                    CellPadding="2" Font-Size="8pt" Width="100%" ForeColor="Navy"                                                                    >
                                                                    <FooterStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Transparent" />
                                                                   <Columns>
                                                                   <%-- <asp:BoundField HeaderText="Depositor WHR Id" DataField="Depositor_WHR_Id" />                                                                
                                                                    <asp:BoundField HeaderText="Commodity ID" DataField="Commodity_ID" />--%>
                                                                    <asp:BoundField HeaderText="Date Of Deposit" DataField="WHR_Date" />
                                                                    <asp:BoundField HeaderText="Date Of Delivery" DataField="Delivery_Order_Date" />
                                                                    <asp:BoundField HeaderText="Deposit Bags" DataField="Deposit_Bags" />
                                                                    <asp:BoundField HeaderText="Deposit Weight" DataField="Deposit_Weight" />
                                                                    <asp:BoundField HeaderText="Deliver Bags" DataField="Deliver_Bags" />
                                                                    <asp:BoundField HeaderText="Deliver Weight" DataField="Deliver_Qty" />
                                                                    <asp:BoundField HeaderText="Period Of Storage" DataField="PeriodOfStorage" />
                                                                    <asp:BoundField HeaderText="Charge On Period" DataField="ChargeOnPeriod" />
                                                                    <asp:BoundField HeaderText="Monthly Rate" DataField="MonthlyRate" />
                                                                    <asp:BoundField HeaderText="Per Day Rate" DataField="PerDayRate" />
                                                                    <asp:BoundField HeaderText="Storage Charge" DataField="Charges" />
                                                                    </Columns>
                                                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                                                    <HeaderStyle BackColor="#719CB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                                        Height="20px" Font-Size="10pt" />
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                </asp:GridView>
                                                           </td>
                                                           </tr>
                                                           
    
                                                           </table>
                                                             
                                                            </div>
                                                            </center>
                                                            </fieldset>
                                                            </td>
</tr>
<tr id="TotalAmt" runat="server" visible="false">
                                                    <td colspan="6" valign="top">
                                                     <fieldset style="width: 980px; border: 1px solid navy;>
                                                    <center>
                                                           <div style="overflow: scroll; height: 100px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
              <td>
            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lblDepoCategory" runat="server" Text="Depositor Category"></asp:Label></td>
        <td >
            <asp:DropDownList Enabled="true" ID="ddlDeposCategory" runat="server" AutoPostBack="True" 
                TabIndex="1" Height="30px" Width="155px" Font-Size="10pt" 
                onselectedindexchanged="ddlDeposCategory_SelectedIndexChanged" >
                <asp:ListItem Value="1">GEN/OBC</asp:ListItem>
                <asp:ListItem Value="2">SC</asp:ListItem>
                <asp:ListItem Value="3">ST</asp:ListItem>
            </asp:DropDownList></td>
         <td class="style2">
            <asp:Label ID="lbldiscount" runat="server" Text="Discount %" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td class="style2">
            <asp:TextBox ID="txtdiscount" runat="server" ReadOnly="True" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="150px" Height="20px"></asp:TextBox></td>
      
    </tr>
                                                         <tr id="tramount" visible="false">
        <td class="style2">
            <asp:Label ID="Label6" runat="server" Text="Total Amount" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td class="style2">
            <asp:TextBox ID="txtTotalCharge" runat="server" ReadOnly="True" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="150px" Height="20px"></asp:TextBox></td>
            <td class="style2">
            <asp:Label ID="lblkhasrano" runat="server" Text="Khasra No" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td class="style2">
            <asp:TextBox ID="txtkhasrano" runat="server" ReadOnly="false" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="150px" Height="20px"></asp:TextBox>
             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtkhasrano"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
            </td>
    </tr>
    <tr>
            <td>
            <asp:Label ID="lblamtobepaid" runat="server" Text="Amount to be Paid " Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtatbepaid" runat="server" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="150px" Height="20px"></asp:TextBox></td>
            <td><asp:Label ID="lblpdate" runat="server" Text="Date Of Payment" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label>
            </td>
            <td><asp:TextBox ID="txtdop" runat="server" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="150px" Height="20px"></asp:TextBox>
             <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd/MM/yyyy" 
        TargetControlID="txtdop"></cc1:CalendarExtender>
            </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label14" runat="server" Text="Paid Amount" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtSamount" runat="server" BackColor="LemonChiffon" AutoPostBack="True" 
                TabIndex="9" CssClass="tb6" Width="150px" Height="20px" ontextchanged="txtSamount_TextChanged" 
                ></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtSamount"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
                </td>
        <td>
          <asp:Label ID="lblRAmount" runat="server" Text="Remaining Amount" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label>
       </td>
        <td>
         <asp:TextBox ID="txtramount" runat="server" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="150px" Height="20px"></asp:TextBox>
            </td>
    </tr>
    
                                                           </table>
                                                             
                                                            </div>
                                                            </center>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
<tr id="trReservationBill" visible="false" runat="server">
<td colspan="4">
<fieldset style="width: 980px; border: 1px solid navy;">
<center>
<div style="overflow: scroll; height: 170px; overflow-x: hidden">
                                                           <table style="width: 100%">
<tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Reservation Detail"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
        <td>
            <asp:Label ID="Label12" runat="server" Text="Reserve From" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtfdate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  Format="dd/MM/yyyy"
        TargetControlID="txtfdate"></cc1:CalendarExtender>
            </td>
        <td>
            <asp:Label ID="Label13" runat="server" Text="Reserved Till" Font-Bold="True" Font-Size="8pt" 
                                                                ForeColor="Navy"></asp:Label></td>
        <td>
        <asp:TextBox ID="txttodate" runat="server" AutoPostBack="true" ontextchanged="txttodate_TextChanged"></asp:TextBox>
         <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
        TargetControlID="txttodate"></cc1:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy"  ID="Label3" runat="server" Text="Packing Type"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlpacktype" runat="server" TabIndex="1" Height="25px" 
                Width="200px" Font-Size="10pt" 
                onselectedindexchanged="ddlpacktype_SelectedIndexChanged">
            </asp:DropDownList></td>
            <td align="left">
            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy"  ID="Label4" runat="server" Text="Weight"></asp:Label></td>
        <td>
            <asp:DropDownList ID="ddlweight" runat="server" TabIndex="1" Height="25px" 
            Width="200px" Font-Size="10pt">
            </asp:DropDownList></td>
    </tr>
    <tr>
    <td> <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy"  ID="Label5" runat="server" Text="Quantity/Unit"></asp:Label></td>
    <td><asp:TextBox ID="txtqty" runat="server" ontextchanged="txtqty_TextChanged" AutoPostBack="true"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtqty"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender></td>
     <td> <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy"  ID="lblrate" runat="server" Text="Rate"></asp:Label></td>
    <td> <asp:TextBox ID="txtcrate" AutoPostBack="true" runat="server" ontextchanged="txtcrate_TextChanged"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtcrate"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender></td>
    </tr>
    <tr>
       <td>
            <asp:Label ID="Label15" runat="server" Text="Confirm Reservation" Width="164px" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td><asp:RadioButtonList ID="rdbadpay" runat="server" RepeatDirection="Horizontal"
                RepeatLayout="Flow" AutoPostBack="True" 
                onselectedindexchanged="rdbadpay_SelectedIndexChanged">
            <asp:ListItem Value="Y">Yes</asp:ListItem>
<%--            <asp:ListItem Value="N">No</asp:ListItem>--%>
        </asp:RadioButtonList></td>
        <td style="position: static;">
            <asp:Label ID="Label42" runat="server" Text="Total Months: " ForeColor="Navy"></asp:Label>
            <asp:Label ID="lblmonth" runat="server" Font-Bold="True" ForeColor="blue"></asp:Label></td>
        <td style=" position: static;">
            <asp:Label ID="Label43" runat="server" Text="Total Days:" ForeColor="#000040"></asp:Label>
            <asp:Label ID="lbldays" runat="server" Font-Bold="True" ForeColor="blue"></asp:Label></td>
    </tr>
   <tr>
   <td><asp:Label runat="server" Text="Amount Per Month" ID="lblamount" Width="164px" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
   <td><asp:TextBox runat="server" ID="txtamount" ReadOnly="true"></asp:TextBox>
    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtamount"
                                        ValidChars="0123456789.">
                                    </cc1:FilteredTextBoxExtender>
   </td>
    <td><asp:Label runat="server" Text="Amount To Be Paid" ID="lbltamt" Width="164px" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
   <td><asp:TextBox runat="server" ID="txttamt" ReadOnly="true"></asp:TextBox>
   </td>
   </tr>
 
</table>
</div>
</center>
</fieldset>
</td>
</tr>
  <tr>
   <td colspan="2" align="right"><asp:Button ID="btnsubmit" Visible="false" 
           runat="server" Text="Submit" CssClass="BTNBLUE" onclick="btnsubmit_Click"/>
   </td>
   <td colspan="2" align="left"><asp:Button ID="btncancel" Visible="false" 
           runat="server" Text="Cancel" CssClass="BTNBLUE" onclick="btncancel_Click"/>
   </td>
   </tr>
</table>
</div>
        </center>
        </fieldset>
</asp:Content>


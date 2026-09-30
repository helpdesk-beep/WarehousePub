<%@ Page Language="C#" MasterPageFile="~/MasterPage/Gdwn.master" AutoEventWireup="true" CodeFile="frm_Godown_Rent.aspx.cs" Inherits="Depot_frm_Godown_Rent" Title="Untitled Page" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<fieldset style="width: 1000px; border: 2px solid navy;">
        <center>
<div>
<table style="width: 100%;">
 <tr style="background-color: #0bb6e6; height: 25px">
        <td colspan="4" align="center" class="style2">
            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Depositor Godown Rent"></asp:Label></td>
    </tr>
    <tr>
<td class="style2">
    <asp:Label ID="Label7" Font-Size="8pt" Font-Bold="true" ForeColor="navy" runat="server" Text="Type Of Depositor"></asp:Label>
    </td>
<td align="left" valign="middle" class="style4"> 
    <asp:DropDownList ID="ddldepositor" runat="server" AutoPostBack="True" TabIndex="1" Height="30px" Width="200px" CssClass="tb6" Font-Size="10pt" 
        onselectedindexchanged="ddldepositor_SelectedIndexChanged">
    </asp:DropDownList></td>
<td align="center" class="style2"> 
    <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="Label8" runat="server" Text="Name of Depositor"></asp:Label></td>
    <td class="style2">
        <asp:DropDownList ID="ddldepos_name" runat="server" TabIndex="1" Height="30px" Width="200px" CssClass="tb6" Font-Size="10pt"
            AutoPostBack="True" 
            onselectedindexchanged="ddldepos_name_SelectedIndexChanged">
        </asp:DropDownList></td>
</tr>
<tr>
              <td>
            <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lblDepoCategory" runat="server" Text="Depositor Category"></asp:Label></td>
        <td >
            <asp:DropDownList Enabled="false" ID="ddlDeposCategory" runat="server" AutoPostBack="True" 
                TabIndex="1" Height="30px" Width="200px" CssClass="tb6" Font-Size="10pt" onselectedindexchanged="ddlDeposCategory_SelectedIndexChanged"
               >
                <asp:ListItem Value="1">GEN</asp:ListItem>
                <asp:ListItem Value="2">OBC</asp:ListItem>
                <asp:ListItem Value="3">SC</asp:ListItem>
                <asp:ListItem Value="4">ST</asp:ListItem>
            </asp:DropDownList></td>
            <td align="center">
           <asp:Label Font-Size="8pt" Font-Bold="true" ForeColor="navy" ID="lblgodown" runat="server" Text="Godown"></asp:Label>
            </td>
            <td>
            <asp:DropDownList ID="ddlgodown" runat="server" AutoPostBack="True" TabIndex="1" Height="30px" Width="200px" CssClass="tb6" Font-Size="10pt"
                    onselectedindexchanged="ddlgodown_SelectedIndexChanged">
            </asp:DropDownList></td>
      
    </tr>
    <tr>
    <td align="center" colspan="6"><b><asp:Label ID="lblmsg" runat="server" Visible="false" 
            CssClass="style5"></asp:Label></b></td>
    </tr>
                                                   
                                                    <tr id="RentDetail" runat="server" visible="false">
                                                    <td colspan="6" valign="top">
                                                     <fieldset style="width: 980px; border: 1px solid navy;">
                                                    <center>
                                                           <div style="overflow: scroll; height: 200px; overflow-x: hidden">
                                                           <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                           <tr>
                                                           <td colspan="6" align="center"  style="background-color: #0bb6e6; height: 25px">
                                                           <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="15pt" ForeColor="WhiteSmoke"
                                        Text="Rent Detail"></asp:Label>
                                                           </td>
                                                           </tr>
                                                           <tr>
                                                           <td colspan="6" align="center">
                                                             <asp:GridView ID="NewGrid" runat="server" AutoGenerateColumns="false"
                                                                    CellPadding="2" Font-Size="8pt" Width="100%" ForeColor="Navy" onselectedindexchanged="GVGRent_SelectedIndexChanged"                                                                    >
                                                                    <FooterStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Transparent" />
                                                                   <Columns>
                                                                    <asp:BoundField HeaderText="Depositor WHR Id" DataField="Depositor_WHR_Id" />                                                                
                                                                    <asp:BoundField HeaderText="Commodity ID" DataField="Commodity_ID" />
                                                                    <asp:BoundField HeaderText="Deposit Bags" DataField="DepositBags" />
                                                                    <asp:BoundField HeaderText="Deposit Weight" DataField="DepositWeight" />
                                                                    <asp:BoundField HeaderText="Deliver Bags" DataField="DeliverBags" />
                                                                    <asp:BoundField HeaderText="Deliver Weight" DataField="DeliverWeight" />
                                                                    <asp:BoundField HeaderText="Date Of Deposit" DataField="DateOfDeposit" />
                                                                    <asp:BoundField HeaderText="Date Of Delivery" DataField="DateOfDelivery" />
                                                                    <asp:BoundField HeaderText="Period Of Storage" DataField="PeriodOfStorage" />
                                                                    <asp:BoundField HeaderText="Monthly Rate" DataField="MonthlyRate" />
                                                                    <asp:BoundField HeaderText="Weekly Rate" DataField="WeeklyRate" />
                                                                    <asp:BoundField HeaderText="Storage Charge" DataField="Charge" />
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
                                                         <tr id="tramount" visible="false">
        <td class="style2">
            <asp:Label ID="lblrate" runat="server" Text="Total Amount" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td class="style2">
            <asp:TextBox ID="txtTotalCharge" runat="server" ReadOnly="True" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="150px" Height="20px"></asp:TextBox></td>
            <td class="style2">
            <asp:Label ID="lbldiscount" runat="server" Text="Discount %" Font-Size="8pt" Font-Bold="true" ForeColor="navy"></asp:Label></td>
        <td class="style2">
            <asp:TextBox ID="txtdiscount" runat="server" ReadOnly="True" BackColor="LemonChiffon" TabIndex="9" CssClass="tb6" Width="150px" Height="20px"></asp:TextBox></td>
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
            <asp:TextBox ID="txtamount" runat="server" BackColor="LemonChiffon" AutoPostBack="True" 
                TabIndex="9" CssClass="tb6" Width="150px" Height="20px" 
                ontextchanged="txtamount_TextChanged"></asp:TextBox></td>
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
                                                  
   
<tr>
        <td colspan="4">
            <asp:Label ID="lblerror" runat="server" Visible="false" Font-Bold="True" Font-Italic="True" ForeColor="Maroon"></asp:Label></td>
    </tr>
    <tr>
        <td>
        </td>
        <td align="right">
            <asp:Button ID="btnSumbmitRent" runat="server" Text="Save" Width="141px" 
                ValidationGroup="1" TabIndex="21" CssClass="BTNBLUE" Enabled="true" 
                onclick="btnSumbmitRent_Click"/></td>
        <td align="right">
            <asp:Button ID="brnCancel" runat="server" Text="Close" Width="137px" 
                TabIndex="21" CssClass="BTNBLUE" onclick="brnCancel_Click" /></td>
        <td>
        </td>
    </tr>
</table>
</div>
</center>
</fieldset>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="head">

    <style type="text/css">
        .style2
        {
            height: 26px;
        }
        .style4
        {
            width: 200px;
            height: 26px;
        }
        .style5
        {
            color: #FF0000;
        }
    </style>

</asp:Content>


